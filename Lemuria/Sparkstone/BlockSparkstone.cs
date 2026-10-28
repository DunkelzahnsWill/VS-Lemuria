using Lemuria.Crystals;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace Lemuria.Sparkstone;

/// <summary>
/// Awakened sparkstone: hums by its network's charge level, a full stone block that drops itself, empty.
/// A plain right-click with a crystal sets it into the block's sockets, and with an empty hand takes
/// one back (step 4 note, Sockets and the Complexity Rating).
/// </summary>
public class BlockSparkstone : Block
{
    public const string ClassCode = "lemuria-sparkstone";

    public override float GetAmbientSoundStrength(IWorldAccessor world, BlockPos pos) => HumAt(world, pos);

    /// <summary>The hum of the stone at a position, shared among the notes nearby on a client.</summary>
    public static float HumAt(IWorldAccessor world, BlockPos pos)
    {
        float strength = (world.BlockAccessor.GetBlockEntity(pos) as BlockEntitySparkstone)?.HumStrength ?? 0.1f;
        return world.Side == EnumAppSide.Client ? strength * HumBudget.Share : strength;
    }

    public override bool OnBlockInteractStart(IWorldAccessor world, IPlayer byPlayer, BlockSelection blockSel) =>
        Interact(world, byPlayer, blockSel) || base.OnBlockInteractStart(world, byPlayer, blockSel);

    /// <summary>The sockets' share of a right-click on any Awakened block, carved or Free.</summary>
    public static bool Interact(IWorldAccessor world, IPlayer byPlayer, BlockSelection blockSel) =>
        world.BlockAccessor.GetBlockEntity(blockSel.Position) is BlockEntitySparkstone stone && stone.OnInteract(byPlayer, blockSel);
}
