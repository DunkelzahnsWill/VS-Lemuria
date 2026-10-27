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
/// act on; a cast that found nothing fizzles and costs nothing. Every block and creature inside a
/// claim the caster has no access to is spared.
/// </summary>
public static class Effects
{
    /// <summary>Seconds a torch is held to a block before it catches; a cast lights it at once.</summary>
    private const float IgniteSeconds = 4f;

    private static readonly AssetLocation FireCode = new("game", "fire");
    private static readonly AssetLocation LakeIceCode = new("game", "lakeice");
    private static readonly AssetLocation ExtinguishSound = new("game", "sounds/effect/extinguish");

    public static bool Apply(CastContext cast) => Evocation.Of(cast.Alignment) switch
    {
        Evocation.Ignite => Ignite(cast),
        Evocation.Quench => Quench(cast),
        Evocation.Tremor => Tremor(cast),
        Evocation.Gale => Gale(cast),
        _ => false,
    };

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
        if (aimed != null && cast.Can(aimed))
        {
            var lit = TryIgnite(cast, aimed);
            acted = lit == Lighting.Lit || (lit == Lighting.NotIgnitable && SetFire(cast, cast.Block!));
        }
        if (cast.Power < 2)
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
        return true;
    }

    // ------------------------------------------------------------------ Quench

    /// <summary>
    /// Puts out fires, as rain does, freezes still fresh water surfaces into the game's lake ice, and
    /// waters farmland as a watering can does (the owner, 2026-10-27). At Glittering it acts on the
    /// block aimed at and the face in front of it, or the water surface
    /// or burning creature aimed at; from Glowing up on everything in the area (2×2×2, 3×3×3 over-driven). The caster's own Quench
    /// puts the caster out when they stand in it.
    /// </summary>
    public static bool Quench(CastContext cast)
    {
        bool acted = false;
        var watered = new HashSet<BlockPos>();
        IEnumerable<BlockPos> blocks = cast.Power >= 2
            ? cast.Area()
            : cast.Block != null
                ? new[] { cast.Block.Position, cast.Block.Position.AddCopy(cast.Block.Face) }
                : cast.Water != null ? new[] { cast.Water } : Array.Empty<BlockPos>();
        foreach (var pos in blocks)
        {
            if (cast.Can(pos) && (Extinguish(cast, pos) | Freeze(cast, pos) | Water(cast, pos, watered)))
            {
                acted = true;
            }
        }
        var burning = cast.Power >= 2
            ? cast.EntitiesInArea(e => e.IsOnFire)
            : cast.EntitiesInArea(e => e.IsOnFire && e.EntityId == cast.Caster?.EntityId);
        if (cast.Power < 2 && cast.Entity is { IsOnFire: true } aimedEntity)
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

    private static readonly EnumBlockMaterial[] Ground =
    {
        EnumBlockMaterial.Soil, EnumBlockMaterial.Sand, EnumBlockMaterial.Gravel, EnumBlockMaterial.Stone, EnumBlockMaterial.Ore,
    };

    private class Column
    {
        public BlockPos Top = null!;
        public Block Surface = null!;
        public Block Filler = null!;
        public Block? Plant;
    }

    /// <summary>
    /// The ground aimed at rises: one column at Glittering, a 3×3 mound at Glowing, 5×5 over-driven.
    /// Its surface stays on top and the rise is filled with what lies beneath, rock in place of ore. A
    /// column stays put when it holds a block entity or lacks open air or plants above it. Creatures,
    /// players and items standing on it ride up, the caster included, and the rise stays.
    /// </summary>
    public static bool Tremor(CastContext cast)
    {
        var aimed = cast.Block?.Position ?? cast.Entity?.Pos.AsBlockPos.DownCopy();
        if (aimed == null)
        {
            return false;
        }
        int rise = Evocation.TremorRise;
        int r = Evocation.TremorRadius(cast.Power);
        var columns = new List<Column>();
        for (int dx = -r; dx <= r; dx++)
        {
            for (int dz = -r; dz <= r; dz++)
            {
                var column = Plan(cast, new BlockPos(aimed.X + dx, aimed.Y, aimed.Z + dz, aimed.dimension), rise);
                if (column != null)
                {
                    columns.Add(column);
                }
            }
        }
        if (columns.Count == 0)
        {
            return false;
        }
        var center = aimed.ToVec3d().Add(0.5, 0.5, 0.5);
        var riders = cast.Api.World.GetEntitiesAround(center, r + 2, rise + 6, e => e is EntityItem || IsCreature(e))
            .Select(e => (Entity: e, Column: columns.FirstOrDefault(c =>
                c.Top.X == (int)Math.Floor(e.Pos.X) && c.Top.Z == (int)Math.Floor(e.Pos.Z)
                && e.Pos.Y >= c.Top.Y + 0.5 && e.Pos.Y <= c.Top.Y + 1.5 + rise)))
            .Where(p => p.Column != null)
            .ToList();
        var accessor = cast.Api.World.BlockAccessor;
        foreach (var column in columns)
        {
            accessor.SetBlock(column.Surface.BlockId, column.Top.UpCopy(rise));
            for (int i = 0; i < rise; i++)
            {
                accessor.SetBlock(column.Filler.BlockId, column.Top.UpCopy(i));
            }
            var plantPos = column.Top.UpCopy(rise + 1);
            if (column.Plant != null && accessor.GetBlock(plantPos).Id == 0 && accessor.GetBlock(plantPos, BlockLayersAccess.Fluid).Id == 0)
            {
                accessor.SetBlock(column.Plant.BlockId, plantPos);
            }
            accessor.TriggerNeighbourBlockUpdate(column.Top.UpCopy(rise));
        }
        foreach (var (entity, _) in riders)
        {
            entity.TeleportToDouble(entity.Pos.X, entity.Pos.Y + rise, entity.Pos.Z);
        }
        return true;
    }

    private static bool IsGround(Block block) => block.Id != 0 && block.IsLiquid() == false && Ground.Contains(block.BlockMaterial);

    /// <summary>Open for the rise: air, or a plant, snow or the like with no block entity, and no liquid.</summary>
    private static bool IsOpen(IBlockAccessor accessor, BlockPos pos)
    {
        var block = accessor.GetBlock(pos);
        return accessor.GetBlock(pos, BlockLayersAccess.Fluid).Id == 0
            && (block.Id == 0 || (block.Replaceable >= 6000 && accessor.GetBlockEntity(pos) == null));
    }

    /// <summary>Finds the ground's top block near the aimed height in one column and checks it may rise.</summary>
    private static Column? Plan(CastContext cast, BlockPos start, int rise)
    {
        var world = cast.Api.World;
        var accessor = world.BlockAccessor;
        var top = start.Copy();
        if (IsGround(accessor.GetBlock(top)))
        {
            for (int i = 0; i < 3 && IsGround(accessor.GetBlock(top.UpCopy())); i++)
            {
                top.Up();
            }
        }
        else
        {
            int i = 0;
            while (i < 3 && !IsGround(accessor.GetBlock(top)))
            {
                top.Down();
                i++;
            }
        }
        var surface = accessor.GetBlock(top);
        var beneath = accessor.GetBlock(top.DownCopy());
        if (!IsGround(surface) || !IsGround(beneath) || accessor.GetBlockEntity(top) != null || accessor.GetBlockEntity(top.DownCopy()) != null)
        {
            return null;
        }
        for (int i = 1; i <= rise; i++)
        {
            if (!IsOpen(accessor, top.UpCopy(i)))
            {
                return null;
            }
        }
        if (!cast.Can(top) || !cast.Can(top.UpCopy(rise)))
        {
            return null;
        }
        var filler = beneath.BlockMaterial == EnumBlockMaterial.Ore ? RockOf(world, beneath) : beneath;
        if (filler == null)
        {
            return null;
        }
        var plant = accessor.GetBlock(top.UpCopy());
        return new Column { Top = top, Surface = surface, Filler = filler, Plant = plant.Id == 0 ? null : plant };
    }

    /// <summary>The rock an ore block sits in, so a rise never copies ore.</summary>
    private static Block? RockOf(IWorldAccessor world, Block ore)
    {
        string? rock = ore.Variant["rock"];
        return rock == null ? null : world.GetBlock(new AssetLocation("game", "rock-" + rock));
    }

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
    /// the area above that. It moves no blocks, and passes over the caster.
    /// </summary>
    public static bool Gale(CastContext cast)
    {
        bool Pushable(Entity e) => (e is EntityItem || IsCreature(e)) && e.EntityId != cast.Caster?.EntityId;
        var targets = cast.Power < 2 && cast.Entity != null
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
