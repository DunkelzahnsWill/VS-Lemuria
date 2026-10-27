using System;
using System.Collections.Generic;
using System.Linq;
using Lemuria.Core;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;

namespace Lemuria.Crystals;

/// <summary>
/// Twins and dialogues (design note, Twins and dialogues). Fire and Ice, and Earth and Sky, dialogue
/// when they stand within 3 blocks of each other, or lie in one container, where everything a player
/// carries counts as one (cursor and crafting grid included, a crystal jar's slots excluded). After
/// DialogueGraceSeconds of discordant hum the dialogue acts at the average of the two Evolutions,
/// rounded down, and both suffer it: level 1 neutralizes both, level 2 discharges both, and one of the
/// pair, picked at random, cracks 1 time in 3. Twins pair off one for one, nearest first, and only
/// where the dialogue changes something. Runs on the server every 10 seconds.
/// </summary>
public class Dialogues
{
    public const int TickMs = 10_000;

    private static readonly AssetLocation ThudSound = new(LemuriaModSystem.ModId, "sounds/crystals/thud");
    private static readonly AssetLocation CrackleSound = new(LemuriaModSystem.ModId, "sounds/crystals/crackle");

    private readonly ICoreServerAPI sapi;
    private readonly StandingCrystals standing;
    private readonly CrystalsModSystem system;

    private class ContainerWatch
    {
        public long LastSeenMs;
        /// <summary>When the warning began, or null while there is none; a test may set it before the server clock's zero.</summary>
        public long? StartMs;
    }

    private readonly Dictionary<InventoryBase, ContainerWatch> containers = new();
    private readonly Dictionary<string, long> playerStarts = new();

    /// <summary>Inventory class names that keep crystals out of dialogues (racks), set by other features.</summary>
    public static readonly HashSet<string> ShieldedInventoryClasses = new();

    private long lastTickMs;

    public Dialogues(ICoreServerAPI sapi, StandingCrystals standing, CrystalsModSystem system)
    {
        this.sapi = sapi;
        this.standing = standing;
        this.system = system;
    }

    private static float Grace => (float)Math.Clamp(LemuriaModSystem.Config.DialogueGraceSeconds, 0, 3600);

    public static int Level(string evolutionA, string evolutionB) => Math.Max(1, (Crystal.Rank(evolutionA) + Crystal.Rank(evolutionB)) / 2);

    /// <summary>A dialogue happens only when it changes something: two empty Glowing twins raise no warning.</summary>
    public static bool WouldChange(string evolutionA, float chargeA, string evolutionB, float chargeB) =>
        Level(evolutionA, evolutionB) <= 1 || chargeA > 0 || chargeB > 0;

    /// <summary>Hands a container to the watch; its crystals are checked on the next tick.</summary>
    public void Watch(InventoryBase inventory)
    {
        if (inventory is InventoryBasePlayer || ShieldedInventoryClasses.Contains(inventory.ClassName))
        {
            return;
        }
        if (!containers.TryGetValue(inventory, out var watch))
        {
            containers[inventory] = watch = new ContainerWatch();
        }
        watch.LastSeenMs = sapi.World.ElapsedMilliseconds;
    }

    public void Tick()
    {
        long now = sapi.World.ElapsedMilliseconds;
        float dt = lastTickMs == 0 ? TickMs / 1000f : (now - lastTickMs) / 1000f;
        lastTickMs = now;
        TickStanding(dt);
        TickContainers(now);
        foreach (var player in sapi.World.AllOnlinePlayers.OfType<IServerPlayer>())
        {
            if (player.ConnectionState == EnumClientState.Playing)
            {
                TickPlayer(player, now);
            }
        }
    }

    // ------------------------------------------------------------------ standing crystals

    /// <summary>Pairs standing twins and moves their warnings on by dt seconds; public for the test command.</summary>
    public void TickStanding(float dt)
    {
        var crystals = standing.All().Where(c => c.Alignment != null && c.Alignment != Crystal.Unaligned)
            .OrderBy(c => c.StandPos.X).ThenBy(c => c.StandPos.Y).ThenBy(c => c.StandPos.Z).ToList();
        var paired = new HashSet<IStandingCrystal>();
        var pairs = new List<(IStandingCrystal A, IStandingCrystal B)>();
        foreach (var a in crystals)
        {
            string? twin = Crystal.TwinOf(a.Alignment!);
            var near = standing.Near(a.StandPos, 3).Where(b => b != a && b.Alignment == twin).ToList();
            a.TwinNear = near.Count > 0;
            if (paired.Contains(a))
            {
                continue;
            }
            var partner = near.Where(b => !paired.Contains(b) && WouldChange(a.Evolution, a.Charge, b.Evolution, b.Charge))
                .OrderBy(b => b.StandPos.DistanceSqTo(a.StandPos.X, a.StandPos.Y, a.StandPos.Z)).FirstOrDefault();
            if (partner != null)
            {
                paired.Add(a);
                paired.Add(partner);
                pairs.Add((a, partner));
            }
        }
        foreach (var crystal in crystals)
        {
            if (!paired.Contains(crystal))
            {
                crystal.DialogueSeconds = 0;
                crystal.SetWarning(false);
            }
        }
        var acting = new List<(IStandingCrystal A, IStandingCrystal B)>();
        foreach (var (a, b) in pairs)
        {
            a.DialogueSeconds += dt;
            b.DialogueSeconds += dt;
            a.SetWarning(true);
            b.SetWarning(true);
            if (Math.Min(a.DialogueSeconds, b.DialogueSeconds) >= Grace)
            {
                acting.Add((a, b));
            }
        }
        foreach (var (a, b) in acting)
        {
            ActStanding(a, b);
        }
    }

    private void ActStanding(IStandingCrystal a, IStandingCrystal b)
    {
        int level = Level(a.Evolution, b.Evolution);
        var mid = new Vec3d((a.StandPos.X + b.StandPos.X) / 2.0 + 0.5, (a.StandPos.Y + b.StandPos.Y) / 2.0 + 0.5, (a.StandPos.Z + b.StandPos.Z) / 2.0 + 0.5);
        string describe = $"{a.Alignment} {a.Evolution} and {b.Alignment} {b.Evolution}";
        a.SetWarning(false);
        b.SetWarning(false);
        if (level <= 1)
        {
            sapi.World.PlaySoundAt(ThudSound, mid.X, mid.Y, mid.Z, null, false, 24f, LemuriaSound.Volume(1f));
            a.Neutralize();
            b.Neutralize();
            system.Mod.Logger.Notification("Crystals: dialogue at level 1, {0} neutralized", describe);
            return;
        }
        sapi.World.PlaySoundAt(CrackleSound, mid.X, mid.Y, mid.Z, null, true, 24f, LemuriaSound.Volume(1f));
        a.Discharge();
        b.Discharge();
        string cracked = "none";
        if (sapi.World.Rand.NextDouble() < Math.Clamp(LemuriaModSystem.Config.DischargeCrackChance, 0, 1))
        {
            var victim = sapi.World.Rand.Next(2) == 0 ? a : b;
            cracked = victim.Alignment ?? "?";
            victim.Crack();
        }
        system.Mod.Logger.Notification("Crystals: dialogue at level 2, {0} discharged, cracked: {1}", describe, cracked);
    }

    // ------------------------------------------------------------------ containers and players

    /// <summary>A run of identical crystals in one slot, as one side of a pairing sees them.</summary>
    private class Unit
    {
        public ItemSlot Slot = null!;
        public int Order;
        public string Alignment = "";
        public string Evolution = "";
        public float Charge;
        public int Left;
    }

    private class InventoryPair
    {
        public Unit A = null!;
        public Unit B = null!;
        public int Count;
    }

    private static List<Unit> Units(IEnumerable<ItemSlot> slots)
    {
        var units = new List<Unit>();
        int order = 0;
        foreach (var slot in slots)
        {
            order++;
            var stack = slot?.Itemstack;
            if (stack == null || !Crystal.IsAligned(stack.Collectible))
            {
                continue;
            }
            units.Add(new Unit
            {
                Slot = slot!,
                Order = order,
                Alignment = Crystal.AlignmentOf(stack.Collectible),
                Evolution = Crystal.EvolutionOf(stack.Collectible),
                Charge = Crystal.GetCharge(stack),
                Left = stack.StackSize,
            });
        }
        return units;
    }

    /// <summary>Twins pair off one for one, nearest slots first, where the dialogue would change something.</summary>
    private static List<InventoryPair> Pair(List<Unit> units)
    {
        var candidates = new List<(Unit A, Unit B, int Distance)>();
        foreach (var a in units)
        {
            if (a.Alignment != Crystal.Fire && a.Alignment != Crystal.Earth)
            {
                continue;
            }
            string twin = Crystal.TwinOf(a.Alignment)!;
            foreach (var b in units)
            {
                if (b.Alignment == twin && WouldChange(a.Evolution, a.Charge, b.Evolution, b.Charge))
                {
                    candidates.Add((a, b, Math.Abs(a.Order - b.Order)));
                }
            }
        }
        var pairs = new List<InventoryPair>();
        foreach (var (a, b, _) in candidates.OrderBy(c => c.Distance))
        {
            int n = Math.Min(a.Left, b.Left);
            if (n <= 0)
            {
                continue;
            }
            a.Left -= n;
            b.Left -= n;
            pairs.Add(new InventoryPair { A = a, B = b, Count = n });
        }
        return pairs;
    }

    private void TickContainers(long now)
    {
        var gone = new List<InventoryBase>();
        foreach (var (inventory, watch) in containers)
        {
            bool stale = now - watch.LastSeenMs > 3 * TickMs;
            var pairs = stale ? new List<InventoryPair>() : Pair(Units(inventory));
            string key = inventory.Pos == null ? "" : $"pos:{inventory.Pos.X},{inventory.Pos.Y},{inventory.Pos.Z}";
            if (pairs.Count == 0)
            {
                if (watch.StartMs != null && key.Length > 0)
                {
                    system.SendDiscord(key, false, inventory.Pos!.ToVec3d().Add(0.5, 0.5, 0.5), 0, 0);
                }
                watch.StartMs = null;
                if (stale || Units(inventory).Count == 0)
                {
                    gone.Add(inventory);
                }
                continue;
            }
            if (watch.StartMs == null)
            {
                watch.StartMs = now;
                if (key.Length > 0)
                {
                    system.SendDiscord(key, true, inventory.Pos!.ToVec3d().Add(0.5, 0.5, 0.5), 0, 0);
                }
                continue;
            }
            if ((now - watch.StartMs.Value) / 1000f < Grace)
            {
                continue;
            }
            watch.StartMs = null;
            if (key.Length > 0)
            {
                system.SendDiscord(key, false, inventory.Pos!.ToVec3d().Add(0.5, 0.5, 0.5), 0, 0);
            }
            var where = inventory.Pos?.ToVec3d().Add(0.5, 0.5, 0.5) ?? new Vec3d();
            Act(pairs, inventory, where, "a container");
        }
        foreach (var inventory in gone)
        {
            containers.Remove(inventory);
        }
    }

    /// <summary>Checks the containers in the watch at once, as if the warning had run out; for the test command.</summary>
    public void TickContainersNow(float elapsedSeconds)
    {
        long now = sapi.World.ElapsedMilliseconds;
        foreach (var watch in containers.Values)
        {
            if (watch.StartMs != null)
            {
                watch.StartMs = now - (long)(elapsedSeconds * 1000);
            }
            watch.LastSeenMs = now;
        }
        TickContainers(now);
    }

    /// <summary>The slots a player carries, as one container; a crystal jar's own slots are left out.</summary>
    public static List<ItemSlot> CarriedSlots(IPlayer player)
    {
        var slots = new List<ItemSlot>();
        var manager = player.InventoryManager;
        foreach (string name in new[] { GlobalConstants.hotBarInvClassName, GlobalConstants.backpackInvClassName, GlobalConstants.mousecursorInvClassName, GlobalConstants.craftingInvClassName })
        {
            var inventory = manager.GetOwnInventory(name);
            if (inventory == null)
            {
                continue;
            }
            foreach (var slot in inventory)
            {
                if (slot is ItemSlotBagContent bagSlot && IsJar(inventory[bagSlot.BagIndex]?.Itemstack))
                {
                    continue;
                }
                slots.Add(slot);
            }
        }
        return slots;
    }

    /// <summary>A crystal jar (Storage) is any item with <c>crystalJar: true</c> in its attributes.</summary>
    public static bool IsJar(ItemStack? stack) => stack?.ItemAttributes?["crystalJar"].AsBool() == true;

    private void TickPlayer(IServerPlayer player, long now)
    {
        var entity = player.Entity;
        if (entity == null)
        {
            return;
        }
        string key = "ent:" + entity.EntityId;
        var pairs = Pair(Units(CarriedSlots(player)));
        bool started = playerStarts.TryGetValue(player.PlayerUID, out long start);
        if (pairs.Count == 0)
        {
            if (started)
            {
                playerStarts.Remove(player.PlayerUID);
                system.SendDiscord(key, false, null, entity.EntityId, 0);
            }
            return;
        }
        if (!started)
        {
            playerStarts[player.PlayerUID] = now;
            system.SendDiscord(key, true, null, entity.EntityId, 0);
            return;
        }
        if ((now - start) / 1000f < Grace)
        {
            return;
        }
        playerStarts.Remove(player.PlayerUID);
        system.SendDiscord(key, false, null, entity.EntityId, 0);
        Act(pairs, null, entity.Pos.XYZ.Add(0, 1, 0), "the inventory of " + player.PlayerName);
        player.InventoryManager.BroadcastHotbarSlot();
    }

    private void Act(List<InventoryPair> pairs, InventoryBase? inventory, Vec3d where, string place)
    {
        bool thud = false, crackle = false;
        foreach (var pair in pairs)
        {
            int level = Level(pair.A.Evolution, pair.B.Evolution);
            string describe = $"{pair.Count} x {pair.A.Alignment} {pair.A.Evolution} and {pair.B.Alignment} {pair.B.Evolution}";
            if (level <= 1)
            {
                Transform(pair.A.Slot, pair.Count, inventory, where, Neutralized);
                Transform(pair.B.Slot, pair.Count, inventory, where, Neutralized);
                thud = true;
                system.Mod.Logger.Notification("Crystals: dialogue at level 1 in {0}, {1} neutralized", place, describe);
                continue;
            }
            Transform(pair.A.Slot, pair.Count, inventory, where, Discharged);
            Transform(pair.B.Slot, pair.Count, inventory, where, Discharged);
            crackle = true;
            int cracked = 0;
            for (int i = 0; i < pair.Count; i++)
            {
                if (sapi.World.Rand.NextDouble() < Math.Clamp(LemuriaModSystem.Config.DischargeCrackChance, 0, 1))
                {
                    // The discharged crystals now sit in their slots with no charge; one of them takes the crack.
                    var slot = sapi.World.Rand.Next(2) == 0 ? pair.A.Slot : pair.B.Slot;
                    if (!slot.Empty)
                    {
                        Transform(slot, 1, inventory, where, s => Cracked(s, where));
                        cracked++;
                    }
                }
            }
            system.Mod.Logger.Notification("Crystals: dialogue at level 2 in {0}, {1} discharged, {2} cracked", place, describe, cracked);
        }
        if (thud)
        {
            sapi.World.PlaySoundAt(ThudSound, where.X, where.Y, where.Z, null, false, 24f, LemuriaSound.Volume(1f));
        }
        if (crackle)
        {
            sapi.World.PlaySoundAt(CrackleSound, where.X, where.Y, where.Z, null, true, 24f, LemuriaSound.Volume(1f));
        }
    }

    private ItemStack? Neutralized(ItemStack stack)
    {
        var block = Crystal.BlockFor(sapi.World, Crystal.Unaligned, Crystal.EvolutionOf(stack.Collectible));
        if (block == null)
        {
            return stack;
        }
        var result = new ItemStack(block, stack.StackSize);
        Crystal.SetState(result, 0, Crystal.GetCracks(stack));
        return result;
    }

    private static ItemStack? Discharged(ItemStack stack)
    {
        Crystal.SetState(stack, 0, Crystal.GetCracks(stack));
        return stack;
    }

    private ItemStack? Cracked(ItemStack stack, Vec3d where)
    {
        int cracks = Crystal.GetCracks(stack) + 1;
        if (cracks >= Math.Max(1, LemuriaModSystem.Config.CracksToShatter))
        {
            sapi.World.PlaySoundAt(new AssetLocation(LemuriaModSystem.ModId, "sounds/crystals/shatter"), where.X, where.Y, where.Z, null, true, 24f, LemuriaSound.Volume(1f));
            system.SendBurst(where, Crystal.AlignmentOf(stack.Collectible));
            return null;
        }
        Crystal.SetState(stack, Crystal.GetCharge(stack), cracks);
        return stack;
    }

    /// <summary>
    /// Takes some crystals out of a slot, changes them, and puts them back: in the same slot when it
    /// emptied, otherwise in an empty slot of the container, otherwise on the ground.
    /// </summary>
    private void Transform(ItemSlot slot, int count, InventoryBase? inventory, Vec3d where, System.Func<ItemStack, ItemStack?> change)
    {
        var taken = slot.TakeOut(count);
        if (taken == null)
        {
            return;
        }
        var result = change(taken);
        if (result != null)
        {
            if (slot.Empty)
            {
                slot.Itemstack = result;
            }
            else
            {
                var free = (inventory ?? slot.Inventory)?.FirstOrDefault(s => s.Empty && s.CanHold(new DummySlot(result)));
                if (free != null)
                {
                    free.Itemstack = result;
                    free.MarkDirty();
                }
                else
                {
                    sapi.World.SpawnItemEntity(result, where);
                }
            }
        }
        slot.MarkDirty();
    }
}
