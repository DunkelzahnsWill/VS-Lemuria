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
/// touch face to face form one network, which holds SparkstoneCapacity charge per block, shared
/// evenly. Every 10 seconds each network takes the charge of the aligned crystals placed on it, as
/// much as it has room for, shares its charge out again and shows its level on every block. A new
/// block joins; a broken one takes its share with it; an unloaded chunk splits a network until it is
/// whole again. Server only.
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

    /// <summary>The network a block belongs to: every loaded Awakened block that touches it, face to face, through others.</summary>
    public List<BlockEntitySparkstone> NetworkOf(BlockEntitySparkstone start, HashSet<BlockEntitySparkstone>? visited = null)
    {
        visited ??= new HashSet<BlockEntitySparkstone>();
        var network = new List<BlockEntitySparkstone>();
        var queue = new Queue<BlockEntitySparkstone>();
        queue.Enqueue(start);
        visited.Add(start);
        var accessor = sapi.World.BlockAccessor;
        while (queue.Count > 0 && network.Count < MaxBlocks)
        {
            var block = queue.Dequeue();
            network.Add(block);
            foreach (var face in BlockFacing.ALLFACES)
            {
                if (accessor.GetBlockEntity(block.Pos.AddCopy(face)) is BlockEntitySparkstone next && visited.Add(next))
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
            if (visited.Contains(block))
            {
                continue;
            }
            Update(NetworkOf(block, visited), feed: true);
        }
    }

    /// <summary>The network's charge and capacity.</summary>
    public static (float Total, float Capacity) Measure(List<BlockEntitySparkstone> network)
    {
        float total = 0;
        foreach (var block in network)
        {
            total += block.Share;
        }
        return (total, network.Count * CapacityPerBlock);
    }

    /// <summary>Takes the crystals' charge (when feeding), then shares the network's charge out evenly and shows its level.</summary>
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
        float share = total / network.Count;
        int level = Crystal.Level(total, capacity);
        foreach (var block in network)
        {
            block.Set(share, level);
        }
        if (level != oldLevel)
        {
            var at = network[0].Pos;
            sapi.World.PlaySoundAt(ChimeSound, at.X + 0.5, at.Y + 0.5, at.Z + 0.5, null, false, 16f, 0.5f);
        }
    }

    /// <summary>An aligned crystal placed on a block of the network pours its charge in, as fast as the stone takes it.</summary>
    private float Feed(List<BlockEntitySparkstone> network, float room)
    {
        float gained = 0;
        var accessor = sapi.World.BlockAccessor;
        foreach (var block in network)
        {
            if (room - gained <= 0.0001f)
            {
                break;
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
        var network = NetworkOf(block);
        var (total, _) = Measure(network);
        float taken = Math.Min(max, total);
        if (taken > 0)
        {
            Update(network, feed: false, change: -taken);
        }
        return taken;
    }

    public void Clear() => loaded.Clear();
}
