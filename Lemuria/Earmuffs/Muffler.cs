using System;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.Client;
using Vintagestory.Client.NoObf;

namespace Lemuria.Earmuffs;

/// <summary>
/// While the player wears ear muffs, their own client lowers its master volume, the OpenAL listener's
/// gain, which every sound and the music pass through. It never writes the player's saved sound
/// settings: it reads their master volume and plays a share of it, and gives it back in full when the
/// ear muffs come off or the game closes. If the player moves the master volume slider while wearing
/// them, the game sets the gain back to the slider, and the next check lowers it again. No Harmony.
/// </summary>
public class Muffler : IDisposable
{
    /// <summary>How often the client checks the face slot, in milliseconds.</summary>
    private const int CheckMs = 250;

    private readonly ICoreClientAPI capi;
    private readonly long listener;
    private bool muffling;

    /// <summary>Share of every sound that gets through, as the server sent it.</summary>
    public float Volume { get; set; } = 0.75f;

    public Muffler(ICoreClientAPI capi)
    {
        this.capi = capi;
        listener = capi.Event.RegisterGameTickListener(_ => Check(), CheckMs);
    }

    /// <summary>Whether the player wears ear muffs in their face slot.</summary>
    public static bool Wearing(IPlayer? player)
    {
        var inventory = player?.InventoryManager?.GetOwnInventory(GlobalConstants.characterInvClassName);
        if (inventory == null || inventory.Count <= (int)EnumCharacterDressType.Face)
        {
            return false;
        }
        var code = inventory[(int)EnumCharacterDressType.Face]?.Itemstack?.Collectible?.Code;
        return code != null && code.Domain == Core.LemuriaModSystem.ModId && code.Path == EarmuffsModSystem.ItemCode;
    }

    private static float SliderLevel => ClientSettings.MasterSoundLevel / 100f;

    private void Check()
    {
        var platform = ScreenManager.Platform;
        if (platform == null)
        {
            return;
        }
        if (Wearing(capi.World.Player))
        {
            float muffled = SliderLevel * Math.Clamp(Volume, 0f, 1f);
            if (!muffling || Math.Abs(platform.MasterSoundLevel - muffled) > 0.001f)
            {
                platform.MasterSoundLevel = muffled;
            }
            muffling = true;
        }
        else if (muffling)
        {
            platform.MasterSoundLevel = SliderLevel;
            muffling = false;
        }
    }

    public void Dispose()
    {
        capi.Event.UnregisterGameTickListener(listener);
        if (muffling && ScreenManager.Platform != null)
        {
            ScreenManager.Platform.MasterSoundLevel = SliderLevel;
        }
        muffling = false;
    }
}
