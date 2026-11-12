using System.Linq;
using Lemuria.Crystals;
using Lemuria.Sparkstone;

namespace Lemuria.Duets;

/// <summary>
/// The four Elemental duets (feature 10's brief, item 2, the owner's of 2026-10-28): two aligned crystals
/// of different alignments working as one, to make what neither makes alone. No pair is twins, so a duet
/// never dialogues. Each product is a secret of the Hidden Vocabulary: nothing in the game names it.
/// </summary>
public sealed class Duet
{
    public const string Clay = "clay";
    public const string Water = "water";
    public const string FiredClay = "firedclay";
    public const string Metal = "metal";

    public static readonly Duet[] All =
    {
        new(Crystal.Ice, Crystal.Earth, Clay),
        new(Crystal.Ice, Crystal.Sky, Water),
        new(Crystal.Fire, Crystal.Sky, FiredClay),
        new(Crystal.Fire, Crystal.Earth, Metal),
    };

    private Duet(string first, string second, string product)
    {
        First = first;
        Second = second;
        Product = product;
    }

    public string First { get; }

    public string Second { get; }

    /// <summary>What it makes, the last part of its lang key (<c>duet-clay</c>).</summary>
    public string Product { get; }

    /// <summary>The duet two alignments make, in either order, or null.</summary>
    public static Duet? Of(string? a, string? b) =>
        All.FirstOrDefault(d => (d.First == a && d.Second == b) || (d.First == b && d.Second == a));

    public static Duet? ByProduct(string product) => All.FirstOrDefault(d => d.Product == product);

    /// <summary>What two crystals make together, or null: both crystals, neither a fitting (Machines' hook).</summary>
    public static string? Name(SocketCrystal a, SocketCrystal b) =>
        a.IsFitting || b.IsFitting ? null : Of(a.Alignment, b.Alignment)?.Product;
}
