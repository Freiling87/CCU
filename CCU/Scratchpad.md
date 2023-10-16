##			How to read this
If you wandered in here out of curiosity, this is my working notes file, and the place to find upcoming features. It's a markdown file, but best viewed in raw form with the Markdown Editor VS extension since a lot of its organization has to do with text characters' alignment. 

Listed in order of Parent tier summary symbol priority:
	C, T = Code this, Test this
	H = Hold, usually pending resolution of a separate or grouped issue
	D = Document
	√ = Fully implemented feature or group of features
##			Change Log
- **Feature Additions**
  - Player Traits
	- Permanent Status Effects
  - Designer Traits
    - Vigilance: Determines how the agent reacts to sound, in regards to investigation and property.
- **Tweaks**
  - Agents spawned via Friend Phone will now re-roll their appearance if they have the traits for it, since they're not clones.
  - Scumbags are no longer automatically Guilty.
- **Bug Fixes**
  - Previously fixed vanilla bug but now documenting: Broken objects should no longer drop bugged items with "E_" in the name.
##			Bugs
Except crickets. Crickets are fine.
###			C	
###			C	Consumable rapid-use
This is technically SORCE, but Melee Maniac seems to interact with it. Could be a good hint.
###			C	Language broke AGAIN
Can't speak to gorilla, have Speaks High Goryllian
###			C	Exempt Scene Setter NPCs from possession
Spawns shapeshifter on death
###			C	Grab Everything
Doesn't pick up safe combo
Do we want them to though?
Maybe a Grab Special would be appropriate
###			C	Shops are empty
Per CL, but this was with a test version.
###			C	Dual Alignment freeze
https://discord.com/channels/187414758536773632/1003391847902740561/1140063406997651469
BT's alignment methods should handle this... test it
###			C	Electronic blocks status effects
Giant robot! Giant robot! Giant robot!
###			C	Indestructible Corpse Explosions
GenEric: https://discord.com/channels/187414758536773632/991046848536006678/1138250512752459857
If a corpse is indestructible and has explode on death, they will explode repeatedly.
This is cancelled by Electronic.
###			C	Prison interrupts Dead SS
https://discord.com/channels/187414758536773632/991046848536006678/1137189897808117860
###			C	Undercant strings
Deleted WIPs, not much to save
###			T	Friend Phone Clone
Appearance patch is ready to go
###				Cyber Nuke by Proxy
This is POSSIBLY an old Roguelibs bug.
Hired Cyber Nuke doesn't show option.
"HacksBlowUpObjects"
First detect who the actual interactingAgent is when using a hacker hire
###				Editor String Copying
I think it's JUST containers → Signs now
Select Vanilla or Modded Container, choose contained item. Then switch to Draw, and select Altar. Item text will be in EVS1. 
I think I found where this bug is from: 
	P_LevelEditor_ObjectButtonType
Located bug:
	LevelEditor.SetExtraVarString() last branch
###				Z-Infected Appearance
https://discord.com/channels/187414758536773632/1003391847902740561/1127403137100161074
	Pretty weird one, if you mix randomized appearance with Z Infected then the resulting zombies will not just re-randomize but they'll also all become identical. Like if you randomize bodies and have a mix of hacker, clerk, bartender and goon then killing a crowd and raising them as zombies will make them all come back in clerk uniforms or whatever. It's not consistent so the thing they turn into is random each time (it's not just like it's reverting to the neutral pre-randomized appearance, alphabetical order or anything like that), but once the "zombie form" is randomized it seems like every member of the class will turn into that on death.
	And yes, I did somehow pivot from updating old NPCs to messing around with new zombie plague victim designs. This is why the Vendor Variety update and SoV Remaster are never getting released, it's physically impossible to spend more than an hour in the character editor with this mod without starting a completely new project.
###				Various Shop Inventory Reports
I think these have been addressed but verify:
https://discord.com/channels/187414758536773632/1003391847902740561/1126283884959629312
###				Deaf NPCs speak Polyglottal Gibberish
Good god
https://discord.com/channels/187414758536773632/991046848536006678/1127299576366383165
###				Random Teleport to Entry Elevator
Make a distance buffer from entry to minimise the cheesening
###				Gun Nut traits only apply on start??
I thought this was fixedddd
###				Concealed Carry Sitting
CL https://discord.com/channels/187414758536773632/1003391847902740561/1126306577008316508
	Concealed carrier still doesn't work with sitting NPCs until something briefly updates their behavior (like a noise making them stand up and look, then sit back down).
###				Vendor Cart Ransack not making noise
I mean Agent noise, not the sound effect
###				Language Teacher bugs
Maxior - https://discord.com/channels/187414758536773632/1003391847902740561/1125451549162881074
Language learning is broken. Asking an NPC to teach you will kick you out of the dialogue and when you talk with them again, learning the language(s) will be the only option. Every other option is gone, including shops and hire options.
###				Undercant Speakers lacking Gibberish Dialogue
For mismatched language
###				Upgrading traits doesnt' remove base trait
Melee Maniac + in upgrade machine, kept both traits
I guess do an OnAdded, frame-delayed, from T_CCU. This would be a good contribution to Roguelibs as well, if you can find out how.
###				Ammo Traits offered on same level
Ammo Auteur and Artiste
###				Gate Language for Home Base
Allow interact with all
Chaos at Home Base is through Werewolf interaction, for example
##			Bug Archive
###		 H   Scene Setter Breakage
https://discord.com/channels/187414758536773632/991046848536006678/1122856007706607656
Spawning with visible hostiles is confirmed to interrupt it inconsistently
Not able to replicate
###		 H   Merchant Stock Price issues
Two symptoms of the same issue. Abbysssal pointed out the solution, but this is not an important enough bug to delay release for.

- Gas Mask is consistently $30 too expensive by formula.
  - Abby found the issue:
		=== MoneyCostDurabilityAmmo_Logging: (GasMask)
		MoneyAmt:	  70
		MoneyAmtFloat: 53.33334
	This example for low durability, net price should have been 23
- Melee Weapons under 100 are underpriced.
	Item		Wrong	Right	Ratio1	Ratio2
	Knife		$5		$10		0.50	2.000
	BaseballBat $10		$17		0.58	1.700
	Crowbar		$8		$13		0.62	1.625
	Wrench		$8		$13		0.62	1.625
  - Abby found the issue:
		=== MoneyCostDurabilityAmmo_Logging: (Wrench)
		MoneyAmt:	  13
		MoneyAmtFloat: 8.58
####			C	Low-Qty Melee too Cheap
Melee weapons with low Durability are cheaper than they should be.
Below, Baseball bat at 66 Qty (200/3) is $10 but should be $17.


###		 H   Normal (EOD) in Shop Inventory
https://discord.com/channels/187414758536773632/1003391847902740561/1121962031235477524
No clue how to replicate this.
###		 H   Scene Setters Activate Chunk
Scene setters may cause events that the designer wished the player to witness to play out too early for them to see. Normally the trigger for activating the chunk is by player proximity.
Unfortunately, I think this fix will be a bit too complex for this project.
###		 H   Hidden Bomb Spawned in Vent off map
https://discord.com/channels/187414758536773632/187414758536773632/1093294336109727845
You might be able to replace the != Wastebasket string with a special validity detector.
So far I've been unable to replicate this bug. Took place on a standard Park map that did not lack other containers for the bomb.
###		 H   Equipment noise spam
Still occuring, even with vanillas.
For now, just disabling the noise if they switch to Fist.
###		 H   CCU Trait section in Load Character screen
Shows outdated traits when choosing
Don't really care yet.
###		 H   Clone changing appearance
Shelving this because I simply don't care enough to fix it yet. This is super-niche.
Buddy Cop Loneliness killer
The one that showed up on level 2 was identical to me
The one that came from level 1 rerolled appearance
###		 H   Unidentified Eyes Strings error message
Between Accessory & Body Type, current logs are uninterrupted
Shelved - no apparent effect for this error.
###		 H   Unidentified Loadout error message
Consistently, the Crepe Heavy has this error.
	[Info   : Unity Log] SETUPMORE4_7
	[Debug  :CCU_LoadoutTools] Custom Loadout: Custom(Crepe Heavy)
	[Error  : Unity Log] Couldn't do ChooseWeapon etc. for agent Custom (1132) (Agent)

This error is from LoadLevel.SetupMore.
But since there doesn't appear to be any actual repercussion on gameplay, I'm shelving it until it is an issue.

I want to say I later fixed this. Verify if the messages still come up.
###		 H   Jukebox Hacks
Possibly RogueLibs, wait for confirmation.

Mambo:
	[Error  : Unity Log] NullReferenceException: Object reference not set to an instance of an object
	Stack trace:
	Turntables.PlayBadMusic () (at <7fd7dd1709b64c98aabccc051a37ae28>:0)
	RogueLibsCore.VanillaInteractions+<>c.<Patch_Turntables>b__68_2 (RogueLibsCore.InteractionModel`1[T] m) (at <ac95cf0d3a8543748b4a19536e8724e6>:0)
	RogueLibsCore.SimpleInteraction`1[T].OnPressed () (at <ac95cf0d3a8543748b4a19536e8724e6>:0)
	RogueLibsCore.InteractionModel.OnPressedButton2 (System.String buttonName) (at <ac95cf0d3a8543748b4a19536e8724e6>:0)
	RogueLibsCore.InteractionModel.OnPressedButton (System.String buttonName) (at <ac95cf0d3a8543748b4a19536e8724e6>:0)
	RogueLibsCore.RogueLibsPlugin.PressedButtonHook (PlayfieldObject __instance, System.String buttonText) (at <ac95cf0d3a8543748b4a19536e8724e6>:0)
	Turntables.PressedButton (System.String buttonText, System.Int32 buttonPrice) (at <7fd7dd1709b64c98aabccc051a37ae28>:0)
	ObjectReal.PressedButton (System.String buttonText) (at <7fd7dd1709b64c98aabccc051a37ae28>:0)
	WorldSpaceGUI.PressedButton (System.Int32 buttonNum) (at <7fd7dd1709b64c98aabccc051a37ae28>:0)
	ButtonHelper.PushButton () (at <7fd7dd1709b64c98aabccc051a37ae28>:0) 
	ButtonHelper.DoUpdate (System.Boolean onlySetImages) (at <7fd7dd1709b64c98aabccc051a37ae28>:0)
	ButtonHelper.Update () (at <7fd7dd1709b64c98aabccc051a37ae28>:0)
Bladder:  
	[Error  : Unity Log] NullReferenceException: Object reference not set to an instance of an object
	Stack trace:
	Turntables+<BadMusicPlayTime>d__31.MoveNext () (at <7fd7dd1709b64c98aabccc051a37ae28>:0)
	UnityEngine.SetupCoroutine.InvokeMoveNext (System.Collections.IEnumerator enumerator, System.IntPtr returnValueAddress) (at <a5d0703505154901897ebf80e8784beb>:0)
###		 H   $0 in container
https://discord.com/channels/187414758536773632/1003391847902740561/1007975536607383574
Maxior - Shelf w/ $0 as container, but not Trash Can
So far, unable to replicate
###		 √   Partisan Concealed Carry
Winnie - https://discord.com/channels/187414758536773632/646853913273696257/1117896939837587517
Issue was Loadout Money without a Loader trait.
##			Feature Scope
###			C	Mutator Configurator
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
###			C	Laser Emitter modes 
Persistent - stays lit even after activation
One that annoys instead of making hostile when activated.
###			C	Scene Setters
Dead (No Drops)
###			C	Decal Objects
Blood pool (or as scene setter)
Bullet holes
###			C	Weak Walls
New door types. 

Below are Strength requirements to break open weak walls. Strength status effect adds 1 to your strength check.

|Wall Type		|Axe		|Crowbar	|Detonator	|Sledgehammer	|Wrench		|Wire Cutters	|
|:--------------|:---------:|:---------:|:---------:|:-------------:|:---------:|:-------------:|
|Barbed Wire	|2			|			|0			|1				|			|0 (Silent)
|Bars			|			|4			|0			|3				|4			|4 (Silent)
|Border/Concrete|			|			|0			|5				|			|			
|Brick			|			|5			|0			|3				|			|
|Cave			|			|4			|0			|3				|			|
|Glass			|			|			|0			|3				|			|
|Hedge			|2			|			|0			|4				|			|
|Steel			|			|5			|0			|4				|			|
|Wood			|3			|3			|0			|2				|			|
###			H	Appearance-less
All work great, but Eyeless copies over to Friend Phone Clone
Also, seems like anything will turn them back on, like teleporting. So I think deactivating the game object rather than simply its animator might work.
###			C	Campaign Branching
Will need tools to:
- Construct Mutator from options
- Decompile options from Mutator (name only)
- Detect Workshop content with specific prefix, destroy broken mutators and make new ones from the data.
###			P	Pre-release checks
####			Quality Review
Review all implemented features and fixes:
- Does this need to be gated for Home Base?
- Are both NPCs and PCs addressed?
- Have you confirmed that Hooks are used as minimally as possible?
####			Project modes
- Disable Developer mode
- Make Player & Designer editions
  - Maybe this can be automated in build event?
####			Designer Traits
Notes here
####			Player Traits
Upgrades: IsAvailable = false
	Keeps from showing in levelup
###			C	CCU Character Creation Page
Might not be impossible. See CharacterCreation.Awake.
Try COPYING something like TraitsMenu in CharacterCreation.Awake and see if you can repurpose it.
Just search Trait in that method, and do everything he does there.
###			T	Behavior - Vigilant

####		D	Notes stash
|Vigilant											|- Reacts to sound like Shopkeeper, Slavemaster & Soldier.
|Vigilanter											|- Reacts to sound like Bartender, Bouncer, Cannibal & Goon.
|Vigilantest										|- Reacts to sound like Supercop.
###			C	T_CCU bools
affectsPC
affectsNPC
###			C	Make Class Alignment Traits Player Traits
New
###			C	I think the PostProcess thing for traits is pointless
These can all be set with
Unlock = 
{ 
	cantLose. etc.
}
within the WithUnlock parameters
BUT when you do this you will need to go deep into those variables since there is no note about this thing's purpose.
###			T	Test out Chunk Key/ Combo
Their code was moved in my code but technically in the same place in the loadlevel sequence. Optimistic that it will work fine.
Check for:
	Duplicate key allocation
	No keys allocated
	Normal allocation taking place
###			C	New Scene Setters
Need tester feedback
Need a note saying classes don't cover all the bases here, some are just strings
###			C	Permanent Status Effects
I think this should be moved to ResistanceHR.
####			D	Player documentation

//##			Permanent Status Effect

|Trait												|CCPV	|Effect													|
|:--------------------------------------------------|------:|:------------------------------------------------------|
|Above the Laws										|32		|- Above the Law
|Bulletproofish										|8		|- Resist Bullets<br>- Bullet damage divided by 1.25
|Conductive											|10		|- Electro Touch
|Critter Hitter										|16		|- Always Crit
|Desecondive										|-32	|- Shrunken
|Dying												|-32	|- Poisoned
|Enfastened											|10		|- Fast
|Enstrongened										|16		|- Strength
|Even Shootier										|4		|- Accuracy<br>
|Gigantic											|100	|- Giant
|Invisibility Enjoyer								|100	|- Invisible
|Killer Throwerer									|32		|- Killer Thrower
|Lucky Duck											|7		|- Feelin' Lucky
|LyCANthrope										|32		|- Werewolf
|Ragestart											|-32	|- Enraged
|Regenerationist									|32		|- Regeneration
|Slothful											|-6		|- Slow
|Strong Immune System								|16		|- Stable System
|The Invincibility Gambit							|100	|- Invincible
|Thick Skin											|12		|- Resist Damage (Small)<br>- Damage divided by 1.25
|Thicker Skin										|24		|- Resist Damage (Medium)<br>- Damage divided by 1.5
|Thickest Skin										|36		|- Resist Damage (Large)<br>- Damage divided by 2
|Thickester Skin									|48		|- Numb to Pain<br>- Damage divided by 3
|Undying											|100	|- Resurrection
|Unlucky Duck										|-1		|- Feelin' Unlucky
####			C	General Issues
#####				C	Clone didn't keep SE
Elevator level transition
Clone Machine, Friend Phone and Loneliness Killer all lose Invisibility on moving to next level.
Trait list is fully intact
I think the trail is pointing to HireUnofficially, which spawns to party without party limit.
#####				C   Trait Upgrading
Test that this works properly
#####				C   Antidote
Left Gigantic intact at least, but verify there's not a limited list it can work on
#####				C   Toilet
Left Gigantic intact at least, but verify there's not a limited list it can work on
#####				C   Block UseItem Effects
Gigantizer if you're giant, e.g.
	This one in particular just wastes it. I'd like to prevent wasting the item.
#####				C   Opposite Effects
Shrinker if you're giant, e.g. Untested
####			C	Above the Laws
#####				C	Block Bribe Interaction
Might be automatic
####			C   Bulletproofish
Works I guess
#####				C	Block Equip Bulletproof Vest
Might be automatic
####			C   Conductive
New
####			C   Critter Hitter
Rename Literally Critler
####			C   Dying
Says Poisoned in SE, does that matter?
####			C   Enfastened
Works
####			C   Enstrongened
Works, but what's the cap?
####			C	Invisibility Enjoyer
E_ text in char sheet
####			C   Killer Throwerer
Works
####			C   Lucky Duck
Verify numerically
####			C   LyCANthrope
Doesn't transform
Just puts a number 2 above head
####			C   Popular
DW, you wanted to make a trait group anyway so just remove it
####			C   Ragestart
Might need to tell to attack neutrals, although this version is pretty interesting
####			C   Regenerationistest
E_RegenerateHealthFaster
Remove
####			C   Shrunk
Works
####			C   Slothful
Works, find caps
####			C   Stablemaster
Yeah I don't care to test this one
####			C   Invincibility Strategy
Sure yeah fine
####			C   Thick Skin
Test
####			C   Thicker Skin
Test
####			C   Thickest Skin
Seems to work
####			C   Unlucky Duck
Verify numerically
###			C	Check out PlayControl.Update
Has some debug tools that can be the basis for a bit of interesting content
###			C	OnAdded/OnRemoved Overrides
If you override these in an abstract class, you don't need to waste the space on the subclasses.
##			Idea Dump
Apparently I want to type these without any forethought, so this will be a dump to be periodically assorted.
###				Vote Weight traits
Antivoter -			-1x
Disenfranchised -	 0x
Activist -			 2x
Councilmember -		 3x
Elector -			 4x
UwuMac's ideas:
	Semidisenfranchised: 50% chance to apply ×0 multiplier to agent elect power. (applies on spawn).
	Straw polls: apply a random integer multiplier to elect power from 1× to 3× (inclusive, applies on spawn)
	Status symbol (mutator...?): shows vote power in NPC name as a postfix, eg: "Slum Dweller (×4 Voter)". 
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
		- Semi-common loot, carried by some NPCs
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
	Crippled Firearmer: NPC's firearms stat is scaled based on base speed and health, where 100% hp is 100% base speed and 0% hp is 25% firearms.
	Crippled Fighter: NPC's melee stat is scaled based on base speed and health, where 100% hp is 100% base speed and 0% hp is 25% melee. 
	Crippled Runner: NPC's speed stat is scaled based on base speed and health, where 100% hp is 100% base speed and 0% hp is 25% speed.
	Intense Crippling: NPC's cripples scale from 100%-0% instead of 100%-25%.
###			Cutscene
Use Patrol Points to move characters. Add ExtraVarString to determine action when they reach patrol point. A text box that shows their dialogue. Equally-numbered points act simultaneously, for the most part.
Vars:
	Move Agent To & Talk
	Move Agent To & Action
	Pan Camera
	Teleport Agent, etc.
	Hack Action on same-tile object
###			C	Hidden Relationship
CL - a trait that hides NPC relationship status until you "interact" (including them initiating combat, so kinda like how bounty NPCs read as neutral until they draw their gun), it's pretty hard to surprise the player sometimes when the space bar reveals so much information about people.
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
	[All Factions] Aligned/Hostile
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
- Trait Selection List (middle)
- Selected Trait List (right)
###			C	Profile scrollbar
To cancel illegibly tiny text on right panel
###			C	CharacterSelect.CharacterLoaded
This is a bool array that might be easier than checking for nulls on charsavedata. 
Slots 32-48 are custom characters, and it's true
###			C	Flammable trait
Use lighter to explode an NPC