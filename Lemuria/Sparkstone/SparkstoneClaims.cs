using Lemuria.Core;
using Vintagestory.API.Common;

namespace Lemuria.Sparkstone;

/// <summary>
/// What Sparkstone opens inside a land claim while the claims switch is off (feature 4's brief, Claims; R9):
/// the use by hand of a Free Awakened block, which is setting a crystal into its sockets and taking one out.
/// A block carved into a machine is feature 8's, whose own patch opens it. The game tests a claim itself before it calls a block, so the socket's own
/// check never ran for a stranger; this answers the game's block access event instead, as Storage does,
/// with no Harmony. Only use is opened, never breaking or placing. One instance per side, since the client
/// tests a click before it sends it.
/// </summary>
public class SparkstoneClaims
{
    private readonly ICoreAPI api;

    public SparkstoneClaims(ICoreAPI api)
    {
        this.api = api;
    }

    /// <summary>The game's <c>OnTestBlockAccess</c>: lifts a claim's lock on a Free Awakened block's use, and leaves every other answer as it came.</summary>
    public EnumWorldAccessResponse OnTestBlockAccess(IPlayer player, BlockSelection blockSel, EnumBlockAccessFlags accessType, ref string claimant, EnumWorldAccessResponse response)
    {
        if (!LemuriaClaims.MayLift(accessType, response) || blockSel?.Position == null
            || api.World.BlockAccessor.GetBlock(blockSel.Position)?.GetType() != typeof(BlockSparkstone))
        {
            return response;
        }
        claimant = null!;
        return LemuriaClaims.Lift(api.World, player, blockSel);
    }
}
