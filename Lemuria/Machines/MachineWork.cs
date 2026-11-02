using System;
using System.Collections.Generic;
using Lemuria.Core;
using Lemuria.Cradle;
using Lemuria.Crystals;
using Lemuria.Evocations;
using Lemuria.Sparkstone;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;
using Vintagestory.GameContent;

namespace Lemuria.Machines;

/// <summary>
/// One working of a Focus or a Scatter (design note "Lemuria step 4: Machines", Focus, Scatter,
/// Amplify, The first secret): the crystal casts its evocation from the machine's front, through the
/// same ray and effects a hand's cast uses, for a tenth of the charge and only when it changed
/// something. A Scatter works one Evolution of power lower on a flat 9×9; an Amplify touching the
/// machine lifts a working one Evolution for twice the charge. Earth raises its tower once and goes
/// Dormant in a Focus and tills in a Scatter; Unaligned quickens cradles.
/// </summary>
public static class MachineWork
{
    public enum Result
    {
        /// <summary>No crystal to work, or a Glittering crystal in a Scatter.</summary>
        Idle,
        /// <summary>The network could not pay.</summary>
        Unpaid,
        /// <summary>The working found nothing to change, free.</summary>
        Nothing,
        Acted,
        /// <summary>An Earth Focus that could not rise: it warbles and goes Dormant, free.</summary>
        Failed,
        /// <summary>The yield would not fit the output: the machine goes Dormant without working, free (step 4 part 2, question 7).</summary>
        Full,
    }

    /// <summary>Raised on the server the first time any Focus or Scatter quickens a cradle, with the machine; the Journal listens.</summary>
    public static event Action<BlockEntityMachine>? Quickened;

    public static Result Work(BlockEntityMachine machine, SocketCrystal crystal)
    {
        var api = (ICoreServerAPI)machine.Api;
        bool scatter = machine.Form == Machine.Scatter;
        int rank = Crystal.Rank(crystal.Evolution);
        if (scatter && rank <= 1)
        {
            // A Glittering crystal has no power to give up (F3).
            return Result.Idle;
        }
        bool amplified = machine.AmplifyRank() >= rank;
        int power = rank + (amplified ? 1 : 0) - (scatter ? 1 : 0);
        float cost = Machine.WorkCost(crystal.Evolution, amplified);
        var networks = SparkstoneModSystem.Networks;
        if (networks == null || !networks.CanPay(machine, cost))
        {
            return Result.Unpaid;
        }
        string alignment = crystal.Alignment ?? Crystal.Unaligned;
        var front = machine.Front;
        var from = Machine.FrontPoint(machine.Pos, front);
        var options = new MachineCast
        {
            Claim = machine.MayChange,
            ActorName = machine.CarverName,
            Square = scatter ? Machine.ScatterSize : 0,
            CropPower = scatter ? 2 : 3,
            Replant = true,
            Collect = machine.Route,
            // Facing up or down, a 2×2 reaches north and east.
            Heading = front.IsVertical ? (0, -1, 1, 0) : null,
        };
        var cast = CastContext.Aim(api, null, null, alignment, power, from, front.Normald.Clone(), options);
        Result result;
        float paid = cost;
        if (alignment == Crystal.Unaligned)
        {
            // A cradle comes first; then the shelf's items, first to last (step 4 part 2, G1: only Unaligned crystals work the shelf).
            result = Quicken(machine, cast) ? Result.Acted : Result.Nothing;
            if (result == Result.Nothing && machine.Shelf is { } shelf)
            {
                result = ShelfWork.Work(machine, shelf, cast);
            }
        }
        else if (alignment == Crystal.Earth && scatter)
        {
            result = Till(cast) ? Result.Acted : Result.Nothing;
        }
        else if (alignment == Crystal.Earth && front != BlockFacing.DOWN)
        {
            // Facing sideways or up, an Earth Focus bores (question 12), then goes Dormant as a rise does.
            result = Boring.Bore(machine, cast);
            if (result == Result.Nothing)
            {
                result = Result.Failed;
            }
            if (result != Result.Full)
            {
                machine.SetActive(false);
            }
        }
        else if (alignment == Crystal.Sky && !scatter && Felling.TreeAt(cast) is { } tree)
        {
            // A Sky Focus aimed at a tree's log fells it whole, paying by its logs in reaches (question 6).
            result = Felling.Fell(machine, cast, tree, cost, out paid);
        }
        else
        {
            result = Effects.Apply(cast) switch
            {
                Effects.Outcome.Acted => Result.Acted,
                Effects.Outcome.Failed => Result.Failed,
                _ => Result.Nothing,
            };
            if (alignment == Crystal.Earth)
            {
                // The tower rises once, then the Focus rests until a rod tap wakes it (F7); out of reach or blocked, free.
                if (result == Result.Nothing)
                {
                    result = Result.Failed;
                }
                machine.SetActive(false);
            }
        }
        if (result == Result.Acted)
        {
            networks.TryPay(machine, paid);
        }
        if (result is Result.Acted or Result.Failed)
        {
            EvocationsModSystem.ServerSide?.SendCast(cast, from, result == Result.Acted ? Effects.Outcome.Acted : Effects.Outcome.Failed);
        }
        return result;
    }

    /// <summary>
    /// Quicken (note, The first secret): every cradle where a seedling grows, at the block aimed at, or
    /// in the area from Glowing's power up and across a Scatter's square, moves on by the growth one
    /// working's interval brings, times QuickenShare, twice that at Glowing and four times at Luminous.
    /// </summary>
    private static bool Quicken(BlockEntityMachine machine, CastContext cast)
    {
        var world = cast.Api.World;
        double hours = Machine.GameHours(world, Machine.WorkSeconds) * Machine.QuickenShare * (cast.Power >= 3 ? 4 : cast.Power);
        bool acted = false;
        IEnumerable<BlockPos> blocks = cast.Wide ? cast.Area() : cast.Center != null ? new[] { cast.Center } : Array.Empty<BlockPos>();
        foreach (var pos in blocks)
        {
            if (cast.Can(pos) && world.BlockAccessor.GetBlockEntity(pos) is BlockEntityCradle cradle && cradle.Quicken(hours))
            {
                acted = true;
            }
        }
        if (acted)
        {
            Quickened?.Invoke(machine);
        }
        return acted;
    }

    /// <summary>
    /// An Earth Scatter tills soil across its square into the game's farmland, as a hoe does
    /// (ItemHoe.DoTill), where nothing stands on it; raising ground stays a Focus's work.
    /// </summary>
    private static bool Till(CastContext cast)
    {
        var world = cast.Api.World;
        var accessor = world.BlockAccessor;
        int tilled = 0;
        Block? soil = null;
        foreach (var pos in cast.Area())
        {
            var block = accessor.GetBlock(pos);
            if (block.Code?.Domain != "game" || !block.Code.Path.StartsWith("soil-", StringComparison.Ordinal) || !cast.Can(pos))
            {
                continue;
            }
            var above = pos.UpCopy();
            if (accessor.GetBlock(above).Id != 0 || accessor.GetBlock(above, BlockLayersAccess.Fluid).Id != 0)
            {
                continue;
            }
            var farmland = world.GetBlock(new AssetLocation("game", "farmland-dry-" + block.LastCodePart(1)));
            if (farmland == null)
            {
                continue;
            }
            TreeAttribute? previous = null;
            if (accessor.GetBlockEntity(pos) is BlockEntitySoilNutrition nutrition)
            {
                previous = new TreeAttribute();
                nutrition.ToTreeAttributes(previous);
            }
            accessor.SetBlock(farmland.BlockId, pos);
            (accessor.GetBlockEntity(pos) as BlockEntityFarmland)?.OnCreatedFromSoil(block, previous);
            accessor.MarkBlockDirty(pos);
            soil = block;
            tilled++;
        }
        if (soil?.Sounds?.Place.Location is { } sound && cast.Center != null)
        {
            world.PlaySoundAt(sound, cast.Center.X + 0.5, cast.Center.Y + 1, cast.Center.Z + 0.5, null, true, 16f, LemuriaSound.Volume(0.6f));
        }
        return tilled > 0;
    }
}
