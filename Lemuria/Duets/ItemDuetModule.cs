using Vintagestory.API.Common;

namespace Lemuria.Duets;

/// <summary>
/// The Duet Module (feature 10's brief, item 8): a module made from a Dead Socket and a Glowing crystal,
/// set into a Focus with no crystal in its sockets. The crystals set into that Focus then go into the
/// module, two at most, last in first out; after each duet working the Memory swaps its pair with the
/// module's. Its attribute <c>lemuriaDuetModule</c> is what the machine reads, so Machines never names
/// this class; its work is in <see cref="DuetWork"/>.
/// </summary>
public class ItemDuetModule : Item
{
    public const string ClassCode = "lemuria-duetmodule";
}
