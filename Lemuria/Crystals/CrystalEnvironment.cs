using System;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace Lemuria.Crystals;

/// <summary>
/// Reads the environments round a refining cradle or a charging crystal and gives each alignment a
/// strength from 0 to 1 (design note, The four environments and Purity). Every Elemental reading is the
/// game's own: heat sources and their fall-off, the climate's temperature and rainfall, wind, and the
/// rain map that tells open sky. About 250 block reads and one climate reading per call, the Mysteries counting their signs among the same blocks. Since step 5
/// the four Mysteries are read too, by the feature that owns them (<see cref="Mysteries"/>), so a
/// reading has eight strengths and purity weighs all eight.
/// </summary>
public static class CrystalEnvironment
{
    public const int FireIndex = 0, IceIndex = 1, EarthIndex = 2, SkyIndex = 3;
    public const int LifeIndex = 4, DeathIndex = 5, TimeIndex = 6, RustIndex = 7;

    /// <summary>How many strengths a reading has: the four Elementals, then the four Mysteries.</summary>
    public const int Count = 8;

    /// <summary>
    /// The Mysteries' count of one reading: it sees every block of the same walk the Elementals read, so
    /// the cube is walked once, then fills Life, Death, Time and Rust.
    /// </summary>
    public interface IMysteryCount
    {
        /// <summary>One block of the cube, its solid and fluid layers, at this many steps from the centre.</summary>
        void Visit(BlockPos at, Block solid, Block fluid, int distance);

        /// <summary>Fills the four Mystery strengths once the walk is done.</summary>
        void Finish(float[] strengths);
    }

    /// <summary>Starts the Mysteries' count for one reading; set by the Mysteries feature, or null when they read nothing.</summary>
    public static System.Func<IWorldAccessor, BlockPos, IMysteryCount>? Mysteries { get; set; }

    /// <summary>How far the reading looks in every direction, diagonals included: a 5x5x5 cube.</summary>
    public const int Reach = 2;

    /// <summary>
    /// Strengths of Fire, Ice, Earth, Sky, Life, Death, Time and Rust before twins cancel, each capped at
    /// 1. A lightning strike's boost makes Sky full; a death nearby makes Death full (step 5 note, A death nearby).
    /// </summary>
    public static float[] Read(IWorldAccessor world, BlockPos pos, bool skyBoost, bool deathBoost = false)
    {
        var accessor = world.BlockAccessor;
        var climate = accessor.GetClimateAt(pos, EnumGetClimateMode.NowValues);
        float temperature = climate?.Temperature ?? 15f;
        float rainfall = climate?.Rainfall ?? 0f;

        float heat = 0;
        int snowIce = 0;
        int stone = 0;
        int around = 0;
        var tmp = new BlockPos(pos.dimension);
        var mysteries = Mysteries?.Invoke(world, pos);
        // Every environment reads the same 5x5x5 cube round the crystal (the owner, step 2 play-test).
        for (int dx = -Reach; dx <= Reach; dx++)
        {
            for (int dy = -Reach; dy <= Reach; dy++)
            {
                for (int dz = -Reach; dz <= Reach; dz++)
                {
                    tmp.Set(pos.X + dx, pos.Y + dy, pos.Z + dz);
                    var solid = accessor.GetBlock(tmp, BlockLayersAccess.Solid);
                    var fluid = accessor.GetBlock(tmp, BlockLayersAccess.Fluid);
                    heat += HeatFrom(world, solid, tmp, pos) + HeatFrom(world, fluid, tmp, pos);
                    mysteries?.Visit(tmp, solid, fluid, Math.Abs(dx) + Math.Abs(dy) + Math.Abs(dz));
                    if (dx == 0 && dy == 0 && dz == 0)
                    {
                        continue;
                    }
                    around++;
                    if (IsSnowOrIce(solid) || IsSnowOrIce(fluid))
                    {
                        snowIce++;
                    }
                    if (solid.BlockMaterial == EnumBlockMaterial.Stone || solid.BlockMaterial == EnumBlockMaterial.Ore)
                    {
                        stone++;
                    }
                }
            }
        }

        var strengths = new float[Count];
        float hotAir = 0.5f * Math.Clamp((temperature - 25f) / 10f, 0f, 1f);
        strengths[FireIndex] = Cap(heat / 10f + hotAir);

        float frost = 0.5f * Math.Clamp(-temperature / 10f, 0f, 1f);
        strengths[IceIndex] = Cap(frost + Math.Min(0.5f, 0.05f * snowIce));

        int rainHeight = accessor.GetRainMapHeightAt(pos.X, pos.Z);
        int cover = Math.Max(0, rainHeight - pos.Y);
        float coverStrength = Math.Clamp((cover - 8) / 32f, 0f, 1f);
        float walls = around == 0 ? 0 : Math.Min(1f, stone / (around * 0.5f));
        strengths[EarthIndex] = Cap(coverStrength * walls);

        // Open sky: nothing above the crystal blocks rain, as the game's lightning rod tells it.
        bool openSky = rainHeight <= pos.Y;
        if (openSky)
        {
            float height = 0.5f * Math.Clamp((pos.Y - world.SeaLevel) / 60f, 0f, 1f);
            double wind = accessor.GetWindSpeedAt(pos)?.X ?? 0;
            float windStrength = 0.5f * (float)Math.Clamp(Math.Abs(wind), 0, 1);
            float rain = 0.25f * Math.Clamp(rainfall, 0f, 1f);
            strengths[SkyIndex] = Cap(height + windStrength + rain);
        }
        // A strike's six hours need the same open sky as the rest of Sky (the owner, Nov 10 2026): under a roof they give nothing.
        if (skyBoost && openSky)
        {
            strengths[SkyIndex] = 1f;
        }
        mysteries?.Finish(strengths);
        for (int i = LifeIndex; i < Count; i++)
        {
            strengths[i] = Cap(strengths[i]);
        }
        if (deathBoost)
        {
            strengths[DeathIndex] = 1f;
        }
        return strengths;
    }

    private static float Cap(float value) => Math.Clamp(value, 0f, 1f);

    /// <summary>Whether nothing above this spot blocks rain: the open sky that Sky reads by, and that a lightning strike counts by.</summary>
    public static bool OpenSky(IBlockAccessor accessor, BlockPos pos) => accessor.GetRainMapHeightAt(pos.X, pos.Z) <= pos.Y;

    /// <summary>The heat a block gives the crystal, as the game gives it a player, divided by nothing yet: fire 10 is full strength.</summary>
    private static float HeatFrom(IWorldAccessor world, Block block, BlockPos sourcePos, BlockPos crystalPos)
    {
        if (block.Id == 0)
        {
            return 0;
        }
        var source = block.GetInterface<IHeatSource>(world, sourcePos);
        if (source == null)
        {
            return 0;
        }
        // Full beside it, falling off two blocks away: the fall-off EntityBehaviorBodyTemperature uses outdoors.
        double dsq = sourcePos.DistanceSqToNearerEdge(crystalPos.X + 0.5, crystalPos.Y + 0.25, crystalPos.Z + 0.5);
        float falloff = Math.Min(1f, 9f / (8f + (float)Math.Pow(dsq, 1.25)));
        return source.GetHeatStrength(world, sourcePos, crystalPos) * falloff;
    }

    private static bool IsSnowOrIce(Block block) => block.Id != 0 && (block.BlockMaterial == EnumBlockMaterial.Snow || block.BlockMaterial == EnumBlockMaterial.Ice);

    /// <summary>
    /// Twins cancel: Ice is taken from Fire and Fire from Ice, Sky from Earth and Earth from Sky, Death
    /// from Life and Life from Death, Rust from Time and Time from Rust.
    /// </summary>
    public static float[] CancelTwins(float[] s) => new[]
    {
        Math.Max(0, s[FireIndex] - s[IceIndex]),
        Math.Max(0, s[IceIndex] - s[FireIndex]),
        Math.Max(0, s[EarthIndex] - s[SkyIndex]),
        Math.Max(0, s[SkyIndex] - s[EarthIndex]),
        Math.Max(0, s[LifeIndex] - s[DeathIndex]),
        Math.Max(0, s[DeathIndex] - s[LifeIndex]),
        Math.Max(0, s[TimeIndex] - s[RustIndex]),
        Math.Max(0, s[RustIndex] - s[TimeIndex]),
    };

    /// <summary>
    /// After twins cancel, the strongest alignment leads, and passes when it is strong enough and pure
    /// enough for the Evolution: Glittering at least 0.25 and as strong as all the others together,
    /// Glowing at least 0.4 and twice as strong. Returns the leader even when it fails, for the reading.
    /// </summary>
    public static bool Lead(float[] raw, int rank, out string alignment, out float strength)
    {
        var s = CancelTwins(raw);
        int best = 0;
        for (int i = 1; i < Count; i++)
        {
            if (s[i] > s[best])
            {
                best = i;
            }
        }
        alignment = Crystal.AllAligned[best];
        strength = s[best];
        float others = 0;
        for (int i = 0; i < Count; i++)
        {
            if (i != best)
            {
                others += s[i];
            }
        }
        float minimum = rank <= 1 ? 0.25f : 0.4f;
        float ratio = rank <= 1 ? 1f : 2f;
        return strength > 0 && strength >= minimum && strength >= ratio * others;
    }

    public static int IndexOf(string alignment) => Array.IndexOf(Crystal.AllAligned, alignment);
}
