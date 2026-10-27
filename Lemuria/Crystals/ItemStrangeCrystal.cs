using Lemuria.Core;
using Vintagestory.API.Client;
using Vintagestory.API.Common;

namespace Lemuria.Crystals;

/// <summary>
/// The strange crystal found in vugs (step 1). Since the step 2 play-test it is a grown Unaligned
/// Glittering crystal by another name: a right-click on a block sets it down as one, and in hand it
/// strikes a placed Unaligned crystal into a Crystalline Chisel, so two finds make the first chisel.
/// </summary>
public class ItemStrangeCrystal : Item
{
    public const string ClassCode = "lemuria-strangecrystal";

    public override void OnHeldAttackStart(ItemSlot slot, EntityAgent byEntity, BlockSelection blockSel, EntitySelection entitySel, ref EnumHandHandling handling)
    {
        if (BlockLemuriaCrystal.TryStrike(slot, byEntity, blockSel))
        {
            handling = EnumHandHandling.PreventDefault;
            return;
        }
        base.OnHeldAttackStart(slot, byEntity, blockSel, entitySel, ref handling);
    }

    public override void OnHeldInteractStart(ItemSlot slot, EntityAgent byEntity, BlockSelection blockSel, EntitySelection entitySel, bool firstEvent, ref EnumHandHandling handling)
    {
        var block = Crystal.BlockFor(byEntity.World, Crystal.Unaligned, Crystal.Glittering);
        if (!firstEvent || blockSel == null || block == null || byEntity is not EntityPlayer { Player: { } player })
        {
            base.OnHeldInteractStart(slot, byEntity, blockSel, entitySel, firstEvent, ref handling);
            return;
        }
        // Placed like any block: against the face clicked, unless the clicked block itself can be replaced.
        var world = byEntity.World;
        var place = blockSel.Clone();
        if (world.BlockAccessor.GetBlock(blockSel.Position).Replaceable < 6000)
        {
            place.Position = blockSel.Position.AddCopy(blockSel.Face);
        }
        handling = EnumHandHandling.PreventDefault;
        string failure = "";
        if (world.Side == EnumAppSide.Server && slot.Itemstack != null && block.TryPlaceBlock(world, player, new ItemStack(block), place, ref failure))
        {
            slot.TakeOut(1);
            slot.MarkDirty();
            world.PlaySoundAt(new AssetLocation("game:sounds/block/ceramicplace"), place.Position.X + 0.5, place.Position.Y + 0.5, place.Position.Z + 0.5, null, true, 12f, 1f);
        }
    }

    public override WorldInteraction[] GetHeldInteractionHelp(ItemSlot inSlot)
    {
        return new[]
        {
            new WorldInteraction { ActionLangCode = LemuriaModSystem.ModId + ":strangecrystal-place", MouseButton = EnumMouseButton.Right },
            new WorldInteraction { ActionLangCode = LemuriaModSystem.ModId + ":crystal-strike", MouseButton = EnumMouseButton.Left, HotKeyCode = "sneak" },
        };
    }
}
