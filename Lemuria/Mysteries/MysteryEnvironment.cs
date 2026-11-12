using System;
using System.Collections.Generic;
using Lemuria.Core;
using Lemuria.Crystals;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace Lemuria.Mysteries;

/// <summary>
/// Reads the four Mysteries round a crystal, cradle or Gather (design note "Lemuria step 5: The Four
/// Mysteries", The Four Mysteries): each is a condition of the place times a sign brought within 2 blocks,
/// both from 0 to 1, in the same 5×5×5 cube every alignment reads.
/// Life: light (daylight at that hour, or the game's own lamps in the cube) times living things, 0.1 each.
/// Death: darkness (one less the light) times bones: 0.25 a bony remains or carcass block, 0.25 every 16 bones in a container.
/// Time: stability above 1, full at 1.5, times temporal gears in containers, 0.5 each.
/// Rust: stability below 1, full at 0.5, times decay: 0.25 every 16 rot or 4 rusty gears in a container.
/// Lemuria's own light never counts, so no crystal or Glint lights its own kind (answer 4). Where the
/// world has temporal stability turned off, depth stands in for it (question 6).
/// </summary>
public static class MysteryEnvironment
{
    public const float LivingEach = 0.1f;
    public const float BoneBlockEach = 0.25f;
    public const int BonesPerStep = 16;
    public const float BonesStep = 0.25f;
    public const float GearEach = 0.5f;
    public const int RotPerStep = 16;
    public const int RustyGearsPerStep = 4;
    public const float DecayStep = 0.25f;

    /// <summary>Light that counts as full: the daylight of an open sky at noon, or two blocks from a torch.</summary>
    public const float FullSun = 20f, FullLamp = 12f;

    /// <summary>Without temporal stability, Rust grows from nothing at sea level to full this many blocks below it.</summary>
    public const int DepthForRust = 40;

    private static readonly AssetLocation BoneCode = new("game", "bone");
    private static readonly AssetLocation TemporalGearCode = new("game", "gear-temporal");
    private static readonly AssetLocation RustyGearCode = new("game", "gear-rusty");
    private static readonly AssetLocation RotCode = new("game", "rot");

    /// <summary>The creatures that are not animals: the game's monsters, and people.</summary>
    private static readonly string[] NotAnimals = { "drifter", "locust", "bell", "shiver", "bowtorn", "eidolon", "erel", "trader", "villager", "playerbot", "mechhelper" };

    /// <summary>Everything a reading found, for the strengths and for test logs.</summary>
    public struct Reading
    {
        public float Light, Living, Bones, Gears, Decay, Stability, Time, Rust;
        public bool StabilityOn;
        public int Containers;
    }

    /// <summary>The game's temporal stability system, found when the server starts; null where it is missing.</summary>
    public static SystemTemporalStability? StabilitySystem { get; set; }

    /// <summary>Starts one reading's count (CrystalEnvironment.Mysteries): it sees the Elementals' walk of the cube.</summary>
    public static CrystalEnvironment.IMysteryCount Count(IWorldAccessor world, BlockPos pos) => new Counter(world, pos);

    /// <summary>Fills a reading's Life, Death, Time and Rust from a walk of its own; for tests and logs.</summary>
    public static void Fill(IWorldAccessor world, BlockPos pos, float[] strengths)
    {
        var counter = Walk(world, pos);
        counter.Finish(strengths);
    }

    /// <summary>Everything a reading at a position finds, from a walk of its own; for tests and logs.</summary>
    public static Reading Read(IWorldAccessor world, BlockPos pos) => Walk(world, pos).Total();

    private static Counter Walk(IWorldAccessor world, BlockPos pos)
    {
        var counter = new Counter(world, pos);
        var accessor = world.BlockAccessor;
        int reach = CrystalEnvironment.Reach;
        var tmp = new BlockPos(pos.dimension);
        for (int dx = -reach; dx <= reach; dx++)
        {
            for (int dy = -reach; dy <= reach; dy++)
            {
                for (int dz = -reach; dz <= reach; dz++)
                {
                    tmp.Set(pos.X + dx, pos.Y + dy, pos.Z + dz);
                    counter.Visit(tmp, accessor.GetBlock(tmp, BlockLayersAccess.Solid), accessor.GetBlock(tmp, BlockLayersAccess.Fluid),
                        Math.Abs(dx) + Math.Abs(dy) + Math.Abs(dz));
                }
            }
        }
        return counter;
    }

    /// <summary>One reading's count of light, living things, bones, gears and decay, block by block.</summary>
    private sealed class Counter : CrystalEnvironment.IMysteryCount
    {
        private readonly IWorldAccessor world;
        private readonly BlockPos pos;
        private Reading reading;
        private float lamp;
        private int bones, rot, rusty, gears;

        public Counter(IWorldAccessor world, BlockPos pos)
        {
            this.world = world;
            this.pos = pos;
        }

        public void Visit(BlockPos at, Block block, Block fluid, int distance)
        {
            var accessor = world.BlockAccessor;
            lamp = Math.Max(lamp, LampLight(accessor, block, at, distance));
            lamp = Math.Max(lamp, LampLight(accessor, fluid, at, distance));
            if (block.Id == 0)
            {
                return;
            }
            if (IsLivingBlock(block))
            {
                reading.Living += LivingEach;
            }
            if (IsBoneBlock(block))
            {
                reading.Bones += BoneBlockEach;
            }
            // Only blocks that have one carry a block entity, so plain stone and air never look.
            if (block.EntityClass == null)
            {
                return;
            }
            var be = accessor.GetBlockEntity(at);
            if (be == null)
            {
                return;
            }
            if (IsLivingEntity(be))
            {
                reading.Living += LivingEach;
            }
            if (be is IBlockEntityContainer container && container.Inventory != null && be is not BlockEntityPlantContainer)
            {
                reading.Containers++;
                foreach (var slot in container.Inventory)
                {
                    var code = slot?.Itemstack?.Collectible?.Code;
                    if (code == null)
                    {
                        continue;
                    }
                    int size = slot!.StackSize;
                    if (code.Equals(BoneCode))
                    {
                        bones += size;
                    }
                    else if (code.Equals(TemporalGearCode))
                    {
                        gears += size;
                    }
                    else if (code.Equals(RustyGearCode))
                    {
                        rusty += size;
                    }
                    else if (code.Equals(RotCode))
                    {
                        rot += size;
                    }
                }
            }
        }

        public Reading Total()
        {
            var total = reading;
            total.Living += LivingEach * CountAnimals(world, pos);
            total.Bones += BonesStep * (bones / BonesPerStep);
            total.Gears = GearEach * gears;
            total.Decay = DecayStep * (rot / RotPerStep + rusty / RustyGearsPerStep);
            int sun = world.BlockAccessor.GetLightLevel(pos, EnumLightLevelType.TimeOfDaySunLight);
            total.Light = Math.Clamp(Math.Max(sun / FullSun, lamp / FullLamp), 0f, 1f);
            Stability(world, pos, ref total);
            return total;
        }

        public void Finish(float[] strengths)
        {
            var r = Total();
            strengths[CrystalEnvironment.LifeIndex] = r.Light * Math.Min(1f, r.Living);
            strengths[CrystalEnvironment.DeathIndex] = (1f - r.Light) * Math.Min(1f, r.Bones);
            strengths[CrystalEnvironment.TimeIndex] = r.Time * Math.Min(1f, r.Gears);
            strengths[CrystalEnvironment.RustIndex] = r.Rust * Math.Min(1f, r.Decay);
        }
    }

    /// <summary>Time and Rust's conditions: the game's stability, storms included, or depth where stability is off.</summary>
    private static void Stability(IWorldAccessor world, BlockPos pos, ref Reading reading)
    {
        reading.StabilityOn = StabilitySystem != null && world.Config.GetBool("temporalStability", true);
        if (!reading.StabilityOn)
        {
            float below = Math.Clamp((world.SeaLevel - pos.Y) / (float)DepthForRust, 0f, 1f);
            reading.Stability = 2;
            reading.Time = 1f - below;
            reading.Rust = below;
            return;
        }
        float stability = StabilitySystem!.GetTemporalStability(pos);
        reading.Stability = stability;
        reading.Time = Math.Clamp((stability - 1f) / 0.5f, 0f, 1f);
        reading.Rust = Math.Clamp((1f - stability) / 0.5f, 0f, 1f);
    }

    /// <summary>A light the game's own blocks give at this distance; Lemuria's crystals, cradles and Glints give none.</summary>
    private static float LampLight(IBlockAccessor accessor, Block block, BlockPos pos, int distance)
    {
        if (block.Id == 0 || block.Code?.Domain == LemuriaModSystem.ModId)
        {
            return 0;
        }
        var hsv = block.GetLightHsv(accessor, pos);
        return hsv == null || hsv.Length < 3 ? 0 : Math.Max(0, hsv[2] - distance);
    }

    /// <summary>A crop, sapling, legacy berry bush or tree seed, counted by its block.</summary>
    public static bool IsLivingBlock(Block block)
    {
        if (block.CropProps != null || block is BlockBerryBush)
        {
            return true;
        }
        string path = block.Code?.Domain == "game" ? block.Code.Path : "";
        return path.StartsWith("sapling-", StringComparison.Ordinal);
    }

    /// <summary>A new berry bush, a fruit tree's root, or a pot or planter with a plant in it, counted by its block entity.</summary>
    private static bool IsLivingEntity(BlockEntity be)
    {
        if (be is BlockEntityPlantContainer pot)
        {
            return pot.GetContents() != null;
        }
        if (be.GetBehavior<BEBehaviorFruitingBush>() != null)
        {
            return true;
        }
        return be.GetBehavior<FruitTreeRootBH>() != null;
    }

    public static bool IsBoneBlock(Block block)
    {
        if (block.Code?.Domain != "game")
        {
            return false;
        }
        string path = block.Code.Path;
        return path.StartsWith("bonyremains", StringComparison.Ordinal) || path.StartsWith("carcass", StringComparison.Ordinal)
            || path.StartsWith("drycarcass", StringComparison.Ordinal);
    }

    /// <summary>Living animals standing in the cube: not players, people or the game's monsters.</summary>
    private static int CountAnimals(IWorldAccessor world, BlockPos pos)
    {
        int reach = CrystalEnvironment.Reach;
        var center = pos.ToVec3d().Add(0.5, 0.5, 0.5);
        var found = world.GetEntitiesAround(center, reach + 1.5f, reach + 1.5f, IsAnimal);
        int n = 0;
        foreach (var e in found)
        {
            if (Math.Abs(e.Pos.X - center.X) <= reach + 0.5 && Math.Abs(e.Pos.Y - pos.Y) <= reach + 0.5 && Math.Abs(e.Pos.Z - center.Z) <= reach + 0.5)
            {
                n++;
            }
        }
        return n;
    }

    public static bool IsAnimal(Entity e)
    {
        if (e is not EntityAgent { Alive: true } || e is EntityPlayer)
        {
            return false;
        }
        string path = e.Code?.Path ?? "";
        foreach (string prefix in NotAnimals)
        {
            if (path.StartsWith(prefix, StringComparison.Ordinal))
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>The Mysteries' strengths alone, before twins cancel, for logs: Life, Death, Time, Rust.</summary>
    public static float[] Strengths(IWorldAccessor world, BlockPos pos)
    {
        var s = new float[CrystalEnvironment.Count];
        Fill(world, pos, s);
        return new[] { s[CrystalEnvironment.LifeIndex], s[CrystalEnvironment.DeathIndex], s[CrystalEnvironment.TimeIndex], s[CrystalEnvironment.RustIndex] };
    }
}
