using System.Collections.Generic;
using System.Linq;
using Lemuria.Core;
using Lemuria.Crystals;
using Lemuria.Sparkstone;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;
using Vintagestory.GameContent.Mechanics;

namespace Lemuria.Machines;

/// <summary>
/// The scenes for step 4 part 2, "Products and logistics" (note, How it is built: Smoke tests): the
/// shelf's actions and order, the port and a full output, the roofed charcoal pit, felling, Memory,
/// the reclamation plate, the Dead Socket and boring.
/// </summary>
public partial class MachinesTests
{
    private const string ShelfKey = LemuriaModSystem.ModId + "-machinestest-shelf";
    private const string MemoryKey = LemuriaModSystem.ModId + "-machinestest-memory";
    private const string DeadSocketKey = LemuriaModSystem.ModId + "-machinestest-deadsocket";

    private ItemStack? Stack(string code, int size = 1)
    {
        var location = new AssetLocation(code);
        CollectibleObject? collectible = World.GetItem(location);
        collectible ??= World.GetBlock(location);
        if (collectible == null)
        {
            logger.Error("Machines test: no {0}", code);
            return null;
        }
        return new ItemStack(collectible, size);
    }

    private IInventory? Chest(BlockPos pos, string side = "north")
    {
        Accessor.SetBlock(Id("game:chest-" + side), pos);
        return (Accessor.GetBlockEntity(pos) as BlockEntityGenericTypedContainer)?.Inventory;
    }

    private static void Add(IInventory? inventory, ItemStack? stack)
    {
        var slot = inventory?.FirstOrDefault(s => s.Empty);
        if (slot != null && stack != null)
        {
            slot.Itemstack = stack;
            slot.MarkDirty();
        }
    }

    private static int CountOf(IInventory? inventory, string path) =>
        inventory?.Where(s => s.Itemstack?.Collectible.Code.Path == path).Sum(s => s.StackSize) ?? -1;

    /// <summary>A Control Shelf against this face of a machine, its open face looking away, with its items in order.</summary>
    private BlockEntityControlShelf? Shelf(BlockPos machine, BlockFacing face, params string[] items)
    {
        var pos = machine.AddCopy(face);
        Accessor.SetBlock(Id(LemuriaModSystem.ModId + ":controlshelf-" + face.Code), pos);
        if (Accessor.GetBlockEntity(pos) is not BlockEntityControlShelf shelf)
        {
            logger.Error("Machines test: no Control Shelf");
            return null;
        }
        SetItems(shelf, items);
        return shelf;
    }

    /// <summary>A chest at the shelf's left, its input, or at its right, its output.</summary>
    private IInventory? Port(BlockEntityControlShelf? shelf, bool input) =>
        shelf == null ? null : Chest(shelf.Pos.AddCopy(input ? shelf.Left : shelf.Right));

    private void SetItems(BlockEntityControlShelf shelf, params string[] items)
    {
        for (int i = 0; i < BlockEntityControlShelf.Places; i++)
        {
            shelf.Inventory[i].Itemstack = i < items.Length ? Stack(items[i]) : null;
            shelf.Inventory[i].MarkDirty();
        }
        shelf.MarkDirty(true);
    }

    private void Solid(BlockPos from, int sizeX, int sizeY, int sizeZ)
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                for (int z = 0; z < sizeZ; z++)
                {
                    Accessor.SetBlock(Id("game:rock-granite"), from.AddCopy(x, y, z));
                }
            }
        }
    }

    // ------------------------------------------------------------------ the shelf: sowing, order, the port

    public void ShelfScene(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Ground(origin, 5, 6, 5);
        var field = origin.AddCopy(1, -1, 1);
        for (int x = 0; x < 3; x++)
        {
            for (int z = 0; z < 3; z++)
            {
                Accessor.SetBlock(Id("game:farmland-moist-medium"), field.AddCopy(x, 0, z));
            }
        }
        // A Glowing Unaligned Focus over the middle of the 3×3, its shelf on its east side.
        var focusPos = field.AddCopy(1, 3, 1);
        var focus = Place(focusPos, Machine.Focus, "down");
        var shelf = Shelf(focusPos, BlockFacing.EAST, "game:seeds-flax", "game:axe-felling-copper");
        var input = Port(shelf, true);
        var output = Port(shelf, false);
        Add(input, Stack("game:seeds-flax", 9));
        Add(input, Stack("game:log-placed-oak-ud", 2));
        // A chest on top of the shelf is not its port (note rev 33): its logs stay.
        var above = shelf == null ? null : Chest(shelf.Pos.UpCopy());
        Add(above, Stack("game:log-placed-oak-ud", 4));
        Put(focus, Crystal.Unaligned, Crystal.Glowing);
        if (focus == null || shelf == null)
        {
            return;
        }
        Fill(focus);
        focus.ReadContext();
        // Seeds first: they sow, and the logs wait.
        var sowed = focus.WorkOnce();
        int sown = 0;
        for (int x = 0; x < 3; x++)
        {
            for (int z = 0; z < 3; z++)
            {
                if (Path(field.AddCopy(x, 1, z)) == "crop-flax-1")
                {
                    sown++;
                }
            }
        }
        int logsLeft = CountOf(input, "log-placed-oak-ud");
        // No seeds left, so the axe splits the logs; with no firewood on the shelf, the firewood goes to the output.
        Fill(focus);
        var split = focus.WorkOnce();
        int toOutput = CountOf(output, "firewood");
        // With firewood on the shelf too, split firewood goes back to the input as stock.
        SetItems(shelf, "game:axe-felling-copper", "game:firewood");
        Add(input, Stack("game:log-placed-oak-ud", 1));
        focus.ReadContext();
        Fill(focus);
        var back = focus.WorkOnce();
        int toInput = CountOf(input, "firewood");
        int aboveLogs = CountOf(above, "log-placed-oak-ud");
        // Its firewood switched Dormant with the rod, and a Dead Socket in its socket, for the restart check
        // (H2, H5): one more turn and the axe, out of logs, sends the whole shelf Dormant.
        shelf.SwitchDormant(1);
        shelf.SetSocket(Stack(LemuriaModSystem.ModId + ":deadsocket"));
        Fill(focus);
        focus.WorkOnce();
        TestSite.StorePos(sapi, ShelfKey, shelf.Pos);
        logger.Notification("Machines test: a shelf of seeds then an axe {0}, {1} of 9 sown, logs left {2}; then {3}, firewood in the output {4}; with firewood on the shelf {5}, firewood in the input {6}; logs left in a chest on top {7}",
            sowed, sown, logsLeft, split, toOutput, back, toInput, aboveLogs);
    }

    // ------------------------------------------------------------------ the shelf: a sapling, the shovel, a full output

    public void DigScene(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Ground(origin, 9, 6, 5, "game:soil-medium-none");
        // A Glittering Unaligned Focus plants a sapling on the soil below it.
        var soil = origin.AddCopy(1, -1, 2);
        var planter = Place(soil.AddCopy(0, 3, 0), Machine.Focus, "down");
        var saplings = Port(Shelf(soil.AddCopy(0, 3, 0), BlockFacing.WEST, "game:sapling-oak-free"), true);
        Add(saplings, Stack("game:sapling-oak-free", 2));
        Put(planter, Crystal.Unaligned, Crystal.Glittering);
        Fill(planter);
        planter?.ReadContext();
        var planted = planter?.WorkOnce();
        string sapling = Path(soil.UpCopy());
        // A Glowing Unaligned Focus with a shovel digs a 3×3 of charcoal into its output.
        var piles = origin.AddCopy(4, -1, 1);
        void Lay()
        {
            for (int x = 0; x < 3; x++)
            {
                for (int z = 0; z < 3; z++)
                {
                    Accessor.SetBlock(Id("game:charcoalpile-8"), piles.AddCopy(x, 0, z));
                }
            }
        }
        Lay();
        var focusPos = piles.AddCopy(1, 3, 1);
        var digger = Place(focusPos, Machine.Focus, "down");
        var diggerShelf = Shelf(focusPos, BlockFacing.EAST, "game:shovel-copper");
        Port(diggerShelf, true);
        var output = Port(diggerShelf, false);
        Put(digger, Crystal.Unaligned, Crystal.Glowing);
        if (digger == null || output == null)
        {
            return;
        }
        Fill(digger);
        digger.ReadContext();
        var dug = digger.WorkOnce();
        int cleared = 0;
        for (int x = 0; x < 3; x++)
        {
            for (int z = 0; z < 3; z++)
            {
                if (Accessor.GetBlock(piles.AddCopy(x, 0, z)).Id == 0)
                {
                    cleared++;
                }
            }
        }
        int charcoal = CountOf(output, "charcoal");
        // Its output stuffed full, the next dig does not happen and the machine goes Dormant.
        foreach (var slot in output)
        {
            slot.Itemstack = Stack("game:rock-granite", 64);
            slot.MarkDirty();
        }
        Lay();
        Fill(digger);
        var full = digger.WorkOnce();
        bool kept = Path(piles) == "charcoalpile-8";
        logger.Notification("Machines test: a shelf sapling {0}: {1}; a shovel {2}, {3} of 9 piles dug, charcoal in the output: {4}; with the output full {5}, the piles kept: {6}, Dormant: {7}, stopped full: {8}",
            planted, sapling, dug, cleared, charcoal > 0 ? "yes" : "no", full, YesNo(kept), YesNo(!digger.Active), YesNo(digger.StoppedFull));
    }

    // ------------------------------------------------------------------ the roofed charcoal pit, as the play-test builds it

    public void Pit(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Ground(origin, 3, 6, 7);
        // Solid granite with a room carved in it: 1 wide, 2 high, 3 deep, toward the south, under a roof.
        Solid(origin, 3, 4, 7);
        var room = origin.AddCopy(1, 1, 2);
        for (int y = 0; y < 2; y++)
        {
            for (int z = 0; z < 3; z++)
            {
                Accessor.SetBlock(0, room.AddCopy(0, y, z));
            }
        }
        // The Focus in the north end wall at the upper layer, facing in, an Amplify beside it in the rock.
        var focusPos = room.AddCopy(0, 1, -1);
        var focus = Place(focusPos, Machine.Focus, "south");
        var amplify = Place(focusPos.WestCopy(), Machine.Amplify, "up");
        // Its shelf on its back, outside, a logs chest at its left and an empty chest at its right (note rev 33).
        var shelf = Shelf(focusPos, BlockFacing.NORTH, "game:axe-felling-copper", "game:shovel-copper", "game:firewood", "game:firestarter");
        var input = Port(shelf, true);
        var output = Port(shelf, false);
        for (int i = 0; i < 4; i++)
        {
            Add(input, Stack("game:log-placed-oak-ud", 16));
        }
        Put(focus, Crystal.Unaligned, Crystal.Glowing);
        Put(amplify, Crystal.Fire, Crystal.Glowing);
        if (focus == null || shelf == null)
        {
            return;
        }
        Fill(focus);
        focus.ReadContext();
        pitFocus = focusPos;
        var front = focusPos.AddCopy(BlockFacing.SOUTH);
        int workings = 0;
        bool lit = false;
        var results = new Dictionary<MachineWork.Result, int>();
        for (; workings < 400 && focus.Active && !lit; workings++)
        {
            Fill(focus);
            var result = focus.WorkOnce();
            results[result] = results.GetValueOrDefault(result) + 1;
            lit = Accessor.GetBlockEntity(front) is BlockEntityCharcoalPit { Lit: true };
        }
        int full = 0;
        for (int y = 0; y < 2; y++)
        {
            for (int z = 0; z < 3; z++)
            {
                var cell = room.AddCopy(0, y, z);
                if (!cell.Equals(front) && Accessor.GetBlockEntity(cell) is BlockEntityGroundStorage pile && pile.Inventory[0].StackSize >= pile.Capacity)
                {
                    full++;
                }
            }
        }
        logger.Notification("Machines test: the roofed pit after {0} working(s) ({1}): {2} of 5 cells full piles, the front cell holds {3}, lit: {4}, nothing in the output: {5}",
            workings, string.Join(", ", results.OrderBy(r => r.Key).Select(r => $"{r.Key} {r.Value}")), full, Path(front), YesNo(lit),
            YesNo(output?.All(s => s.Empty) == true));
        // While it burns every item is quiet, done or out of stock, and a turn checks nothing (H4).
        Fill(focus);
        var burning = focus.WorkOnce();
        bool allQuiet = Enumerable.Range(0, BlockEntityControlShelf.Places).All(shelf.IsDormant);
        // After its 18 in-game hours the pit is charcoal; one sneak tap and the shovel digs it, then firewood stacks the next pit.
        for (int y = 0; y < 2; y++)
        {
            for (int z = 0; z < 3; z++)
            {
                Accessor.SetBlock(Id("game:charcoalpile-8"), room.AddCopy(0, y, z));
            }
        }
        shelf.WakeAll();
        var after = new List<MachineWork.Result>();
        for (int i = 0; i < 6; i++)
        {
            Fill(focus);
            after.Add(focus.WorkOnce());
        }
        bool restarted = Enumerable.Range(0, 2).Any(y => Enumerable.Range(0, 3).Any(z => Accessor.GetBlockEntity(room.AddCopy(0, y, z)) is BlockEntityGroundStorage));
        logger.Notification("Machines test: the burning pit's turn {0}, every item quiet: {1}; after the burn and one sneak tap, charcoal in the output: {2}, the next pit started: {3}",
            burning, YesNo(allQuiet), YesNo(CountOf(output, "charcoal") > 0), YesNo(restarted));
    }

    // ------------------------------------------------------------------ felling

    public void Fell(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Ground(origin, 7, 9, 5);
        var trunk = origin.AddCopy(4, 0, 2);
        for (int y = 0; y < 5; y++)
        {
            Accessor.SetBlock(Id("game:log-grown-oak-ud"), trunk.AddCopy(0, y, 0));
        }
        foreach (var d in new[] { (1, 0), (-1, 0), (0, 1), (0, -1), (0, 0) })
        {
            Accessor.SetBlock(Id("game:leaves-grown-oak"), trunk.AddCopy(d.Item1, d.Item2 == 0 && d.Item1 == 0 ? 5 : 4, d.Item2));
        }
        var focusPos = origin.AddCopy(2, 0, 2);
        var focus = Place(focusPos, Machine.Focus, "east");
        var chest = Chest(focusPos.WestCopy());
        Put(focus, Crystal.Sky, Crystal.Glittering);
        if (focus == null)
        {
            return;
        }
        Fill(focus);
        focus.ReadContext();
        float before = Total(focus);
        var result = focus.WorkOnce();
        float paid = before - Total(focus);
        int logsLeft = Enumerable.Range(0, 5).Count(y => Path(trunk.AddCopy(0, y, 0)).StartsWith("log-"));
        int logs = chest?.Where(s => s.Itemstack?.Collectible.Code.Path.StartsWith("log-") == true).Sum(s => s.StackSize) ?? -1;
        logger.Notification("Machines test: a Sky Focus at a tree of 5 logs {0}, paid {1:0.#} working(s), {2} log(s) standing, {3} log(s) in the chest",
            result, paid / Machine.WorkCost(Crystal.Glittering, false), logsLeft, logs);
    }

    // ------------------------------------------------------------------ Memory

    public void MemoryScene(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Ground(origin, 3, 4, 3);
        var focusPos = origin.AddCopy(1, 0, 1);
        var focus = Place(focusPos, Machine.Focus, "up");
        var memoryPos = focusPos.WestCopy();
        Accessor.SetBlock(Id($"{LemuriaModSystem.ModId}:memory-awakened-{Rock}-west"), memoryPos);
        var memory = Accessor.GetBlockEntity(memoryPos) as BlockEntityMemory;
        Put(focus, Crystal.Fire, Crystal.Glittering);
        bool loaded = Put(memory, Crystal.Ice, Crystal.Glittering) & Put(memory, Crystal.Sky, Crystal.Glittering);
        bool third = Put(memory, Crystal.Earth, Crystal.Glittering);
        if (focus == null || memory == null)
        {
            logger.Error("Machines test: no Memory");
            return;
        }
        Fill(focus);
        focus.ReadContext();
        var worked = new List<string>();
        for (int i = 0; i < 4; i++)
        {
            worked.Add(focus.Crystals.FirstOrDefault()?.Alignment ?? "none");
            Fill(focus);
            focus.WorkOnce();
        }
        TestSite.StorePos(sapi, MemoryKey, memoryPos);
        logger.Notification("Machines test: a Memory took 2 crystals: {0}, a third: {1}; its Focus worked {2}; the magazine holds {3}",
            YesNo(loaded), YesNo(third), string.Join(", ", worked), string.Join(", ", memory.Crystals.Select(c => c.Alignment)));
    }

    // ------------------------------------------------------------------ the reclamation plate

    public void Plate(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Ground(origin, 5, 7, 5);
        // A basin of granite: a plate under the middle, a chest under the plate, salt water over the 3×3.
        Solid(origin.AddCopy(0, 0, 0), 5, 2, 5);
        var middle = origin.AddCopy(2, 0, 2);
        var chest = Chest(middle.DownCopy());
        Accessor.SetBlock(Id(LemuriaModSystem.ModId + ":reclamationplate"), middle);
        for (int x = -1; x <= 1; x++)
        {
            for (int z = -1; z <= 1; z++)
            {
                var cell = middle.AddCopy(x, 1, z);
                Accessor.SetBlock(0, cell);
                Accessor.SetBlock(Id("game:saltwater-still-7"), cell, BlockLayersAccess.Fluid);
            }
        }
        var focusPos = middle.AddCopy(0, 4, 0);
        var focus = Place(focusPos, Machine.Focus, "down");
        var amplify = Place(focusPos.EastCopy(), Machine.Amplify, "up");
        Put(focus, Crystal.Fire, Crystal.Glowing);
        Put(amplify, Crystal.Fire, Crystal.Glowing);
        if (focus == null)
        {
            return;
        }
        Fill(focus);
        focus.ReadContext();
        double chance = LemuriaModSystem.Config.SaltChance;
        LemuriaModSystem.Config.SaltChance = 1;
        MachineWork.Result result;
        try
        {
            result = focus.WorkOnce();
        }
        finally
        {
            LemuriaModSystem.Config.SaltChance = chance;
        }
        int water = 0;
        for (int x = -1; x <= 1; x++)
        {
            for (int z = -1; z <= 1; z++)
            {
                if (Accessor.GetBlock(middle.AddCopy(x, 1, z), BlockLayersAccess.Fluid).Id != 0)
                {
                    water++;
                }
            }
        }
        logger.Notification("Machines test: a Fire Focus over a plate {0}, salt water left {1} of 9, salt in the plate's chest {2}",
            result, water, CountOf(chest, "salt"));
    }

    // ------------------------------------------------------------------ boring

    public void Bore(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Ground(origin, 12, 5, 5);
        Solid(origin.AddCopy(3, 0, 0), 9, 4, 5);
        var focusPos = origin.AddCopy(1, 1, 1);
        var focus = Place(focusPos, Machine.Focus, "east");
        var chest = Chest(focusPos.WestCopy());
        Put(focus, Crystal.Earth, Crystal.Glittering);
        if (focus == null)
        {
            return;
        }
        Fill(focus);
        focus.ReadContext();
        var result = focus.WorkOnce();
        int depth = 0;
        for (int d = 0; d < 8; d++)
        {
            var at = origin.AddCopy(3 + d, 1, 1);
            if (new[] { at, at.UpCopy(), at.SouthCopy(), at.UpCopy().SouthCopy() }.All(p => Accessor.GetBlock(p).Id == 0))
            {
                depth++;
            }
        }
        int stone = chest?.Sum(s => s.StackSize) ?? -1;
        logger.Notification("Machines test: an Earth Focus facing east {0}, a 2×2 bored {1} deep, {2} block(s) in the chest, Dormant: {3}",
            result, depth, stone, YesNo(!focus.Active));
    }

    // ------------------------------------------------------------------ the Dead Socket

    public void DeadSocket(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Ground(origin, 3, 3, 3);
        var pos = origin.AddCopy(1, 1, 1);
        var quern = World.GetBlock(new AssetLocation("game:quern-granite")) as BlockMPBase;
        if (quern != null)
        {
            Accessor.SetBlock(quern.Id, pos.DownCopy());
        }
        var motor = Place(pos, Machine.Motor, "down");
        var line = Free(pos.EastCopy());
        Put(line, Crystal.Fire, Crystal.Glittering, 100);
        var fitting = Stack(LemuriaModSystem.ModId + ":deadsocket");
        if (motor == null || line == null || fitting == null || quern == null)
        {
            logger.Error("Machines test: no Dead Socket scene");
            return;
        }
        bool refused = Place(origin.AddCopy(0, 1, 0), Machine.Focus, "up") is { } focus && !focus.Insert(fitting.Clone(), focus.SocketFacing ?? BlockFacing.UP);
        bool fitted = motor.Insert(fitting, motor.SocketFacing ?? BlockFacing.UP);
        Fill(motor);
        bool powered = motor.PayMotor(0.1);
        TestSite.StorePos(sapi, DeadSocketKey, pos);
        logger.Notification("Machines test: a Dead Socket fitted in a Motor: {0}, refused by a Focus: {1}; powered: {2}, torque {3:0.00}",
            YesNo(fitted), YesNo(refused), YesNo(powered), motor.MotorTorque());
    }

    public void DeadSocketCheck()
    {
        var pos = TestSite.ReadPos(sapi, DeadSocketKey);
        var consumer = pos == null ? null : Accessor.GetBlockEntity(pos.DownCopy())?.GetBehavior<BEBehaviorMPConsumer>();
        logger.Notification("Machines test: the Dead Socket's quern turns: {0}", YesNo((consumer?.Network?.Speed ?? -1) > 0.001f));
        // Then its line is cut: the capacitor's block goes Dormant, and the Motor has nothing to draw on.
        if (pos != null && Accessor.GetBlockEntity(pos.EastCopy()) is BlockEntitySparkstone line)
        {
            // Nothing else is called: the Motor must see at once that its line is cut (play-test, Oct 29).
            line.SetActive(false);
            logger.Notification("Machines test: the moment its line is cut, the Dead Socket's Motor is unpowered: {0}",
                YesNo((Accessor.GetBlockEntity(pos) as BlockEntityMachine)?.MotorPowered == false));
        }
    }

    public void DeadSocketStopped()
    {
        var pos = TestSite.ReadPos(sapi, DeadSocketKey);
        var motor = pos == null ? null : Accessor.GetBlockEntity(pos) as BlockEntityMachine;
        float speed = pos == null ? -1 : Accessor.GetBlockEntity(pos.DownCopy())?.GetBehavior<BEBehaviorMPConsumer>()?.Network?.Speed ?? -1;
        logger.Notification("Machines test: cut off, the Dead Socket's Motor is unpowered: {0}, and the quern has stopped: {1}",
            YesNo(motor?.MotorPowered == false), YesNo(speed >= 0 && speed < 0.001f));
    }

    // ------------------------------------------------------------------ prefixes and Dormant items (H1, H2)

    public void Prefix(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Ground(origin, 5, 6, 5);
        // A 3×3 floor of gravel and soil in a chequer, under a Glowing Unaligned Focus.
        var floor = origin.AddCopy(1, -1, 1);
        for (int x = 0; x < 3; x++)
        {
            for (int z = 0; z < 3; z++)
            {
                Accessor.SetBlock(Id((x + z) % 2 == 0 ? "game:gravel-granite" : "game:soil-medium-none"), floor.AddCopy(x, 0, z));
            }
        }
        var focusPos = floor.AddCopy(1, 3, 1);
        var focus = Place(focusPos, Machine.Focus, "down");
        var shelf = Shelf(focusPos, BlockFacing.EAST, "game:gravel-granite", "game:shovel-copper");
        var input = Port(shelf, true);
        var output = Port(shelf, false);
        Put(focus, Crystal.Unaligned, Crystal.Glowing);
        if (focus == null || shelf == null)
        {
            return;
        }
        Fill(focus);
        focus.ReadContext();
        // Gravel before the shovel: only the gravel goes.
        var dug = focus.WorkOnce();
        int gravelLeft = 0, soilLeft = 0;
        for (int x = 0; x < 3; x++)
        {
            for (int z = 0; z < 3; z++)
            {
                string path = Path(floor.AddCopy(x, 0, z));
                gravelLeft += path.StartsWith("gravel-") ? 1 : 0;
                soilLeft += path.StartsWith("soil-") ? 1 : 0;
            }
        }
        // An oak log before the axe: only oak is split, and only oak is kept back as stock.
        SetItems(shelf, "game:log-placed-oak-ud", "game:axe-felling-copper");
        Add(input, Stack("game:log-placed-oak-ud", 2));
        Add(input, Stack("game:log-placed-birch-ud", 2));
        Fill(focus);
        var split = focus.WorkOnce();
        int oak = CountOf(input, "log-placed-oak-ud"), birch = CountOf(input, "log-placed-birch-ud");
        bool keepsOak = Stack("game:log-placed-oak-ud") is { } oakLog && focus.ShelfUses(oakLog);
        bool keepsBirch = Stack("game:log-placed-birch-ud") is { } birchLog && focus.ShelfUses(birchLog);
        // The rod: a prefix is never switched; the axe switched Dormant is passed over, and woken it works.
        bool prefixSwitched = shelf.SwitchDormant(0);
        bool switched = shelf.SwitchDormant(1);
        Add(input, Stack("game:log-placed-oak-ud", 1));
        Fill(focus);
        var asleep = focus.WorkOnce();
        bool woke = shelf.SwitchDormant(1);
        Fill(focus);
        var awake = focus.WorkOnce();
        logger.Notification("Machines test: a gravel-kept shovel {0}, gravel left {1}, soil left {2}; an oak-kept axe {3}, oak left {4}, birch left {5}, keeps oak: {6}, keeps birch: {7}, firewood in the output {8}",
            dug, gravelLeft, soilLeft, split, oak, birch, YesNo(keepsOak), YesNo(keepsBirch), CountOf(output, "firewood"));
        logger.Notification("Machines test: the rod switches a prefix: {0}; the axe Dormant: {1}, then the machine {2}; woken: {3}, then {4}",
            YesNo(prefixSwitched), YesNo(switched), asleep, YesNo(woke && !shelf.IsDormant(1)), awake);
    }

    // ------------------------------------------------------------------ quiet items and the shelf's socket (H4, H5)

    private BlockPos Farmland3x3(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Ground(origin, 5, 6, 5);
        var field = origin.AddCopy(1, -1, 1);
        for (int x = 0; x < 3; x++)
        {
            for (int z = 0; z < 3; z++)
            {
                Accessor.SetBlock(Id("game:farmland-moist-medium"), field.AddCopy(x, 0, z));
            }
        }
        return field;
    }

    private int Sown(BlockPos field) =>
        Enumerable.Range(0, 3).Sum(x => Enumerable.Range(0, 3).Count(z => Path(field.AddCopy(x, 1, z)) == "crop-flax-1"));

    /// <summary>
    /// Quiet items (H4): seeds run out and go quiet, out of stock, and wake when seeds arrive; sown out,
    /// they go quiet, done, and stay so with stock until a sneak tap, which leaves a switched-off item off.
    /// </summary>
    public void QuietScene(BlockPos column)
    {
        var field = Farmland3x3(column);
        var focusPos = field.AddCopy(1, 3, 1);
        var focus = Place(focusPos, Machine.Focus, "down");
        var shelf = Shelf(focusPos, BlockFacing.EAST, "game:seeds-flax", "game:shovel-copper");
        var input = Port(shelf, true);
        Add(input, Stack("game:seeds-flax", 3));
        Put(focus, Crystal.Unaligned, Crystal.Glowing);
        if (focus == null || shelf == null)
        {
            return;
        }
        Fill(focus);
        focus.ReadContext();
        var first = focus.WorkOnce();
        Fill(focus);
        var second = focus.WorkOnce();
        bool outOfStock = shelf.QuietOf(0) == BlockEntityControlShelf.Quiet.OutOfStock;
        bool shovelDone = shelf.QuietOf(1) == BlockEntityControlShelf.Quiet.Done;
        // Seeds put in the input wake them, through its own change event.
        Add(input, Stack("game:seeds-flax", 2));
        bool woke = shelf.QuietOf(0) == BlockEntityControlShelf.Quiet.Awake;
        Fill(focus);
        focus.WorkOnce();
        Add(input, Stack("game:seeds-flax", 10));
        Fill(focus);
        focus.WorkOnce();
        Fill(focus);
        focus.WorkOnce();
        int sown = Sown(field);
        bool done = shelf.QuietOf(0) == BlockEntityControlShelf.Quiet.Done;
        Add(input, Stack("game:seeds-flax", 1));
        bool stayed = shelf.QuietOf(0) == BlockEntityControlShelf.Quiet.Done;
        // The shovel, done, wakes with one right-click of the rod and switches off with the next.
        shelf.SwitchDormant(1);
        shelf.SwitchDormant(1);
        shelf.WakeAll();
        bool tapped = shelf.QuietOf(0) == BlockEntityControlShelf.Quiet.Awake;
        bool off = shelf.QuietOf(1) == BlockEntityControlShelf.Quiet.Off;
        // The plugs under the places (note rev 74): lit for the awake seeds, dark for the switched-off shovel and the empty places.
        string plugs = string.Concat(Enumerable.Range(0, BlockEntityControlShelf.Places).Select(i => shelf.PlugLit(i) ? "lit " : "dark ")).Trim();
        logger.Notification("Machines test: quiet seeds {0}, then {1}: out of stock: {2}, the shovel done: {3}; seeds arrive and they wake: {4}; {5} of 9 sown, then done: {6}, still done with stock: {7}; a sneak tap wakes them: {8}, the switched-off shovel stays off: {9}; plugs {10}",
            first, second, YesNo(outOfStock), YesNo(shovelDone), YesNo(woke), sown, YesNo(done), YesNo(stayed), YesNo(tapped), YesNo(off), plugs);
    }

    /// <summary>
    /// The shelf's socket (H5): with a Dead Socket in it and no compost in the input, compost before seeds
    /// sends the whole shelf Dormant and nothing is sown; compost arriving wakes it, and it feeds, then sows.
    /// </summary>
    public void ShelfSocketScene(BlockPos column)
    {
        var field = Farmland3x3(column);
        var focusPos = field.AddCopy(1, 3, 1);
        var focus = Place(focusPos, Machine.Focus, "down");
        var shelf = Shelf(focusPos, BlockFacing.EAST, "game:compost", "game:seeds-flax");
        var input = Port(shelf, true);
        Add(input, Stack("game:seeds-flax", 9));
        Put(focus, Crystal.Unaligned, Crystal.Glowing);
        if (focus == null || shelf == null)
        {
            return;
        }
        shelf.SetSocket(Stack(LemuriaModSystem.ModId + ":deadsocket"));
        Fill(focus);
        focus.ReadContext();
        var first = focus.WorkOnce();
        bool asleep = shelf.Asleep && Enumerable.Range(0, 2).All(shelf.IsDormant) && !Enumerable.Range(0, 2).Any(shelf.PlugLit);
        int sownAsleep = Sown(field);
        // Compost arriving wakes it. It feeds while the farmland takes compost, as your clicks would, then
        // compost is done, and the seeds sow; compost kept in stock keeps the shelf awake.
        for (int i = 0; i < 4; i++)
        {
            Add(input, Stack("game:compost", 64));
        }
        bool woke = !shelf.Asleep;
        int feedings = 0;
        var last = MachineWork.Result.Nothing;
        for (int i = 0; i < 40 && Sown(field) == 0; i++)
        {
            Fill(focus);
            last = focus.WorkOnce();
            feedings += last == MachineWork.Result.Acted && Sown(field) == 0 ? 1 : 0;
        }
        logger.Notification("Machines test: a socketed shelf with no compost {0}, the whole shelf Dormant: {1}, {2} sown; compost arrives and it wakes: {3}; it feeds {4} time(s), compost done: {5}, then {6}, {7} of 9 sown, the shelf awake: {8}",
            first, YesNo(asleep), sownAsleep, YesNo(woke), feedings, YesNo(shelf.QuietOf(0) == BlockEntityControlShelf.Quiet.Done), last, Sown(field), YesNo(!shelf.Asleep));
    }

    // ------------------------------------------------------------------ what an idle shelf turn costs

    /// <summary>The roofed pit's Focus, for the idle timing once its pit burns.</summary>
    private BlockPos? pitFocus;

    /// <summary>Microseconds a turn of this machine takes, over this many turns, its line kept full.</summary>
    private double TimeTurns(BlockEntityMachine machine, int turns, out MachineWork.Result last)
    {
        Fill(machine);
        machine.ReadContext();
        last = MachineWork.Result.Idle;
        var watch = System.Diagnostics.Stopwatch.StartNew();
        for (int i = 0; i < turns; i++)
        {
            last = machine.WorkOnce();
        }
        watch.Stop();
        return watch.Elapsed.TotalMilliseconds * 1000 / turns;
    }

    /// <summary>
    /// Times idle shelf turns, with nothing to do (planning thread's cost question, Oct 29): a Scatter's
    /// seeds over a sown 9×9, the roofed pit's Focus once its pit burns, and compost over a fed 3×3.
    /// Not part of the smoke run; for the log only.
    /// </summary>
    public void Idle(BlockPos column)
    {
        const int Turns = 200;
        var origin = TestSite.Surface(sapi, column);
        Ground(origin, 13, 7, 13);
        // A Scatter with flax seeds over a 9×9 already sown.
        var field = origin.AddCopy(2, -1, 2);
        for (int x = 0; x < 9; x++)
        {
            for (int z = 0; z < 9; z++)
            {
                Accessor.SetBlock(Id("game:farmland-moist-medium"), field.AddCopy(x, 0, z));
                Accessor.SetBlock(Id("game:crop-flax-1"), field.AddCopy(x, 1, z));
            }
        }
        var scatterPos = field.AddCopy(4, 5, 4);
        var scatter = Place(scatterPos, Machine.Scatter, "down");
        var scatterShelf = Shelf(scatterPos, BlockFacing.EAST, "game:seeds-flax");
        Add(Port(scatterShelf, true), Stack("game:seeds-flax", 64));
        Port(scatterShelf, false);
        Put(scatter, Crystal.Unaligned, Crystal.Glowing);
        double sown = scatter == null ? -1 : TimeTurns(scatter, Turns, out var sownResult);
        // The roofed pit's Focus, its pit lit, a chest of firewood as its input.
        double pit = -1;
        if (pitFocus != null && Accessor.GetBlockEntity(pitFocus) is BlockEntityMachine pitMachine
            && Accessor.GetBlockEntity(pitFocus.NorthCopy()) is BlockEntityControlShelf pitShelf)
        {
            if (!pitMachine.Active)
            {
                pitMachine.SetActive(true);
            }
            var input = pitShelf.Input?.Inventory;
            for (int i = 0; i < 8; i++)
            {
                Add(input, Stack("game:firewood", 32));
            }
            pit = TimeTurns(pitMachine, Turns, out _);
        }
        // Compost over a 3×3 of farmland already fed full.
        var fed = origin.AddCopy(2, -1, 2);
        var compostPos = fed.AddCopy(1, 3, 1).AddCopy(0, 0, 0);
        // Reuse the sown field's corner: clear its crops so the shelf has only compost to think about.
        for (int x = 0; x < 3; x++)
        {
            for (int z = 0; z < 3; z++)
            {
                Accessor.SetBlock(0, fed.AddCopy(x, 1, z));
            }
        }
        Accessor.SetBlock(0, scatterPos);
        var feeder = Place(compostPos, Machine.Focus, "down");
        var feederShelf = Shelf(compostPos, BlockFacing.WEST, "game:compost");
        Add(Port(feederShelf, true), Stack("game:compost", 64));
        Put(feeder, Crystal.Unaligned, Crystal.Glowing);
        double compost = -1;
        if (feeder != null)
        {
            Fill(feeder);
            feeder.ReadContext();
            for (int i = 0; i < 20; i++)
            {
                feeder.WorkOnce();
            }
            // Stock enough that an idle turn still looks at every block in reach.
            for (int i = 0; i < 8; i++)
            {
                Add(feederShelf?.Input?.Inventory, Stack("game:compost", 64));
            }
            compost = TimeTurns(feeder, Turns, out _);

        }
        logger.Notification("Machines test: an idle turn takes {0:0.0} µs for a Scatter's seeds over a sown 9×9, {1:0.0} µs for the pit's Focus with firewood, {2:0.0} µs for compost over a fed 3×3",
            sown, pit, compost);
    }

    // ------------------------------------------------------------------ after a restart

    public void Kept()
    {
        var shelfPos = TestSite.ReadPos(sapi, ShelfKey);
        var memoryPos = TestSite.ReadPos(sapi, MemoryKey);
        var motorPos = TestSite.ReadPos(sapi, DeadSocketKey);
        var shelf = shelfPos == null ? null : Accessor.GetBlockEntity(shelfPos) as BlockEntityControlShelf;
        var memory = memoryPos == null ? null : Accessor.GetBlockEntity(memoryPos) as BlockEntityMemory;
        var motor = motorPos == null ? null : Accessor.GetBlockEntity(motorPos) as BlockEntityMachine;
        string Why(int i) => shelf!.QuietOf(i) switch
        {
            BlockEntityControlShelf.Quiet.Off => " (switched off)",
            BlockEntityControlShelf.Quiet.OutOfStock => " (out of stock)",
            BlockEntityControlShelf.Quiet.Done => " (done)",
            _ => "",
        };
        logger.Notification("Machines test: after the restart the shelf holds {0}, a socket: {1}, the whole shelf Dormant: {2}; the Memory {3} crystal(s), the Motor {4} Dead Socket(s)",
            shelf == null ? "nothing" : string.Join(", ", shelf.Items.Select((s, i) => s == null ? null : s.Collectible.Code.Path + Why(i)).Where(n => n != null)),
            YesNo(shelf?.HasSocket == true), YesNo(shelf?.Asleep == true), memory?.Crystals.Count ?? -1, motor?.Crystals.Count(c => c.IsFitting) ?? -1);
    }
}
