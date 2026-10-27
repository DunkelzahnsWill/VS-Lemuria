namespace Lemuria.Core;

// Ear muffs' settings in ModConfig/lemuria.json (step 3 note, After the play-test, Ear muffs).
public partial class LemuriaConfig
{
    /// <summary>Share of every sound that gets through ear muffs: 0 silences the world, 1 makes them do nothing.</summary>
    public double EarMuffsVolume { get; set; } = 0.75;

    /// <summary>Every player's volume for all of Lemuria's own sounds, ear muffs or not.</summary>
    public double SoundVolume { get; set; } = 1;
}
