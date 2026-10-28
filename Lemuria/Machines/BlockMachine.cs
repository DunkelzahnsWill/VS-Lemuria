using System;
using Lemuria.Core;
using Lemuria.Sparkstone;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent.Mechanics;

namespace Lemuria.Machines;

/// <summary>
/// The Gather, Refine, Focus, Scatter and Amplify blocks (design note "Lemuria step 4: Machines",
/// Carving machines): Awakened sparkstone with its verb's glyph and four sockets on its front. Mined,
/// it drops itself with its form, and its crystals beside it; set down, it is Active and its front is
/// the face toward you.
/// </summary>
public class BlockMachine : BlockSparkstone
{
    public new const string ClassCode = "lemuria-machine";

    public override bool TryPlaceBlock(IWorldAccessor world, IPlayer byPlayer, ItemStack itemstack, BlockSelection blockSel, ref string failureCode) =>
        MachineBlocks.TryPlace(this, world, byPlayer, itemstack, blockSel, ref failureCode);

    public override ItemStack[] GetDrops(IWorldAccessor world, BlockPos pos, IPlayer byPlayer, float dropQuantityMultiplier = 1f) =>
        MachineBlocks.Drops(this, world);

    public override ItemStack OnPickBlock(IWorldAccessor world, BlockPos pos) => MachineBlocks.Drops(this, world)[0];

    public override void OnNeighbourBlockChange(IWorldAccessor world, BlockPos pos, BlockPos neibpos)
    {
        base.OnNeighbourBlockChange(world, pos, neibpos);
        (world.BlockAccessor.GetBlockEntity(pos) as BlockEntityMachine)?.MarkContextDirty();
    }
}

/// <summary>
/// The Crystal Motor (note, The Crystal Motor): a carved hub that turns the axle, gear or quern at its
/// front through the game's own mechanical power, a power source like a windmill rotor
/// (<see cref="BEBehaviorCrystalMotor"/>). Otherwise a machine block like the others.
/// </summary>
public class BlockCrystalMotor : BlockMPBase, IMPPowered
{
    public const string ClassCode = "lemuria-crystalmotor";

    private BlockFacing front = BlockFacing.NORTH;

    public override void OnLoaded(ICoreAPI api)
    {
        front = BlockFacing.FromCode(Variant["side"]) ?? BlockFacing.NORTH;
        base.OnLoaded(api);
    }

    public override void DidConnectAt(IWorldAccessor world, BlockPos pos, BlockFacing face)
    {
    }

    public override bool HasMechPowerConnectorAt(IWorldAccessor world, BlockPos pos, BlockFacing face, BlockMPBase forBlock) => face == front;

    /// <summary>The stone's hum, rising with the shaft's speed.</summary>
    public override float GetAmbientSoundStrength(IWorldAccessor world, BlockPos pos)
    {
        float speed = world.BlockAccessor.GetBlockEntity(pos)?.GetBehavior<BEBehaviorCrystalMotor>()?.Network?.Speed ?? 0;
        return BlockSparkstone.HumAt(world, pos) * (1f + Math.Clamp(speed / Math.Max(0.01f, Machine.MotorSpeed), 0, 1));
    }

    public override bool OnBlockInteractStart(IWorldAccessor world, IPlayer byPlayer, BlockSelection blockSel) =>
        BlockSparkstone.Interact(world, byPlayer, blockSel) || base.OnBlockInteractStart(world, byPlayer, blockSel);

    public override bool TryPlaceBlock(IWorldAccessor world, IPlayer byPlayer, ItemStack itemstack, BlockSelection blockSel, ref string failureCode)
    {
        if (!MachineBlocks.TryPlace(this, world, byPlayer, itemstack, blockSel, ref failureCode))
        {
            return false;
        }
        if (world.BlockAccessor.GetBlock(blockSel.Position) is BlockCrystalMotor placed)
        {
            placed.WasPlaced(world, blockSel.Position, placed.front);
        }
        return true;
    }

    public override ItemStack[] GetDrops(IWorldAccessor world, BlockPos pos, IPlayer byPlayer, float dropQuantityMultiplier = 1f) =>
        MachineBlocks.Drops(this, world);

    public override ItemStack OnPickBlock(IWorldAccessor world, BlockPos pos) => MachineBlocks.Drops(this, world)[0];

    public override void OnNeighbourBlockChange(IWorldAccessor world, BlockPos pos, BlockPos neibpos)
    {
        base.OnNeighbourBlockChange(world, pos, neibpos);
        (world.BlockAccessor.GetBlockEntity(pos) as BlockEntityMachine)?.MarkContextDirty();
    }
}

/// <summary>What every machine block shares: placing it facing you, dropping its own form, and swapping one machine block for another.</summary>
public static class MachineBlocks
{
    /// <summary>The face of a block at this position that looks toward the player's eyes, up and down included.</summary>
    public static BlockFacing FaceToward(IPlayer player, BlockPos pos)
    {
        var eyes = player.Entity.Pos.XYZ.Add(0, player.Entity.LocalEyePos.Y, 0);
        var d = eyes.SubCopy(pos.X + 0.5, pos.Y + 0.5, pos.Z + 0.5);
        double ax = Math.Abs(d.X), ay = Math.Abs(d.Y), az = Math.Abs(d.Z);
        if (ay > ax && ay > az)
        {
            return d.Y > 0 ? BlockFacing.UP : BlockFacing.DOWN;
        }
        return ax > az ? (d.X > 0 ? BlockFacing.EAST : BlockFacing.WEST) : (d.Z > 0 ? BlockFacing.SOUTH : BlockFacing.NORTH);
    }

    public static bool TryPlace(Block self, IWorldAccessor world, IPlayer byPlayer, ItemStack itemstack, BlockSelection blockSel, ref string failureCode)
    {
        if (!self.CanPlaceBlock(world, byPlayer, blockSel, ref failureCode))
        {
            return false;
        }
        var facing = world.GetBlock(self.CodeWithVariant("side", FaceToward(byPlayer, blockSel.Position).Code)) ?? self;
        facing.DoPlaceBlock(world, byPlayer, blockSel, itemstack);
        (world.BlockAccessor.GetBlockEntity(blockSel.Position) as BlockEntityMachine)?.SetCarver(byPlayer);
        return true;
    }

    /// <summary>The block itself with its form, facing north as it lies in the inventory, empty.</summary>
    public static ItemStack[] Drops(Block self, IWorldAccessor world) =>
        new[] { new ItemStack(world.GetBlock(self.CodeWithVariant("side", "north")) ?? self) };

    /// <summary>
    /// Swaps an Awakened block at a position for another, Free or carved, keeping its share, Active or
    /// Dormant and its crystals: how the chisel carves and the rod turns a front. Server only.
    /// </summary>
    public static BlockEntitySparkstone? Swap(IWorldAccessor world, BlockPos pos, Block into)
    {
        var accessor = world.BlockAccessor;
        if (accessor.GetBlockEntity(pos) is not BlockEntitySparkstone old)
        {
            return null;
        }
        var tree = new TreeAttribute();
        old.ToTreeAttributes(tree);
        accessor.SetBlock(into.BlockId, pos);
        if (accessor.GetBlockEntity(pos) is not BlockEntitySparkstone stone)
        {
            return null;
        }
        stone.FromTreeAttributes(tree, world);
        stone.OnSocketsChanged();
        if (into is BlockCrystalMotor motor)
        {
            motor.WasPlaced(world, pos, null);
        }
        return stone;
    }
}
