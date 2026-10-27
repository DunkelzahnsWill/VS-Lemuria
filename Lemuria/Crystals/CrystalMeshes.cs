using System.Collections.Generic;
using Lemuria.Core;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.API.Util;

namespace Lemuria.Crystals;

/// <summary>
/// Crystal models on the client, cached per block (alignment, Evolution, position), charge level and
/// crack, as they first appear; every crystal of that kind shares one. Cracks swap the crystal's
/// texture for its cracked one (<c>crack1</c>, <c>crack2</c> in the block's textures), and the glow
/// on the faces follows the charge level. Placed crystals, cradles and racks draw from here.
/// </summary>
public static class CrystalMeshes
{
    private const string CacheKey = LemuriaModSystem.ModId + "-crystalmeshes";

    /// <summary>
    /// The model of a crystal block. With <paramref name="held"/> it is the inventory shape (the
    /// double-pointed prism) standing in the block space, as racks show it. Glow -1 keeps the shape's
    /// own glow. The result is shared: clone it before changing it.
    /// </summary>
    public static MeshData? Get(ICoreClientAPI capi, Block block, int glow, int cracks, bool held = false)
    {
        var meshes = ObjectCacheUtil.GetOrCreate(capi, CacheKey, () => new Dictionary<string, MeshData?>());
        string key = $"{block.Code}/{glow}/{cracks}/{held}";
        lock (meshes)
        {
            if (meshes.TryGetValue(key, out var cached))
            {
                return cached;
            }
        }
        MeshData? mesh = null;
        var composite = held ? block.ShapeInventory ?? block.Shape : block.Shape;
        var shape = composite?.Base == null ? null : Shape.TryGet(capi, composite.Base.Clone().WithPathPrefixOnce("shapes/").WithPathAppendixOnce(".json"));
        if (shape != null)
        {
            var textures = new CrackedTextures(capi.Tesselator.GetTextureSource(block), cracks);
            var rotation = held ? new Vec3f() : new Vec3f(composite!.rotateX, composite.rotateY, composite.rotateZ);
            capi.Tesselator.TesselateShape("lemuria-crystal", shape, out mesh, textures, rotation);
            if (glow >= 0 && mesh != null)
            {
                SetGlow(mesh, glow);
            }
        }
        lock (meshes)
        {
            meshes[key] = mesh;
        }
        return mesh;
    }

    /// <summary>Sets the glow of every vertex, 0 to 255.</summary>
    public static void SetGlow(MeshData mesh, int glow)
    {
        if (mesh.Flags == null)
        {
            return;
        }
        for (int i = 0; i < mesh.FlagsCount; i++)
        {
            mesh.Flags[i] = (mesh.Flags[i] & ~VertexFlags.GlowLevelBitMask) | (glow & VertexFlags.GlowLevelBitMask);
        }
    }

    /// <summary>The block's textures, with the crystal texture swapped for its cracked one.</summary>
    private class CrackedTextures : ITexPositionSource
    {
        private readonly ITexPositionSource inner;
        private readonly int cracks;

        public CrackedTextures(ITexPositionSource inner, int cracks)
        {
            this.inner = inner;
            this.cracks = cracks;
        }

        public TextureAtlasPosition? this[string textureCode]
        {
            get
            {
                if (textureCode == "crystal" && cracks > 0)
                {
                    return inner[cracks >= 2 ? "crack2" : "crack1"] ?? inner[textureCode];
                }
                return inner[textureCode];
            }
        }

        public Size2i? AtlasSize => inner.AtlasSize;
    }
}
