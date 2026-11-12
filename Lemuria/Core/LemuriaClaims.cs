using ProtoBuf;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;
using Vintagestory.Server;

namespace Lemuria.Core;

/// <summary>
/// The one switch for every land claim rule in Lemuria (step 3 note, Claims and players; the owner,
/// 2026-10-27): the RespectClaims setting, which admins can flip in game with <c>/lemuria claims on|off</c>.
/// Every feature asks here instead of the game's claims, so none can drift. The server holds the setting;
/// each client is sent it as the player joins and whenever an admin flips it, so a click the server
/// would allow is never stopped on the client.
/// </summary>
public static class LemuriaClaims
{
    private static volatile bool respect = true;

    public static bool Respect
    {
        get => respect;
        set => respect = value;
    }

    /// <summary>The game's TryAccess, which also tells the player why they were refused; always true while claims are off.</summary>
    public static bool TryAccess(IWorldAccessor world, IPlayer player, BlockPos pos, EnumBlockAccessFlags flags) =>
        !respect || world.Claims.TryAccess(player, pos, flags);

    /// <summary>The game's TestAccess, silent; always granted while claims are off.</summary>
    public static bool TestAccess(IWorldAccessor world, IPlayer player, BlockPos pos, EnumBlockAccessFlags flags) =>
        !respect || world.Claims.TestAccess(player, pos, flags) == EnumWorldAccessResponse.Granted;

    /// <summary>
    /// Whether one of the game's own block access tests is one the switch may lift (the owner, 2026-11-10):
    /// the game refused the use of a block because of a land claim, and Lemuria ignores claims. The game
    /// tests a claim before it calls the block, so the checks above never get a say on a right-click; a
    /// feature answers the game's <c>OnTestBlockAccess</c> event for its own blocks and asks here first.
    /// Breaking and building are never lifted: they stay as the game rules them.
    /// </summary>
    public static bool MayLift(EnumBlockAccessFlags accessType, EnumWorldAccessResponse response) =>
        !respect && accessType == EnumBlockAccessFlags.Use && response == EnumWorldAccessResponse.LandClaimed;

    /// <summary>
    /// The answer that lifts the claim's lock on one use. The game asks other mods' <c>CanUseBlock</c> only
    /// once its claims allow a use, so the server asks them here, as the game would have next; they can
    /// still refuse.
    /// </summary>
    public static EnumWorldAccessResponse Lift(IWorldAccessor world, IPlayer? player, BlockSelection blockSel) =>
        world is ServerMain server && player is IServerPlayer serverPlayer && !server.EventManager.TriggerCanUse(serverPlayer, blockSel)
            ? EnumWorldAccessResponse.DeniedByMod
            : EnumWorldAccessResponse.Granted;
}

/// <summary>The claims switch, sent from the server to each client.</summary>
[ProtoContract]
public class ClaimsPacket
{
    [ProtoMember(1)]
    public bool Respect = true;
}
