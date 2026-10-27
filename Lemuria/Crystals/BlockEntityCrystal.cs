using System;
using Lemuria.Core;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;

namespace Lemuria.Crystals;

/// <summary>
/// A placed crystal's charge, cracks and dialogue state (design note, Charge, What you see and hear,
/// Twins and dialogues, Cracks). The server owns the state; clients get the charge level, cracks and
/// warning through the game's block entity sync and draw one cached model per level and crack.
/// An aligned crystal reads its surroundings every 30 seconds and charges where its alignment leads;
/// charging pauses while the chunk is unloaded and does not catch up.
/// </summary>
public class BlockEntityCrystal : BlockEntity, IStandingCrystal
{
    public const string ClassCode = "lemuria-crystal";

    public static readonly AssetLocation ChimeSound = new(LemuriaModSystem.ModId, "sounds/crystals/chime");
    public static readonly AssetLocation WhineSound = new(LemuriaModSystem.ModId, "sounds/crystals/whine");

    private float charge;
    private int cracks;
    private float dialogueSeconds;
    private bool warning;
    private bool sparking;
    private double skyBoostUntil;
    private double lastHours;
    private int lightLevel = -1;

    private ILoadedSound? whine;
    private long sparkListener;

    public BlockPos StandPos => Pos;
    public string? Alignment => Block == null ? null : Crystal.AlignmentOf(Block);
    public string Evolution => Block == null ? Crystal.Glittering : Crystal.EvolutionOf(Block);
    public bool IsAligned => Alignment != null && Alignment != Crystal.Unaligned;
    public float Charge => charge;
    public int Cracks => cracks;
    public float Capacity => Crystal.Capacity(Evolution);
    public int Level => IsAligned ? Crystal.Level(charge, Capacity) : Crystal.Full;
    public bool Warning => warning;
    public bool TwinNear { get; set; }

    public float DialogueSeconds
    {
        get => dialogueSeconds;
        set => dialogueSeconds = value;
    }

    /// <summary>The position variant: the face of its support the crystal stands on.</summary>
    public string Position => Block?.Variant["position"] ?? "up";

    public float HumStrength => warning ? 0f : IsAligned ? Crystal.Hum(Level) : 0.5f;

    public byte[] LightHsv
    {
        get
        {
            var hsv = Block.LightHsv;
            byte v = IsAligned ? Crystal.LightLevel(Evolution, Level) : hsv[2];
            return new[] { hsv[0], hsv[1], v };
        }
    }

    public override void Initialize(ICoreAPI api)
    {
        base.Initialize(api);
        lightLevel = Level;
        if (api.Side == EnumAppSide.Server)
        {
            lastHours = api.World.Calendar.TotalHours;
            if (IsAligned)
            {
                CrystalsModSystem.Standing?.Add(this);
                RegisterGameTickListener(_ => ReadSurroundings(), CrystalCharging.ReadIntervalMs, api.World.Rand.Next(CrystalCharging.ReadIntervalMs));
            }
        }
        else
        {
            HumBudget.AddSource(this);
            UpdateClientSounds();
            sparkListener = RegisterGameTickListener(_ => ClientSparks(), 500, api.World.Rand.Next(500));
        }
    }

    /// <summary>Takes charge and cracks from the stack it was placed from.</summary>
    public override void OnBlockPlaced(ItemStack? byItemStack = null)
    {
        base.OnBlockPlaced(byItemStack);
        if (byItemStack == null || !Crystal.IsCrystal(byItemStack.Collectible))
        {
            return;
        }
        charge = Math.Clamp(Crystal.GetCharge(byItemStack), 0, Capacity);
        cracks = Math.Max(0, Crystal.GetCracks(byItemStack));
        MarkDirty(true);
        // The block type's light is the empty level's, so a charged crystal relights at once.
        if (IsAligned)
        {
            UpdateLight(Crystal.LightLevel(Evolution, Crystal.Empty));
        }
    }

    /// <summary>The crystal as an item, with its charge and cracks.</summary>
    public ItemStack? AsStack()
    {
        var block = Crystal.BlockFor(Api.World, Alignment ?? Crystal.Unaligned, Evolution);
        if (block == null)
        {
            return null;
        }
        var stack = new ItemStack(block);
        Crystal.SetState(stack, IsAligned ? charge : 0, cracks);
        return stack;
    }

    /// <summary>
    /// Every 30 seconds on the server: read the surroundings and charge for the time since the last
    /// reading, or for the hours given (the test command).
    /// </summary>
    public void ReadSurroundings(double? forHours = null)
    {
        double now = Api.World.Calendar.TotalHours;
        double hours = forHours ?? Math.Max(0, now - lastHours);
        lastHours = now;
        if (!IsAligned)
        {
            return;
        }
        float strength = 0;
        if (!TwinNear && charge < Capacity)
        {
            strength = CrystalCharging.Strength(Api.World, Pos, Alignment!, Evolution, Alignment == Crystal.Sky && now < skyBoostUntil);
        }
        SetSparking(strength > 0);
        if (strength > 0)
        {
            SetCharge(CrystalCharging.Gain(Api.World, Evolution, charge, strength, hours));
        }
    }

    /// <summary>Sets the charge and lets everyone see and hear a new level. Server only.</summary>
    public void SetCharge(float value)
    {
        int oldLevel = Level;
        charge = Math.Clamp(value, 0, Capacity);
        int level = Level;
        if (level != oldLevel)
        {
            Api.World.PlaySoundAt(ChimeSound, Pos.X + 0.5, Pos.Y + 0.5, Pos.Z + 0.5, null, 0.85f + 0.1f * level, 16f, 0.6f);
            MarkDirty(true);
            UpdateLight(Crystal.LightLevel(Evolution, oldLevel));
            if (level == Crystal.Full)
            {
                SetSparking(false);
            }
        }
        else
        {
            MarkDirty(false);
        }
    }

    /// <summary>Takes up to this much charge out of the crystal and returns what it gave. Server only.</summary>
    public float TakeCharge(float max)
    {
        float taken = Math.Min(max, charge);
        if (taken > 0)
        {
            SetCharge(charge - taken);
        }
        return taken;
    }

    private void SetSparking(bool on)
    {
        if (sparking != on)
        {
            sparking = on;
            MarkDirty(false);
        }
    }

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
        var block = Crystal.BlockFor(Api.World, Crystal.Unaligned, Evolution, Position);
        if (block == null)
        {
            return;
        }
        var stack = new ItemStack(block);
        Crystal.SetState(stack, 0, cracks);
        var pos = Pos.Copy();
        Api.World.BlockAccessor.SetBlock(block.Id, pos, stack);
    }

    public void Discharge()
    {
        SetCharge(0);
    }

    public void Crack()
    {
        cracks++;
        if (cracks >= Math.Max(1, LemuriaModSystem.Config.CracksToShatter))
        {
            Shatter();
            return;
        }
        MarkDirty(true);
    }

    /// <summary>The crystal breaks in a burst of its colour and leaves nothing.</summary>
    public void Shatter()
    {
        var center = Pos.ToVec3d().Add(0.5, 0.4, 0.5);
        Api.World.PlaySoundAt(new AssetLocation(LemuriaModSystem.ModId, "sounds/crystals/shatter"), center.X, center.Y, center.Z, null, true, 24f, 1f);
        CrystalsModSystem.Server?.SendBurst(center, Alignment ?? Crystal.Unaligned);
        Api.World.BlockAccessor.SetBlock(0, Pos);
    }

    public void OnLightning()
    {
        if (Alignment == Crystal.Sky)
        {
            skyBoostUntil = Api.World.Calendar.TotalHours + CrystalCharging.LightningHours;
            ReadSurroundings();
        }
    }

    /// <summary>
    /// Relights the block when its light level changed, as the cradle does: the light that went away
    /// is removed first, and exchanging the block for itself adds the new light. Runs on the server and
    /// again on each client when the new level arrives, since each side keeps its own light.
    /// </summary>
    private void UpdateLight(byte oldLight)
    {
        byte newLight = LightHsv[2];
        if (oldLight == newLight)
        {
            return;
        }
        if (oldLight > 0)
        {
            var hsv = LightHsv;
            Api.World.BlockAccessor.RemoveBlockLight(new[] { hsv[0], hsv[1], oldLight }, Pos);
        }
        Api.World.BlockAccessor.ExchangeBlock(Block.Id, Pos);
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
            StopClientSounds();
        }
        // The game removes the light the block type gives; a charged crystal's own light goes here.
        byte v = LightHsv[2];
        if (v != Block.LightHsv[2] && v > 0)
        {
            Api.World.BlockAccessor.RemoveBlockLight(LightHsv, Pos);
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
            StopClientSounds();
        }
    }

    public override void ToTreeAttributes(ITreeAttribute tree)
    {
        base.ToTreeAttributes(tree);
        tree.SetFloat("charge", charge);
        tree.SetInt("cracks", cracks);
        tree.SetFloat("dialogueSeconds", dialogueSeconds);
        tree.SetBool("warning", warning);
        tree.SetBool("sparking", sparking);
        tree.SetDouble("skyBoostUntil", skyBoostUntil);
        tree.SetFloat("grace", (float)LemuriaModSystem.Config.DialogueGraceSeconds);
    }

    public override void FromTreeAttributes(ITreeAttribute tree, IWorldAccessor worldForResolving)
    {
        base.FromTreeAttributes(tree, worldForResolving);
        int oldCracks = cracks;
        bool oldWarning = warning;
        charge = tree.GetFloat("charge");
        cracks = tree.GetInt("cracks");
        dialogueSeconds = tree.GetFloat("dialogueSeconds");
        warning = tree.GetBool("warning");
        sparking = tree.GetBool("sparking");
        skyBoostUntil = tree.GetDouble("skyBoostUntil");
        if (Api?.Side == EnumAppSide.Client)
        {
            float grace = tree.GetFloat("grace", 30);
            int level = Level;
            if (lightLevel >= 0 && level != lightLevel && IsAligned)
            {
                UpdateLight(Crystal.LightLevel(Evolution, lightLevel));
            }
            lightLevel = level;
            if (oldCracks != cracks || oldWarning != warning)
            {
                UpdateClientSounds(grace);
            }
            MarkDirty(true);
        }
    }

    private string DiscordKey => $"pos:{Pos.X},{Pos.Y},{Pos.Z}";

    private void UpdateClientSounds(float grace = 30)
    {
        if (Api is not ICoreClientAPI capi)
        {
            return;
        }
        var hums = CrystalsModSystem.Client?.Hums;
        if (warning)
        {
            hums?.Start(DiscordKey, Pos.ToVec3d().Add(0.5, 0.5, 0.5), 0, dialogueSeconds, grace);
        }
        else
        {
            hums?.Stop(DiscordKey);
        }
        if (cracks > 0 && !warning)
        {
            if (whine == null)
            {
                whine = capi.World.LoadSound(new SoundParams
                {
                    Location = WhineSound,
                    ShouldLoop = true,
                    DisposeOnFinish = false,
                    Position = Pos.ToVec3f().Add(0.5f, 0.5f, 0.5f),
                    Range = 12f,
                    Volume = 0.12f,
                    SoundType = EnumSoundType.Ambient,
                });
                whine?.Start();
            }
            whine?.SetPitch(cracks >= 2 ? 1.3f : 1f);
            if (whine != null)
            {
                // Only the nearest cracked crystal whines, so a shelf of them is no louder than one.
                HumBudget.SetLoop(whine, "whine", Pos.ToVec3d().Add(0.5, 0.5, 0.5), cracks >= 2 ? 0.18f : 0.12f);
            }
        }
        else
        {
            HumBudget.RemoveLoop(whine);
            whine?.Stop();
            whine?.Dispose();
            whine = null;
        }
    }

    private void StopClientSounds()
    {
        CrystalsModSystem.Client?.Hums?.Stop(DiscordKey);
        HumBudget.RemoveSource(this);
        HumBudget.RemoveLoop(whine);
        whine?.Stop();
        whine?.Dispose();
        whine = null;
    }

    private void ClientSparks()
    {
        if (sparking && Alignment != null)
        {
            CrystalCharging.Sparks(Api.World, Pos.ToVec3d().Add(0.5, 0.3, 0.5), Crystal.Rgb(Alignment), 1);
        }
    }

    public override bool OnTesselation(ITerrainMeshPool mesher, ITesselatorAPI tessThreadTesselator)
    {
        var mesh = CrystalMeshes.Get((ICoreClientAPI)Api, Block, IsAligned ? Crystal.Glow(Level) : -1, cracks);
        if (mesh == null)
        {
            return false;
        }
        mesher.AddMeshData(mesh);
        return true;
    }

    public override void GetBlockInfo(IPlayer forPlayer, System.Text.StringBuilder dsc)
    {
        // The look-at text shows only the block's name and its description: charge is read by eye and ear.
    }
}
