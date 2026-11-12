# Brief: feature 10, `duets`

**Feature.** Number 10 of the Road Bible's part 3, short name `duets`, in step 1, owned by mason (R5, R15). Not built.

**Its folders.** `Lemuria/Duets/` with its own `DuetsModSystem`; every folder named `duets` under
`Lemuria/assets/lemuria/` (the Duet Module's item type, its recipe and lang); no patch file; and `tests/smoke/duets.*`
(R30). Three parts of its work lie in other features' folders and follow the changes this PR makes to their briefs
(rule 4): Memory's widening, Memory on a drain and the shelf's re-placing in feature 8's `Lemuria/Machines/`; the
limits on machines that Bloom in feature 9's `Lemuria/Mysteries/`; and Memory's shielded magazine in feature 1's
dialogues, `Lemuria/Crystals/`.

**Written from.** The Road Bible, part 3, step 1. The owner's four duets and the Duet Module (2026-10-28), the owner's
Memory idea (2026-11-08) and the limits on Blooming machines (K4, 2026-11-09), as the Road Bible places them. The
old paper's sketch, read as history only: "Lemuria step 5: The Four Mysteries", "Duets (part 2, sketched)" and "Not in
part 1". The briefs of features 1, 3, 6, 8 and 9 as they stand on main at 5e0b384, R30 and R31. The owner's answers
to the draft's eight questions (R32, 2026-11-12) settle every rule the draft proposed, as proposed, and way A for the
panning idea.

## What the player sees and does

### A duet, and how a machine works one

1. **A duet** is two aligned crystals of different alignments working as one, to make what neither makes alone (the
   concept). It is worked by a Focus through its Memory: Memory, which today only cycles crystals, *widens the
   machine's commands*. Each duet's product is a secret of the Hidden Vocabulary: nothing in the game names it, and
   the player finds it by setting the pair together.
2. **The four Elemental duets** are the owner's (2026-10-28). No pair is twins (Fire and Ice, Earth and Sky are), so a
   duet never dialogues:

   | Duet | Makes | From what, and where it goes |
   | --- | --- | --- |
   | Ice and Earth | Clay | The block the Focus faces, if it is soil or sand, becomes the game's raw blue clay, which the player digs as the game's own |
   | Ice and Sky | Water | Fresh water drawn from the air into a liquid container at the machine's output (a barrel, a bucket, a jug) |
   | Fire and Sky | Fired clay | Raw pottery in the machine's input fired into its fired form, as the game's pit kiln fires it, into the output |
   | Fire and Earth | Metal from ore | Ore nuggets in the input extracted into the metal's bits, one bit for each nugget, into the output |

3. **Where the pair sits.** Both crystals of a pair, awake, in the magazine of the Memory set against a
   Focus. A Memory holding a pair stops cycling and instead lends the pair to the machine: on each of the machine's
   turns the duet works once, from the Focus's front, in place of the crystal whose turn it was. A Memory holding no
   pair cycles as today. A Scatter works no duet: a duet works at one point, as a Focus does.
4. **Power and charge.** A duet works at the lower of its two crystals' Evolutions, and an Amplify touching
   the machine lifts it one Evolution for twice the charge, as for every working. It costs what both crystals would
   cost working once each, paid from the machine's network (feature 4), and nothing when it finds nothing to make (a
   miss is free, feature 6). Each crystal loses the charge it would lose working alone.
5. **Amounts, by the Evolution the duet works at.** Glittering, Glowing, amplified Glowing: clay, one block a
   working; water, 10, 20 and 40 litres; fired clay and metal, up to 4, 8 and 16 items a working.
6. **When a duet finds nothing.** Clay with no soil or sand faced; water with no liquid container at the
   output, or a full one; fired clay or metal with nothing to fire or extract in the input, or no room in the output:
   the working is a free miss, and an output with no room sends the machine Dormant as it does today (feature 8,
   question 7).
7. **What the input and output are** stay feature 8's: a Control Shelf's left and right, or with no shelf, a container
   touching the machine, and claims as feature 8 rules them (Claims below).

### The Duet Module

8. **The Duet Module**, the owner's earmark (2026-10-28), a module (the concept: a part fitted to a machine that changes
   what it does): an item, made in the grid from a Dead Socket and a Glowing crystal of any alignment (the
   crystal used up), set into one of a Focus's sockets as the Dead Socket is. It holds a second pair: after each duet
   working, Memory swaps its pair with the module's, both crystals together, so the machine alternates two duets (clay
   and then fired clay, say). It works only on a machine already working a duet: with no pair in the Memory it does
   nothing, and in a socket of a machine with no Memory it does nothing. Swapping costs nothing.

### The owner's Memory idea: panning an Earth battery's store

9. The owner's words (2026-11-08): "use Memory to swap out the crystals needed to (safely) pan an earth battery's
   internal store, perhaps then using a control crystal to re-place stone into the battery automatically". What it
   meets today: an Earth battery's drain wears gravel to sand with no finds, since panning is Sky's (feature 8, J6);
   Earth and Sky are twins, and twins standing within 3 blocks of each other in sockets dialogue (feature 1, item 8),
   so a Sky crystal in a drain's Memory beside its Earth crystals dialogues today; and a stone working a battery does
   only that, so Memory against a fill stone stays still (feature 8, item 18). "Safely" is the twins.
10. **Way A** (the owner: "Making Memory a "Shielded" block makes sense, up to Glowing as the other machinery"; R31).
    Memory's magazine keeps its crystals apart as a jar does, up to Glowing: a crystal in the magazine never dialogues,
    and from Luminous up it holds nothing back, where feature 14's strain begins. Only the crystal in the machine's
    socket stands in the open, so Earth and Sky take turns. Memory may stand against an Earth battery's drain, on any
    face but the one facing into the room, and the drain's Memory then cycles as for any machine, a drain's turn being
    one block worn, panned or put back: while an Earth crystal
    is in the socket the drain wears its room as today; while a Sky crystal is in it, the drain pans the sand its
    wearing left, a block at a time, into the container touching it, with the game's own panning drops for sand, and
    the panned block is gone from the room. The "control crystal" is an Unaligned crystal in that cycle working the
    drain's Control Shelf: on its turn the shelf puts one block of stone from its input into the room's nearest empty
    block, from the bottom up, at a working's cost, as the shelf's one action on a drain. A Control Shelf may stand
    against a drain for this alone. Twins in the drain's own sockets still dialogue there, so the drain holds one
    crystal in its socket and the rest in the magazine.

### Limits for machines that Bloom

11. Today a plant takes Bloom once an in-game day, whatever blooms it, hand or Focus, and a working pays a tenth of a
    crystal's charge whatever it moves on (K4; features 8 and 9). So a Glowing Scatter blooms 81 plants a day for
    one working's charge. So: keep the rule once a day per plant and one stage a Bloom (how often), and make a
    machine pay for how strongly: a Life working pays its usual cost once for every 9 plants it moves on, rounded up.
    A hand's cast stays as it is.

### What the player is told

12. No Journal page in step 1. The duets are secrets of the Hidden Vocabulary, and the Hidden
    Vocabulary and the Journal are completed in step 7, by changes to the briefs concerned (the Road Bible, step 7). A
    looked-at Memory says "Duet: clay" (or the product) while it holds a pair, so a player who has found one can see it.

## How it is built, and what it touches

- `DuetsModSystem`, `Duet` (the four pairs, their amounts, what each takes and makes), `DuetWork` (one working of a
  duet), `ItemDuetModule`. No Harmony and no vanilla file touched.
- **Feature 8 (Machines), by the change to its brief below:** `BlockEntityMemory` learns whether its magazine holds a
  pair and lends it, and its look-at line; `BlockEntityMachine.WorkOnce` asks a hook before the crystal whose turn it
  is works: `MachineWork.DuetHook`, which feature 10 sets, so feature 8 never names feature 10. The hook gets the
  machine and its Memory and returns a result, or none to work as today. The Duet Module's swap happens in the hook.
  Memory against a drain, the drain's Sky turn (panning) and the shelf's re-placing are feature 8's battery and shelf
  code.
- **Feature 1 (Crystals), by the change to its brief below:** its dialogues skip a crystal in a Memory's magazine up to
  Glowing, as they skip a jar's and a rack's.
- **Feature 9 (Mysteries), by the change to its brief below:** Bloom counts the plants a machine's working moved on
  and hands the count back, so the machine pays by it.
- It reads feature 1 for crystals, charge and Evolutions, feature 4 to pay, feature 6's cast context for the aim and
  claims, and the game's own recipes: a nugget's smelted stack names its metal (only metals whose smelted stack is an
  ingot; the iron ores, whose smelted stack is a bloom, give nothing), and a raw item's pit-kiln firing names its
  fired form.
- Saved: nothing of its own. The pair and the module's pair are crystals in sockets, saved as today.
- **Cost.** One hook call on each Focus turn, which looks at the Memory already found; a duet working looks at one
  block or one container, as a working does today.

## Settings

`DuetWaterLitres` 10 (at Glittering; twice at Glowing, four times amplified), `DuetItemsPerWorking` 4
(the same), `BloomPlantsPerWorking` 9.

## Claims

- A duet works through feature 6's cast context and feature 8's claim rule, unchanged: with the claims switch on, it
  changes no block inside a claim the machine does not stand in, so clay is never made in another player's claim, and
  it uses an input or output there only as feature 8 rules a machine's containers (G61). With the switch off, claims
  stop no duet.
- Setting the pair into Memory and a Duet Module into a socket are uses by hand of a Lemuria block, as feature 8 rules
  them (R9).
- Nothing in this feature sets water, fire or any block down in the world except clay in the block faced, under the
  claim rule, so nothing reaches into another player's claim (standing rule 7; R30).

## What it will not do

- No twin duets, strain or Luminous crystals in machines: feature 14. No duet in a Scatter, a Gather, a Refine block or
  a battery's fill.
- No duets of the Mysteries, and no third crystal in a duet.
- No Journal pages for the duets before step 7.
- No metal from the iron ores (they make a bloom in the game's bloomery) and no alloys.

## What to try

1. Carve a Focus facing a block of sand, set a Memory against it, and put a Glittering Ice and a Glittering Earth
   crystal in the Memory: the sand becomes raw blue clay; dig it. Take one crystal out: Memory cycles again.
2. An Ice and Sky pair over a barrel at the output: 10 litres of water a working, 20 with Glowing crystals. With no
   barrel, nothing happens and nothing is paid.
3. A Fire and Sky pair with raw clay pots on the shelf's input: fired pots in the output, up to 4 a working.
4. A Fire and Earth pair with copper and tin nuggets in the input: copper and tin bits in the output, one for each
   nugget. Iron nuggets stay.
5. Set a Duet Module with a Fire and Sky pair into the Focus of item 1, over sand: it makes clay, then fires nothing
   (no pottery yet) and makes clay again.
6. A Glowing Life Scatter over a field of 81 crops: they all move on one stage, and the network pays nine workings'
   charge. The next working that day does nothing and pays nothing.
7. An Earth battery's drain with a Memory holding a Sky and an Unaligned crystal, and a Control Shelf with stone in its
   input: no dialogue; when Sky takes its turn the drain pans its sand into the chest, and when the Unaligned crystal
   takes its turn the shelf puts a block of stone back into the room.
8. Restart: the pair stays in the Memory and the module's pair in its socket.

## Open questions

1. Where a duet's pair sits, and in which machine (item 3). **Answered:** R32, 1, "Yes".
2. Clay made in place from the faced soil or sand (item 2). **Answered:** R32, 2, "Yes", read as the proposal, the
   first of the two the question named.
3. The amounts and charge (items 4 and 5). **Answered:** R32, 3, "Yes".
4. Metal: one bit for each nugget, none from the iron ores. **Answered:** R32, 4, "Yes".
5. The Duet Module (item 8). **Answered:** R32, 5, "Yes".
6. The Memory panning idea (items 9 and 10). **Answered:** R32, 6, way A; R31 says what it changes in the Road Bible
   (the planning thread's change to the concept, on one list with this brief).
7. Limits on Blooming machines (item 11). **Answered:** R32, 7, "Yes".
8. No Journal page before step 7 (item 12). **Answered:** R32, 8, "Yes".
