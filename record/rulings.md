# Rulings

Entries R1, R2 and so on, as the Commandments set out under "The record" (record/COMMANDMENTS.txt): questions raised,
the owner's rulings and commands in the owner's own words, and anything threads agree between themselves. New entries
go at the end. An answer cites the number of the entry it answers.

## R1 to R11: the eleven hand-over questions and the owner's answers

**Raised.** 2026-11-12 03:06Z by the planning thread ("the Optimist Visionary"), together, in its thread and in the
hand-over plan (the Claude Doc "Lemuria: hand-over to Commandments v2",
https://claude.ai/artifact/N6nhzTfNe6FSaMn8E5SiwK, revision 8), as questions H1 to H11. Each question below is copied
from the plan as the owner read it. The plan told the owner how to answer: 'Answer in the thread, for example "H1 yes,
H5 three builders". "Yes" takes my recommendation. Each answer goes into rulings.md in your own words.'

**Answered.** 2026-11-12 03:18Z by the owner, all eleven in one message in the planning thread. Each entry below gives
the owner's line for that question, whole and unchanged.

**Recorded.** 2026-11-12 03:30Z by the planning thread as deputy (the cast, second tier): the owner gave these rulings
to that thread directly and did not say the coordinator would record them.

### R1. H1: does main as it stands count as the prototype?

> **H1. Does main as it stands count as the prototype?** The rules have a proto thread build a first loading build
> before step 1. Lemuria already loads and you played steps 0 to 4.
> - Recommend: yes. You rule that main is the prototype, already played and merged, and no proto thread is started.
>   Its build-and-checks work passes to the thread the shared list names (H5).
> - Otherwise: a proto thread starts and redoes the setup work under the new rules (CI, scripts) before any step code.

The owner: "H1.) Yes."

Concerns: the planning thread (the Road Bible's prototype terms); the coordinator (no proto thread to start).

### R2. H2: how do the old steps and their parts map onto Road Bible steps?

> **H2. How do the old steps and their parts map onto Road Bible steps?**
> - Recommend: keep the old numbers 1 to 11 so the notes still read right. Steps 1 to 4 are listed as done and you
>   pass them by a sign-off in rulings.md (rule 7 allows it), so step 5 may go on. A step's parts become features
>   under it: step 5 holds "The Four Mysteries" (part 1) and "Duets" (part 2).
> - Otherwise: every part becomes its own step (step 5 part 2 becomes step 6, and every later number moves up).

The owner: "H2.) Precursor steps, from a dark age."

Concerns: the planning thread (the Road Bible's steps). Deputy's note: what these words settle for the Road Bible is
asked in R12, and nothing acts on this entry until R12 is answered.

### R3. H3: what are the briefs of features already built?

> **H3. What are the briefs of features already built?** No feature code moves without a settled brief, and a patch
> must leave its brief true.
> - Recommend: the builder that inherits each feature writes its brief into record/briefs/ from the signed-off note as
>   built (play-test changes included). The review thread reads them and you merge them, in one batch right after the
>   Road Bible. Until then those features take only the fixes already ruled.
> - Otherwise: you rule that the signed-off notes in Claude Docs stand as the settled briefs as they are. Faster, but
>   part of the record then lives outside the repo, where the rules put it.

The owner: "H3.) as recommended"

Concerns: the builder that inherits the built features; the review thread; the planning thread.

### R4. H4: what happens to PRs #9 and #10?

> **H4. What happens to PRs #9 and #10?**
> - Recommend: you play and merge both under the old rules before the Road Bible lands, #10 first. If GitHub then
>   shows "This branch is out-of-date" on #9, press "Update branch" and wait for the green check before merging. They
>   were written and checked under the old rules, and they merge cleanly together.
> - Otherwise: they wait, and the builder takes them over under the new rules, with a brief and a diary entry first.

The owner: "H4.) as recommended"

Concerns: the owner (the two merges); the planning thread (what main holds when the Road Bible is written).

### R5. H5: who builds?

> **H5. Who builds?** Every step so far touched five or more feature folders (step 5: Crystals, Cradle, Evocations,
> Journal, Sparkstone and Mysteries). Under the new rules a builder changes only what it owns.
> - Recommend: one builder, role name "mason", owning every feature folder and the whole shared list (scripts, CI,
>   Core, the project file, modinfo, README, CLAUDE.md, the general lang file). It works one feature at a time, as
>   Lemuria was built. Split it later by a Road Bible change if you want speed.
> - Otherwise: three builders by layer: "mason" (Core, Crystals, Cradle, Sparkstone, Storage, Earmuffs), "evoker"
>   (Evocations, Mysteries, Journal), "engineer" (Machines). More at once, but most steps become a chain of agreements
>   in rulings.md.
> - Either way: diary folders are named for the role in lowercase (record/diary/optimist-visionary/, coordinator/,
>   inquisitor/, mason/, janitor/), and the review thread keeps findings.md instead.

The owner: "H5.) As recommended, hand over the legacy files over to Precursor thread."

Concerns: the planning thread (the cast and who owns what); the coordinator (which threads it starts). Deputy's note:
what "Precursor thread" and "the legacy files" mean is asked in R12, and nothing acts on this entry until R12 is
answered.

### R6. H6: does the CI smoke test count as "it loads"?

> **H6. Does the CI smoke test count as "it loads"?** It boots a headless 1.22.7 server with the mod, checks the log,
> and boots again after a restart. No game client runs in CI.
> - Recommend: yes, for the step gates. Your client play stays what rule 3 says, and gates do not wait for it.
> - Otherwise: "it loads" falls to you, and every step gate waits until you say the step's zip loads.

The owner: "H6.) Yes, as recommended"

Concerns: the inquisitor (the gate on a step); the coordinator.

### R7. H7: what happens to the old audit's open items?

> **H7. What happens to the old audit's open items?**
> - Recommend: the new review thread (Glowie) carries them into findings.md as its first findings, each owned by the
>   builder. The two unbuilt fixes and PR #9's unsaved death hour then go as patches. Finding 16 changes what you
>   notice in play, so it waits for your merge.
> - Otherwise: they stay in the old report and the builder works from its diary.

The owner: "H7.) as recommended"

Concerns: the review thread; the builder that inherits the code those items are about.

### R8. H8: what happens to the old design docs, roadmap and diaries?

> **H8. What happens to the old design docs, roadmap and diaries?**
> - Recommend: the Road Bible takes over the roadmap and the pre-production doc's step list, earmarks included as an
>   unplaced list. The old Claude Docs and project-file diaries stay as read-only history, linked from the Road Bible.
>   New diaries live only in record/diary/.
> - Otherwise: the roadmap doc stays alive beside the Road Bible (two places to keep in step).

The owner: "H8.) as recommended"

Concerns: the planning thread (the Road Bible); every thread (where a diary lives).

### R9. H9: the parked claims question

> **H9. The parked claims question.** With the claims switch off, does every Lemuria block used by hand open inside
> another player's claim (cradle, placed crystal, sockets, Control Shelf, Memory, rack), or only racks and set-down
> jars?
> - Recommend: every block, from your step 3 words "every feature reads the same switch, so none can drift".
> - Otherwise: racks and jars only, as PR #10 builds now.

The owner: "H9.) Every feature reads the same switch"

Concerns: the builder that inherits the blocks used by hand, and their briefs (R3); the review thread, which carries
the old audit's open items (R7).

### R10. H10: who brings the old-rule texts up to date?

> **H10. Who brings the old-rule texts up to date?** CLAUDE.md, README's "Working in parallel" and the project
> instructions in settings still state the version 1 rules.
> - Recommend: the builder that owns them rewrites CLAUDE.md and README to point at record/COMMANDMENTS.txt in its
>   first PR. Only you can change the project instructions: replace them in
>   [Project settings](#project-settings/instructions) with one line pointing at record/COMMANDMENTS.txt.

The owner: "H10.) As recommended, I'll tweak the project memory"

Concerns: the builder that owns CLAUDE.md and README; the owner (the project instructions).

### R11. H11: can GitHub tell the owner's merge from a thread's?

> **H11. Can GitHub tell your merge from a thread's?** No. PR #8 shows DunkelzahnsWill as both author and merger,
> because threads act through your account.
> - Recommend: keep it so, and rely on the list of owner-merge PRs the inquisitor sends you at each step gate (rule 7).
> - Otherwise: give the threads their own GitHub account or app.

The owner: "H11.) As recommended"

Concerns: the inquisitor; the coordinator (the list it sends the owner at each step gate).

## R12. Question raised, a rule 1 stop: what are the "Precursor steps" (R2) and the "Precursor thread" (R5)?

**Raised.** 2026-11-12 03:25Z by the planning thread, to the owner, on two decision cards in its thread.
**Recorded.** 2026-11-12 03:30Z by the planning thread as deputy.

**The question.** The owner's answers in R2 and R5 name "Precursor steps", a "Precursor thread" and "the legacy
files". None of the three is in the rules or in the hand-over plan, and in the old design "precursor" has meant only
the crystals found in ruin loot. Each answer reads more than one way, and the readings lead to different Road Bibles.

- R2, "Precursor steps, from a dark age." The planning thread reads it so far as: the steps built before these rules
  are set apart as Precursor steps, and no gate judges them again. The words do not settle whether the steps still to
  come keep their old numbers (5 to 11) or start again at 1, whether step 5 part 1 (PR #9) is a Precursor step, or
  whether the answer is the owner's sign-off on those steps and acceptance of their merged PRs (rule 7).
- R5, "As recommended, hand over the legacy files over to Precursor thread." The words do not settle whether the
  Precursor thread is the one builder recommended in H5 under another role name, a second builder beside it, or a
  thread that builds nothing; or whether "the legacy files" are the code and shared files on main, the old design
  notes, roadmap and diaries, or both.

**The cards, word for word.**

Card 1, 03:25Z: "Number the steps after the Precursor steps: old numbers, or from 1?"
- "Keep the old numbers, 5 to 11" (recommended): "Old steps 0 to 4 and step 5 part 1 are Precursor steps, passed by
  your word with their merged PRs accepted, and the Road Bible goes on at step 5 (the duets), so old notes' numbers
  stay true."
- "Start again at step 1": "Precursor steps are the same and pass the same way, but the duets become step 1 and old
  steps 6 to 11 become 2 to 7, with a table from old numbers to new in the Road Bible."
- Reason given for the recommendation: "The old notes, earmarks and diaries all say step 6 and step 7. Renumbering
  would give each number two meanings, which is what rule 1 exists to prevent."

Card 2, 03:25Z: "Say what the Precursor thread is: the one builder, or a thread beside it?"
- "The one builder, named precursor" (recommended): "One thread owns all the code and shared files on main, takes the
  old diaries as its history and builds every step to come, and the name mason is dropped."
- "A second builder for the old code": "Precursor owns and fixes everything on main today while mason builds the new
  features, and every change they need in old code takes both threads and an agreement in rulings.md first."
- "Keeper of the old paper, no code": "Precursor holds the old notes, roadmap and diaries and owns no code, mason owns
  every folder, and since the rules have no such role I would write a rules change at your word."
- Reason given for the recommendation: "Every step so far changed five or more old folders, so two builders would
  spend most steps agreeing who changes what. One thread also gives every fix one clear originator."

**Stopped.** The planning thread's first Road Bible, whose steps and owners depend on both answers. No other thread is
running. R1, R3, R4 and R6 to R11 are not in question.

**Open.** Waits for the owner. The coordinator was told at 03:26Z. The wake of the review thread that a stop calls
for (rule 1, rule 6 c) has not been made: no review thread has been started yet.

## R13. Question raised: may a thread push to more than one branch?

**Raised.** 2026-11-12 03:29Z by the planning thread, to the owner, on a decision card in its thread.
**Recorded.** 2026-11-12 03:30Z by the planning thread as deputy.

**The question.** Each thread is started with one branch of its own and an instruction never to push to another
without the owner's explicit permission. The rules have a thread hold several pull requests at once: a feature that
waits for the owner's merge, the next brief (rule 3), an experiment on a branch named spike/<name> (rule 2), and a
record-only PR that takes each diary entry and each "Next:" entry to main at once (rule 5). On GitHub every open pull
request needs a branch of its own. So on one branch a thread's record entries wait behind whatever pull request it
has open. That is the case today: the planning thread's entries ride in PR #11, which waits for the owner's merge.

**The card, word for word.** 03:29Z: "Let each thread push to extra branches of its own?"
- "Allow extra branches" (recommended): "A thread can keep several PRs open at once, as rules 2, 3 and 5 expect: a
  waiting feature, its next brief, an experiment, and a record-only PR for each diary entry."
- "Keep one branch per thread": "A thread has one PR open at a time, so its diary entries, briefs and fixes wait
  behind whatever PR is waiting for your merge, and rule 5's "at once" cannot be kept."
- Reason given for the recommendation: "Each thread is given a single branch and may not push elsewhere without your
  word. On GitHub every open PR needs a branch of its own, so the rules cannot run as written on one."

**Stopped.** Nothing. Until it is answered the planning thread keeps to its one branch.

**Open.** Waits for the owner. Concerns every thread, and the coordinator for what it tells each thread it starts.

## R14. The owner's answer to R12, first question: the steps after the Precursor steps start again at step 1

**Answered.** 2026-11-12 03:48Z by the owner, by choosing an option on the first card quoted in R12. The words are
the card's, written by the planning thread; the owner chose among them and typed none.

**Recorded.** 2026-11-12 03:53Z by the planning thread as deputy.

The card asked: "Number the steps after the Precursor steps: old numbers, or from 1?"

The owner chose "Start again at step 1", which the card explained as: "Precursor steps are the same and pass the same
way, but the duets become step 1 and old steps 6 to 11 become 2 to 7, with a table from old numbers to new in the
Road Bible."

"The same" and "the same way" point at the card's other option, which read: "Old steps 0 to 4 and step 5 part 1 are
Precursor steps, passed by your word with their merged PRs accepted, and the Road Bible goes on at step 5 (the
duets), so old notes' numbers stay true."

Concerns: the planning thread (the Road Bible's steps); the inquisitor (rule 7: the owner's word passes the Precursor
steps and accepts their merged PRs); every thread that reads an old note, where a step number is an old number.

## R15. The owner's answer to R12, second question: the Precursor thread is the keeper of the old paper

**Answered.** 2026-11-12 03:49Z by the owner, by choosing an option on the second card quoted in R12. The words are
the card's, written by the planning thread; the owner chose among them and typed none.

**Recorded.** 2026-11-12 03:53Z by the planning thread as deputy.

The card asked: "Say what the Precursor thread is: the one builder, or a thread beside it?"

The owner chose "Keeper of the old paper, no code", which the card explained as: "Precursor holds the old notes,
roadmap and diaries and owns no code, mason owns every folder, and since the rules have no such role I would write a
rules change at your word."

Deputy's note: R14 and R15 together answer R12, so the stop called there is closed by the owner's ruling. This entry
adds a role that the rules do not have. By rule 1 it binds from this entry, and the planning thread writes the change
to the rules at once (rule 4: read by the review thread before it merges, then the owner's merge).

Concerns: the planning thread (the change to the rules; the cast in the Road Bible); the coordinator (which threads
it starts: one builder, "mason", and the precursor; and, by rule 1, sending this entry to every thread at once and to
every thread that starts work until the change merges); the review thread (rule 6 e).

## R16. The owner's answer to R13: a thread may push to extra branches of its own

**Answered.** 2026-11-12 03:49Z by the owner, by choosing an option on the card quoted in R13. The words are the
card's, written by the planning thread; the owner chose among them and typed none.

**Recorded.** 2026-11-12 03:53Z by the planning thread as deputy.

The card asked: "Let each thread push to extra branches of its own?"

The owner chose "Allow extra branches", which the card explained as: "A thread can keep several PRs open at once, as
rules 2, 3 and 5 expect: a waiting feature, its next brief, an experiment, and a record-only PR for each diary entry."

Concerns: every thread; the coordinator, which passes this entry on to each thread it starts, because a thread is
started with one branch and told to keep to it unless the owner says otherwise.

## R17. The owner's yes to the paragraph that adds the precursor to the rules

**Raised.** 2026-11-12 03:55Z by the planning thread, to the owner, in a message in its thread. It follows from R15,
which has the planning thread write a change to the rules.

**Answered.** 2026-11-12 03:57Z by the owner, in a message in the planning thread.

**Recorded.** 2026-11-12 04:01Z by the planning thread as deputy.

**The question, word for word.** The planning thread's message, whole:

> Got the other two as well. All three answers are recorded as R14 to R16, and the stop is lifted.
>
> **One thing from you: is this the Precursor you mean?** The rules have no such role, so this is the paragraph I
> would add to the cast, on the fourth tier beside the builders and the inquisitor:
>
> > The precursor: the keeper of the old paper, in a project that was begun before these rules took force in it. The
> > old paper is what was written for that project before then, such as its design notes, its roadmap and its
> > diaries. The Road Bible's shared list names it and says where it lies. It is outside the record and it is
> > history: where it and the record differ, the record stands. The precursor holds the old paper as it was left and
> > changes none of it. When the owner or a thread asks what the old paper says, the precursor answers from the paper
> > and cites the place, and writes the answer in its diary. It owns no code and no features, builds nothing and
> > decides nothing. Its role name is "precursor". A project begun under these rules has no old paper and no
> > precursor.
>
> Your words gave me "keeper", "holds the old notes, roadmap and diaries" and "owns no code". The tier, the answering
> of questions and "the record stands" are my additions. Say yes, or tell me what to change. After that it goes to
> Glowie to read and then to you to merge, as version 3.
>
> Meanwhile I'm starting the Road Bible, with mason as the one builder and the duets as step 1.

The owner: "Yes, go ahead."

Deputy's note: this is the owner's word for a change to the rules (rule 4). The planning thread writes the paragraph
above into the cast's fourth tier, word for word, raises the version to 3 and changes nothing else. The review thread
reads the change before it merges (rule 6 e), and it then waits for the owner's merge.

Concerns: the planning thread (the change to the rules); the review thread (rule 6 e); the coordinator (the wake of
the review thread once the change's PR is ready, and, when the change merges, telling every thread, each of which
then reads the rules again, rule 4); the precursor, once it is started.

## R18. The owner on the sister project's rules: a reconciliation later, and a small difference is fine

**Raised.** 2026-11-12 03:59Z by the planning thread, in its reply to a remark of the owner's in its thread.

**Answered.** 2026-11-12 04:02Z by the owner, in a message in the planning thread.

**Recorded.** 2026-11-12 04:06Z by the planning thread as deputy.

The owner's remark, 03:59Z: "Its funny, your sister project has made similar mutations in the Commandments. I do
suppose it was impossible to avoid on an already-established project."

**The question, word for word.** The planning thread's reply, whole:

> It fits. The rules assume a project that starts clean, and both of us had history to put somewhere.
>
> One thing to watch: each project now grows its own version 3, with different words under the same number. If you
> want the two to match, paste me the sister project's paragraph and I'll fold it in. The cheap moment is before
> Glowie reads mine, because any later change sends it back for a second read.

The owner: "We can have a reconciliation of scripture at a later time, it took ~2 days straight of Q&A on an unrelated
thread to get it out the door. A small heresy should be fine."

Deputy's note: nothing is to be done now. The change to the rules that R17 orders goes ahead in the words R17 holds,
without the other project's text, which the planning thread has not read. Bringing the two projects' rules into line
is for a later time, at the owner's word, and would then be a change to the rules like any other (rule 4).

Concerns: the planning thread; the review thread (rule 6 e: the owner has already spoken on the two texts
differing).

## R19. The owner's command that brought in the new rules: a new thread for the planning and the hand-over

**Given.** 2026-11-12 03:00:29Z by the owner, to the coordinator, in the project chat. The owner attached one file,
the rules: COMMANDMENTS.txt, kept as the project upload uploads/hearth/66303e87-c7e8-436e-b0ed-8187559f4109, 35,772
bytes, sha256 918dd508ea87e6a3305700c78e09792f336f23ab4e175882918565fbca4a5b02, byte for byte the same as
record/COMMANDMENTS.txt version 2 (commit 3ef709c). The hand-over Doc and the links to PRs #11 and #16 that the
message shows beside it are the planning thread's outputs, not the owner's attachments.

**Recorded.** 2026-11-12 05:21Z by the record thread (then named "record-keeper") as deputy (the cast, second tier),
at the coordinator's request, copied from the owner's own message.

**Changed in place.** 2026-11-12, after this entry first reached main by PR #18: PR #19 (merged 05:30Z) put the
coordinator's words back exactly as given, and PR #20 (merged 05:37Z) corrected the "Given" line, which had named three
attachments the owner never sent. The owner's words were not changed. See G28.

The owner: "Ahoy Coordinator! these are the new rules to be enforced going on, we'll need a new thread spun-up for the
planning and hand-over for the new rules."

The coordinator's words with it, exactly as given: "Finding G4 and the Road Bible (PR #16) lean on this moment."

Concerns: every thread (the moment the rules took force in this project); the planning thread (the Road Bible, G4);
the review thread (G4); the precursor, once it is started (what counts as the old paper).

## R20. The owner's word that a record-keeper takes over the coordinator's recording

**Given.** 2026-11-12 05:18:31Z by the owner, in the message that starts the record thread's own thread (then named
"record-keeper").

**Recorded.** 2026-11-12 05:21Z by the record thread as deputy (the cast, second tier): the owner gave this word to
that thread directly and did not say the coordinator would record it.

**Changed in place.** 2026-11-12, after this entry first reached main by PR #18: PR #19 (merged 05:30Z) put the
coordinator's words back exactly as given. The owner's words were not changed. See G28.

The owner: "A sister thread proposed the idea: To have a record-keeper to takeover your duties in terms of recording
your word."

The coordinator's words with it, exactly as given: "Note with it that the coordinator started the record-keeper on it at
05:18Z and has asked the owner whether the role goes into the rules and under what name."

Answered in R21: the role goes into the rules, under the name "record".

Concerns: the coordinator (its diary, finding G8); the planning thread (a change to the rules, if the owner says the
role goes into them); the review thread (G8).

## R21. The owner's command: five changes to the rules that bring in a record thread

**Given.** 2026-11-12 05:20:38Z by the owner, in the project chat. It answers R20.

**Changed in place.** 2026-11-12, after this entry first reached main by PR #18: PR #19 (merged 05:30Z) put the
coordinator's words back exactly as given. The owner's words were not changed. See G28.

**Recorded.** 2026-11-12 05:21Z by the record thread, copied from the owner's own message. The owner's message, whole
and unchanged, in a plain block so that every mark shows as the owner typed it:

```text
1. THE CAST, new paragraph after the coordinator’s:

Where the coordinator cannot reach the repo, it has a record thread: a standing thread that is the coordinator’s hands in the repo. Its role name is “record”. The coordinator starts it, and starts a replacement if it ends. A replacement takes over the name and the diary. The coordinator tells every thread when its record thread has started and when it is no longer at hand, and tells each thread it starts later whether one is at hand. A thread goes by what the coordinator last told it, and a thread the coordinator has told nothing takes it that none is at hand. The record thread writes rulings.md and player-diary.md, and in the coordinator’s own folder, record/diary/coordinator/, the coordinator’s map and diary: the owner’s words exactly as the owner gave them, and the coordinator’s as the coordinator gives them. It copies the owner’s words from the owner’s own message wherever it can open that message, and every entry that holds the owner’s words says where and when the owner gave them. Where it cannot open the message, the entry says through whose hands the words came. It builds the zips that rule 3 has the coordinator build. Wherever these rules have the coordinator write in rulings.md, in player-diary.md or in its own folder, or build a zip, the record thread does it, and what it writes there is the coordinator’s writing. It also reads the repo for the coordinator, and sends it each new entry of rulings.md, player-diary.md and findings.md, word for word. It holds no office of its own: it decides nothing, relays nothing between the threads and the owner, and wakes no one. It changes nothing in the repo but rulings.md, player-diary.md, the coordinator’s folder and its own diary folder (rule 5), and it writes none of the central books. While a record thread is at hand, a thread to which the owner gives a ruling, a command or play feedback directly sends the owner’s words, word for word, to the coordinator and to the record thread, and the record thread records them. Such a thread records them itself, as a deputy, only if the owner tells it to.

2. THE CAST, coordinator’s paragraph, one sentence added after “...unless the owner says the coordinator will record it.”:

While a record thread is at hand (below), neither of these two sentences deputizes a thread: it is deputized only if the owner tells it to do the recording itself.

3. THE RECORD, the diary line now reads:

record/diary/<thread>/: one folder per thread, written only by that thread (a deputy’s entries in the coordinator’s diary, and a record thread’s writing in the coordinator’s folder, aside).

4. Rule 1: the stop for everyone now reads “the coordinator stops every thread but itself, its record thread and the review thread”, and the paragraph after it ends with:

No stop keeps a record thread from recording. A record thread’s own doubt over what to write goes to the coordinator, and the entry in doubt stays unwritten until the coordinator answers.

5. Rule 5, one sentence added:

A record thread keeps a diary like any other thread, and no map.
```

The coordinator's words with it, exactly as given: "The owner's word on your role is attached (project chat,
2026-11-12 05:20:38Z): five changes to the rules that make you the record thread. Your role name is "record", not
"record-keeper", so your diary folder is record/diary/record/." And: "The planning thread writes the change into the
rules."

Concerns: every thread (rule 1: the coordinator sends this entry to every thread at once, and to every thread that
starts work until the change merges); the planning thread (writes the change into the rules, rule 4); the review
thread (reads the change before it merges, rule 6 e); the record thread.

## R22. The owner's word to carry on until PRs are ready for the owner's merge, or a rule needs reading

**Given.** 2026-11-12 06:02:59Z by the owner, in the project chat, after the coordinator posted the review thread's
report on PR #16 and its counter-view (G30).

**Recorded.** 2026-11-12 06:03Z by the record thread, copied from the owner's own message.

The owner: "Carry on until you're ready for me to start merging PR's, or need word on rule interpretation."

The coordinator's words with it, exactly as given: "I take it to mean: the threads keep working without waiting on the
owner, and I call the owner only when PRs are ready for the owner's merge or a question of how to read the rules needs
the owner's word. That reading is mine, not the owner's. It does not answer G26 or G30."

Concerns: every thread; the coordinator.
