using System;
using System.Linq;
using Lemuria.Core;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;

namespace Lemuria.Cradle;

/// <summary>
/// The cradle: a carved stone basin where a seedling grows through five stages to a ripe Unaligned
/// crystal (design note "Lemuria step 1: Find and farm", The cradle and growth).
/// Adds <c>/lemuria cradle test plant|advance|check</c> for the smoke test and for admins.
/// </summary>
public class CradleModSystem : ModSystem
{
    /// <summary>Savegame data: where the test cradle stands, so a second boot can find it again.</summary>
    private const string TestPosKey = LemuriaModSystem.ModId + "-cradletest";
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
        // One recipe per rock type and chisel metal; the log counts the rock types.
        api.Event.SaveGameLoaded += () => Mod.Logger.Notification("Cradle: recipes for {0} rock type(s)",
            api.World.GridRecipes.Where(r => r.Output?.Code?.Domain == LemuriaModSystem.ModId && r.Output.Code.Path.StartsWith("cradle-"))
                .Select(r => r.Output!.Code!.Path).Distinct().Count());
        var parsers = api.ChatCommands.Parsers;
        // The shared root needs a privilege of its own; each feature's subcommand sets the one it needs.
        api.ChatCommands.GetOrCreate(LemuriaModSystem.ModId)
            .WithDescription("Lemuria commands")
            .RequiresPrivilege(Privilege.chat)
            .BeginSubCommand("cradle")
                .WithDescription("Cradle commands")
                .RequiresPrivilege(Privilege.controlserver)
                .BeginSubCommand("test")
                    .WithDescription("Plants a test cradle at the world spawn, moves time on, and reports its stage")
                    .BeginSubCommand("plant")
                        .WithDescription("Places a granite cradle at the world spawn and plants a seedling in it")
                        .HandleWith(_ => WithTestChunk(OnTestPlant))
                    .EndSubCommand()
                    .BeginSubCommand("advance")
                        .WithDescription("Moves the world's time on by this many in-game days, then lets the test cradle catch up")
                        .WithArgs(parsers.Double("days"))
                        .HandleWith(args => WithTestChunk(_ => OnTestAdvance((double)args[0])))
                    .EndSubCommand()
                    .BeginSubCommand("check")
                        .WithDescription("Reports the test cradle's stage")
                        .HandleWith(_ => WithTestChunk(_ => OnTestCheck()))
                    .EndSubCommand()
                .EndSubCommand()
            .EndSubCommand();
    }

    /// <summary>
    /// Loads the test cradle's chunk column next to the world spawn (the console has no player to keep
    /// chunks loaded), then runs the action with that column; its result goes to the server log, since
    /// the load may finish after the reply. Actions run in the order the commands were typed.
    /// </summary>
    private TextCommandResult WithTestChunk(Action<BlockPos> action)
    {
        var pos = TestPosAtSpawn();
        if (pos == null)
        {
            return TextCommandResult.Error("The world has no spawn position yet");
        }
        int chunkSize = GlobalConstants.ChunkSize;
        sapi.WorldManager.LoadChunkColumnPriority(pos.X / chunkSize, pos.Z / chunkSize, new ChunkLoadOptions
        {
            KeepLoaded = true,
            OnLoaded = () => sapi.Event.EnqueueMainThreadTask(() => Run(action, pos), "lemuria-cradletest"),
        });
        return TextCommandResult.Success("Cradle test: loading the chunk at " + Describe(pos));
    }

    private void Run(Action<BlockPos> action, BlockPos pos)
    {
        try
        {
            action(pos);
        }
        catch (Exception e)
        {
            Mod.Logger.Error("Cradle test failed: {0}", e);
        }
    }

    private BlockPos? TestPosAtSpawn()
    {
        var spawn = sapi.World.DefaultSpawnPosition?.AsBlockPos;
        return spawn == null ? null : new BlockPos(spawn.X + 2, 0, spawn.Z + 2, 0);
    }

    private BlockPos? ReadTestPos()
    {
        var data = sapi.WorldManager.SaveGame.GetData(TestPosKey);
        if (data == null || data.Length < 12)
        {
            return null;
        }
        return new BlockPos(BitConverter.ToInt32(data, 0), BitConverter.ToInt32(data, 4), BitConverter.ToInt32(data, 8), 0);
    }

    private void OnTestPlant(BlockPos column)
    {
        var accessor = sapi.World.BlockAccessor;
        int surface = sapi.WorldManager.GetSurfacePosY(column.X, column.Z) ?? sapi.WorldManager.MapSizeY / 2;
        var pos = new BlockPos(column.X, surface + 1, column.Z, 0);
        var cradleBlock = sapi.World.GetBlock(new AssetLocation(LemuriaModSystem.ModId, "cradle-" + TestRock));
        var seedling = sapi.World.GetItem(new AssetLocation(TestSeedling));
        if (cradleBlock == null || seedling == null)
        {
            Mod.Logger.Error("Cradle test: the cradle or the seedling is not loaded");
            return;
        }
        // The test cradle stands on a granite block, so it always has a solid block below.
        accessor.SetBlock(sapi.World.GetBlock(new AssetLocation("game:rock-" + TestRock))?.Id ?? 0, pos.DownCopy());
        accessor.SetBlock(cradleBlock.Id, pos);
        if (accessor.GetBlockEntity(pos) is not BlockEntityCradle cradle)
        {
            Mod.Logger.Error("Cradle test: no cradle block entity at {0}", Describe(pos));
            return;
        }
        cradle.Plant(seedling.Code.ToString());
        var data = new byte[12];
        BitConverter.TryWriteBytes(data.AsSpan(0), pos.X);
        BitConverter.TryWriteBytes(data.AsSpan(4), pos.Y);
        BitConverter.TryWriteBytes(data.AsSpan(8), pos.Z);
        sapi.WorldManager.SaveGame.StoreData(TestPosKey, data);
        Mod.Logger.Notification("Cradle test: planted {0} in {1} at {2}, stage {3}", seedling.Code, cradleBlock.Code, Describe(pos), StageName(cradle.Stage));
    }

    private BlockEntityCradle? TestCradle(out BlockPos? pos)
    {
        pos = ReadTestPos();
        if (pos == null)
        {
            Mod.Logger.Error("Cradle test: no test cradle yet; run /lemuria cradle test plant first");
            return null;
        }
        if (sapi.World.BlockAccessor.GetBlockEntity(pos) is not BlockEntityCradle cradle)
        {
            Mod.Logger.Error("Cradle test: no cradle at {0}", Describe(pos));
            return null;
        }
        return cradle;
    }

    private void OnTestAdvance(double days)
    {
        var cradle = TestCradle(out _);
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
        var cradle = TestCradle(out var pos);
        if (cradle == null || pos == null)
        {
            return;
        }
        cradle.Advance();
        Mod.Logger.Notification("Cradle test: the cradle at {0} is at stage {1}", Describe(pos), StageName(cradle.Stage));
    }

    private static string StageName(int stage) => stage switch
    {
        BlockEntityCradle.Empty => "empty",
        BlockEntityCradle.Ripe => "ripe",
        _ => stage.ToString(),
    };

    private string Describe(BlockPos pos)
    {
        var spawn = sapi.World.DefaultSpawnPosition.AsBlockPos;
        return $"{pos.X - spawn.X}, {pos.Y}, {pos.Z - spawn.Z} (from spawn)";
    }
}
