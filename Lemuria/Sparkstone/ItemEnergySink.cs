using Vintagestory.API.Client;
using Lemuria.Core;
using Lemuria.Crystals;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Server;

namespace Lemuria.Sparkstone;

/// <summary>
/// The Energy Sink (the owner's testing tool, Nov 8 2026, step 4 part 3's play-test): the admin charger's
/// twin, creative inventory only, no recipe. For a player in creative mode or with controlserver, a
/// right-click empties a placed aligned crystal or a whole sparkstone network at once, so a battery's
/// drain stone can be watched paying back. What a battery has stored stays; for anyone else it does nothing.
/// </summary>
public class ItemEnergySink : Item
{
    public const string ClassCode = "lemuria-adminsink";

    public override void OnHeldInteractStart(ItemSlot slot, EntityAgent byEntity, BlockSelection blockSel, EntitySelection entitySel, bool firstEvent, ref EnumHandHandling handling)
    {
        var player = (byEntity as EntityPlayer)?.Player;
        if (!firstEvent || !ItemAdminCharger.Allowed(player))
        {
            base.OnHeldInteractStart(slot, byEntity, blockSel, entitySel, firstEvent, ref handling);
            return;
        }
        handling = EnumHandHandling.PreventDefault;
        if (byEntity.World.Side != EnumAppSide.Server)
        {
            return;
        }
        string done = blockSel != null && Empty(byEntity.World, blockSel.Position) is { } what
            ? Lang.Get(LemuriaModSystem.ModId + ":adminsink-emptied", what)
            : Lang.Get(LemuriaModSystem.ModId + ":admincharger-nothing");
        (player as IServerPlayer)?.SendMessage(GlobalConstants.GeneralChatGroup, done, EnumChatType.Notification);
    }

    /// <summary>Empties what stands at a position; returns what it emptied, or null. Server only.</summary>
    public static string? Empty(IWorldAccessor world, Vintagestory.API.MathTools.BlockPos pos)
    {
        switch (world.BlockAccessor.GetBlockEntity(pos))
        {
            case BlockEntityCrystal crystal when crystal.IsAligned:
                crystal.SetCharge(0);
                return $"{crystal.Alignment} {crystal.Evolution} crystal";
            case BlockEntitySparkstone stone when SparkstoneModSystem.Networks != null:
                return $"sparkstone network of {SparkstoneModSystem.Networks.Empty(stone)} block(s)";
            default:
                return null;
        }
    }

    public override WorldInteraction[] GetHeldInteractionHelp(ItemSlot inSlot)
    {
        return new[]
        {
            new WorldInteraction { ActionLangCode = LemuriaModSystem.ModId + ":adminsink-empty", MouseButton = EnumMouseButton.Right },
        };
    }
}
