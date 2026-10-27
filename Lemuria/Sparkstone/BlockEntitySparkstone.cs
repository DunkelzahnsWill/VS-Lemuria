using System;
using System.Collections.Generic;
using Lemuria.Core;
using Lemuria.Crystals;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;
using Vintagestory.API.Util;

namespace Lemuria.Sparkstone;

/// <summary>
/// One block of Awakened sparkstone (design note, Awakened sparkstone and touching charge, Glyphs):
/// it keeps its share of its network's charge, and its glyphs glow by the network's charge level in a
/// warm ivory, without lighting the area. The network itself is worked out every 10 seconds by
/// <see cref="SparkstoneNetworks"/>; clients get the level through the game's block entity sync.
/// </summary>
public class BlockEntitySparkstone : BlockEntity
{
    public const string ClassCode = "lemuria-sparkstone";

    private const string MeshCacheKey = LemuriaModSystem.ModId + "-sparkstonemeshes";

    private static readonly string[] GlyphCodes = { "glyph", "glyph-low", "glyph-high", "glyph-full" };
    private static readonly int[] GlyphGlow = { 0, 90, 170, 255 };

    private float share;
    private int level;

    public float Share => share;
    public int Level => level;
    public float HumStrength => Crystal.Hum(level);

    public override void Initialize(ICoreAPI api)
    {
        base.Initialize(api);
        if (api.Side == EnumAppSide.Server)
        {
            SparkstoneModSystem.Networks?.Add(this);
        }
    }

    /// <summary>Sets this block's share and its network's level; syncs to clients only when something shows. Server only.</summary>
    public void Set(float newShare, int newLevel)
    {
        bool levelChanged = newLevel != level;
        bool shareChanged = Math.Abs(newShare - share) > 0.01f;
        share = Math.Max(0, newShare);
        level = newLevel;
        if (levelChanged)
        {
            MarkDirty(true);
        }
        else if (shareChanged)
        {
            // Saved with the chunk; the client sees no difference until the level changes.
            Api.World.BlockAccessor.GetChunkAtBlockPos(Pos)?.MarkModified();
        }
    }

    public override void OnBlockRemoved()
    {
        base.OnBlockRemoved();
        if (Api.Side == EnumAppSide.Server)
        {
            SparkstoneModSystem.Networks?.Remove(this);
        }
    }

    public override void OnBlockUnloaded()
    {
        base.OnBlockUnloaded();
        if (Api?.Side == EnumAppSide.Server)
        {
            SparkstoneModSystem.Networks?.Remove(this);
        }
    }

    public override void ToTreeAttributes(ITreeAttribute tree)
    {
        base.ToTreeAttributes(tree);
        tree.SetFloat("share", share);
        tree.SetInt("level", level);
    }

    public override void FromTreeAttributes(ITreeAttribute tree, IWorldAccessor worldForResolving)
    {
        base.FromTreeAttributes(tree, worldForResolving);
        int oldLevel = level;
        share = tree.GetFloat("share");
        level = Math.Clamp(tree.GetInt("level"), Crystal.Empty, Crystal.Full);
        if (Api?.Side == EnumAppSide.Client && oldLevel != level)
        {
            MarkDirty(true);
        }
    }

    public override bool OnTesselation(ITerrainMeshPool mesher, ITesselatorAPI tessThreadTesselator)
    {
        var capi = (ICoreClientAPI)Api;
        var meshes = ObjectCacheUtil.GetOrCreate(capi, MeshCacheKey, () => new Dictionary<string, MeshData?>());
        string key = Block.Code + "/" + level;
        MeshData? mesh;
        lock (meshes)
        {
            meshes.TryGetValue(key, out mesh);
        }
        if (mesh == null)
        {
            mesh = BuildMesh(capi, tessThreadTesselator);
            lock (meshes)
            {
                meshes[key] = mesh;
            }
        }
        if (mesh == null)
        {
            return false;
        }
        mesher.AddMeshData(mesh);
        return true;
    }

    /// <summary>The polished stone, and over it the glyph grooves in the level's texture and glow.</summary>
    private MeshData? BuildMesh(ICoreClientAPI capi, ITesselatorAPI tesselator)
    {
        var shape = Shape.TryGet(capi, Block.Shape.Base.Clone().WithPathPrefixOnce("shapes/").WithPathAppendixOnce(".json"));
        if (shape == null)
        {
            return null;
        }
        var source = tesselator.GetTextureSource(Block);
        tesselator.TesselateShape("lemuria-sparkstone", shape, out var stone, source, null, 0, 0, 0, null, new[] { "stone" });
        var glyphs = new GlyphTextures(source, GlyphCodes[level]);
        tesselator.TesselateShape("lemuria-sparkstone", shape, out var overlay, glyphs, null, 0, 0, 0, null, new[] { "glyphs" });
        if (stone == null || overlay == null)
        {
            return stone ?? overlay;
        }
        CrystalMeshes.SetGlow(overlay, GlyphGlow[level]);
        stone.AddMeshData(overlay);
        return stone;
    }

    private class GlyphTextures : ITexPositionSource
    {
        private readonly ITexPositionSource inner;
        private readonly string glyph;

        public GlyphTextures(ITexPositionSource inner, string glyph)
        {
            this.inner = inner;
            this.glyph = glyph;
        }

        public TextureAtlasPosition? this[string textureCode] => textureCode == "glyph" ? inner[glyph] ?? inner[textureCode] : inner[textureCode];

        public Size2i? AtlasSize => inner.AtlasSize;
    }

    public override void GetBlockInfo(IPlayer forPlayer, System.Text.StringBuilder dsc)
    {
        // The glyphs show the charge level; the look-at text adds nothing.
    }
}
