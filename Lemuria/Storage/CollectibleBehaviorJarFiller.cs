using System;
using Lemuria.Core;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.GameContent;

namespace Lemuria.Storage;

/// <summary>
/// Makes an unfired crystal jar (design note "Lemuria step 2: The Four Elementals", Crystal jars and
/// racks): right-click an unfired crock set down on the ground (vanilla ground storage) with what
/// this behavior sits on, a seedling for a personal jar or a block of Awakened sparkstone for a
/// shielded one, and the crock becomes the unfired jar, to be fired where it stands. No vanilla file
/// is touched: ground storage passes the click on to the held item when it holds no ground-storable item.
/// </summary>
public class CollectibleBehaviorJarFiller : CollectibleBehavior
{
    public const string Name = LemuriaModSystem.ModId + "-jarfiller";

    private string kind = "personal";

    public CollectibleBehaviorJarFiller(CollectibleObject collObj) : base(collObj)
    {
    }

    public override void Initialize(JsonObject properties)
    {
        base.Initialize(properties);
        kind = properties["kind"].AsString("personal");
    }

    /// <summary>An unfired crock of the game's own, in any clay colour.</summary>
    public static bool IsRawCrock(ItemStack? stack)
    {
        var code = stack?.Collectible?.Code;
        return code != null && code.Domain == "game" && code.Path.StartsWith("crock-", StringComparison.Ordinal) && code.Path.EndsWith("-raw", StringComparison.Ordinal);
    }

    public override void OnHeldInteractStart(ItemSlot slot, EntityAgent byEntity, BlockSelection blockSel, EntitySelection entitySel, bool firstEvent, ref EnumHandHandling handHandling, ref EnumHandling handling)
    {
        if (!firstEvent || blockSel == null || slot.Empty)
        {
            return;
        }
        var world = byEntity.World;
        if (world.BlockAccessor.GetBlockEntity(blockSel.Position) is not BlockEntityGroundStorage storage)
        {
            return;
        }
        var target = storage.GetSlotAt(blockSel);
        if (target == null || !IsRawCrock(target.Itemstack))
        {
            return;
        }
        var player = (byEntity as EntityPlayer)?.Player;
        if (player == null || !LemuriaClaims.TryAccess(world, player, blockSel.Position, EnumBlockAccessFlags.Use))
        {
            return;
        }
        handHandling = EnumHandHandling.PreventDefault;
        handling = EnumHandling.PreventSubsequent;
        if (world.Side != EnumAppSide.Server)
        {
            return;
        }
        var jar = world.GetItem(new AssetLocation(LemuriaModSystem.ModId, $"crystaljar-{kind}-raw"));
        if (jar == null)
        {
            return;
        }
        target.Itemstack = new ItemStack(jar);
        target.MarkDirty();
        storage.MarkDirty(true);
        if (player.WorldData.CurrentGameMode != EnumGameMode.Creative)
        {
            slot.TakeOut(1);
            slot.MarkDirty();
        }
        world.PlaySoundAt(new AssetLocation("game:sounds/player/build"), blockSel.Position, -0.25, null, true, 32f, LemuriaSound.Volume(1f));
    }
}
