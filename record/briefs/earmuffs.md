# Brief: feature 7, `earmuffs`

**Feature.** Number 7 of the Road Bible's part 3, short name `earmuffs`, owned by mason (R5, R15). Built in old step 3,
after its play-test (pull request #4).

**Its folders.** `Lemuria/Earmuffs/`; every folder named `earmuffs` under `Lemuria/assets/lemuria/` (the item type,
the grid recipes, lang and shapes); no patch file; and `tests/smoke/earmuffs.*`.

**Written from.** The signed-off notes as built, play-test changes included (R3): "Lemuria step 3: Naked evocations",
its "After the play-test" and its section "Earmuffs" (E1 to E5, and the owner's finish of 22:01Z to 22:03Z), checked
against the code on main at commit a1aae13. Where this brief and the old paper differ, this brief is the record.

## What the player sees and does

1. **The item.** *Earmuffs*, one word, `lemuria:earmuffs`: a bent band of sticks with a pad over each ear. Its icon
   shows the earmuffs alone, not the player model. It is the first *PPE* (the glossary since old step 3, 22:39Z),
   worn against sound.
2. **Making them.** In the crafting grid, three sticks in a row along the top and, under each end, a sheet of leather
   (any colour) or of linen (any colour): one pair a craft. They are one of the two Lemuria items the game's handbook
   shows, with their recipe (the owner: "just for this PPE"; the other is the Journal, feature 3), and the last page of
   the Journal's chapter 1, "The Glowing Stone", teaches the recipe too.
3. **Wearing them.** Worn in the face slot, so a hat or helmet stays on and a mask or goggles come off. They give no
   warmth and never wear out.
4. **What they quiet.** Every sound the wearer hears, the game's, other mods' and Lemuria's, music included, plays a
   quarter quieter: three quarters of the volume gets through at the default setting (E1). Warnings are muffled like
   everything else, the discordant hum, the whines and the sounds of a dialogue, a crack or a shatter, since earmuffs
   are for working in comfort in a room full of humming crystals (E2). Other players hear as before.
5. **The player's own settings stay theirs.** The wearer's own client lowers its master volume to a share of the
   player's slider while the earmuffs are on, and gives it back in full when they come off or the game closes; the
   saved sound settings are never written. Moving the slider while wearing them works, and the share follows it within
   a quarter second.

## Where earmuffs meet other features

The folder decides (the Road Bible, part 3, "Where features meet"):
- **This feature's.** The item, its recipes, its lang (`lang/earmuffs/`, which holds chapter 1's last page,
  `journal-glowingstone-page7`), the two sound settings and the channel `lemuria-earmuffs` that carries them.
- **Other features'.** The Journal's book, chapter list and the rest of chapter 1 are feature 3's. `SoundVolume` is
  applied through `LemuriaSound` in `Lemuria/Core/`, a shared file (part 4), which every feature's sounds already pass
  through.

## How it is built, and what it touches

- `EarmuffsModSystem`, `Muffler` and `SoundSettingsPacket`. The server reads its two settings and sends them to each
  player as they join over `lemuria-earmuffs`, since clients never read the config. On the client, `Muffler` checks
  the face slot four times a second and sets the sound engine's master gain (the OpenAL listener's), which every sound
  and the music pass through. No Harmony, and no vanilla file touched.
- Saved: nothing but the item.
- **Cost.** Nothing on the server but one small message per player as they join; on the client, a look at one slot
  four times a second.

## Settings

`EarMuffsVolume` 0.75: the share of every sound that gets through earmuffs, from 0 (the world falls silent) to 1
(earmuffs do nothing). `SoundVolume` 1: every player's volume for all of Lemuria's own sounds, earmuffs or not. Both are
sent to each player as they join.

## Claims

The earmuffs are an item and no block, so no claim rule touches them.

## What it will not do

- No PPE against exposure: feature 12's, from Luminous up.
- No soundproof blocks: the unplaced list.
- They never change what other players hear, and never write the wearer's saved settings.

## What to try

1. Make earmuffs from three sticks and two sheets of leather, and again with linen; find their page in the handbook.
2. Put them on in a room of humming crystals: everything plays a quarter quieter, the crystals, the wind, footsteps
   and music alike. Take them off: the volume comes back in full.
3. Move the master volume slider while wearing them: the share follows the slider.
4. Wear a hat with them: both stay on. They show no warmth and no wear.
5. On a server, set `EarMuffsVolume` to 0 and rejoin: the earmuffs silence the world. Set `SoundVolume` to 0.5: every
   Lemuria sound is half as loud, with or without earmuffs.

## Open questions

None open. E1 to E5 and the finish are the owner's, and earmuffs were confirmed in play in old step 3 (22:33Z).
