# Lemuria

*The Song of Stone.* A crystal and stone tech mod for Vintage Story 1.22.7: grow resonant crystals,
refine them through their alignments, and wake sparkstone to work for you.

The mod is in early development. This version is an empty mod that loads; the design lives in the
pre-production document, and features arrive one step at a time, each with a play-test.

| Field | Value |
| --- | --- |
| Mod id and asset domain | `lemuria` |
| Type and side | Code, Universal (required on client and server) |
| Game | 1.22.7, .NET 10 (`net10.0`) |
| Based on | the official `VintageStory.Mod.Templates` 1.22.0, by way of [Walkers](https://github.com/DunkelzahnsWill/VS-Walkers) |
| License | [MIT](LICENSE) |

## Build and test in the cloud (or any Linux x64 machine)

```bash
scripts/setup.sh        # .NET 10 SDK if missing, and the 1.22.7 dedicated server (MD5 and SHA-256 checked) in .cache/
./build.sh              # validate JSON and modinfo.json, build, pack Releases/lemuria_<version>.zip
scripts/smoke-test.sh   # boot a headless server with that zip; fails on any error in the log
```

Setup takes about a minute the first time (a 54 MB download) and skips finished steps afterwards.
The build takes about 10 seconds, the smoke test under a minute.

- **Game DLLs.** The mod compiles against the DLLs of the game folder named by the `VINTAGE_STORY`
  environment variable. When it is not set, `Directory.Build.props` points it at the server that
  `scripts/setup.sh` unpacks, so plain `dotnet build Lemuria/Lemuria.csproj` also works. The DLLs
  and the server are never committed (`.cache/` is ignored).
- **Pinned versions.** The game version, download URL and both checksums live in
  `scripts/versions.env`. The setup refuses to unpack a download whose checksums differ.
- **Build steps** (`CakeBuild/Program.cs`, run with `./build.sh --target=<step>`):
  `ValidateJson` parses every asset JSON and fails on syntax errors, a key repeated in a lang file,
  and a lang key defined in two lang files (compared the way the game merges them). A key repeated
  in any other asset is only a warning. `ValidateModInfo` checks the modid, version format, type,
  side, game dependency, and that the only asset folders are `lemuria` and `game`. `Build` publishes
  the DLL and `Package` writes the zip. Add `--configuration=Debug` for a debug build.
- **Smoke test.** It copies the zip into a fresh server data folder, boots the server with a fixed
  world seed, types any console commands from `tests/smoke/*.commands`, then sends `/stop`. It fails
  unless the server stops cleanly, `lemuria` is in the loaded mod list, every pattern in
  `tests/smoke/*.expect` matches a line of `server-main.log`, and the logs hold no `[Error]` or
  `[Fatal]` line, no `[Warning]` line that mentions the mod, and no crash log. When there are
  `tests/smoke/*.restart.commands`, it boots a second time on the same world, types those in, and
  checks `tests/smoke/*.restart.expect` against that boot's log (`data/Logs-restart/`), so a feature
  can check what it saved. The world and logs stay in `.cache/smoke/` afterwards.
- **CI.** `.github/workflows/build.yml` runs the same three scripts on every pull request and every
  commit on `main`; its check is called `build`. Every run that gets as far as a zip keeps it as an
  artifact named `lemuria_<version>.zip`, also when the smoke test fails, and every run keeps the
  server logs (artifact `smoke-test-logs`).

## Play-testing on your PC

The cloud covers building and the server side. Your PC is needed for anything a player sees or hears.

1. Get a mod zip: download the `lemuria_<version>.zip` artifact from the pull request's GitHub
   Actions run (Checks, then the run, then Artifacts), or build it yourself with the .NET 10 SDK and
   `./build.sh` (`.\build.ps1` on Windows) after pointing `VINTAGE_STORY` at your game folder.
2. Copy `lemuria_<version>.zip`, still zipped, into the game's `Mods` folder
   (`~/.config/VintagestoryData/Mods` on Linux, `%APPDATA%\VintagestoryData\Mods` on Windows), start
   the game and check that the Mod Manager lists Lemuria.
3. For quick iteration, the launch profiles (`.vscode/launch.json`, and
   `Lemuria/Properties/launchSettings.json` for Visual Studio and Rider) start the game with the
   repository's build and asset folders, so JSON edits show without packing a zip.

When something goes wrong, `client-main.log` in the game's `Logs` folder is the file to share.

## Working in parallel

Each piece of work happens on its own branch with a pull request to `main`, and the `build` check
must be green before merging. The layout lets branches stay out of each other's files:

| What | Where it goes |
| --- | --- |
| C# code | its own folder, `Lemuria/<Feature>/` |
| Startup | its own `ModSystem` in that folder, e.g. `Lemuria/Crystals/CrystalsModSystem.cs`. The game finds every ModSystem on its own, so nothing needs registering in a shared file |
| Text | `Lemuria/assets/lemuria/lang/<feature>/en.json`. The game merges every `en.json` under `lang/`, and the build fails if two files define the same key |
| Assets | `Lemuria/assets/lemuria/<type>/<feature>/` (blocktypes, itemtypes, shapes, textures, sounds) |
| JSON patches | `Lemuria/assets/lemuria/patches/<feature>-*.json` |
| Smoke checks | `tests/smoke/<feature>.expect` and, if needed, `tests/smoke/<feature>.commands`; checks after a restart in `tests/smoke/<feature>.restart.commands` and `.restart.expect` |

Shared surfaces: `Lemuria/Core/` (startup and identity, `LemuriaModSystem.ModId`), `modinfo.json`
and `Lemuria/Lemuria.csproj` (the project already references every game assembly a feature should
need, Harmony included; the version changes only at release), `lang/en.json` (general keys such as
the mod name), the scripts and CI. A change to a shared file goes in its own, clearly described commit.

Conventions that keep the mod compatible with others: everything under the `lemuria` domain, every
registered code prefixed with the mod id, chat commands under one `/lemuria` root, network channels
named `lemuria-<feature>`, additive patches, and Harmony only where it adds a capability the game's
API does not offer, never a prefix that cancels another mod's patch.
