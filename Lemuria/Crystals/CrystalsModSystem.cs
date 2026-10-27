using System;
using System.Collections.Generic;
using Lemuria.Core;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;
using Vintagestory.GameContent;

namespace Lemuria.Crystals;

/// <summary>
/// Crystals: the strange glowing crystal found in geodes, the seedlings ground from it, and the
/// Unaligned crystal block (design note "Lemuria step 1: Find and farm", Finding and Grinding).
/// The find listens to the game's own block events, so no vanilla file is patched.
/// </summary>
public class CrystalsModSystem : ModSystem
{
    public const string StrangeCrystalCode = LemuriaModSystem.ModId + ":strangecrystal";

    /// <summary>Server-only chunk data: the geode crystals players placed in that chunk, which never give a find.</summary>
    private const string PlacedKey = LemuriaModSystem.ModId + "-placedgeodecrystals";

    private ICoreServerAPI sapi = null!;
    private Item? strangeCrystal;

    public override void Start(ICoreAPI api)
    {
        api.RegisterBlockClass(BlockLemuriaCrystal.ClassCode, typeof(BlockLemuriaCrystal));
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
        api.Event.DidPlaceBlock += OnDidPlaceBlock;
        api.Event.DidBreakBlock += OnDidBreakBlock;
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
        };
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
}
