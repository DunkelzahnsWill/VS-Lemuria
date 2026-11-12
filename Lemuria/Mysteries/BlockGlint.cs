using System.Collections.Generic;
using Lemuria.Core;
using Lemuria.Crystals;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace Lemuria.Mysteries;

/// <summary>
/// The Glint block: framed glass with a Life crystal set inside (<see cref="BlockEntityGlint"/>). Its light
/// comes from its crystal; right-click sets a Life crystal or takes it out.
/// </summary>
public class BlockGlint : Block
{
    public const string ClassCode = "lemuria-glint";

    private WorldInteraction[]? help;

    public override void OnLoaded(ICoreAPI api)
    {
        base.OnLoaded(api);
        var life = new List<ItemStack>();
        foreach (string evolution in new[] { Crystal.Glittering, Crystal.Glowing })
        {
            if (Crystal.BlockFor(api.World, Crystal.Life, evolution) is { } block)
            {
                life.Add(new ItemStack(block));
            }
        }
        help = new[]
        {
            new WorldInteraction { ActionLangCode = LemuriaModSystem.ModId + ":glint-set", MouseButton = EnumMouseButton.Right, Itemstacks = life.ToArray() },
        };
    }

    public override byte[] GetLightHsv(IBlockAccessor blockAccessor, BlockPos pos, ItemStack? stack = null)
    {
        if (pos != null && blockAccessor.GetBlockEntity(pos) is BlockEntityGlint glint)
        {
            return glint.LightHsv;
        }
        return base.GetLightHsv(blockAccessor, pos, stack);
    }

    public override bool OnBlockInteractStart(IWorldAccessor world, IPlayer byPlayer, BlockSelection blockSel)
    {
        if (world.BlockAccessor.GetBlockEntity(blockSel.Position) is BlockEntityGlint glint)
        {
            if (!LemuriaClaims.TryAccess(world, byPlayer, blockSel.Position, EnumBlockAccessFlags.Use))
            {
                return false;
            }
            if (glint.OnInteract(byPlayer))
            {
                return true;
            }
        }
        return base.OnBlockInteractStart(world, byPlayer, blockSel);
    }

    public override WorldInteraction[] GetPlacedBlockInteractionHelp(IWorldAccessor world, BlockSelection selection, IPlayer forPlayer) =>
        help ?? base.GetPlacedBlockInteractionHelp(world, selection, forPlayer);
}
