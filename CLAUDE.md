# Lemuria: notes for Claude sessions

**The standing rules are the Commandments, `record/COMMANDMENTS.txt`. Read them in full before anything
else.** Where this file or `README.md` seems to say otherwise, the Commandments and the Road Bible stand,
and the coordinator is told so the file can be put right. This file only points the way and keeps the
repository rules below.

- **What you own** comes from the Road Bible, `record/road-bible.md`: part 3 gives each
  feature its number, owning thread and folders, part 4 every shared file. Outside `record/`, change only
  what you own there or have been handed under the rules. Your role's name comes from the coordinator,
  and the standing threads' offices from the Commandments' cast.
- **The record** lives in `record/`. Each time you start work, read what rule 5 lists: the rules, the
  Road Bible, the briefs of the features you will work on (`record/briefs/`), your map and your diary from
  your last "Next:" entry, and the new entries of `rulings.md`, `player-diary.md` and `findings.md`. Keep
  your own folder, `record/diary/<role>/`, one file to an entry, but where rule 5 says otherwise (the
  review thread's diary is `findings.md`, and the record thread writes the coordinator's folder). The old design notes, the roadmap and the
  old diaries in `/mnt/project-files/diary/` are old paper: read-only history, which the record outranks.
- **Every change reaches `main` through a pull request; never push to `main`.** How each kind of PR may
  merge is rule 4, and that list is the authority. Threads never merge by hand and never publish. Assign
  every PR that waits for the owner's merge to the owner (`DunkelzahnsWill`), but a PR the review thread
  is woken to read only once its description cites the review's report, as rule 4 says; and explain plainly any Git
  step the owner must take.
- **No code for a feature before its brief is settled** (rule 2), experiments on `spike/<name>` aside. One
  unmerged feature per thread at a time (rule 3).
- A step number in the code's comments, the smoke tests and this repo's older text is an **old** step
  number (Road Bible, part 3, "Old step numbers and new"). Write "old step N" for one.

## Repository rules

These bind whoever writes code in the repo (Road Bible, part 4).

- Before opening or updating a pull request, run `scripts/setup.sh`, `./build.sh` and
  `scripts/smoke-test.sh`, and say in the PR what passed. CI runs the same three scripts as the
  `build` check.
- A feature's files are the folders part 3 of the Road Bible names for it: its own `Lemuria/<Feature>/`
  folder and ModSystem, every folder named for it under `Lemuria/assets/lemuria/` (such as
  `lang/<feature>/`), `patches/<feature>-*.json` and `tests/smoke/<feature>.*`. Code that a feature needs
  in another feature's folders follows the change to that feature's brief, and then either goes as a
  patch of its own or rides in the feature's PR, whose description names each other feature it changes
  and the merged change to that feature's brief (rule 4). Changes to shared files go in their own,
  clearly described commit.
- Never commit game DLLs, the server, or anything under `.cache/` or `Releases/`.
- Prefix every registered code with `lemuria` (`LemuriaModSystem.ModId`). Network channels are named
  `lemuria-<feature>`.
- Chat commands hang off the one shared root: `api.ChatCommands.GetOrCreate(LemuriaModSystem.ModId)`
  plus your own `.BeginSubCommand("<feature>")`; never `Create` the root.
- Harmony is allowed where it adds a capability the game's API lacks (unlike Walkers). Use one
  Harmony id per feature (`lemuria.<feature>`), unpatch it in `Dispose`, prefer postfixes, and never
  write a prefix that cancels another mod's patch. Say in the PR what each patch is for.
- Multiplayer-safe from the first line: server-authoritative state, sync to clients through the
  game's own channels, and nothing that only works in single player. Keep server time and zip size small.
- Target game 1.22.7 and the server pinned in `scripts/versions.env`. Use legacy AI task codes in
  JSON; the `-r` refactored tasks have known bugs.
- The repository is public. Never commit secrets, and never publish a release or a ModDB page; the
  owner does that.
