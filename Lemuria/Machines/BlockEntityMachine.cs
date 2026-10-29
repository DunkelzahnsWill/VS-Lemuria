using System;
using System.Collections.Generic;
using System.Linq;
using Lemuria.Core;
using Lemuria.Crystals;
using Lemuria.Sparkstone;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;

namespace Lemuria.Machines;

/// <summary>
/// An Awakened block carved into a machine (design note "Lemuria step 4: Machines"): still sparkstone,
/// in its network with its 50 share, with its four sockets on its front. Its form and front are in its
/// block code; its crystals, Active or Dormant, a refining's progress and who carved it are kept here.
/// What touches it, an Amplify, a container, is read only when it or a neighbour is placed or removed
/// and on a Crystal Rod tap (Multiplayer and cost: Context). Server-authoritative; clients get the
/// sockets and the glyphs through the game's block entity sync.
/// </summary>
public class BlockEntityMachine : BlockEntitySparkstone
{
    public new const string ClassCode = "lemuria-machine";

    /// <summary>How often the server looks at the machine, in milliseconds; each verb keeps its own pace.</summary>
    private const int TickMs = 500;

    private const string RefineKey = "lemuriaRefineHours";

    private static readonly AssetLocation ChimeSound = new(LemuriaModSystem.ModId, "sounds/crystals/chime");

    private string? carverUid;
    private string? carverName;

    // Context, read on placement, a neighbour's change and a rod tap; never polled.
    private bool contextDirty = true;
    private BlockPos? amplifierPos;
    private BlockPos? containerPos;
    private BlockPos? shelfPos;
    private BlockPos? memoryPos;
    private bool overflowed;

    // Pace, not saved: an unloaded machine pauses and does not catch up.
    private double workSeconds;
    private int turn;
    private double sinceGather;
    private double sinceRefine;
    private double sinceMotor;
    private double lastGatherHours = -1;
    private double lastRefineHours = -1;
    private double lastMotorHours = -1;
    private float gathered;
    private double skyBoostUntil;
    private bool motorPowered;

    public string Form => Block?.Variant["form"] ?? Machine.Gather;

    public BlockFacing Front => BlockFacing.FromCode(Block?.Variant["side"]) ?? BlockFacing.NORTH;

    public override bool IsFree => false;

    /// <summary>
    /// A Motor stores no charge of its own: its aligned crystals are its store, capacitors as in Free
    /// stone, so it runs on them when cut off and stops when they are spent (note rev 102, The Crystal Motor).
    /// </summary>
    public override bool StoresInSockets => Form == Machine.Motor;

    protected override float OwnCapacity => Form == Machine.Motor ? 0 : base.OwnCapacity;

    /// <summary>The face carved with the glyph and the sockets: the front, or a Motor's rear.</summary>
    public BlockFacing GlyphFace => Machine.GlyphFace(Form, Front);

    public override BlockFacing? SocketFacing => GlyphFace;

    public string? CarverUid => carverUid;

    public string CarverName => carverName ?? "a machine";

    /// <summary>Whether a Crystal Motor's network paid for its turning on its last look. Server only.</summary>
    public bool MotorPowered => motorPowered && Active;

    /// <summary>The crystals that take their turn: every one not Dormant.</summary>
    public List<SocketCrystal> Working => sockets.Where(c => !c.Dormant).ToList();

    public override void Initialize(ICoreAPI api)
    {
        base.Initialize(api);
        if (api.Side == EnumAppSide.Server)
        {
            RegisterGameTickListener(OnServerTick, TickMs, api.World.Rand.Next(TickMs));
        }
    }

    public void SetCarver(IPlayer? player)
    {
        if (player == null)
        {
            return;
        }
        carverUid = player.PlayerUID;
        carverName = player.PlayerName;
        MarkDirty(false);
    }

    /// <summary>Reads what touches the machine again on its next look.</summary>
    public void MarkContextDirty() => contextDirty = true;

    /// <summary>
    /// The Amplify block, the container, and for a Focus or Scatter its Control Shelf and Memory touching
    /// the machine, face to face (step 4 part 2: a shelf's back on a side, never the front; Memory on any
    /// face but the front; one of each).
    /// </summary>
    public void ReadContext()
    {
        contextDirty = false;
        amplifierPos = null;
        containerPos = null;
        shelfPos = null;
        memoryPos = null;
        bool works = Form is Machine.Focus or Machine.Scatter;
        var accessor = Api.World.BlockAccessor;
        foreach (var face in BlockFacing.ALLFACES)
        {
            var pos = Pos.AddCopy(face);
            var be = accessor.GetBlockEntity(pos);
            if (be is BlockEntityMachine { Form: Machine.Amplify } && amplifierPos == null && Form != Machine.Amplify)
            {
                amplifierPos = pos;
            }
            else if (be is BlockEntityControlShelf shelf)
            {
                if (works && shelfPos == null && face.IsHorizontal && face != Front && shelf.Side == face)
                {
                    shelfPos = pos;
                }
            }
            else if (be is BlockEntityMemory)
            {
                if (works && memoryPos == null && face != Front)
                {
                    memoryPos = pos;
                }
            }
            else if (be is IBlockEntityContainer and not BlockEntitySparkstone && containerPos == null)
            {
                containerPos = pos;
            }
        }
    }

    /// <summary>The Control Shelf set against this Focus or Scatter, or null.</summary>
    public BlockEntityControlShelf? Shelf => shelfPos == null ? null : Api.World.BlockAccessor.GetBlockEntity(shelfPos) as BlockEntityControlShelf;

    /// <summary>The Memory set against this Focus or Scatter, or null.</summary>
    public BlockEntityMemory? Memory => memoryPos == null ? null : Api.World.BlockAccessor.GetBlockEntity(memoryPos) as BlockEntityMemory;

    /// <summary>Where stock comes from: the container on top of the shelf; none without a shelf.</summary>
    public IInventory? Input => Shelf?.Input?.Inventory;

    /// <summary>
    /// Where yields go (G2): the container under the shelf; with no shelf, any container touching the
    /// machine, as in part 1. Null for nowhere, when yields drop where the work happened.
    /// </summary>
    public IInventory? Output
    {
        get
        {
            if (shelfPos != null)
            {
                return Shelf?.Output?.Inventory;
            }
            return containerPos != null && Api.World.BlockAccessor.GetBlockEntity(containerPos) is IBlockEntityContainer container ? container.Inventory : null;
        }
    }

    /// <summary>The output's block position, for a liquid container's own calls; null without one.</summary>
    public BlockPos? OutputPos => shelfPos != null ? (Shelf?.Output != null ? Shelf.OutputPos : null) : containerPos;

    /// <summary>Whether the shelf itself uses up this stack as stock (G2: a product it uses goes back to the input).</summary>
    public bool ShelfUses(ItemStack stack) => Shelf is { } shelf && ShelfWork.UsesUp(Api.World, shelf, stack);

    /// <summary>
    /// Whether these yields all fit where they would go: stock the shelf uses into the input if it has
    /// room, the rest into the output. With no output they drop, which always fits.
    /// </summary>
    public bool CanYield(IEnumerable<ItemStack> stacks)
    {
        var merged = Stock.Merge(Api.World, stacks);
        var input = Input;
        var output = Output;
        var toOutput = new List<ItemStack>();
        var toInput = new List<ItemStack>();
        foreach (var stack in merged)
        {
            if (input != null && ShelfUses(stack) && Stock.Fits(Api.World, input, toInput.Append(stack)))
            {
                toInput.Add(stack);
            }
            else
            {
                toOutput.Add(stack);
            }
        }
        return toOutput.Count == 0 || output == null || Stock.Fits(Api.World, output, toOutput);
    }

    /// <summary>Puts one yield where it goes, the stock the shelf uses into the input first; returns what fit nowhere, or null.</summary>
    public ItemStack? Route(ItemStack stack)
    {
        ItemStack? left = stack;
        if (Input is { } input && ShelfUses(stack))
        {
            left = Stock.Put(Api.World, input, stack);
        }
        return left == null ? null : Collect(left);
    }

    /// <summary>Puts yields where they go (see <see cref="CanYield"/>); what fits nowhere drops at the point given.</summary>
    public void Yield(IEnumerable<ItemStack> stacks, Vec3d dropAt)
    {
        foreach (var stack in Stock.Merge(Api.World, stacks))
        {
            var left = Route(stack);
            if (left != null && left.StackSize > 0)
            {
                Api.World.SpawnItemEntity(left, dropAt);
            }
        }
    }

    /// <summary>
    /// The highest Evolution an Amplify touching the machine lifts, by the crystals working in it: 1 for
    /// Glittering, 2 for Glowing, 0 for none or a Dormant one. One Amplify is the most a machine takes.
    /// </summary>
    public int AmplifyRank()
    {
        if (amplifierPos == null || Api.World.BlockAccessor.GetBlockEntity(amplifierPos) is not BlockEntityMachine { Form: Machine.Amplify, Active: true } amp)
        {
            return 0;
        }
        return amp.Working.Select(c => Crystal.Rank(c.Evolution)).DefaultIfEmpty(0).Max();
    }

    /// <summary>
    /// Puts a yield into the output; returns what did not fit, or null. A yield that did not fit an output
    /// sends the machine Dormant once its working is done (question 7), as one that a working could not
    /// test beforehand, such as a part 1 harvest, overflows.
    /// </summary>
    public ItemStack? Collect(ItemStack stack)
    {
        if (Output is not { } output)
        {
            return stack;
        }
        var left = Stock.Put(Api.World, output, stack);
        if (left != null)
        {
            overflowed = true;
        }
        return left;
    }

    /// <summary>
    /// The claim rule (note, Claims, creatures and players): with RespectClaims on, a machine changes
    /// nothing inside a land claim unless it stands inside that same claim.
    /// </summary>
    public bool MayChange(BlockPos pos)
    {
        if (!LemuriaClaims.Respect)
        {
            return true;
        }
        var claims = Api.World.Claims.Get(pos);
        return claims == null || claims.Length == 0 || claims.Any(c => c.PositionInside(Pos));
    }

    // ------------------------------------------------------------------ the server's look

    private void OnServerTick(float dt)
    {
        if (contextDirty)
        {
            ReadContext();
        }
        if (!Active)
        {
            motorPowered = false;
            return;
        }
        switch (Form)
        {
            case Machine.Gather:
                if ((sinceGather += dt) >= CrystalCharging.ReadIntervalMs / 1000.0)
                {
                    sinceGather = 0;
                    GatherNow();
                }
                break;
            case Machine.Refine:
                if ((sinceRefine += dt) >= SparkstoneNetworks.TickMs / 1000.0)
                {
                    sinceRefine = 0;
                    RefineNow();
                }
                break;
            case Machine.Focus:
            case Machine.Scatter:
                int count = Working.Count;
                if (count == 0)
                {
                    workSeconds = 0;
                    break;
                }
                // Each crystal works once every MachineWorkSeconds, the crystals taking turns.
                workSeconds = Math.Min(workSeconds + dt * count, Machine.WorkSeconds * 2);
                if (workSeconds >= Machine.WorkSeconds)
                {
                    workSeconds -= Machine.WorkSeconds;
                    WorkOnce();
                }
                break;
            case Machine.Motor:
                if ((sinceMotor += dt) >= SparkstoneNetworks.TickMs / 1000.0 || lastMotorHours < 0)
                {
                    sinceMotor = 0;
                    PayMotor();
                }
                break;
        }
    }

    // ------------------------------------------------------------------ Focus and Scatter

    /// <summary>The next working crystal in turn does one working. Returns what came of it; public for the test command.</summary>
    public MachineWork.Result WorkOnce()
    {
        var working = Working;
        if (working.Count == 0 || !Active || (Form != Machine.Focus && Form != Machine.Scatter))
        {
            return MachineWork.Result.Idle;
        }
        if (contextDirty)
        {
            ReadContext();
        }
        var crystal = working[turn % working.Count];
        overflowed = false;
        var result = MachineWork.Work(this, crystal);
        if (result == MachineWork.Result.Full || overflowed)
        {
            // An output with no room for the yield: Dormant, drawing and checking nothing, until a rod tap (question 7).
            GoDormantFull();
            if (result != MachineWork.Result.Full)
            {
                result = MachineWork.Result.Full;
            }
        }
        // Memory cycles the crystal that just took its turn out and the next one in; the one in its place works next.
        if (Memory is not { } memory || !memory.Rotate(this, crystal))
        {
            turn++;
        }
        return result;
    }

    /// <summary>Whether the machine last went Dormant because its output was full; a rod tap clears it.</summary>
    public bool StoppedFull { get; private set; }

    private static readonly AssetLocation FullSound = new(LemuriaModSystem.ModId, "sounds/machines/switch-off");

    private void GoDormantFull()
    {
        StoppedFull = true;
        SetActive(false);
        Api.World.PlaySoundAt(FullSound, Pos.X + 0.5, Pos.Y + 0.5, Pos.Z + 0.5, null, true, 16f, LemuriaSound.Volume(0.6f));
    }

    /// <summary>The crystal that works next, for the test command.</summary>
    public void ResetTurn() => turn = 0;

    // ------------------------------------------------------------------ Gather

    /// <summary>
    /// Each aligned crystal gathers from its alignment's environment, read over the same 5×5×5 cube and
    /// purity test as a standing crystal, straight into the network: 200 a day at Glittering and 600 at
    /// Glowing at full strength (note, Gather). Public for the test command, which gives the hours.
    /// </summary>
    public float GatherNow(double? forHours = null)
    {
        double now = Api.World.Calendar.TotalHours;
        double hours = forHours ?? (lastGatherHours < 0 ? 0 : Math.Max(0, now - lastGatherHours));
        lastGatherHours = now;
        float before = gathered;
        foreach (var crystal in Working.Where(c => c.IsAligned))
        {
            float strength = CrystalCharging.Strength(Api.World, Pos, crystal.Alignment!, crystal.Evolution, crystal.Alignment == Crystal.Sky && now < skyBoostUntil);
            gathered += (float)(Machine.GatherPerDay(crystal.Evolution) * strength * hours / Api.World.Calendar.HoursPerDay);
        }
        if (gathered > before)
        {
            Sparks(Working.First(c => c.IsAligned).Alignment!, 3);
        }
        return gathered - before;
    }

    public override float TakeGathered(float room)
    {
        // What the network has no room for is lost.
        float taken = Math.Min(room, gathered);
        gathered = 0;
        return taken;
    }

    public override void OnSocketLightning(SocketCrystal crystal)
    {
        if (Form == Machine.Gather && crystal.Alignment == Crystal.Sky)
        {
            skyBoostUntil = Api.World.Calendar.TotalHours + CrystalCharging.LightningHours;
        }
    }

    // ------------------------------------------------------------------ Refine

    /// <summary>
    /// The aligned crystals are the pattern; every Unaligned crystal of its Evolution or lower turns its
    /// alignment, one for one, at a cradle's full-strength pace, paying half the new crystal's capacity
    /// as it goes; it stalls while the network is empty (note, Refine). Public for the test command.
    /// </summary>
    public int RefineNow(double? forHours = null)
    {
        double now = Api.World.Calendar.TotalHours;
        double hours = forHours ?? (lastRefineHours < 0 ? 0 : Math.Max(0, now - lastRefineHours));
        lastRefineHours = now;
        var working = Working;
        var patterns = working.Where(c => c.IsAligned).ToList();
        if (hours <= 0 || patterns.Count == 0 || patterns.Select(c => c.Alignment).Distinct().Count() > 1)
        {
            return 0;
        }
        string alignment = patterns[0].Alignment!;
        int rank = patterns.Max(c => Crystal.Rank(c.Evolution));
        var networks = SparkstoneModSystem.Networks;
        int done = 0;
        bool changed = false;
        foreach (var crystal in working.Where(c => !c.IsAligned && Crystal.Rank(c.Evolution) <= rank))
        {
            double need = Machine.RefineDays(crystal.Evolution) * Api.World.Calendar.HoursPerDay;
            double progress = crystal.Stack.Attributes.GetDouble(RefineKey);
            double step = Math.Min(hours, need - progress);
            float cost = (float)(Machine.RefineCost(crystal.Evolution) * step / need);
            if (step <= 0 || networks == null || !networks.TryPay(this, cost))
            {
                continue;
            }
            progress += step;
            changed = true;
            Sparks(alignment, 2);
            if (progress < need - 0.0001)
            {
                crystal.Stack.Attributes.SetDouble(RefineKey, progress);
                continue;
            }
            var block = Crystal.BlockFor(Api.World, alignment, crystal.Evolution);
            if (block == null)
            {
                continue;
            }
            int cracks = crystal.Cracks;
            crystal.Stack = new ItemStack(block);
            Crystal.SetState(crystal.Stack, 0, cracks);
            CrystalsModSystem.Standing?.Add(crystal);
            Api.World.PlaySoundAt(ChimeSound, Pos.X + 0.5, Pos.Y + 0.5, Pos.Z + 0.5, null, true, 16f, LemuriaSound.Volume(0.8f));
            done++;
        }
        if (done > 0)
        {
            OnSocketsChanged();
        }
        else if (changed)
        {
            MarkDirty(false);
        }
        return done;
    }

    /// <summary>A refining's progress on a crystal, 0 to 1.</summary>
    public double RefineProgress(SocketCrystal crystal) =>
        crystal.Stack.Attributes.GetDouble(RefineKey) / (Machine.RefineDays(crystal.Evolution) * Api.World.Calendar.HoursPerDay);

    public override ItemStack TakeOut(SocketCrystal crystal)
    {
        var stack = base.TakeOut(crystal);
        stack.Attributes.RemoveAttribute(RefineKey);
        return stack;
    }

    // ------------------------------------------------------------------ Crystal Motor

    /// <summary>
    /// The Motor's torque from its crystals, each lifted by an Amplify that reaches its Evolution, while
    /// its network pays (note, The Crystal Motor). Server only; the motor behaviour reads it.
    /// </summary>
    public float MotorTorque()
    {
        int amp = AmplifyRank();
        return Working.Sum(c => (c.IsFitting ? Machine.DeadSocketTorque : Machine.MotorTorque(c.Evolution)) * (amp >= Crystal.Rank(c.Evolution) ? 2f : 1f));
    }

    /// <summary>Pays the Motor's charge for the hours since the last look; it turns only while that is paid. Public for the test command.</summary>
    public bool PayMotor(double? forHours = null)
    {
        bool was = motorPowered;
        double now = Api.World.Calendar.TotalHours;
        double hours = forHours ?? (lastMotorHours < 0 ? 0 : Math.Max(0, now - lastMotorHours));
        lastMotorHours = now;
        var working = Working;
        if (!Active || working.Count == 0 || SparkstoneModSystem.Networks is not { } networks)
        {
            motorPowered = false;
        }
        else
        {
            int amp = AmplifyRank();
            double cost = working.Sum(c => (c.IsFitting ? Machine.DeadSocketChargePerHour : Machine.MotorChargePerHour(c.Evolution)) * (amp >= Crystal.Rank(c.Evolution) ? Machine.AmplifyCost : 1)) * hours;
            // A line that holds nothing never pays, however short the time (the network's own rounding
            // would let an empty line "pay" 0.0001): a Dead Socket has nothing else to run on.
            motorPowered = networks.CanPay(this, Math.Max((float)cost, 0.001f)) && (hours <= 0 || networks.TryPay(this, (float)cost));
        }
        if (motorPowered != was)
        {
            // The clients' rotors read it from the block entity.
            MarkDirty();
        }
        return motorPowered;
    }

    /// <summary>A crystal set in, taken out, switched Dormant or woken changes a Motor's power at once, not on its next ten-second look.</summary>
    public override void OnSocketsChanged()
    {
        base.OnSocketsChanged();
        if (Form == Machine.Motor && Api?.Side == EnumAppSide.Server)
        {
            PayMotor();
        }
    }

    // ------------------------------------------------------------------ show

    /// <summary>A Crystal Motor also takes a Dead Socket, a fitting; every other machine takes crystals only (note, The Dead Socket).</summary>
    public override bool Accepts(ItemStack stack) => Form == Machine.Motor ? Socketable(stack) : base.Accepts(stack);

    /// <summary>A Motor looks at once whether its line still pays when a block in it is switched (play-test, Oct 29).</summary>
    protected override void OnNetworkChanged()
    {
        if (Form == Machine.Motor && Api?.Side == EnumAppSide.Server)
        {
            PayMotor();
        }
    }

    protected override void OnActiveChanged()
    {
        if (Active)
        {
            StoppedFull = false;
        }
        if (!Active)
        {
            motorPowered = false;
        }
        else if (Form == Machine.Motor && Api?.Side == EnumAppSide.Server)
        {
            // Switched Active, a Motor starts at once rather than on its next look.
            PayMotor();
        }
        workSeconds = 0;
    }

    /// <summary>Faint sparks of a colour at the front, as the machine gathers or refines; the server's particles reach the players nearby.</summary>
    private void Sparks(string alignment, int count) =>
        CrystalCharging.Sparks(Api.World, Machine.FrontPoint(Pos, Front), Crystal.Rgb(alignment), count, 0.3f);

    public override void ToTreeAttributes(ITreeAttribute tree)
    {
        base.ToTreeAttributes(tree);
        tree.SetBool("stoppedFull", StoppedFull);
        if (carverUid != null)
        {
            tree.SetString("carverUid", carverUid);
            tree.SetString("carverName", carverName ?? "");
        }
    }

    public override void FromTreeAttributes(ITreeAttribute tree, IWorldAccessor worldForResolving)
    {
        base.FromTreeAttributes(tree, worldForResolving);
        carverUid = tree.GetString("carverUid");
        carverName = tree.GetString("carverName");
        StoppedFull = tree.GetBool("stoppedFull");
        contextDirty = true;
        if (Api?.Side == EnumAppSide.Client && Form == Machine.Motor)
        {
            // The Motor's behaviour sends whether the line pays; the glyphs follow it.
            bool was = motorPowered;
            motorPowered = tree.GetBool("motorPowered");
            if (was != motorPowered)
            {
                MarkDirty(true);
            }
        }
    }

    /// <summary>
    /// A Motor that turns lights its glyphs, at least to the lowest level, even on a line too low to show
    /// one: a Dead Socket stores nothing, so its Motor's own level is the line's (play-test, Oct 28).
    /// </summary>
    protected override int ShownLevel => Form == Machine.Motor && MotorPowered ? Math.Max(base.ShownLevel, 1) : base.ShownLevel;

    protected override MeshData? BuildMesh(ICoreClientAPI capi, ITesselatorAPI tesselator, int shown)
    {
        var shape = Shape.TryGet(capi, Block.Shape.Base.Clone().WithPathPrefixOnce("shapes/").WithPathAppendixOnce(".json"));
        if (shape == null)
        {
            return null;
        }
        var source = tesselator.GetTextureSource(Block);
        tesselator.TesselateShape("lemuria-machine", shape, out var stone, source, null, 0, 0, 0, null, new[] { "stone" });
        var glyphs = new GlyphTextures(source, GlyphSuffix(shown));
        tesselator.TesselateShape("lemuria-machine", shape, out var overlay, glyphs, null, 0, 0, 0, null, new[] { "glyphs", "front" });
        if (stone == null || overlay == null)
        {
            return stone ?? overlay;
        }
        CrystalMeshes.SetGlow(overlay, GlyphGlowAt(shown));
        stone.AddMeshData(overlay);
        var (rx, ry, rz) = FrontTurn(GlyphFace);
        return stone.Rotate(new Vec3f(0.5f, 0.5f, 0.5f), rx, ry, rz);
    }

    /// <summary>The turn, in radians about X, Y and Z (MeshData.Rotate), that faces a north-facing shape toward this front.</summary>
    public static (float X, float Y, float Z) FrontTurn(BlockFacing front) => front.Code switch
    {
        "east" => (0, -GameMath.PIHALF, 0),
        "south" => (0, GameMath.PI, 0),
        "west" => (0, GameMath.PIHALF, 0),
        "up" => (GameMath.PIHALF, 0, 0),
        "down" => (-GameMath.PIHALF, 0, 0),
        _ => (0, 0, 0),
    };

    /// <summary>
    /// What a crystal can do here, for its plug's light: Gather gathers only with aligned crystals, and a
    /// Glittering crystal has no power to give up in a Scatter; every other verb uses any crystal.
    /// </summary>
    public override bool CanWork(SocketCrystal crystal) => Form switch
    {
        _ when crystal.IsFitting => Form == Machine.Motor,
        Machine.Gather => crystal.IsAligned,
        Machine.Scatter => Crystal.Rank(crystal.Evolution) > 1,
        _ => true,
    };

    public override void GetBlockInfo(IPlayer forPlayer, System.Text.StringBuilder dsc)
    {
        base.GetBlockInfo(forPlayer, dsc);
        int dormant = sockets.Count(c => c.Dormant);
        if (StoppedFull && !Active)
        {
            dsc.AppendLine(Vintagestory.API.Config.Lang.Get(LemuriaModSystem.ModId + ":machine-stoppedfull"));
        }
        if (dormant > 0)
        {
            dsc.AppendLine(Vintagestory.API.Config.Lang.Get(LemuriaModSystem.ModId + ":machine-dormantcrystals", dormant));
        }
    }
}
