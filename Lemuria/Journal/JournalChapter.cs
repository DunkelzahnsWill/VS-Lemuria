using Newtonsoft.Json;
using ProtoBuf;
using System.Collections.Generic;

namespace Lemuria.Journal;

/// <summary>
/// One chapter of the Pilgrim's Journal, from assets/lemuria/config/journal/chapters.json. Its text
/// lives in lang files: <c>journal-{code}-title</c> and <c>journal-{code}-page1</c> to <c>-page{Pages}</c>.
/// </summary>
public class JournalChapter
{
    [JsonProperty("code")]
    public string Code { get; set; } = "";

    [JsonProperty("pages")]
    public int Pages { get; set; }

    /// <summary>Open from the start; other chapters unlock per player as they progress.</summary>
    [JsonProperty("unlockedByDefault")]
    public bool UnlockedByDefault { get; set; }
}

/// <summary>The chapters one player has unlocked, sent by the server to that player only.</summary>
[ProtoContract]
public class JournalUnlocksPacket
{
    [ProtoMember(1)]
    public List<string> Chapters { get; set; } = new();
}
