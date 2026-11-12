namespace Lemuria.Core;

// The duets' settings in ModConfig/lemuria.json (feature 10's brief, Settings). Bloom's plants per
// working, which a machine's Life working reads, sits with the Machines' settings.
public partial class LemuriaConfig
{
    /// <summary>Litres of fresh water an Ice and Sky duet draws into its output at Glittering; twice at Glowing, four times amplified.</summary>
    public double DuetWaterLitres { get; set; } = 10;

    /// <summary>Items a Fire and Sky or Fire and Earth duet fires or extracts at Glittering; twice at Glowing, four times amplified.</summary>
    public int DuetItemsPerWorking { get; set; } = 4;
}
