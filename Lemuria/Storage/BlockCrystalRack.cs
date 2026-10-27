using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace Lemuria.Storage;

/// <summary>
/// The crystal rack: a carved stone panel with sixteen places in four rows of four, each row a little
/// offset from the last. Each place is its own selection box, so a right-click knows which place it
/// means; the last box is the panel itself.
/// </summary>
public class BlockCrystalRack : Block
{
    public const string ClassCode = "lemuria-crystalrack";
    public const int Places = 16;

    private Cuboidf[] boxes = null!;

    public override void OnLoaded(ICoreAPI api)
    {
        base.OnLoaded(api);
        float angle = RotationY(Variant["side"]);
        boxes = new Cuboidf[Places + 1];
        for (int i = 0; i < Places; i++)
        {
            var (x, y) = PlaceCenter(i);
            boxes[i] = new Cuboidf((x - 1.5f) / 16f, (y - 1.75f) / 16f, 11f / 16f, (x + 1.5f) / 16f, (y + 1.75f) / 16f, 14f / 16f)
                .RotatedCopy(0, angle, 0, new Vec3d(0.5, 0.5, 0.5));
        }
        boxes[Places] = new Cuboidf(1f / 16f, 0, 14f / 16f, 15f / 16f, 1f, 1f).RotatedCopy(0, angle, 0, new Vec3d(0.5, 0.5, 0.5));
    }

    /// <summary>The rack's turn about the vertical, as the game's horizontal orientations turn shapes.</summary>
    public static float RotationY(string? side) => side switch
    {
        "east" => 270,
        "south" => 180,
        "west" => 90,
        _ => 0,
    };

    /// <summary>A place's centre on the unturned rack, in pixels: four rows of four, every other row shifted.</summary>
    public static (float X, float Y) PlaceCenter(int index)
    {
        int row = index / 4;
        int column = index % 4;
        float shift = row % 2 == 0 ? -0.75f : 0.75f;
        return (3.25f + column * 3.15f + shift, 2.5f + row * 3.6f);
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
