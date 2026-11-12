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

    /// <summary>The Wind Probe, the seventh carved form (step 4 part 3): sockets on top, gathering from the weather.</summary>
    public const string Probe = "probe";

    /// <summary>The forms in the order the chisel carves them; one more strike makes Free stone again.</summary>
    public static readonly string[] Forms = { Gather, Refine, Focus, Scatter, Amplify, Motor, Probe };

    private static LemuriaConfig Config => LemuriaModSystem.Config;

    /// <summary>The form after this one, or null for Free stone after the last, the Wind Probe.</summary>
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

    /// <summary>The chance a reclamation plate claims salt for one block of salt water boiled (step 4 part 2: 1 in 4).</summary>
    public static double SaltChance => Math.Clamp(Config.SaltChance, 0, 1);

    public static float DeadSocketTorque => (float)Math.Clamp(Config.DeadSocketTorque, 0, 100);

    public static double DeadSocketChargePerHour => Math.Clamp(Config.DeadSocketChargePerHour, 0, 10_000);

    /// <summary>Logs a felling takes for one working, by power: 1 at Glittering's, 9 at Glowing's, 27 at Luminous's (question 6).</summary>
    public static int LogsPerWorking(int power) => power <= 1 ? 1 : power == 2 ? 9 : 27;

    /// <summary>The in-game hours that pass in this many real seconds, at the world's speed of time.</summary>
    public static double GameHours(IWorldAccessor world, double seconds) =>
        seconds * world.Calendar.SpeedOfTime * world.Calendar.CalendarSpeedMul / 3600.0;

    /// <summary>
    /// The face a machine's glyph and sockets are on, from its {side}: the front, but a Motor's rear,
    /// since its hub takes the front that meets the axle (note rev 91, How machines look).
    /// </summary>
    public static BlockFacing GlyphFace(string form, BlockFacing side) => form switch
    {
        Motor => side.Opposite,
        // A Wind Probe's sockets are always on its top, whichever face was carved (step 4 part 3, question 9).
        Probe => BlockFacing.UP,
        _ => side,
    };

    /// <summary>The {side} of a machine of this form whose glyph and sockets are on this face.</summary>
    public static BlockFacing SideFor(string form, BlockFacing glyphFace) => form switch
    {
        Motor => glyphFace.Opposite,
        Probe => BlockFacing.UP,
        _ => glyphFace,
    };

    /// <summary>Charge a crystal in a Wind Probe gathers in an in-game day at a weather of 1 (J2: 400 Glittering, 1,200 Glowing).</summary>
    public static double ProbePerDay(string evolution) =>
        Math.Clamp(evolution == Crystal.Glowing ? Config.ProbePerDayGlowing : Config.ProbePerDayGlittering, 0, 100_000);

    /// <summary>What a Wind Probe gathers in still, dry air, as a share of its rate (J2: a quarter).</summary>
    public static double ProbeCalm => Math.Clamp(Config.ProbeCalmShare, 0, 10);

    /// <summary>Charge one block of a battery's medium holds (part 3: 200, a Glowing crystal's worth).</summary>
    public static float BatteryPerBlock => (float)Math.Clamp(Config.BatteryChargePerBlock, 1, 100_000);

    /// <summary>Share of what a battery stored that its drain pays back (part 3: 4 in 5).</summary>
    public static float BatteryReturn => (float)Math.Clamp(Config.BatteryReturnShare, 0, 1);

    /// <summary>Charge a battery stone moves an in-game hour for each crystal (part 3: 200 Glittering, 600 Glowing).</summary>
    public static double BatteryPerHour(string evolution) =>
        Math.Clamp(evolution == Crystal.Glowing ? Config.BatteryPerHourGlowing : Config.BatteryPerHourGlittering, 0, 1_000_000);

    /// <summary>The fill takes only what lifts its network above this share of its capacity (question 2: three quarters).</summary>
    public static float BatteryFillAbove => (float)Math.Clamp(Config.BatteryFillAbove, 0, 1);

    /// <summary>The drain pays only while its network is below this share, never lifting it past (question 2: half).</summary>
    public static float BatteryDrainBelow => (float)Math.Clamp(Config.BatteryDrainBelow, 0, BatteryFillAbove);

    /// <summary>Charge one step of an Earth deposit's wear pays (question 6: 50).</summary>
    public static float EarthStep => (float)Math.Clamp(Config.EarthChargePerStep, 1, 100_000);

    /// <summary>A battery room's largest side, in blocks (part 3: 7).</summary>
    public static int BatteryMaxSide => Math.Clamp(Config.BatteryMaxSide, 2, 16);

    /// <summary>Where a machine's workings start: just outside the middle of its front.</summary>
    public static Vec3d FrontPoint(BlockPos pos, BlockFacing front) =>
        pos.ToVec3d().Add(0.5 + front.Normalf.X * 0.55, 0.5 + front.Normalf.Y * 0.55, 0.5 + front.Normalf.Z * 0.55);
}
