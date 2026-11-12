# A Gather block keeps its strike and death hours through a restart (G13, G14)

**Done.** `BlockEntityMachine` now saves and loads `skyBoostUntil` and `deathBoostUntil`, as a standing crystal and a
cradle already do. The smoke test `gatherstrike` stores the Gather block struck under open sky and adds a fourth
Gather block, with a Death crystal, that hears a death beside it; after the restart the new `struckkept` reports what
each gathers in a day. With the fix both gather 200 of 200; without it (checked by running the smoke test on the old
code) both gather 0 and the smoke test fails.

**Why.** The machines brief says both hours are kept through a restart or an unloaded chunk (G13, G14), so the patch
only makes the code do what the settled brief says, inside feature 8's folders. The test is the old audit's unfinished
one, with the death hour added, since G14 had no test at all.

**Why not another way.** Reading the boosts back from the crystals in the sockets would tie a block's hours to its
crystals, while the brief gives them to the Gather block; saving two doubles on the block entity is what the other two
holders do.

**Next:** the PR waits until PR #72 (the briefs' review points) merges, as the review thread asked, then goes to the
inquisitor; then the G15 patch.
