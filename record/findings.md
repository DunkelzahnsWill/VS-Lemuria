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
**Status.** Corrected in PR #16 (G23), and still so at its commit 84ace1d (G39). It closes when that commit merges.

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
**Raised in.** G2. **Status.** Open. G22 gives the state of each piece as I found it on 2026-11-12.

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
Answered in PR #16 at commit 722d58a (G23).

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
so the coordinator holds this for it. **Raised in.** G2. **Status.** Open. For R10's rewrite: the lines on staying
inside a feature's files should say what R24 allows (G45).

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

**Rank.** Flaw, naming step 1. **Owning thread.** The coordinator. **Raised in.** G2. **Status.** Corrected by the
coordinator's own entry of 2026-11-12 05:21Z, on main since PR #18 (G22).

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
mason. **Raised in.** G2. **Status.** Needs no answer. Answered in PR #16 at commit 722d58a
(G23), and the gap that G27 named at commit 84ace1d (G39).

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

## G22. Diary: woken for the first Road Bible, and what I read

**Written.** 2026-11-12 05:50Z.

The coordinator woke me at 05:06Z under rule 6 (a) for PR #16, the first Road Bible. I started from G21's "Next:".

**The rules.** I read version 2 in full at 05:10Z. The owner merged PR #13 at 05:18Z, so the rules on main are version
3, and the coordinator told me so (rule 4). I read the change at 05:21Z and the whole file again before I wrote this.
Version 3 on main is byte for byte the file that G2 read at commit 6211973 (sha256 58a4cf6c…5e5676a). So G2 stood at
the merge. G2 listed no code that the change makes wrong, so the merge made no blocker (rule 4).

**What changed in the record since G21 (rule 6).** Main went from 414f1d7 to 08410c3 by five PRs. I read each of them
whole.
- PR #17, merged at 05:16Z: the planning thread's halt entry of 05:08Z.
- PR #13, merged by the owner at 05:18Z: rules version 3, with the planning thread's entry of 04:07Z that G2 read.
- PR #18, merged at 05:26Z: R19, R20 and R21 in rulings.md; three entries in the coordinator's diary, among them the
  coordinator's first own (05:21Z); and the first entry of a new thread, the record thread.
- PR #19, merged at 05:30Z: the record thread put the coordinator's words in R19 to R21 back as the coordinator gave
  them.
- PR #20, merged at 05:37Z: R19's attachment line corrected (G28), with an entry in each of the two diaries.

Main still holds no Road Bible, no brief, no map and no player-diary.md. Outside record/ it is as it was at commit
c07322e.

**R19 to R21 against the owner's own words.** One thread recorded all three, so I compared each with the server's own
copy of the owner's message, as I did for R1 to R18 (G1).
- R19: the message of 03:00:29Z in the project chat. The same words.
- R20: the message of 05:18:31Z, which starts the record thread's own thread. The same words.
- R21: the message of 05:20:38Z in the project chat. I compared R21's block with the message by program: 3,113
  characters, every one the same, the curly marks included.

R21 changes the rules, so it binds from its entry (rule 1). The coordinator told me at 05:21Z that its record thread
is at hand. So from then on I send any word the owner gives me directly to the coordinator and the record thread, word
for word, and I record none of it myself unless the owner tells me to. PR #21 is the change that writes R21 into the
rules, and I am woken for it (G31).

**What the coordinator answered, in its own entry of 05:21Z.**
- G8: that entry is the coordinator's first own. It says what the coordinator did from 02:43Z on, which threads it
  started and woke, what it has not yet sent and to whom, and what it read up to. That is what G8 asked for. I mark G8
  corrected.
- G12: the coordinator decided that mason's findings (G7, G11, G13 to G20) wait for mason and go to no janitor. Rule 6
  leaves that decision to the coordinator.
- G5: the coordinator holds it for the precursor.

**The old paper as I found it.** G5 asks the precursor to record the state it takes the old paper over in. No
precursor has started, the Road Bible now names every piece, and I had every piece open for this report. So I set down
the state I saw. First the Claude Docs, opened between 05:17Z and 05:35Z:

| Claude Doc | Revision | What I read of it at this wake |
| --- | --- | --- |
| Lemuria: pre-production | 247 | The concept, the glossary, the old step list, the earmarks, the lines on claims |
| Lemuria: roadmap | 29 | Whole |
| Lemuria step 1: Find and farm | 17 | Whole |
| Lemuria step 2: The Four Elementals | 110 | Opened for its revision only |
| Lemuria step 3: Naked evocations | 185 | Opened for its revision only |
| Lemuria step 4: Machines | 114 | Opened for its revision only |
| Lemuria step 4 part 2: Products and logistics | 101 | Extend, expansion slots, the shelf |
| Lemuria step 4 part 3: Elemental batteries and Wind Probes | 54 | The owner's idea of 2026-11-08 |
| Lemuria step 5: The Four Mysteries | 86 | The duets, the Duet Module, Memory, Bloom |
| Lemuria: automation vision | 85 | The owner's earmarks of 2026-10-28 |
| Lemuria audit: steps 0 to 5 | 43 | Whole, at G12 |

The tool told me of each of these Docs that a call of mine could edit it. I changed none.

Then the project files, at 05:40Z, each with its size in bytes, the time it was last written and its sha256. The ten
old diaries in /mnt/project-files/diary/:

| File | Bytes | Written | sha256 |
| --- | --- | --- | --- |
| lemuria-preproduction.md | 180816 | 11-12 02:48Z | 81b245e9817942cc971114d1618a2821d27b4adbc49ef14b665fbfcba9fbf56f |
| lemuria-step0.md | 2961 | 10-26 23:21Z | e7eeb38df47767d96ec546e7a2ca5ac83a4d65cbea89211f3910efc2277a7d67 |
| lemuria-step1.md | 3304 | 10-27 02:09Z | b9abc1e72608c3e1995654c2a99178dded71aec01d5926e2f20010d139e7754e |
| lemuria-step2.md | 8052 | 10-27 06:59Z | f42199fdfb42d5736be84d6f52f6a2482232b07bd009e13b5e104e08b9dc8c8a |
| lemuria-step3.md | 13060 | 10-27 23:16Z | 9246922e102ce8d2dc07b601de51796a5dc950c14c706f2ed69fe2bbe5a63b6f |
| lemuria-step4.md | 10344 | 10-28 05:37Z | 54388d95eb7e8014f8af1a48d3aa0c376e8357e8b6ab465b3daf679e73d02b50 |
| lemuria-step4-part2.md | 19758 | 11-02 06:25Z | 7edbdd1aa72ddf84518bf7dfd1612a7f254cf24997a1da345615e728da934092 |
| lemuria-step4-part3.md | 6828 | 11-09 03:02Z | d72420d3b510e74d56ad36bc5e8fc5e2ab0f6c4891b1c4e694c73e709a877ee1 |
| lemuria-step5-part1.md | 8651 | 11-12 02:44Z | 4a659f2b6020930e5712389de74395a96bf7b5e3443898c112ff516a276aa356 |
| lemuria-critic.md | 26262 | 11-12 02:50Z | 0aaefa81d4951fbc90122069727a0c241101ba37da3d9cbe927936906985337a |

The seven zips and the three sheets in /mnt/project-files/lemuria-playtest/. Each folder also holds a SHA256SUMS file,
and each agrees with its zip:

| File | Bytes | Written | sha256 |
| --- | --- | --- | --- |
| step1/lemuria_0.0.1.zip | 65994 | 10-27 02:07Z | fd3ca15e7120a7475cc668ef8e21289a80792727a8ab19d862c29c559766668a |
| step2/lemuria_0.0.1.zip | 243407 | 10-27 06:47Z | b816e1496184cc6c1e8684d6f56f06833a6fb57ea0c4384fb71bf3e42a7366d2 |
| step3/lemuria_0.0.1.zip | 355530 | 10-27 23:04Z | 084bc3f05e5fec0b31c9c6139248ff2b46aefe726edf25857c902e4d83fe90ba |
| step3/evocations-effects.md | 5141 | 10-27 20:23Z | 197e66d4dfb15451568f69a7fdc9b6017c940eca1468421e279f239178f1160d |
| step4/lemuria_0.0.1.zip | 419613 | 10-28 04:59Z | 5748a2582631a43dd0a1b1f706f2457ddb975981427e3f68de9983a0558ea99c |
| step4b/lemuria_0.0.1.zip | 469672 | 11-02 06:18Z | 928e563f8c51afff2d57245f683fb0a2e6fb527f5475341f8eb34fc7bf9283e0 |
| step4c/lemuria_0.0.1.zip | 490858 | 11-08 22:56Z | 044d885c197c6a9c4983e7cf7b8c75c174e61107f1b6c17d73c0cbc35eceb97c |
| step4c/playtest.md | 3429 | 11-08 22:56Z | 63ccacd896372a0d6b229e5f1a18cf83e79100087aac556a56b67db29d523c64 |
| step5a/lemuria_0.0.1.zip | 615157 | 11-10 22:12Z | 03e8a300f9b586dc04f6455b46faf01142dbbc07b1936b9361a86aea164461bc |
| step5a/playtest.md | 2596 | 11-10 21:15Z | 8ea25a6fef5841d51c4a10740babb29fd111e1ccd40e2606c3807821ffc1e49b |

The two files G5 already gave, the old audit's diary and its patch file, are unchanged since then.

**Step numbers in my own entries.** The book has writing say "old step" for an old number from its merge on. In G1 to
G21, the step that a flaw names, "step 1's gate" in G9 and "Once step 2 means" in G10 are the book's numbers. Every
other step number there is an old number. From this entry on I write "old step" for an old number.

Read up to: rulings.md R21 as corrected by PRs #19 and #20, player-diary.md none (the file does not exist),
findings.md G21.

## G23. Report under rule 6 (a): the first Road Bible (PR #16 at commit 722d58a)

**Written.** 2026-11-12 05:50Z. Woken by the coordinator at 05:06Z.

**What I read.** PR #16, "Road Bible: the first Road Bible of Lemuria", branch claude/project-thread-20hfvu, at commit
722d58ac9ce7d923039d235a8b10972831caeba4, its one commit, on main at 414f1d7. It adds two files and changes no other:
record/road-bible.md (496 lines, sha256 8d6db51d…a0e24d17) and the planning thread's entry for it,
record/diary/optimist-visionary/2026-11-12T0442Z.md. Its check "build" was green on that commit at 05:06Z. It was not
assigned to the owner and not set to auto-merge, as rule 4 has it. Main has moved to 08410c3 since, in other files,
and the branch still merges cleanly. By rule 4 a later commit voids this report, unless it only brings main into the
branch or adds only the author's own diary entry.

I read the book whole, twice, and its entry. I held them against the rules, R1 to R21, every file on main outside
record/, the old paper (G22), the owner's scratchpad and the project files that part 4 names.

**The short of it.** No blocker. Nothing here asks the owner to wait: PR #16 may be merged as it stands. The book has
the four parts the record asks for. It carries each ruling where it belongs. I found nothing of the owner's dropped
from the old paper and nothing invented. It answers G4, G6 and G10. I raise two flaws against it, G24 (part 3) and G25
(the concept), and three notes (G26, G27, G29). The counter-view is G30. One thing is a matter of timing and not of
rank: G25 is in the concept, which after the merge changes only by another merge of the owner's. It is cheaper to
correct in this PR first. That choice is the planning thread's and the owner's.

**What I checked and found right.**
- **The four parts** (the record). The concept says what Lemuria is and is not. The prototype's terms say what the
  first build shows and that no folder is a proto thread's. The steps and features give each feature a number, its
  owning thread and its folders. The shared list gives every file outside a feature's folders to one thread.
- **The cut of the repo.** I sorted all 327 files outside record/ on main by the book's rule for a feature's folders.
  296 lie in exactly one feature's folders (machines 86, crystals 74, sparkstone 35, evocations 32, cradle 17, storage
  17, mysteries 15, earmuffs 10, journal 10). 31 are shared, and the shared files table names every one. None lies in
  two features and none is left over.
- **The Precursor steps.** Each PR and each merge time in the table is what GitHub shows. The first commit (2300f03,
  2026-10-26 22:00Z) did go straight to main, on the owner's "Confirm the push here" (the old step 0 diary). The
  column "Built in old steps" is right for each of the nine features: I compared it with the folders that each
  PR from #2 to #10 changed.
- **The rulings.** R1 and R6 are in part 2. R2 and R14 are in the Precursor steps and the table of old and new
  numbers. R3, R7 and R9 are in the paragraph on the nine briefs, and R9 again in standing rule 7. R4 is under the
  Precursor steps. R5, R10 and R15 are in part 4. R8 is in the unplaced list and in the old paper as read-only
  history, linked. R17 is in the head and in part 4. The planning thread's entry names the three rulings it put into
  its own words (R1, R3, R9), and I read each as it does. R11, R16 and R18 ask nothing of the book. R19 now records
  the message that the book's "03:00Z" quotes. R20 and R21 came after the book was written, and nothing in it
  disagrees with them.
- **Steps 1 to 7 and features 10 to 27,** against the old step list in "Lemuria: pre-production", the roadmap, and the
  notes of old step 4 part 2 and old step 5. Every part of old step 5 part 2 and of old steps 6 to 11 has a feature.
  The six aims in play are the old gates, word for word. Each thing given as the owner's with a date stands in the old
  paper under that date. The eleven entries of the unplaced list are the roadmap's eleven "Earmarks with no step yet",
  none missing and none added.
- **The old paper** (part 4). The eleven Claude Docs are every Lemuria Doc the project holds besides the hand-over
  plan. The ten diaries and the seven play-test folders exist as named, and every file among them was last written
  before 03:00Z (G22). Beside them, the art and sound folders hold their scripts, and the patch file is as G5 found
  it.

**My findings that the book answers.**
- **G4 (flaw): corrected by this commit.** Part 4 gives the minute (03:00Z), says that "what they do not name is not
  old paper", and gives each thing beside the old paper to a thread or to no thread, by name. I agree with the minute:
  of the four moments G4 listed, it is the only one in the owner's own words that says from when. R19 has recorded
  that message since (03:00:29Z), so the book could cite R19 at its next change. G4 closes when this commit merges.
- **G6 (note): answered,** by the paragraph after the crystal cycle.
- **G10 (note): answered,** under "Old step numbers and new". One gap is left, inside record/ (G27).

**The new findings.**
- G24, flaw, the planning thread: the book does not say whose work it is when a feature's work lies in another
  feature's folders.
- G25, flaw, the planning thread: in the concept, what is said on land claims does not agree with itself, and a
  quotation is not the owner's words.
- G26, note, for the owner: no zip holds what main holds.
- G27, note, the planning thread: seven small things.
- G28, flaw, the coordinator: R19's attachment line. It is corrected already.
- G29, note, the planning thread and mason: the repository rules are in no part of the record.
- G30, note, for the owner: the counter-view.

**The working files, read since G21 (rule 6).** R19 to R21 hold the owner's words as the owner gave them (G22). R19's
"Given" line was wrong and is corrected (G28). The coordinator now has a diary of its own (G8). The record thread is
new, by R20 and R21: its entries say what it did and why, and name its own two errors plainly. The planning thread's
halt entry of 05:08Z is in order and ends with "Next:".

**For the planning thread.** Cite this entry in PR #16's description as the review thread's report G23 in
record/findings.md, then assign the owner (rule 4). If you bring main into the branch, this report still stands. If
you correct G25 in this PR first, with G24 or without it: push the commit, tell the coordinator, and I read the change
and write a new report for the description to cite in place of this one. G24 may also follow after the merge, by a
change to part 3 that I can certify.

**For the owner.** You may merge PR #16 as it stands. Two flaws are raised against it and neither holds it. You may
decline either in rulings.md. One of them, G25, costs a second merge of yours if it is corrected after this one, and
one more reading of mine if it is corrected before. G26 is a thing only you can act on: no zip holds what main holds
today, and you may ask for one.

**Open after this report.** Flaws: G5 (the precursor; the coordinator until it starts), G7 and G13 to G16 (mason; held
by the coordinator until it starts), G24 and G25 (the planning thread). G4 is corrected in PR #16 and closes when that
commit merges. G8 and G28 are corrected. No blocker is open. Notes need no answer.

## G24. Flaw: the book does not say whose work it is when a feature's work lies in another feature's folders

**Rank.** Flaw, naming step 1. **Owning thread.** The planning thread. **Raised in.** G23, against part 3 of the book
(PR #16 at commit 722d58a). **Status.** Corrected in PR #16 at commit 84ace1d (G39). It closes when that commit
merges. One sentence of the correction is G41, and the last point of this entry stays with the owner (G36).

The book defines a feature as "one part of the mod with a folder of its own in the code", and gives each feature the
folders named for it. For the nine built features that cut is clean (G23). The book does not say what holds when the
work of one feature lies in the folders of another. In Lemuria that is the usual case.

- **How Lemuria was built.** PR #9 is the feature `mysteries`. Of its 2,862 changed lines, 2,446 lie in the folders of
  `mysteries`. The other 416, in 51 files, lie in the folders of six other features: crystals 281, evocations 36,
  cradle 32, sparkstone 26, machines 24, journal 17. PR #6 put a third of its lines into `sparkstone`. Every old step
  from old step 1 on wrote into the Journal's folders, as the book's own table says.
- **The book's own entries.** Feature 10 works "through Memory", which is feature 8's. Step 1 also places two things
  "for the brief to settle" that lie in built features: panning an Earth battery's store through Memory (feature 8),
  and limits on how machines Bloom (features 8 and 9). Feature 11's rings and refining are the cradle's and the
  crystals' (features 2 and 1). Feature 13 has the shielded jar, and "the jar itself is built, in feature 5". Features
  16 and 23 are tiers of sparkstone (feature 4). Feature 22 has "machines and shelves that sleep and wake" (feature
  8). Feature 24 is a further Evolution of the crystals (feature 1).
- **Feature 27,** `secrets`, is "the Hidden Vocabulary and the Journal, complete". The Journal is feature 3, and a
  secret of the Hidden Vocabulary lies where its machine does. By the book's own definition feature 27 has no folder
  of its own.

Two readings are open, and they lead to different work.
1. **The folder decides.** What is added to `Lemuria/Machines/` is feature 8's, whichever feature brings it. Then
   feature 8's settled brief no longer describes its folders, and it is changed first, each time, by a PR that I read
   and the owner merges (rule 4). The planning thread's entry leans on this reading: the patch gate "works only if a
   feature's brief describes what its folders hold". The book does not say it, and a diary is not the Road Bible.
2. **The brief decides.** What feature 10's brief describes is feature 10's, wherever it lies. Rule 2 has a brief say
   "which existing parts it touches". Then one folder holds code that two briefs describe, and the tests of rule 7
   lose their one answer. A later fix inside `Lemuria/Machines/` can leave feature 8's brief "true as written" and
   break feature 10's. And at step 1's gate nobody can say whether a "patch to that feature" is still open.

Two more things are open under either reading.
- Rule 2 has "no code for a feature before its brief is settled". When step 1's code changes Memory, which brief must
  be settled: feature 10's alone, or feature 8's as well? R3 puts the nine briefs first, "right after" the book, but
  nothing in the book ties step 1's code to them.
- Features 1 to 9 stand under no numbered step. The tests of rule 7 at a step gate name "every feature listed under
  the step" and each "patch to that feature". So no gate asks whether a patch to a built feature is still open, or
  whether it has its diary entry.

**To correct it.** One paragraph under "How Lemuria counts features". It says which reading holds, what a feature's PR
may change outside its own folders, and which briefs must be settled or changed before it does. And for feature 27,
either its folder, or its place beside balance as work that is no feature.

**When.** Before the first brief of step 1 is put up. This is part 3, so after the merge it can change by a PR that I
certify (rule 4). It can also ride in PR #16 now. If the duets' brief reaches me first, I read it for this: a brief
that leans on one reading while the book is silent is built on something missing.

**Why not a blocker.** The next work is the nine briefs of the built features (R3), and for those the cut is by folder
and clean. Nothing is built on the gap before step 1's brief.

**What is not in doubt.** With one builder (R5), who may change which folder is settled: mason owns them all. The
doubt is over briefs and gates.

## G25. Flaw: in the concept, the sentences on land claims do not agree, and a quotation is not the owner's words

**Rank.** Flaw, naming step 1. **Owning thread.** The planning thread. **Raised in.** G23, against part 1 of the book
(PR #16 at commit 722d58a). **Status.** Corrected in PR #16 at commit 84ace1d (G39). It closes when that commit
merges. The new last sentence of standing rule 7 is G40.

The concept is the one part of the book where rule 1 leaves a thread no choice: "the exception never covers a reading
of these rules or of the concept", and a question about it stops every thread. So its sentences have to agree with
each other to the word. On land claims they do not, and one quotation is not what the book says it is.

1. **Ascendant stone.** The table of sparkstone tiers says that Ascendant stone heals "never inside another player's
   claim". Standing rule 7 says that "every land claim rule in Lemuria goes through one switch, so that none can
   drift", and that a server admin can turn the switch off. With the switch off, one sentence has the stone heal
   inside a claim and the other says never. The table's sentence comes from the pre-production document, where it was
   written before the owner asked for the one switch in old step 3's play-test (2026-10-27). The code on main follows
   the switch: `LemuriaClaims.TryAccess` is "always true while claims are off" (Lemuria/Core/LemuriaClaims.cs, lines
   26 to 28). Feature 16's brief would have to choose between the two sentences, and rule 1 does not let it.
2. **"Only a Bale storm's scars disregard claims."** It is the last sentence of standing rule 7, after the sentence on
   the switch being off. As it stands it is untrue with the switch off, when by the rule's own words Lemuria's
   workings no longer spare a claim. In the old paper it belongs to the switch being on: reactions spare claims "as
   evocations do, so only a Bale storm reaches into claims".
3. **What standing rule 7 leaves out.** With the switch off, "every Lemuria block that is used by hand also opens"
   (R9). That has a limit which is the owner's too: breaking and building inside a claim are never lifted, and stay as
   the game rules them (the owner's card of 2026-11-10, by the old audit's diary; LemuriaClaims.cs line 39). The book
   does not carry it. The nine briefs come right after the book, and each must state R9 for its own blocks (G16).
4. **The owner's idea of 2026-10-22.** The book gives it in quotation marks, "as parked on 2026-10-22". The words are
   the pre-production document's shortening of it. The owner's scratchpad, which part 4 names as the owner's own,
   holds the idea as it was parked, marked "owner's words, Oct 22 2026 20:53Z":

   > an "Atlantis: The Lost Empire" inspired tech mod, focusing on the growth of crystals and the shaping of stone
   > (maybe living or crystal-infused stone?). No GUI; indications should be immersive and natural (lights or symbols
   > on the models and tools themselves, similar to Botania). Scope: exploration, into machines, into spell-like or
   > magic-like technologies, in the vein of Atlantis, Castle in the Sky, or Escaflowne. Look into movable or "flying"
   > block structures.

   The book's version drops "magic-like", the question mark after the living stone, and the three films, which it
   gives in a paragraph of its own. I did not open the owner's message of that day. The scratchpad is the nearest copy
   I could read.

**To correct it.**
1. End the table's sentence at "paid in charge", or add "while the claims switch is on".
2. Put the scars with the switch being on. For instance: "With it on, as it is by default, Lemuria's workings spare
   what lies inside another player's claim, and only a Bale storm's scars do not."
3. Add one sentence: breaking and building inside a claim stay as the game rules them, whatever the switch says.
4. Quote the scratchpad's entry whole, or take the quotation marks away.

As I read R9 and the owner's words of 2026-10-27, points 1 and 2 are corrections of wording and need no new ruling. If
the planning thread reads them otherwise, that is a question for the owner (rule 1).

**When.** This is the concept. Before the owner's merge it changes by one more commit to PR #16 and a new report of
mine. After the merge it changes only by another PR that I read and the owner merges, or by the owner's command, and
every thread then reads the book again (rules 4 and 5).

**Why not a blocker.** Standing rule 7 says what the owner ruled, and the next work can be built on it: the nine
briefs take R9 from it and from G16. The table's sentence waits for feature 16, in step 3.

## G26. Note: no zip holds what main holds, and what the record shows as played

**Rank.** Note. **Owning thread.** The coordinator, for the owner. **Raised in.** G23, at part 2 of the book.
**Status.** Needs no answer.

Part 2 makes main the prototype by R1, in whose words it is "already played and merged". The book itself says that R1
was given before PRs #10 and #9 merged. I set down what the record shows, so that the owner has it at the merge. It
reopens neither R1 nor R4, and it is no fault of the book.

- R1's question said "you played steps 0 to 4". R4 had the owner "play and merge both" of PRs #9 and #10, and the
  owner merged both at 03:47Z, 29 minutes after that answer.
- The old build thread's last entry, of 02:45Z that day, lists as unfinished "the owner's play-test of step 5 part 1".
- The newest zip, in lemuria-playtest/step5a/ (2026-11-10 22:12Z), was built from PR #9's head, commit 95889bb. It
  does not hold PR #10. Main's code differs from it in 24 files, by 689 lines added and 33 removed. No zip was ever
  built from PR #10, or from main since the two merged.
- There is no player-diary.md.
- Rule 3 has one early zip, owed by "the thread that builds the prototype". By R1 no thread does, so none is owed. The
  next zip the rules send is step 1's, when the duets are reported done.

So until step 1 is done the owner holds no build of what the book calls the prototype, and the play checks of G20
cannot be made. The owner may ask the coordinator for a zip of main at any time. By R21 the record thread builds the
coordinator's zips. Part 4 says where a new zip does not go: into the seven old folders. Where one does go is the
coordinator's to name.

## G27. Note: seven small things in the book

**Rank.** Note. **Owning thread.** The planning thread. **Raised in.** G23. **Status.** Needs no answer.
Answered in PR #16 at commit 84ace1d (G39).

None of these changes any work. Each could ride in the next change to the book.

1. **"In the words of the old roadmap, which called it the step's gate"** (part 3). The six sentences are right, word
   for word. They stand in the old step list of "Lemuria: pre-production". The Doc that part 4 calls "Lemuria:
   roadmap" holds no gate. A reader who follows the book's words opens the wrong Doc. The same goes for step 1's "The
   old roadmap left it to this step's own note".
2. **Old numbers inside record/.** The table of old and new numbers covers the old paper and everything outside
   record/. Entries of record/ written before the book's merge mix the two: the questions in R2 and R4, the diaries,
   and my G1 to G21 (G22 says which of mine are which). One sentence would cover them.
3. **"Not a mod of new creatures"** stands under "What Lemuria is not", and "new creatures" stands again under "Out of
   scope for v1.0". The old paper has it only as out of scope for v1.0. The two lines differ in strength, and the book
   should say which it means.
4. **The standing rules** are headed "the owner's, settled before this book". The last sentence of rule 3, "From here
   on a word's meaning is settled in the record", is new and the planning thread's own. The owner's merge makes it the
   owner's. I say it so that nobody takes it for older.
5. **Short names from working names.** `goodsbeams` and `supermedia` are made from what the book itself calls working
   names. A short name names folders and a brief. The book does not say whether a short name follows when the owner
   renames the thing.
6. **"Whatever is added outside record/ later"** is mason's. So the shared files table will not name a new shared file
   unless the book is changed for it. That is harmless with one builder. If the builders are ever split (R5), the
   table must be brought up to date first.
7. **The old glossary.** Standing rule 3 has the book use each word in the meaning of the old glossary, "as they
   stood". The glossary is old paper: it is outside the record, and any thread's call can still edit it (G5). I read
   it at revision 247 (G22). Until the precursor records its revision, that number is the only anchor the record has
   for the meaning of the book's own words.

## G28. Flaw, corrected at once: R19's "Given" line named three attachments that the owner never sent

**Rank.** Flaw, naming step 1. **Owning thread.** The coordinator, whose record thread wrote the entry. **Raised in.**
G23. I told the coordinator by message before 05:30Z. **Status.** Corrected by PR #20, on main at 08410c3, which I
read whole. The dated lines asked for below came in PR #24, and the three entries were sent again (G38).

R19 as it reached main in PR #18 said that the owner's message of 03:00:29Z "carries three attachments": the hand-over
plan and links to PRs #11 and #16. None of the three existed at 03:00:29Z. The plan was written in answer to that
message, PR #11 was opened at 03:05Z and PR #16 at 05:03Z. They are the planning thread's outputs, which the platform
lists on the thread that the message started. The owner attached one file, the rules: the upload
uploads/hearth/66303e87-c7e8-436e-b0ed-8187559f4109, written at 03:00:29Z, 35,772 bytes, sha256 918dd508…fbca4a5b02,
byte for byte version 2 of the rules.

It mattered because R19 is the entry that G4 and the Road Bible's "03:00Z" lean on, and what the owner sent with those
words is what "the new rules" were.

The owner's words in R19 were right from the start. The coordinator's entry of 05:21Z had the rules file right. The
record thread changed it, and its entries of 05:21Z and 05:30Z say so plainly. R19 now names the one attachment with
its size and its checksum, and both are what I measured.

One thing more for the coordinator and its record thread, which needs no answer. R19 to R21 were changed in place
twice after they reached main (PRs #19 and #20), and nothing inside R19 shows that it was corrected, or when. Threads
had been sent these entries word for word before the corrections. A dated line inside the entry, and the entry sent
again, would keep every thread's copy the same as main's.

## G29. Note: the repository rules in CLAUDE.md and README.md are in no part of the record

**Rank.** Note. **Owning thread.** The planning thread. It concerns mason (G7, R10). **Raised in.** G23. **Status.**
Needs no answer. Answered in PR #16 at commit 84ace1d (G39).

CLAUDE.md ("Repository rules") and README.md ("Working in parallel") hold working rules for whoever writes code, and
no part of the record has them: run the three scripts before a PR and say what passed; every registered code starts
with `lemuria`; one chat command root, `/lemuria`, with a sub-command for each feature; network channels named
`lemuria-<feature>`; one Harmony id for each feature, unpatched in Dispose, with postfixes preferred and each patch
explained in its PR; legacy AI task codes; and never a game file, `.cache/` or `Releases/` in a commit. Standing rules
8 and 9 of the book carry the design behind two of these, not the rules themselves.

R10 has mason rewrite both files "to point at record/COMMANDMENTS.txt", and part 4 says the same. If the rewrite drops
these rules, they are gone. If it keeps them, they bind from two files outside record/, and rule 1 stops a thread at
"a rule you have been told to follow" that "is not in the record". One sentence in the book would settle it: either
the shared files table says that the two files keep the repository rules and that these bind whoever writes code, or
the rules move into the book. I read for it when R10's PR and the nine briefs come.

## G30. Note: the counter-view to the first Road Bible (rule 6 a)

**Rank.** Note. **Owning thread.** The coordinator, for the owner. **Raised in.** G23. **Status.** Needs no answer.

Rule 6 has me argue the counter-view to the vision set out in the book. I do not argue against the mod. The book
carries the owner's design truly (G23). I argue against five choices in how the book is cut. Each is the planning
thread's own by its entry, and each is open to the owner at the merge. None holds anything.

1. **A feature is a folder** (the entry's decision 1). What it gives: the patch gate of rule 7 has folders and a brief
   to test, and the repo is already cut that way. What it costs: R3's brief "from the signed-off note" becomes a brief
   cut from several notes. `machines` draws on the three notes of old step 4, the note of old step 5 and the old
   audit's fixes, and `journal` on every note. So no built feature's brief has one signed-off source, and the owner
   merges nine briefs whose words nobody has signed off in that shape. I read each against all of its notes and the
   code (G3, G12). The other cost is G24. The other way, a feature for each old step, is worse, for the reason the
   entry gives.
2. **The concept holds much that is not built.** Much of part 1 describes what features 11 to 25 are still to build:
   what a dialogue does from Luminous up, exposure and PPE, the Ascendant and Sovereign tiers, teaching and the Runic
   Tablet, both vehicles. The old paper shows how such plans fare. Old step 2's play-test brought the Crystalline
   Chisel. Old step 3's brought the one claims switch and a Tremor that moves the real ground. The owner's answers to
   old step 4's note added Scatter, the Crystal Motor, the Control Shelf, Memory and the reclamation plate. Each time
   the design followed on the owner's word alone. Under the rules a brief may not differ from the concept, a doubt
   over the concept stops every thread, and each change to it is a merge of the owner's after my reading, followed by
   every thread reading the book again. The entry keeps the glossary out of the concept for this very reason (its
   decision 5). The same reason speaks for holding what is not built under its step in part 3, where a change can go
   by my certificate, and for keeping in the concept only what the whole mod stands on.
3. **The count.** Eighteen features are still to build. Each needs a brief that I read and the owner merges, and at
   least one PR that the owner merges. Nine more briefs come first (R3). With one builder and one unmerged feature at
   a time (rule 3), they go one after another. That is 27 briefs and at least 18 feature PRs for the owner's hand
   before v1.0, patches and changes to the book not counted. Seven notes carried the mod this far. The cut into
   eighteen is the planning thread's (decision 2, "the least certain part of the book"). Fewer and larger features
   mean fewer merges and longer briefs. The owner may want to choose the grain before step 2's cut is used.
4. **Play comes after the merge.** Under the old rules nothing merged that the owner had not played. Under rule 3 a
   feature merges from its description, and the zip comes when its step is done. The book keeps the old gates as "aims
   in play" that hold nothing, which is right under rule 7. But steps 2 and 5 bring the first things that mark a world
   for good: reactions whose effects stay "until a clean-up", and a Bale storm's scars. The owner may ask for a zip of
   any PR (rule 3). The book could name the features for which that is wise before the merge, or leave it to each
   brief's "what to try" list.
5. **Step 7's gate.** Step 7 lists one feature, 27, and three things that are no feature: balance, the one multiplayer
   test and the release. The gate of rule 7 tests features, PRs, the checks and the diaries. So step 7 passes when
   feature 27 is merged, whether or not two players have ever met on one server, and the one multiplayer test of
   standing rule 10 hangs on nothing. One sentence would tie it down: the test is played before step 7 is reported
   done. Then what the owner says after it goes into player-diary.md, and the gate waits for the answer to each entry.

## G31. Diary: the report on PR #16 is in, and PR #21 is next

**Written.** 2026-11-12 05:50Z.

The report G23, with its findings G24 to G30, is complete. I do not halt. The coordinator woke me at 05:37Z under rule
6 (e) for PR #21, the change to the rules that writes R21 in, and told me at 05:40Z that it is ready at commit
7de344f. I read it next. This entry ends with "Next:" so that the file is in order if I am cut short.

Next:
- **Unfinished:** the report under rule 6 (e) on PR #21 at commit 7de344f.
- **Where it is:** this file, sent to main by a record-only PR from the branch claude/project-thread-4zobvq.
- **Waiting for:** nothing for PR #21. After it, in some order:
  - a new report on PR #16, if it takes a commit that voids G23;
  - the nine briefs of the built features (R3, rule 6 a), where I read for G12's last point, G16, G20 and G29;
  - step 1's brief, where I read for G24;
  - the first maps (rule 6 f): the planning thread's after PR #16 merges, and G5 at the precursor's.
- **Open findings:** flaws G5 (the precursor; the coordinator until it starts), G7 and G13 to G16 (mason; held by the
  coordinator until it starts), G24 and G25 (the planning thread). G4 is corrected in PR #16 and closes when that
  commit merges. No blocker is open.
- **Read up to:** rulings.md R21, player-diary.md none, findings.md G31.

## G32. Report under rule 6 (e): the change to the rules that brings in the record thread (PR #21 at commit 7de344f)

**Written.** 2026-11-12 06:01Z. Woken by the coordinator at 05:37Z, and told at 05:40Z that the PR is ready.

**What I read.** PR #21, "Rules: version 4 brings in the record thread", branch rules/version-4, at commit
7de344f74ee959d07c8f8693d4def98c2880cb24, its one commit, on main at 163026f. Its check "build" was green on that
commit at 05:40Z. It was not assigned to the owner and not set to auto-merge, as rule 4 has it. Main has moved to
25b284c since, in other files, and the branch still merges cleanly. By rule 4 a later commit voids this report, unless
it only brings main into the branch or adds only the author's own diary entry.

**What changed in the record since G31 (rule 6).** Two PRs. PR #22, merged at 05:47Z: the planning thread's halt entry
of 05:41Z, which is in order and ends with "Next:". It says that the planning thread found R19's wrong line as well
and told the coordinator at 05:29Z (G28). PR #23, merged at 05:58Z: my own G22 to G31. The last entry of rulings.md is
still R21.

**The short of it.** No blocker. Nothing here asks the owner to wait, and nothing asks for a change to PR #21. The
change writes the five changes of R21 into the rules and moves the head to version 4. Every added word is the owner's,
and no other word of the rules differs. I found no sentence of the rules that it contradicts, and no code that it
makes wrong. R21 already binds (rule 1), so the merge only brings the file into line with what binds today. On what
the change leaves open I raise two notes: the counter-view (G33) and two things in the wording (G34).

**What the change is, read from the diff.**
- record/COMMANDMENTS.txt differs in seven places, and the file grows from 187 lines to 189.
  1. The head: "version 3" becomes "version 4", and the line under it "Ratified 2026-11-12. Replaces version 3
     (2026-11-12)."
  2. The cast: a new paragraph of 2,079 characters, after the coordinator's and before the third tier.
  3. The cast, the coordinator's paragraph: one sentence, after "unless the owner says the coordinator will record
     it."
  4. The record, the diary line: ", and a record thread's writing in the coordinator's folder," inside its bracket.
  5. Rule 1, second paragraph: ", its record thread" among the threads that a stop for everyone leaves at work.
  6. Rule 1, third paragraph: two sentences at its end.
  7. Rule 5, first paragraph: one sentence at its end.
- record/diary/optimist-visionary/2026-11-12T0534Z.md is new: the planning thread's entry for this PR (rule 5).
- Nothing outside record/ changes.

**Held against the owner's word.** I took neither the PR's word for it nor R21's. I took the server's own copy of the
owner's message of 05:20:38Z, the one I held R21 against (G22), and compared it with the file by program.
- The owner's message has three kinds of curly mark. The rules file has no character outside plain ASCII, in version 3
  or in version 4. With the curly marks turned into the file's plain ones, every added piece is the owner's, character
  for character: the new paragraph whole; the sentence of change 2, placed directly after the words the owner names,
  which stand in the file once; the first sentence of the diary line; the clause and the two sentences in rule 1; the
  sentence in rule 5.
- Apart from the head and those pieces, version 4 is version 3: its other 180 lines are the same, byte for byte.

**What the owner's words leave to the writer.** The PR and its entry name five things. I agree with each.
1. Plain marks for the owner's curly ones. No word differs.
2. The diary line keeps its second sentence, "It holds the thread's map and its diary, one file to an entry (rule 5)."
   R21 says the line "now reads" and gives one sentence. Change 4 uses "now reads" in the same way, for the clause
   that changes, and nothing in R21 takes the map and the diary out of a thread's folder.
3. Rule 5's new sentence stands at the end of the first paragraph. R21 names no place, and the sentence says the same
   wherever in rule 5 it stands.
4. The head goes to version 4, by the rules' own words: "the version number goes up each time". The date is the day of
   R21 by the record's clock.
5. "Its office ends" stays as it was. R21's word for change 2 is "one sentence added". The planning thread says that
   the text the five changes were copied from has "A deputy's office ends" there. I read that text myself (the repo
   VS-Walkers, branch claude/planning-rules-v3, commit 2fcc1b6). It and our version 4 differ in three places and no
   more: the head, our paragraph on the precursor, and that one word. The entry's account is right (G34).

**Does it contradict another rule?** I read the new words against every place that has the coordinator write, names a
deputy, a diary or a map, or calls a stop.
- **The coordinator's writing.** The two lines of the record on rulings.md and player-diary.md, rules 1, 3, 4, 6 and 7
  and the inquisitor's paragraph all still say that the coordinator writes or builds. One sentence of the new
  paragraph covers them all: "Wherever these rules have the coordinator write in rulings.md, in player-diary.md or in
  its own folder, or build a zip, the record thread does it, and what it writes there is the coordinator's writing."
- **Sending and waking stay the coordinator's.** The record thread "relays nothing between the threads and the owner,
  and wakes no one". So the coordinator still sends each entry to the threads it concerns, sends the zip that the
  record thread builds (rule 3), and makes every wake.
- **The deputy.** The added sentence switches both deputizing sentences off while a record thread is at hand. The
  sentences after it on a deputy's office still hold for a thread that the owner tells to record. My own "unless it is
  deputized" in rule 6 follows the same switch.
- **The diary and the map.** The diary line's bracket and rule 5's new sentence agree with the new paragraph. Rule 5
  still says "The review thread is the exception" and then names a second thread that keeps no map. The two do not
  disagree (G34). Rule 6 (f) wakes me at a first map: the record thread has none, and the coordinator has one, which
  the new paragraph names.
- **Rule 1.** "No stop keeps a record thread from recording" has to be there, because a stop is closed by an entry in
  rulings.md. "A record thread's own doubt over what to write goes to the coordinator" is narrower than a stop, and it
  does not free the coordinator from rule 1: a doubt that the coordinator cannot answer from the owner's words or its
  own is the coordinator's to stop on.
- **The tiers.** The paragraph stands in the second tier's text and gives the record thread no tier of its own. It
  needs none: it "holds no office of its own", and what it may change is listed, file by file.
- **Rule 4,** "A change to the Road Bible or to a brief that cites an entry its own author recorded waits for the
  owner's merge". With a record thread an author no longer records the entries it cites, so the clause bites only
  where the owner makes a thread a deputy. That is what the clause is for.

**R21 at work already.** It binds from its entry, and the record thread has worked under it since 05:21Z. Its three
PRs (#18, #19, #20) changed rulings.md, the coordinator's folder and its own diary folder, and nothing else. That is
inside what the new paragraph allows.

**The counter-view (rule 6 e).** It is G33: who reads what the coordinator's hands wrote; a list in my report at a
step gate that goes quiet; what happens if the record thread ends unseen; the round trip that each ruling now makes;
and the simpler cure, if there is one.

**Code this change would make wrong (rule 4).** None.
- I searched everything outside record/ for a role's name, for the name of a record file and for whatever builds a
  zip. The only words are CLAUDE.md lines 11 and 13, version 1's rule on telling the coordinator, which G7 already
  holds. No code, script or check names a role or a record file.
- One thing the change hands on, though it does not make it wrong. The build makes Releases/lemuria_<version>.zip
  (CakeBuild/Program.cs line 223). Rule 3 has a zip for the owner "named with its commit id and, for a PR, its PR
  number". From now on the record thread builds those zips, so naming the file is its to do when the first one is
  asked for (G26).

**For the planning thread.** Cite this entry in PR #21's description as the review thread's report G32 in
record/findings.md, then assign the owner (rule 4). The branch merges cleanly as it stands. If you bring main into it,
this report still stands.

**For the owner.** Nothing here asks you to wait or to change PR #21. When you merge it, the coordinator tells every
thread, and each reads the rules again before it goes on (rule 4).

**Open after this report.** As in G31, and nothing is added: flaws G5 (the precursor; the coordinator until it
starts), G7 and G13 to G16 (mason; held by the coordinator until it starts), G24 and G25 (the planning thread). G4 is
corrected in PR #16 and closes when that commit merges. No blocker is open. G33 and G34 are notes and need no answer.

## G33. Note: the counter-view to the record thread (rule 6 e)

**Rank.** Note. **Owning thread.** The coordinator, for the owner. **Raised in.** G32. **Status.** Needs no answer.

The owner asked for this role in their own words (R20, R21), and the change writes those words and no others. So this
holds nothing. It is what I would weigh against the role.

1. **Hands that are also eyes.** The record thread writes for the coordinator, and it also "reads the repo for the
   coordinator". The coordinator cannot see what its hands wrote except through those hands. The record thread's first
   PR put two wrong things on main: R19's attachments (G28), and the coordinator's words in the record thread's own
   wording under "as sent", which it found itself (PR #19). Both were put right within minutes, and its diary says so
   plainly. The rule's guard is sound: copy from the owner's own message, and say where and when. Nobody in the
   coordinator's office checks that guard. The check is mine: rule 4 has me read the working files at each wake, and I
   hold each new entry against the owner's own message, as in G1 and G22.
2. **A list that goes quiet.** Rule 6 has my report at a step gate list "every entry a deputy recorded", for the
   owner's eye. From R21 on the record thread's entries are "the coordinator's writing" and not a deputy's. So that
   list will be nearly empty, while no entry at all is written by the coordinator's own hand. At each step gate I will
   say which entries the record thread wrote since the last one, and whether each matches the owner's message. That is
   report, and the owner may tell me to leave it out.
3. **What the record hangs on.** "A thread goes by what the coordinator last told it." If the record thread ends
   unseen, every thread still sends the owner's words to it, no thread is deputized, and nothing is recorded until the
   coordinator finds out and says so. The rules have the coordinator start a replacement "if it ends", and do not say
   how it learns of it. A habit would cover it: a thread that has sent the owner's words and sees no entry on main
   after a while tells the coordinator.
4. **The round trip.** Until today the thread the owner spoke to recorded the word and went on. Now it sends the word
   to two threads, the record thread opens a PR, the check runs for about three minutes, the coordinator sends the
   entry, and then the thread may act (rule 1). That is some minutes for each ruling. I think it is worth it: R1 to
   R18 were all recorded by the thread that then acted on them (G1), and rule 4's clause on entries "its own author
   recorded" shows that the rules never liked that.
5. **The simpler cure, if there is one.** The new paragraph begins "Where the coordinator cannot reach the repo". If
   the coordinator can be given the repo, the role falls away by its own first words, and points 1 to 4 with it.
   Whether the platform allows that is the owner's to know.

## G34. Note: two things in the wording of version 4

**Rank.** Note. **Owning thread.** The planning thread. **Raised in.** G32. **Status.** Needs no answer.

Neither asks for a change to PR #21, which writes the owner's words and no more. Both are for the day the owner brings
this text and the sister project's into line (R18).

1. **"Its office ends".** Right after the sentence that change 2 adds, the rules go on: "Its office ends once it has
   recorded the owner's last word to it." The sister project's text has "A deputy's office ends" there. Ours still
   reads rightly, because the "it" of the added sentence is the deputized thread. It is one word of difference between
   the two texts, in a sentence that both projects changed around.
2. **"The review thread is the exception"** (rule 5). Two sentences on, the rules now name a second thread that keeps
   no map. Nothing disagrees, since the new sentence says what a record thread keeps. But "the exception" is no longer
   the only one.

## G35. Diary: halt after the reports on PR #16 and PR #21

**Written.** 2026-11-12 06:01Z.

The work in hand is complete: the report G23 on PR #16 with G24 to G30, and the report G32 on PR #21 with G33 and G34.

Next:
- **Unfinished:** nothing in hand. G19 has not been tried in a run.
- **Where it is:** this file, sent to main by a record-only PR from the branch claude/project-thread-4zobvq.
- **Waiting for:** the coordinator's next wake. I expect, in some order:
  - a new report on PR #16 or on PR #21, if either takes a commit that voids G23 or G32;
  - the nine briefs of the built features (R3, rule 6 a), where I read for G12's last point, G16, G20 and G29;
  - step 1's brief (rule 6 a), where I read for G24;
  - the first maps (rule 6 f): the planning thread's after PR #16 merges, the coordinator's, mason's, and G5 at the
    precursor's;
  - the rules again, in full, once the owner merges PR #21 (rule 4).
- **Open findings:** flaws G5 (the precursor; the coordinator until it starts), G7 and G13 to G16 (mason; held by the
  coordinator until it starts), G24 and G25 (the planning thread). G4 is corrected in PR #16 and closes when that
  commit merges. No blocker is open.
- **Read up to:** rulings.md R21, player-diary.md none (the file does not exist), findings.md G35.

## G36. Note: the question R23, what the record already says on it, and the counter-view

**Rank.** Note. **Owning thread.** The coordinator, for the owner. It concerns the planning thread and mason. **Raised
in.** This entry, on reading R23, which came out of G24. **Status.** Needs no answer. The owner answered R23 with B
(R24, G39), and PR #39 writes that into rule 4 as version 5 (G45).

R23 asks the owner how code reaches main when a feature needs it changed in another feature's folders, after that
feature's brief is changed: on its own, as a patch (A), or in the feature's own PR (B). The planning thread recommends
B, and the book is to hold A until the owner speaks. No stop was called, so nothing woke me for what rule 1 has me
report at a stop: what the record already says. I report it unasked, because the question came out of my finding and
the owner has not answered yet.

**What the record says.**
- **One sentence names the case, and by its letter it is A.** Rule 4: "A change to code on main that follows a change
  to its brief is a patch, and its verdict is "out": it waits for the owner's merge." Memory is code on main, the
  machines' brief is its brief, and the change follows the change to that brief.
- **The words around it shut B out, by their letter.** A patch is "the class of PR that carries a fix to code already
  on main", and "A patch carries a fix and nothing more." A PR that holds the duets and the change to Memory holds
  what rule 4 calls a patch, and more.
- **Nothing says B.** What B leans on is true as far as it goes. Rule 2 has a brief say "which existing parts it
  touches". That phrase has work to do without B: a feature uses built parts as they are, and it touches the shared
  files, which have no brief of their own. And no sentence keeps a feature's PR inside its own folders. The cast keeps
  a thread inside the folders it owns, and mason owns them all (R5, R15).
- **No ruling speaks of it.** The old steps did it as B does (PR #9, G24). They were built and merged under the old
  rules (R4, R14), so they show a habit and no rule.

So the record does not hold B, and by its letter it holds A. I do not call the question closed by that. Whether the
sentence speaks only of a change that comes alone is a reading, and the owner gives it.

**What each answer changes.**
- **What step 1's gate tests.** Rule 7 names "a feature of the step" and each "patch to that feature". Under A the
  change to Memory is a patch to feature 8, which stands under no numbered step in the book as it is (G24). The gate
  then never asks whether that patch is merged or has its diary entry, though the duets need it. Only the third test,
  on how each PR was merged, reaches it. Under B the change is in "a PR that carries a feature of the step", and every
  test reaches it.
- **How many PRs the owner merges, and in what order.** Under A a feature that reaches into six others, as PR #9 did,
  comes as up to seven code PRs, each waiting for the owner. The patches go first, wherever the feature's code needs
  them to build. Under B it is one code PR. Either way the brief of each feature reached into is changed first, by a
  PR that I read and the owner merges.
- **The rules' own words.** A needs none. B needs a sentence in rule 4. Without it the sentence quoted above and the
  ruling stand side by side, and a later thread stops on them (rule 1).

**The counter-view.** The planning thread recommends B. Against B:
1. By my reading it is more than a reading: it changes what rule 4 says. If the owner says B, the ruling binds from
   its entry, the planning thread writes the change at once, and I read it before it merges (rules 1 and 6 e). One
   sentence after the one quoted would do. It should keep what R23 already says: the other feature's brief is changed
   first.
2. The owner merges a feature "from its description" (rule 3). Under B that description must name each other feature
   the PR changes, and the merged change to its brief. Rule 4's list for a ready PR does not ask for that by name.

Against A: the count and the order above, for an owner who has said to carry on until PRs are ready to merge (R22).
And a gate that does not look at the patches which a step's features needed.

I argue for neither. A is what the rules say today, and B is how Lemuria has been built. My one ask: if it is B, let
the rules say it.

**On "Stop. None called."** I raise nothing against it. The book is to hold A meanwhile, which both readings allow, so
no thread does different work today for want of the answer. R13 was raised the same way, and the owner answered it in
R16. What a stop would have added is this entry, before the question reached the owner.

## G37. Note: the smoke test's line "the quern turns: yes" failed once on code that had not changed

**Rank.** Note. **Owning thread.** Mason (held by the coordinator until mason starts). **Raised in.** This entry, from
my own run of the checks before the PR that carries it. **Status.** Needs no answer. It becomes a finding against the
test or the Motor if it shows again. A second line failed the same way at 06:58Z: G43.

I run the three scripts before each of my PRs. On 2026-11-12 at 06:39Z scripts/smoke-test.sh failed, on a commit that
differs from main only in this file. The server's log has:
- 06:38:58 "Machines test: a Crystal Motor on a quern, powered: yes, torque 0.25, on the quern's network: yes, sockets
  facing up"
- 06:39:05 "Machines test: the quern turns: no"
- 06:39:06 "Machines test: the Dead Socket's quern turns: yes"

The script then said "FAIL: machines.expect: no log line matches: Machines test: the quern turns: yes$". Run again on
the same commit at 06:42Z, it passed. So did my four runs before it today, at 04:25Z, 04:37Z, 05:52Z and 06:02Z, and
nothing outside record/ differs between any of the six.

**What the check does.** `MotorCheck` (Lemuria/Machines/MachinesTests.cs, line 469) reads the speed of the quern's
mechanical network and says "yes" above 0.001. The smoke test types one command a second, and this one comes seven
commands after the Motor starts. The comment above the line says that the network "is still spinning up" then. So the
line rests on timing. In the failed run the second Motor's quern, started a second later, did turn. I have not found
out whether the first quern was only slow or was not driven at all.

**Why it is worth a note.** The check "build" runs this script on every PR, record-only ones included: the book's
shared list says that the skip for record-only PRs "is still to be made". A line that fails by chance leaves a PR red
that was set to merge itself. I have not looked through the history of the checks for it.

## G38. Diary: what I read after G35, the question R23, and PR #16 is next

**Written.** 2026-11-12 06:45Z. The coordinator sent me the owner's word of 06:02:59Z at 06:06Z, R19 to R21 again at
06:07Z and 06:08Z, R22 at 06:10Z, and R23 at 06:26Z. At 06:38Z it woke me for PR #16's new commit (rule 6 a).

**The rules.** Read again in full at 06:08Z: version 3, byte for byte the file that G2 read. PR #21 is not merged yet.

**What changed in the record since G32 (rule 6).** Seven PRs of the record thread, all record-only, and my own.
- PR #24, merged at 06:06Z: a dated line, "Changed in place", in each of R19, R20 and R21. The PRs and the merge times
  that they give are right. The coordinator sent me the three entries again, as they stand. That answers the last
  point of G28, and its Status says so now. Also the coordinator's entries of 05:32Z and 06:00Z, and the record
  thread's of 06:00Z.
- PR #26, merged at 06:10Z: R22, the owner's word to carry on. I held it by program against the server's copy of the
  owner's message of 06:02:59Z: the same, character for character. The coordinator's reading with it is marked as its
  own. Also the coordinator's entry of 06:03Z and the record thread's of 06:07Z.
- PR #25, merged at 06:11Z: my G32 to G35. It was set to auto-merge at 06:04Z, and its check was green at 06:07Z.
  GitHub did not merge it, though PR #26 merged itself in that time. At 06:11Z I merged it, by rule 4: "Where these
  rules allow auto-merge and the checks are already green, the thread may merge it." I told the coordinator.
- PR #27 and PR #28, merged at 06:18Z and 06:22Z: the coordinator's entries of 06:14Z and 06:19Z, and the record
  thread's of 06:14Z.
- PR #29, merged at 06:26Z: R23, the planning thread's question to the owner out of G24, and the record thread's entry
  of 06:20Z. R23 holds no words of the owner. The copy that the coordinator sent me has the same words as main. My
  note on it is G36. I sent the coordinator the short of G36 by message before this PR, so that it could reach the
  owner before the answer. The coordinator tells me that it has given my words to the owner and to the planning
  thread.
- PR #30, merged at 06:41Z: the coordinator's entry of 06:39Z, on my word on R23 and on PR #16's new commit.

**The two PRs I have reported on.**
- PR #21 is at commit 7de344f still. Its description cites G32, in my words and rightly, and it is assigned to the
  owner. G32 stands.
- PR #16 took a second commit, 84ace1d, at 06:32Z. It voids G23 (rule 4). The planning thread says that it corrects
  G24 and G25 and answers G27 and G29. I read it next, and the new report follows in my next PR.

Next:
- **Unfinished:** the new report on PR #16 at commit 84ace1d, which I am reading. G19 has not been tried in a run.
- **Where it is:** this file, sent to main by a record-only PR from the branch claude/project-thread-4zobvq.
- **Waiting for:** nothing, for that report. After it, the coordinator's next wake. I expect, in some order:
  - the owner's answer to R23, which I read against G36, and by rule 6 (e) a change to the rules if it calls for one;
  - the rules again, in full, once the owner merges PR #21 (rule 4);
  - the nine briefs of the built features (R3, rule 6 a), where I read for G12's last point, G16, G20 and G29;
  - step 1's brief (rule 6 a), where I read for G24;
  - the first maps (rule 6 f): the planning thread's after PR #16 merges, the coordinator's, mason's, and G5 at the
    precursor's.
- **Open findings:** flaws G5 (the precursor; the coordinator until it starts), G7 and G13 to G16 (mason; held by the
  coordinator until it starts), G24 and G25 (the planning thread). G4 is corrected in PR #16 and closes when that
  commit merges. No blocker is open.
- **Read up to:** rulings.md R23, player-diary.md none (the file does not exist), findings.md G38.

## G39. Report under rule 6 (a): the first Road Bible again (PR #16 at commit 84ace1d)

**Written.** 2026-11-12 07:12Z. Woken by the coordinator at 06:38Z, after the planning thread told it that PR #16 was
ready again.

**What I read.** PR #16, "Road Bible: the first Road Bible of Lemuria", branch claude/project-thread-20hfvu, at commit
84ace1d56b3e733d88f5ea7b2e9a770b5dac35b2, its second commit, on top of 722d58a. That commit voided G23 (rule 4). I
read the diff between the two commits line by line, then the book whole as it now stands (record/road-bible.md, 548
lines, sha256 5a9388188b9b335ad8025387818ab46b03d28f2c3fd65548661155cdb4206360), and the planning thread's entry for
the commit, record/diary/optimist-visionary/2026-11-12T0629Z.md, which rides in the PR. The check "build" ended green
on that commit at 06:34:58Z. The PR is not assigned to the owner and not set to auto-merge, as rule 4 has it. Its
description was changed at 06:43Z, in the section on R23. A description is no commit, and I read it as it stands. Main
has moved to b9fe918 since, in other files, and the branch still merges cleanly. By rule 4 a later commit voids this
report, unless it only brings main into the branch or adds only the author's own diary entry.

**The rules.** The owner merged PR #21 while I read. I read the rules again in full, version 4, before I went on
(G44), and this report is held against version 4. No sentence that I cite from rules 1 to 7 differs between version 3
and version 4.

**What changed in the record since G38 (rule 6).**
- PR #31, merged at 06:49Z: my own G36 to G38.
- PR #21, merged by the owner at 06:52:24Z as 5f49680: the rules, version 4, with the planning thread's entry of
  05:34Z. The file is byte for byte the one that G32 read at commit 7de344f. G32 found no code that the change makes
  wrong, so nothing became a blocker at the merge (rule 4).
- PR #32 and PR #34, merged at 06:53Z and 07:02Z: the planning thread's entries of 06:47Z and 06:55Z. The first holds
  two sentences that it would put up for rule 4 if the owner answers B to R23, and says that nothing is written into
  the rules for them.
- PR #33, merged at 07:03Z: the coordinator's entry of 06:55Z and the record thread's. Its check failed once by chance
  (G43).
- PR #35, merged at 07:10Z: R24, the owner's answer to R23, with the coordinator's entry of 06:57Z and the record
  thread's. The owner's words in R24, "4.) B", are the owner's message of 06:56:33Z, character for character: I read
  the message itself. R24 also holds the coordinator's reading, marked as its own (B, with the two sentences that the
  planning thread drafted for rule 4), and those two sentences as they were posted to the owner. They are the same as
  in the planning thread's entry of 06:47Z. R23 is marked answered, with a dated line.
- At 07:08:14Z the owner confirmed the coordinator's reading in the project chat: "Correct reading, good job." I read
  that message too. The record thread enters it as R25, which is not on main yet.

**The short of it.** No blocker, and nothing here holds the owner's merge. The second commit corrects G24 and G25 and
answers G27 and G29, as the planning thread says, and I held each correction against its source. I raise two new
flaws, both small and both the planning thread's: one sentence of the concept (G40) and one of part 3 (G41). The
owner's answer to R23 came while I read, and it overtakes point 3 of the new section. So three places in the book are
due a change, whichever way the book merges. The counter-view is G42.

**G25, the concept, held against its sources.**
1. *Ascendant stone.* The row now ends "and never inside another player's claim while the claims switch is on
   (standing rule 7)". It agrees with rule 7.
2. *The scars.* They now stand with the switch on: "Lemuria's workings spare what lies inside another player's claim,
   and only a Bale storm's scars do not." That is where the old paper has them.
3. *The switch off.* Standing rule 7 now says what holds then. I held it against R9, against the owner's card of
   2026-11-10 as the two old diaries have it, and against the code. The card speaks of use: "With the switch off,
   anyone may use racks and jars inside a claim." R9 widens that to "every Lemuria block used by hand". The code lifts
   the game's lock for use and for nothing else (`LemuriaClaims.MayLift`). The book says that "the owner's words open
   Lemuria's blocks there to use and to nothing more". That is more careful than G25 was, which called the limit "the
   owner's too": the words "breaking them stays as the game rules it" are the old planning thread's, in the note of
   old step 3. The new last sentence has a fault of wording, which is G40.
4. *The owner's idea of 2026-10-22.* It is quoted whole. I held the quotation against line 12 of
   /mnt/project-files/owners-scratchpad.md by program: 465 characters, and the same. The book claims no more than it
   can show: the scratchpad "holds it, entered there as the owner's words of that day". I did not open the owner's
   message of that day, and neither did the planning thread. The sentence after the quotation gives the idea as
   history and names the two places where the concept has since gone another way, the one screen and the flying
   structures. I looked for a third and found none.

**G24, part 3: "Where features meet".** The new section says which reading holds (the folder decides), what comes
first (the briefs), and what a feature's PR may change. That is what G24 asked for.
- Point 1 gives the tests of rule 7 one answer: the brief of the feature in whose folders the code lies.
- Point 2 answers the first of G24's two further points. Before step 1's code changes Memory, feature 8 has its first
  brief (R3), and that brief is changed for what is added. One sentence of point 2 says more than it means: G41.
- Point 3 holds way A and names R23. The owner has answered since (below).
- Feature 27 is gone, and step 7 lists no feature. The numbers 10 to 26 each stand once, under steps 1 to 6, and I
  found no sentence that still counts eighteen features or names a feature 27. What follows for step 7's gate is in
  G42.
- G24's last point was that features 1 to 9 stand under no numbered step. The planning thread's entry says that the
  book cannot cure it, because the tests are rule 7's. I agree. It stays with the owner as G36 has it.

**Point 3 and the owner's answer to R23.** Point 3 ends: "R23 asks the owner whether such code may instead ride in the
feature's own pull request. Until the owner has answered, it goes as a patch, which no reading of rule 4 forbids." The
owner has answered, with B (R24). R24 binds from its entry (rule 1), the planning thread writes the change to rule 4
at once, and that change comes to me under rule 6 (e). Rule 1 also has the book changed "before any code follows the
ruling". No such code is near: the nine briefs come first, and then the duets' brief. So the merge need not wait for
point 3. But from R24 on, point 3 says what no longer holds, in its heading, "The code keeps to its folders", and in
the sentence that sends such code "as a patch". The planning thread's entry of 06:29Z plans that change for after the
merge. It was written before the answer.

**G27 and G29, the notes.** Each of G27's seven things is answered as the planning thread's entry says. The aims in
play are now "in the words of the old step list in "Lemuria: pre-production"", and that list does head each of them
"Gate". One sentence covers the old numbers in entries of record/. New creatures stand once, as out of scope for v1.0.
The one sentence among the standing rules that is new says so. A short name stays once its feature's brief is put up.
The tables of part 4 are brought up to date before the builders are ever split. The old paper and the old glossary are
tied to the state that G22 sets down, the glossary to revision 247 of its document. G22 does list what the book says
it lists: eleven Docs with their revisions, and the ten diaries, seven zips and three sheets with size and checksum.
For G29, the shared files table now says that CLAUDE.md and README.md keep the repository rules, that these bind
whoever writes code, that the Commandments and the book stand where one of them differs, and that mason's rewrite
keeps them. The minute the rules took force cites R19, in the head of the book and in part 4.

**What I did not check.** The owner's message of 2026-10-22, as said. And I ran none of the code for this report: the
five places that G40 names are read from main.

**For the planning thread.** Two ways are open.
1. *Correct before the merge.* One more commit can hold all three places: the last sentence of standing rule 7 (G40),
   the one sentence of point 2 (G41), and point 3 after R24. It voids this report (rule 4). I read that one commit as
   soon as the coordinator wakes me, and the owner then merges a book that owes nothing.
2. *Merge as it stands.* Cite this entry in PR #16's description as the review thread's report G39 in
   record/findings.md, then assign the owner (rule 4). After the merge, G41 and point 3 go by one change to part 3,
   which I can certify. G40 lies in the concept: it goes by another PR that I read and the owner merges, or by the
   owner's command, and every thread then reads the book again (rules 4 and 5).

The first costs the owner one merge, and the threads one more round before the book is ready. The second lets the
owner merge now and mason start, and costs the owner one more merge or a command. Either way G41 should be in before
mason puts up the first of the ruled fixes, and G40 before the briefs of features 4, 6, 8 and 9 are put up. The choice
is the planning thread's and the owner's.

**For the owner.** Nothing here asks you to wait. If you merge PR #16 as it stands, three places in it are changed
after it, and one of those changes needs your merge or your word. If the planning thread corrects first, PR #16 comes
to you once, complete.

**Open after this report.** Flaws G5 (the precursor; the coordinator until it starts), G7, G13 to G16 and G43 (mason;
held by the coordinator until it starts), G40 and G41 (the planning thread). G4, G24 and G25 are corrected in PR #16
at commit 84ace1d and close when that commit merges. No blocker is open. G42 is a note and needs no answer.

## G40. Flaw: the new last sentence of standing rule 7 says "by hand", the book's own mark of a naked evocation

**Rank.** Flaw, naming step 1. **Owning thread.** The planning thread. **Raised in.** G39, against part 1 of the book
(PR #16 at commit 84ace1d). **Status.** Open.

The sentence is new with the second commit and answers the third point of G25: "What a player breaks or places by hand
inside another player's claim stays as the game rules it, whatever the switch says: the owner's words open Lemuria's
blocks there to use and to nothing more (the card of 2026-11-10, and R9)." The sentence before it says: "With it off,
Lemuria's workings no longer spare a claim".

What it means to say is right, and the planning thread's entry gives the reason for the two words: "with the switch
off, Lemuria's workings do break and place blocks inside a claim, as when a Tremor raises the ground there. Without
"by hand" the new sentence would contradict the one before it." So "by hand" is there to keep the game's own breaking
and placing apart from Lemuria's workings.

In this book the two words do not do that. "By hand" is the book's own mark of a naked evocation:
- "spend their charge: first by hand in naked evocations" (What Lemuria is);
- "Each alignment has one *naked evocation*, its single use by hand" (The crystal cycle, 6);
- "A naked evocation is an alignment's one working by hand" (By hand, by vehicle, by book);
- feature 6 is "Casting by hand".

And an evocation breaks and places. The table of alignments has "Tremor: raises a pillar or mound" and "Wither:
withers plants". So the Tremor of the entry's own example is, in the book's words, a working by hand that places
blocks. With the switch off, one sentence has it reach into a claim, as one of "Lemuria's workings". The next has it
stay "as the game rules it, whatever the switch says", as what a player places by hand.

**Two readings, and different work.**
1. The last sentence speaks of the game's own breaking and placing of blocks, about which Lemuria's code is never
   asked. A naked evocation and the Crystalline Chisel in a player's hand are workings, and they follow the switch.
2. The last sentence speaks of everything that a player does in person, a naked evocation included. Then only what
   machines do follows the switch.

Under the first reading the briefs of features 4, 6, 8 and 9 are written as built. Under the second, main is wrong in
five places, each of which asks the switch before a player's own act changes a block: `CastContext.Can`
(Lemuria/Evocations/CastContext.cs), `ItemCrystalChisel.Carve` and `Awaken` (Lemuria/Sparkstone/ItemCrystalChisel.cs),
`MachineCarver.Strike` and `BlockBehaviorRodMaker.Make` (Lemuria/Machines/).

**What the book itself, the code and the old paper say.** The first reading, on every count.
- The rule's own first sentence: "Every land claim rule in Lemuria goes through one switch, so that none can drift".
  Under the second reading one claim rule of Lemuria's would not go through it.
- The head of the standing rules: "One sentence among them is new with this book and says so: the last of rule 3."
  Under the second reading this sentence would be new as well, because it would change what is built.
- The old paper: "One switch covers every claim rule in Lemuria, from evocations to the chisel, racks and jars" (the
  note of old step 3, given there as the owner's of 2026-10-27, 20:50Z).
- The code, as above, and the planning thread's entry, as quoted.

**Why I raise it all the same.** It is the concept, where rule 1 leaves a thread no choice: "the exception never
covers a reading of these rules or of the concept", and a question about it stops every thread. Mason writes the
briefs of features 4, 6, 8 and 9 next, and each must say what its workings do inside a claim. A builder who holds
these two sentences against each other word for word has a doubt, and there a doubt is a stop for everyone. Standing
rule 3 of the same book asks for "One meaning to a word".

**My part in it.** G25 proposed the sentence without the two words: "breaking and building inside a claim stay as the
game rules them, whatever the switch says". That was worse, and the planning thread saw it. The gap is in my proposal
as much as in the book.

**To correct it.** Name the game's own breaking and placing, and drop "by hand". For instance: "The game's own
breaking and placing of blocks inside another player's claim stay as the game rules them, whatever the switch says:
the owner's words open Lemuria's blocks there to use and to nothing more (the card of 2026-11-10, and R9)."

**When.** As with G25. Before the owner's merge it changes by one more commit to PR #16 and a new report of mine.
After the merge it changes only by another PR that I read and the owner merges, or by the owner's command, and every
thread then reads the book again (rules 4 and 5). It should be in before the briefs of features 4, 6, 8 and 9 are put
up. I read those briefs for it either way.

**Why not a blocker.** One reading has the rule's own first sentence, the code, the old paper and the author's entry
behind it, and the next work can be built on it. If a thread stops on the sentence all the same, this entry says what
the record already holds on the question (rule 1).

## G41. Flaw: one sentence of "Where features meet" shuts out the fixes that R3 and R7 let in

**Rank.** Flaw, naming step 1. **Owning thread.** The planning thread. It concerns mason. **Raised in.** G39, against
part 3 of the book (PR #16 at commit 84ace1d). **Status.** Open.

Point 2 of the new section says: "No code goes into a feature's folders before that feature's own brief is settled and
describes it (rule 2)." Further down, part 3 says of the nine built features, which have no briefs yet: "Until then
they take only the fixes already ruled (R3)."

By their letter the two disagree. The fixes already ruled are R7's: "The two unbuilt fixes and PR #9's unsaved death
hour then go as patches. Finding 16 changes what you notice in play, so it waits for your merge." They are G13, G14
and G15. Each is code that goes into the folders of a built feature, G13 and G14 into feature 8's and G15 into feature
6's, before that feature has a brief.

The planning thread's entry shows what the sentence is for: "So the nine briefs of R3 come before any such code",
which is code that a later feature needs in a built feature's folders. The book's next sentence has that limit in its
own words, "for a later feature". The sentence itself has none. It is also wider than the rule it cites, which says
"experiments aside".

**The work it would change.** R3 lets the three fixes go before the nine briefs are merged. A mason that reads point 2
by its letter holds them back until then, or stops on the two sentences (rule 1).

**To correct it.** Give the sentence the limit that its neighbour has. For instance: "No code for a later feature goes
into a feature's folders before that feature's own brief is settled and describes it (rule 2)." Or keep it whole and
add "the fixes already ruled aside (R3)".

**When.** Before mason puts up the first of the three fixes. It is part 3. It can ride in PR #16 now, or after the
merge in a change that I certify (rule 4), together with what the owner's answer to R23 changes in point 3.

**Why not a blocker.** R3 and R7 are the owner's own words and stand in rulings.md, and the book repeats R3 in the
same part. The next work, the nine briefs, is not built on the sentence.

## G42. Note: the counter-view to the Road Bible's second commit (rule 6 a)

**Rank.** Note. **Owning thread.** The coordinator, for the owner. **Raised in.** G39. **Status.** Needs no answer.

G30 argued against five choices in the first commit. It stands: the second commit changed nothing for its first four
points, which are the owner's to weigh. Here I argue against four things that the second commit added. Two of them it
took from me. None holds anything.

1. **The folder decides, so a brief describes a folder.** It is the right cure for the gate (G24), and it has a price.
   What the player meets as one thing is written in as many briefs as it has folders: the duets will stand in their
   own brief and, for all that Memory does for them, in the machines'. And a built feature's brief does not rest once
   it is settled. Every later feature that reaches into its folders changes it first, by a PR that I read and the
   owner merges. PR #9 reached into six features. The book lets such changes ride with the new feature's brief, which
   saves merges and leaves the reading. The other way to pay less is to reach in less: a feature keeps its work in its
   own folders and asks of the other only a small opening. The book does not ask a brief for that. It could: where a
   brief names an existing part that its feature changes, the brief says why that work cannot lie in the feature's own
   folders.
2. **Step 7 lists no feature.** G24 offered this, and I did not say then what follows. The first and second tests of
   rule 7's step gate, and half of the fifth, speak of "every feature listed under the step" and of PRs that carry "a
   feature of the step". With no feature they test nothing. Step 7 then passes on how the PRs since step 6's gate were
   merged, on the checks, and on the answers in player-diary.md. Its own work, the Hidden Vocabulary and the Journal
   made complete and the balance, goes as changes to briefs and as patches to features of other steps and of none, and
   no test asks whether those are merged. What stands in for the tests: the coordinator, who reports the step done; my
   comparison of the step with the book at rule 6 (b); and the owner, who alone releases. It costs little, because no
   step follows step 7 and the release is the owner's own act. If the owner wants more, step 7's entry can name what
   must be merged before the step is reported done, by a change to part 3 when the step is near.
3. **One play before a gate.** The sentence that the multiplayer test "is played before step 7 is reported done" is my
   own proposal (G30, point 5), and the planning thread took it. The owner should see what it does: it is the one
   place in the book where a play of the owner's stands before a gate. Rule 3 says of the owner's play of a step's zip
   that it "holds no gate". The planning thread's entry says why the two do not collide: the test is work that the
   step itself lists, as the old step list had it. I agree. The owner may strike the sentence at the merge, as the
   PR's description says.
4. **History in the concept.** The owner's idea of 2026-10-22 now stands in the concept whole, with two things in it
   that the concept has since overruled: "No GUI", and the "flying" block structures. One sentence fences them off,
   and as far as I can see the fence holds (G39). The other way was to keep the concept to what binds and to name the
   idea in part 4, beside the scratchpad that holds it. The owner's own first words at the head of the owner's book
   may well be worth the fence. I say only that the concept is the one part where every sentence can stop every
   thread.

## G43. Flaw: the smoke test fails by chance, and each failure holds a PR that should merge itself

**Rank.** Flaw, naming step 1. **Owning thread.** Mason (held by the coordinator until mason starts). **Raised in.**
This entry. It takes G37 further. **Status.** Open.

G37 said of one failed run that it "becomes a finding against the test or the Motor if it shows again". It has shown
again, on another line.

- **06:39Z, my own run** (G37): "Machines test: the quern turns: no". The next run passed on the same commit.
- **06:58Z, the check "build" on PR #33,** a record-only PR of the record thread's. I have this from the record
  thread, through the coordinator, in its words. The log had "Mysteries test: a Death Focus Nothing, the flower is
  flower-lupine-blue-free, 0 item(s) in the chest touching it", and the script said "FAIL: mysteries.expect: no log
  line matches: Mysteries test: a Death Focus Acted, the flower is gone, [1-9][0-9]* item\(s\) in the chest touching
  it". It ran the job again once and changed no code. I saw that second run of the job (run 43171611369) start at
  06:59:30Z and end green at 07:02:16Z. I could not open the log of the first run: the tool gives me a job's last run
  only.

Nothing outside record/ differs between main today and commit c07322e of 03:47Z, so no change to the code lies between
the runs that passed and the two that failed.

**The two checks.** They are not alike. `MotorCheck` reads the quern's speed seven one-second commands after the Motor
starts, so it rests on timing (G37). The Death Focus check (Lemuria/Mysteries/MysteriesTests.cs, lines 774 to 787)
sets down a flower, a Focus above it, a chest and a charged Death crystal, and calls `WorkOnce()` in the same command.
It waits for nothing. What differed between two runs of the same code I have not found out.

**What it costs.** The check "build" runs the whole smoke test on every PR, record-only ones included: the book's
shared list says that the setup list's skip of the heavy steps "is still to be made". Every entry of the record
reaches main by a PR that merges itself when that check is green. A failure by chance leaves such a PR open until a
thread sees it and runs the job again. PR #33 stood so until 07:03Z, and the entry that carries the owner's answer to
R23 waited behind it (the coordinator's words). At a step gate the same chance can fail "the latest run of the full
checks on main" (rule 7).

**To correct it.** Two things, and either helps alone. The skip that the setup list names, so that a record-only PR
does not run the smoke test at all. And the two checks made to wait for what they test.

**Whose it is.** The scripts, the checks workflow and the two test files are mason's, under the book's shared list and
features 8 and 9. Mason has not started, so the coordinator holds this with G7 and G13 to G16. Until it is corrected,
a red check on a PR that changes nothing outside record/ cannot come from that PR.

**Why not a blocker.** Nothing is built on it, and a second run has cleared each failure so far.

## G44. Diary: the second report on PR #16; the rules are version 4; the owner has answered R23

**Written.** 2026-11-12 07:12Z.

**The rules.** The owner merged PR #21 at 06:52:24Z, and the coordinator told me at 06:54Z. I read the rules again in
full before I went on, at about 06:56Z: version 4, 189 lines, byte for byte the file of commit 7de344f that G32 read
(sha256 084d4307ca14cd82f004448b2ed70a1b2ac10f9e0c8f6c8440dd091df7178f03). What R21 asked of me is now the rules' own
text: a word that the owner gives me directly goes, word for word, to the coordinator and to the record thread, and I
record none of it myself.

**What I did at this wake.** The coordinator woke me at 06:38Z for PR #16's second commit (rule 6 a). The report is
G39, with two flaws for the planning thread (G40, G41) and the counter-view (G42). On the way the smoke test failed by
chance a second time, on a PR of the record thread's (G43). What changed in the record since G38 is listed in G39.

**The planning thread's two sentences for rule 4.** They stand in its entry of 06:47Z, and the coordinator sent them
to me at 06:51Z "for your knowledge; nothing is asked". I have read them and report nothing on them here. They come to
me under rule 6 (e) when the change to the rules is put up, and I read it then against G36, against the owner's answer
and against the owner's own message.

**The owner's answer to R23.** R24 is on main since 07:10Z, and I held it against the owner's own message (G39). The
coordinator also sent me the owner's second message, of 07:08:14Z, and I read it myself in the project chat. R25 is to
hold it. Both messages were given to the coordinator and not to me, so I had nothing to send on.

**PR #35 stood green and unmerged.** Its check was green at 07:06:30Z, and at 07:10Z it had not merged itself, as my
PR #25 had not (G38). I told the coordinator, for the record thread. It merged at 07:10:45Z. I do not know by whose
hand.

**Status lines changed in this PR.** G4, G10, G24, G25, G27, G29, G36 and G37, each citing the entry that changed it.

Next:
- **Unfinished:** nothing of this wake. If the planning thread makes a third commit to PR #16, G39 is void and a new
  report is owed.
- **Where it is:** this file, sent to main by a record-only PR from the branch claude/project-thread-4zobvq.
- **Waiting for:** the coordinator's next wake. I expect, in some order:
  - a third commit to PR #16, if the planning thread corrects G40 and G41 before the merge (rule 6 a);
  - the change to rule 4 that R24 calls for, as version 5 (rule 6 e), read against G36, R24 and R25;
  - the change to part 3 that follows it, in point 3 of "Where features meet", which I certify if it touches part 3
    alone (rule 4);
  - the nine briefs of the built features (R3, rule 6 a), where I read for G12's last point, G16, G20, G29 and G40;
  - step 1's brief (rule 6 a), where I read for G24 and G41;
  - the first maps (rule 6 f): the planning thread's after PR #16 merges, the coordinator's, mason's, and G5 at the
    precursor's.
- **Open findings:** flaws G5 (the precursor; the coordinator until it starts), G7, G13 to G16 and G43 (mason; held by
  the coordinator until it starts), G40 and G41 (the planning thread). G4, G24 and G25 are corrected in PR #16 at
  commit 84ace1d and close when that commit merges. No blocker is open.
- **Read up to:** rulings.md R24, player-diary.md none (the file does not exist), findings.md G44.

## G45. Report under rule 6 (e): the change to the rules that lets code ride in a feature's PR (PR #39 at c2a7e64)

**Written.** 2026-11-12 07:49Z. Woken by the coordinator at 07:32Z, with the planning thread's own words of 07:32:10Z
that the PR is ready.

**What I read.** PR #39, "Rules: version 5 lets code ride in a feature's pull request", branch rules/version-5, at
commit c2a7e64976402516c8bca0b08e391cddf0cc9ade, its one commit, on top of main at 957cb2b. Its check "build" ended
green on that commit at 07:31:49Z. It is not assigned to the owner. At 07:49Z it was still open with that check green,
so it is not set to auto-merge, as rule 4 has it. Main has moved to 011cf88 since, in other files, and the branch
still merges cleanly. By rule 4 a later commit voids this report, unless it only brings main into the branch or adds
only the author's own diary entry.

**The rules.** Read again in full at this wake, before anything else: version 4 as main has it, 189 lines, the file
that G44 read (sha256 084d4307ca14cd82f004448b2ed70a1b2ac10f9e0c8f6c8440dd091df7178f03). This report is held against
version 4 and against R24, which binds from its entry (rule 1).

**What changed in the record since G44 (rule 6).**
- PR #36, merged at 07:14Z: R25, with the coordinator's entries of 07:08Z and 07:09Z and the record thread's of
  07:08Z. The owner's words in R25, "Correct reading, good job.", are the owner's message of 07:08:14Z, character for
  character. The post of the coordinator that the owner answered stands in R25 word for word: I read both in the
  project chat (below).
- PR #37, merged at 07:16Z: my own G39 to G44.
- PR #38 and PR #40, merged at 07:26Z and 07:36Z: the coordinator's entries of 07:21Z and 07:32Z and the record
  thread's of 07:22Z. The coordinator posted G42 to the owner whole at 07:21:54Z, as the project chat shows, and holds
  G43 for mason.
- PR #41, merged at 07:38Z: the planning thread's entry of 07:32Z, which halts and ends with "Next:". It gives its
  reasons for handing PR #16 to the owner as it stands, and says what that costs. It also holds drafts of the three
  sentences that are to change after PR #16 merges. They are in no branch, and I report nothing on them here: each
  comes to me when its PR is put up.
- The last entry of rulings.md is R25. There is still no player-diary.md, no brief, no map and no Road Bible on main.

**The other PR I have reported on.** PR #16 is at commit 84ace1d still, so G39 stands. Its description was changed at
07:24Z. It cites G39, in my words and rightly, and sets out the three sentences that change after the merge (G40, G41,
and point 3 after R24). It is assigned to the owner.

**The short of it.** No blocker. Nothing here asks the owner to wait, and nothing asks for a change to PR #39. The
change adds to rule 4 the two sentences that the owner took as drafted (R24, R25), at the place the draft names, and
moves the head to version 5. No other word of the rules differs. I found no sentence of the rules that it contradicts,
and no code that it makes wrong. R24 already binds (rule 1), so the merge only brings the file into line with what
binds today. It also answers the one ask of G36: "if it is B, let the rules say it." What I would weigh against the
two sentences is the counter-view, G46. It holds nothing.

**What the change is, read from the diff.**
- record/COMMANDMENTS.txt differs in three of its 189 lines, and grows by 345 bytes.
  1. The head: "version 4" becomes "version 5", and the line under it "Ratified 2026-11-12. Replaces version 4
     (2026-11-12)."
  2. Rule 4, the paragraph that begins "A patch carries a fix and nothing more.": two sentences at its end.
- record/diary/optimist-visionary/2026-11-12T0725Z.md is new: the planning thread's entry for this PR (rule 5).
- Nothing outside record/ changes.

The end of that paragraph in version 5. The first sentence is version 4's last, and the other two are new:

> A change to code on main that follows a change to its brief is a patch, and its verdict is "out": it waits for the
> owner's merge. Where a thread's feature needs such a change in the folders of another feature that the thread owns
> or has been handed, the change may instead ride in the PR that carries the feature, once the change to the other
> feature's brief is merged. That PR's description names each other feature it changes and the merged change to that
> feature's brief.

**Held against the owner's word.** I took neither the PR's word for it nor R24's. I read the project chat itself, from
06:18Z to its last message at 07:24Z, and held the server's copy of its messages against the file by program.
- The coordinator had put three posts before the owner: the planning thread's question with the two ways A and B
  (06:19:04Z), my view on it in my own words, which is G36 in short (06:38:19Z), and the planning thread's draft
  (06:51:16Z). The draft names the sentence of rule 4 that the new words stand after, gives the two sentences, and
  says: "Answer "B" to take these words, or change any of them."
- The owner answered "4.) B" at 06:56:33Z. The coordinator posted how it read that at 06:56:53Z, and asked to be told
  if "4.)" meant something other than rule 4. The owner answered "Correct reading, good job." at 07:08:14Z. So the
  owner took the draft and changed none of it. R24 and R25 hold the owner's two messages and the coordinator's second
  post as the chat has them. Of the post with the draft, R24 holds the quoted part: the planning thread's words, and
  all of it that bears on the rules.
- The text that version 5 adds to rule 4 is 344 characters. It stands in the post of 06:51:16Z, as the owner saw it,
  once and character for character. The sentence that the post names as the place stands in rule 4 once, and in
  version 5 the two sentences follow it directly and end the paragraph.
- Apart from the head and that addition, version 5 is version 4: 186 lines are the same, byte for byte, and line 101
  is version 4's line with the two sentences at its end. The file is plain ASCII as before and ends with one newline
  (sha256 d8d96a29a51f607ae995a82feee38f33f7c1e1947fd4d9c8ea029a30555522b1).

**What the owner's word leaves to the writer.** The PR and its entry name two things. I agree with both.
1. The head goes to version 5, by the rules' own words: "the version number goes up each time". The date is the day of
   R24 by the record's clock, as version 4 has the day of R21 (G32).
2. The two sentences go on in the paragraph on the patch and begin no new one. The draft names the sentence they stand
   after, and "such a change" has to find that sentence right before it.

**Does it contradict another rule?** I read the two sentences against every place in the rules that speaks of a patch,
of a feature's PR, of a brief that changes, or of the folders a thread may change.
- **The words "patch" and "fix".** A patch is "the class of PR that carries a fix to code already on main". The change
  that rides is no PR of its own, so it is no patch, and the gate on a patch (rule 7) is never asked about it. For the
  change that comes alone the sentence before still holds: a patch, and its verdict is "out".
- **Rule 4's list.** "A feature: merged by the owner." The PR in which the change rides is a feature's PR. So both
  ways end in the owner's merge, and no way to main is opened that does without it.
- **The order of book and code.** Rule 4 has "that brief is changed first", and rules 1 and 3 have the book changed
  "before any code follows". The new sentence keeps that order and says it more exactly: "once the change to the other
  feature's brief is merged".
- **The cast.** "Outside record/, a thread changes only the folders and files it owns under the Road Bible, or has
  been handed under these rules." The new sentence carries the same bound, in the words of rule 7's gate on a patch:
  "owns or has been handed". It loosens nothing. A feature's PR still never reaches into another thread's folders.
- **Rule 2.** "No code for a feature before its brief is settled". Before the code rides, both briefs are settled: the
  feature's own, and the other feature's with its change merged.
- **Rule 3.** "A thread may have one feature that is not yet merged". The code that rides is part of that one
  feature's PR, so the count stays.
- **Rule 4, "A PR is ready when".** That list is not made longer. The second new sentence adds a duty for one kind of
  PR and binds by itself. Who looks whether it was kept is the first point of G46.
- **Rule 7, the gate on a step.** Not touched, and it now reaches further. Code that rides is in "a PR that carries a
  feature of the step", so all five tests reach it. Under way A the change is a patch to a feature that may stand
  under no numbered step, and then only the third test reaches it (G36).

**What may ride, by the letter.** "such a change" points back to "A change to code on main that follows a change to
its brief". So the two sentences open one case. The planning thread's entry reads them the same way: "the new
sentences open that one case and no other".
- What may ride is code in the folders of another feature that the same thread owns or has been handed, once the
  change to that feature's brief is merged.
- A fix that leaves the other feature's brief true as written follows no change to a brief. The two sentences do not
  speak of it, and nothing changes for it.
- The shared files are no feature's folders. Nothing changes for them either.
- "may instead" leaves the first way open: any such change can still come alone, as a patch with the verdict "out".

**Code this change would make wrong (rule 4).** None.
- The change opens a second way and forbids nothing that version 4 allows. So nothing that is on main can stand
  against it, however it came there.
- I searched everything outside record/ for whatever names or tests the folders that a PR may change. No code, script
  or check does. The workflow (.github/workflows/build.yml) and the three scripts build and test the whole project,
  and none of them looks at which files a PR changed.
- Two texts outside the record speak of it: CLAUDE.md lines 30 to 33, "Stay inside your feature's files", and
  README.md under "Working in parallel", "The layout lets branches stay out of each other's files". They are narrower
  than version 5 and not against it: the new sentence says "may", and a thread that keeps each PR to one feature's
  files breaks no rule. G7 already holds both files, R10 has mason rewrite them in its first PR, and the book in PR
  #16 says that the Commandments stand where the two files differ from them. For that rewrite: those lines should say
  what R24 allows. G7's Status says so now. As in G2 and G32, the two files do not go on the list: neither is code,
  and what they say here is narrower than the new sentence and breaks nothing.

**What the change leaves behind in the record.** One sentence, and it is known.
- The Road Bible in PR #16, part 3, "Where features meet", point 3, still has such code go "as a patch", "Until the
  owner has answered" (G39). PR #16's description says so now. The planning thread changes the point after the merge,
  by a change to part 3 that comes to me for the certificate (its entry of 07:32Z). Until then R24 binds (rule 1).
- G36 says that by its letter rule 4 holds A. That was said of version 4, and G36's Status says so now.

**The planning thread's entry and the PR's description.** I held what they say against the record and the diff. They
are right in all that I could check: the three lines that differ, the two checksums, how the words were copied, what
G36 asked, and the quotation from G36. For each part of the two sentences the entry says what it is for and why not
another word, and for each sentence of the rules that it leaves alone it says why. I agree with each. It also says two
things of its author that a reader could not otherwise know. Its context was cut down to a summary between 07:13Z and
07:16Z, and it read the rules again in full after that. And the "07:19Z" at the head of two of its messages was a time
it had not read: the one that reached me is stamped 07:18:43Z. That is how rule 5 would have a diary be.

**For the planning thread.** Cite this entry in PR #39's description as the review thread's report G45 in
record/findings.md, then assign the owner (rule 4). The branch merges cleanly as it stands. If you bring main into it,
this report still stands.

**For the owner.** Nothing here asks you to wait or to change PR #39. PR #16 and PR #39 share no file, so you can
merge them in either order. When you merge PR #39, the coordinator tells every thread, and each reads the rules again
before it goes on (rule 4). No code becomes a blocker at that merge.

**Open after this report.** As in G44, and nothing is added: flaws G5 (the precursor; the coordinator until it
starts), G7, G13 to G16 and G43 (mason; held by the coordinator until it starts), G40 and G41 (the planning thread).
G4, G24 and G25 are corrected in PR #16 at commit 84ace1d and close when that commit merges. No blocker is open. G46
is a note and needs no answer.

## G46. Note: the counter-view to version 5 (rule 6 e)

**Rank.** Note. **Owning thread.** The coordinator, for the owner. It concerns mason. **Raised in.** G45. **Status.**
Needs no answer.

The owner chose B with the case against it in hand: the coordinator had posted my view to the owner, in my own words,
eighteen minutes before the answer (G45). And the change writes the words that the owner took, and no others. So this
holds nothing. G36 argued the choice between A and B, and I do not argue it again. This is what I would weigh against
the two sentences as they stand.

1. **Two duties that no gate tests.** The sentences set an order, the other feature's brief first, and a duty, that
   the description names each other feature and the merged change to its brief. Rule 4's list asks one thing of a
   feature's PR: the owner's merge. No verdict is asked on it, and I am not woken for it. So before the merge only the
   author looks whether both were kept, and the owner merges the PR "from its description" (rule 3), which is the very
   text in question. After the merge, rule 7's third test asks whether the PR "went the way rule 4 sets for its kind".
   As I read that test, the owner's merge meets it. Under A nothing had to be named: each such change came alone, in a
   PR whose whole diff lay in one feature's folders. What I will do: at each step gate (rule 6 b) I read the diffs of
   the step's feature PRs, list each one that changed another feature's folders, and say whether its description named
   them and whether the change to each brief was merged first. That is report, and the owner may tell me to leave it
   out. A help that costs mason one line: the description gives the changed folders, feature by feature, as a program
   lists them from the diff.
2. **A brief runs ahead of main while the feature is in the making.** From the merge of the change to the machines'
   brief, that brief describes a Memory that main does not hold, until the duets' PR merges. Under A the patch could
   follow the brief at once. Under B the gap lasts as long as the feature takes. In that time a reader of the brief
   cannot tell "not built yet" from "untrue", and a patch to the same feature is judged against a brief that main does
   not yet meet (rule 7: it "leaves that feature's brief true as written"). If the feature's PR is given up or
   reverted, each brief it reached into stays ahead until it is changed back, which takes the owner's merge or the
   owner's command (rule 4). A help that costs a clause: each such change to a brief names the feature that brings the
   code. I will look for that when the changes to the briefs come to me (rule 6 a).
3. **One PR, one fate.** The code that rides and the feature merge together and are reverted together. Under A the
   owner could merge a patch and hold the feature, or take a feature back and keep its patches. Under B what the owner
   does not like in the change to Memory holds the whole of the duets. "may instead" keeps the cure at hand: the owner
   can still ask for any one change to come alone, as a patch.
4. **The saving lasts as long as the one builder.** The code that rides must lie in the folders of a feature "that the
   thread owns or has been handed". Mason owns every feature (R5, R15), so in Lemuria every case is covered today. R5
   leaves a later split of the builders open. From such a split on, a feature that reaches into another builder's
   folders goes back to way A, as that builder's patch, with the outcome in rulings.md before either acts (rule 1).
   The bound is the cast's own and no fault of the sentence. It is a cost to count on the day a split is weighed.
5. **Two rule texts, one more difference.** The owner means to bring this text and the sister project's into line at a
   later time (R18). These two sentences are one more place to compare then. Neither the planning thread nor I have
   read the sister project's text for them.

## G47. Diary: halt after the report on PR #39

**Written.** 2026-11-12 07:49Z.

**The rules.** At this wake I read them again in full before anything else, between 07:33Z and 07:36Z: version 4 as
main has it, the file that G44 read. Version 5 is PR #39's. Its two sentences bind already, as R24 (rule 1).

**Out of room.** Between 07:15Z and 07:17Z my context ran out of room and was cut down to a summary. G44, with its
"Next:", was written and pushed before that, at 07:12Z, and nothing was unpushed. I went on from the summary, from G44
and from the record.

**What I did since G44.**
- PR #37, with G39 to G44, merged itself at 07:16Z. Its check was green at the first run.
- The planning thread wrote to me that it takes the second way of G39: no further commit to PR #16. At 07:21Z I told
  it and the coordinator that G39 was on main, with three facts for what follows: that PR #16's description still
  spoke of R23 as open, which it has since put right; that I can certify the change to part 3 only if it touches part
  3 alone (rule 6); and that I hold point 3 against what rule 4 says on main at that commit.
- I held R25 against the owner's own message, and at this wake R24 and R25 against the project chat itself (G45).
- The coordinator woke me at 07:32Z for PR #39 (rule 6 e). The report is G45, and the counter-view is G46.

**The checks before this PR.** I ran the three scripts at 07:41Z on main at 011cf88, the commit this PR's branch
starts from. All three passed, and the smoke test did not fail on either of the two lines that failed by chance
earlier today (G43).

**Status lines changed in this PR.** G7 and G36, each citing G45.

Next:
- **Unfinished:** nothing of this wake. A later commit to PR #39 voids G45, and a later commit to PR #16 voids G39,
  unless it only brings main into the branch or adds only the author's own diary entry. A new report is then owed.
- **Where it is:** this file, sent to main by a record-only PR from the branch claude/project-thread-4zobvq.
- **Waiting for:** the coordinator's next wake. I expect, in some order:
  - the rules again, in full, once the owner merges PR #39 (rule 4);
  - after the owner merges PR #16, the change to part 3 for G41 and for point 3 after R24, which I certify if it
    touches part 3 alone, after talking to the planning thread or the coordinator (rule 6);
  - the change to the concept for G40 (rule 6 a), which I read and the owner merges;
  - the first maps (rule 6 f): the planning thread's after PR #16 merges, the coordinator's, mason's, and G5 at the
    precursor's;
  - the nine briefs of the built features (R3, rule 6 a), where I read for G12's last point, G16, G20, G29 and G40;
  - step 1's brief and the changes to other briefs that come with it (rule 6 a), where I read for G24, G41 and the
    second point of G46.
- **Open findings:** flaws G5 (the precursor; the coordinator until it starts), G7, G13 to G16 and G43 (mason; held by
  the coordinator until it starts), G40 and G41 (the planning thread). G4, G24 and G25 are corrected in PR #16 at
  commit 84ace1d and close when that commit merges. No blocker is open.
- **Read up to:** rulings.md R25, player-diary.md none (the file does not exist), findings.md G47.
