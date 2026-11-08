using System;
using System.Collections.Generic;
using System.Linq;
using Lemuria.Core;
using Lemuria.Crystals;
using Lemuria.Sparkstone;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;

namespace Lemuria.Machines;

/// <summary>
/// A Gather or Focus working an Elemental battery (design note "Lemuria step 4 part 3", Filling and
/// draining, The count): the fill takes only what lifts its network above three quarters and stores it,
/// the drain pays back 4 in 5 only while its network is below half, never past it, each moving up to
/// 200 an in-game hour for a Glittering crystal and 600 for a Glowing one. Both stones keep the count
/// and show it; a stone working a battery does nothing else. Fire's lava rises from the bottom up and
/// cools back into the rock each block was; Ice freezes from the top down; Earth stores fed charge in
/// its weak blocks and, once that is spent, wears its deposit a block at a time at a Gather block's
/// pace, its byproducts into a container touching the drain. Server-authoritative; clients get the
/// count and why it rests through the block entity sync, for the look-at text.
/// </summary>
public partial class BlockEntityMachine
{
    private static readonly AssetLocation LavaCode = new("game", "lava-still-7");
    private static readonly AssetLocation WaterCode = new("game", "water-still-7");
    private static readonly AssetLocation GlacierIceCode = new("game", "glacierice");
    private static readonly AssetLocation SizzleSound = new("game", "sounds/sizzle");

    private Battery.State? battery;
    private double sinceBattery = double.MaxValue;
    private double lastBatteryHours = -1;
    private bool inBattery;

    // Shown on clients: the room's Elemental, capacity and why it rests.
    private string? batteryElemental;
    private float batteryCapacity;
    private string? batteryProblem;
    private bool batteryShown;
    private float batteryDeposit;
    private bool batteryNoChest;

    /// <summary>What an Earth battery's deposit still holds to wear out, on the last look (play-test, Nov 8).</summary>
    public float BatteryDeposit => batteryDeposit;

    /// <summary>The battery's count as this stone keeps it, or null when it holds none.</summary>
    public Battery.State? BatteryState => battery;

    /// <summary>Why the battery this stone faces into rests, a lang key's last part, or null.</summary>
    public string? BatteryProblem => batteryProblem;

    /// <summary>Whether this stone faced into a battery room on its last look.</summary>
    public bool InBattery => inBattery;

    /// <summary>The room's capacity on the last look.</summary>
    public float BatteryCapacity => batteryCapacity;

    /// <summary>
    /// The battery's look every 10 seconds: true while this stone faces into a battery room, working it
    /// or resting, so the stone does nothing else.
    /// </summary>
    private bool BatteryTick(float dt)
    {
        if (Form is not (Machine.Gather or Machine.Focus))
        {
            return false;
        }
        if ((sinceBattery += dt) < SparkstoneNetworks.TickMs / 1000.0)
        {
            return inBattery;
        }
        sinceBattery = 0;
        WorkBattery();
        return inBattery;
    }

    /// <summary>One look at the battery: finds and checks the room, then fills or drains for the hours since. Public for the test command.</summary>
    public Battery.Room? WorkBattery(double? forHours = null)
    {
        double now = Api.World.Calendar.TotalHours;
        double hours = forHours ?? (lastBatteryHours < 0 ? 0 : Math.Max(0, now - lastBatteryHours));
        lastBatteryHours = now;
        var room = Battery.Find(this, check: false);
        inBattery = room != null;
        if (room == null)
        {
            ShowBattery(null, 0, null);
            return null;
        }
        var state = Shared(room);
        // Only an empty battery takes a new fill's alignment; a held count keeps its own, the others' crystals idle.
        Battery.Check(this, room, state.Stored > 0.01f ? state.Elemental : null);
        if (room.Elemental != null && state.Elemental != room.Elemental)
        {
            state.Elemental = room.Elemental;
            state.Rocks.Clear();
            state.WearIndex = -1;
            state.Wear = 0;
        }
        float capacity = room.Elemental == null ? 0 : RoomCapacity(room, state);
        if (room.Problem == null && room.Elemental != null && hours > 0)
        {
            bool changed = Form == Machine.Focus ? Fill(room, state, capacity, hours) : Drain(room, state, hours);
            if (changed)
            {
                state.Rev++;
                capacity = RoomCapacity(room, state);
            }
        }
        float deposit = room.Elemental == Crystal.Earth ? Deposit(room, state) : 0;
        foreach (var stone in room.Stones)
        {
            stone.battery = state;
            stone.ShowBattery(room.Elemental, capacity, room.Problem, deposit);
        }
        return room;
    }

    /// <summary>The count the room's stones keep: the newest of theirs that names this room, or a new one.</summary>
    private Battery.State Shared(Battery.Room room)
    {
        string key = room.Key;
        var newest = room.Stones.Select(s => s.battery).Where(b => b != null && b.Key == key).OrderByDescending(b => b!.Rev).FirstOrDefault();
        return newest ?? new Battery.State { Key = key, Elemental = room.Elemental ?? "" };
    }

    /// <summary>What a room holds: a block's worth for each block of Fire or Ice, and for each weak block of Earth (question 5).</summary>
    private float RoomCapacity(Battery.Room room, Battery.State state)
    {
        if (room.Elemental != Crystal.Earth)
        {
            return room.Volume * Machine.BatteryPerBlock;
        }
        var accessor = Api.World.BlockAccessor;
        int weak = 0;
        for (int i = 0; i < room.Volume; i++)
        {
            if (i != state.WearIndex && Battery.Classify(accessor, room.At(i)) == Battery.Cell.Weak)
            {
                weak++;
            }
        }
        return weak * Machine.BatteryPerBlock;
    }

    /// <summary>
    /// The charge an Earth room's deposit still holds: 50 for each step its blocks have left to wear to
    /// sand (ore 4, rock 3, cracked rock 2, gravel 1), less what the block being worn has paid.
    /// </summary>
    private float Deposit(Battery.Room room, Battery.State state)
    {
        var accessor = Api.World.BlockAccessor;
        int steps = 0;
        for (int i = 0; i < room.Volume; i++)
        {
            var block = accessor.GetBlock(room.At(i), BlockLayersAccess.Solid);
            if (block?.Code?.Domain != "game")
            {
                continue;
            }
            string path = block.Code.Path;
            steps += path.StartsWith("ore-") ? 4 : path.StartsWith("rock-") ? 3 : path.StartsWith("crackedrock-") ? 2 : path.StartsWith("gravel-") ? 1 : 0;
        }
        return Math.Max(0, steps * Machine.EarthStep - state.Wear);
    }

    private void ShowBattery(string? elemental, float capacity, string? problem, float deposit = 0)
    {
        bool shown = elemental != null || problem != null || inBattery;
        float stored = battery?.Stored ?? 0;
        int idle = elemental == null ? 0 : Working.Count(c => c.Alignment != elemental);
        if (contextDirty)
        {
            ReadContext();
        }
        // An Earth drain needs a container touching it for the deposit's byproducts; say so when it has none.
        bool noChest = elemental == Crystal.Earth && Form == Machine.Gather && Output == null;
        if (shown == batteryShown && elemental == batteryElemental && problem == batteryProblem && Math.Abs(capacity - batteryCapacity) < 0.5f
            && Math.Abs(stored - lastShownStored) < 1f && idle == batteryIdle && Math.Abs(deposit - batteryDeposit) < 1f && noChest == batteryNoChest)
        {
            return;
        }
        batteryDeposit = deposit;
        batteryNoChest = noChest;
        bool redraw = elemental != batteryElemental || shown != batteryShown;
        batteryIdle = idle;
        batteryShown = shown;
        batteryElemental = elemental;
        batteryCapacity = capacity;
        batteryProblem = problem;
        lastShownStored = stored;
        // The plugs light only for crystals of the battery's Elemental, so a change of Elemental redraws them.
        MarkDirty(redraw);
    }

    private int batteryIdle;

    /// <summary>Crystals in this stone that sit idle because they are not the battery's Elemental (J4).</summary>
    public int BatteryIdle => batteryIdle;

    /// <summary>The Elemental of the battery this stone works, as last shown, or null.</summary>
    public string? BatteryElemental => batteryShown ? batteryElemental : null;

    private float lastShownStored;

    /// <summary>The crystals of the battery's Elemental that move charge here, and the charge they move an hour.</summary>
    private double Pace(string elemental, bool fill)
    {
        int amp = AmplifyRank();
        double rate = 0;
        foreach (var crystal in Working.Where(c => c.Alignment == elemental))
        {
            int rank = Crystal.Rank(crystal.Evolution);
            if (fill && elemental == Crystal.Fire && rank + (amp >= rank ? 1 : 0) < 3)
            {
                // Melting rock takes that power: a Glowing Fire crystal and an Amplify (question 3).
                continue;
            }
            rate += Machine.BatteryPerHour(crystal.Evolution);
        }
        return rate;
    }

    /// <summary>The fill stores what lifts its network above three quarters, as fast as its crystals move it, while the room has room.</summary>
    private bool Fill(Battery.Room room, Battery.State state, float capacity, double hours)
    {
        if (SparkstoneModSystem.Networks is not { } networks)
        {
            return false;
        }
        double pace = Pace(room.Elemental!, true) * hours;
        var network = networks.NetworkOf(this);
        var (total, networkCapacity) = SparkstoneNetworks.Measure(network);
        float move = (float)Math.Min(Math.Min(total - Machine.BatteryFillAbove * networkCapacity, pace), capacity - state.Stored);
        if (move <= 0.001f)
        {
            return false;
        }
        int before = Charged(state.Stored);
        networks.Update(network, feed: false, change: -move);
        state.Stored += move;
        Turn(room, state, before, Charged(state.Stored));
        Sparks(room.Elemental!, 3);
        return true;
    }

    /// <summary>
    /// The drain pays back 4 in 5 of what was stored while its network is below half, never past it;
    /// once an Earth battery's fed charge is spent, it wears the deposit at a Gather block's pace.
    /// </summary>
    private bool Drain(Battery.Room room, Battery.State state, double hours)
    {
        if (SparkstoneModSystem.Networks is not { } networks)
        {
            return false;
        }
        var network = networks.NetworkOf(this);
        var (total, networkCapacity) = SparkstoneNetworks.Measure(network);
        float need = Machine.BatteryDrainBelow * networkCapacity - total;
        if (need <= 0.001f)
        {
            return false;
        }
        bool changed = false;
        float back = Machine.BatteryReturn;
        if (state.Stored > 0.001f && back > 0)
        {
            float take = (float)Math.Min(Math.Min(state.Stored, Pace(room.Elemental!, false) * hours), need / back);
            if (take > 0.001f)
            {
                int before = Charged(state.Stored);
                state.Stored -= take;
                if (state.Stored < 0.01f)
                {
                    state.Stored = 0;
                }
                networks.Update(network, feed: false, change: take * back);
                need -= take * back;
                Turn(room, state, before, Charged(state.Stored));
                changed = true;
            }
        }
        if (room.Elemental == Crystal.Earth && state.Stored <= 0.001f && need > 0.001f)
        {
            float paid = Wear(room, state, hours, need);
            if (paid > 0)
            {
                networks.Update(network, feed: false, change: paid);
                changed = true;
            }
        }
        if (changed)
        {
            Sparks(room.Elemental!, 3);
        }
        return changed;
    }

    /// <summary>How many blocks a count has turned: a block turns as soon as the fill starts on it.</summary>
    private static int Charged(float stored) => (int)Math.Ceiling(stored / Machine.BatteryPerBlock - 0.0001);

    /// <summary>Turns Fire's or Ice's blocks as the count crosses them: up into the charged form, or back down.</summary>
    private void Turn(Battery.Room room, Battery.State state, int from, int to)
    {
        if (from == to || room.Elemental == Crystal.Earth)
        {
            return;
        }
        var order = room.FillOrder();
        var world = Api.World;
        var accessor = world.BlockAccessor;
        Block? sound = null;
        for (int n = from; n < to && n < order.Count; n++)
        {
            int index = order[n];
            var pos = room.At(index);
            var cell = Battery.Classify(accessor, pos);
            if (room.Elemental == Crystal.Fire && cell == Battery.Cell.Rock && world.GetBlock(LavaCode) is { } lava)
            {
                state.Rocks[index] = accessor.GetBlock(pos, BlockLayersAccess.Solid).Code.ToShortString();
                accessor.SetBlock(0, pos);
                accessor.SetBlock(lava.BlockId, pos, BlockLayersAccess.Fluid);
                accessor.TriggerNeighbourBlockUpdate(pos);
                Api.Logger.Audit("{0} melted rock to lava in a Fire battery at {1}", CarverName, pos);
                sound = lava;
            }
            else if (room.Elemental == Crystal.Ice && cell == Battery.Cell.Water && world.GetBlock(GlacierIceCode) is { } ice)
            {
                accessor.SetBlock(0, pos, BlockLayersAccess.Fluid);
                accessor.SetBlock(ice.BlockId, pos);
                accessor.TriggerNeighbourBlockUpdate(pos);
                sound = ice;
            }
        }
        for (int n = from - 1; n >= to && n >= 0; n--)
        {
            if (n >= order.Count)
            {
                continue;
            }
            int index = order[n];
            var pos = room.At(index);
            var cell = Battery.Classify(accessor, pos);
            if (room.Elemental == Crystal.Fire && cell == Battery.Cell.Lava)
            {
                string code = state.Rocks.TryGetValue(index, out var c) ? c : state.Rocks.Values.FirstOrDefault() ?? "game:rock-granite";
                if (world.GetBlock(new AssetLocation(code)) is not { } rock)
                {
                    continue;
                }
                state.Rocks.Remove(index);
                accessor.SetBlock(0, pos, BlockLayersAccess.Fluid);
                accessor.SetBlock(rock.BlockId, pos);
                accessor.TriggerNeighbourBlockUpdate(pos);
                sound = rock;
            }
            else if (room.Elemental == Crystal.Ice && cell == Battery.Cell.Ice && world.GetBlock(WaterCode) is { } water)
            {
                accessor.SetBlock(0, pos);
                accessor.SetBlock(water.BlockId, pos, BlockLayersAccess.Fluid);
                accessor.TriggerNeighbourBlockUpdate(pos);
                sound = water;
            }
        }
        if (sound != null)
        {
            var at = room.At(order[Math.Clamp(Math.Min(from, to), 0, order.Count - 1)]);
            var location = room.Elemental == Crystal.Fire ? SizzleSound : sound.Sounds?.Place.Location;
            if (location != null)
            {
                world.PlaySoundAt(location, at.X + 0.5, at.Y + 0.5, at.Z + 0.5, null, true, 16f, LemuriaSound.Volume(0.6f));
            }
        }
    }

    /// <summary>
    /// Earth's deposit (question 6): new charge at a Gather block's pace, while there is a block to wear
    /// and a container touching the drain; each 50 paid wears the block one step, its byproducts into
    /// the container. A step whose byproducts don't fit waits, paying nothing more, so nothing is lost.
    /// Returns the charge paid.
    /// </summary>
    private float Wear(Battery.Room room, Battery.State state, double hours, float need)
    {
        var world = Api.World;
        if (contextDirty)
        {
            ReadContext();
        }
        if (Output is not { } output)
        {
            return 0;
        }
        float step = Machine.EarthStep;
        float paid = 0;
        if (state.Wear < step)
        {
            if (Target(room, state) < 0)
            {
                return 0;
            }
            double pace = Working.Where(c => c.Alignment == Crystal.Earth).Sum(c => Machine.GatherPerDay(c.Evolution)) * hours / world.Calendar.HoursPerDay;
            paid = (float)Math.Min(Math.Min(pace, need), step - state.Wear);
            state.Wear += paid;
        }
        while (state.Wear >= step - 0.001f)
        {
            int index = Target(room, state);
            if (index < 0)
            {
                break;
            }
            var pos = room.At(index);
            if (Battery.WearStep(world, pos) is not var (into, byproducts) || !Stock.Fits(world, output, byproducts))
            {
                break;
            }
            foreach (var stack in Stock.Merge(world, byproducts))
            {
                Stock.Put(world, output, stack);
            }
            world.BlockAccessor.SetBlock(into.BlockId, pos);
            state.Wear -= step;
            if (Battery.WearStep(world, pos) == null)
            {
                state.WearIndex = -1;
            }
            if (into.Sounds?.Break.Location is { } sound)
            {
                world.PlaySoundAt(sound, pos.X + 0.5, pos.Y + 0.5, pos.Z + 0.5, null, true, 16f, LemuriaSound.Volume(0.5f));
            }
        }
        return paid;
    }

    /// <summary>The block the drain wears: the one it started on, or the wearable block nearest its corner (rock, ore, cracked rock, gravel). -1 for none left.</summary>
    private int Target(Battery.Room room, Battery.State state)
    {
        var world = Api.World;
        if (state.WearIndex >= 0 && state.WearIndex < room.Volume && Battery.WearStep(world, room.At(state.WearIndex)) != null
            && Battery.Classify(world.BlockAccessor, room.At(state.WearIndex)) != Battery.Cell.Other)
        {
            return state.WearIndex;
        }
        state.WearIndex = -1;
        var corner = room.DrainCorner;
        int best = -1;
        int bestDistance = int.MaxValue;
        for (int i = 0; i < room.Volume; i++)
        {
            var pos = room.At(i);
            int distance = pos.ManhattanDistance(corner);
            if (distance < bestDistance && Battery.Wearable(world, pos))
            {
                best = i;
                bestDistance = distance;
            }
        }
        state.WearIndex = best;
        return best;
    }

    // ------------------------------------------------------------------ saved and shown

    private void BatteryToTree(ITreeAttribute tree)
    {
        if (battery != null)
        {
            var sub = new TreeAttribute();
            battery.ToTree(sub);
            tree["battery"] = sub;
        }
        if (batteryShown)
        {
            tree.SetBool("batteryShown", true);
            tree.SetString("batteryElemental", batteryElemental ?? "");
            tree.SetFloat("batteryCapacity", batteryCapacity);
            tree.SetString("batteryProblem", batteryProblem ?? "");
            tree.SetInt("batteryIdle", batteryIdle);
            tree.SetFloat("batteryDeposit", batteryDeposit);
            tree.SetBool("batteryNoChest", batteryNoChest);
        }
    }

    private void BatteryFromTree(ITreeAttribute tree)
    {
        var sub = tree.GetTreeAttribute("battery");
        var loaded = sub == null ? null : Battery.State.FromTree(sub);
        // A newer count from a partner stone is kept over an older one this stone saved.
        if (battery == null || loaded == null || loaded.Rev >= battery.Rev || battery.Key != loaded.Key)
        {
            battery = loaded;
        }
        bool wasShown = batteryShown;
        string? wasElemental = batteryElemental;
        batteryShown = tree.GetBool("batteryShown");
        string elemental = tree.GetString("batteryElemental", "");
        batteryElemental = elemental.Length == 0 ? null : elemental;
        batteryIdle = tree.GetInt("batteryIdle");
        batteryDeposit = tree.GetFloat("batteryDeposit");
        batteryNoChest = tree.GetBool("batteryNoChest");
        if (Api?.Side == EnumAppSide.Client && (wasShown != batteryShown || wasElemental != batteryElemental))
        {
            MarkDirty(true);
        }
        batteryCapacity = tree.GetFloat("batteryCapacity");
        string problem = tree.GetString("batteryProblem", "");
        batteryProblem = problem.Length == 0 ? null : problem;
    }

    private void BatteryInfo(System.Text.StringBuilder dsc)
    {
        if (!batteryShown)
        {
            return;
        }
        string prefix = LemuriaModSystem.ModId + ":";
        if (batteryElemental != null)
        {
            dsc.AppendLine(Lang.Get(prefix + "battery-count", Lang.Get(prefix + "alignment-" + batteryElemental), Math.Round(battery?.Stored ?? 0).ToString("#,0"), Math.Round(batteryCapacity).ToString("#,0")));
        }
        if (batteryElemental == Crystal.Earth && batteryDeposit >= 1)
        {
            dsc.AppendLine(Lang.Get(prefix + "battery-deposit", Math.Round(batteryDeposit).ToString("#,0")));
        }
        if (batteryNoChest)
        {
            dsc.AppendLine(Lang.Get(prefix + "battery-nochest"));
        }
        if (batteryProblem != null)
        {
            dsc.AppendLine(Lang.Get(prefix + "battery-rests-" + batteryProblem));
        }
        if (batteryIdle > 0)
        {
            dsc.AppendLine(Lang.Get(prefix + "battery-idle", batteryIdle));
        }
    }
}
