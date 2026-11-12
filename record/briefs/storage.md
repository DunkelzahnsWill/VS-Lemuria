# Brief: feature 5, `storage`

**Feature.** Number 5 of the Road Bible's part 3, short name `storage`, owned by mason (R5, R15). Built in old steps 2
and 3, and in the old audit's fixes (PR #10).

**Its folders.** `Lemuria/Storage/`; every folder named `storage` under `Lemuria/assets/lemuria/` (the rack's block
type, the jars' item type, lang, shapes, textures); no patch file; and `tests/smoke/storage.*`.

**Written from.** The signed-off notes as built, play-test changes included (R3): "Lemuria step 2: The Four
Elementals" (Crystal jars and racks, follow-ups F1 to F3, the play-test's changes) and "Lemuria step 3: Naked
evocations" (Claims and players, as the owner answered the old audit on 2026-11-10), checked against the code on main
at commit a1aae13. Where this brief and the old paper differ, this brief is the record.

## What the player sees and does

1. **Two crystal jars,** `lemuria:crystaljar-{kind}-{state}`: small lidded clay jars carried in one of the player's
   bag slots, like a basket, each giving three slots that take only crystals. Kind *personal*, with crystal bits set
   in its clay, or *shielded*, with a sparkstone glyph on its face; state raw or fired.
   - **Making one.** Set an unfired crock of the game's own (any clay colour) down on the ground, as pottery is set
     down for a pit kiln, and right-click it with one seedling to make an unfired personal jar, or with a block of
     Awakened sparkstone (feature 4) to make an unfired shielded jar. Fire it where it stands, in a pit kiln or kiln,
     as the crock is fired.
   - **Setting one down.** A fired jar can be set down with sneak and right-click, as a crock is, keeping its
     crystals, and picked up again.
   - **What a jar does.** Its crystals never dialogue up to Glowing, with each other or with anything outside, and
     they neither charge nor light. A jar keeps them when it is stored in a chest, so twins in separate jars can share
     any chest. The shielded jar holds crystals of any Evolution safely: up to Glowing it works like a personal jar,
     and from Luminous it is to be the only jar that holds a crystal back (feature 13 builds that).
2. **The crystal rack,** `lemuria:crystalrack-{rock}-{side}`, carved from a placed polished rock half-slab with a
   left-click of the Crystalline Chisel (feature 4), in the cradle's 19 rocks. Set against a wall or on a shelf, it
   faces the player who set it, its row ends filled with stone. It holds 16 crystals lying lengthwise like bottles in
   a wine rack, pointing into the wall, in four rows of four, every other row shifted half a place. Right-click a place
   with a crystal to set one there, and with an empty hand to take back the one you look at.
   - **What a rack does.** Its crystals are stored: they glow faintly by charge level but neither charge, light, hum
     nor dialogue, up to Glowing, so twins can share one rack.

## How it is built, and what it touches

- `StorageModSystem`, `BlockCrystalRack` and `BlockEntityCrystalRack` (16 places in its own inventory, with no
  container tick, so nothing looks for twins there), `CollectibleBehaviorJarFiller` (the behaviour on seedlings and
  sparkstone that turns a set-down crock into an unfired jar; ground storage passes the click on, so no vanilla file
  is touched) and `StorageClaims`.
- The jars are the game's own bag (backpack) item with three slots that take only Lemuria crystals; their crystals are
  left out of dialogues by feature 1, which skips jar slots.
- It reads feature 1 for crystals and charge level, and feature 4 for the sparkstone that makes a shielded jar.
- Saved: the rack's 16 places and a jar's three slots, with each crystal's charge and cracks.
- **Cost.** Nothing ticks: a rack or jar changes only when a player uses it.

## Settings

None of its own.

## Claims

With the claims switch on, using a rack or a jar inside another player's claim needs their permission, as a chest
does. With the switch off (R9; the owner's card of 2026-11-10), anyone may use them there: a rack opens wherever it is
clicked; a jar set down on the ground, in the game's ground storage, may be picked up, crystals and all, while anything
else stored on that spot stays locked; and an unfired crock there may be made into a jar. Breaking them stays as the
game rules it. This is **built**: Storage answers the game's own block access event (`OnTestBlockAccess`), with no
Harmony, and lifts only a use the game refused for a land claim. The game asks about one click up to three times, so
for two seconds after taking a jar the same player keeps that spot and could set something back where it stood;
other mods' own checks on the block still apply.

## What it will not do

- No jar or rack holds back a Luminous crystal or above: the shielded jar's hold on those is feature 13's work.
- No jar charges its crystals, and no rack feeds a network.
- A rack or jar opens to use only, never to breaking, whatever the claims switch says.

## What to try

1. Set an unfired crock down, right-click it with a seedling, and fire it in a pit kiln: a personal jar with crystal
   bits in its clay. Do the same with a block of Awakened sparkstone: a shielded jar with a glyph.
2. Carry a Fire and an Ice crystal in one jar: no discordant hum. Put twins in two jars in one chest: nothing.
3. Set a fired jar down with sneak and right-click and pick it up again: its crystals are kept.
4. Carve a rack from a polished rock half-slab with the chisel and set it against a wall: it faces you. Fill it with
   twins: they glow faintly and stay silent.
5. With the claims switch off, inside someone else's claim, take a crystal from their rack and pick up their set-down
   jar (needs a multiplayer session: old fix 9, G20).

## Open questions

None open. The claims rule is the owner's (R9, and the card of 2026-11-10) and is built.
