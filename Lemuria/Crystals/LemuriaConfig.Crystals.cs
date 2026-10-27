namespace Lemuria.Core;

// Crystals' settings in ModConfig/lemuria.json (design notes "Lemuria step 1: Find and farm" and
// "Lemuria step 2: The Four Elementals", the config file).
public partial class LemuriaConfig
{
    /// <summary>Chance that breaking one small geode crystal (crystal-small) also gives a strange glowing crystal.</summary>
    public double FindChanceSmall { get; set; } = 0.04;

    /// <summary>Chance that breaking one large geode crystal or cluster (crystal-largecluster) also gives one.</summary>
    public double FindChanceLarge { get; set; } = 0.08;

    /// <summary>Seedlings from one strange crystal or Unaligned crystal ground in the quern.</summary>
    public int SeedlingsPerCrystal { get; set; } = 3;

    /// <summary>In-game days to fill a Glittering crystal at full strength.</summary>
    public double FillDaysGlittering { get; set; } = 1;

    /// <summary>The same for a Glowing crystal, which holds twice as much.</summary>
    public double FillDaysGlowing { get; set; } = 2;

    /// <summary>How long twins hum out of tune before the dialogue acts, in seconds.</summary>
    public double DialogueGraceSeconds { get; set; } = 30;

    /// <summary>Chance that a Glowing discharge cracks one crystal of the pair, 1 in 3.</summary>
    public double DischargeCrackChance { get; set; } = 0.33;

    /// <summary>The crack that shatters a crystal.</summary>
    public int CracksToShatter { get; set; } = 3;
}
