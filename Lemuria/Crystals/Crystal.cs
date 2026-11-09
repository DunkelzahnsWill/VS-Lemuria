using System;
using Lemuria.Core;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace Lemuria.Crystals;

/// <summary>
/// What every feature needs to know about a crystal: its alignment and Evolution (from the block
/// code <c>crystal-{alignment}-{evolution}-{position}</c>), its twin, how much charge it holds, and
/// its charge and cracks as kept on an item stack (design note "Lemuria step 2: The Four Elementals").
/// </summary>
public static class Crystal
{
    public const string Unaligned = "unaligned";
    public const string Fire = "fire";
    public const string Ice = "ice";
    public const string Earth = "earth";
    public const string Sky = "sky";
    public const string Life = "life";
    public const string Death = "death";
    public const string Time = "time";
    public const string Rust = "rust";

    public const string Glittering = "glittering";
    public const string Glowing = "glowing";

    /// <summary>The four Elementals, in the order <see cref="CrystalEnvironment"/> reads them.</summary>
    public static readonly string[] Elementals = { Fire, Ice, Earth, Sky };

    /// <summary>The four Mysteries (design note "Lemuria step 5: The Four Mysteries"), read after the Elementals.</summary>
    public static readonly string[] Mysteries = { Life, Death, Time, Rust };

    /// <summary>Every aligned alignment, Elementals then Mysteries, in the order <see cref="CrystalEnvironment"/> reads them.</summary>
    public static readonly string[] AllAligned = { Fire, Ice, Earth, Sky, Life, Death, Time, Rust };

    public const string ChargeKey = "lemuriaCharge";
    public const string CracksKey = "lemuriaCracks";

    /// <summary>Charge levels, shown by glow, light, hum and chime but never as a number.</summary>
    public const int Empty = 0, Low = 1, High = 2, Full = 3;

    public static bool IsCrystal(CollectibleObject? collectible) => collectible is BlockLemuriaCrystal;

    public static string AlignmentOf(CollectibleObject collectible) => collectible.Variant["alignment"] ?? Unaligned;

    public static string EvolutionOf(CollectibleObject collectible) => collectible.Variant["evolution"] ?? Glittering;

    public static bool IsAligned(CollectibleObject? collectible) => IsCrystal(collectible) && AlignmentOf(collectible!) != Unaligned;

    /// <summary>Fire and Ice, Earth and Sky, Life and Death, Time and Rust; Unaligned has none.</summary>
    public static string? TwinOf(string alignment) => alignment switch
    {
        Fire => Ice,
        Ice => Fire,
        Earth => Sky,
        Sky => Earth,
        Life => Death,
        Death => Life,
        Time => Rust,
        Rust => Time,
        _ => null,
    };

    /// <summary>The first of each twin pair (Fire, Earth, Life, Time), so a pair is counted once.</summary>
    public static bool IsFirstTwin(string alignment) => alignment is Fire or Earth or Life or Time;

    /// <summary>Glittering 1, Glowing 2, Luminous 3, Radiant 4, Baleful 5: the numbers a dialogue averages.</summary>
    public static int Rank(string evolution) => evolution switch
    {
        Glittering => 1,
        Glowing => 2,
        "luminous" => 3,
        "radiant" => 4,
        "baleful" => 5,
        _ => 1,
    };

    /// <summary>Charge a crystal holds when full: Glittering 100, Glowing twice as much.</summary>
    public static float Capacity(string evolution) => 100f * Math.Max(1, Rank(evolution));

    public static int Level(float charge, float capacity)
    {
        if (charge <= 0.0001f)
        {
            return Empty;
        }
        if (charge >= capacity - 0.0001f)
        {
            return Full;
        }
        return charge < capacity / 2 ? Low : High;
    }

    public static float GetCharge(ItemStack stack) => stack.Attributes.GetFloat(ChargeKey);

    public static int GetCracks(ItemStack stack) => stack.Attributes.GetInt(CracksKey);

    /// <summary>Sets charge and cracks on a stack, leaving no attribute for zero so plain crystals still stack together.</summary>
    public static void SetState(ItemStack stack, float charge, int cracks)
    {
        if (charge > 0)
        {
            stack.Attributes.SetFloat(ChargeKey, charge);
        }
        else
        {
            stack.Attributes.RemoveAttribute(ChargeKey);
        }
        if (cracks > 0)
        {
            stack.Attributes.SetInt(CracksKey, cracks);
        }
        else
        {
            stack.Attributes.RemoveAttribute(CracksKey);
        }
    }

    /// <summary>Whether a player is sneaking, which sets crystals down and strikes chisels.</summary>
    public static bool Sneaking(EntityAgent entity) => entity.Controls.Sneak || entity.Controls.ShiftKey;

    public static Block? BlockFor(IWorldAccessor world, string alignment, string evolution, string position = "up") =>
        world.GetBlock(new AssetLocation(LemuriaModSystem.ModId, $"crystal-{alignment}-{evolution}-{position}"));

    /// <summary>
    /// A crystal's colour, for sparks and bursts: Fire ruby red, Ice sapphire blue, Earth dark brown, Sky
    /// steely grey, Life white-yellow, Death black-purple, Time light cyan, Rust dark rust, Unaligned ivory.
    /// </summary>
    public static int Rgb(string alignment) => alignment switch
    {
        Fire => ColorUtil.ToRgba(255, 225, 30, 70),
        Ice => ColorUtil.ToRgba(255, 60, 110, 240),
        Earth => ColorUtil.ToRgba(255, 150, 100, 60),
        Sky => ColorUtil.ToRgba(255, 135, 146, 165),
        Life => ColorUtil.ToRgba(255, 250, 240, 170),
        Death => ColorUtil.ToRgba(255, 96, 44, 120),
        Time => ColorUtil.ToRgba(255, 120, 225, 235),
        Rust => ColorUtil.ToRgba(255, 160, 72, 36),
        _ => ColorUtil.ToRgba(255, 240, 228, 196),
    };

    /// <summary>
    /// Light by charge level, the same for every alignment and Evolution: 1 empty, 2 low, 3 high, 4 full,
    /// Unaligned counting as full (step 5 note, K2: a faint light whose glow still shows; a torch is 14).
    /// The Evolution is kept in the call for the callers, which still pass it.
    /// </summary>
    public static byte LightLevel(string evolution, int level) => (byte)(Math.Clamp(level, Empty, Full) + 1);

    /// <summary>Glow on the crystal's faces by charge level, 0 to 255.</summary>
    public static int Glow(int level) => new[] { 60, 120, 190, 255 }[level];

    /// <summary>Hum strength by charge level, 0 to 1.</summary>
    public static float Hum(int level) => new[] { 0.1f, 0.2f, 0.35f, 0.5f }[level];

    /// <summary>The block a crystal attached to that face of it stands on: below for "up", behind for a wall.</summary>
    public static BlockPos SupportPos(BlockPos pos, string position)
    {
        var facing = BlockFacing.FromCode(position) ?? BlockFacing.UP;
        return pos.AddCopy(facing.Opposite);
    }
}
