namespace Lemuria.Core;

// The Mysteries' settings in ModConfig/lemuria.json (design note "Lemuria step 5: The Four Mysteries").
public partial class LemuriaConfig
{
    /// <summary>In-game hours Rewind turns back and Progress moves on at Glittering; Glowing twice, Luminous power four times (12, 24, 48).</summary>
    public double MysteryHoursGlittering { get; set; } = 12;

    /// <summary>Damage one tick of Wither deals to a creature or player (K5: 1.5).</summary>
    public double WitherDamage { get; set; } = 1.5;

    /// <summary>Seconds between Wither's ticks (answer 8: every 5 seconds).</summary>
    public double WitherTickSeconds { get; set; } = 5;

    /// <summary>Seconds a withering lasts; another cast restarts it (answer 8: 30 seconds, six ticks).</summary>
    public double WitherSeconds { get; set; } = 30;

    /// <summary>Charge a Glint burns an in-game hour with a Glittering crystal; a Glowing one burns twice as much (a third and two thirds).</summary>
    public double GlintBurnPerHour { get; set; } = 1.0 / 3.0;
}
