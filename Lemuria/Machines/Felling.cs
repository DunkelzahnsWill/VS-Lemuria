using System;
using System.Collections.Generic;
using System.Linq;
using Lemuria.Evocations;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace Lemuria.Machines;

/// <summary>
/// Felling (step 4 part 2 note, How each piece behaves; question 6): a Sky crystal in a Focus aimed at
/// a tree's log fells the whole tree through the game's own felling search, as an axe does, its logs,
/// sticks and saplings into the machine's output. It fells only what an axe fells as a tree, so placed
/// logs and buildings are safe. It pays one working for each 1, 9 or 27 logs by power, in full or not
/// at all, leaves free; it waits while the network cannot pay, and goes Dormant when the output has no
/// room for the whole tree.
/// </summary>
public static class Felling
{
    private static readonly AssetLocation FellSound = new("game", "sounds/effect/treefell");

    private static ItemAxe? axe;
    private static IWorldAccessor? axeWorld;

    /// <summary>The game's axe, whose tree search the felling uses; any axe will do.</summary>
    private static ItemAxe? Axe(IWorldAccessor world)
    {
        if (axeWorld != world)
        {
            axeWorld = world;
            axe = world.Items.OfType<ItemAxe>().FirstOrDefault();
        }
        return axe;
    }

    /// <summary>The tree the cast aims at, its blocks from the top down, or null when it does not aim at a tree's log.</summary>
    public static List<BlockPos>? TreeAt(CastContext cast)
    {
        var pos = cast.Block?.Position;
        var world = cast.Api.World;
        if (pos == null || world.BlockAccessor.GetBlock(pos).BlockMaterial != EnumBlockMaterial.Wood || Axe(world) is not { } tool)
        {
            return null;
        }
        var found = tool.FindTree(world, pos, out _, out _);
        if (found.Count == 0 || !found.Any(p => world.BlockAccessor.GetBlock(p).BlockMaterial == EnumBlockMaterial.Wood))
        {
            return null;
        }
        // The axe breaks them in the order its search hands them back.
        return found.ToList();
    }

    /// <summary>The number of workings a tree of this many logs costs at this power.</summary>
    public static int Workings(int logs, int power) => (int)Math.Ceiling(logs / (double)Machine.LogsPerWorking(power));

    public static MachineWork.Result Fell(BlockEntityMachine machine, CastContext cast, List<BlockPos> tree, float workingCost, out float paid)
    {
        var world = cast.Api.World;
        var accessor = world.BlockAccessor;
        paid = 0;
        if (tree.Any(p => !cast.Can(p)))
        {
            return MachineWork.Result.Nothing;
        }
        int logs = tree.Count(p => accessor.GetBlock(p).BlockMaterial == EnumBlockMaterial.Wood);
        float cost = workingCost * Workings(logs, cast.Power);
        if (Sparkstone.SparkstoneModSystem.Networks?.CanPay(machine, cost) != true)
        {
            return MachineWork.Result.Unpaid;
        }
        // The drops an axe gets: leaves give less the more of them it breaks, as the game's felling does.
        var drops = new List<ItemStack>();
        float leaves = 1, branchy = 0.8f;
        foreach (var pos in tree)
        {
            var block = accessor.GetBlock(pos);
            bool isLeaves = block.BlockMaterial == EnumBlockMaterial.Leaves;
            bool isBranchy = block.Code?.Path.Contains("branchy") == true;
            var got = block.GetDrops(world, pos, null, isLeaves ? leaves : isBranchy ? branchy : 1);
            if (got != null)
            {
                drops.AddRange(got.Where(d => d != null));
            }
            if (isLeaves && leaves > 0.03f)
            {
                leaves *= 0.85f;
            }
            if (isBranchy && branchy > 0.015f)
            {
                branchy *= 0.7f;
            }
        }
        if (!machine.CanYield(drops))
        {
            return MachineWork.Result.Full;
        }
        foreach (var pos in tree)
        {
            accessor.SetBlock(0, pos);
        }
        foreach (var pos in tree)
        {
            accessor.TriggerNeighbourBlockUpdate(pos);
        }
        var foot = tree[^1];
        machine.Yield(drops, cast.Point);
        world.PlaySoundAt(FellSound, foot.X + 0.5, foot.Y + 0.5, foot.Z + 0.5, null, false, 32, Math.Clamp(tree.Count / 100f, 0.25f, 1));
        paid = cost;
        return MachineWork.Result.Acted;
    }
}
