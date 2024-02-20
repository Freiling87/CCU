# Idea Dump
"Dump" shouldn't imply these are bad ideas, they're just ones I haven't bothered to sort yet.
##			C	Elevator Text
Pop up before and after entry/exit of level
Button 2
##			C	Improve Branching Interaction
If non-default branch:
-Clear all interactions before adding buttons
  - Because Learn Language menu included "Offer drink"
- Add "Back" button
  - Because Done just exits the interaction fully.
##			C	Pay Debt
Reclassifying this as a feature request rather than bug report
CL - Speaking of which though, that reminds me that the "pay debt" behavior doesn't include payments for the slum dweller BQ unlike the vanilla clerk thing. Or maybe you want to keep it modular but even then I'd like that as a behavior too then. 90% of the reason behind Penny's interactions was to make that quest more consistent in case of ATM issues but currently she can't do that.
##			C	"Look For Trouble" Scene Setter
CL - NPC has heightened awareness and will turn to face different directions while idle upon spawning, lasts for like 10 seconds (ideally from the time the player actually loads them in fully by getting close enough) and then they switch to the normal "wander around the floor" behavior. Might be too difficult to implement but the idea would be to simplify NPC fight setups, right now the options are pretty much idle or wander but the former looks dumb when the fight is over and the latter has a high failure rate since NPCs load at different times and often wander away from one another.
Might not need them to spin around, just detect what's in visual range and set to engage.
##			C	Make hire expert traits compatible
New
E.g., if they have all of them, all options are available.
##		C	Idea Dump
Wrong header tier
###			C	Refactor Seek & Destroy
Eliminate all 11 patches, and just patch BrainUpdate now that you can transpile into the killerRobot checks.
###			C	Laser Emitter modes 
Persistent - stays lit even after activation
One that annoys instead of making hostile when activated.
###			C	Scene Setters
Dead (No Drops)
###			C	Decal Objects
Blood pool(or as scene setter)
Bullet holes
###			C	Weak Walls
New door types. 

Below are Strength requirements to break open weak walls. Strength status effect adds 1 to your strength check.

|Wall Type		|Axe		|Crowbar	|Detonator	|Sledgehammer	|Wrench		|Wire Cutters	|
|:--------------|:---------:|:---------:|:---------:|:-------------:|:---------:|:-------------:|
| Barbed Wire | 2 |           | 0 | 1 |           | 0(Silent)
| Bars |           | 4 | 0 | 3 | 4 | 4(Silent)
| Border / Concrete |           |           | 0 | 5 |           |
| Brick |           | 5 | 0 | 3 |           |
| Cave |           | 4 | 0 | 3 |           |
| Glass |           |           | 0 | 3 |           |
| Hedge | 2 |           | 0 | 4 |           |
| Steel |           | 5 | 0 | 4 |           |
| Wood | 3 | 3 | 0 | 2 |           |
###			H	Appearance-less
All work great, but Eyeless copies over to Friend Phone Clone
Also, seems like anything will turn them back on, like teleporting. So I think deactivating the game object rather than simply its animator might work.
###			H	Mutator Configurator
####			C	Load
See P_GameController_SetupConfigurator
####			C	Agent Switches
New
####			C	Agent Triggers
Two big ready-made systems you can catch:
	Quests.AddBigQuestPoints
	SkillPoints.AddPoints
		- These are
####			C	Level Gates
New
####			C	Level Gate Conditions
####			C	Level Switches
Track countables via
	Quests.AddBigQuestPoints
	SkillPoints.AddPoints
	Stats.AddToStat
####			C	Interface issues
#####				C	Hide Mutators in Player version
New
#####				C	Hide Mutators in Pause Screen
Also see if you can make the list more legible.
#####				C	Hide Mutators in Home Base
Hide the mutators except when in leveleditor.
####			H	Object Switches
Pending Object Button refactor
###				Clingy
Removes Dismiss button.
###				Rescuable
BrainUpdate.GoalArbitrate: 
	if (this.agent.rescueForQuest ! = null)
	Check also for trait Rescuable
If they have it, follow branch to exit point.
Ensure that goalType is indeed set to follow.
Check out Escorting=false in the killforquest!=null block below. Might be useful if you want them to move without the player.
Freeing a Rescuable gives you an XP bonus.
###				Hire ideas
Clingy - Once this NPC joins your party they can no longer be manually dismissed. Seems like a natural pairing with the plot-critical trait, gotta stop people from cheesing my overly long escort mission. Could also have weird applications with less extreme hires though, like followers with many advantages but also hostility traits to make them a liability half the time.
a trait where they exit when near the elevator, and you get XP for rescuing or something. No quest needed
###				Vote Weight traits
Antivoter -			-1x
Disenfranchised -	 0x
Activist -			 2x
Councilmember -		 3x
Elector -			 4x
UwuMac's ideas:
	Semidisenfranchised: 50 % chance to apply ×0 multiplier to agent elect power. (applies on spawn).
	Straw polls: apply a random integer multiplier to elect power from 1× to 3× (inclusive, applies on spawn)
	Status symbol(mutator...?): shows vote power in NPC name as a postfix, eg: "Slum Dweller (×4 Voter)".
	Poltertician: this NPC dying does not remove it's elect power from your electability score. 
	Poltertician +: also, this NPC becomes a ghost when it dies that retains ownership, opinions and (ideally) clothing and loses this trait (so it won't respawn infinitely). 
###				Aligned to Same Class
Not same as Class Solidarity (Makes Loyal and adds No-Infighting effect)
###				Self-proclaimed hero
Acts as law enforcer without The Law
###				Same as ID for Agents
Should just be reactivating a field
###				Gib type Wooden
Per Cheese
###				NPC Door Link trait
Neutralizing NPC is only way to open the door
###				Broken Object
Spawn object as wreckage
Per CL, could use direction field since they're exclusive to each other.
###				Squad ID
Only unused field for agents is "Same as Agent" or whatever. Might be able to use that as a starting-behavior modifier.
###				Vent Spewing
Per-object or as chunk mutator? 
Either way, offer various status types
###				Demographic Skin
Did it for hair, why not for skin?
###				Chunk Mutators
Chunk-wide mutators
	Localized Disasters
	Faction Palette Swaps
###				Status Traits
Just a proper name for the alignment/skill etc. temp traits you designed
###				Subclass Traits
####				uwuMac's ideas
Subclass Residency traits:
by default a subclass can spawn in any enviroment. but will only spawn in these enviroments if one or more Subclasss Residency category traits are selected. EG: a char with SCR | Bar and SCR | Arena can only spawn in bars and arenas but on every floor, but SCR | Bar and SCR | Park allows them to only spawn on bars in park.
[Chunk Descriptor or Chunk Special]: character can only spawn in these chunks.
[District]: character can only spawn in these districts.
[Default Goal]: character can only spawn if the NPC has this default goal.

Subclass Owning traits:
these follow the same "additive" principal as residency traits.
Ownful: character can only spawn if they are an owner and not a guard.
Ownless: character can only spawn if they don't own anything.
Apprentice Owner: character can only spawn as a guard.
Master Owner: character can only spawn if they are the chunk owner.

Misc traits:
Disastrous: character can only spawn on floors with disasters.
Deep District Dweller: character can only spawn on floors x-2 or x-3.
Groupie: replace every class of the type this character spawned as in a chunk as long as they meet the restrictions.
Special Delivery: character can replace agents summoned after the level loads (eg: supercop booth, summoning ghosts by breaking gravestones)
####				Class Rotation
Vanilla agents become Agent Groups with any Superclassed customs, and replaced by a random selection therein.
MultiVar trait? Other filters to add to Class Rotation: District, Chunk Type, Difficulty, special tags that are triggered by Chunk Mutators
###				Modularize Zombiism trait
New
###				Modularize Electronic trait
New
###				Modularize Ghost trait
New
###				Custom MapGen
####				Subway Network
Generate a map separated into strips separated by Holes. The player has to figure out a way to get access to the Subway train for each strip of the level, which will take him to a random or designated next station.

For every Chunk Y % 2 && Y < 8, assuming chunks are 1-8,
	Generate a Hole- and wall-separated subway track as needed to minimize interaction between subway-separated districts. This will allow reusing trains in a nonsensical but flexible way. I believe the track generation gives a Y-area of 4, which should be enough to work with.

Check out TileInfo.PlaceTracks for map gen basis.

Trains:
	Now stop at designated station stops. Maybe sprite changes when it's interactable. Could use light and sound cues too.
	Require a Train Ticket:
		-Semi - common loot, carried by some NPCs
		- Interaction - Sell Subway Tickets
		- Hack train or same-chunk computer
		- Threaten, Mug, Extort, good stuff
		- Custom vending machine someday never
####				C	Elevator Sublevels
Similar to Subway network, but separated by Elevators.
###				Flee X
CL: "Behavior - Flee X: X seconds after initiating combat with the player, this agent will despawn." Could just have one trait with a reasonable set value or a couple options like 10, 30 and 60 seconds. It would be a great way to fix my lootbots (since you didn't want to do the item despawn death explosion), could just lower their endurance but give you limited time to pop them once you make your move. It would also be cool for stuff like fighting overpowered enemies that you basically just need to survive against, just gotta dodge the speed 10 melee 50 immortal supermonster for 10 seconds and then it'll poof away on its own.
###			H	Object Button System
Looks pretty good but probably out of scope for now, since branching is slated.
Only bug so far is button label incorrect and possible string contamination.
###			Attribute Decay
uwumac:
	Crippled Firearmer: NPC 's firearms stat is scaled based on base speed and health, where 100% hp is 100% base speed and 0% hp is 25% firearms.
	Crippled Fighter: NPC 's melee stat is scaled based on base speed and health, where 100% hp is 100% base speed and 0% hp is 25% melee. 
	Crippled Runner: NPC 's speed stat is scaled based on base speed and health, where 100% hp is 100% base speed and 0% hp is 25% speed.
	Intense Crippling: NPC 's cripples scale from 100%-0% instead of 100%-25%.
###			Cutscene
Use Patrol Points to move characters. Add ExtraVarString to determine action when they reach patrol point. A text box that shows their dialogue. Equally-numbered points act simultaneously, for the most part.
Vars:
	Move Agent To & Talk
	Move Agent To & Action
	Pan Camera
	Teleport Agent, etc.
	Hack Action on same-tile object
###			C	Hidden Relationship
CL - a trait that hides NPC relationship status until you "interact" (including them initiating combat, so kinda like how bounty NPCs read as neutral until they draw their gun), it 's pretty hard to surprise the player sometimes when the space bar reveals so much information about people.
###			C	No pre-combat callout
Why would you alert someone you're attacking
###			C	Scene Setter requests
Seek & Destroy
Dead (No Drops)
###			C	Namespace Organization
CCU.Appearance.HairColor
Main
	System
		Content		
###			C	Object Interaction
Train Extravarstring1
	- Level Progression
		- Generates a level with gaps between strips of map, 10010010 flipped 90 to be horizontal stripes. Each part of the map borders a train and generates with a train station chunk. Player must operate the train to travel between the three stripes. They can get a ticket through a variety of means.
  	- Random Travel
		- Just moves to a random train station, for evading attackers or something. Also requires a ticket.
Turret
	- Weapon Type
	- Sight type (Laser-narrow version of camera vision beam might be interesting)
###			C	Vouchers only
Agent only accepts vouchers
###			C	Actually Harmless
Search for pressedAttack
###			C	Aligned Freebies
####			C	Free hire to aligned
New
####			C	Free shop to aligned
New
####			C	Free interaction to aligned
New
###			C	Faction Randomizers
- Chooses 1 faction trait of all added, ignores others
- Flips coin for each, gets 0-all
###			C	Agent Relationships
####			C	Aloof
Relationship capped at Loyal
####			C	Aloofer
Relationship capped at Friendly
####			C	Super Sidekick
Aligned to all
####			C	Super Buddy
Loyal to all
####			C	Super Acquaintance
Friendly to all
####			C	Super Misanthrope
Annoyed at all
####			C	Super Pissed
Hostile to all
###			C	NPC Dynamics
####			C	Lead
Leads followers
####			C	Follow
Follows lead
####			C	Forced
Becomes Loyal instead of Hostile when Lead is killed, like Slave
####			C	Slave
Slave of Master in chunk with same Owner ID?
####			C	Master
Master of any Slaves in chunk with same Owner ID? 
They join if under your follower limit, otherwise treat normally
####			C	Slavemaster Mentality
Player trait: When you rescue slaves or prisoners, they become your slaves.
###			C	Bomb Squad
Disarms explosives
Demolitionist Campaign when
###			C	Fixer Type
####			C	Mechanic
Generators
####			C	Electrician
Security Cams
####			C	IT
Computers
####			C	VendCo
Vending Machines
###			C	Fixer Level
####			C	White Hat
Fix hacked objects. Remote if has Laptop
####			C	Repairer
Fix broken or tampered but powered objects.
	- Slot Machine
	- Security Cam
####			C	Supergluer
Fixed destroyed objects. Don't ask how
###			C	Law By Proxy
Gives Above the Law when hired.
###			C	Sensitive System
Takes damage when gaining a status effect.
Double damage from ooze, sulfuric acid and poison.
###			C	Fire Friend
Disables Aligned damage reduction for friendly fire
###			C	Steady Steady
Drug Warrior Paralysis
###			C	Sleepy Hands
Punch has tranq effect
###			C	Corpsebloat
Sickening miasma on death
###			C	Jawbreaker / Jawbreakest
Punch knocks out at various HP %s (5/10?)
###			C	Close-up Shot
When you get near them, they get a camera pan and zoom that follows them for a little bit. Would be ideal if player were still on screen.
###			C	Behavior
####			C	Quick Escape Teleporter
"Blinks in the face of danger"
Need all Item Usage traits tbh
####			C	Mugger
Mug player, grab SORCE code
####			C   Phobia
Corpse, Fire, etc. You could incorporate Cowardly and Fearless into this category.
####			C   Hidey Holer
After escaping line of sight of a pursuer, will move to a chosen hiding place nearby.
Variations for Owned, Unowned, and Prohibited, which determine where they are willing to hide.
Try to put closed doors between self and pursuer
####			C	Chase Scener
When fleeing a threat, the NPC will do unique "obstacle" effects for various objects. The timing of various actions should be aligned to what might best challenge player reaction speed.
	- Shelves tip (blinking) and collapse after 1 second
	- Knocked over garbage cans will trip you unless you jump over them
	- Tipped over Vendor Carts will spill a bunch of apples, just because this was such a movie cliche. Maybe they can trip you or something.
	- Why are there no chain link fences in SOR? 
####			C	Hazard Utilization Enjoyer
Will shoot barrels if you're next to them
####			C   Door Locker (Fleeing)
New
####			C   Door Locker (Always)
New
####			C   Repaireratorer
Mechanic: This NPC can detect broken objects and fix them aslong as theres electricity and can un-tamper generators
Supergluer: this NPCcan rebuild DESTROYED objects, dont ask the logistics
Disarmer: this NPC can disarm timed explosives and mines
####			C	Class Tension Mutator
Class-based opinions about digging in the trash, grabbing food off the ground, picking up or buying drugs (Maybe investment bankers don't mind that part so much). Also for bougie behaviors.
###			C	Class Alignment traits
The two below can be replaced by the Superclass trait group + Class Solidarity, but their behavior would not be identical. Not sure how specifically.
####			C	Upper Cruster Aligned
Like Supercop & Cop
####			C	Worker Aligned
Why tf not
###			C	Organ System
- If you have a knife or sword, operate a dead body to extract one Organ (Gore gib sprite) at the cost of 30 tool durability. Makes noise, makes (I think) anyone hostile if they see you do it. Successfully completing the operating bar gives you a Kidney item.
- Each Doctor will buy n Organ for $30ish. Since chattel slavery is legal, this is legal too. Look at you, saving lives!
- Getting an Organ gives you the Bloody status effect, which makes you drop a small blood splat at random intervals. If someone sees them, they have various reactions, but Cops always follow the trail. Remove Bloody by operating a Bathtub, swimming, or giving yourself a cleansing swirlie.
- The more Organs you carry, and if you're bloody, Cannibals are able to smell you at a greater and greater distance. Your scent also increases chance of Cannibal attack from Manholes, which now spawn in all districts but Park and Mayor's Village.
- Big Quest: Heart to Heart - Sell a certain number of Organs per level.
- If you have Cannibalism, you can eat Organs. They are randomized per run, giving a little more utility to the Identification Wand and Scientist identification.
- Doctors will identify an Organ's effect for a price.
###			C	Blahd Juggernaut Machinegun
Picked up in level 3-4?
Later got Accuracy Modder, not sure if it ever applied it.
Later, applied Ammo Stock 
Later, checked gun and it only had ammo stock
Ensure these traits apply on pickup for all guns, it's not OP
###			C	Full Ammo Info on HUD
Show cur/max instead of jsut cur
###			C	Mutator Group: Custom Player Character Restrictions
For campaigns that allow your choice of character, does a validation process for character.

Suggested format: Custom Player - Require Crepe Aligned
Required Trait: This list would also be good for the "Add/Remove Trait" mutator group. But that would be an extremely long list so it's probably best to find another way. Getting designer text syntax working might be appropriate.
	Addict
	Antisocial
	Electronic
	Extortionist - Any trait that unlocks a "game mode" like this should be on the list
	[All Factions] Aligned / Hostile
	Malodorous
	Rechargeable
	Tech Expert - If puzzle-style progression requires certain hack actions
	The Law
	Wanted
Required Big Quest
Required Special Ability
####			C	Attribute Min/Max
Attribute
	Min  2 -  4
	Max +1 - +4	
		Although the maximum group has player interest, so not sure how to do this
####			C	Trait Cap
Sets cap
Also has overlap with player interest.
Name			Trait Cap
	Abject			  0
	Flawed			  2
	Hindered		  4
	Impressive		 10
	Legendary		 12	
####			C	CCPV Cap
Sets cap
Name			CCPV Cap
	Abject			-10
	Flawed			  0
	Hindered		 10
	Impressive		 30
	Legendary		 40	
###			C	Translator Instructions in Documentation
- The easiest way to do this would be to clone the project from Visual Studio/Github. 
  - Then search for CustomNameInfo, which is the method that interfaces with the translation system.
  - Anytime you see a CustomNameInfo that lacks a translation for your
###			C	Big Quest for Faction traits
Count Blahd-aligned as Blahd for Crepe Quest, e.g.
###			C	Quest Scale Mutators
Multiply/Divide the number of targets for a mission
This could also work as a trait, multiplying the XP reward? But need to balance spawns.
###			C	Interaction - Buy Intel
Maps unmapped safes and chests, scaled to number or repeatable
###			C	Vanillize Item Qty on load
Editor-added items give full ammo, should be semi-random and scaled to NPC level as Loadout items are
###			C	Hide Appearance Traits Button
Toggle trait visibility for:
-Trait Selection List(middle)
-Selected Trait List(right)
###			C	Profile scrollbar
To cancel illegibly tiny text on right panel
###			C	CharacterSelect.CharacterLoaded
This is a bool array that might be easier than checking for nulls on charsavedata. 
Slots 32-48 are custom characters, and it's true
###			C	Flammable trait
Use lighter to explode an NPC
###			C	Next-Level Mutators
No (Melee, Social, Stealth, Guns, etc.) Traits
Gain XP x0/x2/x3/x4
Gain Level/x2/x3/x4/x-1
Stat Up/Down/CoinToss (requested)
Swap Trait Random/Chosen/All
Lose Trait Random/Chosen/All
Lose Inventory, Lose Non-Loadout Inventory
Gain/Lose Money
Gain Loadout
Save/Load Character state (ex: flash back to before you removed Diminutive)