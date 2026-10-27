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

    public override void StartServerSide(ICoreServerAPI api)
    {
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
    }
}
