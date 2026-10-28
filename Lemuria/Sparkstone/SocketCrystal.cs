using System;
using Lemuria.Core;
using Lemuria.Crystals;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;

namespace Lemuria.Sparkstone;

/// <summary>
/// A crystal set in a socket of an Awakened block (step 4 note, Sockets and the Complexity Rating).
/// In a Free block an aligned one is a capacitor: its charge is part of the block's share, so it reads
/// as full as its network. In a machine it works for the machine, and its own charge never changes.
/// Either way it keeps its cracks and still dialogues with twins within 3 blocks, so it stands in the
/// world's register of standing crystals while its block is loaded. Server state, synced with its block.
/// </summary>
public class SocketCrystal : IStandingCrystal
{
    private readonly BlockEntitySparkstone stone;

    public SocketCrystal(BlockEntitySparkstone stone, ItemStack stack)
    {
        this.stone = stone;
        Stack = stack;
    }

    /// <summary>The crystal as an item. A capacitor's charge on it is stale; <see cref="Charge"/> is the truth.</summary>
    public ItemStack Stack { get; set; }

    /// <summary>Rested with the Crystal Rod: it sits out its turn.</summary>
    public bool Resting { get; set; }

    /// <summary>The first socket it fills, as the block packs them.</summary>
    public int Socket { get; set; }

    public BlockEntitySparkstone Stone => stone;

    public BlockPos StandPos => stone.Pos;

    public string? Alignment => Crystal.AlignmentOf(Stack.Collectible);

    public string Evolution => Crystal.EvolutionOf(Stack.Collectible);

    public bool IsAligned => Alignment != Crystal.Unaligned;

    /// <summary>Sockets it fills: a Glittering crystal 1, a Glowing one 2 by default.</summary>
    public int Size => BlockEntitySparkstone.SocketsFor(Evolution);

    public float Capacity => IsAligned ? Crystal.Capacity(Evolution) : 0;

    public bool IsCapacitor => stone.IsFree && IsAligned;

    public float Charge => IsCapacitor ? Capacity * stone.Fraction : IsAligned ? Crystal.GetCharge(Stack) : 0;

    public int Cracks => Crystal.GetCracks(Stack);

    public int Level => IsAligned ? Crystal.Level(Charge, Capacity) : Crystal.Full;

    public bool Warning { get; private set; }

    public float DialogueSeconds { get; set; }

    public bool TwinNear { get; set; }

    public void SetWarning(bool on)
    {
        if (Warning == on)
        {
            return;
        }
        Warning = on;
        if (!on)
        {
            DialogueSeconds = 0;
        }
        stone.MarkDirty(true);
    }

    public void Neutralize()
    {
        var block = Crystal.BlockFor(stone.Api.World, Crystal.Unaligned, Evolution);
        if (block == null)
        {
            return;
        }
        stone.SpendCapacitor(this);
        int cracks = Cracks;
        CrystalsModSystem.Standing?.Remove(this);
        Stack = new ItemStack(block);
        Crystal.SetState(Stack, 0, cracks);
        stone.OnSocketsChanged();
    }

    public void Discharge()
    {
        if (IsCapacitor)
        {
            stone.SpendCapacitor(this);
        }
        else
        {
            Crystal.SetState(Stack, 0, Cracks);
        }
        stone.OnSocketsChanged();
    }

    public void Crack()
    {
        int cracks = Cracks + 1;
        if (cracks >= Math.Max(1, LemuriaModSystem.Config.CracksToShatter))
        {
            stone.Shatter(this);
            return;
        }
        Crystal.SetState(Stack, IsCapacitor ? 0 : Crystal.GetCharge(Stack), cracks);
        stone.OnSocketsChanged();
    }

    public void OnLightning() => stone.OnSocketLightning(this);

    public void ToTreeAttributes(ITreeAttribute tree)
    {
        tree.SetItemstack("stack", Stack);
        tree.SetBool("resting", Resting);
        tree.SetBool("warning", Warning);
        tree.SetFloat("dialogueSeconds", DialogueSeconds);
    }

    public static SocketCrystal? FromTreeAttributes(BlockEntitySparkstone stone, ITreeAttribute tree, IWorldAccessor world)
    {
        var stack = tree.GetItemstack("stack");
        if (stack == null || !stack.ResolveBlockOrItem(world) || !Crystal.IsCrystal(stack.Collectible))
        {
            return null;
        }
        return new SocketCrystal(stone, stack)
        {
            Resting = tree.GetBool("resting"),
            Warning = tree.GetBool("warning"),
            DialogueSeconds = tree.GetFloat("dialogueSeconds"),
        };
    }
}
