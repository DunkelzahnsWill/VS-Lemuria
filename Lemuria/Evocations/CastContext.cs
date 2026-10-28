using Lemuria.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;

namespace Lemuria.Evocations;

/// <summary>
/// What a machine changes about a cast (step 4 note "Lemuria step 4: Machines", Focus, Scatter, and
/// Claims, creatures and players): the claims it may act in, the name the audit log gives it, a
/// Scatter's flat square, from what power ripe crops are taken and whether they are replanted, where
/// yields go, and which way a 2×2 reaches.
/// </summary>
public class MachineCast
{
    /// <summary>Whether the machine may change this block, by its own claim rule.</summary>
    public System.Func<BlockPos, bool> Claim { get; init; } = _ => true;

    /// <summary>The name the audit log records for fires and melted rock: the player who carved the machine.</summary>
    public string ActorName { get; init; } = "a machine";

    /// <summary>A Scatter's square, in blocks across, flat and centred on the block aimed at; 0 for a Focus's usual area.</summary>
    public int Square { get; init; }

    /// <summary>The power from which ripe crops, berries and fruit are taken: 3 for a Focus, as by hand, 2 for a Scatter.</summary>
    public int CropPower { get; init; } = 3;

    /// <summary>Whether a taken crop is replanted from its own seed.</summary>
    public bool Replant { get; init; }

    /// <summary>Where yields go, a container touching the machine; null to drop them where the work happened.</summary>
    public System.Func<ItemStack, ItemStack?>? Collect { get; init; }

    /// <summary>Forward and right for a 2×2, when the machine faces up or down (north and east).</summary>
    public (int Fx, int Fz, int Rx, int Rz)? Heading { get; init; }
}

/// <summary>
/// One cast on the server: who casts, at what power, and what the server's own ray from the caster's
/// eyes found (design note, Multiplayer and cost). The caster may be missing for the test command,
/// which casts from a point with nobody's claims to respect. A machine casts too, from its front, with
/// a <see cref="MachineCast"/> (step 4).
/// </summary>
public class CastContext
{
    public ICoreServerAPI Api { get; }
    public IServerPlayer? Player { get; }
    public EntityAgent? Caster { get; }
    public string Alignment { get; }
    public int Power { get; }
    public Vec3d From { get; }
    public Vec3d Dir { get; }

    /// <summary>The machine casting, or null for a hand's cast.</summary>
    public MachineCast? Machine { get; }

    /// <summary>The block the ray hit, unless a creature or a water surface was nearer.</summary>
    public BlockSelection? Block { get; private set; }

    /// <summary>The creature, player or item the ray hit, when nearer than any block.</summary>
    public Entity? Entity { get; private set; }

    /// <summary>The first still water surface or lava along the ray, when nearer than anything else (Quench only).</summary>
    public BlockPos? Water { get; private set; }

    /// <summary>Where the cast lands: the hit point, or the end of the ray on a miss.</summary>
    public Vec3d Point { get; private set; }

    /// <summary>The block the area is centred on, or null on a miss.</summary>
    public BlockPos? Center { get; private set; }

    public int AreaSize => Machine?.Square > 0 ? Machine.Square : Evocation.AreaSize(Power);

    /// <summary>Whether the cast works across an area rather than on what it aims at: from Glowing up, and always in a Scatter.</summary>
    public bool Wide => AreaSize > 1;

    /// <summary>The name the audit log records.</summary>
    public string ActorName => Machine?.ActorName ?? Player?.PlayerName ?? "the test command";

    private CastContext(ICoreServerAPI api, IServerPlayer? player, EntityAgent? caster, string alignment, int power, Vec3d from, Vec3d dir, MachineCast? machine)
    {
        Machine = machine;
        Api = api;
        Player = player;
        Caster = caster;
        Alignment = alignment;
        Power = power;
        From = from;
        Dir = dir;
        Point = from.AddCopy(dir.X * Evocation.Range(power), dir.Y * Evocation.Range(power), dir.Z * Evocation.Range(power));
    }

    /// <summary>Traces the cast's ray from the eyes along the look direction, as far as its range.</summary>
    public static CastContext Aim(ICoreServerAPI api, IServerPlayer? player, EntityAgent? caster, string alignment, int power, Vec3d from, Vec3d dir, MachineCast? machine = null)
    {
        var context = new CastContext(api, player, caster, alignment, power, from, dir.Clone().Normalize(), machine);
        context.Trace();
        return context;
    }

    private void Trace()
    {
        var world = Api.World;
        double range = Evocation.Range(Power);
        BlockSelection? blockSel = null;
        EntitySelection? entitySel = null;
        world.RayTraceForSelection(From, Point, ref blockSel, ref entitySel, null,
            e => e.EntityId != Caster?.EntityId && e.IsInteractable && e.Alive);
        double best = range;
        if (blockSel != null)
        {
            var hit = blockSel.Position.ToVec3d().Add(blockSel.HitPosition);
            best = hit.DistanceTo(From);
            Block = blockSel;
            Point = hit;
            Center = blockSel.Position.Copy();
        }
        if (entitySel?.Entity != null)
        {
            var hit = entitySel.Entity.Pos.XYZ.Add(entitySel.HitPosition);
            double distance = hit.DistanceTo(From);
            if (distance <= best)
            {
                best = distance;
                Entity = entitySel.Entity;
                Block = null;
                Point = hit;
                Center = entitySel.Entity.Pos.AsBlockPos;
            }
        }
        var evocation = Evocation.Of(Alignment);
        bool boils = evocation == Evocation.Ignite && Power >= 3;
        if (evocation != Evocation.Quench && !boils)
        {
            return;
        }
        // Water and lava have no selection box, so Quench, and Ignite strong enough to boil water, walk
        // the ray themselves in tenths of a block.
        var accessor = world.BlockAccessor;
        var pos = new BlockPos(0);
        for (double d = 0.5; d < best; d += 0.1)
        {
            pos.Set((int)Math.Floor(From.X + Dir.X * d), (int)Math.Floor(From.Y + Dir.Y * d), (int)Math.Floor(From.Z + Dir.Z * d));
            if (boils ? IsBoilable(accessor, pos) : IsFreezable(accessor, pos) || accessor.GetBlock(pos, BlockLayersAccess.Fluid).LiquidCode == "lava")
            {
                Water = pos.Copy();
                Block = null;
                Entity = null;
                Point = From.AddCopy(Dir.X * d, Dir.Y * d, Dir.Z * d);
                Center = Water;
                return;
            }
        }
    }

    /// <summary>Water an over-driven Ignite boils away: fresh or salt, still or running.</summary>
    public static bool IsBoilable(IBlockAccessor accessor, BlockPos pos) =>
        accessor.GetBlock(pos, BlockLayersAccess.Fluid).LiquidCode is "water" or "saltwater";

    /// <summary>Still fresh water with open air above it: a surface the game freezes into lake ice.</summary>
    public static bool IsFreezable(IBlockAccessor accessor, BlockPos pos)
    {
        var fluid = accessor.GetBlock(pos, BlockLayersAccess.Fluid);
        if (fluid?.Code == null || fluid.Code.Domain != "game" || fluid.Code.Path != "water-still-7")
        {
            return false;
        }
        var above = pos.UpCopy();
        return accessor.GetBlock(above, BlockLayersAccess.Fluid).Id == 0 && accessor.GetBlock(above).Replaceable >= 6000;
    }

    /// <summary>
    /// Whether the caster may change this block: always, unless it lies in a claim they have no access
    /// to. A machine asks its own claim rule instead.
    /// </summary>
    public bool Can(BlockPos pos) =>
        Machine != null ? Machine.Claim(pos) : Player == null || LemuriaClaims.TestAccess(Api.World, Player, pos, EnumBlockAccessFlags.BuildOrBreak);

    public bool Can(Entity entity) => Can(entity.Pos.AsBlockPos);

    /// <summary>
    /// The area's corners, centred on the impact block: a 3×3×1 carpet at Glowing, level with it, and a
    /// cube when over-driven (the owner, 2026-10-27).
    /// </summary>
    public (BlockPos Min, BlockPos Max)? Bounds()
    {
        if (Center == null)
        {
            return null;
        }
        int r = AreaSize / 2;
        int ry = Machine?.Square > 0 || Evocation.AreaFlat(Power) ? 0 : r;
        return (Center.AddCopy(-r, -ry, -r), Center.AddCopy(r, ry, r));
    }

    /// <summary>
    /// Forward, one block along the dominant horizontal axis of the look (east for a cast straight down
    /// or up), and the caster's right of it, as the hand sits in Caster.Hand.
    /// </summary>
    public (int Fx, int Fz, int Rx, int Rz) Heading()
    {
        if (Machine?.Heading is { } heading)
        {
            return heading;
        }
        bool alongX = Math.Abs(Dir.X) >= Math.Abs(Dir.Z);
        int fx = alongX ? Math.Sign(Dir.X) : 0;
        int fz = alongX ? 0 : Math.Sign(Dir.Z);
        if (fx == 0 && fz == 0)
        {
            fx = 1;
        }
        return (fx, fz, -fz, fx);
    }

    /// <summary>The blocks of the area.</summary>
    public IEnumerable<BlockPos> Area()
    {
        if (Bounds() is not var (min, max))
        {
            yield break;
        }
        for (int x = min.X; x <= max.X; x++)
        {
            for (int y = min.Y; y <= max.Y; y++)
            {
                for (int z = min.Z; z <= max.Z; z++)
                {
                    yield return new BlockPos(x, y, z, min.dimension);
                }
            }
        }
    }

    /// <summary>
    /// The entities in the area: at Glittering, inside a block-wide box round the point the cast lands
    /// on (what lies on the block aimed at); above that, standing in or on the area's blocks.
    /// </summary>
    public List<Entity> EntitiesInArea(System.Func<Entity, bool> match)
    {
        if (Bounds() is not var (min, max))
        {
            return new List<Entity>();
        }
        if (AreaSize <= 1)
        {
            const double half = 0.5;
            return Api.World.GetEntitiesAround(Point, (float)half + 2, (float)half + 2, e => match(e))
                .Where(e => Math.Abs(e.Pos.X - Point.X) <= half && Math.Abs(e.Pos.Z - Point.Z) <= half
                    && e.Pos.Y <= Point.Y + half && e.Pos.Y + e.SelectionBox.Y2 >= Point.Y - half)
                .ToList();
        }
        var middle = new Vec3d((min.X + max.X + 1) / 2.0, (min.Y + max.Y + 1) / 2.0, (min.Z + max.Z + 1) / 2.0);
        float reach = AreaSize + 2;
        return Api.World.GetEntitiesAround(middle, reach, reach, e => match(e))
            .Where(e => e.Pos.X >= min.X && e.Pos.X <= max.X + 1 && e.Pos.Z >= min.Z && e.Pos.Z <= max.Z + 1
                && e.Pos.Y <= max.Y + 1 && e.Pos.Y + e.SelectionBox.Y2 >= min.Y)
            .ToList();
    }
}
