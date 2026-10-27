namespace Lemuria.Core;

// Crystals' settings in ModConfig/lemuria.json (design note "Lemuria step 1: Find and farm", the config file).
public partial class LemuriaConfig
{
    /// <summary>Chance that breaking one small geode crystal (crystal-small) also gives a strange glowing crystal.</summary>
    public double FindChanceSmall { get; set; } = 0.04;

    /// <summary>Chance that breaking one large geode crystal or cluster (crystal-largecluster) also gives one.</summary>
    public double FindChanceLarge { get; set; } = 0.08;

    /// <summary>Seedlings from one strange crystal or Unaligned crystal ground in the quern.</summary>
    public int SeedlingsPerCrystal { get; set; } = 3;
}
