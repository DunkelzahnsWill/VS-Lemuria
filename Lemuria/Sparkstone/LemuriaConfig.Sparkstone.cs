namespace Lemuria.Core;

// Sparkstone's settings in ModConfig/lemuria.json (design note "Lemuria step 2: The Four Elementals",
// the config file). Charge is in the units where a Glittering crystal holds 100.
public partial class LemuriaConfig
{
    /// <summary>Charge the Crystalline Chisel holds.</summary>
    public double ChiselCapacity { get; set; } = 100;

    /// <summary>Charge one awakening costs.</summary>
    public double AwakenCost { get; set; } = 25;

    /// <summary>Charge each Awakened block adds to its network.</summary>
    public double SparkstoneCapacity { get; set; } = 50;

    /// <summary>Sockets in an Awakened block, Free or carved (step 4).</summary>
    public int RoomAwakened { get; set; } = 4;

    /// <summary>Sockets a Glittering crystal fills.</summary>
    public int SocketsGlittering { get; set; } = 1;

    /// <summary>Sockets a Glowing crystal fills.</summary>
    public int SocketsGlowing { get; set; } = 2;
}
