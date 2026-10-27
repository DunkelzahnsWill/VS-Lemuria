using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace Lemuria.Storage;

/// <summary>
/// The crystal rack: a stone wine rack with sixteen cells in four rows of four, every other row shifted
/// half a cell, where crystals lie lengthwise pointing into the wall. Each cell is its own selection
/// box, so a right-click knows which place it means; the last box is the back panel.
/// </summary>
public class BlockCrystalRack : Block
{
    public const string ClassCode = "lemuria-crystalrack";
    public const int Places = 16;

    /// <summary>The cells' front and back, in pixels from the rack's face; the art script builds the same rack.</summary>
    public const float Front = 9f, Back = 15f;

    private const float RowPitch = 3.85f, Slat = 0.5f, ColumnPitch = 3.2f, Shift = 0.8f;

    private Cuboidf[] boxes = null!;

    public override void OnLoaded(ICoreAPI api)
    {
        base.OnLoaded(api);
        float angle = RotationY(Variant["side"]);
        boxes = new Cuboidf[Places + 1];
        for (int i = 0; i < Places; i++)
        {
            var (x, y) = PlaceCenter(i);
            boxes[i] = new Cuboidf((x - 1.4f) / 16f, (y - 1.6f) / 16f, Front / 16f, (x + 1.4f) / 16f, (y + 1.6f) / 16f, Back / 16f)
                .RotatedCopy(0, angle, 0, new Vec3d(0.5, 0.5, 0.5));
        }
        boxes[Places] = new Cuboidf(0.2f / 16f, 0, Back / 16f, 15.8f / 16f, 1f, 1f).RotatedCopy(0, angle, 0, new Vec3d(0.5, 0.5, 0.5));
    }

    /// <summary>The rack's turn about the vertical, as the game's horizontal orientations turn shapes.</summary>
    public static float RotationY(string? side) => side switch
    {
        "east" => 270,
        "south" => 180,
        "west" => 90,
        _ => 0,
    };

    /// <summary>A cell's centre on the unturned rack, in pixels: four rows of four, every other row shifted.</summary>
    public static (float X, float Y) PlaceCenter(int index)
    {
        int row = index / 4;
        int column = index % 4;
        float shift = row % 2 == 0 ? -Shift : Shift;
        return (3.2f + column * ColumnPitch + shift, row * RowPitch + Slat + (RowPitch - Slat) / 2);
    }

    public override Cuboidf[] GetSelectionBoxes(IBlockAccessor blockAccessor, BlockPos pos) => boxes;

    public override bool OnBlockInteractStart(IWorldAccessor world, IPlayer byPlayer, BlockSelection blockSel)
    {
        if (!world.Claims.TryAccess(byPlayer, blockSel.Position, EnumBlockAccessFlags.Use))
        {
            return false;
        }
        if (world.BlockAccessor.GetBlockEntity(blockSel.Position) is BlockEntityCrystalRack rack && rack.OnInteract(byPlayer, blockSel.SelectionBoxIndex))
        {
            return true;
        }
        return base.OnBlockInteractStart(world, byPlayer, blockSel);
    }
}
