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

    // The Mysteries' evocations (design note "Lemuria step 5: The Four Mysteries"); their effects live in
    // the Mysteries feature, which registers them in Effects.More.
    public const string Bloom = "bloom";
    public const string Wither = "wither";
    public const string Rewind = "rewind";
    public const string Progress = "progress";

    /// <summary>The naked evocation of an aligned crystal; null for Unaligned, which commands through the chisel instead.</summary>
    public static string? Of(string alignment) => alignment switch
    {
        Crystal.Fire => Ignite,
        Crystal.Ice => Quench,
        Crystal.Earth => Tremor,
        Crystal.Sky => Gale,
        Crystal.Life => Bloom,
        Crystal.Death => Wither,
        Crystal.Time => Rewind,
        Crystal.Rust => Progress,
        _ => null,
    };

    /// <summary>Whether a held collectible casts: any aligned crystal, Elemental or Mystery.</summary>
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
    /// The area's width: the target alone at Glittering, 3 blocks across at Glowing and for an over-driven
    /// Glowing crystal, casting as Luminous, 5 beyond (the owner, 2026-10-27).
    /// </summary>
    public static int AreaSize(int power) => power <= 1 ? 1 : power <= 3 ? 3 : 5;

    /// <summary>Whether the area is one block high, a 3×3×1 carpet at Glowing; over-driven, it is a 3×3×3 cube.</summary>
    public static bool AreaFlat(int power) => power == 2;

    /// <summary>
    /// How far a Tremor's sides sink: half its rise, which is TremorRise at Glittering and as much again
    /// per Evolution above (2, 4, 6 by default), kept even so the sides pay for the rise exactly.
    /// </summary>
    public static int TremorSink(int power) => Math.Max(1, TremorRise * Math.Clamp(power, 1, 5) / 2);

    /// <summary>Blocks Gale pushes: 6, 8 and 10 by default.</summary>
    public static double GalePush(int power) => Math.Clamp(Config.GalePushGlittering, 0, 32) + 2 * (power - 1);

    /// <summary>Share of a hand pan's chance Gale's pannings get (the owner, 2026-10-27: less efficient than panning by hand).</summary>
    public static double GalePanningYield => Math.Clamp(Config.GalePanningYield, 0, 1);

    public static int TremorRise => Math.Clamp(Config.TremorRise, 1, 8);
}
