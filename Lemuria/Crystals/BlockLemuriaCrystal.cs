using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace Lemuria.Crystals;

/// <summary>
/// A grown crystal standing on any face, as the game's geode crystals do (the OmniAttachable behavior
/// does the placing). It glows and lights from its JSON and hums at the strength in its attributes.
/// </summary>
public class BlockLemuriaCrystal : Block
{
    public const string ClassCode = "lemuria-crystal";

    private float humStrength;

    public override void OnLoaded(ICoreAPI api)
    {
        base.OnLoaded(api);
        humStrength = Attributes?["humStrength"].AsFloat(0.5f) ?? 0.5f;
    }

    // The client sums nearby hums; with sounds.ambientBlockCount 1 a single crystal plays at this strength.
    public override float GetAmbientSoundStrength(IWorldAccessor world, BlockPos pos) => humStrength;
}
