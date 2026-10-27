using System;
using Lemuria.Core;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;
using Vintagestory.API.Util;

namespace Lemuria.Cradle;

/// <summary>
/// A cradle's seedling and growth (design note "Lemuria step 1: Find and farm", The cradle and growth).
/// The server owns the state and the clock; clients get the stage through the game's block entity
/// sync and draw one cached model per stage. A stage ends after StageDaysAtDefaultMonth in-game days,
/// scaled like a crop by the world's month length and crop growth speed, with up to StageRandomness
/// either way. The clock is checked every 10 seconds, and an unloaded cradle catches up when loaded.
/// </summary>
public class BlockEntityCradle : BlockEntity
{
    public const string ClassCode = "lemuria-cradle";

    public const int Empty = 0;
    public const int FirstStage = 1;
    public const int Ripe = 6;

    /// <summary>Hum strength (0 to 1) by stage, Empty to Ripe.</summary>
    private static readonly float[] Hum = { 0f, 0f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f };

    /// <summary>Light level by stage, Empty to Ripe; a candle is 7, a torch 14.</summary>
    private static readonly byte[] Light = { 0, 0, 0, 0, 0, 2, 5 };

    private const string MeshCacheKey = LemuriaModSystem.ModId + "-cradlemeshes";
    private const int TickMs = 10_000;

    private int stage;
    private string? seedlingCode;
    private double nextStageTotalHours;

    public int Stage => stage;
    public bool IsEmpty => stage == Empty;
    public float HumStrength => Hum[stage];
    public byte[] LightHsv => LightFor(stage);

    /// <summary>Whether an item is a Lemuria seedling, the only thing that goes into a cradle.</summary>
    public static bool IsSeedling(CollectibleObject? collectible) =>
        collectible?.Code?.Domain == LemuriaModSystem.ModId && collectible.Code.Path.StartsWith("seedling-", StringComparison.Ordinal);

    public override void Initialize(ICoreAPI api)
    {
        base.Initialize(api);
        if (api.Side == EnumAppSide.Server)
        {
            RegisterGameTickListener(_ => Advance(), TickMs, api.World.Rand.Next(TickMs));
        }
    }

    /// <summary>Right-click: plant a seedling in an empty cradle, or harvest a ripe one with an empty hand.</summary>
    public bool OnInteract(IPlayer byPlayer)
    {
        var slot = byPlayer.InventoryManager.ActiveHotbarSlot;
        if (stage == Empty && IsSeedling(slot?.Itemstack?.Collectible))
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
        if (stage == Ripe && slot?.Empty != false)
        {
            if (Api.Side == EnumAppSide.Server)
            {
                var crystal = ContentStack();
                SetStage(Empty);
                seedlingCode = null;
                if (crystal != null && !byPlayer.InventoryManager.TryGiveItemstack(crystal, true))
                {
                    Api.World.SpawnItemEntity(crystal, Pos.ToVec3d().Add(0.5, 0.75, 0.5));
                }
                Api.World.PlaySoundAt(new AssetLocation("game:sounds/player/collect"), Pos, -0.25, null);
            }
            return true;
        }
        return false;
    }

    /// <summary>Sets a seedling growing at its first stage. Server only.</summary>
    public void Plant(string seedling)
    {
        seedlingCode = seedling;
        nextStageTotalHours = Api.World.Calendar.TotalHours + StageHours();
        SetStage(FirstStage);
    }

    /// <summary>
    /// Moves the stage on for every stage whose time has come, so a cradle that was unloaded catches up
    /// at once. Server only; called every 10 seconds and by the test command.
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
            Api.World.PlaySoundAt(new AssetLocation(LemuriaModSystem.ModId, "sounds/crystals/chime"), Pos.X + 0.5, Pos.Y + 0.75, Pos.Z + 0.5, null, false, 24f, 1f);
        }
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

    private void SetStage(int newStage)
    {
        int oldStage = stage;
        stage = newStage;
        MarkDirty(true);
        UpdateLight(oldStage);
    }

    /// <summary>
    /// Relights the block when the light level changed, as the game's lantern does: the light that went
    /// away is removed first, and exchanging the block for itself adds the new light. It runs on the
    /// server and again on each client when the new stage arrives, since each side keeps its own light.
    /// </summary>
    private void UpdateLight(int oldStage)
    {
        if (Light[oldStage] == Light[stage])
        {
            return;
        }
        if (Light[oldStage] > 0)
        {
            Api.World.BlockAccessor.RemoveBlockLight(LightFor(oldStage), Pos);
        }
        Api.World.BlockAccessor.ExchangeBlock(Block.Id, Pos);
    }

    private byte[] LightFor(int forStage)
    {
        var crystal = RipeCrystalBlock();
        byte hue = crystal?.LightHsv[0] ?? 0;
        byte saturation = crystal?.LightHsv[1] ?? 0;
        return new[] { hue, saturation, Light[forStage] };
    }

    /// <summary>The crystal a ripe seedling becomes: seedling-{evolution} grows into crystal-unaligned-{evolution}.</summary>
    private Block? RipeCrystalBlock()
    {
        string evolution = seedlingCode == null ? "glittering" : new AssetLocation(seedlingCode).Path["seedling-".Length..];
        return Api?.World.GetBlock(new AssetLocation(LemuriaModSystem.ModId, $"crystal-unaligned-{evolution}-up"));
    }

    /// <summary>What the cradle holds: its seedling while it grows, its crystal when ripe, otherwise nothing.</summary>
    public ItemStack? ContentStack()
    {
        if (stage == Ripe)
        {
            var crystal = RipeCrystalBlock();
            return crystal == null ? null : new ItemStack(crystal);
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
        if (Light[stage] > 0)
        {
            Api.World.BlockAccessor.RemoveBlockLight(LightFor(stage), Pos);
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
    }

    public override void FromTreeAttributes(ITreeAttribute tree, IWorldAccessor worldForResolving)
    {
        base.FromTreeAttributes(tree, worldForResolving);
        int oldStage = stage;
        stage = Math.Clamp(tree.GetInt("stage"), Empty, Ripe);
        seedlingCode = tree.GetString("seedling");
        nextStageTotalHours = tree.GetDouble("nextStageTotalHours");
        if (Api?.Side == EnumAppSide.Client && oldStage != stage)
        {
            UpdateLight(oldStage);
            MarkDirty(true);
        }
    }

    public override bool OnTesselation(ITerrainMeshPool mesher, ITesselatorAPI tessThreadTesselator)
    {
        if (stage != Empty)
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
        var meshes = ObjectCacheUtil.GetOrCreate(capi, MeshCacheKey, () => new System.Collections.Generic.Dictionary<string, MeshData?>());
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
