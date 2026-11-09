using System;
using System.Text;
using Lemuria.Core;
using Lemuria.Crystals;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;

namespace Lemuria.Mysteries;

/// <summary>
/// Glint (design note "Lemuria step 5: The Four Mysteries", Glint, question 10): glass the Crystalline
/// Chisel carved, holding a Life crystal that lights it. Light 10 with a full Glittering crystal and 14 with
/// a full Glowing one, 2 dimmer at each charge level below full, dark when empty. It burns a third of a
/// charge an in-game hour at Glittering and two thirds at Glowing (about 12 days on a full crystal) and,
/// where Life leads at the Glint itself, charges as any standing crystal does, its own light left out.
/// It is a placed crystal, so a Death crystal within 3 blocks dialogues with it. It is glass, not
/// sparkstone, and takes no charge from a network. The server owns the state; clients get the crystal,
/// level and cracks through the game's block entity sync and relight on a change.
/// </summary>
public class BlockEntityGlint : BlockEntity, IStandingCrystal
{
    public const string ClassCode = "lemuria-glint";

    /// <summary>Life's white-yellow, as the crystal's own light.</summary>
    private static readonly byte Hue = 9, Saturation = 2;

    private ItemStack? crystal;
    private double lastHours;
    private float dialogueSeconds;
    private bool warning;
    private bool sparking;
    private byte placedLight;

    public ItemStack? CrystalStack => crystal;
    public BlockPos StandPos => Pos;
    public string? Alignment => crystal == null ? null : Crystal.AlignmentOf(crystal.Collectible);
    public string Evolution => crystal == null ? Crystal.Glittering : Crystal.EvolutionOf(crystal.Collectible);
    public float Charge => crystal == null ? 0 : Crystal.GetCharge(crystal);
    public int Cracks => crystal == null ? 0 : Crystal.GetCracks(crystal);
    public float Capacity => Crystal.Capacity(Evolution);
    public bool Lit => Alignment == Crystal.Life && Level > Crystal.Empty;
    public int Level => Alignment == Crystal.Life ? Crystal.Level(Charge, Capacity) : Crystal.Empty;
    public bool TwinNear { get; set; }

    public float DialogueSeconds
    {
        get => dialogueSeconds;
        set => dialogueSeconds = value;
    }

    /// <summary>A Glint's light: 10 (Glittering) or 14 (Glowing) full, 2 less per level below, 0 empty or without a Life crystal.</summary>
    public static byte LightOf(string evolution, int level) =>
        level <= Crystal.Empty ? (byte)0 : (byte)((evolution == Crystal.Glowing ? 14 : 10) - 2 * (Crystal.Full - level));

    public byte[] LightHsv => new[] { Hue, Saturation, Alignment == Crystal.Life ? LightOf(Evolution, Level) : (byte)0 };

    /// <summary>Charge burned an in-game hour: a third at Glittering, two thirds at Glowing.</summary>
    public static float BurnPerHour(string evolution) =>
        (float)(Math.Clamp(LemuriaModSystem.Config.GlintBurnPerHour, 0, 100) * Math.Max(1, Crystal.Rank(evolution)));

    public override void Initialize(ICoreAPI api)
    {
        base.Initialize(api);
        crystal?.ResolveBlockOrItem(api.World);
        placedLight = LightHsv[2];
        if (api.Side == EnumAppSide.Server)
        {
            lastHours = api.World.Calendar.TotalHours;
            if (crystal != null)
            {
                CrystalsModSystem.Standing?.Add(this);
            }
            RegisterGameTickListener(_ => ReadSurroundings(), CrystalCharging.ReadIntervalMs, api.World.Rand.Next(CrystalCharging.ReadIntervalMs));
        }
        else
        {
            RegisterGameTickListener(_ => ClientSparks(), 500, api.World.Rand.Next(500));
        }
    }

    /// <summary>Right-click: set a Life crystal in an empty Glint, or with an empty hand take its crystal out.</summary>
    public bool OnInteract(IPlayer byPlayer)
    {
        var slot = byPlayer.InventoryManager.ActiveHotbarSlot;
        var held = slot?.Itemstack?.Collectible;
        if (crystal == null && Crystal.IsCrystal(held))
        {
            if (Crystal.AlignmentOf(held!) != Crystal.Life)
            {
                (Api as ICoreClientAPI)?.TriggerIngameError(this, "lifeonly", Lang.Get(LemuriaModSystem.ModId + ":glint-lifeonly"));
                return true;
            }
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
                Api.World.PlaySoundAt(new AssetLocation("game:sounds/block/ceramicplace"), Pos, -0.25, null, true, 16f, LemuriaSound.Volume(1f));
            }
            return true;
        }
        if (crystal != null && slot?.Empty != false)
        {
            if (Api.Side == EnumAppSide.Server)
            {
                var taken = crystal;
                SetCrystal(null);
                if (!byPlayer.InventoryManager.TryGiveItemstack(taken, true))
                {
                    Api.World.SpawnItemEntity(taken, Pos.ToVec3d().Add(0.5, 0.75, 0.5));
                }
                Api.World.PlaySoundAt(new AssetLocation("game:sounds/player/collect"), Pos, -0.25, null, true, 16f, LemuriaSound.Volume(1f));
            }
            return true;
        }
        return false;
    }

    /// <summary>Sets the crystal, or takes it out with null. Server only.</summary>
    public void SetCrystal(ItemStack? stack)
    {
        byte oldLight = LightHsv[2];
        if (crystal == null && stack != null)
        {
            CrystalsModSystem.Standing?.Add(this);
        }
        else if (crystal != null && stack == null)
        {
            CrystalsModSystem.Standing?.Remove(this);
        }
        crystal = stack;
        dialogueSeconds = 0;
        warning = false;
        sparking = false;
        lastHours = Api.World.Calendar.TotalHours;
        MarkDirty(true);
        UpdateLight(oldLight);
    }

    /// <summary>
    /// Every 30 seconds on the server: burns for the hours since the last look and charges where Life
    /// leads here, or for the hours given (the test command). Returns the strength it charged at.
    /// </summary>
    public float ReadSurroundings(double? forHours = null)
    {
        double now = Api.World.Calendar.TotalHours;
        double hours = forHours ?? Math.Max(0, now - lastHours);
        lastHours = now;
        if (Alignment != Crystal.Life)
        {
            SetSparking(false);
            return 0;
        }
        float strength = TwinNear ? 0 : CrystalCharging.Strength(Api.World, Pos, Crystal.Life, Evolution, false);
        float charge = CrystalCharging.Gain(Api.World, Evolution, Charge, strength, hours);
        charge -= BurnPerHour(Evolution) * (float)hours;
        SetSparking(strength > 0 && charge < Capacity);
        SetCharge(charge);
        return strength;
    }

    /// <summary>Sets the crystal's charge, relighting on a new level. Server only.</summary>
    public void SetCharge(float value)
    {
        if (crystal == null)
        {
            return;
        }
        int oldLevel = Level;
        byte oldLight = LightHsv[2];
        Crystal.SetState(crystal, Math.Clamp(value, 0, Capacity), Cracks);
        if (Level != oldLevel)
        {
            Api.World.PlaySoundAt(BlockEntityCrystal.ChimeSound, Pos.X + 0.5, Pos.Y + 0.5, Pos.Z + 0.5, null, 0.85f + 0.1f * Level, 16f, LemuriaSound.Volume(0.5f));
            MarkDirty(true);
            UpdateLight(oldLight);
        }
        else
        {
            MarkDirty(false);
        }
    }

    /// <summary>The admin charger's fill; false without a Life crystal.</summary>
    public bool FillCharge()
    {
        if (Alignment != Crystal.Life)
        {
            return false;
        }
        SetCharge(Capacity);
        return true;
    }

    /// <summary>The Energy Sink's emptying; false without a Life crystal.</summary>
    public bool Empty()
    {
        if (Alignment != Crystal.Life)
        {
            return false;
        }
        SetCharge(0);
        return true;
    }

    private void SetSparking(bool on)
    {
        if (sparking != on)
        {
            sparking = on;
            MarkDirty(false);
        }
    }

    // ------------------------------------------------------------------ dialogues

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
        byte oldLight = LightHsv[2];
        var stack = new ItemStack(block);
        Crystal.SetState(stack, 0, Cracks);
        crystal = stack;
        warning = false;
        MarkDirty(true);
        UpdateLight(oldLight);
    }

    public void Discharge() => SetCharge(0);

    public void Crack()
    {
        if (crystal == null)
        {
            return;
        }
        int cracks = Cracks + 1;
        if (cracks >= Math.Max(1, LemuriaModSystem.Config.CracksToShatter))
        {
            var center = Pos.ToVec3d().Add(0.5, 0.4, 0.5);
            Api.World.PlaySoundAt(new AssetLocation(LemuriaModSystem.ModId, "sounds/crystals/shatter"), center.X, center.Y, center.Z, null, true, 24f, LemuriaSound.Volume(1f));
            CrystalsModSystem.Server?.SendBurst(center, Alignment ?? Crystal.Unaligned);
            SetCrystal(null);
            return;
        }
        Crystal.SetState(crystal, Charge, cracks);
        MarkDirty(true);
    }

    public void OnLightning()
    {
    }

    // ------------------------------------------------------------------ light, drops and models

    private void UpdateLight(byte oldLight)
    {
        byte newLight = LightHsv[2];
        placedLight = newLight;
        if (oldLight == newLight)
        {
            return;
        }
        if (oldLight > 0)
        {
            Api.World.BlockAccessor.RemoveBlockLight(new[] { Hue, Saturation, oldLight }, Pos);
        }
        Api.World.BlockAccessor.ExchangeBlock(Block.Id, Pos);
    }

    public override void OnBlockBroken(IPlayer? byPlayer = null)
    {
        base.OnBlockBroken(byPlayer);
        if (crystal != null && Api.Side == EnumAppSide.Server)
        {
            Api.World.SpawnItemEntity(crystal, Pos.ToVec3d().Add(0.5, 0.5, 0.5));
            crystal = null;
        }
    }

    public override void OnBlockRemoved()
    {
        base.OnBlockRemoved();
        byte v = placedLight;
        if (v > 0)
        {
            Api.World.BlockAccessor.RemoveBlockLight(new[] { Hue, Saturation, v }, Pos);
        }
        if (Api.Side == EnumAppSide.Server)
        {
            CrystalsModSystem.Standing?.Remove(this);
        }
        else
        {
            CrystalsModSystem.Client?.Hums?.Stop(DiscordKey);
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
            CrystalsModSystem.Client?.Hums?.Stop(DiscordKey);
        }
    }

    public override void ToTreeAttributes(ITreeAttribute tree)
    {
        base.ToTreeAttributes(tree);
        if (crystal != null)
        {
            tree.SetItemstack("crystal", crystal);
        }
        tree.SetFloat("dialogueSeconds", dialogueSeconds);
        tree.SetBool("warning", warning);
        tree.SetBool("sparking", sparking);
        tree.SetFloat("grace", (float)LemuriaModSystem.Config.DialogueGraceSeconds);
    }

    public override void FromTreeAttributes(ITreeAttribute tree, IWorldAccessor worldForResolving)
    {
        base.FromTreeAttributes(tree, worldForResolving);
        bool oldWarning = warning;
        crystal = tree.GetItemstack("crystal");
        crystal?.ResolveBlockOrItem(worldForResolving);
        dialogueSeconds = tree.GetFloat("dialogueSeconds");
        warning = tree.GetBool("warning");
        sparking = tree.GetBool("sparking");
        if (Api?.Side == EnumAppSide.Client)
        {
            UpdateLight(placedLight);
            if (oldWarning != warning)
            {
                if (warning)
                {
                    CrystalsModSystem.Client?.Hums?.Start(DiscordKey, Pos.ToVec3d().Add(0.5, 0.5, 0.5), 0, dialogueSeconds, tree.GetFloat("grace", 30));
                }
                else
                {
                    CrystalsModSystem.Client?.Hums?.Stop(DiscordKey);
                }
            }
            MarkDirty(true);
        }
    }

    private string DiscordKey => $"pos:{Pos.X},{Pos.Y},{Pos.Z}";

    private void ClientSparks()
    {
        if (sparking)
        {
            CrystalCharging.Sparks(Api.World, Pos.ToVec3d().Add(0.5, 0.3, 0.5), Crystal.Rgb(Crystal.Life), 1);
        }
    }

    public override bool OnTesselation(ITerrainMeshPool mesher, ITesselatorAPI tessThreadTesselator)
    {
        if (crystal?.Block != null)
        {
            bool aligned = Crystal.AlignmentOf(crystal.Block) != Crystal.Unaligned;
            var mesh = CrystalMeshes.Get((ICoreClientAPI)Api, crystal.Block, aligned ? Crystal.Glow(Level) : -1, Cracks);
            if (mesh != null)
            {
                mesher.AddMeshData(mesh);
            }
        }
        // false: the game draws the glass from the block's own shape as well.
        return false;
    }

    public override void GetBlockInfo(IPlayer forPlayer, StringBuilder dsc)
    {
        if (crystal == null)
        {
            dsc.AppendLine(Lang.Get(LemuriaModSystem.ModId + ":glint-empty"));
        }
        else if (Alignment != Crystal.Life)
        {
            dsc.AppendLine(Lang.Get(LemuriaModSystem.ModId + ":glint-notlife"));
        }
    }
}
