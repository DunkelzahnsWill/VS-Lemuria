using System.Linq;
using Lemuria.Core;
using Lemuria.Crystals;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;

namespace Lemuria.Sparkstone;

/// <summary>
/// Sparkstone (design note "Lemuria step 2: The Four Elementals", The Crystalline Chisel, Awakened sparkstone
/// and touching charge, Glyphs): the Crystalline Chisel, Awakened sparkstone, its networks and glyphs. In
/// step 2 a network stores charge for the tool; machines that spend it come in step 4.
/// Adds <c>/lemuria sparkstone test ...</c> for the smoke test and for admins.
/// </summary>
public class SparkstoneModSystem : ModSystem
{
    private const string TestPosKey = LemuriaModSystem.ModId + "-sparkstonetest";

    /// <summary>The loaded Awakened sparkstone and its networks. Server only; null on a client.</summary>
    public static SparkstoneNetworks? Networks { get; private set; }

    private ICoreServerAPI sapi = null!;

    public override void Start(ICoreAPI api)
    {
        api.RegisterItemClass(ItemCrystalChisel.ClassCode, typeof(ItemCrystalChisel));
        api.RegisterBlockClass(BlockSparkstone.ClassCode, typeof(BlockSparkstone));
        api.RegisterBlockEntityClass(BlockEntitySparkstone.ClassCode, typeof(BlockEntitySparkstone));
    }

    public override void StartServerSide(ICoreServerAPI api)
    {
        sapi = api;
        Networks = new SparkstoneNetworks(api);
        api.Event.RegisterGameTickListener(_ => Networks.Tick(), SparkstoneNetworks.TickMs, 3000);
        api.Event.SaveGameLoaded += () =>
        {
            int stones = api.World.Blocks.Count(b => b?.Code?.Domain == LemuriaModSystem.ModId && b.Code.Path.StartsWith("sparkstone-awakened-"));
            int tools = api.World.GridRecipes.Count(r => r.Output?.Code?.ToString() == LemuriaModSystem.ModId + ":crystalchisel");
            int racks = api.World.Blocks.Count(b => b?.Code?.Domain == LemuriaModSystem.ModId && b.Code.Path.StartsWith("crystalrack-") && b.Code.Path.EndsWith("-north"));
            Mod.Logger.Notification("Sparkstone: Awakened sparkstone in {0} rock type(s), {1} Crystalline Chisel grid recipe(s), racks in {2} rock type(s)", stones, tools, racks);
        };
        api.ChatCommands.GetOrCreate(LemuriaModSystem.ModId)
            .WithDescription("Lemuria commands")
            .RequiresPrivilege(Privilege.chat)
            .BeginSubCommand("sparkstone")
                .WithDescription("Sparkstone commands")
                .RequiresPrivilege(Privilege.controlserver)
                .BeginSubCommand("test")
                    .WithDescription("Tests awakening and networks next to the world spawn")
                    .BeginSubCommand("awaken")
                        .WithDescription("Awakens two polished granite blocks with a full Crystalline Chisel, feeds them from a full crystal, and reports the network")
                        .HandleWith(_ => TestSite.WithChunk(api, Mod.Logger, "Sparkstone", 18, 2, OnTestAwaken))
                    .EndSubCommand()
                    .BeginSubCommand("carve")
                        .WithDescription("Carves a polished granite block into a cradle and a polished granite slab into a crystal rack")
                        .HandleWith(_ => TestSite.WithChunk(api, Mod.Logger, "Sparkstone", 22, 2, OnTestCarve))
                    .EndSubCommand()
                    .BeginSubCommand("check")
                        .WithDescription("Reports the test network's charge, after a restart")
                        .HandleWith(_ => TestSite.WithChunk(api, Mod.Logger, "Sparkstone", 18, 2, _ => OnTestCheck()))
                    .EndSubCommand()
                .EndSubCommand()
            .EndSubCommand();
    }

    private void OnTestAwaken(BlockPos column)
    {
        var pos = TestSite.Surface(sapi, column);
        var accessor = sapi.World.BlockAccessor;
        var polished = sapi.World.GetBlock(new AssetLocation("game:rockpolished-granite"));
        var toolItem = sapi.World.GetItem(new AssetLocation(LemuriaModSystem.ModId, "crystalchisel")) as ItemCrystalChisel;
        if (polished == null || toolItem == null || Networks == null)
        {
            Mod.Logger.Error("Sparkstone test: polished granite or the Crystalline Chisel is missing");
            return;
        }
        accessor.SetBlock(0, pos.UpCopy());
        accessor.SetBlock(0, pos.EastCopy().UpCopy());
        accessor.SetBlock(polished.Id, pos);
        accessor.SetBlock(polished.Id, pos.EastCopy());
        var tool = new ItemStack(toolItem);
        ItemCrystalChisel.SetCharge(tool, ItemCrystalChisel.Capacity);
        var slot = new DummySlot(tool);
        // The console has no player, so there is no claim to check.
        bool first = toolItem.Awaken(slot, sapi.World, null, pos);
        bool second = toolItem.Awaken(slot, sapi.World, null, pos.EastCopy());
        float left = ItemCrystalChisel.GetCharge(tool);
        // A full Fire crystal on the first block feeds the network on its next tick.
        var fire = Crystal.BlockFor(sapi.World, Crystal.Fire, Crystal.Glittering);
        if (fire != null)
        {
            var stack = new ItemStack(fire);
            Crystal.SetState(stack, Crystal.Capacity(Crystal.Glittering), 0);
            accessor.SetBlock(fire.Id, pos.UpCopy(), stack);
        }
        Networks.Tick();
        TestSite.StorePos(sapi, TestPosKey, pos);
        if (accessor.GetBlockEntity(pos) is not BlockEntitySparkstone stone)
        {
            Mod.Logger.Error("Sparkstone test: no sparkstone at {0}", TestSite.Describe(sapi, pos));
            return;
        }
        var network = Networks.NetworkOf(stone);
        var (total, capacity) = SparkstoneNetworks.Measure(network);
        float crystalLeft = (accessor.GetBlockEntity(pos.UpCopy()) as BlockEntityCrystal)?.Charge ?? -1;
        Mod.Logger.Notification("Sparkstone test: awakened {0} and {1}, the tool has {2:0} left; the network of {3} block(s) holds {4:0} of {5:0}, level {6}, and the crystal has {7:0} left",
            first ? "one" : "none", second ? "two" : "no second", left, network.Count, total, capacity, stone.Level, crystalLeft);
    }

    private void OnTestCarve(BlockPos column)
    {
        var pos = TestSite.Surface(sapi, column);
        var accessor = sapi.World.BlockAccessor;
        var polished = sapi.World.GetBlock(new AssetLocation("game:rockpolished-granite"));
        var slab = sapi.World.GetBlock(new AssetLocation("game:polishedrockslab-granite-down-free"));
        if (polished == null || slab == null)
        {
            Mod.Logger.Error("Sparkstone test: polished granite or its slab is missing");
            return;
        }
        accessor.SetBlock(0, pos.UpCopy());
        accessor.SetBlock(0, pos.EastCopy().UpCopy());
        accessor.SetBlock(polished.Id, pos.UpCopy());
        accessor.SetBlock(slab.Id, pos.EastCopy().UpCopy());
        // The console has no player, so there is no claim to check and the rack faces north.
        bool cradle = ItemCrystalChisel.Carve(sapi.World, null, pos.UpCopy());
        bool rack = ItemCrystalChisel.Carve(sapi.World, null, pos.EastCopy().UpCopy());
        Mod.Logger.Notification("Sparkstone test: the chisel carved {0} and {1}",
            cradle ? accessor.GetBlock(pos.UpCopy()).Code.Path : "no cradle", rack ? accessor.GetBlock(pos.EastCopy().UpCopy()).Code.Path : "no rack");
    }

    private void OnTestCheck()
    {
        var pos = TestSite.ReadPos(sapi, TestPosKey);
        if (pos == null || Networks == null || sapi.World.BlockAccessor.GetBlockEntity(pos) is not BlockEntitySparkstone stone)
        {
            Mod.Logger.Error("Sparkstone test: no test network; run /lemuria sparkstone test awaken first");
            return;
        }
        var network = Networks.NetworkOf(stone);
        var (total, capacity) = SparkstoneNetworks.Measure(network);
        Mod.Logger.Notification("Sparkstone test: after the restart the network of {0} block(s) holds {1:0} of {2:0}, level {3}", network.Count, total, capacity, stone.Level);
    }

    public override void Dispose()
    {
        if (sapi != null)
        {
            Networks?.Clear();
            Networks = null;
        }
    }
}
