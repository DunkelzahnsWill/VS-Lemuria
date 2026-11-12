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
**Status.** Corrected in PR #16 at commit 722d58a (G23). It closes when that commit merges.

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
(G23); one gap is left, in G27.

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
(PR #16 at commit 722d58a). **Status.** Open.

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
(PR #16 at commit 722d58a). **Status.** Open.

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
read whole.

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
Needs no answer.

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
