using System;
using Lemuria.Core;
using Lemuria.Crystals;
using Vintagestory.API.Common;

namespace Lemuria.Evocations;

/// <summary>
/// The numbers every cast follows (design note "Lemuria step 3: Naked evocations", Rules): which
/// evocation an alignment casts, what a cast costs, and how far and how wide it reaches at its power.
/// Power is the Evolution a cast acts at: 1 for Glittering, 2 for Glowing, and one more when
/// over-driven, so an over-driven Glowing crystal casts at 3, as a Luminous one will in step 6.
/// </summary>
public static class Evocation
{
    public const string Ignite = "ignite";
    public const string Quench = "quench";
    public const string Tremor = "tremor";
    public const string Gale = "gale";

    /// <summary>The naked evocation of an Elemental; null for Unaligned, which commands through the chisel instead.</summary>
    public static string? Of(string alignment) => alignment switch
    {
        Crystal.Fire => Ignite,
        Crystal.Ice => Quench,
        Crystal.Earth => Tremor,
        Crystal.Sky => Gale,
        _ => null,
    };

    /// <summary>Whether a held collectible casts: a Fire, Ice, Earth or Sky crystal.</summary>
    public static bool Casts(CollectibleObject? collectible) =>
        Crystal.IsAligned(collectible) && Of(Crystal.AlignmentOf(collectible!)) != null;

    private static LemuriaConfig Config => LemuriaModSystem.Config;

    /// <summary>Seconds of holding before a cast can fire. The client, which never reads the config, uses the default.</summary>
    public static double WindUpSeconds => Math.Clamp(Config.CastWindUpSeconds, 0, 10);

    /// <summary>Seconds of holding before the crystal over-drives, never sooner than the wind-up.</summary>
    public static double OverdriveSeconds => Math.Max(WindUpSeconds, Math.Clamp(Config.OverdriveSeconds, 0, 30));

    /// <summary>The charge a cast spends: a tenth of a full crystal, twice that over-driven.</summary>
    public static float Cost(string evolution, bool overdrive)
    {
        double cost = Crystal.Capacity(evolution) * Math.Clamp(Config.CastCostShare, 0.001, 1);
        return (float)(overdrive ? cost * Math.Clamp(Config.OverdriveCostMultiplier, 1, 100) : cost);
    }

    public static double CrackChance => Math.Clamp(Config.OverdriveCrackChance, 0, 1);

    public static int Power(string evolution, bool overdrive) => Crystal.Rank(evolution) + (overdrive ? 1 : 0);

    /// <summary>Range in blocks: 8, 12 and 16 by default.</summary>
    public static double Range(int power) =>
        Math.Clamp(Config.EvocationRangeGlittering, 1, 64) + Math.Clamp(Config.EvocationRangeStep, 0, 32) * (power - 1);

    /// <summary>
    /// The area's size along each axis: the target alone at Glittering, 2×2×2 at Glowing, 3×3×3 for an
    /// over-driven Glowing crystal, casting as Luminous (the owner, 2026-10-27), one more per Evolution after.
    /// </summary>
    public static int AreaSize(int power) => Math.Clamp(power, 1, 5);

    /// <summary>
    /// How far a Tremor's sides sink: half its rise, which is TremorRise at Glittering and as much again
    /// per Evolution above (2, 4, 6 by default), kept even so the sides pay for the rise exactly.
    /// </summary>
    public static int TremorSink(int power) => Math.Max(1, TremorRise * Math.Clamp(power, 1, 5) / 2);

    /// <summary>Blocks Gale pushes: 6, 8 and 10 by default.</summary>
    public static double GalePush(int power) => Math.Clamp(Config.GalePushGlittering, 0, 32) + 2 * (power - 1);

    public static int TremorRise => Math.Clamp(Config.TremorRise, 1, 8);
}
