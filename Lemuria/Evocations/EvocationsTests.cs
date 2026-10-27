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
        return Effects.Apply(cast) == Effects.Outcome.Acted;
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
        void Fill()
        {
            for (int x = 0; x < 3; x++)
            {
                for (int z = 0; z < 3; z++)
                {
                    Accessor.SetBlock(0, pond.AddCopy(x, 0, z));
                    Accessor.SetBlock(water?.Id ?? 0, pond.AddCopy(x, 0, z), BlockLayersAccess.Fluid);
                }
            }
        }
        Fill();
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
        // The Glowing carpet is centred on the impact, so aimed at a corner it covers 2×2 of the pond.
        bool cast = CastAt(Crystal.Ice, 2, pond.ToVec3d().Add(0.5, 4, 0.5), pond.ToVec3d().Add(0.5, 0.9, 0.5), out var context);
        int glowing = Frozen();
        // Over-driven and aimed at ice it would pack that instead, so the pond thaws first.
        Fill();
        CastAt(Crystal.Ice, 3, eye, pond.ToVec3d().Add(1.5, 0.9, 1.5), out _);
        logger.Notification("Evocations test: a Glowing Quench aimed at {0} cast {1}; {2} of 9 water blocks froze to lake ice, {3} after an over-driven one on the open pond",
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
        // A field of grassy soil over three blocks of plain soil over granite, open sky above.
        for (int x = -1; x <= 13; x++)
        {
            for (int z = -5; z <= 4; z++)
            {
                var pos = origin.AddCopy(x, 0, z);
                for (int y = -7; y <= -5; y++)
                {
                    Accessor.SetBlock(Id("game:rock-granite"), pos.AddCopy(0, y, 0));
                }
                for (int y = -4; y <= -2; y++)
                {
                    Accessor.SetBlock(Id("game:soil-medium-none"), pos.AddCopy(0, y, 0));
                }
                Accessor.SetBlock(Id("game:soil-medium-normal"), pos.AddCopy(0, -1, 0));
                for (int y = 0; y < 10; y++)
                {
                    Accessor.SetBlock(0, pos.AddCopy(0, y, 0));
                    Accessor.SetBlock(0, pos.AddCopy(0, y, 0), BlockLayersAccess.Fluid);
                }
            }
        }
        var top = origin.AddCopy(0, -1, 0);
        int Solid(int x0, int x1, int z0, int z1)
        {
            int count = 0;
            for (int x = x0; x <= x1; x++)
            {
                for (int z = z0; z <= z1; z++)
                {
                    for (int y = -7; y <= 8; y++)
                    {
                        if (Accessor.GetBlock(top.AddCopy(x, y, z)).Id != 0)
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }
        string Path(int x, int y, int z) => Accessor.GetBlock(top.AddCopy(x, y, z)).Code?.Path ?? "air";
        // Looking east from the west edge: forward is east, right is south.
        var eye = origin.ToVec3d().Add(-0.5, 1.6, 0.5);
        int before = Solid(-1, 13, -5, 4);

        // Glowing first, far off, so the Glittering rise does not stand in its ray: a 2×2 at x 8-9, z 0-1 rises 4.
        var sideHen = Spawn("game:chicken-hen", top.ToVec3d().Add(10.5, 1, 0.5));
        double sideHenBefore = sideHen?.Pos.Y ?? 0;
        bool glowing = CastAt(Crystal.Earth, 2, eye, top.ToVec3d().Add(8.5, 0.95, 0.5), out _);
        int glowingRose = 0, glowingSunk = 0;
        foreach (var (x, z) in new[] { (8, 0), (9, 0), (8, 1), (9, 1) })
        {
            glowingRose += Path(x, 4, z) == "soil-medium-normal" && Path(x, 5, z) == "air" ? 1 : 0;
        }
        foreach (var (x, z) in new[] { (7, 0), (7, 1), (10, 0), (10, 1), (8, -1), (9, -1), (8, 2), (9, 2) })
        {
            glowingSunk += Path(x, -2, z) == "soil-medium-normal" && Path(x, -1, z) == "air" ? 1 : 0;
        }
        double sideHenDrop = sideHen == null ? 0 : sideHenBefore - sideHen.Pos.Y;
        sideHen?.Die(EnumDespawnReason.Removed);

        // Glittering: ore in one rising column, a hen on another; a 2×2 at x 2-3, z 0-1 rises 2, its sides sink 1.
        Accessor.SetBlock(Id("game:ore-poor-nativecopper-granite"), top.AddCopy(3, -1, 0));
        var hen = Spawn("game:chicken-hen", top.ToVec3d().Add(2.5, 1, 0.5));
        double henBefore = hen?.Pos.Y ?? 0;
        bool glittering = CastAt(Crystal.Earth, 1, eye, top.ToVec3d().Add(2.5, 0.95, 0.5), out _);
        int rose = 0, sunk = 0;
        foreach (var (x, z) in new[] { (2, 0), (3, 0), (2, 1), (3, 1) })
        {
            rose += Path(x, 2, z) == "soil-medium-normal" && Path(x, 3, z) == "air" ? 1 : 0;
        }
        foreach (var (x, z) in new[] { (1, 0), (1, 1), (4, 0), (4, 1), (2, -1), (3, -1), (2, 2), (3, 2) })
        {
            sunk += Path(x, -1, z) == "soil-medium-normal" && Path(x, 0, z) == "air" ? 1 : 0;
        }
        bool cornersStay = Path(1, 0, -1) == "soil-medium-normal" && Path(4, 0, 2) == "soil-medium-normal";
        bool oreMoved = Path(3, 1, 0).StartsWith("ore-") && !Path(3, -1, 0).StartsWith("ore-");
        double henRose = hen == null ? 0 : hen.Pos.Y - henBefore;
        hen?.Die(EnumDespawnReason.Removed);
        int after = Solid(-1, 13, -5, 4);

        // A chest beside the rise fails it whole, with its cost spent; aimed at the sky, a free miss.
        Accessor.SetBlock(Id("game:chest-east"), top.AddCopy(7, 1, -3));
        var failEye = origin.ToVec3d().Add(0.5, 1.6, -2.5);
        var failed = Effects.Apply(CastContext.Aim(sapi, null, null, Crystal.Earth, 1, failEye, top.ToVec3d().Add(5.5, 0.95, -2.5).SubCopy(failEye)));
        bool unchanged = Path(5, 0, -3) == "soil-medium-normal" && Path(5, 1, -3) == "air";
        var sky = Effects.Apply(CastContext.Aim(sapi, null, null, Crystal.Earth, 1, eye, new Vec3d(0, 1, 0)));

        logger.Notification("Evocations test: Glowing Tremor cast {0}, {1} of 4 columns rose 4, {2} of 8 sides sank 2, the hen beside it dropped {3:0.#}",
            YesNo(glowing), glowingRose, glowingSunk, sideHenDrop);
        logger.Notification("Evocations test: Glittering Tremor cast {0}, {1} of 4 columns rose 2, {2} of 8 sides sank 1, corners stay: {3}; the ore rose with its column: {4}; the hen rode up {5:0.#}; blocks before {6}, after {7}",
            YesNo(glittering), rose, sunk, YesNo(cornersStay), YesNo(oreMoved), henRose, before, after);
        logger.Notification("Evocations test: Tremor beside a chest {0}, the ground unchanged: {1}; at the sky {2}",
            failed, YesNo(unchanged), sky);
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

    // ------------------------------------------------------------------ reaping, panning and lava

    /// <summary>
    /// A Glowing Gale's carpet over leaves, reeds, mushrooms, crops, bushes and sand, then an over-driven
    /// one; an over-driven Fire melting rock into lava; a Glowing Quench cooling it back to obsidian; an
    /// over-driven Quench packing ice.
    /// </summary>
    public void Reap(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Ground(origin.AddCopy(-1, 0, -2), 9, 5, 9);
        // A 3×3 patch on the granite floor, its middle 3 blocks east: sand in the middle, leaves, crops,
        // a cattail, a mushroom and two ripe berry bushes on soil.
        var mid = origin.AddCopy(3, 0, 2);
        string Path(BlockPos pos) => Accessor.GetBlock(pos).Code?.Path ?? "air";
        void OnSoil(string code, BlockPos pos)
        {
            Accessor.SetBlock(Id("game:soil-medium-none"), pos.DownCopy());
            Accessor.SetBlock(Id(code), pos);
        }
        Accessor.SetBlock(Id("game:sand-granite"), mid);
        Accessor.SetBlock(Id("game:leaves-grown-oak"), mid.AddCopy(-1, 0, 0));
        Accessor.SetBlock(Id("game:leaves-placed-oak"), mid.AddCopy(1, 0, 0));
        Accessor.SetBlock(Id("game:farmland-moist-medium"), mid.AddCopy(0, -1, -1));
        Accessor.SetBlock(Id("game:crop-flax-9"), mid.AddCopy(0, 0, -1));
        Accessor.SetBlock(Id("game:farmland-moist-medium"), mid.AddCopy(0, -1, 1));
        Accessor.SetBlock(Id("game:crop-flax-3"), mid.AddCopy(0, 0, 1));
        OnSoil("game:tallplant-coopersreed-land-normal-free", mid.AddCopy(-1, 0, -1));
        OnSoil("game:mushroom-fieldmushroom-normal", mid.AddCopy(1, 0, -1));
        var bushPos = mid.AddCopy(-1, 0, 1);
        OnSoil("game:fruitingbush-grown-blueberry-free", bushPos);
        if (Accessor.GetBlockEntity(bushPos) == null)
        {
            Accessor.SpawnBlockEntity("Generic", bushPos);
        }
        var bush = Accessor.GetBlockEntity(bushPos)?.GetBehavior<BEBehaviorFruitingBush>();
        if (bush?.BState != null)
        {
            bush.BState.Growthstate = EnumFruitingBushGrowthState.Ripe;
        }
        OnSoil("game:smallberrybush-blueberry-ripe", mid.AddCopy(1, 0, 1));
        var above = mid.ToVec3d().Add(0.5, 4, 0.5);
        var at = mid.ToVec3d().Add(0.5, 0.9, 0.5);
        bool reaped = CastAt(Crystal.Sky, 2, above, at, out _);
        string Drops()
        {
            var drops = sapi.World.GetEntitiesAround(mid.ToVec3d().Add(0.5, 0.5, 0.5), 3, 3, e => e is EntityItem && e.Alive).ToList();
            bool different = drops.Select(d => ((EntityItem)d).Itemstack.Collectible.Code.ToString()).Distinct().Count() == drops.Count;
            foreach (var item in drops)
            {
                item.Die(EnumDespawnReason.Removed);
            }
            return (drops.Count > 0 ? "some" : "no") + " stack(s), all different: " + YesNo(different);
        }
        string reapDrops = Drops();
        logger.Notification("Evocations test: Gale over the patch cast {0}; grown leaves gone: {1}, placed leaves stay: {2}, cattail cut and regrowing: {3}, mushroom picked: {4}, sand panned out: {5}; left for over-driving: ripe flax {6}, ripe bush {7}, ripe legacy bush {8}; drops in {9}",
            YesNo(reaped), YesNo(Path(mid.AddCopy(-1, 0, 0)) == "air"), YesNo(Path(mid.AddCopy(1, 0, 0)).StartsWith("leaves-placed")),
            YesNo(Path(mid.AddCopy(-1, 0, -1)) == "tallplant-coopersreed-land-harvested-free"), YesNo(Path(mid.AddCopy(1, 0, -1)) == "air"),
            YesNo(Path(mid) == "air"), YesNo(Path(mid.AddCopy(0, 0, -1)) == "crop-flax-9"),
            YesNo(bush?.BState?.Growthstate == EnumFruitingBushGrowthState.Ripe), YesNo(Path(mid.AddCopy(1, 0, 1)).EndsWith("-ripe")), reapDrops);
        // Something to stop the ray in the middle again, which Gale leaves alone, so the over-driven
        // cube is centred on the patch, not the floor under it.
        Accessor.SetBlock(Id("game:leaves-placed-oak"), mid);
        bool overdriven = CastAt(Crystal.Sky, 3, above, at, out _);
        logger.Notification("Evocations test: over-driven Gale cast {0}; ripe flax reaped: {1}, young flax stays: {2}, bush picked and standing: {3}, legacy bush picked: {4}; drops in {5}",
            YesNo(overdriven), YesNo(Path(mid.AddCopy(0, 0, -1)) == "air"), YesNo(Path(mid.AddCopy(0, 0, 1)) == "crop-flax-3"),
            YesNo(bush?.BState?.Growthstate == EnumFruitingBushGrowthState.Mature && Path(bushPos).StartsWith("fruitingbush-")),
            YesNo(Path(mid.AddCopy(1, 0, 1)) == "smallberrybush-blueberry-empty"), Drops());

        // Granite to melt, 6 blocks south of the patch; ore beside it stays.
        var rock = origin.AddCopy(3, -1, 5);
        for (int x = -1; x <= 2; x++)
        {
            for (int z = -1; z <= 2; z++)
            {
                Accessor.SetBlock(Id("game:rock-granite"), rock.AddCopy(x, 0, z));
                Accessor.SetBlock(Id("game:rock-granite"), rock.AddCopy(x, -1, z));
            }
        }
        Accessor.SetBlock(Id("game:ore-poor-nativecopper-granite"), rock.AddCopy(0, 0, 1));
        // Cast from the west: forward is east, right is south; the ore sits right of the aimed block.
        var eye = rock.ToVec3d().Add(-2.5, 2.6, 0.5);
        bool melted = CastAt(Crystal.Fire, 3, eye, rock.ToVec3d().Add(0.5, 0.95, 0.5), out _);
        int Lava()
        {
            int n = 0;
            foreach (var (dx, dz) in new[] { (0, 0), (1, 0), (0, 1), (1, 1) })
            {
                n += Accessor.GetBlock(rock.AddCopy(dx, 0, dz), BlockLayersAccess.Fluid).LiquidCode == "lava" ? 1 : 0;
            }
            return n;
        }
        int lava = Lava();
        bool oreStays = Path(rock.AddCopy(0, 0, 1)).StartsWith("ore-");
        bool glowingMelts = CastAt(Crystal.Fire, 2, eye, rock.ToVec3d().Add(-0.5, 0.95, 0.5), out _) && Path(rock.AddCopy(-1, 0, 0)) != "rock-granite";
        bool cooled = CastAt(Crystal.Ice, 2, rock.ToVec3d().Add(1, 4, 1), rock.ToVec3d().Add(1, 0.5, 1), out _);
        int obsidian = 0;
        foreach (var (dx, dz) in new[] { (0, 0), (1, 0), (0, 1), (1, 1) })
        {
            obsidian += Path(rock.AddCopy(dx, 0, dz)) is "rock-obsidian" or "rock-basalt" ? 1 : 0;
        }
        logger.Notification("Evocations test: over-driven Fire at granite cast {0}, {1} of 3 granite blocks melted, the ore stays: {2}; a Glowing one melts rock: {3}; Quench cast {4}, {5} of 3 cooled to rock, lava left {6}",
            YesNo(melted), lava, YesNo(oreStays), YesNo(glowingMelts), YesNo(cooled), obsidian, Lava());

        // Ice to pack, 6 blocks south of the rock, laid into the floor: lake ice, glacier ice, packed ice
        // and lake ice in the 2×2, more lake ice just beyond it.
        Ground(origin.AddCopy(-1, 0, 9), 9, 5, 5);
        var ice = origin.AddCopy(3, -1, 11);
        for (int x = -1; x <= 2; x++)
        {
            for (int z = -1; z <= 2; z++)
            {
                Accessor.SetBlock(Id("game:rock-granite"), ice.AddCopy(x, -1, z));
            }
        }
        void Lake(BlockPos pos)
        {
            Accessor.SetBlock(0, pos);
            Accessor.SetBlock(Id("game:lakeice"), pos, BlockLayersAccess.Fluid);
        }
        Lake(ice);
        Accessor.SetBlock(Id("game:glacierice"), ice.AddCopy(1, 0, 0));
        Accessor.SetBlock(Id("game:packedglacierice"), ice.AddCopy(0, 0, 1));
        Lake(ice.AddCopy(1, 0, 1));
        Lake(ice.AddCopy(2, 0, 0));
        var iceEye = ice.ToVec3d().Add(-2.5, 2.6, 0.5);
        bool packed = CastAt(Crystal.Ice, 3, iceEye, ice.ToVec3d().Add(0.5, 0.95, 0.5), out _);
        logger.Notification("Evocations test: over-driven Quench at lake ice cast {0}; lake to glacier: {1} and {2}, glacier to packed: {3}, packed stays: {4}, beyond the 2×2 stays lake ice: {5}",
            YesNo(packed), YesNo(Path(ice) == "glacierice"), YesNo(Path(ice.AddCopy(1, 0, 1)) == "glacierice"),
            YesNo(Path(ice.AddCopy(1, 0, 0)) == "packedglacierice"), YesNo(Path(ice.AddCopy(0, 0, 1)) == "packedglacierice"),
            YesNo(Accessor.GetBlock(ice.AddCopy(2, 0, 0), BlockLayersAccess.Fluid).Code?.Path == "lakeice"));
    }

    // ------------------------------------------------------------------ melting ice and boiling water

    /// <summary>
    /// Ignite against ice, snow and water (step 3 note rev 178): Glittering melts the lake ice aimed at,
    /// a Glowing carpet melts a pool of it but not glacier ice, and snow; over-driven melts glacier and
    /// packed ice and boils a pool, salt water included, away. A Glowing one boils nothing.
    /// </summary>
    public void Thaw(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Ground(origin.AddCopy(-1, 0, -2), 12, 5, 14);
        string Fluid(BlockPos pos) => Accessor.GetBlock(pos, BlockLayersAccess.Fluid).Code?.Path ?? "air";
        string Solid(BlockPos pos) => Accessor.GetBlock(pos, BlockLayersAccess.Solid).Code?.Path ?? "air";
        // A 3×3 pool let into the floor around a middle block, each cell from fill(dx, dz).
        BlockPos Pool(int x, int z, Action<BlockPos, int, int> fill)
        {
            var mid = origin.AddCopy(x, -1, z);
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dz = -1; dz <= 1; dz++)
                {
                    var pos = mid.AddCopy(dx, 0, dz);
                    Accessor.SetBlock(Id("game:rock-granite"), pos.DownCopy());
                    Accessor.SetBlock(0, pos);
                    Accessor.SetBlock(0, pos, BlockLayersAccess.Fluid);
                    fill(pos, dx, dz);
                }
            }
            return mid;
        }
        int Count(BlockPos mid, System.Func<BlockPos, bool> match)
        {
            int n = 0;
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dz = -1; dz <= 1; dz++)
                {
                    n += match(mid.AddCopy(dx, 0, dz)) ? 1 : 0;
                }
            }
            return n;
        }
        bool CastDown(int power, BlockPos at) =>
            CastAt(Crystal.Fire, power, at.ToVec3d().Add(0.5, 4, 0.5), at.ToVec3d().Add(0.5, 0.9, 0.5), out _);
        void Lake(BlockPos pos, int dx, int dz) => Accessor.SetBlock(Id("game:lakeice"), pos, BlockLayersAccess.Fluid);

        var a = Pool(2, 1, Lake);
        bool glittering = CastDown(1, a);
        var b = Pool(7, 1, (pos, dx, dz) =>
        {
            if (dx == 1 && dz == 1)
            {
                Accessor.SetBlock(Id("game:glacierice"), pos);
            }
            else
            {
                Lake(pos, dx, dz);
            }
        });
        bool glowing = CastDown(2, b);
        var snow = origin.AddCopy(6, 0, 4);
        Accessor.SetBlock(Id("game:snowlayer-1"), snow);
        Accessor.SetBlock(Id("game:snowblock"), snow.AddCopy(1, 0, 0));
        bool snowCast = CastDown(2, snow);
        logger.Notification("Evocations test: Glittering Fire at lake ice cast {0}, {1} of 9 melted to water; Glowing cast {2}, {3} of 9 melted, glacier ice stays: {4}; snow cast {5}, snow gone: {6}",
            YesNo(glittering), Count(a, p => Fluid(p) == "water-still-7"), YesNo(glowing), Count(b, p => Fluid(p) == "water-still-7"),
            YesNo(Solid(b.AddCopy(1, 0, 1)) == "glacierice"), YesNo(snowCast), YesNo(Solid(snow) == "air" && Solid(snow.AddCopy(1, 0, 0)) == "air"));

        var c = Pool(2, 7, (pos, dx, dz) => Accessor.SetBlock(Id((dx + dz) % 2 == 0 ? "game:glacierice" : "game:packedglacierice"), pos));
        bool overdriven = CastDown(3, c);
        var water = Id("game:water-still-7");
        var d = Pool(7, 7, (pos, dx, dz) => Accessor.SetBlock(dx == 1 && dz == 1 ? Id("game:saltwater-still-7") : water, pos, BlockLayersAccess.Fluid));
        bool glowingBoils = CastDown(2, d) && Count(d, p => Fluid(p) == "air") > 0;
        bool boiled = CastDown(3, d);
        logger.Notification("Evocations test: over-driven Fire at glacier ice cast {0}, {1} of 9 melted to water; a Glowing one boils water: {2}; over-driven at the pool cast {3}, {4} of 9 boiled away, salt water too: {5}",
            YesNo(overdriven), Count(c, p => Fluid(p) == "water-still-7" && Solid(p) == "air"), YesNo(glowingBoils), YesNo(boiled),
            Count(d, p => Fluid(p) == "air"), YesNo(Fluid(d.AddCopy(1, 0, 1)) == "air"));
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
