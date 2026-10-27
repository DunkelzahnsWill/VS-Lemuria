using System.Collections.Generic;
using Lemuria.Core;
using Lemuria.Crystals;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;

namespace Lemuria.Storage;

/// <summary>
/// A crystal rack's sixteen places (design note, Crystal jars and racks). Crystals on a rack are
/// stored: they glow faintly by charge level but neither charge, light, hum nor dialogue, up to
/// Glowing. The rack keeps its own inventory and no container tick, so nothing looks for twins here.
/// </summary>
public class BlockEntityCrystalRack : BlockEntity
{
    public const string ClassCode = "lemuria-crystalrack";
    public const string InventoryClass = "lemuriacrystalrack";

    private InventoryGeneric inventory = null!;

    public InventoryGeneric Inventory => inventory;

    public override void Initialize(ICoreAPI api)
    {
        EnsureInventory(api);
        base.Initialize(api);
        inventory.LateInitialize($"{InventoryClass}-{Pos.X}/{Pos.Y}/{Pos.Z}", api);
        inventory.ResolveBlocksOrItems();
    }

    private void EnsureInventory(ICoreAPI? api)
    {
        inventory ??= new InventoryGeneric(BlockCrystalRack.Places, null, api);
    }

    /// <summary>Right-click a place with a crystal to set it there, or with an empty hand to take back the one there.</summary>
    public bool OnInteract(IPlayer player, int place)
    {
        if (place < 0 || place >= BlockCrystalRack.Places)
        {
            return false;
        }
        var slot = player.InventoryManager.ActiveHotbarSlot;
        var target = inventory[place];
        if (target.Empty && slot?.Itemstack != null && Crystal.IsCrystal(slot.Itemstack.Collectible))
        {
            if (Api.Side == EnumAppSide.Server)
            {
                var one = slot.TakeOut(1);
                slot.MarkDirty();
                target.Itemstack = one;
                target.MarkDirty();
                Api.World.PlaySoundAt(new AssetLocation("game:sounds/block/ceramicplace"), Pos, -0.25, null, true, 32f, LemuriaSound.Volume(1f));
                MarkDirty(true);
            }
            return true;
        }
        if (!target.Empty && slot?.Empty != false)
        {
            if (Api.Side == EnumAppSide.Server)
            {
                var stack = target.TakeOutWhole();
                target.MarkDirty();
                if (!player.InventoryManager.TryGiveItemstack(stack, true))
                {
                    Api.World.SpawnItemEntity(stack, Pos.ToVec3d().Add(0.5, 0.5, 0.5));
                }
                Api.World.PlaySoundAt(new AssetLocation("game:sounds/player/collect"), Pos, -0.25, null, true, 32f, LemuriaSound.Volume(1f));
                MarkDirty(true);
            }
            return true;
        }
        return false;
    }

    /// <summary>Puts a crystal on a place directly; for the test command. Server only.</summary>
    public bool Put(int place, ItemStack stack)
    {
        if (!inventory[place].Empty || !Crystal.IsCrystal(stack.Collectible))
        {
            return false;
        }
        inventory[place].Itemstack = stack;
        MarkDirty(true);
        return true;
    }

    public override void OnBlockBroken(IPlayer? byPlayer = null)
    {
        base.OnBlockBroken(byPlayer);
        if (Api.Side != EnumAppSide.Server)
        {
            return;
        }
        foreach (var slot in inventory)
        {
            if (!slot.Empty)
            {
                Api.World.SpawnItemEntity(slot.TakeOutWhole(), Pos.ToVec3d().Add(0.5, 0.5, 0.5));
            }
        }
    }

    public override void ToTreeAttributes(ITreeAttribute tree)
    {
        base.ToTreeAttributes(tree);
        var inv = new TreeAttribute();
        inventory.ToTreeAttributes(inv);
        tree["inventory"] = inv;
    }

    public override void FromTreeAttributes(ITreeAttribute tree, IWorldAccessor worldForResolving)
    {
        base.FromTreeAttributes(tree, worldForResolving);
        EnsureInventory(Api);
        var inv = tree.GetTreeAttribute("inventory");
        if (inv != null)
        {
            inventory.FromTreeAttributes(inv);
            if (Api != null)
            {
                inventory.ResolveBlocksOrItems();
            }
        }
        if (Api?.Side == EnumAppSide.Client)
        {
            MarkDirty(true);
        }
    }

    public override bool OnTesselation(ITerrainMeshPool mesher, ITesselatorAPI tessThreadTesselator)
    {
        var capi = (ICoreClientAPI)Api;
        float angle = BlockCrystalRack.RotationY(Block.Variant["side"]) * GameMath.DEG2RAD;
        var center = new Vec3f(0.5f, 0.5f, 0.5f);
        for (int i = 0; i < BlockCrystalRack.Places; i++)
        {
            var stack = inventory[i].Itemstack;
            if (stack?.Block == null || !Crystal.IsCrystal(stack.Collectible))
            {
                continue;
            }
            var mesh = CrystalMeshes.Get(capi, stack.Block, FaintGlow(stack), Crystal.GetCracks(stack), held: true);
            if (mesh == null)
            {
                continue;
            }
            var (x, y) = BlockCrystalRack.PlaceCenter(i);
            // Lying lengthwise in its cell like a bottle in a wine rack, pointing into the wall.
            float scale = Crystal.EvolutionOf(stack.Collectible) == Crystal.Glowing ? 0.38f : 0.32f;
            var placed = mesh.Clone()
                .Scale(center, scale, scale, scale)
                .Rotate(center, GameMath.PIHALF, 0, 0)
                .Translate(x / 16f - 0.5f, y / 16f - 0.5f, (BlockCrystalRack.Front + BlockCrystalRack.Back) / 32f - 0.5f)
                .Rotate(center, 0, angle, 0);
            mesher.AddMeshData(placed);
        }
        return false;
    }

    /// <summary>A stored crystal glows faintly by its charge level.</summary>
    private static int FaintGlow(ItemStack stack)
    {
        if (!Crystal.IsAligned(stack.Collectible))
        {
            return 50;
        }
        int level = Crystal.Level(Crystal.GetCharge(stack), Crystal.Capacity(Crystal.EvolutionOf(stack.Collectible)));
        return Crystal.Glow(level) / 3;
    }

    public override void GetBlockInfo(IPlayer forPlayer, System.Text.StringBuilder dsc)
    {
        int count = 0;
        foreach (var slot in inventory)
        {
            if (!slot.Empty)
            {
                count++;
            }
        }
        dsc.AppendLine(Vintagestory.API.Config.Lang.Get(LemuriaModSystem.ModId + ":crystalrack-holds", count, BlockCrystalRack.Places));
    }
}
