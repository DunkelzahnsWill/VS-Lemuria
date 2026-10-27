using Lemuria.Crystals;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace Lemuria.Sparkstone;

/// <summary>Awakened sparkstone: hums by its network's charge level, a full stone block that drops itself, empty.</summary>
public class BlockSparkstone : Block
{
    public const string ClassCode = "lemuria-sparkstone";

    public override float GetAmbientSoundStrength(IWorldAccessor world, BlockPos pos)
    {
        float strength = (world.BlockAccessor.GetBlockEntity(pos) as BlockEntitySparkstone)?.HumStrength ?? 0.1f;
        return world.Side == EnumAppSide.Client ? strength * HumBudget.Share : strength;
    }
}
