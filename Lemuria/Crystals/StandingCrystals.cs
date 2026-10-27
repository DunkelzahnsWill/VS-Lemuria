using System;
using System.Collections.Generic;
using Vintagestory.API.Config;
using Vintagestory.API.MathTools;

namespace Lemuria.Crystals;

/// <summary>
/// A crystal standing in the world: placed on a block, or resting in a cradle. Both dialogue with
/// twins within 3 blocks and charge from their surroundings; the cradle's refining crystal also
/// listens for lightning. Server only.
/// </summary>
public interface IStandingCrystal
{
    /// <summary>Where it stands: the crystal's own block, or its cradle.</summary>
    BlockPos StandPos { get; }

    /// <summary>The crystal's alignment and Evolution, or null when nothing stands here right now.</summary>
    string? Alignment { get; }
    string Evolution { get; }
    float Charge { get; }

    /// <summary>Seconds the crystal has hummed out of tune with a twin; 0 when it is not in a dialogue.</summary>
    float DialogueSeconds { get; set; }

    /// <summary>Set by the dialogue tick: a twin stands within 3 blocks, so the crystal does not charge.</summary>
    bool TwinNear { get; set; }

    /// <summary>Starts or stops the discordant hum in place of the crystal's note.</summary>
    void SetWarning(bool on);

    /// <summary>Turns the crystal Unaligned of its own Evolution and empties it (a level 1 dialogue).</summary>
    void Neutralize();

    /// <summary>Empties the crystal (a level 2 dialogue).</summary>
    void Discharge();

    /// <summary>Adds one crack; the crack that reaches CracksToShatter shatters the crystal.</summary>
    void Crack();

    /// <summary>A lightning strike within 16 blocks.</summary>
    void OnLightning();
}

/// <summary>
/// The standing crystals of the loaded world, kept per chunk, so finding a twin within 3 blocks or a
/// crystal near a lightning strike is a few lookups rather than a scan. Server only.
/// </summary>
public class StandingCrystals
{
    private readonly Dictionary<long, HashSet<IStandingCrystal>> byChunk = new();

    private static long Key(int cx, int cy, int cz) => ((long)cx << 40) | ((long)(cy & 0xFFFF) << 24) | (uint)(cz & 0xFFFFFF);

    private static long KeyOf(BlockPos pos) => Key(pos.X / GlobalConstants.ChunkSize, pos.InternalY / GlobalConstants.ChunkSize, pos.Z / GlobalConstants.ChunkSize);

    public void Add(IStandingCrystal crystal)
    {
        long key = KeyOf(crystal.StandPos);
        if (!byChunk.TryGetValue(key, out var set))
        {
            byChunk[key] = set = new HashSet<IStandingCrystal>();
        }
        set.Add(crystal);
    }

    public void Remove(IStandingCrystal crystal)
    {
        long key = KeyOf(crystal.StandPos);
        if (byChunk.TryGetValue(key, out var set))
        {
            set.Remove(crystal);
            if (set.Count == 0)
            {
                byChunk.Remove(key);
            }
        }
    }

    /// <summary>Every standing crystal, in no particular order.</summary>
    public IEnumerable<IStandingCrystal> All()
    {
        foreach (var set in byChunk.Values)
        {
            foreach (var crystal in set)
            {
                yield return crystal;
            }
        }
    }

    public int Count
    {
        get
        {
            int n = 0;
            foreach (var set in byChunk.Values)
            {
                n += set.Count;
            }
            return n;
        }
    }

    /// <summary>The standing crystals within this many blocks of a position in every direction, diagonals included.</summary>
    public List<IStandingCrystal> Near(BlockPos pos, int range)
    {
        var found = new List<IStandingCrystal>();
        int size = GlobalConstants.ChunkSize;
        for (int cx = (pos.X - range) / size; cx <= (pos.X + range) / size; cx++)
        {
            for (int cy = Math.Max(0, pos.InternalY - range) / size; cy <= (pos.InternalY + range) / size; cy++)
            {
                for (int cz = (pos.Z - range) / size; cz <= (pos.Z + range) / size; cz++)
                {
                    if (!byChunk.TryGetValue(Key(cx, cy, cz), out var set))
                    {
                        continue;
                    }
                    foreach (var crystal in set)
                    {
                        var p = crystal.StandPos;
                        if (Math.Abs(p.X - pos.X) <= range && Math.Abs(p.InternalY - pos.InternalY) <= range && Math.Abs(p.Z - pos.Z) <= range)
                        {
                            found.Add(crystal);
                        }
                    }
                }
            }
        }
        return found;
    }

    public void Clear() => byChunk.Clear();
}
