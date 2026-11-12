# Brief: feature 3, `journal`

**Feature.** Number 3 of the Road Bible's part 3, short name `journal`, owned by mason (R5, R15). Built in every old
step from old step 1 on.

**Its folders.** `Lemuria/Journal/`; every folder named `journal` under `Lemuria/assets/lemuria/`: the item type, the
grid recipe (`recipes/grid/journal/`), the chapter list (`config/journal/chapters.json`), the cover texture and its
lang file `lang/journal/en.json`; no patch file; and `tests/smoke/journal.*`.

**Written from.** The signed-off notes as built, play-test changes included (R3): the Journal's sections of the notes
of old steps 1 to 5 (the old paper), checked against the code on main at commit a1aae13. Where this brief and the old
paper differ, this brief is the record.

## What the player sees and does

1. **The book.** *The Pilgrim's Journal*, `lemuria:journal`, is the mod's guide and its one GUI (standing rule 1). It is
   made in the crafting grid, shapeless, from any plain book (`book-normal-*`, the kind players make; a written book
   loses its text) and one Unaligned Glittering crystal or one strange glowing crystal, which the recipe uses up. A
   seedling will not do. The strange crystal's description hints at it. The game's handbook shows the Journal and its
   recipe (and the earmuffs, feature 7); every other Lemuria item stays out of the handbook, so the Journal is where
   they are learned. A lost Journal is made again the same way.
2. **The screen.** Right-click opens the Journal's own book screen: a chapter list on the left and an open spread of
   two pages of rich text, turned with the buttons below or the arrow keys.
3. **Chapters, and when each opens.** Chapter 1 is open from the start. Every other chapter opens for one player as
   that player progresses, with a chat line "Your Journal has a new chapter", and is kept with that player on the
   server, so a new Journal shows everything already open.

   | In reading order | Opens when the player | Its text lies in |
   | --- | --- | --- |
   | The Glowing Stone (7 pages) | From the start | `lang/journal/`, its last page (earmuffs) in `lang/earmuffs/` |
   | The Four Elementals (8) | First harvests a ripe crystal, or first carries an aligned crystal | `lang/journal/` |
   | Living Stone (4) | First carries a fully charged crystal | `lang/journal/` |
   | Naked Evocations (7) | First holds a charged aligned crystal in hand, or casts | `lang/journal/` |
   | Crystal Machines (13) | First carves a machine | `lang/machines/` |
   | The Control Shelf (8) | Opens with Crystal Machines | `lang/machines/` |
   | Quicken (1, a hidden page) | Is the carver of a Focus or Scatter that first quickens a cradle, while online | `lang/machines/` |
   | Batteries and Probes (3) | Opens with Crystal Machines | `lang/machines/` |
   | The Four Mysteries (10) | Opens with The Four Elementals | `lang/mysteries/` |

   A chapter added later to a chapter that a player has already opened opens for them when they next join, so a
   returning player who had Crystal Machines gets The Control Shelf and Batteries and Probes, and one who had The Four
   Elementals gets The Four Mysteries.
4. **How it is written.** In the pilgrim's voice, plain hints and never riddles; the build pages are instructions
   only: what to build and set, never how the player gets about to do it (the owner, 2026-11-02). The author's mix-up
   of Light and Life may slip in once. It says "vug", not geode, and "a handful of seedlings" (old step 1's
   play-test). Quicken is the first page of the Hidden Vocabulary and is named only once it is found; Crystal
   Machines' last page hints at it without naming it.

## Where the Journal meets other features

The folder decides (the Road Bible, part 3, "Where features meet"):
- **This feature's.** The book, its recipe and screen, the chapter list and its order, every unlock rule above and the
  per-player store of what is open, the chat line, and the text of chapters 1 to 4 (but chapter 1's earmuffs page).
- **Other features'.** The text of a chapter whose lang lies in another feature's folder is that feature's, and its
  brief describes it: chapters 5 to 8 are feature 8's, chapter 9 feature 9's, and chapter 1's last page feature 7's.
  The events this feature listens to for its unlocks are raised by features 2 (a ripe crystal lifted out), 6 (a cast
  that acted), and 8 (a machine carved, a cradle quickened).

## How it is built, and what it touches

- `JournalModSystem`, `ItemJournal`, `GuiDialogJournal` and `JournalChapter`. The chapter list is read from
  `config/journal/chapters.json` (a code, a page count and whether it is open from the start); page text is the lang
  keys `journal-<chapter>-title` and `journal-<chapter>-page<N>`, which the game merges from every lang file.
- Unlocks are kept in each player's server-side data, sent to that player only over the channel `lemuria-journal` as
  they join and on each new unlock. What a player carries is looked at every 10 seconds and when they change hotbar
  slots; the rest listens to the events named above. No Harmony and no vanilla file touched.
- **Cost.** A look at each online player's inventory every 10 seconds, a short unlock list per player, and one small
  message per unlock.

## Settings

None.

## Claims

The Journal is an item and no block, so no claim rule touches it.

## What it will not do

- No second GUI anywhere in the mod, no numbers on the page, and no handbook pages for Lemuria items but the Journal's
  and the earmuffs'.
- The Journal's story (its author's confusion between Light, Life, Death and Darkness, and pages that open only
  through mishaps) is on the unplaced list, not in this brief.
- The complete Hidden Vocabulary and the Journal's last pages belong to step 7, by changes to this brief and the
  features concerned.

## What to try

1. Make the Journal from a plain book and a strange crystal: the crystal is used up. Try a seedling: nothing.
2. Read chapter 1, then harvest a ripe crystal: "Your Journal has a new chapter", The Four Elementals, and The Four
   Mysteries beside it.
3. Carry a fully charged crystal: Living Stone. Hold a charged aligned crystal in hand: Naked Evocations.
4. Carve a machine: Crystal Machines, The Control Shelf and Batteries and Probes open together. Quicken a cradle with
   an Unaligned Focus: the hidden page Quicken.
5. Throw the Journal away and make another: every chapter you had is still open.
6. Turn pages with the arrow keys; pick a chapter from the list.

## Open questions

None open. The unlock rules are the notes' as built; the owner played each in its step.
