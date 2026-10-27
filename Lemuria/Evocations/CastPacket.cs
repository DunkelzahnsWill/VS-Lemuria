using ProtoBuf;

namespace Lemuria.Evocations;

/// <summary>
/// What players near a cast see and hear, one short message per cast over <c>lemuria-evocations</c>
/// (design note, Multiplayer and cost): a streak of sparks from the caster's hand to where the cast
/// landed, a spray or a puff there, and the evocation's sound. The blocks a cast changes reach clients
/// as the game's own block updates.
/// </summary>
[ProtoContract]
public class CastPacket
{
    public const int Cast = 0, Fizzle = 1, Failed = 2;

    [ProtoMember(1)]
    public string Alignment { get; set; } = "";

    [ProtoMember(2)]
    public int Kind { get; set; }

    [ProtoMember(3)]
    public int Power { get; set; }

    [ProtoMember(4)]
    public double FromX { get; set; }

    [ProtoMember(5)]
    public double FromY { get; set; }

    [ProtoMember(6)]
    public double FromZ { get; set; }

    [ProtoMember(7)]
    public double ToX { get; set; }

    [ProtoMember(8)]
    public double ToY { get; set; }

    [ProtoMember(9)]
    public double ToZ { get; set; }
}
