using System;
using System.Linq;
using System.Text;
using Lemuria.Core;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace Lemuria.Machines;

/// <summary>
/// The Control Shelf (step 4 part 2 note, How each piece behaves): a stone shelf whose back is set
/// against a side of a Focus or Scatter, never its front, one to a machine. Its 1 to 4 items, in view,
/// name the machine's hands, first to last from left to right as you face it; it is also the machine's
/// port, its input the container on top of it and its output the one under it (G2). Code
/// <c>controlshelf-{side}</c>, {side} the way its open face looks; set down, it looks toward you.
/// </summary>
public class BlockControlShelf : Block
{
    public const string ClassCode = "lemuria-controlshelf";

    public BlockFacing Side => BlockFacing.FromCode(Variant["side"]) ?? BlockFacing.NORTH;

    public override bool TryPlaceBlock(IWorldAccessor world, IPlayer byPlayer, ItemStack itemstack, BlockSelection blockSel, ref string failureCode)
    {
        if (!CanPlaceBlock(world, byPlayer, blockSel, ref failureCode))
        {
            return false;
        }
        var toward = MachineBlocks.FaceToward(byPlayer, blockSel.Position);
        if (toward.IsVertical)
        {
            toward = BlockFacing.HorizontalFromYaw(byPlayer.Entity.Pos.Yaw).Opposite;
        }
        var facing = world.GetBlock(CodeWithVariant("side", toward.Code)) ?? this;
        facing.DoPlaceBlock(world, byPlayer, blockSel, itemstack);
        return true;
    }

    public override ItemStack[] GetDrops(IWorldAccessor world, BlockPos pos, IPlayer byPlayer, float dropQuantityMultiplier = 1f) =>
        new[] { new ItemStack(world.GetBlock(CodeWithVariant("side", "north")) ?? this) };

    public override ItemStack OnPickBlock(IWorldAccessor world, BlockPos pos) => GetDrops(world, pos, null!)[0];

    public override bool OnBlockInteractStart(IWorldAccessor world, IPlayer byPlayer, BlockSelection blockSel) =>
        (world.BlockAccessor.GetBlockEntity(blockSel.Position) as BlockEntityControlShelf)?.OnInteract(byPlayer, blockSel) == true
        || base.OnBlockInteractStart(world, byPlayer, blockSel);
}

/// <summary>
/// The shelf's four items, shown on its ledge; put on and taken off with a right-click on the place
/// looked at. Server-authoritative; clients get the items through the game's block entity sync, and
/// the machine reads them when it works.
/// </summary>
public class BlockEntityControlShelf : BlockEntityDisplay
{
    public const string EntityClassCode = "lemuria-controlshelf";

    public const int Places = 4;

    private readonly InventoryGeneric inventory;

    public BlockEntityControlShelf()
    {
        inventory = new InventoryGeneric(Places, "lemuria-controlshelf-0", null, (id, inv) => new ItemSlotDisplay(inv, "shelf") { MaxSlotStackSize = 1 });
    }

    public override InventoryBase Inventory => inventory;

    public override string InventoryClassName => "lemuria-controlshelf";

    public override string AttributeTransformCode => "onshelfTransform";

    /// <summary>The way the shelf's open face looks.</summary>
    public BlockFacing Side => (Block as BlockControlShelf)?.Side ?? BlockFacing.NORTH;

    /// <summary>The block its back is set against: its machine, when it is a Focus or Scatter.</summary>
    public BlockPos MachinePos => Pos.AddCopy(Side.Opposite);

    /// <summary>The input: a container on top of the shelf.</summary>
    public IBlockEntityContainer? Input => Container(Pos.UpCopy());

    /// <summary>The output: a container under the shelf.</summary>
    public IBlockEntityContainer? Output => Container(Pos.DownCopy());

    public BlockPos OutputPos => Pos.DownCopy();

    private IBlockEntityContainer? Container(BlockPos pos) =>
        Api?.World.BlockAccessor.GetBlockEntity(pos) is IBlockEntityContainer { Inventory: not null } container and not Sparkstone.BlockEntitySparkstone and not BlockEntityControlShelf
            ? container
            : null;

    /// <summary>The items on the shelf, first to last.</summary>
    public ItemStack?[] Items => Enumerable.Range(0, Places).Select(i => inventory[i].Itemstack).ToArray();

    /// <summary>The place on the ledge under a hit on the open face, 0 to 3 from left to right as you face it.</summary>
    public int PlaceAt(Vec3d hit)
    {
        var (u, _) = Sparkstone.Sockets.Uv(Side, hit);
        return Math.Clamp((int)(u * Places), 0, Places - 1);
    }

    public bool OnInteract(IPlayer player, BlockSelection blockSel)
    {
        if (blockSel.Face != Side)
        {
            return false;
        }
        var held = player.InventoryManager.ActiveHotbarSlot;
        int place = PlaceAt(blockSel.HitPosition);
        if (Api.Side == EnumAppSide.Server && !LemuriaClaims.TryAccess(Api.World, player, Pos, EnumBlockAccessFlags.Use))
        {
            return true;
        }
        if (held.Empty)
        {
            if (inventory[place].Empty)
            {
                return false;
            }
            if (Api.Side == EnumAppSide.Server)
            {
                var stack = inventory[place].TakeOutWhole();
                if (!player.InventoryManager.TryGiveItemstack(stack, true))
                {
                    Api.World.SpawnItemEntity(stack, Pos.ToVec3d().Add(0.5, 0.5, 0.5).Add(Side.Normald.Clone().Mul(0.7)));
                }
                Api.World.PlaySoundAt(new AssetLocation("game", "sounds/player/collect"), Pos, 0, null, true, 12f, LemuriaSound.Volume(0.8f));
                Changed();
            }
            return true;
        }
        int target = inventory[place].Empty ? place : Enumerable.Range(0, Places).FirstOrDefault(i => inventory[i].Empty, -1);
        if (target < 0)
        {
            return true;
        }
        if (Api.Side == EnumAppSide.Server && held.TryPutInto(Api.World, inventory[target], 1) > 0)
        {
            held.MarkDirty();
            Api.World.PlaySoundAt(new AssetLocation("game", "sounds/block/ceramicplace"), Pos, 0, null, true, 12f, LemuriaSound.Volume(0.8f));
            Changed();
        }
        return true;
    }

    private void Changed()
    {
        inventory.MarkSlotDirty(0);
        MarkDirty(true);
        // The machine reads its shelf again, as on a rod tap.
        (Api.World.BlockAccessor.GetBlockEntity(MachinePos) as BlockEntityMachine)?.MarkContextDirty();
    }

    protected override float[][] genTransformationMatrices()
    {
        var matrices = new float[Places][];
        // The same turn the block's shape takes for its side, as the game's own shelf does.
        float turn = Block?.Shape?.rotateY ?? 0;
        for (int i = 0; i < Places; i++)
        {
            // Along the ledge from left to right as you face a north-looking shelf: east to west is right to left.
            float x = 12.5f / 16f - i * 3f / 16f;
            matrices[i] = new Matrixf()
                .Translate(0.5f, 0, 0.5f)
                .RotateYDeg(turn)
                .Translate(x - 0.5f, 5f / 16f, 6.5f / 16f - 0.5f)
                .Scale(0.42f, 0.42f, 0.42f)
                .Translate(-0.5f, 0, -0.5f)
                .Values;
        }
        return matrices;
    }

    public override void FromTreeAttributes(ITreeAttribute tree, IWorldAccessor worldForResolving)
    {
        base.FromTreeAttributes(tree, worldForResolving);
        RedrawAfterReceivingTreeAttributes(worldForResolving);
    }

    public override void GetBlockInfo(IPlayer forPlayer, StringBuilder dsc)
    {
        var names = Items.Select((s, i) => s == null ? null : $"{i + 1}. {s.GetName()}").Where(n => n != null).ToList();
        dsc.AppendLine(names.Count == 0 ? Lang.Get(LemuriaModSystem.ModId + ":controlshelf-empty") : string.Join("\n", names));
    }
}
