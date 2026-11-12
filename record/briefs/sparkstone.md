# Brief: feature 4, `sparkstone`

**Feature.** Number 4 of the Road Bible's part 3, short name `sparkstone`, owned by mason (R5, R15). Built in old steps
2, 3, 4 (all three parts) and 5 part 1.

**Its folders.** `Lemuria/Sparkstone/`; every folder named `sparkstone` under `Lemuria/assets/lemuria/` (the block
type, the chisel's, admin charger's and Energy Sink's item types, lang, shapes, sounds, textures); no patch file; and
`tests/smoke/sparkstone.*`.

**Written from.** The signed-off notes as built, play-test changes included (R3): "Lemuria step 2: The Four
Elementals" (the Crystalline Chisel, Awakened sparkstone, glyphs, the admin charger), "Lemuria step 3", "Lemuria step
4: Machines" (sockets, the Complexity Rating, capacitors, Active and Dormant, how they look), "Lemuria step 4 part 3"
(the Energy Sink) and "Lemuria step 5: The Four Mysteries" (the Glint's carving), checked against the code on main at
commit a1aae13. Where this brief and the old paper differ, this brief is the record.

## What the player sees and does

1. **The Crystalline Chisel**, `lemuria:crystalchisel`, one tool that carves, holds charge and wakes stone (old step 2's
   play-test, C1 to C3). It is struck with no GUI: sneak and left-click a placed Unaligned crystal, Glittering or
   Glowing, while holding another Unaligned crystal or a strange crystal; the one in hand is used up and becomes the
   chisel, and the placed one stays (the strike's code lies with the crystal, feature 1). A strange crystal set down
   counts as Unaligned, so two finds strike a first chisel before any cradle exists. It sits in the hand as a crystal
   does, never wears out, and carries no vanilla chisel tag, so the game's own chiselling never starts.
2. **Carving with a left-click,** free and with an empty chisel too:
   - a placed polished rock block (the game's own) into a cradle of that rock (feature 2);
   - a placed polished rock half-slab into a crystal rack (feature 5);
   - a placed glass block, plain, quartz or coloured, never a pane or a slab, into a Glint (feature 9);
   - a placed Awakened block into the next machine form, handed to feature 8's carver.
   The chisel replaces every grid recipe for the cradle and the rack.
3. **Charge in the chisel.** It holds charge like a Glittering crystal, 100. Right-click a charged crystal, placed or
   resting in a cradle, or any block of a charged network, to draw as much as it has room for; the source loses what
   the chisel gains. Three glyphs on its blade light one by one with its level (none, one, two, three for empty, low,
   high, full), and a chisel holding any charge gives a faint light in the hand.
4. **Awakening.** Hold right-click for a second on a placed polished rock block to awaken it into *Awakened
   sparkstone* of the same rock, `lemuria:sparkstone-awakened-{rock}`: one block a use, for 25 charge, a quarter of a
   full chisel. An empty chisel does nothing.
5. **Awakened sparkstone** is a full stone block, its polished faces carved with glyph grooves, mined with a pickaxe
   and dropping itself, empty. It is the first *sparkstone tier*, Awakened (standing rule 2).
6. **Networks and touching charge.** Awakened blocks that touch face to face form one *network*, which holds 50
   charge for each block, shared evenly by what each block can hold. An aligned crystal placed on a block of it feeds
   it: the crystal charges from its environment as any standing crystal does and pours its charge in as fast as the
   stone takes it, filling itself once the network is full. Crystals give to stone and never draw from it. The chisel
   draws from any block, and the whole network gives. Charge does not jump between networks. A network is worked out
   every 10 seconds: a new block joins, a broken one splits it and takes its share, and a network cut by an unloaded
   chunk works as smaller networks until it is whole again.
7. **Glyphs and hum.** Every block of a network glows by its charge level (empty, low, high, full) in a clear, deep
   cyan (old step 4's play-test: cyan stands out on pale rocks and stays apart from Time's light cyan), without
   lighting the area. A network hums Unaligned's note an octave down, A2, by level, the game merging a wall of it into
   one sound, and a level change plays one chime.
8. **Sockets and the Complexity Rating.** Every Awakened block, Free or carved into a machine, has *room* for 4, its
   *Complexity Rating*: a Glittering crystal fills one socket and a Glowing one two. Right-click an Awakened block with
   any Lemuria crystal to set it into the first free sockets, instead of casting; with no room it clicks dully and
   stays in hand. Right-click with an empty hand to take back the crystal you look at. Sneak and right-click still
   sets a crystal standing on the block. Strange crystals and seedlings never go in; a fitting goes in only where a
   block says so (the Dead Socket, feature 8). A *Free* block's sockets appear on the face its first crystal goes
   into, until it is empty again; a machine's are on its front (feature 8).
9. **How sockets look.** Each socket is a round mark, two rows of two. A socketed crystal stands in a stone plug lined
   up with the glyph, shaped by its Evolution: round over one mark at Glittering, long across both marks of its row at
   Glowing. The plug glows cyan while its crystal can work, and stays dark while the crystal or its block is Dormant or
   where it can do nothing (an Unaligned crystal in a Free block, which stores nothing; a Glittering crystal in a
   Scatter, feature 8).
10. **Capacitors.** An aligned crystal in a Free block's socket is a *capacitor*: its capacity, 100 or 200, and its
    charge join its network's, so the network stores more and machines draw on it. A full Free block holds 450. Taken
    out, a capacitor leaves as full as its network is, so storing a crystal never drains it. It gathers nothing from its
    surroundings, an Unaligned crystal stores nothing, and a socketed crystal still dialogues with any twin within 3
    blocks (feature 1), save one in a Memory's magazine up to Glowing (feature 8; feature 10's brief, R32; not built
    yet: it is built with feature 10, and nothing in this feature's code changes for it).
11. **Active and Dormant.** Every Awakened block, Free or carved, is *Active* or *Dormant*, and starts Active. A
    Dormant block is out of its network: it passes no charge, so switching one Dormant cuts a line in two, and its
    capacitors keep what they hold. Its glyphs go dark and its hum falls silent. The switch is a tap of the Crystal Rod
    (feature 8); signals from machines, containers and clocks belong to feature 22.
12. **Two creative test tools,** in the creative inventory only, with no recipe. For a player in creative mode or with
    the server's controlserver privilege, and for nobody else:
    - the *admin charger*, `lemuria:admincharger`: a right-click fills a placed crystal, a crystal resting in a cradle,
      a Glint's crystal or a whole network to full at once; sneak and right-click fills every crystal and Crystalline
      Chisel the player carries, jars included;
    - the *Energy Sink*, `lemuria:adminsink`: a right-click empties a placed aligned crystal, a Glint's crystal or a whole network at
      once; sneak and right-click empties every crystal and chisel the player carries. It leaves a cradle's crystal and
      a battery's count alone (feature 8), so a drain stone can be watched paying back.
    Cracks and alignment stay as they are. Both sit in the hand as crystals do.

## How it is built, and what it touches

- `SparkstoneModSystem`, `BlockSparkstone`, `BlockEntitySparkstone` (a block's share, level, sockets, capacitors and
  Active or Dormant), `SparkstoneNetworks` (the 10-second network tick; a network bigger than a set size is worked
  out in parts, so one huge wall cannot stall a tick), `Sockets` and `SocketCrystal`, `ItemCrystalChisel`,
  `ItemAdminCharger` and `ItemEnergySink`.
- It reads feature 1 for crystals, charge, cracks and environments. Feature 8 builds every machine on
  `BlockEntitySparkstone`, and carves through the chisel's hand-off (`StoneCarver`); feature 5's shielded jar is made
  with a block of Awakened sparkstone; the chisel carves the blocks of features 2, 5 and 9.
- Saved: each block's share of charge, its sockets' crystals with their charge and cracks, the face of a Free block's
  sockets, and whether it is Active; the chisel's charge on the item. Clients get one level for a whole network, and a
  block entity update only when what shows changes.
- **Cost.** A network is walked once every 10 seconds; a Dormant block costs nothing, and switching one re-forms its
  networks once.

## Settings

`ChiselCapacity` 100, `AwakenCost` 25, `SparkstoneCapacity` 50, `RoomAwakened` 4, `SocketsGlittering` 1,
`SocketsGlowing` 2.

## Claims

The chisel's carving and awakening are Lemuria's workings in a player's hand. With the claims switch on, they check
that the player may build there, as placing a block does, so nobody carves or wakes stone inside another player's
claim; with it off, they no longer spare a claim (standing rule 7, on the first reading that G40 sets out and PR #46
wrote into the concept). Setting a crystal into a socket, taking one out and drawing charge with the chisel are uses
by hand of a Lemuria block: with the switch on they need the claim owner's permission, and with it off they open
inside another player's claim (R9). Breaking or placing a block there stays as the game rules it. **When this brief was
written** (main at a1aae13): the chisel's carving and awakening, a held item's click that the game does not test,
asked the switch themselves, as built; a socket asked it too, but the game tests a claim before a block's own
interaction, so with the switch off a socket's use was still refused (G16); and the chisel's draw asked no claim at
all, so with the switch on a stranger could draw charge from a crystal, a cradle's crystal or a network inside
another player's claim (G57). Both fixes follow this brief as patches.

A network takes no account of a claim, whatever the switch says: Awakened blocks that touch join one network
whoever set them down and wherever they stand, its charge is shared across a claim's edge, and a draw or a
machine's working anywhere on it takes from the whole of it. So a block set just outside a claim, touching a
network inside it, reaches that network's charge. The owner declined to change this (G68, "Decline", 2026-11-12
19:57Z, R29). It is built.

## What it will not do

- No Ascendant or Sovereign stone, room for 8 or 16, healing or reshaping: features 16 and 23.
- No beams: charge moves only through touching stone (feature 15).
- The test tools are no part of survival play, and do nothing for an ordinary player.

## What to try

1. Strike a Crystalline Chisel from two Unaligned crystals, or in a new world from two strange crystals set down.
2. Carve a cradle from a polished rock block, a rack from a polished rock half-slab and a Glint from a glass block.
3. Draw a full crystal into the chisel: the blade's three glyphs light. Hold right-click on polished rock for a
   second: Awakened sparkstone, and a quarter of the chisel spent.
4. Build a line of a few Awakened blocks and place a charging crystal on it: the cyan glyphs of every touching block
   fill, and the line hums.
5. Right-click charged crystals into a Free block: they become capacitors and the line holds far more. Take one out:
   it leaves as full as the line was.
6. Sneak and right-click a block in the middle of the line with the Crystal Rod: it goes dark and splits the line.
7. In creative, fill a network at once with the admin charger, and empty it with the Energy Sink.
8. With the claims switch off, set a crystal into another player's Free block inside their claim (needs a second
   player; after the R9 patch).

## Open questions

1. Do the chisel's carving and awakening follow the switch, or stay as the game rules a player's own building?
   **Answered:** the record already holds the first reading (G40: the rule's own first sentence, the code, and the
   note of old step 3, "One switch covers every claim rule in Lemuria, from evocations to the chisel, racks and
   jars"), and PR #46 wrote it into the concept, merged on 2026-11-12.
2. Does R9 cover sockets and drawing charge? **Answered:** R9; sockets were among the six blocks its question named.
