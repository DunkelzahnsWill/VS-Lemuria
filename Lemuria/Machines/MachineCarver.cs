using System;
using Lemuria.Core;
using Lemuria.Sparkstone;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace Lemuria.Machines;

/// <summary>
/// Carving machines (design note "Lemuria step 4: Machines", Carving machines): each left-click of the
/// Crystalline Chisel on a placed Awakened block carves the next form, free. The face struck on Free
/// stone becomes the front with the Gather glyph; each strike on that front carves Refine, Focus,
/// Scatter, Amplify and the Crystal Motor, then Free stone again. A block with crystals in its sockets
/// does not recarve, and striking another face of a machine does nothing.
/// </summary>
public class MachineCarver : IStoneCarver
{
    private static readonly AssetLocation RingSound = new(LemuriaModSystem.ModId, "sounds/crystals/ring");
    private static readonly AssetLocation DullSound = new(LemuriaModSystem.ModId, "sounds/crystals/dull");

    /// <summary>Raised on the server when a player carves a machine form (not Free stone); the Journal listens.</summary>
    public static event Action<IPlayer>? Carved;

    /// <summary>Every strike on Awakened stone is the carver's, so the chisel never starts mining it.</summary>
    public bool CanStrike(IWorldAccessor world, BlockSelection blockSel) =>
        world.BlockAccessor.GetBlockEntity(blockSel.Position) is BlockEntitySparkstone;

    public void Strike(IWorldAccessor world, IPlayer? player, BlockSelection blockSel)
    {
        var pos = blockSel.Position;
        var block = world.BlockAccessor.GetBlock(pos);
        if (player != null && !LemuriaClaims.TryAccess(world, player, pos, EnumBlockAccessFlags.BuildOrBreak))
        {
            return;
        }
        var into = NextBlock(world, block, blockSel.Face, out bool refused);
        if (into == null)
        {
            if (refused)
            {
                world.PlaySoundAt(DullSound, pos.X + 0.5, pos.Y + 0.5, pos.Z + 0.5, null, true, 12f, LemuriaSound.Volume(0.6f));
            }
            return;
        }
        if (world.BlockAccessor.GetBlockEntity(pos) is BlockEntitySparkstone { Crystals.Count: > 0 })
        {
            world.PlaySoundAt(DullSound, pos.X + 0.5, pos.Y + 0.5, pos.Z + 0.5, null, true, 12f, LemuriaSound.Volume(0.6f));
            return;
        }
        Carve(world, player, pos, into);
    }

    /// <summary>
    /// What a strike on this face carves the block into, or null: refused is true for a machine's other
    /// faces, which do nothing but a dull knock.
    /// </summary>
    public static Block? NextBlock(IWorldAccessor world, Block block, BlockFacing face, out bool refused)
    {
        refused = false;
        string? rock = block.Variant["rock"];
        if (rock == null)
        {
            return null;
        }
        if (!Machine.IsMachine(block))
        {
            return block is BlockSparkstone ? world.GetBlock(Machine.Code(Machine.Gather, rock, face.Code)) : null;
        }
        if (block.Variant["side"] != face.Code)
        {
            refused = true;
            return null;
        }
        string? next = Machine.Next(block.Variant["form"] ?? Machine.Gather);
        return world.GetBlock(next == null ? Machine.FreeCode(rock) : Machine.Code(next, rock, face.Code));
    }

    /// <summary>Carves the block at pos into another Awakened block, keeping its share and Active or Dormant. Server only; public for the test command.</summary>
    public static bool Carve(IWorldAccessor world, IPlayer? player, BlockPos pos, Block into)
    {
        var stone = MachineBlocks.Swap(world, pos, into);
        if (stone == null)
        {
            return false;
        }
        if (stone is BlockEntityMachine machine)
        {
            machine.SetCarver(player);
            machine.ReadContext();
            if (player != null)
            {
                Carved?.Invoke(player);
            }
        }
        world.PlaySoundAt(RingSound, pos.X + 0.5, pos.Y + 0.5, pos.Z + 0.5, null, true, 12f, LemuriaSound.Volume(0.4f));
        return true;
    }
}
