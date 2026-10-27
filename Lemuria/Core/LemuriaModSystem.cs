using System;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace Lemuria.Core;

/// <summary>
/// Startup and identity of the mod. Feature code does not go here: each feature adds its own
/// ModSystem in its own folder (README, "Working in parallel").
/// </summary>
public class LemuriaModSystem : ModSystem
{
    /// <summary>The asset domain, and the prefix for every code the mod registers (blocks, items, behaviors, commands).</summary>
    public const string ModId = "lemuria";

    /// <summary>
    /// The server's settings (ModConfig/lemuria.json), loaded before any feature starts. On a client
    /// that is not also the server it keeps the defaults and nothing reads it.
    /// </summary>
    public static LemuriaConfig Config { get; private set; } = new();

    /// <summary>Runs before the feature systems, so their start-up code can read <see cref="Config"/>.</summary>
    public override double ExecuteOrder() => 0.01;

    public override void StartPre(ICoreAPI api)
    {
        if (api.Side != EnumAppSide.Server)
        {
            return;
        }
        try
        {
            Config = api.LoadModConfig<LemuriaConfig>(LemuriaConfig.FileName) ?? new LemuriaConfig();
        }
        catch (Exception e)
        {
            Mod.Logger.Error("ModConfig/{0} could not be read, so the defaults apply this session: {1}", LemuriaConfig.FileName, e.Message);
            Config = new LemuriaConfig();
            return;
        }
        // Writes the file back, so settings added by a newer version show up in it with their defaults.
        api.StoreModConfig(Config, LemuriaConfig.FileName);
    }

    /// <summary>Carries the claims switch to each client (<see cref="LemuriaClaims"/>).</summary>
    public const string ChannelName = ModId + "-core";

    public override void Start(ICoreAPI api)
    {
        api.Network.RegisterChannel(ChannelName).RegisterMessageType<ClaimsPacket>();
    }

    public override void StartServerSide(ICoreServerAPI api)
    {
        LemuriaClaims.Respect = Config.RespectClaims;
        var channel = api.Network.GetChannel(ChannelName);
        api.Event.PlayerJoin += player => channel.SendPacket(new ClaimsPacket { Respect = LemuriaClaims.Respect }, player);
        api.Event.SaveGameLoaded += () => Mod.Logger.Notification("Claims: Lemuria {0} land claims", LemuriaClaims.Respect ? "respects" : "ignores");
        api.ChatCommands.GetOrCreate(ModId)
            .WithDescription("Lemuria commands")
            .RequiresPrivilege(Privilege.chat)
            .BeginSubCommand("claims")
                .WithDescription("Whether every Lemuria feature respects land claims (RespectClaims): on or off")
                .RequiresPrivilege(Privilege.controlserver)
                .WithArgs(api.ChatCommands.Parsers.OptionalWord("on|off"))
                .HandleWith(args =>
                {
                    string? word = args[0] as string;
                    if (word != "on" && word != "off")
                    {
                        return TextCommandResult.Success($"Lemuria {(LemuriaClaims.Respect ? "respects" : "ignores")} land claims. Use /{ModId} claims on or off.");
                    }
                    Config.RespectClaims = LemuriaClaims.Respect = word == "on";
                    api.StoreModConfig(Config, LemuriaConfig.FileName);
                    channel.BroadcastPacket(new ClaimsPacket { Respect = LemuriaClaims.Respect });
                    Mod.Logger.Notification("Claims: set {0} by {1}; Lemuria {2} land claims", word, args.Caller.GetName(), LemuriaClaims.Respect ? "respects" : "ignores");
                    return TextCommandResult.Success($"Lemuria now {(LemuriaClaims.Respect ? "respects" : "ignores")} land claims, saved to {LemuriaConfig.FileName}.");
                })
            .EndSubCommand();

        // scripts/smoke-test.sh checks for this line (tests/smoke/core.expect). The check looks for the
        // file rather than a translation, which would be missing whenever the server language is not English.
        bool assetsLoaded = api.Assets.Exists(new AssetLocation(ModId, "lang/en.json"));
        Mod.Logger.Notification("Server side started, version {0}, assets {1}", Mod.Info.Version, assetsLoaded ? "loaded" : "missing");
        if (!assetsLoaded)
        {
            Mod.Logger.Error("The {0} asset domain did not load. Check that assets/{0} is in the mod zip.", ModId);
        }
    }

    public override void StartClientSide(ICoreClientAPI api)
    {
        Mod.Logger.Notification("Client side started, version {0}", Mod.Info.Version);
        api.Network.GetChannel(ChannelName).SetMessageHandler<ClaimsPacket>(packet => LemuriaClaims.Respect = packet.Respect);
    }
}
