namespace Lemuria.Core;

// Evocations' settings in ModConfig/lemuria.json (design note "Lemuria step 3: Naked evocations",
// the config file).
public partial class LemuriaConfig
{
    /// <summary>How long you hold right-click before a cast can fire, in seconds.</summary>
    public double CastWindUpSeconds { get; set; } = 5;

    /// <summary>How long you hold before the crystal over-drives, in seconds.</summary>
    public double OverdriveSeconds { get; set; } = 15;

    /// <summary>Share of a full crystal one cast spends.</summary>
    public double CastCostShare { get; set; } = 0.1;

    /// <summary>How many casts' worth an over-driven cast spends.</summary>
    public double OverdriveCostMultiplier { get; set; } = 2;

    /// <summary>Chance that an over-driven cast cracks the crystal, 1 in 3.</summary>
    public double OverdriveCrackChance { get; set; } = 0.33;

    /// <summary>Range in blocks at Glittering.</summary>
    public double EvocationRangeGlittering { get; set; } = 8;

    /// <summary>Blocks of range each Evolution above Glittering adds.</summary>
    public double EvocationRangeStep { get; set; } = 4;

    /// <summary>Blocks the ground rises at Glittering; each Evolution above adds as much again (2, 4, 6), and its sides sink half as much.</summary>
    public int TremorRise { get; set; } = 2;

    /// <summary>Blocks Gale pushes at Glittering; each Evolution above adds 2.</summary>
    public double GalePushGlittering { get; set; } = 6;

    /// <summary>Share of a hand pan's chance each of Gale's pannings finds something: 0.5 halves the finds.</summary>
    public double GalePanningYield { get; set; } = 0.5;
}
