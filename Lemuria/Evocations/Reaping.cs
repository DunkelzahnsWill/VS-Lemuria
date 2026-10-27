using System;
using System.Collections.Generic;
using System.Linq;
using Lemuria.Core;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Config;
using Vintagestory.API.MathTools;
using Vintagestory.API.Util;
using Vintagestory.GameContent;

namespace Lemuria.Evocations;

/// <summary>
/// What Gale's gust does to blocks after its push (step 3 note, Gale; the owner, 2026-10-27): in its
/// usual area it strips grown leaves and ripe crops as a hand would, with the game's drops, and pans
/// every pannable block out whole, one panning per layer, as the game's wooden pan rolls them. The
/// drops land in the area, the same kinds stacked together. Placed leaves, unripe crops, berry bushes
/// and fruit trees are left alone, and nothing is replanted.
/// </summary>
public static class Reaping
{
    private static readonly Dictionary<string, PanningDrop[]> panningDrops = new();
    private static IWorldAccessor? loadedFor;

    public static bool Reap(CastContext cast)
    {
        var world = cast.Api.World;
        var accessor = world.BlockAccessor;
        var drops = new List<ItemStack>();
        var seen = new HashSet<BlockPos>();
        bool acted = false;
        foreach (var pos in cast.Area())
        {
            acted |= Take(cast, pos, seen, drops);
            // Aimed at farmland, the crop growing on it.
            if (accessor.GetBlockEntity(pos) is BlockEntityFarmland)
            {
                acted |= Take(cast, pos.UpCopy(), seen, drops);
            }
        }
        if (drops.Count > 0)
        {
            var at = cast.Center!.ToVec3d().Add(0.5, 0.5, 0.5);
            foreach (var stack in Stack(drops))
            {
                world.SpawnItemEntity(stack, at);
            }
        }
        return acted;
    }

    private static bool Take(CastContext cast, BlockPos pos, HashSet<BlockPos> seen, List<ItemStack> drops)
    {
        if (!seen.Add(pos.Copy()) || !cast.Can(pos))
        {
            return false;
        }
        var world = cast.Api.World;
        var accessor = world.BlockAccessor;
        var block = accessor.GetBlock(pos);
        if (IsGrownLeaves(block) || IsRipeCrop(block))
        {
            drops.AddRange(block.GetDrops(world, pos, cast.Player) ?? Array.Empty<ItemStack>());
            var farmland = accessor.GetBlockEntity(pos.DownCopy()) as BlockEntityFarmland;
            accessor.SetBlock(0, pos);
            farmland?.OnCropBlockBroken();
        }
        else if (block.Attributes?.IsTrue("pannable") == true && accessor.GetBlockEntity(pos) == null)
        {
            drops.AddRange(Pan(world, block, cast.Caster));
            accessor.SetBlock(0, pos);
        }
        else
        {
            return false;
        }
        accessor.TriggerNeighbourBlockUpdate(pos);
        return true;
    }

    public static bool IsGrownLeaves(Block block) =>
        block is BlockLeaves && block.Variant["type"]?.StartsWith("grown") == true;

    public static bool IsRipeCrop(Block block) =>
        block is BlockCrop crop && crop.CropProps != null && crop.CurrentCropStage >= crop.CropProps.GrowthStages;

    /// <summary>
    /// Pans a pannable block out whole: one panning per layer, 8 for a full block, each rolled as the
    /// wooden pan rolls it (its panningDrops for that material, at most one find a panning, the
    /// panner's drop-rate stats).
    /// </summary>
    public static List<ItemStack> Pan(IWorldAccessor world, Block block, EntityAgent? by)
    {
        var finds = new List<ItemStack>();
        string? layer = block.Variant["layer"];
        int pannings = layer != null && int.TryParse(layer, out int layers) ? layers : 8;
        var material = layer != null
            ? world.GetBlock(new AssetLocation(block.Code.Domain, block.FirstCodePart() + "-" + block.FirstCodePart(1))) ?? block
            : block;
        string code = material.Code.ToShortString();
        var drops = DropsFor(world, code);
        if (drops == null)
        {
            return finds;
        }
        string? rock = material.Variant["rock"];
        for (int i = 0; i < pannings; i++)
        {
            foreach (var drop in drops.OrderBy(_ => world.Rand.Next()))
            {
                float chance = drop.Chance.nextFloat() * (drop.DropModbyStat != null && by != null ? by.Stats.GetBlended(drop.DropModbyStat) : 1f);
                var stack = drop.Code.Path.Contains("{rocktype}") ? Resolve(world, drop, rock) : drop.ResolvedItemstack;
                if (world.Rand.NextDouble() < chance && stack != null)
                {
                    finds.Add(stack.Clone());
                    break;
                }
            }
        }
        return finds;
    }

    private static PanningDrop[]? DropsFor(IWorldAccessor world, string code)
    {
        if (loadedFor != world)
        {
            panningDrops.Clear();
            loadedFor = world;
            var pan = world.Blocks.FirstOrDefault(b => b is BlockPan && b.Attributes?["panningDrops"].Exists == true);
            var all = pan?.Attributes["panningDrops"].AsObject<Dictionary<string, PanningDrop[]>>() ?? new();
            bool natural = !world.Config.GetAsBool("loreContent", true);
            foreach (var (key, list) in all)
            {
                var kept = list.Where(d => !(natural && d.ManMade)).ToArray();
                foreach (var drop in kept)
                {
                    drop.Resolve(world, "Lemuria panning", false);
                }
                panningDrops[key] = kept;
            }
        }
        // As the pan matches them: the last pattern that fits wins.
        PanningDrop[]? found = null;
        foreach (var (key, drops) in panningDrops)
        {
            if (WildcardUtil.Match(key, code))
            {
                found = drops;
            }
        }
        return found;
    }

    private static ItemStack? Resolve(IWorldAccessor world, PanningDrop drop, string? rock)
    {
        var location = new AssetLocation(drop.Code.Domain, drop.Code.Path.Replace("{rocktype}", rock ?? ""));
        CollectibleObject? collectible = drop.Type == EnumItemClass.Block ? world.GetBlock(location) : world.GetItem(location);
        return collectible == null ? null : new ItemStack(collectible, drop.StackSize);
    }

    /// <summary>Puts drops of the same kind together, up to each kind's stack size.</summary>
    private static IEnumerable<ItemStack> Stack(List<ItemStack> drops)
    {
        var stacks = new List<ItemStack>();
        foreach (var drop in drops)
        {
            int left = drop.StackSize;
            foreach (var stack in stacks)
            {
                if (left <= 0)
                {
                    break;
                }
                if (stack.Equals(null, drop, GlobalConstants.IgnoredStackAttributes) && stack.StackSize < stack.Collectible.MaxStackSize)
                {
                    int moved = Math.Min(left, stack.Collectible.MaxStackSize - stack.StackSize);
                    stack.StackSize += moved;
                    left -= moved;
                }
            }
            if (left > 0)
            {
                var rest = drop.Clone();
                rest.StackSize = left;
                stacks.Add(rest);
            }
        }
        return stacks;
    }
}
