using Lemuria.Core;
using Vintagestory.API.Common;

namespace Lemuria.Cradle;

/// <summary>
/// What Cradle opens inside a land claim while the claims switch is off (feature 2's brief, Claims; R9): planting, setting a crystal in, harvesting and taking one out of a cradle.
/// The game tests a claim itself before it calls a block, so the block's own check never ran for a
/// stranger; this answers the game's block access event instead, as Storage does, with no Harmony. Only
/// use is opened, never breaking or placing. One instance per side, since the client tests a click
/// before it sends it.
/// </summary>
public class CradleClaims
{
    private readonly ICoreAPI api;

    public CradleClaims(ICoreAPI api)
    {
        this.api = api;
    }

    /// <summary>The game's <c>OnTestBlockAccess</c>: lifts a claim's lock on the use of Cradle's blocks, and leaves every other answer as it came.</summary>
    public EnumWorldAccessResponse OnTestBlockAccess(IPlayer player, BlockSelection blockSel, EnumBlockAccessFlags accessType, ref string claimant, EnumWorldAccessResponse response)
    {
        if (!LemuriaClaims.MayLift(accessType, response) || blockSel?.Position == null || !Opens(player, blockSel))
        {
            return response;
        }
        claimant = null!;
        return LemuriaClaims.Lift(api.World, player, blockSel);
    }

    /// <summary>Whether this use of a claimed block is one Cradle opens.</summary>
    public bool Opens(IPlayer? player, BlockSelection blockSel) => api.World.BlockAccessor.GetBlock(blockSel.Position) is BlockCradle;
}
