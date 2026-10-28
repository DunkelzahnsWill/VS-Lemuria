using System.Linq;
using Lemuria.Crystals;
using Lemuria.Sparkstone;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace Lemuria.Machines;

/// <summary>
/// Memory, a module (step 4 part 2 note, How each piece behaves; G5): Awakened sparkstone set against
/// any face of a Focus or Scatter but its front, one to a machine, holding a magazine of 2 crystals
/// in Awakened stone. After each turn, the crystal that just worked moves to the bottom of the magazine
/// and the first one that fits cycles into its place; a Dormant crystal is passed over, and turning
/// costs nothing. Code <c>memory-awakened-{rock}-{side}</c>, its magazine on the {side} face. It is
/// sparkstone in its network like any other block, with no capacitors.
/// </summary>
public class BlockEntityMemory : BlockEntitySparkstone
{
    public new const string ClassCode = "lemuria-memory";

    /// <summary>Crystals a magazine holds in Awakened stone (the note: 2, then 3 in Ascendant and 4 in Sovereign).</summary>
    public const int Magazine = 2;

    public BlockFacing Side => BlockFacing.FromCode(Block?.Variant["side"]) ?? BlockFacing.NORTH;

    public override bool IsFree => false;

    public override bool StoresInSockets => false;

    public override BlockFacing? SocketFacing => Side;

    /// <summary>Each crystal takes a row of two marks, whatever its Evolution, so the magazine counts crystals.</summary>
    public override int SocketRoom => Magazine * 2;

    public override int SizeOf(SocketCrystal crystal) => 2;

    public override bool CanWork(SocketCrystal crystal) => true;

    /// <summary>
    /// The machine's crystal that just worked goes to the bottom of the magazine and the first crystal
    /// awake in it that fits the machine takes its place. Returns whether they swapped. Server only.
    /// </summary>
    public bool Rotate(BlockEntityMachine machine, SocketCrystal worked)
    {
        if (!Active || worked.Dormant || worked.IsFitting || !machine.Crystals.Contains(worked))
        {
            return false;
        }
        var waiting = sockets.Where(c => !c.Dormant).ToList();
        if (waiting.Count == 0)
        {
            return false;
        }
        var face = machine.SocketFacing ?? machine.Front;
        var outgoing = machine.TakeOut(worked);
        foreach (var next in waiting)
        {
            var incoming = TakeOut(next);
            if (machine.Insert(incoming, face))
            {
                if (!Insert(outgoing, Side))
                {
                    // Cannot happen with a crystal just taken out; drop nothing, put it back where it was.
                    machine.Insert(outgoing, face);
                }
                return true;
            }
            // It does not fit the machine's room: back to the top of the magazine, and try the next.
            if (Insert(incoming, Side))
            {
                var back = sockets[^1];
                sockets.RemoveAt(sockets.Count - 1);
                sockets.Insert(0, back);
                OnSocketsChanged();
            }
        }
        machine.Insert(outgoing, face);
        return false;
    }

    protected override MeshData? BuildMesh(ICoreClientAPI capi, ITesselatorAPI tesselator, int shown)
    {
        var shape = Shape.TryGet(capi, Block.Shape.Base.Clone().WithPathPrefixOnce("shapes/").WithPathAppendixOnce(".json"));
        if (shape == null)
        {
            return null;
        }
        var source = tesselator.GetTextureSource(Block);
        tesselator.TesselateShape("lemuria-memory", shape, out var stone, source, null, 0, 0, 0, null, new[] { "stone" });
        var glyphs = new GlyphTextures(source, GlyphSuffix(shown));
        tesselator.TesselateShape("lemuria-memory", shape, out var overlay, glyphs, null, 0, 0, 0, null, new[] { "glyphs", "front" });
        if (stone == null || overlay == null)
        {
            return stone ?? overlay;
        }
        CrystalMeshes.SetGlow(overlay, GlyphGlowAt(shown));
        stone.AddMeshData(overlay);
        var (rx, ry, rz) = BlockEntityMachine.FrontTurn(Side);
        return stone.Rotate(new Vec3f(0.5f, 0.5f, 0.5f), rx, ry, rz);
    }
}
