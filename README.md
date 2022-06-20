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

If you make content with CCU, you are *strongly advised* to put [Mod], [CCU] or something similar in your steam upload title to catch their attention. You could also put a warning in the upload's image. If they don't have CCU and try to play your content, it will play as a (really confusing) vanilla campaign.

In the upload's description, provide a link to the Player Edition and advise them to install it for the full experience.
#		Development Priorities
In light of certain questions I've received, I think it is important to be clear about my priorities for this project:
- All content will be retro-compatible. The newest release of CCU will always maintain or improve the functioning of content made with older versions.
- In terms of content, my main goal is to modularize vanilla features to allow designers full control over the basics. Adding new custom content comes later.
- Custom content is curated to allow maximum flexibility and reusability. If a single CCU feature is cool *on its own*, then it probably needs to be broken down into modularized parts. If it can't be broken down and only has one application, it's either crucial or completely unnecessary.
#		Campaign Utilities
##			Mutators
These are on hold for version 2. They include Level Branching, Big Quest controls, 
#		NPC Utilities
CCU traits do not count toward the 8-Trait Limit, since they have no effect on player characters. *
##			Traits
###				Behavior
|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Accident-Prone										|- Won't path around Crushers, Fire Spewers & Sawblades<br>- Might extend this to the other traps.
|Eat Corpses										|- Eat corpses, like Cannibal<br>- Requires: Cannibalize
|Grab Alcohol										|- Grab Alcohol, like me<br>- Don't do drinks, kids
|Grab Drugs											|- Grab Drugs, like me also<br>- Don't do drugs, kids
|Grab Food											|- Grab Food, like your mom
|Grab Money											|- Grab Money, like Slum Dweller
|Pick Pockets										|- Pick pockets, like Thief<br>- Requires: Sticky Glove
|Seek & Destroy										|- Stalk & attack players, like Killer Robot
|Suck Blood											|- Suck blood, like Vampire<br>- Requires: Bite
###				Combat
|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Coward												|- Always flees from combat
|Fearless											|- Never flees from combat
###				Cost Scale
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
###				Interaction Gate
|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Untrusting											|- Will only interact with Agents Friendly or better *
|Untrustinger										|- Will only interact with Agents Loyal or better *
|Untrustingest										|- Will only interact with Agents Aligned *

****Exceptions**: Leave Weapons Behind, Offer Motivation, Pay Debt*
###				Loadout
|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Manager Key										|- Starts with the chunk's Key
|Manager Mayor Badge								|- Starts with the chunk's Mayor Badge
|Manager Safe Combo									|- Starts with the chunk's Safe Combo
###				Merchant Type
|Trait												|Inventory												|
|:--------------------------------------------------|:------------------------------------------------------|
|Anthropophagie										|Alcohol, Axes & Bear Traps
|Armorer											|Armor, Durability Spray
|Assassin											|Weaponry & Stealth tools
|Banana Boutique									|Bananas. Also Banana peels for the cost-cutter.
|Barbarian Merchant									|Ale, meat & a sharp blade. All that is best in life!
|Bartender											|Vanilla Bartender shop inventory
|Bartender (Dive)									|Inventory for seedy joints.
|Bartender (Fancy)									|Inventory for upscale establishments.
|Blacksmith											|Melee Weapons & Durability Spray
|Consumer Electronics								|Appliances & Electronics for the modern idiot
|Convenience Store									|Beer, Smokes, and where your dad said he was going when he left
|Cop (Contraband)									|Stuff confiscated from the City's Ne'er- and/or Rarely-Do-Wells
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
|Hypnotist											|Finally, people might just like you!
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
|Family Friend										|- Aligned to Friend of the Family & Mobsters
|Honorable Thief									|- Will not Pickpocket Honor Among Thieves<br>- If Merchant, will only sell to Honor Among Thieves
|Scumbag											|- Hostile to Scumbag Slaughterer
|Slayable											|- Hostile to Scientist Slaughterer<br>- XP bonus when neutralized by Scientist Slaughterer
|Specistist											|- Hostile to Specist<br>- XP bonus when neutralized by Specist
|Suspecter											|- Annoyed at Suspicious