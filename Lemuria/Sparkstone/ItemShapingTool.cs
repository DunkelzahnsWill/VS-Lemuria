using System;
using System.Collections.Generic;
using System.Text;
using Lemuria.Core;
using Lemuria.Cradle;
using Lemuria.Crystals;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.MathTools;
using Vintagestory.API.Util;

namespace Lemuria.Sparkstone;

/// <summary>
/// The shaping tool (design note, The shaping tool): a chisel with an Unaligned crystal set in its
/// handle. It holds charge like a Glittering crystal and has no durability. Right-click a charged
/// crystal, placed or resting in a cradle, or any block of a sparkstone network to draw as much as it
/// has room for; hold right-click for a second on a placed polished rock block to awaken it, for
/// AwakenCost. Three glyphs on the blade light one by one with its charge level.
/// </summary>
public class ItemShapingTool : Item
{
    public const string ClassCode = "lemuria-shapingtool";

    /// <summary>The charge level as last set on the server, which the client draws the glyphs and light from.</summary>
    public const string LevelKey = "lemuriaToolLevel";

    private const string MeshCacheKey = LemuriaModSystem.ModId + "-shapingtoolmeshes";
    private const float AwakenSeconds = 1f;

    private static readonly AssetLocation DrawSound = new(LemuriaModSystem.ModId, "sounds/crystals/chime");

    public static float Capacity => (float)Math.Clamp(LemuriaModSystem.Config.ShapingToolCapacity, 1, 100_000);

    public static float Cost => (float)Math.Clamp(LemuriaModSystem.Config.AwakenCost, 0, 100_000);

    public static float GetCharge(ItemStack stack) => Crystal.GetCharge(stack);

    public static int GetLevel(ItemStack stack) => stack.Attributes.GetInt(LevelKey);

    /// <summary>Sets the tool's charge and its level. Server only (the capacity is the server's setting).</summary>
    public static void SetCharge(ItemStack stack, float charge)
    {
        charge = Math.Clamp(charge, 0, Capacity);
        Crystal.SetState(stack, charge, 0);
        stack.Attributes.SetInt(LevelKey, Crystal.Level(charge, Capacity));
    }

    /// <summary>The polished rock block's rock, when the block is the game's own polished rock.</summary>
    public static string? PolishedRock(Block block)
    {
        var code = block.Code;
        if (code == null || code.Domain != "game" || !code.Path.StartsWith("rockpolished-", StringComparison.Ordinal))
        {
            return null;
        }
        return code.Path["rockpolished-".Length..];
    }

    public override void OnHeldInteractStart(ItemSlot slot, EntityAgent byEntity, BlockSelection blockSel, EntitySelection entitySel, bool firstEvent, ref EnumHandHandling handling)
    {
        if (!firstEvent || blockSel == null || slot.Itemstack == null)
        {
            base.OnHeldInteractStart(slot, byEntity, blockSel, entitySel, firstEvent, ref handling);
            return;
        }
        var world = byEntity.World;
        var be = world.BlockAccessor.GetBlockEntity(blockSel.Position);
        if (be is BlockEntityCrystal or BlockEntityCradle or BlockEntitySparkstone)
        {
            handling = EnumHandHandling.PreventDefault;
            if (world.Side == EnumAppSide.Server)
            {
                Draw(slot, be, blockSel.Position);
            }
            return;
        }
        var block = world.BlockAccessor.GetBlock(blockSel.Position);
        if (PolishedRock(block) != null && GetLevel(slot.Itemstack) > Crystal.Empty)
        {
            handling = EnumHandHandling.PreventDefault;
            return;
        }
        base.OnHeldInteractStart(slot, byEntity, blockSel, entitySel, firstEvent, ref handling);
    }

    public override bool OnHeldInteractStep(float secondsUsed, ItemSlot slot, EntityAgent byEntity, BlockSelection blockSel, EntitySelection entitySel)
    {
        if (blockSel == null)
        {
            return false;
        }
        if (byEntity.World.Side == EnumAppSide.Client && secondsUsed > 0.1f && (int)(secondsUsed * 10) % 3 == 0)
        {
            var pos = blockSel.Position.ToVec3d().Add(blockSel.HitPosition);
            CrystalCharging.Sparks(byEntity.World, pos, Crystal.Rgb(Crystal.Unaligned), 1, 0.1f);
        }
        return secondsUsed < AwakenSeconds;
    }

    public override void OnHeldInteractStop(float secondsUsed, ItemSlot slot, EntityAgent byEntity, BlockSelection blockSel, EntitySelection entitySel)
    {
        if (blockSel == null || secondsUsed < AwakenSeconds - 0.05f || byEntity.World.Side != EnumAppSide.Server || slot.Itemstack == null)
        {
            return;
        }
        Awaken(slot, byEntity.World, (byEntity as EntityPlayer)?.Player, blockSel.Position);
    }

    /// <summary>Draws charge from a crystal, a cradle's crystal or a sparkstone network into the tool. Server only.</summary>
    private void Draw(ItemSlot slot, BlockEntity source, BlockPos pos)
    {
        var stack = slot.Itemstack!;
        float room = Capacity - GetCharge(stack);
        if (room <= 0.0001f)
        {
            return;
        }
        float taken = source switch
        {
            BlockEntityCrystal crystal when crystal.IsAligned => crystal.TakeCharge(room),
            BlockEntityCradle cradle => cradle.TakeCharge(room),
            BlockEntitySparkstone stone => SparkstoneModSystem.Networks?.Draw(stone, room) ?? 0,
            _ => 0,
        };
        if (taken <= 0)
        {
            return;
        }
        SetCharge(stack, GetCharge(stack) + taken);
        slot.MarkDirty();
        api.World.PlaySoundAt(DrawSound, pos.X + 0.5, pos.Y + 0.5, pos.Z + 0.5, null, true, 12f, 0.4f);
    }

    /// <summary>Awakens a placed polished rock block into Awakened sparkstone of the same rock, where the player may build. Server only.</summary>
    public bool Awaken(ItemSlot slot, IWorldAccessor world, IPlayer? player, BlockPos pos)
    {
        var stack = slot.Itemstack;
        string? rock = PolishedRock(world.BlockAccessor.GetBlock(pos));
        if (stack == null || rock == null || GetCharge(stack) < Cost)
        {
            return false;
        }
        if (player != null && !world.Claims.TryAccess(player, pos, EnumBlockAccessFlags.BuildOrBreak))
        {
            return false;
        }
        var stone = world.GetBlock(new AssetLocation(LemuriaModSystem.ModId, "sparkstone-awakened-" + rock));
        if (stone == null)
        {
            return false;
        }
        world.BlockAccessor.SetBlock(stone.Id, pos);
        SetCharge(stack, GetCharge(stack) - Cost);
        slot.MarkDirty();
        world.PlaySoundAt(new AssetLocation(LemuriaModSystem.ModId, "sounds/sparkstone/awaken"), pos.X + 0.5, pos.Y + 0.5, pos.Z + 0.5, null, true, 16f, 1f);
        return true;
    }

    /// <summary>A tool holding any charge gives a faint light in the hand.</summary>
    public override byte[] GetLightHsv(IBlockAccessor blockAccessor, BlockPos pos, ItemStack? stack = null)
    {
        if (stack != null && GetLevel(stack) > Crystal.Empty)
        {
            return new byte[] { 10, 1, 3 };
        }
        return base.GetLightHsv(blockAccessor, pos, stack);
    }

    /// <summary>Draws the blade with as many lit glyphs as the charge level: none, one, two or three.</summary>
    public override void OnBeforeRender(ICoreClientAPI capi, ItemStack itemstack, EnumItemRenderTarget target, ref ItemRenderInfo renderinfo)
    {
        int level = Math.Clamp(GetLevel(itemstack), Crystal.Empty, Crystal.Full);
        if (level > Crystal.Empty)
        {
            var refs = ObjectCacheUtil.GetOrCreate(capi, MeshCacheKey, () => new Dictionary<int, MultiTextureMeshRef?>());
            if (!refs.TryGetValue(level, out var meshRef))
            {
                var shape = Vintagestory.API.Common.Shape.TryGet(capi, new AssetLocation(LemuriaModSystem.ModId, $"shapes/item/sparkstone/shapingtool-{level}.json"));
                if (shape != null)
                {
                    capi.Tesselator.TesselateShape(this, shape, out var mesh);
                    meshRef = mesh == null ? null : capi.Render.UploadMultiTextureMesh(mesh);
                }
                refs[level] = meshRef;
            }
            if (meshRef != null)
            {
                renderinfo.ModelRef = meshRef;
            }
        }
        base.OnBeforeRender(capi, itemstack, target, ref renderinfo);
    }

    public override void GetHeldItemInfo(ItemSlot inSlot, StringBuilder dsc, IWorldAccessor world, bool withDebugInfo)
    {
        base.GetHeldItemInfo(inSlot, dsc, world, withDebugInfo);
        if (inSlot.Itemstack != null)
        {
            dsc.AppendLine(Lang.Get($"{LemuriaModSystem.ModId}:shapingtool-charge-{GetLevel(inSlot.Itemstack)}"));
        }
    }

    public override WorldInteraction[] GetHeldInteractionHelp(ItemSlot inSlot)
    {
        return new[]
        {
            new WorldInteraction { ActionLangCode = LemuriaModSystem.ModId + ":shapingtool-draw", MouseButton = EnumMouseButton.Right },
            new WorldInteraction { ActionLangCode = LemuriaModSystem.ModId + ":shapingtool-awaken", MouseButton = EnumMouseButton.Right },
        };
    }
}
