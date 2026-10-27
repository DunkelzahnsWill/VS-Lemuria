using System;
using System.Linq;
using Lemuria.Core;
using Lemuria.Crystals;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;
using Vintagestory.GameContent;

namespace Lemuria.Cradle;

/// <summary>
/// The cradle: a carved stone basin where a seedling grows through five stages to a ripe Unaligned
/// crystal (design note "Lemuria step 1: Find and farm", The cradle and growth), and where an
/// Unaligned crystal refines, an aligned one charges and a cracked one mends (design note "Lemuria
/// step 2: The Four Elementals", Refining, The evolution ring, Cracks and repair).
/// Adds <c>/lemuria cradle test ...</c> for the smoke test and for admins.
/// </summary>
public class CradleModSystem : ModSystem
{
    /// <summary>Savegame data: where the test cradles stand, so a second boot can find them again.</summary>
    private const string TestPosKey = LemuriaModSystem.ModId + "-cradletest";
    private const string RefinePosKey = LemuriaModSystem.ModId + "-cradletest-refine";
    private const string HalfPosKey = LemuriaModSystem.ModId + "-cradletest-half";
    private const string TestRock = "granite";
    private const string TestSeedling = LemuriaModSystem.ModId + ":seedling-glittering";

    private ICoreServerAPI sapi = null!;

    public override void Start(ICoreAPI api)
    {
        api.RegisterBlockClass(BlockCradle.ClassCode, typeof(BlockCradle));
        api.RegisterBlockEntityClass(BlockEntityCradle.ClassCode, typeof(BlockEntityCradle));
    }

    public override void StartServerSide(ICoreServerAPI api)
    {
        sapi = api;
        // Cradles are carved from placed polished rock with the Crystalline Chisel (Sparkstone), never made on the grid.
        api.Event.SaveGameLoaded += () => Mod.Logger.Notification("Cradle: cradles in {0} rock type(s), {1} grid recipe(s)",
            api.World.Blocks.Count(b => b is BlockCradle),
            api.World.GridRecipes.Count(r => r.Output?.Code?.Domain == LemuriaModSystem.ModId && r.Output.Code.Path.StartsWith("cradle-")));
        var parsers = api.ChatCommands.Parsers;
        // The shared root needs a privilege of its own; each feature's subcommand sets the one it needs.
        api.ChatCommands.GetOrCreate(LemuriaModSystem.ModId)
            .WithDescription("Lemuria commands")
            .RequiresPrivilege(Privilege.chat)
            .BeginSubCommand("cradle")
                .WithDescription("Cradle commands")
                .RequiresPrivilege(Privilege.controlserver)
                .BeginSubCommand("test")
                    .WithDescription("Tests cradles next to the world spawn: growth, refining, the evolution ring and repair")
                    .BeginSubCommand("plant")
                        .WithDescription("Places a granite cradle at the world spawn and plants a seedling in it")
                        .HandleWith(_ => Test(2, 2, OnTestPlant))
                    .EndSubCommand()
                    .BeginSubCommand("advance")
                        .WithDescription("Moves the world's time on by this many in-game days, then lets the test cradle catch up")
                        .WithArgs(parsers.Double("days"))
                        .HandleWith(args => Test(2, 2, _ => OnTestAdvance((double)args[0])))
                    .EndSubCommand()
                    .BeginSubCommand("check")
                        .WithDescription("Reports the test cradles' stage, crystals and refining progress")
                        .HandleWith(_ => Test(2, 2, _ => OnTestCheck()))
                    .EndSubCommand()
                    .BeginSubCommand("refine")
                        .WithDescription("Sets Unaligned crystals in two cradles beside burning firepits, and refines one fully and one a quarter")
                        .HandleWith(_ => Test(2, 6, OnTestRefine))
                    .EndSubCommand()
                    .BeginSubCommand("ring")
                        .WithDescription("Plants a seedling inside a ring of two Fire crystals, ripens it, and reports its Evolution")
                        .HandleWith(_ => Test(2, 10, OnTestRing))
                    .EndSubCommand()
                    .BeginSubCommand("mend")
                        .WithDescription("Sets a twice-cracked crystal in a cradle, moves time on two stages, and reports its cracks")
                        .HandleWith(_ => Test(2, 14, OnTestMend))
                    .EndSubCommand()
                .EndSubCommand()
            .EndSubCommand();
    }

    private TextCommandResult Test(int dx, int dz, Action<BlockPos> action) => TestSite.WithChunk(sapi, Mod.Logger, "Cradle", dx, dz, action);

    private BlockEntityCradle? PlaceCradle(BlockPos pos)
    {
        var accessor = sapi.World.BlockAccessor;
        var cradleBlock = sapi.World.GetBlock(new AssetLocation(LemuriaModSystem.ModId, "cradle-" + TestRock));
        if (cradleBlock == null)
        {
            Mod.Logger.Error("Cradle test: the cradle is not loaded");
            return null;
        }
        // A test cradle stands on a granite block, so it always has a solid block below.
        accessor.SetBlock(sapi.World.GetBlock(new AssetLocation("game:rock-" + TestRock))?.Id ?? 0, pos.DownCopy());
        accessor.SetBlock(cradleBlock.Id, pos);
        if (accessor.GetBlockEntity(pos) is not BlockEntityCradle cradle)
        {
            Mod.Logger.Error("Cradle test: no cradle block entity at {0}", TestSite.Describe(sapi, pos));
            return null;
        }
        return cradle;
    }

    private void OnTestPlant(BlockPos column)
    {
        var pos = TestSite.Surface(sapi, column);
        var seedling = sapi.World.GetItem(new AssetLocation(TestSeedling));
        var cradle = PlaceCradle(pos);
        if (cradle == null || seedling == null)
        {
            return;
        }
        cradle.Plant(seedling.Code.ToString());
        TestSite.StorePos(sapi, TestPosKey, pos);
        Mod.Logger.Notification("Cradle test: planted {0} in {1} at {2}, stage {3}", seedling.Code, cradle.Block.Code, TestSite.Describe(sapi, pos), StageName(cradle.Stage));
    }

    private BlockEntityCradle? Find(string key, string what)
    {
        var pos = TestSite.ReadPos(sapi, key);
        if (pos == null)
        {
            Mod.Logger.Error("Cradle test: no {0} cradle yet", what);
            return null;
        }
        if (sapi.World.BlockAccessor.GetBlockEntity(pos) is not BlockEntityCradle cradle)
        {
            Mod.Logger.Error("Cradle test: no cradle at {0}", TestSite.Describe(sapi, pos));
            return null;
        }
        return cradle;
    }

    private void OnTestAdvance(double days)
    {
        var cradle = Find(TestPosKey, "test; run /lemuria cradle test plant first");
        if (cradle == null)
        {
            return;
        }
        sapi.World.Calendar.Add((float)(days * sapi.World.Calendar.HoursPerDay));
        cradle.Advance();
        Mod.Logger.Notification("Cradle test: time moved on {0:0.##} days, stage {1}", days, StageName(cradle.Stage));
    }

    private void OnTestCheck()
    {
        var cradle = Find(TestPosKey, "test; run /lemuria cradle test plant first");
        if (cradle != null)
        {
            cradle.Advance();
            Mod.Logger.Notification("Cradle test: the cradle at {0} is at stage {1}", TestSite.Describe(sapi, cradle.Pos), StageName(cradle.Stage));
        }
        var refined = Find(RefinePosKey, "refine test");
        if (refined != null)
        {
            Mod.Logger.Notification("Cradle test: the refining cradle holds {0}", Describe(refined.CrystalStack));
        }
        var half = Find(HalfPosKey, "refine test");
        if (half != null)
        {
            Mod.Logger.Notification("Cradle test: the second refining cradle holds {0}, {1:0.00} of the way to {2}", Describe(half.CrystalStack), half.RefineProgress, half.RefineTarget ?? "nothing");
        }
    }

    private static string Describe(ItemStack? stack)
    {
        if (stack == null || !Crystal.IsCrystal(stack.Collectible))
        {
            return "nothing";
        }
        int cracks = Crystal.GetCracks(stack);
        return $"{Crystal.AlignmentOf(stack.Collectible)} {Crystal.EvolutionOf(stack.Collectible)}" + (cracks > 0 ? $" cracked {cracks}" : "");
    }

    /// <summary>
    /// A burning firepit beside a position, east of it, on a granite block, under a granite roof two
    /// blocks up, so the open sky adds nothing to the reading.
    /// </summary>
    private void Firepit(BlockPos pos)
    {
        var accessor = sapi.World.BlockAccessor;
        int granite = sapi.World.GetBlock(new AssetLocation("game:rock-" + TestRock))?.Id ?? 0;
        for (int dx = -1; dx <= 2; dx++)
        {
            for (int dz = -1; dz <= 1; dz++)
            {
                accessor.SetBlock(0, pos.AddCopy(dx, 1, dz));
                accessor.SetBlock(granite, pos.AddCopy(dx, 2, dz));
            }
        }
        var at = pos.EastCopy();
        accessor.SetBlock(sapi.World.GetBlock(new AssetLocation("game:rock-" + TestRock))?.Id ?? 0, at.DownCopy());
        accessor.SetBlock(sapi.World.GetBlock(new AssetLocation("game:firepit-cold"))?.Id ?? 0, at);
        if (accessor.GetBlockEntity(at) is BlockEntityFirepit firepit)
        {
            firepit.igniteWithFuel(new ItemStack(sapi.World.GetItem(new AssetLocation("game:firewood")), 1));
        }
    }

    private void OnTestRefine(BlockPos column)
    {
        var unaligned = Crystal.BlockFor(sapi.World, Crystal.Unaligned, Crystal.Glittering);
        if (unaligned == null)
        {
            return;
        }
        var hours = sapi.World.Calendar.HoursPerDay;
        var pos = TestSite.Surface(sapi, column);
        var cradle = PlaceCradle(pos);
        if (cradle == null)
        {
            return;
        }
        Firepit(pos);
        var stack = new ItemStack(unaligned);
        Crystal.SetState(stack, 0, 1);
        cradle.SetCrystal(stack);
        // A full day and a bit at the firepit's strength refines a Glittering crystal.
        cradle.RefineFor(hours * 1.5);
        TestSite.StorePos(sapi, RefinePosKey, pos);
        Mod.Logger.Notification("Cradle test: after a day and a half beside a burning firepit the cradle holds {0}", Describe(cradle.CrystalStack));

        var halfPos = pos.AddCopy(0, 0, 2);
        var half = PlaceCradle(halfPos);
        if (half == null)
        {
            return;
        }
        Firepit(halfPos);
        half.SetCrystal(new ItemStack(unaligned));
        half.RefineFor(hours / 4);
        TestSite.StorePos(sapi, HalfPosKey, halfPos);
        Mod.Logger.Notification("Cradle test: after a quarter day the second cradle is {0:0.00} of the way to {1}", half.RefineProgress, half.RefineTarget ?? "nothing");
    }

    private void OnTestRing(BlockPos column)
    {
        var pos = TestSite.Surface(sapi, column);
        var accessor = sapi.World.BlockAccessor;
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dz = -1; dz <= 1; dz++)
            {
                accessor.SetBlock(0, pos.AddCopy(dx, 0, dz));
                accessor.SetBlock(sapi.World.GetBlock(new AssetLocation("game:rock-" + TestRock))?.Id ?? 0, pos.AddCopy(dx, -1, dz));
            }
        }
        var seedling = sapi.World.GetItem(new AssetLocation(TestSeedling));
        var fire = Crystal.BlockFor(sapi.World, Crystal.Fire, Crystal.Glittering);
        var cradle = PlaceCradle(pos);
        if (cradle == null || seedling == null || fire == null)
        {
            return;
        }
        // Two Fire crystals, the same alignment twice, west and east of the cradle.
        accessor.SetBlock(fire.Id, pos.WestCopy());
        accessor.SetBlock(fire.Id, pos.EastCopy());
        cradle.Plant(seedling.Code.ToString());
        sapi.World.Calendar.Add((float)(11.5 * sapi.World.Calendar.HoursPerDay));
        cradle.Advance();
        Mod.Logger.Notification("Cradle test: inside a ring of {0} the seedling is at stage {1} and gives {2}",
            string.Join(" and ", cradle.RingAlignments(Crystal.Glittering)), StageName(cradle.Stage), Describe(cradle.ContentStack()));
    }

    private void OnTestMend(BlockPos column)
    {
        var pos = TestSite.Surface(sapi, column);
        var cradle = PlaceCradle(pos);
        var ice = Crystal.BlockFor(sapi.World, Crystal.Ice, Crystal.Glittering);
        if (cradle == null || ice == null)
        {
            return;
        }
        var stack = new ItemStack(ice);
        Crystal.SetState(stack, 0, 2);
        cradle.SetCrystal(stack);
        sapi.World.Calendar.Add((float)(2.5 * sapi.World.Calendar.HoursPerDay));
        cradle.Mend();
        string afterOne = Describe(cradle.CrystalStack);
        sapi.World.Calendar.Add((float)(2.5 * sapi.World.Calendar.HoursPerDay));
        cradle.Mend();
        Mod.Logger.Notification("Cradle test: a twice-cracked Ice crystal is {0} after one stage and {1} after two", afterOne, Describe(cradle.CrystalStack));
    }

    private static string StageName(int stage) => stage switch
    {
        BlockEntityCradle.Empty => "empty",
        BlockEntityCradle.Ripe => "ripe",
        _ => stage.ToString(),
    };
}
