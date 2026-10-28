using System;
using Lemuria.Core;
using Lemuria.Crystals;
using Lemuria.Evocations;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace Lemuria.Machines;

/// <summary>
/// The numbers and names every machine follows (design note "Lemuria step 4: Machines", Carving
/// machines, Charge, and the config file): the six carved forms in the order the chisel strikes them,
/// what a working costs, and the block codes <c>machine-{form}-awakened-{rock}-{side}</c>.
/// </summary>
public static class Machine
{
    public const string Gather = "gather";
    public const string Refine = "refine";
    public const string Focus = "focus";
    public const string Scatter = "scatter";
    public const string Amplify = "amplify";
    public const string Motor = "motor";

    /// <summary>The forms in the order the chisel carves them; one more strike makes Free stone again.</summary>
    public static readonly string[] Forms = { Gather, Refine, Focus, Scatter, Amplify, Motor };

    private static LemuriaConfig Config => LemuriaModSystem.Config;

    /// <summary>The form after this one, or null for Free stone after the Motor.</summary>
    public static string? Next(string form)
    {
        int i = Array.IndexOf(Forms, form);
        return i + 1 < Forms.Length ? Forms[i + 1] : null;
    }

    public static AssetLocation Code(string form, string rock, string side) =>
        new(LemuriaModSystem.ModId, $"machine-{form}-awakened-{rock}-{side}");

    public static AssetLocation FreeCode(string rock) => new(LemuriaModSystem.ModId, "sparkstone-awakened-" + rock);

    public static bool IsMachine(Block? block) => block?.Code?.Domain == LemuriaModSystem.ModId && block.Code.Path.StartsWith("machine-", StringComparison.Ordinal);

    public static double WorkSeconds => Math.Clamp(Config.MachineWorkSeconds, 0.5, 3600);

    public static double AmplifyCost => Math.Clamp(Config.AmplifyCostMultiplier, 1, 100);

    /// <summary>A working's charge: a tenth of a hand cast, 1 for Glittering and 2 for Glowing, twice that amplified.</summary>
    public static float WorkCost(string evolution, bool amplified)
    {
        double cost = Evocation.Cost(evolution, false) * Math.Clamp(Config.MachineCostShare, 0, 10);
        return (float)(amplified ? cost * AmplifyCost : cost);
    }

    public static int ScatterSize => Math.Clamp(Config.ScatterSize, 1, 33) | 1;

    public static double GatherPerDay(string evolution) =>
        Math.Clamp(evolution == Crystal.Glowing ? Config.GatherPerDayGlowing : Config.GatherPerDayGlittering, 0, 100_000);

    /// <summary>The charge one refining costs: half the new crystal's capacity.</summary>
    public static float RefineCost(string evolution) => (float)(Crystal.Capacity(evolution) * Math.Clamp(Config.MachineRefineCostShare, 0, 100));

    /// <summary>In-game days a refining takes: a cradle's full-strength pace, 1 for Glittering and 2 for Glowing.</summary>
    public static double RefineDays(string evolution) =>
        Math.Clamp(evolution == Crystal.Glowing ? Config.RefineDaysGlowing : Config.RefineDaysGlittering, 0.01, 1000);

    public static float MotorTorque(string evolution) =>
        (float)Math.Clamp(evolution == Crystal.Glowing ? Config.MotorTorqueGlowing : Config.MotorTorqueGlittering, 0, 100);

    public static float MotorSpeed => (float)Math.Clamp(Config.MotorSpeed, 0, 1);

    public static double MotorChargePerHour(string evolution) =>
        Math.Clamp(evolution == Crystal.Glowing ? Config.MotorChargePerHourGlowing : Config.MotorChargePerHourGlittering, 0, 10_000);

    public static double QuickenShare => Math.Clamp(Config.QuickenShare, 0, 100);

    /// <summary>The in-game hours that pass in this many real seconds, at the world's speed of time.</summary>
    public static double GameHours(IWorldAccessor world, double seconds) =>
        seconds * world.Calendar.SpeedOfTime * world.Calendar.CalendarSpeedMul / 3600.0;

    /// <summary>
    /// The face a machine's glyph and sockets are on, from its {side}: the front, but a Motor's rear,
    /// since its hub takes the front that meets the axle (note rev 91, How machines look).
    /// </summary>
    public static BlockFacing GlyphFace(string form, BlockFacing side) => form == Motor ? side.Opposite : side;

    /// <summary>The {side} of a machine of this form whose glyph and sockets are on this face.</summary>
    public static BlockFacing SideFor(string form, BlockFacing glyphFace) => form == Motor ? glyphFace.Opposite : glyphFace;

    /// <summary>Where a machine's workings start: just outside the middle of its front.</summary>
    public static Vec3d FrontPoint(BlockPos pos, BlockFacing front) =>
        pos.ToVec3d().Add(0.5 + front.Normalf.X * 0.55, 0.5 + front.Normalf.Y * 0.55, 0.5 + front.Normalf.Z * 0.55);
}
