using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Lemuria.Core;
using Lemuria.Evocations;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace Lemuria.Mysteries;

/// <summary>
/// The Mysteries' naked evocations (design note "Lemuria step 5: The Four Mysteries", The naked
/// evocations), cast through step 3's casting and worked by step 4's machines: Bloom, Wither, Rewind and
/// Progress. Each returns Acted only when it changed something, so a miss and an idle machine cost nothing.
/// Every block, container and creature in a claim the caster may not touch is spared.
/// </summary>
public static class MysteryEffects
{
    private static readonly AssetLocation DeadCropCode = new("game", "deadcrop");

    /// <summary>When each plant last took Bloom and each container last took a machine's Progress, kept in the savegame.</summary>
    public static Ledger Ledger { get; } = new();

    private static LemuriaConfig Config => LemuriaModSystem.Config;

    /// <summary>The hours Rewind and Progress move by power: 12, 24, 48.</summary>
    public static double Hours(int power) =>
        Math.Clamp(Config.MysteryHoursGlittering, 0.1, 1000) * (power <= 1 ? 1 : power == 2 ? 2 : 4);

    /// <summary>The blocks a cast works on: what it aimed at, or the area from Glowing's power up; each with the block above.</summary>
    private static IEnumerable<BlockPos> Targets(CastContext cast, bool withAbove)
    {
        var seen = new HashSet<BlockPos>();
        IEnumerable<BlockPos> blocks = cast.Wide ? cast.Area() : cast.Block != null ? new[] { cast.Block.Position.Copy() } : Array.Empty<BlockPos>();
        foreach (var pos in blocks)
        {
            if (seen.Add(pos))
            {
                yield return pos;
            }
            if (withAbove)
            {
                var up = pos.UpCopy();
                if (seen.Add(up))
                {
                    yield return up;
                }
            }
        }
    }

    // ------------------------------------------------------------------ Bloom

    /// <summary>
    /// Bloom (question 7, K4): a crop, berry bush or fruit tree moves on one growth stage; a sapling or
    /// tree seed loses an in-game day of its growing time. Each plant takes Bloom once an in-game day,
    /// by hand or by Focus alike, and power sets only the area.
    /// </summary>
    public static Effects.Outcome Bloom(CastContext cast)
    {
        bool acted = false;
        var world = cast.Api.World;
        int today = (int)world.Calendar.TotalDays;
        foreach (var pos in Targets(cast, true))
        {
            if (!cast.Can(pos))
            {
                continue;
            }
            var plant = PlantAt(world, pos);
            if (plant == null)
            {
                continue;
            }
            string key = Ledger.BloomKey(plant.Value.Key);
            if (Ledger.Last(key) is double last && (int)(last / world.Calendar.HoursPerDay) >= today)
            {
                continue;
            }
            if (plant.Value.Grow())
            {
                Ledger.Mark(key, world.Calendar.TotalHours);
                acted = true;
            }
        }
        return acted ? Effects.Outcome.Acted : Effects.Outcome.Nothing;
    }

    /// <summary>
    /// How many plants a Bloom would move on, without moving any (feature 10's limits for machines that
    /// Bloom, K4): each plant in reach that the caster may change, that has not taken Bloom today and has a
    /// stage left to take, counted once however many of its blocks lie in reach. A machine pays by it.
    /// </summary>
    public static int BloomCount(CastContext cast)
    {
        var world = cast.Api.World;
        int today = (int)world.Calendar.TotalDays;
        var counted = new HashSet<BlockPos>();
        foreach (var pos in Targets(cast, true))
        {
            if (!cast.Can(pos) || PlantAt(world, pos) is not { } plant || !plant.CanGrow())
            {
                continue;
            }
            if (Ledger.Last(Ledger.BloomKey(plant.Key)) is double last && (int)(last / world.Calendar.HoursPerDay) >= today)
            {
                continue;
            }
            counted.Add(plant.Key);
        }
        return counted.Count;
    }

    /// <summary>Whether this position's plant took Bloom today already. For tests and the look-at.</summary>
    public static bool BloomedToday(IWorldAccessor world, BlockPos pos)
    {
        var plant = PlantAt(world, pos);
        int today = (int)world.Calendar.TotalDays;
        return plant != null && Ledger.Last(Ledger.BloomKey(plant.Value.Key)) is double last && (int)(last / world.Calendar.HoursPerDay) >= today;
    }

    private readonly struct Plant
    {
        public readonly BlockPos Key;
        public readonly Func<bool> Grow;

        /// <summary>Whether <see cref="Grow"/> has a stage to give, asked without growing anything.</summary>
        public readonly Func<bool> CanGrow;

        public Plant(BlockPos key, Func<bool> grow, Func<bool> canGrow)
        {
            Key = key;
            Grow = grow;
            CanGrow = canGrow;
        }
    }

    private static readonly MethodInfo? SetBushState =
        typeof(BEBehaviorFruitingBush).GetMethod("setGrowthState", BindingFlags.Instance | BindingFlags.NonPublic);

    /// <summary>The plant growing at this position, with what one Bloom does to it; null for anything else.</summary>
    private static Plant? PlantAt(IWorldAccessor world, BlockPos pos)
    {
        var accessor = world.BlockAccessor;
        var block = accessor.GetBlock(pos);
        var be = accessor.GetBlockEntity(pos);
        if (block.CropProps != null && block is BlockCrop crop)
        {
            return new Plant(pos.Copy(), () => GrowCrop(world, pos.Copy(), crop), () => crop.CurrentCropStage < crop.CropProps.GrowthStages);
        }
        if (be?.GetBehavior<BEBehaviorFruitingBush>() is { } bush)
        {
            return new Plant(pos.Copy(), () => GrowBush(bush), () => SetBushState != null && NextBushState(bush) != null);
        }
        if (block is BlockBerryBush)
        {
            return new Plant(pos.Copy(), () => GrowLegacyBush(world, pos.Copy(), block), () => block.Variant["state"] is "empty" or "flowering");
        }
        if (be is BlockEntitySapling sapling)
        {
            return new Plant(pos.Copy(), () => ShortenSapling(world, sapling), () => true);
        }
        if (be is BlockEntityFruitTreePart part && part.RootOff != null && part.TreeType != null)
        {
            var rootPos = pos.AddCopy(part.RootOff.X, part.RootOff.Y, part.RootOff.Z);
            return new Plant(rootPos, () => GrowFruitTree(world, rootPos, part.TreeType), () => FruitTreeDays(world, rootPos, part.TreeType) > 0);
        }
        return null;
    }

    /// <summary>One growth stage on, by the farmland's own growing (nutrients and all) where it stands on farmland.</summary>
    private static bool GrowCrop(IWorldAccessor world, BlockPos pos, BlockCrop crop)
    {
        if (crop.CurrentCropStage >= crop.CropProps.GrowthStages)
        {
            return false;
        }
        if (world.BlockAccessor.GetBlockEntity(pos.DownCopy()) is BlockEntityFarmland farmland)
        {
            return farmland.TryGrowCrop(world.Calendar.TotalHours);
        }
        var next = world.GetBlock(crop.CodeWithParts((crop.CurrentCropStage + 1).ToString()));
        if (next == null)
        {
            return false;
        }
        world.BlockAccessor.ExchangeBlock(next.BlockId, pos);
        return true;
    }

    private static EnumFruitingBushGrowthState? NextBushState(BEBehaviorFruitingBush bush) => bush.BState?.Growthstate switch
    {
        EnumFruitingBushGrowthState.Young => EnumFruitingBushGrowthState.Mature,
        EnumFruitingBushGrowthState.Mature => EnumFruitingBushGrowthState.Flowering,
        EnumFruitingBushGrowthState.Flowering => EnumFruitingBushGrowthState.Ripening,
        EnumFruitingBushGrowthState.Ripening => EnumFruitingBushGrowthState.Ripe,
        _ => null,
    };

    private static bool GrowBush(BEBehaviorFruitingBush bush)
    {
        var next = NextBushState(bush);
        if (next == null || SetBushState == null)
        {
            return false;
        }
        SetBushState.Invoke(bush, new object[] { next.Value });
        return true;
    }

    /// <summary>A legacy berry bush: empty to flowering to ripe, as its own growing does; a ripe one waits to be picked.</summary>
    private static bool GrowLegacyBush(IWorldAccessor world, BlockPos pos, Block block)
    {
        string? state = block.Variant["state"];
        string? next = state == "empty" ? "flowering" : state == "flowering" ? "ripe" : null;
        var nextBlock = next == null ? null : world.GetBlock(block.CodeWithVariant("state", next));
        if (nextBlock == null)
        {
            return false;
        }
        world.BlockAccessor.ExchangeBlock(nextBlock.BlockId, pos);
        world.BlockAccessor.MarkBlockDirty(pos);
        return true;
    }

    /// <summary>A sapling or planted tree seed: one in-game day off its growing time, through its own saved state.</summary>
    private static bool ShortenSapling(IWorldAccessor world, BlockEntitySapling sapling)
    {
        var tree = new TreeAttribute();
        sapling.ToTreeAttributes(tree);
        if (!tree.HasAttribute("totalHoursTillGrowth"))
        {
            return false;
        }
        tree.SetDouble("totalHoursTillGrowth", tree.GetDouble("totalHoursTillGrowth") - world.Calendar.HoursPerDay);
        sapling.FromTreeAttributes(tree, world);
        sapling.MarkDirty(true);
        return true;
    }

    /// <summary>A flowering tree sets fruit, a fruiting one ripens, at its next look; other seasons wait.</summary>
    private static bool GrowFruitTree(IWorldAccessor world, BlockPos rootPos, string treeType)
    {
        var root = world.BlockAccessor.GetBlockEntity(rootPos);
        double days = FruitTreeDays(world, rootPos, treeType);
        if (days <= 0 || root?.GetBehavior<FruitTreeRootBH>() is not { } bh || !bh.propsByType.TryGetValue(treeType, out var props))
        {
            return false;
        }
        props.lastStateChangeTotalDays -= days;
        root.MarkDirty(true);
        return true;
    }

    /// <summary>The days a Bloom takes off a fruit tree's present state: its flowering or fruiting days, 0 in other seasons.</summary>
    private static double FruitTreeDays(IWorldAccessor world, BlockPos rootPos, string treeType)
    {
        var bh = world.BlockAccessor.GetBlockEntity(rootPos)?.GetBehavior<FruitTreeRootBH>();
        if (bh == null || !bh.propsByType.TryGetValue(treeType, out var props))
        {
            return 0;
        }
        return props.State switch
        {
            EnumFruitTreeState.Flowering => props.FloweringDays,
            EnumFruitTreeState.Fruiting => props.FruitingDays,
            _ => 0,
        };
    }

    // ------------------------------------------------------------------ Wither

    /// <summary>
    /// Wither (answer 8, K5): a crop becomes the game's dead crop, keeping its farmland; saplings, tall
    /// grass, flowers, ferns and leaves wilt away, dropping what breaking them drops; grassy soil loses its
    /// grass. From Glowing's power it also withers every creature and player in its area but the caster:
    /// 1.5 damage every 5 seconds for 30 seconds, a new cast restarting the 30 seconds.
    /// </summary>
    public static Effects.Outcome Wither(CastContext cast)
    {
        bool acted = false;
        foreach (var pos in Targets(cast, true))
        {
            if (cast.Can(pos) && WitherBlock(cast, pos))
            {
                acted = true;
            }
        }
        if (cast.Power >= 2)
        {
            foreach (var entity in cast.EntitiesInArea(e => e is EntityAgent { Alive: true } && e.EntityId != cast.Caster?.EntityId))
            {
                if (cast.Can(entity) && MysteriesModSystem.Withering?.Start(entity, cast.Caster) == true)
                {
                    acted = true;
                }
            }
        }
        return acted ? Effects.Outcome.Acted : Effects.Outcome.Nothing;
    }

    private static readonly string[] Wilting = { "sapling-", "tallgrass-", "flower-", "fern-", "tallfern-", "leaves" };

    public static bool Wilts(Block block)
    {
        if (block.Code?.Domain != "game")
        {
            return false;
        }
        string path = block.Code.Path;
        return Wilting.Any(p => path.StartsWith(p, StringComparison.Ordinal));
    }

    private static bool WitherBlock(CastContext cast, BlockPos pos)
    {
        var world = cast.Api.World;
        var accessor = world.BlockAccessor;
        var block = accessor.GetBlock(pos);
        if (block.Id == 0)
        {
            return false;
        }
        if (block.CropProps != null)
        {
            var dead = world.GetBlock(DeadCropCode);
            if (dead == null)
            {
                return false;
            }
            accessor.SetBlock(dead.BlockId, pos);
            if (accessor.GetBlockEntity(pos) is BlockEntityDeadCrop deadCrop)
            {
                deadCrop.Inventory[0].Itemstack = new ItemStack(block);
                deadCrop.deathReason = EnumCropStressType.Unknown;
                deadCrop.MarkDirty(true);
            }
            return true;
        }
        if (Wilts(block))
        {
            if (cast.Machine?.Collect is { } collect)
            {
                // A machine's drops go into a container touching it, as Gale's do; only what does not fit drops.
                var drops = block.GetDrops(world, pos, null) ?? Array.Empty<ItemStack>();
                accessor.SetBlock(0, pos);
                accessor.TriggerNeighbourBlockUpdate(pos);
                foreach (var stack in drops)
                {
                    if (stack != null && collect(stack) is { StackSize: > 0 } left)
                    {
                        world.SpawnItemEntity(left, pos.ToVec3d().Add(0.5, 0.5, 0.5));
                    }
                }
            }
            else
            {
                accessor.BreakBlock(pos, null);
            }
            return true;
        }
        // Grassy soil (soil-{fertility}-{grass}) loses its grass.
        if (block.Code?.Domain == "game" && block.Code.Path.StartsWith("soil-", StringComparison.Ordinal) && block.Variant["grasscoverage"] is { } grass && grass != "none")
        {
            var bare = world.GetBlock(block.CodeWithVariant("grasscoverage", "none"));
            if (bare != null)
            {
                accessor.ExchangeBlock(bare.BlockId, pos);
                accessor.MarkBlockDirty(pos);
                return true;
            }
        }
        return false;
    }

    // ------------------------------------------------------------------ Rewind and Progress

    /// <summary>
    /// Rewind (question 9): food spoiling in a container, ground storage or dropped turns back 12, 24 or
    /// 48 in-game hours by power, never past fresh; only spoiling turns back. A machine turns back only food
    /// with a full measure spoiled (question 12), so a Time Focus keeps a chest fresh for about 2 a day.
    /// </summary>
    public static Effects.Outcome Rewind(CastContext cast) => MoveTime(cast, back: true);

    /// <summary>
    /// Progress (question 9): every slow change the game gives the items there moves on 12, 24 or 48
    /// in-game hours, and a sealed barrel's recipe comes that much closer. A machine works each container
    /// only once a full measure of hours has passed since it last did (question 12).
    /// </summary>
    public static Effects.Outcome Progress(CastContext cast) => MoveTime(cast, back: false);

    private static Effects.Outcome MoveTime(CastContext cast, bool back)
    {
        var world = cast.Api.World;
        double hours = Hours(cast.Power);
        bool machine = cast.Machine != null;
        bool acted = false;
        foreach (var pos in Targets(cast, false))
        {
            if (!cast.Can(pos) || world.BlockAccessor.GetBlockEntity(pos) is not { } be)
            {
                continue;
            }
            string key = Ledger.ProgressKey(pos);
            if (!back && machine && !MeasureDue(world, key, hours))
            {
                continue;
            }
            bool moved = false;
            if (be is IBlockEntityContainer container && container.Inventory != null && be is not BlockEntityPlantContainer)
            {
                foreach (var slot in container.Inventory)
                {
                    if (MoveStack(world, slot, hours, back, machine))
                    {
                        moved = true;
                    }
                }
            }
            if (!back && be is BlockEntityBarrel { Sealed: true } barrel)
            {
                barrel.SealedSinceTotalHours -= hours;
                moved = true;
            }
            if (moved)
            {
                be.MarkDirty(true);
                if (!back && machine)
                {
                    Ledger.Mark(key, world.Calendar.TotalHours);
                }
                acted = true;
            }
        }
        var items = cast.Wide
            ? cast.EntitiesInArea(e => e is EntityItem)
            : cast.Entity is EntityItem aimed ? new List<Entity> { aimed } : new List<Entity>();
        foreach (var entity in items.OfType<EntityItem>())
        {
            string key = Ledger.ProgressKey(entity.EntityId);
            if (!cast.Can(entity) || (!back && machine && !MeasureDue(world, key, hours)))
            {
                continue;
            }
            if (MoveStack(world, entity.Slot, hours, back, machine))
            {
                entity.Itemstack = entity.Slot.Itemstack;
                if (!back && machine)
                {
                    Ledger.Mark(key, world.Calendar.TotalHours);
                }
                acted = true;
            }
        }
        return acted ? Effects.Outcome.Acted : Effects.Outcome.Nothing;
    }

    private static bool MeasureDue(IWorldAccessor world, string key, double hours) =>
        Ledger.Last(key) is not double last || world.Calendar.TotalHours - last >= hours - 0.001;

    /// <summary>
    /// Moves one stack's transitions: back, only its spoiling, never below fresh (and for a machine only
    /// when a full measure has spoiled); on, every transition it has, then lets the game turn it into
    /// what it becomes. Returns whether the stack changed.
    /// </summary>
    public static bool MoveStack(IWorldAccessor world, ItemSlot? slot, double hours, bool back, bool machine)
    {
        var stack = slot?.Itemstack;
        var collectible = stack?.Collectible;
        if (stack == null || collectible == null)
        {
            return false;
        }
        var props = collectible.GetTransitionableProperties(world, stack, null);
        if (props == null || props.Length == 0)
        {
            return false;
        }
        // Bring the clock up to now first, as the container's own tick would.
        collectible.UpdateAndGetTransitionStates(world, slot!);
        stack = slot!.Itemstack;
        if (stack == null || stack.Attributes["transitionstate"] is not ITreeAttribute state
            || state["transitionedHours"] is not FloatArrayAttribute transitioned)
        {
            return false;
        }
        props = stack.Collectible.GetTransitionableProperties(world, stack, null) ?? Array.Empty<TransitionableProperties>();
        bool changed = false;
        for (int i = 0; i < props.Length && i < transitioned.value.Length; i++)
        {
            if (props[i] == null)
            {
                continue;
            }
            if (back)
            {
                if (props[i].Type != EnumTransitionType.Perish)
                {
                    continue;
                }
                float before = transitioned.value[i];
                if (before <= 0.01f || (machine && before < hours - 0.001))
                {
                    continue;
                }
                transitioned.value[i] = (float)Math.Max(0, before - hours);
                changed = true;
            }
            else
            {
                transitioned.value[i] += (float)hours;
                changed = true;
            }
        }
        if (!changed)
        {
            return false;
        }
        if (!back)
        {
            // The game turns a stack into the next one a transition at a time.
            for (int n = 0; n < 4 && slot.Itemstack != null; n++)
            {
                var code = slot.Itemstack.Collectible.Code;
                slot.Itemstack.Collectible.UpdateAndGetTransitionStates(world, slot);
                if (slot.Itemstack == null || slot.Itemstack.Collectible.Code.Equals(code))
                {
                    break;
                }
            }
        }
        slot.MarkDirty();
        return true;
    }
}
