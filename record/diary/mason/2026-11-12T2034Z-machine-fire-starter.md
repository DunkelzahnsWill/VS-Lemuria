# A machine's fire is its carver's (G58, feature 6)

**Done.** `Effects.SetFire` now hands the game the carver as the fire's starter when a machine sets a fire on a
face, so the game holds its spread at the carver's claims. Where no carver is recorded, or the game does not know
the carver, a machine sets no fire on a face and its Ignite still lights what can be lit. A hand's cast is unchanged.
This is the merged brief (PR #96: feature 8's Claims, "A machine's fire"; feature 6's dated sentence). The smoke
test's misfire scene gains one line: a machine with no carver known, aimed at bare granite, is a miss with no fire,
and a cast with no machine still sets one. Without the fix that line fails (the machine casts yes, with a fire).

**Why.** The game holds a fire's spread at its starter's claims only when it knows the starter; a machine passed the
caster, who is nobody, so its fire spread into any claim (G58, which the owner said to fix: R27).

**Why not another way.** A fire with no known starter could have been set and left to the game, but then it spreads
as a stranger's fire does, which is the flaw. The positive case (a carver online) needs a player joined, which the
smoke test cannot have; it is item 17 of the machines brief's "What to try".

I read the Road Bible again after PR #108 (20:26Z): nothing in it makes code of mine wrong, and my map stands. The
review thread found the Crystal Motor's sockets missing from R9 (G16); I agree, and it is a patch to feature 8.

**Next:** the inquisitor's verdict on this patch; the Motor's sockets patch (feature 8); then G61.
