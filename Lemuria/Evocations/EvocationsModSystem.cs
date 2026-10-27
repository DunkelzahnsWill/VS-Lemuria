using System;
using System.Linq;
using Lemuria.Core;
using Lemuria.Crystals;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;
using Vintagestory.API.Util;
using Vintagestory.GameContent;

namespace Lemuria.Evocations;

/// <summary>
/// Evocations (design note "Lemuria step 3: Naked evocations"): a charged Fire, Ice, Earth or Sky
/// crystal in the hand casts its naked evocation, Ignite, Quench, Tremor or Gale, and can be
/// over-driven for a stronger cast at the risk of a crack. Adds no item or block: the behavior that
/// casts sits on step 2's crystals by a JSON patch on Lemuria's own crystal file, and every effect
/// uses the game's own fire, lake ice, knockback, ignition and claims. No Harmony.
/// Adds <c>/lemuria evocations test ...</c> for the smoke test and for admins.
/// </summary>
public class EvocationsModSystem : ModSystem
{
    public const string ChannelName = LemuriaModSystem.ModId + "-evocations";

    /// <summary>Players within this many blocks of a cast see and hear it.</summary>
    private const double HearingRange = 64;

    public static EvocationsModSystem? ServerSide { get; private set; }
    public static EvocationsModSystem? ClientSide { get; private set; }

    /// <summary>Raised on the server after a cast acted on something; the Journal listens.</summary>
    public static event Action<IServerPlayer>? Cast;

    public ICoreServerAPI Api { get; private set; } = null!;
    private IServerNetworkChannel? serverChannel;
    private ICoreClientAPI? capi;

    /// <summary>The caster's own wind-up; null on a dedicated server.</summary>
    public WindUp WindUp { get; private set; } = null!;

    public override void Start(ICoreAPI api)
    {
        api.RegisterCollectibleBehaviorClass(CollectibleBehaviorEvocation.Name, typeof(CollectibleBehaviorEvocation));
        api.Network.RegisterChannel(ChannelName).RegisterMessageType<CastPacket>();
    }

    /// <summary>
    /// A Fire crystal lights an unlit firepit within arm's reach with a plain right-click, as a torch
    /// does, instead of opening it: the firepit hands the click on to a held block with the game's
    /// CanIgnite behavior. Fire crystals carry one in their block behaviors only, where the firepit
    /// looks; held interactions go through the collectible behaviors, so it never acts as a torch would.
    /// </summary>
    public override void AssetsFinalize(ICoreAPI api)
    {
        foreach (var block in api.World.Blocks)
        {
            if (block is BlockLemuriaCrystal && Crystal.AlignmentOf(block) == Crystal.Fire && !block.HasBehavior<BlockBehaviorCanIgnite>())
            {
                block.BlockBehaviors = block.BlockBehaviors.Append(new BlockBehaviorCanIgnite(block));
            }
        }
        if (api.Side == EnumAppSide.Server)
        {
            // One kind per alignment and Evolution, counted on the upright crystal as Crystals counts them.
            bool Upright(Block b) => b.Variant["position"] == "up";
            int casting = api.World.Blocks.Count(b => Upright(b) && Evocation.Casts(b) && b.HasBehavior<CollectibleBehaviorEvocation>());
            int lighting = api.World.Blocks.Count(b => Upright(b) && Evocation.Casts(b) && b.HasBehavior<BlockBehaviorCanIgnite>());
            Mod.Logger.Notification("Evocations: {0} crystal kinds cast, {1} of them light firepits within reach", casting, lighting);
        }
    }

    public override void StartServerSide(ICoreServerAPI api)
    {
        Api = api;
        ServerSide = this;
        serverChannel = api.Network.GetChannel(ChannelName);
        api.Event.SaveGameLoaded += () =>
        {
            var config = LemuriaModSystem.Config;
            Mod.Logger.Notification("Evocations: wind-up {0:0.##} s, over-drive at {1:0.##} s; a cast costs {2:0.##} of a Glittering crystal's {3:0}; range {4:0.#}, {5:0.#} and {6:0.#} blocks",
                Evocation.WindUpSeconds, Evocation.OverdriveSeconds, Evocation.Cost(Crystal.Glittering, false), Crystal.Capacity(Crystal.Glittering),
                Evocation.Range(1), Evocation.Range(2), Evocation.Range(3));
        };
        RegisterTestCommands(api);
    }

    public override void StartClientSide(ICoreClientAPI api)
    {
        capi = api;
        ClientSide = this;
        WindUp = new WindUp(api);
        api.Network.GetChannel(ChannelName).SetMessageHandler<CastPacket>(OnCastPacket);
    }

    /// <summary>Tells the players near a cast to show it.</summary>
    public void SendCast(CastContext cast, Vec3d hand, Effects.Outcome outcome)
    {
        if (serverChannel == null)
        {
            return;
        }
        var packet = new CastPacket
        {
            Alignment = cast.Alignment,
            Kind = outcome switch { Effects.Outcome.Acted => CastPacket.Cast, Effects.Outcome.Failed => CastPacket.Failed, _ => CastPacket.Fizzle },
            Power = cast.Power,
            FromX = hand.X,
            FromY = hand.Y,
            FromZ = hand.Z,
            ToX = cast.Point.X,
            ToY = cast.Point.Y,
            ToZ = cast.Point.Z,
        };
        var near = Api.World.AllOnlinePlayers.OfType<IServerPlayer>()
            .Where(p => p.ConnectionState == EnumClientState.Playing && p.Entity != null && p.Entity.Pos.XYZ.DistanceTo(hand) <= HearingRange)
            .ToArray();
        if (near.Length > 0)
        {
            serverChannel.SendPacket(packet, near);
        }
    }

    public void OnCast(IServerPlayer player, CastContext cast) => Cast?.Invoke(player);

    // ------------------------------------------------------------------ client

    private static AssetLocation? SoundOf(string alignment) => Evocation.Of(alignment) switch
    {
        Evocation.Ignite => new AssetLocation("game", "sounds/torch-ignite"),
        Evocation.Quench => new AssetLocation(LemuriaModSystem.ModId, "sounds/evocations/hiss"),
        Evocation.Tremor => new AssetLocation(LemuriaModSystem.ModId, "sounds/evocations/rumble"),
        Evocation.Gale => new AssetLocation(LemuriaModSystem.ModId, "sounds/evocations/gust"),
        _ => null,
    };

    /// <summary>A streak of sparks in the crystal's colour from the hand to where the cast landed, and a spray or puff there.</summary>
    private void OnCastPacket(CastPacket packet)
    {
        if (capi == null)
        {
            return;
        }
        var world = capi.World;
        var from = new Vec3d(packet.FromX, packet.FromY, packet.FromZ);
        var to = new Vec3d(packet.ToX, packet.ToY, packet.ToZ);
        int rgb = Crystal.Rgb(packet.Alignment);
        double distance = from.DistanceTo(to);
        int steps = Math.Clamp((int)(distance * 4), 2, 80);
        var streak = new SimpleParticleProperties(1, 1, rgb, new Vec3d(), new Vec3d(), new Vec3f(-0.05f, -0.05f, -0.05f), new Vec3f(0.05f, 0.05f, 0.05f),
            0.45f, 0f, 0.12f, 0.2f, EnumParticleModel.Quad)
        {
            VertexFlags = 255,
            OpacityEvolve = new EvolvingNatFloat(EnumTransformFunction.LINEAR, -200f),
        };
        for (int i = 0; i <= steps; i++)
        {
            double t = (double)i / steps;
            var at = new Vec3d(from.X + (to.X - from.X) * t, from.Y + (to.Y - from.Y) * t, from.Z + (to.Z - from.Z) * t);
            streak.MinPos.Set(at.X - 0.04, at.Y - 0.04, at.Z - 0.04);
            streak.AddPos.Set(0.08, 0.08, 0.08);
            world.SpawnParticles(streak);
        }
        if (packet.Kind == CastPacket.Failed)
        {
            // The crystal's note wavers and breaks off: the cast failed and its charge is spent.
            Spray(to, rgb, 6, 0.4f);
            world.PlaySoundAt(new AssetLocation(LemuriaModSystem.ModId, "sounds/evocations/warble"), from.X, from.Y, from.Z, null, false, 16f, LemuriaSound.Volume(0.8f));
            return;
        }
        if (packet.Kind == CastPacket.Fizzle)
        {
            Spray(to, rgb, 8, 0.3f);
            world.PlaySoundAt(new AssetLocation(LemuriaModSystem.ModId, "sounds/evocations/fizzle"), to.X, to.Y, to.Z, null, true, 16f, LemuriaSound.Volume(0.6f));
            return;
        }
        Spray(to, rgb, 12 + 12 * packet.Power, 0.5f + 0.5f * (Evocation.AreaSize(packet.Power) - 1));
        var sound = SoundOf(packet.Alignment);
        if (sound != null)
        {
            world.PlaySoundAt(sound, to.X, to.Y, to.Z, null, true, 32f, LemuriaSound.Volume(1f));
        }
        if (Evocation.Of(packet.Alignment) == Evocation.Tremor && world.Player?.Entity?.Pos.XYZ.DistanceTo(to) <= 16)
        {
            world.AddCameraShake(0.15f + 0.1f * packet.Power);
        }
    }

    private void Spray(Vec3d at, int rgb, int count, float spread)
    {
        var particles = new SimpleParticleProperties(count, count, rgb,
            at.AddCopy(-spread, -0.1, -spread), at.AddCopy(spread, 0.3, spread),
            new Vec3f(-1f, 0.2f, -1f), new Vec3f(1f, 1.5f, 1f), 0.8f, 0.3f, 0.12f, 0.3f, EnumParticleModel.Quad)
        {
            VertexFlags = 220,
            OpacityEvolve = new EvolvingNatFloat(EnumTransformFunction.LINEAR, -180f),
        };
        capi!.World.SpawnParticles(particles);
    }

    // ------------------------------------------------------------------ test commands

    private void RegisterTestCommands(ICoreServerAPI api)
    {
        api.ChatCommands.GetOrCreate(LemuriaModSystem.ModId)
            .WithDescription("Lemuria commands")
            .RequiresPrivilege(Privilege.chat)
            .BeginSubCommand("evocations")
                .WithDescription("Evocation commands")
                .RequiresPrivilege(Privilege.controlserver)
                .BeginSubCommand("test")
                    .WithDescription("Casts each evocation at fixed scenes next to the world spawn")
                    .BeginSubCommand("fire")
                        .WithDescription("Ignite lights a firepit 6 blocks away and sets a hen alight; Quench puts both out")
                        .HandleWith(_ => TestSite.WithChunk(api, Mod.Logger, "Evocations", 4, -12, column => new EvocationsTests(this, api, Mod.Logger).Fire(column)))
                    .EndSubCommand()
                    .BeginSubCommand("water")
                        .WithDescription("A Glowing Quench aimed at a pond's corner freezes 2x2 of it and an over-driven one the rest; Quench soaks farmland")
                        .HandleWith(_ => TestSite.WithChunk(api, Mod.Logger, "Evocations", 4, -20, column => new EvocationsTests(this, api, Mod.Logger).Water(column)))
                    .EndSubCommand()
                    .BeginSubCommand("tremor")
                        .WithDescription("Tremor raises a 2x2 of real ground 2 and 4 blocks from its sides; ore moves with it, a chest beside it fails it, hens ride")
                        .HandleWith(_ => TestSite.WithChunk(api, Mod.Logger, "Evocations", 4, -28, column => new EvocationsTests(this, api, Mod.Logger).Tremor(column)))
                    .EndSubCommand()
                    .BeginSubCommand("gale")
                        .WithDescription("Gale pushes a dropped item and a hen away from the caster")
                        .HandleWith(_ => TestSite.WithChunk(api, Mod.Logger, "Evocations", 4, -36, column => new EvocationsTests(this, api, Mod.Logger).Gale(column)))
                    .EndSubCommand()
                    .BeginSubCommand("reap")
                        .WithDescription("Gale reaps leaves and crops and pans sand; over-driven Fire melts granite to lava, and Quench cools it")
                        .HandleWith(_ => TestSite.WithChunk(api, Mod.Logger, "Evocations", 4, -44, column => new EvocationsTests(this, api, Mod.Logger).Reap(column)))
                    .EndSubCommand()
                    .BeginSubCommand("overdrive")
                        .WithDescription("Checks the wind-up and over-drive times, what casts cost, cracks, shattering and casting from a stack")
                        .HandleWith(_ =>
                        {
                            new EvocationsTests(this, api, Mod.Logger).Overdrive();
                            return TextCommandResult.Success("Evocations test: over-drive checked, see the log");
                        })
                    .EndSubCommand()
                .EndSubCommand()
            .EndSubCommand();
    }

    public override void Dispose()
    {
        WindUp?.Dispose();
        if (ServerSide == this)
        {
            ServerSide = null;
            Cast = null;
        }
        if (ClientSide == this)
        {
            ClientSide = null;
        }
    }
}
