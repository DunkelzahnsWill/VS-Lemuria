namespace Lemuria.Core;

// The cradle's settings in ModConfig/lemuria.json (design note "Lemuria step 1: Find and farm", the config file).
public partial class LemuriaConfig
{
    /// <summary>In-game days per growth stage at the default 9-day month; scaled by the world's month length like a crop.</summary>
    public double StageDaysAtDefaultMonth { get; set; } = 2;

    /// <summary>Each stage is up to this share longer or shorter, so a row of cradles does not change in lockstep.</summary>
    public double StageRandomness { get; set; } = 0.1;
}
