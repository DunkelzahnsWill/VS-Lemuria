using System.Linq;
using Lemuria.Core;
using Lemuria.Crystals;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;

namespace Lemuria.Storage;

/// <summary>
/// Storage (design note "Lemuria step 2: The Four Elementals", Crystal jars and racks): the personal
/// and shielded crystal jars, carried in a bag slot with three crystal-only slots, and the crystal
/// rack with its sixteen places. A jar's slots and a rack's places keep crystals out of dialogues.
/// Adds <c>/lemuria storage test ...</c> for the smoke test and for admins.
/// </summary>
public class StorageModSystem : ModSystem
{
    private const string TestPosKey = LemuriaModSystem.ModId + "-storagetest";

    private ICoreServerAPI sapi = null!;

    public override void Start(ICoreAPI api)
    {
        api.RegisterCollectibleBehaviorClass(CollectibleBehaviorJarFiller.Name, typeof(CollectibleBehaviorJarFiller));
        api.RegisterBlockClass(BlockCrystalRack.ClassCode, typeof(BlockCrystalRack));
        api.RegisterBlockEntityClass(BlockEntityCrystalRack.ClassCode, typeof(BlockEntityCrystalRack));
        Dialogues.ShieldedInventoryClasses.Add(BlockEntityCrystalRack.InventoryClass);
    }

    public override void StartServerSide(ICoreServerAPI api)
    {
        sapi = api;
        api.Event.SaveGameLoaded += () =>
        {
            int jars = api.World.Items.Count(i => i?.Code?.Domain == LemuriaModSystem.ModId && i.Code.Path.StartsWith("crystaljar-"));
            int racks = api.World.GridRecipes.Where(r => r.Output?.Code?.Domain == LemuriaModSystem.ModId && r.Output.Code.Path.StartsWith("crystalrack-"))
                .Select(r => r.Output!.Code!.Path).Distinct().Count();
            Mod.Logger.Notification("Storage: {0} crystal jar kinds, rack recipes for {1} rock type(s)", jars, racks);
        };
        api.ChatCommands.GetOrCreate(LemuriaModSystem.ModId)
            .WithDescription("Lemuria commands")
            .RequiresPrivilege(Privilege.chat)
            .BeginSubCommand("storage")
                .WithDescription("Storage commands")
                .RequiresPrivilege(Privilege.controlserver)
                .BeginSubCommand("test")
                    .WithDescription("Tests the crystal rack next to the world spawn")
                    .BeginSubCommand("rack")
                        .WithDescription("Sets Fire and Ice crystals side by side on a rack, lets a dialogue tick pass, and reports them")
                        .HandleWith(_ => TestSite.WithChunk(api, Mod.Logger, "Storage", 14, 2, OnTestRack))
                    .EndSubCommand()
                    .BeginSubCommand("check")
                        .WithDescription("Reports the test rack's crystals, after a restart")
                        .HandleWith(_ => TestSite.WithChunk(api, Mod.Logger, "Storage", 14, 2, _ => OnTestCheck()))
                    .EndSubCommand()
                .EndSubCommand()
            .EndSubCommand();
    }

    private void OnTestRack(BlockPos column)
    {
        var pos = TestSite.Surface(sapi, column);
        var accessor = sapi.World.BlockAccessor;
        var rackBlock = sapi.World.GetBlock(new AssetLocation(LemuriaModSystem.ModId, "crystalrack-granite-north"));
        if (rackBlock == null)
        {
            Mod.Logger.Error("Storage test: the rack is not loaded");
            return;
        }
        accessor.SetBlock(sapi.World.GetBlock(new AssetLocation("game:rock-granite"))?.Id ?? 0, pos.DownCopy());
        accessor.SetBlock(rackBlock.Id, pos);
        if (accessor.GetBlockEntity(pos) is not BlockEntityCrystalRack rack)
        {
            Mod.Logger.Error("Storage test: no rack block entity");
            return;
        }
        var fire = new ItemStack(Crystal.BlockFor(sapi.World, Crystal.Fire, Crystal.Glowing));
        Crystal.SetState(fire, 150, 1);
        rack.Put(0, fire);
        rack.Put(1, new ItemStack(Crystal.BlockFor(sapi.World, Crystal.Ice, Crystal.Glittering)));
        TestSite.StorePos(sapi, TestPosKey, pos);
        CrystalsModSystem.Server?.Dialogues.Watch(rack.Inventory);
        CrystalsModSystem.Server?.Dialogues.TickContainersNow(0);
        CrystalsModSystem.Server?.Dialogues.TickContainersNow((float)LemuriaModSystem.Config.DialogueGraceSeconds + 1);
        Mod.Logger.Notification("Storage test: after the warning the rack holds {0}", Describe(rack));
    }

    private void OnTestCheck()
    {
        var pos = TestSite.ReadPos(sapi, TestPosKey);
        if (pos == null || sapi.World.BlockAccessor.GetBlockEntity(pos) is not BlockEntityCrystalRack rack)
        {
            Mod.Logger.Error("Storage test: no test rack; run /lemuria storage test rack first");
            return;
        }
        Mod.Logger.Notification("Storage test: after the restart the rack holds {0}", Describe(rack));
    }

    private static string Describe(BlockEntityCrystalRack rack)
    {
        var parts = rack.Inventory.Where(s => !s.Empty).Select(s =>
        {
            var stack = s.Itemstack!;
            int cracks = Crystal.GetCracks(stack);
            int level = Crystal.Level(Crystal.GetCharge(stack), Crystal.Capacity(Crystal.EvolutionOf(stack.Collectible)));
            return $"{Crystal.AlignmentOf(stack.Collectible)} {Crystal.EvolutionOf(stack.Collectible)} level {level}" + (cracks > 0 ? $" cracked {cracks}" : "");
        });
        return string.Join(", ", parts);
    }
}
