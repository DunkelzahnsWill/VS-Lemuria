using System;
using System.Collections.Generic;
using System.Linq;
using Vintagestory.API.Common;

namespace Lemuria.Machines;

/// <summary>
/// Goods in and out of a machine's containers (step 4 part 2 note, Containers and tubes): whether a
/// working's yields would fit before it happens, putting them in, and counting and taking the stock a
/// shelf item uses up. Any container the game's chutes and hoppers use counts, chutes and hoppers too.
/// </summary>
public static class Stock
{
    /// <summary>Whether all these stacks would fit into the inventory, merged as the game merges them; nothing moves.</summary>
    public static bool Fits(IWorldAccessor world, IInventory inventory, IEnumerable<ItemStack> stacks)
    {
        var slots = Enumerable.Range(0, inventory.Count).Select(i => inventory[i]!).ToList();
        var held = slots.Select(s => s.Itemstack?.Clone()).ToList();
        foreach (var incoming in stacks)
        {
            int left = incoming.StackSize;
            for (int i = 0; i < held.Count && left > 0; i++)
            {
                var sink = held[i];
                if (sink == null || sink.StackSize >= sink.Collectible.MaxStackSize || !sink.Equals(world, incoming, Vintagestory.API.Config.GlobalConstants.IgnoredStackAttributes))
                {
                    continue;
                }
                int moved = Math.Min(left, sink.Collectible.MaxStackSize - sink.StackSize);
                sink.StackSize += moved;
                left -= moved;
            }
            for (int i = 0; i < held.Count && left > 0; i++)
            {
                if (held[i] != null || !slots[i].CanHold(new DummySlot(incoming)))
                {
                    continue;
                }
                var placed = incoming.Clone();
                placed.StackSize = Math.Min(left, Math.Max(1, incoming.Collectible.MaxStackSize));
                held[i] = placed;
                left -= placed.StackSize;
            }
            if (left > 0)
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>Puts a stack into the inventory; returns what did not fit, or null.</summary>
    public static ItemStack? Put(IWorldAccessor world, IInventory inventory, ItemStack stack)
    {
        var source = new DummySlot(stack);
        for (int i = 0; i < 64 && !source.Empty; i++)
        {
            var best = inventory.GetBestSuitedSlot(source);
            if (best.slot == null)
            {
                break;
            }
            if (source.TryPutInto(world, best.slot, source.StackSize) <= 0)
            {
                break;
            }
            best.slot.MarkDirty();
        }
        return source.Empty ? null : source.Itemstack;
    }

    /// <summary>How many items in the inventory match.</summary>
    public static int Count(IInventory? inventory, System.Func<ItemStack, bool> match)
    {
        if (inventory == null)
        {
            return 0;
        }
        int count = 0;
        foreach (var slot in inventory)
        {
            if (slot.Itemstack is { } stack && match(stack))
            {
                count += stack.StackSize;
            }
        }
        return count;
    }

    /// <summary>The first stack in the inventory that matches, or null.</summary>
    public static ItemStack? First(IInventory? inventory, System.Func<ItemStack, bool> match) =>
        inventory?.FirstOrDefault(s => s.Itemstack != null && match(s.Itemstack))?.Itemstack;

    /// <summary>Takes up to this many matching items out of the inventory; returns how many it took.</summary>
    public static int Take(IInventory? inventory, System.Func<ItemStack, bool> match, int count)
    {
        if (inventory == null)
        {
            return 0;
        }
        int taken = 0;
        foreach (var slot in inventory)
        {
            if (taken >= count)
            {
                break;
            }
            if (slot.Itemstack is { } stack && match(stack))
            {
                int n = Math.Min(count - taken, stack.StackSize);
                slot.TakeOut(n);
                slot.MarkDirty();
                taken += n;
            }
        }
        return taken;
    }

    /// <summary>Stacks of the same item merged, so a fit test and a put see them as a player's hand would.</summary>
    public static List<ItemStack> Merge(IWorldAccessor world, IEnumerable<ItemStack> stacks)
    {
        var merged = new List<ItemStack>();
        foreach (var stack in stacks)
        {
            if (stack == null || stack.StackSize <= 0)
            {
                continue;
            }
            var same = merged.FirstOrDefault(m => m.Equals(world, stack, Vintagestory.API.Config.GlobalConstants.IgnoredStackAttributes));
            if (same != null)
            {
                same.StackSize += stack.StackSize;
            }
            else
            {
                merged.Add(stack.Clone());
            }
        }
        return merged;
    }
}
