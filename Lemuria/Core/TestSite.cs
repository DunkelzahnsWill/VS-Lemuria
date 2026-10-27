using System;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;

namespace Lemuria.Core;

/// <summary>
/// Helpers for the features' test commands, which the smoke test types into the server console.
/// The console has no player to keep chunks loaded, so a test loads the chunk column it works in
/// first, then runs its action on the main thread; results go to the server log, since the load may
/// finish after the reply. Each feature tests at its own offset from the world spawn.
/// </summary>
public static class TestSite
{
    /// <summary>The column at this offset from the world spawn, or null while the world has no spawn.</summary>
    public static BlockPos? Column(ICoreServerAPI sapi, int dx, int dz)
    {
        var spawn = sapi.World.DefaultSpawnPosition?.AsBlockPos;
        return spawn == null ? null : new BlockPos(spawn.X + dx, 0, spawn.Z + dz, 0);
    }

    /// <summary>The first air block above the surface of a column.</summary>
    public static BlockPos Surface(ICoreServerAPI sapi, BlockPos column)
    {
        int surface = sapi.WorldManager.GetSurfacePosY(column.X, column.Z) ?? sapi.WorldManager.MapSizeY / 2;
        return new BlockPos(column.X, surface + 1, column.Z, 0);
    }

    /// <summary>
    /// Loads the chunk column at the offset, keeps it loaded, then runs the action with that column.
    /// Actions run in the order the commands were typed. Errors go to the log under the feature's name.
    /// </summary>
    public static TextCommandResult WithChunk(ICoreServerAPI sapi, ILogger logger, string feature, int dx, int dz, Action<BlockPos> action)
    {
        var column = Column(sapi, dx, dz);
        if (column == null)
        {
            return TextCommandResult.Error("The world has no spawn position yet");
        }
        int chunkSize = GlobalConstants.ChunkSize;
        sapi.WorldManager.LoadChunkColumnPriority(column.X / chunkSize, column.Z / chunkSize, new ChunkLoadOptions
        {
            KeepLoaded = true,
            OnLoaded = () => sapi.Event.EnqueueMainThreadTask(() =>
            {
                try
                {
                    action(column);
                }
                catch (Exception e)
                {
                    logger.Error("{0} test failed: {1}", feature, e);
                }
            }, LemuriaModSystem.ModId + "-" + feature.ToLowerInvariant() + "test"),
        });
        return TextCommandResult.Success(feature + " test: loading the chunk at " + Describe(sapi, column));
    }

    /// <summary>A position as an offset from the world spawn, so logs read the same in every world.</summary>
    public static string Describe(ICoreServerAPI sapi, BlockPos pos)
    {
        var spawn = sapi.World.DefaultSpawnPosition.AsBlockPos;
        return $"{pos.X - spawn.X}, {pos.Y}, {pos.Z - spawn.Z} (from spawn)";
    }

    /// <summary>Stores a position in the savegame, so a second boot can find a test block again.</summary>
    public static void StorePos(ICoreServerAPI sapi, string key, BlockPos pos)
    {
        var data = new byte[12];
        BitConverter.TryWriteBytes(data.AsSpan(0), pos.X);
        BitConverter.TryWriteBytes(data.AsSpan(4), pos.Y);
        BitConverter.TryWriteBytes(data.AsSpan(8), pos.Z);
        sapi.WorldManager.SaveGame.StoreData(key, data);
    }

    /// <summary>A position stored with <see cref="StorePos"/>, or null.</summary>
    public static BlockPos? ReadPos(ICoreServerAPI sapi, string key)
    {
        var data = sapi.WorldManager.SaveGame.GetData(key);
        if (data == null || data.Length < 12)
        {
            return null;
        }
        return new BlockPos(BitConverter.ToInt32(data, 0), BitConverter.ToInt32(data, 4), BitConverter.ToInt32(data, 8), 0);
    }
}
