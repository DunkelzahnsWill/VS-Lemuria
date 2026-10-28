using System;
using System.Collections.Generic;
using System.Linq;
using Lemuria.Core;
using Lemuria.Crystals;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;
using Vintagestory.API.Util;

namespace Lemuria.Sparkstone;

/// <summary>
/// One block of Awakened sparkstone (design note, Awakened sparkstone and touching charge, Glyphs):
/// it keeps its share of its network's charge, and its glyphs glow by the network's charge level in a
/// deep cyan, without lighting the area. The network itself is worked out every 10 seconds by
/// <see cref="SparkstoneNetworks"/>; clients get the level through the game's block entity sync.
/// Step 4 (note "Lemuria step 4: Machines", Sockets, Capacitors, Active and Dormant): every Awakened
/// block has sockets for crystals up to its room, and is Active or Dormant. In a Free block, one not
/// carved into a machine, an aligned crystal is a capacitor whose capacity and charge join the
/// network's. A Dormant block is out of its network, keeps its charge, and its glyphs go dark.
/// After the owner's play-test (note rev 78, How machines look): lit glyphs are deep cyan, each socket
/// has a round mark, and a socketed crystal sits in a plug that is lit while it can work (note rev 96:
/// each Evolution's plug fills the marks it takes, round for Glittering, long across a row for Glowing).
/// Machines (Lemuria/Machines) build on this class.
/// </summary>
public class BlockEntitySparkstone : BlockEntity
{
    public const string ClassCode = "lemuria-sparkstone";

    private const string MeshCacheKey = LemuriaModSystem.ModId + "-sparkstonemeshes";
    private const string PartCacheKey = LemuriaModSystem.ModId + "-socketparts";

    private static readonly AssetLocation MarkShape = new(LemuriaModSystem.ModId, "shapes/block/sparkstone/socketmark.json");
    private static readonly AssetLocation CollarShape = new(LemuriaModSystem.ModId, "shapes/block/sparkstone/socketcollar.json");
    private static readonly AssetLocation GlowingCollarShape = new(LemuriaModSystem.ModId, "shapes/block/sparkstone/socketcollar-glowing.json");

    private static readonly string[] GlyphCodes = { "glyph", "glyph-low", "glyph-high", "glyph-full" };
    private static readonly int[] GlyphGlow = { 0, 90, 170, 255 };

    private static readonly AssetLocation PlaceSound = new("game", "sounds/block/ceramicplace");
    private static readonly AssetLocation TakeSound = new("game", "sounds/player/collect");
    private static readonly AssetLocation DullSound = new(LemuriaModSystem.ModId, "sounds/crystals/dull");

    private float share;
    private int level;
    private bool active = true;
    /// <summary>The face a Free block's sockets are on, from its first crystal until it is empty again; -1 for none.</summary>
    private int socketFace = -1;

    protected readonly List<SocketCrystal> sockets = new();

    public float Share => share;
    public int Level => level;
    public bool Active => active;
    public float HumStrength => active ? Crystal.Hum(level) : 0f;

    public IReadOnlyList<SocketCrystal> Crystals => sockets;

    /// <summary>A Free block is Awakened sparkstone not carved into a machine; its aligned crystals are capacitors.</summary>
    public virtual bool IsFree => true;

    /// <summary>The face the sockets are on: a machine's front, or a Free block's chosen face.</summary>
    public virtual BlockFacing? SocketFacing => socketFace >= 0 ? BlockFacing.ALLFACES[socketFace] : null;

    /// <summary>Sockets in an Awakened block (RoomAwakened, 4).</summary>
    public static int Room => Math.Clamp(LemuriaModSystem.Config.RoomAwakened, 1, 16);

    /// <summary>Sockets a crystal of this Evolution fills: Glittering 1, Glowing 2 by default.</summary>
    public static int SocketsFor(string evolution) => evolution == Crystal.Glowing
        ? Math.Clamp(LemuriaModSystem.Config.SocketsGlowing, 1, 16)
        : Math.Clamp(LemuriaModSystem.Config.SocketsGlittering, 1, 16);

    /// <summary>Sockets in this block: <see cref="Room"/>, unless the block is made to hold more or fewer.</summary>
    public virtual int SocketRoom => Room;

    /// <summary>Sockets a crystal fills in this block, by its Evolution unless the block counts otherwise.</summary>
    public virtual int SizeOf(SocketCrystal crystal) => SocketsFor(crystal.Evolution);

    public int UsedRoom => sockets.Sum(c => c.Size);

    /// <summary>Whether its aligned crystals are capacitors, adding their store to the block's: a Free block's are.</summary>
    public virtual bool StoresInSockets => IsFree;

    /// <summary>The charge the block itself holds, before its capacitors: 50 by default.</summary>
    protected virtual float OwnCapacity => SparkstoneNetworks.CapacityPerBlock;

    /// <summary>The charge this block can hold: its own, and its capacitors'.</summary>
    public float Capacity => OwnCapacity + (StoresInSockets ? sockets.Sum(c => c.Capacity) : 0);

    /// <summary>How full this block is, which is how full its network is.</summary>
    public float Fraction => Capacity <= 0.0001f ? 0 : Math.Clamp(share / Capacity, 0, 1);

    public override void Initialize(ICoreAPI api)
    {
        base.Initialize(api);
        foreach (var crystal in sockets)
        {
            crystal.Stack.ResolveBlockOrItem(api.World);
        }
        if (api.Side == EnumAppSide.Server)
        {
            SparkstoneModSystem.Networks?.Add(this);
            foreach (var crystal in sockets)
            {
                Register(crystal);
            }
        }
        else
        {
            HumBudget.AddSource(this);
            UpdateClientSounds();
        }
    }

    private static void Register(SocketCrystal crystal)
    {
        if (crystal.IsAligned)
        {
            CrystalsModSystem.Standing?.Add(crystal);
        }
    }

    /// <summary>Sets this block's share and its network's level; syncs to clients only when something shows. Server only.</summary>
    public void Set(float newShare, int newLevel)
    {
        bool levelChanged = newLevel != level;
        bool shareChanged = Math.Abs(newShare - share) > 0.01f;
        share = Math.Max(0, newShare);
        level = newLevel;
        if (levelChanged)
        {
            MarkDirty(true);
        }
        else if (shareChanged)
        {
            // Saved with the chunk; the client sees no difference until the level changes.
            Api.World.BlockAccessor.GetChunkAtBlockPos(Pos)?.MarkModified();
        }
    }

    /// <summary>Charge a Gather block poured in since the network last fed, up to the room left; 0 for plain stone. Server only.</summary>
    public virtual float TakeGathered(float room) => 0;

    /// <summary>A lightning strike within 16 blocks of a crystal in a socket.</summary>
    public virtual void OnSocketLightning(SocketCrystal crystal)
    {
    }

    // ------------------------------------------------------------------ Active and Dormant

    /// <summary>
    /// Switches the block Active or Dormant (note, Active and Dormant). A Dormant block leaves its network
    /// with its share, so its capacitors keep their charge; switching it Active joins it again. The
    /// networks on both sides are worked out once. Server only.
    /// </summary>
    public void SetActive(bool on)
    {
        if (active == on)
        {
            return;
        }
        var networks = SparkstoneModSystem.Networks;
        var neighbours = on ? null : NeighbourStones();
        active = on;
        MarkDirty(true);
        OnActiveChanged();
        if (networks == null)
        {
            return;
        }
        if (on)
        {
            networks.Update(networks.NetworkOf(this), feed: false);
            return;
        }
        var visited = new HashSet<BlockEntitySparkstone> { this };
        foreach (var next in neighbours!)
        {
            if (next.Active && !visited.Contains(next))
            {
                networks.Update(networks.NetworkOf(next, visited), feed: false);
            }
        }
    }

    protected virtual void OnActiveChanged()
    {
    }

    private List<BlockEntitySparkstone> NeighbourStones()
    {
        var found = new List<BlockEntitySparkstone>();
        foreach (var face in BlockFacing.ALLFACES)
        {
            if (Api.World.BlockAccessor.GetBlockEntity(Pos.AddCopy(face)) is BlockEntitySparkstone next)
            {
                found.Add(next);
            }
        }
        return found;
    }

    // ------------------------------------------------------------------ sockets

    /// <summary>
    /// Whether a stack can go into a socket at all: any crystal of Lemuria's, never a strange crystal or
    /// seedling, or a fitting whose attributes say so (<c>lemuriaSocketFitting</c>, the Dead Socket).
    /// </summary>
    public static bool Socketable(ItemStack? stack) =>
        stack != null && (Crystal.IsCrystal(stack.Collectible) || stack.Collectible?.Attributes?["lemuriaSocketFitting"].AsBool() == true);

    /// <summary>Whether this block takes the stack into its sockets: every crystal, and fittings only where a block says so.</summary>
    public virtual bool Accepts(ItemStack stack) => Socketable(stack) && Crystal.IsCrystal(stack.Collectible);

    /// <summary>Whether this crystal or fitting fits the room left.</summary>
    public bool Fits(SocketCrystal incoming) =>
        Sockets.Pack(sockets.Select(c => c.Size).Append(incoming.Size).ToList(), SocketRoom) != null;

    /// <summary>
    /// Sets one crystal into the first free sockets; a Free block's first crystal picks the face. Its
    /// charge joins the block's when it is a capacitor. Returns false when it does not fit. Server only.
    /// </summary>
    public bool Insert(ItemStack one, BlockFacing face)
    {
        if (!Accepts(one) || !Fits(new SocketCrystal(this, one)))
        {
            return false;
        }
        if (SocketFacing == null)
        {
            socketFace = face.Index;
        }
        var crystal = new SocketCrystal(this, one);
        sockets.Add(crystal);
        if (crystal.IsCapacitor)
        {
            share += Math.Clamp(Crystal.GetCharge(one), 0, crystal.Capacity);
        }
        if (Api.Side == EnumAppSide.Server)
        {
            Register(crystal);
        }
        OnSocketsChanged();
        return true;
    }

    /// <summary>Takes a crystal out with its charge: a capacitor leaves as full as its network. Server only.</summary>
    public virtual ItemStack TakeOut(SocketCrystal crystal)
    {
        var stack = crystal.Stack.Clone();
        if (crystal.IsCapacitor)
        {
            float charge = crystal.Charge;
            share = Math.Max(0, share - charge);
            Crystal.SetState(stack, charge, crystal.Cracks);
        }
        Remove(crystal);
        return stack;
    }

    private void Remove(SocketCrystal crystal)
    {
        sockets.Remove(crystal);
        CrystalsModSystem.Standing?.Remove(crystal);
        if (sockets.Count == 0 && IsFree)
        {
            socketFace = -1;
        }
        OnSocketsChanged();
    }

    /// <summary>A capacitor's charge leaves the block, as when a dialogue empties it.</summary>
    public void SpendCapacitor(SocketCrystal crystal)
    {
        if (crystal.IsCapacitor)
        {
            share = Math.Max(0, share - crystal.Charge);
        }
    }

    /// <summary>A crystal in a socket breaks in a burst of its colour and leaves nothing.</summary>
    public void Shatter(SocketCrystal crystal)
    {
        SpendCapacitor(crystal);
        var center = Pos.ToVec3d().Add(0.5, 0.5, 0.5);
        Api.World.PlaySoundAt(new AssetLocation(LemuriaModSystem.ModId, "sounds/crystals/shatter"), center.X, center.Y, center.Z, null, true, 24f, LemuriaSound.Volume(1f));
        CrystalsModSystem.Server?.SendBurst(center, crystal.Alignment ?? Crystal.Unaligned);
        Remove(crystal);
    }

    /// <summary>The crystal over the socket a hit on the socket face points at, or null.</summary>
    public SocketCrystal? CrystalAt(BlockFacing face, Vec3d hit)
    {
        if (SocketFacing != face)
        {
            return null;
        }
        var (u, v) = Sockets.Uv(face, hit);
        int cell = Sockets.CellAt(SocketRoom, u, v);
        return sockets.FirstOrDefault(c => cell >= c.Socket && cell < c.Socket + c.Size);
    }

    /// <summary>Packs the sockets again, rebalances the network when the capacity moved, and shows the change.</summary>
    public virtual void OnSocketsChanged()
    {
        var firsts = Sockets.Pack(sockets.Select(c => c.Size).ToList(), Math.Max(SocketRoom, UsedRoom));
        for (int i = 0; i < sockets.Count; i++)
        {
            sockets[i].Socket = firsts?[i] ?? i;
        }
        MarkDirty(true);
        if (Api?.Side == EnumAppSide.Server && active && SparkstoneModSystem.Networks is { } networks)
        {
            networks.Update(networks.NetworkOf(this), feed: false);
        }
    }

    /// <summary>
    /// A right-click with a crystal sets it in (sneaking still stands it on the stone, step 2); with an
    /// empty hand it takes back the crystal looked at. Returns whether the click was the sockets'.
    /// Both sides answer the same; the server does the work.
    /// </summary>
    public bool OnInteract(IPlayer player, BlockSelection blockSel)
    {
        var slot = player.InventoryManager.ActiveHotbarSlot;
        bool sneaking = player.Entity.Controls.Sneak || player.Entity.Controls.ShiftKey;
        if (slot?.Itemstack != null && Socketable(slot.Itemstack) && !sneaking)
        {
            if (!CanUse(player, blockSel))
            {
                return true;
            }
            if (Api.Side == EnumAppSide.Server)
            {
                var one = slot.Itemstack.Clone();
                one.StackSize = 1;
                if (Insert(one, SocketFacing ?? blockSel.Face))
                {
                    slot.TakeOut(1);
                    slot.MarkDirty();
                    Api.World.PlaySoundAt(PlaceSound, Pos, 0, null, true, 16f, LemuriaSound.Volume(1f));
                }
                else
                {
                    Api.World.PlaySoundAt(DullSound, Pos, 0, null, true, 12f, LemuriaSound.Volume(0.6f));
                }
            }
            return true;
        }
        if (slot?.Empty != false && !sneaking && CrystalAt(blockSel.Face, blockSel.HitPosition) is { } crystal)
        {
            if (!CanUse(player, blockSel))
            {
                return true;
            }
            if (Api.Side == EnumAppSide.Server)
            {
                var stack = TakeOut(crystal);
                if (!player.InventoryManager.TryGiveItemstack(stack, true))
                {
                    Api.World.SpawnItemEntity(stack, Pos.ToVec3d().Add(0.5, 0.5, 0.5).Add(blockSel.Face.Normald.Clone().Mul(0.7)));
                }
                Api.World.PlaySoundAt(TakeSound, Pos, 0, null, true, 16f, LemuriaSound.Volume(1f));
            }
            return true;
        }
        return false;
    }

    private bool CanUse(IPlayer player, BlockSelection blockSel) =>
        Api.Side != EnumAppSide.Server || LemuriaClaims.TryAccess(Api.World, player, blockSel.Position, EnumBlockAccessFlags.Use);

    public override void OnBlockBroken(IPlayer? byPlayer = null)
    {
        base.OnBlockBroken(byPlayer);
        if (Api.Side != EnumAppSide.Server)
        {
            return;
        }
        foreach (var crystal in sockets.ToList())
        {
            Api.World.SpawnItemEntity(TakeOut(crystal), Pos.ToVec3d().Add(0.5, 0.5, 0.5));
        }
    }

    public override void OnBlockRemoved()
    {
        base.OnBlockRemoved();
        if (Api.Side == EnumAppSide.Server)
        {
            SparkstoneModSystem.Networks?.Remove(this);
            Unregister();
        }
        else
        {
            StopClientSounds();
        }
    }

    public override void OnBlockUnloaded()
    {
        base.OnBlockUnloaded();
        if (Api?.Side == EnumAppSide.Server)
        {
            SparkstoneModSystem.Networks?.Remove(this);
            Unregister();
        }
        else if (Api != null)
        {
            StopClientSounds();
        }
    }

    private void Unregister()
    {
        foreach (var crystal in sockets)
        {
            CrystalsModSystem.Standing?.Remove(crystal);
        }
    }

    public override void ToTreeAttributes(ITreeAttribute tree)
    {
        base.ToTreeAttributes(tree);
        tree.SetFloat("share", share);
        tree.SetInt("level", level);
        tree.SetBool("dormant", !active);
        tree.SetInt("socketFace", socketFace);
        tree.SetInt("sockets", sockets.Count);
        for (int i = 0; i < sockets.Count; i++)
        {
            var sub = new TreeAttribute();
            sockets[i].ToTreeAttributes(sub);
            tree["socket" + i] = sub;
        }
        tree.SetFloat("grace", (float)LemuriaModSystem.Config.DialogueGraceSeconds);
    }

    public override void FromTreeAttributes(ITreeAttribute tree, IWorldAccessor worldForResolving)
    {
        base.FromTreeAttributes(tree, worldForResolving);
        int oldLevel = level;
        bool oldActive = active;
        share = tree.GetFloat("share");
        level = Math.Clamp(tree.GetInt("level"), Crystal.Empty, Crystal.Full);
        active = !tree.GetBool("dormant");
        socketFace = tree.GetInt("socketFace", -1);
        var oldSockets = sockets.Select(c => c.Stack.Collectible?.Code + "/" + c.Cracks + "/" + c.Dormant + "/" + c.Level + "/" + c.Warning).ToList();
        if (Api?.Side == EnumAppSide.Server)
        {
            Unregister();
        }
        sockets.Clear();
        int count = tree.GetInt("sockets");
        for (int i = 0; i < count; i++)
        {
            if (tree.GetTreeAttribute("socket" + i) is { } sub && SocketCrystal.FromTreeAttributes(this, sub, worldForResolving) is { } crystal)
            {
                sockets.Add(crystal);
            }
        }
        var firsts = Sockets.Pack(sockets.Select(c => c.Size).ToList(), Math.Max(SocketRoom, UsedRoom));
        for (int i = 0; i < sockets.Count; i++)
        {
            sockets[i].Socket = firsts?[i] ?? i;
        }
        if (Api?.Side == EnumAppSide.Server)
        {
            foreach (var crystal in sockets)
            {
                Register(crystal);
            }
        }
        if (Api?.Side == EnumAppSide.Client)
        {
            UpdateClientSounds(tree.GetFloat("grace", 30));
            var newSockets = sockets.Select(c => c.Stack.Collectible?.Code + "/" + c.Cracks + "/" + c.Dormant + "/" + c.Level + "/" + c.Warning).ToList();
            if (oldLevel != level || oldActive != active || !oldSockets.SequenceEqual(newSockets))
            {
                MarkDirty(true);
            }
        }
    }

    // ------------------------------------------------------------------ client

    private readonly HashSet<string> discordKeys = new();

    /// <summary>A socket crystal in a dialogue hums out of tune, as a standing one does.</summary>
    private void UpdateClientSounds(float grace = 30)
    {
        var hums = CrystalsModSystem.Client?.Hums;
        if (hums == null)
        {
            return;
        }
        var now = new HashSet<string>();
        for (int i = 0; i < sockets.Count; i++)
        {
            if (sockets[i].Warning)
            {
                string key = $"socket:{Pos.X},{Pos.Y},{Pos.Z}:{i}";
                now.Add(key);
                if (!discordKeys.Contains(key))
                {
                    hums.Start(key, Pos.ToVec3d().Add(0.5, 0.5, 0.5), 0, sockets[i].DialogueSeconds, grace);
                }
            }
        }
        foreach (var key in discordKeys.Where(k => !now.Contains(k)).ToList())
        {
            hums.Stop(key);
        }
        discordKeys.Clear();
        discordKeys.UnionWith(now);
    }

    private void StopClientSounds()
    {
        foreach (var key in discordKeys)
        {
            CrystalsModSystem.Client?.Hums?.Stop(key);
        }
        discordKeys.Clear();
        HumBudget.RemoveSource(this);
    }

    public override bool OnTesselation(ITerrainMeshPool mesher, ITesselatorAPI tessThreadTesselator)
    {
        var capi = (ICoreClientAPI)Api;
        var meshes = ObjectCacheUtil.GetOrCreate(capi, MeshCacheKey, () => new Dictionary<string, MeshData?>());
        int shown = active ? level : -1;
        string key = Block.Code + "/" + shown;
        MeshData? mesh;
        lock (meshes)
        {
            meshes.TryGetValue(key, out mesh);
        }
        if (mesh == null)
        {
            mesh = BuildMesh(capi, tessThreadTesselator, shown);
            lock (meshes)
            {
                meshes[key] = mesh;
            }
        }
        if (mesh == null)
        {
            return false;
        }
        mesher.AddMeshData(mesh);
        if (SocketMeshes(capi) is { } crystals)
        {
            mesher.AddMeshData(crystals);
        }
        return true;
    }

    /// <summary>The glyph textures at a level: lit by the network's level, dark (-1) while Dormant.</summary>
    protected static string GlyphSuffix(int shown) => shown <= 0 ? "" : GlyphCodes[shown]["glyph".Length..];

    protected static int GlyphGlowAt(int shown) => shown < 0 ? 0 : GlyphGlow[shown];

    /// <summary>The polished stone, and over it the glyph grooves in the level's texture and glow.</summary>
    protected virtual MeshData? BuildMesh(ICoreClientAPI capi, ITesselatorAPI tesselator, int shown)
    {
        var shape = Shape.TryGet(capi, Block.Shape.Base.Clone().WithPathPrefixOnce("shapes/").WithPathAppendixOnce(".json"));
        if (shape == null)
        {
            return null;
        }
        var source = tesselator.GetTextureSource(Block);
        tesselator.TesselateShape("lemuria-sparkstone", shape, out var stone, source, null, 0, 0, 0, null, new[] { "stone" });
        var glyphs = new GlyphTextures(source, GlyphSuffix(shown));
        tesselator.TesselateShape("lemuria-sparkstone", shape, out var overlay, glyphs, null, 0, 0, 0, null, new[] { "glyphs" });
        if (stone == null || overlay == null)
        {
            return stone ?? overlay;
        }
        CrystalMeshes.SetGlow(overlay, GlyphGlowAt(shown));
        stone.AddMeshData(overlay);
        return stone;
    }

    /// <summary>
    /// Whether a crystal in this block can do its work here: in Free stone only an aligned crystal, a
    /// capacitor, does anything. Machines say what their own verb needs.
    /// </summary>
    public virtual bool CanWork(SocketCrystal crystal) => crystal.IsAligned;

    /// <summary>A socketed crystal's plug is lit while it can work: not Dormant, its block Active, and of use there.</summary>
    public bool Lit(SocketCrystal crystal) => active && !crystal.Dormant && CanWork(crystal);

    /// <summary>
    /// The sockets as one mesh in block coordinates: a round mark for every socket on the socket face,
    /// and each crystal in its Evolution's plug, pointing out of the face and glowing by its level, a
    /// Dormant one dim. A Glowing crystal's long plug lies along its row. A machine always shows its marks; a Free block only while it holds a crystal.
    /// </summary>
    public MeshData? SocketMeshes(ICoreClientAPI capi)
    {
        var face = SocketFacing;
        if (face == null || (sockets.Count == 0 && IsFree))
        {
            return null;
        }
        MeshData? all = null;
        void Add(MeshData part)
        {
            if (all == null)
            {
                all = part;
            }
            else
            {
                all.AddMeshData(part);
            }
        }
        int room = Math.Max(SocketRoom, UsedRoom);
        var (rx, ry, rz) = Turn(face);
        if (Part(capi, MarkShape, "mark", "socket") is { } mark)
        {
            for (int i = 0; i < room; i++)
            {
                Add(OnFace(mark, face, Sockets.Point(face, room, i, 1), 0.004f, rx, ry, rz));
            }
        }
        foreach (var crystal in sockets)
        {
            var point = Sockets.Point(face, room, crystal.Socket, crystal.Size);
            bool glowing = crystal.Evolution == Crystal.Glowing;
            bool lit = Lit(crystal);
            if (Part(capi, glowing ? GlowingCollarShape : CollarShape, glowing ? "collar-glowing" : "collar", lit ? "socket-lit" : "socket") is { } collar)
            {
                // The long plug is built along x, which the face's turn keeps along the row except on east and west.
                var plug = glowing && face.Axis == EnumAxis.X
                    ? collar.Clone().Rotate(new Vec3f(0.5f, 0.5f, 0.5f), 0, GameMath.PIHALF, 0)
                    : collar;
                var placed = OnFace(plug, face, point, 0.004f, rx, ry, rz, plug == collar);
                CrystalMeshes.SetGlow(placed, lit ? 160 : 0);
                Add(placed);
            }
            if (crystal.IsFitting)
            {
                if (FittingMesh(capi, crystal.Stack) is { } fitting)
                {
                    Add(OnFace(fitting, face, point, 0.004f, rx, ry, rz));
                }
                continue;
            }
            var block = Crystal.BlockFor(capi.World, crystal.Alignment ?? Crystal.Unaligned, crystal.Evolution);
            if (block == null)
            {
                continue;
            }
            int glow = crystal.Dormant ? 20 : crystal.IsAligned ? Crystal.Glow(crystal.Level) : 120;
            var mesh = CrystalMeshes.Get(capi, block, glow, crystal.Cracks);
            if (mesh == null)
            {
                continue;
            }
            float scale = glowing ? 0.42f : 0.34f;
            Add(OnFace(mesh.Clone().Scale(new Vec3f(0.5f, 0, 0.5f), scale, scale, scale), face, point, -0.02f, rx, ry, rz, false));
        }
        return all;
    }

    /// <summary>A fitting's own item shape, built upright on the block's floor like a crystal; cached per item.</summary>
    private static MeshData? FittingMesh(ICoreClientAPI capi, ItemStack stack)
    {
        var parts = ObjectCacheUtil.GetOrCreate(capi, PartCacheKey, () => new Dictionary<string, MeshData?>());
        string key = "fitting/" + stack.Collectible.Code;
        lock (parts)
        {
            if (parts.TryGetValue(key, out var cached))
            {
                return cached;
            }
        }
        MeshData? mesh = null;
        if (stack.Item != null)
        {
            capi.Tesselator.TesselateItem(stack.Item, out mesh);
        }
        else if (stack.Block != null)
        {
            capi.Tesselator.TesselateBlock(stack.Block, out mesh);
        }
        lock (parts)
        {
            parts[key] = mesh;
        }
        return mesh;
    }

    /// <summary>Stands a mesh built upright on the block's floor on a point of the face, lifted this far off it.</summary>
    private MeshData OnFace(MeshData mesh, BlockFacing face, Vec3d point, float lift, float rx, float ry, float rz, bool clone = true)
    {
        var n = face.Normalf;
        return (clone ? mesh.Clone() : mesh)
            .Translate(0, 0.5f, 0)
            .Rotate(new Vec3f(0.5f, 0.5f, 0.5f), rx, ry, rz)
            .Translate((float)point.X - 0.5f + n.X * lift, (float)point.Y - 0.5f + n.Y * lift, (float)point.Z - 0.5f + n.Z * lift);
    }

    /// <summary>A socket mark or collar in this block's rock, its top in the groove or the lit texture; cached per rock.</summary>
    private MeshData? Part(ICoreClientAPI capi, AssetLocation shapeLocation, string name, string rim)
    {
        var parts = ObjectCacheUtil.GetOrCreate(capi, PartCacheKey, () => new Dictionary<string, MeshData?>());
        string key = $"{Block.Variant["rock"]}/{name}/{rim}";
        lock (parts)
        {
            if (parts.TryGetValue(key, out var cached))
            {
                return cached;
            }
        }
        MeshData? mesh = null;
        if (Shape.TryGet(capi, shapeLocation) is { } shape)
        {
            capi.Tesselator.TesselateShape("lemuria-socket", shape, out mesh, new RimTextures(capi.Tesselator.GetTextureSource(Block), rim));
        }
        lock (parts)
        {
            parts[key] = mesh;
        }
        return mesh;
    }

    /// <summary>The block's own textures, with a part's "rim" drawn in the groove or the lit texture.</summary>
    private class RimTextures : ITexPositionSource
    {
        private readonly ITexPositionSource inner;
        private readonly string rim;

        public RimTextures(ITexPositionSource inner, string rim)
        {
            this.inner = inner;
            this.rim = rim;
        }

        public TextureAtlasPosition? this[string textureCode] => inner[textureCode is "rim" or "socket" ? rim : textureCode];

        public Size2i? AtlasSize => inner.AtlasSize;
    }

    /// <summary>The turn, in radians about X, Y and Z, that points a standing crystal out of this face.</summary>
    public static (float X, float Y, float Z) Turn(BlockFacing face) => face.Code switch
    {
        "down" => (GameMath.PI, 0, 0),
        "north" => (-GameMath.PIHALF, 0, 0),
        "south" => (GameMath.PIHALF, 0, 0),
        "east" => (0, 0, -GameMath.PIHALF),
        "west" => (0, 0, GameMath.PIHALF),
        _ => (0, 0, 0),
    };

    protected class GlyphTextures : ITexPositionSource
    {
        private readonly ITexPositionSource inner;
        private readonly string suffix;

        public GlyphTextures(ITexPositionSource inner, string suffix)
        {
            this.inner = inner;
            this.suffix = suffix;
        }

        /// <summary>Every texture whose code starts with "glyph" or names a machine's form is looked up at the level.</summary>
        public TextureAtlasPosition? this[string textureCode] =>
            textureCode is "rock" ? inner[textureCode] : inner[textureCode + suffix] ?? inner[textureCode];

        public Size2i? AtlasSize => inner.AtlasSize;
    }

    public override void GetBlockInfo(IPlayer forPlayer, System.Text.StringBuilder dsc)
    {
        // The glyphs show the charge level; the look-at text adds only whether the stone is Dormant.
        if (!active)
        {
            dsc.AppendLine(Vintagestory.API.Config.Lang.Get(LemuriaModSystem.ModId + ":sparkstone-dormant"));
        }
    }
}
