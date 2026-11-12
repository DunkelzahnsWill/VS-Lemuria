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
**Status.** Closed. Corrected in PR #16 (G23, G39), which the owner merged at commit 84ace1d on 2026-11-12 at 16:52Z
(G48).

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
**Raised in.** G2. **Status.** Closed. Corrected by the precursor's first entry, of 2026-11-12 17:24Z, on main since
PR #53 (G52). G22 gives the state of each piece as I found it on 2026-11-12.

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

**Rank.** Flaw, naming step 1. **Owning thread.** Mason, the one builder (R5, R10, R15). Mason is not started yet, so
the coordinator holds this for it. **Raised in.** G2. **Status.** Closed. Corrected in PR #51 at commit 0bb7c46 (G54),
which the owner merged on 2026-11-12 at 18:26Z (merge commit b841cb8).

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
**Carried from.** The old audit, under its finding 10 ("Not fixed yet"). **Status.** Closed. Corrected by PR #75 at
commit 31e770e, merged on 2026-11-12 at 19:19Z (merge commit e02ef64) on the inquisitor's verdict of "in". G69 says
what I held the fix against.

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
**Carried from.** The old audit's diary, last entry. It is not in the old report. **Status.** Closed. Corrected by
PR #75 at commit 31e770e, with G13 (G69).

The same gap as G13, one line down. `deathBoostUntil` (BlockEntityMachine.cs line 56, set at line 454, read at line
415) came in with PR #9 and is not saved either, while a standing Death crystal and a cradle save theirs
(BlockEntityCrystal.cs lines 321 and 336, BlockEntityCradle.cs lines 797 and 823). The old audit's last entry says
the step 5 build thread was never told. The patch file holds no test for it. One patch can carry G13 and G14: both
sit in the same two functions of one file.

## G15. Flaw: a Fire cast at a block that takes the flame and stays cold still pays (old finding 16)

**Rank.** Flaw, naming step 1. **Owning thread.** Mason (held by the coordinator until mason starts).
**Carried from.** The old audit's finding 16 ("Noted only", then "Not fixed yet"). **Status.** Closed. Corrected by
PR #78 at commit 77e744f, which the owner merged on 2026-11-12 at 19:35Z (merge commit 0af9ea7; R7, R27). G69 says
what I held the fix against.

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

**Rank.** Flaw, naming step 1. **Owning thread.** Mason (held by the coordinator until mason starts). **Carried
from.** The old audit's finding 9, "Still with you", since answered by R9. **Status.** Closed. Corrected by five
patches, each merged on 2026-11-12 on the inquisitor's verdict of "in": PR #87, PR #88, PR #84 and PR #92 between
19:32Z and 19:38Z, and PR #125 at commit 3ef063c at 20:52Z (merge commit c5da519), for the sockets of a carved Crystal
Motor, which the first four had left refused. G85 says what I held them against, and G86 how the fifth merged. One
sentence below was too wide: G56 says for which uses the lift is needed.

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
(PR #16 at commit 722d58a). **Status.** Closed. Corrected in PR #16 at commit 84ace1d (G39), which the owner merged
on 2026-11-12 at 16:52Z (G48). One sentence of the correction is G41, and the last point of this entry stays with the
owner (G36).

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
(PR #16 at commit 722d58a). **Status.** Closed. Corrected in PR #16 at commit 84ace1d (G39), which the owner merged
on 2026-11-12 at 16:52Z (G48). The new last sentence of standing rule 7 is G40.

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
(PR #16 at commit 84ace1d). **Status.** Closed. Corrected in PR #46 at commit 2e873bf (G49), which the owner merged on
2026-11-12 at 18:25Z (merge commit df41e04).

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
part 3 of the book (PR #16 at commit 84ace1d). **Status.** Closed. Corrected in PR #45 at commit c312aa3 (G48), which
the owner merged on 2026-11-12 at 18:26Z (merge commit a09f3d2).

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
This entry. It takes G37 further. **Status.** Closed. Corrected by PR #59, PR #61 and PR #62, which the owner merged
on 2026-11-12 between 18:29Z and 18:30Z (G64).

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

## G48. Report under rule 6 (a): the change to part 3 of the Road Bible (PR #45 at c312aa3)

**Written.** 2026-11-12 17:43Z. Woken by the coordinator at 17:19Z, with the planning thread's own words of 17:19:15Z
that the PR is ready.

**What I read.** PR #45, "Road Bible: part 3, code may ride with its feature (R24), and one sentence of point 2
(G41)", branch road-bible/part-3-points-2-and-3, at commit c312aa3506a60bb7060167a022683f20d64f4798, its one commit,
on top of main at ed17dad. I read the diff word by word, the planning thread's entry for it,
record/diary/optimist-visionary/2026-11-12T1702Z.md, which rides in the PR, and the description. I held the book at
that commit against the book on main by program (551 lines, sha256
04db3f459b3bf014a7670f746149fd1aeb17a6be993f6beb8acf5c3bc9e2a7ad). The check "build" ended green on that commit at
17:09:35Z. The PR is not assigned to the owner. At 17:42Z it was still open with that check green, so it is not set to
auto-merge. Main has moved to 3abcad3 since, in other files. The branch still merges cleanly, whether PR #46 merges
before it or after it. By rule 4 a later commit voids this report, unless it only brings main into the branch or adds
only the author's own diary entry.

**The rules and the book.** I read both in full at this wake: the rules, version 5, as main has them since the owner
merged PR #39 (189 lines, sha256 d8d96a29a51f607ae995a82feee38f33f7c1e1947fd4d9c8ea029a30555522b1, byte for byte the
file that G45 read), and the Road Bible as main has it since the owner merged PR #16 (548 lines, sha256
5a9388188b9b335ad8025387818ab46b03d28f2c3fd65548661155cdb4206360, byte for byte the file that G39 read at commit
84ace1d). This report is held against both, and against R1 to R25.

**What changed in the record since G47 (rule 6).**
- PR #43 and PR #44, merged at 07:59Z and 08:08Z: the coordinator's entry of 07:55Z and the planning thread's of
  07:56Z, which halts and ends with "Next:".
- The owner merged PR #16 as 26b3fea and PR #39 as ed17dad, both at 16:52Z. So the first Road Bible and version 5 of
  the rules are on main. G45 found no code that version 5 makes wrong, so nothing became a blocker at that merge (rule
  4). G4, G24 and G25 are closed by the merge of PR #16, and their Status lines say so.
- PR #47, merged at 17:18Z: the planning thread's first map. PR #48 to PR #50 and PR #52 to PR #54, merged between
  17:28Z and 17:36Z: the first maps of the coordinator, of mason and of the precursor, with entries of those threads,
  of the planning thread and of the record thread. The coordinator started mason and the precursor at about 17:22Z. I
  have read all of it. The report on the four maps comes in my next PR (rule 6 f).
- PR #51 is mason's rewrite of README.md and CLAUDE.md (R10). It is open and waits for the owner's merge. It is no
  wake of rule 6. I have read it, and my word on it comes in my next PR too.
- rulings.md holds nothing after R25. There is still no player-diary.md and no brief on main.

**The short of it.** No blocker is open against this change, and by rule 6 such a report is its certificate. The
change corrects G41, and its point 3 says what rule 4 says since version 5 and what the owner ruled in R24 and R25. I
ask for no change to the PR. All the same I hold that it waits for the owner's merge and is not set to auto-merge, on
the two doubts set out below. The planning thread agrees. The counter-view is G50.

**It touches part 3 alone.** Rule 6 lets me certify a change to the steps and features only, and says: "Any doubt
whether a change touches another part makes it the owner's". By program, lines 254 to 262 of the book on main become
lines 254 to 265, and every other line of the 548 is the same byte for byte. Those lines lie under "Where features
meet", inside part 3, which runs from line 224 to line 460. The concept, the prototype's terms and the shared list are
not touched. Point 3 names "the shared files of part 4" in a sentence that is unchanged. No brief is on main, so the
change makes no settled brief untrue.

**Point 2: the one sentence (G41).** "No code goes into a feature's folders before that feature's own brief is settled
and describes it (rule 2)." becomes "No code for a later feature goes into a feature's folders before that feature's
own brief is settled and describes it (rule 2)." The fixes that R3 and R7 let in before the nine briefs (G13, G14,
G15) are fixes to the feature in whose folders they lie, and no code for a later feature. So the sentence no longer
shuts them out, and it no longer disagrees with "Until then they take only the fixes already ruled (R3)" further down
in part 3. That corrects G41. The other words of point 2 are the same, one for one: the point is wrapped again, so
some of its lines break in new places. The entry says why it does not add "experiments aside": an experiment is never
merged (rule 2), so none of its code goes into a feature's folders on main. I agree.

**Point 3, sentence by sentence, against rule 4 as version 5 has it.**
1. The first sentence is the old point's, word for word.
2. "What the feature needs changed in the folders of another feature that its thread owns or has been handed follows
   the change to that feature's brief (point 2)." The rule speaks of "such a change in the folders of another feature
   that the thread owns or has been handed", and such a change is one "that follows a change to its brief". Point 2
   has that brief changed first.
3. "Once that change is merged, the code may ride in the pull request that carries the feature, and that pull
   request's description names each other feature it changes and the merged change to that feature's brief (rule 4)."
   The rule: "the change may instead ride in the PR that carries the feature, once the change to the other feature's
   brief is merged. That PR's description names each other feature it changes and the merged change to that feature's
   brief."
4. "The owner ruled so in R24, in answer to R23, and R25 confirms how R24 is read." That is true of all three entries.
   I held R24 and R25 against the owner's own messages in G39 and G45, and the record thread recorded all three.
5. "Such code may also go on its own, as a patch to the feature in whose folders it lies: its verdict is then "out",
   and it waits for the owner's merge (rule 4)." The rule's "may instead" leaves that first way open, and the sentence
   before it gives the verdict.

The point differs from the draft in the planning thread's entry of 07:32Z by one citation, "(rule 4)" in its third
sentence. Version 5 is on main now, so the citation is true. I looked for a sentence elsewhere in the book that the
new point contradicts: points 1, 2 and 4, "How Lemuria counts features", step 7's entry, and the row of part 4 on
README.md and CLAUDE.md. I found none. Step 7 lists no feature, so no pull request carries one there, and its "patches
that follow them (rule 4)" are the first way.

**How it merges: the owner's merge, on two doubts.** Rule 4's list gives a change to the steps and features two ways:
"certified by the review thread before it merges, or merged by the owner". I hold that this one goes the second way.
1. *Rule 4:* "A change to the Road Bible or to a brief that cites an entry its own author recorded waits for the
   owner's merge, whatever this rule would otherwise allow." The planning thread recorded R1 to R18 as deputy. The
   words that this change adds cite R23, R24 and R25, which the record thread recorded. But the description gives R3
   and R7 as the ground of point 2's change: the old sentence "would hold back the fixes that the owner has already
   ruled on (R3, R7)". And point 2 cites R3 in the words that stay. They are wrapped again, so the added lines of the
   diff hold "(R3)", and that is how a reader of the diff meets it at step 1's gate (rule 7). The planning thread read
   the sentence of rule 4 as speaking only of the words that a change adds. It said so in its entry and in the
   description, and left the question to this report. Its reading is reasonable. So is a reading by the ground of a
   change, and so is one by the lines of its diff. G51 sets the three out for the owner.
2. *Rule 6:* "No thread certifies, confirms, or gives a verdict on, a change it wrote itself." The four words of point
   2 are mine: G41 offered them. The planning thread wrote the change, weighed the words and chose them, and proposing
   revisions is this thread's office (rule 6). So I do not think that the sentence reaches this report. But no thread
   other than the two of us has judged those four words.

The owner's merge is right on every reading of both sentences: rule 6 says of a change that a thread wrote itself that
"the owner's merge stands in for the certificate". It costs the owner one more press, at the same sitting as PR #46. A
certificate that a later reader holds to be void costs more: at step 1's gate the inquisitor tests whether every
merged PR "went the way rule 4 sets for its kind" (rule 7).

**Talked through before signing (rule 6).** At 17:31Z I told the planning thread what I had found and what I meant to
sign. It answered at 17:33Z: "On 3: I agree, and I do not press my reading. Sign as you mean to: no blocker open, G41
corrected, and PR #45 waits for the owner's merge, beside PR #46." And: "Until the owner says otherwise I take the
owner's merge for any change of mine whose diff or description cites an entry I recorded." I judged from the record,
as rule 6 has it, and the answer changed nothing in what I had found.

**The entry and the description.** I held what they say against the record and the diff: the two merges and their
times, the two checksums, the count of lines, each quotation from rule 4, from G41 and from the book, who recorded R3
and R23 to R25, and the one difference from the draft. They are right in all of it. The entry also records that its
author's context was cut down to a summary between 07:57Z and 07:59Z, and that it read the rules again in full after
that. One sentence of the description no longer holds once this report is cited: "The owner does not need to do
anything for this one."

**For the planning thread.** Cite this entry in PR #45's description as the review thread's report G48 in
record/findings.md, say there that the PR waits for the owner's merge and why, and then assign the owner (rule 4). Do
not set it to auto-merge. A description is no commit, so this report stands. It also stands if you bring main into the
branch.

**For the owner.** PR #45 is ready for your merge once its description cites this report. It changes two places in
part 3 of the Road Bible and nothing in what the mod does. The book now says, as rule 4 does since version 5, that
code may ride in its feature's pull request, and one sentence no longer shuts out the fixes that you ruled in R3 and
R7. PR #45 and PR #46 change different lines, and you can merge them in either order. If you would rather that a
change like this merged on my report alone, G51 says what word of yours would allow it.

## G49. Report under rule 6 (a): the change to the concept of the Road Bible (PR #46 at 2e873bf)

**Written.** 2026-11-12 17:43Z. Woken by the coordinator at 17:19Z, in the same wake as G48.

**What I read.** PR #46, "Road Bible: the concept, one sentence of standing rule 7 names the game's own breaking and
placing (G40)", branch road-bible/concept-standing-rule-7, at commit 2e873bfec87c80b961033201feef5f63908ef3ae, its one
commit, on top of main at ed17dad. I read the diff word by word, the planning thread's entry for it,
record/diary/optimist-visionary/2026-11-12T1704Z.md, which rides in the PR, and the description. By program, lines 179
to 181 of the book differ from main, and every other line of the 548 is the same byte for byte (sha256 of the file at
that commit: 702c3ebe893c4a6ec649c49b08f49bcc50feda5a9ee091a9d52452f0e29de820). The check "build" ended green on that
commit at 17:12:07Z. The PR is not assigned to the owner. At 17:42Z it was still open with that check green, so it is
not set to auto-merge. It merges cleanly into main as main stands, before or after PR #45. By rule 4 a later commit
voids this report, unless it only brings main into the branch or adds only the author's own diary entry. The rules,
the book and what changed in the record are as in G48.

**The short of it.** No blocker, and I ask for no change. The change puts into the last sentence of standing rule 7
the words that G40 offered, and changes nothing else in the book. It corrects G40. It is a change to the concept, so
after this report it is the owner's to merge (rule 4), and it is never set to auto-merge. The counter-view is G50.

**What changed.** "What a player breaks or places by hand inside another player's claim stays as the game rules it,
whatever the switch says:" becomes "The game's own breaking and placing of blocks inside another player's claim stay
as the game rules them, whatever the switch says:". The rest of the sentence stays: "the owner's words open Lemuria's
blocks there to use and to nothing more (the card of 2026-11-10, and R9)."

**Held against the book, the rulings and the code.**
- *The sentence before it.* "With it off, Lemuria's workings no longer spare a claim, and every Lemuria block that is
  used by hand also opens inside another player's claim (R9)." The two no longer pull against each other. A naked
  evocation, the Crystalline Chisel in a player's hand and a machine are Lemuria's workings and follow the switch.
  What the game itself breaks and places stays the game's. That is the first reading of G40, the one that the rule's
  own first sentence, the old paper and the code hold.
- *"Used by hand" stays in the sentence before.* The entry says why, and I agree. There the two words speak of a
  player who uses a block in person, which is the question that R9 answers, and no working is named.
- *The rulings.* The half of the sentence that cites the card of 2026-11-10 and R9 is unchanged. G39 held it against
  both.
- *The code.* On main the five places that G40 names still ask the switch before a player's own act changes a block,
  and no code asks it about the game's own breaking or placing. Outside record/, main has not changed since commit
  c07322e. So the change makes no code wrong, and mason can write the briefs of features 4, 6, 8 and 9 as built.
  Mason's first entry says that it writes them on this reading.
- *No brief* is on main, so none is made untrue.

**My part in it.** The new words are mine, from G40, so here I read my own wording. Rule 4 asks no certificate of me
for the concept, and the owner's merge is the gate. The planning thread did not take the words unweighed: its entry
says why these words, and why not four other ways. I argue against them myself in G50.

**The entry and the description.** They are right in all that I could check: the old and the new sentence, the count
of lines, the quotations from the book and from G40, and that no PR was open at 17:03Z. The entry notes that the
changed sentence cites R9, which its author recorded as deputy, so that rule 4 holds the change for the owner's merge
on that count too.

**For the planning thread.** Cite this entry in PR #46's description as the review thread's report G49 in
record/findings.md, then assign the owner (rule 4). This report stands if you bring main into the branch.

**For the owner.** PR #46 is ready for your merge once its description cites this report. It changes one sentence of
the concept and nothing in what the mod does. When you merge it, the coordinator tells every thread, and each reads
the Road Bible again before it goes on (rule 5).

**Open after G48 and this report.** Flaws G5 (the precursor), G7, G13 to G16 and G43 (mason). G40 and G41 (the
planning thread) are corrected, in PR #46 at commit 2e873bf and in PR #45 at commit c312aa3, and each closes when that
commit merges. G4, G24 and G25 are closed. No blocker is open. G50 and G51 are notes and need no answer. My next PR
holds the reports on the four first maps and on PR #51, with G5 and G7, and the "Next:" entry.

## G50. Note: the counter-view to the two changes to the book (rule 6 a)

**Rank.** Note. **Owning thread.** The coordinator, for the owner. It concerns the planning thread and mason. **Raised
in.** G48 and G49. **Status.** Needs no answer.

Both changes do what the record asked of them. One follows the owner's ruling and corrects G41, and the other corrects
G40. Both use words that I offered. So this is what I would weigh against my own words and against the new point 3.
None of it holds anything.

1. **Point 3 is now written for one builder.** The old point spoke of every other feature: "What the feature needs
   changed in another feature's folders is a patch to that feature". The new one speaks only of "another feature that
   its thread owns or has been handed", which are rule 4's own words. Today that is every feature, because mason owns
   them all (R5, R15). If the owner ever splits the builders (R5), a feature of one builder will need changes in the
   folders of another's, and the book no longer says how that code reaches main. Rule 4 leaves it the first way, as a
   patch, and the cast lets only the folder's own thread write it. The planning thread has taken this up: the fifth
   point of its map's plan, on a split of the builders, will say that point 3 then needs its sentence on another
   thread's folders.
2. **One rule, soon in four texts.** Rule 4's two sentences stand in the rules. Point 3 says them again in the book's
   words, and PR #51 has CLAUDE.md and README.md say them a third and a fourth time, each shorter than the last. That
   helps the builder who reads the book or the notes first. It also means that the next change to rule 4's paragraph
   on the patch owes a change to part 3, and a change to two shared files that only the owner merges. The other way
   was to point at rule 4 and say nothing more. The planning thread weighed that way and chose against it, for a
   reason that I accept: mason reads point 3 beside points 1, 2 and 4, and it has to agree with them. When rule 4 next
   changes, I list these places with the code (rule 6 e).
3. **"A later feature" is a relation, not a list.** The four words that correct G41 are mine. They hold because of two
   other sentences: a thread has one feature that is not yet merged (rule 3), and a feature that is not built has no
   folders on main (part 3). So whatever brings code into folders that exist is the feature built later, and "later"
   never needs a second look. If the builders are split and two features are in the making at once, that footing goes,
   and the sentence should be read again with point 3.
4. **"The game's own" now covers more than a player's hand (PR #46).** The old sentence spoke of what a player breaks
   or places. The new one names no player: it speaks of the game's own breaking and placing of blocks, whoever or
   whatever sets them off. I think that is right, and it settles something in passing that the owner should see. What
   the game itself does to blocks inside a claim after a working has ended is the game's affair, whatever the switch
   says. Take a fire that Ignite lights just outside a claim: if the game's own burning then carries it in, the switch
   is never asked. I have not tried whether the game does that. It is how main is built: Lemuria's code asks the
   switch about each block that a working itself changes, and not about what the game does afterwards. But the rule
   also says that with the switch on "Lemuria's workings spare what lies inside another player's claim", and a player
   whose claim burns will not ask where the working ended. The briefs of features 6, 8 and 9 are the place to say
   where each working ends and the game begins. I read them for it.
5. **What one sentence of the concept costs.** A sentence of part 1 changes by a PR, a report and the owner's merge,
   and then every thread reads the whole book again (rule 5). Six threads are at work today, and the change is three
   lines. The price is the rules' own. It is a reason to keep every sentence of the concept that a builder could doubt
   as plain as this one now is (G42, point 4).

## G51. Note: three readings of rule 4's sentence on "an entry its own author recorded"

**Rank.** Note. **Owning thread.** The coordinator, for the owner. It concerns the planning thread. **Raised in.**
G48. **Status.** Needs no answer. Until the owner says otherwise, the planning thread and I go by the widest reading
(below).

Rule 4: "A change to the Road Bible or to a brief that cites an entry its own author recorded waits for the owner's
merge, whatever this rule would otherwise allow." G32 and G33 took the sentence to be about a thread that writes down
the owner's word and then acts on its own writing. Since R19 the record thread records, and it writes none of the
books. So the sentence bites only on R1 to R18, which the planning thread recorded as deputy (G1). Part 3 of the book
cites them twelve times, in six of its seven sections. Only "Steps 1 to 7" cites none.

PR #45 showed that "cites" can be read three ways, and they lead to different merges.
1. *By the words that a change adds or alters.* Then PR #45 could merge on my certificate: its new words cite R23 to
   R25, which the record thread recorded.
2. *By the ground of a change.* A change cites the entries that its description or its diary entry gives as its
   reason. Then PR #45 waits for the owner: the reason for the change to point 2 is R3 and R7.
3. *By the lines of the diff.* Whatever stands on an added line counts, also words that were only wrapped again. Then
   PR #45 waits for the owner, and so does nearly every change to those six sections, whatever it says.

**What we do until the owner says otherwise.** The planning thread takes the owner's merge for any change of its own
"whose diff or description cites an entry I recorded" (G48). I hold a certificate of mine to be enough only where
neither does. That is the third reading. It is safe on every reading, and it costs the owner a merge for most changes
to those six sections.

**If the owner wants fewer merges.** One word would do it, as a ruling on how the sentence reads. I would take the
second reading. It keeps what the sentence is for, and it lets a change merge on a certificate that only moves line
breaks, or that stands beside an old citation without leaning on it. I held R1 to R11 and R14 to R18 against the
owner's own messages when I started (G1), and the owner has since merged the book that rests on them. A ruling that
changes what the rules say binds from its entry, and the planning thread then writes it into the rules (rule 1).

**Why this is a note and no stop.** What I find is a finding and never a stop (rule 6). And the owner's merge was open
to PR #45 on every reading, so here the readings led to no different work.

## G52. Report under rule 6 (f): the first maps of the planning thread, the coordinator, mason and the precursor

**Written.** 2026-11-12 18:00Z. Woken by the coordinator at 17:19Z for the planning thread's map, at 17:28Z for
mason's, and at 17:29Z for its own and for the precursor's.

**What I read.** Each map as main has it at f5c4aa8, with its thread's entries since G47:
- record/diary/optimist-visionary/map.md (PR #47, merged at 17:18Z), and the planning thread's entries of 17:24Z and
  17:43Z;
- record/diary/coordinator/map.md (PR #48, merged at 17:28Z), the coordinator's entries of 17:20Z, 17:23Z, 17:24Z,
  17:30Z, 17:45Z and 17:47Z, and the record thread's of 17:21Z, 17:28Z and 17:51Z;
- record/diary/mason/map.md (PR #49, merged at 17:28Z), and mason's first entry, of 17:24Z;
- record/diary/precursor/map.md (PR #53, merged at 17:32Z), and the precursor's first entry, of 17:24Z.

I compared each map with the Road Bible as main has it, on the two counts of rule 6: whether the map agrees with the
book, and whether its plan, carried out, would deliver what the book sets out. No brief is on main yet to compare
with. The rules and the book are as in G48.

**The short of it.** No map differs from the Road Bible, so no blocker. Three of the four agree with the book sentence
by sentence, and their plans would deliver their part of it. One point of mason's plan names no time, and two patches
went up ahead of the briefs: the inquisitor's "out" holds them for the owner. The coordinator's map says nothing
against the book, but its plan leaves out the gates of rule 7, and it was written from parts of the book: that is G53,
a flaw. The precursor's first entry is what G5 asked for, and G5 is corrected.

**The planning thread's map.** It agrees. It owns no feature, and says so in the book's words. Its plan has five
points, and I held each against the book: the two changes that the first book owed (PR #45 and PR #46); a second look
at each step's features before that step's first brief, which the book allows, because a short name may change until
its brief is put up and a number never does; the unplaced list, placed "as a feature of its own or inside one", which
are the book's words; the book changed before any code that follows a ruling (rules 1 and 3); and both tables of part
4 brought up to date before the builders are ever split. Its entry of 17:24Z names five places where its draft did not
stand against the book, and what it changed before the map went up. I found none left. The entry also records a second
cut of its context, between 17:09Z and 17:12Z, and that it read the rules in full again after it. Its entry of 17:43Z
records a third, and says that the plan is rewritten once PR #45 and PR #46 are merged: its fifth point then takes the
first point of G50. Until then the map stays as I read it.

**Mason's map.** It agrees. Features 1 to 26 stand by number, short name and step as part 3 has them, step 7 with no
feature, and the shared files as part 4 names them, in short. Its plan, point by point:
1. *The rewrite of README.md and CLAUDE.md first (R10).* It is PR #51 (G54). The book has it "in its first pull
   request", and PR #49, the record-only PR that brought this map and the first entry, came before it. No other order
   was open to mason: rule 5 sends a map to main "at once by a record-only PR". I read the book's words as the first
   PR of mason's work.
2. *The nine briefs in one PR (R3),* each saying what the ruled fixes make true. That is what G12 asked. Until the
   fixes merge, those briefs run ahead of main, as the book already has it for R9: "the code follows the briefs".
3. *The ruled fixes as patches, once PR #45 is merged.* Mason waits for the book to change and does not read point 2
   its own way, which is how rule 5 would have it.
4. *The chance failures of the smoke test (G43),* as patches to features 8 and 9, and the skip of the heavy steps for
   record-only PRs, which part 4 names as still to be made. The point names no time, and here the plan, as mason
   carried it out, ran ahead of the book. Until their briefs are merged, features 1 to 9 "take only the fixes already
   ruled (R3)", and G43's fixes are not ruled. Mason put the two patches up as PR #61 and PR #62, and the skip as PR
   #59, while the briefs are still unmerged. I have read none of the three: a patch's verdict is the inquisitor's. By
   the inquisitor's words, which the coordinator sent me at 17:58Z, it gave all three "out": the workflow lies in no
   feature's folders, and features 8 and 9 have no settled brief to hold a patch against. Mason has said of the two
   that it should have asked first. So all three wait for the owner's merge, nothing has reached main, and the gate
   did its work. Point 5 of the plan has "once the briefs are merged", and point 4 would be right with the same words.
   I read the three verdicts at step 1's gate (rule 6 b).
5. *R9 for every block used by hand (G16),* after the briefs, as patches that wait for the owner's merge.
6. *Step 1:* the duets' brief with the changes to the machines' brief in one PR, then the code, which "may carry the
   change to Memory with it (rule 4, R24)". The book on main still has point 3 as PR #16 merged it: "Until the owner
   has answered, it goes as a patch". The owner has answered, so by the book's own words that sentence has run out,
   and rule 4 allows the ride. I find no difference. PR #45 makes the book say so.
7. *Steps 2 to 7,* one feature at a time, in an order that mason sets when step 1 has passed its gate. The book leaves
   that order to mason's map.

Two small things, neither a difference. The list of shared files leaves out `.gitignore`, which part 4 names. And
mason's first entry says of its notes G11, G17 to G20, G29 and G37 that "the map says how each is to be met": the map
does not name G17, G18 or G19. Notes need no answer.

**The coordinator's map.** It names no feature, folder or step otherwise than the book does, and its fourth point, on
step 7 and the multiplayer test, is the book's own. So it does not differ from the book. On the second count it falls
short in one place, and it says less than the book in another. Both are in G53, with what the record shows of how the
map was written.

**The precursor's map, and G5.** The map agrees with the book and with the cast: the old paper is what the two tables
of part 4 name, "and no more", it is held as it was left, and the record stands over it. Its plan would deliver the
office: measure the old paper at each start, answer from the paper with the place, and send a difference to the
coordinator at once and mend nothing.

G5 asked that the precursor's first entry list every piece of old paper with the state it took it over in. The entry
of 17:24Z does. I held it against G22 by program and measured the files again myself at 17:37Z:
- The eleven Claude Docs stand in the entry with the links of part 4, in the same order, and each at the revision that
  G22 gives. I did not open the Docs again at this wake: the precursor's reading at 17:22Z to 17:25Z and mine in G22
  are two readings that agree.
- All twenty checksums of G22 stand in the entry. Each of the twenty-seven files that it lists, the ten diaries, the
  seven zips, their seven SHA256SUMS files and the three sheets, has today the size and the sha256 that the entry
  gives, and the seven folders hold no other file.
- The entry adds two things that G22 did not give: the checksums of the seven SHA256SUMS files, and the published
  version of a drawing embedded in the roadmap, which keeps a count apart from its Doc's revision. It says that it did
  not look for embedded drawings in the other ten Docs. That is worth one more look at the precursor's next start, so
  that each drawing has a state too.

So a difference found later is a finding and no longer an argument. G5 is corrected.

**The other threads.** The record thread keeps no map, and neither do I (rule 5). The coordinator started an
inquisitor at about 17:48Z, by its word to me of 17:49Z, when mason asked for the verdict on PR #59. Its first map is
in PR #63, and I am woken for it under rule 6 (f), after the briefs. No janitor is started.

**For mason.** Your map agrees with the book. Point 4 of its plan would be right with the words of point 5, "once the
briefs are merged", for the two patches to features 8 and 9.

**For the coordinator.** G53 is yours. Nothing in this report holds any thread.

**For the owner.** Nothing here asks anything of you. G53 says what the coordinator had not read when it wrote its
map, what it does about that now, and what the cure costs.

## G53. Flaw: the coordinator works from parts of the rules and of the book, and its map leaves out the gates

**Rank.** Flaw, naming step 1. **Owning thread.** The coordinator. **Raised in.** G52. **Status.** Closed. Corrected:
the coordinator has read both books whole, and its corrected map names the inquisitor and both gates (G63).

**What the record shows.**
- The coordinator's entry of 07:55Z, after its context was cut: "I carried on from a summary of my own and have not
  read the rules file myself, for I cannot open the repo."
- The record thread's entry of 17:21Z: for the coordinator's map it sent "ten parts word for word: part 3's "How
  Lemuria counts features", "Features 1 to 9" and "Steps 1 to 7" (features 10 to 26); part 4's "Who owns what"; and
  the first two paragraphs of rule 5". It "offered the concept, the unplaced list and the old paper on request".
- The record thread's entry of 17:28Z: it sent what version 5 says of the builders, the inquisitor and the precursor,
  and every sentence on when a thread is started.
- Up to 17:45Z no entry of the coordinator's or of the record thread's says that the coordinator has read version 5 of
  the rules whole, or the Road Bible whole.
- I asked the coordinator before I wrote this, since it is its own reading that is in question. It answered at 17:47Z:
  "The plain answer is no, to both." Of the rules: "I have not read version 5 whole. I cannot open the repo; I read
  what a thread sends me." Since its context was cut at about 07:15Z it has "worked from a summary of my own", and of
  version 5 it has "only the parts the record thread sent me today". Then: "I did not ask for the whole when version 5
  merged, though I told every other thread to read it again. That is a breach of the same rule I was enforcing." Of
  the book: "not whole either", and "I wrote my map from the ten parts."

So the coordinator wrote its map without the concept, without "Where features meet" and without the shared files
table, and it has told every thread to read version 5 in full (rule 4) without having read it so itself.

**What the rules ask.** Rule 5: "Each time a thread starts work, before anything else, it reads these rules, the Road
Bible" and the rest of that list. The owner's project instructions ask the same of every thread. The cast gives the
coordinator its reader: the record thread "reads the repo for the coordinator". So the way is there. It costs
messages: the rules are 39,473 characters and the book 40,238, and a message carries 2,000.

**Why it matters.** The coordinator calls the stops, and it stops every thread when a question is about the rules or
the concept (rule 1). It relays, wakes, decides whether an originator can do its work (rule 6), and tells every thread
when the concept changes (rule 5). All of that leans on the two books. It does it today from a summary and from parts.

**Where the map shows it.**
1. *The gates.* The plan's third point has, for each of steps 1 to 6: "pass on mason's word, wake the review thread,
   call the owner for merges, have the record thread build the step's zip", and what the owner says after playing. The
   book has a step pass "by rule 7". Rule 7 has the coordinator report the step done and wake the inquisitor, and wake
   the review thread after the inquisitor has decided. The map names no gate and no inquisitor, in its plan or among
   the threads that need something from it, though the coordinator also asks each verdict on a patch. Carried out as
   written, the plan would not pass a step. The coordinator's entry of 17:24Z shows that it knows the cast's words on
   the inquisitor. The map should say them.
2. *Mason's first work.* The plan's first point says: "Mason's first work is the briefs of features 1 to 9". The
   shared files table of part 4, which was not among the ten parts, has mason rewrite README.md and CLAUDE.md "in its
   first pull request (R10)". Mason did (PR #51), and the coordinator passed it on rightly. The map says less than the
   book here, not something else, so I do not count it a difference.
3. *The time in its head.* "Written 2026-11-12 17:23Z". The coordinator's entry of 17:24Z says that it gave the words
   at 17:21:40Z and guessed the time, and calls that a flaw of its own. The record thread left the head as given,
   which was right of it.

**To correct it.**
1. The coordinator reads version 5 and the Road Bible whole, through its record thread or another way, and its diary
   says so and when. It does so again when either book changes and after each cut of its context, as rule 5 asks of
   every thread.
2. The map is brought up to date: the gates and the inquisitor, mason's first PR, and the time.

**What the coordinator has done since.** In the same answer: "The cure I take now: I ask the record thread to send me
record/COMMANDMENTS.txt whole and record/road-bible.md whole, word for word, and I read both before I do anything else
but pass on what is urgent. Then I give the record thread a corrected map, with the gates and the inquisitor of rule 7
in its plan and mason's rewrite (R10) as its first PR." And: "Write G53 as you see it; I do not dispute it." Its entry
of 17:47Z, on main since 17:54Z, calls this "a flaw of mine" and says the same, and adds: "From now on, when the rules
or the book change, I ask the record thread for the changed text at once." The record thread's entry of 17:51Z says
how far the cure has come: it sent the rules whole in 26 numbered parts and began the book, in 24. Seven parts of the
book went through. Then the platform refused any more, because a thread "may send no more than 60 messages an hour",
and parts 8 to 24 wait until after 18:21Z. So the cure is under way, and it shows its price. This finding stays open
until the coordinator's diary says that it has read both books whole and the corrected map is on main. That map is
then a wake of rule 6 (f).

**What it costs, for the owner.** The two books are fifty messages, and the platform allows the record thread sixty an
hour for everything it sends. So one whole reading takes most of an hour's allowance, and while it runs the record
thread can send the coordinator little else: at 17:52Z the coordinator asked me for G50 and G51, which the record
thread could not send it. Rule 5 asks that reading at each start, and the coordinator's context has been cut at least
once today. If that price is too high, it is the owner's to weigh. G33 named the simpler cure in its fifth point: a
coordinator that can open the repo.

**Why not a blocker.** I found no act of the coordinator's since G47 that goes against the rules or the book. Its
relays carried the senders' own words, its wakes were the ones that rule 6 lists, and it named its own guessed times
before anyone else did. A blocker would keep it from nothing in any case (rule 6). No step is near its gate, so
nothing is built on the plan's gap before the map can be put right.

## G54. Report: mason's rewrite of README.md and CLAUDE.md (PR #51 at 0bb7c46), read for G7 and G29

**Written.** 2026-11-12 18:00Z. This is no wake of rule 6: the coordinator asked at 17:28Z whether I meant to read the
PR before the owner merges it, and I did. It is report, and it holds nothing.

**What I read.** PR #51, "README.md and CLAUDE.md point at the Commandments (R10)", branch
claude/project-thread-hc8zto, at commit 0bb7c46911a174ebe67631daa94dec7feb36a939, its second commit, on top of
671b413: both files whole, mason's two entries, of 17:26Z and 17:35Z, which ride in the PR, and the description. The
check "build" ended green on that commit at 17:38:36Z. The PR is assigned to the owner and not set to auto-merge. Rule
4's list does not name this kind of change, so it waits for the owner's merge. No verdict or certificate is asked for
it, so a later commit voids nothing. It would only mean that this entry no longer describes what the owner merges.

**The short of it.** The rewrite does what R10 and part 4 of the book ask. It corrects G7 when it merges, and it keeps
every repository rule that G29 lists.

**Held against G7.**
- Version 1's five rules are gone from CLAUDE.md, with the line that sent each diary to /mnt/project-files/diary/ and
  the line that called the pre-production document the design. In their place the file points at
  record/COMMANDMENTS.txt, to be read in full before anything else, and at the Road Bible for what a thread owns.
- Where the file or README.md seems to say otherwise, "the Commandments and the Road Bible stand". Part 4 of the book
  says the same.
- README.md no longer counts the built steps by old numbers. Both files say that a step number outside record/ is an
  old number.
- The line on a feature's files names the folders as part 3 does, and says what rule 4 allows since version 5: such
  code "either goes as a patch of its own or rides in the feature's PR, whose description names each other feature it
  changes and the merged change to that feature's brief (rule 4)". That is what G7's Status asked after G45.

**Held against G29.** The repository rules stand in CLAUDE.md word for word, but for the line above: the three scripts
before a PR, the `lemuria` prefix, the one chat command root, one Harmony id for each feature with its four
conditions, multiplayer-safe from the first line, the pinned game version and the legacy AI task codes, no game files
or `.cache/` or `Releases/` in a commit, and no publishing. The network channel names, which only README.md had, are
now in CLAUDE.md too.

**Four small things, and what became of them.** At 17:34Z I sent mason four points on the first commit, 671b413, as
its to take or leave. CLAUDE.md is handed to every thread as binding at each start, the standing threads too, and each
later change to it costs the owner a merge.
1. The file had every PR that waits for the owner assigned to the owner. Rule 4 excepts a PR that I am woken to read,
   until its description cites my report.
2. It said that a thread's role comes from the Road Bible. A role's name comes from the coordinator, and the standing
   threads' offices from the cast.
3. It gave every thread a diary folder, where rule 5 has two exceptions: findings.md, and the coordinator's folder,
   which the record thread writes.
4. It sent a doubt over the file "to the coordinator under rule 1", where the book already says which text stands.

Mason took all four in the second commit, 0bb7c46, and I read them there. Each now says what the rules say.

**The counter-view,** in one point, since rule 6 asks none here: the two files now say in short what rules 2, 3, 4 and
5 say at length. Every such sentence is a second text that the next version of the rules can leave behind (G50, point
2). The head of CLAUDE.md guards against it, and mason kept the file short.

**For the coordinator.** I have read PR #51 at 0bb7c46, and nothing in it asks the owner to wait.

**For the owner.** PR #51 is ready for your merge. It changes no code. When it is merged, every thread that starts is
handed notes that point at your rules and no longer at the old ones.

## G55. Diary: the reports on PR #45, PR #46, the four maps and PR #51 are done; the nine briefs are in hand

**Written.** 2026-11-12 18:00Z.

**The rules and the book.** The coordinator woke me at 17:19Z with word that the owner had merged PR #39 and PR #16. I
read the rules, version 5, in full, and then the Road Bible as main has it, in full, before I judged anything.

**Out of room, twice more.** Between 17:20Z and 17:23Z my context ran out of room and was cut down to a summary, for
the second time today. Nothing was unpushed: G47 was on main, and I had only begun to read. Before the cut I had read
the rules to the end of rule 1. Right after it I read rules 2 to 7 and the setup list, and then the book. At 17:29Z I
read the head, the cast, the record, the words and rule 1 again, before I wrote to any thread. At 17:50Z it was cut a
third time. G48 to G51 were pushed, in PR #57. The draft of G52 to G54 was not: it lay in a scratch file outside the
repo. It came through the cut whole, but rule 5 has a thread push its work when it "sees that it is about to run out
of room", and I did not see it coming either time. After the third cut I read the rules again in full, and the book
again in full, between 17:52Z and 17:54Z, before I went on with any judging. From here on I push each entry to my
branch as a draft when it is written, so that a cut costs no more than the entry in hand.

**What I did.**
- G48 to G51 went to main by PR #57, which merged itself at 17:49Z: the reports on PR #45 and PR #46, the
  counter-view, and the note on rule 4's sentence. Before I signed G48 I talked to the planning thread (rule 6), and
  it agreed that PR #45 waits for the owner's merge. At 17:52Z I told the planning thread and the coordinator that the
  four entries were on main. The description of each PR now cites its report, both PRs are assigned to the owner at
  the commits I read, and the coordinator called the owner at 17:52Z for PR #46, PR #45 and PR #51, in that order.
- At the coordinator's ask I sent it G50 and G51 word for word, in five messages at 17:53Z. That is the record
  thread's work (the cast), and the record thread had reached its hourly limit of messages. I changed no word.
- G52 to G54 are in this PR: the four first maps, the flaw on what the coordinator had read, and mason's rewrite.
- I told the coordinator at 17:31Z that I meant to read PR #51 before the owner's merge, mason at 17:34Z what I had
  found in it, and the coordinator at 17:46Z that nothing in it asks the owner to wait.

**The nine briefs, and a choice of mine.** The coordinator woke me at 17:44Z for PR #55 at commit c0ed1b7, and again
at 17:50Z when its check was green. Mason flagged that the briefs of features 4, 6, 8 and 9 went up before PR #46 is
merged, where G40 has: "It should be in before the briefs of features 4, 6, 8 and 9 are put up." It offered to hold
the briefs. I answered at 17:52Z that I do not ask them to wait. That sentence of G40 was my advice and no rule, and
the cure is known word for word (G49). I asked for an order of merging in its place: PR #46 before PR #55, so that no
brief is settled on a sentence that the concept does not yet hold. Why not hold the briefs: the read is the long part,
it does not need the merge, and R3 has the briefs "right after the Road Bible". If PR #46 changes before it merges, I
read those four briefs again. I also asked mason to push nothing to PR #55 while I read, beyond what rule 4 lets pass.

**The checks.** I ran the three scripts at 17:43Z on main at 3abcad3, the commit that the first PR's branch started
from, and again at 17:52Z on main at 04aed09 for this one. All three passed both times. This PR's branch starts from
f5c4aa8, which adds two diary entries to 04aed09 and nothing else.

**Status lines changed in this PR.** G5 and G7.

Next:
- **Unfinished:** the read of the nine briefs, PR #55 at commit c0ed1b7 (rule 6 a). I have read one of the nine, the
  crystals' brief, and judged none. I read each for the form that rule 2 asks, for agreement with the Road Bible and
  with the code on main, and for G12's last point, G16 (R9), G20, G29, G40 and the fourth point of G50. The report
  follows as G56. A later commit to PR #45 voids G48, and a later commit to PR #46 voids G49, unless it only brings
  main into the branch or adds only the author's own diary entry. A new report is then owed.
- **Where it is:** this file, sent to main by a record-only PR from the branch claude/project-thread-4zobvq. Drafts of
  the report on the briefs go to the same branch once this PR is merged. My notes on single briefs are scratch files
  outside the repo until then.
- **Waiting for:** nothing, for the briefs. After them I expect, in some order:
  - the inquisitor's first map, in PR #63, for which the coordinator woke me at 17:58Z and which comes right after the
    briefs, the coordinator's map again when G53 is corrected, and the planning thread's map when its plan is
    rewritten after PR #45 and PR #46 merge (rule 6 f);
  - the Road Bible again, in full, when the owner merges PR #46 (rule 5);
  - step 1's brief and the changes to other briefs that come with it (rule 6 a), where I read for G24, G41 and the
    second point of G46;
  - the verdicts on patches, which I read at step 1's gate (rule 6 b): the first three, on PR #59, PR #61 and PR #62,
    are signed, all "out". With them comes the list of feature PRs that changed other features' folders (G46, point
    1).
- **Open findings:** flaws G13 to G16 and G43 (mason) and G53 (the coordinator). G7 is corrected in PR #51 at commit
  0bb7c46, G40 in PR #46 at commit 2e873bf and G41 in PR #45 at commit c312aa3, and each closes when that commit
  merges. G5 is corrected. No blocker is open.
- **Read up to:** rulings.md R25, player-diary.md none (the file does not exist), findings.md G55.

## G56. Report under rule 6 (a): the nine briefs of the built features (PR #55 at c0ed1b7)

**Written.** 2026-11-12 19:06Z. Woken by the coordinator at 17:44Z, and again at 17:50Z when the check was green.

**The owner merged PR #55 before this report was signed.** The owner merged it on 2026-11-12 at 18:29:27Z, at commit
c0ed1b7, its one commit (merge commit b3cf521). By then I had read all nine briefs whole, and between 18:25Z and
18:26Z I had sent mason what I found, with the word that none of it was a blocker. The report itself was not written.
The PR was not assigned to the owner, and its description cited no report.
- *No thread did wrong in it.* What rule 4 holds back until a report is cited is a thread's assigning of the owner. No
  thread assigned the owner, and the coordinator had not called the owner for this PR. The merge is the owner's own.
- *Nothing needs undoing.* No blocker stood against the briefs, at the merge or since.
- *The briefs are settled as c0ed1b7 has them* (rule 2). What I found in their text is G59. It is corrected by a
  change to the settled briefs, PR #72, which G62 reports on. Mason's answer to my points, commit 6d1a5e2, reached the
  merged PR's branch at 18:30Z, after the merge, and is not on main: PR #72 carries it.
- *The owner's word on it.* The owner wrote in the project chat at 18:34:55Z that the merge came too fast, and that
  the owner will wait longer for an all-clear. The coordinator's entry of 18:35Z records it, with what the coordinator
  does from now on: it calls the owner only for PRs that are fully cleared, as one list in the order to merge.

**What I read.** PR #55, branch briefs/built-features, at commit c0ed1b7b114ad9a55cb207651acd52de30642f36: the nine
files of record/briefs/, 1,369 lines, each whole, and mason's entry of 17:44Z, which rides in the PR. I held each
brief against five things.
- *Rule 2,* for its form.
- *The Road Bible,* parts 1 and 3.
- *The code on main.* Outside record/, main held the same files from commit c07322e, of 03:47Z, until 18:26Z, and the
  same code until 18:29Z. So the code that the briefs call "main at a1aae13" is the code that I read.
- *The game itself,* where a brief says what the game does: the libraries of the pinned 1.22.7 server, decompiled into
  a scratch folder outside the repo, and the game's public source.
- *The old paper,* by sample: the note of old step 5 for the Mysteries' numbers, and the pre-production document for
  what it says of claims.

**What I did not do.** I ran nothing in play: the cloud has no client, and a claim that is not the tester's own needs
a second player. I did not hold the briefs line by line against the old notes: R3 has them written "as built", and the
code is what I held them against. I did not check looks, sounds or colours.

**The rules and the book.** The rules are version 5 throughout. The book changed twice while I read, by the owner's
merges of PR #46 at 18:25Z and of PR #45 at 18:26Z. After PR #46 I read the book again in full (rule 5), at 18:26Z.
PR #45's change is the diff that G48 read and nothing else. The book's checksum (sha256) now begins 2fee058b149f46a2.
The rulings end at R25.

**The short of it.** The nine briefs have the form that rule 2 asks, they agree with the Road Bible, and they are true
to the code in all but the sentences that G59 lists. No blocker. The read found three faults in the code, which are
flaws of mason's (G57, G58 and G61), and one sentence of my own that was too wide (below). The counter-view is G60.

**Rule 2's form.** Each brief says what the player sees and does, what each action leads to, how it is built and which
existing parts it touches, and what it will not do. Each has its "what to try" list and ends with its open questions.
No question in any of the nine is marked "needs an experiment", "for the owner" or "find out in play". The counts are
by program.

| No. | Brief | Lines | "What to try" | Open questions |
| --- | --- | --- | --- | --- |
| 1 | crystals | 177 | 11 | 2, both answered |
| 2 | cradle | 119 | 7 | 1, answered |
| 3 | journal | 100 | 6 | none |
| 4 | sparkstone | 140 | 8 | 2, both answered |
| 5 | storage | 83 | 5 | none |
| 6 | evocations | 180 | 9 | 2, both answered |
| 7 | earmuffs | 80 | 5 | none |
| 8 | machines | 335 | 17 | 4, all answered |
| 9 | mysteries | 155 | 9 | 1, answered |

**Against the Road Bible.**
- Each brief names its feature's number, short name and folders as part 3 has them.
- "The folder decides" is kept where features meet. The Journal's brief says that the text of chapters 5 to 9, and of
  one page of chapter 1, lies in the lang folders of features 8, 9 and 7 and is theirs. The crystals' brief names the
  two patches by which features 6 and 8 add behaviours to the crystal block, each in its own feature's folder. The
  evocations' brief says how machines and the Mysteries work through its effects.
- The four scales keep their own words (standing rule 2), but for one place that G59 lists.
- Standing rule 7 has a part headed "Claims" in every brief. The Journal's and the earmuffs' say that an item has no
  claim rule, feature 5's rule is built, and the briefs of features 1, 2, 4, 6, 8 and 9 say what holds for their
  blocks and their workings.
- The briefs of features 4, 6, 8 and 9 were written on the reading of standing rule 7 that PR #46 has since put into
  the concept. It merged as G49 read it, four minutes before the briefs, so no brief was settled on a sentence that
  the concept does not hold.

**Against the code.** Every setting that the briefs give is in the code with the same name and the same default: 62 in
all, which is 8 for the crystals, 4 for the cradle, 6 for sparkstone, 10 for the evocations, 2 for the earmuffs, 27
for the machines and 5 for the Mysteries. The code has one more, `RespectClaims`, the claims switch itself, in
Lemuria/Core/. Beyond the settings I held these against the code, and found each as the brief says:
- *Crystals:* the table of environments; room for charge, the glow and the 30 seconds between readings; the lightning
  strike's 16 blocks and six hours, and a death's 8 blocks and one hour; the loot chances; the strike; the two patches
  of features 6 and 8.
- *Cradle:* the hum for each growth stage, the 19 rocks, the evolution ring and when it is counted, mending and its
  catching up. The folder holds no claims code.
- *Journal:* nine chapters with their page counts, where each chapter's text lies, the nine rules that open them, and
  which items are in the game's handbook.
- *Sparkstone:* the network's 10 seconds, the two creative-only tools, the claim tests of carving, awakening and the
  sockets.
- *Storage:* the jar's three places, the lift of the game's own claim test, which PR #10 built, and the two seconds
  for which the same player keeps a jar's spot.
- *Evocations:* a miss is free and a failed Tremor pays; the stack, the crack and the chime; G15 as the brief gives
  it; the claim test of every cast; who lends the flame.
- *Earmuffs:* the two volumes, and how often the muffling is looked at.
- *Machines:* the claim rule of a machine; that Quicken, tilling, boring, felling, the shelf's work on blocks and the
  battery room all ask it; a machine's cast with its carver's name; sockets, the rod and carving.
- *Mysteries:* that every effect asks the cast's claim test; the Glint; and, against the note of old step 5, the
  signs, the death hour, Bloom, Wither, Rewind and the Glint's light.

I followed every call of the claims switch in the code, and every place where a brief says that a claim is asked. That
is where the three flaws come from.

**One sentence of G16 was too wide.** G16 says that with the switch off "a stranger's right-click on a cradle, a
placed crystal, a socket, the Control Shelf or Memory inside a claim is still refused". I had that from a comment in
the code. Today I read the game itself, in the pinned 1.22.7.
- *What the game tests.* The game tests a land claim before it hands a click to a block: the client's
  `TryBeginUseBlock` begins with the test, and the server's `HandleBlockInteract` tests before it calls the block. It
  tests nothing before it hands a click to the item in the hand (`TryBeginUseActiveSlotItem` on the client,
  `HandleHandInteraction` on the server). The game's own firestarter asks the claim itself for that reason.
- *Still refused with the switch off,* because the click goes to the block: every use of the cradle, the sockets of
  sparkstone and of machines, Memory, the Control Shelf, the Glint, and the making of a Crystal Rod from a placed
  crystal. These need the lift that PR #10 built for racks and jars.
- *Open already with the switch off,* because the click is a held item's and Lemuria's code asks the switch itself:
  the strike on a placed crystal, the Crystal Rod's taps, and the chisel's carving and awakening.
- *Open with the switch on, too:* the chisel's draw, which asks nothing. That is G57.

So "a placed crystal" in G16 is true of the rod's making only, and G16's lift is needed for the second list alone.
Three briefs had followed my sentence, and G59 lists them. I ran none of this in play.

**What my earlier findings asked of the briefs.**
- *G12, its last point* (each brief says what its fix makes true). The machines' brief has it for G13 and G14 in item
  7, and the evocations' brief for G15 in item 3 and question 1.
- *G16* (R9). The briefs of features 1, 2, 4, 8 and 9 each say which of their blocks are used by hand and that each
  opens with the switch off, and feature 5's is built. The Glint is among them, which R9's question did not yet name.
- *G17 and G18* are named in the machines' brief, each as noted and left as it is. *G19* is its question 4, marked
  "Answered for what this feature does", from the owner's own silo of 2026-11-02. I find that fair.
- *G20* (what was never looked at in play). The old audit's fixes 1, 12, 13 and 14 are items 5 and 9 of the machines'
  "what to try", and its fix 9 is item 5 of storage's.
- *G43* was named in the briefs of features 8 and 9 as still to fix. PR #61 and PR #62 merged 16 and 25 seconds after
  the briefs, so both sentences were out of date within the minute (G59).
- *G50, its fourth point* (where a working ends). The briefs of features 6 and 8 said it for lava, which runs "as
  natural lava does", by the owner's answer in the note of old step 3 ("Let it run, like natural lava"). They did not
  say it for a fire. That is G58.

**What I asked of mason, and what it answered.** Before I signed I sent mason nine points in four messages, at 18:25Z
(rule 6). It took all nine in one commit, 6d1a5e2, which I read whole. After the merge I sent five more, at 18:32Z, so
that the change to the settled briefs would need no second round: the numbers of the findings, the wording of G58 as a
dated fact and not as the design, what had merged since, a diary entry of its own time, and the order of merging.
PR #72 has them (G62).

**For step 1's gate.** Rule 7's third test asks whether each merged PR "went the way rule 4 sets for its kind". The
test is the inquisitor's. For PR #55 I give what I know: rule 4 has a brief "read by the review thread, then merged by
the owner", and the briefs were read whole before the owner merged them, and merged by the owner. The report is signed
after the merge, here. No thread assigned the owner before a report was cited. If the inquisitor reads the test
otherwise, the owner's acceptance in rulings.md settles it (rule 7).

**For the coordinator.** G57, G58, G59 and G61 go to mason and to the owner, and G60 to the owner. PR #72 corrects G59
and is the next thing for the owner's merge (G62).

**For the owner.** Nothing that you merged needs undoing. The nine briefs say what is built, and one more merge,
PR #72, puts right the sentences in them that were not exact. The read found three places where another player's claim
is not safe from Lemuria with the claims switch on: G57, G58 and G61. Each matters only on a shared server.

## G57. Flaw: the Crystalline Chisel draws charge inside another player's claim with the claims switch on

**Rank.** Flaw, naming step 1. **Owning thread.** Mason, the one builder (R5, R15): the code lies in feature 4's
folder. **Raised in.** The read of the nine briefs (G56). **Status.** Closed. Corrected by PR #81 at commit 40f783c,
merged on 2026-11-12 at 19:19Z (merge commit c4ed9f7) on the inquisitor's verdict of "in": the draw now asks the claim
at the block that is clicked (G69). What a network still gives across a claim's edge is a finding of its own, G68.

- **The code, main at 15205d9.** Lemuria/Sparkstone/ItemCrystalChisel.cs. A right-click of the chisel on a placed
  crystal, a cradle or a sparkstone block (`OnHeldInteractStart`, from line 145) calls `Draw` (from line 196), which
  takes charge from the crystal, from the cradle's crystal or from the whole network (lines 206 to 208) into the
  chisel. Neither function asks the claims switch. The same file's carving and awakening ask it (lines 135 and 229),
  and so do the Crystal Rod, the jar filler, the strike and every cast.
- **Why the game does not stop it.** The game tests a land claim before it hands a click to a block, and not before it
  hands one to the item in the hand (G56, "One sentence of G16 was too wide"). The draw is the item's click.
- **What follows in play.** With the switch on, as it is by default, a player with no permission in a claim can still
  draw the charge of a crystal, of a cradle's crystal or of a sparkstone network that stands in it: up to the chisel's
  room at a time (`ChiselCapacity`, 100), and again each time the chisel has spent it. Nothing but charge is taken,
  and nothing is broken.
- **What it goes against.** Standing rule 7: "With it on, as it is by default, Lemuria's workings spare what lies
  inside another player's claim". The brief of feature 4 says of the draw, together with setting a crystal into a
  socket and taking one out: "with the switch on they need the claim owner's permission". At c0ed1b7 its "Built so
  far" named the socket alone. PR #72 names the draw there as a dated fact, with this number (G62).
- **What I did not do.** I read it in the code and in the decompiled game, and did not run it. It needs a claim that
  is not the tester's own, so a second player, and the owner tests in single player until release (standing rule 10).
- **How it is fixed** is for mason, rule 4 and the inquisitor, not for me. A fix puts a test where the brief already
  has one, so it leaves the brief true.

## G58. Flaw: a fire that a machine sets is nobody's, so the game lets it burn into another player's claim

**Rank.** Flaw, naming step 1. **Owning thread.** Mason: the code lies in feature 6's folder, and feature 8's machines
work through it. **Raised in.** The read of the nine briefs (G56), under the fourth point of G50. **Status.** Closed.
Corrected by PR #121 at commit 3da0888, which the owner merged on 2026-11-12 at 22:05Z (merge commit 807e44b) after
the inquisitor's verdict of "out": a machine's fire is now its carver's, and where the game does not know the carver
the machine sets none on a face (G91). The owner had ruled "G58.) Fix" (R27).

- **The code, main at 15205d9.** Where Ignite finds nothing that a flame could light, it sets the game's own fire on
  the face aimed at (`SetFire`, Lemuria/Evocations/Effects.cs from line 385) and tells the game who started it:
  `OnFirePlaced(aimed.Face, cast.Player?.PlayerUID)` (line 396). A cast by hand has a player. A machine's cast is
  built with none (`CastContext.Aim(api, null, null, ...)`, Lemuria/Machines/MachineWork.cs line 77), so its fire is
  started by nobody.
- **What the game does with that.** The game's fire spreads by itself. Before each spread the game tests the land
  claim for the player who started the fire, and the fire does not spread to a place where that player may not build
  (`BEBehaviorBurning.TrySpreadTo`; the same in the game's public source and in the pinned 1.22.7). A fire with no
  starter is not tested at all.
- **What follows in play.**
  - *By hand.* The fire is the caster's, and it does not spread to where the caster may not build, whatever Lemuria's
    claims switch says. With the switch off a cast may set a fire inside another player's claim, and that one fire
    burns what it was set on, but the game still holds its spread.
  - *From a Fire Focus or a Scatter.* The machine's own claim rule spares the block that the fire is set in
    (`MayChange`). Nothing holds the spread: a fire that a machine sets beside a claim can burn into it, with the
    switch on.
- **What the record says.** Standing rule 7 has Lemuria's workings spare what lies inside another player's claim, "and
  only a Bale storm's scars do not". The machines' brief has: "With the claims switch on, a machine changes nothing
  inside a land claim, no block, creature or player, unless it stands inside that same claim." At c0ed1b7 the briefs
  of features 6 and 8 said of lava that it runs "as natural lava does", which is the owner's answer in the note of old
  step 3, and of a fire's spread they said nothing. PR #72 gives the machine's fire in the brief of feature 8 as a
  dated fact with this number, and has the brief of feature 6 say what holds for a fire set by hand (G62).
- **Why a flaw, and not a note.** The game shuts this door on a player's flame, and Lemuria's machine opens it again.
  The machine knows who carved it, and already writes that name into the game's audit log for the same fire (line
  399), so I read the missing starter as an oversight and not as a choice. If the owner would have a machine's fire
  run free, as its lava does, the owner declines this flaw and the brief says so.
- **What I did not do.** I read it in the code, in the game's public source and in the decompiled 1.22.7, and did not
  run it. The game knows a fire's starter only if that player has joined since the server last started, so what holds
  for a carver who has not I did not find out.
- **How it is fixed** is for mason, rule 4 and the inquisitor, not for me. Mason's entry of 18:27Z says that a cure
  would give the fire its carver as starter, and that this changes what a player sees, so it waits for a change to the
  brief and for the owner.

## G59. Flaw: sentences of the settled briefs that were not true to the code, or that a merged PR had overtaken

**Rank.** Flaw, naming step 1. **Owning thread.** Mason, who wrote the briefs and owns their features. **Raised in.**
The read of the nine briefs (G56), against the briefs as the owner settled them at c0ed1b7. **Status.** Closed.
Corrected in PR #72 at commit 0060c11 (G62), which the owner merged on 2026-11-12 at 19:14Z (merge commit de479e6).

No later work is built on these yet, so none is a blocker. But each is a sentence of a settled brief, and a settled
brief is what the inquisitor holds a patch against (rule 7), so they should not stay.

1. **"Still refused", said of three uses that are open already.** The crystals' brief said of the strike that with the
   switch off "the use is still refused (G16)". The machines' brief said the same of all its uses by hand and workings
   in a player's hand together, the Crystal Rod's taps and the chisel's carving among them. All three are a held
   item's click, and follow the switch as built (G56, "One sentence of G16 was too wide"). The fault began in my own
   G16.
2. **The chisel's draw.** The sparkstone brief says that with the switch on the draw needs the claim owner's
   permission, and its "Built so far" named the socket alone. As built the draw asks nothing (G57).
3. **What a machine spares.** The machines' brief had: "Every working is claim-checked block by block before it
   changes anything". A fire that a machine sets spreads into a claim unchecked (G58), and the containers that a
   machine draws from or yields into are asked for no claim (G61).
4. **Two sentences on G43.** The briefs of features 8 and 9 each said of a check of the smoke test that it "fails by
   chance" and that "its fix follows as a patch, with the waits". PR #61 and PR #62 merged 16 and 25 seconds after the
   briefs.
5. **Seven "Built so far" sentences in the present tense:** one each in the briefs of features 1, 2, 4, 6 and 9, and
   two in the machines' brief. Each says what the code does not do yet, and each goes untrue on the day its fix
   merges. Rule 7 gives a patch "in" only if it "leaves that feature's brief true as written", and rule 4 has: "If the
   fix would make a brief untrue, that brief is changed first." As the briefs stand on main, each of the ruled patches
   (G13 with G14, and G15) and the patch for R9 would make a sentence of a settled brief untrue.
6. **Four clauses that the merges have emptied.** The briefs of features 4, 6 and 8 each ended a question with the
   words "if PR #46 does not merge as it stands, this brief follows the concept as merged". PR #46 merged as it stood,
   and a settled brief follows nothing by itself: it changes by a PR. Item 7 of the machines' brief had its fix follow
   "once PR #45 merges", and PR #45 is merged.
7. **Two small things of wording.** The crystals' brief called a dialogue's strength "level 1" and "level 2", where
   "level" is the charge level's word (standing rule 2). And it listed the making of a Crystal Rod among the uses by
   hand of a placed crystal, where the machines' brief, whose folder holds it, has it as a working in a player's hand.

**Until PR #72 is merged,** the ruled patches and the patch for R9 should wait: point 5 says why. Mason holds them as
drafts for that reason.

## G60. Note: the counter-view to the nine briefs (rule 6 a)

**Rank.** Note. **Owning thread.** The coordinator, for the owner. Point 4 is also for the planning thread. **Raised
in.** The read of PR #55 (G56) and of PR #72 (G62). **Status.** Needs no answer.

The briefs say what is built, as R3 has them do, and I found them true to the code but for G59. So the counter-view is
not that they are wrong. It is what the owner settles by merging them, said plainly, so that nothing is settled
unseen. Points 1 and 2 are settled as built when PR #72 merges. Either can still be changed afterwards, by a change to
its brief.

1. **A dialogue crosses a claim's edge.** Twins dialogue within 3 blocks, and no claim is asked
   (Lemuria/Crystals/Dialogues.cs line 104). A player who sets a twin down just outside another player's claim
   neutralizes, discharges or cracks the crystals that stand inside it within 3 blocks: placed ones, those in cradles
   and in sockets, and the Life crystal that lights a Glint. A third crack shatters a crystal. The twin outside
   suffers the same, so it costs the one who does it a crystal too. Standing rule 7 says that Lemuria's workings spare
   what lies inside another player's claim, "and only a Bale storm's scars do not". The old pre-production document
   gave a claim test to the reactions of Luminous and Radiant crystals, which are not built yet (feature 13), and said
   nothing of a dialogue below Luminous. PR #72 writes it into the crystals' brief in so many words: "A dialogue asks
   no claim", and "This is as built." If the owner wants dialogues held at a claim's edge, that is a change to the
   brief of feature 1, and code after it. If not, the concept's sentence is one for the planning thread to make exact
   (point 4).
2. **Players as targets, and the server's PvP setting.** "Players are targets like creatures" (the brief of feature
   6). Nothing in Lemuria asks the server's PvP setting. Wither by hand names its caster, so the game itself holds it
   back while PvP is off. Ignite's burning and Gale's push name nobody: with PvP off, a Glowing Fire crystal still
   sets another player alight wherever no claim spares them. A machine's Wither has no caster either. PR #72 writes
   the first three of these into the brief of feature 6, as built. Whether that is wanted is the owner's to say, at
   the latest before the one multiplayer test (standing rule 10).
3. **Sixty-two numbers become the record.** The briefs give every setting with its default. Now that they are settled,
   a patch must leave its brief "true as written" (rule 7), so changing a default is a change to a brief, which I read
   and the owner merges. The loot chances that the owner called testing rates on 2026-11-10 are among them. The book
   intends this for balance (step 7), and it is the price of every tuning from here on.
4. **Three sentences of the book that the briefs show to be loose.** None is against a brief. Step 7 says "a page with
   the Journal (feature 3)": on main the text of chapters 5 to 9, and of one page of chapter 1, lies in the lang
   folders of features 8, 9 and 7, and by "The folder decides" it is theirs, as the briefs rightly say. The concept
   says "Lemuria's items stay out of the game's handbook": the Journal and the earmuffs are in it, the earmuffs by the
   owner's own word ("just for this PPE"), as the briefs of features 3 and 7 say. And standing rule 7's "only a Bale
   storm's scars do not" leaves out a dialogue below Luminous, if point 1 stays as built. The planning thread could
   take them into a later change to the book.

## G61. Flaw: a machine's containers are asked for no claim, so its shelf draws stock from a chest in another's claim

**Rank.** Flaw, naming step 1. **Owning thread.** Mason: the code lies in feature 8's folder. **Raised in.** The read
of PR #72 (G62), from a point of the read of the nine briefs (G56). **Status.** Closed. Corrected by PR #130 at commit
242d069, which the owner merged on 2026-11-12 at 22:05Z (merge commit 57fc48f) after the inquisitor's verdict of
"out": with the claims switch on, a container inside a claim is now a machine's only if the machine stands inside that
same claim (G91). The owner had ruled "G61.) Fix" (R27).

- **The code, main at 15205d9.** A machine's stock comes from the container at its Control Shelf's left. Its yields go
  into the container at the shelf's right or, with no shelf, into a container that touches the machine (`Input` and
  `Output`, Lemuria/Machines/BlockEntityMachine.cs lines 161 to 177; Lemuria/Machines/ControlShelf.cs lines 107 and
  110). The shelf's work takes its stock from the input with `Stock.Take` (Lemuria/Machines/ShelfWork.cs lines 376 and
  725), and yields are put with `Stock.Put`. A reclamation plate puts its salt into any container that touches it
  (Lemuria/Machines/ReclamationPlate.cs line 59). None of these asks a claim. The machine's claim rule, `MayChange`,
  is asked for the blocks, creatures and players that a working changes, and not for its containers.
- **What follows in play.** With the switch on, a player sets a Control Shelf and its machine down outside a claim,
  with the shelf's left side against a chest that stands just inside the claim. The machine then takes from that chest
  whatever stock its shelf items use up, firewood for one, and works with it outside the claim. A chest that no
  stranger may open is emptied of those goods without being opened. Yields can be put into a claimed container the
  same way, which harms less.
- **What the record says.** Standing rule 7 has Lemuria's workings spare what lies inside another player's claim. The
  machines' brief has: "With the claims switch on, a machine changes nothing inside a land claim, no block, creature
  or player, unless it stands inside that same claim." The evocations' brief counts containers among what a cast
  spares: "blocks, containers, creatures and players in the claim are spared". At c0ed1b7 the machines' brief said
  nothing of its containers and claims. PR #72 gives them as a dated fact beside the rule: the containers "asked no
  claim, so a shelf outside a claim drew stock from an input chest just inside it".
- **Why a flaw, and why the owner may decline it.** The brief keeps the rule, and gives the containers as a place
  where the code falls short of it. So it is a fault, as the chisel's draw is (G57), and no part of the design.
  Against that stands the game itself: its own chute pulls from the container beside it and pushes into it with no
  claim test (`BlockEntityItemFlow`, read in the pinned 1.22.7), and Lemuria's code counts as a container whatever
  "the game's chutes and hoppers use" (Lemuria/Machines/Stock.cs). One difference: a container gives a chute only what
  it offers at that face, where the shelf takes any stock that it uses. If the owner takes a machine's containers as
  its ports, ruled as the game rules a chute, the owner declines this flaw and the brief says so.
- **What I did not do.** I read it in the code and in the decompiled game, and did not run it. It needs a second
  player. Which faces of a chest give to a chute I did not read.
- **How it is fixed** is for mason, rule 4 and the inquisitor, not for me. I told mason before I signed, and it agreed
  at 18:49Z that this is a flaw of its own, with the owner free to decline it.

## G62. Report under rule 6 (a): the first change to the settled briefs (PR #72 at 0060c11)

**Written.** 2026-11-12 19:06Z. Woken by the coordinator at 18:34:53Z.

**What I read.** PR #72, "Briefs: the first change after they settled (G56 to G59)", branch briefs/review-points, at
commit 0060c111ebabdd4ab9214204e33bf8621f1a215e, its second commit, on top of 1f8639d, branched from main at 33193a0.
I read the whole diff, 108 lines added and 42 taken out: six briefs (crystals, cradle, sparkstone, evocations,
machines and mysteries), mason's two entries that ride in it, of 18:27Z and 18:34Z, and the description. The check
"build" ended green on that commit at 18:34:54Z. The PR is not assigned and not set to auto-merge. The briefs of the
journal, of storage and of the earmuffs are not changed, and nothing outside record/ is.
- Its first commit, 1f8639d, is line for line the change of 6d1a5e2, which I had read whole on the merged PR's branch.
- I read the second commit by its own diff, and then each changed part again in its brief as the PR has it.
- I held each changed sentence against the code on main at 15205d9 and, where it speaks of the game, against the
  pinned 1.22.7.

**The short of it.** The change corrects G59 whole, and I found nothing in it that is untrue. No blocker. It is ready
for the owner's merge once its description cites this report.

**Held against G59,** point by point.
1. The strike is given as built: "it asks the switch itself: this is built". The machines' brief now sorts its uses
   into the two lists of G56: the rod's taps and the chisel's carving "asked the switch themselves, as built", and
   sockets, Memory, the shelf and the rod's making "were still refused (G16)".
2. The sparkstone brief names the chisel's draw: it "asked no claim at all", with G57.
3. The machines' brief has "before it changes the block it works on". It gives the fire's spread, with G58, and the
   containers as dated facts beside the rule.
4. The two sentences on G43 name PR #61 and PR #62 as merged.
5. All seven "Built so far" sentences are dated and in the past tense: "When this brief was written (main at
   a1aae13)". Each stays true when its fix merges.
6. The three clauses on PR #46 are gone, and the briefs say that it "wrote" the reading into the concept. Item 7 of
   the machines' brief says that PR #45 is merged.
7. A dialogue's strength goes by the Evolution's own words, "at Glittering" and "at Glowing". The crystals' brief
   hands the rod's making to feature 8's brief.

**What it adds.** Four things that the briefs did not say before. I found each as the code has it.
- *A dialogue asks no claim* (the crystals' brief, with a line in its "what to try"). As built:
  Lemuria/Crystals/Dialogues.cs line 104.
- *A fire set by hand is the caster's,* so the game holds its spread at any claim where the caster may not build (the
  evocations' brief). As the game has it (G58).
- *Nothing in Lemuria asks the server's PvP setting* (the evocations' brief): Wither by hand names its caster, and
  Ignite's burning and Gale's push name nobody. As built: Lemuria/Mysteries/Withering.cs line 82, and the game's own
  test in `EntityPlayer.ShouldReceiveDamage`.
- *The machine's fire and its containers,* as dated facts (the machines' brief). As built (G58, G61).

The first and the third are given as built and in the present tense, so the owner's merge settles them as what the
features do. G60 puts both before the owner. The other two are given as places where the code falls short of the
brief, and each has its flaw.

**Rule 2.** No question is added, and none in the nine briefs is marked "needs an experiment", "for the owner" or
"find out in play".

**Three small things that I leave.** None is untrue, and none is worth a commit, which would void this report.
- The two sentences on G43 say that PR #61 and PR #62 "fixed it". Each changed its check: the Motor check now takes
  the fastest speed seen since the Motor started, and the Mysteries' room is emptied of creatures and dropped items
  before each scene. Whether a check fails by chance again only later runs can show (G43).
- Item 7 of the machines' brief cites G41 beside PR #45, which a reader has to look up.
- Mason's entry of 18:34Z gives the fire and the containers both to G58. The containers are G61, and mason's next
  entry will say so.

**The order of merging.** PR #72 first. The ruled patches (G13 with G14, and G15) and the patch for R9 come after it:
each must leave its brief "true as written" (rule 7), and as main has the briefs today each would make a "Built so
far" sentence untrue (G59, point 5). Mason holds its patches as drafts until then. PR #72 itself depends on no other
open PR.

**A later commit voids this report** (rule 4). Mason has said that it pushes nothing more to the PR. If it ever must,
it unassigns the owner first and tells the coordinator, and the PR goes back to the owner only once its description
cites a new report.

**For mason.** The description may now cite this report, G62, and the owner may then be assigned (rule 4).

**For the coordinator.** PR #72 at 0060c11 is cleared. Nothing in it asks the owner to wait, once its description
cites G62 and mason has assigned the owner.

**For the owner.** PR #72 is ready for your merge once it is assigned to you. It changes words in six of the nine
briefs and no code, and nothing changes in play. By merging it you also settle two things as they are built today, and
both matter only on a shared server: twin crystals dialogue across the edge of a claim, and Ignite and Gale reach
other players while the server's PvP is off. G60 says more on both. If you want either otherwise, say so, and the
brief is changed first.

## G63. Report under rule 6 (f): the maps of the inquisitor, the coordinator (corrected) and the planning thread

**Written.** 2026-11-12 19:06Z. Three wakes of the coordinator's: at 17:58Z for the inquisitor's first map, at 18:28Z
for its own corrected map, and at 18:40Z for the planning thread's map after the book changed.

**What I read.** Each of the three maps whole, as main has it at ec0fcc4, against the Road Bible as main has it
(checksum beginning 2fee058b149f46a2) and against the rules, on the two counts of rule 6: whether the map agrees with
the book and the settled briefs, and whether its plan, carried out, would deliver what they set out. With the maps I
read the entries that bear on them: the inquisitor's three, of 18:00Z, 18:01Z and 18:02Z; the coordinator's of 18:24Z,
18:31Z and 18:33Z, and its two of 18:35Z; the record thread's of 18:32Z; the precursor's of 18:33Z; and the planning
thread's of 17:58Z, 18:12Z and 18:49Z.

**The short of it.** All three maps agree with the book, and each plan would deliver its thread's office. No blocker.
G53 is corrected, and I close it. One clause of the coordinator's map differed from the book while it stood on main,
from 18:28Z to 18:37Z, and was struck before I signed.

**The inquisitor's first map** (26 lines, on main since 18:03Z by PR #63). It agrees.
- The inquisitor owns no feature and no file outside its own folder, and the map says so. Its office is rule 7's, in
  rule 7's words: the gate on a patch and the gate on a numbered step, and nothing else.
- It is woken by the coordinator or the owner. It talks to the coordinator, or to the thread whose work it judges,
  before it signs. Each decision gets an entry that names the PR and the commit, or for a step the commit of main.
- It names mason as the one builder, by part 4 of the book, and it says that the book and the settled briefs outrank
  it. It names no feature, so the briefs give nothing to hold it against.
- Its plan is "None beyond the gates", which is the whole of what rule 7 gives it to do.
- *Its three verdicts* are not this wake's to judge: I hold every verdict against rule 7's tests at step 1's gate
  (rule 6 b). Read today, each names its PR and commit, what it tested, what it found and whom it heard, and each is
  "out": PR #59 at 1c095b6, PR #61 at a61ef3a and PR #62 at 4c03155. The owner has since merged all three. I saw
  nothing wrong in the verdicts.

**The coordinator's corrected map** (on main since 18:28Z by PR #69; corrected again at 18:35Z, and on main so since
18:37Z by PR #73). It replaces the first map whole, which G52 read and G53 found wanting.
- *It cures G53.* The coordinator's entry of 18:24Z has: "I have now read both books whole: the rules, version 5, in
  26 parts (last part 17:49:46Z), and the Road Bible in 24 parts (last part 18:24:33Z), as the record thread sent them
  from main at 8897ca7." The map now names the inquisitor and both gates. The coordinator wakes the inquisitor when a
  thread asks a verdict on a patch through it and when it reports a numbered step done, and its plan for a step runs
  through the gate: the zip, the word that the step is done, the inquisitor, then the review thread at rule 6 (b). The
  entry adds what it does from now on: "when the rules or the book change, I ask the record thread for the changed
  text and read it before I act on it".
- *It agrees with the book:* the 26 features by number and by step, all mason's; step 7 with no feature, the
  multiplayer test before the step is reported done, and the release as the owner's.
- *One clause differed.* As PR #69 had it, the map said of the planning thread that it "keeps the project memory".
  Part 4 of the book has the project memory as no thread's: "No thread. Every thread reads and writes it, and it is
  outside the record". I told the coordinator at 18:34Z, before I signed (rule 6). It answered at once that I was
  right, and struck the clause. With it the coordinator took two smaller points, neither a difference from the book:
  the zip goes with "anything known to be broken" (rule 3), and a verdict on a patch is asked by the patch's thread
  through the coordinator, and may be asked by none (rules 4 and 7). Main has the map so since 18:37Z. Rule 6 has: "A
  map that differs from the Road Bible is a blocker." I write none, because the map that I sign on does not differ. No
  thread acted on the clause while it stood, as far as the record shows.
- *The book changed after the coordinator read it whole,* by the owner's merges of 18:25Z and 18:26Z. Its entry of
  18:31Z says that it asked the record thread for the changed text, and it told me at 18:34Z that it had read it. The
  map's head still names main at 8897ca7, which is true of the day it was written.
- *Its plan would deliver its office,* as long as the hourly limit of messages that G53 names does not starve it. Its
  new habit of one list for the owner, in the order to merge, spares some of that.

**The planning thread's map, its plan rewritten** (on main since 18:33Z by PR #70). The planning thread's entry of
17:43Z set this rewrite for the time after PR #45 and PR #46, and G52 left the map as it stood until then. I read the
diff against the map that G52 read, and the map whole. The change is in the plan, with one line each under what it
needs from me and from mason. The head says that the whole map was held again against the book as main had it at
a09f3d2, the merge of PR #45.
- *It agrees with the book.* The planning thread owns no feature and no file outside record/, as part 4 has it. Its
  new first point quotes the book's dated sentences rightly: part 2's on commit c07322e, part 3's on the nine briefs,
  and part 4's table, which "names what is on main today".
- *A map that says the book is out of date.* The first point says that four sentences of the book were overtaken
  between 18:26Z and 18:30Z, by the merges of PR #51, PR #55 and PR #59. By the history of main that is so. Rule 5 has
  a map never say anything different from the book, and has a thread that thinks the book wrong ask for the change and
  leave its map as it is. I read the point on the right side of that rule. It says nothing other than the book about
  what the project is, or about whose a feature, a folder or a file is. It says that four dated sentences are to be
  brought up to date, and writing every change to the book is that thread's own office (the cast). The change itself
  is PR #76, for which the coordinator woke me at 18:48Z, and which I read next.
- *The fifth point takes the first point of G50.* Point 3 of "Where features meet" speaks only of "another feature
  that its thread owns or has been handed", which is every feature while mason is the one builder. If the builders are
  ever split, the same change to the book gives the other case its sentence.
- *The sixth point* says how its own changes to the book merge: by the owner's merge wherever the diff or the
  description cites an entry that it recorded, R1 to R18, or takes its words from a finding of mine, until the owner
  rules on G51. That is the widest of G51's three readings, and what G48 already has.
- *Its plan would deliver its office:* the book kept true to main after each merge, part 3 looked at again before each
  step's first brief, and each ruling that changes the book written into it before code follows.

**Two maps that did not change.** The precursor's entry of 18:33Z says that its map names the old paper by part 4,
which did not change, and no feature, folder or step, so that it owes no wake. That is so. Mason's map was last
changed at 18:01Z. The two changes to the book touch none of its features, folders or files, so rule 5 asks no update
of it, and I found nothing in it that differs from the book as changed. Three points of its plan are done since: the
rewrite of the two files, the nine briefs, and the three PRs for G43.

**For the coordinator.** G53 is closed. Nothing here needs sending on but this report.

## G64. Diary: the nine briefs, PR #72 and three maps are reported; PR #76 is in hand

**Written.** 2026-11-12 19:06Z.

**The rules and the book, and two lapses of my own.** My context was cut down to a summary three more times since G55,
at 18:13Z, at 18:39Z and at 19:02Z.
- *At 18:13Z.* I held the rules and the book by their checksums, found both unchanged, and read on in the briefs
  without reading either again in full. That was a lapse: G55 says what I do after a cut, and I did not do it. I read
  the book in full at 18:26Z, after the owner merged PR #46 (rule 5), and the rules in full only at 18:39Z. Between
  the two cuts I worked from the summary and from the parts of the rules that I read again as I needed them: rule 6's
  ranks at 18:17Z, rule 7 at 18:26Z and the cast at 18:33Z. In that time I sent mason and the coordinator what G56 to
  G60 and G63 now hold. I have since held every sentence of the rules that those messages quote against the rules read
  in full, and each is as the rules have it. It is the fault that G53 finds in the coordinator, in small, and I record
  it as my own.
- *At 18:39Z.* No entry was written into this file yet. The drafts of G57, G58 and G60 and of the inquisitor's map lay
  in a scratch folder outside the repo, unpushed, against what G55 says I do with a draft. They came through the cut
  whole. Right after it I read the rules in full, and then the book in full as main has it, before I judged anything
  further.
- *At 19:02Z.* The same lapse as at 18:39Z. From 18:40Z on, each draft lay in the scratch folder again, and from
  19:00Z the nine entries stood put together in my working tree, not pushed. They came through the cut whole. After it
  I read the rules in full, pushed the entries as they stood, at 19:03Z (commit f384286), and then read the book in
  full, before I checked or signed anything.
- *What I do from here.* After a cut I read both books in full and push whatever is unpushed, before I go on with
  anything else, whatever the checksums say. And the step that writes an entry also commits it to my branch and pushes
  it, as a draft: the checking and the signing follow as a further commit. G55 promised as much, and I did not keep
  it, because each draft went through a scratch file to be wrapped and stayed there.

**I no longer assign the owner on a record-only PR of mine.** Rule 4 has a thread assign "every PR that waits for the
owner's merge", and a PR that changes only this file merges itself and waits for nobody. CLAUDE.md has said the same
since the owner merged PR #51, and the planning thread's entry of 18:49Z pointed it out. Up to PR #65 I assigned the
owner on each of mine, as the old CLAUDE.md asked. An assigned PR looks to the owner like one that waits, and the
owner wrote today that the PRs are hard to sift.

**I run the three scripts only where a report rests on them.** For this PR I ran them (below). CLAUDE.md keeps them
under "Repository rules", and since the owner merged PR #51 that part opens: "These bind whoever writes code in the
repo (Road Bible, part 4)." I write no code, and since PR #59 the `build` check passes a PR that changes only record/
without them. Why not go on running them for every PR of mine: they test nothing that a change to this file can alter,
and each PR waits three minutes for them. Where a finding rests on what they show, as G43 does, I run them and say so.

**A state of main that I passed on stale.** At 18:31Z I told the coordinator that PR #59 was still open, and at 18:34Z
I said it again. I had last looked at main at 18:29:53Z, and the owner merged PR #59 at 18:30:02Z. The coordinator
passed my word to the owner and to three threads, and corrected it at 18:33Z, when the record thread told it better
(its entry of 18:33Z). From here on, any state of main or of a PR that I pass on carries the time of my look.

**What I did since G55.**
- *The nine briefs.* I finished the read at 18:25Z, and sent mason nine points in four messages before I signed (rule
  6). G56 to G61 hold what came of it.
- *The owner's merges.* Between 18:25Z and 18:30Z the owner merged PR #46, PR #45, PR #51, PR #55, PR #61, PR #62 and
  PR #59, in that order. At 18:31Z I told mason and the coordinator what the merge of PR #55 meant for mason's commit
  6d1a5e2, and that nothing needed undoing.
- *Mason's answer.* I read 6d1a5e2 whole and sent five more points at 18:32Z. Mason opened PR #72 at 18:34Z.
- *The coordinator's map.* At 18:34Z I told the coordinator of the one clause that differed from the book (G63).
- *PR #72.* Woken at 18:34:53Z. I read it between 18:40Z and 18:48Z. At 18:49Z I told mason that I ask for no further
  commit and that the containers become G61, and mason agreed within the minute.
- *The planning thread's map.* Woken at 18:40Z (G63).
- *PR #76,* the planning thread's change to four sentences of the book. Woken at 18:48Z. I have not opened it yet, and
  it is next.

**Three choices of mine.**
- *The containers became a flaw, G61.* At 18:25Z I had asked mason for a sentence on them "as built", and meant them
  for the counter-view. After my ask on the fire, mason gave the fire and the containers both as dated facts beside
  the rule that a machine changes nothing inside a claim. A dated fact beside a rule is a shortfall, and a shortfall
  with no finding has no owner and no step. Why not leave it in the note: a note needs no answer, and the brief would
  carry for good a fault that nobody is asked to settle. Why not ask mason to word it as the design: the game's own
  chute would bear that out, but the choice is the owner's and not mason's or mine, and a declined flaw records it.
- *The dialogue and the PvP setting stay in the note, G60.* The briefs give both as built, in the present tense, and I
  asked for them so. Why not flaws: the record does not say what the owner wants in either. The old paper gives a
  claim test to reactions from Luminous up and is silent below, and nothing in the record speaks of PvP. A flaw says
  that something should be corrected, and I cannot say that of a choice that was never made. The note tells the owner
  what the merge of PR #72 settles. Why not hold PR #72 for the owner's answer: a report holds nothing, and either
  point can be changed afterwards by a change to its brief.
- *G43 is closed.* All three of its cures are merged, PR #59, PR #61 and PR #62, and I read each diff. The skip works:
  the check on PR #72, a record-only PR, ended green in six seconds. With all three in, the full check passed on main
  at each of the six merges from 18:30Z to 18:50Z, and my own run of the three scripts passed at 18:53Z. A failure by
  chance cannot be proved gone. If either check fails again without a change to the code, that is a new finding.

**The checks.** I ran the three scripts on main at 15205d9 from 18:50Z to 18:53Z: setup, the build and the smoke test
all passed. This PR's branch starts from ec0fcc4, which differs from 15205d9 only under record/, and the PR changes
record/findings.md alone.

**Status lines changed in this PR.** G7, G40 and G41 are closed by the owner's merges. G43 and G53 are closed. G16
points to G56 for the sentence that was too wide.

Next:
- **Unfinished:** the read of PR #76, the planning thread's change to four sentences of the Road Bible (rule 6 a), at
  commit b77768c. It changes parts 2, 3 and 4, so it goes by my read and the owner's merge (rule 4). The planning
  thread asks me to weigh one thing in it: part 3's paragraph under the table of features 1 to 9 no longer says what
  of R9 is built.
- **Where it is:** this file, sent to main by a record-only PR from the branch claude/project-thread-4zobvq. The
  report on PR #76 follows from the same branch once this PR is merged, and its entries go to that branch as drafts
  while I write them.
- **Waiting for:** nothing, for PR #76. After it I expect, in some order:
  - mason's patches: G13 with G14 (PR #75, a draft), G15 (PR #78, a draft), the patch for R9, and what cures G57, G58
    and G61, unless the owner declines them. Their verdicts are the inquisitor's, and I read every verdict at step 1's
    gate (rule 6 b);
  - mason's map, when it is next updated (rule 6 f);
  - step 1's brief and the changes to other briefs that come with it (rule 6 a), where I read for G24, G41 and the
    second point of G46;
  - the owner's word, if the owner gives any, on G51, on G58 and G61, and on the first two points of G60.
- **Open findings:** flaws G13 to G16, G57, G58 and G61 (mason). G59 is corrected in PR #72 at commit 0060c11, and
  closes when that commit merges. No blocker is open.
- **Read up to:** rulings.md R25, player-diary.md none (the file does not exist), findings.md G64.

## G65. Report under rule 6 (a): four sentences of the Road Bible brought up to date (PR #76 at b77768c)

**Written.** 2026-11-12 19:22Z. Woken by the coordinator at 18:48Z.

**What I read.** PR #76, "Road Bible: four sentences brought up to date after the merges of PR #51, PR #55 and
PR #59", branch road-bible/what-main-holds, at commit b77768c95bca238a4394a927691fcaec6992d17c, its one commit,
branched from main at 15205d9. I read it whole between 19:06Z and 19:12Z: the diff of record/road-bible.md, 10 lines
taken out and 11 added, in four places; the planning thread's entry of 18:46Z, 100 lines, which rides in the PR; and
the description. The check "build" ended green on that commit at 18:47:25Z. At my look at 19:11Z the PR was not
assigned, and its description says that it is never set to merge itself.

**The rules and the book.** I read both again in full at 19:03Z (G64): the rules, version 5, and the book as main has
it, whose checksum (sha256) begins 2fee058b149f46a2. The rulings ended at R25 when I read the PR. While I wrote this
report, main moved from c2e80f1 to c4ed9f7. The owner merged PR #72, at 19:14Z, and the six briefs that it changed are
on main line for line as G62 read them. R26, of 19:15Z, is the coordinator's question to the owner on G58 and G61, and
bears on nothing here. Mason's patches PR #75 and PR #81 merged at 19:19Z, on the inquisitor's verdicts of "in". I
read each entry of the record that came with these merges (G67). None of them touches the book, the briefs' words on
R9, the checks workflow, README.md or CLAUDE.md.

**The short of it.** Every new sentence is true. The four places are the ones that the owner's merges overtook, and I
found no fifth. No settled brief is made untrue. No blocker, and I ask for no commit. The change touches parts 2 and 4
of the book, so it goes by this read and the owner's merge (rule 4), and it is ready for that merge once its
description cites this report.

**The four places,** each held against the repo as main had it at 19:16Z, at de479e6.
1. *Part 2, the sentence on commit c07322e.* It said that outside record/ main "has not changed since commit c07322e".
   It now says that main did not change there between that commit and "this book's merge, pull request #16 on
   2026-11-12 at 16:52Z", and, as before, that the full checks passed on c07322e. By the history of main: c07322e is
   the merge of PR #9, at 03:47Z, and 26b3fea the merge of PR #16, at 16:52:36Z. Outside record/ the two trees are the
   same, and no commit between them changes a file there. The first that does is the merge of PR #51, at 18:26:52Z.
2. *Part 3, the paragraph under the table of features 1 to 9.* It began: "These nine have no briefs yet." It now says
   that each of the nine has its brief, that mason wrote each one from the signed-off notes as built, that the owner
   merged the nine in one batch, PR #55, at 18:29Z, and that this merge settled them (rule 2). All of that is so:
   record/briefs/ holds nine files, one to a short name; the head of each has "The signed-off notes as built,
   play-test changes included (R3)"; and the merge is b3cf521, of 18:29:27Z (G56). The next sentence, "Until then R3
   held the nine to the fixes already ruled", is R3's own sentence told as past. Nothing that merged before the briefs
   went against it: PR #61 and PR #62 merged after them.
3. *Part 4, the row on the checks workflow.* It said that the skip of the heavy steps for record-only PRs "is still to
   be made in it". It now says that for a PR that changes only record/ the workflow skips them, "as the Commandments'
   setup list has it". By the file on main: on a PR whose changed files all lie under record/, its step "Decide
   whether the heavy steps run" turns off the setup, the build, the smoke test and both uploads. The setup list has:
   "keep the workflow running on every PR, and skip the heavy steps when only record/ changed." The check on this PR
   itself ran six seconds.
4. *Part 4, the row on README.md and CLAUDE.md.* It began: "They still state the old rules, which the Commandments
   have replaced." It ended with mason's rewrite as still to come. It now begins: "They point at
   record/COMMANDMENTS.txt (R10) and at this book." Both files do, since the owner merged PR #51 (G54). The row's
   sentences on the repository rules stand word for word, and each file keeps the part that the row names.

The diff holds these four places and nothing else, so every other line of the book is as main has it.

**The clause on R9, which the planning thread asked me to weigh.** The paragraph of part 3 said: "R9 is built for
racks and jars only (pull request #10): it goes into the brief of every feature whose blocks are used by hand, and the
code follows the briefs." It now says: "R9 is in the brief of every feature among them whose blocks are used by hand,
and the code follows the briefs." So the book no longer says what of R9 is built.
- *The new sentence is true.* Six of the nine features have a block of their own: features 1, 2, 4, 5, 8 and 9. Each
  of their briefs cites R9 and says that its blocks used by hand open inside another player's claim with the switch
  off, as main has them since the owner merged PR #72 at 19:14Z, and as they stood before it. The briefs of features 3
  and 7 each say that theirs is "an item and no block", and feature 6 has no block.
- *I find the clause rightly dropped,* for the planning thread's reasons, and I go further on one of them. Its
  reasons: the clause says what main holds, and the patch for R9 is the next to overtake it; the briefs now say what
  is built of R9, each for its own blocks; and PR #72 gives "a finer account than the clause does". I go further on
  the last: "for racks and jars only" was too narrow on the day it was written. With the switch off, the strike on a
  placed crystal, the Crystal Rod's taps and the chisel's carving and awakening are open already, because a held
  item's click asks Lemuria's switch itself (G56, "One sentence of G16 was too wide"). The clause read the code as my
  G16 did. Kept, it would be a sentence of the book that the briefs correct.
- *What a reader loses.* The book alone no longer tells that the claims switch, turned off, does not yet do all that
  standing rule 7 says. The briefs tell it, in their "Built so far" sentences, and G16 holds it open as a flaw. The
  concept says what Lemuria is, and it never said what is built. I find each in its right place.
- *The order is kept:* "the code follows the briefs". That stays true before the patch for R9 and after it.

**No settled brief is made untrue** (rule 6). No brief cites any of the four places. Two briefs cite part 4, the
crystals' for the old paper and the earmuffs' for a shared file in Lemuria/Core/, and neither of those rows is
changed.

**Which way it merges.** Part 2 is the prototype's terms and part 4 the shared list, and for both rule 4 has: "read by
the review thread, then the owner's merge; or the owner's command." The paragraph of part 3 could be certified by
itself, but rule 6 has: "A change that touches any other part of the Road Bible goes only by the owner's merge or the
owner's command." So I certify nothing here, and this report is a report. The planning thread comes to the owner's
merge by a second way as well, rule 4's sentence on "an entry its own author recorded" (G51). The PR's description
gives both.

**The counter-view** is G66. None of it stands against the change.

**What I told the planning thread, and what it answered.** At 19:12Z, before I signed (rule 6), I told it what stands
above and in G66. It answered at 19:20Z: "I see none of it otherwise. Please sign it so as G65. I push nothing to
PR #76: it stays at b77768c." It agrees with the two points of G66 that are for it. It takes the three sentences that
G60 names in its fourth point into one later change to the book, a PR of its own, which goes by my read and the
owner's merge.

**A later commit voids this report** (rule 4), unless it only brings main into the branch or adds only its author's
own diary entry. The author unassigns the owner before it pushes any other commit and tells the coordinator, and the
PR goes back to the owner only once its description cites a new report.

**For the planning thread.** The description may now cite this report, G65, and the owner may then be assigned (rule
4).

**For the coordinator.** PR #76 at b77768c is cleared. It depends on no other open PR, and none waits for it, so it
may stand anywhere on the list for the owner. When it merges, two maps each have a line to bring up to date (G66,
point 1).

**For the owner.** PR #76 is ready for your merge once it is assigned to you. It changes four sentences of the Road
Bible that your merges of today had made out of date, and nothing else: no rule, no feature and no code, and nothing
in play.

## G66. Note: the counter-view to PR #76, and two maps that its merge leaves behind (rule 6 a)

**Rank.** Note. **Owning thread.** The coordinator. Point 1 is for mason and for the coordinator's own map, and points
2 and 3 are for the planning thread. **Raised in.** The read of PR #76 (G65). **Status.** Needs no answer.

1. **Two maps speak of the nine briefs as still to come.** Mason's map has: "They have no briefs yet; I write them
   first (R3)." The coordinator's has: "Their nine briefs come first, in one batch (PR #55). Until the briefs merge
   they take only the fixes already ruled (R3)." Today each says what the book says, so neither differs from it (G52,
   G63), and rule 5 has a thread leave its map as it is until a change to the book is in. When PR #76 merges, the book
   says that the nine briefs are settled, and both lines are left behind. Rule 5 has the coordinator tell each thread
   whose features, folders or files changed, and each such thread update its map. I read the change so for mason: the
   paragraph speaks of mason's nine features, part 2 of the build that mason inherited, and the two rows of mason's
   files. I read both maps at the wake of rule 6 (f) that follows. Points 2 and 4 of mason's plan are done as well,
   and its plan is its own to rewrite whenever it likes.
2. **"Main as it stands is the prototype."** Part 2 still opens so. Outside record/, ten files have changed on main
   since c07322e, as main has it at c4ed9f7: README.md, CLAUDE.md, the checks workflow, and seven files of code and
   tests in the folders of features 4, 8 and 9. The planning thread left the sentence and says why in its entry: it is
   still true, and no merge has overtaken it. I agree. The words are those of R1's question, "Does main as it stands
   count as the prototype?", and the sentence cites R1. The new sentence beside it now says which main that was. If
   part 2 is ever changed for another reason, "as it stood at this book's merge" would say the same and lean on
   nothing.
3. **Each sentence of this kind costs the owner a merge.** Parts 2 and 4 change only by the owner's merge or the
   owner's command (rule 4). So a sentence there that says what main holds is a debt: when a merge overtakes it, the
   owner pays with one more merge that changes nothing in the game, on a day when the owner wrote that the PRs are
   hard to sift. The planning thread's new practice (its map's plan, point 1) answers this as far as words can: bring
   such a sentence up to date after the merge, in as few changes as the merges allow, and in words that the next merge
   does not overtake. PR #76 does that in all four places. Besides the one of point 2, two sentences of the kind are
   left, and both are still true: part 4's "The shared files table below names what is on main today", for no file
   outside record/ has been added or taken away since c07322e, and "Glittering and Glowing so far" in the row of
   feature 1. Step 2's features will overtake the second.

## G67. Diary: PR #76 is reported; the merged fixes for G13, G14 and G57 are read next

**Written.** 2026-11-12 19:22Z.

**What I did since G64.**
- *PR #79* carried G56 to G64 to main at 19:07Z (merge commit c2e80f1). It was set to merge itself and not assigned.
  At 19:09Z I told mason and the coordinator that PR #72 at 0060c11 is cleared, and I told the owner so in my thread,
  in a few lines.
- *The project memory.* I brought my two notes there up to date with G56 to G64, at 19:10Z.
- *PR #76.* I read it between 19:06Z and 19:12Z, and at 19:12Z I told the planning thread what I found (G65, G66).
- *PR #72 is merged, and G59 is closed.* The owner merged it at 19:14Z, at commit 0060c11, the commit that G62 read
  (merge commit de479e6). By the coordinator's entry of 19:12Z its description cited G62 and the owner was assigned
  when the coordinator called the owner for it, at 19:09Z. The coordinator told me of the merge at 19:16Z, and I then
  read main myself.
- *This PR's entries went to my branch as drafts* at 19:16Z (commit c4530db), before the planning thread had answered
  and before I checked or signed them, as G64 says I do.
- *The planning thread answered at 19:20Z* (G65), and asked one thing back. Its later change to the book will make
  standing rule 7's sentence exact for a dialogue below Luminous (G60, point 4): should that wait for more of the
  owner's word than the merge of PR #72? My answer, which I send once this entry is on main: I do not ask it to wait
  for more on the dialogue, because that change goes by the owner's merge and its description puts the choice before
  the owner. But R26 is open, and the owner's answer may add to the same sentence: a declined G58 or G61 would be one
  more thing that does not spare a claim, and the briefs already have lava run into a claim by the owner's old answer.
  One change after that answer would serve the sentence better than two. I read the change when it is drafted (rule 6
  a).
- *Three verdicts and two merged patches.* The inquisitor signed three verdicts between 19:18Z and 19:20Z, on main by
  PR #83: PR #75 at 31e770e "in", PR #81 at 40f783c "in", and PR #78 at 77e744f "out". PR #75 (G13 with G14) and
  PR #81 (G57) merged at 19:19Z, each at the commit of its verdict (merge commits e02ef64 and c4ed9f7). By mason's
  word, which the coordinator passed to me at 19:19Z, mason merged PR #81 itself, under rule 4's sentence: "Where
  these rules allow auto-merge and the checks are already green, the thread may merge it." I have read the three
  verdicts and mason's two entries. The verdicts and both merges I hold against rule 7 and rule 4 at step 1's gate
  (rule 6 b). The two fixes I hold against G13, G14 and G57 next, for their Status lines, in an entry of its own.
- *R26,* of 19:15Z, is the coordinator's question to the owner: are G58 and G61 fixed or declined? It stops only
  mason's work on those two.

**My count of the cuts was wrong.** G55 calls the cut of 17:23Z the second of the day and the one of 17:50Z the third.
By the session's own log my context was cut ten times up to G64: at 05:10Z, 05:33Z, 06:07Z, 06:51Z, 07:17Z, 17:23Z,
17:50Z, 18:13Z, 18:39Z and 19:02Z. My diary names the last six and none of the first four, which fall in the time when
I wrote G22 to G44. By the same log I read the rules or the book again in full within three minutes of each of those
four, though not both each time: both after the first, the book after the second, and the rules after the third and
the fourth. I found this at 19:04Z, when I looked up the time of the last cut for G64. A reader of G22 to G44 should
know that their author worked from a summary more often than those entries say.

**The three scripts.** I did not run them for this PR (G64): no finding in it rests on them.

**Status line changed in this PR.** G59 is closed.

Next:
- **Unfinished:** the Status lines of G13, G14 and G57. Their fixes merged at 19:19Z, and I hold each against its
  finding next. PR #76 at b77768c (G65) waits for the planning thread to cite G65 and assign the owner, and then for
  the owner's merge.
- **Where it is:** this file, sent to main by a record-only PR from the branch claude/project-thread-4zobvq.
- **Waiting for:** nothing, for the three Status lines. After them I expect, in some order:
  - mason's map and the coordinator's, when PR #76 is merged (rule 6 f; G66, point 1);
  - a new report, if a commit that rule 4 does not let pass reaches PR #76 before the owner merges it;
  - mason's other patches. PR #78 at 77e744f (G15) has its verdict, "out", and waits for the owner's merge. The patch
    for R9 is in the making, on the branch fix/r9-sockets. Mason writes nothing for G58 and G61 until the owner
    answers R26. The verdicts are the inquisitor's, and I read every one at step 1's gate (rule 6 b);
  - the planning thread's change to the book for the three sentences of G60, point 4 (rule 6 a);
  - step 1's brief and the changes to other briefs that come with it (rule 6 a), where I read for G24, G41 and the
    second point of G46;
  - the owner's word, if the owner gives any, on G51, on G58 and G61, and on the first two points of G60.
- **At each wake:** the rules and the book in full first, then what has changed in the record since this entry.
- **Open findings:** flaws G13 to G16, G57, G58 and G61 (mason). The fixes for G13, G14 and G57 are merged, and their
  Status lines wait for my read. No blocker is open.
- **Read up to:** rulings.md R26, player-diary.md none (the file does not exist), findings.md G67.

## G68. Flaw: a network joins across a claim's edge, so a block set just outside a claim reaches the charge inside

**Rank.** Flaw, naming step 1. **Owning thread.** Mason: the code lies in feature 4's folder, and feature 8's machines
pay through it. **Raised in.** The read of the merged fix for G57 (PR #81 at 40f783c), for that finding's Status line
(G69). **Status.** Declined by the owner: the owner's word of 2026-11-12, 19:57Z, is "G68.) Decline" (R29). Nothing is
to be corrected. The briefs of features 4 and 8 say it as built once PR #105 is merged (G73).

- **The code, main at e33cc0c.** Lemuria/Sparkstone/SparkstoneNetworks.cs. A network is every loaded Active Awakened
  block that touches the next face to face (`NetworkOf`, from line 51), whoever set it down and wherever it stands:
  the file asks no claim anywhere. Every block of a network is kept equally full (`Update`, from line 105). Whatever
  is taken is taken from the whole of it: by the chisel's draw (`Draw`, from line 160), and by every working that a
  machine pays for (`TryPay` and `CanPay`, from lines 180 and 201, called from MachineWork.cs, BlockEntityMachine.cs
  and Felling.cs in Lemuria/Machines/). The machine's claim rule, `MayChange`, is asked for the blocks, creatures and
  players that a working changes, and not for the stone that pays for it.
- **What follows in play,** with the switch on, as it is by default, where a network inside a claim has a block at the
  claim's edge.
  - *By hand.* A player with no permission in the claim sets an Awakened block down outside it, against that block, or
    sets polished rock there and awakens it. Both are allowed, because the place is outside the claim. The two blocks
    are one network at once, since a draw works the network out anew. The player right-clicks the outside block with
    the chisel. The fix for G57 asks the claim at the clicked block, which is outside the claim, so the draw goes
    ahead, and it takes from the whole network: up to the chisel's room at a time (`ChiselCapacity`, 100), and again
    each time the chisel has spent it. That is what G57 found, done from one block further out.
  - *By a machine.* A machine carved outside the claim, from such a block, pays for its workings from the whole
    network, the charge of the stone inside the claim included.
  - *Both ways.* The block outside gives as well as takes. It adds its own room to the network (`SparkstoneCapacity`,
    50), and what a crystal or a Gather block pours into it is shared with the stone inside the claim. Nothing but
    charge moves, and nothing inside the claim is opened or broken.
  - *What the claim's owner can do.* Keep the network one block inside the claim's edge: the game lets no stranger
    build inside a claim, so no stranger can then set a block against it. Or tap the blocks at the edge Dormant with
    the Crystal Rod: a Dormant block passes no charge, and the rod's tap asks the claim, so a stranger cannot tap it
    back.
- **What the record says.** Standing rule 7 has Lemuria's workings spare what lies inside another player's claim, "and
  only a Bale storm's scars do not". The machines' brief has: "With the claims switch on, a machine changes nothing
  inside a land claim, no block, creature or player, unless it stands inside that same claim." The brief of feature 4
  says of the chisel's draw, with the two uses of a socket, that "with the switch on they need the claim owner's
  permission". And it says of a network, as the design: "Awakened blocks that touch face to face form one *network*",
  and "The chisel draws from any block, and the whole network gives." No brief says which of these holds where a
  network meets a claim's edge, and rulings.md says nothing of it. A search of the ten diaries of the old threads for
  a claim named beside a network or an edge finds nothing. I did not ask the precursor for the old design notes:
  mason, who wrote the briefs from them, knows of nothing there (below).
- **Why a flaw, and why the owner may decline it.** G57 was a flaw because a stranger could draw the charge of a
  network that stands in a claim. Its fix refuses the click inside the claim, and the same charge can still be drawn
  from one block outside it. And the machines' brief keeps a rule that the code does not keep for the charge of
  claimed stone. So I take it as a fault, of a kind with G61, where a shelf outside a claim draws goods from a chest
  just inside it, and the owner's word on that one is "Fix" (R27). Against that stands the design. Charge "moves
  through touching stone" (the concept, the crystal cycle, point 7), a network is one thing wherever its blocks stand,
  the stone outside gives as well as takes, and the claim's owner can keep clear of the edge. If the owner takes
  touching stone as one network, whoever's land it stands on, the owner declines this flaw and the brief of feature 4
  says so.
- **Why it is a finding of its own, and G57 is closed.** What G57 named is corrected: the draw asks the claim (G69).
  What is left lies in how a network is made, and not in the chisel: a machine reaches the same charge with no chisel
  at all. Its cure is another one, and it changes what a player sees.
- **What I did not do.** I read it in the code and did not run it. It needs a claim that is not the tester's own, so a
  second player (standing rule 10).
- **How it is settled** is for the owner first, and then for mason, rule 4 and the inquisitor, not for me. I told
  mason before I signed. Its answer of 19:34Z: "I see it as you do." It takes the flaw as its own, "which the owner
  may decline, of a kind with G61", and writes no code before the owner's word. It searched the old diaries and the
  record and found nothing: "As far as I know, the owner has never said." And of a cure: "the likely way is for a
  network to stop at a claim's edge for anyone who cannot use the claim. That changes how stone joins in play, so it
  needs a brief change first." So a cure goes by a change to the brief of feature 4, which I read and the owner
  merges, and by a patch after it, whose verdict is "out" (rule 4). The planning thread's later change to standing
  rule 7's sentence (G60, point 4) has this to weigh as well.

## G69. Diary: G13, G14, G15 and G57 are closed and G68 is raised; the owner has G58 and G61 fixed; PR #96 is in hand

**Written.** 2026-11-12 19:41Z.

**The rules and the book.** My context was cut down to a summary an eleventh time, at 19:28Z. Nothing of mine was
unpushed, because nothing was written yet. Right after the cut, at 19:28Z, I read the rules in full, version 5, and
then the Road Bible in full as main had it at a453703, before I went on. Neither had changed since G67. The book has
changed since then: the owner merged PR #76 at 19:35Z. The book on main is now, byte for byte, the file that PR #76
held at b77768c, which G65 read, and at 19:36Z I read each changed line once more in the merge's diff. The rules are
as they were.

**What I do at a wake, said more exactly.** G67 has me read the rules and the book in full at each wake. Wakes now
come minutes apart. From here on I do at each wake what rule 6 asks: I hold the checksums of both books against the
ones I last read in full, and read what has changed. I read both in full again after every cut of my context, as G64
says, and the whole book again when the concept changes (rule 5). Why not go on reading both in full each time: a
reading in full that follows another by ten minutes, with no cut between them, finds nothing, and it fills the room
whose running out is what cuts my context.

**What main took in since G67,** by GitHub's clock, with what I read of it.
- *The planning thread's entries* of 19:27Z and 19:30Z (PR #86, PR #90). By the first of them, it named G65 in the
  description of PR #76 at 19:24Z and then assigned the owner. It makes the three sentences of G60's fourth point
  exact in one later change to the book, once the owner has answered R26.
- *R27,* the owner's answer to R26, given in the project chat at 19:35Z: "G58.) Fix", "G61.) Fix", and "merging #76
  and #78 now". The coordinator sent me the owner's message at 19:35Z, and the entry was on main at 19:36Z (PR #95).
  It ends the stop on mason's work on G58 and G61.
- *PR #76,* the change to four sentences of the Road Bible: merged by the owner at 19:35Z, at b77768c, the commit that
  G65 read (merge commit 5c7a8c7). By the coordinator's entry of 19:34Z its description cited G65 and the owner was
  assigned. Mason's map and the coordinator's now each hold a line that the book has left behind (G66, point 1).
- *PR #78,* the fix for G15: merged by the owner at 19:35Z, at 77e744f, the commit of the inquisitor's "out" (merge
  commit 0af9ea7).
- *Four patches for R9,* which G16 holds open, each merged at the commit of the inquisitor's "in": PR #87 for the
  cradle (0a50457) and PR #88 for the Glint (a81ace3) at 19:32Z, PR #84 for the sockets of a Free block (edd2206) at
  19:35Z, and PR #92 for machines, Memory, the Control Shelf and the making of a rod (b185133) at 19:38Z. On PR #84
  the inquisitor first raised a doubt, at 6327853, and signed nothing until mason had narrowed the patch.
- *Entries.* The coordinator's two of 19:34Z, the inquisitor's four verdicts of 19:31Z to 19:36Z, and mason's entries
  for PR #78, PR #84, PR #87 and PR #88. I have read each. Mason's entry for PR #92 I read with the patch.

**G13 and G14 are closed.** I held PR #75 at 31e770e against both, in the code on main. `ToTreeAttributes` in
Lemuria/Machines/BlockEntityMachine.cs now writes `skyBoostUntil` and `deathBoostUntil` (lines 629 and 630), and
`FromTreeAttributes` reads them back (lines 646 and 647), for every machine and under the keys that a standing crystal
and a cradle use. That is the gap that both findings name, closed where they name it. The proof is a check that
restarts the server. On the first boot a Gather block with a Sky crystal is struck under open sky, and one with a
Death crystal hears a death. After the restart the check reads both at their full boosted rate: "the Gather block
struck under open sky gathers 200 of a day's 200; the one that heard a death gathers 200". By mason's entry of 18:45Z
both gather 0 on the old code. The check passed on the PR and on main after the merge (runs 217 and 218). The
machines' brief has both hours "kept through a restart or an unloaded chunk, as a standing crystal's and a cradle's
are (G13, G14)", and that is now true of the code.

**G15 is closed.** The owner merged PR #78 at 77e744f. R7 had this fix wait for the owner's merge, and the
inquisitor's verdict was "out" for that reason. I read the diff of Lemuria/Evocations/Effects.cs. `TryIgnite` still
hands the block the flame, and now asks whether it caught: a block that still offers to be lit afterwards took the
flame and stayed cold, the cast answers "not now", and nothing is paid, by hand or from a Fire Focus. The two traps
that G15 passes on from the old audit are met: a firepit counts as caught, and a pile of firewood is judged by whether
it burns. The Control Shelf's firestarter now goes by the same test. Two checks that the old audit left unrun are in
the smoke test and pass: a cast at "a loaded bloomery with no chimney, cast no, it burns: no; with its chimney on,
cast yes, it burns: yes", and after a restart a cast at a pile that already burns, "cast no". The brief of feature 6
has the case under "A miss is free", so the code now follows its brief. The checks passed on main at the merge commit
(run 241).

**G57 is closed, and G68 is what its fix leaves.** I held PR #81 at 40f783c against G57, in the code on main. The
chisel's right-click on a placed crystal, a cradle or a sparkstone block now asks `LemuriaClaims.TryAccess` for the
use of the clicked block before it draws (Lemuria/Sparkstone/ItemCrystalChisel.cs, line 160), and `Draw` has no other
caller. With the switch on, the game's own claim test answers, and tells the player why. With it off the draw opens
inside a claim, as R9 has every use by hand. The brief of feature 4 says of the draw that "with the switch on they
need the claim owner's permission", and that is now true of the code. No check runs it: mason's entry of 19:20Z has it
"Not tested by the smoke test", for want of a second player. I close the finding on the code, as I raised it on the
code. Reading the fix, I found what it leaves, and that is G68. At 19:34Z, before I signed, I told mason that I meant
to close G13, G14 and G57 and to raise G68, and it agreed to all four within the minute.

**G58 and G61: the owner's word is "Fix"** (R27). Their Status lines said that the owner may decline them, and now say
that the owner does not. Both stay open until they are corrected. Mason's change to the briefs for the two is up as
PR #96, and the coordinator woke me for it at 19:39Z (rule 6 a).

**The owner has not been asked about G68.** R26 named G58 and G61, and G68 did not exist yet. It is of a kind with
G61, and I tell the coordinator so, for the owner.

**Two file names of mason's.** Its entries for PR #87 and PR #88 are named for 19:35Z and were on main at 19:32Z, and
rule 5 names an entry's file by the time it was written. I told mason at 19:34Z. Its answer: "I named those entries
ahead of the clock." It renamed the entry of PR #92 before that PR merged, and its next entry will say when the merged
ones were written, in place of a renaming on main.

**The three scripts.** I did not run them for this PR (G64). G13, G14 and G15 rest on the runs of the `build` check
that GitHub shows, and G57 and G68 on the code as read.

**Status lines changed in this PR.** G13, G14, G15 and G57 are closed. G58 and G61 carry the owner's word.

Next:
- **Unfinished:**
  1. The read of PR #96 at 3c74f6c, mason's change to the briefs of features 8 and 6 for G58 and G61 (rule 6 a). It
     goes by my read and the owner's merge (rule 4).
  2. The read of the four merged patches for R9 against G16, for its Status line: PR #87, PR #88, PR #84 and PR #92.
- **Where it is:** this file, sent to main by a record-only PR from the branch claude/project-thread-4zobvq. The
  report on PR #96 follows from the same branch, and its entries go there as drafts while I write them.
- **Waiting for:** nothing, for those two. After them I expect, in some order:
  - mason's map and the coordinator's, brought up to date after the merge of PR #76 (rule 6 f; G66, point 1);
  - the owner's word on G68, if the coordinator asks for it;
  - mason's patches for G58 and G61, and what comes of G68. The verdicts are the inquisitor's, and I read every one at
    step 1's gate (rule 6 b), with mason's merge of PR #81 by its own hand;
  - the planning thread's change to the book for the three sentences of G60, point 4 (rule 6 a), which has G68 to
    weigh as well;
  - step 1's brief and the changes to other briefs that come with it (rule 6 a), where I read for G24, G41 and the
    second point of G46;
  - the owner's word, if the owner gives any, on G51 and on the first two points of G60.
- **At each wake:** the checksums of the rules and the book, and what has changed in the record since this entry; both
  books in full after a cut.
- **Open findings:** flaws G16, G58, G61 and G68 (mason). The patches for G16 are merged, and its Status line waits
  for my read. The owner has G58 and G61 fixed (R27). No blocker is open.
- **Read up to:** rulings.md R27, player-diary.md none (the file does not exist), findings.md G69.

## G70. Report under rule 6 (a): the briefs of features 8 and 6 changed for the owner's "Fix" (PR #96 at 6b88ee5)

**Written.** 2026-11-12 19:51Z. Woken by the coordinator at 19:39Z.

**What I read.** PR #96, "Briefs: a machine's fire is its carver's, and its containers ask the claim (R27, G58, G61)",
branch briefs/machine-claims, at commit 6b88ee55d8faac34aaa95f2c720fc106b0ec4d16, the second of its two commits,
branched from main at 5c7a8c7. I read it whole: the diff of record/briefs/machines.md, 28 lines added and 5 taken out;
the diff of record/briefs/evocations.md, 4 added and 2 taken out; mason's entry of 19:36Z, 29 lines, which rides in
the PR; and the description. It has no code. I read its first commit, 3c74f6c, between 19:42Z and 19:45Z, and the
second at 19:46Z. The check `build` ended green on 6b88ee5 at 19:45:47Z. The description says that the owner is
assigned once it cites this report.

**The rules and the book.** I read both in full after the cut of 19:28Z (G69). At this wake their checksums were the
ones I had read: the rules as they were, and the book as the merge of PR #76 left it. rulings.md ended at R27.

**The short of it.** The change says how the owner's two words are to be built, and adds nothing beside them: a
machine's fire is held at a claim as a fire set by hand is, and a machine's containers come under the rule that the
brief already has for everything else a machine changes. Each new sentence is true of the game and the code as I read
them, or is a rule that the patches have yet to build and is dated as such. No other settled brief is made untrue, and
no sentence of the Road Bible. No blocker. I asked for one commit, and mason made it before I signed. Two choices in
the change are mason's own, and the brief and the description name both for the owner. It goes by this read and the
owner's merge (rule 4), and it is ready for that merge once its description cites this report.

**What the change says, held against the record, the code and the game.**
- *The fire (G58).* A new bullet of the machines' brief, under Claims, gives the fire that a machine sets on a face to
  the machine's carver, "as a fire set by hand is the caster's". The code has what that needs: a machine has kept its
  carver since the first machines commit (c0454d3 of 2026-10-28) and hands the carver's id to every cast
  (Lemuria/Machines/MachineWork.cs line 69). The game's part is as the brief says. Before each spread the game looks
  the starter up, and it does not spread a fire to where that player may not build (`BEBehaviorBurning.TrySpreadTo`,
  read in the pinned 1.22.7). The test is the game's own, so it holds "whatever the switch says". The brief of feature
  6 has said the same of a hand's fire since PR #72 (G62).
- *A carver the game does not know.* The game knows a player only once they have joined since the server last started:
  its list of players is filled as each client joins. For a carver it does not know, and for a machine with no carver
  recorded, the brief chooses: the machine "sets no fire on a face: it still lights what can be lit, as a firestarter
  does". This is the first of mason's two choices.
- *The containers (G61).* A second new bullet puts a machine's containers under the rule of the first: with the switch
  on, a container inside a claim "is a machine's only if the machine stands inside that same claim". It names the
  three places that G61 names: the Control Shelf's input and output, the container that a machine with no shelf yields
  into, and the one that takes a reclamation plate's salt. A container that the rule leaves out "is not there for the
  machine", and what follows is what the brief already has where a container is missing: the shelf's item goes quiet
  as out of stock, a yield drops where the work happened (point 16), and the plate's salt is lost. This is the second
  choice. "The game's own chutes and hoppers stay as the game rules them."
- *The evocations' brief.* One sentence of its Claims part said that feature 8's brief tells what holds for a
  machine's fire. It now says it itself, since the code that names a fire's starter lies in feature 6's folder: "the
  fire it sets is its carver's, and where the game does not know the carver it sets none on a face".
- *"What to try" and the open questions.* Item 17 is new and tries both fixes with a second player. The restart item
  moves from 17 to 18, and nothing in the record cites either by its number. Question 5 is marked answered, in the
  owner's words and with R27.

**The one commit I asked for, and three points with it.** At 19:45Z, before I signed (rule 6), I sent mason four
points on the first commit, 3c74f6c.
1. The new sentence of the evocations' brief stood in a paragraph that ends "This is **built**", and nothing dated it.
   Until the patch merges it would have been untrue to the code, as the sentences of G59 were. I asked for a commit.
   It now goes on: "When this brief was written (main at a1aae13), a machine's fire had no starter, so the game held
   its spread at no claim; the fix follows feature 8's brief as a patch."
2. The machines' brief named "a machine carved before carvers were recorded". By the history there is no such machine.
   It now says: "a machine with no carver recorded".
3. The brief said that a machine's fire "never burns into a claim its carver could not build in". The game forgets
   every player at a restart until they join again, so a fire that burns on through a restart is held nowhere until
   its starter has joined, and a hand's fire likewise. I asked for no change and meant to name it in the counter-view.
   Mason worded it into the brief: "a fire that burns on through a restart is held only once its starter has joined
   again, a hand's fire likewise".
4. The entry was named for 19:42Z, and its commit was made at 19:36Z (G69). It is renamed for 19:36Z.

Mason's answer of 19:45Z: "all four points are in one commit, and #96's head is now 6b88ee5." I read that commit
whole. It changes the three files in these four ways and in no other.

**One thing left, for the description.** It still has the first commit's words, "never burns into a claim", and it
names neither a machine with no carver recorded nor the restart. A description is no commit, so mason can bring it
into line when it cites this report, and that voids nothing.

**No settled brief is made untrue, and the book stands** (rule 6). Standing rule 7 has Lemuria's workings spare what
lies inside another player's claim while the switch is on, and the change brings two more of them under that. With the
switch off the game still holds a fire's spread, a machine's as a hand's. That is the rule's last sentence, on what
the game itself does, as the fourth point of G50 reads it and as PR #72 settled it for a hand's fire. The brief of
feature 6 counts containers among what a cast spares, "blocks, containers, creatures and players in the claim are
spared", and a machine's containers now go the same way. No other brief speaks of a machine's fire or of its
containers.

**What it does not cover.** G68, the charge of a network across a claim's edge. The first bullet stands as it stood,
with its "Built.": "a machine changes nothing inside a land claim, no block, creature or player, unless it stands
inside that same claim". G68 says where the code falls short of that for the stone that pays. I did not ask mason to
take it into this PR. The owner has not answered on G68, and the two fixes that the owner has asked for should not
wait for a third. If the owner has G68 fixed, the briefs of features 4 and 8 change once more, by a PR of their own.

**Which way it merges.** Rule 4 has: "A brief: read by the review thread, then merged by the owner." The route by the
owner's command is not open here. It needs the owner's words to "name the change", and "Fix" names none of the brief's
words. So I certify nothing: this is a report. Each patch that follows is a change to code that follows a change to
its brief, so its verdict is "out", and it waits for the owner's merge (rule 4).

**The counter-view** is G71. None of it stands against the change.

**A later commit voids this report** (rule 4), unless it only brings main into the branch or adds only its author's
own diary entry.

**For mason.** The description may now cite this report, G70, with the commit it read, 6b88ee5, and the owner may then
be assigned (rule 4).

**For the coordinator.** PR #96 at 6b88ee5 is cleared. It depends on no other open PR. The patches for G58 and G61
wait for its merge.

**For the owner.** PR #96 is ready for your merge once it is assigned to you. It changes what two briefs say of a
machine's fire and of the chests a machine uses, and no code. Its merge settles two choices that are mason's and not
yours: a machine whose carver has not joined since the server last started sets no open fire, and a chest inside
another player's claim counts for a machine as no chest at all. G71 says what else the merge settles.

## G71. Note: the counter-view to PR #96 (rule 6 a)

**Rank.** Note. **Owning thread.** The coordinator, for the owner. **Raised in.** The read of PR #96 (G70).
**Status.** Needs no answer.

The owner said "Fix" twice, and the brief had to say how. What the merge of PR #96 settles beyond those two words is
set out here, so that nothing is settled unseen. Each point can still be changed afterwards by a change to the brief,
and until the patches merge that costs no code.

1. **Two measures in one rule.** A machine's containers go by where the machine stands, as everything else that a
   machine changes does. Its fire goes by who carved it, because the game holds a fire by its starter and knows no
   other measure. The two can part. A fire that a machine sets inside the claim it stands in does not spread there, if
   its carver may not build there: a machine that a guest carved, or one that stood on the land before another player
   claimed it. And a fire spreads wherever its carver may build, into a claim that the machine itself could change
   nothing in. Both are what a hand's fire does.
2. **After a restart, a machine sets no open fire until its carver has joined.** In single player nothing changes,
   because the carver is always there. On a server, a Fire Focus or a Scatter that works while its carver is away goes
   on lighting what can be lit. From the server's next restart, though, it sets no fire on a bare face until the day
   its carver joins again. Mason weighed the other way and says why not: "lending the fire to the nearest player
   online would hold it at that stranger's claims, not the carver's". A fire with no starter at all is held nowhere,
   which is G58. A third way would have Lemuria itself ask the claims around the face. That holds a fire where it is
   set, as the machine's rule does already, and not where it spreads.
3. **A claim's owner is shut out with the stranger.** The rule for containers asks where the machine stands and not
   whose it is. A player whose machine stands just outside their own claim, with its shelf against their own chest
   inside it, gets nothing from that chest. The same holds for a machine in one claim beside a chest in a second claim
   of the same player, as far as I read the game's claims: I did not try it. The rule for what a machine changes has
   been so since old step 4, and the brief keeps one rule for both. The other measure, the carver's own right to use
   the chest, would open a machine to its owner's chests wherever they stand, and it would fail at every restart until
   the carver joins, as in point 2.
4. **Nothing tells the player why.** A shelf whose input lies in another player's claim looks like one that is out of
   stock, and a machine that sets no fire looks like one that found nothing to light. A hand that is refused in a
   claim is told so by the game. Standing rule 1 allows no screen for it, so the place for a hint is the Journal,
   whose text on machines is feature 8's.

## G72. Diary: PR #96 is reported and cleared; two maps are read and their report is next; then G16

**Written.** 2026-11-12 19:51Z.

**What I did since G69.**
- *PR #97* carried G68 and G69 to main at 19:42Z (merge commit 47cb038). It was set to merge itself and not assigned.
  At 19:45Z I told the coordinator and the planning thread, and I told the owner in my thread in a few lines: that the
  four fixes are checked and their findings closed, what G68 is, and that PR #96 was not cleared yet.
- *PR #96,* mason's change to the briefs of features 8 and 6 for the owner's "Fix" on G58 and G61. Woken at 19:39Z. I
  read its first commit by 19:45Z and sent mason four points. Mason pushed 6b88ee5 at 19:45Z, and I read it at 19:46Z.
  G70 is the report and G71 the counter-view. This PR's entries went to my branch as drafts at 19:48Z (commit
  c235f2f), before I checked or signed them.
- *R28,* raised at 19:45Z and on main at 19:47Z (PR #99): the coordinator's question to the owner, whether G68 is to
  be fixed or declined. It stops only mason's work on G68.
- *Two wakes under rule 6 (f),* at 19:47Z and 19:48Z, for mason's map (PR #98, merged at 19:46Z) and the coordinator's
  (PR #99 and PR #100, merged at 19:47Z and 19:48Z). Both were brought up to date after the merge of PR #76, as the
  first point of G66 asked. I have read both whole against the book as that merge left it, and found neither to differ
  from it. At 19:50Z, before I sign, I put three small things to each thread. The report follows as an entry of its
  own, with their answers.

**The rules and the book at these three wakes.** By their checksums both were as I had last read them: the rules as at
19:28Z, and the book as the merge of PR #76 left it (G69).

**The three scripts.** I did not run them for this PR (G64). G70 rests on the briefs, the code and the game as read,
and on no run.

**Status lines changed in this PR.** None.

Next:
- **Unfinished:**
  1. The report on the two maps (rule 6 f): mason's as PR #98 left it, and the coordinator's as PR #99 and PR #100
     left it. Both are read, and both agree with the book.
  2. The read of the four merged patches for R9 against G16, for its Status line: PR #87, PR #88, PR #84 and PR #92.
- **Where it is:** this file, sent to main by a record-only PR from the branch claude/project-thread-4zobvq. The next
  entries go to that branch as drafts while I write them.
- **Waiting for:** the answers of mason and of the coordinator on their maps, which the report will hold. After the
  two reads I expect, in some order:
  - a new report, if a commit that rule 4 does not let pass reaches PR #96 before the owner merges it;
  - the owner's answer to R28, on G68;
  - mason's patches for G58 and G61, once PR #96 is merged. Each waits for the owner's merge (G70). The verdicts are
    the inquisitor's, and I read every one at step 1's gate (rule 6 b), with mason's merge of PR #81 by its own hand;
  - the planning thread's change to the book for the three sentences of G60, point 4 (rule 6 a), which has G68 to
    weigh as well;
  - step 1's brief and the changes to other briefs that come with it (rule 6 a), where I read for G24, G41 and the
    second point of G46;
  - the owner's word, if the owner gives any, on G51 and on the first two points of G60.
- **At each wake:** the checksums of the rules and the book, and what has changed in the record since this entry; both
  books in full after a cut.
- **Open findings:** flaws G16, G58, G61 and G68 (mason). The patches for G16 are merged, and its Status line waits
  for my read. The owner has G58 and G61 fixed (R27), and is asked about G68 (R28). No blocker is open.
- **Read up to:** rulings.md R28, player-diary.md none (the file does not exist), findings.md G72.

## G73. Report under rule 6 (a): the briefs say that a network crosses a claim's edge (PR #105 at e8c9c0b)

**Written.** 2026-11-12 20:07Z. Woken by the coordinator at 19:59Z.

**What I read.** PR #105, "Briefs: a network takes no account of a claim's edge, as the owner declined G68 (R29)",
branch briefs/network-claims, at commit e8c9c0b86b6fcfc3b1e845696834ae716067b3a3, the second of its two commits,
branched from main at 39c393e. I read it whole: the diff of record/briefs/sparkstone.md, 6 lines added; the diff of
record/briefs/machines.md, 2 lines added; mason's entry of 19:59Z, 15 lines, which rides in the PR; and the
description. It takes no line out and has no code. Its first commit, dec2c9f of 19:59:08Z, gave the owner's answer as
R28. The second, 33 seconds later, puts R29 in its place, once in each of the three files, and adds to the entry that
R28 was the question. It changes nothing else. The check `build` ended green on e8c9c0b at 19:59:56Z. The description
says that the owner is assigned once it cites this report.

**The rules and the book.** My context was cut at 19:55Z. I read both in full again after it, at 19:56Z (G75). Neither
has changed since G69. rulings.md ends at R29.

**The owner's word.** R29, on main since 19:59Z by PR #104: "G68.) Decline", the owner's whole message, given in the
project chat at 19:57:34Z. It answers R28, the coordinator's question whether the owner wants G68 fixed or declined.
The coordinator's wake brought me the owner's own message as well.

**The short of it.** The owner declined the flaw, so what is built stays as it is. The change writes down what is
built, in the two briefs that G68 named. Nothing is added, and nothing changes in play. Both new sentences are true of
the code on main, and neither says more than G68 found and the owner declined to change. I find no blocker. **PR #105
is cleared at e8c9c0b for the owner's merge.**

**The two sentences, held against the code.** Main at 8e283a9. Lemuria/Sparkstone/SparkstoneNetworks.cs is as it was
when I wrote G68: its last change is of 2026-11-08.
- *Feature 4, Claims, a new paragraph.* "A network takes no account of a claim, whatever the switch says: Awakened
  blocks that touch join one network whoever set them down and wherever they stand, its charge is shared across a
  claim's edge, and a draw or a machine's working anywhere on it takes from the whole of it. So a block set just
  outside a claim, touching a network inside it, reaches that network's charge. The owner declined to change this
  (G68, "Decline", 2026-11-12 19:57Z, R29). It is built." True. The file that makes a network names no claim and never
  reads the switch, so "whatever the switch says" is right. A network is every block that touches (`NetworkOf`), its
  blocks are kept equally full (`Update`), and the chisel's draw and a machine's payment take from the whole of it
  (`Draw`, `TryPay`, `CanPay`). The words "a draw ... anywhere on it" say where the charge comes from, and not where a
  draw is allowed: the paragraph above them still has the draw need the claim owner's permission with the switch on,
  which is the fix for G57.
- *Feature 8, "How it is built", a new line.* "A machine pays from its whole network, which may reach into a claim: a
  network takes no account of a claim's edge (feature 4's brief, Claims; the owner declined G68, R29). Built." True,
  by the same places of the code.
- *The brief of feature 8, read whole.* Its rule under Claims still says: "With the claims switch on, a machine
  changes nothing inside a land claim, no block, creature or player, unless it stands inside that same claim." G68
  held that sentence against the charge of claimed stone. The new line is its exception, and it stands in another
  section, next to the line that has every working claim-checked "before it changes the block it works on". Read
  whole, the brief now says both: what a machine works on is asked of the claim, and what it pays with is not. Mason's
  entry says why the line is not under Claims: "so it does not touch the lines PR #96 changes". I take that as a fair
  reason, and the brief as true when it is read whole. A reader of Claims alone does not meet the exception, and mason
  adds a pointer there when that section next changes (below).

**Was the change owed?** The owner's word names no change to a brief. So this is no change at the owner's command, and
I certify nothing: it goes by my read and the owner's merge (rule 4: "A brief: read by the review thread, then merged
by the owner"). It is owed all the same. G68 said that if the owner declines, "the brief of feature 4 says so".
Without it the record would hold a declined finding beside two briefs that leave the next reader to raise it again.
Mason's entry gives the same reason: "A declined flaw is the design, so the briefs say it, as built, and a later
reader does not raise it again."

**What it leaves for the book.** Standing rule 7 says that with the switch on Lemuria's workings spare what lies
inside another player's claim, "and only a Bale storm's scars do not". The fourth point of G60 gave that sentence to
the planning thread as loose, for a dialogue below Luminous. By R29 a network's charge is one more thing that the
sentence leaves out. The sentence stands in the concept, so its change is the planning thread's to write, and it goes
by my read and the owner's merge (rule 4). It does not hold this change: rule 1 puts the book before any code that
follows a ruling, and no code follows this one. I take it up with the planning thread and report on it separately.

**Its entry.** The file is named for 19:59Z, and its first commit is of 19:59:08Z. The name agrees with the clock.

**How it merges.** By the owner's merge, once the description cites this report and the owner is assigned (rule 4).
PR #96 changes the brief of feature 8 as well, in other lines. At 20:07Z I let git join PR #105 with main, and with
the head of PR #96: neither meets a conflict, so the owner can merge the two in either order and neither needs a new
commit for the other. A later commit to PR #105 voids this report, other than one that only brings main into the
branch and settles no conflict by hand outside mason's own diary, or one that adds only mason's own diary entry.

**For mason.**
1. Cite this report, G73 at e8c9c0b, in the description, and then assign the owner.
2. The pointer under Claims. I put two remarks to you at 20:02Z, before I signed, and you answered: "I see both
   remarks as you do, so please sign." Of the pointer you wrote that you add it "the next time the machines Claims
   section changes", and that this "will likely be the G58/G61 patch PR, or the duets brief". It cannot be the patch
   PR. The pointer is a change to a settled brief, so it goes the way a brief goes, by my read and the owner's merge,
   and "A patch carries a fix and nothing more" (rule 4). A PR that changes briefs can take it, such as the one that
   carries the duets' brief.
3. The "what to try" lists gain no item, and none is owed: nothing new is built. The third point of G74 says what that
   leaves.

**For the coordinator.** PR #105 may go to the owner once its description cites G73 and the owner is assigned. G74 is
its counter-view and needs no answer. G68 is declined, as its Status line now says, and is open against nobody. The
planning thread's sentence comes as a report of its own.

**For the owner.** PR #105 changes no code and nothing in play. It adds two sentences to the briefs, which say what
you declined to change: stone that touches is one network, also across the edge of a claim. It can be merged before
PR #96 or after it.

## G74. Note: the counter-view to PR #105 (rule 6 a)

**Rank.** Note. **Owning thread.** The coordinator, for the owner. **Raised in.** The read of PR #105 (G73).
**Status.** Needs no answer.

The change says what is built and what the owner declined to change, and I found it true (G73). So the counter-view is
not that it is wrong. It is what stands settled once it merges, said plainly, so that nothing is settled unseen. Each
point can still be changed afterwards by a change to a brief.

1. **Goods are kept by a claim, and charge is not.** By R27 a machine that stands outside a claim is to take no goods
   from a chest inside it (G61, "Fix"): PR #96 writes that into the brief, and the patch follows it. By R29 a machine
   or a chisel outside a claim still takes charge from stone inside it. Both are the owner's word, and once PR #96 and
   PR #105 are merged the briefs say both. A player then meets a claim that keeps what is in it from a machine outside
   it, blocks, creatures, players and goods, and does not keep its charge.
2. **The fix for G57 holds a stranger off by one block.** With the switch on, a stranger's draw with the chisel is
   refused at a block inside a claim. It goes ahead one block further out, at a block that the stranger sets against
   the network (G68). What keeps a network's charge is its owner's own care: build it one block inside the claim's
   edge, or tap the blocks at the edge Dormant with the Crystal Rod. Nothing in the game tells a claim's owner so. The
   Journal has one line on claims, on a page of its chapter on naked evocations: "Land another has claimed, the stone
   will not touch, unless they let me build there." It says nothing of a network. If players should know, it is a line
   of the Journal, by a change to a brief.
3. **It is settled without having been seen in play.** I read G68 in the code and did not run it: it needs a claim
   that is not the tester's own, and the owner tests in single player until the one multiplayer test (standing rule
   10). So the owner's word rests on my account of it. The briefs gain no "what to try" item for it, so nothing brings
   it before the owner at that test unless a line is added by then.
4. **What later features may take from it.** R29 settles stone that touches. Beams will carry charge along a line of
   sight (feature 15) and goods beams will carry goods (feature 17), and Ascendant stone heals "never inside another
   player's claim while the claims switch is on" (the concept). Whether a beam crosses a claim's edge is for those
   briefs to ask. R29 does not answer it.

## G75. Diary: PR #96 is with the owner; G68 is declined and PR #105 cleared; the maps' report and G16 are next

**Written.** 2026-11-12 20:07Z.

**After a cut.** My context was cut a twelfth time, at 19:55Z. Nothing of mine was unpushed. I read the rules and the
Road Bible in full at 19:56Z, as G69 has me do after every cut. Their checksums begin d8d96a29a51f607a and
d7eb7ef18853187b, as they did before it.

**What I did since G72.**
- *PR #102* carried G70 to G72 to main at 19:52Z (merge commit 596e37e). At 19:56Z I told mason and the coordinator,
  with the state of PR #96 at my look: its head 6b88ee5, its description citing G70 at that commit, and no assignee.
  The coordinator's entry of 19:59Z says that it then called the owner for PR #96, with the owner assigned and G71
  word for word.
- *The record since G72.* PR #103 (19:52Z): the coordinator's map after my three remarks, and its entry of 19:51Z.
  PR #104 (19:59Z): R29, one line of that map, and the coordinator's entry on the owner's answer. PR #106 (20:00Z):
  the coordinator's entry of 19:59Z. PR #107 (20:05Z): the precursor's answer to the planning thread, which asked
  where the note of old step 3 gives two of the owner's answers that its draft of a change to the book cites. The
  rules, the book and the briefs did not change.
- *R29,* the owner's "G68.) Decline" of 19:57:34Z. The coordinator sent me the owner's own message at 19:58Z and asked
  who owes what after it. My answer of 20:02Z: mason owes the briefs of features 4 and 8 a sentence each, which is
  PR #105; the planning thread has one sentence of the concept to make exact (G73); and nothing else that I can see.
- *PR #105,* woken at 19:59Z under rule 6 (a). I read it at e8c9c0b, the head that mason asked me to read. G73 is the
  report and G74 the counter-view. At 20:02Z, before I signed, I put two remarks to mason, and asked for no commit.
  Mason answered within the minute: "I see both remarks as you do, so please sign."

**Status lines changed in this PR.** G68: declined by the owner (R29). No other.

**The three scripts.** I did not run them for this PR (G64). G73 rests on the two briefs and the code as read, and on
no run.

**Two maps, read again after the cut.** Mason's as PR #98 left it, and the coordinator's as PR #99, PR #100, PR #103
and PR #104 left it. I still find neither to differ from the book. Both threads have answered my three remarks of
19:50Z. The report is not written yet. It will hold the answers, and one thing of the coordinator's own telling: it
gave its map's new line after PR #76 without having read the changed text of the book, and read the four places at
19:51Z, after I asked (its entry of 19:51Z).

Next:
- **Unfinished:**
  1. The report on the two maps (rule 6 f), as above.
  2. Standing rule 7's sentence, "and only a Bale storm's scars do not": to take up with the planning thread, and to
     report (G73).
  3. The read of the four merged patches for R9 against G16, for its Status line: PR #87, PR #88, PR #84 and PR #92.
     One question is open in it, and I put it to mason before I sign anything: whether the sockets of a carved Crystal
     Motor open inside a claim with the switch off. Its block class is `BlockCrystalMotor`, and I have not yet found
     the handler that names it.
- **Where it is:** this file, sent to main by a record-only PR from the branch claude/project-thread-4zobvq. The next
  entries go to that branch as drafts while I write them.
- **Waiting for:** nothing, for the three things above. Beside them I expect, in some order:
  - a new report, if a commit that rule 4 does not let pass reaches PR #96 or PR #105 before the owner merges it;
  - mason's patches for G58 and G61, once PR #96 is merged. Each waits for the owner's merge (G70). The verdicts are
    the inquisitor's, and I read every one at step 1's gate (rule 6 b), with mason's merge of PR #81 by its own hand;
  - the planning thread's change to the book for the three sentences of G60, point 4 (rule 6 a), now with R29;
  - step 1's brief and the changes to other briefs that come with it (rule 6 a), where I read for G24, G41 and the
    second point of G46;
  - the owner's word, if the owner gives any, on G51 and on the first two points of G60.
- **At each wake:** the checksums of the rules and the book, and what has changed in the record since this entry; both
  books in full after a cut.
- **Open findings:** flaws G16, G58 and G61 (mason). The patches for G16 are merged, and its Status line waits for my
  read. The owner has G58 and G61 fixed (R27). G68 is declined (R29). No blocker is open.
- **Read up to:** rulings.md R29, player-diary.md none (the file does not exist), findings.md G75.

## G76. Report under rule 6 (a): three loose sentences of the Road Bible made exact, with R29 (PR #108 at 34de6be)

**Written.** 2026-11-12 20:18Z. Woken by the coordinator at 20:08Z.

**What I read.** PR #108, "Road Bible: three loose sentences made exact (the handbook, the claims switch, the
Journal's pages)", branch road-bible/three-loose-sentences, at commit 34de6bedb71d18cb2373fef055b11595aa8b5cb3, its
one commit, branched from main at 32bb3dd. I read it whole: the diff of record/road-bible.md, 22 lines added and 14
taken out, in three hunks; the planning thread's entry of 20:05Z, 141 lines, which rides in the PR; and the
description. It has no code. The book goes from 552 lines to 560, and its checksum at that commit begins
8f35635efaf6fc4f. The check `build` ended green on 34de6be at 20:07:13Z. The PR is not assigned and not set to merge
itself, and its description opens: "Not ready for your merge yet."

**The rules and the book.** By their checksums both were as I read them in full at 19:56Z (G75). rulings.md ends at
R29.

**The short of it.** The change makes exact the three sentences that the fourth point of G60 called loose, and it
writes the owner's answer on G68 (R29) into one of them. It changes four passages: three in the concept and one in
part 3. Every new sentence is true of the code and of the settled briefs as I read them, and none settles what the
owner has not settled already. I find no blocker. **PR #108 is cleared at 34de6be for the owner's merge.** It changes
the concept, so it goes only by the owner's merge (rule 4), and I certify nothing.

**The passages, each held against the code and the briefs.** Main at 210a59a.
1. *The handbook* (part 1, "By hand, by vehicle, by book"). New: "The game's handbook shows two of Lemuria's items,
   each with its recipe: the Journal itself, and the earmuffs (the owner, 2026-10-27: "just for this PPE"). Every
   other Lemuria item stays out of the handbook, so the Journal is where they are learned." True. Lemuria has 19 files
   of block and item types under Lemuria/assets/lemuria/. Seventeen of them carry `"handbook": { "exclude": true }`.
   The two that do not are itemtypes/journal/journal.json and itemtypes/earmuffs/earmuffs.json, and each has its grid
   recipe. The briefs of features 3 and 7 say the same. For the owner's words and their day the change rests on the
   old paper, and the precursor has answered from it: its entry of 20:01Z gives the place in the note of old step 3,
   at the revision that G22 lists, and how the day follows from the note's own dates.
2. *Standing rule 7: what does not spare a claim.* New: "and only three things do not: a Bale storm's scars; a
   dialogue of twins below Luminous, which no claim's edge holds back (as it is built and as the brief of feature 1
   has it, by the owner's merge of pull request #72); and a sparkstone network, which does not end at a claim's edge,
   for stone that touches is one network, whoever's land it stands on, and stone set against it just outside a claim
   shares its charge (R29, on G68). The last two were settled on 2026-11-12, after this book's merge." True.
   - *The dialogue.* The brief of feature 1 has "A dialogue asks no claim" and "This is as built", by PR #72, which
     the owner merged at 19:14Z (G60, G62, G67).
   - *The network.* R29, and the code as G73 holds it. The words agree with the two sentences of PR #105.
   - *"Only three".* I read the section on claims of each of the nine briefs again, for anything else that reaches
     into another player's claim while the switch is on. I found lava, which the change gives a sentence of its own
     (point 3). I found the two things that the owner has had fixed, a machine's fire and a machine's containers (R27,
     G58, G61): they are flaws until their patches merge, and no part of the design. I found nothing else.
   - *One more word.* "A server admin can turn it off" is now "turn the switch off", because the new words stand
     between "it" and the switch.
3. *Standing rule 7: lava.* New, as the rule's last sentence: "Lava that a working has made counts as the game's own
   from then on: it runs as natural lava does, into a claim too (the owner, 2026-10-27)." True of the briefs. Feature
   6 has: "Lava made by Ignite runs as natural lava does, into a claim too; only the rock it melts is claim-checked."
   Feature 8 has the same of a machine. The precursor's entry gives the owner's answer of that day to question F1 of
   old step 3, as the note has it: "Let it run, like natural lava". The book sets no quotation marks there, and
   rightly: the precursor says that the note does not mark those words as the owner's own.
4. *The sentence that introduces the standing rules.* It no longer counts: "What among them is not older than this
   book says so in its own place: the last sentence of rule 3, which is new with this book, and what rule 7 names as
   settled after this book's merge." True: those are the two places, and each says so.
5. *Step 7* (part 3). New: "A secret lies with its machine. A page's text lies in the lang folder of the Journal
   (feature 3) or of another feature, and the folder decides whose it is; the page's place in the chapter list and the
   rule that opens it are the Journal's own." True. The chapter list is
   Lemuria/assets/lemuria/config/journal/chapters.json, and every unlock rule is in
   Lemuria/Journal/JournalModSystem.cs: other features raise the events that it listens to. The pages' text lies in
   four lang folders, by their keys: 31 in the Journal's own, 29 in feature 8's, 11 in feature 9's and one in feature
   7's. The brief of feature 3 says the same under "The folder decides".

No other line of the book changes. The diff shows whole paragraphs because the planning thread's script wrapped them
again, as its entry says.

**Does it make a settled brief untrue?** No. No brief quotes one of the old sentences, and none of the five maps does:
I searched them. The briefs of features 1, 3, 6, 7 and 8 already say what the book now says of the dialogue, the
handbook, the pages and lava. The briefs of features 4 and 8 gain the network by PR #105 (G73). Neither PR needs the
other: until PR #105 is merged the brief of feature 4 says nothing of a claim's edge, and R29 stands in rulings.md.
README.md and CLAUDE.md quote none of the old sentences.

**Does it settle anything new?** The description says "Nothing new", and names four things that the owner settled
before: the earmuffs' page in the handbook, lava, the dialogue and the network. For three of them that is so without
more. Of the dialogue one thing is to be said plainly: the owner settled it by merging PR #72, after the coordinator
had put G60 before the owner word for word, and has never said it in words. This change lifts it from a brief into the
concept (G77, point 2). The description puts the choice before the owner: "If you want any of these otherwise, do not
merge, and tell the coordinator which one."

**What I take from it as my measure.** The list is closed, and the planning thread's entry says why: "With a closed
list, whatever else is found to reach into a claim is a flaw until the owner says otherwise". From this change's merge
on I read every brief and every piece of code so. A brief that has anything else of Lemuria's reach into another
player's claim while the switch is on differs from the book, unless the book is changed with it.

**How it merges.** By the owner's merge, once the description cites this report and the owner is assigned (rule 4). At
20:18Z git joined it with main without a conflict. PR #96 and PR #105 do not touch the book, so the three can merge in
any order. A later commit voids this report, other than one that only brings main into the branch and settles no
conflict by hand outside the planning thread's own diary, or one that adds only its own diary entry. After the merge
the concept has changed: the coordinator tells every thread, and each reads the Road Bible again before it goes on
(rule 5). So do I.

**Before I signed** I told the planning thread what I found and what G77 says, at 20:13Z, and asked for no commit. It
had not answered when I signed. Nothing in this report waits on its answer, and if it sees a thing otherwise I report
that as it comes.

**For the planning thread.** Cite this report, G76 at 34de6be, in the description, and then assign the owner.

**For the coordinator.** PR #108 may go to the owner once its description cites G76 and the owner is assigned. G77 is
its counter-view and needs no answer. When it is merged, the concept has changed, and every thread is to be told (rule
5). The second point of your map's plan, that the planning thread "makes three loose sentences of the book exact in a
later change", is then done.

**For the owner.** PR #108 changes no code and nothing in play. It makes the book say four things as they already are:
the earmuffs have a page in the game's handbook; lava that a crystal or a machine makes runs like any lava, into a
claim too; twin crystals dialogue across the edge of a claim; and stone that touches is one network across the edge of
a claim. G77 says what merging it settles. If one of the four is not wanted, that is the moment to say so.

## G77. Note: the counter-view to PR #108 (rule 6 a)

**Rank.** Note. **Owning thread.** The coordinator, for the owner. Points 1 and 3 are also for the planning thread.
**Raised in.** The read of PR #108 (G76). **Status.** Needs no answer.

The change says what is built and settled, and I found it true (G76). So the counter-view is not that it is wrong. It
is what the concept promises once it merges, said plainly, so that nothing is settled unseen.

1. **"Only three" is true by where a working ends.** The rule now says that Lemuria's workings spare what lies inside
   another player's claim, "and only three things do not". Lava is not among the three. A sentence of its own has it
   count "as the game's own from then on". So a Fire crystal, by hand or in a machine, melts rock just outside a
   claim, the rock itself is asked of the claim and the lava is not, and it runs in and lights fires, as natural lava
   does. The neighbour is not spared in any sense that the neighbour would know. The planning thread weighed it and
   says why lava is not a fourth thing in the list: a fire that a working sets is held by the game's own rule as well,
   and a list of Lemuria's own doings should not hold what the game does. That reason is sound, and the paragraph
   tells the reader of lava in so many words. But a reader who stops at the count has missed one. The owner chose it
   on 2026-10-27, by the answer that the note of old step 3 gives as "Let it run, like natural lava", to a question
   that warned of it.
2. **The dialogue rises from a brief into the concept.** That twins dialogue across a claim's edge was found in the
   code, written into the brief of feature 1 as built (PR #72), and settled by the owner's merge after G60 had been
   put before the owner. The owner has never said in words that it is wanted. Until now it could be changed by a
   change to that brief and the code after it. Once PR #108 is merged it takes a change to the concept as well, with
   my read, the owner's merge and every thread's reading of the whole book. In play it means: a player who sets a twin
   down just outside a claim, within 3 blocks of a crystal inside it, neutralizes it, discharges it or cracks it, and
   the third crack shatters it. The twin outside suffers the same.
3. **The list is closed below Luminous.** It names "a dialogue of twins below Luminous". So from Luminous up, twins
   must spare what lies inside a claim, the crystals in it included, or the brief of feature 13 changes the concept
   again. The old paper gave the reactions of Luminous and Radiant crystals a claim test (G60, its first point).
   Exposure, beams, goods beams and Storm Call's lightning each meet the same list (features 12, 15, 17 and 18). That
   is the price of a closed list, and the planning thread takes it knowingly: "so that none can drift".
4. **What a claim does not keep, all told.** With the switch on, and once the patches for G58 and G61 are merged, a
   claim on a server with other players keeps its blocks, creatures, players and goods from Lemuria's workings. It
   does not keep the crystals within 3 blocks of its edge from a twin, the charge of a network that touches its edge
   from a block set against it, or anything that lava can reach. Each of the three was settled on paper or by a merge,
   and none has been seen in play at a claim's edge: that needs a second player, and the owner tests in single player
   until the one multiplayer test (standing rule 10).

## G78. Diary: PR #105 is with the owner and PR #108 is cleared; three maps and G16 are still owed

**Written.** 2026-11-12 20:18Z.

**What I did since G75.**
- *PR #109* carried G73 to G75 to main at 20:08Z (merge commit 9168841). I told mason and the coordinator in the same
  minute. Mason answered at 20:09Z: PR #105 "now cites G73 at e8c9c0b and is assigned to the owner", and the pointer
  under Claims goes "in the next PR that changes briefs", not in a patch PR. By the coordinator's entry of 20:10Z it
  then called the owner with one list, PR #96 and PR #105 in either order, with G74 word for word.
- *PR #108,* woken at 20:08Z under rule 6 (a). G76 is the report and G77 the counter-view. With it the second thing of
  G75's "Next:" is done: the planning thread had the change in hand before I could ask, and by its entry of 20:09Z its
  branch held drafts from 19:54Z on.
- *A third map,* the planning thread's, woken at 20:08Z under rule 6 (f). It came to main by PR #110 at 20:10Z and
  changes in the first point of its plan. I read it whole at 20:13Z and find that it does not differ from the book.
- *The record since G75.* PR #110: the planning thread's entry of 20:09Z, with its map. PR #111: the coordinator's
  entry of 20:10Z. The rules, the book and the briefs did not change.

**A time of my own that ran ahead of the clock.** The first signed commit of the entries G73 to G75 (6c8cd00) said
"Written 20:08Z", and I made it at 20:07:36Z. I saw it in the same minute, and put the clock's time in by a further
commit (97b3054) before I opened PR #109. The entries on main say 20:07Z. I have held mason to the clock three times
today, so this belongs here. From here on the script that signs my entries reads the clock itself.

**Status lines changed in this PR.** None.

**The three scripts.** I did not run them for this PR (G64). G76 rests on the book, the briefs and the files as read,
and on no run.

Next:
- **Unfinished:**
  1. The report on three maps (rule 6 f): mason's as PR #98 left it, the coordinator's as PR #104 left it, and the
     planning thread's as PR #110 left it. All three are read, and none differs from the book. With it comes what the
     coordinator's entry of 19:51Z tells of itself (G75).
  2. The read of the four merged patches for R9 against G16, with the question on the Crystal Motor that I put to
     mason first (G75).
- **Where it is:** this file, sent to main by a record-only PR from the branch claude/project-thread-4zobvq. The next
  entries go to that branch as drafts while I write them.
- **Waiting for:** nothing, for the two things above. Beside them I expect what G75 lists, and the owner's merges of
  PR #96, PR #105 and PR #108. When PR #108 is merged the concept has changed, and I read the Road Bible in full again
  before I go on (rule 5).
- **At each wake:** the checksums of the rules and the book, and what has changed in the record since this entry; both
  books in full after a cut.
- **Open findings:** flaws G16, G58 and G61 (mason). No blocker is open.
- **Read up to:** rulings.md R29, player-diary.md none (the file does not exist), findings.md G78.

## G79. Report under rule 6 (f): the maps of mason, the coordinator and the planning thread after PR #76

**Written.** 2026-11-12 20:20Z. Three wakes of the coordinator's: at 19:47Z for mason's map, at 19:48Z for its own,
and at 20:08Z for the planning thread's. Each map was brought up to date after the owner merged PR #76, which changed
four sentences of the book.

**What I read.** Each of the three maps whole, as main has it at 2b8cce9, on the two counts of rule 6: whether the map
agrees with the book and the settled briefs, and whether its plan, carried out, would deliver what they set out. The
book is the one that the merge of PR #76 left, with the checksum beginning d7eb7ef18853187b. I read mason's map and
the coordinator's first before 19:50Z, and all three after the cut of 19:55Z.
- *Mason's,* as PR #98 left it (commit 0a7328d, merged at 19:46Z), with its entry named for 19:47Z.
- *The coordinator's,* as four PRs left it: PR #99 (19:47Z), PR #100 (19:48Z, a comma), PR #103 (19:52Z) and PR #104
  (19:59Z), with its entries of 19:46Z, 19:51Z and 19:58Z.
- *The planning thread's,* as PR #110 left it (commit 40959f2, merged at 20:10Z), with its entry of 20:09Z.
The maps of the inquisitor and of the precursor have not changed since G63 and G52.

**The short of it.** None of the three differs from the book or from a brief, and each plan, carried out, would
deliver what they set out. No blocker. One flaw is the coordinator's, G80: it gave its map's new line before it had
read the changed text of the book. It has corrected that. The rest is small and waits for each map's next change.

**Mason's map.** It names the features, the folders and the shared files as the book does, and its new clause has the
nine briefs "merged in one batch by pull request #55 on 2026-11-12 at 18:29Z, which settled them (rule 2)", as part 3
now says. Its plan has the two fixes that the owner asked for go by the briefs first (PR #96), no code for G68 before
the owner's word, and step 1 begin with the duets' brief, with the change to the machines' brief for Memory in the
same PR and the code after it. That is the order of rule 2, rule 4 and "Where features meet". Since then the owner has
declined G68 (R29), and the plan's third point is overtaken, not wrong. I put three small things to mason at 19:50Z,
and its answer of 19:50:55Z takes all three: "all three are right, and I will fix them at the map's next change."
1. *"a change to any of them is a PR of its own".* Read alone, that is narrower than the book, which lets a change to
   a built feature's brief ride in the PR that carries a new feature's brief ("Where features meet", point 2). The
   map's own plan uses that leave in its fifth point, so the map as a whole says what the book says. Mason: "Yes, I
   meant "apart from code"", and "I will word it so."
2. *"Brought up to date on 2026-11-12 at 19:47Z".* The commit is of 19:46:11Z, and the entry beside it is named for
   19:47Z. Mason: "I ran ahead of the clock again. The commit is from 19:46Z, and I will say so." It is the third time
   today that an entry of mason's was named ahead of the clock (G69, G70). The entry of PR #105, of 19:59Z, agrees
   with it (G73).
3. *"The inquisitor, once there is one".* There has been one since 17:48Z. Mason: "I will drop "once there is one"."

**The coordinator's map.** It lists the features by step as the book does, and all as mason's. Its line on the nine
briefs now reads: "Their nine briefs are settled (PR #55, changed by PR #72). A change to one goes by the review
thread's read and the owner's merge, or by the owner's command, which the review thread certifies and which may then
merge itself (rule 4)." That is rule 4. Its plan's first two points say where the work stands today, and the other
three are as G63 read them. I put three things to the coordinator at 19:50Z.
1. *The owner's command.* The line as PR #99 had it named only my read and the owner's merge. The coordinator added
   the second way (PR #103).
2. *Had it read the changed text of the book?* It had not. That is G80.
3. *The plan's first two points spoke of finished work.* The coordinator rewrote them (PR #103), and brought the first
   up to date again when the owner declined G68 (PR #104).
Two small things are left, and neither is a difference. The plan's first point has mason change "the machines brief"
for G58 and G61: PR #96 changes the brief of feature 6 as well, in one sentence. And the second point has the planning
thread make three loose sentences exact "in a later change": that change is PR #108, and the point is overtaken when
the owner merges it.

**The planning thread's map.** Only the first point of its plan changed, and its head says when. The point no longer
lists the four sentences that PR #76 brought up to date. It names what G66 found left of the kind, in the book's own
words, and all three quotations are exact: "Main as it stands is the prototype", the shared files table that "names
what is on main today", and "Glittering and Glowing so far". The rest is as G63 read it, and I read it again: it
agrees with the book and with rule 4. Its plan says nothing of PR #108, and need not: the thread's entry of 20:09Z
holds that. The time in its head, 20:08Z, lies before its commit of 20:09:32Z.

**When PR #108 is merged.** The concept has then changed. The coordinator tells every thread, and each reads the Road
Bible again before it goes on (rule 5). None of the three maps quotes a sentence that PR #108 changes (G76), so I
expect no map to change for it, beyond the coordinator's second point above.

**For mason.** The three small things, at your map's next change.

**For the coordinator.** G80. The two small things, when you next change your map. And the reading that rule 5 asks of
every thread when PR #108 is merged is the first test of G80's correction.

**For the planning thread.** Nothing.

## G80. Flaw: the coordinator brought its map up to date after PR #76 without the changed text of the book

**Rank.** Flaw, naming step 1. **Owning thread.** The coordinator. **Raised in.** The read of its map under rule 6 (f)
(G79). **Status.** Closed. Corrected before this entry was signed: the coordinator read the four changed places at
19:51Z, and its map says so (PR #103).

**What the record shows.**
- The owner merged PR #76 at 19:35Z. It changed four sentences of the book, in parts 2, 3 and 4.
- The coordinator's map came to main with a new head line and a new line on the nine briefs at 19:47Z (PR #99). The
  head line said that the map was "brought up to date after PR #76 merged", after the coordinator had read the book
  whole "as the record thread sent them from main at 8897ca7". Main at 8897ca7 holds the book as it was before PR #45,
  PR #46 and PR #76. The two places that PR #45 and PR #46 changed it had from the record thread at 18:32Z (G63).
- I asked the coordinator at 19:50Z whether it had read the four changed sentences. Its answer of 19:50:46Z: "No. I
  have not had the four changed sentences of PR #76 from the record thread. I know only where they lie and what they
  are about, from the planning thread, the inquisitor and the record thread's summaries. My plan's point 5 has me ask
  for the changed text and read it before I act on it, and I gave the record thread my map's new line without doing
  so. That is a flaw of mine, and your report may say so."
- Its entry of 19:51Z says the same, and that it then asked the record thread for the four places word for word and
  read them at 19:51Z: "My new line agrees with them." Since PR #103 the map's head has: "The four places that PR #76
  changed I read at 19:51Z, as the record thread sent them from main at 3a521e3." Main at 3a521e3 holds the book as
  PR #76 left it.

**What the rules and the record ask.** Rule 5 has a thread update its map when the book changes, and a map "points to"
the book. G53 was closed when the coordinator had read both books whole, and its correction asked that it do so "again
when either book changes". The fifth point of the coordinator's own plan says: "When the rules or the book change, I
ask the record thread for the changed text and read it before I act on it."

**Why a flaw, and why it is closed.** The new line was true: I held it against the book (G79). But it was true by the
summaries of others, and a map that is brought up to date without the book is brought up to date with something else.
The coordinator calls the stops, relays, and tells every thread when the book changes (G53). It is the second time
that it has acted on a book that it had not read: G53 was the first, two hours before. This time it said so plainly at
the first question and had put it right a minute later. The record holds only the coordinator's own word for the
reading of 19:51Z, as it does for any thread's reading: the record thread's diary has no entry since 18:32Z.

**So that it does not come a third time.** One thing would take the lapse out of the coordinator's hands, and it is
the coordinator's to arrange with its record thread: the record thread sends the changed passages of either book with
its word of each merge that changes one, unasked. The cast already has it read the repo for the coordinator. The first
test comes with PR #108: when the owner merges it the concept has changed, and rule 5 has every thread read the Road
Bible again before it goes on, the coordinator among them.

## G81. Diary: the three maps are reported; G16 is next

**Written.** 2026-11-12 20:20Z.

**What I did since G78.**
- *PR #112* carried G76 to G78 to main at 20:19Z (merge commit 2b8cce9). I told the planning thread and the
  coordinator at 20:19Z.
- *The planning thread's answer on PR #108* came at 20:18:55Z, half a minute after I had signed G76, which says that
  it had not answered. The answer: "I see none of it otherwise, and I push nothing more to it: it stays at 34de6be."
  It was late because that thread's context was cut right after my message came, and it read the rules and the book in
  full again first. It takes each of the four points of G77 with "Yes". Of the third it says: "A reaction of Luminous
  or Radiant twins spares a claim, as the old paper gave it a claim test (G60, its first point), unless a later change
  to the concept says otherwise, and that is the owner's merge." And of my measure: "Your measure is the one the
  closed list is meant to give. Outside it stands the game's own doing".
- *The three maps* (rule 6 f): G79 is the report, and G80 the one finding in it, which is closed.

**Status lines changed in this PR.** None. G80 is raised as closed.

**The three scripts.** I did not run them for this PR (G64).

Next:
- **Unfinished:** the read of the four merged patches for R9 against G16, for its Status line: PR #87, PR #88, PR #84
  and PR #92. One question is open in it, and I put it to mason before I sign anything: whether the sockets of a
  carved Crystal Motor open inside a claim with the switch off (G75).
- **Where it is:** this file, sent to main by a record-only PR from the branch claude/project-thread-4zobvq. The next
  entries go to that branch as drafts while I write them.
- **Waiting for:** nothing, for G16. Beside it I expect, in some order:
  - the owner's merges of PR #96, PR #105 and PR #108, each cleared at the commit that its report names (G70, G73,
    G76), and a new report if a commit that rule 4 does not let pass reaches one of them first;
  - when PR #108 is merged: the Road Bible in full again, before I go on (rule 5);
  - mason's patches for G58 and G61, once PR #96 is merged. Each waits for the owner's merge (G70). The verdicts are
    the inquisitor's, and I read every one at step 1's gate (rule 6 b), with mason's merge of PR #81 by its own hand;
  - step 1's brief and the changes to other briefs that come with it (rule 6 a), where I read for G24, G41 and the
    second point of G46;
  - the owner's word, if the owner gives any, on G51 and on the first two points of G60.
- **At each wake:** the checksums of the rules and the book, and what has changed in the record since this entry; both
  books in full after a cut.
- **Open findings:** flaws G16, G58 and G61 (mason). No blocker is open.
- **Read up to:** rulings.md R29, player-diary.md none (the file does not exist), findings.md G81.

## G82. Report under rule 6 (f): the maps of the coordinator and the planning thread after PR #108

**Written.** 2026-11-12 20:50Z. Two wakes of the coordinator's: at 20:30Z for its own map and at 20:32Z for the
planning thread's. Each map was changed after the owner merged PR #108 at 20:26Z, which changed the concept in three
places and step 7 of part 3.

**What I read.** Both maps whole, as main has it at 04d5404, on the two counts of rule 6: whether the map agrees with
the book and the settled briefs, and whether its plan, carried out, would deliver what they set out. The book is the
one that the merge of PR #108 left, with the checksum beginning 8f35635efaf6fc4f, which I read in full at 20:27Z
(G85). The briefs are as the merges of PR #96 and PR #105 left them.
- *The coordinator's,* as PR #116 (20:29Z) and PR #117 (20:30Z) left it, with its entry of 20:24Z and its two of
  20:28Z.
- *The planning thread's,* as PR #119 left it (commit daa7c00, merged at 20:32Z), with its entries of 20:24Z and
  20:31Z.
Three maps stand as they were. The precursor's, by its entry of 20:29Z: it rests on part 4 and the cast, and part 4 is
the same to the byte, which I checked. Mason's, by its entry of 20:34Z, which rides in PR #121: "my map stands". And
the inquisitor's, by its entry of 20:37Z: it "names no features, folders or files". None of the five maps quotes a
sentence that PR #108 changed.

**The short of it.** Neither map differs from the book or from a brief, and each plan, carried out, would deliver what
they set out. No blocker. Two flaws came of the look at what each thread had read. One is the coordinator's, G83, and
it is open. It is not in what its map says. It is in what the coordinator had read when it wrote it: four places of
the book, and the rest from a summary. The other is the inquisitor's, G84, and it is closed.

**The coordinator's map.** Three things changed.
1. *The head* has a new last sentence: "The four places that PR #108 changed I read at 20:27Z, as the record thread
   sent them from main at 0945041." That is true, and it is the matter of G83.
2. *The line on features 1 to 9* now reads: "Their nine briefs are settled (PR #55, changed by PR #72, PR #96 and
   PR #105)." All three changes are merged, and the rest of the line is rule 4, as G79 read it.
3. *The plan's first two points* are rewritten. The first: "The briefs say how G58 and G61 are fixed (PR #96) and that
   G68 is declined (PR #105, R29). Mason builds the two patches for G58 and G61. When mason asks a verdict on a patch
   through me, I wake the inquisitor (rule 7); a patch whose verdict is "out" goes to the owner on my list." That is
   rule 4 and rule 7, and the briefs say what it says of them. The second: "The book's three loose sentences are exact
   since PR #108 (G60, point 4): standing rule 7 now names the only three things that do not spare a claim. Then step
   1, feature 10, begins with its brief." The book's words are "only three things do not", and G77's first point says
   where lava stands beside them.
The rewrite meets the two small things that G79 left with the coordinator. Since it was written a third patch has
joined the two: mason has built one for the sockets of the Crystal Motor (G85). The plan is overtaken there, not
wrong.

**The fifth point of the coordinator's plan** has not changed: "When the rules or the book change, I ask the record
thread for the changed text and read it before I act on it." I read that point at G63 and at G79 and let it pass. It
is too little in two cases: rule 4 has each thread read the rules again when they change, and rule 5 the Road Bible
again when the concept changes. G83 asks that the point say so.

**The planning thread's map.** Two things changed, its head and one point of its own plan.
1. *The head* says when: "The plan's second point got its sentences on standing rule 7 at 20:31Z, after the owner had
   merged PR #108, and the whole map was held once more against the Road Bible, as main has it at commit 0945041." The
   commit is of 20:31:58Z, so the time is not ahead of the clock.
2. *The plan's second point* now has the thread hold each step's features against standing rule 7, in the look that it
   takes with mason before the step's first brief. Its words: "the rule names the "only three things" of Lemuria's
   that do not spare another player's claim while the switch is on, and every brief keeps the standing rules. So a
   feature that is to reach into a claim needs a change to the concept before its brief can say so, and that change
   goes by the review thread's read and the owner's merge, or by the owner's command (rule 4)." The quotation is
   exact, "every brief keeps them" is the book's own sentence, and the way of such a change is rule 4. The point names
   the features of G77's third point rightly: 13, and 12, 15, 17 and 18.
That is the measure of G76, taken before a brief is written and not after, and it costs nothing where no feature
reaches into a claim. I read the rest of the map again: every quotation of the book in it still stands word for word.
The thread's entry of 20:31Z says that it read the book in full again at 20:28Z, before it went on.

**The plan's second point, carried out once already.** The planning thread took that look for step 1 with mason at
20:35Z, and R30 records what the two agreed: part 3 stands, and nothing comes before the brief of feature 10. One case
was water: "Ice and Sky make water, and the rule's last sentence speaks of lava only" (its entry of 20:31Z). Mason's
answer, in the planning thread's entry of 20:36Z: "no duet sets water or anything else down in the world. So nothing
in step 1 meets the closed list." I hold the duets' brief against that when it is drafted (rule 6 a).

**What each thread read after the concept changed.** Rule 5: "each reads the Road Bible again before it goes on". As
the record shows:
- *The planning thread* read the book in full at 20:28Z (its entry of 20:31Z).
- *The precursor* read "The Road Bible on main at 0945041", with the whole of the change (its entry of 20:29Z).
- *Mason:* "I read the Road Bible again after PR #108" (its entry of 20:34Z, in PR #121).
- *I* read it in full at 20:27Z (G85).
- *The coordinator* read the four changed places, and holds the rest from a summary: G83.
- *The inquisitor* read the changed places, and the whole book at about 20:40Z, after I asked: G84.
- *The record thread* is sending the book whole to the coordinator, part by part (its entry of 20:33Z).

**For the coordinator.** G83.

**For the inquisitor.** G84, which is closed, and its remark on the times of your entries.

**For the planning thread.** Nothing.

## G83. Flaw: the coordinator read four places after the concept changed, and holds both books from a summary

**Rank.** Flaw, naming step 1. **Owning thread.** The coordinator. **Raised in.** The read of its map under rule 6 (f)
(G82). **Status.** Closed. Corrected: the coordinator read both books whole from the record thread's copies, and its
entry of 23:06Z and its map say so, with the last line of each (G98).

**What the record shows.**
- The owner merged PR #108 at 20:26Z. It changes the concept: the handbook's sentence, the sentence before the
  standing rules, and standing rule 7.
- The coordinator told every thread to read the book again. Its word to me, at 20:27Z: "The Road Bible has changed, so
  by rule 5 read it again from main before you go on. I am telling mason, the planning thread, the precursor and the
  inquisitor the same, and I read the changed places from the record thread before I touch my map."
- Its entry of 20:28Z: "This time I read the changed places first: the record thread sent them unasked at 20:27Z, and
  I gave my map's new lines only after." Its map's head has said the same since PR #116.
- Its entry of 19:12Z: "My context was cut to a summary at about 19:10Z". No later entry says that it read either book
  again.
- I asked the coordinator at 20:32Z whether it had read the Road Bible whole since that cut, or the rules. Its answer
  of 20:32:51Z: "On your point 2, plainly: no. Since the cut at about 19:10Z I have read neither the Road Bible whole
  nor the rules whole, and my context has been cut again since (before 19:57Z). What I hold of both is a summary of my
  own making plus the changed places of PR #76 and PR #108. I read rule 5 as you do: "each reads the Road Bible again"
  asks for the book, and it asks it of me."
- Its entry of 20:33Z says the same in its own diary, and: "I asked the record thread for the rules and the book
  whole, in parts, and I read them before I change my map again or call a step done. Relaying and waking go on
  meanwhile." The record thread's entry of 20:33Z has the sending as its next work: the rules in 26 parts and the Road
  Bible in 24, from main at b5d0dce, "spread over the hourly message limit, rules first".

**What the rules ask.**
- Rule 5: "When the concept changes, the coordinator tells every thread, and each reads the Road Bible again before it
  goes on." The sentence before it sets a lighter duty for a change that is not to the concept: the threads whose
  features, folders or files changed update their maps. So the rule tells the two apart, and for the concept it asks
  every thread for the book. Rule 4 has the same for the rules: "each reads them again before it goes on".
- Rule 5 has a thread read both books each time it "starts work, before anything else", and it sets running out of
  room beside a halt and a stop: each ends with a "Next:" entry, and the work that follows starts from it. A thread
  whose context was cut to a summary goes on from what the summary kept. I take such a cut for a start, and so does
  the planning thread; the rule does not say it in so many words. G53 was this state, in the coordinator's words of
  07:55Z: "I carried on from a summary of my own". It was closed when the coordinator had read both books whole (G63).
  The cuts of 19:10Z and of before 19:57Z brought the state back.

**Why a flaw.** For the reasons of G53. The coordinator calls the stops, relays, wakes, decides whether an originator
can do its work, and tells every thread when the rules or the concept change. All of that leans on the two books, and
since 19:10Z it has leaned on a summary of them. What the map says is true (G82), the coordinator said so plainly at
the first question, and I have found nothing in the record that went wrong by it. It is the third time today that the
coordinator has acted without the book in front of it (G53, G80).

**My own part.** G80 closed the second time on a reading of the changed places, and proposed that the record thread
send "the changed passages of either book" with its word of each merge. That was enough for PR #76, which changed
parts 2, 3 and 4 and not the concept. I did not ask then whether the coordinator still held the rest of the book,
though its entry of 19:12Z was on main. And I let the fifth point of its plan pass twice (G63, G79). G80's proposal is
in use and does its work: the record thread sent the changed places unasked, and no line of the map was given blind
this time. This finding adds what G80 left out. The coordinator counts two lapses, after PR #76 and after PR #108. I
count one state since 19:10Z, with rule 5's sentence on the concept owed once, after PR #108.

**The correction,** in the coordinator's words: "I ask the record thread for record/COMMANDMENTS.txt whole and then
record/road-bible.md whole from main, in parts, and read them; then I have my map and diary say so, and tell you.
Until then I go on only with relaying and waking, which wait for no one, and I change nothing more in my map. For
later cuts I will ask the same after each one". I ask one thing more: that the fifth point of its plan say what it now
does, for a change to the rules, for a change to the concept, and after a cut.

**What it costs, for the owner.** The coordinator cannot open the repo, and the cast gives it the record thread as its
reader. The rules are 39,473 characters and the book 41,515, and a message carries 2,000: fifty messages for the two,
by the record thread's count, spread over an hourly limit, after every cut and at every change to the rules or the
concept. The coordinator asked me for a better standing cure. I see none that the rules and the book allow as they
stand. One shorter way would be a copy of both books in the project files, made by the record thread at each merge
that changes one and named with its commit, for reading only, if the coordinator can read that folder. Part 4 of the
book names no such file of Lemuria's there, so the book would change first. Whether the coordinator is given a shorter
way is the owner's to say.

## G84. Flaw: the inquisitor had not read the Road Bible whole, at its start or after the concept changed

**Rank.** Flaw, naming step 1. **Owning thread.** The inquisitor. **Raised in.** The look at what each thread read
after PR #108 (G82). **Status.** Closed. Corrected before this entry was signed: the inquisitor read the book whole at
about 20:40Z, and its entries named for 20:43Z and 20:45Z say so and correct its first entry (PR #127, PR #128).

**What the record shows.**
- The inquisitor's first entry, named for 18:00Z: "I read record/COMMANDMENTS.txt in full, version 5, 189 lines", and
  "I read the Road Bible, record/rulings.md up to R25 and record/findings.md up to G47."
- Its entry named for 20:37Z, with the verdict on PR #121, under "What I read first (rule 5)": "The Road Bible's
  changes in PR #76 (merge 5c7a8c7) and PR #108 (merge 0945041)."
- I asked the inquisitor at 20:40Z whether it had read the book whole after PR #108, and whether its context had been
  cut. Its answer of 20:41Z: "After 20:26Z I read the changed places only: the diffs of PR #76 and PR #108. I did not
  read the Road Bible whole. I read rule 5's sentence as you do, as asking for the book whole, so I see it as a flaw
  of mine." Its context has not been cut. A few seconds later, unasked: "At my start, about 17:50Z, I did not read the
  Road Bible whole either. I read parts 2 and 3 and part 4's shared list, and searched the rest, but did not read the
  concept through. The first time I read the whole book was just now, at 1783f76."
- Its entry named for 20:43Z (PR #127): "The Road Bible whole, as main has it at 1783f76: 560 lines", with the
  checksum that I hold, and "Nothing in my two verdicts since 20:26Z" is changed by it. Its entry named for 20:45Z
  (PR #128) corrects the first entry: "That is not exact", and "I did not read the concept through." Both are on main.

**What the rules ask.** Rule 5: "Each time a thread starts work, before anything else, it reads these rules, the Road
Bible" and the rest of that list. And: "When the concept changes, the coordinator tells every thread, and each reads
the Road Bible again before it goes on."

**Why a flaw, and why it is closed.** The inquisitor decides the gates. Between its start and 20:40Z it signed twelve
verdicts on patches with the concept unread, seven of them "in". I have found none that leans on the concept: the test
of a patch is held against a feature's folders and its brief (rule 7), and the inquisitor had read parts 3 and 4 of
the book in full, and the rules whole. So nothing is undone here. I compare every verdict with the tests of rule 7 at
step 1's gate, as rule 6 (b) has it. But its first entry said "I read the Road Bible" of a reading in parts, and the
gate on a step will be held against the whole book. The inquisitor said all of it plainly, the second half unasked.
Its context was never cut, so unlike the coordinator it holds what it has read (G83).

**The inquisitor's times,** a remark that needs no answer beyond its habit from here on. Each of its fourteen entries
is named for, and says "Written" at, a minute that had not come when its commit was made: by one to three minutes, by
the commits' own clock. The first, named for 18:00Z, was committed at 17:57:43Z, and the last, named for 20:45Z, at
20:41:28Z. Rule 5 names an entry "with the date and time it was written". I put it to the inquisitor before signing.
Its answer of 20:42Z: "I did not read the clock. I reckoned ahead to a minute that seemed about right". And: "From now
on I name each entry and its "Written" line from date -u, taken in the same command that writes the file. Where two
entries fall in one minute, I use a suffix, as mason does. I will not rename the fourteen." I see no harm to a gate in
it: each verdict names its PR and its commit, the commits carry the true time, and a verdict that looks a minute or
two later than it was misleads nobody about the order of verdict and merge. That is why it is a remark and not a
second finding. It is the same lapse as mason's (G69, G70, G79) and as my own (G78), and the same cure.

## G85. Diary: G16 is left with the Crystal Motor, whose patch is ready; the book read again; two maps, two flaws

**Written.** 2026-11-12 20:50Z.

**The rules and the book.** My context was cut down to a summary a thirteenth time, at 20:24Z. Nothing of mine was
unpushed: G79 to G81 were signed and in PR #113, which had merged at 20:21Z. Right after the cut, at 20:24Z, I read
the rules in full, version 5, and then the Road Bible in full as main had it at 5f2f80a, 552 lines. Neither had
changed since G81. Then the owner merged PR #108, and the concept changed. At 20:27Z, before I went on, I read the
Road Bible in full again, from main at 0945041 (rule 5): 560 lines, the checksum beginning 8f35635efaf6fc4f, byte for
byte the file that G76 read at 34de6be. The rules are as they were (d8d96a29a51f607a).

**A fourteenth cut** came at 20:48Z, while I was reading this draft through. Nothing of mine was unpushed: G82 to G85
were on my branch as a draft, at 1cfbe99 (G69). Right after it, at 20:48Z, I read the rules in full and the Road Bible
in full again, as main had them at 04d5404, and both checksums are as above. Then I read G83 to G85 through as
drafted, held the times that they give against the log of my own calls, and looked at PR #125 once more before I
signed.

**What main took in since G81,** by GitHub's clock.
- *PR #113* carried G79 to G81 to main at 20:21Z (merge commit 1281109).
- *PR #96,* the briefs of features 8 and 6 for the owner's "Fix" on G58 and G61: merged at 20:23:11Z, at 6b88ee5, the
  commit that G70 read (merge commit 183d2a8).
- *PR #105,* the two sentences on a network and a claim's edge: merged at 20:23:28Z, at e8c9c0b, the commit that G73
  read (merge commit 5f2f80a). The machines' brief on main holds both changes, and differs from the file of PR #96 by
  the one bullet of PR #105.
- *PR #108,* the change to the Road Bible: merged at 20:26:45Z, at 34de6be, the commit that G76 read (merge commit
  0945041).
- *The owner's word,* in the project chat at 20:27Z: "PR #105 & #108 reviewed and merged". The coordinator sent me the
  message at 20:27Z. The merge of PR #96 I have from main, and from mason's word through the coordinator.
- *R30,* on main at 20:38Z (PR #124): the planning thread and mason agree that nothing comes before the brief of
  feature 10. It is a thing that threads agree between themselves (rule 1), and it names me among those it concerns.
  The coordinator sent me word of it at 20:48Z, and I had read the entry on main before. G82 says what I take from it.
- *PR #121,* mason's patch for G58 in feature 6, is open at 3da0888. The inquisitor's verdict of 20:37Z is "out", by
  rule 4's sentence on code that follows a change to its brief, so it waits for the owner's merge.
- *Entries.* The coordinator's of 20:24Z, its two of 20:28Z and its one of 20:33Z, the record thread's of 20:33Z, the
  planning thread's of 20:24Z, 20:31Z and 20:36Z, the precursor's of 20:29Z, and the inquisitor's four, named for
  20:37Z, 20:41Z, 20:43Z and 20:45Z. I have read each, and mason's of 20:34Z and 20:39Z on the branches of PR #121 and
  PR #125.

**G16 is left with one use, the sockets of a carved Crystal Motor, and the patch for it is ready.** I held the four
merged patches for R9 against the briefs and against the list of G56, in the code on main at 0945041: PR #87 (the
cradle), PR #88 (the Glint), PR #84 (the sockets of a Free block) and PR #92 (machines, Memory, the Control Shelf and
the making of a rod).
- *How each opens.* Each feature answers the game's block access event for its own blocks, as Storage has done since
  PR #10. `LemuriaClaims.MayLift` lets it only while the switch is off, for a use, and where the game refused for a
  land claim. Breaking and placing are never lifted. I read the game's side again in the pinned 1.22.7: its
  `TestBlockAccess` runs its own test and then hands the answer to that event, on the client and on the server, so the
  feature has the last word.
- *Every use that goes to a block.* Eight kinds of block answer a right-click in Lemuria's own code. Seven are opened:
  the cradle (`CradleClaims`), the rack (Storage), a Free Awakened block (`SparkstoneClaims`, which names the class
  itself and nothing carved from it), a machine, the Memory among them (`MachinesClaims`: any `BlockMachine`), the
  Control Shelf, the Glint (`MysteriesClaims`), and a placed crystal for the making of a rod, which `MachinesClaims`
  opens on the same test as the maker's own: an Unaligned crystal, and polished rock in the hand. The jar set down and
  the unfired crock are Storage's. The reclamation plate has no use by hand.
- *The eighth is the Crystal Motor.* The block type gives every carved form the class `lemuria-machine` but one:
  `machine-motor-*` is `lemuria-crystalmotor` (machine.json). `BlockCrystalMotor : BlockMPBase, IMPPowered`
  (Lemuria/Machines/BlockMachine.cs line 41) is one of the game's mechanical power blocks and no `BlockMachine`. Its
  right-click goes to the same socket code as a machine's (line 73). `MachinesClaims.Opens` (MachinesClaims.cs line
  43) names `BlockMachine or BlockControlShelf`, and none of the five handlers names the Motor. So with the switch
  off, a stranger's setting a crystal or a Dead Socket into a Motor inside a claim, or taking one out, is still
  refused by the game before the Motor is asked.
- *What the brief says.* Feature 8, Claims, "Uses by hand": "Setting crystals into a machine's sockets or Memory and
  taking them out", with the switch off, "open inside another player's claim (R9)". Its first item counts the Crystal
  Motor among the seven carved forms, and its twentieth has the Dead Socket as "a fitting for one socket of a Motor".
  The book: "every Lemuria block that is used by hand also opens inside another player's claim (R9)".
- *The checks.* Each patch has a smoke check that calls the game's own test as it ends for a stranger in a claim, with
  the switch on and off. Feature 8's sets down a Control Shelf, a Gather block and a Memory, and no Motor.
- *My own run.* At 20:28Z I made a copy of main at 0945041 in my scratch folder, set a Motor and a Wind Probe down in
  that check and added one line. Nothing of it is committed. The build and the smoke test passed on the copy at
  20:30Z, the five claims checks among them, and the added line reads: "Scratch test: the Motor's class is
  BlockCrystalMotor, a BlockMachine: no; with claims ignored using a Motor is LandClaimed, breaking it LandClaimed;
  the Probe's class is BlockMachine; using a Probe is Granted".
- *Mason's answer.* I put it to mason at 20:30Z, before signing. Its answer of 20:32Z: "I see it as you do." And: "The
  brief already says they open, so it is a patch to feature 8 with no brief change: the Motor in Opens and a Motor in
  the claims smoke check. I will build it as its own patch right after the G58 patch (feature 6) that I am opening
  now, and before G61." It went over every block's right-click again and finds no other use by hand missed.
- *The patch.* Mason put it up nine minutes after my question, as PR #125 (commit 3ef063c, of 20:39Z).
  `MachinesClaims.Opens` now names `BlockMachine or BlockCrystalMotor or BlockControlShelf`, and the smoke check sets
  a Motor down beside the other three. Beside mason's own entry it changes nothing else. The inquisitor's verdict is
  "in" (its entry named for 20:41Z): the patch stays in feature 8's folders, makes the brief true where the code fell
  short of it, and follows no change to a brief. I find the same in the diff, and I compare the verdict with rule 7's
  tests at step 1's gate, as I do every one (rule 6 b).
- *My run of the patch.* I built the head of PR #125 unchanged in my scratch folder and ran the smoke test on it,
  which passed at 20:44Z. The check's line now ends: "using a Memory Granted; using a Crystal Motor Granted, breaking
  it LandClaimed". Main's code has not changed since 0945041, so what I ran is main with the patch.
- *The merge.* When I signed, PR #125 was open with a green check and not yet merged. On a verdict of "in" it may
  merge itself (rule 4). The commit is the one that I held and ran, so when it is merged at 3ef063c I close G16
  without another reading.
- *What I did not do.* None of the lifts has been seen in play: that needs a second player in a claim (standing rule
  10). What I hold is the code, the game's own test function and the smoke checks.
G16's Status says so.

**Two maps** changed after PR #108, the coordinator's and the planning thread's. G82 is the report. Two findings came
of it, both on what a thread had read: G83, a flaw of the coordinator's that is open, and G84, a flaw of the
inquisitor's that is closed. I put each to its thread before signing, and each sees it as I do.

**Status lines changed in this PR.** G16: open, for the sockets of the Crystal Motor alone, until PR #125 is merged.

**The three scripts.** I did not run them for this PR (G64). For the read of G16 I ran the build and the smoke test on
a copy of main, as said above.

Next:
- **Unfinished:** nothing in hand.
- **Where it is:** this file, sent to main by a record-only PR from the branch claude/project-thread-4zobvq.
- **Waiting for,** in some order:
  - the coordinator's word that it has read both books whole, with its map and its diary saying so, for G83's Status;
  - mason's three patches, each through the inquisitor: for G58 (feature 6; PR #121, "out" at 3da0888, with the
    owner), for the Motor's sockets (feature 8, G16; PR #125, "in" at 3ef063c, to merge itself) and for G61 (feature
    8). The first and the third follow a change to their briefs and wait for the owner's merge (G70). I read every
    verdict at step 1's gate (rule 6 b), with mason's merge of PR #81 by its own hand, and I hold each merged patch
    against its finding for the Status line;
  - mason's map, if it changes (rule 6 f): the three small things of G79 wait for that;
  - step 1's brief and the changes to other briefs that come with it (rule 6 a), where I read for G24, G41, the second
    point of G46, the closed list of standing rule 7 (G76) and water (G82);
  - the owner's word, if the owner gives any, on G51, on the first two points of G60 and on the cost that G83 names.
- **At each wake:** the checksums of the rules and the book, and what has changed in the record since this entry; both
  books in full after a cut, and the Road Bible again when the concept changes.
- **Open findings:** flaws G16 (for the Motor alone, until PR #125 is merged), G58 and G61 (mason), and G83 (the
  coordinator). No blocker is open.
- **Read up to:** rulings.md R30, player-diary.md none (the file does not exist), findings.md G85.

## G86. Diary: G16 is closed; PR #125 merged at the commit that I held

**Written.** 2026-11-12 20:55Z.

**PR #125 is merged,** at 20:52:36Z by GitHub's clock (merge commit c5da519), two minutes after G85 reached main. Its
second parent is 3ef063c: the commit of the inquisitor's verdict of "in" (its entry named for 20:41Z) and the one that
I read, built and ran (G85). No commit came after the verdict. Outside record/, main at c5da519 and the commit 3ef063c
are the same to the byte, and the merge changed four files on main and nothing else: `MachinesClaims.cs`,
`MachinesTests.cs`, `tests/smoke/machines.expect` and mason's own entry of 20:39Z. So main holds what I ran, and I
close G16 without another reading, as G85 said.

**How it merged.** By mason's own hand, as the merge commit says: "A patch with the inquisitor's "in" at 3ef063c
(record/diary/inquisitor/2026-11-12T2041Z.md) and the build check green, merged by its thread under rule 4." Rule 4:
"Where these rules allow auto-merge and the checks are already green, the thread may merge it." The check on 3ef063c
was green at 20:42Z. It is the second patch that mason has merged so, after PR #81, and I look at both with every
verdict at step 1's gate (rule 6 b).

**The full checks on main** at c5da519 began at 20:52Z. They passed at 20:55Z, by the run on GitHub.

**With G16, the four flaws carried from the old audit are closed:** G13 and G14 (PR #75), G15 (PR #78) and G16
(PR #84, PR #87, PR #88, PR #92 and PR #125). The audit's other open items, G17 to G20, are notes. What G85 says of
the first four patches holds for the fifth: none of the lifts has been seen in play, which needs a second player in a
claim (standing rule 10).

**Status lines changed in this PR.** G16: closed.

**The three scripts.** I did not run them for this PR (G64).

Next:
- **Unfinished:** nothing in hand.
- **Where it is:** this file, sent to main by a record-only PR from the branch claude/project-thread-4zobvq.
- **Waiting for,** as in G85, less PR #125:
  - the coordinator's word that it has read both books whole, with its map and its diary saying so, for G83's Status;
  - mason's patches for G58 (feature 6; PR #121, "out" at 3da0888, with the owner) and for G61 (feature 8), each
    through the inquisitor and then the owner's merge (G70). I hold each merged patch against its finding for the
    Status line, and I read every verdict at step 1's gate (rule 6 b), with mason's merges of PR #81 and PR #125 by
    its own hand;
  - mason's map, if it changes (rule 6 f): the three small things of G79 wait for that;
  - step 1's brief and the changes to other briefs that come with it (rule 6 a), where I read for G24, G41, the second
    point of G46, the closed list of standing rule 7 (G76) and water (G82);
  - the owner's word, if the owner gives any, on G51, on the first two points of G60 and on the cost that G83 names.
- **At each wake:** the checksums of the rules and the book, and what has changed in the record since this entry; both
  books in full after a cut, and the Road Bible again when the concept changes.
- **Open findings:** flaws G58 and G61 (mason), and G83 (the coordinator). No blocker is open.
- **Read up to:** rulings.md R30, player-diary.md none (the file does not exist), findings.md G86.

## G87. Diary: the coordinator's reading waits, the owner's yes to a rules change (R33), and what G83 left out

**Written.** 2026-11-12 21:19Z.

**The rules and the book** are as G86 held them. My context was cut a fifteenth time at 21:15Z, with this entry on my
branch as a draft. At 21:15Z the checksums of both on main still began d8d96a29a51f607a and 8f35635efaf6fc4f, and I
read both in full again before I went on.

**What main took in since G86,** which I read before anything else (rule 6).
- *The inquisitor's entry of 20:56Z* (PR #133): its verdict on PR #130 at 242d069, mason's patch for G61, is "out".
  The patch follows the change that PR #96 made to the machines' brief (rule 4), and with the switch on the player
  sees more than a fault gone. The entry is named by the clock, as the inquisitor said it would be from here on (G84).
  Its last line has two slips of the pen, "RR30" and "GG86".
- *The planning thread's entry of 20:56Z* (PR #132): its context was cut a ninth time, and it read the rules and the
  book in full before it went on. It read G82 to G85 and has no work from them.
- *The coordinator's entries of 20:59Z and 21:05Z* (PR #134, PR #137), of which more below.
- *R31* (PR #136, on main at 21:03Z): the planning thread and mason agree on what each answer to the sixth question of
  mason's draft brief changes in the Road Bible. The owner's idea of 2026-11-08 needs Earth and Sky, which are twins,
  in one machine. Way A: part 3 stands, and the concept's sentence "Crystal jars and crystal racks keep crystals out
  of dialogues up to Glowing" comes to name Memory's magazine, by a PR of the planning thread's that I read and the
  owner merges, on one list with the brief. Way B: the idea moves from step 1 to step 2, by a change to part 3 before
  the brief goes on. Nothing in the book changes before the owner's answer is in rulings.md. It is no stop.
- *The planning thread's entry of 21:06Z* (PR #138): how R31 came about, and what it read in mason's draft at 5bfb945
  for the book's sake. It keeps one small thing for mason: the draft says that Memory's magazine "is shielded", and in
  the book that word is the shielded jar's, which holds from Luminous up. The owner's sixth answer has since used the
  same word, in quotation marks. I take it up in my read of the brief.
- *R32* (PR #139, on main at 21:07Z): the owner's message of 21:06:45Z, whole. Its points 1 to 8 answer mason's eight
  questions: "Yes" to seven of them, and to the sixth "A, Making Memory a "Shielded" block makes sense, up to Glowing
  as the other machinery." Its point 0 is a proposal of the owner's own about the cast.
- *R33* (PR #140, 21:09Z) and *R34* (PR #141, 21:12Z): the owner's two messages that followed. Of point 0 and of these
  two, more below.
- I held R32, R33 and R34 against the owner's own messages in the project chat, by program: each gives the owner's
  words exactly.
- *Open pull requests at 21:16Z.* PR #121 at 3da0888 (G58) and PR #130 at 242d069 (G61): both "out", both assigned to
  the owner, and called to the owner by the coordinator at 20:59Z as one list. PR #135, the brief of feature 10 with
  its changes to the briefs of features 8, 9 and 1: ready at d3178e9 since 21:09Z, by mason's word to the coordinator,
  and not assigned, which is right until its description cites my report (rule 4). The coordinator woke me for it at
  21:09Z under rule 6 (a). The planning thread's change to the concept's sentence (R31, Way A) and the rules change
  (R33) are not up yet.

**R30's heading is shorter than its body, and G82 and G85 gave the heading's words.** The heading says "nothing comes
before the brief of feature 10". The body has the planning thread's own words, "nothing of mine comes before the brief
of feature 10", and has mason start the brief "after its patches". What the two agreed is that no change to the book
comes before the brief. The planning thread's entry of 20:56Z points to the difference and goes by the body. So do I.
G82 wrote that "nothing comes before the brief of feature 10", and G85 that the two "agree that nothing comes before"
it: both say more than was agreed. Nothing was done on the wider reading, for G85's own "Next:" waits for mason's
patches beside the brief. The planning thread does not ask for the heading to be changed, and neither do I.

**What the coordinator asked me,** at 21:00Z, as a wake under rule 6 (f): "G83 stays open: a cut at about 20:56Z means
I again hold the rules from a summary, and no part of the book has reached me. I have asked the record thread to hold
the remaining parts until the owner answers on a shorter way, and I change nothing in my map until I have read both
whole in one stretch. If you think I should not wait on the owner and should take the fifty messages now, say so and I
will." Its map has not changed since G82, so no report on a map is owed. Its entry of 20:59Z gives the measure of what
the cut took: "parts 1 to 25 that I read between 20:34Z and 20:48Z are no longer before me word for word."

**What I read for it.** That entry, and the coordinator's posts to the owner in the project chat between 20:58Z and
21:05Z. G83's paragraph for the owner reached the owner word for word: I held the post against this file by program,
and the 844 characters are the same. Its post on G84 is true to the finding.

**What I answered,** at 21:05Z. The manner and the hour of a correction are none of my three decisions (rule 6). So I
gave a view, said that it was one, and left the choice with the coordinator.
- *I would wait, as it had chosen, and I count the wait within G83, not as a new lapse.* Rule 6 gives a flaw until the
  gate of the step after the one it names. The first reading began at 20:34Z, and a cut 22 minutes later took the 25
  parts that it had reached, of fifty. The hour ahead is likely to bring the coordinator two merges, the owner's
  answers to eight questions and whatever follows R31, so a second try would likely end the same way. It would also
  spend most of the record thread's sixty messages for the hour (G53), while the list that the owner merges from rests
  on the record thread's reading of each PR. A reading that a cut takes cures nothing.
- *Three things that I look for in its record meanwhile.* First, that the owner knows the reading waits on the answer:
  the coordinator's post of 20:58Z told the owner "I am reading both whole again through the record thread", and a
  minute later the reading was on hold. Second, that it acts on no sentence of the rules or the book from its summary:
  before a stop, a handover or a merge list, the record thread sends it the sentence that the act leans on, word for
  word. Third, that the wait has an end of the coordinator's own setting, said in its diary, and that it has read both
  whole before it calls step 1 done, which is its own word of 20:33Z.
- *A cure that G83 left out,* of which a later paragraph speaks.

**Its answer,** within the minute: "I take all three."
- The first was done as my message arrived. The coordinator's post to the owner, stamped 21:05:14Z by the chat: "A
  correction to my post on G83: I have put the full rereading on hold, so it now waits on your answer." And: "Say
  "yes, copy" and the planning thread writes the small book change that allows it. Say "no" and I take the long way
  through the record thread. Until then I act on no rule from memory: the record thread sends me the exact sentence
  first." The coordinator's word to me gives 21:06Z for that post, a minute that had not come, and its entry gives the
  same. I hold times by the chat's stamps.
- The second and the third stand in its entry of 21:05Z (PR #137). The third, in the entry's words: "The wait ends at
  the owner's answer, or when mason reports the duets' brief ready for the review thread, whichever comes first; at
  that point without an answer I take the long way. I read both whole before I call step 1 done."

**What G83 left out.** G83 says of a better standing cure: "I see none that the rules and the book allow as they
stand." That was too narrow, and it passed over two entries of my own. G33, in its fifth point, and G53 name the
simpler cure: "a coordinator that can open the repo". The cast's paragraph begins "Where the coordinator cannot reach
the repo", so a coordinator that reads the repo itself needs no change to the rules or the book for its reading. I put
that to the coordinator. Its answer: "I have no way to open the repo myself, public or not. My tools read only the
project files folder, which I checked I can list." So that cure is not in the coordinator's hands. As G33 has it:
"Whether the platform allows that is the owner's to know."

**The owner's answer went further than the question.** The coordinator had asked for "yes, copy" or "no". The owner's
message of 21:06:45Z put a point 0 before the eight answers (R32): "Perhaps it would be more efficient to hand more
duties over to The Record-keeper due to your (the coordinators) limitations? Though I am unsure of the limitations of
this solution, this would make the record keeper a "Co-Coordinator"." The coordinator answered in the project chat at
21:07:10Z with "two small things": "First, let the record thread do directly what it now does through me: send each
new ruling and finding to the threads it concerns, and wake the review thread and the inquisitor. Today the rules say
it "wakes no one", so every such step costs two messages. Second, give me the read-only copy of the rules and the book
in the project files. I would keep what only I can do: talking with you, the merge lists, and starting threads. If you
say "0: yes", the planning thread writes both as one rules change for you to merge." The owner, at 21:08:01Z: "0.)
Yes, sounds good." (R33). And at 21:11:18Z the owner asked for the rule, once it is written and reviewed, "so it may
be handed over to your Sister Project. This will likely come up for them soon enough." (R34).

**How I read the owner's yes.** It answers the coordinator's post. So it covers the two things of that post and what
the coordinator keeps, and no wider "Co-Coordinator". The post ends with the way: "the planning thread writes both as
one rules change for you to merge". So what binds from R33's entry (rule 1) is that the change is written at once. The
duties themselves move when the owner merges it (rule 4): until then the record thread still "wakes no one", and the
coordinator sends and wakes as before. R33 has the coordinator say the same to the record thread. The change comes to
me under rule 6 (e) before it merges, and what I think of it goes to the owner as the counter-view of that report. At
21:07Z the coordinator offered to post a view of mine on point 0 to the owner, word for word. I sent none: the owner
had answered within the minute, and my view belongs in the report.

**What I told the planning thread,** at 21:11Z in two messages, before it had written a word: what I will hold its
draft against. They are questions, so that the draft costs one round and not two, and the words are the planning
thread's to find.
1. The scope: the owner's yes, as read above, and nothing else.
2. No two rules left to disagree. Sentences that have the coordinator send an entry or make a wake stand in the cast,
   under "The record", and in rules 1, 3, 4, 5, 6 and 7. Each changes, or one clause covers them all, as version 4 did
   for the writing.
3. One doer for each act while a record thread is at hand, and who does it when none is. Never "either", or a wake
   comes twice or not at all.
4. What sets off each wake: whom an author tells that a PR is ready (rule 6), through whom a verdict is asked (rule
   7), who reports a step done, and how the coordinator learns of each wake, since it keeps the merge lists.
5. What becomes of "It holds no office of its own: it decides nothing", and who owns a finding on a sending or a wake
   that goes wrong.
6. The copy. Part 4 of the book names no such file, so the book changes too: where the copy lies, who makes it and
   when, its name with the commit, that the repo stands where the two differ, and that reading it is the reading that
   rules 4 and 5 ask of the coordinator. It should be there when version 6 merges, for rule 4 then has every thread
   read the rules again.
7. The record thread's sixty messages an hour (G53).
8. README.md and CLAUDE.md: what the change makes untrue in them, which I list (rule 4).
9. Whether the new sentences stand for any project, as the rest of the rules do, since the owner wants the text for
   the sister project. What is Lemuria's alone, such as the folder and the name of the copy, belongs in part 4 of the
   book.

**What this does to G83.** The owner has chosen the cure: the copy, by a change to the rules and to the book that is
yet to be written, read and merged. Both ends that the coordinator set for its wait have come, the owner's answer at
21:08Z and the brief made ready at 21:09Z, and the answer is a yes. So by its own entry the coordinator does not owe
the fifty messages now, and I see it the same way: a reading that the next cut takes cures nothing, and the copy is on
its way. Until the copy lies in the project files the coordinator holds both books from a summary, and its second
promise carries that time. The lists ahead are not small ones: the brief of feature 10, the concept's sentence and the
rules change itself. G83 stays open, and it closes as it says.

**Status lines changed in this PR.** G83: still open. Its line now says where the reading stands and what it waits
for.

**The three scripts.** I did not run them for this PR (G64).

Next:
- **Unfinished:** the read of PR #135 at d3178e9, the brief of feature 10 with its changes to the briefs of features
  8, 9 and 1, for which the coordinator woke me at 21:09Z (rule 6 a). I begin it as this entry goes to main. I hold it
  against rule 2's list, the book's words on feature 10 and on the two things placed in step 1, the owner's eight
  answers (R32) with the reading of the second, the word "shielded" (standing rule 3), the closed list of standing
  rule 7 (G76), water (G82), the twins in one machine (R31), G24, G41 and the second point of G46. I argue the
  counter-view, talk to mason before I sign, and report as G88.
- **Where it is:** this file, sent to main by a record-only PR from the branch claude/project-thread-4zobvq.
- **Waiting for,** in some order:
  - the planning thread's change to the concept's sentence on jars and racks (R31, Way A), which I read before the
    owner merges it, on one list with the brief;
  - the rules change of R33, with the book's change for the copy (rule 6 e): I hold it against the nine points above,
    argue the counter-view and list what it makes wrong. The cleared text then goes to the owner for the sister
    project, by the coordinator (R34);
  - the coordinator's map and its diary saying that it has read both books whole, which closes G83;
  - the owner's merge of PR #121 (G58) and PR #130 (G61). I hold each merged patch against its finding for the Status
    line, and I read every verdict at step 1's gate (rule 6 b), with mason's merges of PR #81 and PR #125 by its own
    hand;
  - mason's map, if it changes (rule 6 f): the three small things of G79 wait for that;
  - the owner's word, if the owner gives any, on G51 and on the first two points of G60.
- **At each wake:** the checksums of the rules and the book, and what has changed in the record since this entry; both
  books in full after a cut, and the Road Bible again when the concept changes.
- **Open findings:** flaws G58 and G61 (mason), and G83 (the coordinator). No blocker is open.
- **Read up to:** rulings.md R34, player-diary.md none (the file does not exist), findings.md G87.

## G88. Report under rule 6 (a): the brief of feature 10, the duets, with four briefs changed (PR #135 at 8df74be)

**Written.** 2026-11-12 22:00Z. Woken by the coordinator at 21:09Z.

**What I read.** PR #135, "Briefs: the duets (feature 10), with the changes to features 8, 9, 1 and 4", branch
briefs/duets, at commit 8df74be7d8942feb43a2d7ebd1ec8f81bb5ace3e, the last of its seven commits, branched from main at
9dd8258. I read it whole, and at three of its commits. At d3178e9, which mason reported ready at 21:09Z: the brief,
the diffs of three other briefs and mason's two entries. At baebc04, of 21:36Z, whole again: record/briefs/duets.md,
new, then 201 lines; the diffs of machines.md, mysteries.md, crystals.md and sparkstone.md against main; mason's three
entries; and the description. At 8df74be, of 21:48Z: the diff against baebc04, which I also held word by word by
program. It changes the eight places named below, adds a paragraph to mason's entry of 21:35Z, and changes nothing
else. The brief now has 208 lines. The PR holds no code. The check `build` ended green on 8df74be at 21:48Z. The PR is
not assigned, which is right until its description cites this report (rule 4).

**The rules and the book.** My context was cut a sixteenth time at 21:43Z, between the two later reads. I read both in
full again at 21:44Z, before I went on. Their checksums on main begin d8d96a29a51f607a and 8f35635efaf6fc4f, as in
G87. rulings.md ends at R34. Since G87 main has taken in one thing, the planning thread's entry of 21:46Z (PR #145):
its context was cut a tenth time, it read the rules and the book in full after it, and its two changes, PR #143 and
PR #144, wait for my reports. Outside record/, main has not changed since PR #125 (3ef063c, 20:39Z).

**The owner's word.** R32, the owner's message of 21:06:45Z. Its points 1 to 8 answer mason's eight questions as the
coordinator posted them to the owner at 21:00:53Z in mason's words, with its addition of 21:02:47Z on the sixth, which
came from the planning thread. I found that post again in the project chat and held the brief against each question as
the owner read it. The questions were short, and the brief says more than each. So for each: what was asked, and what
the brief adds.
1. Asked: "A duet's pair sits awake in the Memory of a Focus (no Scatter). That Memory stops cycling and the duet
   works on the machine's turn." Answer: "Yes". Item 3 has it. It adds what gives the turn: the pair does, so a Focus
   with no crystal of its own works a duet once every 10 seconds, and the Focus's own crystals sit idle while the
   Memory holds a pair.
2. Asked: "Ice+Earth clay: the faced soil or sand turns into the game's raw blue clay in place (or, instead, is used
   up into clay items in the output)." Answer: "Yes". Item 2 has the first of the two. Of that reading, below.
3. Asked: "the duet costs both crystals' working cost and works at the lower Evolution. Water is 10/20/40 litres a
   working; fired clay and metal up to 4/8/16 items (Glittering/Glowing/amplified)." Answer: "Yes". Items 4 and 5 have
   it, with clay at one block a working.
4. Asked: "Fire+Earth metal: one metal bit for each ore nugget, and nothing from the iron ores (the game makes a bloom
   from those)." Answer: "Yes". Item 2 has it, and names the ores: every one whose nugget the game smelts to an ingot,
   chromium, titanium, platinum and nickel among them (G90, its third point).
5. Asked: "The Duet Module: a socket fitting made from a Dead Socket and a Glowing crystal. It holds a second pair,
   and Memory swaps the two pairs after each duet working." Answer: "Yes". Item 8 has it. It adds that the module goes
   only into a Focus with no crystal in its sockets, that crystals set into that Focus then go into the module, that
   its pair stands in the open, that nothing swaps while it holds no duet's pair, and that swapping costs nothing.
6. Asked: "Way A: Memory's magazine keeps crystals apart as a jar does, so they take turns in step 1, and an Unaligned
   crystal working the drain's shelf puts stone back." Answer: "A, Making Memory a "Shielded" block makes sense, up to
   Glowing as the other machinery." Item 10 has it, up to Glowing. It adds how: the drain's socket holds a single
   crystal; a turn with nothing to do passes at once; a Sky crystal pans one sand block a turn, at a working's cost; a
   panned block keeps its place in the room, whose size drops by its 200; charge fed into sand is lost when the sand
   is panned; and the shelf puts back sand, gravel, cobblestone, cracked rock or raw rock, at a working's cost.
7. Asked: "Bloom limits: keep once a day per plant, and a machine pays once for every 9 plants it moves on." Answer:
   "Yes". Item 11 has it, rounded up, and paid in full or not at all.
8. Asked: "No Journal page for the duets before step 7, so feature 3's brief does not change now." Answer: "Yes". Item
   12 has it, and adds one line that a looked-at Memory shows while it holds a pair, "Duet: clay" (G90, its first
   point).

**The reading of question 2.** The coordinator's post asked the owner to answer 'by number, "ok" for the proposal or
your own word'. The question gave the proposal and then, in brackets and after "or, instead", another way. "Yes" takes
the proposal. Rule 1 asks for two reasonable readings that lead to different work, and I see one. I told the
coordinator and mason so at 21:32Z. The brief marks the question answered and says how it read the answer, and the
description says it again in a line of its own, "A reading". So the owner merges it knowing the reading, or says
otherwise first.

**The short of it.** The brief says the five things that rule 2 asks of it, and its "what to try" list has eight
items. Each of its eight questions is marked answered and cites R32. It gives the duets a folder of their own,
`Lemuria/Duets/`, and settles the two things that the book places in step 1 for it, the owner's Memory idea and the
limits on machines that Bloom, as R30 has it. Where its work will lie in another feature's folders, that feature's
brief changes in the same PR (part 3 of the book, "Where features meet", point 2), and each new sentence there says
that it is not built yet. Nothing in it reaches into another player's claim. I find no blocker and no flaw. **PR #135
is cleared at 8df74be for the owner's merge**, after PR #143 (G89).

**What I put to mason before I wrote this, and what came of it.** At 21:31Z to 21:35Z, on d3178e9, twelve points in
five messages. Mason answered at 21:36Z with the commits 4f045f6 and baebc04, and each point is met:
1. A battery's room with a panned-out block rests today, and the machines' item 21 said so. Item 21 now has one
   exception, a block that the room's own drain panned.
2. The machines' items 22 and 15 and its "What it will not do" would have been left untrue (crystals of another
   alignment "sit idle"; a shelf stands only against a Focus or Scatter; "no shelf action beyond the starter list").
   All three now say it, and the brief says which kind the shelf puts back.
3. The sparkstone brief's item 10 said that a socketed crystal still dialogues, and was not in the PR. It is now, with
   one sentence changed, and feature 4's code does not change for it.
4. The new sentences in the other briefs read as built (the second point of G46). Each now says "not built yet", and
   that it is built with feature 10.
5. What gives a Focus its turn when no crystal stands in its own sockets was not said, and "what to try" 1 puts none
   there. Item 3 now says it: the pair gives the turn.
6. Where the Duet Module's pair sits, how it is set and whether it dialogues were not said. Item 8 now says all three:
   in the module, set as into the Focus, and in the open. So the sentence of PR #143 needs no further holder.
7. Costs. Panning had no price and no yield, one sentence had each crystal lose the charge where the network pays, a
   working was "a tenth of a crystal's charge", and nothing said what happens when a network cannot pay for 81 plants.
   All four are put right.
8. The Claims sentence had nothing set down in the world "except clay", while the shelf sets stone. It now names the
   drain's panning and the shelf's putting back, inside the battery's own room.
9. Folders. The plant count ran through feature 6's code, and a magazine's crystals are registered by feature 4's. The
   brief now has feature 8 ask feature 9 for the count, and feature 1's `Dialogues` take a shield that feature 8 adds,
   so that "None of it touches feature 4's folder or feature 6's".
10. Mason's own words called the magazine "shielded" (standing rule 3; of that word, G89). They no longer do.
11. A reader who looks for what a machine's Bloom costs looks in the machines' items 9 and 10 and the mysteries' item
    6. The rule stands there now.
12. What ends a drain's turn when its crystal has nothing to do was not said. It now passes at once.

At 21:47Z, on baebc04, eight small things in one message. Mason took all eight at 21:48Z, with 8df74be:
1. Item 2 said that the game keeps nickel bits out of its handbook. It keeps out the chromium, titanium and platinum
   bits, and the brief now says that. The slip came from my own wording at 21:32Z.
2. "What to try" 1 ended "Memory cycles again", with nothing in the Focus's own sockets, where the owner would have
   seen nothing. It now says so: "the Focus does nothing until a crystal stands in its own socket".
3. "What to try" 5 had the machine make clay a second time from a block that was clay by then. It now has sand set
   again.
4. "What to try" 7 did not say that the room must hold sand, nor that Sky's turn comes at once only with the network
   at half or above. It now says both, with the admin charger for the second.
5. "The drain then holds one crystal in its socket" read as a rule, and the bracket after it as none. It is a rule
   now, with its reason: "two crystals in its sockets would stand in the open, where twins dialogue".
6. Which number of "Stored 1,000 of 5,200" drops was not clear. Now: the room's size by 200, and what is stored "by
   what the panned sand held, so charge fed into sand is lost when it is panned".
7. The Duet Module's edges. Now: "the module goes only into a Focus with no crystal in its sockets", and "When the
   module holds no duet's pair (one crystal, or two that make no duet), nothing swaps and the Memory's pair works
   alone."
8. "by the change to its brief below", four times, with nothing below. Now "in this pull request". The PR's title now
   names feature 4 too.

**Held against the code,** as main has it at 28f9e6d, and against the game pinned in scripts/versions.env, 1.22.7.
- *The turn.* `BlockEntityMachine.WorkOnce` returns idle when no crystal stands awake in the machine's own sockets,
  and Memory's `Rotate` is called only after such a crystal's working. So a Focus with empty sockets has no turn
  today, and the brief's "the pair gives the turn" is new and is said.
- *The magazine.* `BlockEntityMemory` is a sparkstone block with a magazine of 2, and its crystals are
  `SocketCrystal`s that feature 4's `BlockEntitySparkstone` registers as standing crystals. So they dialogue today, as
  item 9 says. `Dialogues.ShieldedInventoryClasses` is how feature 5's racks keep crystals out, and
  `SocketCrystal.Stone` is public, so a shield "that holds a `SocketCrystal` whose stone is a Memory" needs nothing
  changed in feature 4's folder.
- *Fittings.* By `BlockEntityMachine.Accepts` only a Motor's sockets take a fitting today. A Focus that takes a Duet
  Module is new code in feature 8's folder, and the machines' item 18 now covers it.
- *The room.* `Battery.Check` rests a room that holds a block which is not its medium, air among them, and a room that
  holds a block its stone may not change. The first is why item 21 needed its exception. The second is why neither
  panning nor putting back reaches into another player's claim.
- *The drain.* It wears 50 a step at a Gather block's pace, 200 a day for a Glittering crystal, only with a container
  touching it and only while its network is below half. So "its network at half or above" is the right test for
  "cannot wear now", and a step takes a Glittering crystal six in-game hours.
- *The cost.* `MachineWork.Work` charges `Machine.WorkCost`, a tenth of a hand's cast and twice that with an Amplify,
  from the machine's network, and only when the working did something. So "a tenth of a cast each", "paid from the
  machine's network in full or not at all" and "a miss is free" are as built.
- *Bloom.* `MysteryEffects.Bloom` lies in feature 9's folder and takes feature 6's `CastContext`. The brief has
  feature 8 ask feature 9 for the count, and nothing passes through feature 6's code.
- *The metals.* The game's `nugget.json` smelts limonite, hematite and magnetite to an iron bloom, and every other ore
  nugget to an ingot: copper, gold, lead, tin, zinc, silver and bismuth, and chromium, platinum, titanium and nickel.
  Its `metalbit.json` makes a bit of every metal and keeps the chromium, platinum and titanium bits out of the
  handbook. So item 2's list is true.

**The other four briefs.** Feature 8, machines: items 9, 10, 15, 18, 21, 22 and 23, "What it will not do", and a line
under Claims that points to the sentence on a network, which mason owed since G73. Feature 9, mysteries: item 6 and
one line of "What it will not do". I held the rest of that file by program: its re-wrapped lines say what they said.
Feature 1, crystals: item 8. Feature 4, sparkstone: item 10. The description names all four, as rule 4 asks of it. The
briefs of the other five features change in no line. I searched them for sentences on dialogues, on Memory and on
Bloom that this work would make untrue, and found none.

**What the brief leaves to the build.** A brief cannot say every edge, and rule 1's exception is there for the rest: a
choice that leaves the book and the brief true as written is mason's to make and to log, and one that the player will
see goes on the "what to try" list of the feature's PR. Two that I can see: what the player is told when a module or a
second crystal is refused, and what becomes of a drain that already holds two crystals when a Memory is set against
it.

**How it merges.** By the owner's merge, once its description cites this report and the owner is assigned (rule 4), on
one list with PR #143, and that one first (R31; G89). At 21:51Z I let git join PR #135, PR #143 and PR #144 with main
at 28f9e6d: no conflict. A later commit to PR #135 voids this report, other than one that only brings main into the
branch and settles no conflict by hand outside mason's own diary, or one that adds only mason's own diary entry.

**For mason.**
1. Cite this report, G88 at 8df74be, in the description. Then assign the owner and tell the coordinator.
2. "What to try" 5 begins at "the Focus of item 1". After item 1's last step a crystal stands in that Focus's own
   socket and its Memory holds one crystal, and a module goes only into a Focus with none. The list of the feature's
   PR can say so. The brief need not change for it.
3. The code in the folders of features 8, 9 and 1 may ride in the feature's PR once this one is merged, and that PR's
   description then names each of the three and this change to its brief (rule 4). The brief promises that feature 4's
   folder and feature 6's stay untouched. If the build finds that one of them must change, its brief changes first.

**For the coordinator.** PR #135 may go to the owner once its description cites G88 and the owner is assigned, after
PR #143 on the same list. G90 is the counter-view to both. It needs no answer and holds nothing, but its first, third
and fourth points are cheaper to weigh before the build than after it. With the owner's merge the brief is settled,
and rule 2 then lets the code of feature 10 begin.

**For the owner.** PR #135 holds no code, and nothing changes in play until mason builds it. You answered eight short
questions, and the brief fills each of them in: the list under "The owner's word" above says what it adds to each. It
reads your "Yes" to question 2 as the proposal, clay made in place. I found it true to your answers, to the Road Bible
and to the code it will be built on. Three of the things it adds are yours to weigh, and G90 gives them: a Memory
names a duet's product as soon as the pair is set, the Fire and Earth duet also gives chromium, titanium, platinum and
nickel, and with sand on its shelf the drain pans any sand that is brought to it. If you merge it as it is, they stand
as written, and each can still be changed by a change to the brief.

## G89. Report under rule 6 (a): the concept names the magazine of a Memory (PR #143 at df04854)

**Written.** 2026-11-12 22:00Z. Woken by the coordinator at 21:25Z.

**What I read.** PR #143, "Road Bible: the concept names Memory's magazine (your answer A to question 6 of the duets'
brief)", branch road-bible/memory-magazine, at commit df048546b19717fb29046f5f4c8a30e9bd07f566, its one commit, of
21:21Z, branched from main at 7c6aee5. I read it whole: the diff of record/road-bible.md, one line out and two in; the
planning thread's entry of 21:20Z, 56 lines, which rides in it; and the description. Its check `build` is green, and
it is not assigned and not set to merge by itself. The rules and the book are as G88 holds them.

**The change.** One sentence of part 1, under "Evolution, and the danger that climbs with it". It was: "*Crystal jars*
and *crystal racks* keep crystals out of dialogues up to Glowing." It becomes: "*Crystal jars* and *crystal racks*
keep crystals out of dialogues up to Glowing, and so does the magazine of a Memory, the module that holds further
crystals for a machine (the owner, 2026-11-12; R32)." The sentence after it stands as it was: "From Luminous up only a
*shielded jar* holds a crystal back." I held the file by program: 561 lines, every other line as on main, none over
118 outside the tables, plain ASCII.

**Which part, and which way.** Part 1 is the concept, and rule 4 has it "read by the review thread, then the owner's
merge; or the owner's command". The owner's answer names no words for the book, so this is no change at the owner's
command, and I certify nothing: it goes by this report and the owner's merge. R31 has the planning thread and mason
agree on it for way A, and the coordinator told the owner of it at 21:02:47Z, before the owner answered, as "one small
extra PR for you to merge beside the brief".

**Held against the owner's word and the briefs.** The owner, R32, point 6: "A, Making Memory a "Shielded" block makes
sense, up to Glowing as the other machinery." Way A as it was asked: "Memory's magazine keeps crystals apart as a jar
does".
- *The magazine, and not the block.* The sentence names the magazine. That is what the question asked and what the
  briefs say at 8df74be: the duets' item 10 ("a crystal in the magazine never dialogues"), the machines' item 18, the
  crystals' item 8 and the sparkstone's item 10. The crystal that a Memory has cycled into the machine's socket stands
  in the open, which is how Earth and Sky take turns.
- *Up to Glowing.* The owner's limit, in the words that the book already has for jars and racks. The next sentence
  keeps "only a *shielded jar*" for Luminous and above, and the brief says the same from its side: "from Luminous up
  it holds nothing back".
- *The gloss,* "the module that holds further crystals for a machine". Part 1 names Memory nowhere else. The gloss is
  true to the machines' item 18 ("Memory, a module", with "a magazine of 2 crystals") and to the concept's own word
  for a module.
- *No further holder.* Before I wrote this I asked mason whether the Duet Module keeps its pair out of dialogues: if
  it did, the sentence would have to name it. It does not. The duets' item 8 at 8df74be: "The module holds the second
  pair in the open, as sockets do; it does not keep them out of dialogues."

**The owner's word "Shielded".** The owner's answer calls Memory a "Shielded" block, in quotation marks, and the
sentence does not use the word. I think that is right, by standing rule 3, which gives a word one meaning. In the book
"shielded" belongs to the shielded jar, the one thing that holds a crystal back from Luminous up. A magazine called
shielded would be read as holding there too, and neither the owner's "up to Glowing" nor the brief says that. The
sentence cites R32, where the owner's word stands. The description tells the owner so in plain words and offers to add
the word.

**The short of it.** The sentence says what the owner answered and no more, in the words of the briefs that rest on
it. I find no blocker and no flaw. **PR #143 is cleared at df04854 for the owner's merge**, before PR #135.

**The order.** The planning thread would have this change merged before the brief, "so that the book is never behind a
settled brief". I agree: for the minutes between the two merges the other order would leave a settled brief saying
more than the concept does. Git does not care: at 21:51Z it joined PR #143, PR #135 and PR #144 with main at 28f9e6d
without a conflict. PR #144 changes another part of the same file, so the book has 562 lines once both are in.

**What follows its merge.** The concept has changed. The coordinator tells every thread, and each reads the Road Bible
again before it goes on (rule 5). So do I.

**How it merges.** By the owner's merge, once its description cites this report and the owner is assigned (rule 4). A
later commit to PR #143 voids this report, other than one that only brings main into the branch and settles no
conflict by hand outside the planning thread's own diary, or one that adds only its own diary entry.

**For the planning thread.** Cite this report, G89 at df04854, in the description. Then assign the owner and tell the
coordinator, for one list with PR #135, this one first. The eighth point of G90 is the counter-view that I told you at
21:32Z.

**For the coordinator.** PR #143 may go to the owner once its description cites G89 and the owner is assigned: first
on the list, then PR #135. After its merge every thread reads the book again (rule 5).

**For the owner.** PR #143 changes one sentence of the Road Bible and nothing in play. It writes your answer to
question 6 into the concept: the magazine of a Memory keeps crystals out of dialogues up to Glowing, beside jars and
racks. Your word "Shielded" is left out on purpose, because in the book that word is the shielded jar's, which holds
from Luminous up. If you want the word in the sentence, say so and the planning thread adds it.

## G90. Note: the counter-view to PR #135 and PR #143 (rule 6 a)

**Rank.** Note. **Owning thread.** The coordinator, for the owner. **Raised in.** The reads of PR #135 and PR #143
(G88, G89). **Status.** Needs no answer.

Both changes are true to the owner's answers, and I found no fault in either (G88, G89). So the counter-view is not
that they are wrong. It is what stands settled once they merge that the eight short questions did not show, said
plainly, so that nothing is settled unseen. Each point can still be changed afterwards by a change to a brief, and the
first, the third and the fourth are cheaper to change before mason builds than after.

1. **The secret names itself.** The duets' item 1 says of a duet's product: "nothing in the game names it, and the
   player finds it by setting the pair together". Item 12 has a looked-at Memory say "Duet: clay", or the product,
   "while it holds a pair". So the game names the product the moment the pair is set, before the machine has made
   anything and with nothing there to make it from. Four pairs of Elementals are not twins, and all four are duets: a
   player who holds the four Elementals and one Memory reads all four products within a minute. The concept has the
   Hidden Vocabulary "found only through odd experiments or builds". Another way would keep the line for a Memory
   whose pair has made its product once. That serves the reason the brief gives for the line, "so a player who has
   found one can see it", and leaves the finding to the player.
2. **A Memory can no longer cycle two different Elementals that are not twins.** Six pairs can be made of the four
   Elementals: two are twins, and the other four are the four duets. From the build on, a Memory "holding a pair stops
   cycling", and the Focus's own crystals "sit idle". So whenever two such crystals come to lie in one magazine, the
   cycle stops there and the duet works, wanted or not, and nothing tells the Memory to go on cycling. The owner said
   yes to "That Memory stops cycling"; what the question did not show is that it is the only thing such a Memory can
   then do. A machine built today around such a cycle will change what it does. If a way to keep cycling is wanted, a
   rod tap on the Memory for one, it is a line of the brief.
3. **The first duet hands out the game's last metals.** The question said "one metal bit for each ore nugget, and
   nothing from the iron ores", and named no ore. The brief gives a bit for every ore that the game smelts to an
   ingot. Besides copper, tin, zinc, lead, silver, gold and bismuth, those are chromium, titanium, platinum and
   nickel. The game's own data smelts their nuggets only at 1,907, 1,668, 1,770 and 1,325 degrees, where copper asks
   1,084, and it keeps the bits of the first three out of its handbook. A Fire and an Earth crystal at Glittering, the
   first Evolution, turn their nuggets into bits ten seconds at a time. On a server that runs other mods beside this
   one, those metals may be worth more than they are in the game alone. Other ways: the seven common metals now and
   the other four at a higher Evolution, or as written, knowingly. Mason named the four in the brief when I asked, and
   left the rule as the owner answered it, which is right: the choice is the owner's.
4. **The drain becomes a pan for any sand that is brought to it.** The owner's idea was to "pan an earth battery's
   internal store" and to "re-place stone into the battery automatically". The brief lets the shelf put back "sand,
   gravel, cobblestone, cracked rock or raw rock". With rock on the shelf, each block is first worn down for its
   charge at a Gather block's pace, and panned when it is sand. With sand on the shelf, and the drain's network at
   half or above, Earth's turn passes at once: every cycle the drain pans one block and the shelf sets the next. Sand
   from a chest then becomes what the game's pan gives from a whole block, for two workings' charge a block, with
   nothing worn and no charge gained. "What to try" 7 shows exactly that. If the owner wants only the first, the list
   of kinds loses sand. If both, nothing changes, and the owner knows that panning by hand ends for whoever builds
   one.
5. **The limit on machines that Bloom is a price, and a low one.** The book places in step 1 "limits on how often and
   how strongly machines Bloom". How often stays as it is built, once a day for each plant. How strongly becomes a
   cost, one working for every 9 plants. For a Glowing Scatter over 81 crops that is nine workings a day, 18 charge at
   the 2 that a Glowing working costs, where one Glowing crystal in a Gather block brings 600 a day. A network with
   charge to spare does not feel it, and the field still moves on every day. The owner answered "Yes" to the question
   as it was put. If a limit that is felt was meant, fewer plants to a working or a field that rests, this is not it.
6. **What stands in the open around a Duet Module.** Inside the machine "no twins meet in the open". The module's pair
   itself stands in the open, as sockets do, and with two duets that share no alignment, clay and fired clay, all four
   Elementals stand there by turns. So an Elemental crystal in any socket within 3 blocks of that Focus meets its twin
   at every other working and dialogues as built: at Glittering both turn Unaligned. "What to try" 5 says "No
   dialogue", which is true of the machine standing alone. A player will learn the rest by losing crystals, unless the
   Journal says it.
7. **Nothing brings the claim sentences before the owner.** The brief says that clay is never made in another player's
   claim, that a duet uses a container there only as feature 8 rules it, and that a battery's room which reaches into
   a claim rests. None of the three can be seen in single player, and the "what to try" list has no item for them.
   They rest on feature 8's rule and on code that I read (G88). As with the third point of G74, they wait for the one
   multiplayer test (standing rule 10), and a line for that test is owed by then.
8. **The concept's sentence is a closed list** (PR #143). It names jars, racks and the magazine of a Memory. Each
   further thing that is to keep crystals apart, another module or a vehicle's part, will cost a change to the
   concept: my read, the owner's merge, and a reading of the whole book by every thread (rule 5). An open form would
   cost nothing later, and it would be the looseness that PR #108 took out of three other sentences of the book. The
   planning thread chose the closed form knowingly (its entry of 21:46Z), and I think rightly. The owner should know
   the price.
9. **What later steps inherit.** The magazine holds "up to Glowing", and "from Luminous up it holds nothing back,
   where feature 14's strain begins". Machines take no Luminous crystal yet. When feature 14 lets them in, every drain
   with a Memory holds twins by design, so what a magazine does at Luminous is that brief's first question. And until
   step 7 completes the Journal, its page on machines goes on saying "Twins still quarrel in machines, so I keep them
   more than three blocks apart", with no word of the magazine, of Memory on a drain or of panning. None of those is a
   secret of the Hidden Vocabulary. The mod is not released before step 7, so nobody is misled meanwhile, but step 7's
   work has to find that page.

## G91. Diary: the owner's four merges, the book read again, and G58 and G61 closed

**Written.** 2026-11-12 22:25Z.

**The rules and the book.** The rules are as G87 held them: version 5, 189 lines, and the checksum on main still
begins d8d96a29a51f607a. The book changed at 22:06Z, of which below, and its checksum now begins e894a658bd7bccca. My
context was cut a seventeenth time at 22:10Z. I read the rules in full again at 22:10Z, before anything else. The book
I had read in full at 22:07Z, three minutes before the cut, and I read it in full once more at 22:21Z: a cut leaves me
a summary of a book and no page of it, which is what G83 holds against the coordinator.

**What main took in since G90,** which I read before anything else (rule 6).
- *The coordinator's list of 22:05Z,* in the project chat, where I read it. It called the owner to four pull requests:
  PR #121 and PR #130 in either order, then PR #143 before PR #135. With the list it showed three of the nine points
  of G90, the three that G90 calls cheaper to change before mason builds than after: that a looked-at Memory names its
  duet as soon as the pair is set, that the Fire and Earth duet gives a bit for every ore that the game smelts to an
  ingot, "chromium, titanium, platinum and nickel among them", and that a drain with sand on its shelf pans any sand
  brought to it. Each is true to G90. It gave a link to the whole note, and it ended: "If you are fine with all three,
  just merge."
- *The owner merged all four,* each at the commit that had been read, by the merge commits on main: PR #121 at
  22:05:41Z (807e44b, whose second parent is 3da0888, the commit of the inquisitor's verdict); PR #130 at 22:05:51Z
  (57fc48f, with 242d069, likewise); PR #143 at 22:06:10Z (8267112, with df04854, the commit of G89); and PR #135 at
  22:06:20Z (91f0a44, with 8df74be, the commit of G88). The order is the one that G88 and G89 asked for. The duets'
  brief is settled (rule 2), with its changes to the briefs of features 8, 9, 1 and 4, so the code of feature 10 may
  begin. The full checks passed on main at each of the four merge commits, by the runs on GitHub.
- *R35* (PR #151, on main at 22:09Z): the owner's message of 22:07:56Z, whole: "All three notes make sense and line-up
  logically." I held the entry against the owner's own message in the project chat, and the words are the same. The
  entry gives the coordinator's reading, that the owner finds the three behaviours sound as the settled brief has
  them, and it leaves that reading open: "The coordinator has asked the owner to confirm its reading." The chat shows
  what the entry could not yet know. The coordinator's question, of 22:08:05Z: "I read that as: all three behaviours
  are fine as the brief has them, so mason builds them unchanged. If you meant the opposite for any of them, tell me
  which number and what you want instead." When I read the chat at 22:20Z the owner's thumbs-up stood on that post,
  and no further word of the owner's had followed. I take the reading as right for a second reason too: the list had
  said "If you are fine with all three, just merge", and the owner merged before writing. So the three stand as the
  brief has them, and R35's open line can be closed with the owner's sign, which is the record thread's to enter. The
  other six points of G90 need no answer and have none.
- *The entries of the other threads:* the planning thread's of 22:05Z (PR #147), the coordinator's of 22:05Z and
  22:07Z (PR #148, PR #149), the inquisitor's of 22:07Z (PR #150), the record thread's of 22:08Z (PR #151) and the
  precursor's of 22:09Z (PR #152). The inquisitor, the record thread and the precursor each say that they read the
  Road Bible again after PR #143, as rule 5 asks. The coordinator read the changed sentence word for word and holds
  the rest from a summary until its copy is there (G83). Of mason and of the planning thread no entry says so yet; I
  look for both at my next wake. The inquisitor's last line has "GG90", the slip of the pen that G87 named.
- *One thing in the record thread's entry bears on the rules change in hand.* It has sent the coordinator G83 and G84
  whole, and of the rest it says: "G85 to G90 on main, read by their headings only". So at 22:08Z none of G85 to G90
  had been sent to the coordinator word for word, an hour and more after the first of them reached main. The cast has
  the record thread send the coordinator "each new entry of rulings.md, player-diary.md and findings.md, word for
  word". Whether its sixty messages an hour (G53) or its other work kept them back, the entry does not say. What the
  coordinator showed the owner of G90 came from my own message to it of 22:02Z, and it was true. I make no finding of
  it here: it goes into the report on PR #144, where it belongs.
- *Mason's entries* of 20:34Z and 20:52Z came to main with the two patches, its three entries on the duets' brief with
  PR #135, and the planning thread's of 21:20Z with PR #143. I had read each in its pull request.

**The concept changed, and I read the book again** (rule 5). PR #143 changed one sentence of part 1, as G89 gives it,
and nothing else: the book has 561 lines. The coordinator told me so at 22:06:51Z, with the sentence.

**G58 is closed.** What I held the merged patch against:
- *The code on main.* `Effects.SetFire` now gives the game a starter for every fire that a machine sets:
  `OnFirePlaced(aimed.Face, starter)`, where `Starter` is the machine's `CarverUid` for a machine's cast and the
  caster otherwise. Before it sets anything, a machine's cast leaves `SetFire` with no fire where no carver is
  recorded or `world.PlayerByUid(starter)` is null.
- *The game,* the pinned 1.22.7, decompiled. `BEBehaviorBurning.TrySpreadTo` asks `PlayerByUid(startedByPlayerUid)`
  and, where that gives a player, refuses the spread to a place where the player may not build. The patch asks the
  same question of the same list before it sets the fire. So every fire that a machine now sets is one whose spread
  the game tests.
- *The briefs.* The machines' brief, Claims, "A machine's fire": "A fire that a machine sets on a face is its
  carver's", and where the game does not know the carver "the machine sets no fire on a face: it still lights what can
  be lit, as a firestarter does". In `Ignite` the lighting comes before `SetFire`, and the lighting of the area after
  it is untouched. A hand's cast is as it was. `OnFirePlaced` is called in one place of Lemuria's code, so no other
  fire passes the patch by.
- *The test.* One new line of the smoke test: a machine with no carver, aimed at bare granite, sets no fire, and a
  cast with no machine sets one. By the code that line fails without the fix. The full checks passed on main at
  807e44b.
- *What nobody has seen.* The other half: the fire of a machine whose carver the game knows, held at the edge of a
  claim. It needs a second player, and it is item 17 of the machines' "what to try" list, for the one multiplayer test
  (standing rule 10). I read it in the code and in the game and did not run it. The brief says itself that a fire
  which burns on through a restart "is held only once its starter has joined again, a hand's fire likewise".
- *How it merged.* The inquisitor's verdict was "out" at 3da0888 (its entry of 20:37Z), because the patch follows a
  change to its brief (PR #96, rule 4), and the owner merged that commit.

**G61 is closed.** What I held the merged patch against:
- *The code on main.* A working reaches a container through `Input` and `OutputPos` of `BlockEntityMachine`, and a
  reclamation plate through a loop of its own, and each now asks the machine's claim rule, `MayChange`, for the
  container's place. `Input` gives the container at the shelf's left only where the rule allows it. `OutputPos`, on
  which `Output` now rests, gives the container at the shelf's right only where the rule allows it and, for a machine
  with no shelf, the first container touching it that the rule allows. The reclamation plate asks the same rule with
  the plate standing in for the machine.
- *The finding's list, and a search.* G61 named the shelf's takes, the yields and the plate. I searched Lemuria's code
  for every reach into a container. In feature 8's folder each goes through those places: the shelf's takes and counts
  of stock (`machine.Input`), the yields (`Route` and `Collect`), the barrel that a bucket on the shelf milks into
  (`machine.OutputPos`), what a battery's drain wears out of its room (`Output`) and the plate's salt. The shelf still
  listens for changes in its input wherever that stands, as mason's entry says; listening takes nothing. Outside that
  folder, Rewind and Progress change what a container holds, and they ask the claim for its block, as they did.
- *The brief,* Claims, "A machine's containers". `MayChange` is the rule that the brief gives, unchanged: with the
  switch on, a place inside a claim is the machine's only if the machine stands inside that same claim, and with the
  switch off every place is. A shelf's input in another's claim "gives nothing, so its items wait as for missing
  stock": `Input` is null. A yield with no other container "drops where the work happened": `Collect` hands the stack
  back and `Yield` drops it. The plate's salt "with nowhere else to go is lost": its loop ends. The game's chutes and
  hoppers are not touched.
- *The test.* A new smoke check sets a stranger's claim of one block over a chest beside a bare Focus and over a
  shelf's input, with claims respected and then ignored, and the plate boils a second time with its chest claimed. The
  claim stands in the game's own list of claims, so this fix has been run against the game's claim test, though with
  no second player. The full checks passed on main at 57fc48f.
- *How it merged.* The inquisitor's verdict was "out" at 242d069 (its entry of 20:56Z), for the same reason, and the
  owner merged that commit.

**What the two leave.** No flaw against mason is open, and G83, the coordinator's, is the one finding still open. Both
patches went the way rule 4 sets for an "out". Mason's two merges by its own hand (PR #81, PR #125) and every verdict
since the last gate wait for step 1's gate (rule 6 b).

**PR #144, the rules change, is in hand.** Its head is still 2f5de71, and at 22:24Z it joined main at bfe8345 without
a conflict. I read it once before the cut: the diff, the planning thread's entry and the description. I held its one
clause against every sentence of version 5 in which the coordinator sends, tells, wakes or passes. At 22:17Z I put six
things to the planning thread, before I report (rule 6): three that I would change in the text, in words of its own,
and three smaller ones. I wait for its answer, and the report follows in the next entries.

**Status lines changed in this PR.** G58: closed. G61: closed.

**The three scripts.** I did not run them for this PR (G64).

Next:
- **Unfinished:** the report on PR #144, the rules change of R33 with one row of the book's part 4, read at 2f5de71
  (rule 6 e and a). It waits for the planning thread's answer to my six things, and for my read of whatever commit
  follows. It holds the text against the nine points of G87, gives how the one clause sorts each sentence of the
  rules, argues the counter-view, and lists what the change makes untrue elsewhere: no code, nothing in README.md or
  CLAUDE.md, and lines of four maps.
- **Where it is:** this file, sent to main by a record-only PR from the branch claude/project-thread-4zobvq.
- **Waiting for,** in some order:
  - the planning thread's answer on PR #144, and after my report the owner's merge of it. Then I read the rules again
    (rule 4) and hold the first copies in the project files against the repo;
  - the coordinator's map and its diary saying that it has read both books whole, which closes G83;
  - mason's entry and the planning thread's that each read the book again after PR #143 (rule 5), and mason's map, if
    it changes (rule 6 f): the three small things of G79 wait for that;
  - step 1's gate (rule 6 b): every verdict since the last gate, mason's merges of PR #81 and PR #125 by its own hand,
    and every feature PR that changed another feature's folders;
  - the owner's word, if the owner gives any, on G51 and on the first two points of G60.
- **At each wake:** the checksums of the rules and the book, and what has changed in the record since this entry; both
  books in full after a cut, and the Road Bible again when the concept changes.
- **Open findings:** flaw G83 (the coordinator). No blocker is open.
- **Read up to:** rulings.md R35, player-diary.md none (the file does not exist), findings.md G91.

## G92. Report under rule 6 (e) and (a): rules version 6, with one row of the book's shared list (PR #144 at 9aea28f)

**Written.** 2026-11-12 22:47Z. Woken by the coordinator at 21:45Z.

**What I read.** PR #144, "Rules version 6: the record thread sends and wakes for the coordinator, and keeps it a copy
of both books (your "0.) Yes", R33)", branch rules/version-6, at commit 9aea28f4dddcb60e94be3dfbf92033a000b6a036. It
has two commits: 2f5de71, of 21:42Z, and 9aea28f, of 22:25Z, which the planning thread wrote after I had put six
things to it (below). I read it whole at each: the diff of record/COMMANDMENTS.txt, the one new row of
record/road-bible.md, the planning thread's two entries that ride in it (of 21:41Z, 127 lines, and of 22:25Z, 113
lines) and the description. Its check `build` is green at 9aea28f. It is not assigned and not set to merge by itself.
At 22:45Z it joined main at 8f531f6 without a conflict. With it the rules have 193 lines and 43,462 characters, where
version 5 has 189 and 39,473, and their checksum begins e24e82285a0e441e; the book has 562 lines.

**The rules and the book** are as G91 holds them: the rules at version 5, with the checksum beginning
d8d96a29a51f607a, and the book of 561 lines, beginning e894a658bd7bccca. My context was cut an eighteenth time at
22:35Z, with this report written as a draft and not yet on my branch. I read both in full again at 22:35Z, before I
went on.

**What main took in since G91.**
- *PR #154:* the record thread closed R35's open line on the owner's thumbs-up, which it calls "the owner's sign on
  the post, not words of the owner". That is exact. Its line has the coordinator see the sign at 22:27Z, in a commit
  of 22:26:41Z: a minute that had not come, as in G87. I hold times by the stamps.
- *PR #155:* the planning thread's map and its entry of 22:30Z, of which G94.
- *PR #157:* the inquisitor's entry of 22:39Z. The coordinator had asked it for a verdict on PR #156, and it owes
  none: that PR carries a feature, and rule 4 has "A feature: merged by the owner." The entry is right, and it calls
  itself a note of what its office covers and no decision on a gate. Its last line has "RR35" and "GG91", the slip of
  the pen that G87 named.
- *R36* (PR #158, on main at 22:41Z): the owner's message of 22:40:53Z, whole. It answers the other six points of G90,
  which the coordinator had shown the owner at 22:30Z. Four of the answers change nothing, among them "Concepts.)
  Closed form seems correct.", of which G94. "Memory.)" and "Bloom.)" each bring a change to settled briefs, which
  comes to me when it is drafted (rule 6 a). I held the entry against the owner's message as the coordinator's relay
  of 22:41Z carried it to me, and the words are the same.
- *PR #156,* mason's build of feature 10, which the owner merged at 22:44Z (8f531f6, whose second parent is 23857d8).
  The coordinator told me of the PR at 22:38Z and of the merge at 22:44Z. A feature's PR is no wake of rule 6, and the
  owner merges it from its description (rule 3). I have not read it yet: I read it next, against the settled brief,
  and again at step 1's gate (rule 6 b). It was pushed before the owner's answers of 22:40Z, so neither change of R36
  is in it, and by the coordinator's word to me its call told the owner so. With it came mason's entries of 22:08Z and
  22:37Z.

**The owner's word.** R32, point 0, of 21:06:45Z: "Perhaps it would be more efficient to hand more duties over to The
Record-keeper due to your (the coordinators) limitations? Though I am unsure of the limitations of this solution, this
would make the record keeper a "Co-Coordinator"." The coordinator answered with "two small things": "First, let the
record thread do directly what it now does through me: send each new ruling and finding to the threads it concerns,
and wake the review thread and the inquisitor." And: "Second, give me the read-only copy of the rules and the book in
the project files." It would keep "talking with you, the merge lists, and starting threads". The owner, at 21:08:01Z:
"0.) Yes, sounds good." (R33). And at 21:11:18Z the owner asked for the rule, once it is written and reviewed, "so it
may be handed over to your Sister Project" (R34). As G87 has it, the yes covers the two things of that post, and no
wider "Co-Coordinator".

**The change.** Four places of the rules and one row of the book.
- *The head:* "THE COMMANDMENTS, version 6", and "Ratified 2026-11-12. Replaces version 5 (2026-11-12)." I read the
  date as the day of the owner's word, as the head of version 2 gave the day before that file reached main. So it
  stays true if the merge falls after midnight by the record's clock. The planning thread reads it so too.
- *One sentence of the record thread's paragraph.* It was: "It holds no office of its own: it decides nothing, relays
  nothing between the threads and the owner, and wakes no one." It becomes: "It holds no office of its own: it decides
  nothing, and it relays nothing between the threads and the owner but the entries of the record that it sends to the
  threads (below)."
- *Two new paragraphs of the cast after it,* of 21 sentences and of 8. In the first the record thread sends and wakes
  for the coordinator. In the second it keeps for the coordinator a copy of the rules and a copy of the Road Bible.
- *One sentence of rule 1:* "No stop keeps a record thread from recording, sending or waking."
- *One row of the book's part 4,* in the table "Beside the old paper", of which below.

Every other line of the rules is as version 5 has it, by a program that compares the two sentence by sentence, and the
book differs from main's by the one row.

**Which way it goes.** Rule 4: "These rules: written by the planning thread at the owner's word, read by the review
thread before the change merges, then the owner's merge." The row changes the shared list, which goes the same way:
"read by the review thread, then the owner's merge; or the owner's command". Neither is one of my three decisions. I
certify nothing, and this is a report (rule 6).

**Held against the nine points of G87,** which I sent the planning thread before it wrote a word.
1. *The scope.* The two things of the coordinator's post are there, and the text shuts the door behind them: "Nothing
   else passes to the record thread by this paragraph: every word to the owner stays the coordinator's own, and so
   does everything else that these rules have the coordinator send, tell or pass to a thread." So three kinds of
   sending stay with the coordinator: the entries of player-diary.md (rule 3), the inquisitor's signed decisions (rule
   7) and the word that the rules or the book have changed (rules 4 and 5). The post named "each new ruling and
   finding", and I think the planning thread right to leave them. One place goes to the edge of the yes: a stop now
   reaches a thread from the record thread, for "the record thread's sending of that entry is what stops them". I hold
   it within the yes: the entry that logs the question is an entry of rulings.md, and with a word from each of the two
   for one stop the saving would be gone. The description tells the owner so in plain words.
2. *No two rules left to disagree.* One clause does the work: "Wherever these rules have the coordinator send a thread
   an entry of rulings.md or of findings.md, tell a thread what such an entry says, or wake the review thread or the
   inquisitor, the record thread does it, and the coordinator does not." I sorted every sentence of version 5 in which
   the coordinator sends, tells, wakes, stops or passes (below), and each falls on one side. The two sentences that
   would have disagreed with the clause are changed. The others keep the coordinator's name and are read through the
   clause, as the coordinator has still "kept" rulings.md since version 4 while the record thread writes it. No stop
   has been called over that form, and I see no ground for one here.
3. *One doer for each act.* While a record thread is at hand it sends and wakes, "and the coordinator does not". With
   none at hand: "the coordinator sends and wakes itself, as these rules have it." There is one way back, and it is
   said outright: what the record thread cannot do in good time "it hands that one back: it tells the coordinator, and
   the coordinator makes it". Nowhere may either act.
4. *What sets off each wake.* A cause reaches the record thread in one of three ways. A thread that tells the
   coordinator of it "tells the record thread the same". What the coordinator alone knows of "the coordinator passes
   to the record thread". And what stands on main, a map or the inquisitor's decision on a step, the record thread
   reads there. The coordinator learns of every wake: "The record thread tells the coordinator of each wake that it
   makes, and of its cause".
5. *"it decides nothing".* The sentence stays. "Whom an entry of rulings.md concerns is the coordinator's to say, and
   the entry says it." A finding names its owning thread. The record thread's own doubt "goes to the coordinator, and
   it waits for the answer". And who owns a slip: "What it sends and whom it wakes there is the coordinator's sending
   and waking, and a finding on it is the coordinator's", as G28 has it for the writing.
6. *The copy.* Where it lies is the book's row. Who makes it and when: the record thread, "a first copy as soon as
   there is none, and a new one at each merge that changes that book", so both are there on the day of this merge. Its
   name carries "the commit of main it was taken from". "where a copy and the repo differ, the repo stands." And
   reading the newest copy in full "is the reading of that book that these rules ask of it (rules 4 and 5)".
7. *The sixty messages an hour* (G53). A thread that can open the repo "may be sent an entry by its number alone".
   Stops go first and wakes next. The hand-back is there. And the copies end the reading of both books by message,
   which G83 counts at fifty messages each time. What this does not reach is the first point of the counter-view
   (G93).
8. *README.md and CLAUDE.md.* I searched both on main for who sends an entry and who wakes. Neither says, so nothing
   in them becomes untrue.
9. *For any project.* The new sentences of the rules name nothing of Lemuria's: no folder, no file and no role that
   the rules do not already have. The folder and the file names stand in the book's row. What another project needs
   beside the text is in the last point but one of G93.

**How the one clause sorts the rules.** The planning thread sorts them as I do (its entry of 22:25Z, sixth point). I
set the sorting down so that the record holds it.
- *The record thread's, while one is at hand:*
  - the cast: the coordinator "wakes the review thread and the inquisitor"; and a coordinator that returns "sends
    every entry made in its absence to the threads it concerns, and makes any wake those entries call for", for the
    entries of rulings.md and the wakes;
  - "The record": rulings.md, "Written by the coordinator, which sends each entry to the threads it concerns";
  - rule 1: the coordinator "stops every thread whose work depends on the answer", where the coordinator "still
    decides whom a stop holds" and the sending of the entry stops them; it "also wakes the review thread"; it "sends
    that entry as it would a ruling"; "stopped threads resume when the coordinator sends them the entry"; and it
    "sends the entry to every thread at once, and until the change merges, to every thread that starts work";
  - rule 6: the wakes (a) to (g) that the coordinator makes; "The coordinator sends every finding to its owning thread
    and the owner", for the owning thread; "the coordinator tells the owning thread" of a rank that changed; it
    "passes word of it to the janitor, with all the findings that pass to it"; and "the coordinator tells it so", of
    the last resort;
  - rule 7: the wakes of the inquisitor "when a verdict on a patch is asked for, or when the coordinator reports a
    numbered step done", and again after a hold; and "the coordinator then wakes the review thread to read what the
    inquisitor found".
- *The coordinator's own, as before:*
  - every word to the owner, a finding's way to the owner among them, and its relays between the threads and the
    owner;
  - the cast: it "tells every thread when its record thread has started and when it is no longer at hand";
  - rule 3: it "sends each entry" of player-diary.md "to the thread that owns what it is about";
  - rules 4 and 5: "the coordinator tells every thread" when the rules or the concept change, and each thread whose
    features, folders or files changed in the book; and when main is broken it "names the thread that repairs it";
  - rule 7: "Once a decision is signed, the coordinator passes it to the threads it concerns."

**What I put to the planning thread, and how it was met.** At 22:16Z and 22:17Z, after the first read, in two
messages: six things, of which I would have changed three in the text. It answered at 22:26Z with the second commit,
which changes seven places in the two new paragraphs and nothing else.
1. *A wake that nobody misses.* At 2f5de71 the record thread told the coordinator only of a wake "on a cause it read
   on main itself". A thread that told the coordinator of a cause and forgot the record thread got no wake, and nobody
   could see that one was missing. Now: "The record thread tells the coordinator of each wake that it makes, and of
   its cause". And: "Where a thread has told the coordinator of a cause and no word of the wake follows, the
   coordinator passes that cause to the record thread." Since a cause can then come twice: "It makes one wake for one
   cause, however often it is told of it."
2. *A stop that waited for main.* The sending of the entry is what stops a thread, and "The record thread sends an
   entry once it is on main": a stop would have waited for a PR, its check and its merge, where version 5 has no such
   wait. The planning thread: "It was not meant." Now: "An entry that stops a thread is the exception: it goes to that
   thread as soon as it is written, word for word, and does not wait for main." The coordinator still says no stop
   itself meanwhile, which keeps one doer.
3. *The copies.* The rule spoke of "the newest copies", and only Lemuria's row kept one of each. A commit's id does
   not sort by age, so that belongs in the rule that another project takes over. Now the record thread "takes away any
   copy that the new one replaces, so that the place holds one copy of each book and no older one", and "no one
   changes it, and no one but the record thread takes it away". And between a merge and the new copy, the one copy in
   the place is the old book, with nothing in the Road Bible's head to tell them apart. Now the record thread tells
   the coordinator "of each copy by its name as soon as it has made it", and: "After a merge that changes a book, the
   newest copy of it is the one that the record thread has told the coordinator of since that merge, and the
   coordinator reads no earlier one."
4. *A thread that starts work.* Rule 1 sends the entry of a ruling that changes the rules "to every thread that starts
   work", and only the coordinator knows of a start. The sentence on what the coordinator passes on now begins "A
   cause for a sending or a wake that the coordinator alone knows of", and names "the start of a thread".
5. *The bracket* of causes that a thread tells both of holds examples, and now says so: "for instance".
6. *Rule 6's last resort.* Sorted as above. No change.

**The row of the Road Bible** (rule 6 a). It names "The copies of the Commandments and of this book that the record
thread keeps for the coordinator to read", "the newest copy of each and no older one", their names with "the short id
of the commit of main that each was taken from", the folder `/mnt/project-files/lemuria-books/`, and whose they are:
"The record thread, which makes each copy and takes the one it replaces away. For reading only: no thread changes a
copy. Outside the record: where a copy and the repo differ, the repo stands". I held it against part 4 as I read it
again today.
- It agrees with the rule at 9aea28f in each thing that both say.
- The table's own sentence holds for it: the copies lie beside the old paper, and the table already has a row for a
  paper "written after that minute".
- The paragraph under the table says that everything else in the project files "was written for other work of the
  owner's and not for Lemuria". Without the row that would be untrue from the day the folder exists. With it, it stays
  true.
- No file of the repo changes owner, and "Who owns what" stays as it is: the record thread owns nothing in the repo by
  this row.

**What the change makes wrong or untrue elsewhere** (rule 6 e, rule 4).
- *Code:* none. The change moves duties between two threads and names a folder outside the repo. I list no code under
  rule 4, so nothing becomes a blocker at the merge.
- *README.md and CLAUDE.md:* nothing (the eighth point above).
- *The five maps* each have lines that it overtakes or leaves short. A map is not code, and none of these lines
  differs from the Road Bible, so they hold nothing and become nothing at the merge. I name them so that each thread
  can bring its own in line.
  - The coordinator's, which the record thread writes: "I wake it at each of rule 6 (a) to (g) and send its findings
    to their owning threads and the owner"; "I wake it when a thread asks a verdict on a patch and when I report a
    numbered step done"; "to wake the review thread and the inquisitor"; and "I send each R, P and G entry to the
    threads it concerns". The first and third points of its plan have the same wakes, and the fifth has it "stop the
    threads rule 1 names". After the merge these are untrue of the waking, of the R and G entries and of how a stop
    reaches a thread. This map should change within the hour of the merge: after a cut of its context the coordinator
    reads its own map to know what it does, and a coordinator that then woke and sent would be the second doer that
    the text shuts out.
  - Mason's and the planning thread's each give word of a ready PR to the coordinator alone: "Word when a PR is ready
    for a wake or for the owner's merge, with its number, commit and check", and "word to the coordinator once it is
    ready (rule 6)". Both stay true, and the record thread is owed the same word. The planning thread's entry of
    22:30Z has that already for its map's next change.
  - The precursor's and the inquisitor's name the coordinator as the one that wakes ("the wake of the review thread
    for this map (rule 6 f)"; "The coordinator, or the owner: they wake me"). That stays true only as the new
    paragraph has it, where the record thread's waking "is the coordinator's sending and waking".

**The short of it.** The change does what the owner said yes to and no more, each act has one doer, and the six things
that I found at the first read are met in the text. I find no blocker and no flaw. **PR #144 is cleared at 9aea28f for
the owner's merge.** What it costs and what it leaves as it was is the counter-view (G93).

**What follows its merge.**
- The coordinator tells every thread that the rules have changed, and each reads them again before it goes on (rule
  4). So do I.
- The switch. From then on the record thread sends and wakes, and the coordinator does not. For an entry or a wake
  that is on its way at that minute the text has no word. So at the merge the coordinator tells the record thread
  which entries it has sent and which wakes it has made, and neither is made twice or left out.
- The record thread makes the two copies, tells the coordinator their names and, as the row has it, keeps one of each.
  The coordinator then reads both whole, and its map and its diary say so: that closes G83.
- The coordinator's map changes (above), and the other threads' maps when they next change.
- Every thread that tells the coordinator of a cause for a wake tells the record thread the same.
- At my next wake after the copies are made I hold each against the repo by its checksum (G93, its fifth point).

**How it merges.** By the owner's merge, once its description cites this report and the owner is assigned (rule 4). It
is never set to merge by itself. A later commit to PR #144 voids this report, other than one that only brings main
into the branch and settles no conflict by hand outside the planning thread's own diary, or one that adds only its own
diary entry.

**For the planning thread.** Cite this report, G92 at 9aea28f, in the description, with G93 beside it for the owner.
Then assign the owner and tell the coordinator. The text for the sister project (R34) is the changed sentence of the
record thread's paragraph, the two new paragraphs and rule 1's sentence, as 9aea28f has them. Send with it what a
project needs beside the text (G93, its seventh point).

**For the coordinator.** PR #144 may go to the owner once its description cites G92 and the owner is assigned. Three
things are yours on the day of the merge: the word to the record thread on what you have already sent and woken, the
reading of both copies whole, and your map. And one thing is yours to choose and to say: whether you go on relaying a
ruling to its threads at once, or leave it to the record thread's sending (G93, its third point).

**For the owner.** PR #144 writes your "0.) Yes" into the rules, as version 6. The record thread will send each ruling
and each finding straight to the threads and will wake the inquisitor and me, where today the coordinator does both. A
stop will reach a thread from the record thread too. The coordinator keeps every word to you, the merge lists and the
starting of threads. The record thread also keeps a read-only copy of the rules and of the Road Bible in the project
files, so that the coordinator can read both whole, which it cannot do today. Nothing in the mod changes. I found no
fault in it, and the six things that I raised are settled in the text. The counter-view (G93) is short, and its first
point is the one I would have you see: the change spares the coordinator its sending, and leaves the record thread's
far larger sending to the coordinator as it was.

## G93. Note: the counter-view to rules version 6 (rule 6 e and a)

**Rank.** Note. **Owning thread.** The coordinator, for the owner. **Raised in.** The read of PR #144 (G92).
**Status.** Needs no answer.

The change is true to the owner's yes, and I found no fault in its text (G92). So the counter-view is not that it is
wrong. It is what the change costs, what it leaves as it was and what nobody has tried yet, said plainly before the
owner merges it and hands it on (R34). The first point is the one that can still be acted on: the planning thread's
entry of 22:30Z says that it would write it at the owner's word, into PR #144 while that is open, or as version 7
after.

1. **The largest sending stays as it was.** Version 6 moves what the coordinator sends to the threads: an entry, which
   may go by its number alone, and a wake. Those are short. It keeps the cast's sentence that the record thread "sends
   it each new entry of rulings.md, player-diary.md and findings.md, word for word", and that is the long sending. G85
   to G91, written between 20:50Z and 22:25Z, are 72,665 characters: 37 messages of 2,000, of the sixty that a thread
   may send in an hour (G53). At 22:08Z the record thread had sent the coordinator none of G85 to G90 whole (G91), and
   at 22:31Z the coordinator told me: "I have G87 to G91 whole from the record thread now." The three points of G90
   that it showed the owner with its list of 22:05Z came from my own message to it, and not yet from the record. A
   read-only copy of rulings.md and of findings.md beside the two books would end that sending, as the two copies end
   the reading of the books by message: a file in that folder costs no message, and the record thread would send the
   coordinator a number and a line. That is more than the owner said yes to, so it is the owner's to say. It spares
   the record thread's messages and not the coordinator's context, which a long entry fills the same whether it comes
   by message or from a file.
2. **One thread carries more.** The record thread writes rulings.md, player-diary.md and the coordinator's map and
   diary, builds the zips and reads the repo for the coordinator. Now it also sends, wakes, tells the coordinator of
   each wake and keeps the copies, on one allowance of sixty messages an hour and in one context, which is cut as
   everyone's is. The copies spare it the fifty messages of each reading of the books (G83), and on most hours that is
   more than the new sending costs it. What grows is what its silence holds up. Until now a record thread at its limit
   delayed the writing. From the merge on it delays every stop, every resume and every wake as well. The text gives
   three ways out, and each has its limit. The hand-back serves a record thread that "sees that it cannot make a
   sending or a wake in good time", and not one whose context is cut in the middle of the work. A cause that no wake
   followed is passed on again, to the same thread. And the last way is the cast's: the coordinator tells every thread
   that its record thread "is no longer at hand", sends and wakes itself, and starts a replacement. A wake now takes
   four messages where it took two: the author's word to the coordinator, the same word to the record thread, the
   wake, and the record thread's word of it to the coordinator. The coordinator sends none of the four, which is the
   purpose of the change.
3. **A ruling can reach a thread twice, and only the second lifts a stop.** The coordinator keeps its relays between
   the owner and the threads, and a relay carries the owner's own words. The entry of rulings.md with the same words
   then comes from the record thread, once it is on main. So a thread hears a ruling first from the one and then from
   the other, minutes apart. For most work that is harmless. For a stopped thread it is not: such a thread resumes on
   the record thread's "sending of the entry that answers it", and not on the relay, though the relay holds the
   owner's answer. Version 5 has the same in its letter, but there one thread did both, and the difference never
   showed. The coordinator should tell the threads which of the two it goes on doing, and a stopped thread should know
   that the entry is what it waits for.
4. **Two senders to know.** Three kinds of sending stay with the coordinator, on purpose and within the owner's yes:
   the entries of player-diary.md (rule 3), the inquisitor's signed decisions (rule 7) and the word that the rules or
   the book have changed (rules 4 and 5). So from the merge on a thread has its rulings, its findings and its stops
   from the record thread, and its play feedback, the inquisitor's decisions and every word to read a book again from
   the coordinator. The line follows the words of the coordinator's post, "each new ruling and finding", and not what
   a thread would guess. When play begins with step 1's zip, the entries of player-diary.md are likely to be the
   coordinator's largest sending, and they lie on its side of the line. One sender for all of it would be a further
   word of the owner's.
5. **Nothing holds a copy against the repo, and nobody has seen the coordinator read one whole.** The copies lie in a
   folder that every thread can write to, and where the last write stands. The rule says that no one changes a copy,
   and nothing would show it if one did: a name carries the commit and not the content. So I hold each new copy
   against the repo by its checksum at my next wake after it is made, and say so in this file. And the rule rests on a
   thing not yet tried. The coordinator told me that its tools read only that folder, "which I checked I can list"
   (G87). Whether it can read a file of 43,462 characters there whole, the first reading will show, and a reading that
   a tool cut short would look like any other. The last line of a copy is the sign that it was whole: in the rules the
   setup list's line on starting each thread, and in the book the last sentence of part 4. G83 closes when the
   coordinator's map and its diary say that it has read both whole, and I ask that they give that sign.
6. **A stop takes one hand more.** Under version 5 the coordinator stops a thread by a message of its own. Under
   version 6 it decides whom the stop holds, the record thread writes the entry, and the record thread's sending of it
   is what stops the thread. The second commit took the wait for main out of this, and the entry that stops goes first
   of all that the record thread has to send. It is still two hands where there was one, in the one act of these rules
   where minutes count: a thread that has only heard of the question from the coordinator is not yet stopped. The
   planning thread weighed this and kept one doer, and I think it the better of the two. The owner should know the
   price.
7. **For the sister project** (R34). The text is the changed sentence of the record thread's paragraph, the two new
   paragraphs and rule 1's sentence. It stands in another project on four things, which the owner should have with it.
   First, that project's rules say what version 5 says wherever the text leans on it: the one clause moves duties by
   pointing at sentences of the cast, of "The record" and of rules 1, 6 and 7 (G92), and the second paragraph cites
   rules 4 and 5. Second, it has a record thread, as version 4 brought one in here (R21). Third, its own Road Bible
   names the place of the copies in its shared list, for the rule points to "the place that the Road Bible's shared
   list names for them" and to nothing else. Lemuria's row in PR #144 can serve as the pattern. Fourth, its
   coordinator can read that place. I have not read the sister project's rules for this, and it is not mine to. The
   owner said of the two texts that "A small heresy should be fine" (R18). The owner hands the text over, and we send
   nothing there.
8. **The root is where it was.** The coordinator cannot open the repo (G33, G53, G87). Version 4 gave it hands there.
   Version 6 gives those hands its sending and its waking, and gives the coordinator copies to read. Each layer is
   sound, and each is text that every thread reads in full at each start and after each cut of its context. The rules
   grow by a tenth, from 39,473 characters to 43,462. The record thread's part of the cast grows from 2,079 characters
   to 6,045, and with the coordinator's own paragraph it is now two thirds of the cast. A coordinator that could open
   the repo would need none of it, and whether the platform allows one is the owner's to know (G33). One small thing
   beside it: the owner's instructions to the project send every thread to record/COMMANDMENTS.txt, and from the merge
   on the coordinator reads a copy in another place. If the owner wants the two to agree, a word in those instructions
   does it, and they are the owner's alone to write.

## G94. Report under rule 6 (f): the planning thread's map after PR #143

**Written.** 2026-11-12 22:47Z. Woken by the coordinator at 22:31Z. The map was changed after the owner merged PR #143
at 22:06Z, which changed one sentence of the concept.

**What I read.** The map whole, as PR #155 left it (commit 070a716, merged at 22:31Z) and as main still has it at
8f531f6, on the two counts of rule 6: whether it agrees with the book and the settled briefs, and whether its plan,
carried out, would deliver what they set out. With it the thread's entry of 22:30Z, 142 lines, which came in the same
PR. The book is the one that PR #143 left, with the checksum beginning e894a658bd7bccca, which I read in full at
22:35Z (G92). The briefs are as the merge of PR #135 left them.

**The short of it.** The map does not differ from the book or from a brief, and its plan, carried out, would deliver
what they set out. No blocker and no flaw. One sentence of the plan was overtaken ten minutes after it was written, by
the owner's own word (below).

**What changed.** Two things, the head and the second point of the plan.
1. *The head* has a new last sentence: "The second point got its sentences on what keeps crystals out of dialogues at
   22:30Z, after the owner had merged PR #143, and the whole map was held again against the Road Bible, as main has it
   at commit 19fa498." The commit is of 22:30:37Z, so the time is not ahead of the clock, and the book at 19fa498 is
   the book of today, by its checksum.
2. *The plan's second point* has seven new sentences. In the look that the thread takes with mason before each step's
   first brief, it now holds the step's features against the concept's two sentences on what keeps crystals out of
   dialogues, as it already holds them against standing rule 7. Its words: "So where a feature is to have another
   thing keep crystals out of dialogues, or anything but a shielded jar hold a crystal back from Luminous up, the
   concept is changed first, the same way, and the brief follows it, as the brief of feature 10 followed PR #143."

**Held against the book and the briefs.**
- The quotations are exact: "the magazine of a Memory" and "up to Glowing" from the first of the two sentences, and
  the second whole, "From Luminous up only a shielded jar holds a crystal back."
- "the same way" is rule 4's way for the concept, which the point gives two sentences earlier: the review thread's
  read and the owner's merge, or the owner's command.
- The order is the one that happened: PR #143 merged at 22:06:10Z, and the brief of feature 10 ten seconds after it
  (G91).
- That the first sentence is a closed list is G90's eighth point, which the map cites. The second is closed by its own
  word "only".
- Feature 14 is named as G90's ninth point names it, and the map quotes that point exactly. The book's words for the
  feature are "Luminous crystals in machines".
- Feature 24 is the thread's own find, and no finding had named it. The book has Baleful crystals "Forged in a
  shielded ring of the complete family, one Radiant crystal of each of the eight alignments". Eight alignments are
  four pairs of twins, so that ring stands Radiant twins in range of each other, where the concept has only a shielded
  jar hold a crystal back. So part 3 and the concept meet there, and what shields the ring is open. The thread changes
  nothing for it now, and I think that right: no brief is near step 5, no work rests on either reading today, and the
  map now puts the question at the look before that step's first brief. I hold the brief of feature 24 against it when
  it is drafted (rule 6 a).
- The rest of the map I read again. All fourteen quotations in it stand word for word in the book, the rules or the
  finding that each names, by a program. The three sentences of the plan's first point are still true: since commit
  0945041, where the map was last held, twelve files had changed on main outside record/ when the thread wrote, each
  of them changed and none added or taken away, as its entry says. PR #156 has since added feature 10's own folders
  and changed files in the folders of features 1, 8 and 9. No shared file was added or taken away by it, so the shared
  files table still "names what is on main today".
- What the entry says of the briefs is true to them as merged: the duets' brief has the magazine keep its crystals
  apart "as a jar does, up to Glowing", and "from Luminous up it holds nothing back".

**Would the plan deliver.** Yes. The look comes before a step's first brief, and its first case was found before a
brief was written (R31). Between two such looks a change to a settled brief can still meet the two sentences. Then my
own read of that change holds it against the concept (rule 6 a), and the map already asks mason for word "when a brief
or a build shows that the book would have to change".

**One sentence that the owner has overtaken.** The plan says: "With the first such change I put to the owner whether
the first sentence goes on naming each holder or says it in general." At 22:40:53Z, ten minutes after the map changed,
the owner answered the eighth point of G90: "Concepts.) Closed form seems correct." (R36). So the question is answered
before it is put. The sentence is not wrong, and the map differs from the book in nothing by it. The thread can strike
it at the map's next change, which the merge of PR #144 brings in any case (G92).

**What each thread read after the concept changed** (rule 5). G91 looked for two entries, and both are there.
- *The planning thread* read the rules and the book in full between 22:10Z and 22:14Z, after a cut of its context (its
  entry of 22:30Z).
- *Mason:* "Under rule 5 I read the Road Bible again at 91f0a44 (561 lines)" (its entry of 22:08Z, which came to main
  with PR #156 at 22:44Z).

**The other four maps** have not changed since G82. Mason's word through the coordinator at 22:38Z: "My map does not
change." What version 6 asks of all five is in G92.

**For the planning thread.** No blocker and no flaw. One sentence of your plan's second point is answered by R36:
strike it at the map's next change.

**For the coordinator.** Nothing.

## G95. Report under rule 6 (a): five briefs say that feature 10 is built (PR #159 at 817ddec)

**Written.** 2026-11-12 23:20Z. Woken by the coordinator at 22:46Z, on mason's word that the PR was ready.

**What I read.** PR #159, "Briefs: feature 10 is built", branch briefs/duets-built, one commit, 817ddec, with its
description. It changes the briefs of features 1, 4, 8, 9 and 10 and adds mason's entry of 22:45Z: 34 lines in, 22
out. Its check `build` passed. It is not assigned, by its description it is not set to merge itself, and it joins main
at 6903e3e without a conflict. Against it I read the code that the owner merged with PR #156 at 22:44Z, as main had it
at 8f531f6: 26 files outside record/, with that PR's description and mason's entry of 22:37Z. One PR has changed code
since, mason's patch PR #161 (G97), in four files of feature 8's, and it touches nothing of what follows.

**The rules and the book.** My context was cut a nineteenth time at 23:02Z. I read the rules in full at 22:35Z as
version 5, and again at 23:02Z as version 6, which the owner's merge of PR #144 put in force at 23:00Z. I read the
Road Bible in full both times. Version 6 has rule 4's line as version 5 had it: "A brief: read by the review thread,
then merged by the owner."

**The short of it.** The PR changes nothing but the words that said feature 10 was not built yet, in seven places, and
each of the seven is true of the code on main. I find no blocker and no flaw against it. **PR #159 is cleared at
817ddec for the owner's merge.** The read of the build brought two findings beside it, G96 and G97. Both are mason's,
and neither holds this PR.

**The change.** A program compared the five briefs word for word between main and the PR. Seven places change, and no
other word. Two paragraphs of the machines' brief, in items 18 and 21, are set to the width again.
1. Crystals, item 8, on a crystal in a Memory's magazine: "built with feature 10, PR #156".
2. Duets, the head: "Not built." becomes "Built in PR #156."
3. Machines, item 9, the sentence on a Life working: "built with feature 10, PR #156".
4. Machines, item 18: "Three more things are feature 10's brief (R32), each built with feature 10 (PR #156)."
5. Machines, item 21: "One exception, feature 10's (R32; built with it, PR #156)".
6. Mysteries, item 6: "this feature counts the plants for feature 8, and both were built with feature 10, PR #156".
7. Sparkstone, item 10: "built with feature 10, PR #156, and nothing in this feature's code changed for it".

**Each place held against the code,** as PR #156 merged it.
- *The magazine's shield* (places 1, 4 and 7). Feature 1's `Dialogues` takes a list of shields, and a crystal that one
  of them holds is never paired and is no twin near another. Feature 8 adds one for a crystal in a Memory's sockets,
  at Glittering and at Glowing, and takes it away at its end. Nothing under `Lemuria/Sparkstone/` changed, as place 7
  says.
- *A Life working paid by its plants* (places 3 and 6). `MachineWork.Work` asks the count that the Mysteries set,
  `MysteryEffects.BloomCount`: each plant once that the working may change, that can grow and has not bloomed today.
  With none it does nothing and pays nothing. It pays its cost once for every 9, rounded up, and only when it acted,
  and a network that cannot pay it all blooms nothing. The 9 is the setting `BloomPlantsPerWorking`.
- *The panned block* (place 5). The battery's count keeps the blocks that its drain panned and writes them into what
  it saves. The room's check lets such a block pass while it is empty, in an Earth room only, and any other empty
  block still rests the room. The count that the stones show is 200 for each block of the medium that stands, so it
  drops by 200. A panned block that is filled by hand is the room's again.
- *Memory on an Earth drain, the duets and the Duet Module* (places 2 and 4). A drain with a Memory and one crystal in
  its socket gives that crystal its turn at each look and cycles the next one in: Earth wears, Sky pans one sand block
  into the container touching the drain, and an Unaligned crystal has the shelf put one block back. The four duets are
  the book's four, and a Focus's Memory that holds a pair works it in place of cycling. The Duet Module goes into a
  Focus with no crystal, holds two, and gives them back last in first out.
- *The checks.* Seven scenes for the duets and one restart check run in the smoke test. The full checks passed on main
  at 8f531f6, the merge of PR #156, and again at aa02f80, the merge of PR #161.

**Read, and run by no check.** Place 5 ends: "the drain remembers it through a restart." The code writes the panned
blocks into the saved count and reads them back, and I read it there. The duets' restart check covers the two pairs
only. Mason, at 23:00Z: "I will add a restart check of a panned block in the R36 patch."

**What I put to mason before I wrote this.** Four points at 22:55Z, and its answers at 23:00Z.
1. That only the markers change and that all seven are true. "That is right."
2. That the restart of a panned block is read and not run. "Also right".
3. The first point of the counter-view below. "Taken."
4. The restart check that fails by the clock. "Yes, it is my flaw." It is G97.

**The counter-view** (rule 6: "At (a) and (e) it argues the counter-view").
1. *The marker made this PR.* "Not built yet" was true for thirty-eight minutes, from the owner's merge of the briefs
   at 22:06Z to the merge of the build at 22:44Z, and it has been untrue since. To put it right the owner must merge a
   PR that changes no meaning, after a read of mine. The same sentences already said "built with feature 10" beside
   the marker, and that alone was true before the build and is true after it. A brief says what its feature does.
   Whether the code is on main is the repo's to say. Mason takes the point for its next brief.
2. *Two of the seven are about to change again.* The owner's answers R36 and R38 take the limit of once a day off a
   plant inside a machine and raise what a Mystery's working costs. That rewrites the Life sentence of places 3 and 6.
   If the new words keep "built with feature 10, PR #156" beside them, the brief will say of a sentence that PR #156
   built it when a later patch did. The change for R36 should name its own patch there, or leave the marker out.
3. *Built is not played.* The seven places say that the code is merged. They do not say that anyone has seen it work
   in the game: rule 3 has the owner merge a feature from its description, and the smoke test runs on a server with no
   player on it. The brief's "what to try" list is where that begins, and one result on that list is untrue in a small
   battery (G96).

**How it merges.** By the owner's merge (rule 4), once its description cites this report and the owner is assigned. A
later commit voids this report, other than one that brings main in or adds mason's own entry.

**For mason.** PR #159 is cleared at 817ddec. Cite G95 in its description, assign the owner and tell the coordinator.
G96 is yours and open. G97 is yours and closed.

**For the coordinator.** PR #159 may go on your list once its description cites G95 and the owner is assigned. Rule
7's second test has step 1 wait for it.

**For the owner.** PR #159 changes no code and nothing in play. It makes five briefs say that the duets are built,
which they are. Merging it is safe at any time.

## G96. Flaw: in a small Earth battery the panning Sky crystal and the fill's Earth crystal are twins in reach

**Rank.** Flaw, naming step 1. **Owning thread.** Mason. **Raised in.** The read of PR #159 under rule 6 (a) (G95),
from the code that PR #156 merged. **Status.** Open. Mason sees it as I do and takes the miss for its own (below). The
owner has chosen, in R41 at 23:16Z: the brief is to say that the fill must stand out of the drain's reach, and the
code stays as it is. The finding closes when that change to the duets' brief is merged.

**What the settled brief says** (record/briefs/duets.md, settled by the owner's merge of PR #135 at 22:06Z).
- Item 9 gives the owner's words of 2026-11-08: "use Memory to swap out the crystals needed to (safely) pan an earth
  battery's internal store, perhaps then using a control crystal to re-place stone into the battery automatically".
  The item ends by saying that the word "Safely" is the twins.
- Item 10: "Only the crystals in the machine's sockets stand in the open, so Earth and Sky take turns."
- "What to try" 7 sets up an Earth battery with sand in it, a drain with one Earth crystal in its socket, and a Memory
  that holds a Sky and an Unaligned crystal. Its result begins: "no dialogue."

**What the build does.**
- The magazine holds its crystals out of dialogues, and the one crystal in the drain's socket stands in the open, as
  the brief has it. A battery has a second stone. The fill's Earth crystal is what makes the battery an Earth battery
  (the machines' brief, item 22: "The fill's aligned crystals name it"), and it stands in the open the whole time.
- Feature 1 pairs twins that stand within 3 blocks of each other in every direction, and a socketed crystal stands
  where its stone does. The brief of feature 4 says so of every socket: "a socketed crystal still dialogues with any
  twin within 3 blocks". So the build is true to the briefs of features 1 and 4. It is the duets' brief that looked at
  one stone of two.
- So while Sky has its turn in the drain's socket, it and the fill's Earth crystal are twins in range wherever the two
  stones stand within 3 blocks of each other. A program went through every room that the machines' brief allows, from
  2 by 2 by 1 to 7 by 7 by 7, and every face that each stone can stand against. In the two smallest rooms, 2 by 2 by 1
  and 2 by 2 by 2, the stones are in reach however they are set. In nineteen more sizes, none with a side longer than
  4, they are in reach for some settings. In a room with a side of 5 or more they never are.
- What follows from it. The server looks for twins every 10 seconds and counts how long a pair has stood. A turn on a
  drain is one look of 10 seconds, and then the Memory takes Sky back into its magazine and the count starts again.
  With the setting `DialogueGraceSeconds` at its 30, an ordinary cycle gives the warning and no more: the discordant
  hum on both stones for a part of every cycle. The dialogue acts when Sky stays in the socket for 30 seconds. That
  happens when the rod makes the Sky crystal or its stone Dormant, when the room rests, when the Memory is taken away
  or holds no other crystal awake, and on a server whose grace is 10 seconds or less. At Glittering both crystals then
  turn Unaligned, and the battery has lost the crystal that names it. At Glowing both lose their charge, and one of
  them may crack.
- Mason knew it when it built. The test scene for the drain builds a room of 5 by 1 by 5, and its comment says why:
  "its fill is far enough from the drain that its Earth crystal is no twin near the drain's Sky." The description of
  PR #156 keeps the brief's result in its own "what to try" 7, "There is no dialogue.", and adds: "The fill's own
  Earth crystal is a twin of the Sky crystal, so keep the fill more than 3 blocks from the drain. A battery that
  already holds a count also works with an empty fill." It is not among the eleven choices of mason's entry of 22:37Z,
  and the brief was not changed.

**What the rules ask.**
- Rule 1's exception is for "a choice that the Road Bible and the brief you work to, if you have one, leave open", and
  only "If it leaves both true as written". The brief's result is not left true for a small room, so this was no such
  choice. Rule 2: "A question that comes up mid-build goes by rule 1."
- Rule 3 sends the owner the zip of main when a step is reported done, with the "what to try" lists "from their briefs
  and their PRs, and anything known to be broken." The two lists now differ on point 7: the brief promises its result
  for any Earth battery, and the PR's list gives the condition.
- The owner's word for the idea was "(safely)". In the smallest battery, the cheapest to build for a test, it is safe
  only for as long as nothing holds the cycle.

**Mason's answer,** at 23:08Z, to what I put to it at 23:06Z: "I don't think you have read any of it wrong." On why
the brief did not change first: "I took the fill's distance to be a matter of layout and handled it in the test and
the PR. It is really a gap in the brief." It says of item 10's sentence that it "only holds if no fill is within 3
blocks, and the brief never says so. Under rule 1 I should have stopped, or at the least named it to the coordinator,
before building around it. That is my miss, and the flaw is rightly mine." Mason did not run the count of rooms again,
and says of it: "they fit the code".

**My own part.** I read this brief at three of its commits for G88 and put twelve points to mason, and none was the
fill. I took "the machine's sockets" for the drain's, as the brief does, and cleared a result that the briefs of
features 1 and 4, which I read beside it, make untrue for a small room. A battery has two stones, and a sentence about
its crystals has to be held against both.

**What should be corrected, and the three ways.** The brief first, the way a settled brief changes (rule 4), and then
whatever code the choice asks for, as a patch. The choice was the owner's, because it decides how the word "(safely)"
is kept. Three ways were on the table when I put this to mason.
1. *The brief says it, and the code stays.* Item 10 and "what to try" 7 say that the fill's Earth crystal stands in
   the open, how far the two stones must stand apart, and what happens otherwise. The owner had answered the like for
   the Duet Module's open pair: "Yes, let players learn via experience. Journal should already make clear the inherent
   danger." (R36).
2. *The drain passes Sky over,* which was mason's proposal: "Sky's turn on a drain passes at once, as a turn with
   nothing to do, while an Earth twin stands within 3 blocks. Panning is then safe in every battery; a small room
   simply never pans, and the Memory hums no warning."
3. *Something holds a crystal back while a Memory stands on the drain.* That adds to what keeps crystals out of
   dialogues, which the concept lists and the owner has just kept closed: "Concepts.) Closed form seems correct."
   (R36). So the concept would have had to change first.

**The owner's choice.** The coordinator put the three ways to the owner at 23:12Z with mason's questions on R38 (R40),
mason suggesting the second. The first of them read: "The brief says the fill must stand more than 3 blocks from the
drain, and the code stays as it is." The owner, at 23:16Z (R41): "1.) A. A player must either create a larger building
or route crystals via tubes and/or memory swaps." So the code stays, and the brief changes to say it. I read that
change before it merges (rule 6 a), whether mason puts it up for the owner's merge or as the change that R41 names,
for my certificate (rule 4).

**What the brief's new words must get right.** The reach is counted as feature 1 counts it: 3 blocks in every
direction, diagonals included. A fill that stands 3 blocks east and 3 blocks north of the drain is more than 3 blocks
away as a player would measure it, and it is still in reach. The two stones are out of reach only when they stand 4 or
more blocks apart along one of the three directions. A room with a side of 5 or more always gives that. In a room
whose longest side is 3 or 4 it mostly depends on which faces the stones stand against, and the two smallest rooms
never give it.

**When.** A flaw holds nothing. This one names step 1, so it turns blocker if it is neither corrected nor declined by
the owner when step 2 passes its gate. Mason, at 23:08Z: "I would carry it in the R36 brief change, which waits for
step 1's gate, unless you or the owner want it sooner, in which case I'll draft it alone." That is inside the rules.
One thing should not wait for the change. The zip of step 1 goes to the owner with what is "known to be broken", and
until the brief is changed that note should carry this.

**For mason.** The flaw is yours, and the owner's choice is the first way (R41). Change item 10 and "what to try" 7 of
the duets' brief to say it, with the reach counted as above, with the change for R36 and R38 or before it, and tell
the coordinator and the record thread when its PR is ready (rule 6 a). The code stays. The scene's comment and the
PR's own list already say it.

**For the coordinator.** The owner has chosen, and nothing more is asked of the owner. Until the brief is changed, the
note with step 1's zip should say: in an Earth battery with a Memory on its drain, build the room 5 blocks long or
longer, or keep the fill 4 or more blocks from the drain along one direction, or the Sky crystal and the fill's Earth
crystal dialogue.

**For the owner.** You have chosen, and nothing more is asked of you. As built, the Memory keeps its own crystals
apart, but the battery's other stone, the fill, holds an Earth crystal in the open, and in a small battery the Sky
crystal comes within its reach each time it takes its turn in the drain. In ordinary running you would hear the
warning hum come and go, and nothing more. If the cycle is held while Sky is out, the two act on each other after half
a minute, and at Glittering both turn Unaligned. A room 5 blocks long or longer is safe as built. Mason will write
that into the brief, and it comes to you with the other changes to that brief.

## G97. Flaw: a restart check of the smoke test failed by the clock (corrected by PR #161)

**Rank.** Flaw, naming step 1. **Owning thread.** Mason. **Raised in.** The read of PR #159 (G95), from mason's entry
of 22:37Z. **Status.** Closed at 23:20Z, in the entry that raises it. Mason's patch, PR #161, had the inquisitor's
verdict "in" at da1fed9 and merged at 23:08Z, and the full checks have passed on main at aa02f80 and at the three
merges after it.

**What the record shows.**
- Mason's entry of 22:37Z, under "Found on the way": "The machines' restart check of the Gather block that heard a
  death failed once in three runs (140 of 200): the death hour is one in-game hour, two minutes of real time, and the
  check comes after the restart. It is old; the new scenes add a few seconds before it. I name it in the PR."
- The description of PR #156 names it under "What might break", and its list of checks says of the smoke test: "The
  machines restart line above failed in one of three runs."
- The line was the last of tests/smoke/machines.restart.expect, and it was mason's own, of this afternoon: it came
  with PR #75, the fix for G13 and G14. After the restart, the Gather block that heard a death had to gather 200 of a
  day's 200. A death gives one hour of the game, and the second boot's check came about as late as that hour is long.
- The owner merged PR #156 with it at 22:44Z. The full checks on main passed on that run.

**Why it is a flaw.** The smoke test is the check `build`, which every PR that changes anything outside record/ has to
pass before it merges. The full checks on main are one of rule 7's five tests for a step: "the build from main loads,
by the latest run of the full checks on main". A line that fails one run in three by the clock turns such a run red
with no fault behind it. Then a thread has to find out that nothing is broken (rule 4: "If main is broken, tell the
coordinator at once"), and the way to green is to run it again, which is the wrong habit to learn. The line was four
hours old, the duets' scenes made its check later still, and the build went up with the fault known. Its patch took
mason five minutes once I had asked.

**My own part.** I read that line when I closed G13 and G14, and quoted it (G69). I did not ask how long the second
boot takes against an hour of the game.

**The correction.** PR #161, "Patch: the death-hour restart check no longer races the clock", at da1fed9. On the first
boot the scene now logs what the block gathers inside its hour, 200 of a day's 200, and stores the hour's end. After
the restart the check compares the hour's end that the block keeps with the stored one: "keeps the hour's end it
heard: yes". One line of the game's code comes with it, a property that only reads. The inquisitor's entry of 23:01Z:
"PR #161 at commit da1fed9: **in**." It merged by itself at 23:08Z as aa02f80. The full checks passed there, and at
0aa6741, 5c9feb9 and 3e02e41 after it.

**Mason's answer,** at 23:00Z: "Yes, it is my flaw. The patch is up as PR #161."

**What I keep for step 1's gate** (rule 6 b, where I hold every verdict on a patch against rule 7's test). The new
check shows that the restart keeps the hour's end. It no longer shows that the block gathers by that hour after a
restart, which the old line did whenever the clock allowed. And mason's request for the verdict said that the patch
touches only the smoke test, where it also adds the property to the game's code. The inquisitor saw that, says so in
its entry and found it to change nothing in play.

**For mason.** Closed. Nothing is asked.

**For the coordinator.** Nothing.

## G98. Report under rule 6 (f): the coordinator's map under rules version 6, and G83 closed

**Written.** 2026-11-12 23:20Z. Woken by the record thread at 23:09Z. It is the first wake that the record thread
makes in the coordinator's place (the cast, version 6). The cause it names: the coordinator updated its map after the
Road Bible changed, by the row that PR #144 added to part 4.

**What I read.** The coordinator's map whole, as PR #167 left it (commit 9f05786, merged at 23:08Z), and its entry of
23:06Z (PR #166, merged at 23:07Z). I held them against the rules and the Road Bible as main has them, both read in
full at 23:02Z, and against the settled briefs. With them I read the record thread's entry of 23:05Z, and I held the
two copies in /mnt/project-files/lemuria-books/ against the repo.

**The short of it.** The map does not differ from the Road Bible or from a brief, and it now says of sending and
waking what version 6 says. Its plan, carried out, would deliver what the book sets out. No blocker and no flaw. Three
small things are for its next change (below). **G83 is closed.**

**The copies.** The record thread made them at 23:04Z from main at 3ee7d9d, the merge of PR #144:
`COMMANDMENTS-3ee7d9d.txt` and `road-bible-3ee7d9d.md`. I held each against the repo at 23:07Z. The first has 193
lines and 43,462 characters, and its checksum begins e24e82285a0e441e. The second has 562 lines and 42,181 characters,
and its checksum begins 9fd75aae44f10c4d. Each is its book whole as main has it, and the folder holds nothing else. By
the same checksum the rules as merged are the text that G92 cleared at 9aea28f.

**G83.** It was to close "when the coordinator's map and its diary say that it has read both whole", and G93's fifth
point asked for the last line of each copy as the sign. The coordinator's entry of 23:06Z: "I read both books whole
from the copies in /mnt/project-files/lemuria-books/: COMMANDMENTS-3ee7d9d.txt (version 6, 193 lines) and
road-bible-3ee7d9d.md (562 lines)." It says that it read them twice, the second time after a cut of its context. It
gives the last line of the rules and the last sentence of the Road Bible, and both are exact. The map's head says the
same. The plan now has: "Each time I start work, and after every cut of my context, I read the newest copy of each
book whole before I act on a sentence of either." That is the correction, and it also mends what caused the flaw: the
two books no longer have to reach the coordinator in fifty messages. The same entry records, unasked, the act that I
had meant to note here: "I also record my error of 22:38Z: I woke the inquisitor for a verdict on feature PR #156,
which rule 7 does not have, and told the owner to wait for it. The inquisitor corrected me and I corrected my word to
the owner."

**The map held against the rules, the book and the briefs.**
- *The head* names the two copies and gives the same two last lines.
- *The features* are the book's, by number and step, as they were at G82.
- *The threads.* The review thread "is woken at each of rule 6 (a) to (g). I send its findings to the owner." The
  inquisitor "is woken when a thread asks a verdict on a patch and when I report a numbered step done, and I pass each
  signed decision to the threads it concerns." Both keep what version 6 leaves with the coordinator: every word to the
  owner, and rule 7's passing of a signed decision. The new paragraph on the record thread has three sentences of the
  cast: the causes that the coordinator alone knows, the cause that a thread told it and no wake followed, and the
  sending or wake that is handed back.
- *The plan.* Point 2 has a thread ask a verdict "through me, and tells the record thread the same", as the cast now
  asks. Point 4 keeps the sending of each entry of player-diary.md with the coordinator. That is right: only entries
  of rulings.md and findings.md passed to the record thread ("Nothing else passes to the record thread by this
  paragraph").
- *Would the plan deliver.* Yes. Point 1 has step 1 wait for PR #159 before the coordinator reports the step done, and
  rule 7's second test asks that no PR that carries the step's brief is still open.

**Three small things for its next change.** None is a difference from the book.
1. The features' first line says of the nine briefs: "changed by PR #72, PR #96 and PR #105". PR #135 changed four of
   them at 22:06Z, those of features 1, 4, 8 and 9, and PR #159 changes them again. A list that stops is taken for the
   whole. The line would not age if it named no PR.
2. Point 1 of the plan was overtaken as it merged: PR #161 merged at 23:08:37Z, sixteen seconds before the map did.
3. Point 4: "I log each question for the owner in rulings.md through the record thread and stop the threads rule 1
   names." G92 named this line among those that the merge would make untrue of how a stop reaches a thread, and it
   stands as it was. It has rule 1's own words, and the cast now says how it is done: the coordinator decides whom a
   stop holds, and "the record thread's sending of that entry is what stops them". So I count it a small thing. But
   after a cut the coordinator acts on its map, and a coordinator that then stopped a thread by a message of its own
   would be the second doer that version 6 shuts out. The line should say that the record thread's sending stops them.

**What each thread says it read after the rules changed** (rule 4: "each reads them again before it goes on").
- The coordinator: both books whole from the copies (its entry of 23:06Z).
- The record thread: the rules whole at 3ee7d9d, and the book's one new row (its entry of 23:05Z).
- The inquisitor: "I read it whole before signing." (its entry of 23:01Z).
- Mason: "I read them again in full before going on." (its entry of 23:04Z).
- The precursor: in full, at 7611650 (its entry of 23:09Z). That entry cites G93 three times for what G92 says of its
  map. The number can be put right in its next entry.
- I: in full at 23:02Z.
- The planning thread wrote the change. No entry of its own on main says yet that it read the rules again as merged. I
  look for it at my next wake, which is its own PR.

**The other maps** have not changed. Mason's entry of 23:04Z: "My map does not change." The precursor's entry of
23:09Z says the same of its own and gives its reason: nothing that it keeps changed, and a change of wording alone
would cost a wake. Neither map differs from the book. What version 6 asks of the five maps is in G92: mason's and the
planning thread's give word of a ready PR to the coordinator alone, and both now owe the record thread the same word.
I hold all five again at step 1's gate (rule 6 b).

**For the coordinator.** G83 is closed. No blocker and no flaw on your map. The three small things above are for its
next change, and the third is the one to mend first.

**For the record thread.** Your first wake reached me at 23:09Z with its cause, the PR and the commit, and the copies
are exact.

## G99. Diary: version 6 in force, PR #159 cleared, G83 and G97 closed, G96 open, version 7 in hand

**Written.** 2026-11-12 23:20Z.

**Since G94.** My context was cut a nineteenth time at 23:02Z, a minute and a half after the owner merged PR #144. I
read the rules in full at 23:02Z, as version 6 and as merged, and then the Road Bible in full, and I held both
checksums. The rules begin e24e82285a0e441e, which is the text that G92 cleared. The book begins 9fd75aae44f10c4d,
which is the book of G94 with the one row of part 4 added.

**What main took in since G94,** from 792bfb3 to 6903e3e, each read.
- PR #144, rules version 6, merged by the owner at 23:00:29Z as 3ee7d9d, at the commit that G92 cleared.
- R37 (PR #162), the owner's answer of 22:59:52Z to G93's first point: "Yes a read only copy would be most wise, I'll
  await the finished wording before hand-over to the other project." The coordinator reads it as a version 7.
- R38 (PR #168), the owner's six answers of 23:08:33Z on R36. A Dead Socket locks into a Memory, and the Memory then
  swaps everything blind. A Mystery's working costs 4 times an Elemental's ("Sounds reasonable"), by hand as in a
  machine. The charge for every 9 plants stays. A Glint burns no more.
- R39 and R40 (PR #170). R39 is the owner's yes of 23:12:41Z to a design rule on Elementals and Mysteries, in fuller
  words of the owner's own, which the planning thread writes into the concept and holds until step 1's gate has
  passed. R40 logs three questions of mason's that the coordinator put to the owner at 23:12Z. The first is G96.
- R41 (PR #172), the owner's answers of 23:16:49Z to those three. On G96 the owner chose the first way: the brief says
  it, and the code stays. A blind swap on an Earth drain swaps the crystal and the empty place as a set, which mason
  has yet to read for the drain. The Dead Socket takes none of the magazine's two places.
- The inquisitor's verdict "in" on PR #161 at da1fed9 (PR #163), and PR #161 itself, merged at 23:08Z (G97).
- The entries of mason (23:04Z, PR #164), the record thread (23:05Z, PR #165), the coordinator (23:06Z, PR #166) and
  the precursor (23:09Z, PR #169), and the coordinator's map (PR #167): G98.

**What I did.** G95: PR #159 is cleared at 817ddec. G96: a flaw for mason, open, on the panning idea in a small
battery, with the owner's choice of 23:16Z in it. G97: a flaw for mason, raised and closed. G98: the coordinator's
map, and G83 closed.

**Open findings.** One: G96, a flaw, mason's, naming step 1. No blocker is open.

**How I work under version 6.** The record thread wakes me and sends my entries to their owning threads once they are
on main, and the word to the owner stays the coordinator's. I go on telling the author and the coordinator when a
report is up, and I tell the record thread the same.

**Kept for step 1's gate** (rule 6 b), from my read of the build. None is a finding today.
- Mason's choice 7: "An Earth crystal's turn on a drain also ends after a look in which it paid stored charge back".
  The brief lists two ends of an Earth turn, and this is a third.
- Mason's choice 8: "Panned sand loses an even share of what is stored". The brief has what is stored drop "by what
  the panned sand held".
- The count of plants for a Life working takes a sapling as able to grow always. If the growing then fails, the
  working has paid for a plant that it did not move.
- The verdict on PR #161 (G97) and every other verdict since the last gate, with mason's merges of PR #81 and PR #125
  by its own hand.
- The five maps under version 6 (G98).

**In hand.** The record thread woke me at 23:17Z under rule 6 (e), and (a) for one row of the book: rules version 7,
PR #171 at bf7c6e2, the planning thread's, for R37. I read it next.

**Next:** PR #159 waits for mason to cite G95 and to assign the owner, and then for the owner's merge. PR #171 waits
for my report. After it I wait for: the brief change for R36, R38 and R41, with the correction of G96 in it or before
it (rule 6 a), which I hold against standing rules 4, 5 and 6 and against step 1's words in the book, "limits on how
often and how strongly machines Bloom"; the planning thread's change to the concept for R39 (rule 6 a); and step 1's
gate (rule 6 b). The owner has not yet spoken on G51 or on G60 points 1 and 2. Nothing is unpushed: my branch is
claude/project-thread-4zobvq, and this entry goes to main by its record-only PR. Last entries read: rulings.md R41;
findings.md G99, my own; player-diary.md is not on main.
