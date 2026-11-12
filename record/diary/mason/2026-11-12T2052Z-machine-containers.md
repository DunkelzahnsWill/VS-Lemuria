# A machine's containers inside a claim (G61, feature 8)

**Done.** With the claims switch on, a container inside a land claim is now a machine's only if the machine stands
inside that same claim, as the machines brief has said since PR #96 (Claims, "A machine's containers"):

- a Control Shelf's input and output (`BlockEntityMachine.Input` and `OutputPos`, which every working reads);
- the container a machine with no shelf yields into: the machine now keeps every container touching it and yields
  into the first that the claim rule allows, and with none it drops its yield where the work happened;
- the container a reclamation plate puts its salt into, the plate standing in for the machine; with none allowed the
  salt is lost.

A new smoke check (`machines test containers`) puts a stranger's one-block claim over a chest beside a bare Focus
and over a shelf's input, and the plate scene boils a second time with its chest claimed. Without the fix both lines
fail (checked: the Focus yields into the claimed chest, the input is there, the salt goes in).

**Why.** The owner said to fix G61 (R27): a shelf outside a claim drew stock from an input chest just inside it.

**Why not another way.** The check sits in the three places every working goes through, rather than at each take and
put in `ShelfWork`, so no working can miss it. The shelf still listens to its input chest's changes; listening takes
nothing, and an item stays out of stock because the input reads as none. The compiler warnings of G11 still wait.

PR #125 (the Crystal Motor's sockets, G16) merged at 20:53Z on the inquisitor's "in", by my hand under rule 4.

**Next:** the inquisitor's verdict on this patch, which I expect to be "out" as it follows PR #96; then the duets'
brief (feature 10, R30).
