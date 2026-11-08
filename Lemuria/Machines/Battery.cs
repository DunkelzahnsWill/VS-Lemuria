using System;
using System.Collections.Generic;
using System.Linq;
using Lemuria.Crystals;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;

namespace Lemuria.Machines;

/// <summary>
/// Elemental batteries (design note "Lemuria step 4 part 3", Elemental batteries): a battery is the box
/// between its two stones, the drain (a Gather) facing into one bottom corner and the fill (a Focus)
/// into the top corner opposite, from 2×2×1 to 7×7×7, closed in by solid blocks, glass included. The
/// crystals in its stones decide its Elemental (the owner, 2026-11-02 08:08Z): Fire's medium is raw
/// rock, melted to lava; Ice's still fresh water, frozen to glacier ice; Earth's rock and ore,
/// pre-charged, with sand, gravel, cracked rock and cobblestone a weak medium fed charge. This class
/// finds and checks a room; <see cref="BlockEntityMachine"/> works it. Server only.
/// </summary>
public static class Battery
{
    /// <summary>What a block of a room is, as far as a battery cares.</summary>
    public enum Cell
    {
        Other,
        /// <summary>The world's raw rock, or rock quarried whole: Fire's medium, Earth's deposit.</summary>
        Rock,
        Lava,
        /// <summary>Still fresh water: Ice's medium.</summary>
        Water,
        /// <summary>The game's glacier ice: Ice's charged form.</summary>
        Ice,
        /// <summary>Ore: Earth's richest deposit.</summary>
        Ore,
        /// <summary>Sand, gravel, cracked rock or cobblestone: Earth's weak medium.</summary>
        Weak,
    }

    public static readonly string[] Elementals = { Crystal.Fire, Crystal.Ice, Crystal.Earth };

    /// <summary>A room between two stones, and what every stone facing into it is.</summary>
    public sealed class Room
    {
        public BlockPos Min = null!;
        public BlockPos Max = null!;

        /// <summary>The bottom corner the drain faces into: where an Earth drain wears nearest first.</summary>
        public BlockPos DrainCorner = null!;

        /// <summary>Every Gather facing into the room from outside: the drains.</summary>
        public readonly List<BlockEntityMachine> Drains = new();

        /// <summary>Every Focus facing into the room from outside: the fills.</summary>
        public readonly List<BlockEntityMachine> Fills = new();

        /// <summary>The Elemental its stones' crystals name, or null.</summary>
        public string? Elemental;

        /// <summary>Why the battery rests, a lang key's last part, or null when it works.</summary>
        public string? Problem;

        public int SizeX => Max.X - Min.X + 1;
        public int SizeY => Max.Y - Min.Y + 1;
        public int SizeZ => Max.Z - Min.Z + 1;
        public int Volume => SizeX * SizeY * SizeZ;

        /// <summary>The room's name in a stone's saved count, so a count follows only its own room.</summary>
        public string Key => $"{Min.X},{Min.Y},{Min.Z};{Max.X},{Max.Y},{Max.Z}";

        public IEnumerable<BlockEntityMachine> Stones => Drains.Concat(Fills);

        public bool Contains(BlockPos pos) =>
            pos.X >= Min.X && pos.X <= Max.X && pos.Y >= Min.Y && pos.Y <= Max.Y && pos.Z >= Min.Z && pos.Z <= Max.Z;

        public int IndexOf(BlockPos pos) => (pos.X - Min.X) + SizeX * ((pos.Z - Min.Z) + SizeZ * (pos.Y - Min.Y));

        public BlockPos At(int index)
        {
            int x = index % SizeX;
            int z = index / SizeX % SizeZ;
            int y = index / (SizeX * SizeZ);
            return new BlockPos(Min.X + x, Min.Y + y, Min.Z + z, Min.dimension);
        }

        /// <summary>Every block of the room, bottom layer first.</summary>
        public IEnumerable<BlockPos> Blocks() => Enumerable.Range(0, Volume).Select(At);

        /// <summary>
        /// The order a fill turns blocks and the drain turns them back in reverse: Fire's lava rises
        /// from the bottom layer up, Ice freezes from the top layer down (questions 3 and 4).
        /// </summary>
        public List<int> FillOrder()
        {
            var order = new List<int>(Volume);
            int layer = SizeX * SizeZ;
            for (int y = 0; y < SizeY; y++)
            {
                int ly = Elemental == Crystal.Ice ? SizeY - 1 - y : y;
                for (int i = 0; i < layer; i++)
                {
                    order.Add(ly * layer + i);
                }
            }
            return order;
        }
    }

    /// <summary>What a battery keeps, the same in each of its stones (question 8: either can be broken and set back).</summary>
    public sealed class State
    {
        public string Key = "";
        public string Elemental = "";
        public float Stored;
        public long Rev;

        /// <summary>The rock each melted block of a Fire battery was, by its index in the room.</summary>
        public readonly Dictionary<int, string> Rocks = new();

        /// <summary>The block an Earth drain is wearing all the way down, by index, or -1.</summary>
        public int WearIndex = -1;

        /// <summary>New charge paid toward the next step of Earth's wear.</summary>
        public float Wear;

        public void ToTree(ITreeAttribute tree)
        {
            tree.SetString("key", Key);
            tree.SetString("elemental", Elemental);
            tree.SetFloat("stored", Stored);
            tree.SetLong("rev", Rev);
            tree.SetInt("wearIndex", WearIndex);
            tree.SetFloat("wear", Wear);
            var rocks = new TreeAttribute();
            foreach (var (index, code) in Rocks)
            {
                rocks.SetString(index.ToString(), code);
            }
            tree["rocks"] = rocks;
        }

        public static State FromTree(ITreeAttribute tree)
        {
            var state = new State
            {
                Key = tree.GetString("key", ""),
                Elemental = tree.GetString("elemental", ""),
                Stored = tree.GetFloat("stored"),
                Rev = tree.GetLong("rev"),
                WearIndex = tree.GetInt("wearIndex", -1),
                Wear = tree.GetFloat("wear"),
            };
            if (tree["rocks"] is ITreeAttribute rocks)
            {
                foreach (var (index, value) in rocks)
                {
                    if (int.TryParse(index, out int i) && value is StringAttribute code)
                    {
                        state.Rocks[i] = code.value;
                    }
                }
            }
            return state;
        }
    }

    // ------------------------------------------------------------------ cells

    private static bool Game(Block block, string prefix) =>
        block.Code?.Domain == "game" && block.Code.Path.StartsWith(prefix, StringComparison.Ordinal);

    public static Cell Classify(IBlockAccessor accessor, BlockPos pos)
    {
        var solid = accessor.GetBlock(pos, BlockLayersAccess.Solid);
        var fluid = accessor.GetBlock(pos, BlockLayersAccess.Fluid);
        if (solid == null || fluid == null)
        {
            return Cell.Other;
        }
        if (solid.BlockId == 0)
        {
            if (fluid.LiquidCode == "lava")
            {
                return Cell.Lava;
            }
            return fluid.Code?.Domain == "game" && fluid.Code.Path == "water-still-7" ? Cell.Water : Cell.Other;
        }
        if (fluid.BlockId != 0)
        {
            return Cell.Other;
        }
        if (solid.Code?.Domain == "game" && solid.Code.Path == "glacierice")
        {
            return Cell.Ice;
        }
        if (Game(solid, "rock-"))
        {
            return Cell.Rock;
        }
        if (Game(solid, "ore-"))
        {
            return Cell.Ore;
        }
        if (Game(solid, "sand-") || Game(solid, "gravel-") || Game(solid, "crackedrock-") || Game(solid, "cobblestone-"))
        {
            return Cell.Weak;
        }
        return Cell.Other;
    }

    /// <summary>Whether a block of this kind may stand in a battery of this Elemental.</summary>
    public static bool Fits(string elemental, Cell cell) => elemental switch
    {
        Crystal.Fire => cell is Cell.Rock or Cell.Lava,
        Crystal.Ice => cell is Cell.Water or Cell.Ice,
        Crystal.Earth => cell is Cell.Rock or Cell.Ore or Cell.Weak,
        _ => false,
    };

    private static bool AnyMedium(Cell cell) => cell != Cell.Other;

    // ------------------------------------------------------------------ finding a room

    /// <summary>
    /// The room this stone works, or null when it works none: a Gather or Focus facing into a box that
    /// a drain and a fill at opposite corners make, with both corners a battery's medium. The room is
    /// checked (Problem set when it rests) only when <paramref name="check"/> is true.
    /// </summary>
    public static Room? Find(BlockEntityMachine stone, bool check = true)
    {
        if (stone.Form is not (Machine.Gather or Machine.Focus))
        {
            return null;
        }
        var world = stone.Api.World;
        var accessor = world.BlockAccessor;
        var aim = stone.Pos.AddCopy(stone.Front);
        if (!AnyMedium(Classify(accessor, aim)))
        {
            return null;
        }
        int max = Machine.BatteryMaxSide;
        var near = Nearby(world, aim, max);
        Room? best = null;
        foreach (var (drain, d) in near.Where(n => n.Machine.Form == Machine.Gather))
        {
            foreach (var (fill, f) in near.Where(n => n.Machine.Form == Machine.Focus))
            {
                if (d.Y > f.Y)
                {
                    continue;
                }
                int sx = Math.Abs(d.X - f.X) + 1, sy = f.Y - d.Y + 1, sz = Math.Abs(d.Z - f.Z) + 1;
                if (sx < 2 || sz < 2 || sx > max || sz > max || sy > max)
                {
                    continue;
                }
                var room = new Room
                {
                    Min = new BlockPos(Math.Min(d.X, f.X), d.Y, Math.Min(d.Z, f.Z), d.dimension),
                    Max = new BlockPos(Math.Max(d.X, f.X), f.Y, Math.Max(d.Z, f.Z), d.dimension),
                    DrainCorner = d.Copy(),
                };
                if (!room.Contains(aim) || room.Contains(drain.Pos) || room.Contains(fill.Pos)
                    || (best != null && best.Volume <= room.Volume))
                {
                    continue;
                }
                if (!AnyMedium(Classify(accessor, d)) || !AnyMedium(Classify(accessor, f)))
                {
                    continue;
                }
                best = room;
            }
        }
        if (best == null)
        {
            return null;
        }
        foreach (var (machine, front) in near)
        {
            if (best.Contains(front) && !best.Contains(machine.Pos))
            {
                (machine.Form == Machine.Gather ? best.Drains : best.Fills).Add(machine);
            }
        }
        if (check)
        {
            Check(stone, best);
        }
        return best;
    }

    /// <summary>Every Gather and Focus whose front block lies within this reach of a position.</summary>
    private static List<(BlockEntityMachine Machine, BlockPos Front)> Nearby(IWorldAccessor world, BlockPos at, int reach)
    {
        var found = new List<(BlockEntityMachine, BlockPos)>();
        int size = GlobalConstants.ChunkSize;
        int r = reach + 1;
        for (int cx = (at.X - r) / size; cx <= (at.X + r) / size; cx++)
        {
            for (int cy = Math.Max(0, at.InternalY - r) / size; cy <= (at.InternalY + r) / size; cy++)
            {
                for (int cz = (at.Z - r) / size; cz <= (at.Z + r) / size; cz++)
                {
                    var chunk = world.BlockAccessor.GetChunk(cx, cy, cz);
                    if (chunk?.BlockEntities == null)
                    {
                        continue;
                    }
                    foreach (var be in chunk.BlockEntities.Values)
                    {
                        if (be is not BlockEntityMachine { Form: Machine.Gather or Machine.Focus } machine)
                        {
                            continue;
                        }
                        var front = machine.Pos.AddCopy(machine.Front);
                        if (Math.Abs(front.X - at.X) <= reach && Math.Abs(front.Y - at.Y) <= reach && Math.Abs(front.Z - at.Z) <= reach)
                        {
                            found.Add((machine, front));
                        }
                    }
                }
            }
        }
        return found;
    }

    /// <summary>
    /// Names the room's Elemental by its fill's crystals, or by the count it holds, and checks it: every block that Elemental's
    /// medium or its charged form, every block around it solid, the whole room loaded and, with claims
    /// respected, inside a claim its stones may change. Sets the room's Problem when it rests.
    /// </summary>
    public static void Check(BlockEntityMachine stone, Room room, string? held = null)
    {
        var accessor = stone.Api.World.BlockAccessor;
        if (Elementals.Contains(held))
        {
            // A battery holding a count stays the Elemental that stored it until the count is spent (note rev 37).
            room.Elemental = held;
        }
        else
        {
            // The fill's crystals name the battery; crystals of any other alignment, in either stone, sit idle (the owner, J4, Nov 8).
            var alignments = room.Fills.SelectMany(s => s.Working).Where(c => c.IsAligned && Elementals.Contains(c.Alignment))
                .Select(c => c.Alignment!).Distinct().ToList();
            if (alignments.Count == 0)
            {
                room.Problem = "nocrystal";
                return;
            }
            if (alignments.Count > 1)
            {
                room.Problem = "twoelementals";
                return;
            }
            room.Elemental = alignments[0];
        }
        foreach (var pos in room.Blocks())
        {
            if (accessor.GetChunkAtBlockPos(pos) == null)
            {
                room.Problem = "unloaded";
                return;
            }
            if (!Fits(room.Elemental, Classify(accessor, pos)))
            {
                room.Problem = "foreign";
                return;
            }
            if (!stone.MayChange(pos))
            {
                room.Problem = "claim";
                return;
            }
        }
        if (!Closed(accessor, room))
        {
            room.Problem = "gap";
        }
    }

    /// <summary>Whether every block touching the room from outside is solid on the face toward it; glass is solid.</summary>
    private static bool Closed(IBlockAccessor accessor, Room room)
    {
        foreach (var pos in room.Blocks())
        {
            foreach (var face in BlockFacing.ALLFACES)
            {
                var next = pos.AddCopy(face);
                if (room.Contains(next))
                {
                    continue;
                }
                if (accessor.GetChunkAtBlockPos(next) == null)
                {
                    return false;
                }
                var block = accessor.GetBlock(next, BlockLayersAccess.Solid);
                if (block == null || block.BlockId == 0 || !(block.SideSolid[face.Opposite.Index] || Game(block, "glass")))
                {
                    return false;
                }
            }
        }
        return true;
    }

    // ------------------------------------------------------------------ Earth's deposit

    /// <summary>
    /// What one step of Earth's wear turns a block into, and its byproducts (question 7): ore to its
    /// rock with the ore's pieces as mining it gives, rock to cracked rock, cracked rock to gravel with
    /// two stones, gravel to sand with a pan's worth from the game's panning table whole. Null for a
    /// block that wears no further.
    /// </summary>
    public static (Block Into, List<ItemStack> Byproducts)? WearStep(IWorldAccessor world, BlockPos pos)
    {
        var block = world.BlockAccessor.GetBlock(pos, BlockLayersAccess.Solid);
        if (block?.Code?.Domain != "game")
        {
            return null;
        }
        string? rock = block.Variant["rock"] ?? block.LastCodePart();
        Block? Get(string path) => world.GetBlock(new AssetLocation("game", path));
        var byproducts = new List<ItemStack>();
        Block? into;
        if (Game(block, "ore-"))
        {
            into = Get("rock-" + rock) ?? Get("crackedrock-" + rock);
            byproducts.AddRange(block.GetDrops(world, pos, null) ?? Array.Empty<ItemStack>());
        }
        else if (Game(block, "rock-"))
        {
            into = Get("crackedrock-" + rock) ?? Get("gravel-" + rock);
        }
        else if (Game(block, "crackedrock-"))
        {
            into = Get("gravel-" + rock);
            if (world.GetItem(new AssetLocation("game", "stone-" + rock)) is { } stone)
            {
                byproducts.Add(new ItemStack(stone, 2));
            }
        }
        else if (Game(block, "gravel-"))
        {
            into = Get("sand-" + rock);
            byproducts.AddRange(Evocations.Reaping.Pan(world, block, null, 1, 1));
        }
        else
        {
            return null;
        }
        return into == null ? null : (into, byproducts.Where(s => s != null && s.StackSize > 0).ToList());
    }

    /// <summary>
    /// Whether an Earth drain may wear a block: rock, ore, cracked rock or gravel, wherever it came from,
    /// so a room of gravel pans (the owner, 2026-11-02 08:23Z); sand and cobblestone never wear.
    /// </summary>
    public static bool Wearable(IWorldAccessor world, BlockPos pos)
    {
        var cell = Classify(world.BlockAccessor, pos);
        if (cell is not (Cell.Rock or Cell.Ore or Cell.Weak))
        {
            return false;
        }
        var block = world.BlockAccessor.GetBlock(pos, BlockLayersAccess.Solid);
        if (cell == Cell.Weak && !(Game(block, "crackedrock-") || Game(block, "gravel-")))
        {
            return false;
        }
        return WearStep(world, pos) != null;
    }
}
