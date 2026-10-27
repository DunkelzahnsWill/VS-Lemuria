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

    public const string Glittering = "glittering";
    public const string Glowing = "glowing";

    /// <summary>The four Elementals, in the order <see cref="CrystalEnvironment"/> reads them.</summary>
    public static readonly string[] Elementals = { Fire, Ice, Earth, Sky };

    public const string ChargeKey = "lemuriaCharge";
    public const string CracksKey = "lemuriaCracks";

    /// <summary>Charge levels, shown by glow, light, hum and chime but never as a number.</summary>
    public const int Empty = 0, Low = 1, High = 2, Full = 3;

    public static bool IsCrystal(CollectibleObject? collectible) => collectible is BlockLemuriaCrystal;

    public static string AlignmentOf(CollectibleObject collectible) => collectible.Variant["alignment"] ?? Unaligned;

    public static string EvolutionOf(CollectibleObject collectible) => collectible.Variant["evolution"] ?? Glittering;

    public static bool IsAligned(CollectibleObject? collectible) => IsCrystal(collectible) && AlignmentOf(collectible!) != Unaligned;

    /// <summary>Fire and Ice, Earth and Sky; Unaligned has none.</summary>
    public static string? TwinOf(string alignment) => alignment switch
    {
        Fire => Ice,
        Ice => Fire,
        Earth => Sky,
        Sky => Earth,
        _ => null,
    };

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

    public static Block? BlockFor(IWorldAccessor world, string alignment, string evolution, string position = "up") =>
        world.GetBlock(new AssetLocation(LemuriaModSystem.ModId, $"crystal-{alignment}-{evolution}-{position}"));

    /// <summary>A crystal's colour, for sparks and bursts: Fire ruby red, Ice sapphire blue, Earth dark brown, Sky steely grey, Unaligned ivory.</summary>
    public static int Rgb(string alignment) => alignment switch
    {
        Fire => ColorUtil.ToRgba(255, 225, 30, 70),
        Ice => ColorUtil.ToRgba(255, 60, 110, 240),
        Earth => ColorUtil.ToRgba(255, 150, 100, 60),
        Sky => ColorUtil.ToRgba(255, 135, 146, 165),
        _ => ColorUtil.ToRgba(255, 240, 228, 196),
    };

    /// <summary>Light by charge level: Glittering 2, 4, 5, 7 and Glowing 3, 5, 7, 9 (a candle is 7, a torch 14).</summary>
    public static byte LightLevel(string evolution, int level) => evolution == Glowing
        ? new byte[] { 3, 5, 7, 9 }[level]
        : new byte[] { 2, 4, 5, 7 }[level];

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
