using System;
using System.Collections.Generic;
using System.Linq;
using Lemuria.Core;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace Lemuria.Journal;

/// <summary>
/// The Pilgrim's Journal (design note "Lemuria step 1: Find and farm", The Pilgrim's Journal).
/// Chapter 1 is open from the start; later chapters unlock per player through <see cref="Unlock"/>
/// and are kept with the player on the server, so a new Journal shows everything already unlocked.
/// Unlocks go to their own player only, over the channel <c>lemuria-journal</c>.
/// </summary>
public class JournalModSystem : ModSystem
{
    public const string ChannelName = LemuriaModSystem.ModId + "-journal";

    /// <summary>The key in a player's server-side data that holds their unlocked chapters, comma-separated.</summary>
    private const string PlayerDataKey = LemuriaModSystem.ModId + "-journal";

    private static readonly AssetLocation ChaptersAsset = new(LemuriaModSystem.ModId, "config/journal/chapters.json");

    private ICoreAPI api = null!;
    private ICoreServerAPI? sapi;
    private ICoreClientAPI? capi;
    private IServerNetworkChannel? serverChannel;
    private GuiDialogJournal? dialog;

    /// <summary>Every chapter, in reading order.</summary>
    public IReadOnlyList<JournalChapter> Chapters { get; private set; } = Array.Empty<JournalChapter>();

    /// <summary>On the client: the chapters this player has unlocked, besides the ones open from the start.</summary>
    private readonly HashSet<string> clientUnlocked = new();

    public override void Start(ICoreAPI api)
    {
        this.api = api;
        api.RegisterItemClass(ItemJournal.ClassCode, typeof(ItemJournal));
        api.Network.RegisterChannel(ChannelName).RegisterMessageType<JournalUnlocksPacket>();
    }

    public override void AssetsLoaded(ICoreAPI api)
    {
        var asset = api.Assets.TryGet(ChaptersAsset);
        Chapters = asset?.ToObject<List<JournalChapter>>() ?? new List<JournalChapter>();
        if (api.Side == EnumAppSide.Server)
        {
            Mod.Logger.Notification("Journal: {0} chapter(s), {1} open from the start", Chapters.Count, Chapters.Count(c => c.UnlockedByDefault));
        }
    }

    public override void StartServerSide(ICoreServerAPI api)
    {
        sapi = api;
        serverChannel = api.Network.GetChannel(ChannelName);
        api.Event.PlayerJoin += SendUnlocks;
        api.Event.SaveGameLoaded += () => Mod.Logger.Notification("Journal: {0} recipe(s)",
            api.World.GridRecipes.Count(r => r.Output?.Code?.ToString() == LemuriaModSystem.ModId + ":journal"));
    }

    public override void StartClientSide(ICoreClientAPI api)
    {
        capi = api;
        api.Network.GetChannel(ChannelName).SetMessageHandler<JournalUnlocksPacket>(packet =>
        {
            clientUnlocked.Clear();
            clientUnlocked.UnionWith(packet.Chapters);
        });
    }

    /// <summary>
    /// Unlocks a chapter for one player and tells their client. Server only; returns false when the
    /// chapter does not exist or was already unlocked. Later steps call this as the player progresses.
    /// </summary>
    public bool Unlock(IServerPlayer player, string chapterCode)
    {
        if (Chapters.All(c => c.Code != chapterCode))
        {
            return false;
        }
        var unlocked = StoredUnlocks(player);
        if (!unlocked.Add(chapterCode))
        {
            return false;
        }
        player.ServerData.CustomPlayerData[PlayerDataKey] = string.Join(",", unlocked);
        SendUnlocks(player);
        return true;
    }

    private static HashSet<string> StoredUnlocks(IServerPlayer player)
    {
        player.ServerData.CustomPlayerData.TryGetValue(PlayerDataKey, out var stored);
        return new HashSet<string>((stored ?? "").Split(',', StringSplitOptions.RemoveEmptyEntries));
    }

    private void SendUnlocks(IServerPlayer player)
    {
        serverChannel?.SendPacket(new JournalUnlocksPacket { Chapters = StoredUnlocks(player).ToList() }, player);
    }

    /// <summary>The chapters this client's player can read, in reading order.</summary>
    public List<JournalChapter> ReadableChapters() =>
        Chapters.Where(c => c.UnlockedByDefault || clientUnlocked.Contains(c.Code)).ToList();

    /// <summary>Opens the Journal's book screen. Client only.</summary>
    public void OpenJournal()
    {
        if (capi == null)
        {
            return;
        }
        if (dialog?.IsOpened() == true)
        {
            return;
        }
        dialog = new GuiDialogJournal(capi, ReadableChapters());
        dialog.TryOpen();
    }

    public override void Dispose()
    {
        dialog?.TryClose();
        if (sapi != null)
        {
            sapi.Event.PlayerJoin -= SendUnlocks;
        }
    }
}
