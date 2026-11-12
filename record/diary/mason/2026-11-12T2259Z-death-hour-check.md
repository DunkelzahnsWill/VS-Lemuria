# The restart check of the death hour no longer races the clock

**Done.** The machines' restart check of the Gather block that heard a death failed once in three of my runs for
PR #156 (it gathered 140 of 200). The review thread (22:55Z) counts it as a flaw of mine. The cause: the death hour is
one in-game hour, two minutes of real time, and the second boot's check can come after it ends. This patch checks
what the restart must keep, the hour's end the block heard, compared with the value stored before the restart. The
full-Death gathering moves to the first boot, where it is read at once inside the hour. No game code changes, save
one read-only property for the test (`BlockEntityMachine.DeathBoostUntil`). The three scripts pass.

**Next:** the inquisitor's verdict, through the coordinator.
