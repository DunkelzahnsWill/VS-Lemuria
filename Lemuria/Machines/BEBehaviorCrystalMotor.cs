using System;
using Lemuria.Core;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent.Mechanics;

namespace Lemuria.Machines;

/// <summary>
/// The Crystal Motor's power, on the game's own rotor (design note "Lemuria step 4: Machines", The
/// Crystal Motor; How it is built): BEBehaviorMPRotor, the base of the windmill and the water wheel.
/// Each crystal gives torque, a Glittering one as much as one length of windmill sails, at a steady
/// speed a little below a windmill's in a strong wind, while the machine is Active and its network
/// pays. The game's rotor joins its network on the side opposite its code and sets its axis only for
/// the four sides, so the Motor joins on its front and sets its axis for up and down itself.
/// Its hub and the crystals on it turn in <see cref="MotorRenderer"/> rather than the game's
/// instanced renderer, so the crystals turn with it.
/// </summary>
public class BEBehaviorCrystalMotor : BEBehaviorMPRotor
{
    public const string Name = "lemuria-crystalmotor";

    private MotorRenderer? renderer;
    private float torque;
    private bool powered;

    public BEBehaviorCrystalMotor(BlockEntity blockentity) : base(blockentity)
    {
        OutFacingForNetworkDiscovery = ownFacing;
    }

    private BlockEntityMachine? Machine => Blockentity as BlockEntityMachine;

    protected override AssetLocation? Sound => null;

    protected override float Resistance => 0.003f;

    protected override double AccelerationFactor => 0.05d;

    protected override float TargetSpeed => Api?.Side == EnumAppSide.Server ? (Machine?.MotorPowered == true ? Lemuria.Machines.Machine.MotorSpeed : 0f) : (powered ? Lemuria.Machines.Machine.MotorSpeed : 0f);

    protected override float TorqueFactor => Api?.Side == EnumAppSide.Server ? (Machine?.MotorPowered == true ? Machine.MotorTorque() : 0f) : (powered ? torque : 0f);

    public override void Initialize(ICoreAPI api, JsonObject properties)
    {
        base.Initialize(api, properties);
        if (ownFacing.IsVertical)
        {
            AxisSign = new[] { 0, 1, 0 };
        }
        if (api is ICoreClientAPI capi)
        {
            renderer = new MotorRenderer(capi, this);
        }
    }

    /// <summary>The hub and crystals are drawn by the Motor's own renderer, not the game's mechanical power renderer.</summary>
    protected override CompositeShape? GetShape() => null;

    /// <summary>The block entity was drawn again: the crystals on the hub may have changed.</summary>
    public void OnSocketsShown() => renderer?.MarkDirty();

    /// <summary>The turn of the hub, which the renderer follows.</summary>
    public float HubAngle => AngleRad;

    public BlockFacing Front => ownFacing;

    public override void ToTreeAttributes(ITreeAttribute tree)
    {
        base.ToTreeAttributes(tree);
        if (Api?.Side == EnumAppSide.Server && Machine != null)
        {
            tree.SetBool("motorPowered", Machine.MotorPowered);
            tree.SetFloat("motorTorque", Machine.MotorTorque());
        }
    }

    public override void FromTreeAttributes(ITreeAttribute tree, IWorldAccessor worldAccessForResolve)
    {
        base.FromTreeAttributes(tree, worldAccessForResolve);
        powered = tree.GetBool("motorPowered");
        torque = tree.GetFloat("motorTorque");
    }

    public override void OnBlockRemoved()
    {
        base.OnBlockRemoved();
        renderer?.Dispose();
        renderer = null;
    }

    public override void OnBlockUnloaded()
    {
        base.OnBlockUnloaded();
        renderer?.Dispose();
        renderer = null;
    }
}

/// <summary>Draws a Crystal Motor's hub and the crystals in its sockets, turning with the shaft. Client only.</summary>
public class MotorRenderer : IRenderer
{
    private readonly ICoreClientAPI capi;
    private readonly BEBehaviorCrystalMotor motor;
    private readonly Matrixf model = new();
    private MeshRef? mesh;
    private bool dirty = true;

    public MotorRenderer(ICoreClientAPI capi, BEBehaviorCrystalMotor motor)
    {
        this.capi = capi;
        this.motor = motor;
        capi.Event.RegisterRenderer(this, EnumRenderStage.Opaque, LemuriaModSystem.ModId + "-motor");
    }

    public double RenderOrder => 0.5;

    public int RenderRange => 32;

    public void MarkDirty() => dirty = true;

    private void Rebuild()
    {
        dirty = false;
        mesh?.Dispose();
        mesh = null;
        var be = motor.Blockentity as BlockEntityMachine;
        var block = motor.Blockentity.Block;
        var shape = Shape.TryGet(capi, new AssetLocation(LemuriaModSystem.ModId, "shapes/block/machines/hub.json"));
        if (be == null || block == null || shape == null)
        {
            return;
        }
        capi.Tesselator.TesselateShape(block, shape, out var hub);
        if (hub == null)
        {
            return;
        }
        var (rx, ry, rz) = BlockEntityMachine.FrontTurn(motor.Front);
        hub.Rotate(new Vec3f(0.5f, 0.5f, 0.5f), rx, ry, rz);
        if (be.SocketMeshes(capi) is { } crystals)
        {
            hub.AddMeshData(crystals);
        }
        mesh = capi.Render.UploadMesh(hub);
    }

    public void OnRenderFrame(float deltaTime, EnumRenderStage stage)
    {
        if (dirty)
        {
            Rebuild();
        }
        if (mesh == null)
        {
            return;
        }
        var pos = motor.Blockentity.Pos;
        var render = capi.Render;
        var camera = capi.World.Player.Entity.CameraPos;
        render.GlToggleBlend(true);
        var prog = render.PreparedStandardShader(pos.X, pos.Y, pos.Z);
        prog.Tex2D = capi.BlockTextureAtlas.AtlasTextures[0].TextureId;
        // The same turn the game's mechanical renderers give a part, so the hub turns with the shaft.
        var axis = motor.AxisSign;
        float angle = motor.HubAngle;
        prog.ModelMatrix = model.Identity()
            .Translate(pos.X - camera.X, pos.Y - camera.Y, pos.Z - camera.Z)
            .Translate(0.5f, 0.5f, 0.5f)
            .Rotate(angle * axis[0], angle * axis[1], angle * axis[2])
            .Translate(-0.5f, -0.5f, -0.5f)
            .Values;
        prog.ViewMatrix = render.CameraMatrixOriginf;
        prog.ProjectionMatrix = render.CurrentProjectionMatrix;
        render.RenderMesh(mesh);
        prog.Stop();
    }

    public void Dispose()
    {
        capi.Event.UnregisterRenderer(this, EnumRenderStage.Opaque);
        mesh?.Dispose();
        mesh = null;
    }
}
