using Lemuria.Core;
using Lemuria.Machines;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace Lemuria.Duets;

/// <summary>
/// The duets (feature 10's brief, step 1 of the Road Bible): two aligned crystals of different
/// alignments in the Memory against a Focus work as one (<see cref="Duet"/>, <see cref="DuetWork"/>), and
/// a Duet Module lets the Focus alternate two duets (<see cref="ItemDuetModule"/>). Machines asks through
/// its hooks, so it never names this feature; Memory on an Earth drain, the shelf's putting back and the
/// limits on machines that Bloom are the Machines' and Mysteries' own code. No Harmony.
/// Adds <c>/lemuria duets test ...</c> for the smoke test and for admins.
/// </summary>
public class DuetsModSystem : ModSystem
{
    public override void Start(ICoreAPI api)
    {
        api.RegisterItemClass(ItemDuetModule.ClassCode, typeof(ItemDuetModule));
        // Both sides name a pair's duet: the server to work it, a client for the Memory's look-at line.
        MachineWork.DuetOf = Duet.Name;
    }

    public override void StartServerSide(ICoreServerAPI api)
    {
        MachineWork.DuetHook = DuetWork.Work;
        new DuetsTests(api, Mod.Logger).Register();
    }

    public override void Dispose()
    {
        MachineWork.DuetOf = null;
        MachineWork.DuetHook = null;
    }
}
