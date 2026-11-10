using System;
using System.Linq;
using Lemuria.Core;
using Lemuria.Cradle;
using Lemuria.Crystals;
using Lemuria.Evocations;
using Lemuria.Machines;
using Lemuria.Sparkstone;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;
using Vintagestory.GameContent;

namespace Lemuria.Mysteries;

/// <summary>
/// The scenes behind <c>/lemuria mysteries test ...</c> (design note "Lemuria step 5: The Four Mysteries",
/// How it is built: Smoke tests). Each builds its place beside the world spawn with no player, so no claim
/// applies, and reports to the log. The four places leave an Unaligned crystal in their cradle, so the
/// owner can watch one refine in play. Where the place's temporal stability matters, a scene pins it
/// through the game's own hook for the moment it reads, or raises a storm for that moment, so the
/// smoke world's seed decides nothing.
/// </summary>
public class MysteriesTests
{
    private const string GlintKey = LemuriaModSystem.ModId + "-mysteriestest-glint";

    private readonly ICoreServerAPI sapi;
    private readonly ILogger logger;

    public MysteriesTests(ICoreServerAPI sapi, ILogger logger)
    {
        this.sapi = sapi;
        this.logger = logger;
    }

    private IWorldAccessor World => sapi.World;

    private IBlockAccessor Accessor => sapi.World.BlockAccessor;

    private int Id(string code) => World.GetBlock(new AssetLocation(code))?.Id ?? 0;

    private string Path(BlockPos pos) => Accessor.GetBlock(pos).Code?.Path ?? "?";

    private static string YesNo(bool value) => value ? "yes" : "no";

    private static string F(float value) => value.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);

    private double HoursPerDay => World.Calendar.HoursPerDay;

    public void Register()
    {
        // A scene reaches up to 16 blocks east and south of its column, so the chunks it reaches load first.
        TextCommandResult Test(int dx, int dz, Action<BlockPos> action) =>
            TestSite.WithChunk(sapi, logger, "Mysteries", dx + 16, dz + 16, _ =>
                TestSite.WithChunk(sapi, logger, "Mysteries", dx + 16, dz, _ =>
                    TestSite.WithChunk(sapi, logger, "Mysteries", dx, dz + 16, _ =>
                        TestSite.WithChunk(sapi, logger, "Mysteries", dx, dz, action))));
        var scenes = new (string Name, string Description, int Dx, int Dz, Action<BlockPos> Action)[]
        {
            ("life", "Builds a Life place: ten crops lit by a torch under glass, and a cradle that refines", 40, 40, Life),
            ("death", "Builds a Death place: bones in a dark cave, and a cradle that refines", 40, 60, Death),
            ("time", "Builds a Time place: temporal gears in a chest where stability is full, and a cradle that refines", 60, 40, Time),
            ("rust", "Builds a Rust place: rusty gears in a chest during a storm, and a cradle that refines", 60, 60, Rust),
            ("forge", "A Glowing Unaligned crystal still refines Fire at a firepit; ten crops beside another pause it", 80, 40, Forge),
            ("twins", "Life beside Death and Time beside Rust neutralize each other", 80, 60, Twins),
            ("deathhour", "A hen dies beside a Death crystal, which charges for the hour after", 100, 40, DeathHour),
            ("bloom", "Bloom grows a crop one stage, once a day; Glowing blooms a 3×3", 100, 60, Bloom),
            ("wither", "Wither kills a crop and bares grassy soil; Glowing withers a pig 1.5 every 5 seconds", 120, 40, Wither),
            ("time-cast", "Rewind turns spoiling meat back, never past fresh; Progress ripens cheese and moves a sealed barrel", 120, 60, Rewind),
            ("glint", "Carves glass into a Glint; its light by level and Evolution, its burn, and every crystal's light 1 to 4", 140, 40, Glint),
            ("machines", "A Life Focus blooms once; a Refine block copies Time; Time and Rust Focus blocks work a chest by the measure", 140, 60, MachinesScene),
        };
        var test = sapi.ChatCommands.GetOrCreate(LemuriaModSystem.ModId)
            .WithDescription("Lemuria commands")
            .RequiresPrivilege(Privilege.chat)
            .BeginSubCommand("mysteries")
                .WithDescription("Commands for the Four Mysteries")
                .RequiresPrivilege(Privilege.controlserver)
                .BeginSubCommand("test")
                    .WithDescription("Tests the Four Mysteries next to the world spawn");
        foreach (var scene in scenes)
        {
            var s = scene;
            test = test.BeginSubCommand(s.Name).WithDescription(s.Description).HandleWith(_ => Test(s.Dx, s.Dz, s.Action)).EndSubCommand();
        }
        test.BeginSubCommand("glintcheck")
                .WithDescription("After a restart: the test Glint still holds its crystal and charge")
                .HandleWith(_ => Test(140, 40, _ => GlintCheck()))
            .EndSubCommand()
            .EndSubCommand()
            .EndSubCommand();
    }

    // ------------------------------------------------------------------ helpers

    /// <summary>Clears a box of air above a granite floor, with a roof one block above the box (none for null).</summary>
    private void Room(BlockPos origin, int sizeX, int sizeY, int sizeZ, string? roof = "game:glass-plain", string floor = "game:rock-granite")
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                Accessor.SetBlock(Id("game:rock-granite"), origin.AddCopy(x, -2, z));
                Accessor.SetBlock(Id(floor), origin.AddCopy(x, -1, z));
                for (int y = 0; y < sizeY; y++)
                {
                    Accessor.SetBlock(0, origin.AddCopy(x, y, z));
                    Accessor.SetBlock(0, origin.AddCopy(x, y, z), BlockLayersAccess.Fluid);
                }
                Accessor.SetBlock(roof == null ? 0 : Id(roof), origin.AddCopy(x, sizeY, z));
            }
        }
    }

    private BlockEntityCradle? Cradle(BlockPos pos, string evolution = Crystal.Glittering)
    {
        Accessor.SetBlock(Id(LemuriaModSystem.ModId + ":cradle-granite"), pos);
        if (Accessor.GetBlockEntity(pos) is not BlockEntityCradle cradle)
        {
            logger.Error("Mysteries test: no cradle at {0}", TestSite.Describe(sapi, pos));
            return null;
        }
        cradle.SetCrystal(CrystalStack(Crystal.Unaligned, evolution));
        return cradle;
    }

    private ItemStack? CrystalStack(string alignment, string evolution, float charge = 0)
    {
        var block = Crystal.BlockFor(World, alignment, evolution);
        if (block == null)
        {
            return null;
        }
        var stack = new ItemStack(block);
        Crystal.SetState(stack, charge, 0);
        return stack;
    }

    private BlockEntityCrystal? PlaceCrystal(BlockPos pos, string alignment, string evolution, float charge = 0)
    {
        Accessor.SetBlock(Crystal.BlockFor(World, alignment, evolution)?.Id ?? 0, pos);
        var crystal = Accessor.GetBlockEntity(pos) as BlockEntityCrystal;
        crystal?.SetCharge(charge);
        return crystal;
    }

    /// <summary>Refines the cradle's crystal for a day and a twentieth of its Evolution's refining time; returns what it holds.</summary>
    private string RefineDay(BlockEntityCradle cradle)
    {
        var config = LemuriaModSystem.Config;
        double days = cradle.Evolution == Crystal.Glowing ? config.RefineDaysGlowing : config.RefineDaysGlittering;
        cradle.RefineFor(days * HoursPerDay * 1.05);
        return Holds(cradle.CrystalStack);
    }

    private static string Holds(ItemStack? stack) =>
        stack == null || !Crystal.IsCrystal(stack.Collectible) ? "nothing" : $"{Crystal.AlignmentOf(stack.Collectible)} {Crystal.EvolutionOf(stack.Collectible)}";

    private float Mystery(BlockPos pos, int index) => MysteryEnvironment.Strengths(World, pos)[index - CrystalEnvironment.LifeIndex];

    /// <summary>Runs an action while the game reads this temporal stability everywhere.</summary>
    private T Pinned<T>(float stability, Func<T> action)
    {
        var system = MysteryEnvironment.StabilitySystem;
        GetTemporalStabilityDelegate pin = (_, _, _, _) => stability;
        if (system != null)
        {
            system.OnGetTemporalStability += pin;
        }
        try
        {
            return action();
        }
        finally
        {
            if (system != null)
            {
                system.OnGetTemporalStability -= pin;
            }
        }
    }

    /// <summary>Runs an action during a strong temporal storm, then puts the storm data back as it was.</summary>
    private T Storm<T>(Func<T> action)
    {
        var data = MysteryEnvironment.StabilitySystem?.StormData;
        if (data == null)
        {
            return action();
        }
        bool active = data.nowStormActive;
        float strength = data.stormGlitchStrength;
        data.nowStormActive = true;
        data.stormGlitchStrength = 0.9f;
        try
        {
            return action();
        }
        finally
        {
            data.nowStormActive = active;
            data.stormGlitchStrength = strength;
        }
    }

    private void Chest(BlockPos pos, params ItemStack?[] stacks)
    {
        Accessor.SetBlock(Id("game:chest-east"), pos);
        if (Accessor.GetBlockEntity(pos) is not BlockEntityGenericTypedContainer chest)
        {
            logger.Error("Mysteries test: no chest at {0}", TestSite.Describe(sapi, pos));
            return;
        }
        for (int i = 0; i < stacks.Length && i < chest.Inventory.Count; i++)
        {
            chest.Inventory[i].Itemstack = stacks[i];
            chest.Inventory[i].MarkDirty();
        }
    }

    private ItemStack? Items(string code, int size) =>
        World.GetItem(new AssetLocation(code)) is { } item ? new ItemStack(item, size) : null;

    private void Crop(BlockPos pos, string crop = "game:crop-flax-1")
    {
        Accessor.SetBlock(Id("game:farmland-moist-medium"), pos.DownCopy());
        Accessor.SetBlock(Id(crop), pos);
    }

    private bool CastAt(string alignment, int power, Vec3d from, Vec3d at) =>
        Effects.Apply(CastContext.Aim(sapi, null, null, alignment, power, from, at.SubCopy(from))) == Effects.Outcome.Acted;

    private bool CastDown(string alignment, int power, BlockPos at) =>
        CastAt(alignment, power, at.ToVec3d().Add(0.5, 3.5, 0.5), at.ToVec3d().Add(0.5, 0.3, 0.5));

    private Entity? Spawn(string code, Vec3d at)
    {
        var type = World.GetEntityType(new AssetLocation(code));
        if (type == null)
        {
            logger.Error("Mysteries test: no entity type {0}", code);
            return null;
        }
        var entity = sapi.ClassRegistry.CreateEntity(type);
        entity.Pos.SetPos(at);
        World.SpawnEntity(entity);
        return entity;
    }

    /// <summary>A stack's hours of one kind of transition, or -1.</summary>
    private float TransitionHours(ItemStack? stack, EnumTransitionType type)
    {
        if (stack?.Attributes["transitionstate"] is not ITreeAttribute state || state["transitionedHours"] is not FloatArrayAttribute hours)
        {
            return -1;
        }
        var props = stack.Collectible.GetTransitionableProperties(World, stack, null);
        for (int i = 0; props != null && i < props.Length && i < hours.value.Length; i++)
        {
            if (props[i]?.Type == type)
            {
                return hours.value[i];
            }
        }
        return -1;
    }

    private ItemStack? Aged(string code, EnumTransitionType type, float hours)
    {
        var stack = Items(code, 1);
        if (stack != null)
        {
            stack.Collectible.UpdateAndGetTransitionStates(World, new DummySlot(stack));
            stack.Collectible.SetTransitionState(stack, type, hours);
        }
        return stack;
    }

    private ItemSlot? ChestSlot(BlockPos pos, int index) =>
        Accessor.GetBlockEntity(pos) is BlockEntityGenericTypedContainer chest ? chest.Inventory[index] : null;

    // ------------------------------------------------------------------ the four places

    public void Life(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Room(origin, 7, 3, 7);
        var center = origin.AddCopy(3, 0, 3);
        var cradle = Cradle(center);
        var torch = center.AddCopy(0, 0, 1);
        Accessor.SetBlock(Id("game:torch-basic-lit-up"), torch);
        float conditionAlone = Mystery(center, CrystalEnvironment.LifeIndex);
        int planted = 0;
        for (int dx = -2; dx <= 2 && planted < 10; dx++)
        {
            for (int dz = -2; dz <= 2 && planted < 10; dz++)
            {
                if (Math.Abs(dx) == 2 || Math.Abs(dz) == 2)
                {
                    Crop(center.AddCopy(dx, 0, dz));
                    planted++;
                }
            }
        }
        float full = Mystery(center, CrystalEnvironment.LifeIndex);
        string refined = cradle == null ? "nothing" : RefineDay(cradle);
        var charged = PlaceCrystal(center.AddCopy(-1, 0, -1), Crystal.Life, Crystal.Glittering);
        charged?.ReadSurroundings(HoursPerDay / 2);
        logger.Notification("Mysteries test: Life by torchlight alone reads {0}, with {1} crops {2}; the cradle refined to {3}; a Life crystal there charged {4:0}",
            F(conditionAlone), planted, F(full), refined, charged?.Charge ?? -1);
        cradle?.SetCrystal(CrystalStack(Crystal.Unaligned, Crystal.Glittering));
    }

    public void Death(BlockPos column)
    {
        // A cave twelve blocks under the surface: a granite shell round a 5×3×5 hollow, dark at any hour.
        var origin = TestSite.Surface(sapi, column).AddCopy(0, -14, 0);
        for (int x = -1; x <= 5; x++)
        {
            for (int y = -2; y <= 3; y++)
            {
                for (int z = -1; z <= 5; z++)
                {
                    bool hollow = x is >= 0 and <= 4 && y is >= 0 and <= 2 && z is >= 0 and <= 4;
                    Accessor.SetBlock(hollow ? 0 : Id("game:rock-granite"), origin.AddCopy(x, y, z));
                    Accessor.SetBlock(0, origin.AddCopy(x, y, z), BlockLayersAccess.Fluid);
                }
            }
        }
        var center = origin.AddCopy(2, 0, 2);
        var cradle = Cradle(center);
        float conditionAlone = Mystery(center, CrystalEnvironment.DeathIndex);
        foreach (var (dx, dz) in new[] { (-2, -2), (2, -2), (-2, 2), (2, 2) })
        {
            Accessor.SetBlock(Id("game:bonyremains-ribcage"), center.AddCopy(dx, 0, dz));
        }
        var torch = center.AddCopy(0, 0, 1);
        Accessor.SetBlock(Id("game:torch-basic-lit-up"), torch);
        float signAlone = Mystery(center, CrystalEnvironment.DeathIndex);
        Accessor.SetBlock(0, torch);
        float full = Mystery(center, CrystalEnvironment.DeathIndex);
        // Sixteen bones in a chest count as a quarter.
        var chestPos = center.AddCopy(1, 0, 0);
        Chest(chestPos, Items("game:bone", 16));
        float withChest = MysteryEnvironment.Read(World, center).Bones;
        string refined = cradle == null ? "nothing" : RefineDay(cradle);
        logger.Notification("Mysteries test: Death in the dark with no bones reads {0}, four bony remains by torchlight {1}, in the dark {2}; with 16 bones in a chest the bones count {3}; the cradle refined to {4}",
            F(conditionAlone), F(signAlone), F(full), F(withChest), refined);
        cradle?.SetCrystal(CrystalStack(Crystal.Unaligned, Crystal.Glittering));
    }

    public void Time(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Room(origin, 5, 3, 5);
        var center = origin.AddCopy(2, 0, 2);
        var cradle = Cradle(center);
        Chest(center.AddCopy(1, 0, 0), Items("game:gear-temporal", 2));
        float here = MysteryEnvironment.Read(World, center).Stability;
        float signAlone = Pinned(1f, () => Mystery(center, CrystalEnvironment.TimeIndex));
        float full = Pinned(1.5f, () => Mystery(center, CrystalEnvironment.TimeIndex));
        float storm = Storm(() => Mystery(center, CrystalEnvironment.TimeIndex));
        string refined = cradle == null ? "nothing" : Pinned(1.5f, () => RefineDay(cradle));
        logger.Notification("Mysteries test: two temporal gears read Time {0} where stability is 1 and {1} where it is 1.5; a storm brings it to {2}; the cradle refined to {3} (stability here {4})",
            F(signAlone), F(full), F(storm), refined, F(here));
        cradle?.SetCrystal(CrystalStack(Crystal.Unaligned, Crystal.Glittering));
    }

    public void Rust(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Room(origin, 5, 3, 5);
        var center = origin.AddCopy(2, 0, 2);
        var cradle = Cradle(center);
        var chestPos = center.AddCopy(1, 0, 0);
        Chest(chestPos);
        float conditionAlone = Storm(() => Mystery(center, CrystalEnvironment.RustIndex));
        Chest(chestPos, Items("game:gear-rusty", 16));
        float stable = Pinned(1f, () => Mystery(center, CrystalEnvironment.RustIndex));
        float storm = Storm(() => Mystery(center, CrystalEnvironment.RustIndex));
        float stormStability = Storm(() => MysteryEnvironment.Read(World, center).Stability);
        float decay = MysteryEnvironment.Read(World, center).Decay;
        string refined = cradle == null ? "nothing" : Storm(() => RefineDay(cradle));
        logger.Notification("Mysteries test: a storm with no decay reads Rust {0}; sixteen rusty gears count {1} and read {2} where stability is 1, {3} in the storm (stability {4}); the cradle refined to {5}",
            F(conditionAlone), F(decay), F(stable), F(storm), F(stormStability), refined);
        cradle?.SetCrystal(CrystalStack(Crystal.Unaligned, Crystal.Glittering));
    }

    // ------------------------------------------------------------------ the Elementals stay; twins cancel

    /// <summary>A burning firepit east of a position on a granite block.</summary>
    private void Firepit(BlockPos at)
    {
        Accessor.SetBlock(Id("game:rock-granite"), at.DownCopy());
        Accessor.SetBlock(Id("game:firepit-cold"), at);
        if (Accessor.GetBlockEntity(at) is BlockEntityFirepit firepit)
        {
            firepit.igniteWithFuel(new ItemStack(World.GetItem(new AssetLocation("game:firewood")), 1));
        }
    }

    public void Forge(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Room(origin, 12, 2, 5, "game:rock-granite");
        var hot = origin.AddCopy(2, 0, 2);
        Firepit(hot.EastCopy());
        var cradle = Cradle(hot, Crystal.Glowing);
        string refined = cradle == null ? "nothing" : RefineDay(cradle);
        // Eight blocks on, the same firepit with ten crops and a torch round the cradle.
        var paused = origin.AddCopy(8, 0, 2);
        Firepit(paused.EastCopy());
        Accessor.SetBlock(Id("game:torch-basic-lit-up"), paused.WestCopy());
        int planted = 0;
        for (int dx = -2; dx <= 2 && planted < 10; dx++)
        {
            for (int dz = -2; dz <= 2 && planted < 10; dz++)
            {
                if (Math.Abs(dz) == 2)
                {
                    Crop(paused.AddCopy(dx, 0, dz));
                    planted++;
                }
            }
        }
        var second = Cradle(paused, Crystal.Glowing);
        string pausedHolds = second == null ? "nothing" : RefineDay(second);
        var raw = CrystalEnvironment.Read(World, paused, false);
        logger.Notification("Mysteries test: a Glowing Unaligned crystal at a firepit refined to {0}; with {1} crops lit beside the firepit it holds {2}, {3:0.00} of the way (Fire {4}, Life {5})",
            refined, planted, pausedHolds, second?.RefineProgress ?? -1, F(raw[CrystalEnvironment.FireIndex]), F(raw[CrystalEnvironment.LifeIndex]));
    }

    public void Twins(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Room(origin, 12, 2, 3, null);
        var place = origin.AddCopy(1, 0, 1);
        var life = PlaceCrystal(place, Crystal.Life, Crystal.Glittering, 50);
        PlaceCrystal(place.AddCopy(2, 0, 0), Crystal.Death, Crystal.Glittering);
        PlaceCrystal(place.AddCopy(8, 0, 0), Crystal.Time, Crystal.Glittering, 50);
        PlaceCrystal(place.AddCopy(10, 0, 0), Crystal.Rust, Crystal.Glittering);
        // In a reading, Life and Death of the same strength cancel.
        var raw = new float[CrystalEnvironment.Count];
        raw[CrystalEnvironment.LifeIndex] = 0.8f;
        raw[CrystalEnvironment.DeathIndex] = 0.8f;
        raw[CrystalEnvironment.TimeIndex] = 0.6f;
        raw[CrystalEnvironment.RustIndex] = 0.6f;
        bool cancelled = CrystalEnvironment.CancelTwins(raw).All(s => s == 0);
        var dialogues = CrystalsModSystem.Server?.Dialogues;
        if (dialogues == null || life == null)
        {
            logger.Error("Mysteries test: no dialogues or crystals");
            return;
        }
        dialogues.TickStanding(1);
        dialogues.TickStanding((float)LemuriaModSystem.Config.DialogueGraceSeconds);
        string Now(BlockPos pos) => Accessor.GetBlockEntity(pos) is BlockEntityCrystal c ? $"{c.Alignment}" : "gone";
        logger.Notification("Mysteries test: equal twins cancel in a reading: {0}; standing Life and Death now {1} and {2}; Time and Rust now {3} and {4}",
            YesNo(cancelled), Now(place), Now(place.AddCopy(2, 0, 0)), Now(place.AddCopy(8, 0, 0)), Now(place.AddCopy(10, 0, 0)));
    }

    public void DeathHour(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Room(origin, 14, 2, 3);
        // Two Death crystals by torchlight, where Death reads nothing: one beside the hen, one 12 blocks off.
        var near = origin.AddCopy(1, 0, 1);
        var far = origin.AddCopy(13, 0, 1);
        var nearCrystal = PlaceCrystal(near, Crystal.Death, Crystal.Glittering);
        var farCrystal = PlaceCrystal(far, Crystal.Death, Crystal.Glittering);
        Accessor.SetBlock(Id("game:torch-basic-lit-up"), near.AddCopy(0, 0, 1));
        Accessor.SetBlock(Id("game:torch-basic-lit-up"), far.AddCopy(0, 0, 1));
        // A Gather block with a Death crystal beside each, as lightning reaches a Gather's Sky crystals.
        var nearGather = PlaceMachine(near.AddCopy(1, 0, 1), Machines.Machine.Gather, "up");
        var farGather = PlaceMachine(far.AddCopy(-1, 0, 1), Machines.Machine.Gather, "up");
        Put(nearGather, Crystal.Death, Crystal.Glittering);
        Put(farGather, Crystal.Death, Crystal.Glittering);
        nearGather?.GatherNow(0);
        farGather?.GatherNow(0);
        var hen = Spawn("game:chicken-hen", near.ToVec3d().Add(2.5, 0, 0.5));
        if (hen == null || nearCrystal == null || farCrystal == null)
        {
            return;
        }
        hen.Die(EnumDespawnReason.Death, new DamageSource { Source = EnumDamageSource.Internal, Type = EnumDamageType.Poison });
        sapi.Event.RegisterCallback(_ =>
        {
            nearCrystal.ReadSurroundings(0.5);
            farCrystal.ReadSurroundings(0.5);
            logger.Notification("Mysteries test: after a hen died, half an hour charged the Death crystal beside it to {0:0.0} and the one 12 blocks off to {1:0.0}",
                nearCrystal.Charge, farCrystal.Charge);
            logger.Notification("Mysteries test: the Gather block beside it gathered {0:0.0} in that half hour, the one 12 blocks off {1:0.0}",
                nearGather?.GatherNow(0.5) ?? -1, farGather?.GatherNow(0.5) ?? -1);
        }, 500);
    }

    // ------------------------------------------------------------------ the four evocations

    public void Bloom(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Room(origin, 9, 4, 5, null);
        var single = origin.AddCopy(1, 0, 2);
        Crop(single);
        bool first = CastDown(Crystal.Life, 1, single);
        string after = Path(single);
        bool again = CastDown(Crystal.Life, 1, single);
        string afterAgain = Path(single);
        var square = origin.AddCopy(5, 0, 2);
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dz = -1; dz <= 1; dz++)
            {
                Crop(square.AddCopy(dx, 0, dz));
            }
        }
        CastDown(Crystal.Life, 2, square);
        int grew = 0;
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dz = -1; dz <= 1; dz++)
            {
                if (Path(square.AddCopy(dx, 0, dz)) == "crop-flax-2")
                {
                    grew++;
                }
            }
        }
        logger.Notification("Mysteries test: Bloom grew flax to {0} ({1}); cast again the same day: {2}, still {3}; a Glowing Bloom grew {4} of 9",
            after, first ? "acted" : "nothing", again ? "acted" : "refused", afterAgain, grew);
    }

    public void Wither(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Room(origin, 12, 4, 5, null);
        var crop = origin.AddCopy(1, 0, 1);
        Crop(crop, "game:crop-flax-4");
        CastDown(Crystal.Death, 1, crop);
        string cropNow = Path(crop), under = Path(crop.DownCopy());
        var soil = origin.AddCopy(3, -1, 1);
        Accessor.SetBlock(Id("game:soil-medium-normal"), soil);
        CastDown(Crystal.Death, 1, soil);
        var grassSoil = origin.AddCopy(5, -1, 1);
        Accessor.SetBlock(Id("game:soil-medium-normal"), grassSoil);
        Accessor.SetBlock(Id("game:tallgrass-tall-free"), grassSoil.UpCopy());
        string grass = Path(grassSoil.UpCopy());
        CastDown(Crystal.Death, 1, grassSoil.UpCopy());
        logger.Notification("Mysteries test: Wither made the flax {0} on {1}; grassy soil is now {2}; {3} wilted to {4}",
            cropNow, under, Path(soil), grass, Accessor.GetBlock(grassSoil.UpCopy()).Id == 0 ? "nothing" : Path(grassSoil.UpCopy()));
        // The pig: a Glittering cast leaves it be; a Glowing one withers it.
        var withering = MysteriesModSystem.Withering;
        var pigAt = origin.AddCopy(9, 0, 2);
        var pig = Spawn("game:pig-eurasian-adult-male", pigAt.ToVec3d().Add(0.5, 0, 0.5));
        var health = pig?.GetBehavior<EntityBehaviorHealth>();
        if (pig == null || health == null || withering == null)
        {
            logger.Error("Mysteries test: no pig or withering");
            return;
        }
        var from = pigAt.ToVec3d().Add(0.5, 4, 0.5);
        var at = pigAt.ToVec3d().Add(0.5, 0.4, 0.5);
        CastAt(Crystal.Death, 1, from, at);
        bool glittering = withering.IsWithering(pig);
        float start = health.Health;
        CastAt(Crystal.Death, 2, from, at);
        bool glowing = withering.IsWithering(pig);
        float five = withering.Pass(5);
        float ten = withering.Pass(10);
        CastAt(Crystal.Death, 2, from, at);
        float thirty = withering.Pass(30);
        float after = withering.Pass(10);
        logger.Notification("Mysteries test: a Glittering Wither withers the pig: {0}; a Glowing one: {1}; it dealt {2} in 5 s, {3} in the next 10 s; recast, {4} in 30 s, then {5}; the pig went from {6:0.#} to {7:0.#}, alive: {8}",
            YesNo(glittering), YesNo(glowing), F(five), F(ten), F(thirty), F(after), start, health.Health, YesNo(pig.Alive));
    }

    public void Rewind(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Room(origin, 9, 3, 3, null);
        var chestPos = origin.AddCopy(1, 0, 1);
        Chest(chestPos,
            Aged("game:redmeat-raw", EnumTransitionType.Perish, 30),
            Aged("game:redmeat-raw", EnumTransitionType.Perish, 5),
            Aged("game:rawcheese-salted", EnumTransitionType.Ripen, 100));
        bool rewound = CastDown(Crystal.Time, 1, chestPos);
        float meat = TransitionHours(ChestSlot(chestPos, 0)?.Itemstack, EnumTransitionType.Perish);
        float fresh = TransitionHours(ChestSlot(chestPos, 1)?.Itemstack, EnumTransitionType.Perish);
        float ripen = TransitionHours(ChestSlot(chestPos, 2)?.Itemstack, EnumTransitionType.Ripen);
        logger.Notification("Mysteries test: Rewind ({0}) turned meat back from 30 to {1:0.#} hours and from 5 to {2:0.#}; the cheese's ripening stays at {3:0.#}",
            rewound ? "acted" : "nothing", meat, fresh, ripen);

        var cheesePos = origin.AddCopy(4, 0, 1);
        Chest(cheesePos, Aged("game:rawcheese-salted", EnumTransitionType.Ripen, 330));
        bool progressed = CastDown(Crystal.Rust, 1, cheesePos);
        string cheese = ChestSlot(cheesePos, 0)?.Itemstack?.Collectible.Code.Path ?? "nothing";
        var barrelPos = origin.AddCopy(7, 0, 1);
        Accessor.SetBlock(Id("game:barrel"), barrelPos);
        double moved = -1;
        if (Accessor.GetBlockEntity(barrelPos) is BlockEntityBarrel barrel)
        {
            barrel.Sealed = true;
            barrel.SealedSinceTotalHours = World.Calendar.TotalHours;
            double before = barrel.SealedSinceTotalHours;
            CastDown(Crystal.Rust, 1, barrelPos);
            moved = before - barrel.SealedSinceTotalHours;
        }
        logger.Notification("Mysteries test: Progress ({0}) turned salted raw cheese 12 hours from ripe into {1}; a sealed barrel came {2:0.#} hours closer",
            progressed ? "acted" : "nothing", cheese, moved);
    }

    // ------------------------------------------------------------------ Glint and crystal light

    public void Glint(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Room(origin, 12, 3, 3, "game:rock-granite");
        var pos = origin.AddCopy(1, 0, 1);
        Accessor.SetBlock(Id("game:glass-plain"), pos);
        bool carved = ItemCrystalChisel.Carve(World, null, pos);
        if (Accessor.GetBlockEntity(pos) is not BlockEntityGlint glint)
        {
            logger.Error("Mysteries test: the chisel left {0}, not a Glint", Path(pos));
            return;
        }
        int Light() => Accessor.GetBlock(pos).GetLightHsv(Accessor, pos)[2];
        glint.SetCrystal(CrystalStack(Crystal.Life, Crystal.Glittering, 100));
        int glitteringFull = Light();
        glint.ReadSurroundings(24);
        float burned = glint.Charge;
        glint.SetCrystal(CrystalStack(Crystal.Life, Crystal.Glowing, 200));
        int glowingFull = Light();
        glint.SetCharge(120);
        int glowingHigh = Light();
        glint.SetCharge(50);
        int glowingLow = Light();
        glint.SetCharge(0);
        int empty = Light();
        glint.SetCrystal(CrystalStack(Crystal.Fire, Crystal.Glowing, 200));
        int fire = Light();
        glint.SetCrystal(CrystalStack(Crystal.Life, Crystal.Glowing, 150));
        TestSite.StorePos(sapi, GlintKey, pos);
        logger.Notification("Mysteries test: the chisel carved glass into {0} ({1}); its light is {2} with a full Glittering Life crystal, after a day it holds {3:0.#}; Glowing {4} full, {5} high, {6} low, {7} empty; with Fire {8}",
            Path(pos), carved ? "carved" : "not carved", glitteringFull, burned, glowingFull, glowingHigh, glowingLow, empty, fire);

        // Every placed crystal's light by its charge, 1 to 4.
        var crystal = PlaceCrystal(origin.AddCopy(4, 0, 1), Crystal.Fire, Crystal.Glittering);
        var lights = new[] { 0f, 30f, 70f, 100f }.Select(c =>
        {
            crystal?.SetCharge(c);
            return crystal == null ? -1 : Accessor.GetBlock(crystal.Pos).GetLightHsv(Accessor, crystal.Pos)[2];
        }).ToList();
        var unaligned = PlaceCrystal(origin.AddCopy(6, 0, 1), Crystal.Unaligned, Crystal.Glittering);
        int unalignedLight = unaligned == null ? -1 : Accessor.GetBlock(unaligned.Pos).GetLightHsv(Accessor, unaligned.Pos)[2];
        logger.Notification("Mysteries test: a Fire crystal's light empty to full is {0}; an Unaligned crystal's {1}",
            string.Join(", ", lights), unalignedLight);

        // A second Glint with a charged Life crystal beside a Death crystal: their dialogue neutralizes both.
        var second = origin.AddCopy(9, 0, 1);
        Accessor.SetBlock(Id("game:glass-plain"), second);
        ItemCrystalChisel.Carve(World, null, second);
        var other = Accessor.GetBlockEntity(second) as BlockEntityGlint;
        other?.SetCrystal(CrystalStack(Crystal.Life, Crystal.Glittering, 50));
        PlaceCrystal(second.AddCopy(2, 0, 0), Crystal.Death, Crystal.Glittering);
        var dialogues = CrystalsModSystem.Server?.Dialogues;
        dialogues?.TickStanding(1);
        dialogues?.TickStanding((float)LemuriaModSystem.Config.DialogueGraceSeconds);
        logger.Notification("Mysteries test: beside a Death crystal the Glint's crystal is now {0}, its light {1}",
            other?.Alignment ?? "gone", Accessor.GetBlock(second).GetLightHsv(Accessor, second)[2]);
    }

    public void GlintCheck()
    {
        var pos = TestSite.ReadPos(sapi, GlintKey);
        if (pos == null || Accessor.GetBlockEntity(pos) is not BlockEntityGlint glint)
        {
            logger.Error("Mysteries test: no test Glint; run /lemuria mysteries test glint first");
            return;
        }
        logger.Notification("Mysteries test: after the restart the Glint holds {0} {1} at {2:0}, its light {3}",
            glint.Alignment ?? "nothing", glint.Evolution, glint.Charge, Accessor.GetBlock(pos).GetLightHsv(Accessor, pos)[2]);
    }

    // ------------------------------------------------------------------ machines

    private BlockEntityMachine? PlaceMachine(BlockPos pos, string form, string side)
    {
        var block = World.GetBlock(Machines.Machine.Code(form, "granite", side));
        if (block == null)
        {
            logger.Error("Mysteries test: no block {0}", Machines.Machine.Code(form, "granite", side));
            return null;
        }
        Accessor.SetBlock(block.Id, pos);
        return Accessor.GetBlockEntity(pos) as BlockEntityMachine;
    }

    private bool Put(BlockEntitySparkstone? stone, string alignment, string evolution, float charge = 0) =>
        stone != null && CrystalStack(alignment, evolution, charge) is { } stack && stone.Insert(stack, stone.SocketFacing ?? BlockFacing.UP);

    private static void Fill(BlockEntitySparkstone? stone)
    {
        if (stone != null)
        {
            SparkstoneModSystem.Networks?.Fill(stone);
        }
    }

    private static float Total(BlockEntitySparkstone? stone) =>
        stone != null && SparkstoneModSystem.Networks is { } networks ? SparkstoneNetworks.Measure(networks.NetworkOf(stone)).Total : 0;

    public void MachinesScene(BlockPos column)
    {
        var origin = TestSite.Surface(sapi, column);
        Room(origin, 14, 6, 9, null);
        // A Life Focus three blocks over young flax, facing down.
        var crop = origin.AddCopy(1, 0, 1);
        Crop(crop);
        var life = PlaceMachine(crop.AddCopy(0, 3, 0), Machines.Machine.Focus, "down");
        Put(life, Crystal.Life, Crystal.Glittering);
        Fill(life);
        var bloomed = life?.WorkOnce();
        string grown = Path(crop);
        var again = life?.WorkOnce();

        // A Refine block copies Time into an Unaligned crystal, paid from two stones of Fire capacitors.
        var refinePos = origin.AddCopy(5, 0, 1);
        var refine = PlaceMachine(refinePos, Machines.Machine.Refine, "up");
        Put(refine, Crystal.Time, Crystal.Glittering);
        Put(refine, Crystal.Unaligned, Crystal.Glittering);
        foreach (int dx in new[] { -1, 1 })
        {
            Accessor.SetBlock(World.GetBlock(Machines.Machine.FreeCode("granite"))?.Id ?? 0, refinePos.AddCopy(dx, 0, 0));
            var store = Accessor.GetBlockEntity(refinePos.AddCopy(dx, 0, 0)) as BlockEntitySparkstone;
            for (int j = 0; j < 4; j++)
            {
                Put(store, Crystal.Fire, Crystal.Glittering, 100);
            }
        }
        Fill(refine);
        int refined = refine?.RefineNow(Machines.Machine.RefineDays(Crystal.Glittering) * HoursPerDay) ?? 0;
        int time = refine?.Crystals.Count(c => c.Alignment == Crystal.Time) ?? 0;
        logger.Notification("Mysteries test: a Life Focus {0}, the flax is now {1}, then {2}; a Refine block refined {3} crystal(s), {4} of 2 are Time",
            bloomed, grown, again, refined, time);

        // A Time Focus facing east at a chest of spoiling meat; a Rust Focus at a chest of salted raw cheese.
        var meatChest = origin.AddCopy(4, 0, 6);
        Chest(meatChest, Aged("game:redmeat-raw", EnumTransitionType.Perish, 30));
        var timeFocus = PlaceMachine(meatChest.AddCopy(-3, 0, 0), Machines.Machine.Focus, "east");
        Put(timeFocus, Crystal.Time, Crystal.Glittering);
        Fill(timeFocus);
        var rewound = timeFocus?.WorkOnce();
        float meat = TransitionHours(ChestSlot(meatChest, 0)?.Itemstack, EnumTransitionType.Perish);
        if (ChestSlot(meatChest, 0) is { Itemstack: { } stack } slot)
        {
            stack.Collectible.SetTransitionState(stack, EnumTransitionType.Perish, 5);
            slot.MarkDirty();
        }
        float before = Total(timeFocus);
        var fresh = timeFocus?.WorkOnce();
        float paid = before - Total(timeFocus);

        var cheeseChest = origin.AddCopy(11, 0, 6);
        Chest(cheeseChest, Aged("game:rawcheese-salted", EnumTransitionType.Ripen, 100));
        var rustFocus = PlaceMachine(cheeseChest.AddCopy(-3, 0, 0), Machines.Machine.Focus, "east");
        Put(rustFocus, Crystal.Rust, Crystal.Glittering);
        Fill(rustFocus);
        var moved = rustFocus?.WorkOnce();
        float ripen = TransitionHours(ChestSlot(cheeseChest, 0)?.Itemstack, EnumTransitionType.Ripen);
        var notDue = rustFocus?.WorkOnce();
        // A Death Focus over flowers, a chest touching it: the wilted flowers go into the chest.
        var flowers = origin.AddCopy(11, 0, 1);
        int flowerId = World.Blocks.FirstOrDefault(b => b.Code?.Domain == "game" && b.Code.Path.StartsWith("flower-") && b.Code.Path.EndsWith("-free"))?.Id ?? 0;
        Accessor.SetBlock(Id("game:soil-medium-none"), flowers.DownCopy());
        Accessor.SetBlock(flowerId, flowers);
        var deathFocus = PlaceMachine(flowers.AddCopy(0, 3, 0), Machines.Machine.Focus, "down");
        var deathChest = flowers.AddCopy(1, 3, 0);
        Chest(deathChest);
        Put(deathFocus, Crystal.Death, Crystal.Glittering);
        Fill(deathFocus);
        var withered = deathFocus?.WorkOnce();
        int collected = Accessor.GetBlockEntity(deathChest) is BlockEntityGenericTypedContainer dc ? dc.Inventory.Sum(s => s.StackSize) : -1;
        logger.Notification("Mysteries test: a Death Focus {0}, the flower is {1}, {2} item(s) in the chest touching it",
            withered, Accessor.GetBlock(flowers).Id == 0 ? "gone" : Path(flowers), collected);

        logger.Notification("Mysteries test: a Time Focus {0}, the meat from 30 to {1:0.#} hours; at 5 hours it {2}, paid {3:0.#}; a Rust Focus {4}, the cheese from 100 to {5:0.#} hours, then {6}",
            rewound, meat, fresh, paid, moved, ripen, notDue);
    }
}
