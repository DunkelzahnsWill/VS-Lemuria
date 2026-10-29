using System;
using System.Linq;
using System.Text;
using Lemuria.Core;
using Lemuria.Crystals;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;
using Vintagestory.API.Util;
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
        // An item put on or taken off starts awake.
        inventory.SlotModified += place =>
        {
            if (place >= 0 && place < Places)
            {
                quiet[place] = Quiet.Awake;
            }
        };
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

    /// <summary>Why an item on the shelf is dark, if it is (H2, note rev 44; H4, note rev 56).</summary>
    public enum Quiet : byte
    {
        Awake,
        /// <summary>Switched off with the rod; it stays off until a right-click wakes it.</summary>
        Off,
        /// <summary>It uses stock and the input holds none of its kind; it wakes when its kind arrives.</summary>
        OutOfStock,
        /// <summary>It was checked with nothing to do in its reach; it stays quiet until a rod tap.</summary>
        Done,
    }

    private readonly Quiet[] quiet = new Quiet[Places];

    /// <summary>
    /// The whole shelf Dormant (H5): with a Dead Socket in its socket, an item out of stock sends every
    /// place dark until that stock arrives or a sneak tap wakes it.
    /// </summary>
    private bool asleep;

    /// <summary>The fitting in the socket in the middle of the shelf's top: a Dead Socket, or null.</summary>
    private ItemStack? socket;

    /// <summary>The input whose change event this shelf listens to, so stock arriving wakes an item. Server only.</summary>
    private InventoryBase? listened;

    /// <summary>The items on the shelf, first to last.</summary>
    public ItemStack?[] Items => Enumerable.Range(0, Places).Select(i => inventory[i].Itemstack).ToArray();

    /// <summary>Why the item in this place is dark, or Awake; an empty place is Awake.</summary>
    public Quiet QuietOf(int place) => place >= 0 && place < Places && !inventory[place].Empty ? quiet[place] : Quiet.Awake;

    /// <summary>
    /// Whether the machine passes over the item in this place: switched off, quiet, or the whole shelf
    /// Dormant. Only an item switched off is passed over as stock too (H2); a quiet one keeps its kind
    /// back in the input, so the stock that wakes it is there when it does.
    /// </summary>
    public bool IsDormant(int place) => place >= 0 && place < Places && !inventory[place].Empty && (asleep || quiet[place] != Quiet.Awake);

    /// <summary>Whether the whole shelf is Dormant for want of stock (H5).</summary>
    public bool Asleep => asleep;

    /// <summary>Whether a Dead Socket sits in the shelf's socket.</summary>
    public bool HasSocket => socket != null;

    /// <summary>Whether this stack is a fitting the shelf's socket takes: the Dead Socket, as any socket takes it.</summary>
    public static bool IsFitting(ItemStack? stack) => stack?.Collectible?.Attributes?["lemuriaSocketFitting"].AsBool() == true;

    /// <summary>
    /// The item in this place that is a shovel's or an axe's prefix (H1): an item that names no action,
    /// in the place just left of it. Null when there is none.
    /// </summary>
    public ItemStack? PrefixOf(int place) =>
        place > 0 && inventory[place - 1].Itemstack is { } before && ShelfWork.KindOf(before) == ShelfWork.Kind.None ? before : null;

    /// <summary>
    /// A rod's right-click on a place: an awake item that names an action switches off, and a dark one
    /// wakes, whatever made it dark, and wakes the shelf with it (H2, H4); a prefix, an empty place or
    /// anything else does nothing. Returns whether it switched. Server only.
    /// </summary>
    public bool SwitchDormant(int place)
    {
        if (place < 0 || place >= Places || inventory[place].Itemstack is not { } stack || ShelfWork.KindOf(stack) == ShelfWork.Kind.None)
        {
            return false;
        }
        bool off = !IsDormant(place);
        quiet[place] = off ? Quiet.Off : Quiet.Awake;
        if (!off)
        {
            asleep = false;
        }
        Api.World.PlaySoundAt(new AssetLocation(LemuriaModSystem.ModId, off ? "sounds/machines/switch-off" : "sounds/machines/switch-on"), Pos, 0, null, true, 12f, LemuriaSound.Volume(0.6f));
        Changed();
        return true;
    }

    /// <summary>
    /// A sneak and right-click with the rod (H4): the shelf and every quiet item wake together; an item
    /// switched off by hand stays off. Server only.
    /// </summary>
    public void WakeAll()
    {
        asleep = false;
        for (int i = 0; i < Places; i++)
        {
            if (quiet[i] != Quiet.Off)
            {
                quiet[i] = Quiet.Awake;
            }
        }
        Api.World.PlaySoundAt(new AssetLocation(LemuriaModSystem.ModId, "sounds/machines/switch-on"), Pos, 0, null, true, 12f, LemuriaSound.Volume(0.6f));
        Changed();
    }

    /// <summary>
    /// The machine found an item with nothing to do (H4): out of stock or done, it goes quiet and is
    /// its plug dark. With a Dead Socket, out of stock sends the whole shelf Dormant (H5). Server only.
    /// </summary>
    public void Quieten(int place, Quiet why)
    {
        if (QuietOf(place) != Quiet.Awake)
        {
            return;
        }
        quiet[place] = why;
        if (why == Quiet.OutOfStock && socket != null)
        {
            asleep = true;
        }
        MarkDirty(true);
    }

    /// <summary>
    /// Listens to the input's own change event, the one the game's chutes listen to, so stock arriving
    /// wakes an item out of stock without anything polling; a new input is looked over once. Server only;
    /// called on each of the machine's turns, where it costs one block lookup.
    /// </summary>
    public void Listen()
    {
        var now = Input?.Inventory as InventoryBase;
        if (ReferenceEquals(now, listened))
        {
            return;
        }
        Unlisten();
        listened = now;
        if (now != null)
        {
            now.SlotModified += OnInputChanged;
            for (int i = 0; i < now.Count; i++)
            {
                OnInputChanged(i);
            }
        }
    }

    private void Unlisten()
    {
        if (listened != null)
        {
            listened.SlotModified -= OnInputChanged;
            listened = null;
        }
    }

    /// <summary>A slot of the input changed: an item out of stock whose kind is now there wakes, and the shelf with it.</summary>
    private void OnInputChanged(int slotId)
    {
        if (Api?.Side != EnumAppSide.Server || listened == null || slotId < 0 || slotId >= listened.Count || listened[slotId].Itemstack is not { } arrived)
        {
            return;
        }
        bool woke = false;
        for (int i = 0; i < Places; i++)
        {
            if (QuietOf(i) == Quiet.OutOfStock && ShelfWork.IsStockFor(Api.World, this, i, arrived))
            {
                quiet[i] = Quiet.Awake;
                woke = true;
            }
        }
        if (woke)
        {
            asleep = false;
            MarkDirty(true);
        }
    }

    public override void OnBlockRemoved()
    {
        Unlisten();
        base.OnBlockRemoved();
    }

    public override void OnBlockUnloaded()
    {
        Unlisten();
        base.OnBlockUnloaded();
    }

    public override void OnBlockBroken(IPlayer? byPlayer = null)
    {
        if (socket != null && Api.Side == EnumAppSide.Server)
        {
            Api.World.SpawnItemEntity(socket, Pos.ToVec3d().Add(0.5, 1.1, 0.5));
            socket = null;
        }
        base.OnBlockBroken(byPlayer);
    }

    /// <summary>Sets or clears the fitting in the shelf's socket without a player; for the test command. Server only.</summary>
    public void SetSocket(ItemStack? stack)
    {
        socket = stack;
        if (stack == null)
        {
            asleep = false;
        }
        Changed();
    }

    /// <summary>
    /// The socket in the middle of the shelf's top (H5): a Dead Socket goes in with a right-click, as it
    /// does in any socket, and an empty hand takes it out. Server only.
    /// </summary>
    private bool InteractSocket(IPlayer player, ItemSlot held)
    {
        if (socket == null && IsFitting(held.Itemstack))
        {
            if (Api.Side == EnumAppSide.Server)
            {
                socket = held.TakeOut(1);
                held.MarkDirty();
                Api.World.PlaySoundAt(new AssetLocation("game", "sounds/block/ceramicplace"), Pos, 0, null, true, 12f, LemuriaSound.Volume(0.8f));
                Changed();
            }
            return true;
        }
        if (socket != null && held.Empty)
        {
            if (Api.Side == EnumAppSide.Server)
            {
                var stack = socket;
                socket = null;
                asleep = false;
                if (!player.InventoryManager.TryGiveItemstack(stack, true))
                {
                    Api.World.SpawnItemEntity(stack, Pos.ToVec3d().Add(0.5, 1.1, 0.5));
                }
                Api.World.PlaySoundAt(new AssetLocation("game", "sounds/player/collect"), Pos, 0, null, true, 12f, LemuriaSound.Volume(0.8f));
                Changed();
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// A right-click with the Crystal Rod: sneaking, on any face, it wakes the shelf and its quiet items;
    /// on a place of the open face it switches that item off or wakes it. Server only.
    /// </summary>
    public void RodTap(BlockSelection blockSel, bool sneaking)
    {
        if (sneaking)
        {
            WakeAll();
        }
        else if (blockSel.Face == Side)
        {
            SwitchDormant(PlaceAt(blockSel.HitPosition));
        }
    }

    /// <summary>The place on the ledge under a hit on the open face, 0 to 3 from left to right as you face it.</summary>
    public int PlaceAt(Vec3d hit)
    {
        var (u, _) = Sparkstone.Sockets.Uv(Side, hit);
        return Math.Clamp((int)(u * Places), 0, Places - 1);
    }

    public bool OnInteract(IPlayer player, BlockSelection blockSel)
    {
        var held = player.InventoryManager.ActiveHotbarSlot;
        bool rod = held.Itemstack?.Collectible is ItemCrystalRod;
        bool sneaking = player.Entity.Controls.Sneak || player.Entity.Controls.ShiftKey;
        if (blockSel.Face != Side && !(rod && sneaking) && !(blockSel.Face == BlockFacing.UP && (socket != null || IsFitting(held.Itemstack))))
        {
            return false;
        }
        if (Api.Side == EnumAppSide.Server && !LemuriaClaims.TryAccess(Api.World, player, Pos, EnumBlockAccessFlags.Use))
        {
            return true;
        }
        if (rod)
        {
            // The rod switches an item Dormant or wakes it, or wakes the whole shelf, and never goes on the shelf itself.
            if (Api.Side == EnumAppSide.Server)
            {
                RodTap(blockSel, sneaking);
            }
            return true;
        }
        if (blockSel.Face == BlockFacing.UP)
        {
            return InteractSocket(player, held);
        }
        int place = PlaceAt(blockSel.HitPosition);
        if (held.Empty)
        {
            if (inventory[place].Empty)
            {
                return false;
            }
            if (Api.Side == EnumAppSide.Server)
            {
                var stack = inventory[place].TakeOutWhole();
                inventory[place].MarkDirty();
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
            inventory[target].MarkDirty();
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
        Fit(mesh, slot.Itemstack?.Class == EnumItemClass.Item);
        MeshCache[getMeshCacheKey(slot)] = mesh;
        return mesh;
    }

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

    /// <summary>
    /// Stands a model upright in its place, shrunk to fit. An item (a tool, seeds) is also turned so its
    /// broad side faces the player and its outline shows (play-test, Oct 29 03:39Z); a block stays as it is.
    /// </summary>
    private static void Fit(MeshData mesh, bool showOutline)
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
        if (showOutline && size.X < size.Z)
        {
            // Its thin side toward the player, its outline across the shelf.
            mesh.Rotate(new Vec3f((min.X + max.X) / 2, (min.Y + max.Y) / 2, (min.Z + max.Z) / 2), 0, GameMath.PIHALF, 0);
            (min, max) = Bounds(mesh);
            size = max - min;
        }
        float scale = Math.Min(1f, Math.Min(PlaceWidth / Math.Max(size.X, 0.001f), Math.Min(PlaceHeight / Math.Max(size.Y, 0.001f), PlaceDepth / Math.Max(size.Z, 0.001f))));
        mesh.Translate(0.5f - (min.X + max.X) / 2, -min.Y, 0.5f - (min.Z + max.Z) / 2);
        mesh.Scale(new Vec3f(0.5f, 0, 0.5f), scale, scale, scale);
    }

    private static readonly AssetLocation MarksShape = new(LemuriaModSystem.ModId, "shapes/block/machines/controlshelf-marks.json");

    private static readonly string[] PortMarks = { "portin-side", "portin-front", "portout-side", "portout-front" };

    /// <summary>
    /// Whether the plug under this place is lit (note rev 74): its item is Active, neither switched off
    /// nor quiet, and the shelf is not Dormant. An empty place or an item that does nothing has a dark
    /// plug, and a prefix's plug follows its tool's.
    /// </summary>
    public bool PlugLit(int place)
    {
        if (asleep || place < 0 || place >= Places || inventory[place].Itemstack is not { } stack)
        {
            return false;
        }
        if (ShelfWork.KindOf(stack) == ShelfWork.Kind.None)
        {
            return PrefixOf(place + 1) != null && ShelfWork.KindOf(inventory[place + 1].Itemstack) is ShelfWork.Kind.Shovel or ShelfWork.Kind.Axe && PlugLit(place + 1);
        }
        return quiet[place] == Quiet.Awake;
    }

    /// <summary>
    /// The Dead Socket in the shelf's socket, built as its own shape on the block atlas and set on the
    /// shelf's top; a plug under each place, lit or dark; and the port glyphs at its left and right,
    /// lit while the shelf is awake and dark while it is Dormant.
    /// </summary>
    public override bool OnTesselation(ITerrainMeshPool mesher, ITesselatorAPI tessThreadTesselator)
    {
        if (Api is ICoreClientAPI capi)
        {
            if (socket != null && FittingMesh(capi, socket) is { } fitting)
            {
                mesher.AddMeshData(fitting);
            }
            for (int i = 0; i < Places; i++)
            {
                if (Mark(capi, "plug" + i, PlugLit(i)) is { } plug)
                {
                    mesher.AddMeshData(plug);
                }
            }
            foreach (var name in PortMarks)
            {
                if (Mark(capi, name, !asleep) is { } port)
                {
                    mesher.AddMeshData(port);
                }
            }
        }
        return base.OnTesselation(mesher, tessThreadTesselator);
    }

    /// <summary>One mark of the shelf, lit or dark, turned with the shelf; cached per mark, state and side.</summary>
    private MeshData? Mark(ICoreClientAPI capi, string name, bool lit)
    {
        var meshes = ObjectCacheUtil.GetOrCreate(capi, LemuriaModSystem.ModId + "-shelfmarks", () => new System.Collections.Generic.Dictionary<string, MeshData?>());
        string key = $"{name}/{lit}/{Block.Code}";
        lock (meshes)
        {
            if (meshes.TryGetValue(key, out var cached))
            {
                return cached;
            }
        }
        MeshData? mesh = null;
        if (Shape.TryGet(capi, MarksShape) is { } shape)
        {
            capi.Tesselator.TesselateShape("lemuria-shelfmarks", shape, out mesh, new LitTextures(capi.Tesselator.GetTextureSource(Block), lit ? "-full" : ""), null, 0, 0, 0, null, new[] { name });
            if (mesh != null)
            {
                CrystalMeshes.SetGlow(mesh, lit ? (name.StartsWith("plug", StringComparison.Ordinal) ? 160 : 255) : 0);
                // The same turn the block's shape takes for its side, as the items' matrices do.
                float turn = Block.Shape?.rotateY ?? 0;
                mesh.MatrixTransform(new Matrixf().Translate(0.5f, 0, 0.5f).RotateYDeg(turn).Translate(-0.5f, 0, -0.5f).Values);
            }
        }
        lock (meshes)
        {
            meshes[key] = mesh;
        }
        return mesh;
    }

    /// <summary>The block's textures, each looked up lit ("-full") when the mark is lit.</summary>
    private class LitTextures : ITexPositionSource
    {
        private readonly ITexPositionSource inner;
        private readonly string suffix;

        public LitTextures(ITexPositionSource inner, string suffix)
        {
            this.inner = inner;
            this.suffix = suffix;
        }

        public TextureAtlasPosition? this[string textureCode] => inner[textureCode + suffix] ?? inner[textureCode];

        public Size2i? AtlasSize => inner.AtlasSize;
    }

    private MeshData? FittingMesh(ICoreClientAPI capi, ItemStack stack)
    {
        var meshes = ObjectCacheUtil.GetOrCreate(capi, LemuriaModSystem.ModId + "-shelfsocket", () => new System.Collections.Generic.Dictionary<string, MeshData?>());
        string key = $"{stack.Collectible.Code}/{Block.Code}";
        lock (meshes)
        {
            if (meshes.TryGetValue(key, out var cached))
            {
                return cached;
            }
        }
        MeshData? mesh = null;
        var shapeBase = stack.Item?.Shape?.Base ?? stack.Block?.Shape?.Base;
        if (shapeBase != null && Shape.TryGet(capi, shapeBase.Clone().WithPathPrefixOnce("shapes/").WithPathAppendixOnce(".json")) is { } shape)
        {
            var textures = stack.Item?.Textures ?? stack.Block?.Textures;
            capi.Tesselator.TesselateShape("lemuria-shelfsocket", shape, out mesh, new Sparkstone.BlockEntitySparkstone.FittingTextures(capi, capi.Tesselator.GetTextureSource(Block), textures));
            // Standing on the socket's mark, a hair above it.
            mesh.Translate(0, 1f + 0.13f / 16f, 0);
        }
        lock (meshes)
        {
            meshes[key] = mesh;
        }
        return mesh;
    }

    public override void ToTreeAttributes(ITreeAttribute tree)
    {
        base.ToTreeAttributes(tree);
        for (int i = 0; i < Places; i++)
        {
            tree.SetInt("quiet" + i, (int)quiet[i]);
        }
        tree.SetBool("asleep", asleep);
        if (socket != null)
        {
            tree.SetItemstack("socket", socket);
        }
        else
        {
            tree.RemoveAttribute("socket");
        }
    }

    public override void FromTreeAttributes(ITreeAttribute tree, IWorldAccessor worldForResolving)
    {
        base.FromTreeAttributes(tree, worldForResolving);
        for (int i = 0; i < Places; i++)
        {
            // A shelf saved before quiet items kept only whether an item was switched off.
            quiet[i] = tree.HasAttribute("quiet" + i) ? (Quiet)Math.Clamp(tree.GetInt("quiet" + i), 0, 3)
                : tree.GetBool("dormant" + i) ? Quiet.Off : Quiet.Awake;
        }
        asleep = tree.GetBool("asleep");
        socket = tree.GetItemstack("socket");
        socket?.ResolveBlockOrItem(worldForResolving);
        RedrawAfterReceivingTreeAttributes(worldForResolving);
    }

    public override void GetBlockInfo(IPlayer forPlayer, StringBuilder dsc)
    {
        // Each dark item says why it is dark: switched off, out of stock or done (H4).
        string Why(int i) => QuietOf(i) switch
        {
            Quiet.Off => " " + Lang.Get(LemuriaModSystem.ModId + ":controlshelf-dormant"),
            Quiet.OutOfStock => " " + Lang.Get(LemuriaModSystem.ModId + ":controlshelf-outofstock"),
            Quiet.Done => " " + Lang.Get(LemuriaModSystem.ModId + ":controlshelf-done"),
            _ => PrefixOf(i + 1) != null && ShelfWork.KindOf(Items.ElementAtOrDefault(i + 1)) is ShelfWork.Kind.Shovel or ShelfWork.Kind.Axe ? " " + Lang.Get(LemuriaModSystem.ModId + ":controlshelf-prefix") : "",
        };
        var names = Items.Select((s, i) => s == null ? null : $"{i + 1}. {s.GetName()}" + Why(i)).Where(n => n != null).ToList();
        dsc.AppendLine(names.Count == 0 ? Lang.Get(LemuriaModSystem.ModId + ":controlshelf-empty") : string.Join("\n", names));
        if (socket != null)
        {
            dsc.AppendLine(Lang.Get(LemuriaModSystem.ModId + ":controlshelf-socket", socket.GetName()));
        }
        if (asleep)
        {
            dsc.AppendLine(Lang.Get(LemuriaModSystem.ModId + ":controlshelf-asleep"));
        }
    }
}
