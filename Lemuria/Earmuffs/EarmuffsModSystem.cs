using System;
using System.Linq;
using Lemuria.Core;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace Lemuria.Earmuffs;

/// <summary>
/// Ear muffs (step 3 note, After the play-test, Ear muffs): worn in the face slot, they quiet every
/// sound the wearer hears by the server's EarMuffsVolume, on the wearer's own client only. Also carries
/// SoundVolume, which scales every sound Lemuria plays (<see cref="LemuriaSound"/>). The server sends
/// both settings to each player as they join. Adds <c>/lemuria earmuffs test</c> for the smoke test.
/// </summary>
public class EarmuffsModSystem : ModSystem
{
    public const string ItemCode = "earmuffs";
    public const string ChannelName = LemuriaModSystem.ModId + "-earmuffs";

    private Muffler? muffler;

    public override void Start(ICoreAPI api)
    {
        api.Network.RegisterChannel(ChannelName).RegisterMessageType<SoundSettingsPacket>();
    }

    public override void StartServerSide(ICoreServerAPI api)
    {
        var config = LemuriaModSystem.Config;
        var settings = new SoundSettingsPacket
        {
            EarMuffsVolume = (float)Math.Clamp(config.EarMuffsVolume, 0, 1),
            SoundVolume = (float)Math.Clamp(config.SoundVolume, 0, 1),
        };
        LemuriaSound.Scale = settings.SoundVolume;
        var channel = api.Network.GetChannel(ChannelName);
        api.Event.PlayerJoin += player => channel.SendPacket(settings, player);
        api.Event.SaveGameLoaded += () => Mod.Logger.Notification("Earmuffs: ear muffs let {0:0.##} of every sound through; Lemuria's sounds play at {1:0.##}",
            settings.EarMuffsVolume, settings.SoundVolume);
        RegisterTestCommand(api, settings);
    }

    public override void StartClientSide(ICoreClientAPI api)
    {
        muffler = new Muffler(api);
        api.Network.GetChannel(ChannelName).SetMessageHandler<SoundSettingsPacket>(packet =>
        {
            muffler.Volume = packet.EarMuffsVolume;
            LemuriaSound.Scale = packet.SoundVolume;
        });
    }

    private void RegisterTestCommand(ICoreServerAPI api, SoundSettingsPacket settings)
    {
        api.ChatCommands.GetOrCreate(LemuriaModSystem.ModId)
            .WithDescription("Lemuria commands")
            .RequiresPrivilege(Privilege.chat)
            .BeginSubCommand("earmuffs")
                .WithDescription("Ear muffs commands")
                .RequiresPrivilege(Privilege.controlserver)
                .BeginSubCommand("test")
                    .WithDescription("Checks the ear muffs item, its recipes and the settings sent to players")
                    .HandleWith(_ =>
                    {
                        var item = api.World.GetItem(new AssetLocation(LemuriaModSystem.ModId, ItemCode));
                        int recipes = api.World.GridRecipes.Count(r => r.Output?.Code?.Domain == LemuriaModSystem.ModId && r.Output.Code.Path == ItemCode);
                        string slot = item?.Attributes?["clothescategory"].AsString() ?? "none";
                        float warmth = item?.Attributes?["warmth"].AsFloat(0) ?? 0;
                        bool wears = item?.Durability <= 1;
                        Mod.Logger.Notification("Earmuffs test: the item exists: {0}, worn in the {1} slot, warmth {2:0.##}, never wears: {3}; {4} grid recipe(s), one per leather or linen colour; sent to players: ear muffs {5:0.##}, Lemuria's sounds {6:0.##}",
                            item != null ? "yes" : "no", slot, warmth, wears ? "yes" : "no", recipes, settings.EarMuffsVolume, settings.SoundVolume);
                        return TextCommandResult.Success("Earmuffs test: checked, see the log");
                    })
                .EndSubCommand()
            .EndSubCommand();
    }

    public override void Dispose()
    {
        muffler?.Dispose();
        muffler = null;
        LemuriaSound.Scale = 1f;
    }
}
