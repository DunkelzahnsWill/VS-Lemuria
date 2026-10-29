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
/// port: as you face it, its input is the container touching its left and its output the one touching
/// its right, so goods cross it the way its order reads (G2; note rev 33). Code
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

    /// <summary>Your left as you face the shelf's open face: where place 1 is, and the input.</summary>
    public BlockFacing Left => Side.Code switch
    {
        "east" => BlockFacing.SOUTH,
        "south" => BlockFacing.WEST,
        "west" => BlockFacing.NORTH,
        _ => BlockFacing.EAST,
    };

    /// <summary>Your right as you face the shelf: the output.</summary>
    public BlockFacing Right => Left.Opposite;

    /// <summary>The input: a container touching the shelf's left.</summary>
    public IBlockEntityContainer? Input => Container(Pos.AddCopy(Left));

    /// <summary>The output: a container touching the shelf's right.</summary>
    public IBlockEntityContainer? Output => Container(Pos.AddCopy(Right));

    public BlockPos OutputPos => Pos.AddCopy(Right);

    private IBlockEntityContainer? Container(BlockPos pos) =>
        Api?.World.BlockAccessor.GetBlockEntity(pos) is IBlockEntityContainer { Inventory: not null } container and not Sparkstone.BlockEntitySparkstone and not BlockEntityControlShelf
            ? container
            : null;

    private readonly bool[] dormant = new bool[Places];

    /// <summary>The items on the shelf, first to last.</summary>
    public ItemStack?[] Items => Enumerable.Range(0, Places).Select(i => inventory[i].Itemstack).ToArray();

    /// <summary>
    /// Whether the item in this place is Dormant (H2, note rev 44): switched with the Crystal Rod, drawn
    /// dark, and passed over by the machine as work and as stock until it is woken.
    /// </summary>
    public bool IsDormant(int place) => place >= 0 && place < Places && dormant[place] && !inventory[place].Empty;

    /// <summary>
    /// The item in this place that is a shovel's or an axe's prefix (H1): an item that names no action,
    /// in the place just left of it. Null when there is none.
    /// </summary>
    public ItemStack? PrefixOf(int place) =>
        place > 0 && inventory[place - 1].Itemstack is { } before && ShelfWork.KindOf(before) == ShelfWork.Kind.None ? before : null;

    /// <summary>
    /// A rod's right-click on a place: an item that names an action switches Dormant or wakes; a prefix,
    /// an empty place or anything else does nothing. Returns whether it switched. Server only.
    /// </summary>
    public bool SwitchDormant(int place)
    {
        if (place < 0 || place >= Places || inventory[place].Itemstack is not { } stack || ShelfWork.KindOf(stack) == ShelfWork.Kind.None)
        {
            return false;
        }
        dormant[place] = !dormant[place];
        Api.World.PlaySoundAt(new AssetLocation(LemuriaModSystem.ModId, dormant[place] ? "sounds/machines/switch-off" : "sounds/machines/switch-on"), Pos, 0, null, true, 12f, LemuriaSound.Volume(0.6f));
        Changed();
        return true;
    }

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
        if (held.Itemstack?.Collectible is ItemCrystalRod)
        {
            // The rod switches an item Dormant or wakes it, and never goes on the shelf itself.
            if (Api.Side == EnumAppSide.Server)
            {
                SwitchDormant(place);
            }
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
                dormant[place] = false;
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
                .Translate(-0.5f, 0, -0.5f)
                .Values;
        }
        return matrices;
    }

    /// <summary>Room for one item in its place on the ledge, in blocks: across, up and deep.</summary>
    private const float PlaceWidth = 2.6f / 16f, PlaceHeight = 7.4f / 16f, PlaceDepth = 6f / 16f;

    /// <summary>
    /// Each item stands upright in its own place (play-test, Oct 29): the game's own model, turned so
    /// its longest side points up, shrunk to fit the place and set on the ledge, so an axe stands
    /// instead of lying across its neighbours and a block or bush no larger than its place.
    /// </summary>
    protected override MeshData getOrCreateMesh(ItemSlot slot, int index)
    {
        var cached = getMesh(slot);
        if (cached != null)
        {
            return cached;
        }
        var mesh = getDefaultMesh(slot.Itemstack).Clone();
        Fit(mesh);
        if (IsDormant(inventory.GetSlotId(slot)) && mesh.Rgba != null)
        {
            // Dormant, it is drawn dark, as a Dormant crystal's plug is.
            for (int i = 0; i < mesh.VerticesCount * 4; i++)
            {
                if (i % 4 != 3)
                {
                    mesh.Rgba[i] = (byte)(mesh.Rgba[i] * 0.35f);
                }
            }
        }
        MeshCache[getMeshCacheKey(slot)] = mesh;
        return mesh;
    }

    protected override string getMeshCacheKey(ItemSlot slot) =>
        base.getMeshCacheKey(slot) + (IsDormant(inventory.GetSlotId(slot)) ? "-dormant" : "");

    private static (Vec3f Min, Vec3f Max) Bounds(MeshData mesh)
    {
        var min = new Vec3f(float.MaxValue, float.MaxValue, float.MaxValue);
        var max = new Vec3f(float.MinValue, float.MinValue, float.MinValue);
        for (int i = 0; i < mesh.VerticesCount; i++)
        {
            float x = mesh.xyz[i * 3], y = mesh.xyz[i * 3 + 1], z = mesh.xyz[i * 3 + 2];
            min.Set(Math.Min(min.X, x), Math.Min(min.Y, y), Math.Min(min.Z, z));
            max.Set(Math.Max(max.X, x), Math.Max(max.Y, y), Math.Max(max.Z, z));
        }
        return (min, max);
    }

    private static void Fit(MeshData mesh)
    {
        if (mesh.VerticesCount == 0)
        {
            return;
        }
        var (min, max) = Bounds(mesh);
        var size = max - min;
        var centre = new Vec3f((min.X + max.X) / 2, (min.Y + max.Y) / 2, (min.Z + max.Z) / 2);
        // Lying down, it stands up: its longest side turned to point up.
        if (size.X > size.Y && size.X >= size.Z)
        {
            mesh.Rotate(centre, 0, 0, GameMath.PIHALF);
        }
        else if (size.Z > size.Y)
        {
            mesh.Rotate(centre, GameMath.PIHALF, 0, 0);
        }
        (min, max) = Bounds(mesh);
        size = max - min;
        float scale = Math.Min(1f, Math.Min(PlaceWidth / Math.Max(size.X, 0.001f), Math.Min(PlaceHeight / Math.Max(size.Y, 0.001f), PlaceDepth / Math.Max(size.Z, 0.001f))));
        mesh.Translate(0.5f - (min.X + max.X) / 2, -min.Y, 0.5f - (min.Z + max.Z) / 2);
        mesh.Scale(new Vec3f(0.5f, 0, 0.5f), scale, scale, scale);
    }

    public override void ToTreeAttributes(ITreeAttribute tree)
    {
        base.ToTreeAttributes(tree);
        for (int i = 0; i < Places; i++)
        {
            tree.SetBool("dormant" + i, dormant[i]);
        }
    }

    public override void FromTreeAttributes(ITreeAttribute tree, IWorldAccessor worldForResolving)
    {
        base.FromTreeAttributes(tree, worldForResolving);
        for (int i = 0; i < Places; i++)
        {
            dormant[i] = tree.GetBool("dormant" + i);
        }
        RedrawAfterReceivingTreeAttributes(worldForResolving);
    }

    public override void GetBlockInfo(IPlayer forPlayer, StringBuilder dsc)
    {
        var names = Items.Select((s, i) => s == null ? null
            : $"{i + 1}. {s.GetName()}" + (IsDormant(i) ? " " + Lang.Get(LemuriaModSystem.ModId + ":controlshelf-dormant")
                : PrefixOf(i + 1) != null && ShelfWork.KindOf(Items.ElementAtOrDefault(i + 1)) is ShelfWork.Kind.Shovel or ShelfWork.Kind.Axe ? " " + Lang.Get(LemuriaModSystem.ModId + ":controlshelf-prefix") : ""))
            .Where(n => n != null).ToList();
        dsc.AppendLine(names.Count == 0 ? Lang.Get(LemuriaModSystem.ModId + ":controlshelf-empty") : string.Join("\n", names));
    }
}
