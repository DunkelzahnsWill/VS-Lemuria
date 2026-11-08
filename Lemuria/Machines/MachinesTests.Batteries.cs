using System.Linq;
using Lemuria.Core;
using Lemuria.Crystals;
using Lemuria.Sparkstone;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace Lemuria.Machines;

/// <summary>
/// Step 4 part 3's scenes (design note "Lemuria step 4 part 3", How it is built: Smoke tests): a Fire, an
/// Ice and an Earth battery in test granite, a room with a gap, and a Wind Probe on a mast. Each builds
/// its stones with no player, gives the hours by hand and reports to the log.
/// </summary>
public partial class MachinesTests
{
    private const string IceBatteryKey = LemuriaModSystem.ModId + "-machinestest-icebattery";

    /// <summary>Free blocks in a row from a position, each joining the one before, to give a network room.</summary>
    private void Line(BlockPos from, BlockFacing direction, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Free(from.AddCopy(direction.Normali.X * i, direction.Normali.Y * i, direction.Normali.Z * i));
        }
    }

    private int CountIn(BlockPos min, int sizeX, int sizeY, int sizeZ, System.Func<BlockPos, bool> match)
    {
        int n = 0;
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                for (int z = 0; z < sizeZ; z++)
                {
                    n += match(min.AddCopy(x, y, z)) ? 1 : 0;
                }
            }
        }
        return n;
    }

    private bool IsLava(BlockPos pos) => Accessor.GetBlock(pos, BlockLayersAccess.Fluid).LiquidCode == "lava";

    /// <summary>
    /// A 2×2×1 battery in test granite: which Elemental its crystals make it (the owner, Nov 2: the crystal
    /// decides; Nov 8: the fill's crystals win), then as a Fire battery the fill stores what lifts its line above three quarters as lava,
    /// the drain pays a second line up to half and the lava cools back to granite; a gap in the shell rests it.
    /// </summary>
    public void FireBattery(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column).AddCopy(0, -6, 0);
        Solid(origin, 8, 4, 8);
        var corner = origin.AddCopy(3, 1, 3);
        // The drain faces east into the room's corner; the fill faces west into the corner opposite.
        var drain = Place(corner.AddCopy(-1, 0, 0), Machine.Gather, "east");
        var fill = Place(corner.AddCopy(2, 0, 1), Machine.Focus, "west");
        if (drain == null || fill == null)
        {
            return;
        }
        var amplify = Place(fill.Pos.AddCopy(0, 0, 1), Machine.Amplify, "north");
        Put(amplify, Crystal.Fire, Crystal.Glowing);
        // Each stone's line runs away from the room, the fill's sixteen blocks, the drain's seven.
        Line(fill.Pos.AddCopy(1, 0, 0), BlockFacing.UP, 3);
        Line(fill.Pos.AddCopy(1, 3, 0), BlockFacing.NORTH, 3);
        Line(fill.Pos.AddCopy(1, 3, -3), BlockFacing.UP, 10);
        Line(drain.Pos.AddCopy(-1, 0, 0), BlockFacing.UP, 7);

        string Kind()
        {
            var room = Battery.Find(fill);
            return room == null ? "none" : room.Problem ?? room.Elemental ?? "?";
        }
        Put(fill, Crystal.Earth, Crystal.Glittering);
        string earth = Kind();
        // Two Elementals in its stones: the fill's crystals win and the others sit idle (the owner, J4, Nov 8).
        Put(drain, Crystal.Fire, Crystal.Glittering);
        string both = Kind();
        fill.WorkBattery(0);
        int idleFire = drain.BatteryIdle;
        var earthCrystal = fill.Crystals.FirstOrDefault(c => c.Alignment == Crystal.Earth);
        if (earthCrystal != null)
        {
            fill.TakeOut(earthCrystal);
            fill.OnSocketsChanged();
        }
        Put(drain, Crystal.Earth, Crystal.Glittering);
        Put(fill, Crystal.Fire, Crystal.Glowing);
        string fire = Kind();
        fill.WorkBattery(0);
        logger.Notification("Machines test: a 2×2×1 room of granite with an Earth crystal in its fill is {0}, with a Fire one in its drain still {1} (the drain's idle: {2}); with Fire in the fill and Earth beside Fire in the drain {3}, the drain's idle: {4}",
            earth, both, idleFire, fire, drain.BatteryIdle);

        fill.ReadContext();
        Fill(fill);
        float lineBefore = Total(fill);
        var room = fill.WorkBattery(2);
        float stored = fill.BatteryState?.Stored ?? 0;
        int lava = CountIn(corner.AddCopy(0, 0, 0), 2, 1, 2, IsLava);
        bool sameCount = drain.BatteryState == fill.BatteryState;
        logger.Notification("Machines test: a Fire battery's fill took {0:0} of the line's {1:0} in {2:0}, stored {3:0} of {4:0}, {5} of 4 block(s) lava, the drain keeps the same count: {6}",
            lineBefore - Total(fill), lineBefore, Capacity(fill), stored, fill.BatteryCapacity, lava, YesNo(sameCount));

        // The Energy Sink (owner's testing tool, Nov 8) empties the fill's line and leaves the battery's count.
        Sparkstone.ItemEnergySink.Empty(sapi.World, fill.Pos.AddCopy(1, 0, 0));
        logger.Notification("Machines test: the Energy Sink emptied the fill's line to {0:0} and left its battery's count: {1}",
            Total(fill), YesNo(System.Math.Abs((fill.BatteryState?.Stored ?? -1) - stored) < 0.01f));

        float drainLine = Total(drain);
        drain.WorkBattery(2);
        int rock = CountIn(corner, 2, 1, 2, p => Path(p) == "rock-granite");
        logger.Notification("Machines test: its drain paid {0:0} into a line of {1:0}, stored left {2:0}, lava left {3}, granite back {4} of 4",
            Total(drain) - drainLine, Capacity(drain), drain.BatteryState?.Stored ?? 0, CountIn(corner, 2, 1, 2, IsLava), rock);

        // A gap in the shell: both stones rest and say why.
        Accessor.SetBlock(0, corner.AddCopy(0, 1, 0));
        var gap = drain.WorkBattery(1);
        logger.Notification("Machines test: with a gap in its roof the battery rests: {0}, the fill says so: {1}", gap?.Problem ?? "no", fill.BatteryProblem ?? "no");
    }

    /// <summary>A 3×3×1 Ice battery under a glass roof: the fill freezes still water to glacier ice from the top, the drain thaws the last frozen first.</summary>
    public void IceBattery(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column).AddCopy(0, -6, 0);
        Solid(origin, 9, 4, 9);
        var corner = origin.AddCopy(3, 1, 3);
        int water = Id("game:water-still-7");
        for (int x = 0; x < 3; x++)
        {
            for (int z = 0; z < 3; z++)
            {
                var pos = corner.AddCopy(x, 0, z);
                Accessor.SetBlock(0, pos);
                Accessor.SetBlock(water, pos, BlockLayersAccess.Fluid);
            }
        }
        Accessor.SetBlock(Id("game:glass-plain"), corner.AddCopy(1, 1, 1));
        var drain = Place(corner.AddCopy(-1, 0, 0), Machine.Gather, "east");
        var fill = Place(corner.AddCopy(3, 0, 2), Machine.Focus, "west");
        if (drain == null || fill == null)
        {
            return;
        }
        Put(drain, Crystal.Ice, Crystal.Glittering);
        Put(fill, Crystal.Ice, Crystal.Glowing);
        Line(fill.Pos.AddCopy(1, 0, 0), BlockFacing.UP, 16);
        Fill(fill);
        fill.WorkBattery(2);
        int ice = CountIn(corner, 3, 1, 3, p => Path(p) == "glacierice");
        float stored = fill.BatteryState?.Stored ?? 0;
        // The drain's line is the drain alone: half of it is all it pays.
        drain.WorkBattery(2);
        int after = CountIn(corner, 3, 1, 3, p => Path(p) == "glacierice");
        logger.Notification("Machines test: an Ice battery stored {0:0}, {1} of 9 block(s) glacier ice; its drain paid the line to half, stored left {2:0}, glacier ice left {3}",
            stored, ice, drain.BatteryState?.Stored ?? 0, after);
        TestSite.StorePos(sapi, IceBatteryKey, fill.Pos);
    }

    /// <summary>After a restart, the Ice battery's count is still in both its stones.</summary>
    public void BatteryKept()
    {
        var pos = TestSite.ReadPos(sapi, IceBatteryKey);
        if (pos == null || Accessor.GetBlockEntity(pos) is not BlockEntityMachine fill)
        {
            logger.Error("Machines test: no test Ice battery; run /lemuria machines test icebattery first");
            return;
        }
        var drain = Accessor.GetBlockEntity(pos.AddCopy(-4, 0, -2)) as BlockEntityMachine;
        float a = fill.BatteryState?.Stored ?? -1, b = drain?.BatteryState?.Stored ?? -1;
        logger.Notification("Machines test: after the restart both Ice battery stones keep the count: {0}, stored more than 0: {1}",
            YesNo(System.Math.Abs(a - b) < 0.01f), YesNo(a > 0));
    }

    /// <summary>
    /// A 2×2×1 Earth battery of granite with a chest touching its drain: the drain wears the granite block
    /// nearest its corner to sand, paying 50 a step, with two stones and nothing else in the chest; the
    /// sand left is a weak medium, which the fill then feeds.
    /// </summary>
    public void EarthBattery(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column).AddCopy(0, -6, 0);
        Solid(origin, 8, 4, 8);
        var corner = origin.AddCopy(3, 1, 3);
        var drain = Place(corner.AddCopy(-1, 0, 0), Machine.Gather, "east");
        var fill = Place(corner.AddCopy(2, 0, 1), Machine.Focus, "west");
        if (drain == null || fill == null)
        {
            return;
        }
        var chest = Chest(drain.Pos.AddCopy(0, 0, -1));
        Put(drain, Crystal.Earth, Crystal.Glowing);
        Put(fill, Crystal.Earth, Crystal.Glittering);
        Line(drain.Pos.AddCopy(-1, 0, 0), BlockFacing.UP, 15);
        Line(fill.Pos.AddCopy(1, 0, 0), BlockFacing.UP, 8);
        drain.ReadContext();
        float line = Total(drain);
        // A Glowing crystal wears at a Gather block's 600 a day: each day's look pays one 50 step at most, and granite takes three.
        for (int i = 0; i < 3; i++)
        {
            drain.WorkBattery(24);
        }
        float paid = Total(drain) - line;
        int finds = chest?.Where(s => s.Itemstack != null && s.Itemstack.Collectible.Code.Path != "stone-granite").Sum(s => s.StackSize) ?? -1;
        logger.Notification("Machines test: an Earth battery's drain wore the granite at its corner to {0}, paid {1:0}, stones in its chest {2}, other finds {3}",
            Path(corner), paid, CountOf(chest, "stone-granite"), finds);
        Fill(fill);
        fill.WorkBattery(2);
        logger.Notification("Machines test: the sand left is fed: stored {0:0} of {1:0}", fill.BatteryState?.Stored ?? 0, fill.BatteryCapacity);
        // Placed gravel and cracked rock wear too (the owner, Nov 2 08:23Z), with no pan finds (Nov 8); sand and cobblestone never wear.
        var spot = corner.AddCopy(1, 0, 1);
        bool Wears(string code)
        {
            Accessor.SetBlock(Id(code), spot);
            return Battery.Wearable(World, spot);
        }
        logger.Notification("Machines test: placed gravel wears: {0}, cracked rock: {1}, sand: {2}, cobblestone: {3}",
            YesNo(Wears("game:gravel-granite")), YesNo(Wears("game:crackedrock-granite")), YesNo(Wears("game:sand-granite")), YesNo(Wears("game:cobblestone-granite")));
        Accessor.SetBlock(Id("game:rock-granite"), spot);
    }

    /// <summary>A Wind Probe on a mast under open sky gathers in a set wind, a strike fills its crystals and pours first; a roofed one gathers nothing.</summary>
    public void Probe(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Ground(origin, 3, 12, 3);
        var mast = origin.AddCopy(1, 0, 1);
        Line(mast, BlockFacing.UP, 3);
        var probe = Place(mast.AddCopy(0, 3, 0), Machine.Probe, "up");
        if (probe == null || SparkstoneModSystem.Networks is not { } networks)
        {
            return;
        }
        Put(probe, Crystal.Sky, Crystal.Glowing);
        Put(probe, Crystal.Sky, Crystal.Glowing);
        float wind = probe.ProbeNow(24, 0.5, 0);
        float struck = probe.Strike();
        float before = Total(probe);
        networks.Update(networks.NetworkOf(probe), feed: true);
        logger.Notification("Machines test: a Wind Probe with 2 Glowing Sky crystals, sockets facing {0}, gathered {1:0} in a day of wind 0.5; a strike filled {2:0}; the line took {3:0} of its {4:0}, {5:0} left from the strike",
            probe.SocketFacing?.Code, wind, struck, Total(probe) - before, Capacity(probe), probe.Struck);
        Accessor.SetBlock(Id("game:rock-granite"), probe.Pos.UpCopy());
        logger.Notification("Machines test: roofed, the probe gathers {0:0}", probe.ProbeNow(24, 0.5, 0));
    }
}
