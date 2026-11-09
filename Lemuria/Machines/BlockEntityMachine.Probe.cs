using System;
using System.Linq;
using Lemuria.Core;
using Lemuria.Crystals;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace Lemuria.Machines;

/// <summary>
/// The Wind Probe (design note "Lemuria step 4 part 3", Wind Probes): the seventh carved form, its Sky
/// crystals on its top under open sky. Each gathers 400 a day at Glittering and 1,200 at Glowing, times
/// the weather where it stands: a quarter, plus the game's wind (0 to 1.5), plus half the rain or snow
/// falling on it; nothing around it is read, and twins and purity don't touch it. Its block carries
/// the game's AttractsLightning behaviour with the lightning rod's numbers; a strike that lands on it
/// fills each Sky crystal to its capacity, poured into its network before the wind's charge, as there
/// is room. Server only.
/// </summary>
public partial class BlockEntityMachine
{
    /// <summary>Charge a strike put in the probe's crystals, still to pour into its network. Saved.</summary>
    private float struck;

    private double lastProbeHours = -1;
    private double sinceProbe;

    /// <summary>The weather a probe reads at its last look: 0.25 + wind + half the rain, or 0 under a roof. For the look-at text and tests.</summary>
    public double ProbeWeather { get; private set; }

    /// <summary>Charge a strike left in the probe, not yet poured into its network.</summary>
    public float Struck => struck;

    /// <summary>Whether nothing above the probe blocks the sky, as the game's lightning rod checks it.</summary>
    public bool OpenSky => Api.World.BlockAccessor.GetRainMapHeightAt(Pos.X, Pos.Z) <= Pos.Y;

    /// <summary>The weather where the probe stands, or 0 under a roof; overrides are for the test command.</summary>
    public double ReadWeather(double? wind = null, double? rain = null)
    {
        if (!OpenSky)
        {
            return 0;
        }
        double w = wind ?? Math.Abs(Api.World.BlockAccessor.GetWindSpeedAt(Pos)?.X ?? 0);
        double r = rain ?? (Api.ModLoader.GetModSystem<WeatherSystemBase>()?.GetPrecipitation(Pos.ToVec3d().Add(0.5, 1, 0.5)) ?? 0);
        return Machine.ProbeCalm + Math.Clamp(w, 0, 1.5) + 0.5 * Math.Clamp(r, 0, 1);
    }

    private void ProbeTick(float dt)
    {
        if ((sinceProbe += dt) >= 5 || lastProbeHours < 0)
        {
            sinceProbe = 0;
            ProbeNow();
        }
    }

    /// <summary>
    /// Each Sky crystal gathers from the weather for the hours since the last look; what the network has
    /// no room for is lost, as a Gather block's is. Public for the test command, which gives the hours
    /// and the weather.
    /// </summary>
    public float ProbeNow(double? forHours = null, double? wind = null, double? rain = null)
    {
        double now = Api.World.Calendar.TotalHours;
        double hours = forHours ?? (lastProbeHours < 0 ? 0 : Math.Max(0, now - lastProbeHours));
        lastProbeHours = now;
        ProbeWeather = ReadWeather(wind, rain);
        float before = gathered;
        foreach (var crystal in Working.Where(c => c.Alignment == Crystal.Sky))
        {
            gathered += (float)(Machine.ProbePerDay(crystal.Evolution) * ProbeWeather * hours / Api.World.Calendar.HoursPerDay);
        }
        if (gathered > before)
        {
            Sparks(Crystal.Sky, 3);
        }
        return gathered - before;
    }

    /// <summary>
    /// A strike landed on the probe: each Active Sky crystal fills to its capacity, 100 or 200, and the
    /// probe holds that until its network has room. Server only; public for the test command.
    /// </summary>
    public float Strike()
    {
        if (Form != Machine.Probe || !Active)
        {
            return 0;
        }
        float fill = Working.Where(c => c.Alignment == Crystal.Sky).Sum(c => c.Capacity);
        if (fill > struck)
        {
            struck = fill;
            MarkDirty(false);
        }
        Sparks(Crystal.Sky, 12);
        return fill;
    }

    /// <summary>A probe pours what a strike left first, then the wind's charge; the network's own feed calls this.</summary>
    private float TakeProbe(float room)
    {
        float fromStrike = Math.Min(room, struck);
        if (fromStrike > 0)
        {
            struck -= fromStrike;
            MarkDirty(false);
        }
        float fromWind = Math.Min(room - fromStrike, gathered);
        gathered = 0;
        return fromStrike + fromWind;
    }

    private void ProbeInfo(System.Text.StringBuilder dsc)
    {
        if (!OpenSky)
        {
            dsc.AppendLine(Lang.Get(LemuriaModSystem.ModId + ":probe-roofed"));
        }
        if (struck >= 1)
        {
            dsc.AppendLine(Lang.Get(LemuriaModSystem.ModId + ":probe-struck", Math.Round(struck)));
        }
    }

    /// <summary>The game's lightning lands where it lands, after any rod moved it; a probe there is struck. Server only.</summary>
    public static void OnLightningLanded(IWorldAccessor world, Vec3d impactPos)
    {
        var at = impactPos.AsBlockPos;
        foreach (var pos in new[] { at, at.DownCopy() })
        {
            if (world.BlockAccessor.GetBlockEntity(pos) is BlockEntityMachine { Form: Machine.Probe } probe)
            {
                probe.Strike();
                return;
            }
        }
    }
}
