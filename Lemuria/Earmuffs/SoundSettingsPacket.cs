using ProtoBuf;

namespace Lemuria.Earmuffs;

/// <summary>The server's two sound settings, sent to each player as they join; clients never read the config.</summary>
[ProtoContract]
public class SoundSettingsPacket
{
    [ProtoMember(1)]
    public float EarMuffsVolume = 0.75f;

    [ProtoMember(2)]
    public float SoundVolume = 1f;
}
