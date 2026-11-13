# Brief: feature 6, `evocations`

**Feature.** Number 6 of the Road Bible's part 3, short name `evocations`, owned by mason (R5, R15). Built in old step
3, old step 4 parts 1 and 2, old step 5 part 1, and the old audit's fixes (PR #10).

**Its folders.** `Lemuria/Evocations/`; every folder named `evocations` under `Lemuria/assets/lemuria/` (lang and
sounds); `Lemuria/assets/lemuria/patches/evocations-crystal.json`, which gives feature 1's crystal block its casting;
and `tests/smoke/evocations.*`.

**Written from.** The signed-off notes as built, play-test changes included (R3): "Lemuria step 3: Naked evocations"
whole, with its "After the play-test", T1 to T5, G1, G2, F1, F2, H1 and H2; and the parts of "Lemuria step 4: Machines"
and "Lemuria step 5: The Four Mysteries" that cast through this feature. Checked against the code on main at commit
a1aae13. Where this brief and the old paper differ, this brief is the record.

## What the player sees and does

### Casting

1. Hold right-click with a charged aligned crystal in hand. It *winds up* for 5 seconds, its own note swelling over
   its hum in a slightly softer voicing, to half a full hum's volume at most. Let go after that and the *naked
   evocation* fires at the block or creature you look at, as far as its range; let go sooner and nothing happens.
2. A cast spends a tenth of a full crystal: 10 charge from a Glittering crystal, 20 from a Glowing one. A Mystery's
   cast (feature 9) spends 4 times that, 40 and 80 (R36; R38, 4; standing rule 12; not built yet). A crystal
   holding less only clicks dully. From a stack, a cast spends one crystal's charge, and that crystal leaves the stack
   for a free slot, or drops at your feet, so the hand keeps the full ones.
3. **A miss is free.** A cast that finds nothing it can act on fizzles in a puff of its colour, costing no charge and
   risking no crack. So does a cast at a block that takes the flame and does not catch, such as a loaded bloomery with
   no chimney: by hand or from a machine, Ignite pays only for what caught (the note of old step 3, Ignite, from the
   old audit's finding 16; R7). **When this brief was written** (main at a1aae13), a cast at such a block still
   paid (G15); the fix follows this brief as a patch, which waits for the owner's merge (R7).
4. Only a crystal in the hand casts: never one in a jar, on a rack, in a bag or a socket, and a standing crystal never
   casts by itself. An Unaligned crystal does not cast; its evocation, Command, works through the Crystalline Chisel
   and machines.
5. Sneak and right-click sets a crystal down (feature 1). Blocks that can be used still take a plain right-click
   first, as they do with a bow: a cradle or rack takes the crystal, and a chest or door opens.

### Over-driving

6. Keep holding to 15 seconds: the note jumps a fifth higher and a rising whine joins it, the crystal flares in its
   colour, and hairline light runs through it. Let go, and it casts as one Evolution higher: Glittering as Glowing,
   Glowing at the *power of Luminous*. An over-driven cast costs twice the charge and cracks the crystal 1 time in 3.
   Holding longer adds nothing, and a crystal holding less than twice the cost never flares. It is the same stress as
   a dialogue's cracks (feature 1).

### Power, range and area

| | Glittering | Glowing | The power of Luminous (Glowing over-driven) |
| --- | --- | --- | --- |
| Range | 8 blocks | 12 blocks | 16 blocks |
| Area | The block or creature aimed at | A flat 3×3×1 "carpet", centred on it and level with it | Within 1 block of it, a 3×3×3 cube |

### The four Elemental evocations

| | Glittering | Glowing | The power of Luminous |
| --- | --- | --- | --- |
| **Ignite** (Fire) | Lights what you aim at, or sets a fire on its face; melts the ice or snow you aim at | Also lights what can be lit in the area and sets its creatures and players alight; aimed at ice or snow, melts all of it in the area | The same in the larger area, glacier and packed ice included; aimed at water, boils the area's water away; aimed at raw rock, melts a 2×2×1 of it into lava |
| **Quench** (Ice) | Puts out the fire, freezes the water, cools the lava or waters the tilled soil you aim at | Every fire, burning creature or player, still fresh water surface, lava and tilled soil in the area | The same in the larger area; aimed at ice, packs a 2×2×1 of it |
| **Tremor** (Earth) | A 2×2 of ground rises 2 blocks; its sides sink 1 | It rises 4; its sides sink 2 | It rises 6; its sides sink 3 |
| **Gale** (Sky) | Pushes what you aim at about 6 blocks away, or harvests or pans the block you aim at | Everything in the area, about 8 blocks, and harvests or pans the area | About 10 blocks, and harvests crops, berries and fruit too |

- **Ignite.** Lights anything a torch can light, at once: firepits, forges, pit kilns, charcoal pits, coal piles, clay
  ovens, bloomeries, torches and bombs (mind the bombs), even within arm's reach. Aimed at ice or snow it melts it:
  lake ice and loose glacier ice become still fresh water, snow layers and blocks vanish and snow lying on a block
  comes off; the world's glacier and packed ice melt only at the power of Luminous. Aimed at anything else it sets a
  fire on the face aimed at, which burns what burns and dies on stone; only that block catches, never the whole area.
  From Glowing up, creatures and players in the area catch fire. At the power of Luminous, aimed at raw rock, a 2×2×1
  (the block aimed at and those forward, right and forward-right of it, level with it) becomes the game's still lava,
  which runs, glows, warms and lights fires as natural lava does, into a claim too; ore, cobblestone, polished rock and
  the rock under your own feet stay. Aimed at water at that power, every fresh or salt water block within 1 block of
  the hit boils away with a hiss and leaves nothing; a lake flows back in. By hand salt water leaves no salt.
- **Quench.** Puts out fire blocks, burning torches (as rain does) and lit firepits, forges, coal piles and pit kilns,
  which keep fuel not yet alight. Freezes still fresh water into the game's lake ice, which melts in warm weather;
  flowing and salt water are left alone. Cools still lava to obsidian and running lava to basalt. Waters tilled soil
  as a watering can does, aimed at the soil or a crop on it. Puts out burning creatures and players. At the power of
  Luminous, aimed at ice, packs a 2×2×1: lake ice to glacier ice, glacier ice to packed ice, which do not melt where
  they stand.
- **Tremor.** The 2×2 is the column aimed at (or under a creature's feet) and those forward, right and forward-right
  of it, forward being the way you face. It is the real ground: each column's top blocks lift, surface on top, ore and
  all, and nothing is made. The blocks beneath come from the ground on its four sides, never its corners: each of
  those 8 columns gives up the blocks just under its surface and sinks half the rise, keeping its surface, plants and
  thin snow on top. Only natural soil and stone moves (soil, sand, gravel, clay, peat, raw rock, ore); cobblestone and
  polished rock count as built. Water is pushed aside and never moved. A rise that cannot happen whole fails with a
  warbled note at your hand and still spends its charge, and an over-driven one still risks its crack: when a block
  that would move is wood, a snow block, ice or built, when one of the 12 columns holds a block entity or lies where
  you may not build, or when the space above is not open (air, plants, thin snow or water). A Tremor that finds no
  ground at all fizzles free. What stands on the rise rides up, you included; what stands on the sides drops.
- **Gale.** A steely-grey gust pushes the creatures, players and dropped items in the area away from you and a little
  up. Then it harvests and pans what lies in its area, dropping it there, the same kinds stacked together: leaves grown
  on trees break as by hand (placed leaves are spared); cattails, papyrus and tule are cut as with a knife, leaving the
  root; mushrooms are picked. At the power of Luminous it also harvests ripe crops (the farmland left bare, nothing
  replanted), ripe berries and fruit (the plant stays). Sand, gravel, bony soil and any block the game or another mod
  marks pannable is panned out whole, each panning rolled from the game's own pan table at half a hand pan's chance,
  so Gale finds about half of what panning by hand does. It moves no other blocks.

### Players, creatures and you

- Players are targets like creatures: Ignite sets them alight from Glowing up, Quench puts them out, Gale pushes them
  and Tremor lifts whoever stands on the ground it raises. Your own Ignite and Gale pass over you, and your own Quench
  puts you out if you are burning.

### What you see and hear

- A streak of sparks in the crystal's colour from your hand to the target. Ignite plays the game's torch ignition,
  Quench a hiss of steam, Tremor a deep rumble with a short shake of your view, Gale a gust; melting rock and cooling
  lava sizzle. Over-driving lifts the note, adds the whine and a flare. The crystal chimes as its charge falls a level.

## Where evocations meet other features

- **Machines (feature 8)** work the same effects through this feature: a cast's effect is callable with a machine as
  its source instead of a player (`CastContext`), so a Focus or Scatter does exactly what a cast does, at a tenth of
  the charge, under feature 8's rules (its reach, its containers, its claim rule, a felling, the shelf).
- **The Mysteries (feature 9)** cast Bloom, Wither, Rewind and Progress through this casting, unchanged: the wind-up,
  over-drive, cost (four times an Elemental's, item 2), range and area above are this feature's; what each of the four
  does lies in feature 9's folder (`Effects.More`, a table that feature fills) and feature 9's brief describes it.
- **Feature 1's crystal** gets its casting from this feature's patch, `evocations-crystal.json`.
- **The Journal (feature 3)** opens Naked Evocations when a cast acts; this feature raises the event.

## How it is built, and what it touches

- `EvocationsModSystem`, `CollectibleBehaviorEvocation` (the held interaction on a crystal), `WindUp`, `Caster` and
  `CastPacket`, `CastContext` (who casts: a player or a machine; the claim test), `Evocation`, `Effects` and
  `Reaping` (Gale's harvest and panning).
- Every cast is checked and done on the server: the charge, the target by its own ray trace from the player's eyes,
  the claims, then the blocks. Every effect uses the game's own hooks: the torch's ignition interface (`IIgnitable`),
  its fire block and burning behaviour, its lake ice, an entity's own fire, claim checks and ray tracing. No Harmony,
  and no vanilla file touched.
- The game lights a forge and its like only for a player, so a cast with no caster (a machine's) lends the torch to a
  player: its carver, or else the nearest online, and waits while nobody is online.
- **Cost.** A cast touches at most a 3×3×3 or 12 columns and needs 5 seconds of holding. One short message per cast
  for its streak, spray and sound; the wind-up, over-drive note, whine and flare play on the caster's client from the
  held interaction. Nothing is saved.
- **The audit log.** The game's log records each fire Ignite starts and each block of rock it melts under the caster's
  name (for a machine, its carver), as it does for torches.

## Settings

`CastWindUpSeconds` 5, `OverdriveSeconds` 15, `CastCostShare` 0.1, `MysteryCastCostMultiplier` 4,
`OverdriveCostMultiplier` 2, `OverdriveCrackChance` 0.33, `EvocationRangeGlittering` 8, `EvocationRangeStep` 4,
`TremorRise` 2, `GalePushGlittering` 6, `GalePanningYield` 0.5. The area is not a setting.

## Claims

A naked evocation is one of Lemuria's workings (standing rule 7, on the first reading that G40 sets out and PR #46
wrote into the concept). With the claims switch on, inside another player's claim an evocation does nothing unless
they gave you access, as with building there: blocks, containers, creatures and players in the claim are spared, and
the rest of the area still works. Lava made by Ignite runs as natural lava does, into a claim too; only the rock it
melts is claim-checked. A fire Ignite sets is the caster's, so the game's own spread of it stops at any claim where
the caster may not build, whatever the switch says. A machine's cast has no caster: the fire it sets is its
carver's, and where the game does not know the carver it sets none on a face, as feature 8's brief says (R27, G58).
When this brief was written (main at a1aae13), a machine's fire had no starter, so the game held its spread at no
claim; the fix follows feature 8's brief as a patch. With the switch off, evocations no longer spare a claim. This is
**built** (`CastContext`).

Nothing in Lemuria asks the server's PvP setting. Players are targets like creatures; Wither by hand names its
caster, so the game's own PvP test holds it, while Ignite's burning and Gale's push name nobody and reach players
whatever that setting says. This is as built.

## What it will not do

- No Luminous crystal in the hand yet: the last column is reached only by over-driving a Glowing crystal. Luminous
  crystals and their exposure are features 11 and 12.
- Never casts from a jar, rack, bag or socket; never by itself.
- By hand, boiling salt water gives no salt; salt is a machine's, with feature 8's reclamation plate.
- A cast never starts a wildfire by itself: only the block aimed at catches, and any spread after that is the game's
  own fire.

## What to try

1. Take a charged Fire crystal in hand: Naked Evocations opens. Hold right-click for 5 seconds at an unlit firepit with
   fuel, 6 blocks away, and let go: a streak of red sparks lights it.
2. Aim a Fire cast at a loaded bloomery with no chimney: it fizzles, and the crystal keeps its charge (after the G15
   patch).
3. Put the fire out with an Ice crystal; freeze a 3×3 of a pond's edge with a Glowing one; water a field of crops.
4. Aim an Earth crystal at the ground under your feet: a 2×2 rises 2 with you on it and its sides sink a block. A
   Glowing one raises it 4. Aim at a chest's column: it warbles and spends its charge.
5. Push a chicken, or a heap of dropped items, with a Sky crystal. Strip a 3×3 of leaves, cut cattails, pan a beach.
   Over-driven, harvest a ripe field, a berry bush or a fruit tree.
6. Over-drive a crystal to 15 seconds until its note jumps and it flares; keep over-driving until it cracks, and mend
   it in a cradle.
7. Over-drive a Glowing Fire crystal at bare rock: a 2×2 melts to lava; cool it to obsidian with Ice. Over-drive it at
   a small pool: it boils dry. Over-drive a Glowing Ice crystal at lake ice: glacier ice, then packed ice.
8. Melt a frozen pond's edge and a patch of snow with Fire.
9. Watch a crystal's charge fall a level at a time: ten casts empty it, five over-driven.

## Open questions

1. A Fire cast at a block that takes the flame and stays cold: free or paid? **Answered:** free, by the note of old
   step 3 (Ignite, "my reading of the rule on misses, from the audit") and R7, where the owner's merge of the fix is
   the owner's word on that reading. This brief says it so that the fix leaves the brief true (G12, G15).
2. Do evocations follow the claims switch? **Answered:** yes, on the record's first reading (G40), which PR #46 wrote
   into the concept, merged on 2026-11-12.
