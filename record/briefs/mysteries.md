# Brief: feature 9, `mysteries`

**Feature.** Number 9 of the Road Bible's part 3, short name `mysteries`, owned by mason (R5, R15). Built in old step 5
part 1 (pull request #9) and the old audit's fixes (PR #10).

**Its folders.** `Lemuria/Mysteries/`; every folder named `mysteries` under `Lemuria/assets/lemuria/` (the Glint's
block type, and lang); no patch file; and `tests/smoke/mysteries.*`.

**Written from.** The signed-off notes as built, play-test changes included (R3): "Lemuria step 5: The Four Mysteries"
(part 1: questions 1 to 12 of 2026-11-09, K1 to K5, and the owner's word of 2026-11-10 on loot rates and a Death
Gather), checked against the code on main at commit a1aae13. Where this brief and the old paper differ, this brief is
the record.

## What the player sees and does

### The Four Mysteries, read where their sign is

1. **Condition times sign.** Life, Death, Time and Rust are the second family of alignments (feature 1 holds the
   crystals, colours and hums, and reads every alignment in one walk of the 5×5×5 cube). Each Mystery reads a
   condition of its place times a sign the player brings within 2 blocks, both from 0 to 1, so it wakes only where its
   sign is set and every Elemental place keeps working. This feature counts them:

   | Mystery | Condition | Sign | A place that works |
   | --- | --- | --- | --- |
   | Life | Light: the brighter of the daylight on the crystal at that hour and the game's own torches, lamps and fires in the cube | Living things: each crop, sapling, berry bush, fruit tree, plant in a pot or planter, and animal (not monsters or people) 0.1 | A cradle in a greenhouse bed; lamps let it charge by night |
   | Death | Darkness, Life's light turned over | Bones: each bony remains, carcass or skull block 0.25, and every 16 bones in a container 0.25 | A dark cellar with a chest of bones, under less than 8 blocks of rock or lined with wood |
   | Time | High temporal stability by the game's measure: none at 1, full at 1.5 | Temporal gears in a container or on display, 0.5 each | A roofed room on steady ground, two gears beside the cradle |
   | Rust | Low temporal stability: none at 1, full at 0.5 or below | Decay: every 16 rot or 4 rusty gears in a container 0.25 | A wood-lined room far below ground with a chest of rot, or any roofed room in a temporal storm |

2. **Lemuria's own light never counts,** for Life or against Death's darkness: crystals, cradles and Glints give none
   to the reading, so no lamp charges its own kind (answer 4).
3. **Temporal storms** lower stability everywhere, as the game does, so Time goes quiet and Rust wakes, even on the
   surface. **In a world without temporal stability,** depth stands in: Time reads full at and above sea level, and
   Rust grows from nothing there to full 40 blocks below (question 6).
4. **A death nearby.** A creature dying within 8 blocks, players included, gives the Death crystals standing there,
   the cradles holding one, and the Gather blocks holding one full Death for an in-game hour (question 5; the owner,
   2026-11-10, for the Gather). This feature hears the death and hands it to feature 1 (`CrystalsModSystem.OnDeathAt`), which tells every standing
   crystal, cradle and Gather within 8 blocks; each keeps its own hour.
   Bones in the dark are the slow, cheap way: one bony remains block in a dark room refines and charges a Glittering
   Death crystal at a quarter of full speed (K3).

### The four naked evocations

5. Each is cast exactly as feature 6's are, by its rules unchanged: hold right-click 5 seconds, over-drive from 15, a
   tenth of a full crystal a cast, nothing for a miss, the same areas and ranges by power, and claims respected
   (feature 6). This feature holds what each does:
   - **Bloom (Life),** at a plant: a crop, berry bush or fruit tree moves on one growth stage; a sapling or tree seed
     loses one in-game day of its growing time. Each plant takes Bloom once an in-game day, whatever blooms it, hand or
     machine (question 7, K4), so it roughly doubles a farm's pace.
   - **Wither (Death),** at a plant or the ground: a crop becomes the game's dead crop, keeping its farmland; saplings,
     tall grass, flowers, ferns and leaves wilt away, dropping what breaking them drops; grassy soil loses its grass.
     From Glowing it also withers every creature and player in its area: 1.5 damage every 5 seconds for 30 seconds, 9
     in all; another cast restarts the 30 seconds without stacking higher. Over-driven it covers 3×3×3 at the same
     damage. At Glittering it withers plants only (answer 8, K5). A restart ends a withering.
   - **Rewind (Time),** at a container, ground storage or dropped food: food spoiling there turns back 12, 24 or 48
     in-game hours by power, never past fresh. Only spoiling turns back: a ripened cheese stays ripe (question 9).
   - **Progress (Rust),** at the same: every slow change the game gives the items there moves on 12, 24 or 48 in-game
     hours: raw cheese ripens, pemmican dries, hides cure, food spoils toward rot for compost, and a sealed barrel's
     recipe comes that much closer (question 9).
   Rewind and Progress never touch what a player carries, so neither can spoil another player's food.
6. **In machines** (question 11): a Focus or Scatter works these four as it works any crystal's evocation (feature 8),
   for a working. A Life Focus pays only when a plant moves on, at most once a day per plant, and a machine's Life
   working pays its cost once for every 9 plants it would move on, rounded up, in full or not at all (feature 8; feature
   10's brief, item 11, R32; not built yet: this feature counts the plants for feature 8, and both are built with
   feature 10); a Death Focus with a Glowing crystal withers what stands in front of it and pays only when something is
   there; a Time or Rust Focus works a container only when it has a full measure to move, so a chest kept fresh by a
   Glittering Time Focus costs about 2 charge a day (question 12).

### Glint

7. **Making it.** A left-click of the Crystalline Chisel (feature 4) carves a placed glass block, plain, quartz or
   coloured, into a Glint, `lemuria:glint`: framed glass. Right-click with a Life crystal sets it in; right-click with
   an empty hand takes it out. No other alignment seats.
8. **Light.** Light 10 with a full Glittering crystal and 14 with a full Glowing one, about a torch's, in Life's
   white-yellow; 2 dimmer at each charge level below full; dark when empty. Every other crystal gives only a faint
   light (feature 1, K2), so a Glint is Lemuria's one lamp.
9. **Cost and recharging.** It burns a third of a charge an in-game hour at Glittering and two thirds at Glowing, about
   12 in-game days on a full crystal either way. Where Life leads at the Glint itself, its crystal charges as any
   standing crystal does, its own light left out, so a Glint in a greenhouse lights it by night and fills again by day.
10. **Twins.** A Glint is a placed crystal: a Death crystal within 3 blocks dialogues with it (feature 1). It is glass,
    not sparkstone, and takes no charge from a network. Feature 4's admin charger fills its crystal and the Energy Sink
    empties it.

### The Journal

11. Chapter 9, "The Four Mysteries" (10 pages), opens with The Four Elementals (feature 3's unlock rule). Its text is
    this feature's, in `lang/mysteries/`: each Mystery's place and sign, a page for each evocation saying what to aim
    at and what power changes, and Glint, written as instructions.

## How it is built, and what it touches

- `MysteriesModSystem` (the death listener, through the game's `OnEntityDeath`), `MysteryEnvironment` (the four
  conditions and signs), `MysteryEffects` (the four evocations), `Withering` (the damage over time, a one-second
  server tick over the creatures withering), `Ledger` (when each plant last took Bloom and each container a machine's
  Progress, kept in the savegame so a restart hands out no second Bloom; entries older than two days are dropped), and
  `BlockGlint` and `BlockEntityGlint`. No Harmony and no vanilla file touched.
- It reaches the other features only through their hooks, so no other feature knows this one: feature 1's
  `CrystalEnvironment` takes its count of the Mysteries in the same walk of the cube, and feature 6's `Effects.More`
  takes its four evocations, which machines (feature 8) then work too. It reads feature 1 for crystals, charge and
  dialogues, and hands a death to feature 1, which passes it to features 2 and 8.
- Saved: a Glint's crystal with its charge and cracks; the Ledger. A withering is not saved.
- **Cost.** A reading adds one daylight read, counts of signs among the blocks already read, a look into the
  containers in the cube, and one creature query: about a tenth more per reading. A death checks only the crystals,
  cradles and Gathers within 8 blocks. A Glint is looked at every 30 seconds.
- When this brief was written (main at a1aae13), the smoke test's Death Focus check failed by chance: a creature or
  dropped item in the Focus's line takes the aim before the flower (G43); PR #62 fixed it, merged on 2026-11-12.

## Settings

`MysteryHoursGlittering` 12 (Rewind's and Progress's hours at Glittering; twice at Glowing, four times over-driven),
`WitherDamage` 1.5, `WitherTickSeconds` 5, `WitherSeconds` 30, `GlintBurnPerHour` 1/3 (at Glittering; twice at
Glowing).

## Claims

- **Casts.** Every block, container and creature in a claim the caster may not touch is spared, as for every
  evocation (feature 6). With the claims switch off, claims spare nothing, by feature 6's rule.
- **The Glint.** With the switch on, setting a crystal into a Glint inside another player's claim, or taking one out,
  needs their permission, as a chest does. With the switch off it opens there (R9). Breaking or placing a Glint there
  stays as the game rules it. **When this brief was written** (main at a1aae13), the Glint asked the switch, but the
  game tests a claim before a block's own interaction, so with the switch off the use was still refused (G16); the
  fix follows this brief as a patch.

## What it will not do

- No Luminous or Radiant Mysteries (feature 11), exposure (12), reactions (13), strain or twin duets (14).
- No sparkglass lenses, inverted lamp or totems (K1, K3): the tools that engineer pure environments, feature 11. No
  temporal probes: the unplaced list.
- No Mystery batteries, no Glint fed by a network, and no Bloom on animals or cradles (Quicken speeds cradles).
- No limits for machines that Bloom beyond the rule by hand (K4) and the cost by plants in item 6 (feature 10's brief).

## What to try

Each place can also be built at spawn with `/lemuria mysteries test <life|death|time|rust>`.
1. Life: an Unaligned crystal in a cradle amid a crop bed under glass refines into Life by day, and by night with
   lanterns in the bed. Take the crops away: it stops.
2. Death: a cradle in a dark room under less than 8 blocks of rock with a chest of 64 bones refines into Death. Kill a
   hen within 8 blocks of a Death crystal in daylight: it charges for an in-game hour, and so does a Gather holding one.
3. Time: a cradle in a roofed room on steady ground with two temporal gears beside it refines into Time; during a
   temporal storm it stops. Rust: a wood-lined room far below ground with a chest of 64 rot or 16 rusty gears.
4. Nothing else moved: a Glowing Unaligned crystal at a forge still refines into Fire; set ten potted plants beside it
   and it pauses while they stand.
5. Bloom a crop: one stage on, and nothing more that day, even after a restart. Wither grass, leaves and a crop; with
   a Glowing crystal, Wither a creature: 1.5 every 5 seconds for 30 seconds, a second cast restarting the count.
   Rewind a chest of spoiling food; Progress raw cheese and a sealed barrel.
6. Carve a glass block into a Glint with the chisel and set a charged Life crystal in: light 10, or 14 with a Glowing
   one, dimming as it spends; in a greenhouse it fills again by day. A full crystal set down beside it gives light 4.
7. A Life Focus facing a crop bed blooms it; a Refine block turns Unaligned crystals into Time beside a Time crystal;
   a Time Focus facing a chest keeps it fresh.
8. Set a Life and a Death crystal within 3 blocks: a warning, the discordant hum, and at Glittering both fall back to
   Unaligned.
9. With the claims switch off, inside someone else's claim, take the crystal out of their Glint (needs a second
   player; after the R9 patch).

## Open questions

1. Does R9 cover the Glint, which came after R9's question? **Answered:** R9 ("Every feature reads the same switch":
   every Lemuria block used by hand), as G16 reads it.
