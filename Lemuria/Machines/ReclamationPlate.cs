using Lemuria.Evocations;
using Lemuria.Sparkstone;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace Lemuria.Machines;

/// <summary>
/// The reclamation plate (step 4 part 2 note, How each piece behaves; question 9): a thin plate laid
/// under salt water. It needs no charge and no crystal. When a Fire Focus, not a hand cast, boils salt
/// water anywhere in the column above it, it claims the game's own salt, 1 time in 4 for each block
/// boiled, into a container, chute or hopper touching it; with nowhere to put it the salt is lost and
/// the water still boils. It wakes only when water above it boils, through <see cref="Effects.Boiled"/>.
/// </summary>
public class BlockReclamationPlate : Block
{
    public const string ClassCode = "lemuria-reclamationplate";

    private static readonly AssetLocation SaltCode = new("game", "salt");

    /// <summary>How far down through water and air the boiled block's column is followed to its plate.</summary>
    private const int Depth = 32;

    /// <summary>The plate under a boiled block of salt water, if one lies at the foot of its column.</summary>
    public static BlockPos? PlateUnder(IBlockAccessor accessor, BlockPos boiled)
    {
        var pos = boiled.Copy();
        for (int i = 0; i < Depth && pos.Y > 0; i++, pos.Down())
        {
            var block = accessor.GetBlock(pos);
            if (block is BlockReclamationPlate)
            {
                return pos;
            }
            if (block.Id != 0 && block.Replaceable < 6000)
            {
                return null;
            }
        }
        return null;
    }

    /// <summary>A machine's boiling of one block of salt water, claimed by the plate under it. Server only.</summary>
    public static void OnBoiled(CastContext cast, BlockPos pos, Block water)
    {
        if (cast.Machine == null || water.LiquidCode != "saltwater")
        {
            return;
        }
        var world = cast.Api.World;
        if (PlateUnder(world.BlockAccessor, pos) is not { } plate || world.Rand.NextDouble() >= Machine.SaltChance || world.GetItem(SaltCode) is not { } salt)
        {
            return;
        }
        var stack = new ItemStack(salt);
        foreach (var face in BlockFacing.ALLFACES)
        {
            if (world.BlockAccessor.GetBlockEntity(plate.AddCopy(face)) is IBlockEntityContainer { Inventory: { } inventory } and not BlockEntitySparkstone and not BlockEntityControlShelf
                && Stock.Put(world, inventory, stack) == null)
            {
                return;
            }
        }
    }
}
