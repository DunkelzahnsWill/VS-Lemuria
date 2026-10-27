using System;
using System.Text;
using Lemuria.Core;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.MathTools;

namespace Lemuria.Crystals;

/// <summary>
/// A grown crystal standing on any face, as the game's geode crystals do (the OmniAttachable behavior
/// does the placing). Its block entity holds charge and cracks; this class gives them to the world:
/// drops with charge and cracks, light and hum by charge level, and the watch over containers, which
/// check their crystals for twins on the tick they already run for food.
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

    private bool Aligned => Crystal.AlignmentOf(this) != Crystal.Unaligned;

    // The client plays every crystal of one note within 32 blocks as one sound at one crystal's strength
    // (sounds.ambientBlockCount 1, ambientMaxDistanceMerge 32), shared among the notes nearby (HumBudget).
    public override float GetAmbientSoundStrength(IWorldAccessor world, BlockPos pos)
    {
        float strength = world.BlockAccessor.GetBlockEntity(pos) is BlockEntityCrystal crystal
            ? crystal.HumStrength
            : Aligned ? Crystal.Hum(Crystal.Empty) : humStrength;
        return world.Side == EnumAppSide.Client ? strength * HumBudget.Share : strength;
    }

    public override byte[] GetLightHsv(IBlockAccessor blockAccessor, BlockPos pos, ItemStack? stack = null)
    {
        if (pos != null && blockAccessor.GetBlockEntity(pos) is BlockEntityCrystal crystal)
        {
            return crystal.LightHsv;
        }
        if (stack != null && Aligned)
        {
            string evolution = Crystal.EvolutionOf(this);
            int level = Crystal.Level(Crystal.GetCharge(stack), Crystal.Capacity(evolution));
            return new[] { LightHsv[0], LightHsv[1], Crystal.LightLevel(evolution, level) };
        }
        return base.GetLightHsv(blockAccessor, pos, stack);
    }

    /// <summary>The crystal with its charge and cracks, always standing up as an item.</summary>
    public override ItemStack[] GetDrops(IWorldAccessor world, BlockPos pos, IPlayer byPlayer, float dropQuantityMultiplier = 1f)
    {
        var stack = (world.BlockAccessor.GetBlockEntity(pos) as BlockEntityCrystal)?.AsStack();
        return stack != null ? new[] { stack } : base.GetDrops(world, pos, byPlayer, dropQuantityMultiplier);
    }

    public override ItemStack OnPickBlock(IWorldAccessor world, BlockPos pos)
    {
        var block = Crystal.BlockFor(world, Crystal.AlignmentOf(this), Crystal.EvolutionOf(this));
        return new ItemStack(block ?? this);
    }

    public override string GetPlacedBlockInfo(IWorldAccessor world, BlockPos pos, IPlayer forPlayer)
    {
        var crystal = world.BlockAccessor.GetBlockEntity(pos) as BlockEntityCrystal;
        return crystal == null ? "" : CracksText(crystal.Cracks);
    }

    public override void GetHeldItemInfo(ItemSlot inSlot, StringBuilder dsc, IWorldAccessor world, bool withDebugInfo)
    {
        base.GetHeldItemInfo(inSlot, dsc, world, withDebugInfo);
        var stack = inSlot.Itemstack;
        if (stack == null)
        {
            return;
        }
        if (Aligned)
        {
            int level = Crystal.Level(Crystal.GetCharge(stack), Crystal.Capacity(Crystal.EvolutionOf(this)));
            dsc.AppendLine(Lang.Get($"{LemuriaModSystem.ModId}:crystal-charge-{level}"));
        }
        string cracks = CracksText(Crystal.GetCracks(stack));
        if (cracks.Length > 0)
        {
            dsc.AppendLine(cracks);
        }
    }

    /// <summary>Sneak and left-click a placed Unaligned crystal with an Unaligned crystal in hand: see <see cref="TryStrike"/>.</summary>
    public override void OnHeldAttackStart(ItemSlot slot, EntityAgent byEntity, BlockSelection blockSel, EntitySelection entitySel, ref EnumHandHandling handling)
    {
        if (!Aligned && TryStrike(slot, byEntity, blockSel))
        {
            handling = EnumHandHandling.PreventDefault;
            return;
        }
        base.OnHeldAttackStart(slot, byEntity, blockSel, entitySel, ref handling);
    }

    /// <summary>
    /// Strikes the Unaligned crystal (or strange crystal) in hand on a placed Unaligned crystal, while
    /// sneaking, into a Crystalline Chisel (Sparkstone): one from the hand is used up, the placed one
    /// stays. Returns whether this was a strike, on both sides; the server makes the chisel.
    /// </summary>
    public static bool TryStrike(ItemSlot slot, EntityAgent byEntity, BlockSelection? blockSel)
    {
        var world = byEntity.World;
        bool sneaking = byEntity.Controls.Sneak || byEntity.Controls.ShiftKey;
        if (!sneaking || blockSel == null || world.BlockAccessor.GetBlock(blockSel.Position) is not BlockLemuriaCrystal target
            || Crystal.AlignmentOf(target) != Crystal.Unaligned)
        {
            return false;
        }
        var chisel = world.GetItem(new AssetLocation(LemuriaModSystem.ModId, "crystalchisel"));
        if (chisel == null)
        {
            return false;
        }
        if (world.Side != EnumAppSide.Server || slot.Itemstack == null)
        {
            return true;
        }
        var player = (byEntity as EntityPlayer)?.Player;
        if (player != null && !world.Claims.TryAccess(player, blockSel.Position, EnumBlockAccessFlags.Use))
        {
            return true;
        }
        slot.TakeOut(1);
        slot.MarkDirty();
        var tool = new ItemStack(chisel);
        if (slot.Empty)
        {
            slot.Itemstack = tool;
            slot.MarkDirty();
        }
        else if (!byEntity.TryGiveItemStack(tool))
        {
            world.SpawnItemEntity(tool, byEntity.Pos.XYZ.Add(0, 0.5, 0));
        }
        world.PlaySoundAt(new AssetLocation(LemuriaModSystem.ModId, "sounds/crystals/ring"), blockSel.Position.X + 0.5, blockSel.Position.Y + 0.5, blockSel.Position.Z + 0.5, null, true, 16f, 1f);
        return true;
    }

    private static string CracksText(int cracks) => cracks switch
    {
        <= 0 => "",
        1 => Lang.Get(LemuriaModSystem.ModId + ":crystal-cracks-1"),
        _ => Lang.Get(LemuriaModSystem.ModId + ":crystal-cracks-2"),
    };

    /// <summary>Aligned crystals make the container they lie in tick, so it can look for twins.</summary>
    public override bool RequiresTransitionableTicking(IWorldAccessor world, ItemStack itemstack)
    {
        return (world.Side == EnumAppSide.Server && Aligned) || base.RequiresTransitionableTicking(world, itemstack);
    }

    /// <summary>
    /// Called by a container's 10-second food tick for each stack (and elsewhere). On the server an
    /// aligned crystal hands its container to the dialogue watch, which pairs twins on its own tick.
    /// </summary>
    public override TransitionState[] UpdateAndGetTransitionStates(IWorldAccessor world, ItemSlot inslot)
    {
        if (world.Side == EnumAppSide.Server && Aligned && inslot?.Inventory != null)
        {
            CrystalsModSystem.Server?.Dialogues.Watch(inslot.Inventory);
        }
        return base.UpdateAndGetTransitionStates(world, inslot);
    }
}
