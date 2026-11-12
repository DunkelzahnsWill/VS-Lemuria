using Lemuria.Core;
using Lemuria.Crystals;
using Lemuria.Evocations;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Server;
using Vintagestory.GameContent;

namespace Lemuria.Mysteries;

/// <summary>
/// The Four Mysteries (design note "Lemuria step 5: The Four Mysteries", part 1): Life, Death, Time and
/// Rust read as a condition of the place times a sign within 2 blocks (<see cref="MysteryEnvironment"/>),
/// a death within 8 blocks giving Death an hour, their four naked evocations (<see cref="MysteryEffects"/>,
/// <see cref="Withering"/>), and Glint, the Life-crystal lamp (<see cref="BlockEntityGlint"/>). Crystals,
/// cradles and machines read the Mysteries through the hook in CrystalEnvironment, and cast them through
/// Evocations' Effects.More, so no other feature knows this one. No Harmony.
/// Adds <c>/lemuria mysteries test ...</c> for the smoke test, for admins and for the play-test.
/// </summary>
public class MysteriesModSystem : ModSystem
{
    /// <summary>The running server's withering creatures; null on a client.</summary>
    public static Withering? Withering { get; private set; }

    private ICoreServerAPI? sapi;

    public override void Start(ICoreAPI api)
    {
        api.RegisterBlockClass(BlockGlint.ClassCode, typeof(BlockGlint));
        api.RegisterBlockEntityClass(BlockEntityGlint.ClassCode, typeof(BlockEntityGlint));
        // Both sides read the Mysteries, as both read the Elementals; only the server acts on it.
        CrystalEnvironment.Mysteries = MysteryEnvironment.Count;
    }

    public override void StartServerSide(ICoreServerAPI api)
    {
        sapi = api;
        MysteryEnvironment.StabilitySystem = api.ModLoader.GetModSystem<SystemTemporalStability>();
        Withering = new Withering(api);
        Effects.More[Evocation.Bloom] = MysteryEffects.Bloom;
        Effects.More[Evocation.Wither] = MysteryEffects.Wither;
        Effects.More[Evocation.Rewind] = MysteryEffects.Rewind;
        Effects.More[Evocation.Progress] = MysteryEffects.Progress;
        api.Event.OnEntityDeath += OnEntityDeath;
        api.Event.RegisterGameTickListener(_ => Withering?.Tick(), 1000, 500);
        api.Event.SaveGameLoaded += () =>
        {
            MysteryEffects.Ledger.Load(api);
            bool on = api.World.Config.GetBool("temporalStability", true);
            Mod.Logger.Notification("Mysteries: Life, Death, Time and Rust read their signs; temporal stability {0}; Rewind and Progress move {1:0.#}, {2:0.#} and {3:0.#} hours",
                on ? "on" : "off, so depth stands in", MysteryEffects.Hours(1), MysteryEffects.Hours(2), MysteryEffects.Hours(3));
            Mod.Logger.Notification("Mysteries: Wither deals {0:0.##} every {1:0.#} s, {2} times; a Glint burns {3:0.###} an hour at Glittering",
                Withering.Damage, Withering.TickSeconds, Withering.Ticks, BlockEntityGlint.BurnPerHour(Crystal.Glittering));
        };
        api.Event.GameWorldSave += () => MysteryEffects.Ledger.Save(api);
        new MysteriesTests(api, Mod.Logger).Register();
    }

    /// <summary>A creature or player died: Death crystals and cradles within 8 blocks get an hour of full Death.</summary>
    private void OnEntityDeath(Entity entity, DamageSource damageSource)
    {
        if (entity is EntityAgent)
        {
            CrystalsModSystem.OnDeathAt(entity.Pos.AsBlockPos);
        }
    }

    public override void Dispose()
    {
        if (sapi != null)
        {
            sapi.Event.OnEntityDeath -= OnEntityDeath;
            Effects.More.Remove(Evocation.Bloom);
            Effects.More.Remove(Evocation.Wither);
            Effects.More.Remove(Evocation.Rewind);
            Effects.More.Remove(Evocation.Progress);
            Withering = null;
            MysteryEnvironment.StabilitySystem = null;
        }
        CrystalEnvironment.Mysteries = null;
    }
}
