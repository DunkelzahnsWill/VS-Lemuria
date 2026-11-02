namespace Lemuria.Core;

// Machines' settings in ModConfig/lemuria.json (design note "Lemuria step 4: Machines", the config
// file). The sockets' room is Sparkstone's (LemuriaConfig.Sparkstone.cs). Charge is in the units
// where a Glittering crystal holds 100.
public partial class LemuriaConfig
{
    /// <summary>How often a Focus or Scatter works for each crystal it holds, in seconds.</summary>
    public double MachineWorkSeconds { get; set; } = 10;

    /// <summary>Share of a hand cast's charge that one working costs.</summary>
    public double MachineCostShare { get; set; } = 0.1;

    /// <summary>How many times the charge an amplified working or Motor spends.</summary>
    public double AmplifyCostMultiplier { get; set; } = 2;

    /// <summary>Width of a Scatter's square, in blocks.</summary>
    public int ScatterSize { get; set; } = 9;

    /// <summary>Charge a Glittering crystal in a Gather block gathers in an in-game day at full strength.</summary>
    public double GatherPerDayGlittering { get; set; } = 200;

    /// <summary>The same for a Glowing crystal.</summary>
    public double GatherPerDayGlowing { get; set; } = 600;

    /// <summary>Share of the new crystal's capacity that one refining costs.</summary>
    public double MachineRefineCostShare { get; set; } = 0.5;

    /// <summary>Torque a Glittering crystal gives a Crystal Motor; one length of windmill sails gives 0.25, shown as 20 kN.</summary>
    public double MotorTorqueGlittering { get; set; } = 0.25;

    /// <summary>The same for a Glowing crystal.</summary>
    public double MotorTorqueGlowing { get; set; } = 0.75;

    /// <summary>The Motor's steady speed; a windmill reaches 0.6 in a strong wind, a water wheel 0.3.</summary>
    public double MotorSpeed { get; set; } = 0.5;

    /// <summary>Charge a Glittering crystal in a Motor draws an in-game hour.</summary>
    public double MotorChargePerHourGlittering { get; set; } = 2;

    /// <summary>The same for a Glowing crystal.</summary>
    public double MotorChargePerHourGlowing { get; set; } = 6;

    /// <summary>Growth a Glittering crystal's working adds, as a share of the growth between workings: 1 doubles the pace; Glowing adds twice this, the power of Luminous four times.</summary>
    public double QuickenShare { get; set; } = 1;

    /// <summary>Chance a reclamation plate claims salt for each block of salt water a Fire Focus boils above it (step 4 part 2).</summary>
    public double SaltChance { get; set; } = 0.25;

    /// <summary>Torque a Dead Socket gives a Crystal Motor, as a Glittering crystal does (step 4 part 2).</summary>
    public double DeadSocketTorque { get; set; } = 0.25;

    /// <summary>Charge a Dead Socket draws from its line an in-game hour, twice a Glittering crystal's.</summary>
    public double DeadSocketChargePerHour { get; set; } = 4;
}
