#		Streets of Rogue Custom Content Utilities
SOR gives you a few options for creating custom content, but it does have some limitations. Here are a few things that are currently impossible with the Vanilla version:
- Aligning Custom NPCs in different chunks
- Variable Custom NPC appearances
- Giving Custom NPCs behaviors like pickpocketing, hiring, selling items, etc.

CCU aims to address these limitations, giving more power to content creators, while keeping the player's experience un-cluttered with designer tools.
#		The Mod Itself
There are two versions of CCU: 
- The Designer Edition is for making or playing enhanced content. 
- The Player edition is for those who only want to play it.
#		Best practices for use
##			Steam Upload
Players need at least one version of CCU installed for your content to work correctly. 

If you make content with CCU, you are *strongly advised* to put [MODDED] or something similar in your steam upload title. 

In the upload's description, provide a link to the Player Edition and advise them to install it for the full experience.
#		Campaign Utilities
##			Mutators
These are on hold for version 2. They include Level Branching, Big Quest controls, 
#		NPC Utilities
CCU traits do not count toward the 8-Trait Limit, since they have no effect on player characters. *
##			Traits
###				Behavior
|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Accident-Prone										|- Won't path around Crushers, Fire Spewers & Sawblades
|Eat Corpse											|- Eat corpses, like Cannibal<br>- Requires: Cannibalize
|Grab Drugs											|- Grab Drugs, like Me
|Grab Money											|- Grab Money, like Slum Dweller
|Pick Pockets										|- Pick pockets, like Thief<br>- Requires: Sticky Glove
|Seek & Destroy										|- Stalk & attack players, like Killer Robot
|Suck Blood											|- Suck blood, like Vampire<br>- Requires: Bite
###				Combat
|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Coward												|- Always flees from combat
|Fearless											|- Never flees from combat
###				Cost
This affects all costs from the agent: Hire, Interaction, Merchant
|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Less												|- Costs reduced by 50%
|More												|- Costs increased by 50%
|Zero												|- Costs reduced by 100%
###				Drug Warrior
Upon entering combat, the agent will apply a status effect to themselves, similarly to how the Drug Dealer acts in vanilla.
|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
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
|Sure I Can											|Killer Thrower
|The Impermanent Hunk								|Strength
|The Incredible Bulk								|Giant
|The Last Whiff										|Nicotine
|Wildcard											|Random (Vanilla Drug Dealer)
###				Explode on Death
|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Big												|Big explosion
|Huge												|Huge explosion
|Normal												|Normal explosion
###				Hire
|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Decoy												|- Cause a Ruckus
|Hacker												|- Hack target Object or hackable Agent
|Intruder											|- Break into target Door or Window
|Muscle												|- Hire as protection
###				Interaction
|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Administer Blood Bag								|- Lose 20HP for a Blood Bag
|Borrow Money										|- Gain debt for $50
|Bribe Cops											|- Pay to bribe cops
|Bribe for Entry									|- Pay to make owners Friendly
|Bribe for Entry (Alcohol)							|- Give Alchol to make owners Friendly
|Buy Round											|- Pay to make everyone in chunk Friendly
|Buy Slave											|- Pay to buy Slave
|Donate Blood										|- Lose 20HP for $20
|Heal Player										|- Pay to heal self
|Identify											|- Pay to identify items
|Influence Election									|- Pay to influence election
|Leave Weapons Behind								|- Drop all weapons
|Manage Chunk										|- Manage Arena, Deportation Center, or Hotel
|Moochable											|- Valid target for Moocher
|Offer Motivation									|- Give small item to make Friendly
|Pay Debt											|- Pay off Debt
|Play Bad Song										|- Pay to play bad song
|Start Election										|- Interact to start Election
|Use Blood Bag										|- Give Blood Bag to heal for 20HP
|Visitor's Badge									|- Carries Mayor's Visitor Badge
###				Merchant Type
|Trait												|Inventory												|
|:--------------------------------------------------|:------------------------------------------------------|
|Anthropophagie										|Alcohol, Axes & Bear Traps
|Armorer											|Armor, Durability Spray
|Assassin											|Weaponry & Stealth tools
|Banana Boutique									|Bananas. Also Banana peels for the cost-cutter.
|Barbarian Merchant									|Ale, Meat & Weaponry. All that is best in life!
|Bartender											|Vanilla Bartender shop inventory
|Bartender (Dive)									|Inventory for seedy joints.
|Bartender (Fancy)									|Inventory for upscale establishments.
|Blacksmith											|Melee Weapons & Durability Spray
|Consumer Electronics								|Appliances & Electronics for the modern idiot
|Contraband											|Stuff confiscated from the City's Ne'er-do-wells
|Convenience Store									|Beer, Smokes, and where your dad said he was going when he left
|Cop (Standard)										|Standard Patrolman's gear
|Cop (SWAT)											|Doorkicker's gear
|Demolition Depot									|Explosives
|Drug Dealer										|Vanilla Drug Dealer shop inventory
|Firefighter Five-and-Dime							|Firefighting & EMT equipment
|Fire Sale											|Arsonist's tools
|Gunsmith											|Guns, Mods & Ammo
|Hardware Store										|Tools & safety equipment
|High Tech											|High tech shit
|Home Fortress Outlet								|Traps
|Hypnotist											|Make people like you with this shit!
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
|Shopkeeper											|Vanilla Shopkeeper shop inventory
|Slave Shop											|Tools for acquiring human merchandise
|Soldier											|Vanilla Soldier shop inventory
|Sporting Goods										|Just Goods, no Bads
|Teleportationist									|If it teleports, we sell-aports! (We're working on the slogan)
|Thief												|Vanilla Thief shop inventory
|Thief Master										|Advanced intrusion tools
|Throwcery Store									|Thrown items & killer thrower
|Toy Store											|Harmless stuff that you probably wouldn't have any use for
|Upper Cruster										|Vanilla Upper Cruster store inventory
|Vampire											|Bloodsucker's supply
|Villain											|For when you're not concerned about collateral damage
###				Passive
|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Crusty												|- Same as Upper-Crusty
|Explode On Death									|- Explode on Death
|Extortable											|- Valid target for Extortionist
|Guilty												|- Valid target for Cop Big Quest
|Innocent											|- Gets away with murder
|Possessed											|- Has Shapeshifter
|Status Effect-Immune								|- Guess, genius
|Z-Infected											|- Zombifies on death
###				Relationships - Faction
|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Faction 1 Aligned									|Aligned with same trait
|Faction 1 Hostile									|Hostile to Faction 1 Aligned
|Faction 2 Aligned									|Aligned with same trait
|Faction 2 Hostile									|Hostile to Faction 2 Aligned
|Faction 3 Aligned									|Aligned with same trait
|Faction 3 Hostile									|Hostile to Faction 3 Aligned
|Faction 4 Aligned									|Aligned with same trait
|Faction 4 Hostile									|Hostile to Faction 4 Aligned
###				Relationships - General
|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Hostile to Cannibals								|
|Hostile to Soldiers								|
|Hostile to Vampires								|
|Hostile to Werewolves								|
|Relationless										|
###				Relationships - Player
|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Player-Aligned
|Player-Annoyed
|Player-Friendly
|Player-Hostile
|Player-Loyal
|Player-Neutral
|Player-Submissive
###				Trait Gate
|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Bashable											|- Hostile to Blahd Basher<br>- XP bonus when neutralized by Blahd Basher<br>- Aligned to Blahds
|Common Folk										|- Loyal to Friend of the Common Folk
|Cool Cannibal										|- Neutral to Cool With Cannibals<br>- If Merchant, will only sell to Cool With Cannibals
|Cop Access											|- If Merchant Type is cop-related, will only sell to The Law
|Crushable											|- Hostile to Crepe Crusher<br>- XP bonus when neutralized by Crepe Crusher<br>- Aligned to Crepes
|Family Friend										|- Aligned to Friend of the Family
|Honorable Thief									|- Will not Pickpocket Honor Among Thieves<br>- If Merchant, will only sell to Honor Among Thieves
|Scumbag											|- Hostile to Scumbag Slaughterer
|Slayable											|- Hostile to Scientist Slaughterer<br>- XP bonus when neutralized by Scientist Slaughterer
|Specistist											|- Hostile to Specist<br>- XP bonus when neutralized by Specist
|Suspecter											|- Annoyed at Suspicious