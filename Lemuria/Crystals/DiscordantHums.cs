using System;
using System.Collections.Generic;
using Lemuria.Core;
using ProtoBuf;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace Lemuria.Crystals;

/// <summary>
/// A discordant hum starting or stopping at a chest or around a player, sent by the server over the
/// channel <c>lemuria-crystals</c>. Placed crystals start their own through their block entity.
/// </summary>
[ProtoContract]
public class DiscordPacket
{
    /// <summary>"pos:x,y,z" for a container, "ent:id" for a player.</summary>
    [ProtoMember(1)] public string Key { get; set; } = "";
    [ProtoMember(2)] public bool On { get; set; }
    [ProtoMember(3)] public double X { get; set; }
    [ProtoMember(4)] public double Y { get; set; }
    [ProtoMember(5)] public double Z { get; set; }
    [ProtoMember(6)] public long EntityId { get; set; }
    [ProtoMember(7)] public float Elapsed { get; set; }
    [ProtoMember(8)] public float Grace { get; set; }
}

/// <summary>
/// The client's discordant hums: two tones a semitone apart that beat faster and louder over the
/// warning, where the crystals stand, at the chest that holds them, or around the player who carries
/// them (design note, What you see and hear). Client only.
/// </summary>
public class DiscordantHums
{
    public static readonly AssetLocation Sound = new(LemuriaModSystem.ModId, "sounds/crystals/discord");

    private class Hum
    {
        public ILoadedSound Sound = null!;
        public long StartMs;
        public float Grace;
        public Vec3d? Pos;
        public long EntityId;
    }

    private readonly ICoreClientAPI capi;
    private readonly Dictionary<string, Hum> hums = new();

    public DiscordantHums(ICoreClientAPI capi)
    {
        this.capi = capi;
        capi.Event.RegisterGameTickListener(_ => Update(), 250);
    }

    public void Start(string key, Vec3d? pos, long entityId, float elapsed, float grace)
    {
        if (hums.TryGetValue(key, out var existing))
        {
            existing.Pos = pos;
            existing.StartMs = capi.ElapsedMilliseconds - (long)(elapsed * 1000);
            return;
        }
        var sound = capi.World.LoadSound(new SoundParams
        {
            Location = Sound,
            ShouldLoop = true,
            DisposeOnFinish = false,
            Position = pos?.ToVec3f() ?? new Vec3f(),
            Range = 24f,
            Volume = 0.25f,
            SoundType = EnumSoundType.Ambient,
        });
        if (sound == null)
        {
            return;
        }
        hums[key] = new Hum
        {
            Sound = sound,
            StartMs = capi.ElapsedMilliseconds - (long)(elapsed * 1000),
            Grace = grace <= 0 ? 30 : grace,
            Pos = pos,
            EntityId = entityId,
        };
        Place(hums[key]);
        sound.Start();
    }

    public void Stop(string key)
    {
        if (!hums.Remove(key, out var hum))
        {
            return;
        }
        hum.Sound.Stop();
        hum.Sound.Dispose();
    }

    public void OnPacket(DiscordPacket packet)
    {
        if (packet.On)
        {
            Start(packet.Key, packet.EntityId == 0 ? new Vec3d(packet.X, packet.Y, packet.Z) : null, packet.EntityId, packet.Elapsed, packet.Grace);
        }
        else
        {
            Stop(packet.Key);
        }
    }

    private void Place(Hum hum)
    {
        if (hum.EntityId != 0)
        {
            var entity = capi.World.GetEntityById(hum.EntityId);
            if (entity != null)
            {
                hum.Sound.SetPosition((float)entity.Pos.X, (float)entity.Pos.Y + 1f, (float)entity.Pos.Z);
            }
        }
        else if (hum.Pos != null)
        {
            hum.Sound.SetPosition((float)hum.Pos.X, (float)hum.Pos.Y, (float)hum.Pos.Z);
        }
    }

    private void Update()
    {
        foreach (var hum in hums.Values)
        {
            float t = Math.Clamp((capi.ElapsedMilliseconds - hum.StartMs) / 1000f / hum.Grace, 0f, 1f);
            hum.Sound.SetVolume(0.25f + 0.65f * t);
            hum.Sound.SetPitch(1f + 0.6f * t);
            Place(hum);
        }
    }

    public void Dispose()
    {
        foreach (var hum in hums.Values)
        {
            hum.Sound.Stop();
            hum.Sound.Dispose();
        }
        hums.Clear();
    }
}
