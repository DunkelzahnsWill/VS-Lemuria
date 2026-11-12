# Brief: feature 1, `crystals`

**Feature.** Number 1 of the Road Bible's part 3, short name `crystals`, owned by mason (R5, R15). Built in old steps 1,
2, 3 and 5 part 1, and in the old audit's fixes (PR #10).

**Its folders.** `Lemuria/Crystals/`; every folder named `crystals` under `Lemuria/assets/lemuria/` (block and item
types, lang, shapes, sounds and textures); `Lemuria/assets/lemuria/patches/crystals-lootvessel.json`; and
`tests/smoke/crystals.*`.

**Written from.** The signed-off notes as built, play-test changes included (R3): "Lemuria step 1: Find and farm",
"Lemuria step 2: The Four Elementals", "Lemuria step 3: Naked evocations" and "Lemuria step 5: The Four Mysteries"
(the old paper, Road Bible part 4), checked against the code on main at commit a1aae13. Where this brief and the old
paper differ, this brief is the record. A step number below with "old" is an old number (Road Bible, part 3).

## What the player sees and does

1. **The find.** Breaking one of the game's own vug crystals (its geode crystals) in survival with a copper pickaxe
   or better gives, besides the usual drop, a *strange glowing crystal* now and then: 4% for a small crystal, 8% for
   a large one or a cluster. About one vug in three gives one. A vug crystal that a player placed gives nothing,
   explosions and creative mode never give one, and every vug still unmined counts, in old worlds too.
2. **Grinding.** In the game's quern, a strange crystal or an Unaligned crystal grinds into three *seedlings* of its
   own Evolution: Glittering seedlings from a strange crystal or an Unaligned Glittering crystal, Glowing seedlings
   from an Unaligned Glowing crystal. Nothing else of the mod grinds. Seedlings are planted in a cradle (feature 2).
3. **The crystal.** A crystal is a block that stands on any face, as vug crystals do. It has one of nine alignments
   (the Road Bible's table): Unaligned, the four Elementals (Fire, Ice, Earth, Sky) and the four Mysteries (Life,
   Death, Time, Rust), and one of two Evolutions so far, Glittering and Glowing. A Glowing crystal is larger, a tall
   point with two smaller ones at its foot. In the hand and the inventory a crystal is one long eight-sided crystal
   pointed at both ends, bigger at Glowing. It breaks by hand and drops itself with its charge and cracks.
   Crystals and strange crystals stack to 32, seedlings to 64.
4. **Setting down.** Sneak and right-click sets a crystal or a strange crystal down on any face; a strange crystal
   set down becomes an Unaligned Glittering crystal. A plain right-click with a charged aligned crystal casts
   instead (feature 6), and blocks that take a crystal (a cradle, a rack, a socket, a Glint) take it on a plain
   right-click.
5. **Colours.** Unaligned pale ivory, Fire ruby red, Ice sapphire blue, Earth a slightly dark brown, Sky steely grey
   (a touch darker than at first, to stand apart from Unaligned), Life white-yellow, Death black-purple, Time light
   cyan like a temporal gear, Rust a dark rust.
6. **Charge, read without a number.** An aligned crystal *charges* from its alignment's environment (below) while it
   stands placed on any block or rests in a cradle, never in a hand, a bag, a container or a jar. A Glittering
   crystal holds 100 charge (units nobody sees) and fills in 1 in-game day at full strength; a Glowing one holds 200
   and fills in 2; weaker places take longer in proportion. Charge never leaks, and is the same work whatever its
   alignment. Unaligned crystals hold none. The *charge level*, empty, low (below half), high (half or more) or full,
   shows by glow on the crystal's own faces (60, 120, 190, 255), by a faint real light of 1, 2, 3 or 4 (every
   alignment and Evolution, wherever the crystal is: old step 5's answer K2), and by its hum (0.1, 0.2, 0.35, 0.5).
   Each change of level plays one chime. While it charges, faint sparks of its colour rise from it; none where its
   environment fails. An Unaligned crystal glows and lights as a full one.
7. **The notes.** Each alignment hums its own note of one pentatonic scale: Sky G4, Ice E4, Life D4, Time B3,
   Unaligned A3, Rust G3, Death E3, Fire D3, Earth B2; each twin mirrors the other around Unaligned's A. Louder with
   charge, never higher or lower. Hums never stack: any number of crystals sound no louder than one full one.
8. **Twins and dialogues.** Fire and Ice, Earth and Sky, Life and Death, and Time and Rust are twins. Twins
   *dialogue* when they stand within 3 blocks of each other in every direction, placed or resting in a cradle or a
   socket; or when they lie in one container: a chest, crate, storage vessel or any other container in which food
   ages, or a player's inventory, where everything carried counts as one container, the cursor and crafting grid
   included. When twins meet, both notes fall silent under a *discordant hum*, two tones a semitone apart that beat
   faster and louder, where the crystals stand, at the chest or around the player. If they are still together 30
   seconds later, the dialogue acts at the average of the two Evolutions, rounded down (Glittering 1, Glowing 2),
   never below 1, and both suffer it:
   - level 1 (Glittering with Glittering or Glowing): both *neutralize*, turning Unaligned of their own Evolution and
     losing any charge, with one dull thud;
   - level 2 (Glowing with Glowing): both *discharge*, losing all charge with a crackle, and one of them, picked at
     random, *cracks* 1 time in 3.
   Twins pair off one for one, nearest first. A dialogue happens only when it changes something, so empty Glowing
   twins raise no warning, and twins left together do not charge. A crystal in a jar or on a rack never dialogues
   up to Glowing (feature 5). A crystal standing within 3 blocks of its twin does not charge.
9. **Cracks.** A crack is one step of *stress*. A cracked crystal works as before. Cracks show as hairline lines,
   deeper at the second, and a thin whine joins its note, rising at the second. The third crack shatters it in a
   burst of its colour. The chance of a crack goes by the dialogue's Evolution, not by the cracks a crystal already
   has. A cradle mends cracks (feature 2); over-driving adds stress too (feature 6).
10. **Precursor crystals in ruins.** The ore and tool loot vessels of the game's ruins hold, now and then, a
    precursor crystal: an ordinary aligned crystal, empty and uncracked, four in five Glittering. The Elementals' own
    entries are 0.072 Glittering and 0.018 Glowing, any of the four alike; the Mysteries' are separate entries at
    half that, 0.036 and 0.009, so an Elemental stays about as common as before and a Mystery turns up about half as
    often. The Mysteries' half is a testing rate (the owner, 2026-11-10; the Road Bible's unplaced list).

## The environments each alignment reads

A charging crystal (and a refining cradle, feature 2, and a Gather block, feature 8) reads its surroundings every 30
seconds and gives each alignment a strength from 0 to 1, capped at 1. Every alignment reads the same 5×5×5 cube, 2
blocks out each way; only Earth's cover and Sky's open sky look further, straight up. Every reading is the game's
own: its heat sources, climate, wind, rain map, lightning event and temporal stability.

| Alignment | Strength from |
| --- | --- |
| Fire | Heat sources in the cube, each by the heat the game gives it (a burning firepit, fire or pit kiln 10, lava 12, embers 6, a burning forge 7) over 10, fading with distance as a fire warms you; plus hot air above 25 °C, up to 0.5 at 35 °C |
| Ice | Freezing air below 0 °C, up to 0.5 at −10 °C; plus snow and ice in the cube (snow layers, snow blocks, lake and glacier ice), 0.05 a block, up to 0.5 |
| Earth | Cover between the crystal and open sky: nothing up to 8 blocks, full at 40; times its walls: full when at least half the blocks in the cube are stone or ore, stonework included |
| Sky | Only under open sky, nothing overhead, not even glass: height, up to 0.5 at 60 blocks above sea level; wind, up to 0.5 in a strong wind; rain, up to 0.25. A lightning strike within 16 blocks gives six hours of full strength to a Sky crystal that stands under open sky when it lands, and only while nothing is overhead: a roof stops the hours, which come back if it goes before they run out (the owner, 2026-11-10) |
| Life | Light, the brighter of the daylight on the crystal at that hour and the game's own torches, lamps and fires in the cube, times living things: each crop, sapling, berry bush, fruit tree, potted plant and animal (not monsters) 0.1 |
| Death | Darkness, Life's light turned over, times bones: each bony remains, carcass or skull block 0.25, and every 16 bones in a container in the cube 0.25. A creature dying within 8 blocks, players included, gives Death crystals and cradles there full Death for an in-game hour |
| Time | High temporal stability, none at 1 and full at 1.5, times temporal gears in a container or on display, 0.5 each |
| Rust | Low temporal stability, none at 1 and full at 0.5 or below (deep down, rust patches, temporal storms), times decay: every 16 rot or 4 rusty gears in a container 0.25 |

- **The Mysteries read a condition times a sign** (the Road Bible's concept), so a sign in the wrong place, or the
  place without its sign, reads nothing. Lemuria's own light never counts for Life or against Death's darkness, so no
  lamp charges its own kind. Where a world has temporal stability off, depth stands in: Time full at and above sea
  level, Rust from nothing there to full 40 blocks below. Temporal storms lower stability, as the game does.
- **Twins cancel** before purity: Ice is taken from Fire and Fire from Ice, and so for each pair.
- **Purity.** The strongest alignment left leads and must pass a test that rises with Evolution: at Glittering at
  least 0.25 and at least as strong as all the others together; at Glowing at least 0.4 and twice as strong. Light,
  sunlight included, plays no part for the Elementals.
- The Mysteries' readings are counted by feature 9's code in the same walk of the cube (How it is built).

## How it is built, and what it touches

- `CrystalsModSystem` and its block entity `BlockEntityCrystal` hold charge, cracks, the level and its indicators, the
  dialogue timer, the hour after a death and Sky's strike hours. `CrystalEnvironment` reads the cube once for every
  alignment; feature 9 fills in the Mysteries' part through a hook, so crystals, cradles, machines and Glints read the
  Mysteries without knowing that feature. `StandingCrystals` keeps placed crystals in a list per chunk, so finding a
  twin within 3 blocks is a few lookups every 10 seconds, not a scan. `Dialogues` and `DiscordantHums` run the
  dialogues in the world, in containers (on the 10-second tick the game already runs for food) and in each online
  player's inventory (every 10 seconds). `HumBudget` keeps hums from stacking.
- A death that feature 9 hears comes in through `CrystalsModSystem.OnDeathAt`, which gives the hour to every standing
  crystal, cradle and Gather (and every socketed crystal) within 8 blocks; each keeps its own hour.
- The find listens to the game's own block-break and block-place events; player-placed vug crystals are remembered
  per chunk. No vanilla file is patched except the loot vessels' lists, by `crystals-lootvessel.json`.
- The chisel's strike on a placed Unaligned crystal (sneak and left-click with another Unaligned crystal or a strange
  crystal in hand, the one in hand used up) is in this folder, `BlockLemuriaCrystal.TryStrike`, and makes the
  Crystalline Chisel of feature 4.
- Two other features add behaviours to this feature's crystal block by patches in their own folders: casting
  (feature 6, `evocations-crystal.json`) and the Crystal Rod's making (feature 8, `machines-crystal.json`).
- It reads nothing from other features. Cradle, Storage, Sparkstone, Evocations, Machines and Mysteries read its
  charge, cracks, environments and dialogues.
- Saved: a crystal's charge and cracks on the item or block, its dialogue timer, the end of its Death hour and of
  Sky's strike hours, and the per-chunk list of placed vug crystals. All on the server; clients get the game's own
  block updates, item attributes and one short message when a discordant hum starts or stops.
- **Cost.** A charging crystal reads about 250 blocks and one climate value every 30 seconds while loaded; fifty cost
  under 500 block reads a second. Unloaded crystals cost nothing; charging pauses there and does not catch up.

## Settings

In `ModConfig/lemuria.json`, read by the server: `FindChanceSmall` 0.04, `FindChanceLarge` 0.08,
`SeedlingsPerCrystal` 3, `FillDaysGlittering` 1, `FillDaysGlowing` 2, `DialogueGraceSeconds` 30,
`DischargeCrackChance` 0.33, `CracksToShatter` 3.

## Claims

Every claim rule goes through the one claims switch (standing rule 7, `Lemuria/Core/`). With the switch on, the
strike on a placed crystal inside another player's claim needs their permission, as using a block there does. With
the switch off (R9), every use by hand of a placed crystal there opens: the strike, and the Crystal Rod's making
(feature 8). Breaking or placing a crystal there stays as the game rules it, whatever the switch says (the card of
2026-11-10, and R9). **Built so far:** the strike asks the switch, but the game refuses a use inside a claim before it
asks the block, so with the switch off the use is still refused (G16). The fix follows this brief as a patch.

## What it will not do

- No Luminous, Radiant or Baleful crystals, no reactions, no exposure: those are features 11 to 14 and 24.
- No GUI and no number: charge shows only by glow, light, hum and chime (standing rule 1).
- A crystal never draws charge from stone, never charges in a container, and Lemuria's own light never feeds Life.
- No Mystery battery and no faster charging that drains the environment (the unplaced list).

## What to try

1. Break the crystals of a few vugs with a copper pickaxe: now and then a strange glowing crystal drops beside the
   usual drop. Place a vug crystal yourself and break it: nothing.
2. Grind a strange crystal in a quern: three Glittering seedlings. Grind an Unaligned Glowing crystal: three Glowing
   ones.
3. Sneak and right-click a crystal onto a wall and a ceiling: it stands on any face.
4. Refine a crystal of each Elemental and leave it where it was refined: its glow and faint light step up through four
   levels with a chime each, and each alignment hums its own note. A full crystal gives light 4, an empty one 1.
5. Carry a Fire and an Ice crystal together: their notes fall silent under a discordant hum. Part them within 30
   seconds: nothing happens. Leave them: both turn Unaligned with a thud.
6. Let two charged Glowing twins meet: they discharge with a crackle, and one may crack. Its note gains a whine.
7. Set an Unaligned Glittering crystal beside a burning firepit in a cradle and watch Fire's strength win; add ten
   potted plants beside a Glowing one in daylight: it stalls, since Glowing asks for Fire twice as strong.
8. Kill a hen within 8 blocks of a Death crystal in daylight: it charges for an in-game hour.
9. In a warm thunderstorm, a Sky crystal under open sky struck within 16 blocks fills fast for six hours; roof it and
   the boost stops, unroof it and it comes back.
10. Break ruin loot vessels of the ore and tool kinds: now and then a precursor crystal, a Mystery about half as often
    as an Elemental.
11. With the claims switch off (`/lemuria claims off`), in another player's claim, strike a chisel on their placed
    Unaligned crystal: it works (needs a second player; after the R9 patch).

## Open questions

1. Is "every Lemuria block used by hand opens" with the switch off meant for the strike on a placed crystal and the
   rod's making? **Answered:** R9, "Every feature reads the same switch".
2. The Mysteries' loot rates: **answered** for now (the owner, 2026-11-10: half, for testing); revisiting them waits on
   the unplaced list, not on this brief.
