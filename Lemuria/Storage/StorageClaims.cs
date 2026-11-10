using System;
using System.Collections.Generic;
using Lemuria.Core;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace Lemuria.Storage;

/// <summary>
/// What Storage opens to everyone inside a land claim while the claims switch is off (step 3 note,
/// Claims and players; the owner, 2026-11-10): the crystal rack, a crystal jar set down on the ground,
/// and an unfired crock about to be made into a jar. The game tests a claim itself before it calls a
/// block, so the rack's own check never ran for a stranger; this answers the game's block access event
/// instead, with no Harmony. Only use is opened, never breaking, and on a spot of the game's ground
/// storage only the part that holds the jar or the crock: anything else stored there stays locked.
/// One instance per side, since the client tests a click before it sends it.
/// </summary>
public class StorageClaims
{
    /// <summary>
    /// How long the game's own follow-up tests of a click that was let through still pass: the ground
    /// storage's second check comes within the same call, the end of the interaction a frame or two later.
    /// </summary>
    public const int FollowMs = 2000;

    private readonly ICoreAPI api;

    /// <summary>The last click on a jar or a crock that each player was let through: where, which part, and when.</summary>
    private readonly Dictionary<string, (BlockPos Pos, int Part, long At)> opened = new();

    public StorageClaims(ICoreAPI api)
    {
        this.api = api;
    }

    /// <summary>The game's <c>OnTestBlockAccess</c>: lifts a claim's lock on what Storage opens, and leaves every other answer as it came.</summary>
    public EnumWorldAccessResponse OnTestBlockAccess(IPlayer player, BlockSelection blockSel, EnumBlockAccessFlags accessType, ref string claimant, EnumWorldAccessResponse response)
    {
        if (!LemuriaClaims.MayLift(accessType, response) || blockSel?.Position == null)
        {
            return response;
        }
        if (!Opens(api.World, player?.PlayerUID, player?.InventoryManager?.ActiveHotbarSlot?.Itemstack, blockSel))
        {
            return response;
        }
        claimant = null!;
        return LemuriaClaims.Lift(api.World, player, blockSel);
    }

    /// <summary>
    /// Whether this use of a claimed spot is one Storage opens: any part of a crystal rack; the part of a
    /// ground storage spot that holds a crystal jar; the part that holds an unfired crock while the player
    /// holds what makes it a jar; and the game's own follow-up tests of such a click.
    /// </summary>
    public bool Opens(IWorldAccessor world, string? playerUid, ItemStack? held, BlockSelection blockSel) =>
        Opens(world, playerUid, held, blockSel, world.ElapsedMilliseconds);

    /// <summary>The same at a given time in the world's milliseconds, so the test command need not wait.</summary>
    public bool Opens(IWorldAccessor world, string? playerUid, ItemStack? held, BlockSelection blockSel, long now)
    {
        var pos = blockSel.Position;
        if (world.BlockAccessor.GetBlock(pos) is BlockCrystalRack)
        {
            return true;
        }
        var storage = world.BlockAccessor.GetBlockEntity(pos) as BlockEntityGroundStorage;
        int part = storage == null ? -1 : PartOf(storage, blockSel);
        if (part >= 0)
        {
            var stored = storage!.Inventory[part].Itemstack;
            if (IsJar(stored) || (CollectibleBehaviorJarFiller.IsRawCrock(stored) && MakesJar(held)))
            {
                if (playerUid != null)
                {
                    lock (opened)
                    {
                        opened[playerUid] = (pos.Copy(), part, now);
                    }
                }
                return true;
            }
        }
        if (playerUid == null)
        {
            return false;
        }
        lock (opened)
        {
            if (!opened.TryGetValue(playerUid, out var last))
            {
                return false;
            }
            if (now - last.At > FollowMs || now < last.At)
            {
                opened.Remove(playerUid);
                return false;
            }
            if (!last.Pos.Equals(pos))
            {
                return false;
            }
            // The ground storage tests the spot once more inside the click, without saying which part.
            if (blockSel.HitPosition == null)
            {
                return true;
            }
            // The click's end arrives a moment later: the spot went with its last jar and left air, or the same part is meant.
            return storage == null ? world.BlockAccessor.GetBlock(pos, BlockLayersAccess.Solid).Id == 0 : part == last.Part;
        }
    }

    /// <summary>A crystal jar, unfired or fired.</summary>
    public static bool IsJar(ItemStack? stack)
    {
        var code = stack?.Collectible?.Code;
        return code != null && code.Domain == LemuriaModSystem.ModId && code.Path.StartsWith("crystaljar-", StringComparison.Ordinal);
    }

    /// <summary>
    /// What makes a crock a jar, and only while the ground storage would pass the click on to it: an item
    /// it could store itself would take the crock instead.
    /// </summary>
    private static bool MakesJar(ItemStack? held) =>
        held?.Collectible is { } item && item.HasBehavior<CollectibleBehaviorJarFiller>() && !item.HasBehavior<CollectibleBehaviorGroundStorable>();

    /// <summary>The part of a ground storage spot a click means, as the spot itself works it out, or -1 when the test names no part.</summary>
    private static int PartOf(BlockEntityGroundStorage storage, BlockSelection blockSel)
    {
        if (blockSel.HitPosition == null || storage.StorageProps == null)
        {
            return -1;
        }
        var slot = storage.GetSlotAt(blockSel);
        return slot == null ? -1 : storage.Inventory.GetSlotId(slot);
    }
}
