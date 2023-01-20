</p>

<h1 align="center">
<br>
Designer Traits
</h1>
<br><br>

Designer Traits are hidden from players entirely. For Player Traits, go [here](/CCU/Documentation/C0503_PlayerTraits.md).

##			Appearance

The Appearance system allows you to give NPCs variable appearances. Its documentation is [here](/CCU/Documentation/C0501_Appearance.md). 

##			Behavior

|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Accident-Prone										|- Won't path around Crushers, Fire Spewers & Sawblades<br>- Might extend this to the other traps.
|Brainless											|- Heavy Reddit user
|Concealed Carrier									|- Hides weapon when not in combat
|Eat Corpses										|- Eat corpses, like Cannibal<br>- Requires: Cannibalize
|Grab Alcohol										|- Grab Alcohol, like me<br>- Don't do drinks, kids
|Grab Drugs											|- Grab Drugs, like me also<br>- Don't do drugs, kids
|Grab Everything									|- Grab Everything, like a toddler<br>- Don't have kids, kids<br>- Grabs traps with Accident-Prone
|Grab Food											|- Grab Food, like your mom
|Grab Money											|- Grab Money, like Slum Dweller
|Pick Pockets										|- Pick pockets, like Thief<br>- Requires: Sticky Glove
|Seek & Destroy										|- Stalk & attack players, like Killer Robot
|Suck Blood											|- Suck blood, like Vampire<br>- Requires: Bite

##			Combat

|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Coward												|- Always flees from combat
|Fearless											|- Never flees from combat

##			Cost Scale
This affects all costs from the agent's interactions, except bribery for Quest Items.

|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Less												|- Costs reduced by 50%
|More												|- Costs increased by 50%
|Much More											|- Costs increased by 100%
|Zero												|- Costs reduced by 100%

##			Drug Warrior
Upon entering combat, the agent will apply a status effect to themselves, similarly to how the Drug Dealer acts in vanilla.

|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|An Inimitable Bulk									|Giant
|Armor-Plated										|Resist Bullets
|Berserker											|Rage
|Colognier											|Perfumerous
|Confusionist										|Confused
|Electrocutioner									|Electro-Touch
|Fainting Goat Warrior								|Tranquilizer
|Fireproofer										|Resist Fire
|Flasher											|Fast
|Gambler											|Feelin' Lucky
|Harshmellow										|Withdrawal
|Immortalish										|Resurrection
|Invincibilist										|Invincible
|Invisibilist										|Invisible
|Maimer												|Always Crit
|Numb to Pain										|20% damage resistance
|Number to Pain										|33% damage resistance
|Numbest to Pain									|50% damage resistance
|Numbestest to Pain									|66% damage resistance
|Recoverist											|Stable System
|Some Bark											|Loud
|Stimpacker											|Health regeneration
|Stimpackerer										|Fast health regeneration
|Sure I Can!										|Killer Thrower
|The Impermanent Hunk								|Strength
|The Last Whiff										|Nicotine
|Wildcard											|Random (Vanilla Drug Dealer)

##			Drug Warrior Modifier

|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Suppress Syringe AV								|- No longer does Syringe sound & text when activating Drug Warrior

##			Explode on Death

|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Big												|Big explosion
|Dizzy												|Dizzy explosion
|EMP												|EMP explosion
|Firebomb											|Molotov explosion
|Huge												|Huge explosion
|Noise Only											|Makes a loud noise
|Normal												|Normal explosion
|Slime												|Poisons anyone nearby
|Stomp												|Dizzy & Knockback to anyone nearby
|Warp												|Teleports anyone nearby to a random place in the level
|Water												|Splashes water on death

##			Explosion Timer
For use with Explode on Death traits. Vanilla timer duration is 1.5 seconds. You can use multiple of these traits to fine-tune the timer, but the math is up to you.

|Trait												|Fuse duration multiplier								|
|:--------------------------------------------------|:------------------------------------------------------|
|Long Fuse											|2.00
|Longer Fuse										|3.00
|Longest Fuse										|4.00
|Short Fuse											|0.66
|Shorter Fuse										|0.33
|Shortest Fuse										|0.00

##			Gib Type
Agents default to Meat Chunks if they have no Gib trait.

|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Ectoplasm											|- Alien gibs
|Gibless											|- No gibs
|Glass Shards										|- Guess
|Golemite											|- Rock chunks
|Ice Shards											|- Guess
|Leaves												|- Yes, leaves
|Meat Chunks										|- Just like mama used to explode into

##			Hire

|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Cyber-Intruder										|- Hack target Object or hackable Agent
|Decoy												|- Cause a Ruckus
|Intruder											|- Break into target Door or Window
|Muscle												|- Hire as protection

##			Hire Duration

***Expert Ability:*** Hacking, Lockpicking, Ruckus-Raising. Maybe more in the future.

|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Homesickless										|- Agent always follows employer to next level
|Homesickly											|- Agent never follows employer to next level
|Permanent Hire										|- Option to hire at 800% cost, with infinite uses of Expert Ability.
|Permanent Hire Only								|- As Permanent Hire, but removes original one-time hire option.

##			Interaction

|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Administer Blood Bag								|- Lose 20HP for a Blood Bag
|Borrow Money										|- Gain debt for $50
|Borrow Money (Moocher)								|- Gain debt for $50 if you have Moocher
|Bribe Cops											|- Pay to bribe cops<br>- Also works with Cop Debt
|Bribe for Entry (Alcohol)							|- Give Alchol to make owners Friendly
|Buy Round											|- Pay to make everyone in chunk Friendly
|Give Blood											|- Lose 20HP for $20
|Heal Player										|- Pay to heal self
|Identify											|- Pay to identify items
|Influence Election									|- Pay to influence election
|Leave Weapons Behind								|- Drop all weapons
|Manage Chunk										|- Manage Arena, Deportation Center, or Hotel<br>- Hotel management requires Key added with Loadout trait below
|Offer Motivation									|- Give small item to make Friendly
|Pay Debt											|- Pay off Debt
|Pay Entrance Fee									|- Pay to make owners Friendly
|Play Bad Music										|- Pay to break someone's achy breaky heart, despite their protests<br>- Also works with Mayor Evidence
|Start Election										|- Interact to start Election
|Use Blood Bag										|- Give Blood Bag to heal for 20HP

##			Interaction Gate

|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Untrusting											|- Will only interact with Agents Friendly or better
|Untrustinger										|- Will only interact with Agents Loyal or better
|Untrustingest										|- Will only interact with Agents Aligned

***Exceptions**: Leave Weapons Behind, Offer Motivation, Pay Debt, Pay Entrance Fee*

##			Inventory

|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Infinite Ammo										|- You know this. You are smart.
|Infinite Armor										|- Really, really smart.
|Infinite Melee										|- Who's a big smarty-smart?*

\* *It's you*

##			Loadout
The Loadout system allows you to set up an inventory generator for NPCs. Its documentation is [here](/CCU/Documentation/C0502_Loadout.md).

##			Loot Drop

|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Blurse of Midas									|- Agent does not drop Money on being neutralized
|Blurse of Softlock									|- Agent does not drop important items on being neutralized (Keys, quest items, etc.)
|Blurse of the Pharoah								|- Agent does not drop non-equippable items on being neutralized
|Blurse of Valhalla									|- Agent does not drop equippable items on being neutralized

##			Merchant Type
For a detailed list of merchant inventories and item frequency weights, go [here](https://github.com/Freiling87/CCU/tree/master/CCU/Traits/Merchant%20Type).

|Trait												|Inventory												|
|:--------------------------------------------------|:------------------------------------------------------|
|Anthropophagie										|Alcohol, Axes & Bear Traps
|Armorer											|Armor, Durability Spray
|Army Quartermaster									|Vanilla Soldier shop inventory
|Assassin											|Weaponry & Stealth tools
|Banana Boutique									|Bananas. Also Banana peels for the cost-cutter.
|Barbarian Merchant									|Ale, meat & a sharp blade. All that is best in life!
|Bartender											|Vanilla Bartender shop inventory
|Bartender (Dive)									|Inventory for seedy joints.
|Bartender (Fancy)									|Inventory for upscale establishments.
|Blacksmith											|Melee Weapons & Durability Spray
|Bloodsuckers' Bazaar								|Vampire supply
|Consumer Electronics								|Appliances & Electronics for the modern idiot
|Convenience Store									|Beer, Smokes, and where your dad said he was going when he left
|Cop (Contraband)									|Stuff confiscated from the City's Ne'er- and/or Rarely-Do-Wells
|Cop (Standard)										|Standard Patrolman's gear
|Cop (SWAT)											|Doorkicker's gear
|Demolition Depot									|Explosives
|Drug Dealer										|Vanilla Drug Dealer shop inventory
|Firefighter Five-and-Dime							|Firefighting & EMT equipment
|Fire Sale											|Arsonist's tools
|General Store										|Vanilla Shopkeeper shop inventory
|Gunsmith											|Guns, Mods & Ammo
|Hardware Store										|Tools & safety equipment
|High Tech											|High tech shit
|Home Fortress Outlet								|Traps
|Hypnotist											|Finally, people might just like you!
|Intruder's Outlet									|Vanilla Thief shop inventory
|Junk Dealer										|Trash, used toilet paper (Resistance Vouchers), and occasionally a gem
|McFud's											|Home of the Slopper!
|Medical Supplier									|Medicine, scalpels, syringes
|Mining Gear										|Safety Equipment, tools & explosives
|Monke Mart											|Stuff for monke, gorgia, you name it
|Movie Theater										|Vanilla Movie Theater inventory
|Occultist											|Supernatural, ritualistic & spooky shit
|Outdoor Outfitter									|Outdoor & Survival goods
|Pacifist Provisioner								|Tools to avoid unnecessary casualties
|Pawn Shop											|A mixed bag of stuff you'd find in a Pawn Shop
|Pest Control										|Tools to help get rid of pesky vermin
|Pharmacy											|Mostly medicine
|Research Materials									|Weird guns & scientist stuff
|Resistance Commissary								|Escape teleporter & Vouchers
|Riot, Inc.											|All the ingredients in the Anarchist's Cookbook
|Slave Shop											|Tools for acquiring human merchandise
|Sporting Goods										|Just Goods, no Bads
|Teleportationist									|If it teleports, we sell-aports! (We're working on the slogan)
|Thief Master										|Advanced intrusion tools
|Throwcery Store									|Thrown items & killer thrower
|Toy Store											|Harmless stuff that you probably wouldn't have any use for
|Upper Cruster										|Vanilla Upper Cruster store inventory
|Villain											|For when you're not concerned about collateral damage

##			Merchant Stock
Traits in this category are multiplicative.

***Durable Wares:** Melee & Ranged weapons, Wearables*
***Stackable Wares:** Consumables, Food, Tools, Throwing Weapons*

|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Clearancer											|- Allows the same item to be sold multiple times, like Bartender. Better for smaller inventories.
|Masterworker										|- Durable wares have 2x Durability
|Masterworkerer										|- Durable wares have 3x Durability
|Masterworkerest									|- Durable wares have 4x Durability
|Shiddy Goods										|- Durable wares have 1/3x Durability
|Shoddy Goods										|- Durable wares have 2/3x Durability
|Wholesaler											|- Stackable wares have 2x Quantity
|Wholesalerer										|- Stackable wares have 3x Quantity
|Wholesalerest										|- Stackable wares have 4x Quantity

##			Passive
|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Crusty												|- Same as Upper-Crusty
|Extortable											|- Valid target for Extortionist
|Guilty												|- Valid target for Cop Big Quest
|Indestructible										|- Can be killed, but body can't be destroyed
|Immovable											|- Receives zero knockback
|Innocent											|- Gets away with murder
|Not Vincible										|- Had to change name for reasons
|Possessed											|- Has Shapeshifter
|Status Effect-Immune								|- Guess, genius
|Z-Infected											|- Zombifies on death

##			Relationships - Faction
|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Blahd Aligned										|- Aligned to Blahd
|Blahd Hostile										|- Hostile to Blahd
|Cannibal Aligned									|- Aligned to Cannibal
|Cannibal Hostile									|- Hostile to Cannibal
|Crepe Aligned										|- Aligned to Crepe
|Crepe Hostile										|- Hostile to Crepe
|Faction 1 Aligned									|- Aligned with same trait
|Faction 1 Hostile									|- Hostile to Faction 1 Aligned
|Faction 2 Aligned									|- Aligned with same trait
|Faction 2 Hostile									|- Hostile to Faction 2 Aligned
|Faction 3 Aligned									|- Aligned with same trait
|Faction 3 Hostile									|- Hostile to Faction 3 Aligned
|Faction 4 Aligned									|- Aligned with same trait
|Faction 4 Hostile									|- Hostile to Faction 4 Aligned
|Firefighter Aligned								|- Aligned to Firefighter
|Gorilla Aligned									|- Aligned to Gorilla
|Gorilla Hostile									|- Hostile to Gorilla
|Scientist Hostile									|- Hostile to Scientist
|Soldier Aligned									|- Aligned to Soldier
|Soldier Hostile									|- Hostile to Soldier
|Vampire Hostile									|- Hostile to Vampire
|Werewolf Hostile									|- Hostile to Werewolf

##			Relationships - General
|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Relationless										|- Permanently Neutral to all other Agents

##			Relationships - Player
|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Player-Aligned										|- Aligned to Players
|Player-Annoyed										|- Annoyed at Players
|Player-Friendly									|- Friendly to Players
|Player-Hostile										|- Hostile to Players
|Player-Loyal										|- Loyal to Players
|Player-Neutral										|- Neutral to Players (For overriding other relationship traits)
|Player-Submissive									|- Submissive to Players

##			Trait Gate
|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Common Folk										|- Loyal to Friend of the Common Folk
|Cool Cannibal										|- Neutral to Cool With Cannibals<br>- If Merchant, will only sell to Cool With Cannibals
|Cop Access											|- If Merchant Type is cop-related, will only sell to The Law
|Family Friend										|- Aligned to Friend of the Family & Mobsters
|Honorable Thief									|- Will not Pickpocket Honor Among Thieves<br>- If Merchant, will only sell to Honor Among Thieves
|Scumbag											|- Hostile to Scumbag Slaughterer
|Slayable											|- Hostile to Scientist Slaughterer<br>- XP bonus when neutralized by Scientist Slaughterer
|Specistist											|- Hostile to Specist<br>- XP bonus when neutralized by Scientist Slaughterer
|Suspecter											|- Annoyed at Suspicious