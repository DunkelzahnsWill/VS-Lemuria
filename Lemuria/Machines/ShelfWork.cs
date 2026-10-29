using System;
using System.Collections.Generic;
using System.Linq;
using Lemuria.Core;
using Lemuria.Evocations;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace Lemuria.Machines;

/// <summary>
/// The Control Shelf's work (step 4 part 2 note, Actions-to-Charge "one click a block", How each piece
/// behaves, Charcoal's second rung). On an Unaligned crystal's turn, when no cradle needs it, the first
/// item on the shelf, left to right, that has something to do in the working's reach does to each block
/// or creature there what one click of yours with that item does, for one working's charge. What it uses
/// up comes from the input, yields go where the machine sends them, and tools never wear. The reach is a
/// working's: one block at Glittering's power, a flat 3×3 at Glowing's, within 1 block at Luminous's,
/// and a Scatter's flat 9×9.
/// </summary>
public static class ShelfWork
{
    public enum Kind
    {
        None,
        Seeds,
        Sapling,
        Fertilizer,
        Shovel,
        Bucket,
        Axe,
        Firewood,
        Firestarter,
    }

    private static readonly AssetLocation PlantSound = new("game", "sounds/block/plant");
    private static readonly AssetLocation DirtSound = new("game", "sounds/block/dirt");
    private static readonly AssetLocation ChopSound = new("game", "sounds/block/chop2");
    private static readonly AssetLocation PileSound = new("game", "sounds/block/planks");
    private static readonly AssetLocation IgniteSound = new("game", "sounds/torch-ignite");
    private static readonly AssetLocation MilkCode = new("game", "milkportion");
    private static readonly AssetLocation GroundStorageCode = new("game", "groundstorage");
    private static readonly AssetLocation FirepitStartCode = new("game", "firepit-construct1");

    /// <summary>Litres one milking yields: the game's goats and sheep give 10.</summary>
    private const float MilkLitres = 10;

    /// <summary>Firewood one click puts on a pile: the game's transfer quantity, 2.</summary>
    private const int FirewoodPerClick = 2;

    /// <summary>What an item on the shelf does (question 5's starter actions).</summary>
    public static Kind KindOf(ItemStack? stack)
    {
        var item = stack?.Collectible;
        if (item == null)
        {
            return Kind.None;
        }
        return item switch
        {
            ItemPlantableSeed => Kind.Seeds,
            BlockSapling => Kind.Sapling,
            ItemFirewood => Kind.Firewood,
            ItemFirestarter => Kind.Firestarter,
            BlockLiquidContainerBase => Kind.Bucket,
            _ when item.Attributes?["fertilizerProps"].Exists == true => Kind.Fertilizer,
            _ when item.Tool == EnumTool.Shovel => Kind.Shovel,
            _ when item.Tool == EnumTool.Axe => Kind.Axe,
            _ => Kind.None,
        };
    }

    /// <summary>
    /// Whether the shelf uses this stack up as stock: the seeds, saplings, fertilizer or firewood on it,
    /// and logs while an axe is on it, so a product it uses goes back to the input (G2).
    /// </summary>
    public static bool UsesUp(IWorldAccessor world, BlockEntityControlShelf shelf, ItemStack stack)
    {
        var items = shelf.Items;
        for (int i = 0; i < items.Length; i++)
        {
            var item = items[i];
            if (shelf.IsDormant(i))
            {
                continue;
            }
            switch (KindOf(item))
            {
                case Kind.Seeds or Kind.Sapling or Kind.Fertilizer or Kind.Firewood when Same(item!, stack):
                    return true;
                // With a prefix, only logs of its kind are kept back as the axe's stock (H1).
                case Kind.Axe when Split(world, stack, item!) != null && (shelf.PrefixOf(i) is not { } kind || Same(kind, stack)):
                    return true;
            }
        }
        return false;
    }

    private static bool Same(ItemStack a, ItemStack b) => a.Collectible.Code.Equals(b.Collectible.Code);

    public static MachineWork.Result Work(BlockEntityMachine machine, BlockEntityControlShelf shelf, CastContext cast)
    {
        var reach = Reach(cast);
        var items = shelf.Items;
        for (int i = 0; i < items.Length; i++)
        {
            var item = items[i];
            // A Dormant item is passed over, as work and as stock (H2).
            if (item == null || shelf.IsDormant(i))
            {
                continue;
            }
            var prefix = shelf.PrefixOf(i);
            var result = KindOf(item) switch
            {
                Kind.Seeds => Sow(machine, cast, reach, item),
                Kind.Sapling => Plant(machine, cast, reach, item),
                Kind.Fertilizer => Feed(machine, cast, reach, item),
                Kind.Shovel => Dig(machine, cast, reach, prefix),
                Kind.Bucket => Milk(machine, cast),
                Kind.Axe => Chop(machine, reach.Count, item, prefix),
                Kind.Firewood => Stack(machine, cast, reach, item),
                Kind.Firestarter => Light(cast, reach),
                _ => MachineWork.Result.Nothing,
            };
            if (result != MachineWork.Result.Nothing)
            {
                return result;
            }
        }
        return MachineWork.Result.Nothing;
    }

    /// <summary>The blocks of a working's reach.</summary>
    public static List<BlockPos> Reach(CastContext cast) =>
        cast.Wide ? cast.Area().ToList() : cast.Center != null ? new List<BlockPos> { cast.Center.Copy() } : new List<BlockPos>();

    private static bool StockMatches(ItemStack item, ItemStack stock) => Same(item, stock);

    // ------------------------------------------------------------------ seeds, saplings, fertilizer

    /// <summary>Seeds sow each empty farmland in reach, one seed from the input each.</summary>
    private static MachineWork.Result Sow(BlockEntityMachine machine, CastContext cast, List<BlockPos> reach, ItemStack item)
    {
        var world = cast.Api.World;
        string? type = item.Collectible.Variant["type"];
        var crop = type == null || type == "bellpepper" ? null : world.GetBlock(new AssetLocation(item.Collectible.Code.Domain, "crop-" + type + "-1"));
        int stock = Stock.Count(machine.Input, s => StockMatches(item, s));
        if (crop?.CropProps == null || stock == 0)
        {
            return MachineWork.Result.Nothing;
        }
        int sown = 0;
        foreach (var pos in reach)
        {
            if (sown >= stock)
            {
                break;
            }
            if (world.BlockAccessor.GetBlockEntity(pos) is BlockEntityFarmland farmland && farmland.CanPlant() && cast.Can(pos.UpCopy())
                && farmland.TryPlant(crop, new DummySlot(item.Clone()), null!, new BlockSelection { Position = pos.Copy(), Face = BlockFacing.UP }))
            {
                sown++;
            }
        }
        return Used(machine, cast, item, sown, PlantSound);
    }

    /// <summary>A sapling plants each block of open soil in reach with air above it, one from the input each.</summary>
    private static MachineWork.Result Plant(BlockEntityMachine machine, CastContext cast, List<BlockPos> reach, ItemStack item)
    {
        var accessor = cast.Api.World.BlockAccessor;
        var sapling = item.Block;
        int stock = Stock.Count(machine.Input, s => StockMatches(item, s));
        if (sapling == null || stock == 0)
        {
            return MachineWork.Result.Nothing;
        }
        int planted = 0;
        foreach (var pos in reach)
        {
            if (planted >= stock)
            {
                break;
            }
            var soil = accessor.GetBlock(pos);
            var above = pos.UpCopy();
            if (soil.BlockMaterial != EnumBlockMaterial.Soil || soil.Fertility <= 0 || accessor.GetBlockEntity(pos) != null
                || accessor.GetBlock(above).Id != 0 || accessor.GetBlock(above, BlockLayersAccess.Fluid).Id != 0 || !cast.Can(above))
            {
                continue;
            }
            accessor.SetBlock(sapling.BlockId, above);
            accessor.GetBlockEntity(above)?.OnBlockPlaced(item.Clone());
            accessor.TriggerNeighbourBlockUpdate(above);
            planted++;
        }
        return Used(machine, cast, item, planted, PlantSound);
    }

    /// <summary>Fertilizer feeds each farmland in reach that would take it, as your click does, one from the input each.</summary>
    private static MachineWork.Result Feed(BlockEntityMachine machine, CastContext cast, List<BlockPos> reach, ItemStack item)
    {
        var world = cast.Api.World;
        var props = FertilizerOf(item.Collectible);
        int stock = Stock.Count(machine.Input, s => StockMatches(item, s));
        if (props == null || stock == 0)
        {
            return MachineWork.Result.Nothing;
        }
        string? overlay = item.Collectible.Attributes["fertilizerTextureCode"].AsString();
        int fed = 0;
        foreach (var pos in reach)
        {
            if (fed >= stock)
            {
                break;
            }
            if (world.BlockAccessor.GetBlockEntity(pos) is BlockEntityFarmland farmland && cast.Can(pos) && Fertilize(world, farmland, props, overlay))
            {
                fed++;
            }
        }
        return Used(machine, cast, item, fed, DirtSound);
    }

    /// <summary>
    /// What the game's farmland does with fertilizer in your hand (BlockEntitySoilNutrition.OnBlockInteract,
    /// which needs a player): slow-release nutrients up to 150 each and a permanent boost once. Done
    /// through its saved state, so no field the game keeps to itself is touched. Returns whether it took any.
    /// </summary>
    private static readonly System.Runtime.CompilerServices.ConditionalWeakTable<CollectibleObject, FertilizerProps> FertilizerCache = new();

    /// <summary>An item's fertilizer properties, parsed from its JSON once rather than on every turn.</summary>
    private static FertilizerProps? FertilizerOf(CollectibleObject item)
    {
        if (FertilizerCache.TryGetValue(item, out var cached))
        {
            return cached;
        }
        var props = item.Attributes?["fertilizerProps"].AsObject<FertilizerProps>();
        if (props != null)
        {
            FertilizerCache.AddOrUpdate(item, props);
        }
        return props;
    }

    private static readonly System.Reflection.FieldInfo? SlowReleaseField =
        typeof(BlockEntitySoilNutrition).GetField("slowReleaseNutrients", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

    private static readonly System.Reflection.FieldInfo? PermaBoostsField =
        typeof(BlockEntitySoilNutrition).GetField("PermaBoosts", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

    /// <summary>
    /// Whether farmland would take any of this fertilizer, read from the game's own fields without
    /// writing the farmland out (the same test as its OnBlockInteract). True when the fields cannot be
    /// read, so the full check below decides.
    /// </summary>
    private static bool MayTake(BlockEntityFarmland farmland, FertilizerProps props)
    {
        if (SlowReleaseField?.GetValue(farmland) is not float[] { Length: >= 3 } slow || PermaBoostsField?.GetValue(farmland) is not HashSet<string> boosts)
        {
            return true;
        }
        return (props.N > 0 && slow[0] < 150) || (props.P > 0 && slow[1] < 150) || (props.K > 0 && slow[2] < 150)
            || (props.PermaBoost != null && !boosts.Contains(props.PermaBoost.Code));
    }

    private static bool Fertilize(IWorldAccessor world, BlockEntityFarmland farmland, FertilizerProps props, string? overlay)
    {
        if (!MayTake(farmland, props))
        {
            return false;
        }
        var tree = new TreeAttribute();
        farmland.ToTreeAttributes(tree);
        float n = Math.Min(Math.Max(0, 150 - tree.GetFloat("slowN")), props.N);
        float p = Math.Min(Math.Max(0, 150 - tree.GetFloat("slowP")), props.P);
        float k = Math.Min(Math.Max(0, 150 - tree.GetFloat("slowK")), props.K);
        var boosts = tree.GetStringArray("permaBoosts") ?? Array.Empty<string>();
        bool boost = props.PermaBoost != null && !boosts.Contains(props.PermaBoost.Code);
        if (n <= 0 && p <= 0 && k <= 0 && !boost)
        {
            return false;
        }
        tree.SetFloat("slowN", tree.GetFloat("slowN") + n);
        tree.SetFloat("slowP", tree.GetFloat("slowP") + p);
        tree.SetFloat("slowK", tree.GetFloat("slowK") + k);
        if (boost)
        {
            tree.SetInt("originalFertilityN", tree.GetInt("originalFertilityN") + props.PermaBoost!.N);
            tree.SetInt("originalFertilityP", tree.GetInt("originalFertilityP") + props.PermaBoost.P);
            tree.SetInt("originalFertilityK", tree.GetInt("originalFertilityK") + props.PermaBoost.K);
            tree.SetStringArray("permaBoosts", boosts.Append(props.PermaBoost.Code).ToArray());
        }
        if (overlay != null)
        {
            var strengths = tree.GetTreeAttribute("fertilizerOverlayStrength") ?? new TreeAttribute();
            strengths.SetFloat(overlay, strengths.GetFloat(overlay) + Math.Max(n, Math.Max(p, k)));
            tree["fertilizerOverlayStrength"] = strengths;
        }
        farmland.FromTreeAttributes(tree, world);
        farmland.MarkDirty(true);
        return true;
    }

    /// <summary>Takes what a working used from the input and sounds once; nothing used is nothing done.</summary>
    private static MachineWork.Result Used(BlockEntityMachine machine, CastContext cast, ItemStack item, int count, AssetLocation sound)
    {
        if (count <= 0)
        {
            return MachineWork.Result.Nothing;
        }
        Stock.Take(machine.Input, s => StockMatches(item, s), count);
        Sound(cast, sound);
        return MachineWork.Result.Acted;
    }

    private static void Sound(CastContext cast, AssetLocation sound)
    {
        if (cast.Center is { } at)
        {
            cast.Api.World.PlaySoundAt(sound, at.X + 0.5, at.Y + 0.5, at.Z + 0.5, null, true, 16f, LemuriaSound.Volume(0.8f));
        }
    }

    // ------------------------------------------------------------------ shovel

    /// <summary>A loose block a shovel digs: soil, gravel, sand and finished charcoal; never tilled farmland or a block with contents.</summary>
    public static bool Diggable(IBlockAccessor accessor, BlockPos pos)
    {
        var block = accessor.GetBlock(pos);
        if (block.Id == 0 || accessor.GetBlockEntity(pos) != null)
        {
            return false;
        }
        return block.BlockMaterial is EnumBlockMaterial.Soil or EnumBlockMaterial.Sand or EnumBlockMaterial.Gravel;
    }

    /// <summary>
    /// Whether a block is of a prefix's kind (H1): it is that block, or it drops it, as a charcoal pile
    /// drops charcoal.
    /// </summary>
    public static bool OfKind(IWorldAccessor world, BlockPos pos, ItemStack prefix)
    {
        var block = world.BlockAccessor.GetBlock(pos);
        if (block.Code.Equals(prefix.Collectible.Code))
        {
            return true;
        }
        var drops = block.GetDrops(world, pos, null);
        return drops != null && drops.Any(d => d != null && Same(d, prefix));
    }

    /// <summary>
    /// A shovel digs every loose block in reach, from the top down, its drops into the output; all or
    /// nothing. With a prefix it digs only blocks of the prefix's kind.
    /// </summary>
    private static MachineWork.Result Dig(BlockEntityMachine machine, CastContext cast, List<BlockPos> reach, ItemStack? prefix)
    {
        var world = cast.Api.World;
        var accessor = world.BlockAccessor;
        var dug = reach.Where(p => Diggable(accessor, p) && cast.Can(p) && (prefix == null || OfKind(world, p, prefix))).OrderByDescending(p => p.Y).ToList();
        if (dug.Count == 0)
        {
            return MachineWork.Result.Nothing;
        }
        var drops = new List<ItemStack>();
        foreach (var pos in dug)
        {
            drops.AddRange(accessor.GetBlock(pos).GetDrops(world, pos, null)?.Where(d => d != null) ?? Enumerable.Empty<ItemStack>());
        }
        if (!machine.CanYield(drops))
        {
            return MachineWork.Result.Full;
        }
        var sound = accessor.GetBlock(dug[0]).Sounds?.Break.Location;
        foreach (var pos in dug)
        {
            accessor.SetBlock(0, pos);
        }
        foreach (var pos in dug)
        {
            accessor.TriggerNeighbourBlockUpdate(pos);
        }
        machine.Yield(drops, dug[0].ToVec3d().Add(0.5, 0.5, 0.5));
        Sound(cast, sound ?? DirtSound);
        return MachineWork.Result.Acted;
    }

    // ------------------------------------------------------------------ bucket

    /// <summary>
    /// A bucket milks each animal in reach that the game would let you milk now, 10 litres each, into a
    /// liquid container as the output (a barrel at the shelf's right). The game's own rules: once a day, for
    /// 21 days after giving birth, never while stressed. A machine never angers it.
    /// </summary>
    private static MachineWork.Result Milk(BlockEntityMachine machine, CastContext cast)
    {
        var world = cast.Api.World;
        if (machine.OutputPos is not { } outPos || world.BlockAccessor.GetBlock(outPos) is not BlockLiquidContainerBase barrel)
        {
            return MachineWork.Result.Nothing;
        }
        var ready = cast.EntitiesInArea(e => e.Alive && e.HasBehavior<EntityBehaviorMilkable>() && Milkable(e)).Where(e => cast.Can(e)).ToList();
        if (ready.Count == 0 || world.GetItem(MilkCode) is not { } milk)
        {
            return MachineWork.Result.Nothing;
        }
        var content = barrel.GetContent(outPos);
        if ((content != null && !content.Collectible.Code.Equals(milk.Code)) || barrel.GetCurrentLitres(outPos) + MilkLitres * ready.Count > barrel.CapacityLitres + 0.001f)
        {
            return MachineWork.Result.Full;
        }
        foreach (var animal in ready)
        {
            barrel.TryPutLiquid(outPos, new ItemStack(milk, 99999), MilkLitres);
            animal.WatchedAttributes.SetFloat("lastMilkedTotalHours", (float)world.Calendar.TotalHours);
        }
        world.PlaySoundAt(new AssetLocation("game", "sounds/creature/sheep/milking"), ready[0], null, true, 16f, LemuriaSound.Volume(0.8f));
        return MachineWork.Result.Acted;
    }

    /// <summary>The game's test for milking (EntityBehaviorMilkable.CanMilk, which is protected).</summary>
    private static bool Milkable(Vintagestory.API.Common.Entities.Entity animal)
    {
        var multiply = animal.GetBehavior<EntityBehaviorMultiply>();
        if (multiply == null || animal.WatchedAttributes.GetFloat("stressLevel") > 0.1f)
        {
            return false;
        }
        var calendar = animal.World.Calendar;
        if (Math.Max(0, calendar.TotalDays - multiply.TotalDaysLastBirth) >= 21)
        {
            return false;
        }
        return calendar.TotalHours - animal.WatchedAttributes.GetFloat("lastMilkedTotalHours") >= calendar.HoursPerDay;
    }

    // ------------------------------------------------------------------ axe

    private static IWorldAccessor? splitWorld;
    private static readonly Dictionary<string, ItemStack?> SplitCache = new();

    /// <summary>
    /// What one log or block splits into with an axe in the grid (the game's firewood recipes), or null.
    /// Looked up once per axe and item code and cached, since walking every grid recipe on every turn
    /// cost a gate's Focus tens of milliseconds while its pit burned (timed, Oct 29). Server only.
    /// </summary>
    public static ItemStack? Split(IWorldAccessor world, ItemStack stack, ItemStack axe)
    {
        if (!ReferenceEquals(world, splitWorld))
        {
            SplitCache.Clear();
            splitWorld = world;
        }
        string key = axe.Collectible.Code + "|" + stack.Collectible.Code;
        if (!SplitCache.TryGetValue(key, out var output))
        {
            output = FindSplit(world, stack, axe);
            SplitCache[key] = output;
        }
        return output;
    }

    private static ItemStack? FindSplit(IWorldAccessor world, ItemStack stack, ItemStack axe)
    {
        foreach (var recipe in world.GridRecipes)
        {
            var parts = recipe.ResolvedIngredients?.Where(i => i != null).ToList();
            if (parts == null || parts.Count != 2 || recipe.Output?.ResolvedItemStack is not { } output)
            {
                continue;
            }
            var tool = parts.FirstOrDefault(i => i!.IsTool);
            var other = parts.FirstOrDefault(i => !i!.IsTool);
            if (tool != null && other != null && tool.SatisfiesAsIngredient(axe) && other.SatisfiesAsIngredient(stack))
            {
                return output;
            }
        }
        return null;
    }

    /// <summary>
    /// An axe splits the input's logs, as many as the reach has blocks, 4 firewood to a log; all or
    /// nothing. With a prefix it splits only logs of that kind.
    /// </summary>
    private static MachineWork.Result Chop(BlockEntityMachine machine, int reachBlocks, ItemStack axe, ItemStack? prefix)
    {
        var world = machine.Api.World;
        var input = machine.Input;
        if (input == null || reachBlocks <= 0)
        {
            return MachineWork.Result.Nothing;
        }
        var plan = new List<(ItemSlot Slot, int Count, ItemStack Output)>();
        int left = reachBlocks;
        foreach (var slot in input)
        {
            if (left <= 0)
            {
                break;
            }
            if (slot.Itemstack is not { } stack || (prefix != null && !Same(prefix, stack)) || Split(world, stack, axe) is not { } output)
            {
                continue;
            }
            int n = Math.Min(left, stack.StackSize);
            plan.Add((slot, n, output));
            left -= n;
        }
        if (plan.Count == 0)
        {
            return MachineWork.Result.Nothing;
        }
        var yields = plan.Select(p =>
        {
            var y = p.Output.Clone();
            y.StackSize = p.Output.StackSize * p.Count;
            return y;
        }).ToList();
        // The logs leave first, so their room in the input counts for the firewood that goes back.
        var taken = plan.Select(p => (p.Slot, p.Slot.TakeOut(p.Count))).ToList();
        foreach (var (slot, _) in taken)
        {
            slot.MarkDirty();
        }
        if (!machine.CanYield(yields))
        {
            foreach (var (slot, stack) in taken)
            {
                if (stack != null)
                {
                    new DummySlot(stack).TryPutInto(world, slot, stack.StackSize);
                    slot.MarkDirty();
                }
            }
            return MachineWork.Result.Full;
        }
        var at = machine.Shelf?.Pos.ToVec3d().Add(0.5, 1.2, 0.5) ?? machine.Pos.ToVec3d().Add(0.5, 1, 0.5);
        machine.Yield(yields, at);
        var shelfPos = machine.Shelf?.Pos ?? machine.Pos;
        world.PlaySoundAt(ChopSound, shelfPos.X + 0.5, shelfPos.Y + 0.5, shelfPos.Z + 0.5, null, true, 16f, LemuriaSound.Volume(0.8f));
        return MachineWork.Result.Acted;
    }

    // ------------------------------------------------------------------ firewood and the charcoal pit

    /// <summary>A pile of this firewood, and whether it is full.</summary>
    private static (BlockEntityGroundStorage Pile, bool Full)? Pile(IBlockAccessor accessor, BlockPos pos, ItemStack firewood)
    {
        if (accessor.GetBlockEntity(pos) is not BlockEntityGroundStorage pile || pile.Inventory[0]?.Itemstack is not { } held || !Same(held, firewood)
            || pile.Inventory.Skip(1).Any(s => !s.Empty))
        {
            return null;
        }
        return (pile, held.StackSize >= pile.Capacity);
    }

    /// <summary>Whether a firewood pile may start here: an open cell on solid ground or on a full pile, as your click would.</summary>
    private static bool CanStartPile(IBlockAccessor accessor, BlockPos pos, Block groundStorage)
    {
        if (accessor.GetBlock(pos).Id != 0 || accessor.GetBlock(pos, BlockLayersAccess.Fluid).Id != 0)
        {
            return false;
        }
        var below = pos.DownCopy();
        return accessor.GetBlock(below).CanAttachBlockAt(accessor, groundStorage, below, BlockFacing.UP);
    }

    /// <summary>
    /// Firewood (Charcoal's second rung): adds 2 to each pile of it in reach that is not full and starts a
    /// pile in each open cell on solid ground or a full pile, as your clicks would. The cell straight in
    /// front of the machine, when a solid block roofs it, is kept for last: once nothing else in reach has
    /// work, firewood builds the firepit there on the full pile beneath, one stage a working, and the game
    /// makes it a charcoal pit. Firewood comes from the input.
    /// </summary>
    private static MachineWork.Result Stack(BlockEntityMachine machine, CastContext cast, List<BlockPos> reach, ItemStack firewood)
    {
        var world = cast.Api.World;
        var accessor = world.BlockAccessor;
        int stock = Stock.Count(machine.Input, s => StockMatches(firewood, s));
        if (stock == 0 || world.GetBlock(GroundStorageCode) is not { } groundStorage)
        {
            return MachineWork.Result.Nothing;
        }
        var front = machine.Pos.AddCopy(machine.Front);
        bool roofed = accessor.GetBlock(front.UpCopy()).SideSolid[BlockFacing.DOWN.Index];
        int used = 0;
        bool acted = false;
        foreach (var pos in reach.OrderBy(p => p.Y))
        {
            if (stock - used <= 0)
            {
                break;
            }
            if (roofed && pos.Equals(front) || !cast.Can(pos))
            {
                continue;
            }
            if (Pile(accessor, pos, firewood) is { Full: false } pile)
            {
                var held = pile.Pile.Inventory[0].Itemstack!;
                int add = Math.Min(Math.Min(FirewoodPerClick, pile.Pile.Capacity - held.StackSize), stock - used);
                held.StackSize += add;
                pile.Pile.Inventory[0].MarkDirty();
                pile.Pile.DetermineStorageProperties(null);
                pile.Pile.MarkDirty(true);
                accessor.TriggerNeighbourBlockUpdate(pos);
                used += add;
                acted = true;
            }
            else if (CanStartPile(accessor, pos, groundStorage))
            {
                accessor.SetBlock(groundStorage.BlockId, pos);
                if (accessor.GetBlockEntity(pos) is not BlockEntityGroundStorage started)
                {
                    accessor.SetBlock(0, pos);
                    continue;
                }
                var stack = firewood.Clone();
                stack.StackSize = Math.Min(FirewoodPerClick, stock - used);
                started.Inventory[0].Itemstack = stack;
                started.Inventory[0].MarkDirty();
                started.DetermineStorageProperties(null);
                started.MarkDirty(true);
                accessor.TriggerNeighbourBlockUpdate(pos);
                used += stack.StackSize;
                acted = true;
            }
        }
        if (!acted && roofed && reach.Any(p => p.Equals(front)) && cast.Can(front) && Pile(accessor, front.DownCopy(), firewood) is { Full: true })
        {
            acted = BuildFirepit(world, front, firewood);
            used = acted ? 1 : 0;
        }
        if (!acted)
        {
            return MachineWork.Result.Nothing;
        }
        Stock.Take(machine.Input, s => StockMatches(firewood, s), used);
        Sound(cast, PileSound);
        return MachineWork.Result.Acted;
    }

    /// <summary>
    /// One stage of the firepit over a full pile: firewood lays its first stage where your dry grass
    /// would, then each stage after it as your clicks do, and on the last the game turns it into a
    /// charcoal pit (BlockFirepit.TryConstruct). Returns whether it built a stage.
    /// </summary>
    private static bool BuildFirepit(IWorldAccessor world, BlockPos pos, ItemStack firewood)
    {
        var accessor = world.BlockAccessor;
        var block = accessor.GetBlock(pos);
        if (block.Id == 0 && accessor.GetBlock(pos, BlockLayersAccess.Fluid).Id == 0)
        {
            if (world.GetBlock(FirepitStartCode) is not { } start)
            {
                return false;
            }
            accessor.SetBlock(start.BlockId, pos);
            accessor.TriggerNeighbourBlockUpdate(pos);
            return true;
        }
        return block is BlockFirepit { Stage: < 5 } firepit && firepit.TryConstruct(world, pos, firewood.Collectible, null!);
    }

    // ------------------------------------------------------------------ firestarter

    /// <summary>
    /// A firestarter lights what one lights in reach, a built charcoal pit or a firepit with fuel in it,
    /// but never a pile on the ground or a bomb, so a pit whose wood ran out waits instead of burning.
    /// </summary>
    private static MachineWork.Result Light(CastContext cast, List<BlockPos> reach)
    {
        var accessor = cast.Api.World.BlockAccessor;
        bool lit = false;
        foreach (var pos in reach)
        {
            if (!cast.Can(pos))
            {
                continue;
            }
            switch (accessor.GetBlockEntity(pos))
            {
                case BlockEntityCharcoalPit { Lit: false } pit:
                    pit.IgniteNow();
                    lit = true;
                    break;
                case BlockEntityFirepit firepit when !firepit.IsBurning && !firepit.fuelSlot.Empty && accessor.GetBlock(pos) is BlockFirepit { Stage: 5 }:
                    firepit.igniteFuel();
                    lit = true;
                    break;
            }
        }
        if (!lit)
        {
            return MachineWork.Result.Nothing;
        }
        Sound(cast, IgniteSound);
        return MachineWork.Result.Acted;
    }
}
