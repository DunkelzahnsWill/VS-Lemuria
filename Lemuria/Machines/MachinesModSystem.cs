using System.Linq;
using Lemuria.Core;
using Lemuria.Crystals;
using Lemuria.Sparkstone;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;

namespace Lemuria.Machines;

/// <summary>
/// Machines (design note "Lemuria step 4: Machines", part 1): the Crystalline Chisel carves Awakened
/// sparkstone into Gather, Refine, Focus, Scatter and Amplify blocks and the Crystal Motor; the Crystal
/// Rod switches blocks Active or Dormant, turns a machine's front and rests its crystals; an Unaligned
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
        ItemCrystalChisel.StoneCarver = new MachineCarver();
    }

    public override void StartServerSide(ICoreServerAPI api)
    {
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
                    .BeginSubCommand("quicken")
                        .WithDescription("An Unaligned Focus quickens a seedling in a cradle")
                        .HandleWith(_ => Test(-44, 32, tests.Quicken))
                    .EndSubCommand()
                    .BeginSubCommand("check")
                        .WithDescription("Reports the test blocks' crystals, Dormant block and refining, after a restart")
                        .HandleWith(_ => Test(-34, 36, _ => tests.Check()))
                    .EndSubCommand()
                .EndSubCommand()
            .EndSubCommand();
    }

    public override void Dispose()
    {
        ItemCrystalChisel.StoneCarver = null;
    }
}
