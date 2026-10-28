using Lemuria.Core;
using Lemuria.Crystals;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace Lemuria.Machines;

/// <summary>
/// How the Crystal Rod is made (design note "Lemuria step 4: Machines", The Crystal Rod; the owner's
/// answer F6): hold right-click for a second on a placed Unaligned crystal, of either Evolution, with a
/// block of polished rock in hand. The crystal and one rock are used up and the rod comes to you. A short
/// click does nothing, so building against a crystal never uses it up. Added to every crystal block by
/// patches/machines-crystal.json.
/// </summary>
public class BlockBehaviorRodMaker : BlockBehavior
{
    public const string Name = LemuriaModSystem.ModId + "-rodmaker";

    private const float HoldSeconds = 1f;

    private static readonly AssetLocation MadeSound = new(LemuriaModSystem.ModId, "sounds/crystals/ring");

    public BlockBehaviorRodMaker(Block block) : base(block)
    {
    }

    private bool Applies(IPlayer? player) =>
        Crystal.AlignmentOf(block) == Crystal.Unaligned
        && player?.InventoryManager?.ActiveHotbarSlot?.Itemstack?.Block is { } held
        && Sparkstone.ItemCrystalChisel.PolishedRock(held) != null;

    public override bool OnBlockInteractStart(IWorldAccessor world, IPlayer byPlayer, BlockSelection blockSel, ref EnumHandling handling)
    {
        if (!Applies(byPlayer))
        {
            return base.OnBlockInteractStart(world, byPlayer, blockSel, ref handling);
        }
        handling = EnumHandling.PreventDefault;
        return true;
    }

    public override bool OnBlockInteractStep(float secondsUsed, IWorldAccessor world, IPlayer byPlayer, BlockSelection blockSel, ref EnumHandling handling)
    {
        if (!Applies(byPlayer))
        {
            return base.OnBlockInteractStep(secondsUsed, world, byPlayer, blockSel, ref handling);
        }
        handling = EnumHandling.PreventDefault;
        if (world.Side == EnumAppSide.Client && secondsUsed > 0.1f && (int)(secondsUsed * 10) % 3 == 0)
        {
            CrystalCharging.Sparks(world, blockSel.Position.ToVec3d().Add(0.5, 0.4, 0.5), Crystal.Rgb(Crystal.Unaligned), 1, 0.1f);
        }
        // As the game's harvestable bushes do: the client holds on, and the server ends the hold, so a
        // client a little ahead of the server never stops short of the second.
        return world.Side == EnumAppSide.Client || secondsUsed < HoldSeconds;
    }

    public override void OnBlockInteractStop(float secondsUsed, IWorldAccessor world, IPlayer byPlayer, BlockSelection blockSel, ref EnumHandling handling)
    {
        if (!Applies(byPlayer))
        {
            base.OnBlockInteractStop(secondsUsed, world, byPlayer, blockSel, ref handling);
            return;
        }
        handling = EnumHandling.PreventDefault;
        if (secondsUsed < HoldSeconds - 0.05f || world.Side != EnumAppSide.Server)
        {
            return;
        }
        Make(world, byPlayer, blockSel.Position);
    }

    /// <summary>Uses up the Unaligned crystal at pos and one polished rock from the player's hand, and gives the rod. Server only.</summary>
    public static bool Make(IWorldAccessor world, IPlayer? player, BlockPos pos, ItemSlot? rock = null)
    {
        var crystal = world.BlockAccessor.GetBlock(pos);
        rock ??= player?.InventoryManager?.ActiveHotbarSlot;
        var rodItem = world.GetItem(new AssetLocation(LemuriaModSystem.ModId, "crystalrod"));
        if (!Crystal.IsCrystal(crystal) || Crystal.AlignmentOf(crystal) != Crystal.Unaligned || rodItem == null
            || rock?.Itemstack?.Block is not { } held || Sparkstone.ItemCrystalChisel.PolishedRock(held) == null)
        {
            return false;
        }
        if (player != null && !LemuriaClaims.TryAccess(world, player, pos, EnumBlockAccessFlags.BuildOrBreak))
        {
            return false;
        }
        world.BlockAccessor.SetBlock(0, pos);
        world.BlockAccessor.TriggerNeighbourBlockUpdate(pos);
        rock.TakeOut(1);
        rock.MarkDirty();
        var rod = new ItemStack(rodItem);
        if (player?.InventoryManager?.TryGiveItemstack(rod, true) != true)
        {
            world.SpawnItemEntity(rod, pos.ToVec3d().Add(0.5, 0.5, 0.5));
        }
        world.PlaySoundAt(MadeSound, pos.X + 0.5, pos.Y + 0.5, pos.Z + 0.5, null, true, 16f, LemuriaSound.Volume(0.8f));
        return true;
    }
}
