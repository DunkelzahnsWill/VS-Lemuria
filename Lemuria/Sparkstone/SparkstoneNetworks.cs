using System;
using System.Collections.Generic;
using Lemuria.Core;
using Lemuria.Crystals;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;

namespace Lemuria.Sparkstone;

/// <summary>
/// Sparkstone networks (design note, Awakened sparkstone and touching charge): Awakened blocks that
/// touch face to face form one network, which holds SparkstoneCapacity charge per block. Every 10
/// seconds each network takes the charge of the aligned crystals placed on it and of its Gather
/// blocks, as much as it has room for, shares its charge out again and shows its level on every
/// block. A new block joins; a broken one takes its share with it; an unloaded chunk splits a network
/// until it is whole again. Server only.
/// Step 4 (note "Lemuria step 4: Machines", Capacitors, Active and Dormant): a Free block's capacitors
/// add their capacity, so each block's share is in proportion to what it can hold and every block of
/// a network is equally full; a Dormant block is left out and keeps its share.
/// </summary>
public class SparkstoneNetworks
{
    public const int TickMs = 10_000;

    /// <summary>A network bigger than this is worked out in parts, so one huge wall cannot stall a tick.</summary>
    private const int MaxBlocks = 4096;

    private static readonly AssetLocation ChimeSound = new(LemuriaModSystem.ModId, "sounds/crystals/chime");

    private readonly ICoreServerAPI sapi;
    private readonly HashSet<BlockEntitySparkstone> loaded = new();

    public SparkstoneNetworks(ICoreServerAPI sapi)
    {
        this.sapi = sapi;
    }

    public void Add(BlockEntitySparkstone block) => loaded.Add(block);

    public void Remove(BlockEntitySparkstone block) => loaded.Remove(block);

    public int Count => loaded.Count;

    public static float CapacityPerBlock => (float)Math.Clamp(LemuriaModSystem.Config.SparkstoneCapacity, 0, 100_000);

    /// <summary>
    /// The network a block belongs to: every loaded Active Awakened block that touches it, face to face,
    /// through others. A Dormant block is a network of its own, which passes no charge.
    /// </summary>
    public List<BlockEntitySparkstone> NetworkOf(BlockEntitySparkstone start, HashSet<BlockEntitySparkstone>? visited = null)
    {
        visited ??= new HashSet<BlockEntitySparkstone>();
        var network = new List<BlockEntitySparkstone>();
        var queue = new Queue<BlockEntitySparkstone>();
        queue.Enqueue(start);
        visited.Add(start);
        if (!start.Active)
        {
            network.Add(start);
            return network;
        }
        var accessor = sapi.World.BlockAccessor;
        while (queue.Count > 0 && network.Count < MaxBlocks)
        {
            var block = queue.Dequeue();
            network.Add(block);
            foreach (var face in BlockFacing.ALLFACES)
            {
                if (accessor.GetBlockEntity(block.Pos.AddCopy(face)) is BlockEntitySparkstone { Active: true } next && visited.Add(next))
                {
                    queue.Enqueue(next);
                }
            }
        }
        return network;
    }

    public void Tick()
    {
        var visited = new HashSet<BlockEntitySparkstone>();
        foreach (var block in new List<BlockEntitySparkstone>(loaded))
        {
            if (visited.Contains(block) || !block.Active)
            {
                continue;
            }
            Update(NetworkOf(block, visited), feed: true);
        }
    }

    /// <summary>The network's charge and capacity.</summary>
    public static (float Total, float Capacity) Measure(List<BlockEntitySparkstone> network)
    {
        float total = 0, capacity = 0;
        foreach (var block in network)
        {
            total += block.Share;
            capacity += block.Capacity;
        }
        return (total, capacity);
    }

    /// <summary>Takes the crystals' charge (when feeding), then shares the network's charge out by what each block holds and shows its level.</summary>
    public void Update(List<BlockEntitySparkstone> network, bool feed, float change = 0)
    {
        if (network.Count == 0)
        {
            return;
        }
        var (total, capacity) = Measure(network);
        int oldLevel = network[0].Level;
        total = Math.Clamp(total + change, 0, capacity);
        if (feed)
        {
            total += Feed(network, capacity - total);
        }
        float fraction = capacity <= 0.0001f ? 0 : total / capacity;
        int level = Crystal.Level(total, capacity);
        foreach (var block in network)
        {
            block.Set(fraction * block.Capacity, level);
        }
        if (level != oldLevel)
        {
            var at = network[0].Pos;
            sapi.World.PlaySoundAt(ChimeSound, at.X + 0.5, at.Y + 0.5, at.Z + 0.5, null, false, 16f, LemuriaSound.Volume(0.5f));
        }
    }

    /// <summary>
    /// An aligned crystal placed on a block of the network pours its charge in, as fast as the stone
    /// takes it, and so do Gather blocks; what a Gather block gathered beyond the room left is lost.
    /// </summary>
    private float Feed(List<BlockEntitySparkstone> network, float room)
    {
        float gained = 0;
        var accessor = sapi.World.BlockAccessor;
        foreach (var block in network)
        {
            gained += block.TakeGathered(Math.Max(0, room - gained));
            if (room - gained <= 0.0001f)
            {
                continue;
            }
            foreach (var face in BlockFacing.ALLFACES)
            {
                var pos = block.Pos.AddCopy(face);
                if (accessor.GetBlockEntity(pos) is BlockEntityCrystal crystal && crystal.IsAligned && crystal.Charge > 0
                    && Crystal.SupportPos(pos, crystal.Position).Equals(block.Pos))
                {
                    gained += crystal.TakeCharge(room - gained);
                }
            }
        }
        return gained;
    }

    /// <summary>Takes up to this much charge from the network a block belongs to, and returns what it gave.</summary>
    public float Draw(BlockEntitySparkstone block, float max)
    {
        if (!block.Active)
        {
            return 0;
        }
        var network = NetworkOf(block);
        var (total, _) = Measure(network);
        float taken = Math.Min(max, total);
        if (taken > 0)
        {
            Update(network, feed: false, change: -taken);
        }
        return taken;
    }

    /// <summary>
    /// Spends exactly this much from the network a block belongs to, when it holds that much; otherwise
    /// takes nothing (step 4 note, Charge: a network that cannot pay leaves its machines idle).
    /// </summary>
    public bool TryPay(BlockEntitySparkstone block, float cost)
    {
        if (cost <= 0)
        {
            return true;
        }
        if (!block.Active)
        {
            return false;
        }
        var network = NetworkOf(block);
        var (total, _) = Measure(network);
        if (total + 0.0001f < cost)
        {
            return false;
        }
        Update(network, feed: false, change: -cost);
        return true;
    }

    /// <summary>Whether the network a block belongs to holds at least this much.</summary>
    public bool CanPay(BlockEntitySparkstone block, float cost) =>
        cost <= 0 || (block.Active && Measure(NetworkOf(block)).Total + 0.0001f >= cost);

    /// <summary>Fills the network a block belongs to, for the admin charger; returns its size.</summary>
    public int Fill(BlockEntitySparkstone block)
    {
        var network = NetworkOf(block);
        var (total, capacity) = Measure(network);
        Update(network, feed: false, change: capacity - total);
        return network.Count;
    }

    public void Clear() => loaded.Clear();
}
