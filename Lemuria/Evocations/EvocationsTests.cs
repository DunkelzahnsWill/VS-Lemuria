using System;
using System.Linq;
using Lemuria.Core;
using Lemuria.Crystals;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;
using Vintagestory.GameContent;

namespace Lemuria.Evocations;

/// <summary>
/// The scenes behind <c>/lemuria evocations test ...</c> (design note, How it is built: Smoke tests).
/// Each casts from a point beside its scene through the same ray and effects a player's cast uses,
/// with no player, so no claim applies, and reports to the log.
/// </summary>
public class EvocationsTests
{
    private readonly EvocationsModSystem system;
    private readonly ICoreServerAPI sapi;
    private readonly ILogger logger;

    public EvocationsTests(EvocationsModSystem system, ICoreServerAPI sapi, ILogger logger)
    {
        this.system = system;
        this.sapi = sapi;
        this.logger = logger;
    }

    private IBlockAccessor Accessor => sapi.World.BlockAccessor;

    private int Id(string code) => sapi.World.GetBlock(new AssetLocation(code))?.Id ?? 0;

    /// <summary>Clears a box of air above a granite floor.</summary>
    private void Ground(BlockPos origin, int sizeX, int sizeY, int sizeZ, string floor = "game:rock-granite")
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                Accessor.SetBlock(Id(floor), origin.AddCopy(x, -1, z));
                for (int y = 0; y < sizeY; y++)
                {
                    Accessor.SetBlock(0, origin.AddCopy(x, y, z));
                    Accessor.SetBlock(0, origin.AddCopy(x, y, z), BlockLayersAccess.Fluid);
                }
            }
        }
    }

    /// <summary>Casts from a point at a target point, as a player standing there would.</summary>
    private bool CastAt(string alignment, int power, Vec3d from, Vec3d at, out CastContext cast)
    {
        cast = CastContext.Aim(sapi, null, null, alignment, power, from, at.SubCopy(from));
        return Effects.Apply(cast);
    }

    private Entity? Spawn(string code, Vec3d at)
    {
        var type = sapi.World.GetEntityType(new AssetLocation(code));
        if (type == null)
        {
            logger.Error("Evocations test: no entity type {0}", code);
            return null;
        }
        var entity = sapi.ClassRegistry.CreateEntity(type);
        entity.Pos.SetPos(at);
        sapi.World.SpawnEntity(entity);
        return entity;
    }

    private static string YesNo(bool value) => value ? "yes" : "no";

    // ------------------------------------------------------------------ Ignite and Quench

    public void Fire(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Ground(origin.AddCopy(-1, 0, -2), 11, 4, 5);
        var firepitPos = origin.AddCopy(7, 0, 0);
        Accessor.SetBlock(Id("game:firepit-cold"), firepitPos);
        if (Accessor.GetBlockEntity(firepitPos) is not BlockEntityFirepit firepit)
        {
            logger.Error("Evocations test: no firepit");
            return;
        }
        firepit.Inventory[0].Itemstack = new ItemStack(sapi.World.GetItem(new AssetLocation("game:firewood")), 4);
        firepit.Inventory[0].MarkDirty();
        var eye = origin.ToVec3d().Add(1.5, 1.6, 0.5);
        bool lit = CastAt(Crystal.Fire, 1, eye, firepitPos.ToVec3d().Add(0.5, 0.2, 0.5), out var cast);
        string aimed = cast.Block?.Position.Equals(firepitPos) == true ? "the firepit" : "something else";
        var hen = Spawn("game:chicken-hen", origin.ToVec3d().Add(4.5, 0, 1.5));
        bool henAlight = false;
        if (hen != null)
        {
            CastAt(Crystal.Fire, 2, eye, origin.ToVec3d().Add(4.5, 0, 1.5), out _);
            henAlight = hen.IsOnFire;
        }
        sapi.Event.RegisterCallback(dt =>
        {
            bool burning = firepit.IsBurning;
            int fuel = firepit.Inventory[0].StackSize;
            logger.Notification("Evocations test: Ignite aimed at {0} 6 blocks away, cast {1}; the firepit burns: {2}; the hen is alight: {3}",
                aimed, YesNo(lit), YesNo(burning), YesNo(henAlight));
            bool quenched = CastAt(Crystal.Ice, 1, eye, firepitPos.ToVec3d().Add(0.5, 0.2, 0.5), out _);
            bool henOut = true;
            if (hen != null)
            {
                CastAt(Crystal.Ice, 2, eye, hen.Pos.XYZ, out _);
                henOut = !hen.IsOnFire;
                hen.Die(EnumDespawnReason.Removed);
            }
            logger.Notification("Evocations test: Quench cast {0}; the firepit burns: {1}, fuel kept: {2}; the hen is out: {3}",
                YesNo(quenched), YesNo(firepit.IsBurning), YesNo(firepit.Inventory[0].StackSize >= Math.Max(1, fuel - 1)), YesNo(henOut));
            bool fizzled = !CastAt(Crystal.Fire, 1, eye, eye.AddCopy(0, 30, 0), out _);
            logger.Notification("Evocations test: a cast at the open sky fizzles: {0}", YesNo(fizzled));
        }, 2000);
    }

    public void Water(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Ground(origin.AddCopy(-1, 0, -1), 7, 4, 5);
        // A granite basin with a 3×3 pond of still fresh water, its surface level with the ground.
        var pond = origin.AddCopy(2, -1, 0);
        for (int x = -1; x <= 3; x++)
        {
            for (int z = -1; z <= 3; z++)
            {
                Accessor.SetBlock(Id("game:rock-granite"), pond.AddCopy(x, -1, z));
                Accessor.SetBlock(Id("game:rock-granite"), pond.AddCopy(x, 0, z));
            }
        }
        var water = sapi.World.GetBlock(new AssetLocation("game:water-still-7"));
        for (int x = 0; x < 3; x++)
        {
            for (int z = 0; z < 3; z++)
            {
                Accessor.SetBlock(0, pond.AddCopy(x, 0, z));
                Accessor.SetBlock(water?.Id ?? 0, pond.AddCopy(x, 0, z), BlockLayersAccess.Fluid);
            }
        }
        // Cast from straight above the pond's middle, so the ray meets that block's water first.
        var eye = pond.ToVec3d().Add(1.5, 4, 1.5);
        int Frozen()
        {
            int ice = 0;
            for (int x = 0; x < 3; x++)
            {
                for (int z = 0; z < 3; z++)
                {
                    if (Accessor.GetBlock(pond.AddCopy(x, 0, z), BlockLayersAccess.Fluid).Code?.Path == "lakeice")
                    {
                        ice++;
                    }
                }
            }
            return ice;
        }
        // A cast straight down reaches east and south of the impact, so the Glowing one aims at the north-west corner.
        bool cast = CastAt(Crystal.Ice, 2, pond.ToVec3d().Add(0.5, 4, 0.5), pond.ToVec3d().Add(0.5, 0.9, 0.5), out var context);
        int glowing = Frozen();
        CastAt(Crystal.Ice, 3, eye, pond.ToVec3d().Add(1.5, 0.9, 1.5), out _);
        logger.Notification("Evocations test: a Glowing Quench aimed at {0} cast {1}; {2} of 9 water blocks froze to lake ice, {3} after an over-driven one",
            context.Water != null ? "the water" : "something else", YesNo(cast), glowing, Frozen());

        // A 3×3 field of dry farmland, out of the pond's reach, with flax sprouting at one corner.
        Ground(origin.AddCopy(-1, 0, 6), 7, 4, 5);
        var field = origin.AddCopy(2, -1, 7);
        for (int x = 0; x < 3; x++)
        {
            for (int z = 0; z < 3; z++)
            {
                Accessor.SetBlock(Id("game:farmland-dry-medium"), field.AddCopy(x, 0, z));
            }
        }
        Accessor.SetBlock(Id("game:crop-flax-1"), field.AddCopy(0, 1, 0));
        bool Soaked(BlockPos pos) => Accessor.GetBlockEntity(pos) is IFarmlandBlockEntity farmland && farmland.MoistureLevel >= 0.99f;
        bool watered = CastAt(Crystal.Ice, 1, origin.ToVec3d().Add(0.5, 1.6, 7.5), field.ToVec3d().Add(0.5, 1.1, 0.5), out _);
        bool cropSoaked = Soaked(field);
        bool wateredAll = CastAt(Crystal.Ice, 3, field.ToVec3d().Add(1.5, 4, 1.5), field.ToVec3d().Add(1.5, 0.9, 1.5), out _);
        int soaked = 0;
        for (int x = 0; x < 3; x++)
        {
            for (int z = 0; z < 3; z++)
            {
                if (Soaked(field.AddCopy(x, 0, z)))
                {
                    soaked++;
                }
            }
        }
        logger.Notification("Evocations test: Quench aimed at a crop cast {0}; its farmland soaked: {1}; an over-driven Quench soaked {2} of 9 farmland",
            YesNo(watered), YesNo(cropSoaked), wateredAll ? soaked : 0);
    }

    // ------------------------------------------------------------------ Tremor

    public void Tremor(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        int rise = Evocation.TremorRise;
        // A strip of grassy soil over plain soil over granite, 12 blocks long and 5 wide.
        for (int x = -1; x < 11; x++)
        {
            for (int z = -2; z <= 2; z++)
            {
                var pos = origin.AddCopy(x, 0, z);
                Accessor.SetBlock(Id("game:rock-granite"), pos.AddCopy(0, -3, 0));
                Accessor.SetBlock(Id("game:soil-medium-none"), pos.AddCopy(0, -2, 0));
                Accessor.SetBlock(Id("game:soil-medium-normal"), pos.AddCopy(0, -1, 0));
                for (int y = 0; y < rise + 4; y++)
                {
                    Accessor.SetBlock(0, pos.AddCopy(0, y, 0));
                }
            }
        }
        var top = origin.AddCopy(0, -1, 0);
        // Ore under one column of the mound, and a chest on another.
        Accessor.SetBlock(Id("game:ore-poor-nativecopper-granite"), top.AddCopy(7, -1, 0));
        Accessor.SetBlock(Id("game:chest-east"), top.AddCopy(6, 1, 1));
        var eye = origin.ToVec3d().Add(-0.5, 1.6, 0.5);
        // The mound first, so the pillar does not stand in the way of its ray.
        var hen = Spawn("game:chicken-hen", top.ToVec3d().Add(6.5, 1, 0.5));
        double henBefore = hen?.Pos.Y ?? 0;
        bool mound = CastAt(Crystal.Earth, 2, eye, top.ToVec3d().Add(6.5, 0.95, 0.5), out _);
        bool pillar = CastAt(Crystal.Earth, 1, eye, top.ToVec3d().Add(2.5, 0.95, 0.5), out _);
        bool pillarRose = Accessor.GetBlock(top.AddCopy(2, rise, 0)).Code?.Path == "soil-medium-normal"
            && Accessor.GetBlock(top.AddCopy(2, 0, 0)).Code?.Path == "soil-medium-none"
            && Accessor.GetBlock(top.AddCopy(3, rise, 0)).Id == 0;
        int risen = 0;
        for (int dx = 5; dx <= 7; dx++)
        {
            for (int dz = -1; dz <= 1; dz++)
            {
                if (Accessor.GetBlock(top.AddCopy(dx, rise, dz)).Code?.Path == "soil-medium-normal")
                {
                    risen++;
                }
            }
        }
        string filler = Accessor.GetBlock(top.AddCopy(7, rise - 1, 0)).Code?.Path ?? "air";
        string underOre = Accessor.GetBlock(top.AddCopy(7, -1, 0)).Code?.Path ?? "air";
        double henRose = hen == null ? 0 : hen.Pos.Y - henBefore;
        hen?.Die(EnumDespawnReason.Removed);
        logger.Notification("Evocations test: Tremor pillar cast {0}, rose {1}; mound cast {2}, {3} of 9 columns rose; over ore the rise is {4} and the ore is still {5}; the hen rode up {6:0.#} blocks",
            YesNo(pillar), YesNo(pillarRose), YesNo(mound), risen, filler, underOre, henRose);
    }

    // ------------------------------------------------------------------ Gale

    public void Gale(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Ground(origin.AddCopy(-1, 0, -2), 12, 4, 5);
        var eye = origin.ToVec3d().Add(0.5, 1.6, 0.5);
        var stick = sapi.World.SpawnItemEntity(new ItemStack(sapi.World.GetItem(new AssetLocation("game:stick"))), origin.ToVec3d().Add(4.5, 0.1, 0.5), new Vec3d());
        if (stick == null)
        {
            logger.Error("Evocations test: the stick did not spawn");
            return;
        }
        bool pushed = CastAt(Crystal.Sky, 1, eye, origin.ToVec3d().Add(4.5, 0, 0.5), out _);
        double itemSpeed = stick.Pos.Motion.X;
        var hen = Spawn("game:chicken-hen", origin.ToVec3d().Add(6.5, 0, 0.5));
        bool pushedHen = false;
        double henPush = 0;
        if (hen != null)
        {
            pushedHen = CastAt(Crystal.Sky, 2, eye, hen.Pos.XYZ.Add(0, 0.3, 0), out _);
            henPush = hen.WatchedAttributes.GetDouble("kbdirX");
            hen.Die(EnumDespawnReason.Removed);
        }
        stick.Die(EnumDespawnReason.Removed);
        logger.Notification("Evocations test: Gale cast at the item {0}, pushed away at {1:0.###}; at the hen {2}, pushed away at {3:0.###}",
            YesNo(pushed), itemSpeed, YesNo(pushedHen), henPush);
    }

    // ------------------------------------------------------------------ casting rules

    public void Overdrive()
    {
        var glowing = Crystal.BlockFor(sapi.World, Crystal.Fire, Crystal.Glowing);
        var glittering = Crystal.BlockFor(sapi.World, Crystal.Sky, Crystal.Glittering);
        if (glowing == null || glittering == null)
        {
            logger.Error("Evocations test: crystals missing");
            return;
        }
        ItemStack Full(Block block, int size = 1)
        {
            var stack = new ItemStack(block, size);
            Crystal.SetState(stack, Crystal.Capacity(Crystal.EvolutionOf(block)), 0);
            return stack;
        }
        var full = Full(glowing);
        var low = new ItemStack(glowing);
        Crystal.SetState(low, 30, 0);
        var empty = new ItemStack(glowing);
        logger.Notification("Evocations test: let go at 3 s {0}, 6 s {1}, 16 s {2}; 16 s with 30 charge {3}; empty {4}",
            Caster.Plan(full, 3), Caster.Plan(full, 6), Caster.Plan(full, 16), Caster.Plan(low, 16), Caster.Plan(empty, 6));

        var where = sapi.World.DefaultSpawnPosition.XYZ;
        var slot = new DummySlot(Full(glowing));
        var a = Caster.Spend(sapi.World, slot, null, where, false, 0);
        var b = Caster.Spend(sapi.World, slot, null, where, true, 0.9);
        var c = Caster.Spend(sapi.World, slot, null, where, true, 0);
        logger.Notification("Evocations test: a full Glowing crystal after a cast holds {0:0}, after an over-driven cast {1:0} with {2} crack(s), after a cracking one {3:0} with {4} crack(s)",
            a.Charge, b.Charge, b.Cracks, c.Charge, c.Cracks);

        var cracked = Full(glowing);
        Crystal.SetState(cracked, 200, Math.Max(1, LemuriaModSystem.Config.CracksToShatter) - 1);
        var cracking = new DummySlot(cracked);
        var d = Caster.Spend(sapi.World, cracking, null, where, true, 0);
        logger.Notification("Evocations test: its last crack shatters it: {0}, hand empty: {1}", YesNo(d.Shattered), YesNo(cracking.Empty));

        var stackSlot = new DummySlot(Full(glittering, 3));
        var e = Caster.Spend(sapi.World, stackSlot, null, where, false, 0);
        logger.Notification("Evocations test: casting from a stack of 3 full crystals leaves {0} full in hand, and one holding {1:0} leaves the stack: {2}",
            stackSlot.StackSize, e.Charge, YesNo(e.LeftStack && stackSlot.Itemstack != null && Crystal.GetCharge(stackSlot.Itemstack) >= 100));
        // The crystal that left the stack was dropped at the spawn; pick it up again so the world stays clean.
        foreach (var item in sapi.World.GetEntitiesAround(where, 2, 2, x => x is EntityItem))
        {
            item.Die(EnumDespawnReason.Removed);
        }
    }
}
