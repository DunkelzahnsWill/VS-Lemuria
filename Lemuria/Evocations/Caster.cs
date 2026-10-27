using System;
using Lemuria.Core;
using Lemuria.Crystals;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;

namespace Lemuria.Evocations;

/// <summary>
/// Casting on the server (design note "Lemuria step 3: Naked evocations", Casting, Over-driving and
/// cracks): checks the charge, aims by the server's own ray, applies the evocation, and spends the
/// charge only when it found something to act on.
/// </summary>
public static class Caster
{
    private static readonly AssetLocation DullSound = new(LemuriaModSystem.ModId, "sounds/crystals/dull");
    private static readonly AssetLocation ChimeSound = new(LemuriaModSystem.ModId, "sounds/crystals/chime");
    private static readonly AssetLocation CrackleSound = new(LemuriaModSystem.ModId, "sounds/crystals/crackle");
    private static readonly AssetLocation ShatterSound = new(LemuriaModSystem.ModId, "sounds/crystals/shatter");

    /// <summary>Whether a crystal holds enough charge for a cast, over-driven or not.</summary>
    public static bool HasCharge(ItemStack stack, bool overdrive) =>
        Crystal.GetCharge(stack) >= Evocation.Cost(Crystal.EvolutionOf(stack.Collectible), overdrive) - 0.001f;

    /// <summary>The dull click of a crystal too empty to cast, heard by everyone near, the caster included.</summary>
    public static void Dull(IWorldAccessor world, Entity entity)
    {
        world.PlaySoundAt(DullSound, entity.Pos.X, entity.Pos.Y + 1, entity.Pos.Z, null, true, 12f, 0.6f);
    }

    /// <summary>
    /// Casts with the crystal in the slot after right-click was held for some seconds: nothing before
    /// the wind-up, over-driven once held past the over-drive time with charge for it.
    /// </summary>
    public static void TryCast(EvocationsModSystem system, IServerPlayer player, ItemSlot slot, float secondsHeld)
    {
        var stack = slot.Itemstack;
        var entity = player.Entity;
        if (stack == null || entity == null)
        {
            return;
        }
        var plan = Plan(stack, secondsHeld);
        if (plan == Release.TooEmpty)
        {
            Dull(entity.World, entity);
        }
        if (plan != Release.Cast && plan != Release.Overdrive)
        {
            return;
        }
        string alignment = Crystal.AlignmentOf(stack.Collectible);
        string evolution = Crystal.EvolutionOf(stack.Collectible);
        bool overdrive = plan == Release.Overdrive;
        var eye = entity.Pos.XYZ.Add(entity.LocalEyePos);
        var look = entity.Pos.GetViewVector();
        var cast = CastContext.Aim(system.Api, player, entity, alignment, Evocation.Power(evolution, overdrive), eye, new Vec3d(look.X, look.Y, look.Z));
        bool acted = Effects.Apply(cast);
        system.SendCast(cast, Hand(eye, cast.Dir), acted);
        if (!acted)
        {
            return;
        }
        Spend(entity.World, slot, entity, entity.Pos.XYZ.Add(0, 1, 0), overdrive, entity.World.Rand.NextDouble());
        system.OnCast(player, cast);
    }

    public enum Release
    {
        /// <summary>Not a crystal that casts, or let go before the wind-up: nothing happens.</summary>
        Nothing,
        TooEmpty,
        Cast,
        Overdrive,
    }

    /// <summary>What letting go after holding right-click for some seconds does with this crystal.</summary>
    public static Release Plan(ItemStack stack, double secondsHeld)
    {
        if (!Evocation.Casts(stack.Collectible) || secondsHeld < Evocation.WindUpSeconds)
        {
            return Release.Nothing;
        }
        if (!HasCharge(stack, false))
        {
            return Release.TooEmpty;
        }
        return secondsHeld >= Evocation.OverdriveSeconds && HasCharge(stack, true) ? Release.Overdrive : Release.Cast;
    }

    /// <summary>About where the crystal sits in a right hand, for the streak to start from.</summary>
    public static Vec3d Hand(Vec3d eye, Vec3d dir)
    {
        var right = new Vec3d(-dir.Z, 0, dir.X);
        if (right.Length() > 0.01)
        {
            right.Normalize();
        }
        return eye.AddCopy(dir.X * 0.6 + right.X * 0.3, dir.Y * 0.6 - 0.3, dir.Z * 0.6 + right.Z * 0.3);
    }

    /// <summary>What a cast did to the crystal that paid for it.</summary>
    public readonly record struct Spent(float Charge, int Cracks, bool Shattered, bool LeftStack);

    /// <summary>
    /// Spends one crystal's charge for a cast. From a stack, that one crystal leaves it for a free slot
    /// of the holder's, or drops at their feet. An over-driven cast cracks it when the roll falls under
    /// the crack chance, and the last crack shatters it. Chimes when the charge falls a level.
    /// </summary>
    public static Spent Spend(IWorldAccessor world, ItemSlot slot, EntityAgent? holder, Vec3d where, bool overdrive, double crackRoll)
    {
        var stack = slot.Itemstack!;
        bool split = stack.StackSize > 1;
        var one = split ? slot.TakeOut(1) : stack;
        string alignment = Crystal.AlignmentOf(one.Collectible);
        string evolution = Crystal.EvolutionOf(one.Collectible);
        float capacity = Crystal.Capacity(evolution);
        float before = Crystal.GetCharge(one);
        float after = Math.Max(0, before - Evocation.Cost(evolution, overdrive));
        int cracks = Crystal.GetCracks(one);
        bool cracked = overdrive && crackRoll < Evocation.CrackChance;
        if (cracked)
        {
            cracks++;
        }
        if (cracks >= Math.Max(1, LemuriaModSystem.Config.CracksToShatter))
        {
            if (!split)
            {
                slot.Itemstack = null;
            }
            slot.MarkDirty();
            world.PlaySoundAt(ShatterSound, where.X, where.Y, where.Z, null, true, 24f, 1f);
            CrystalsModSystem.Server?.SendBurst(where, alignment);
            return new Spent(0, cracks, true, split);
        }
        Crystal.SetState(one, after, cracks);
        if (split && (holder == null || !holder.TryGiveItemStack(one)))
        {
            world.SpawnItemEntity(one, where);
        }
        slot.MarkDirty();
        if (cracked)
        {
            world.PlaySoundAt(CrackleSound, where.X, where.Y, where.Z, null, true, 16f, 0.8f);
        }
        if (Crystal.Level(after, capacity) != Crystal.Level(before, capacity))
        {
            world.PlaySoundAt(ChimeSound, where.X, where.Y, where.Z, null, true, 16f, 0.6f);
        }
        return new Spent(after, cracks, false, split);
    }
}
