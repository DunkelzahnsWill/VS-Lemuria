using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;

namespace Lemuria.Mysteries;

/// <summary>
/// When each plant last took Bloom and each container last took a machine's Progress (design note,
/// questions 7 and 12, K4): a plant takes Bloom once an in-game day whatever blooms it, and a Rust Focus
/// works a container only once a full measure of hours has passed. Kept in the savegame, so a restart
/// does not hand out a second Bloom; entries older than two days are dropped as it saves. Server only.
/// </summary>
public class Ledger
{
    private const string SaveKey = Core.LemuriaModSystem.ModId + "-mysteriesledger";

    private readonly Dictionary<string, double> hours = new();

    public static string BloomKey(BlockPos pos) => $"bloom:{pos.X},{pos.Y},{pos.Z},{pos.dimension}";

    public static string ProgressKey(BlockPos pos) => $"progress:{pos.X},{pos.Y},{pos.Z},{pos.dimension}";

    public static string ProgressKey(long entityId) => $"progress:e{entityId}";

    public double? Last(string key) => hours.TryGetValue(key, out var h) ? h : null;

    public void Mark(string key, double totalHours) => hours[key] = totalHours;

    public void Forget(string key) => hours.Remove(key);

    public void Load(ICoreServerAPI sapi)
    {
        hours.Clear();
        var data = sapi.WorldManager.SaveGame.GetData(SaveKey);
        if (data == null)
        {
            return;
        }
        foreach (string line in Encoding.UTF8.GetString(data).Split('\n', StringSplitOptions.RemoveEmptyEntries))
        {
            int tab = line.LastIndexOf('\t');
            if (tab > 0 && double.TryParse(line[(tab + 1)..], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out double h))
            {
                hours[line[..tab]] = h;
            }
        }
    }

    public void Save(ICoreServerAPI sapi)
    {
        double cutoff = sapi.World.Calendar.TotalHours - 2 * sapi.World.Calendar.HoursPerDay;
        foreach (var stale in hours.Where(e => e.Value < cutoff).Select(e => e.Key).ToList())
        {
            hours.Remove(stale);
        }
        var text = new StringBuilder();
        foreach (var (key, h) in hours)
        {
            text.Append(key).Append('\t').Append(h.ToString("R", System.Globalization.CultureInfo.InvariantCulture)).Append('\n');
        }
        sapi.WorldManager.SaveGame.StoreData(SaveKey, Encoding.UTF8.GetBytes(text.ToString()));
    }
}
