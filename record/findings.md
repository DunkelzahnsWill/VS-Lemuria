# Findings

Entries G1, G2 and so on, as the Commandments set out under "The record" (record/COMMANDMENTS.txt). Written only by
the review thread ("the Cynical Critic", or "Glowie"). This file is also the review thread's diary (rule 5).

**How to read it.**
- Every entry has a number, and new entries go at the end. An answer cites the number: the owner's answers go in
  rulings.md, a thread's answers in its own diary.
- A **finding** has a rank (blocker, flaw or note, as rule 6 defines them), an owning thread and a status. A
  **diary** entry or a **report** has no rank.
- An entry is not rewritten once it is on main. The one exception is a finding's status, which is kept up to date
  and cites the entry that changed it.
- The newest report or "Next:" entry lists every finding still open.

## G1. Diary: the review thread starts, and what it read

**Written.** 2026-11-12 04:36Z.

The coordinator started this thread at 04:14Z, gave it the role name of the review thread, and woke it under rule 6
(e) for PR #13. This is my first entry, so there is no "Next:" entry to start from.

**What I read before anything else (rule 5, rule 6).**
- The Commandments, version 2, in full, from record/COMMANDMENTS.txt on main (sha256 918dd508…4a5b02, the same
  bytes as the file the owner uploaded).
- rulings.md, every entry, R1 to R18. There is no Road Bible, no brief, no map and no player-diary.md yet, and this
  file did not exist.
- Every diary entry on main at commit a83a83a: the planning thread's seven (03:02Z to 04:06Z) and the four deputy's
  entries in the coordinator's diary. Also the planning thread's entry of 04:07Z, which rides in PR #13.
- The planning thread's own exchange with the owner in its thread, to hold the rulings against the owner's words as
  the owner typed or tapped them.
- Outside the record: the old audit's diary, report and patch file (R7), and the code on main.

**The rulings against the owner's own words.** R1 to R18 were all recorded by one thread, the planning thread as
deputy, and PR #13 is that thread's own. Rule 6 has me read the record and not an author's account of it, so I
compared each with its source:
- R1 to R11 match the owner's message of 03:18Z, line for line.
- R14, R15 and R16 match the three cards and the options the owner tapped at 03:48Z and 03:49Z.
- R17 matches the planning thread's message of 03:55Z and the owner's answer of 03:57Z.
- R18 matches the owner's messages of 03:59Z and 04:02Z and the planning thread's reply between them.

I found no difference.

**Decisions about this file.**
- Every entry gets a G number, reports and diary entries too. Why: the rules have a PR's description cite my report
  and a thread's answer cite the entry it answers, and a number is the plainest thing to cite. Why not number the
  findings only: "entries G1, G2 and so on" reads as every entry.
- My record-only PRs go from the branch I was given (claude/project-thread-4zobvq), one at a time. R16 lets me use
  more branches of my own once two PRs must be open together.

Read up to: rulings.md R18, player-diary.md none, findings.md none.

## G2. Report under rule 6 (e): the change to the rules that adds the precursor (PR #13 at commit 6211973)

**Written.** 2026-11-12 04:36Z. Woken by the coordinator at 04:14Z.

**What I read.** PR #13, "Rules: version 3 adds the precursor to the cast", branch rules/version-3, at commit
6211973eaa1a3f4a236635a564102c98ebf6b7bd, against main at a83a83a. Its check "build" was green on that commit at
04:13Z. By rule 4 a later commit voids this report, unless it only brings main into the branch or adds only the
author's own diary entry. Editing the PR's description is not a commit and voids nothing.

**The short of it.** No blocker. Nothing here asks the owner to wait, and nothing asks for a change to PR #13. The
change does what R15 and R17 order and nothing else, and I found no sentence of the rules that it contradicts. R15
and R17 already bind (rule 1: a ruling that changes the rules binds from its entry), so the merge only brings the
file into line with what binds today. On what the change leaves open I raise three flaws (G4, G5, G7) and two notes
(G3, G6). They are for the Road Bible and for threads.

**What the change is, read from the diff.**
- record/COMMANDMENTS.txt differs in three lines. "version 2" becomes "version 3". The line under it becomes
  "Ratified 2026-11-12. Replaces version 2 (2026-11-11)." One line is added to the cast's fourth tier, after the
  inquisitor's. No other line of the rules differs.
- record/diary/optimist-visionary/2026-11-12T0407Z.md is new: the planning thread's entry for this PR (rule 5).
- Nothing outside record/ changes.

**Held against the owner's word.**
- The added paragraph is, character for character, the paragraph in the planning thread's message of 03:55Z, to
  which the owner answered "Yes, go ahead." at 03:57Z (R17). I compared the file with that message in the thread
  itself and with R17's copy of it. All three are the same.
- That message told the owner which words were the planning thread's own: "the tier, the answering of questions and
  'the record stands'". The paragraph holds more that the owner's card did not give: the old paper defined by a
  date, the shared list's part in it, "changes none of it", "builds nothing and decides nothing", and its last
  sentence. The owner had the whole paragraph in front of them when they said yes, so the yes covers all of it. I
  say this so that nobody later takes those words for the owner's own.
- The way it went (rule 4): written by the planning thread at the owner's word, its diary entry in the PR, the
  version up by one, and not assigned to the owner before this report. All as the rule has it.

**Does it contradict another rule?** I read the paragraph against every place that names the cast or leans on it.
- Rule 7's "It stands on the fourth tier with the builders" is still true of the inquisitor.
- "Outside record/, a thread changes only the folders and files it owns under the Road Bible" is a limit, and
  "changes none of it" is a tighter limit for one thread. They do not disagree.
- Rule 5 (a diary and a map for each thread) and rule 6 (f) (my wake at a first map) take in the precursor as they
  take in the inquisitor. Nothing more is needed.
- "A thread's answers go in its own diary" (the record): the paragraph says the same of the precursor. Rule 1 has
  an outcome between threads "whose decisions touch each other" go into rulings.md before either acts on it. The
  precursor decides nothing, so as I read it an answer of its is no such outcome, and its diary is enough.
- A blocker (rule 6) stops features, briefs, experiments, patches, a prototype and changes to the Road Bible. The
  precursor does none of them, so it needs no place among the threads a blocker never holds.
- The inquisitor and I may read "every diary, log, journal, map and book, and any other writing". The paragraph
  makes the precursor the keeper of the old paper, not its only reader. I go on reading the old paper myself (rule
  6: never an author's summary), and so may any thread.

**The counter-view (rule 6 e).**
1. The role may cost more than it gives, and its answers can pass for rulings: G3.
2. What "the old paper" is rests on a date the paragraph does not give and a list that does not exist yet: G4.
3. Nothing shows that the old paper is "as it was left": G5.
4. "Precursor" now means three things in this project: G6.
5. There will be two texts called version 3, this project's and the sister project's. The owner has spoken on it
   (R18), so I raise no finding. When the two are brought into line, that change comes to me like any other.
6. The date in the head is the day of R17 by UTC, as the PR's diary entry explains. The rules do not say which day
   "Ratified" means. If the owner merges on a later day, the head will not show the day of the merge. I hold
   nothing on it.

**Code this change would make wrong (rule 4).** None.
- I searched everything outside record/ for a role's name, for the places the old paper lies, and for anything that
  reads or writes them. The change adds a thread that owns no code. No code, script or check on main names a role
  or touches the old paper.
- The only hits are words, not code: CLAUDE.md lines 4 and 22, and README.md line 8. Version 2 made them stale, not
  this change, and R10 already gives their rewrite to the builder. Rule 4's list is for code, and for what this
  change breaks, so they are not on it. They are finding G7.
- "precursor crystals" in Lemuria/Crystals/CrystalsModSystem.cs and tests/smoke/crystals.expect is the design's
  word for the crystals found in ruin loot. This change does not make it wrong (G6).

**The working files, read for the first time (rule 6).** Apart from this change: the coordinator's diary holds no
entry of the coordinator's own (G8); R14 passes the Precursor steps by pointing (G9); and old step numbers live on
in the code's comments (G10). The rulings match the owner's words (G1).

**One correction to the PR's description, which needs no commit.** It says ./build.sh passed with "0 warnings, 0
errors". On the same code I get three compiler warnings (G11). The "0 Warning(s)" line in the output belongs to
the clean step that runs first. It does not touch this change, which holds no code.

**For the planning thread.** Cite this entry in PR #13's description as the review thread's report G2 in
record/findings.md, then assign the PR to the owner (rule 4). The branch merges cleanly as it stands. If you bring
main into it, this report still stands.

**For the owner.** Nothing here asks you to wait or to change PR #13. You may decline any flaw below in
rulings.md.

**Open after this report.** Flaws: G4, G5, G7, G8. Notes, which need no answer: G3, G6, G9, G10, G11. No blocker.
The old audit's open items follow from G12.

## G3. Note: the counter-view to having a precursor at all

**Rank.** Note. **Owning thread.** The coordinator, for the owner. **Raised in.** G2. **Status.** Needs no answer.

Every other thread in the cast makes something or decides something. The precursor does neither: it "builds nothing
and decides nothing" and "changes none of it". Its one duty is to answer what the old paper says, cite the place
and write the answer in its diary.

- **What it costs.** One more thread to start and to keep in step, a diary and a map (rule 5), my wake at its first
  map (rule 6 f), and for each answer a record-only PR with a run of the checks, about two and a half minutes.
- **What any thread can already do.** Open the old paper itself. I read the old audit's diary and report for this
  very report, and the planning thread is reading the old notes for the Road Bible. R8 had already made the old
  paper read-only history, linked from the Road Bible.
- **What the role adds to R8.** A keeper by name, so that no builder takes the old notes for its own to tidy.
  Answers on the record with their place cited, which a brief written under R3 can point to. And the tie-break
  "where it and the record differ, the record stands".
- **Where it can go wrong.** An answer is a summary, and the old paper was revised many times: the old planning
  diary runs to 176 KB, and the old audit's report alone stands at revision 43. To say "what the paper says" the
  precursor must often choose which place is the last word, and a builder may take that choice for a ruling. The
  paragraph's guards are "decides nothing" and "cites the place". They work only if an answer quotes the paper's
  own words, gives the link and the revision, and says so plainly when the paper says two things or nothing.
- **What "the record stands" brings with it.** R3 has each built feature's brief written from its signed-off note.
  Once such a brief merges, it outranks the note it was copied from, the one the owner played and signed off, slips
  of copying included. Guarding against that is my work when those briefs come to me (rule 6 a): I read each
  against its note and the code, not against the precursor's account of the note.

The owner asked for this thread in their own words (R5) and chose its office on a card (R15), so this holds
nothing. The paragraph does not call the precursor a standing thread. As I read it, the coordinator may start it
when the first question for it comes, which keeps the cost near nothing if the role proves idle.

## G4. Flaw: what "the old paper" is rests on a date the rules do not give and a list that does not exist yet

**Rank.** Flaw, naming step 1 (the duets, R14). **Owning thread.** The planning thread. **Raised in.** G2.
**Status.** Open.

The paragraph defines the old paper as "what was written for that project before" these rules took force in it,
gives three examples, and has the Road Bible's shared list name it and say where it lies. That leaves three things
open. Until the shared list settles them, a thread that must know whether a file is old paper would have to read
the rules for itself, and rule 1 never lets a thread choose a reading of the rules.

1. **The moment.** The record offers four: the head's "Ratified 2026-11-11"; the owner's word in the project chat,
   Nov 12 03:00Z; the owner's change to the project instructions, 03:16Z (the planning thread's entry of 03:31Z);
   and the rules reaching main in PR #11, 03:46Z. Things fall between them: the hand-over plan that R1 to R11 quote
   was put to the owner at 03:06Z, and PRs #9 and #10 merged at 03:47Z under the old rules (R4).
2. **Whether the list is the whole of it.** If something written before that moment is not on the list, is it old
   paper that no thread may change, or not old paper at all? "Names it" reads both ways.
3. **What lies beside it.** The places the old paper lies also hold things that were "written for that project
   before then" and are still in use:
   - in the project's shared folder: the art and sound generator scripts (art-sound/lemuria-step1 to
     lemuria-step5a), the play-test zips and their playtest.md lists (lemuria-playtest/), the old audit's unbuilt
     test scenes (lemuria-critic/unfinished-tests.patch, which G13 and G15 need) and the owner's scratchpad
     (owners-scratchpad.md);
   - the project memory, which every thread still writes;
   - in the repo: README.md and CLAUDE.md, which R10 has the builder rewrite, and the code itself.

   A keeper that "owns no code" should not be handed the generator scripts or the patch file. And mason cannot
   change them unless the list gives them to mason (the cast: "Outside record/, a thread changes only the folders
   and files it owns under the Road Bible").

**To correct it.** No change to PR #13 is needed. The first Road Bible's shared list:
- gives the moment for Lemuria, to the minute;
- names each piece of old paper with its place (a Claude Doc by its link, a folder by its path), and says that what
  it does not name is not old paper;
- gives each thing under 3 to the precursor, to another thread or to no thread, by name.

I read for all three when I am woken for the Road Bible (rule 6 a).

**Why not a blocker.** R5, R8 and R15 already name the heart of it: the old notes, the roadmap and the old diaries.
Nothing is built on the edges yet, and the book that cures it is being written now.

**For the coordinator until the Road Bible merges.** A precursor started before then has only what R8 and R15 name
to go by, and should take nothing else for old paper.

## G5. Flaw: nothing shows that the old paper is "as it was left"

**Rank.** Flaw, naming step 1. **Owning thread.** The precursor. Until one is started, the coordinator.
**Raised in.** G2. **Status.** Open.

The paragraph has the precursor hold the old paper "as it was left". Nothing records how it was left, and both
places it lies can still be written by any thread:
- **Claude Docs.** When I opened the old audit's report today, the tool told me that my own calls could edit it. It
  stands at revision 43.
- **The project's shared folder.** Every thread can write there, and the last write wins. CLAUDE.md on main still
  sends every thread's diary to the very folder that holds the old diaries (G7).

So a later reader cannot tell an old note that was left this way from one that was changed last week, and "where it
and the record differ" turns into an argument about what the paper said.

**To correct it.** The precursor's first diary entry lists every piece of old paper with the state it took it over
in: a Claude Doc's revision number, a file's sha256. A difference found later is then a finding and not an
argument. It costs one entry. I look for it when I am woken for the precursor's first map (rule 6 f).

**Two states I can give now**, as I read the files for G12:
- /mnt/project-files/diary/lemuria-critic.md: sha256
  0aaefa81d4951fbc90122069727a0c241101ba37da3d9cbe927936906985337a.
- /mnt/project-files/lemuria-critic/unfinished-tests.patch: sha256
  4fab1ae707ae41f25662db8da66d7242b686076af6702cf74625676522a6c498.

## G6. Note: "precursor" now means three things in this project

**Rank.** Note. **Owning thread.** The planning thread. **Raised in.** G2. **Status.** Needs no answer.

1. A thread: the precursor, the keeper of the old paper (this change).
2. Steps: the "Precursor steps", old steps 0 to 4 and step 5 part 1 (R2, R14).
3. Crystals: in the design and in the code, "precursor crystals" are the crystals found in ruin loot
   (Lemuria/Crystals/CrystalsModSystem.cs lines 19 and 225, tests/smoke/crystals.expect lines 4 and 8, and a heading
   of the old step 2 note as the old audit's report quotes it). R12 noticed this when the first two came up.

The first two are the owner's own words and stay. One sentence in the Road Bible's concept that tells the three
apart would let a brief for the crystals keep "precursor crystals" without a reader taking them for the
precursor's. I read for it at rule 6 (a). It holds nothing.

## G7. Flaw: CLAUDE.md and README.md still give the old rules, and every thread is handed CLAUDE.md as binding

**Rank.** Flaw, naming step 1. **Owning thread.** Mason, the one builder (R5, R10, R15). Mason is not started yet,
so the coordinator holds this for it. **Raised in.** G2. **Status.** Open.

On main at a83a83a:
- CLAUDE.md lines 8 to 23 are version 1's five rules, under the heading "The Commandments (the owner's standing
  rules)". Lines 18 to 20 let a small fix merge itself on a green check, with no verdict from an inquisitor. Line
  22 puts each thread's diary at /mnt/project-files/diary/<thread>.md.
- CLAUDE.md line 4 says "The design is the pre-production document". README.md line 8 says "The design lives in the
  pre-production document", and its line 6 counts the built steps by their old numbers (R14).

Version 2 made these stale, and R10 already has the builder that owns the two files rewrite them in its first PR.
Two things make it a finding as well:
1. The platform gives CLAUDE.md to every thread that starts, as instructions to follow exactly. I was given it at
   this wake. I followed record/COMMANDMENTS.txt, because the project instructions and the owner's word of 03:00Z
   say so. A new thread should not have to work that out: it starts with two rule texts that disagree on where a
   diary lives and on when a fix may merge itself.
2. With this change, the folder that line 22 sends diaries to is where the old paper lies, which the precursor is
   to hold as it was left. And the document that line 4 calls "the design" becomes history, which the record
   outranks.

**To correct it.** R10's rewrite, merged.

**For the coordinator until then.** Tell each thread you start that record/COMMANDMENTS.txt outranks CLAUDE.md's
"Commandments" section and its line 4.

**Why it is not on the list of code this change makes wrong (G2).** Neither file is code, and it was version 2, not
this change, that made them stale. On that list each part would become a blocker on the day PR #13 merges, owned by
a builder that does not exist yet.

## G8. Flaw: the coordinator's diary holds no entry of the coordinator's own

**Rank.** Flaw, naming step 1. **Owning thread.** The coordinator. **Raised in.** G2. **Status.** Open.

record/diary/coordinator/ on main at a83a83a holds four files, each headed "a deputy's entry, written by the
planning thread". The coordinator has been at work under these rules since the owner brought them in at 03:00Z: it
answered the planning thread at 03:26Z (that thread's entry of 03:31Z) and started this thread at 04:14Z.

- Rule 5 has every thread keep its diary, and says: "Its first diary entry after starting says what it read up to."
- The cast has a coordinator send on every entry a deputy made, and make the wakes those entries call for, "unless
  the record shows it done". The record shows none of it done. Some of it was: R1 to R18 came to me with this wake.
- The cast has me read the coordinator's record with every other thread's. There is none of its own to read.

**To correct it.** A first entry by the coordinator: what it read up to, which of R1 to R18 it has sent to which
thread, and which wakes it has made. I raise nothing on missing maps while there is no Road Bible for a map to
point to.

## G9. Note: R14 passes the Precursor steps and accepts their PRs by pointing, not in the owner's words

**Rank.** Note. **Owning thread.** The coordinator, who keeps rulings.md. It concerns the inquisitor.
**Raised in.** G2. **Status.** Needs no answer.

Rule 7 lets the owner pass a step "by a sign-off recorded in rulings.md". Its third test at a step gate asks whether
each merged PR went the way rule 4 sets for its kind or was "accepted by the owner in rulings.md". For old steps 0
to 4 and step 5 part 1, and for PRs #1 to #10, both rest on R14. R14 is honest about what it holds, which is this:
the owner tapped "Start again at step 1"; that option says the Precursor steps "pass the same way"; and the words it
points to, "passed by your word with their merged PRs accepted", stand in the option the owner did not tap. That
the PRs meant are #1 to #10 is in the planning thread's diary (03:54Z), not in rulings.md.

I read R14 as the planning thread does. I note it because step 1's gate will lean on it. If the inquisitor wants
more than a pointer then, one line from the owner in rulings.md settles it. Nothing is asked for now.

## G10. Note: old step numbers live on in the code's comments

**Rank.** Note. **Owning thread.** The planning thread, for the Road Bible's table of old numbers. It concerns
mason. **Raised in.** G2. **Status.** Needs no answer.

R14 starts the numbered steps again at 1, and the planning thread's entry of 03:54Z has the Road Bible say that a
step number in an old note is an old number. Old numbers are not only in old notes. On main at a83a83a, 137 comment
lines in the C# code and 33 in tests/smoke/ name a step by its old number ("step 4 note, Sockets and the Complexity
Rating"; "# Step 2: the evolution ring, repair and refining"), and 103 of those lines cite an old note by name.
None of it is shown to the player. Once step 2 means what old step 6 meant, a comment that says "step 2" reads wrongly
to anyone who has only the Road Bible.

One sentence in the Road Bible, that a step number in the code's comments and in the smoke tests is an old number
too, covers it. Rewriting 170 comments at once is not worth a PR: mason can reword them as it works in each file.

## G11. Note: the build on main prints three compiler warnings

**Rank.** Note. **Owning thread.** Mason (held by the coordinator until mason starts). **Raised in.** G2.
**Status.** Needs no answer.

./build.sh on main at a83a83a succeeds and prints three warnings from the compiler. All three are about a value
that may be null, and all are in Lemuria/Machines: Battery.cs line 366 (CS8604), BlockMachine.cs line 156 and
MachinesTests.cs line 80 (both CS8625). None looks like a fault to me. The "0 Warning(s)" in the output is the line
of the clean step that runs first, and is easily taken for the build's own. The smoke test passes and does not look
at compiler warnings.

## G12. Diary: the old audit's open items carried in (R7)

**Written.** 2026-11-12 04:36Z.

R7 has me carry the old audit's open items into this file as my first findings, "each owned by the builder".

**What I read.** The old audit thread's diary, /mnt/project-files/diary/lemuria-critic.md, to its last entry of Nov
12 02:55Z and its "Next:" list. Its report, the Claude Doc "Lemuria audit: steps 0 to 5"
(https://claude.ai/artifact/CGQS5mi4JxVGV53vCgMKLA), all sixteen findings, at revision 43. The patch file
/mnt/project-files/lemuria-critic/unfinished-tests.patch. Then the code on main at a83a83a for G13 to G16, so that
they say what main holds today and not what the old thread remembered. I ran none of it: those four are read from
the code, and G17 to G20 are carried as the old audit left them.

**What is open, and where it went.**

| The old audit's item | Here |
| --- | --- |
| A struck Gather block forgets its strike on a restart (under old finding 10) | G13 |
| The same gap for the hour after a death, from PR #9 (old diary only) | G14 |
| Old finding 16: a Fire cast at a block that stays cold still pays | G15 |
| Old finding 9's open question, answered by R9: every block used by hand | G16 |
| Old finding 11: two network searches for each working ("Noted only") | G17 |
| Old finding 15: the shelf's bucket milks by default amounts ("Noted only") | G18 |
| Not yet shown in a run: a pile the Control Shelf starts may not light | G19 |
| Play checks the cloud could not make | G20 |

**What I did not carry, and why.**
- Old findings 1 to 4, 9, 10 and 12 to 14 were fixed in PR #10, and 5 to 8 in PR #9. Both PRs are merged (R4) and
  accepted (R14). I did not audit those fixes again.
- The old "Next:" list asked for PR #10's description and the old report to be brought up to date once the two
  unbuilt fixes land. PR #10 is merged, and the report is old paper now, to be left as it is.
- The old thread waited for the owner's word on the reading behind old finding 16. R7 answers it: that fix waits
  for the owner's merge, and the merge is the word.

**Decisions.**
- Owning thread of each: mason, the one builder (R5, R7, R15). Mason is not started. Rule 6 sends a finding to the
  janitor when its originator "does not exist". I do not read these that way: R7 gives them to the builder in the
  owner's own answer, and rule 6 leaves it to the coordinator to decide whether the originator can do the work. The
  coordinator holds them and sends them on when mason starts.
- Rank: flaws for the four that change what the code does, notes for the rest. Why not blockers: a blocker would
  keep mason from every brief until it is closed (rule 6), and R3 has the briefs come first. Each flaw names step
  1, the earliest step that has not passed its gate (R14 passes the Precursor steps).
- How the four are fixed is for R7, rule 4 and the inquisitor, not for me. One thing I will read for when the
  built features' briefs come to me (R3, rule 6 a): each brief must say what its fix makes true (the strike is
  kept through a restart, a cast that lights nothing is free, every block used by hand opens). A brief copied from
  the code as it stands would turn the fault into the design, and the fix would then make the brief untrue.

## G13. Flaw: a struck Gather block forgets its strike through a restart

**Rank.** Flaw, naming step 1. **Owning thread.** Mason (held by the coordinator until mason starts).
**Carried from.** The old audit, under its finding 10 ("Not fixed yet"). **Status.** Open.

- **The code, main at a83a83a.** Lemuria/Machines/BlockEntityMachine.cs keeps the strike in `skyBoostUntil` (line
  55, set at line 445, read at line 411). `ToTreeAttributes` and `FromTreeAttributes` (lines 619 to 654) neither
  write it nor read it back. A standing crystal and a cradle save theirs (BlockEntityCrystal.cs lines 320 and 335,
  BlockEntityCradle.cs lines 796 and 822).
- **What the player loses.** The rest of a struck Gather block's six boosted hours when its chunk unloads or the
  server restarts: "at most about twelve real minutes of boost" (the old report).
- **Why it is a fault and not the design.** The old planning thread ruled on Nov 10 that it is a gap in the code:
  step 4's note gives a Gather block the strike "as it does standing ones" (as the old report quotes it). That note
  is old paper now, so the brief for this feature has to say it (G12).
- **What is left of the old work.** The old diary's last entry gives the fix line by line. The patch file holds
  its test (`/lemuria machines test gatherstrike`, then `struckkept` after the restart). The patch still applies
  to main at a83a83a. By the old diary it builds, and it has never been run.

## G14. Flaw: a Gather block forgets the hour after a death through a restart

**Rank.** Flaw, naming step 1. **Owning thread.** Mason (held by the coordinator until mason starts).
**Carried from.** The old audit's diary, last entry. It is not in the old report. **Status.** Open.

The same gap as G13, one line down. `deathBoostUntil` (BlockEntityMachine.cs line 56, set at line 454, read at line
415) came in with PR #9 and is not saved either, while a standing Death crystal and a cradle save theirs
(BlockEntityCrystal.cs lines 321 and 336, BlockEntityCradle.cs lines 797 and 823). The old audit's last entry says
the step 5 build thread was never told. The patch file holds no test for it. One patch can carry G13 and G14: both
sit in the same two functions of one file.

## G15. Flaw: a Fire cast at a block that takes the flame and stays cold still pays (old finding 16)

**Rank.** Flaw, naming step 1. **Owning thread.** Mason (held by the coordinator until mason starts).
**Carried from.** The old audit's finding 16 ("Noted only", then "Not fixed yet"). **Status.** Open.

- **The code, main at a83a83a.** `TryIgnite` in Lemuria/Evocations/Effects.cs (from line 306) hands the block the
  flame and answers "lit" without asking whether it caught. A loaded bloomery with no chimney takes the flame and
  stays cold, so a cast by hand or from a Fire Focus pays for nothing, each time. `Light` (from line 344), the
  Control Shelf's firestarter, already asks again and pays only for what caught.
- **The rule behind it.** The old planning thread's reading of step 3's note, under Ignite: a cast that finds
  nothing it can act on costs nothing. By the old audit's diary it was marked there as planning's own reading and
  told to the owner.
- **The owner's word.** R7: "Finding 16 changes what you notice in play, so it waits for your merge." So this fix
  is not one for auto-merge, and the owner's merge of it is also the owner's word on that reading.
- **What is left of the old work.** The old diary's last entry gives the design and two traps: a firepit only
  smoulders until its next tick and would be misread as cold, and a pile of firewood offers to be lit even while
  it burns. The patch file holds two tests (`/lemuria evocations test misfire` and `burningpile`), never run.

## G16. Flaw: the owner's ruling R9 is not built: with the claims switch off only racks and jars open in a claim

**Rank.** Flaw, naming step 1. **Owning thread.** Mason (held by the coordinator until mason starts).
**Carried from.** The old audit's finding 9, "Still with you", since answered by R9. **Status.** Open.

- **The ruling.** R9, the owner: "Every feature reads the same switch". With the claims switch off, every Lemuria
  block used by hand opens inside another player's claim.
- **The code, main at a83a83a.** The game tests a claim itself before it calls a block. Only Storage answers that
  test (Lemuria/Storage/StorageModSystem.cs line 35, StorageClaims.cs): for the crystal rack, a crystal jar set
  down, and an unfired crock about to become a jar. The other blocks used by hand ask `LemuriaClaims.TryAccess`,
  which gets no say on a right-click the game has already refused (the comment at Lemuria/Core/LemuriaClaims.cs
  lines 36 and 37 says so), and the cradle's folder holds no claims code at all. So with the switch off, a
  stranger's right-click on a cradle, a placed crystal, a socket, the Control Shelf or Memory inside a claim is
  still refused.
- **How it reaches code.** Through the briefs first (rule 1: a ruling that changes what a brief says changes the
  brief before any code follows). No brief exists yet. The briefs of the built features (R3) must each say it for
  their own blocks used by hand, and must say which blocks those are: R9's question named six, and step 5 part 1
  has added at least the Glint since. I read for it at rule 6 (a).
- **What stays.** Breaking and building inside a claim stay as the game rules them, as PR #10 built for racks and
  jars after the owner's card of Nov 10.

## G17. Note: every paid working searches its whole network twice (old finding 11)

**Rank.** Note. **Owning thread.** Mason (held by the coordinator until mason starts). **Carried from.** The old
audit's finding 11 ("Noted only"). **Status.** Needs no answer.

Before a machine works it checks that its network can pay, and once it acts it pays. Each step walks the whole
network again, up to 4,096 blocks. The old report's example: 20 Focus blocks with four crystals each on a 500-block
network make about 50,000 lookups a second, against step 4's promise that "a hundred machines cost under 2,000
block reads a second". No play-test base comes near it. It waits until bases grow, or until a brief sets a cost
line. I did not read this code again today.

## G18. Note: the Control Shelf's bucket milks by the game's default amounts (old finding 15)

**Rank.** Note. **Owning thread.** Mason (held by the coordinator until mason starts). **Carried from.** The old
audit's finding 15 ("Noted only"). **Status.** Needs no answer.

A bucket on the shelf milks each animal by the game's default rule, whatever the animal sets for itself. The game's
own goats and sheep use the defaults, so nothing differs today. Only another mod's animal would be milked by the
wrong amounts. The old planning thread: leave it until a compatibility pass before version 1.0. I did not read this
code again today.

## G19. Note: not shown in a run: a pile of firewood that a Control Shelf starts may not light

**Rank.** Note. **Owning thread.** Mason (held by the coordinator until mason starts). **Carried from.** The old
audit's diary, last entry. **Status.** Needs no answer. It becomes a finding against the shelf if a run shows it.

The old audit read in the game's code that a pile built by code cannot be set alight until its chunk reloads or a
player adds or takes a piece, because the game works out the pile's burn time only then. If that holds, a pile that
a Control Shelf stacks cannot be lit, while step 4 part 2's note has the shelf stack "as your clicks would" (as the
old diary quotes it). Nobody has shown it in a run, and I have not either.

## G20. Note: play checks from the old audit that the cloud could not make

**Rank.** Note. **Owning thread.** Mason, for the "what to try" lists of the briefs (R3); held by the coordinator
until mason starts. **Carried from.** The old report, "Play-test check" under its fixes. **Status.** Needs no
answer.

PR #10 is merged and accepted (R4, R14). The record does not say whether these were tried in play:
- A Fire Focus aimed at a forge with coal in it lights it (old fix 1).
- A shelf with a do-nothing item in its fourth place still draws, with a dark fourth plug (old fix 12). The old
  audit read this one from the client's code and never saw it drawn.
- A Dead Socket set in after the fertiliser ran out sends the whole shelf Dormant (old fix 13).
- A firestarter on a shelf lights a forge in reach and leaves a pile of firewood alone (old fix 14).
- With the claims switch off, a rack or a jar inside someone else's claim opens (old fix 9). This needs a claim
  that is not the owner's, so a multiplayer session.

They belong on the "what to try" lists that the briefs will carry.

## G21. Diary: halt after the report on PR #13

**Written.** 2026-11-12 04:36Z.

The work in hand is complete: the report G2 with its findings G3 to G11, and the old audit's open items, G12 to
G20.

Read up to: rulings.md R18, player-diary.md none (the file does not exist), findings.md G21.

Next:
- **Unfinished:** nothing in hand. G19 has not been tried in a run.
- **Where it is:** this file, sent to main by a record-only PR from the branch claude/project-thread-4zobvq.
- **Waiting for:** the coordinator's next wake. I expect, in some order:
  - the first Road Bible (rule 6 a), where I read for G4, G6 and G10, and hold its steps, its table of old numbers
    and its unplaced earmarks against the old paper (R8, R14);
  - the briefs of the built features (R3, rule 6 a), where I read for G12's last point, G16 and G20;
  - the first maps (rule 6 f), and G5 at the precursor's;
  - a new report on PR #13, only if a commit voids G2.
- **Open findings:** flaws G4 (the planning thread), G5 (the precursor; the coordinator until it starts), G7 and
  G13 to G16 (mason; held by the coordinator until it starts) and G8 (the coordinator). No blocker is open.
- **Read up to:** rulings.md R18, player-diary.md none, findings.md G21.
