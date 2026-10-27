using Lemuria.Core;
using Lemuria.Crystals;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace Lemuria.Evocations;

/// <summary>
/// Casting from the hand, on every crystal (patches/evocations-crystal.json); it acts only for the
/// Elementals (design note "Lemuria step 3: Naked evocations", Casting). Hold right-click: the
/// crystal winds up for half a second and over-drives past two; let go and the server casts. Like a
/// bow's draw, the wind-up's note, whine and flare play on the caster's own client. Sneaking passes
/// the click on, so the game sets the crystal down.
/// </summary>
public class CollectibleBehaviorEvocation : CollectibleBehavior
{
    public const string Name = LemuriaModSystem.ModId + "-evocation";

    /// <summary>Set on the caster while this behavior holds their right-click, so a stray step or stop never casts.</summary>
    private const string CastingKey = LemuriaModSystem.ModId + "-casting";

    public CollectibleBehaviorEvocation(CollectibleObject collObj) : base(collObj)
    {
    }

    public override void OnHeldInteractStart(ItemSlot slot, EntityAgent byEntity, BlockSelection blockSel, EntitySelection entitySel, bool firstEvent, ref EnumHandHandling handHandling, ref EnumHandling handling)
    {
        var stack = slot.Itemstack;
        if (!firstEvent || stack == null || !Evocation.Casts(stack.Collectible) || Crystal.Sneaking(byEntity) || byEntity is not EntityPlayer)
        {
            return;
        }
        handHandling = EnumHandHandling.PreventDefault;
        handling = EnumHandling.PreventSubsequent;
        bool charged = Caster.HasCharge(stack, false);
        byEntity.Attributes.SetBool(CastingKey, charged);
        if (!charged)
        {
            if (byEntity.World.Side == EnumAppSide.Server)
            {
                Caster.Dull(byEntity.World, byEntity);
            }
            return;
        }
        if (byEntity.World.Side == EnumAppSide.Client)
        {
            EvocationsModSystem.ClientSide?.WindUp.Begin(byEntity, Crystal.AlignmentOf(stack.Collectible));
        }
    }

    public override bool OnHeldInteractStep(float secondsUsed, ItemSlot slot, EntityAgent byEntity, BlockSelection blockSel, EntitySelection entitySel, ref EnumHandling handling)
    {
        if (!byEntity.Attributes.GetBool(CastingKey))
        {
            return true;
        }
        handling = EnumHandling.PreventSubsequent;
        var stack = slot.Itemstack;
        if (stack == null || !Evocation.Casts(stack.Collectible))
        {
            return false;
        }
        if (byEntity.World.Side == EnumAppSide.Client)
        {
            EvocationsModSystem.ClientSide?.WindUp.Step(byEntity, Crystal.AlignmentOf(stack.Collectible), secondsUsed, Caster.HasCharge(stack, true));
        }
        return true;
    }

    public override void OnHeldInteractStop(float secondsUsed, ItemSlot slot, EntityAgent byEntity, BlockSelection blockSel, EntitySelection entitySel, ref EnumHandling handling)
    {
        if (!byEntity.Attributes.GetBool(CastingKey))
        {
            return;
        }
        handling = EnumHandling.PreventSubsequent;
        byEntity.Attributes.SetBool(CastingKey, false);
        if (byEntity.World.Side == EnumAppSide.Client)
        {
            EvocationsModSystem.ClientSide?.WindUp.End(byEntity);
            return;
        }
        if (byEntity is EntityPlayer { Player: IServerPlayer player } && EvocationsModSystem.ServerSide is { } system)
        {
            Caster.TryCast(system, player, slot, secondsUsed);
        }
    }

    public override bool OnHeldInteractCancel(float secondsUsed, ItemSlot slot, EntityAgent byEntity, BlockSelection blockSel, EntitySelection entitySel, EnumItemUseCancelReason cancelReason, ref EnumHandling handling)
    {
        if (!byEntity.Attributes.GetBool(CastingKey))
        {
            return true;
        }
        handling = EnumHandling.PreventSubsequent;
        // Letting go is the cast, which the stop that follows does; anything else ends it unsent.
        if (cancelReason != EnumItemUseCancelReason.ReleasedMouse)
        {
            byEntity.Attributes.SetBool(CastingKey, false);
            if (byEntity.World.Side == EnumAppSide.Client)
            {
                EvocationsModSystem.ClientSide?.WindUp.End(byEntity);
            }
        }
        return true;
    }

    public override WorldInteraction[] GetHeldInteractionHelp(ItemSlot inSlot, ref EnumHandling handling)
    {
        if (!Evocation.Casts(inSlot.Itemstack?.Collectible))
        {
            return base.GetHeldInteractionHelp(inSlot, ref handling);
        }
        return new[]
        {
            new WorldInteraction
            {
                ActionLangCode = $"{LemuriaModSystem.ModId}:evocation-cast-{Evocation.Of(Crystal.AlignmentOf(inSlot.Itemstack!.Collectible))}",
                MouseButton = EnumMouseButton.Right,
            },
        };
    }
}
