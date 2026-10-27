using System;
using Lemuria.Core;
using System.Collections.Generic;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace Lemuria.Crystals;

/// <summary>
/// Hums never stack (step 2 play-test): any number of crystals together sound no louder than one at
/// full charge. The game already plays every block of one note within <c>ambientMaxDistanceMerge</c>
/// (32 in the JSON) as one sound at one block's strength; this shares that strength among the different
/// notes near the player, and lets only the nearest of each looping sound play. Client only.
/// </summary>
public static class HumBudget
{
    /// <summary>How far from the player a hum still counts, in blocks; the same as the JSON's merge distance.</summary>
    public const float Range = 32f;

    private static readonly HashSet<BlockEntity> sources = new();

    private class Loop
    {
        public string Group = "";
        public Vec3d Pos = null!;
        public float Volume;
    }

    private static readonly Dictionary<ILoadedSound, Loop> loops = new();

    private static volatile float share = 1f;

    /// <summary>What each note's hum is multiplied by: 1 shared among the notes the player hears, times Lemuria's sound volume.</summary>
    public static float Share => share;

    /// <summary>A block entity whose block hums through its ambient sound (crystal, cradle, sparkstone).</summary>
    public static void AddSource(BlockEntity be) => sources.Add(be);

    public static void RemoveSource(BlockEntity be) => sources.Remove(be);

    /// <summary>A looping sound of which only the nearest of its group plays, at this volume.</summary>
    public static void SetLoop(ILoadedSound sound, string group, Vec3d pos, float volume)
    {
        if (!loops.TryGetValue(sound, out var loop))
        {
            loops[sound] = loop = new Loop();
        }
        loop.Group = group;
        loop.Pos = pos;
        loop.Volume = volume;
    }

    public static void RemoveLoop(ILoadedSound? sound)
    {
        if (sound != null)
        {
            loops.Remove(sound);
        }
    }

    public static void Update(ICoreClientAPI capi)
    {
        var player = capi.World.Player?.Entity;
        if (player == null)
        {
            return;
        }
        var at = player.Pos.XYZ;
        var notes = new HashSet<string>();
        var selection = new BlockSelection();
        foreach (var be in sources)
        {
            if (be.Block == null || be.Pos.DistanceSqTo(at.X, at.Y, at.Z) > Range * Range)
            {
                continue;
            }
            selection.Position = be.Pos;
            var ambient = be.Block.GetSounds(capi.World.BlockAccessor, selection)?.Ambient;
            if (ambient != null && be.Block.GetAmbientSoundStrength(capi.World, be.Pos) > 0)
            {
                notes.Add(ambient.ToString());
            }
        }
        share = LemuriaSound.Scale / Math.Max(1, notes.Count);

        var nearest = new Dictionary<string, (ILoadedSound Sound, double DistanceSq)>();
        foreach (var (sound, loop) in loops)
        {
            double d = loop.Pos.SquareDistanceTo(at);
            if (!nearest.TryGetValue(loop.Group, out var best) || d < best.DistanceSq)
            {
                nearest[loop.Group] = (sound, d);
            }
        }
        foreach (var (sound, loop) in loops)
        {
            sound.SetVolume(nearest[loop.Group].Sound == sound ? LemuriaSound.Volume(loop.Volume) : 0f);
        }
    }

    public static void Clear()
    {
        sources.Clear();
        loops.Clear();
        share = 1f;
    }
}
