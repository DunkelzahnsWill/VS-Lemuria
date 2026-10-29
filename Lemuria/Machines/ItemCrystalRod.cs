using Lemuria.Core;
using Lemuria.Sparkstone;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace Lemuria.Machines;

/// <summary>
/// The Crystal Rod (design note "Lemuria step 4: Machines", The Crystal Rod; F6): the maintenance tool.
/// Sneak and right-click any Awakened block to switch it Active or Dormant. Right-click another face of
/// a machine to make it the front, crystals and all; right-click a crystal on the front to switch it Dormant or
/// wake it. Every tap makes a machine read what touches it again. It holds no charge and never wears out.
/// </summary>
public class ItemCrystalRod : Item
{
    public const string ClassCode = "lemuria-crystalrod";

    private static readonly AssetLocation OnSound = new(LemuriaModSystem.ModId, "sounds/machines/switch-on");
    private static readonly AssetLocation OffSound = new(LemuriaModSystem.ModId, "sounds/machines/switch-off");
    private static readonly AssetLocation TurnSound = new("game", "sounds/block/rock-hit-pickaxe");

    public override void OnHeldInteractStart(ItemSlot slot, EntityAgent byEntity, BlockSelection blockSel, EntitySelection entitySel, bool firstEvent, ref EnumHandHandling handling)
    {
        var world = byEntity.World;
        if (firstEvent && blockSel != null && world.BlockAccessor.GetBlockEntity(blockSel.Position) is BlockEntityControlShelf shelf)
        {
            // On a Control Shelf the rod switches an item, or sneaking wakes the shelf (H4).
            handling = EnumHandHandling.PreventDefault;
            var by = (byEntity as EntityPlayer)?.Player;
            if (world.Side == EnumAppSide.Server && (by == null || LemuriaClaims.TryAccess(world, by, blockSel.Position, EnumBlockAccessFlags.Use)))
            {
                shelf.RodTap(blockSel, byEntity.Controls.Sneak || byEntity.Controls.ShiftKey);
            }
            return;
        }
        if (!firstEvent || blockSel == null || world.BlockAccessor.GetBlockEntity(blockSel.Position) is not BlockEntitySparkstone stone)
        {
            base.OnHeldInteractStart(slot, byEntity, blockSel, entitySel, firstEvent, ref handling);
            return;
        }
        handling = EnumHandHandling.PreventDefault;
        if (world.Side != EnumAppSide.Server)
        {
            return;
        }
        var player = (byEntity as EntityPlayer)?.Player;
        if (player != null && !LemuriaClaims.TryAccess(world, player, blockSel.Position, EnumBlockAccessFlags.Use))
        {
            return;
        }
        Tap(world, stone, blockSel, byEntity.Controls.Sneak || byEntity.Controls.ShiftKey);
    }

    /// <summary>One tap of the rod on an Awakened block. Server only; public for the test command.</summary>
    public static void Tap(IWorldAccessor world, BlockEntitySparkstone stone, BlockSelection blockSel, bool sneaking)
    {
        var pos = stone.Pos.Copy();
        if (sneaking)
        {
            stone.SetActive(!stone.Active);
            world.PlaySoundAt(stone.Active ? OnSound : OffSound, pos.X + 0.5, pos.Y + 0.5, pos.Z + 0.5, null, true, 16f, LemuriaSound.Volume(0.7f));
        }
        else if (stone is BlockEntityMachine machine && blockSel.Face != machine.GlyphFace)
        {
            var into = world.GetBlock(Machine.Code(machine.Form, machine.Block.Variant["rock"] ?? "granite", Machine.SideFor(machine.Form, blockSel.Face).Code));
            if (into != null && MachineBlocks.Swap(world, pos, into) is BlockEntitySparkstone turned)
            {
                stone = turned;
                world.PlaySoundAt(TurnSound, pos.X + 0.5, pos.Y + 0.5, pos.Z + 0.5, null, true, 12f, LemuriaSound.Volume(0.5f));
            }
        }
        else if (stone is BlockEntityMachine or BlockEntityMemory && stone.CrystalAt(blockSel.Face, blockSel.HitPosition) is { } crystal)
        {
            crystal.Dormant = !crystal.Dormant;
            stone.OnSocketsChanged();
            world.PlaySoundAt(crystal.Dormant ? OffSound : OnSound, pos.X + 0.5, pos.Y + 0.5, pos.Z + 0.5, null, true, 12f, LemuriaSound.Volume(0.4f));
        }
        (stone as BlockEntityMachine)?.ReadContext();
    }

    public override WorldInteraction[] GetHeldInteractionHelp(ItemSlot inSlot) => new[]
    {
        new WorldInteraction { ActionLangCode = LemuriaModSystem.ModId + ":crystalrod-switch", MouseButton = EnumMouseButton.Right, HotKeyCode = "sneak" },
        new WorldInteraction { ActionLangCode = LemuriaModSystem.ModId + ":crystalrod-turn", MouseButton = EnumMouseButton.Right },
        new WorldInteraction { ActionLangCode = LemuriaModSystem.ModId + ":crystalrod-rest", MouseButton = EnumMouseButton.Right },
    };
}
