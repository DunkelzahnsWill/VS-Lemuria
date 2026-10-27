using System;
using System.Collections.Generic;
using System.Linq;
using Lemuria.Core;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;
using Vintagestory.GameContent;

namespace Lemuria.Crystals;

/// <summary>
/// Crystals: the strange glowing crystal found in vugs, the seedlings ground from it, and the crystal
/// blocks (design note "Lemuria step 1: Find and farm", Finding and Grinding), now in five alignments
/// and two Evolutions, with charge, the four environments, twins and dialogues, each alignment's note
/// and the precursor crystals in ruin loot (design note "Lemuria step 2: The Four Elementals").
/// The find listens to the game's own block events and lightning to the game's own event; the only
/// vanilla file touched is the loot vessel's loot lists, by JSON patch.
/// Adds <c>/lemuria crystals test ...</c> for the smoke test and for admins.
/// </summary>
public class CrystalsModSystem : ModSystem
{
    public const string StrangeCrystalCode = LemuriaModSystem.ModId + ":strangecrystal";
    public const string ChannelName = LemuriaModSystem.ModId + "-crystals";

    /// <summary>Server-only chunk data: the geode crystals players placed in that chunk, which never give a find.</summary>
    private const string PlacedKey = LemuriaModSystem.ModId + "-placedgeodecrystals";

    /// <summary>
    /// The running instances, one per side (a single-player game runs both in one process); block
    /// entities reach the dialogue watch through the server's and the hums through the client's.
    /// </summary>
    public static CrystalsModSystem? Server { get; private set; }
    public static CrystalsModSystem? Client { get; private set; }

    /// <summary>The standing crystals of the loaded world. Server only; null on a client.</summary>
    public static StandingCrystals? Standing { get; private set; }

    private ICoreServerAPI sapi = null!;
    private IServerNetworkChannel? serverChannel;
    private Item? strangeCrystal;

    public Dialogues Dialogues { get; private set; } = null!;

    /// <summary>The client's discordant hums; null on a dedicated server.</summary>
    public DiscordantHums? Hums { get; private set; }

    public override void Start(ICoreAPI api)
    {
        api.RegisterBlockClass(BlockLemuriaCrystal.ClassCode, typeof(BlockLemuriaCrystal));
        api.RegisterBlockEntityClass(BlockEntityCrystal.ClassCode, typeof(BlockEntityCrystal));
        api.Network.RegisterChannel(ChannelName).RegisterMessageType<DiscordPacket>();
    }

    public override void AssetsFinalize(ICoreAPI api)
    {
        if (api.Side != EnumAppSide.Server)
        {
            return;
        }
        // The quern's three seedlings come from the JSON; the server's config may ask for another count.
        int seedlings = Math.Clamp(LemuriaModSystem.Config.SeedlingsPerCrystal, 1, 64);
        int grindable = 0;
        foreach (var collectible in api.World.Collectibles)
        {
            if (collectible?.Code?.Domain != LemuriaModSystem.ModId || collectible.GrindingProps?.GroundStack == null)
            {
                continue;
            }
            collectible.GrindingProps.GroundStack.StackSize = seedlings;
            if (collectible.GrindingProps.GroundStack.ResolvedItemstack != null)
            {
                collectible.GrindingProps.GroundStack.ResolvedItemstack.StackSize = seedlings;
            }
            grindable++;
        }
        Mod.Logger.Notification("Crystals: {0} crystals grind into {1} seedlings each", grindable, seedlings);
    }

    public override void StartServerSide(ICoreServerAPI api)
    {
        sapi = api;
        Server = this;
        Standing = new StandingCrystals();
        Dialogues = new Dialogues(api, Standing, this);
        serverChannel = api.Network.GetChannel(ChannelName);
        api.Event.DidPlaceBlock += OnDidPlaceBlock;
        api.Event.DidBreakBlock += OnDidBreakBlock;
        api.Event.RegisterGameTickListener(_ => Dialogues.Tick(), Dialogues.TickMs, 1000);
        var weather = api.ModLoader.GetModSystem<WeatherSystemServer>();
        if (weather != null)
        {
            weather.OnLightningImpactBegin += OnLightning;
        }
        api.Event.SaveGameLoaded += () =>
        {
            strangeCrystal = api.World.GetItem(new AssetLocation(StrangeCrystalCode));
            if (strangeCrystal == null)
            {
                Mod.Logger.Error("Crystals: the item {0} is missing, so geodes give no finds", StrangeCrystalCode);
                return;
            }
            var config = LemuriaModSystem.Config;
            Mod.Logger.Notification("Crystals: geode find chance {0:0.###} per small crystal, {1:0.###} per large crystal or cluster",
                Math.Clamp(config.FindChanceSmall, 0, 1), Math.Clamp(config.FindChanceLarge, 0, 1));
            int crystals = api.World.Blocks.Count(b => b is BlockLemuriaCrystal && b.Variant["position"] == "up");
            int aligned = api.World.Blocks.Count(b => Crystal.IsAligned(b) && b.Variant["position"] == "up");
            Mod.Logger.Notification("Crystals: {0} crystal kinds, {1} of them aligned, in {2} Evolutions", crystals, aligned,
                api.World.Blocks.Where(b => b is BlockLemuriaCrystal).Select(b => b.Variant["evolution"]).Distinct().Count());
            LogLoot();
        };
        RegisterTestCommands(api);
    }

    public override void StartClientSide(ICoreClientAPI api)
    {
        Client = this;
        Hums = new DiscordantHums(api);
        api.Network.GetChannel(ChannelName).SetMessageHandler<DiscordPacket>(packet => Hums.OnPacket(packet));
    }

    /// <summary>Starts or stops a discordant hum at a container or around a player, on every client.</summary>
    public void SendDiscord(string key, bool on, Vec3d? pos, long entityId, float elapsed)
    {
        serverChannel?.BroadcastPacket(new DiscordPacket
        {
            Key = key,
            On = on,
            X = pos?.X ?? 0,
            Y = pos?.Y ?? 0,
            Z = pos?.Z ?? 0,
            EntityId = entityId,
            Elapsed = elapsed,
            Grace = (float)LemuriaModSystem.Config.DialogueGraceSeconds,
        });
    }

    /// <summary>A shattering crystal's burst of colour; particles spawned on the server reach the players nearby.</summary>
    public void SendBurst(Vec3d center, string alignment)
    {
        if (sapi != null)
        {
            CrystalCharging.Burst(sapi.World, center, Crystal.Rgb(alignment));
        }
    }

    private void OnLightning(ref Vec3d impactPos, ref EnumHandling handling)
    {
        if (Standing == null)
        {
            return;
        }
        var pos = impactPos.AsBlockPos;
        foreach (var crystal in Standing.Near(pos, 16))
        {
            crystal.OnLightning();
        }
    }

    private void LogLoot()
    {
        int entries = 0;
        foreach (string type in new[] { "ore", "tool" })
        {
            var block = sapi.World.GetBlock(new AssetLocation("game:lootvessel-" + type));
            var list = block?.Attributes?["lootList"];
            if (list?.Exists != true)
            {
                continue;
            }
            foreach (var entry in list.AsArray() ?? System.Array.Empty<JsonObject>())
            {
                if ((entry["codes"].AsArray() ?? System.Array.Empty<JsonObject>()).Any(c => c.AsString("").StartsWith(LemuriaModSystem.ModId + ":crystal-", StringComparison.Ordinal)))
                {
                    entries++;
                }
            }
        }
        Mod.Logger.Notification("Crystals: precursor crystals in {0} loot list entries of the ore and tool vessels", entries);
    }

    /// <summary>A geode crystal of the game's own: crystal-small and crystal-largecluster.</summary>
    private static bool IsGeodeCrystal(Block? block) => block is BlockCrystal && block.Code?.Domain == "game";

    private void OnDidPlaceBlock(IServerPlayer byPlayer, int oldblockId, BlockSelection blockSel, ItemStack withItemStack)
    {
        // The selection holds the position the block went to; the block there tells what was placed.
        var pos = blockSel.Position;
        if (!IsGeodeCrystal(sapi.World.BlockAccessor.GetBlock(pos)))
        {
            return;
        }
        UpdatePlaced(pos, add: true);
    }

    private void OnDidBreakBlock(IServerPlayer byPlayer, int oldblockId, BlockSelection blockSel)
    {
        var block = sapi.World.GetBlock(oldblockId);
        if (!IsGeodeCrystal(block))
        {
            return;
        }
        var pos = blockSel.Position;
        // Only crystals the world made count, so nobody farms the chance by placing and breaking.
        if (UpdatePlaced(pos, add: false))
        {
            return;
        }
        if (byPlayer.WorldData.CurrentGameMode != EnumGameMode.Survival || strangeCrystal == null)
        {
            return;
        }
        var config = LemuriaModSystem.Config;
        bool small = block.Variant["variant"]?.StartsWith("small", StringComparison.Ordinal) == true;
        double chance = Math.Clamp(small ? config.FindChanceSmall : config.FindChanceLarge, 0, 1);
        if (sapi.World.Rand.NextDouble() >= chance)
        {
            return;
        }
        sapi.World.SpawnItemEntity(new ItemStack(strangeCrystal), pos.ToVec3d().Add(0.5, 0.25, 0.5));
    }

    /// <summary>
    /// Adds or removes a position in its chunk's list of player-placed geode crystals.
    /// Returns whether the position was in the list before.
    /// </summary>
    private bool UpdatePlaced(BlockPos pos, bool add)
    {
        var chunk = sapi.WorldManager.GetChunk(pos);
        if (chunk == null)
        {
            return false;
        }
        int index = (pos.InternalY % GlobalConstants.ChunkSize * GlobalConstants.ChunkSize + pos.Z % GlobalConstants.ChunkSize)
            * GlobalConstants.ChunkSize + pos.X % GlobalConstants.ChunkSize;
        var placed = Read(chunk.GetServerModdata(PlacedKey));
        bool had = add ? !placed.Add(index) : placed.Remove(index);
        if (add != had)
        {
            chunk.SetServerModdata(PlacedKey, Write(placed));
            chunk.MarkModified();
        }
        return had;
    }

    private static HashSet<int> Read(byte[]? data)
    {
        var set = new HashSet<int>();
        if (data == null)
        {
            return set;
        }
        for (int i = 0; i + 4 <= data.Length; i += 4)
        {
            set.Add(BitConverter.ToInt32(data, i));
        }
        return set;
    }

    private static byte[] Write(HashSet<int> set)
    {
        var data = new byte[set.Count * 4];
        int i = 0;
        foreach (int value in set)
        {
            BitConverter.TryWriteBytes(data.AsSpan(i), value);
            i += 4;
        }
        return data;
    }

    // ------------------------------------------------------------------ test commands

    private const string TestPosKey = LemuriaModSystem.ModId + "-crystalstest";
    private const int TestDx = 8, TestDz = 2;

    private void RegisterTestCommands(ICoreServerAPI api)
    {
        api.ChatCommands.GetOrCreate(LemuriaModSystem.ModId)
            .WithDescription("Lemuria commands")
            .RequiresPrivilege(Privilege.chat)
            .BeginSubCommand("crystals")
                .WithDescription("Crystal commands")
                .RequiresPrivilege(Privilege.controlserver)
                .BeginSubCommand("test")
                    .WithDescription("Tests charge and dialogues next to the world spawn")
                    .BeginSubCommand("charge")
                        .WithDescription("Places a Fire crystal beside a burning firepit, moves time on half a day, and reports its charge")
                        .HandleWith(_ => TestSite.WithChunk(api, Mod.Logger, "Crystals", TestDx, TestDz, OnTestCharge))
                    .EndSubCommand()
                    .BeginSubCommand("standing")
                        .WithDescription("Places two pairs of twins, lets the warning run out, and reports what the dialogues did")
                        .HandleWith(_ => TestSite.WithChunk(api, Mod.Logger, "Crystals", TestDx, TestDz, OnTestStanding))
                    .EndSubCommand()
                    .BeginSubCommand("chest")
                        .WithDescription("Puts twins in a chest, and twins in a crystal jar in the same chest, lets the warning run out, and reports")
                        .HandleWith(_ => TestSite.WithChunk(api, Mod.Logger, "Crystals", TestDx, TestDz, OnTestChest))
                    .EndSubCommand()
                    .BeginSubCommand("check")
                        .WithDescription("Reports the test crystal's charge and cracks, after a restart")
                        .HandleWith(_ => TestSite.WithChunk(api, Mod.Logger, "Crystals", TestDx, TestDz, OnTestCheck))
                    .EndSubCommand()
                .EndSubCommand()
            .EndSubCommand();
    }

    private BlockEntityCrystal? PlaceCrystal(BlockPos pos, string alignment, string evolution, float charge, int cracks)
    {
        var block = Crystal.BlockFor(sapi.World, alignment, evolution);
        if (block == null)
        {
            Mod.Logger.Error("Crystals test: no block for {0} {1}", alignment, evolution);
            return null;
        }
        var stack = new ItemStack(block);
        Crystal.SetState(stack, charge, cracks);
        var accessor = sapi.World.BlockAccessor;
        accessor.SetBlock(sapi.World.GetBlock(new AssetLocation("game:rock-granite"))?.Id ?? 0, pos.DownCopy());
        accessor.SetBlock(block.Id, pos, stack);
        return accessor.GetBlockEntity(pos) as BlockEntityCrystal;
    }

    private void Clear(BlockPos origin, int sizeX, int sizeY, int sizeZ)
    {
        var accessor = sapi.World.BlockAccessor;
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                for (int z = 0; z < sizeZ; z++)
                {
                    accessor.SetBlock(0, origin.AddCopy(x, y, z));
                }
            }
        }
    }

    private static string Level(int level) => level switch
    {
        Crystal.Empty => "empty",
        Crystal.Low => "low",
        Crystal.High => "high",
        _ => "full",
    };

    private void OnTestCharge(BlockPos column)
    {
        var pos = TestSite.Surface(sapi, column);
        Clear(pos.AddCopy(-1, 0, -1), 4, 3, 3);
        var firepitPos = pos.EastCopy();
        var accessor = sapi.World.BlockAccessor;
        // A granite roof two blocks up, so the open sky adds nothing to the reading.
        for (int dx = -1; dx <= 2; dx++)
        {
            for (int dz = -1; dz <= 1; dz++)
            {
                accessor.SetBlock(sapi.World.GetBlock(new AssetLocation("game:rock-granite"))?.Id ?? 0, pos.AddCopy(dx, 2, dz));
            }
        }
        accessor.SetBlock(sapi.World.GetBlock(new AssetLocation("game:rock-granite"))?.Id ?? 0, firepitPos.DownCopy());
        accessor.SetBlock(sapi.World.GetBlock(new AssetLocation("game:firepit-cold"))?.Id ?? 0, firepitPos);
        if (accessor.GetBlockEntity(firepitPos) is BlockEntityFirepit firepit)
        {
            firepit.igniteWithFuel(new ItemStack(sapi.World.GetItem(new AssetLocation("game:firewood")), 1));
        }
        var crystal = PlaceCrystal(pos, Crystal.Fire, Crystal.Glittering, 0, 1);
        if (crystal == null)
        {
            return;
        }
        TestSite.StorePos(sapi, TestPosKey, pos);
        var raw = CrystalEnvironment.Read(sapi.World, pos, false);
        CrystalEnvironment.Lead(raw, 1, out var leader, out var strength);
        crystal.ReadSurroundings(sapi.World.Calendar.HoursPerDay / 2.0);
        Mod.Logger.Notification("Crystals test: beside a burning firepit {0} leads at {1:0.##}; after half a day the Fire crystal is {2}, with {3} crack(s)",
            leader, strength, Level(crystal.Level), crystal.Cracks);
    }

    private void OnTestCheck(BlockPos column)
    {
        var pos = TestSite.ReadPos(sapi, TestPosKey);
        if (pos == null || sapi.World.BlockAccessor.GetBlockEntity(pos) is not BlockEntityCrystal crystal)
        {
            Mod.Logger.Error("Crystals test: no test crystal; run /lemuria crystals test charge first");
            return;
        }
        Mod.Logger.Notification("Crystals test: the {0} crystal at {1} is {2}, with {3} crack(s)",
            crystal.Alignment, TestSite.Describe(sapi, pos), Level(crystal.Level), crystal.Cracks);
    }

    private void OnTestStanding(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column).AddCopy(0, 0, 4);
        Clear(origin.AddCopy(-1, 0, -1), 12, 3, 3);
        // Glittering Fire and Ice two blocks apart, and charged Glowing Fire and Ice two blocks apart,
        // the pairs eight blocks from each other, so each pairs only with its own twin.
        PlaceCrystal(origin, Crystal.Fire, Crystal.Glittering, 50, 0);
        PlaceCrystal(origin.AddCopy(2, 0, 0), Crystal.Ice, Crystal.Glittering, 0, 0);
        PlaceCrystal(origin.AddCopy(8, 0, 0), Crystal.Fire, Crystal.Glowing, 200, 0);
        PlaceCrystal(origin.AddCopy(10, 0, 0), Crystal.Ice, Crystal.Glowing, 120, 0);
        float grace = (float)LemuriaModSystem.Config.DialogueGraceSeconds;
        Dialogues.TickStanding(1);
        bool warned = sapi.World.BlockAccessor.GetBlockEntity(origin) is BlockEntityCrystal { Warning: true };
        Dialogues.TickStanding(grace);
        string Describe(BlockPos pos)
        {
            var block = sapi.World.BlockAccessor.GetBlock(pos);
            var be = sapi.World.BlockAccessor.GetBlockEntity(pos) as BlockEntityCrystal;
            return block is BlockLemuriaCrystal && be != null
                ? $"{be.Alignment} {be.Evolution} {Level(be.Level)}" + (be.Cracks > 0 ? $" cracked {be.Cracks}" : "")
                : "gone";
        }
        Mod.Logger.Notification("Crystals test: warning {0}; Glittering pair now {1} and {2}; Glowing pair now {3} and {4}",
            warned ? "heard" : "missing", Describe(origin), Describe(origin.AddCopy(2, 0, 0)), Describe(origin.AddCopy(8, 0, 0)), Describe(origin.AddCopy(10, 0, 0)));
    }

    private void OnTestChest(BlockPos column)
    {
        var pos = TestSite.Surface(sapi, column).AddCopy(0, 0, 8);
        Clear(pos.AddCopy(-1, 0, -1), 3, 2, 3);
        var accessor = sapi.World.BlockAccessor;
        var chestBlock = sapi.World.GetBlock(new AssetLocation("game:chest-east"));
        if (chestBlock == null)
        {
            Mod.Logger.Error("Crystals test: no chest block");
            return;
        }
        accessor.SetBlock(sapi.World.GetBlock(new AssetLocation("game:rock-granite"))?.Id ?? 0, pos.DownCopy());
        accessor.SetBlock(chestBlock.Id, pos);
        if (accessor.GetBlockEntity(pos) is not BlockEntityContainer chest)
        {
            Mod.Logger.Error("Crystals test: no chest block entity");
            return;
        }
        var inventory = chest.Inventory;
        // Three Fire crystals and one Ice crystal lose one of each; the other two Fire crystals stay.
        inventory[0].Itemstack = new ItemStack(Crystal.BlockFor(sapi.World, Crystal.Fire, Crystal.Glittering), 3);
        inventory[3].Itemstack = new ItemStack(Crystal.BlockFor(sapi.World, Crystal.Ice, Crystal.Glittering), 1);
        var jarItem = sapi.World.GetItem(new AssetLocation(LemuriaModSystem.ModId, "crystaljar-personal-fired"));
        if (jarItem != null)
        {
            var jar = new ItemStack(jarItem);
            var slots = new TreeAttribute();
            slots.SetItemstack("slot-0", new ItemStack(Crystal.BlockFor(sapi.World, Crystal.Earth, Crystal.Glittering)));
            slots.SetItemstack("slot-1", new ItemStack(Crystal.BlockFor(sapi.World, Crystal.Sky, Crystal.Glittering)));
            var backpack = new TreeAttribute();
            backpack["slots"] = slots;
            jar.Attributes["backpack"] = backpack;
            inventory[8].Itemstack = jar;
        }
        foreach (var slot in inventory)
        {
            slot.MarkDirty();
        }
        Dialogues.Watch(inventory);
        Dialogues.TickContainersNow(0);
        Dialogues.TickContainersNow((float)LemuriaModSystem.Config.DialogueGraceSeconds + 1);
        int unaligned = 0, fire = 0, ice = 0;
        string jarHolds = "no jar";
        foreach (var slot in inventory)
        {
            var stack = slot.Itemstack;
            if (stack == null)
            {
                continue;
            }
            if (Crystal.IsCrystal(stack.Collectible))
            {
                switch (Crystal.AlignmentOf(stack.Collectible))
                {
                    case Crystal.Unaligned: unaligned += stack.StackSize; break;
                    case Crystal.Fire: fire += stack.StackSize; break;
                    case Crystal.Ice: ice += stack.StackSize; break;
                }
            }
            else if (Dialogues.IsJar(stack))
            {
                var held = stack.Attributes.GetTreeAttribute("backpack")?.GetTreeAttribute("slots");
                var names = new List<string>();
                if (held != null)
                {
                    foreach (var entry in held)
                    {
                        if ((entry.Value as ItemstackAttribute)?.value is ItemStack s)
                        {
                            s.ResolveBlockOrItem(sapi.World);
                            names.Add(Crystal.AlignmentOf(s.Collectible));
                        }
                    }
                }
                jarHolds = string.Join(" and ", names);
            }
        }
        Mod.Logger.Notification("Crystals test: the chest now holds {0} Unaligned, {1} Fire and {2} Ice crystal(s); its jar still holds {3}", unaligned, fire, ice, jarHolds);
    }

    public override void Dispose()
    {
        Hums?.Dispose();
        if (sapi != null)
        {
            var weather = sapi.ModLoader.GetModSystem<WeatherSystemServer>();
            if (weather != null)
            {
                weather.OnLightningImpactBegin -= OnLightning;
            }
        }
        if (Server == this)
        {
            Standing = null;
            Server = null;
        }
        if (Client == this)
        {
            Client = null;
        }
    }
}
