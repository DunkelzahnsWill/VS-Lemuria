# The coordinator's map

Written 18:24Z on 2026-11-12 and corrected 18:35Z, after I read the rules (version 5) and the Road Bible whole, as the record thread sent them from main at 8897ca7. It replaces my first map, which was written before I had read either whole (G53).

## The features, by number

All are mason's (Road Bible, part 4).
- Features 1 to 9 (crystals, cradle, journal, sparkstone, storage, evocations, earmuffs, machines, mysteries): built in the Precursor steps. Their nine briefs come first, in one batch (PR #55). Until the briefs merge they take only the fixes already ruled (R3).
- Step 1: feature 10, duets.
- Step 2: features 11 to 14, purity, exposure, reactions, strain.
- Step 3: features 15 to 19, beams, ascendant, goodsbeams, supermedia, kilns.
- Step 4: features 20 and 21, vehicles, flight.
- Step 5: features 22 to 25, teaching, sovereign, baleful, balestorms.
- Step 6: feature 26, ruins.
- Step 7, v1.0: no feature. The one multiplayer test is played before the step is reported done. The release on ModDB is the owner's.

## The threads I need, and that need me

- The owner: merges, rulings, play. I call the owner only when a PR is ready for the owner's merge or a rule needs the owner's reading (R22).
- The record thread: my hands in the repo (R20, R21). It writes rulings.md, player-diary.md, this map and my diary in my words, builds the zips, and sends me each new R, P and G entry and the changed text of the rules and the book.
- The planning thread: writes changes to the rules and the Road Bible. The project memory is no thread's: every thread reads and writes it (Road Bible, part 4).
- The review thread: I wake it at each of rule 6 (a) to (g) and send its findings to their owning threads and the owner.
- The inquisitor: decides the two gates of rule 7. I wake it when a thread asks a verdict on a patch and when I report a numbered step done, and I pass each signed decision to the threads it concerns.
- Mason: builds everything outside record/. Its first PR was the rewrite of README.md and CLAUDE.md (R10, PR #51), then the nine briefs (PR #55).
- The precursor: keeps the old paper. No code.
- The janitor: not started. I create it when a fix passes to it (rule 6).
Every thread needs me to carry its words to the others, to wake the review thread and the inquisitor, and to tell it when the rules or the book change.

## My plan

1. Now. The owner has merged PR #46, #45, #51, #55, #59, #61 and #62, and every thread reads the book again (rule 5). Mason writes the ruled patches (G13 with G14, G15) and a change to the briefs after the review thread's read of them. When mason asks a verdict on a patch through me, I wake the inquisitor (rule 7).
2. The nine briefs are merged (PR #55). The review thread's report on them follows as G56. Mason's change to the briefs goes by the review thread's read (rule 6 a) and the owner's merge. R9 goes into the code by the briefs, and the planning thread brings the book up to date where merged PRs have overtaken it.
3. Steps 1 to 6, each the same way. Mason puts up the briefs of the step's features; I wake the review thread (a); the owner merges. Mason builds each feature; the owner merges it. For each patch on which its thread asks a verdict through me, I wake the inquisitor: "in" merges itself, and "out", or a patch with no verdict asked, goes to the owner. When every feature of the step is merged and no PR of the step is open, I have the record thread build the zip of main with the "what to try" lists and anything known to be broken, and send it to the owner (rule 3), I report the step done, and I wake the inquisitor for the gate on the step (rule 7). Pass or hold, I then wake the review thread to read what the inquisitor found (rule 6 b), and I send its report to the owner, with the inquisitor's list of PRs that needed the owner's merge if the history cannot show whose hand merged. After a hold I wake the inquisitor again when what failed is made good. The next step starts no code, experiments aside, until the step before has passed its gate.
4. Step 7. No feature: changes to briefs and the patches that follow them, balance, the multiplayer test before I report the step done, then the gate as above. The owner releases.
5. Always. I log each question for the owner in rulings.md through the record thread and stop the threads rule 1 names. I send each R, P and G entry to the threads it concerns. Each word the owner says of play goes to player-diary.md and to the thread that owns it. If main is broken I tell the owner and name the thread that repairs it. When the rules or the book change, I ask the record thread for the changed text and read it before I act on it.
