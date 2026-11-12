# Brief: feature 2, `cradle`

**Feature.** Number 2 of the Road Bible's part 3, short name `cradle`, owned by mason (R5, R15). Built in old steps 1,
2, 3, 4 part 1 and 5 part 1.

**Its folders.** `Lemuria/Cradle/`; every folder named `cradle` under `Lemuria/assets/lemuria/` (the block type, lang
and shapes); no patch file; and `tests/smoke/cradle.*`.

**Written from.** The signed-off notes as built, play-test changes included (R3): "Lemuria step 1: Find and farm",
"Lemuria step 2: The Four Elementals", "Lemuria step 3", "Lemuria step 4: Machines" (Quicken) and "Lemuria step 5:
The Four Mysteries" (the old paper), checked against the code on main at commit a1aae13. Where this brief and the old
paper differ, this brief is the record.

## What the player sees and does

1. **The cradle** is a carved stone basin, one block, in the 19 rock types of the game's polished rock. It is carved
   from a placed polished rock block with a left-click of the Crystalline Chisel (feature 4); the grid makes none. It
   stands on top of any solid block. Its look-at text shows only its name: what it holds is read by eye and ear.
2. **Growing.** Right-click it with a seedling (feature 1) to plant one; only Lemuria seedlings go in. A seedling grows
   through five *growth stages* and is then ripe, a sixth look. Each stage takes 2 in-game days at the default 9-day
   month, so 10 days in all, and follows the world's month length and crop growth speed, each stage up to 10% longer
   or shorter, as crops do, so a row of cradles does not change in step. No light, warmth, water or soil is needed.
   Growth goes on while nobody is near: an unloaded cradle catches up when its chunk loads.

   | Stage | Look | Glow (0 to 255) | Light | Hum (0 to 1) |
   | --- | --- | --- | --- | --- |
   | 1 | A sliver in the hollow | 40 | none | none |
   | 2 | A finger-length shard | 80 | none | 0.1 |
   | 3 | A small crystal | 120 | none | 0.2 |
   | 4 | Half grown | 160 | none | 0.3 |
   | 5 | Nearly full size | 200 | 1 | 0.4 |
   | Ripe | Full size, like a placed crystal | 255 | as the crystal it is | 0.5, and one chime as it ripens |

   A growing crystal gives at most light 1, the faintest crystal light (old step 5, K2).
3. **Harvest.** Right-click a ripe cradle with an empty hand: one Unaligned crystal, of the seedling's Evolution, and
   the cradle is empty. Breaking a cradle gives the cradle back, with its seedling while it grows, its crystal when
   ripe, or the crystal set in it, so nothing is lost.
4. **The evolution ring.** Two or more aligned Glittering crystals of any alignments, the same one twice included,
   standing in the eight blocks around a cradle at its height (placed, on the cradle's sides, or resting in a
   neighbouring cradle) while a Glittering seedling grows, make its harvest an Unaligned *Glowing* crystal. The cradle
   reads the ring as each of the five stages ends, and the ring must stand at all five: each time it does, the cradle
   chimes a rising note and sparks of the ring's colours rise; a stage that ends without it plays a dull note, and
   that crop ripens Glittering. Charge and cracks do not matter, and the ring is not used up. A Glowing seedling grows
   in 10 days into an Unaligned Glowing crystal with no ring. Rings that raise Glowing seedlings belong to feature 11.
5. **Refining.** Right-click an empty cradle with an Unaligned crystal to set it in. It *refines* into an aligned
   crystal of the same Evolution, one for one, while one alignment's environment leads and passes the purity test
   (feature 1). A Glittering crystal needs 1 in-game day at full strength and a Glowing one 2; weaker places take
   longer in proportion (at a quarter strength, 4 days for Glittering). The cradle keeps its progress toward one
   alignment: progress pauses while none passes, and starts over if another alignment takes the lead. Sparks in the
   leading alignment's colour rise while it refines, none while it stalls. When it is done the crystal takes its new
   colour with a chime and starts to charge where it stands. Refining pauses while the chunk is unloaded and does not
   catch up, because a fire can go out unseen. Right-click with an empty hand to take the crystal out, refined or not;
   unfinished progress is lost. Only Unaligned crystals refine, only into their own Evolution, and in a cradle (a
   Refine block, feature 8, does it otherwise).
6. **Resting, charging and mending.** An aligned crystal set in an empty cradle *rests* there instead: it charges
   where its own alignment leads and passes, as a placed crystal does, and is *repaired*, one crack mended per growth
   stage's length (2 in-game days at the default month, scaled as growth is). Mending catches up while unloaded;
   charging does not. A crystal resting in a cradle counts as standing for dialogues and for a neighbour's ring.
7. **The hour after a death.** A creature dying within 8 blocks, players included, gives a resting Death crystal, or
   an Unaligned one refining, full Death for an in-game hour (old step 5, question 5).
8. **Quicken.** A growing seedling can be quickened by an Unaligned crystal's working in a Focus or Scatter (feature
   8): each working moves its growth on, as if the time had passed. A ripe cradle, an empty one, or one refining or
   mending is not quickened.
9. **The Journal.** Harvesting a ripe crystal opens chapter 2, "The Four Elementals" (feature 3).

## How it is built, and what it touches

- `CradleModSystem`, `BlockCradle` and `BlockEntityCradle`. The server owns the state and the clock; the clock is
  checked every 10 seconds, comparing two numbers, and does nothing else until a stage ends. Refining and charging read
  the environments of feature 1 every 30 seconds through `CrystalEnvironment`.
- It reads feature 1 for crystals, their charge, cracks, environments and dialogues. Feature 4 carves it; feature 8
  quickens it through `BlockEntityCradle`'s growth call; feature 4's chisel draws charge from a resting crystal and
  its admin charger fills one (its Energy Sink leaves a cradle's crystal alone); feature 3 opens a chapter on harvest.
- Saved: the stage and its times, the ring count, the crystal set in it with its charge and cracks, the refining
  progress and its alignment, the mending clock, and the end of the Death hour and of Sky's strike hours.
- **Cost.** A cradle costs a clock check every 10 seconds, one block update per stage, light changes only at stage 5
  and ripe, and one cached model per rock and stage on the client. A refining or charging cradle adds a reading every
  30 seconds.

## Settings

`StageDaysAtDefaultMonth` 2, `StageRandomness` 0.1, `RefineDaysGlittering` 1, `RefineDaysGlowing` 2.

## Claims

With the claims switch on, planting, setting a crystal in, harvesting and taking one out of a cradle inside another
player's claim need their permission, as using a chest there does. With the switch off (R9), every such use by hand
opens there. Breaking or placing a cradle there stays as the game rules it. **Built so far:** the cradle has no claims
code of its own, and the game refuses a use inside a claim before it asks the block, so with the switch off the use is
still refused (G16). The fix follows this brief as a patch.

## What it will not do

- Rings to Luminous and Radiant, and purer tests for those Evolutions: feature 11.
- No faster growth beyond Quicken (the unplaced list's "faster crystal growth").
- No GUI: the stage, the ring and the refining show only by look, glow, sparks, chimes and hum.

## What to try

1. Carve a cradle from a placed polished rock block with the Crystalline Chisel, plant a seedling, and watch and
   listen: five stages, brighter and louder, and one chime when it is ripe, 10 in-game days later at default settings.
   A row of cradles does not change in step.
2. Harvest it with an empty hand: an Unaligned crystal. Break a growing cradle: the cradle and its seedling come back.
3. Stand two aligned Glittering crystals in the eight blocks around a cradle while its seedling grows: a rising chime
   at each stage, and the harvest is an Unaligned Glowing crystal. Take one away before a stage ends: a dull note, and
   it ripens Glittering.
4. Set an Unaligned crystal in a cradle beside a burning firepit, keeping the fire fed: red sparks, and a day later a
   Fire crystal. Let the fire go out: the sparks stop and the progress waits.
5. Set a cracked crystal in an empty cradle: a crack mends each growth stage's length, and it charges there where its
   alignment leads.
6. Unload the cradle's chunk for a few in-game days: growth and mending have caught up when you return; refining has
   not.
7. With the claims switch off, plant in another player's cradle inside their claim (needs a second player; after the
   R9 patch).

## Open questions

1. Does R9's "every Lemuria block used by hand" cover the cradle? **Answered:** R9 ("Every feature reads the same
   switch"); the cradle was one of the six blocks R9's question named.
