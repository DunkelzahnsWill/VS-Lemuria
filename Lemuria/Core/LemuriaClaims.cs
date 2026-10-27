using ProtoBuf;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

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
}

/// <summary>The claims switch, sent from the server to each client.</summary>
[ProtoContract]
public class ClaimsPacket
{
    [ProtoMember(1)]
    public bool Respect = true;
}
