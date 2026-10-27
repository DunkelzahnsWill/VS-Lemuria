using System.Collections.Generic;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace Lemuria.Cradle;

/// <summary>
/// The cradle, a carved stone basin in one rock type. Its block entity holds the seedling and its
/// growth stage; this class gives that state to the world: drops, light, hum and the right-click.
/// </summary>
public class BlockCradle : Block
{
    public const string ClassCode = "lemuria-cradle";

    private WorldInteraction[]? plantHelp;

    public override void OnLoaded(ICoreAPI api)
    {
        base.OnLoaded(api);
        var seedlings = new List<ItemStack>();
        foreach (var item in api.World.Items)
        {
            if (BlockEntityCradle.IsSeedling(item))
            {
                seedlings.Add(new ItemStack(item));
            }
        }
        plantHelp = new[]
        {
            new WorldInteraction { ActionLangCode = "lemuria:cradle-plant", MouseButton = EnumMouseButton.Right, Itemstacks = seedlings.ToArray() },
        };
    }

    /// <summary>A cradle stands only on top of a solid block.</summary>
    public override bool CanPlaceBlock(IWorldAccessor world, IPlayer byPlayer, BlockSelection blockSel, ref string failureCode)
    {
        if (!base.CanPlaceBlock(world, byPlayer, blockSel, ref failureCode))
        {
            return false;
        }
        var below = blockSel.Position.DownCopy();
        if (!world.BlockAccessor.GetBlock(below).CanAttachBlockAt(world.BlockAccessor, this, below, BlockFacing.UP))
        {
            failureCode = "requiresolidground";
            return false;
        }
        return true;
    }

    public override bool OnBlockInteractStart(IWorldAccessor world, IPlayer byPlayer, BlockSelection blockSel)
    {
        if (world.BlockAccessor.GetBlockEntity(blockSel.Position) is BlockEntityCradle cradle && cradle.OnInteract(byPlayer))
        {
            return true;
        }
        return base.OnBlockInteractStart(world, byPlayer, blockSel);
    }

    /// <summary>The cradle itself, plus its seedling while it grows or its crystal when ripe, so nothing is lost.</summary>
    public override ItemStack[] GetDrops(IWorldAccessor world, BlockPos pos, IPlayer byPlayer, float dropQuantityMultiplier = 1f)
    {
        var drops = new List<ItemStack>(base.GetDrops(world, pos, byPlayer, dropQuantityMultiplier) ?? System.Array.Empty<ItemStack>());
        var content = (world.BlockAccessor.GetBlockEntity(pos) as BlockEntityCradle)?.ContentStack();
        if (content != null)
        {
            drops.Add(content);
        }
        return drops.ToArray();
    }

    public override byte[] GetLightHsv(IBlockAccessor blockAccessor, BlockPos pos, ItemStack? stack = null)
    {
        if (pos != null && blockAccessor.GetBlockEntity(pos) is BlockEntityCradle cradle)
        {
            return cradle.LightHsv;
        }
        return base.GetLightHsv(blockAccessor, pos, stack);
    }

    public override float GetAmbientSoundStrength(IWorldAccessor world, BlockPos pos)
    {
        return (world.BlockAccessor.GetBlockEntity(pos) as BlockEntityCradle)?.HumStrength ?? 0f;
    }

    /// <summary>The look-at text shows only the block's name: the stage is read by eye and ear.</summary>
    public override string GetPlacedBlockInfo(IWorldAccessor world, BlockPos pos, IPlayer forPlayer) => "";

    public override WorldInteraction[] GetPlacedBlockInteractionHelp(IWorldAccessor world, BlockSelection selection, IPlayer forPlayer)
    {
        var cradle = world.BlockAccessor.GetBlockEntity(selection.Position) as BlockEntityCradle;
        if (cradle != null && cradle.IsEmpty && plantHelp != null)
        {
            return plantHelp;
        }
        return base.GetPlacedBlockInteractionHelp(world, selection, forPlayer);
    }
}
