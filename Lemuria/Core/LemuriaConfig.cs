namespace Lemuria.Core;

/// <summary>
/// The server's settings, ModConfig/lemuria.json. Each feature adds its own settings in its own folder
/// as another part of this class (<c>Lemuria/&lt;Feature&gt;/LemuriaConfig.&lt;Feature&gt;.cs</c>), so the file
/// stays one flat list for the owner, and each feature keeps its values in range where it reads them.
/// <see cref="LemuriaModSystem"/> loads it once at start, on the server only; clients never read it.
/// </summary>
public partial class LemuriaConfig
{
    public const string FileName = "lemuria.json";

    /// <summary>Whether every Lemuria feature respects land claims; admins can also flip it with /lemuria claims on|off.</summary>
    public bool RespectClaims { get; set; } = true;
}
