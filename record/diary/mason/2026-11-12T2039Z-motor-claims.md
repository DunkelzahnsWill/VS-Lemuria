# A Crystal Motor's sockets open with the claims switch off (G16, feature 8)

**Done.** `MachinesClaims.Opens` now names the Crystal Motor beside every other machine and the Control Shelf, so with
the claims switch off a stranger may set a crystal or a Dead Socket into a Motor inside a claim, or take one out, as
the machines brief already says (Claims, "Uses by hand"; R9). Breaking it stays refused. The claims smoke check gains
a Motor: using it Granted, breaking it LandClaimed.

**Why.** The review thread found it at 20:30Z: `BlockCrystalMotor` turns the game's axles, so it is a `BlockMPBase`
and no `BlockMachine`, and PR #92's lift named only `BlockMachine`. I went over every use by hand in the mod again and
found no other gap.

**Why not another way.** No brief changes: the brief is right and the code fell short of it. The compiler warnings of
G11 in this feature's files wait for the G61 patch, so that this one carries the fix and nothing more.

**Next:** the inquisitor's verdict; then G61 (feature 8).
