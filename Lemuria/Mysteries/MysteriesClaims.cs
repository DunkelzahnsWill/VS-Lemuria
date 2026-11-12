using Lemuria.Core;
using Vintagestory.API.Common;

namespace Lemuria.Mysteries;

/// <summary>
/// What the Mysteries open inside a land claim while the claims switch is off (feature 9's brief, Claims;
/// R9): setting a crystal into a Glint and taking one out. The game tests a claim itself before it calls a
/// block, so the Glint's own check never ran for a stranger; this answers the game's block access event
/// instead, as Storage does, with no Harmony. Only use is opened, never breaking or placing. One instance
/// per side, since the client tests a click before it sends it.
/// </summary>
public class MysteriesClaims
{
    private readonly ICoreAPI api;

    public MysteriesClaims(ICoreAPI api)
    {
        this.api = api;
    }

    /// <summary>The game's <c>OnTestBlockAccess</c>: lifts a claim's lock on a Glint's use, and leaves every other answer as it came.</summary>
    public EnumWorldAccessResponse OnTestBlockAccess(IPlayer player, BlockSelection blockSel, EnumBlockAccessFlags accessType, ref string claimant, EnumWorldAccessResponse response)
    {
        if (!LemuriaClaims.MayLift(accessType, response) || blockSel?.Position == null
            || api.World.BlockAccessor.GetBlock(blockSel.Position) is not BlockGlint)
        {
            return response;
        }
        claimant = null!;
        return LemuriaClaims.Lift(api.World, player, blockSel);
    }
}
