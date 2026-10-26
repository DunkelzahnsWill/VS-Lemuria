# Lemuria: notes for Claude sessions

Read README.md first; it is the source of truth for building, testing and the parallel-work layout.
The design is the pre-production document ("Lemuria: pre-production", linked from the project
memory); its settled decisions are not reopened in a build thread. A question about the design goes
to the planning thread, not into code.

## The Commandments (the owner's standing rules)

1. **Stop the line.** Any confusion mid-process (a term with two meanings, a rule you cannot find, an
   ask with two readings) stops all hands. Tell the coordinator, who halts every other thread and
   tells the owner. Nobody resumes until it is settled in writing. Threads whose decisions touch
   each other talk directly or through the coordinator.
2. **No code before the concept is settled.** Pre-production irons out every fuzzy spot first. Ask
   the natural follow-up questions (how it is implemented, what an action leads to) then, not mid-build.
3. **Play-test early.** Ship a playable zip to the owner as soon as the prototype loads, and hold
   feature work the owner has not yet seen in play.
4. **Git goes through pull requests only.** Never push to `main`. A small fix inside one feature's
   files with no design change may be set to auto-merge on green CI; anything larger waits for the
   owner's merge. Threads never merge by hand or publish. Assign every PR to the owner
   (`DunkelzahnsWill`), push every fix before calling a PR ready, and explain Git steps plainly.
5. **Keep a diary per thread** at `/mnt/project-files/diary/<thread>.md`: one dated entry per PR or
   decision with what was done, why, and why not another way.

## Repository rules

- Before opening or updating a pull request, run `scripts/setup.sh`, `./build.sh` and
  `scripts/smoke-test.sh`, and say in the PR what passed. CI runs the same three scripts as the
  `build` check.
- Stay inside your feature's files (README, "Working in parallel"): your own `Lemuria/<Feature>/`
  folder and ModSystem, `lang/<feature>/en.json`, your feature's asset folders,
  `patches/<feature>-*.json` and `tests/smoke/<feature>.*`. Changes to shared files go in their own,
  clearly described commit.
- Never commit game DLLs, the server, or anything under `.cache/` or `Releases/`.
- Prefix every registered code with `lemuria` (`LemuriaModSystem.ModId`).
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
