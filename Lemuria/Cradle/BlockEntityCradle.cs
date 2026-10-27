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

namespace Lemuria.Cradle;

/// <summary>
/// A cradle's seedling and growth (design note "Lemuria step 1: Find and farm", The cradle and growth),
/// and since step 2 a crystal set in it, which refines, charges and mends there (design note "Lemuria
/// step 2: The Four Elementals", Refining, Charge, The evolution ring, Cracks and repair).
/// The server owns the state and the clock; clients get it through the game's block entity sync and
/// draw one cached model per stage or crystal. A stage ends after StageDaysAtDefaultMonth in-game days,
/// scaled like a crop by the world's month length and crop growth speed, with up to StageRandomness
/// either way. The clock is checked every 10 seconds, and an unloaded cradle catches up when loaded;
/// mending catches up too, while refining and charging pause unloaded and do not.
/// </summary>
public class BlockEntityCradle : BlockEntity, IStandingCrystal
{
    public const string ClassCode = "lemuria-cradle";

    public const int Empty = 0;
    public const int FirstStage = 1;
    public const int Ripe = 6;

    /// <summary>Raised on the server when a player lifts a ripe crystal out of a cradle.</summary>
    public static event Action<IPlayer>? Harvested;

    /// <summary>Hum strength (0 to 1) by stage, Empty to Ripe.</summary>
    private static readonly float[] Hum = { 0f, 0f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f };

    /// <summary>Light level by stage, Empty to Ripe; a candle is 7, a torch 14.</summary>
    private static readonly byte[] Light = { 0, 0, 0, 0, 0, 2, 5 };

    private static readonly AssetLocation ChimeSound = new(LemuriaModSystem.ModId, "sounds/crystals/chime");
    private static readonly AssetLocation RingSound = new(LemuriaModSystem.ModId, "sounds/crystals/ring");
    private static readonly AssetLocation DullSound = new(LemuriaModSystem.ModId, "sounds/crystals/dull");

    private const string MeshCacheKey = LemuriaModSystem.ModId + "-cradlemeshes";
    private const int TickMs = 10_000;

    /// <summary>How far above the block's floor a crystal stands in the basin, in blocks.</summary>
    private const float Floor = 4f / 16f;

    private int stage;
    private string? seedlingCode;
    private double nextStageTotalHours;
    /// <summary>How many growth stages ended with an evolution ring standing round the cradle.</summary>
    private int ringStages;

    /// <summary>The crystal set in the cradle, with its charge and cracks; null when it holds none.</summary>
    private ItemStack? crystal;
    private string? refineTarget;
    private double refineProgress;
    private double nextMendTotalHours;
    private double lastReadHours;
    private double skyBoostUntil;
    private float dialogueSeconds;
    private bool warning;
    /// <summary>What the sparks show: the alignment refining or charging, or null for none.</summary>
    private string? sparkAlignment;
    private bool sparkRefining;
    private int ticks;

    private ILoadedSound? note;
    private byte placedLight;

    public int Stage => stage;
    public bool IsEmpty => stage == Empty && crystal == null;
    public ItemStack? CrystalStack => crystal;
    public string? RefineTarget => refineTarget;
    public double RefineProgress => refineProgress;

    public BlockPos StandPos => Pos;
    public string? Alignment => crystal == null ? null : Crystal.AlignmentOf(crystal.Collectible);
    public string Evolution => crystal == null ? Crystal.Glittering : Crystal.EvolutionOf(crystal.Collectible);
    public float Charge => crystal == null ? 0 : Crystal.GetCharge(crystal);
    public int Cracks => crystal == null ? 0 : Crystal.GetCracks(crystal);
    private bool CrystalAligned => crystal != null && Alignment != Crystal.Unaligned;
    private int CrystalLevel => CrystalAligned ? Crystal.Level(Charge, Crystal.Capacity(Evolution)) : Crystal.Full;
    public bool TwinNear { get; set; }

    public float DialogueSeconds
    {
        get => dialogueSeconds;
        set => dialogueSeconds = value;
    }

    public float HumStrength
    {
        get
        {
            if (crystal == null)
            {
                return Hum[stage];
            }
            // An aligned crystal hums its own note from the block entity; the basin's ambient is Unaligned's.
            return CrystalAligned ? 0f : 0.5f;
        }
    }

    public byte[] LightHsv => LightNow();

    /// <summary>Whether an item is a Lemuria seedling, which grows in a cradle.</summary>
    public static bool IsSeedling(CollectibleObject? collectible) =>
        collectible?.Code?.Domain == LemuriaModSystem.ModId && collectible.Code.Path.StartsWith("seedling-", StringComparison.Ordinal);

    public override void Initialize(ICoreAPI api)
    {
        base.Initialize(api);
        crystal?.ResolveBlockOrItem(api.World);
        placedLight = LightNow()[2];
        if (api.Side == EnumAppSide.Server)
        {
            lastReadHours = api.World.Calendar.TotalHours;
            if (crystal != null)
            {
                CrystalsModSystem.Standing?.Add(this);
            }
            RegisterGameTickListener(_ => OnServerTick(), TickMs, api.World.Rand.Next(TickMs));
        }
        else
        {
            UpdateNote();
            RegisterGameTickListener(_ => ClientSparks(), 500, api.World.Rand.Next(500));
        }
    }

    private void OnServerTick()
    {
        Advance();
        Mend();
        if (crystal != null && ++ticks % (CrystalCharging.ReadIntervalMs / TickMs) == 0)
        {
            ReadSurroundings();
        }
    }

    /// <summary>
    /// Right-click: plant a seedling or set a crystal in an empty cradle, or with an empty hand harvest
    /// a ripe crystal or take out the crystal set in it.
    /// </summary>
    public bool OnInteract(IPlayer byPlayer)
    {
        var slot = byPlayer.InventoryManager.ActiveHotbarSlot;
        var held = slot?.Itemstack?.Collectible;
        if (IsEmpty && IsSeedling(held))
        {
            if (Api.Side == EnumAppSide.Server)
            {
                Plant(slot!.Itemstack!.Collectible.Code.ToString());
                if (byPlayer.WorldData.CurrentGameMode != EnumGameMode.Creative)
                {
                    slot.TakeOut(1);
                    slot.MarkDirty();
                }
                Api.World.PlaySoundAt(new AssetLocation("game:sounds/block/plant"), Pos, -0.25, null);
            }
            return true;
        }
        if (IsEmpty && Crystal.IsCrystal(held))
        {
            if (Api.Side == EnumAppSide.Server)
            {
                var one = slot!.Itemstack!.Clone();
                one.StackSize = 1;
                if (byPlayer.WorldData.CurrentGameMode != EnumGameMode.Creative)
                {
                    slot.TakeOut(1);
                    slot.MarkDirty();
                }
                SetCrystal(one);
                Api.World.PlaySoundAt(new AssetLocation("game:sounds/block/ceramicplace"), Pos, -0.25, null);
            }
            return true;
        }
        if (stage == Ripe && slot?.Empty != false)
        {
            if (Api.Side == EnumAppSide.Server)
            {
                var ripe = ContentStack();
                SetStage(Empty);
                seedlingCode = null;
                ringStages = 0;
                Give(byPlayer, ripe);
                Harvested?.Invoke(byPlayer);
            }
            return true;
        }
        if (crystal != null && slot?.Empty != false)
        {
            if (Api.Side == EnumAppSide.Server)
            {
                var taken = crystal;
                SetCrystal(null);
                Give(byPlayer, taken);
            }
            return true;
        }
        return false;
    }

    private void Give(IPlayer player, ItemStack? stack)
    {
        if (stack != null && !player.InventoryManager.TryGiveItemstack(stack, true))
        {
            Api.World.SpawnItemEntity(stack, Pos.ToVec3d().Add(0.5, 0.75, 0.5));
        }
        Api.World.PlaySoundAt(new AssetLocation("game:sounds/player/collect"), Pos, -0.25, null);
    }

    /// <summary>Sets a seedling growing at its first stage. Server only.</summary>
    public void Plant(string seedling)
    {
        seedlingCode = seedling;
        ringStages = 0;
        nextStageTotalHours = Api.World.Calendar.TotalHours + StageHours();
        SetStage(FirstStage);
    }

    /// <summary>Sets a crystal in the cradle, or takes it out with null. Unfinished refining is lost. Server only.</summary>
    public void SetCrystal(ItemStack? stack)
    {
        byte oldLight = LightNow()[2];
        if (crystal == null && stack != null)
        {
            CrystalsModSystem.Standing?.Add(this);
        }
        else if (crystal != null && stack == null)
        {
            CrystalsModSystem.Standing?.Remove(this);
        }
        crystal = stack;
        refineTarget = null;
        refineProgress = 0;
        dialogueSeconds = 0;
        warning = false;
        sparkAlignment = null;
        lastReadHours = Api.World.Calendar.TotalHours;
        nextMendTotalHours = Api.World.Calendar.TotalHours + StageHours();
        MarkDirty(true);
        UpdateLight(oldLight);
    }

    /// <summary>
    /// Moves the stage on for every stage whose time has come, so a cradle that was unloaded catches up
    /// at once. Each stage that ends reads the evolution ring. Server only; called every 10 seconds and
    /// by the test command.
    /// </summary>
    public void Advance()
    {
        if (stage < FirstStage || stage >= Ripe)
        {
            return;
        }
        int newStage = stage;
        double now = Api.World.Calendar.TotalHours;
        while (newStage < Ripe && now >= nextStageTotalHours)
        {
            newStage++;
            ReadRing();
            if (newStage < Ripe)
            {
                nextStageTotalHours += StageHours();
            }
        }
        if (newStage == stage)
        {
            return;
        }
        SetStage(newStage);
        if (newStage == Ripe)
        {
            Api.World.PlaySoundAt(ChimeSound, Pos.X + 0.5, Pos.Y + 0.75, Pos.Z + 0.5, null, false, 24f, 1f);
        }
    }

    private string SeedlingEvolution => seedlingCode == null ? Crystal.Glittering : new AssetLocation(seedlingCode).Path["seedling-".Length..];

    /// <summary>
    /// The evolution ring (design note, The evolution ring): two or more aligned crystals of the
    /// seedling's Evolution standing in the eight blocks round the cradle, at its height, placed or
    /// resting in a neighbouring cradle. In step 2 only Glittering seedlings are raised.
    /// </summary>
    private void ReadRing()
    {
        if (SeedlingEvolution != Crystal.Glittering)
        {
            return;
        }
        var colours = RingAlignments(SeedlingEvolution);
        var center = Pos.ToVec3d().Add(0.5, 0.6, 0.5);
        if (colours.Count >= 2)
        {
            ringStages++;
            // A rising note: the chime a little higher with every stage the ring has held.
            Api.World.PlaySoundAt(RingSound, center.X, center.Y, center.Z, null, 1f + 0.06f * ringStages, 16f, 0.8f);
            foreach (string alignment in colours)
            {
                CrystalCharging.Sparks(Api.World, center, Crystal.Rgb(alignment), 6, 0.3f);
            }
        }
        else
        {
            Api.World.PlaySoundAt(DullSound, center.X, center.Y, center.Z, null, false, 16f, 0.8f);
        }
    }

    /// <summary>The alignments of the aligned crystals of this Evolution in the eight blocks round the cradle.</summary>
    public List<string> RingAlignments(string evolution)
    {
        var found = new List<string>();
        var accessor = Api.World.BlockAccessor;
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dz = -1; dz <= 1; dz++)
            {
                if (dx == 0 && dz == 0)
                {
                    continue;
                }
                var pos = Pos.AddCopy(dx, 0, dz);
                var be = accessor.GetBlockEntity(pos);
                string? alignment = null;
                string? crystalEvolution = null;
                if (be is BlockEntityCrystal placed && placed.IsAligned)
                {
                    alignment = placed.Alignment;
                    crystalEvolution = placed.Evolution;
                }
                else if (be is BlockEntityCradle cradle && cradle.CrystalAligned)
                {
                    alignment = cradle.Alignment;
                    crystalEvolution = cradle.Evolution;
                }
                if (alignment != null && crystalEvolution == evolution)
                {
                    found.Add(alignment);
                }
            }
        }
        return found;
    }

    /// <summary>One stage's length in in-game hours, rolled afresh for every stage as a crop's is.</summary>
    private double StageHours()
    {
        var config = LemuriaModSystem.Config;
        var calendar = Api.World.Calendar;
        double days = Math.Clamp(config.StageDaysAtDefaultMonth, 0.01, 1000) * calendar.DaysPerMonth / 9.0;
        double randomness = Math.Clamp(config.StageRandomness, 0, 0.9);
        days *= 1 + randomness * (2 * Api.World.Rand.NextDouble() - 1);
        double growthRate = Api.World.Config.GetDecimal("cropGrowthRateMul", 1);
        if (growthRate <= 0)
        {
            growthRate = 1;
        }
        return days * calendar.HoursPerDay / growthRate;
    }

    /// <summary>Mends one crack per growth stage's length, catching up when the cradle was unloaded. Server only.</summary>
    public void Mend()
    {
        if (crystal == null)
        {
            return;
        }
        int cracks = Cracks;
        if (cracks <= 0)
        {
            return;
        }
        double now = Api.World.Calendar.TotalHours;
        int mended = 0;
        while (cracks > 0 && now >= nextMendTotalHours)
        {
            cracks--;
            mended++;
            nextMendTotalHours += StageHours();
        }
        if (mended > 0)
        {
            Crystal.SetState(crystal, Charge, cracks);
            Api.World.PlaySoundAt(ChimeSound, Pos.X + 0.5, Pos.Y + 0.75, Pos.Z + 0.5, null, false, 16f, 0.5f);
            MarkDirty(true);
        }
    }

    /// <summary>
    /// Every 30 seconds while it holds a crystal: an Unaligned crystal refines toward the alignment that
    /// leads and passes; an aligned one charges where its own alignment does. Server only.
    /// </summary>
    public void ReadSurroundings(double? forHours = null)
    {
        double now = Api.World.Calendar.TotalHours;
        double hours = forHours ?? Math.Max(0, now - lastReadHours);
        lastReadHours = now;
        if (crystal == null)
        {
            return;
        }
        bool boost = now < skyBoostUntil;
        var raw = CrystalEnvironment.Read(Api.World, Pos, boost);
        bool passes = CrystalEnvironment.Lead(raw, Crystal.Rank(Evolution), out var leader, out var strength);
        if (!CrystalAligned)
        {
            Refine(passes, leader, strength, hours);
            return;
        }
        bool charging = passes && leader == Alignment && !TwinNear && CrystalLevel < Crystal.Full;
        SetSparks(charging ? Alignment : null, false);
        if (charging)
        {
            int oldLevel = CrystalLevel;
            byte oldLight = LightNow()[2];
            float charge = CrystalCharging.Gain(Api.World, Evolution, Charge, strength, hours);
            Crystal.SetState(crystal, charge, Cracks);
            if (CrystalLevel != oldLevel)
            {
                Api.World.PlaySoundAt(ChimeSound, Pos.X + 0.5, Pos.Y + 0.75, Pos.Z + 0.5, null, false, 16f, 0.6f);
                MarkDirty(true);
                UpdateLight(oldLight);
            }
            else
            {
                MarkDirty(false);
            }
        }
    }

    private void Refine(bool passes, string leader, float strength, double hours)
    {
        if (!passes)
        {
            SetSparks(null, true);
            return;
        }
        if (refineTarget != leader)
        {
            refineTarget = leader;
            refineProgress = 0;
        }
        SetSparks(leader, true);
        var config = LemuriaModSystem.Config;
        double days = Math.Clamp(Evolution == Crystal.Glowing ? config.RefineDaysGlowing : config.RefineDaysGlittering, 0.01, 1000);
        refineProgress += strength * hours / (days * Api.World.Calendar.HoursPerDay);
        if (refineProgress < 1)
        {
            MarkDirty(false);
            return;
        }
        var block = Crystal.BlockFor(Api.World, leader, Evolution);
        if (block == null)
        {
            return;
        }
        byte oldLight = LightNow()[2];
        var refined = new ItemStack(block);
        Crystal.SetState(refined, 0, Cracks);
        crystal = refined;
        refineTarget = null;
        refineProgress = 0;
        sparkAlignment = null;
        Api.World.PlaySoundAt(ChimeSound, Pos.X + 0.5, Pos.Y + 0.75, Pos.Z + 0.5, null, false, 24f, 1f);
        MarkDirty(true);
        UpdateLight(oldLight);
    }

    /// <summary>Refines the crystal at once, as if its time had passed at full strength; for the test command.</summary>
    public void RefineFor(double hours)
    {
        ReadSurroundings(hours);
    }

    private void SetSparks(string? alignment, bool refining)
    {
        if (sparkAlignment != alignment || sparkRefining != refining)
        {
            sparkAlignment = alignment;
            sparkRefining = refining;
            MarkDirty(false);
        }
    }

    // ------------------------------------------------------------------ the resting crystal in dialogues

    public void SetWarning(bool on)
    {
        if (warning == on)
        {
            return;
        }
        warning = on;
        if (!on)
        {
            dialogueSeconds = 0;
        }
        MarkDirty(true);
    }

    public void Neutralize()
    {
        if (crystal == null)
        {
            return;
        }
        var block = Crystal.BlockFor(Api.World, Crystal.Unaligned, Evolution);
        if (block == null)
        {
            return;
        }
        byte oldLight = LightNow()[2];
        var stack = new ItemStack(block);
        Crystal.SetState(stack, 0, Cracks);
        crystal = stack;
        refineTarget = null;
        refineProgress = 0;
        warning = false;
        MarkDirty(true);
        UpdateLight(oldLight);
    }

    public void Discharge()
    {
        if (crystal == null)
        {
            return;
        }
        byte oldLight = LightNow()[2];
        Crystal.SetState(crystal, 0, Cracks);
        MarkDirty(true);
        UpdateLight(oldLight);
    }

    public void Crack()
    {
        if (crystal == null)
        {
            return;
        }
        int cracks = Cracks + 1;
        if (cracks >= Math.Max(1, LemuriaModSystem.Config.CracksToShatter))
        {
            var center = Pos.ToVec3d().Add(0.5, 0.6, 0.5);
            string alignment = Alignment ?? Crystal.Unaligned;
            Api.World.PlaySoundAt(new AssetLocation(LemuriaModSystem.ModId, "sounds/crystals/shatter"), center.X, center.Y, center.Z, null, true, 24f, 1f);
            CrystalsModSystem.Server?.SendBurst(center, alignment);
            SetCrystal(null);
            return;
        }
        if (Cracks == 0)
        {
            nextMendTotalHours = Api.World.Calendar.TotalHours + StageHours();
        }
        Crystal.SetState(crystal, Charge, cracks);
        MarkDirty(true);
    }

    public void OnLightning()
    {
        if (crystal == null)
        {
            return;
        }
        if (Alignment == Crystal.Sky || (!CrystalAligned && refineTarget == Crystal.Sky))
        {
            skyBoostUntil = Api.World.Calendar.TotalHours + CrystalCharging.LightningHours;
            ReadSurroundings();
        }
    }

    /// <summary>Takes up to this much charge out of the resting crystal and returns what it gave. Server only.</summary>
    public float TakeCharge(float max)
    {
        if (!CrystalAligned)
        {
            return 0;
        }
        float taken = Math.Min(max, Charge);
        if (taken <= 0)
        {
            return 0;
        }
        int oldLevel = CrystalLevel;
        byte oldLight = LightNow()[2];
        Crystal.SetState(crystal!, Charge - taken, Cracks);
        if (CrystalLevel != oldLevel)
        {
            Api.World.PlaySoundAt(ChimeSound, Pos.X + 0.5, Pos.Y + 0.75, Pos.Z + 0.5, null, false, 16f, 0.6f);
        }
        MarkDirty(true);
        UpdateLight(oldLight);
        return taken;
    }

    // ------------------------------------------------------------------ light, sound and models

    private void SetStage(int newStage)
    {
        byte oldLight = LightNow()[2];
        stage = newStage;
        MarkDirty(true);
        UpdateLight(oldLight);
    }

    /// <summary>
    /// Relights the block when the light level changed, as the game's lantern does: the light that went
    /// away is removed first, and exchanging the block for itself adds the new light. It runs on the
    /// server and again on each client when the new state arrives, since each side keeps its own light.
    /// </summary>
    private void UpdateLight(byte oldLight)
    {
        var hsv = LightNow();
        placedLight = hsv[2];
        if (oldLight == hsv[2])
        {
            return;
        }
        if (oldLight > 0)
        {
            Api.World.BlockAccessor.RemoveBlockLight(new[] { hsv[0], hsv[1], oldLight }, Pos);
        }
        Api.World.BlockAccessor.ExchangeBlock(Block.Id, Pos);
    }

    private byte[] LightNow()
    {
        if (crystal?.Block != null)
        {
            var hsv = crystal.Block.LightHsv;
            byte v = CrystalAligned ? Crystal.LightLevel(Evolution, CrystalLevel) : hsv[2];
            return new[] { hsv[0], hsv[1], v };
        }
        var ripe = RipeCrystalBlock();
        // A ripe Glowing crystal lights at its own 7, a Glittering one at the stage's 5.
        byte level = stage == Ripe ? Math.Max(Light[stage], ripe?.LightHsv[2] ?? (byte)0) : Light[stage];
        return new[] { ripe?.LightHsv[0] ?? (byte)0, ripe?.LightHsv[1] ?? (byte)0, level };
    }

    /// <summary>
    /// The crystal a ripe seedling becomes: seedling-{evolution} grows into crystal-unaligned-{evolution},
    /// one Evolution higher when an evolution ring stood at all five stages.
    /// </summary>
    private Block? RipeCrystalBlock()
    {
        string evolution = SeedlingEvolution;
        if (evolution == Crystal.Glittering && ringStages >= Ripe - FirstStage)
        {
            evolution = Crystal.Glowing;
        }
        return Api?.World.GetBlock(new AssetLocation(LemuriaModSystem.ModId, $"crystal-unaligned-{evolution}-up"));
    }

    /// <summary>What the cradle holds: its crystal, its seedling while it grows, its crystal when ripe, otherwise nothing.</summary>
    public ItemStack? ContentStack()
    {
        if (crystal != null)
        {
            return crystal.Clone();
        }
        if (stage == Ripe)
        {
            var ripe = RipeCrystalBlock();
            return ripe == null ? null : new ItemStack(ripe);
        }
        if (stage >= FirstStage && seedlingCode != null)
        {
            var seedling = Api.World.GetItem(new AssetLocation(seedlingCode));
            return seedling == null ? null : new ItemStack(seedling);
        }
        return null;
    }

    public override void OnBlockBroken(IPlayer? byPlayer = null)
    {
        base.OnBlockBroken(byPlayer);
        byte v = LightNow()[2];
        if (v > 0)
        {
            Api.World.BlockAccessor.RemoveBlockLight(LightNow(), Pos);
        }
    }

    public override void OnBlockRemoved()
    {
        base.OnBlockRemoved();
        if (Api.Side == EnumAppSide.Server)
        {
            CrystalsModSystem.Standing?.Remove(this);
        }
        else
        {
            StopNote();
        }
    }

    public override void OnBlockUnloaded()
    {
        base.OnBlockUnloaded();
        if (Api?.Side == EnumAppSide.Server)
        {
            CrystalsModSystem.Standing?.Remove(this);
        }
        else if (Api != null)
        {
            StopNote();
        }
    }

    public override void ToTreeAttributes(ITreeAttribute tree)
    {
        base.ToTreeAttributes(tree);
        tree.SetInt("stage", stage);
        if (seedlingCode != null)
        {
            tree.SetString("seedling", seedlingCode);
        }
        tree.SetDouble("nextStageTotalHours", nextStageTotalHours);
        tree.SetInt("ringStages", ringStages);
        if (crystal != null)
        {
            tree.SetItemstack("crystal", crystal);
        }
        if (refineTarget != null)
        {
            tree.SetString("refineTarget", refineTarget);
        }
        tree.SetDouble("refineProgress", refineProgress);
        tree.SetDouble("nextMendTotalHours", nextMendTotalHours);
        tree.SetDouble("skyBoostUntil", skyBoostUntil);
        tree.SetFloat("dialogueSeconds", dialogueSeconds);
        tree.SetBool("warning", warning);
        if (sparkAlignment != null)
        {
            tree.SetString("sparkAlignment", sparkAlignment);
        }
        tree.SetBool("sparkRefining", sparkRefining);
        tree.SetFloat("grace", (float)LemuriaModSystem.Config.DialogueGraceSeconds);
    }

    public override void FromTreeAttributes(ITreeAttribute tree, IWorldAccessor worldForResolving)
    {
        base.FromTreeAttributes(tree, worldForResolving);
        bool oldWarning = warning;
        string? oldCrystal = crystal?.Collectible?.Code?.ToString();
        stage = Math.Clamp(tree.GetInt("stage"), Empty, Ripe);
        seedlingCode = tree.GetString("seedling");
        nextStageTotalHours = tree.GetDouble("nextStageTotalHours");
        ringStages = tree.GetInt("ringStages");
        crystal = tree.GetItemstack("crystal");
        crystal?.ResolveBlockOrItem(worldForResolving);
        refineTarget = tree.GetString("refineTarget");
        refineProgress = tree.GetDouble("refineProgress");
        nextMendTotalHours = tree.GetDouble("nextMendTotalHours");
        skyBoostUntil = tree.GetDouble("skyBoostUntil");
        dialogueSeconds = tree.GetFloat("dialogueSeconds");
        warning = tree.GetBool("warning");
        sparkAlignment = tree.GetString("sparkAlignment");
        sparkRefining = tree.GetBool("sparkRefining");
        if (Api?.Side == EnumAppSide.Client)
        {
            UpdateLight(placedLight);
            if (oldWarning != warning || oldCrystal != crystal?.Collectible?.Code?.ToString())
            {
                UpdateNote(tree.GetFloat("grace", 30));
            }
            MarkDirty(true);
        }
    }

    private string DiscordKey => $"pos:{Pos.X},{Pos.Y},{Pos.Z}";

    /// <summary>On the client: an aligned crystal's own note, or the discordant hum in its place.</summary>
    private void UpdateNote(float grace = 30)
    {
        if (Api is not ICoreClientAPI capi)
        {
            return;
        }
        var hums = CrystalsModSystem.Client?.Hums;
        if (warning)
        {
            hums?.Start(DiscordKey, Pos.ToVec3d().Add(0.5, 0.6, 0.5), 0, dialogueSeconds, grace);
        }
        else
        {
            hums?.Stop(DiscordKey);
        }
        note?.Stop();
        note?.Dispose();
        note = null;
        if (CrystalAligned && !warning)
        {
            note = capi.World.LoadSound(new SoundParams
            {
                Location = new AssetLocation(LemuriaModSystem.ModId, $"sounds/crystals/hum-{Alignment}"),
                ShouldLoop = true,
                DisposeOnFinish = false,
                Position = Pos.ToVec3f().Add(0.5f, 0.6f, 0.5f),
                Range = 16f,
                Volume = Crystal.Hum(CrystalLevel),
                SoundType = EnumSoundType.Ambient,
            });
            note?.Start();
        }
    }

    private void StopNote()
    {
        CrystalsModSystem.Client?.Hums?.Stop(DiscordKey);
        note?.Stop();
        note?.Dispose();
        note = null;
    }

    private void ClientSparks()
    {
        note?.SetVolume(Crystal.Hum(CrystalLevel));
        if (sparkAlignment != null)
        {
            CrystalCharging.Sparks(Api.World, Pos.ToVec3d().Add(0.5, Floor + 0.2, 0.5), Crystal.Rgb(sparkAlignment), sparkRefining ? 3 : 1);
        }
    }

    public override bool OnTesselation(ITerrainMeshPool mesher, ITesselatorAPI tessThreadTesselator)
    {
        var capi = (ICoreClientAPI)Api;
        if (crystal?.Block != null)
        {
            var mesh = CrystalMeshes.Get(capi, crystal.Block, CrystalAligned ? Crystal.Glow(CrystalLevel) : -1, Cracks);
            if (mesh != null)
            {
                mesher.AddMeshData(mesh.Clone().Translate(0, Floor, 0));
            }
        }
        else if (stage == Ripe && RipeCrystalBlock() is Block ripe && Crystal.EvolutionOf(ripe) == Crystal.Glowing)
        {
            var mesh = CrystalMeshes.Get(capi, ripe, -1, 0);
            if (mesh != null)
            {
                mesher.AddMeshData(mesh.Clone().Translate(0, Floor, 0));
            }
        }
        else if (stage != Empty)
        {
            var mesh = GetMesh(tessThreadTesselator);
            if (mesh != null)
            {
                mesher.AddMeshData(mesh);
            }
        }
        // false: the game draws the basin from the block's own shape as well.
        return false;
    }

    /// <summary>One cached model per rock and stage; every cradle of that kind shares it.</summary>
    private MeshData? GetMesh(ITesselatorAPI tesselator)
    {
        var capi = (ICoreClientAPI)Api;
        var meshes = ObjectCacheUtil.GetOrCreate(capi, MeshCacheKey, () => new Dictionary<string, MeshData?>());
        string key = Block.Code + "/" + stage;
        lock (meshes)
        {
            if (meshes.TryGetValue(key, out var cached))
            {
                return cached;
            }
            MeshData? mesh = null;
            var shape = Shape.TryGet(capi, new AssetLocation(LemuriaModSystem.ModId, $"shapes/block/cradle/stage{stage}.json"));
            if (shape != null)
            {
                tesselator.TesselateShape(Block, shape, out mesh);
            }
            else
            {
                capi.Logger.Warning("[{0}] The cradle shape for stage {1} is missing", LemuriaModSystem.ModId, stage);
            }
            meshes[key] = mesh;
            return mesh;
        }
    }
}
