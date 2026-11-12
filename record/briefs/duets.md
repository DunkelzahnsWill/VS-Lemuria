# Brief: feature 10, `duets`

**Feature.** Number 10 of the Road Bible's part 3, short name `duets`, in step 1, owned by mason (R5, R15). Built in PR
#156.

**Its folders.** `Lemuria/Duets/` with its own `DuetsModSystem`; every folder named `duets` under
`Lemuria/assets/lemuria/` (the Duet Module's item type, its recipe and lang); no patch file; and `tests/smoke/duets.*`
(R30). Three parts of its work lie in other features' folders and follow the changes this PR makes to their briefs
(rule 4): Memory's widening, Memory on a drain and the shelf's re-placing in feature 8's `Lemuria/Machines/`; the
limits on machines that Bloom in feature 9's `Lemuria/Mysteries/` and feature 8's; and the hook that keeps a
magazine's crystals out of dialogues in feature 1's `Lemuria/Crystals/`. None of it touches feature 4's folder or
feature 6's (How it is built). Feature 4's brief changes in one sentence, which would otherwise say too much.

**Written from.** The Road Bible, part 3, step 1. The owner's four duets and the Duet Module (2026-10-28), the owner's
Memory idea (2026-11-08) and the limits on Blooming machines (K4, 2026-11-09), as the Road Bible places them. The
old paper's sketch, read as history only: "Lemuria step 5: The Four Mysteries", "Duets (part 2, sketched)" and "Not in
part 1". The briefs of features 1, 3, 4, 6, 8 and 9 as they stand on main at 5e0b384, R30 and R31. The owner's answers
to the draft's eight questions (R32, 2026-11-12) settle every rule the draft proposed, as proposed, and way A for the
panning idea.

**Changed after it settled** by a second PR, from the owner's answers on Memory and Bloom (R36), on six questions about
them (R38), on the reason Mysteries cost more (R39, and the Road Bible's standing rule 12 beside this PR), and on three
more (R41), with the correction of G96: Memory's rod tap and blind swap (items 13 to 15), the Mysteries' price and
machines that Bloom (item 11), and the fill out of reach (item 10). This change touches the briefs of features 6, 8 and
9 too. Its code follows as a patch, once this change merges; where an item below is not built yet, it says so.

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
   | Fire and Earth | Metal from ore | Ore nuggets in the input extracted into the metal's bits, one bit for each nugget, into the output, for every ore whose smelted form in the game is an ingot (copper, tin, zinc, lead, silver, gold and bismuth, and also chromium, titanium, platinum and nickel, the bits of the first three being kept out of the game's own handbook); the iron ores give nothing |

3. **Where the pair sits, and what gives the turn.** Both crystals of a pair, awake, in the magazine of the Memory set
   against a Focus. A Memory holding a pair stops cycling and lends the pair to the machine, and the pair gives the
   turn: the Focus works the duet from its front once every 10 seconds, a machine's pace, whether or not its own
   sockets hold crystals, and its own crystals sit idle while the Memory holds a pair. A Memory holding no pair cycles
   as today. A Scatter works no duet: a duet works at one point, as a Focus does.
4. **Power and charge.** A duet works at the lower of its two crystals' Evolutions, and an Amplify touching
   the machine lifts it one Evolution for twice the charge, as for every working. It costs what both crystals would
   cost working once each (a tenth of a cast each, feature 8), paid from the machine's network in full or not at all
   (feature 4), and nothing when it finds nothing to make (a miss is free, feature 6).
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
   what it does): an item, made in the grid from a Dead Socket and a Glowing crystal of any alignment (the crystal used
   up), set into one of a Focus's sockets as the Dead Socket is, and taken out the same way. A Focus holding a Duet
   Module holds no crystal of its own: the module goes only into a Focus with no crystal in its sockets, a crystal set
   into the Focus then goes into the module, two at most, and comes out of it the same way, last in first out. The
   module holds the second pair in the open, as sockets do; it does not keep them out of dialogues. After each duet
   working, Memory swaps its pair with the module's, both crystals together, so the machine alternates two duets (clay
   and then fired clay, say). Only the pair in the module stands in the open: Ice and Earth in the magazine and Fire and
   Sky in the module, then the other way about, so no twins meet in the open. When the module holds no duet's pair (one
   crystal, or two that make no duet), nothing swaps and the Memory's pair works alone. It works only on a machine
   already working a duet: with no pair in the Memory it does nothing, and in a socket of a machine with no Memory it
   does nothing. Swapping costs nothing.

### The owner's Memory idea: panning an Earth battery's store

9. The owner's words (2026-11-08): "use Memory to swap out the crystals needed to (safely) pan an earth battery's
   internal store, perhaps then using a control crystal to re-place stone into the battery automatically". What it
   meets today: an Earth battery's drain wears gravel to sand with no finds, since panning is Sky's (feature 8, J6);
   Earth and Sky are twins, and twins standing within 3 blocks of each other in sockets dialogue (feature 1, item 8),
   so a Sky crystal in a drain's Memory beside its Earth crystals dialogues today; and a stone working a battery does
   only that, so Memory against a fill stone stays still (feature 8, item 18). "Safely" is the twins.
10. **Way A** (the owner: "Making Memory a "Shielded" block makes sense, up to Glowing as the other machinery"; R31).
    Memory's magazine keeps its crystals apart as a jar does, up to Glowing: a crystal in the magazine never dialogues,
    and from Luminous up it holds nothing back, where feature 14's strain begins. Only the crystals in the machine's
    sockets stand in the open, so Earth and Sky take turns.
    - *Memory on a drain.* Memory may stand against an Earth battery's drain, on any face but the one facing into the
      room. The drain's socket holds a single crystal and the magazine the rest, since two crystals in its sockets would
      stand in the open, where twins dialogue. The Memory cycles them as for any machine, a drain's turn being one block
      worn, panned or put back. While an Earth crystal is in the socket the drain wears its room as today. A turn with
      nothing to do passes at once to the next crystal, as a Dormant crystal is passed over: the Memory looks every 10
      seconds, a machine's pace, and an Earth crystal's turn ends when it has worn one step or cannot wear now (its
      network at half or above, or no block left to wear), so Sky and the Unaligned crystal come round whatever the
      network holds.
    - *The fill stands out of reach* (G96; R41, 1, "A"). The fill's Earth crystal stands in the open in its socket, so
      a Sky crystal in the drain's socket dialogues with it when the two stand within 3 blocks of each other, counted
      as feature 1 counts it, in every direction and diagonals included. Panning is safe only where the fill stands out
      of that reach, 4 or more blocks from the drain along at least one of east, up and north (a fill 3 east and 3
      north of the drain is still in reach): "A player must either create a larger building
      or route crystals via tubes and/or memory swaps." Nothing in the code keeps them apart.
    - *Panning.* While a Sky crystal is in the socket, the drain pans one sand block of its room a turn, nearest first,
      at a working's cost: the block is gone, and what the game's pan gives from one whole block of sand goes into the
      container touching the drain. With no sand, no container or a full one, it is a free miss.
    - *The room with a hole.* A block a drain panned keeps its place in the room: both stones go on working, it holds
      nothing, and the room's size in the count the stones show drops by its 200 ("Stored 1,000 of 5,200") until stone
      is put back; what is stored drops by what the panned sand held, so charge fed into sand is lost when it is panned.
      The drain remembers the blocks it panned, through a restart. Any other empty block still rests the room (feature
      8, item 21).
    - *Putting stone back,* the "control crystal". An Unaligned crystal in the drain's cycle works a Control Shelf set
      against the drain. The shelf's one action on a drain: it puts one block from its input, of the kind standing on
      the shelf (sand, gravel, cobblestone, cracked rock or raw rock), into the panned block nearest the bottom, at a
      working's cost, and the room holds it as its medium from then on. With no stock it waits, as a shelf item out of
      stock does. A Control Shelf may stand against a drain for this alone.

### Limits for machines that Bloom

11. Today a plant takes Bloom once an in-game day, whatever blooms it, hand or Focus, and a working pays a tenth of a
    cast whatever it moves on (K4; features 8 and 9). So a Glowing Scatter blooms 81 plants a day for one working's
    charge. So: keep the rule once a day per plant and one stage a Bloom (how often), and make a machine pay for how
    strongly: a Life working pays its usual cost once for every 9 plants it would move on, rounded up, in full or not
    at all. A network that cannot pay it all blooms nothing that working, as a felling does (feature 8). A hand's cast
    stays as it is.
    **Changed by R36** (not built yet) (the owner: "let us remove the use limit, within a machine only, for bloom-ery
    and raise its charge consumption along with the charge consumption of all mysteries"): inside a machine the
    once-a-day rule goes, and every Mystery working, in a machine or by hand, costs 4 times an Elemental's (R38, 3 to 5;
    features 6, 8 and 9). The charge per 9 plants stays (R38, 5), so a 3×3 field blooms every 10 seconds for 4 charge a
    working at Glittering's power, and a Glowing Scatter's 81 plants for 36.

### What the player is told

12. No Journal page in step 1. The duets are secrets of the Hidden Vocabulary, and the Hidden
    Vocabulary and the Journal are completed in step 7, by changes to the briefs concerned (the Road Bible, step 7). A
    looked-at Memory says "Duet: clay" (or the product) while it holds a pair, so a player who has found one can see it.

### Memory's rod tap and blind swap (R36, R38, R41; not built yet)

13. **The rod tap.** A right-click of the Crystal Rod on a Memory, anywhere but on a crystal in it, switches it
    between lending a pair (item 3) and cycling as any Memory does, so the two crystals of a duet can also simply take
    turns. A Memory has no front to turn, so the click is free for this; a click on a crystal still makes it Dormant
    and a sneaking click still switches the block Active or Dormant (feature 8, item 6). A Memory starts lending, and
    its look-at line says "Lends its pair" or "Cycles".
14. **The blind swap.** A Dead Socket locks into a Memory (R38, 1), beside its magazine and taking none of its room,
    and comes out as a Dead Socket comes out of a Motor. A Memory holding one swaps blind at every turn of its machine:
    everything in the machine's sockets changes places with everything in its magazine, empty places included,
    whatever the crystals are (R38, 2). The owner's example: a Focus holding one Fire crystal, its Memory holding Ice
    and Sky; after the turn the Focus holds Ice and Sky and the Memory Fire and an empty place. A blind Memory lends no
    pair, and the rod tap does nothing to it. On an Earth drain it swaps the same way, the drain's crystal and its
    empty place going to the magazine as one set and the magazine's two coming out into the drain (R41, 2), so two
    crystals then stand in the drain's sockets in the open.
15. **Its danger is learned.** Twins a blind swap sets side by side in the machine's sockets stand in the open and
    dialogue as always (feature 1, item 8): nothing warns before the swap, and the player learns it by the hum (R36,
    "let players learn via experience").

## How it is built, and what it touches

- `DuetsModSystem`, `Duet` (the four pairs, their amounts, what each takes and makes), `DuetWork` (one working of a
  duet), `ItemDuetModule`. No Harmony and no vanilla file touched.
- **Feature 8 (Machines), by the change to its brief in this pull request:** `BlockEntityMemory` learns whether its
  magazine holds a pair and lends it, and its look-at line; `BlockEntityMachine.WorkOnce` asks a hook before the crystal
  whose turn it is works: `MachineWork.DuetHook`, which feature 10 sets, so feature 8 never names feature 10. The hook
  gets the machine and its Memory and returns a result, or none to work as today. The Duet Module's swap happens in the
  hook. Memory against a drain, the drain's Sky turn (panning), the panned blocks it remembers, the room's count and the
  shelf's re-placing are feature 8's battery and shelf code. `MachineWork` asks feature 9 how many plants a Life working
  would move on before it pays, and pays by that.
- **Feature 1 (Crystals), by the change to its brief in this pull request:** `Dialogues` takes a list of shields, as it
  takes the shielded inventory classes today; feature 8 adds one that holds a `SocketCrystal` whose stone is a Memory,
  up to Glowing. A magazine's crystals stay registered as every socket's are, so feature 4's code does not change.
- **Feature 9 (Mysteries), by the change to its brief in this pull request:** `MysteryEffects` counts the plants a Bloom
  would move on, without moving them, for feature 8 to ask. Nothing passes through feature 6's `Effects`, `CastContext`
  or `MachineCast`.
- **Feature 4 (Sparkstone), by the change to its brief in this pull request:** one sentence only, its item 10, which
  says a socketed crystal always dialogues with a twin within 3 blocks: a Memory's magazine now keeps its crystals out
  up to Glowing.
- It reads feature 1 for crystals, charge and Evolutions, feature 4 to pay, feature 6's cast context for the aim and
  claims, and the game's own recipes: a nugget's smelted stack names its metal (only metals whose smelted stack is an
  ingot; the iron ores, whose smelted stack is a bloom, give nothing), and a raw item's pit-kiln firing names its
  fired form.
- Saved: nothing of its own. The pair and the module's pair are crystals in sockets, saved as today.
- **For R36 (not built yet),** all in feature 8's and feature 6's folders, by the changes to their briefs in this PR:
  `BlockEntityMemory` keeps whether it lends or cycles and whether a Dead Socket is locked in, both saved, and a blind
  swap swaps the machine's sockets with its magazine, places and all; `ItemCrystalRod`'s tap on a Memory switches it;
  `MachineWork` prices a Mystery working at `MysteryCostMultiplier` times an Elemental's and stops asking feature 9 for
  the once-a-day rule inside a machine; feature 6's casting prices a Mystery cast at `MysteryCastCostMultiplier` times
  an Elemental's. No code of feature 10's own folder changes.
- **Cost.** One hook call on each Focus turn, which looks at the Memory already found; a duet working looks at one
  block or one container, as a working does today.

## Settings

`DuetWaterLitres` 10 (at Glittering; twice at Glowing, four times amplified), `DuetItemsPerWorking` 4
(the same), `BloomPlantsPerWorking` 9; feature 8's `MysteryCostMultiplier` 4 and feature 6's
`MysteryCastCostMultiplier` 4 (R38).

## Claims

- A duet works through feature 6's cast context and feature 8's claim rule, unchanged: with the claims switch on, it
  changes no block inside a claim the machine does not stand in, so clay is never made in another player's claim, and
  it uses an input or output there only as feature 8 rules a machine's containers (G61). With the switch off, claims
  stop no duet.
- Setting the pair into Memory and a Duet Module into a socket are uses by hand of a Lemuria block, as feature 8 rules
  them (R9).
- Nothing in this feature sets water, fire or any block down in the world except clay in the block faced, under the
  claim rule, and inside an Earth battery's own room the drain's panning and the shelf's putting back. A room that
  holds a block its stone may not change rests (feature 8), so neither reaches into another player's claim (standing
  rule 7; R30).

## What it will not do

- No twin duets, strain or Luminous crystals in machines: feature 14. No duet in a Scatter, a Gather, a Refine block or
  a battery's fill.
- No duets of the Mysteries, and no third crystal in a duet.
- No Journal pages for the duets before step 7.
- No metal from the iron ores (they make a bloom in the game's bloomery) and no alloys.
- No other shelf action on a drain than putting stone back, and no Memory on a fill or on any other Gather.

## What to try

1. Carve a Focus facing a block of sand, set a Memory against it, and put a Glittering Ice and a Glittering Earth
   crystal in the Memory: the sand becomes raw blue clay; dig it. Take one crystal out: the duet stops, and the Focus
   does nothing until a crystal stands in its own socket, when Memory cycles again.
2. An Ice and Sky pair over a barrel at the output: 10 litres of water a working, 20 with Glowing crystals. With no
   barrel, nothing happens and nothing is paid.
3. A Fire and Sky pair with raw clay pots on the shelf's input: fired pots in the output, up to 4 a working.
4. A Fire and Earth pair with copper and tin nuggets in the input: copper and tin bits in the output, one for each
   nugget. Iron nuggets stay.
5. Set a Duet Module into the Focus of item 1, then a Fire and a Sky crystal into the Focus (they go into the module),
   over sand: it makes clay, then fires nothing (no pottery yet); set sand in front again and it makes clay again. No
   dialogue.
6. A Glowing Life Scatter over a field of 81 crops: they all move on one stage, and the network pays nine Mystery
   workings' charge, 36. Ten seconds later they move on again, for 36 more.
7. An Earth battery's room with sand in it and its network at half or above (fill it with the admin charger), its drain
   with one Earth crystal in its socket, a Memory holding a Sky and an Unaligned crystal, a chest touching it and a
   Control Shelf with sand on it and in its input, the fill 4 or more blocks from the drain along one direction: no
   dialogue. Earth cannot wear with the network that full, so its turn passes at once. When Sky takes its turn the drain
   pans a sand block into the chest and the stones show 200 less; when the Unaligned crystal takes its turn the shelf
   puts a sand block back.
8. Restart: the pair stays in the Memory and the module's pair in its socket; a panned block is still panned and the
   shelf still puts stone back into it; a Memory's rod-tap setting and its Dead Socket stay.
9. Tap a Memory holding Ice and Earth with the rod: it says "Cycles", and the Focus works Ice and then Earth as single
   crystals. Tap again: clay.
10. Lock a Dead Socket into a Focus's Memory, the Focus holding one Fire crystal and the Memory Ice and Sky: after one
    turn the Focus holds Ice and Sky and the Memory Fire and an empty place; after the next, back again. With Earth and
    Sky swapped in together, the warning hum.

## Open questions

1. Where a duet's pair sits, and in which machine (item 3). **Answered:** R32, 1, "Yes".
2. Clay made in place from the faced soil or sand (item 2). **Answered:** R32, 2, "Yes", read as the proposal, the
   first of the two the question named.
3. The amounts and charge (items 4 and 5). **Answered:** R32, 3, "Yes".
4. Metal: one bit for each nugget, none from the iron ores. **Answered:** R32, 4, "Yes".
5. The Duet Module (item 8). **Answered:** R32, 5, "Yes".
6. The Memory panning idea (items 9 and 10). **Answered:** R32, 6, way A; R31 says what it changes in the Road Bible
   (the planning thread's change to the concept, on one list with this brief).
7. Limits on Blooming machines (item 11). **Answered:** R32, 7, "Yes"; changed by R36 and R38, 3 to 5.
8. No Journal page before step 7 (item 12). **Answered:** R32, 8, "Yes".
9. A battery's fill within 3 blocks of its drain (G96): the fill's Earth crystal stands in the open, so a Sky crystal in
   the drain's socket dialogues with it in a small room. Way 1: the brief says the fill must stand more than 3 blocks
   from the drain, and the code stays. Way 2: Sky's turn on a drain passes at once, as a turn with nothing to do,
   while an Earth twin stands within 3 blocks, so panning is safe in every battery and a small room never pans. Way 3:
   the fill's crystals are shielded while a Memory stands on the drain. **Answered:** R41, 1, way 1 ("A"), item 10.
10. A Dead Socket in a Memory on a drain, whose socket holds one crystal (item 10): mason proposes it does nothing
    there, since a blind swap would set two crystals in the open beside the fill. And in a Scatter it swaps as in a
    Focus. **Answered:** R41, 2, "Swap both the crystal and the "empty space", as a "set" of crystals", read as a blind
    swap on a drain as anywhere else (item 14); a Scatter's swap was put and not objected to.
11. Does the Dead Socket take one of the magazine's places? **Answered:** R38, 1, and R41, 3: it does not (item 14).
