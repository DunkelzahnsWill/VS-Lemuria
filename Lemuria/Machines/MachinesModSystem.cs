using System.Linq;
using Lemuria.Core;
using Lemuria.Crystals;
using Lemuria.Evocations;
using Lemuria.Sparkstone;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;
using Vintagestory.GameContent;

namespace Lemuria.Machines;

/// <summary>
/// Machines (design note "Lemuria step 4: Machines", part 1): the Crystalline Chisel carves Awakened
/// sparkstone into Gather, Refine, Focus, Scatter and Amplify blocks and the Crystal Motor; the Crystal
/// Rod switches blocks Active or Dormant, turns a machine's front and switches its crystals Dormant; an Unaligned
/// crystal in a Focus quickens cradles. Sockets, capacitors and Active and Dormant live on every
/// Awakened block in Sparkstone; a machine's working goes through Evocations' own effects. No Harmony.
/// Adds <c>/lemuria machines test ...</c> for the smoke test and for admins.
/// </summary>
public class MachinesModSystem : ModSystem
{

    public override void Start(ICoreAPI api)
    {
        api.RegisterBlockClass(BlockMachine.ClassCode, typeof(BlockMachine));
        api.RegisterBlockClass(BlockCrystalMotor.ClassCode, typeof(BlockCrystalMotor));
        api.RegisterBlockEntityClass(BlockEntityMachine.ClassCode, typeof(BlockEntityMachine));
        api.RegisterBlockEntityBehaviorClass(BEBehaviorCrystalMotor.Name, typeof(BEBehaviorCrystalMotor));
        api.RegisterBlockBehaviorClass(BlockBehaviorRodMaker.Name, typeof(BlockBehaviorRodMaker));
        api.RegisterItemClass(ItemCrystalRod.ClassCode, typeof(ItemCrystalRod));
        api.RegisterBlockClass(BlockControlShelf.ClassCode, typeof(BlockControlShelf));
        api.RegisterBlockEntityClass(BlockEntityControlShelf.EntityClassCode, typeof(BlockEntityControlShelf));
        api.RegisterBlockEntityClass(BlockEntityMemory.ClassCode, typeof(BlockEntityMemory));
        api.RegisterBlockClass(BlockReclamationPlate.ClassCode, typeof(BlockReclamationPlate));
        ItemCrystalChisel.StoneCarver = new MachineCarver();
        // With the claims switch off, a shelf and the rod's making inside a claim are open to everyone (R9).
        api.Event.OnTestBlockAccess += new MachinesClaims(api).OnTestBlockAccess;
    }

    public override void StartServerSide(ICoreServerAPI api)
    {
        sapi = api;
        Effects.Boiled += BlockReclamationPlate.OnBoiled;
        // A Wind Probe is struck where the game's lightning finally lands, after any rod moved it (step 4 part 3).
        weather = api.ModLoader.GetModSystem<WeatherSystemServer>();
        if (weather != null)
        {
            weather.OnLightningImpactEnd += OnLightningLanded;
        }
        var tests = new MachinesTests(api, Mod.Logger);
        api.Event.SaveGameLoaded += () =>
        {
            int machines = api.World.Blocks.Count(b => Machine.IsMachine(b) && b.Variant["side"] == "north");
            int rodMakers = api.World.Blocks.Count(b => b.HasBehavior<BlockBehaviorRodMaker>() && Crystal.AlignmentOf(b) == Crystal.Unaligned && b.Variant["position"] == "up");
            Mod.Logger.Notification("Machines: {0} machine block(s) facing north; {1} Unaligned crystal kind(s) make the Crystal Rod", machines, rodMakers);
        };
        // A scene reaches up to 16 blocks east and south of its column, so the chunks it reaches load first.
        TextCommandResult Test(int dx, int dz, System.Action<BlockPos> action) =>
            TestSite.WithChunk(api, Mod.Logger, "Machines", dx + 16, dz + 16, _ =>
                TestSite.WithChunk(api, Mod.Logger, "Machines", dx + 16, dz, _ =>
                    TestSite.WithChunk(api, Mod.Logger, "Machines", dx, dz + 16, _ =>
                        TestSite.WithChunk(api, Mod.Logger, "Machines", dx, dz, action))));
        api.ChatCommands.GetOrCreate(LemuriaModSystem.ModId)
            .WithDescription("Lemuria commands")
            .RequiresPrivilege(Privilege.chat)
            .BeginSubCommand("machines")
                .WithDescription("Machine commands")
                .RequiresPrivilege(Privilege.controlserver)
                .BeginSubCommand("test")
                    .WithDescription("Tests machines next to the world spawn")
                    .BeginSubCommand("carve")
                        .WithDescription("Strikes an Awakened block round all its forms, and makes a Crystal Rod")
                        .HandleWith(_ => Test(-20, 20, tests.Carve))
                    .EndSubCommand()
                    .BeginSubCommand("focus")
                        .WithDescription("A Fire Focus lights a firepit, an Ice Focus waters farmland, an Earth Focus raises a 2×2 and goes Dormant")
                        .HandleWith(_ => Test(-20, 30, tests.Focus))
                    .EndSubCommand()
                    .BeginSubCommand("harvest")
                        .WithDescription("A Glowing Sky Focus with an Amplify harvests ripe flax into a chest and replants it")
                        .HandleWith(_ => Test(-20, 44, tests.Harvest))
                    .EndSubCommand()
                    .BeginSubCommand("scatter")
                        .WithDescription("A Scatter tills and waters a 9×9 of soil")
                        .HandleWith(_ => Test(-34, 20, tests.Scatter))
                    .EndSubCommand()
                    .BeginSubCommand("network")
                        .WithDescription("A capacitor raises its network's capacity; a Dormant block splits its network")
                        .HandleWith(_ => Test(-34, 36, tests.Network))
                    .EndSubCommand()
                    .BeginSubCommand("refine")
                        .WithDescription("A Refine block refines three Unaligned crystals to Fire")
                        .HandleWith(_ => Test(-34, 44, tests.Refine))
                    .EndSubCommand()
                    .BeginSubCommand("motor")
                        .WithDescription("Sets a Crystal Motor under a quern and starts it")
                        .HandleWith(_ => Test(-44, 20, tests.Motor))
                    .EndSubCommand()
                    .BeginSubCommand("motorcheck")
                        .WithDescription("Reports whether the test quern turns")
                        .HandleWith(_ => Test(-44, 20, _ => tests.MotorCheck()))
                    .EndSubCommand()
                    .BeginSubCommand("motorstopped")
                        .WithDescription("Reports whether the test quern stopped once its Motor's crystal went Dormant")
                        .HandleWith(_ => Test(-44, 20, _ => tests.MotorStopped()))
                    .EndSubCommand()
                    .BeginSubCommand("quicken")
                        .WithDescription("An Unaligned Focus quickens a seedling in a cradle")
                        .HandleWith(_ => Test(-44, 32, tests.Quicken))
                    .EndSubCommand()
                    .BeginSubCommand("shelf")
                        .WithDescription("A Control Shelf sows a 3×3, picks its first item with work, and sends firewood to its output or back to its input")
                        .HandleWith(_ => Test(-60, 20, tests.ShelfScene))
                    .EndSubCommand()
                    .BeginSubCommand("dig")
                        .WithDescription("A shelf plants a sapling and digs charcoal into its output; a full output sends the machine Dormant")
                        .HandleWith(_ => Test(-60, 36, tests.DigScene))
                    .EndSubCommand()
                    .BeginSubCommand("pit")
                        .WithDescription("One Focus with an axe, shovel, firewood and firestarter builds and lights a roofed charcoal pit from a chest of logs")
                        .HandleWith(_ => Test(-60, 52, tests.Pit))
                    .EndSubCommand()
                    .BeginSubCommand("silofull")
                        .WithDescription("The silo from a logs chest full to the brim")
                        .HandleWith(_ => Test(-122, 52, tests.PitFull))
                    .EndSubCommand()
                    .BeginSubCommand("fell")
                        .WithDescription("A Sky Focus fells a tree into a chest, paying by its logs")
                        .HandleWith(_ => Test(-76, 20, tests.Fell))
                    .EndSubCommand()
                    .BeginSubCommand("memory")
                        .WithDescription("A Memory cycles its crystals through a Focus")
                        .HandleWith(_ => Test(-76, 36, tests.MemoryScene))
                    .EndSubCommand()
                    .BeginSubCommand("plate")
                        .WithDescription("A Fire Focus boils salt water over a reclamation plate, the chance set to 1")
                        .HandleWith(_ => Test(-76, 52, tests.Plate))
                    .EndSubCommand()
                    .BeginSubCommand("bore")
                        .WithDescription("An Earth Focus facing east bores a 2×2 into a chest")
                        .HandleWith(_ => Test(-90, 20, tests.Bore))
                    .EndSubCommand()
                    .BeginSubCommand("deadsocket")
                        .WithDescription("A Dead Socket turns a Crystal Motor on a quern from its line")
                        .HandleWith(_ => Test(-90, 36, tests.DeadSocket))
                    .EndSubCommand()
                    .BeginSubCommand("deadsocketcheck")
                        .WithDescription("Reports whether the Dead Socket's quern turns, then cuts its line")
                        .HandleWith(_ => Test(-90, 36, _ => tests.DeadSocketCheck()))
                    .EndSubCommand()
                    .BeginSubCommand("deadsocketstopped")
                        .WithDescription("Reports whether the Dead Socket's quern stopped once its line was cut")
                        .HandleWith(_ => Test(-90, 36, _ => tests.DeadSocketStopped()))
                    .EndSubCommand()
                    .BeginSubCommand("prefix")
                        .WithDescription("A shelf prefix keeps a shovel to gravel and an axe to oak; the rod switches a shelf item Dormant and back")
                        .HandleWith(_ => Test(-90, 52, tests.Prefix))
                    .EndSubCommand()
                    .BeginSubCommand("quiet")
                        .WithDescription("Seeds go quiet out of stock and wake when seeds arrive; sown out they stay quiet until a sneak tap")
                        .HandleWith(_ => Test(-106, 40, tests.QuietScene))
                    .EndSubCommand()
                    .BeginSubCommand("fertfirst")
                        .WithDescription("Compost before seeds, no socket, only seeds in the input: the compost goes quiet and the seeds sow")
                        .HandleWith(_ => Test(-106, 64, tests.FertiliserFirstScene))
                    .EndSubCommand()
                    .BeginSubCommand("shelfsocket")
                        .WithDescription("A Dead Socket in the shelf's socket: no compost sends the shelf Dormant; compost wakes it to feed, then sow")
                        .HandleWith(_ => Test(-106, 52, tests.ShelfSocketScene))
                    .EndSubCommand()
                    .BeginSubCommand("idle")
                        .WithDescription("Times idle shelf turns: seeds over a sown field, the pit's Focus once it burns, compost over a fed field (run after pit)")
                        .HandleWith(_ => Test(-106, 20, tests.Idle))
                    .EndSubCommand()
                    .BeginSubCommand("firebattery")
                        .WithDescription("A 2×2×1 battery of granite: its crystals name it; as a Fire battery it melts, pays back and cools; a gap rests it")
                        .HandleWith(_ => Test(-138, 20, tests.FireBattery))
                    .EndSubCommand()
                    .BeginSubCommand("icebattery")
                        .WithDescription("A 3×3×1 Ice battery freezes still water from the top and thaws the last frozen first")
                        .HandleWith(_ => Test(-138, 36, tests.IceBattery))
                    .EndSubCommand()
                    .BeginSubCommand("earthbattery")
                        .WithDescription("An Earth battery wears granite to sand into a chest, then feeds the sand")
                        .HandleWith(_ => Test(-138, 52, tests.EarthBattery))
                    .EndSubCommand()
                    .BeginSubCommand("probe")
                        .WithDescription("A Wind Probe on a mast gathers in a set wind, is struck, and gathers nothing roofed")
                        .HandleWith(_ => Test(-154, 20, tests.Probe))
                    .EndSubCommand()
                    .BeginSubCommand("gatherstrike")
                        .WithDescription("One bolt and three Gather blocks: open, roofed after the strike, and roofed as it lands")
                        .HandleWith(_ => Test(-186, 20, tests.GatherStrike))
                    .EndSubCommand()
                    .BeginSubCommand("lights")
                        .WithDescription("A shelf's firestarter relights an extinct torch and waits at a forge with nobody online; what it spares")
                        .HandleWith(_ => Test(-154, 40, tests.Lights))
                    .EndSubCommand()
                    .BeginSubCommand("struckkept")
                        .WithDescription("Reports whether the struck Gather block and the one that heard a death keep their hours, after a restart")
                        .HandleWith(_ => Test(-186, 20, _ => tests.StruckKept()))
                    .EndSubCommand()
                    .BeginSubCommand("batterykept")
                        .WithDescription("Reports whether the Ice battery's stones still keep its count, after a restart")
                        .HandleWith(_ => Test(-138, 36, _ => tests.BatteryKept()))
                    .EndSubCommand()
                    .BeginSubCommand("claims")
                        .WithDescription("Asks the game's claim test of a stranger using a Control Shelf and making a rod, with claims respected and ignored")
                        .HandleWith(_ => Test(-218, 20, tests.Claims))
                    .EndSubCommand()
                    .BeginSubCommand("kept")
                        .WithDescription("Reports the test shelf's items, Memory's crystals and Dead Socket, after a restart")
                        .HandleWith(_ => Test(-60, 20, _ => TestSite.WithChunk(api, Mod.Logger, "Machines", -76, 36, _ => TestSite.WithChunk(api, Mod.Logger, "Machines", -90, 36, _ => tests.Kept()))))
                    .EndSubCommand()
                    .BeginSubCommand("check")
                        .WithDescription("Reports the test blocks' crystals, Dormant block and refining, after a restart")
                        .HandleWith(_ => Test(-34, 36, _ => tests.Check()))
                    .EndSubCommand()
                .EndSubCommand()
            .EndSubCommand();
    }

    private WeatherSystemServer? weather;
    private ICoreServerAPI? sapi;

    /// <summary>
    /// The landing point of the last flash a probe heard. Each flash keeps its own, and the game reports it
    /// on every tick the flash lives, a third of a second; a network feed in between would let one strike
    /// fill a probe twice.
    /// </summary>
    private Vec3d? lastFlash;

    private void OnLightningLanded(ref Vec3d impactPos, ref EnumHandling handling)
    {
        if (sapi != null && !ReferenceEquals(impactPos, lastFlash))
        {
            lastFlash = impactPos;
            BlockEntityMachine.OnLightningLanded(sapi.World, impactPos);
        }
    }

    public override void Dispose()
    {
        if (weather != null)
        {
            weather.OnLightningImpactEnd -= OnLightningLanded;
        }
        Effects.Boiled -= BlockReclamationPlate.OnBoiled;
        ItemCrystalChisel.StoneCarver = null;
    }
}
