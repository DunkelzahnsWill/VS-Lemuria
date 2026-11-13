# Mason's map

Role: mason, the one builder of Lemuria (R5, R15). This folder, record/diary/mason/, holds this map and my diary.

The Road Bible (record/road-bible.md) and the settled briefs outrank this map (rule 5). Where the map and they differ,
they stand, and the map is wrong.

First written on 2026-11-12, and held against the Road Bible as main had it at commit a1aae13. Brought up to date on
2026-11-12 at 19:47Z, against the Road Bible as main has it after pull request #76 (merge 5c7a8c7).

## Where I fit

I build every feature of the Road Bible's part 3, and own every file and folder outside record/ (part 4, "Who owns
what"). I write each feature's brief (rule 2), its code, its patches and its diary entries.

## My features

All of part 3, by number:
- **Built in the Precursor steps:** 1 `crystals`, 2 `cradle`, 3 `journal`, 4 `sparkstone`, 5 `storage`,
  6 `evocations`, 7 `earmuffs`, 8 `machines`, 9 `mysteries`. Their nine briefs were merged in one batch by pull
  request #55 on 2026-11-12 at 18:29Z, which settled them (rule 2); a change to any of them is a PR of its own.
- **Step 1:** 10 `duets`.
- **Step 2:** 11 `purity`, 12 `exposure`, 13 `reactions`, 14 `strain`.
- **Step 3:** 15 `beams`, 16 `ascendant`, 17 `goodsbeams`, 18 `supermedia`, 19 `kilns`.
- **Step 4:** 20 `vehicles`, 21 `flight`.
- **Step 5:** 22 `teaching`, 23 `sovereign`, 24 `baleful`, 25 `balestorms`.
- **Step 6:** 26 `ruins`.
- **Step 7:** no feature; its work goes by changes to the briefs of the features concerned and the patches that
  follow them.

And the shared files of part 4: `Lemuria/Core/`, the project and build files, `scripts/`, the checks workflow,
`README.md` and `CLAUDE.md`, the general lang file, `tests/smoke/core.*`, the licence and the editor settings.

## What I need from other threads

- **The coordinator, and its record thread.** Each entry of rulings.md and player-diary.md that concerns my features,
  the owner's answers to the questions my briefs mark "for the owner", and the wakes of the review thread and the
  inquisitor that my PRs call for (rules 6 and 7). Word when a change to the Road Bible merges.
- **The review thread.** Its report on each brief and each change to a brief before it merges (rule 6 a), on this
  map (rule 6 f), and its findings on my code.
- **The planning thread.** Part 3 changed first wherever a brief or a build shows that a step is cut wrongly or a
  feature's folders are named wrongly.
- **The precursor.** What the old paper says, with the place, where a brief rests on it and I cannot read it myself.
- **The inquisitor,** once there is one: the verdict on each patch I ask for (rule 7).

## What other threads need from me

- **The review thread.** Each brief and each change to a brief as a PR of its own, ready, with word to the coordinator
  and the record thread (rule 6; rules version 6). The corrections of the findings I own.
- **The coordinator and the record thread.** Since rules version 6, both get the same word when a PR is ready for a
  wake or for the owner's merge, with its number, commit and check, and when a blocker is corrected or a verdict is
  asked for: the record thread does the waking.
  The repair of main first, whenever main is broken by code I own (rule 4).
- **The owner.** Each feature PR described so that it can be merged from its description (rule 3), and a zip of any
  PR on request.
- **The record thread.** Main in a state that builds, so that a step's zip can be made from it.

## My plan

This part is mine, and I rewrite it when the work changes.

1. **Done on 2026-11-12:** README.md and CLAUDE.md pointed at the Commandments (#51); the nine briefs (#55) and
   their first change (#72, G59); the checks workflow's skip for record-only pull requests (#59); the chance failures
   of the smoke test (#61, #62, G43); the ruled fixes G13 with G14 (#75) and G15 (#78); the chisel's draw (#81, G57);
   and R9 for every block used by hand (#84, #87, #88, #92; G16).
2. **G58 and G61,** which the owner said to fix (R27): the change to the briefs of features 8 and 6 first (#96, read
   by the review thread, merged by the owner), then the code as patches to features 6 and 8.
3. **G68,** a network across a claim's edge: no code before the owner's word, fix or decline.
4. **The three compiler warnings** (G11) go with the first patch that touches their files.
5. **Step 1, the duets (feature 10): done.** Its brief (#135), its code (#156) and the briefs' markers (#159);
   step 1 passed its gate on 2026-11-12 at main 74b1c9b.
6. **R36 to R41 and G96:** the change to the briefs of features 3, 6, 8, 9 and 10 (#182, report G103), merged by the
   owner after the planning thread's standing rule 12 (#184). Then its code as one patch to those features' folders
   (rule 4: a change that follows a brief change, verdict "out", the owner's merge): Memory's rod click and blind
   swap, the drain's two crystals, the Mysteries' price by hand and in machines, the machine's Bloom without the daily
   limit, the three Journal pages, and a restart check of a panned block. Its diary logs whether a turn in which a
   crystal can do nothing by its form (a Glittering crystal in a Scatter) counts as worked for a blind swap.
7. **Steps 2 to 7** one feature at a time, each brief before its code, in the order I set here once the patch of
   item 6 is up.
