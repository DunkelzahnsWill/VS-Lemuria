# Mason's map

Role: mason, the one builder of Lemuria (R5, R15). This folder, record/diary/mason/, holds this map and my diary.

The Road Bible (record/road-bible.md) and the settled briefs outrank this map (rule 5). Where the map and they differ,
they stand, and the map is wrong.

First written on 2026-11-12, and held against the Road Bible as main had it at commit a1aae13.

## Where I fit

I build every feature of the Road Bible's part 3, and own every file and folder outside record/ (part 4, "Who owns
what"). I write each feature's brief (rule 2), its code, its patches and its diary entries.

## My features

All of part 3, by number:
- **Built in the Precursor steps:** 1 `crystals`, 2 `cradle`, 3 `journal`, 4 `sparkstone`, 5 `storage`,
  6 `evocations`, 7 `earmuffs`, 8 `machines`, 9 `mysteries`. They have no briefs yet; I write them first (R3).
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
  (rule 6). The corrections of the findings I own.
- **The coordinator.** Word when a PR is ready for a wake or for the owner's merge, with its number, commit and check.
  The repair of main first, whenever main is broken by code I own (rule 4).
- **The owner.** Each feature PR described so that it can be merged from its description (rule 3), and a zip of any
  PR on request.
- **The record thread.** Main in a state that builds, so that a step's zip can be made from it.

## My plan

This part is mine, and I rewrite it when the work changes.

1. **README.md and CLAUDE.md** rewritten to point at the Commandments, keeping the repository rules (R10, G7, G29).
   It is my first pull request and waits for the owner's merge.
2. **The nine briefs of the built features** (R3), in one pull request, written from the signed-off notes as built,
   play-test changes included. Each says what the ruled fixes make true (G12): a struck Gather block keeps its strike
   and its hour after a death through a restart (G13, G14), a Fire cast that lights nothing costs nothing (G15), and
   with the claims switch off every Lemuria block used by hand opens in another player's claim (R9, G16). The old
   audit's play checks go on their "what to try" lists (G20). The review thread reads them (rule 6 a) and the owner
   merges them.
3. **The fixes already ruled** (R3, R7), as patches, once PR #45 has put G41's limit into part 3: G13 and G14 in one
   patch to feature 8, G15 as a patch to feature 6 that waits for the owner's merge (R7).
4. **The chance failures of the smoke test** (G43): the skip of the heavy steps for record-only pull requests in the
   checks workflow (part 4; PR #59, waiting for the owner's merge), and the two checks fixed as patches to features 8
   and 9 (PR #61, PR #62). Those two wait for the owner's merge once the briefs are merged, since until then features
   1 to 9 take only the fixes already ruled (R3), unless the owner merges them sooner (the review thread's note, G52).
   The three compiler warnings (G11) go with the first patch that touches their files.
5. **R9 for every block used by hand** (G16): once the briefs are merged, the code follows them, as patches to each
   feature concerned, which wait for the owner's merge (rule 4).
6. **Step 1, the duets (feature 10):** its brief, with the changes it needs to the machines' brief (Memory), in one
   pull request; then its code, which may carry the change to Memory with it (rule 4, R24). The brief settles the
   owner's two ideas placed in step 1 and its "what to try" list sets the aim in play.
7. **Steps 2 to 7** one feature at a time, each brief before its code, in the order I set here when step 1 has
   passed its gate.
