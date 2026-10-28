using Lemuria.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace Lemuria.Evocations;

/// <summary>
/// The four naked evocations (design note "Lemuria step 3: Naked evocations", The four evocations).
/// Each acts through the game's own hooks, on the server, and returns whether it found anything to
/// act on; a cast that found nothing fizzles and costs nothing, and a Tremor that cannot rise whole
/// fails with its cost spent. Every block and creature inside a
/// claim the caster has no access to is spared.
/// </summary>
public static class Effects
{
    /// <summary>Seconds a torch is held to a block before it catches; a cast lights it at once.</summary>
    private const float IgniteSeconds = 4f;

    private static readonly AssetLocation FireCode = new("game", "fire");
    private static readonly AssetLocation LakeIceCode = new("game", "lakeice");
    private static readonly AssetLocation ExtinguishSound = new("game", "sounds/effect/extinguish");

    /// <summary>What a cast came to: nothing to act on (a free miss), done, or failed with its cost spent (Tremor).</summary>
    public enum Outcome
    {
        Nothing,
        Acted,
        Failed,
    }

    public static Outcome Apply(CastContext cast) => Evocation.Of(cast.Alignment) switch
    {
        Evocation.Ignite => Done(Ignite(cast)),
        Evocation.Quench => Done(Quench(cast)),
        Evocation.Tremor => Tremor(cast),
        Evocation.Gale => Done(Gale(cast)),
        _ => Outcome.Nothing,
    };

    private static Outcome Done(bool acted) => acted ? Outcome.Acted : Outcome.Nothing;

    private static bool IsCreature(Entity e) => e is EntityAgent { Alive: true };

    // ------------------------------------------------------------------ Ignite

    /// <summary>
    /// Lights what a torch can light, at once; aimed at anything else, sets a fire on the face aimed at,
    /// as a torch does when you sneak. From Glowing up it also lights what can be lit in the area and
    /// sets its creatures and players alight. The caster is never set alight.
    /// </summary>
    public static bool Ignite(CastContext cast)
    {
        bool acted = false;
        var aimed = cast.Block?.Position;
        if (aimed != null && cast.Power >= 3 && IsRawRock(cast.Api.World.BlockAccessor.GetBlock(aimed)))
        {
            return Melt(cast, aimed);
        }
        if (aimed != null && Thawed(cast.Api.World, aimed, cast.Power) != null)
        {
            return Thaw(cast, aimed);
        }
        if (cast.Water != null)
        {
            return Boil(cast);
        }
        if (aimed != null && cast.Can(aimed))
        {
            var lit = TryIgnite(cast, aimed);
            acted = lit == Lighting.Lit || (lit == Lighting.NotIgnitable && SetFire(cast, cast.Block!));
        }
        if (!cast.Wide)
        {
            return acted;
        }
        foreach (var pos in cast.Area())
        {
            if (!pos.Equals(aimed) && cast.Can(pos) && TryIgnite(cast, pos) == Lighting.Lit)
            {
                acted = true;
            }
        }
        if (cast.Power < 2)
        {
            return acted;
        }
        foreach (var entity in cast.EntitiesInArea(e => IsCreature(e) && e.EntityId != cast.Caster?.EntityId && !e.IsOnFire))
        {
            if (cast.Can(entity))
            {
                entity.Ignite();
                acted = true;
            }
        }
        return acted;
    }

    private static readonly AssetLocation LavaCode = new("game", "lava-still-7");
    private static readonly AssetLocation SizzleSound = new("game", "sounds/sizzle");

    /// <summary>The world's own rock, never ore, cobblestone or polished rock.</summary>
    private static bool IsRawRock(Block block) => block.Code?.Domain == "game" && block.Code.Path.StartsWith("rock-");

    /// <summary>
    /// At power 3 (an over-driven Glowing crystal; Luminous in step 6), raw rock aimed at melts: a 2×2×1
    /// on its level, the aimed block and the ones forward, right and forward-right of it, into still
    /// lava, which runs as lava does (the owner, 2026-10-27, "let it run"). The block under the caster's
    /// feet never melts, and each melted block is claim-checked and written to the audit log. If nothing
    /// melts, the cast fizzles free.
    /// </summary>
    private static bool Melt(CastContext cast, BlockPos aimed)
    {
        var world = cast.Api.World;
        var accessor = world.BlockAccessor;
        var lava = world.GetBlock(LavaCode);
        if (lava == null)
        {
            return false;
        }
        var underFeet = cast.Caster?.Pos.AsBlockPos.DownCopy();
        var (fx, fz, rx, rz) = cast.Heading();
        int melted = 0;
        foreach (var (dx, dz) in new[] { (0, 0), (fx, fz), (rx, rz), (fx + rx, fz + rz) })
        {
            var pos = aimed.AddCopy(dx, 0, dz);
            if (!IsRawRock(accessor.GetBlock(pos)) || pos.Equals(underFeet) || !cast.Can(pos))
            {
                continue;
            }
            accessor.SetBlock(0, pos);
            accessor.SetBlock(lava.BlockId, pos, BlockLayersAccess.Fluid);
            accessor.TriggerNeighbourBlockUpdate(pos);
            cast.Api.Logger.Audit("{0} melted rock to lava with a Fire crystal at {1}", cast.ActorName, pos);
            melted++;
        }
        if (melted > 0)
        {
            world.PlaySoundAt(SizzleSound, aimed.X + 0.5, aimed.Y + 0.5, aimed.Z + 0.5, null, true, 24f, LemuriaSound.Volume(1f));
        }
        return melted > 0;
    }

    private static readonly AssetLocation WaterCode = new("game", "water-still-7");

    private enum Thawing
    {
        /// <summary>Ice that melts to still water, as the game's lake ice does.</summary>
        ToWater,
        /// <summary>Snow that vanishes with no water.</summary>
        Away,
        /// <summary>A block that sheds its snow cover.</summary>
        Uncover,
    }

    /// <summary>
    /// How a block thaws under Ignite (step 3 note rev 178, "Melting ice and boiling water"): lake ice and
    /// the loose glacier ice melt to water at every Evolution, the world's glacier and packed glacier ice
    /// only at the power of Luminous; snow layers and snow blocks vanish; snow cover comes off.
    /// </summary>
    private static (Thawing How, int Layer)? Thawed(IWorldAccessor world, BlockPos pos, int power)
    {
        var accessor = world.BlockAccessor;
        foreach (int layer in new[] { BlockLayersAccess.Fluid, BlockLayersAccess.Solid })
        {
            var block = accessor.GetBlock(pos, layer);
            if (block.Code?.Domain != "game")
            {
                continue;
            }
            string path = block.Code.Path;
            if (path is "lakeice" or "ice-glacier" or "ice-packedglacier" || (power >= 3 && path is "glacierice" or "packedglacierice"))
            {
                return (Thawing.ToWater, layer);
            }
            if (layer == BlockLayersAccess.Solid && (block is BlockSnow || path.StartsWith("snowlayer-")))
            {
                return (Thawing.Away, layer);
            }
            if (layer == BlockLayersAccess.Solid && block.snowLevel > 0 && block.notSnowCovered != null && block.notSnowCovered != block)
            {
                return (Thawing.Uncover, layer);
            }
        }
        return null;
    }

    /// <summary>
    /// Aimed at ice or snow, Ignite melts it instead of setting a fire: the block aimed at at
    /// Glittering, all ice and snow in the area from Glowing up (the 3×3×1 carpet, or the 3×3×3 at the
    /// power of Luminous). Each block is claim-checked.
    /// </summary>
    private static bool Thaw(CastContext cast, BlockPos aimed)
    {
        var world = cast.Api.World;
        var accessor = world.BlockAccessor;
        var water = world.GetBlock(WaterCode);
        int thawed = 0;
        foreach (var pos in cast.Wide ? cast.Area() : new[] { aimed })
        {
            if (Thawed(world, pos, cast.Power) is not var (how, layer) || !cast.Can(pos))
            {
                continue;
            }
            switch (how)
            {
                case Thawing.ToWater:
                    if (water == null)
                    {
                        continue;
                    }
                    accessor.SetBlock(0, pos, layer);
                    accessor.SetBlock(water.BlockId, pos, BlockLayersAccess.Fluid);
                    break;
                case Thawing.Away:
                    accessor.SetBlock(0, pos);
                    break;
                case Thawing.Uncover:
                    accessor.SetBlock(accessor.GetBlock(pos).notSnowCovered.BlockId, pos);
                    break;
            }
            accessor.TriggerNeighbourBlockUpdate(pos);
            thawed++;
        }
        if (thawed > 0)
        {
            world.PlaySoundAt(SizzleSound, aimed.X + 0.5, aimed.Y + 0.5, aimed.Z + 0.5, null, true, 16f, LemuriaSound.Volume(1f));
        }
        return thawed > 0;
    }

    /// <summary>
    /// At the power of Luminous, Ignite aimed at water, fresh or salt, still or running, boils every
    /// water block within one block of the hit away to nothing, with a hiss and steam (step 3 note rev
    /// 178). Salt from salt water is a machine's work, not this. Lakes refill by the game's own rules.
    /// </summary>
    private static bool Boil(CastContext cast)
    {
        var world = cast.Api.World;
        var accessor = world.BlockAccessor;
        int boiled = 0;
        foreach (var pos in cast.Area())
        {
            if (!CastContext.IsBoilable(accessor, pos) || !cast.Can(pos))
            {
                continue;
            }
            accessor.SetBlock(0, pos, BlockLayersAccess.Fluid);
            accessor.TriggerNeighbourBlockUpdate(pos);
            Steam.MinPos.Set(pos.X, pos.Y + 0.5, pos.Z);
            world.SpawnParticles(Steam);
            boiled++;
        }
        if (boiled > 0)
        {
            world.PlaySoundAt(SizzleSound, cast.Water!.X + 0.5, cast.Water.Y + 0.5, cast.Water.Z + 0.5, null, true, 24f, LemuriaSound.Volume(1f));
        }
        return boiled > 0;
    }

    private static readonly SimpleParticleProperties Steam = new(
        6, 10, ColorUtil.ToRgba(90, 230, 230, 230), new Vec3d(), new Vec3d(1, 0.5, 1),
        new Vec3f(-0.2f, 0.4f, -0.2f), new Vec3f(0.2f, 1f, 0.2f), 1.5f, -0.02f, 0.5f, 1.5f, EnumParticleModel.Quad)
    {
        WindAffected = true,
        SelfPropelled = true,
        OpacityEvolve = EvolvingNatFloat.create(EnumTransformFunction.LINEAR, -90),
    };

    private enum Lighting
    {
        /// <summary>Not a block a torch lights: a cast sets a fire on its face instead.</summary>
        NotIgnitable,
        /// <summary>A block a torch lights, but not now (already burning, or no fuel): nothing happens, as with a torch.</summary>
        NotNow,
        Lit,
    }

    /// <summary>Lights a block through the ignition interface torches use, if it can be lit now.</summary>
    private static Lighting TryIgnite(CastContext cast, BlockPos pos)
    {
        var world = cast.Api.World;
        var ignitable = world.BlockAccessor.GetBlock(pos).GetInterface<IIgnitable>(world, pos);
        if (ignitable == null)
        {
            return Lighting.NotIgnitable;
        }
        var state = ignitable.OnTryIgniteBlock(cast.Caster!, pos, 0);
        if (state == EnumIgniteState.NotIgnitable)
        {
            return Lighting.NotIgnitable;
        }
        if (state == EnumIgniteState.NotIgnitablePreventDefault)
        {
            return Lighting.NotNow;
        }
        var handling = EnumHandling.PassThrough;
        ignitable.OnTryIgniteBlockOver(cast.Caster!, pos, IgniteSeconds, ref handling);
        return Lighting.Lit;
    }

    /// <summary>The game's fire on the face aimed at, as a torch sets it, in the caster's name for the audit log.</summary>
    private static bool SetFire(CastContext cast, BlockSelection aimed)
    {
        var world = cast.Api.World;
        var accessor = world.BlockAccessor;
        var pos = aimed.Position.AddCopy(aimed.Face);
        var fire = world.GetBlock(FireCode);
        if (fire == null || !cast.Can(pos) || accessor.GetBlock(pos).Id != 0 || accessor.GetBlock(pos, BlockLayersAccess.Fluid).Id != 0)
        {
            return false;
        }
        accessor.SetBlock(fire.BlockId, pos);
        accessor.GetBlockEntity(pos)?.GetBehavior<BEBehaviorBurning>()?.OnFirePlaced(aimed.Face, cast.Player?.PlayerUID);
        if (cast.Machine != null)
        {
            cast.Api.Logger.Audit("{0} set a fire with a Fire crystal machine at {1}", cast.ActorName, pos);
        }
        return true;
    }

    // ------------------------------------------------------------------ Quench

    /// <summary>
    /// Puts out fires, as rain does, freezes still fresh water surfaces into the game's lake ice, and
    /// waters farmland as a watering can does (the owner, 2026-10-27). At Glittering it acts on the
    /// block aimed at and the face in front of it, or the water surface
    /// or burning creature aimed at; from Glowing up on everything in the area (3×3×1, 3×3×3 over-driven).
    /// It also cools lava as water does: a source to obsidian, flowing lava to basalt. The caster's own Quench
    /// puts the caster out when they stand in it.
    /// </summary>
    public static bool Quench(CastContext cast)
    {
        var aimed = cast.Block?.Position;
        if (aimed != null && cast.Power >= 3 && PackedFrom(cast.Api.World, aimed) != null)
        {
            return Pack(cast, aimed);
        }
        bool acted = false;
        var watered = new HashSet<BlockPos>();
        IEnumerable<BlockPos> blocks = cast.Wide
            ? cast.Area()
            : cast.Block != null
                ? new[] { cast.Block.Position, cast.Block.Position.AddCopy(cast.Block.Face) }
                : cast.Water != null ? new[] { cast.Water } : Array.Empty<BlockPos>();
        foreach (var pos in blocks)
        {
            if (cast.Can(pos) && (Extinguish(cast, pos) | Freeze(cast, pos) | Water(cast, pos, watered) | Cool(cast, pos)))
            {
                acted = true;
            }
        }
        var burning = cast.Wide
            ? cast.EntitiesInArea(e => e.IsOnFire)
            : cast.EntitiesInArea(e => e.IsOnFire && e.EntityId == cast.Caster?.EntityId);
        if (!cast.Wide && cast.Entity is { IsOnFire: true } aimedEntity)
        {
            burning.Add(aimedEntity);
        }
        foreach (var entity in burning)
        {
            if (cast.Can(entity))
            {
                entity.IsOnFire = false;
                acted = true;
            }
        }
        return acted;
    }

    private static void KeepTemperatures(ItemSlot slot, Vec3d pos, float targettemp, bool sizzle)
    {
    }

    /// <summary>Puts out one block's fire the way the game does for rain and water; fuel not yet alight stays.</summary>
    private static bool Extinguish(CastContext cast, BlockPos pos)
    {
        var world = cast.Api.World;
        var accessor = world.BlockAccessor;
        var entity = accessor.GetBlockEntity(pos);
        switch (entity)
        {
            case BlockEntityFirepit firepit when firepit.IsBurning:
                firepit.CoolNow(30, KeepTemperatures);
                return true;
            case BlockEntityForge forge when forge.IsBurning:
                forge.CoolNow(30, KeepTemperatures);
                return true;
            case BlockEntityCoalPile pile when pile.IsBurning:
                pile.Extinguish();
                return true;
        }
        // Fire blocks, burning ground storage and pit kilns burn through the game's burning behavior.
        if (entity?.GetBehavior<BEBehaviorBurning>() is { IsBurning: true } burning)
        {
            burning.KillFire(false);
            world.PlaySoundAt(ExtinguishSound, pos.X + 0.5, pos.Y + 0.5, pos.Z + 0.5, null, false, 16, LemuriaSound.Volume(1f));
            return true;
        }
        if (entity is BlockEntityGroundStorage { IsBurning: true } storage)
        {
            storage.Extinguish();
            return true;
        }
        // A burning torch goes out as it does in rain.
        var block = accessor.GetBlock(pos);
        if (block is BlockTorch && block.Variant["state"] == "lit" && world.GetBlock(block.CodeWithVariant("state", "extinct")) is { } extinct)
        {
            accessor.SetBlock(extinct.BlockId, pos);
            world.PlaySoundAt(ExtinguishSound, pos.X + 0.5, pos.Y + 0.5, pos.Z + 0.5, null, false, 16, LemuriaSound.Volume(1f));
            return true;
        }
        return false;
    }

    /// <summary>Freezes a still fresh water surface into lake ice, in the water's own layer, as the game's frost does.</summary>
    private static bool Freeze(CastContext cast, BlockPos pos)
    {
        var accessor = cast.Api.World.BlockAccessor;
        var ice = cast.Api.World.GetBlock(LakeIceCode);
        if (ice == null || !CastContext.IsFreezable(accessor, pos))
        {
            return false;
        }
        accessor.SetBlock(ice.BlockId, pos, BlockLayersAccess.Fluid);
        return true;
    }

    private static readonly AssetLocation GlacierIceCode = new("game", "glacierice");
    private static readonly AssetLocation PackedIceCode = new("game", "packedglacierice");

    /// <summary>
    /// What a block of ice packs into, and the layer it lies in: lake ice (fluid layer) to glacier ice,
    /// glacier ice (the world's, or the meltable ice-glacier) to packed glacier ice. Null for anything
    /// else, packed ice included.
    /// </summary>
    private static (Block Into, int Layer)? PackedFrom(IWorldAccessor world, BlockPos pos)
    {
        var accessor = world.BlockAccessor;
        foreach (int layer in new[] { BlockLayersAccess.Solid, BlockLayersAccess.Fluid })
        {
            var code = accessor.GetBlock(pos, layer).Code;
            if (code?.Domain != "game")
            {
                continue;
            }
            var into = code.Path switch
            {
                "lakeice" => world.GetBlock(GlacierIceCode),
                "glacierice" or "ice-glacier" => world.GetBlock(PackedIceCode),
                _ => null,
            };
            if (into != null)
            {
                return (into, layer);
            }
        }
        return null;
    }

    /// <summary>
    /// At power 3 (an over-driven Glowing crystal; Luminous in step 6), ice aimed at packs (the owner,
    /// 2026-10-27; note rev 143): a 2×2×1 placed as Ignite's melt is, lake ice to glacier ice and glacier
    /// ice to packed glacier ice, the game's natural blocks, which don't melt where they stand. Each block
    /// is claim-checked; a block with something else in the solid layer over lake ice stays. If nothing
    /// packs, the cast fizzles free.
    /// </summary>
    private static bool Pack(CastContext cast, BlockPos aimed)
    {
        var world = cast.Api.World;
        var accessor = world.BlockAccessor;
        var (fx, fz, rx, rz) = cast.Heading();
        Block? last = null;
        foreach (var (dx, dz) in new[] { (0, 0), (fx, fz), (rx, rz), (fx + rx, fz + rz) })
        {
            var pos = aimed.AddCopy(dx, 0, dz);
            if (PackedFrom(world, pos) is not var (into, layer) || !cast.Can(pos))
            {
                continue;
            }
            if (layer == BlockLayersAccess.Fluid)
            {
                if (accessor.GetBlock(pos, BlockLayersAccess.Solid).BlockId != 0)
                {
                    continue;
                }
                accessor.SetBlock(0, pos, BlockLayersAccess.Fluid);
            }
            accessor.SetBlock(into.BlockId, pos);
            accessor.TriggerNeighbourBlockUpdate(pos);
            last = into;
        }
        if (last?.Sounds?.Place.Location is { } sound)
        {
            world.PlaySoundAt(sound, aimed.X + 0.5, aimed.Y + 0.5, aimed.Z + 0.5, null, true, 16f, LemuriaSound.Volume(1f));
        }
        return last != null;
    }

    /// <summary>
    /// Cools lava as water does in the game: a source to obsidian, flowing lava to basalt (the owner,
    /// 2026-10-27).
    /// </summary>
    private static bool Cool(CastContext cast, BlockPos pos)
    {
        var world = cast.Api.World;
        var accessor = world.BlockAccessor;
        var fluid = accessor.GetBlock(pos, BlockLayersAccess.Fluid);
        if (fluid.LiquidCode != "lava")
        {
            return false;
        }
        var rock = world.GetBlock(new AssetLocation("game", fluid.Code.Path == "lava-still-7" ? "rock-obsidian" : "rock-basalt"));
        if (rock == null)
        {
            return false;
        }
        accessor.SetBlock(0, pos, BlockLayersAccess.Fluid);
        accessor.SetBlock(rock.BlockId, pos);
        accessor.TriggerNeighbourBlockUpdate(pos);
        world.PlaySoundAt(SizzleSound, pos.X + 0.5, pos.Y + 0.5, pos.Z + 0.5, null, true, 16f, LemuriaSound.Volume(1f));
        return true;
    }

    /// <summary>
    /// Soaks the farmland at a block, or under the crop growing there, to full moisture through the
    /// game's own watering, as a full watering can does. Farmland already soaked is left as it is.
    /// </summary>
    private static bool Water(CastContext cast, BlockPos pos, HashSet<BlockPos> watered)
    {
        var accessor = cast.Api.World.BlockAccessor;
        var at = accessor.GetBlockEntity(pos) is BlockEntityFarmland ? pos
            : accessor.GetBlock(pos) is BlockCrop ? pos.DownCopy() : null;
        if (at == null || !watered.Add(at) || !cast.Can(at) || accessor.GetBlockEntity(at) is not BlockEntityFarmland farmland || farmland.MoistureLevel >= 0.99f)
        {
            return false;
        }
        farmland.WaterFarmland(2f, false);
        return true;
    }

    // ------------------------------------------------------------------ Tremor

    /// <summary>A block the ground is made of, which Tremor may move: soil, sand, gravel, clay, peat, natural rock and ore.</summary>
    private static bool Movable(Block block)
    {
        if (block.Id == 0 || block.IsLiquid())
        {
            return false;
        }
        return block.BlockMaterial switch
        {
            EnumBlockMaterial.Soil or EnumBlockMaterial.Sand or EnumBlockMaterial.Gravel or EnumBlockMaterial.Ore => true,
            // Stone counts only as the world's own rock, never cobblestone, bricks or polished stone.
            EnumBlockMaterial.Stone => block.Code?.Path.StartsWith("rock-") == true,
            _ => false,
        };
    }

    /// <summary>Open for rising ground: air, water, or a plant or thin snow with no block entity. Never lava.</summary>
    private static bool IsOpen(IBlockAccessor accessor, BlockPos pos)
    {
        var fluid = accessor.GetBlock(pos, BlockLayersAccess.Fluid);
        if (fluid.Id != 0 && fluid.LiquidCode != "water" && fluid.LiquidCode != "saltwater")
        {
            return false;
        }
        var block = accessor.GetBlock(pos);
        return block.Id == 0 || (block.Replaceable >= 6000 && accessor.GetBlockEntity(pos) == null);
    }

    /// <summary>The top block of the ground in one column, within 3 blocks of the given height; null when there is none.</summary>
    private static BlockPos? GroundTop(IBlockAccessor accessor, BlockPos start)
    {
        var top = start.Copy();
        if (Movable(accessor.GetBlock(top)))
        {
            for (int i = 0; i < 3 && Movable(accessor.GetBlock(top.UpCopy())); i++)
            {
                top.Up();
            }
            return top;
        }
        for (int i = 0; i < 3; i++)
        {
            top.Down();
            if (Movable(accessor.GetBlock(top)))
            {
                return top;
            }
        }
        return null;
    }

    /// <summary>One column of a Tremor: its ground top and the plant or thin snow on it, read before anything moves.</summary>
    private class Column
    {
        public BlockPos Top = null!;
        public Block? Cover;
        public readonly List<Block> Blocks = new();
    }

    /// <summary>
    /// A 2×2 of real ground rises (step 3 note, Tremor; the owner, 2026-10-27): the aimed column and the
    /// ones forward, right and forward-right of it lift their own top blocks 2, 4 or 6 with Evolution,
    /// surface on top. The blocks that fill in beneath come from the 8 columns on its four sides, never
    /// its corners: each gives up the blocks just under its surface and sinks half the rise, keeping its
    /// surface and any plant or thin snow on top. Nothing is copied or made. Water is never moved:
    /// rising ground pushes it aside and it flows into the sunken ground. It happens whole or not at all:
    /// a block that is not natural ground, a block entity, a claim or no open space above fails it with
    /// its cost spent; no ground at all is a free miss. Riders on the rise go up; whatever stands on the
    /// sides drops.
    /// </summary>
    public static Outcome Tremor(CastContext cast)
    {
        var accessor = cast.Api.World.BlockAccessor;
        var aimed = cast.Block?.Position ?? cast.Entity?.Pos.AsBlockPos.DownCopy();
        var start = aimed == null ? null : GroundTop(accessor, aimed);
        if (start == null)
        {
            return Outcome.Nothing;
        }
        int sink = Evocation.TremorSink(cast.Power);
        int rise = 2 * sink;
        var (fx, fz, rx, rz) = cast.Heading();
        var footprint = new[] { (0, 0), (fx, fz), (rx, rz), (fx + rx, fz + rz) };
        var risers = new List<Column>();
        var sides = new List<(Column Side, Column Riser)>();
        foreach (var (dx, dz) in footprint)
        {
            var top = GroundTop(accessor, new BlockPos(start.X + dx, start.Y, start.Z + dz, start.dimension));
            if (top == null)
            {
                return Outcome.Failed;
            }
            risers.Add(new Column { Top = top });
        }
        foreach (var riser in risers)
        {
            foreach (var face in BlockFacing.HORIZONTALS)
            {
                int x = riser.Top.X + face.Normali.X, z = riser.Top.Z + face.Normali.Z;
                if (footprint.Any(c => start.X + c.Item1 == x && start.Z + c.Item2 == z))
                {
                    continue;
                }
                var top = GroundTop(accessor, new BlockPos(x, riser.Top.Y, z, start.dimension));
                if (top == null)
                {
                    return Outcome.Failed;
                }
                sides.Add((new Column { Top = top }, riser));
            }
        }
        // Read every block that moves and check it, before anything changes.
        foreach (var riser in risers)
        {
            for (int i = rise - 1; i >= 0; i--)
            {
                if (!Readable(cast, accessor, riser.Top.DownCopy(i), riser.Blocks))
                {
                    return Outcome.Failed;
                }
            }
            for (int i = 1; i <= rise; i++)
            {
                if (!IsOpen(accessor, riser.Top.UpCopy(i)) || !cast.Can(riser.Top.UpCopy(i)))
                {
                    return Outcome.Failed;
                }
            }
            riser.Cover = Cover(accessor, riser.Top);
        }
        foreach (var (side, _) in sides)
        {
            for (int i = sink; i >= 0; i--)
            {
                if (!Readable(cast, accessor, side.Top.DownCopy(i), side.Blocks))
                {
                    return Outcome.Failed;
                }
            }
            if (accessor.GetBlockEntity(side.Top.UpCopy()) != null || !cast.Can(side.Top.UpCopy()))
            {
                return Outcome.Failed;
            }
            side.Cover = Cover(accessor, side.Top);
        }

        var world = cast.Api.World;
        var riders = world.GetEntitiesAround(start.ToVec3d().Add(0.5 + (fx + rx) * 0.5, 0.5, 0.5 + (fz + rz) * 0.5), 4, rise + 6, e => e is EntityItem || IsCreature(e))
            .Select(e => (Entity: e, Drop: Standing(e, risers) != null ? rise : Standing(e, sides.Select(s => s.Side)) != null ? -sink : 0))
            .Where(p => p.Drop != 0)
            .ToList();

        foreach (var riser in risers)
        {
            // From the bottom up: the sides' blocks, then the column's own top blocks, then its cover.
            var fill = sides.Where(s => s.Riser == riser).SelectMany(s => s.Side.Blocks.Take(sink)).ToList();
            var column = fill.Concat(riser.Blocks).ToList();
            var bottom = riser.Top.DownCopy(rise - 1);
            for (int i = 0; i < column.Count; i++)
            {
                var pos = bottom.UpCopy(i);
                accessor.SetBlock(0, pos, BlockLayersAccess.Fluid);
                accessor.SetBlock(column[i].BlockId, pos);
            }
            var coverPos = riser.Top.UpCopy(rise + 1);
            if (riser.Cover != null && accessor.GetBlock(coverPos).Id == 0)
            {
                accessor.SetBlock(riser.Cover.BlockId, coverPos);
            }
        }
        foreach (var (side, _) in sides)
        {
            // The surface drops onto what is left; the cover follows it; the rest is open air.
            var surface = side.Blocks[^1];
            var newTop = side.Top.DownCopy(sink);
            accessor.SetBlock(surface.BlockId, newTop);
            for (int i = 1; i <= sink; i++)
            {
                accessor.SetBlock(0, newTop.UpCopy(i));
            }
            if (side.Cover != null)
            {
                accessor.SetBlock(0, side.Top.UpCopy());
                accessor.SetBlock(side.Cover.BlockId, newTop.UpCopy());
            }
        }
        foreach (var column in risers.Concat(sides.Select(s => s.Side)))
        {
            for (int i = -sink - 1; i <= rise + 1; i++)
            {
                accessor.TriggerNeighbourBlockUpdate(column.Top.UpCopy(i));
            }
        }
        foreach (var (entity, drop) in riders)
        {
            entity.TeleportToDouble(entity.Pos.X, entity.Pos.Y + drop, entity.Pos.Z);
        }
        return Outcome.Acted;
    }

    /// <summary>Reads one block that will move, if it may: natural ground, no block entity, not in a claim the caster may not build in.</summary>
    private static bool Readable(CastContext cast, IBlockAccessor accessor, BlockPos pos, List<Block> into)
    {
        var block = accessor.GetBlock(pos);
        if (!Movable(block) || accessor.GetBlockEntity(pos) != null || !cast.Can(pos))
        {
            return false;
        }
        into.Add(block);
        return true;
    }

    /// <summary>The plant or thin snow on a column's top, which rides with its surface.</summary>
    private static Block? Cover(IBlockAccessor accessor, BlockPos top)
    {
        var above = accessor.GetBlock(top.UpCopy());
        return above.Id != 0 && above.Replaceable >= 6000 && accessor.GetBlockEntity(top.UpCopy()) == null ? above : null;
    }

    /// <summary>The column an entity stands on, if it is one of these.</summary>
    private static Column? Standing(Entity entity, IEnumerable<Column> columns) =>
        columns.FirstOrDefault(c => c.Top.X == (int)Math.Floor(entity.Pos.X) && c.Top.Z == (int)Math.Floor(entity.Pos.Z)
            && entity.Pos.Y >= c.Top.Y + 0.5 && entity.Pos.Y <= c.Top.Y + 2.5);

    // ------------------------------------------------------------------ Gale

    /// <summary>
    /// Speed a push starts at per block it should carry: a push lifts the target a little, and it flies
    /// about 35 physics ticks and slides a few more before the ground stops it.
    /// </summary>
    private const double PushPerBlock = 1 / 35.0;

    private const double Lift = 0.12;

    /// <summary>
    /// A gust pushes creatures, players and dropped items away from the caster and a little up, about
    /// 6, 8 or 10 blocks: what is aimed at, or lies where the cast lands, at Glittering, everything in
    /// the area above that, passing over the caster. Then, in the same area, it strips grown leaves and ripe
    /// crops and pans sand and gravel out (<see cref="Reaping"/>).
    /// </summary>
    public static bool Gale(CastContext cast)
    {
        bool Pushable(Entity e) => (e is EntityItem || IsCreature(e)) && e.EntityId != cast.Caster?.EntityId;
        var targets = !cast.Wide && cast.Entity != null
            ? (Pushable(cast.Entity) ? new List<Entity> { cast.Entity } : new List<Entity>())
            : cast.EntitiesInArea(Pushable);
        double speed = Evocation.GalePush(cast.Power) * PushPerBlock;
        bool acted = false;
        foreach (var entity in targets)
        {
            if (!cast.Can(entity))
            {
                continue;
            }
            var away = new Vec3d(entity.Pos.X - cast.From.X, 0, entity.Pos.Z - cast.From.Z);
            if (away.Length() < 0.01)
            {
                away.Set(cast.Dir.X, 0, cast.Dir.Z);
            }
            away.Normalize();
            Push(entity, away.X * speed, Lift, away.Z * speed);
            acted = true;
        }
        // Then the gust strips leaves and ripe crops and pans sand and gravel out (Reaping).
        if (Reaping.Reap(cast))
        {
            acted = true;
        }
        return acted;
    }

    /// <summary>
    /// Pushes an entity. Creatures and players take it as the game's knockback, which their own physics
    /// applies on whichever side moves them (the client, for a player); items move on the server.
    /// </summary>
    public static void Push(Entity entity, double x, double y, double z)
    {
        if (entity is EntityItem)
        {
            entity.Pos.Motion.Set(x, y, z);
            return;
        }
        double keep = 1 - Math.Clamp(entity.Properties.KnockbackResistance, 0, 1);
        var attributes = entity.WatchedAttributes;
        attributes.SetDouble("kbdirX", x * keep);
        attributes.SetDouble("kbdirY", y * keep);
        attributes.SetDouble("kbdirZ", z * keep);
        attributes.SetInt("onHurtCounter", attributes.GetInt("onHurtCounter") + 1);
        // Too small to count as a hurt, but it tells the health behavior to apply the knockback.
        attributes.SetFloat("onHurt", 0.01f);
    }
}
