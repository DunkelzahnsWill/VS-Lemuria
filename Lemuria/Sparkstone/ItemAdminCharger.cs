using Lemuria.Core;
using Lemuria.Cradle;
using Lemuria.Crystals;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Server;

namespace Lemuria.Sparkstone;

/// <summary>
/// The admin charger (design note, After the play-test; the owner's testing tool): creative inventory
/// only, no recipe. For a player in creative mode or with controlserver, a right-click fills a placed
/// crystal, a crystal resting in a cradle or a whole sparkstone network to full at once; sneak and
/// right-click fills every crystal and Crystalline Chisel the player carries, jars included. Cracks and
/// alignment stay as they are; for anyone else it does nothing. Later steps extend it to what they add.
/// </summary>
public class ItemAdminCharger : Item
{
    public const string ClassCode = "lemuria-admincharger";

    public static bool Allowed(IPlayer? player) =>
        player != null && (player.WorldData?.CurrentGameMode == EnumGameMode.Creative || player.HasPrivilege(Privilege.controlserver));

    public override void OnHeldInteractStart(ItemSlot slot, EntityAgent byEntity, BlockSelection blockSel, EntitySelection entitySel, bool firstEvent, ref EnumHandHandling handling)
    {
        var player = (byEntity as EntityPlayer)?.Player;
        if (!firstEvent || !Allowed(player))
        {
            base.OnHeldInteractStart(slot, byEntity, blockSel, entitySel, firstEvent, ref handling);
            return;
        }
        handling = EnumHandHandling.PreventDefault;
        if (byEntity.World.Side != EnumAppSide.Server)
        {
            return;
        }
        string done = byEntity.Controls.Sneak || byEntity.Controls.ShiftKey
            ? Lang.Get(LemuriaModSystem.ModId + ":admincharger-carried", FillCarried(player!))
            : blockSel != null && Fill(byEntity.World, blockSel.Position) is { } what
                ? Lang.Get(LemuriaModSystem.ModId + ":admincharger-filled", what)
                : Lang.Get(LemuriaModSystem.ModId + ":admincharger-nothing");
        (player as IServerPlayer)?.SendMessage(GlobalConstants.GeneralChatGroup, done, EnumChatType.Notification);
    }

    /// <summary>Fills what stands at a position to full; returns what it filled, or null. Server only.</summary>
    public static string? Fill(IWorldAccessor world, Vintagestory.API.MathTools.BlockPos pos)
    {
        switch (world.BlockAccessor.GetBlockEntity(pos))
        {
            case BlockEntityCrystal crystal when crystal.IsAligned:
                crystal.SetCharge(crystal.Capacity);
                return $"{crystal.Alignment} {crystal.Evolution} crystal";
            case BlockEntityCradle cradle when cradle.FillCharge():
                return $"{cradle.Alignment} {cradle.Evolution} crystal in the cradle";
            case BlockEntitySparkstone stone when SparkstoneModSystem.Networks != null:
                return $"sparkstone network of {SparkstoneModSystem.Networks.Fill(stone)} block(s)";
            default:
                return null;
        }
    }

    /// <summary>Fills every aligned crystal and Crystalline Chisel the player carries, jar slots included; returns how many stacks.</summary>
    public static int FillCarried(IPlayer player)
    {
        int filled = 0;
        var manager = player.InventoryManager;
        foreach (string name in new[] { GlobalConstants.hotBarInvClassName, GlobalConstants.backpackInvClassName, GlobalConstants.mousecursorInvClassName, GlobalConstants.craftingInvClassName })
        {
            var inventory = manager.GetOwnInventory(name);
            if (inventory == null)
            {
                continue;
            }
            foreach (var slot in inventory)
            {
                var stack = slot?.Itemstack;
                if (stack == null)
                {
                    continue;
                }
                if (Crystal.IsAligned(stack.Collectible))
                {
                    Crystal.SetState(stack, Crystal.Capacity(Crystal.EvolutionOf(stack.Collectible)), Crystal.GetCracks(stack));
                }
                else if (stack.Collectible is ItemCrystalChisel)
                {
                    ItemCrystalChisel.SetCharge(stack, ItemCrystalChisel.Capacity);
                }
                else
                {
                    continue;
                }
                slot!.MarkDirty();
                filled++;
            }
        }
        return filled;
    }

    public override WorldInteraction[] GetHeldInteractionHelp(ItemSlot inSlot)
    {
        return new[]
        {
            new WorldInteraction { ActionLangCode = LemuriaModSystem.ModId + ":admincharger-fill", MouseButton = EnumMouseButton.Right },
            new WorldInteraction { ActionLangCode = LemuriaModSystem.ModId + ":admincharger-fillcarried", MouseButton = EnumMouseButton.Right, HotKeyCode = "sneak" },
        };
    }
}
