using System;
using System.Collections.Generic;
using Lemuria.Core;
using Lemuria.Crystals;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace Lemuria.Evocations;

/// <summary>
/// The caster's own client while right-click is held (design note, What you see and hear): the
/// crystal's note swells over the half-second wind-up and sparks gather at the hand; past two seconds,
/// with charge for it, a rising whine, a flare of its colour and hairline light running through it.
/// </summary>
public class WindUp : IDisposable
{
    private static readonly AssetLocation WhineSound = new(LemuriaModSystem.ModId, "sounds/crystals/whine");

    private readonly ICoreClientAPI capi;
    private readonly Dictionary<long, State> states = new();

    private class State
    {
        public ILoadedSound? Note;
        public ILoadedSound? Whine;
        public float LastSparks;
    }

    public WindUp(ICoreClientAPI capi)
    {
        this.capi = capi;
    }

    public void Begin(EntityAgent entity, string alignment)
    {
        End(entity);
        var state = new State
        {
            Note = Load(new AssetLocation(LemuriaModSystem.ModId, $"sounds/crystals/hum-{alignment}"), entity, 0.15f),
        };
        state.Note?.Start();
        states[entity.EntityId] = state;
    }

    public void Step(EntityAgent entity, string alignment, float seconds, bool canOverdrive)
    {
        if (!states.TryGetValue(entity.EntityId, out var state))
        {
            return;
        }
        var hand = Hand(entity);
        float windUp = (float)Evocation.WindUpSeconds;
        state.Note?.SetPosition((float)hand.X, (float)hand.Y, (float)hand.Z);
        state.Note?.SetVolume(Math.Min(1f, 0.15f + 0.85f * seconds / Math.Max(0.05f, windUp)));
        bool overdriven = canOverdrive && seconds >= Evocation.OverdriveSeconds;
        if (overdriven && state.Whine == null)
        {
            state.Whine = Load(WhineSound, entity, 0.6f);
            state.Whine?.Start();
            Flare(hand, Crystal.Rgb(alignment), 40);
        }
        if (state.Whine != null)
        {
            state.Whine.SetPosition((float)hand.X, (float)hand.Y, (float)hand.Z);
            state.Whine.SetPitch(Math.Min(1.6f, 1f + (seconds - (float)Evocation.OverdriveSeconds) * 0.2f));
        }
        if (seconds - state.LastSparks >= 0.08f)
        {
            state.LastSparks = seconds;
            int rgb = Crystal.Rgb(alignment);
            if (overdriven)
            {
                Flare(hand, rgb, 6);
            }
            else if (seconds >= windUp)
            {
                CrystalCharging.Sparks(capi.World, hand, rgb, 2, 0.08f);
            }
            else
            {
                CrystalCharging.Sparks(capi.World, hand, rgb, 1, 0.12f);
            }
        }
    }

    public void End(EntityAgent entity)
    {
        if (!states.Remove(entity.EntityId, out var state))
        {
            return;
        }
        Stop(state);
    }

    private ILoadedSound? Load(AssetLocation location, EntityAgent entity, float volume)
    {
        var hand = Hand(entity);
        return capi.World.LoadSound(new SoundParams
        {
            Location = location,
            ShouldLoop = true,
            DisposeOnFinish = false,
            Position = new Vec3f((float)hand.X, (float)hand.Y, (float)hand.Z),
            Range = 16,
            Volume = volume,
            SoundType = EnumSoundType.Sound,
        });
    }

    private static Vec3d Hand(EntityAgent entity)
    {
        var look = entity.Pos.GetViewVector();
        return Caster.Hand(entity.Pos.XYZ.Add(entity.LocalEyePos), new Vec3d(look.X, look.Y, look.Z));
    }

    /// <summary>The over-driven crystal's flare: bright sparks bursting out of the hand.</summary>
    private void Flare(Vec3d at, int rgba, int count)
    {
        var particles = new SimpleParticleProperties(count, count, rgba,
            at.AddCopy(-0.05, -0.05, -0.05), at.AddCopy(0.05, 0.05, 0.05),
            new Vec3f(-0.8f, -0.4f, -0.8f), new Vec3f(0.8f, 0.8f, 0.8f), 0.35f, 0f, 0.1f, 0.25f, EnumParticleModel.Quad)
        {
            VertexFlags = 255,
            OpacityEvolve = new EvolvingNatFloat(EnumTransformFunction.LINEAR, -255f),
        };
        capi.World.SpawnParticles(particles);
    }

    private static void Stop(State state)
    {
        foreach (var sound in new[] { state.Note, state.Whine })
        {
            sound?.Stop();
            sound?.Dispose();
        }
    }

    public void Dispose()
    {
        foreach (var state in states.Values)
        {
            Stop(state);
        }
        states.Clear();
    }
}
