# Brief: feature 8, `machines`

**Feature.** Number 8 of the Road Bible's part 3, short name `machines`, owned by mason (R5, R15). Built in old step 4
(all three parts), old step 5 part 1, and the old audit's fixes (PR #10).

**Its folders.** `Lemuria/Machines/`; every folder named `machines` under `Lemuria/assets/lemuria/` (the machine,
Control Shelf, Memory and reclamation plate block types, the Crystal Rod's and Dead Socket's item types, the grid
recipes, lang, shapes, sounds, textures); `Lemuria/assets/lemuria/patches/machines-crystal.json`, which gives feature
1's crystal block the making of the rod; and `tests/smoke/machines.*`.

**Written from.** The signed-off notes as built, play-test changes included (R3): "Lemuria step 4: Machines" (questions
1 to 9, F1 to F8, the four play-test reports of Oct 28), "Lemuria step 4 part 2: Products and logistics" (questions 1
to 12, G1 to G6, H1 to H6, I5, the silo of Nov 2), "Lemuria step 4 part 3: Elemental batteries and Wind Probes"
(questions 1 to 12, J1 to J6) and the machines of "Lemuria step 5: The Four Mysteries" (question 11, K4, K5, and the
owner's word of Nov 10 on a Death Gather), checked against the code on main at commit a1aae13. Where this brief and the
old paper differ, this brief is the record.

## What the player sees and does

### Carving, and what every machine shares

1. **Seven carved forms.** A left-click of the Crystalline Chisel (feature 4) on a placed Awakened block carves the
   face struck into a machine's front, first with the Gather glyph; each further strike on that face carves the next
   form, Refine, Focus, Scatter, Amplify, the Crystal Motor, the Wind Probe, and then Free stone again, eight strikes
   round. Carving is free, works with an empty chisel and rings softly. A block with crystals in its sockets does not
   recarve; striking another face of a machine does nothing. Codes: `lemuria:machine-{form}-awakened-{rock}-{side}`,
   `{form}` one of gather, refine, focus, scatter, amplify, motor, probe, and `{side}` the front.
2. **Still sparkstone.** A machine is Awakened sparkstone: it joins the network it touches, holds its 50 share (a
   Motor holds none of its own), glows and hums with the network, and has room for 4 with sockets on its front only
   (feature 4). A Motor's sockets and glyph are on its rear, opposite its hub; a Probe's are always on its top. Its
   glyph is a lit cyan pictograph of its form: Gather a spiral drawing in, Refine a crystal in a ring, Focus an eye,
   Scatter an eye with rays, Amplify three nested rings, the Motor a wheel. Its other faces carry one plain machine
   frame, so every face says Free or machine at a glance.
3. **Mined and set down.** A machine drops itself, form kept, and its crystals beside it. Set down, it is Active and
   its front is the face toward you, so you stand where a Focus will look (a Motor's sockets toward you).
4. **Charge.** A machine draws from the network of Active sparkstone it touches. The unit is the *working*, one
   crystal's turn: 1 charge at Glittering's power, 2 at Glowing's, 4 at the power of Luminous (Actions-to-Charge, old
   step 4 part 2). Nothing done, nothing paid. A network that cannot pay leaves its machines idle. Machines pause
   while their chunk is unloaded and do not catch up.
5. **Context.** A machine works out what touches it (an Amplify, Memory, a shelf, a container, its network) only when
   it or a block touching it is placed or removed, and on a rod tap: it never polls.

### The Crystal Rod

6. `lemuria:crystalrod`: hold right-click for a second on a placed Unaligned crystal with a block of polished rock in
   hand; the crystal and one rock are used up (the owner's recipe, F6). A short click does nothing. It holds no charge
   and never wears out.
   - Sneak and right-click any Awakened block: Active or Dormant (feature 4), with a rising or falling chime.
   - Right-click a face of a machine: that face becomes its front, crystals and all (a Motor's sockets go to the face
     clicked). A Free block has no front to turn.
   - Right-click a crystal in a machine's socket: it is *Dormant* and sits out its turn, its plug dark, until the next
     right-click (part 2's question 1: "Dormant", never "rest", which is the cradle's).
   - Every tap makes the machine read what touches it again.

### The verbs

7. **Gather.** Each aligned crystal gathers from its alignment's environment (feature 1's 5×5×5 cube and purity test)
   straight into the network, never into itself: 200 charge an in-game day at full strength for Glittering and 600
   for Glowing, less in proportion. A lightning strike within 16 blocks gives its Sky crystals six hours of full
   strength, if the Gather stands under open sky when it lands and while nothing is overhead; a death within 8 blocks
   gives its Death crystals full Death for an in-game hour (the owner, Nov 10 2026). Both are kept through a restart
   or an unloaded chunk, as a standing crystal's and a cradle's are (G13, G14). When this brief was written (main at
   a1aae13) they were not saved, so a restart ended them; the fix follows this brief as a patch (G41; PR #45
   merged on 2026-11-12). Unaligned crystals gather
   nothing, and what the network has no room for is lost.
8. **Refine.** Its aligned crystal is its pattern: every Unaligned crystal in the same block, of the pattern's
   Evolution or lower, refines into the pattern's alignment, keeping its Evolution, anywhere, in 1 in-game day for
   Glittering and 2 for Glowing, for half the new crystal's capacity (50 or 100), drawn as it goes; it stalls while
   the network is empty. Sparks run from the pattern; a finished crystal chimes and becomes a pattern itself. Aligned
   crystals that differ refine nothing.
9. **Focus.** It looks straight out of its front at the first block in its line within reach, 8 blocks at Glittering,
   12 at Glowing and 16 at Luminous's power, and works on that block at Glittering, the flat 3×3×1 carpet around it at
   Glowing, and everything within 1 block at Luminous's power. It works once every 10 seconds for each crystal, the
   crystals taking turns, each doing its own evocation's work (feature 6) for one working:
   - *Fire*: lights what can be lit (firepits, forges, pit kilns, charcoal pits, coal piles, ovens, bloomeries,
     torches, bombs) or sets a fire on the face it aims at; from Glowing sets the creatures and players in its area
     alight; melts ice and snow; at Luminous's power boils water, and melts a 2×2×1 of raw rock into running lava. Like
     a cast, it pays only for what caught (feature 6, G15).
   - *Ice*: puts out fires and burning creatures and players, freezes still fresh water, cools lava, waters tilled
     soil; at Luminous's power packs ice into glacier ice, then packed ice.
   - *Earth*: facing down, raises Tremor's 2×2 once, 2, 4 or 6 blocks by power, fed by the ground beside it sinking and
     lifting whoever stands on it, then switches itself Dormant, so a tower grows tap by tap. Facing sideways or up it
     *bores*: the 2×2 (the block aimed at with those to its right and above, as you face the wall; north and east when
     facing up) moves 2, 4 or 6 blocks into the face, its ground going into the touching container as itself, all or
     nothing, and the Focus goes Dormant. Out of reach or blocked, it warbles and goes Dormant at no cost.
   - *Sky*: pushes creatures, players and loose items away, about 6, 8 or 10 blocks; harvests leaves, cattails,
     papyrus, tule and mushrooms; pans pannable blocks at Gale's half chance; at Luminous's power harvests ripe crops,
     each replanted, and berries and fruit. Aimed at a tree's log it *fells* the tree through the game's own felling,
     as an axe does (only what an axe fells as a tree), its logs, sticks and saplings into the container, for one
     working for each 1, 9 or 27 logs by power, in full or not at all, leaves free.
   - *Life, Death, Time, Rust* (feature 9's crystals): Bloom, Wither, Rewind and Progress, by the rules by hand. A
     plant takes Bloom once an in-game day, hand or Focus (K4); a Death Focus with a Glowing crystal withers what
     stands in front of it (K5); a Time or Rust Focus works a container only when it has a full measure to move.
   - *Unaligned*: Quicken (below), and the shelf's work (below).
   Each working sends a short streak of sparks in its crystal's colour to the target and plays its evocation's sound
   there, softer than a cast.
10. **Scatter.** As a Focus, but on a flat 9×9 centred on the first block in its line, each crystal working one
    Evolution of power below a Focus, for the same charge: a Glowing crystal at Glittering's power, with an Amplify at
    Glowing's. A Glittering crystal does nothing in a Scatter, even amplified ("They are too weak", the owner's play).
    Two exceptions: Sky harvests crops, berries and fruit one Evolution sooner, so a Glowing Sky crystal with an
    Amplify harvests a 9×9 field; and Earth tills the square into farmland where nothing stands on it, raising no
    ground. Fire sets a fire only on the block it aims at. A Scatter does not fell.
11. **Amplify,** a *module* (part 2's G5). It lifts every Focus and Scatter it touches face to face by one Evolution of
    power, and doubles the torque of every Motor it touches, each for twice the charge, but only for workings of the
    Evolution of a crystal in its own sockets or lower; an empty Amplify lifts nothing. Its crystals do no work of their
    own, and one Amplify is the most a machine takes. It does nothing for Gather and Refine.
12. **The Crystal Motor.** It turns the axle, gear or quern at its front through the game's own mechanical power, while
    it is Active and its network pays. Each crystal of any alignment gives torque: Glittering 0.25 (one length of
    windmill sails, shown as 20 kN), Glowing 0.75, at a steady speed 0.5, day and night, indoors or under ground; an
    Amplify doubles it. It draws 2 charge an in-game hour for each Glittering crystal and 6 for each Glowing one, twice
    that amplified, and slows to a stop when the network runs dry. It stores no charge of its own: each aligned crystal
    in it brings its store (100 or 200) and its charge, as a capacitor does, so a full Motor holds 400, and cut off it
    turns on that store ("like little batteries", the owner). Motors on one axle line add up; a quern it turns grinds
    at about half the pace of grinding by hand.
13. **The Wind Probe.** Its Sky crystals stand on its top and gather from the weather where it stands, only under open
    sky: for each crystal 400 charge an in-game day at Glittering and 1,200 at Glowing, times a quarter plus the game's
    wind there (0 in still air, up to 1 in a storm, up to half again higher sixty blocks above the sea) plus half the
    rain or snow on it. It reads no surroundings, so twins and purity do not touch it. It draws lightning as the game's
    lightning rod does, with the rod's own numbers (12 blocks around on flat land, up to 40 from a tall mast); a strike
    fills each crystal to capacity, poured into the network first as there is room. Other crystals in it sit idle. It
    is a source on the *energy line* (J2): at the same Evolution, each newer source brings more on average than the
    one before.
14. **Quicken,** the first secret of the Hidden Vocabulary. An Unaligned crystal in a Focus aimed at a cradle with a
    growing seedling (feature 2) moves it on, each working, by as much growth as 10 seconds bring for Glittering,
    twice that for Glowing and four times at Luminous's power, for one working. A Glowing Focus quickens every cradle
    in its carpet; a Scatter quickens every one in its square, one Evolution lower. A ripe, empty, refining or mending
    cradle is not quickened. A cradle comes before the shelf for an Unaligned crystal.

### Products and logistics (old step 4 part 2)

15. **The Control Shelf,** `lemuria:controlshelf-{side}`, made in the grid from polished rock over a stick. Set against a
    side of a Focus or Scatter (north, east, south or west, never its front), one to a machine. As you face it, the
    container at its left is its *input* and the one at its right its *output*, both outside the machine; a container
    above or below it is not its port. Glyphs on its sides and small on its roof's front mark the input (two points
    meeting) and the output (two points parting).
    - It holds 1 to 4 items, each standing upright in its own place, shrunk to fit, a tool turned to show its outline,
      put on and taken off with a right-click.
    - On its turn an Unaligned crystal in the machine, and only an Unaligned one (G1), does the work of the first item
      from the left that has something to do in its reach (a working's reach, as for its Focus or Scatter), as one
      click of yours on each block or creature there, for one working. Seeds sow, saplings plant, fertilizer feeds
      farmland until it will take no more, a shovel digs loose blocks, a bucket milks into a barrel as its output, an
      axe splits the input's logs (4 firewood to a log), firewood stacks piles and builds a charcoal pit's firepit,
      and a firestarter lights what one lights (a charcoal pit, a cold firepit with fuel, a forge, a bloomery, a pit
      kiln, a clay oven, a boiler, a torch gone out, other mods' blocks that take the game's firestarter), never a pile
      on the ground or a bomb. The game lights a forge and its like only for a player, so the machine borrows its
      carver, or the nearest player online, and waits while nobody is online. It pays only for what caught.
    - Stock comes from the input, and only of the kind on the shelf; yields go to the output; a product the shelf
      itself uses goes back to the input as stock (G2). Tools never wear.
    - *Prefixes* (H1): an item that names no action, just left of a shovel or an axe, keeps that tool to its own kind
      (charcoal before a shovel digs only finished charcoal; an oak log before an axe splits only oak). It is never
      used up and never switched.
    - *Dormant items* (H2): right-click an item with the rod to switch it off or wake it.
    - *Quiet items* (H4): an item goes quiet by itself when it is out of stock, and wakes by itself when its kind next
      arrives in the input (the container's own change event, so nothing polls); or when it is done, with nothing to do
      in its reach, and then stays quiet until a rod tap. Sneak and right-click the shelf with the rod: the shelf and
      every item wake together (I5).
    - *Plugs*: each place has a plug in the ledge below it, lit cyan while its item is Active and dark while it is
      Dormant, quiet or stopped; looking at the shelf says why each dark plug is dark.
    - *The shelf's socket* (H5, H6): one in the middle of its top takes a Dead Socket; with one there, an item out of
      stock sends the whole shelf Dormant, every plug and both glyphs dark, until that stock arrives or a wake. An item
      that is done does not.
16. **Containers.** A machine with no shelf yields into any container touching it, or drops its yield where the work
    happened when none touches it. When a yield would not fit, the machine goes Dormant without working, and a rod tap
    wakes it once there is room (part 2's question 7). Chutes and hoppers count as containers. Tubes are the game's own
    chutes and hoppers, with an Archimedes screw turned by a Motor to lift goods.
17. **Charcoal's second rung, and the silo.** The game's charcoal pit burns only when its piles and firepit are closed
    in by solid blocks that do not burn, so a machine's pit is built under a roof. Firewood on the shelf adds 2 to each
    pile in reach that is not full and starts a pile in each empty cell standing on solid ground or a full pile, keeping
    the cell straight in front of the machine for last when a block roofs it, and there builds the firepit. The gate,
    the *silo*: a chamber under a stone roof with a one-block shaft through the roof's middle, the machine on the roof
    over the shaft facing down, its shelf and containers on the roof beside it, its shelf running axe, shovel,
    firewood, firestarter. The chamber is as big as the reach: 3×3×1 for a Focus with a Glowing Unaligned crystal,
    3×3×3 amplified by a Glowing crystal, 9×9×1 for a Scatter. A 3×3 burn takes about 75 logs. After the game's 18-hour
    burn, one rod tap wakes the shelf, the shovel digs the charcoal and the next pit is stacked. The owner's own silo
    run of Nov 2 chopped, stacked, burned and dug (the old diary of old step 4 part 2; G19 is answered below).
18. **Memory,** a module, `lemuria:memory-awakened-{rock}-{side}`, made in the grid, shapeless, from a block of Awakened
    sparkstone and any Glowing crystal, both used up. Set against any face of a Focus or Scatter but its front, one to
    a machine, it holds a magazine of 2 crystals (4 sockets' room), set in and taken out as sockets are. After each
    turn the crystal that just worked moves to the bottom of the magazine and the first one cycles into its place, where
    it fits; a Dormant crystal is passed over, and turning costs nothing. A stone working a battery does nothing else,
    so Memory against a fill stone stays still. Memory widening a machine's commands, the duets, is feature 10's.
19. **The reclamation plate,** `lemuria:reclamationplate`, made in the grid from a copper plate over polished rock: a
    thin plate on the floor under salt water, with no charge and no crystal. When a Fire Focus, not a hand cast, boils
    salt water anywhere in the column above it, it claims the game's own salt 1 time in 4 for each block boiled, into a
    container, chute or hopper touching it; with nowhere to put it the salt is lost.
20. **The Dead Socket,** `lemuria:deadsocket`, made in the grid from copper nails and strips over polished rock: a
    fitting for one socket of a Motor, giving a Glittering crystal's torque, 0.25, for 4 charge an in-game hour from its
    line, an Amplify doubling both. It stores nothing and stops as soon as its line is cut, empty or Dormant. In the
    shelf's socket it is the shelf's stop (above); in any other socket it does nothing.

### Elemental batteries (old step 4 part 3)

21. **The room.** A battery is the box between two stones: a *drain*, a Gather facing into one bottom corner, and a
    *fill*, a Focus facing into the top corner opposite. From 2×2×1 to 7×7×7, every block in it is the medium or its
    charged form and every block touching it from outside is solid, glass included; otherwise both stones rest and say
    why. Any other Focus or Gather of that alignment facing into the room works it too. A stone working a battery does
    only that.
22. **Its Elemental.** The fill's aligned crystals name it, Fire, Ice or Earth, and crystals of any other alignment in
    either stone sit idle (J3, J4); a fill with none, or with two Elementals, names none, and the battery rests. A
    battery holding a count stays its Elemental until the count is spent. Sky and the Mysteries name no battery.
23. **Filling and draining.** The fill takes only what lifts its network above three quarters; the drain pays back 4 in
    5 of what was stored, only while its network is below half, never past half. Each stone moves up to 200 an in-game
    hour for each Glittering crystal and 600 for each Glowing one. The two stones may sit on one network or two. A rod
    tap makes either stone Dormant. Both stones show the count when looked at ("Stored 1,200 of 5,400"), so either can
    be broken and set back. Each block holds 200: a 2×2×1 battery holds 800, a 3×3×3 one 5,400, a 7×7×7 one 68,600.
    - *Fire*: raw rock, the world's own or quarried whole; a Fire Focus holding a Glowing crystal with an Amplify melts
      it into lava from the bottom layer up, and the drain cools it back into the rock each block was, from the top down.
      Lava sets fire to wood within two blocks, as the game's own does.
    - *Ice*, inverted: still fresh water, which an Ice Focus of any Evolution freezes into glacier ice from the top down,
      and the drain melts back, the last frozen first.
    - *Earth*: an Earth Focus feeds charge into its weak blocks (sand, gravel, cracked rock, cobblestone), up to 200
      each; ore, rock, cracked rock and gravel are pre-charged, wherever they came from (J1, J5). The drain spends fed
      charge first, then wears the room one block at a time, nearest first: ore to rock, rock to cracked rock, cracked
      rock to gravel, gravel to sand, 50 a step at a Gather block's pace (ore 200, rock 150, cracked rock 100, gravel
      50). Byproducts go into a container touching the drain: the ore's pieces, and two stones when cracked rock wears
      to gravel; gravel wears to sand with no finds, since panning is Sky's (J6). With no container, or a full one, it
      wears nothing and pays only fed charge. Its stones also show what the deposit still holds.

### The Journal

24. Carving the first machine opens chapters 5, "Crystal Machines" (13 pages), 6, "The Control Shelf" (8), and 8,
    "Batteries and Probes" (3), together, and the first quickening opens the hidden page "Quicken" for the carver
    (feature 3's unlock rules). Their text is this feature's, in `lang/machines/`: instructions only, what to build and
    set, never how the player gets about to do it (the owner, Nov 2 2026); the silo's build step by step; and Crystal
    Machines' last page hints at the hidden verb without naming it.

## How it is built, and what it touches

- `MachinesModSystem`; `BlockMachine` and `BlockEntityMachine` (on feature 4's `BlockEntitySparkstone`, with the
  partials `.Battery` and `.Probe`), `Machine`, `MachineCarver` (the chisel's hand-off, `StoneCarver`), `MachineWork`,
  `Felling`, `Boring`, `BEBehaviorCrystalMotor` (on the game's own rotor, `BEBehaviorMPRotor`), `Battery`,
  `ControlShelf`, `ShelfWork`, `Stock`, `BlockEntityMemory`, `ReclamationPlate`, `ItemCrystalRod` and
  `BlockBehaviorRodMaker` (the rod's making, on the crystal block through the patch). No Harmony.
- It reads feature 1 for crystals, environments and dialogues; feature 4 for networks, sockets and the chisel; feature
  2 for Quicken (`BlockEntityCradle`'s growth call); feature 6 for every working (a Focus does exactly what a cast
  does, with the machine as its source); feature 9's evocations reach it through feature 6. It raises the events
  feature 3 listens to: a machine carved, a cradle quickened.
- Saved: each block's crystals with charge and cracks, Active or Dormant and Dormant crystals, a Refine block's
  progress, shelf items and whether each is Dormant or quiet and why, Memory's crystals, Dead Sockets, a battery's
  count, the rock each Fire block was and the block an Earth drain wears; a machine's form and front are in its code.
- **Cost.** A Focus or Scatter casts one line every 10 seconds for each crystal, four at most, and one working touches
  at most 27 blocks in a Focus or 81 in a Scatter; a Gather reads its surroundings every 30 seconds; a Refine block
  steps on the network's 10-second tick; Motors turn on the game's mechanical power tick; a battery stone checks its
  room, at most 343 blocks, only when it works; a quiet shelf item costs nothing; a full output sends its machine
  Dormant. Each paid working walks its whole network twice, once to check and once to pay (G17): noted, not a fault
  while no base comes near it.
- Every working is claim-checked block by block before it changes the block it works on, and the game's audit log
  records each fire a machine starts and each block of rock it melts under its carver's name.
- A machine pays from its whole network, which may reach into a claim: a network takes no account of a claim's edge
  (feature 4's brief, Claims; the owner declined G68, R29). Built.
- When this brief was written (main at a1aae13), the smoke test's Motor check failed by chance on a slow runner,
  reading the quern's speed only once (G43); PR #61 fixed it, merged on 2026-11-12.

## Settings

`MachineWorkSeconds` 10, `MachineCostShare` 0.1, `AmplifyCostMultiplier` 2, `ScatterSize` 9, `GatherPerDayGlittering`
200, `GatherPerDayGlowing` 600, `MachineRefineCostShare` 0.5, `MotorTorqueGlittering` 0.25, `MotorTorqueGlowing` 0.75,
`MotorSpeed` 0.5, `MotorChargePerHourGlittering` 2, `MotorChargePerHourGlowing` 6, `QuickenShare` 1, `SaltChance`
0.25, `DeadSocketTorque` 0.25, `DeadSocketChargePerHour` 4, `ProbePerDayGlittering` 400, `ProbePerDayGlowing` 1200,
`ProbeCalmShare` 0.25, `BatteryChargePerBlock` 200, `BatteryReturnShare` 0.8, `BatteryPerHourGlittering` 200,
`BatteryPerHourGlowing` 600, `BatteryFillAbove` 0.75, `BatteryDrainBelow` 0.5, `EarthChargePerStep` 50,
`BatteryMaxSide` 7. Reaches, areas, room and the one-Amplify limit are not settings.

## Claims

- **What a machine does.** With the claims switch on, a machine changes nothing inside a land claim, no block, creature
  or player, unless it stands inside that same claim. Lava it makes runs as natural lava does; only the rock it melts
  is claim-checked. With the switch off, claims stop no machine. Built.
- **A machine's fire** (the owner, R27: G58 "Fix"). A fire that a machine sets on a face is its carver's, as a fire
  set by hand is the caster's: the game's fire spreads by itself and does not spread to where its starter may not
  build, so a machine's fire does not spread into a claim its carver could not build in, whatever the switch says
  (as for a hand's fire, feature 6). The game knows a player only once they have joined since the server last
  started: a fire that burns on through a restart is held only once its starter has joined again, a hand's fire
  likewise. Where the game does not know the carver (one who has not joined since the server last started, or a
  machine with no carver recorded), the machine sets no fire on a face: it still lights what can be lit, as a
  firestarter does. When this brief was written (main at a1aae13), a machine's fire had
  no starter, so the game held its spread at no claim, with the switch on too (G58). The fix follows this brief as a
  patch.
- **A machine's containers** (the owner, R27: G61 "Fix"). With the switch on, a container that lies inside a land
  claim is a machine's only if the machine stands inside that same claim: a Control Shelf's input and output, the
  container a machine with no shelf yields into, and the one a reclamation plate puts its salt into (the plate
  standing in for the machine). A container the rule leaves out is not there for the machine: a shelf's input in
  another's claim gives nothing, so its items wait as for missing stock; a yield with no other container drops where
  the work happened, as with none touching; the plate's salt with nowhere else to go is lost. With the switch off,
  claims stop no machine. The game's own chutes and hoppers stay as the game rules them. When this brief was written
  (main at a1aae13), the containers asked no claim, so a shelf outside a claim drew stock from an input chest just
  inside it (G61). The fix follows this brief as a patch.
- **Uses by hand.** Setting crystals into a machine's sockets or Memory and taking them out, putting items on the shelf
  and taking them off, and the rod's taps (switching, turning, a crystal or a shelf item) are uses by hand of a
  Lemuria block: with the switch on they need the claim owner's permission, as a chest does, and with it off they open
  inside another player's claim (R9). Making a rod from a placed crystal and carving with the chisel are Lemuria's
  workings in a player's hand: with the switch on they check that the player may build there, and with it off they
  no longer spare a claim (standing rule 7, G40's first reading, as feature 4's brief has it). Breaking or placing a
  machine there stays as the game rules it. **When this brief was written** (main at a1aae13): the rod's taps
  and the chisel's carving, a held item's click that the game does not test, asked the switch themselves, as built;
  sockets, Memory, the shelf and the rod's making (a click on the placed crystal, through the block) asked it too,
  but the game tests a claim before a block's own interaction, so with the switch off those uses were still refused
  (G16). The fix follows this brief as a patch.

## What it will not do

- No Luminous crystals in machines, strain or twin duets: feature 14. No over-driving a machine or stacked Amplify
  blocks: not placed yet.
- No duets, Duet Module, Memory widening commands, Memory panning a battery, or limits on machines that Bloom:
  feature 10. No Extend module or beams: feature 15. No Ascendant stone, room for 8, a magazine of 3 or expansion
  slots: feature 16. No signals, teaching, sleep and wake, ossified shelf or Runic Tablet: feature 22.
- No Sky or Mystery battery, and no super media or Storm Call (feature 18). No better kilns or charcoal's third rung
  (feature 19).
- No bees, fishing or resin on the shelf, and no shelf action beyond the starter list. The bucket milks by the game's
  default amounts (G18), which differs only for another mod's animal: left for a compatibility pass before version 1.0.

## What to try

1. Strike an Awakened block eight times with the chisel: Gather, Refine, Focus, Scatter, Amplify, Motor, Probe, and
   Free stone again. Chapters 5, 6 and 8 of the Journal open.
2. Set a quern down with an Awakened block on top, strike its top round to the Motor, join it to charged sparkstone
   and set a Glittering crystal in: the quern turns. Sneak and right-click it with the rod: it stops.
3. Hang a Focus over tilled soil with an Ice crystal: it waters. Swap in a Glowing Sky crystal with an Amplify holding
   a Glowing crystal and a chest touching it: ripe crops are harvested into the chest and replanted. A Glowing Earth
   crystal in a Scatter tills a 9×9.
4. Point an Earth Focus down: a 2×2 rises and the Focus goes dark; tap it for the next rise. Face it at a hillside with
   a chest: it bores a tunnel you can walk in.
5. A Fire Focus aimed at a forge with coal in it lights it (old fix 1). Aimed at a loaded bloomery with no chimney, it
   costs nothing (after the G15 patch).
6. Carve a Refine block with one Fire and three Unaligned crystals: a day later all four are Fire.
7. Strike a Gather holding a Sky crystal with lightning, or kill a hen near one holding a Death crystal, and restart
   within the boost: it is still boosted (after the G13 and G14 patch).
8. Build the silo and run the gate: a chest of logs becomes charcoal, with one rod tap after each burn.
9. A shelf with a do-nothing item in its fourth place still draws, with a dark fourth plug (old fix 12). A Dead Socket
   set in after the fertilizer ran out sends the whole shelf Dormant (old fix 13). A firestarter on a shelf lights a
   forge in reach and leaves a pile of firewood alone (old fix 14).
10. Gravel just left of a shovel: it digs only the gravel. Take the seeds out of the input: their plug goes dark; put
    them back: they wake and sow.
11. Set Memory against a Scatter with Glowing Earth and Ice crystals, a Glowing Unaligned crystal in its magazine and
    seeds on its shelf: one machine tills, sows and waters a 9×9.
12. Boil a salt water pool over reclamation plates with an amplified Glowing Fire Focus: salt arrives in the chest.
13. Put only a Dead Socket in a Motor on a charged line: the quern turns; break the line: it stops at once.
14. Build an Ice battery of still water, its fill on a Wind Probe's mast and its drain on a network of its own with a
    Motor at a quern: in a strong wind ice spreads down, and the quern turns on what the drain pays back. Build a
    2×2×1 Fire battery in the world's rock, and a 3×3×3 Earth battery with some ore and a chest at its drain.
15. In a warm thunderstorm, or with the game's `/debug lntest spawn` within 20 blocks, a probe on a mast is struck and
    its crystals fill.
16. With the claims switch off, inside someone else's claim, set a crystal into their machine and take an item off their
    shelf (needs a second player; after the R9 patch).
17. With the claims switch on, set a Control Shelf outside a second player's claim with its left against a chest
    just inside it: the shelf draws nothing from that chest. Have a Fire Focus outside the claim set a fire beside
    it: the fire does not spread into the claim (needs a second player; after the G58 and G61 patches).
18. Restart: crystals in sockets, Dormant blocks and crystals, shelf items, Memory's crystals, Dead Sockets, a
    refining's progress and a battery's count are all still there.

## Open questions

1. Does a Gather's boost belong in its saved state? **Answered:** the old planning thread ruled on Nov 10 that its
   loss is a gap in the code, and a standing crystal and a cradle keep theirs (G13, G14).
2. Does R9 cover sockets, the shelf, Memory and the rod? **Answered:** R9 ("Every feature reads the same switch");
   sockets, the shelf and Memory were among the six blocks its question named, and the rod's taps are uses by hand.
3. Does carving with the chisel and making a rod follow the switch? **Answered:** G40's first reading, which PR #46
   wrote into the concept, merged on 2026-11-12.
4. Can a pile that a shelf stacks be lit (G19)? **Answered** for what this feature does: by its rules nothing of a
   machine's ever lights a pile (a firestarter never does, and a pit burns from the firepit the shelf builds), and the
   owner's silo of 2026-11-02 burned and was dug in play (the old diary of old step 4 part 2). The old audit's reading,
   that a pile stacked by code cannot be lit by other means until its chunk reloads, stays G19's note, and "what to
   try" 8 watches the burn.
5. Is a machine's fire to be held at a claim, and its containers asked for one (G58, G61)? **Answered:** "G58.) Fix
   G61.) Fix", the owner's word of 2026-11-12 19:35Z (R27). Two choices are this brief's own, for the owner to
   change if wanted: a machine whose carver the game does not know sets no fire on a face, and a container in
   another's claim counts as no container at all.
