using Vintagestory.API.Common;

namespace Lemuria.Journal;

/// <summary>The Pilgrim's Journal: right-click opens its own book screen on the client.</summary>
public class ItemJournal : Item
{
    public const string ClassCode = "lemuria-journal";

    public override void OnHeldInteractStart(ItemSlot slot, EntityAgent byEntity, BlockSelection blockSel, EntitySelection entitySel, bool firstEvent, ref EnumHandHandling handling)
    {
        if (!firstEvent)
        {
            return;
        }
        if (byEntity.World.Side == EnumAppSide.Client)
        {
            byEntity.World.Api.ModLoader.GetModSystem<JournalModSystem>().OpenJournal();
        }
        handling = EnumHandHandling.PreventDefault;
    }
}
