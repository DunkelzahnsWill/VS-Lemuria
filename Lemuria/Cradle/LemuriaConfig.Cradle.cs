namespace Lemuria.Core;

// The cradle's settings in ModConfig/lemuria.json (design notes "Lemuria step 1: Find and farm" and
// "Lemuria step 2: The Four Elementals", the config file).
public partial class LemuriaConfig
{
    /// <summary>In-game days per growth stage at the default 9-day month; scaled by the world's month length like a crop.</summary>
    public double StageDaysAtDefaultMonth { get; set; } = 2;

    /// <summary>Each stage is up to this share longer or shorter, so a row of cradles does not change in lockstep.</summary>
    public double StageRandomness { get; set; } = 0.1;

    /// <summary>In-game days to refine a Glittering crystal at full strength; weaker places take longer in proportion.</summary>
    public double RefineDaysGlittering { get; set; } = 1;

    /// <summary>The same for a Glowing crystal.</summary>
    public double RefineDaysGlowing { get; set; } = 2;
}
