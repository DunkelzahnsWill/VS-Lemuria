using System;
using System.Collections.Generic;
using System.Linq;
using Vintagestory.API.MathTools;

namespace Lemuria.Sparkstone;

/// <summary>
/// Where the sockets sit on an Awakened block's face (step 4 note, Sockets and the Complexity Rating):
/// two columns of sockets, as many rows as the room needs, so the 4 of an Awakened block make a 2×2.
/// Crystals are packed largest first, a Glowing crystal across a whole row, so any mix that fits the
/// room has a place. A face is read as a player standing before it sees it: right and up, with north
/// up on the top and bottom faces.
/// </summary>
public static class Sockets
{
    /// <summary>The face's up direction as seen from outside it: up for the four sides, north for the top and bottom.</summary>
    public static Vec3d Up(BlockFacing face) => face.IsVertical ? new Vec3d(0, 0, -1) : new Vec3d(0, 1, 0);

    /// <summary>The face's right direction as seen from outside it (up × outward normal).</summary>
    public static Vec3d Right(BlockFacing face)
    {
        var up = Up(face);
        var n = face.Normald;
        return new Vec3d(up.Y * n.Z - up.Z * n.Y, up.Z * n.X - up.X * n.Z, up.X * n.Y - up.Y * n.X);
    }

    public static int Rows(int room) => Math.Max(1, (room + 1) / 2);

    /// <summary>A point on the face, 0 to 1 across (u, rightward) and up (v), from a hit inside the block.</summary>
    public static (double U, double V) Uv(BlockFacing face, Vec3d hit)
    {
        var d = hit.SubCopy(0.5, 0.5, 0.5);
        return (0.5 + d.Dot(Right(face)), 0.5 + d.Dot(Up(face)));
    }

    /// <summary>The socket under a point on the face.</summary>
    public static int CellAt(int room, double u, double v)
    {
        int rows = Rows(room);
        int row = Math.Clamp((int)((1 - v) * rows), 0, rows - 1);
        int column = u < 0.5 ? 0 : 1;
        return Math.Min(room - 1, row * 2 + column);
    }

    /// <summary>The centre of a run of sockets on the face, in (u, v).</summary>
    public static (double U, double V) Center(int room, int first, int size)
    {
        int rows = Rows(room);
        double u = 0, v = 0;
        for (int i = first; i < first + size; i++)
        {
            u += i % 2 == 0 ? 0.28 : 0.72;
            v += 1 - (i / 2 + 0.5) / rows;
        }
        return (u / size, v / size);
    }

    /// <summary>The centre of a run of sockets in block coordinates, on the face itself.</summary>
    public static Vec3d Point(BlockFacing face, int room, int first, int size)
    {
        var (u, v) = Center(room, first, size);
        var right = Right(face);
        var up = Up(face);
        var n = face.Normald;
        return new Vec3d(0.5, 0.5, 0.5)
            .Add(right.X * (u - 0.5), right.Y * (u - 0.5), right.Z * (u - 0.5))
            .Add(up.X * (v - 0.5), up.Y * (v - 0.5), up.Z * (v - 0.5))
            .Add(n.X * 0.5, n.Y * 0.5, n.Z * 0.5);
    }

    /// <summary>
    /// Packs crystals of these sizes into the room, largest first, a crystal of 2 at the start of a row:
    /// the first socket of each, in the order given, or null when they do not fit.
    /// </summary>
    public static int[]? Pack(IReadOnlyList<int> sizes, int room)
    {
        var used = new bool[room];
        var firsts = new int[sizes.Count];
        foreach (int index in Enumerable.Range(0, sizes.Count).OrderByDescending(i => sizes[i]))
        {
            int size = Math.Max(1, sizes[index]);
            int step = size >= 2 ? 2 : 1;
            int found = -1;
            for (int start = 0; start + size <= room && found < 0; start += step)
            {
                bool free = true;
                for (int i = start; i < start + size; i++)
                {
                    free &= !used[i];
                }
                if (free)
                {
                    found = start;
                }
            }
            if (found < 0)
            {
                return null;
            }
            for (int i = found; i < found + size; i++)
            {
                used[i] = true;
            }
            firsts[index] = found;
        }
        return firsts;
    }
}
