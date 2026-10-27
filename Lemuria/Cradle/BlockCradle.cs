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

    /// <summary>The basin's sounds with each alignment's note as its ambient, for a resting aligned crystal.</summary>
    private readonly Dictionary<string, BlockSounds> alignedSounds = new();

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
        var crystals = new List<ItemStack>();
        foreach (var block in api.World.Blocks)
        {
            if (Crystals.Crystal.IsCrystal(block) && block.Variant["position"] == "up")
            {
                crystals.Add(new ItemStack(block));
            }
        }
        foreach (string alignment in Crystals.Crystal.Elementals)
        {
            var ambient = Crystals.Crystal.BlockFor(api.World, alignment, Crystals.Crystal.Glittering)?.Sounds?.Ambient;
            if (Sounds != null && ambient != null)
            {
                var sounds = Sounds.Clone();
                sounds.Ambient = ambient;
                alignedSounds[alignment] = sounds;
            }
        }
        plantHelp = new[]
        {
            new WorldInteraction { ActionLangCode = "lemuria:cradle-plant", MouseButton = EnumMouseButton.Right, Itemstacks = seedlings.ToArray() },
            new WorldInteraction { ActionLangCode = "lemuria:cradle-setcrystal", MouseButton = EnumMouseButton.Right, Itemstacks = crystals.ToArray() },
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
        float strength = (world.BlockAccessor.GetBlockEntity(pos) as BlockEntityCradle)?.HumStrength ?? 0f;
        return world.Side == EnumAppSide.Client ? strength * Crystals.HumBudget.Share : strength;
    }

    /// <summary>
    /// A resting aligned crystal hums its own note through the basin's ambient sound, so it merges with
    /// placed crystals of the same note instead of adding to them.
    /// </summary>
    public override BlockSounds GetSounds(IBlockAccessor blockAccessor, BlockSelection blockSel, ItemStack? stack = null)
    {
        if (blockSel?.Position != null && blockAccessor.GetBlockEntity(blockSel.Position) is BlockEntityCradle cradle
            && cradle.HumAlignment is { } alignment && alignedSounds.TryGetValue(alignment, out var sounds))
        {
            return sounds;
        }
        return base.GetSounds(blockAccessor, blockSel, stack);
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
