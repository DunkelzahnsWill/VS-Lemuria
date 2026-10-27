namespace Lemuria.Core;

/// <summary>
/// The volume of every sound Lemuria plays, so a server owner can quiet them all at once (SoundVolume,
/// step 3 note, Ear muffs). Every feature passes its sound's volume through <see cref="Volume"/>. The
/// server sets <see cref="Scale"/> from its config; each client is sent it as the player joins.
/// </summary>
public static class LemuriaSound
{
    private static volatile float scale = 1f;

    public static float Scale
    {
        get => scale;
        set => scale = System.Math.Clamp(value, 0f, 1f);
    }

    public static float Volume(float volume) => volume * scale;
}
