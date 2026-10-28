using System.Collections.Generic;
using System.Linq;
using Lemuria.Core;
using Lemuria.Cradle;
using Lemuria.Crystals;
using Lemuria.Sparkstone;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;
using Vintagestory.GameContent;
using Vintagestory.GameContent.Mechanics;

namespace Lemuria.Machines;

/// <summary>
/// The scenes behind <c>/lemuria machines test ...</c> (design note "Lemuria step 4: Machines", How it is
/// built: Smoke tests). Each builds its machines from Awakened granite with no player, so no claim
/// applies, fills their network, makes them work once and reports to the log.
/// </summary>
public class MachinesTests
{
    private const string Rock = "granite";
    private const string NetworkKey = LemuriaModSystem.ModId + "-machinestest-network";
    private const string MotorKey = LemuriaModSystem.ModId + "-machinestest-motor";

    private readonly ICoreServerAPI sapi;
    private readonly ILogger logger;

    public MachinesTests(ICoreServerAPI sapi, ILogger logger)
    {
        this.sapi = sapi;
        this.logger = logger;
    }

    private IWorldAccessor World => sapi.World;

    private IBlockAccessor Accessor => sapi.World.BlockAccessor;

    private int Id(string code) => World.GetBlock(new AssetLocation(code))?.Id ?? 0;

    private string Path(BlockPos pos) => Accessor.GetBlock(pos).Code?.Path ?? "?";

    private static string YesNo(bool value) => value ? "yes" : "no";

    /// <summary>Clears a box of air above a floor.</summary>
    private void Ground(BlockPos origin, int sizeX, int sizeY, int sizeZ, string floor = "game:rock-granite")
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                Accessor.SetBlock(Id("game:rock-granite"), origin.AddCopy(x, -2, z));
                Accessor.SetBlock(Id(floor), origin.AddCopy(x, -1, z));
                for (int y = 0; y < sizeY; y++)
                {
                    Accessor.SetBlock(0, origin.AddCopy(x, y, z));
                    Accessor.SetBlock(0, origin.AddCopy(x, y, z), BlockLayersAccess.Fluid);
                }
            }
        }
    }

    private BlockEntitySparkstone? Free(BlockPos pos)
    {
        Accessor.SetBlock(World.GetBlock(Machine.FreeCode(Rock))?.Id ?? 0, pos);
        return Accessor.GetBlockEntity(pos) as BlockEntitySparkstone;
    }

    private BlockEntityMachine? Place(BlockPos pos, string form, string side)
    {
        var block = World.GetBlock(Machine.Code(form, Rock, side));
        if (block == null)
        {
            logger.Error("Machines test: no block {0}", Machine.Code(form, Rock, side));
            return null;
        }
        Accessor.SetBlock(block.Id, pos);
        if (block is BlockCrystalMotor motor)
        {
            motor.WasPlaced(World, pos, null);
        }
        return Accessor.GetBlockEntity(pos) as BlockEntityMachine;
    }

    private static ItemStack? CrystalStack(IWorldAccessor world, string alignment, string evolution, float charge = 0)
    {
        var block = Crystal.BlockFor(world, alignment, evolution);
        if (block == null)
        {
            return null;
        }
        var stack = new ItemStack(block);
        Crystal.SetState(stack, charge, 0);
        return stack;
    }

    private bool Put(BlockEntitySparkstone? stone, string alignment, string evolution, float charge = 0) =>
        stone != null && CrystalStack(World, alignment, evolution, charge) is { } stack && stone.Insert(stack, stone.SocketFacing ?? BlockFacing.UP);

    private static void Fill(BlockEntitySparkstone? stone)
    {
        if (stone != null)
        {
            SparkstoneModSystem.Networks?.Fill(stone);
        }
    }

    private static float Total(BlockEntitySparkstone stone) =>
        SparkstoneModSystem.Networks is { } networks ? SparkstoneNetworks.Measure(networks.NetworkOf(stone)).Total : 0;

    private static float Capacity(BlockEntitySparkstone stone) =>
        SparkstoneModSystem.Networks is { } networks ? SparkstoneNetworks.Measure(networks.NetworkOf(stone)).Capacity : 0;

    private bool Soaked(BlockPos pos) => Accessor.GetBlockEntity(pos) is IFarmlandBlockEntity farmland && farmland.MoistureLevel >= 0.99f;

    // ------------------------------------------------------------------ carving and the rod

    public void Carve(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Ground(origin, 3, 3, 3);
        var pos = origin.AddCopy(1, 0, 1);
        if (Free(pos) == null)
        {
            logger.Error("Machines test: no Awakened granite");
            return;
        }
        var forms = new List<string>();
        for (int i = 0; i < 7; i++)
        {
            var next = MachineCarver.NextBlock(World, Accessor.GetBlock(pos), BlockFacing.EAST, out _);
            if (next == null || !MachineCarver.Carve(World, null, pos, next))
            {
                forms.Add("none");
                break;
            }
            var block = Accessor.GetBlock(pos);
            forms.Add(Machine.IsMachine(block) ? $"{block.Variant["form"]}-{block.Variant["side"]}" : "free");
        }
        // Struck once more, it is a Gather block again; another face does nothing.
        MachineCarver.Carve(World, null, pos, MachineCarver.NextBlock(World, Accessor.GetBlock(pos), BlockFacing.EAST, out _)!);
        bool otherFace = MachineCarver.NextBlock(World, Accessor.GetBlock(pos), BlockFacing.UP, out bool refused) == null && refused;
        var machine = Accessor.GetBlockEntity(pos) as BlockEntityMachine;
        Put(machine, Crystal.Fire, Crystal.Glittering);
        logger.Notification("Machines test: strikes carve {0}; another face does nothing: {1}; the Gather block holds {2} crystal(s) on its front",
            string.Join(", ", forms), YesNo(otherFace), machine?.Crystals.Count ?? 0);
        // The rod turns the machine's front, crystals and all, and switches it.
        if (machine != null)
        {
            ItemCrystalRod.Tap(World, machine, new BlockSelection { Position = pos, Face = BlockFacing.UP, HitPosition = new Vec3d(0.5, 1, 0.5) }, false);
            var turned = Accessor.GetBlockEntity(pos) as BlockEntityMachine;
            string side = Accessor.GetBlock(pos).Variant["side"] ?? "?";
            if (turned != null)
            {
                ItemCrystalRod.Tap(World, turned, new BlockSelection { Position = pos, Face = BlockFacing.UP, HitPosition = new Vec3d(0.5, 1, 0.5) }, true);
            }
            logger.Notification("Machines test: the rod turned the front {0} with {1} crystal(s); a sneaking tap switches it Dormant: {2}",
                side, turned?.Crystals.Count ?? 0, YesNo(turned?.Active == false));
        }
        // The rod from an Unaligned crystal and a polished rock.
        var crystalPos = origin.AddCopy(0, 0, 0);
        Accessor.SetBlock(Crystal.BlockFor(World, Crystal.Unaligned, Crystal.Glittering)?.Id ?? 0, crystalPos);
        var rockStack = new ItemStack(World.GetBlock(new AssetLocation("game:rockpolished-granite")), 2);
        var rockSlot = new DummySlot(rockStack);
        bool made = BlockBehaviorRodMaker.Make(World, null, crystalPos, rockSlot);
        logger.Notification("Machines test: the Crystal Rod made: {0}, the crystal used up: {1}, rock left {2}",
            YesNo(made), YesNo(Accessor.GetBlock(crystalPos).Id == 0), rockSlot.StackSize);
    }

    // ------------------------------------------------------------------ Focus

    public void Focus(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Ground(origin, 12, 9, 6);
        // Fire: a Focus facing east at a firepit four blocks away.
        var firepitPos = origin.AddCopy(5, 0, 1);
        Accessor.SetBlock(Id("game:firepit-cold"), firepitPos);
        if (Accessor.GetBlockEntity(firepitPos) is BlockEntityFirepit firepit)
        {
            firepit.Inventory[0].Itemstack = new ItemStack(World.GetItem(new AssetLocation("game:firewood")), 4);
            firepit.Inventory[0].MarkDirty();
        }
        var fire = Place(origin.AddCopy(1, 0, 1), Machine.Focus, "east");
        Put(fire, Crystal.Fire, Crystal.Glittering);
        Fill(fire);
        float before = fire == null ? 0 : Total(fire);
        var fireResult = fire?.WorkOnce();
        float paid = fire == null ? 0 : before - Total(fire);
        // Ice: a Focus over young flax on dry farmland, facing down.
        var field = origin.AddCopy(1, 0, 4);
        Accessor.SetBlock(Id("game:farmland-dry-medium"), field);
        // Tilled as a hoe tills it, so the farmland counts its drying from now.
        if (Accessor.GetBlockEntity(field) is BlockEntityFarmland tilled)
        {
            tilled.OnCreatedFromSoil(World.GetBlock(new AssetLocation("game:soil-medium-none")), null);
            // Dry, whatever rain the smoke world's clock has brought.
            var tree = new Vintagestory.API.Datastructures.TreeAttribute();
            tilled.ToTreeAttributes(tree);
            tree.SetFloat("moistureLevel", 0);
            tilled.FromTreeAttributes(tree, World);
        }
        Accessor.SetBlock(Id("game:crop-flax-1"), field.UpCopy());
        var ice = Place(field.AddCopy(0, 4, 0), Machine.Focus, "down");
        Put(ice, Crystal.Ice, Crystal.Glittering);
        Fill(ice);
        var iceResult = ice?.WorkOnce();
        bool soaked = Soaked(field);
        // Earth: a Focus six blocks over the granite floor, facing down.
        var ground = origin.AddCopy(8, -1, 2);
        var earth = Place(ground.AddCopy(0, 7, 0), Machine.Focus, "down");
        Put(earth, Crystal.Earth, Crystal.Glittering);
        Fill(earth);
        var earthResult = earth?.WorkOnce();
        int rose = new[] { (0, 0), (1, 0), (0, -1), (1, -1) }.Count(d => Path(ground.AddCopy(d.Item1, 2, d.Item2)).StartsWith("rock-"));
        bool dormant = earth?.Active == false;
        var again = earth?.WorkOnce();
        sapi.Event.RegisterCallback(_ =>
        {
            bool burning = Accessor.GetBlockEntity(firepitPos) is BlockEntityFirepit { IsBurning: true };
            logger.Notification("Machines test: a Fire Focus {0}, paid {1:0.0}, the firepit burns: {2}; an Ice Focus {3}, the farmland under the flax soaked: {4}",
                fireResult, paid, YesNo(burning), iceResult, YesNo(soaked));
            logger.Notification("Machines test: an Earth Focus {0}, {1} of 4 columns rose 2 to the north and east, Dormant: {2}, then {3}",
                earthResult, rose, YesNo(dormant), again);
        }, 2000);
    }

    // ------------------------------------------------------------------ Amplify and harvest

    public void Harvest(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Ground(origin, 5, 6, 5);
        var field = origin.AddCopy(1, -1, 1);
        for (int x = 0; x < 3; x++)
        {
            for (int z = 0; z < 3; z++)
            {
                Accessor.SetBlock(Id("game:farmland-moist-medium"), field.AddCopy(x, 0, z));
                Accessor.SetBlock(Id("game:crop-flax-9"), field.AddCopy(x, 1, z));
            }
        }
        var focusPos = field.AddCopy(1, 4, 1);
        var focus = Place(focusPos, Machine.Focus, "down");
        var amplify = Place(focusPos.EastCopy(), Machine.Amplify, "up");
        var chestPos = focusPos.WestCopy();
        Accessor.SetBlock(Id("game:chest-east"), chestPos);
        Put(focus, Crystal.Sky, Crystal.Glowing);
        Put(amplify, Crystal.Fire, Crystal.Glowing);
        Fill(focus);
        focus?.ReadContext();
        int amp = focus?.AmplifyRank() ?? 0;
        var result = focus?.WorkOnce();
        int replanted = 0;
        for (int x = 0; x < 3; x++)
        {
            for (int z = 0; z < 3; z++)
            {
                if (Path(field.AddCopy(x, 1, z)) == "crop-flax-1")
                {
                    replanted++;
                }
            }
        }
        int stored = Accessor.GetBlockEntity(chestPos) is BlockEntityGenericTypedContainer chest ? chest.Inventory.Sum(s => s.StackSize) : -1;
        logger.Notification("Machines test: a Glowing Sky Focus amplified to {0} {1}; {2} of 9 ripe flax replanted, {3} item(s) in the chest",
            amp, result, replanted, stored > 0 ? "some" : stored.ToString());
    }

    // ------------------------------------------------------------------ Scatter

    public void Scatter(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Ground(origin, 11, 7, 11, "game:soil-medium-none");
        var center = origin.AddCopy(5, -1, 5);
        var scatter = Place(center.AddCopy(0, 5, 0), Machine.Scatter, "down");
        Put(scatter, Crystal.Unaligned, Crystal.Glittering);
        var idle = scatter?.WorkOnce();
        scatter?.TakeOut(scatter.Crystals[0]);
        Put(scatter, Crystal.Earth, Crystal.Glowing);
        Fill(scatter);
        var tilled = scatter?.WorkOnce();
        int farmland = 0;
        for (int x = -4; x <= 4; x++)
        {
            for (int z = -4; z <= 4; z++)
            {
                if (Path(center.AddCopy(x, 0, z)).StartsWith("farmland-"))
                {
                    farmland++;
                }
            }
        }
        if (scatter != null)
        {
            scatter.TakeOut(scatter.Crystals[0]);
        }
        Put(scatter, Crystal.Ice, Crystal.Glowing);
        Fill(scatter);
        var watered = scatter?.WorkOnce();
        int soaked = 0;
        for (int x = -4; x <= 4; x++)
        {
            for (int z = -4; z <= 4; z++)
            {
                if (Soaked(center.AddCopy(x, 0, z)))
                {
                    soaked++;
                }
            }
        }
        logger.Notification("Machines test: a Scatter with a Glittering crystal {0}; Glowing Earth {1}, {2} of 81 tilled; Glowing Ice {3}, {4} of 81 soaked",
            idle, tilled, farmland, watered, soaked);
    }

    // ------------------------------------------------------------------ capacitors, Dormant and refining progress

    public void Network(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Ground(origin, 6, 2, 3);
        var line = Enumerable.Range(0, 3).Select(i => Free(origin.AddCopy(i, 0, 1))).ToList();
        if (line.Any(s => s == null) || SparkstoneModSystem.Networks is not { } networks)
        {
            logger.Error("Machines test: no Awakened granite or networks");
            return;
        }
        var first = line[0]!;
        float before = Capacity(first);
        Put(first, Crystal.Fire, Crystal.Glittering, 100);
        float after = Capacity(first);
        float charged = Total(first);
        Fill(first);
        var taken = first.TakeOut(first.Crystals[0]);
        float full = Crystal.GetCharge(taken);
        // Set it in again to stay for the restart check, and switch the middle block Dormant.
        first.Insert(taken, BlockFacing.UP);
        line[1]!.SetActive(false);
        int left = networks.NetworkOf(first).Count;
        int right = networks.NetworkOf(line[2]!).Count;
        // A Refine block, half way through, for the restart check.
        var refine = Place(origin.AddCopy(4, 0, 1), Machine.Refine, "up");
        Put(refine, Crystal.Fire, Crystal.Glittering);
        Put(refine, Crystal.Unaligned, Crystal.Glittering);
        Fill(refine);
        if (refine != null)
        {
            refine.RefineNow(Machine.RefineDays(Crystal.Glittering) * World.Calendar.HoursPerDay / 2);
        }
        TestSite.StorePos(sapi, NetworkKey, origin);
        logger.Notification("Machines test: a capacitor raised the capacity from {0:0} to {1:0} and brought {2:0}; taken out full it holds {3:0}; a Dormant block splits the line into {4} and {5}; the refining is {6:0.00} done",
            before, after, charged, full, left, right, refine == null ? -1 : refine.RefineProgress(refine.Crystals[1]));
    }

    public void Check()
    {
        var origin = TestSite.ReadPos(sapi, NetworkKey);
        if (origin == null)
        {
            logger.Error("Machines test: no test network; run /lemuria machines test network first");
            return;
        }
        var first = Accessor.GetBlockEntity(origin.AddCopy(0, 0, 1)) as BlockEntitySparkstone;
        var middle = Accessor.GetBlockEntity(origin.AddCopy(1, 0, 1)) as BlockEntitySparkstone;
        var refine = Accessor.GetBlockEntity(origin.AddCopy(4, 0, 1)) as BlockEntityMachine;
        var unaligned = refine?.Crystals.FirstOrDefault(c => !c.IsAligned);
        logger.Notification("Machines test: after the restart the capacitor holds {0:0} in {1} crystal(s), the middle block is Dormant: {2}, the refining is {3:0.00} done",
            first?.Crystals.FirstOrDefault()?.Charge ?? -1, first?.Crystals.Count ?? 0, YesNo(middle?.Active == false),
            unaligned == null || refine == null ? -1 : refine.RefineProgress(unaligned));
    }

    // ------------------------------------------------------------------ Refine

    public void Refine(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Ground(origin, 3, 2, 3);
        var refine = Place(origin.AddCopy(1, 0, 1), Machine.Refine, "up");
        Put(refine, Crystal.Fire, Crystal.Glittering);
        for (int i = 0; i < 3; i++)
        {
            Put(refine, Crystal.Unaligned, Crystal.Glittering);
        }
        // Capacitors beside it pay for three refinings.
        for (int i = 0; i < 2; i++)
        {
            var store = Free(origin.AddCopy(i * 2, 0, 1));
            for (int j = 0; j < 4; j++)
            {
                Put(store, Crystal.Fire, Crystal.Glittering, 100);
            }
        }
        Fill(refine);
        int done = refine?.RefineNow(Machine.RefineDays(Crystal.Glittering) * World.Calendar.HoursPerDay) ?? 0;
        int fire = refine?.Crystals.Count(c => c.Alignment == Crystal.Fire) ?? 0;
        logger.Notification("Machines test: a Refine block refined {0} crystal(s) in {1:0.#} day(s), {2} of 4 are Fire", done, Machine.RefineDays(Crystal.Glittering), fire);
    }

    // ------------------------------------------------------------------ Crystal Motor

    public void Motor(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Ground(origin, 3, 3, 3);
        var pos = origin.AddCopy(1, 0, 1);
        // As the note's play-test sets it up: a quern first, an Amplify on it struck on top into a Motor,
        // so the hub faces down onto the quern and the sockets up. Carving must join the quern by itself.
        var quern = World.GetBlock(new AssetLocation("game:quern-granite")) as BlockMPBase;
        if (quern != null)
        {
            Accessor.SetBlock(quern.Id, pos.DownCopy());
        }
        var amplify = Place(pos, Machine.Amplify, "up");
        var into = MachineCarver.NextBlock(World, Accessor.GetBlock(pos), BlockFacing.UP, out _);
        var motor = into != null && MachineCarver.Carve(World, null, pos, into) ? Accessor.GetBlockEntity(pos) as BlockEntityMachine : null;
        if (amplify == null || motor == null || quern == null)
        {
            logger.Error("Machines test: no Crystal Motor or quern");
            return;
        }
        Put(motor, Crystal.Fire, Crystal.Glittering);
        Fill(motor);
        bool powered = motor.PayMotor(0.1);
        TestSite.StorePos(sapi, MotorKey, pos);
        bool joined = Accessor.GetBlockEntity(pos.DownCopy())?.GetBehavior<BEBehaviorMPConsumer>()?.Network != null
            && Accessor.GetBlockEntity(pos.DownCopy())?.GetBehavior<BEBehaviorMPConsumer>()?.Network == motor.GetBehavior<BEBehaviorCrystalMotor>()?.Network;
        logger.Notification("Machines test: a Crystal Motor on a quern, powered: {0}, torque {1:0.00}, on the quern's network: {2}, sockets facing {3}",
            YesNo(powered), motor.MotorTorque(), YesNo(joined), motor.GlyphFace.Code);
        // Cut off, a Motor runs on what its crystals store and stops when that is spent; an Unaligned crystal stores nothing.
        var lone = Place(origin.Copy(), Machine.Motor, "down");
        var unaligned = Place(origin.AddCopy(2, 0, 0), Machine.Motor, "down");
        Put(lone, Crystal.Fire, Crystal.Glittering, 10);
        Put(unaligned, Crystal.Unaligned, Crystal.Glittering);
        if (lone == null || unaligned == null)
        {
            logger.Error("Machines test: no lone Crystal Motor");
            return;
        }
        float charge = Total(lone), stored = Capacity(lone);
        bool runs = lone.PayMotor(1);
        bool stops = !lone.PayMotor(10);
        logger.Notification("Machines test: a cut-off Motor stores {0:0} in {1:0} with one Glittering crystal, runs an hour: {2}, stops when spent: {3}; an Unaligned one stores {4:0} and turns: {5}",
            charge, stored, YesNo(runs), YesNo(stops), Capacity(unaligned), YesNo(unaligned.PayMotor(1)));
    }

    public void MotorCheck()
    {
        var pos = TestSite.ReadPos(sapi, MotorKey);
        var consumer = pos == null ? null : Accessor.GetBlockEntity(pos.DownCopy())?.GetBehavior<BEBehaviorMPConsumer>();
        // A few seconds after it starts, the network is still spinning up toward the Motor's speed.
        float speed = consumer?.Network?.Speed ?? -1;
        logger.Notification("Machines test: the quern turns: {0}", YesNo(speed > 0.001f));
        // Then the rod switches its only crystal Dormant, as in the owner's play-test; motorstopped checks it stops.
        if (pos != null && Accessor.GetBlockEntity(pos) is BlockEntityMachine motor)
        {
            ItemCrystalRod.Tap(World, motor, new BlockSelection { Position = pos, Face = motor.GlyphFace, HitPosition = new Vec3d(0.25, 1, 0.25) }, false);
        }
    }

    public void MotorStopped()
    {
        var pos = TestSite.ReadPos(sapi, MotorKey);
        var motor = pos == null ? null : Accessor.GetBlockEntity(pos) as BlockEntityMachine;
        float speed = pos == null ? -1 : Accessor.GetBlockEntity(pos.DownCopy())?.GetBehavior<BEBehaviorMPConsumer>()?.Network?.Speed ?? -1;
        logger.Notification("Machines test: with its only crystal Dormant the Motor is unpowered: {0}, and the quern has stopped: {1} (speed {2:0.0000})",
            YesNo(motor?.MotorPowered == false), YesNo(speed >= 0 && speed < 0.001f), speed);
    }

    // ------------------------------------------------------------------ Quicken

    public void Quicken(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Ground(origin, 3, 5, 3);
        var cradlePos = origin.AddCopy(1, 0, 1);
        Accessor.SetBlock(World.GetBlock(new AssetLocation(LemuriaModSystem.ModId, "cradle-" + Rock))?.Id ?? 0, cradlePos);
        if (Accessor.GetBlockEntity(cradlePos) is not BlockEntityCradle cradle)
        {
            logger.Error("Machines test: no cradle");
            return;
        }
        cradle.Plant(LemuriaModSystem.ModId + ":seedling-glittering");
        var focus = Place(cradlePos.AddCopy(0, 3, 0), Machine.Focus, "down");
        Put(focus, Crystal.Unaligned, Crystal.Glittering);
        Fill(focus);
        bool quickened = false;
        MachineWork.Quickened += OnQuickened;
        var result = focus?.WorkOnce();
        MachineWork.Quickened -= OnQuickened;
        logger.Notification("Machines test: an Unaligned Focus over a growing seedling {0}, quickened: {1}", result, YesNo(quickened));

        void OnQuickened(BlockEntityMachine _) => quickened = true;
    }
}
