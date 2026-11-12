using System;
using System.Collections.Generic;
using System.Linq;
using Lemuria.Core;
using Lemuria.Crystals;
using Lemuria.Machines;
using Lemuria.Sparkstone;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;
using Vintagestory.GameContent;

namespace Lemuria.Duets;

/// <summary>
/// The scenes behind <c>/lemuria duets test ...</c> (feature 10's brief, What to try): each builds its
/// machines from Awakened granite with no player, so no claim applies, fills their network, works them
/// and reports to the log, then switches its Focus Dormant so nothing works on after the scene.
/// </summary>
public class DuetsTests
{
    private const string Rock = "granite";
    private const string ModuleFocusKey = LemuriaModSystem.ModId + "-duetstest-focus";
    private const string ModuleMemoryKey = LemuriaModSystem.ModId + "-duetstest-memory";

    private readonly ICoreServerAPI sapi;
    private readonly ILogger logger;

    public DuetsTests(ICoreServerAPI sapi, ILogger logger)
    {
        this.sapi = sapi;
        this.logger = logger;
    }

    public void Register()
    {
        // A scene reaches up to 16 blocks east and south of its column, so the chunks it reaches load first.
        TextCommandResult Test(int dx, int dz, Action<BlockPos> action) =>
            TestSite.WithChunk(sapi, logger, "Duets", dx + 16, dz + 16, _ =>
                TestSite.WithChunk(sapi, logger, "Duets", dx + 16, dz, _ =>
                    TestSite.WithChunk(sapi, logger, "Duets", dx, dz + 16, _ =>
                        TestSite.WithChunk(sapi, logger, "Duets", dx, dz, action))));
        var scenes = new (string Name, string Description, int Dx, int Dz, Action<BlockPos> Action)[]
        {
            ("clay", "Ice and Earth in a Memory turn the sand a Focus faces into raw blue clay", -250, 20, Clay),
            ("water", "Ice and Sky fill a barrel at the output, twice as much at Glowing; none without a barrel", -250, 40, Water),
            ("fired", "Fire and Sky fire raw bowls from the shelf's input into its output", -250, 60, Fired),
            ("metal", "Fire and Earth extract copper and tin nuggets into bits; iron nuggets stay", -270, 20, Metal),
            ("module", "A Duet Module alternates two duets, the Memory swapping its pair with the module's", -270, 40, Module),
            ("bloom", "A Glowing Life Scatter over 81 crops pays nine workings' charge, then nothing that day", -270, 60, Bloom),
            ("drain", "A Memory against an Earth drain cycles Earth, Sky panning sand, and Unaligned putting it back", -294, 20, Drain),
        };
        var test = sapi.ChatCommands.GetOrCreate(LemuriaModSystem.ModId)
            .WithDescription("Lemuria commands")
            .RequiresPrivilege(Privilege.chat)
            .BeginSubCommand("duets")
                .WithDescription("Commands for the duets")
                .RequiresPrivilege(Privilege.controlserver)
                .BeginSubCommand("test")
                    .WithDescription("Tests the duets next to the world spawn");
        foreach (var scene in scenes)
        {
            var s = scene;
            test = test.BeginSubCommand(s.Name).WithDescription(s.Description).HandleWith(_ => Test(s.Dx, s.Dz, s.Action)).EndSubCommand();
        }
        test.BeginSubCommand("kept")
                .WithDescription("After a restart: the Memory still holds its pair and the Duet Module its own")
                .HandleWith(_ => Test(-270, 40, _ => Kept()))
            .EndSubCommand()
            .EndSubCommand()
            .EndSubCommand();
    }

    // ------------------------------------------------------------------ helpers

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

    private void Solid(BlockPos from, int sizeX, int sizeY, int sizeZ)
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                for (int z = 0; z < sizeZ; z++)
                {
                    Accessor.SetBlock(Id("game:rock-granite"), from.AddCopy(x, y, z));
                    Accessor.SetBlock(0, from.AddCopy(x, y, z), BlockLayersAccess.Fluid);
                }
            }
        }
    }

    private BlockEntityMachine? Place(BlockPos pos, string form, string side)
    {
        var block = World.GetBlock(Machine.Code(form, Rock, side));
        if (block == null)
        {
            logger.Error("Duets test: no block {0}", Machine.Code(form, Rock, side));
            return null;
        }
        Accessor.SetBlock(block.Id, pos);
        return Accessor.GetBlockEntity(pos) as BlockEntityMachine;
    }

    private BlockEntityMemory? Memory(BlockPos pos, string side)
    {
        Accessor.SetBlock(Id($"{LemuriaModSystem.ModId}:memory-awakened-{Rock}-{side}"), pos);
        return Accessor.GetBlockEntity(pos) as BlockEntityMemory;
    }

    private BlockEntityControlShelf? Shelf(BlockPos pos, string side)
    {
        Accessor.SetBlock(Id($"{LemuriaModSystem.ModId}:controlshelf-{side}"), pos);
        return Accessor.GetBlockEntity(pos) as BlockEntityControlShelf;
    }

    private IInventory? Chest(BlockPos pos)
    {
        Accessor.SetBlock(Id("game:chest-north"), pos);
        return (Accessor.GetBlockEntity(pos) as BlockEntityGenericTypedContainer)?.Inventory;
    }

    private ItemStack? Stack(string code, int size = 1)
    {
        var location = new AssetLocation(code);
        CollectibleObject? collectible = World.GetItem(location) ?? (CollectibleObject?)World.GetBlock(location);
        return collectible == null ? null : new ItemStack(collectible, size);
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

    private ItemStack? CrystalStack(string alignment, string evolution)
    {
        var block = Crystal.BlockFor(World, alignment, evolution);
        if (block == null)
        {
            return null;
        }
        var stack = new ItemStack(block);
        Crystal.SetState(stack, 0, 0);
        return stack;
    }

    private bool Put(BlockEntitySparkstone? stone, string alignment, string evolution) =>
        stone != null && CrystalStack(alignment, evolution) is { } stack && stone.Insert(stack, stone.SocketFacing ?? BlockFacing.UP);

    private bool PutModule(BlockEntityMachine? focus) =>
        focus != null && Stack(LemuriaModSystem.ModId + ":duetmodule") is { } module && focus.Insert(module, focus.SocketFacing ?? focus.Front);

    private static void Fill(BlockEntitySparkstone? stone)
    {
        if (stone != null)
        {
            SparkstoneModSystem.Networks?.Fill(stone);
        }
    }

    private static float Total(BlockEntitySparkstone stone) =>
        SparkstoneModSystem.Networks is { } networks ? SparkstoneNetworks.Measure(networks.NetworkOf(stone)).Total : 0;

    private MachineWork.Result Work(BlockEntityMachine focus)
    {
        Fill(focus);
        focus.ReadContext();
        return focus.WorkOnce();
    }

    private static void Rest(BlockEntityMachine? machine) => machine?.SetActive(false);

    /// <summary>A Focus facing north, a Memory on its west face, at the middle of a cleared floor; the block it faces is returned.</summary>
    private (BlockEntityMachine? Focus, BlockEntityMemory? Memory, BlockPos Faced) Rig(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Ground(origin, 5, 4, 6);
        var focusPos = origin.AddCopy(2, 0, 3);
        var focus = Place(focusPos, Machine.Focus, "north");
        var memory = Memory(focusPos.WestCopy(), "west");
        return (focus, memory, focusPos.NorthCopy());
    }

    // ------------------------------------------------------------------ the four duets

    public void Clay(BlockPos column)
    {
        var (focus, memory, faced) = Rig(column);
        if (focus == null || memory == null)
        {
            return;
        }
        Accessor.SetBlock(Id("game:sand-granite"), faced);
        bool loaded = Put(memory, Crystal.Ice, Crystal.Glittering) & Put(memory, Crystal.Earth, Crystal.Glittering);
        string duet = memory.Pair?.Product ?? "none";
        bool shielded = memory.Crystals.All(Dialogues.Shielded);
        float before = Total(focus);
        Fill(focus);
        before = Total(focus);
        var result = Work(focus);
        float paid = before - Total(focus);
        string made = Path(faced);
        var again = Work(focus);
        memory.TakeOut(memory.Crystals[^1]);
        var single = Work(focus);
        logger.Notification("Duets test: a Memory with Ice and Earth lends duet {0}, its crystals shielded: {1}; the Focus {2}, paid {3:0.0}, the sand is now {4}; again {5}; with one crystal taken out {6}",
            duet, YesNo(loaded && shielded), result, paid, made, again, single);
        Rest(focus);
    }

    public void Water(BlockPos column)
    {
        var (focus, memory, faced) = Rig(column);
        if (focus == null || memory == null)
        {
            return;
        }
        Put(memory, Crystal.Ice, Crystal.Glittering);
        Put(memory, Crystal.Sky, Crystal.Glittering);
        Fill(focus);
        float before = Total(focus);
        var none = Work(focus);
        bool unpaid = Math.Abs(Total(focus) - before) < 0.01f;
        var barrelPos = focus.Pos.EastCopy();
        Accessor.SetBlock(Id("game:barrel"), barrelPos);
        var barrel = Accessor.GetBlockEntity(barrelPos) as BlockEntityLiquidContainer;
        var block = Accessor.GetBlock(barrelPos) as BlockLiquidContainerBase;
        var glittering = Work(focus);
        float first = block?.GetCurrentLitres(barrelPos) ?? -1;
        memory.TakeOut(memory.Crystals[^1]);
        memory.TakeOut(memory.Crystals[^1]);
        Put(memory, Crystal.Ice, Crystal.Glowing);
        Put(memory, Crystal.Sky, Crystal.Glowing);
        var glowing = Work(focus);
        float second = (block?.GetCurrentLitres(barrelPos) ?? -1) - first;
        logger.Notification("Duets test: Ice and Sky with no barrel {0}, nothing paid: {1}; over a barrel {2}, {3:0} litres; at Glowing {4}, {5:0} litres more; a barrel: {6}",
            none, YesNo(unpaid), glittering, first, glowing, second, YesNo(barrel != null));
        Rest(focus);
    }

    public void Fired(BlockPos column)
    {
        var (focus, memory, _) = Rig(column);
        if (focus == null || memory == null)
        {
            return;
        }
        // A shelf on the east face, its back to the Focus: input at its left (south), output at its right (north).
        var shelfPos = focus.Pos.EastCopy();
        Shelf(shelfPos, "east");
        var input = Chest(shelfPos.SouthCopy());
        var output = Chest(shelfPos.NorthCopy());
        Add(input, Stack("game:bowl-blue-raw", 6));
        Put(memory, Crystal.Fire, Crystal.Glittering);
        Put(memory, Crystal.Sky, Crystal.Glittering);
        var result = Work(focus);
        logger.Notification("Duets test: Fire and Sky {0}, fired bowls in the output {1}, raw bowls left in the input {2}",
            result, CountOf(output, "bowl-blue-fired"), CountOf(input, "bowl-blue-raw"));
        Rest(focus);
    }

    public void Metal(BlockPos column)
    {
        var (focus, memory, _) = Rig(column);
        if (focus == null || memory == null)
        {
            return;
        }
        var shelfPos = focus.Pos.EastCopy();
        Shelf(shelfPos, "east");
        var input = Chest(shelfPos.SouthCopy());
        var output = Chest(shelfPos.NorthCopy());
        Add(input, Stack("game:nugget-nativecopper", 2));
        Add(input, Stack("game:nugget-cassiterite", 2));
        Add(input, Stack("game:nugget-hematite", 2));
        Put(memory, Crystal.Fire, Crystal.Glittering);
        Put(memory, Crystal.Earth, Crystal.Glittering);
        var result = Work(focus);
        var again = Work(focus);
        logger.Notification("Duets test: Fire and Earth {0}, copper bits {1}, tin bits {2}, iron nuggets left {3}; again {4}; every smeltable ore gives a bit: {5}",
            result, CountOf(output, "metalbit-copper"), CountOf(output, "metalbit-tin"), CountOf(input, "nugget-hematite"), again,
            YesNo(new[] { "nativesilver", "sphalerite", "galena", "chromite", "ilmenite", "pentlandite", "bismuthinite", "nativegold" }
                .All(o => Stack("game:nugget-" + o) is { } n && DuetWork.Bit(World, n) != null)));
        Rest(focus);
    }

    // ------------------------------------------------------------------ the Duet Module

    public void Module(BlockPos column)
    {
        var (focus, memory, faced) = Rig(column);
        if (focus == null || memory == null)
        {
            return;
        }
        Accessor.SetBlock(Id("game:sand-granite"), faced);
        bool refusedWithCrystal = Put(focus, Crystal.Fire, Crystal.Glittering) && !PutModule(focus);
        focus.TakeOut(focus.Crystals[0]);
        bool fitted = PutModule(focus);
        bool both = Put(focus, Crystal.Fire, Crystal.Glittering) & Put(focus, Crystal.Sky, Crystal.Glowing);
        bool third = Put(focus, Crystal.Ice, Crystal.Glittering);
        // Last in first out: a click on the module takes the Sky crystal, the last in, and the module stays.
        var lifo = focus.TakeOut(focus.Module!);
        string lifoOut = Crystal.AlignmentOf(lifo.Collectible) ?? "?";
        focus.Insert(lifo, focus.SocketFacing ?? focus.Front);
        var idle = Work(focus);
        Put(memory, Crystal.Ice, Crystal.Glittering);
        Put(memory, Crystal.Earth, Crystal.Glittering);
        var clay = Work(focus);
        string madeClay = Path(faced);
        string afterFirst = string.Join("+", memory.Crystals.Select(c => c.Alignment));
        var fired = Work(focus);
        string afterSecond = string.Join("+", memory.Crystals.Select(c => c.Alignment));
        Accessor.SetBlock(Id("game:sand-granite"), faced);
        var clayAgain = Work(focus);
        bool open = !focus.ModuleCrystals.Any(Dialogues.Shielded) && memory.Crystals.All(Dialogues.Shielded);
        logger.Notification("Duets test: a Duet Module refused beside a crystal: {0}, fitted in an empty Focus: {1}, took 2 crystals: {2}, a third: {3}, gives back last in first {4}; alone {5}",
            YesNo(refusedWithCrystal), YesNo(fitted), YesNo(both), YesNo(third), lifoOut, idle);
        logger.Notification("Duets test: with Ice and Earth in the Memory the Focus made clay {0} ({1}), the Memory then holds {2}; fired clay {3}, the Memory holds {4}; clay again {5}; the module's pair in the open, the Memory's shielded: {6}",
            clay, madeClay, afterFirst, fired, afterSecond, clayAgain, YesNo(open));
        TestSite.StorePos(sapi, ModuleFocusKey, focus.Pos);
        TestSite.StorePos(sapi, ModuleMemoryKey, memory.Pos);
        Rest(focus);
    }

    public void Kept()
    {
        var focus = TestSite.ReadPos(sapi, ModuleFocusKey) is { } f ? Accessor.GetBlockEntity(f) as BlockEntityMachine : null;
        var memory = TestSite.ReadPos(sapi, ModuleMemoryKey) is { } m ? Accessor.GetBlockEntity(m) as BlockEntityMemory : null;
        logger.Notification("Duets test: after the restart the Memory lends duet {0}, the Duet Module holds {1} crystal(s) making {2}",
            memory?.Pair?.Product ?? "none", focus?.ModuleCrystals.Count ?? -1,
            focus is { ModuleCrystals.Count: 2 } ? Duet.Name(focus.ModuleCrystals[0], focus.ModuleCrystals[1]) ?? "none" : "none");
    }

    // ------------------------------------------------------------------ the limits on Blooming machines

    public void Bloom(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Ground(origin, 11, 7, 11, "game:soil-medium-none");
        var center = origin.AddCopy(5, -1, 5);
        for (int x = -4; x <= 4; x++)
        {
            for (int z = -4; z <= 4; z++)
            {
                Accessor.SetBlock(Id("game:farmland-moist-medium"), center.AddCopy(x, 0, z));
                Accessor.SetBlock(Id("game:crop-flax-1"), center.AddCopy(x, 1, z));
            }
        }
        var scatter = Place(center.AddCopy(0, 5, 0), Machine.Scatter, "down");
        if (scatter == null)
        {
            return;
        }
        Put(scatter, Crystal.Life, Crystal.Glowing);
        Fill(scatter);
        float before = Total(scatter);
        var result = scatter.WorkOnce();
        float paid = before - Total(scatter);
        int grew = 0;
        for (int x = -4; x <= 4; x++)
        {
            for (int z = -4; z <= 4; z++)
            {
                if (Path(center.AddCopy(x, 1, z)) == "crop-flax-2")
                {
                    grew++;
                }
            }
        }
        Fill(scatter);
        before = Total(scatter);
        var again = scatter.WorkOnce();
        float paidAgain = before - Total(scatter);
        logger.Notification("Duets test: a Glowing Life Scatter over 81 flax {0}, {1} grew a stage, paid {2:0.0} for {3:0} working(s); again that day {4}, paid {5:0.0}",
            result, grew, paid, paid / Machine.WorkCost(Crystal.Glowing, false), again, paidAgain);
        Rest(scatter);
    }

    // ------------------------------------------------------------------ Memory on an Earth drain

    public void Drain(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column).AddCopy(0, -6, 0);
        Solid(origin, 10, 4, 10);
        // A 5×1×5 room of sand; its fill is far enough from the drain that its Earth crystal is no twin near the drain's Sky.
        var corner = origin.AddCopy(3, 1, 3);
        for (int x = 0; x < 5; x++)
        {
            for (int z = 0; z < 5; z++)
            {
                Accessor.SetBlock(Id("game:sand-granite"), corner.AddCopy(x, 0, z));
            }
        }
        var drain = Place(corner.AddCopy(-1, 0, 0), Machine.Gather, "east");
        var fill = Place(corner.AddCopy(5, 0, 4), Machine.Focus, "west");
        if (drain == null || fill == null)
        {
            return;
        }
        var memory = Memory(drain.Pos.UpCopy(), "up");
        var finds = Chest(drain.Pos.NorthCopy());
        // The shelf on the drain's west face, its back to the drain: sand on it, sand in its input (north).
        var shelfPos = drain.Pos.WestCopy();
        var shelf = Shelf(shelfPos, "west");
        var input = Chest(shelfPos.NorthCopy());
        Add(input, Stack("game:sand-granite", 4));
        if (shelf != null && Stack("game:sand-granite") is { } onShelf)
        {
            shelf.Inventory[0].Itemstack = onShelf;
            shelf.Inventory[0].MarkDirty();
        }
        Put(fill, Crystal.Earth, Crystal.Glittering);
        Put(drain, Crystal.Earth, Crystal.Glittering);
        Put(memory, Crystal.Sky, Crystal.Glittering);
        Put(memory, Crystal.Unaligned, Crystal.Glittering);
        Fill(drain);
        drain.ReadContext();
        var turns = new List<string>();
        string Socket() => drain.Crystals.FirstOrDefault()?.Alignment ?? "none";
        turns.Add(Socket());
        drain.WorkBattery(1);
        float capacity = drain.BatteryCapacity;
        bool second = Put(drain, Crystal.Earth, Crystal.Glittering);
        bool shielded = memory != null && memory.Crystals.All(Dialogues.Shielded);
        turns.Add(Socket());
        Fill(drain);
        drain.WorkBattery(1);
        bool panned = Accessor.GetBlock(corner).Id == 0;
        float pannedCapacity = drain.BatteryCapacity;
        int found = finds?.Sum(s => s.StackSize) ?? -1;
        turns.Add(Socket());
        drain.WorkBattery(1);
        bool back = Path(corner) == "sand-granite";
        turns.Add(Socket());
        logger.Notification("Duets test: an Earth drain with a Memory refused a second crystal: {0}, its magazine shielded: {1}; its socket held {2}",
            YesNo(!second), YesNo(shielded), string.Join(", ", turns));
        logger.Notification("Duets test: Sky panned the corner sand: {0}, the room's count {1:0} then {2:0}, finds in the chest: {3}; the shelf put sand back: {4}, sand left in its input {5}, the count {6:0}, the room rests: {7}",
            YesNo(panned), capacity, pannedCapacity, YesNo(found > 0), YesNo(back), CountOf(input, "sand-granite"), drain.BatteryCapacity, drain.BatteryProblem ?? "no");
        Rest(drain);
        Rest(fill);
    }
}
