using Lemuria.Core;
using Lemuria.Crystals;
using Vintagestory.API.Common;

namespace Lemuria.Machines;

/// <summary>
/// What Machines open inside a land claim while the claims switch is off (feature 8's brief, Claims; R9):
/// setting crystals into a machine's sockets or its Memory and taking them out, putting items on a Control
/// Shelf and taking them off, and making a Crystal Rod from a placed Unaligned crystal with a polished rock
/// in hand. A Free Awakened block is feature 4's, whose own patch opens it. The game tests a claim itself before it calls a block, so the blocks' own checks never
/// ran for a stranger; this answers the game's block access event instead, as Storage does, with no
/// Harmony. Only use is opened, never breaking or placing; the rod's making still asks the switch for
/// building itself. One instance per side, since the client tests a click before it sends it.
/// </summary>
public class MachinesClaims
{
    private readonly ICoreAPI api;

    public MachinesClaims(ICoreAPI api)
    {
        this.api = api;
    }

    /// <summary>The game's <c>OnTestBlockAccess</c>: lifts a claim's lock on what Machines open, and leaves every other answer as it came.</summary>
    public EnumWorldAccessResponse OnTestBlockAccess(IPlayer player, BlockSelection blockSel, EnumBlockAccessFlags accessType, ref string claimant, EnumWorldAccessResponse response)
    {
        if (!LemuriaClaims.MayLift(accessType, response) || blockSel?.Position == null
            || !Opens(api.World.BlockAccessor.GetBlock(blockSel.Position), player?.InventoryManager?.ActiveHotbarSlot?.Itemstack))
        {
            return response;
        }
        claimant = null!;
        return LemuriaClaims.Lift(api.World, player, blockSel);
    }

    /// <summary>
    /// Whether using this block is one Machines open: any machine, the Memory and the Crystal Motor among them, any Control Shelf, and a placed Unaligned crystal
    /// while the hand holds the polished rock that makes it a rod. Any other use of a crystal is left as
    /// it came.
    /// </summary>
    public static bool Opens(Block block, ItemStack? held) =>
        block is BlockMachine or BlockCrystalMotor or BlockControlShelf
        || (block.HasBehavior<BlockBehaviorRodMaker>() && Crystal.AlignmentOf(block) == Crystal.Unaligned
            && held?.Block is { } rock && Sparkstone.ItemCrystalChisel.PolishedRock(rock) != null);
}
