using System.Linq;
using Lemuria.Core;
using Lemuria.Crystals;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;
using Vintagestory.GameContent;

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

    /// <summary>This side's answer to the game's claim test for racks and jars; the server's is the one the test command asks.</summary>
    private StorageClaims claims = null!;

    public override void Start(ICoreAPI api)
    {
        api.RegisterCollectibleBehaviorClass(CollectibleBehaviorJarFiller.Name, typeof(CollectibleBehaviorJarFiller));
        api.RegisterBlockClass(BlockCrystalRack.ClassCode, typeof(BlockCrystalRack));
        api.RegisterBlockEntityClass(BlockEntityCrystalRack.ClassCode, typeof(BlockEntityCrystalRack));
        Dialogues.ShieldedInventoryClasses.Add(BlockEntityCrystalRack.InventoryClass);
        // With the claims switch off, racks and jars inside a claim are open to everyone (the owner, 2026-11-10).
        claims = new StorageClaims(api);
        api.Event.OnTestBlockAccess += claims.OnTestBlockAccess;
    }

    public override void StartServerSide(ICoreServerAPI api)
    {
        sapi = api;
        api.Event.SaveGameLoaded += () =>
        {
            int jars = api.World.Items.Count(i => i?.Code?.Domain == LemuriaModSystem.ModId && i.Code.Path.StartsWith("crystaljar-"));
            // Racks are carved from polished slabs with the Crystalline Chisel (Sparkstone), not made on the grid.
            int racks = api.World.Blocks.Count(b => b?.Code?.Domain == LemuriaModSystem.ModId && b.Code.Path.StartsWith("crystalrack-") && b.Code.Path.EndsWith("-north"));
            Mod.Logger.Notification("Storage: {0} crystal jar kinds, racks in {1} rock type(s)", jars, racks);
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
                    .BeginSubCommand("claims")
                        .WithDescription("Asks the game's claim test about a rack and a jar set down on the ground, with the claims switch on and off")
                        .HandleWith(_ => TestSite.WithChunk(api, Mod.Logger, "Storage", 14, 8, OnTestClaims))
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

    /// <summary>
    /// The claims switch for racks and jars (step 3 note, Claims and players; the owner, 2026-11-10). The
    /// console has no player and the test world no claim, so this hands the game's own claim test a
    /// claim's refusal, as the game does for a stranger, and reports what comes back; the parts of a
    /// ground storage spot are asked of <see cref="StorageClaims"/> directly, for made-up players.
    /// </summary>
    private void OnTestClaims(BlockPos column)
    {
        var world = sapi.World;
        var accessor = world.BlockAccessor;
        var rackPos = TestSite.Surface(sapi, column);
        var spot = rackPos.AddCopy(2, 0, 0);
        var lone = rackPos.AddCopy(4, 0, 0);
        ItemStack? Stack(string code) =>
            world.GetItem(new AssetLocation(code)) is { } item ? new ItemStack(item) : world.GetBlock(new AssetLocation(code)) is { } block ? new ItemStack(block) : null;
        var rackBlock = world.GetBlock(new AssetLocation(LemuriaModSystem.ModId, "crystalrack-granite-north"));
        var groundStorage = world.GetBlock(new AssetLocation("game:groundstorage"));
        int granite = world.GetBlock(new AssetLocation("game:rock-granite"))?.Id ?? 0;
        var jar = Stack(LemuriaModSystem.ModId + ":crystaljar-personal-fired");
        var crock = Stack("game:crock-blue-raw");
        var bowl = Stack("game:bowl-blue-fired");
        var seedling = Stack(LemuriaModSystem.ModId + ":seedling-glittering");
        var sparkstone = Stack(LemuriaModSystem.ModId + ":sparkstone-awakened-granite");
        var trigger = sapi.Event.GetType().GetMethod("TriggerTestBlockAccess");
        if (rackBlock == null || groundStorage == null || jar == null || crock == null || bowl == null || seedling == null || sparkstone == null || trigger == null)
        {
            Mod.Logger.Error("Storage test: the rack, the ground storage, a jar, a crock, a bowl, a seedling, sparkstone or the game's claim test is missing");
            return;
        }
        foreach (var pos in new[] { rackPos, spot, lone })
        {
            accessor.SetBlock(granite, pos.DownCopy());
        }
        accessor.SetBlock(rackBlock.Id, rackPos);
        BlockEntityGroundStorage? SetDown(BlockPos pos, params ItemStack?[] stacks)
        {
            accessor.SetBlock(groundStorage.Id, pos);
            if (accessor.GetBlockEntity(pos) is not BlockEntityGroundStorage storage)
            {
                return null;
            }
            for (int i = 0; i < stacks.Length; i++)
            {
                storage.Inventory[i].Itemstack = stacks[i]?.Clone();
                storage.Inventory[i].MarkDirty();
            }
            storage.DetermineStorageProperties(null);
            storage.MarkDirty(true);
            return storage;
        }
        // The spot's four parts as the game numbers them: west then east, north before south.
        var mixed = SetDown(spot, jar, crock, bowl);
        var single = SetDown(lone, jar);
        if (mixed == null || single == null)
        {
            Mod.Logger.Error("Storage test: no ground storage block entity");
            return;
        }
        BlockSelection Whole(BlockPos pos) => new() { Position = pos.Copy() };
        BlockSelection Part(BlockPos pos, int part) => new()
        {
            Position = pos.Copy(),
            Face = BlockFacing.UP,
            HitPosition = new Vec3d(part >= 2 ? 0.75 : 0.25, 0.1, part % 2 == 1 ? 0.75 : 0.25),
        };
        // The game's own test, as it ends for a stranger in a claim: refused by the claim, then every mod's answer.
        EnumWorldAccessResponse Game(BlockSelection sel, EnumBlockAccessFlags access) =>
            (EnumWorldAccessResponse)trigger.Invoke(sapi.Event, new object?[] { null, sel, access, "the owner", null, EnumWorldAccessResponse.LandClaimed })!;
        bool before = LemuriaClaims.Respect;
        try
        {
            LemuriaClaims.Respect = true;
            var respected = Game(Whole(rackPos), EnumBlockAccessFlags.Use);
            var jarRespected = Game(Part(spot, 0), EnumBlockAccessFlags.Use);
            LemuriaClaims.Respect = false;
            Mod.Logger.Notification("Storage test: with claims respected the game leaves a rack {0} and a jar on the ground {1}; with claims ignored using the rack is {2}, breaking it {3}, using the jar {4}, breaking it {5}",
                respected, jarRespected, Game(Whole(rackPos), EnumBlockAccessFlags.Use), Game(Whole(rackPos), EnumBlockAccessFlags.BuildOrBreak),
                Game(Part(spot, 0), EnumBlockAccessFlags.Use), Game(Part(spot, 0), EnumBlockAccessFlags.BuildOrBreak));
        }
        finally
        {
            LemuriaClaims.Respect = before;
        }
        string YesNo(bool value) => value ? "yes" : "no";
        var asked = new StorageClaims(sapi);
        long now = world.ElapsedMilliseconds;
        bool jarPart = asked.Opens(world, "anna", null, Part(spot, 0), now);
        bool ownCheck = asked.Opens(world, "anna", null, Whole(spot), now);
        bool othersCheck = asked.Opens(world, "bert", null, Whole(spot), now);
        bool bowlPart = asked.Opens(world, "anna", null, Part(spot, 2), now);
        bool crockWithSeedling = asked.Opens(world, "cleo", seedling, Part(spot, 1), now);
        bool crockWithSparkstone = asked.Opens(world, "emil", sparkstone, Part(spot, 1), now);
        bool crockEmptyHanded = asked.Opens(world, "dora", null, Part(spot, 1), now);
        bool crockWithBowl = asked.Opens(world, "finn", bowl, Part(spot, 1), now);
        Mod.Logger.Notification("Storage test: a spot with a jar, a crock and a bowl opens the jar's part: {0}, the bowl's: {1}, the crock's with a seedling in hand: {2}, with Awakened sparkstone: {3}, with an empty hand: {4}, with a bowl: {5}",
            YesNo(jarPart), YesNo(bowlPart), YesNo(crockWithSeedling), YesNo(crockWithSparkstone), YesNo(crockEmptyHanded), YesNo(crockWithBowl));
        // Anna takes the jar: the end of her click is tested once more, on a part that is now empty.
        mixed.Inventory[0].Itemstack = null;
        mixed.Inventory[0].MarkDirty();
        bool endOfClick = asked.Opens(world, "anna", null, Part(spot, 0), now + 100);
        bool bowlAfter = asked.Opens(world, "anna", null, Part(spot, 2), now + 100);
        bool later = asked.Opens(world, "anna", null, Part(spot, 0), now + StorageClaims.FollowMs + 1);
        // A spot that holds only a jar goes with it and leaves air; a block set there since is not opened.
        bool loneJar = asked.Opens(world, "anna", null, Part(lone, 0), now);
        accessor.SetBlock(0, lone);
        bool gone = asked.Opens(world, "anna", null, Part(lone, 0), now + 100);
        accessor.SetBlock(granite, lone);
        bool replaced = asked.Opens(world, "anna", null, Part(lone, 0), now + 100);
        Mod.Logger.Notification("Storage test: the click on the jar passes the spot's own check: {0}, another player's: {1}; with the jar taken the end of the click passes: {2}, the bowl's part: {3}, {4} ms later: {5}; a lone jar: {6}, the end of its click on the air it left: {7}, on a block set there since: {8}",
            YesNo(ownCheck), YesNo(othersCheck), YesNo(endOfClick), YesNo(bowlAfter), StorageClaims.FollowMs + 1, YesNo(later), YesNo(loneJar), YesNo(gone), YesNo(replaced));
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
