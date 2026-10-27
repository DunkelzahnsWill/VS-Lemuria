namespace Lemuria.Core;

// Sparkstone's settings in ModConfig/lemuria.json (design note "Lemuria step 2: The Four Elementals",
// the config file). Charge is in the units where a Glittering crystal holds 100.
public partial class LemuriaConfig
{
    /// <summary>Charge the shaping tool holds.</summary>
    public double ShapingToolCapacity { get; set; } = 100;

    /// <summary>Charge one awakening costs.</summary>
    public double AwakenCost { get; set; } = 25;

    /// <summary>Charge each Awakened block adds to its network.</summary>
    public double SparkstoneCapacity { get; set; } = 50;
}
