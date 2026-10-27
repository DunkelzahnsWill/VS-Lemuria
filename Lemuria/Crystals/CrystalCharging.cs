using System;
using Lemuria.Core;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace Lemuria.Crystals;

/// <summary>
/// Charging, shared by placed crystals and crystals resting in a cradle (design note, Charge): an
/// aligned crystal fills wherever its own alignment leads and passes the purity test for its
/// Evolution, in proportion to the leading strength, and never while a twin stands within 3 blocks.
/// </summary>
public static class CrystalCharging
{
    /// <summary>How often a standing crystal reads its surroundings, in milliseconds.</summary>
    public const int ReadIntervalMs = 30_000;

    /// <summary>Hours of full Sky strength a lightning strike within 16 blocks gives.</summary>
    public const double LightningHours = 6;

    public static double FillDays(string evolution)
    {
        var config = LemuriaModSystem.Config;
        double days = evolution == Crystal.Glowing ? config.FillDaysGlowing : config.FillDaysGlittering;
        return Math.Clamp(days, 0.01, 1000);
    }

    /// <summary>
    /// Reads the surroundings for an aligned crystal and returns the strength it charges at: the leading
    /// strength when its own alignment leads and passes, otherwise 0.
    /// </summary>
    public static float Strength(IWorldAccessor world, BlockPos pos, string alignment, string evolution, bool skyBoost)
    {
        var raw = CrystalEnvironment.Read(world, pos, skyBoost);
        bool passes = CrystalEnvironment.Lead(raw, Crystal.Rank(evolution), out var leader, out var strength);
        return passes && leader == alignment ? strength : 0f;
    }

    /// <summary>The charge gained over some in-game hours at a strength, never past full.</summary>
    public static float Gain(IWorldAccessor world, string evolution, float charge, float strength, double hours)
    {
        if (strength <= 0 || hours <= 0)
        {
            return charge;
        }
        float capacity = Crystal.Capacity(evolution);
        double perHour = capacity / (FillDays(evolution) * world.Calendar.HoursPerDay);
        return (float)Math.Min(capacity, charge + perHour * strength * hours);
    }

    /// <summary>Faint sparks of a crystal's colour rising from it, as it charges or refines. Client side.</summary>
    public static void Sparks(IWorldAccessor world, Vec3d center, int rgba, int count, float spread = 0.2f)
    {
        var particles = new SimpleParticleProperties(count, count, rgba,
            center.AddCopy(-spread, 0, -spread), center.AddCopy(spread, 0.2, spread),
            new Vec3f(-0.05f, 0.25f, -0.05f), new Vec3f(0.05f, 0.6f, 0.05f), 1.2f, -0.02f, 0.15f, 0.35f, EnumParticleModel.Quad)
        {
            VertexFlags = 200,
            OpacityEvolve = new EvolvingNatFloat(EnumTransformFunction.LINEAR, -200f),
            SizeEvolve = new EvolvingNatFloat(EnumTransformFunction.LINEAR, -0.15f),
        };
        world.SpawnParticles(particles);
    }

    /// <summary>A burst of the crystal's colour as it shatters.</summary>
    public static void Burst(IWorldAccessor world, Vec3d center, int rgba)
    {
        var particles = new SimpleParticleProperties(30, 40, rgba,
            center.AddCopy(-0.2, -0.1, -0.2), center.AddCopy(0.2, 0.3, 0.2),
            new Vec3f(-2f, -0.5f, -2f), new Vec3f(2f, 2.5f, 2f), 0.8f, 1f, 0.2f, 0.6f, EnumParticleModel.Cube)
        {
            VertexFlags = 255,
            OpacityEvolve = new EvolvingNatFloat(EnumTransformFunction.LINEAR, -150f),
        };
        world.SpawnParticles(particles);
    }
}
