using System;
using System.Collections.Generic;
using System.Linq;
using Lemuria.Core;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Server;

namespace Lemuria.Mysteries;

/// <summary>
/// Wither on creatures and players (answer 8, K5): 1.5 damage every 5 seconds for 30 seconds, 9 in all.
/// Another cast restarts the 30 seconds and never stacks higher. Kept in memory only, so a restart ends
/// every withering (the note, Storage). Server only; one tick a second over the creatures withering.
/// </summary>
public class Withering
{
    private sealed class State
    {
        public long NextTickMs;
        public int TicksLeft;
        public long CauseId;
    }

    private readonly ICoreServerAPI sapi;
    private readonly Dictionary<long, State> withering = new();

    public Withering(ICoreServerAPI sapi)
    {
        this.sapi = sapi;
    }

    private static LemuriaConfig Config => LemuriaModSystem.Config;

    public static float Damage => (float)Math.Clamp(Config.WitherDamage, 0, 1000);

    public static double TickSeconds => Math.Clamp(Config.WitherTickSeconds, 0.5, 600);

    public static int Ticks => Math.Max(1, (int)Math.Round(Math.Clamp(Config.WitherSeconds, 0.5, 3600) / TickSeconds));

    public int Count => withering.Count;

    public bool IsWithering(Entity entity) => withering.ContainsKey(entity.EntityId);

    /// <summary>Starts a withering, or restarts one already running: its first tick 5 seconds from now. Returns true.</summary>
    public bool Start(Entity entity, Entity? cause)
    {
        withering[entity.EntityId] = new State
        {
            NextTickMs = sapi.World.ElapsedMilliseconds + (long)(TickSeconds * 1000),
            TicksLeft = Ticks,
            CauseId = cause?.EntityId ?? 0,
        };
        return true;
    }

    /// <summary>Every second: deals the ticks that are due.</summary>
    public void Tick() => Advance(sapi.World.ElapsedMilliseconds);

    /// <summary>As if this many seconds passed from now, for the test command. Returns the damage dealt.</summary>
    public float Pass(double seconds) => Advance(sapi.World.ElapsedMilliseconds + (long)(seconds * 1000), shift: true);

    private float Advance(long nowMs, bool shift = false)
    {
        float dealt = 0;
        foreach (var (id, state) in withering.ToList())
        {
            var entity = sapi.World.GetEntityById(id);
            if (entity == null || !entity.Alive)
            {
                withering.Remove(id);
                continue;
            }
            while (state.TicksLeft > 0 && state.NextTickMs <= nowMs)
            {
                var source = new DamageSource
                {
                    Source = EnumDamageSource.Internal,
                    Type = EnumDamageType.Poison,
                    // A withering tick is not a hit: a blow's moment of invulnerability does not stop it.
                    IgnoreInvFrames = true,
                    CauseEntity = state.CauseId != 0 ? sapi.World.GetEntityById(state.CauseId) : null,
                };
                if (entity.ReceiveDamage(source, Damage))
                {
                    dealt += Damage;
                }
                state.TicksLeft--;
                state.NextTickMs += (long)(TickSeconds * 1000);
                if (!entity.Alive)
                {
                    break;
                }
            }
            if (state.TicksLeft <= 0 || !entity.Alive)
            {
                withering.Remove(id);
            }
            else if (shift)
            {
                // A test's skip in time: keep the next tick's distance from the real clock.
                state.NextTickMs -= nowMs - sapi.World.ElapsedMilliseconds;
            }
        }
        return dealt;
    }
}
