using System;
using System.Collections.Generic;
using System.Linq;
using Lemuria.Core;
using Lemuria.Crystals;
using Lemuria.Machines;
using Lemuria.Sparkstone;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;
using Vintagestory.GameContent;

namespace Lemuria.Duets;

/// <summary>
/// One working of a duet (feature 10's brief, items 3 to 8): the pair awake in the Memory against a Focus
/// gives the turn, once every 10 seconds, and the Focus works the duet from its front at the lower of the
/// two Evolutions, an Amplify lifting it one for twice the charge. It costs both crystals' working cost,
/// paid in full or not at all, and nothing when it finds nothing to make. After each working the Memory
/// swaps its pair with a Duet Module's, when the module holds a duet's pair. Machines asks it through
/// <see cref="MachineWork.DuetHook"/>. Server only.
/// </summary>
public static class DuetWork
{
    private static readonly AssetLocation ClayCode = new("game", "rawclay-blue-none");
    private static readonly AssetLocation WaterCode = new("game", "waterportion");
    private static readonly AssetLocation PourSound = new("game", "sounds/effect/water-pour");
    private static readonly AssetLocation FireSound = new("game", "sounds/effect/extinguish");
    private static readonly AssetLocation BitSound = new("game", "sounds/effect/anvilhit1");

    private static LemuriaConfig Config => LemuriaModSystem.Config;

    public static double WaterLitres => Math.Clamp(Config.DuetWaterLitres, 0, 1000);

    public static int ItemsPerWorking => Math.Clamp(Config.DuetItemsPerWorking, 0, 1000);

    /// <summary>What a duet's power makes of the Glittering amount: once, twice at Glowing, four times amplified Glowing.</summary>
    public static int Times(int power) => power >= 3 ? 4 : Math.Max(1, power);

    /// <summary>The hook: works the Memory's pair as a duet, or returns null when it lends none.</summary>
    public static MachineWork.Result? Work(BlockEntityMachine machine, BlockEntityMemory memory)
    {
        if (memory.Pair is not { } pair || Duet.ByProduct(pair.Product) is not { } duet || machine.Api is not ICoreServerAPI api)
        {
            return null;
        }
        int rank = Math.Min(Crystal.Rank(pair.A.Evolution), Crystal.Rank(pair.B.Evolution));
        bool amplified = machine.AmplifyRank() >= rank;
        int power = rank + (amplified ? 1 : 0);
        float cost = Machine.WorkCost(pair.A.Evolution, amplified) + Machine.WorkCost(pair.B.Evolution, amplified);
        var networks = SparkstoneModSystem.Networks;
        if (networks == null || !networks.CanPay(machine, cost))
        {
            return MachineWork.Result.Unpaid;
        }
        var result = duet.Product switch
        {
            Duet.Clay => Clay(machine, api),
            Duet.Water => Water(machine, power),
            Duet.FiredClay => Extract(machine, power, Fired, FireSound),
            Duet.Metal => Extract(machine, power, Bit, BitSound),
            _ => MachineWork.Result.Nothing,
        };
        if (result == MachineWork.Result.Acted)
        {
            networks.TryPay(machine, cost);
            var front = Machine.FrontPoint(machine.Pos, machine.Front);
            CrystalCharging.Sparks(api.World, front, Crystal.Rgb(duet.First), 3, 0.3f);
            CrystalCharging.Sparks(api.World, front, Crystal.Rgb(duet.Second), 3, 0.3f);
        }
        Swap(machine, memory);
        return result;
    }

    /// <summary>
    /// After each duet working the Memory swaps its pair with the Duet Module's, both crystals together,
    /// so the machine alternates two duets; nothing swaps when the module holds no duet's pair.
    /// </summary>
    public static bool Swap(BlockEntityMachine machine, BlockEntityMemory memory)
    {
        var held = machine.ModuleCrystals;
        if (!machine.HoldsModule || held.Count != 2 || held.Any(c => c.Dormant) || Duet.Name(held[0], held[1]) == null)
        {
            return false;
        }
        var face = machine.SocketFacing ?? machine.Front;
        var fromModule = held.Select(machine.TakeOutExactly).ToList();
        var fromMemory = memory.SwapPair(fromModule);
        foreach (var stack in fromMemory)
        {
            machine.Insert(stack, face);
        }
        return true;
    }

    // ------------------------------------------------------------------ the four products

    /// <summary>Ice and Earth: the soil or sand block just in front of the Focus becomes the game's raw blue clay, one block a working.</summary>
    private static MachineWork.Result Clay(BlockEntityMachine machine, ICoreServerAPI api)
    {
        var world = api.World;
        var pos = machine.Pos.AddCopy(machine.Front);
        if (!machine.MayChange(pos) || world.GetBlock(ClayCode) is not { } clay)
        {
            return MachineWork.Result.Nothing;
        }
        var block = world.BlockAccessor.GetBlock(pos, BlockLayersAccess.Solid);
        if (block?.Code?.Domain != "game" || !(block.Code.Path.StartsWith("soil-", StringComparison.Ordinal) || block.Code.Path.StartsWith("sand-", StringComparison.Ordinal))
            || world.BlockAccessor.GetBlockEntity(pos) != null)
        {
            return MachineWork.Result.Nothing;
        }
        world.BlockAccessor.SetBlock(clay.BlockId, pos);
        world.BlockAccessor.TriggerNeighbourBlockUpdate(pos);
        if (clay.Sounds?.Place.Location is { } sound)
        {
            world.PlaySoundAt(sound, pos.X + 0.5, pos.Y + 0.5, pos.Z + 0.5, null, true, 16f, LemuriaSound.Volume(0.8f));
        }
        return MachineWork.Result.Acted;
    }

    /// <summary>Ice and Sky: fresh water drawn from the air into the liquid container at the output, 10 litres at Glittering.</summary>
    private static MachineWork.Result Water(BlockEntityMachine machine, int power)
    {
        var world = machine.Api.World;
        if (machine.OutputPos is not { } pos || world.BlockAccessor.GetBlock(pos) is not BlockLiquidContainerBase container
            || world.GetItem(WaterCode) is not { } water)
        {
            return MachineWork.Result.Nothing;
        }
        float litres = (float)(WaterLitres * Times(power));
        var stack = new ItemStack(water);
        float perLitre = BlockLiquidContainerBase.GetContainableProps(stack)?.ItemsPerLitre ?? 100;
        stack.StackSize = Math.Max(1, (int)Math.Ceiling(litres * perLitre));
        if (litres <= 0 || container.TryPutLiquid(pos, stack, litres) <= 0)
        {
            return MachineWork.Result.Nothing;
        }
        world.BlockAccessor.GetBlockEntity(pos)?.MarkDirty(true);
        world.PlaySoundAt(PourSound, pos.X + 0.5, pos.Y + 0.5, pos.Z + 0.5, null, true, 16f, LemuriaSound.Volume(0.6f));
        return MachineWork.Result.Acted;
    }

    /// <summary>Fire and Sky: what one raw item fires into in the game's pit kiln, or null for anything the pit kiln does not fire.</summary>
    public static ItemStack? Fired(IWorldAccessor world, ItemStack raw)
    {
        var props = raw.Collectible?.CombustibleProps;
        if (props == null || props.SmeltingType != EnumSmeltType.Fire || props.SmeltedRatio > 1 || props.SmeltedStack == null)
        {
            return null;
        }
        if (props.SmeltedStack.ResolvedItemstack == null && !props.SmeltedStack.Resolve(world, "lemuria duet"))
        {
            return null;
        }
        if (props.SmeltedStack.ResolvedItemstack?.Clone() is not { } fired)
        {
            return null;
        }
        fired.StackSize = 1;
        return fired;
    }

    /// <summary>
    /// Fire and Earth: one metal bit from one ore nugget whose smelted form is an ingot (copper, tin, zinc,
    /// lead, silver, gold, bismuth, chromium, titanium, platinum, nickel); the iron ores, whose smelted
    /// form is a bloom, give nothing.
    /// </summary>
    public static ItemStack? Bit(IWorldAccessor world, ItemStack nugget)
    {
        var code = nugget.Collectible?.Code;
        if (code?.Domain != "game" || !code.Path.StartsWith("nugget-", StringComparison.Ordinal))
        {
            return null;
        }
        var smelted = nugget.Collectible!.CombustibleProps?.SmeltedStack;
        string? path = smelted?.Code?.Path;
        if (path == null || !path.StartsWith("ingot-", StringComparison.Ordinal))
        {
            return null;
        }
        return world.GetItem(new AssetLocation("game", "metalbit-" + path["ingot-".Length..])) is { } bit ? new ItemStack(bit) : null;
    }

    /// <summary>
    /// Takes up to the working's items from the input, one product for each, into the output; nothing to
    /// take is a free miss, and products with no room send the machine Dormant without working.
    /// </summary>
    private static MachineWork.Result Extract(BlockEntityMachine machine, int power, System.Func<IWorldAccessor, ItemStack, ItemStack?> make, AssetLocation sound)
    {
        var world = machine.Api.World;
        if (machine.Input is not { } input)
        {
            return MachineWork.Result.Nothing;
        }
        int left = ItemsPerWorking * Times(power);
        var plan = new List<(ItemSlot Slot, int Count)>();
        var products = new List<ItemStack>();
        foreach (var slot in input)
        {
            if (left <= 0)
            {
                break;
            }
            if (slot.Itemstack is not { } stack || make(world, stack) is not { } one)
            {
                continue;
            }
            int n = Math.Min(left, stack.StackSize);
            one.StackSize = n;
            plan.Add((slot, n));
            products.Add(one);
            left -= n;
        }
        if (plan.Count == 0)
        {
            return MachineWork.Result.Nothing;
        }
        if (!machine.CanYield(products))
        {
            return MachineWork.Result.Full;
        }
        foreach (var (slot, n) in plan)
        {
            slot.TakeOut(n);
            slot.MarkDirty();
        }
        var at = Machine.FrontPoint(machine.Pos, machine.Front);
        machine.Yield(products, at);
        world.PlaySoundAt(sound, at.X, at.Y, at.Z, null, true, 16f, LemuriaSound.Volume(0.6f));
        return MachineWork.Result.Acted;
    }
}
