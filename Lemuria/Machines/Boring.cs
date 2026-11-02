using System.Collections.Generic;
using Lemuria.Core;
using Lemuria.Evocations;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace Lemuria.Machines;

/// <summary>
/// Boring (step 4 part 2 note, How each piece behaves; question 12): facing sideways or up, an Earth
/// Focus's 2×2 moves away from it into the face by 2, 4 or 6 blocks, by power, for one working as a
/// rise is. Facing sideways, the 2×2 is the block aimed at with those to its right and above it, as you
/// face the wall; facing up, it reaches north and east, as in part 1. The ground goes into the machine's
/// output as itself, all or nothing; whatever stops Tremor stops it, with a warble and no cost, and the
/// Focus goes Dormant after every bore, so a tunnel grows tap by tap as far as the Focus's reach.
/// </summary>
public static class Boring
{
    private static readonly AssetLocation BoreSound = new("game", "sounds/block/rock-break-pickaxe");

    /// <summary>The blocks one bore takes, in the order it takes them, or null when the cast hit nothing.</summary>
    public static List<BlockPos>? Cut(BlockEntityMachine machine, CastContext cast)
    {
        var aimed = cast.Block?.Position;
        if (aimed == null)
        {
            return null;
        }
        var front = machine.Front;
        int depth = 2 * Evocation.TremorSink(cast.Power);
        Vec3i right, up;
        if (front == BlockFacing.UP)
        {
            right = new Vec3i(1, 0, 0);
            up = new Vec3i(0, 0, -1);
        }
        else
        {
            // Your right as you face the wall the Focus looks at.
            right = new Vec3i(-front.Normali.Z, 0, front.Normali.X);
            up = new Vec3i(0, 1, 0);
        }
        var cut = new List<BlockPos>();
        for (int d = 0; d < depth; d++)
        {
            foreach (var (r, u) in new[] { (0, 0), (1, 0), (0, 1), (1, 1) })
            {
                cut.Add(aimed.AddCopy(front.Normali.X * d + right.X * r + up.X * u, front.Normali.Y * d + right.Y * r + up.Y * u, front.Normali.Z * d + right.Z * r + up.Z * u));
            }
        }
        return cut;
    }

    public static MachineWork.Result Bore(BlockEntityMachine machine, CastContext cast)
    {
        var world = cast.Api.World;
        var accessor = world.BlockAccessor;
        if (Cut(machine, cast) is not { } cut)
        {
            return MachineWork.Result.Nothing;
        }
        var taken = new List<BlockPos>();
        var ground = new List<ItemStack>();
        foreach (var pos in cut)
        {
            var block = accessor.GetBlock(pos);
            if (block.Id == 0 || (block.Replaceable >= 6000 && accessor.GetBlockEntity(pos) == null))
            {
                // Open already: air, or a plant or thin snow, which stays.
                continue;
            }
            if (!Effects.Movable(block) || accessor.GetBlockEntity(pos) != null || !cast.Can(pos))
            {
                return MachineWork.Result.Failed;
            }
            taken.Add(pos);
            ground.Add(new ItemStack(block));
        }
        if (taken.Count == 0)
        {
            return MachineWork.Result.Nothing;
        }
        if (!machine.CanYield(ground))
        {
            return MachineWork.Result.Full;
        }
        foreach (var pos in taken)
        {
            accessor.SetBlock(0, pos);
        }
        foreach (var pos in taken)
        {
            accessor.TriggerNeighbourBlockUpdate(pos);
        }
        var mouth = taken[0].ToVec3d().Add(0.5, 0.5, 0.5);
        machine.Yield(ground, mouth);
        world.PlaySoundAt(BoreSound, mouth.X, mouth.Y, mouth.Z, null, true, 24f, LemuriaSound.Volume(1f));
        return MachineWork.Result.Acted;
    }
}
