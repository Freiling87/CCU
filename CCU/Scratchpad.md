#		How to read this
If you wandered in here out of curiosity, this is my working notes file, and completed/planned feature list. It's a markdown file, but best viewed in raw form since a lot of its organization has to do with text characters' alignment.

Listed in order of Parent tier summary symbol priority:
	C, T = Code this, Test this
	H = Hold, usually pending resolution of a separate or grouped issue
	√ = Fully implemented feature or group of features
#	C	v.1.0.0 Changelog
- **Hard Changes:** These will require you to update any affected content accordingly. 
  - Trait renaming: A few traits shared names with some vanilla content, and apparently the game doesn't differentiate. This was causing some vanilla characters' descriptions to be changed to the traits' descriptions.
    - Faction Relationships: Some of these were incomplete and have been renamed to reflect this.
      - Hostile to Cannibal → Faction Military (Now includes alignment with Soldiers)
      - Hostile to Soldier → Faction Cannibal (Now includes alignment with Cannibals)
    - Hire Type
      - Hacker → Cyber-Intruder
    - Merchant Type traits renamed
      - Shopkeeper → General Store
      - Soldier → Army Quartermaster
      - Thief → Intruder's Warehouse
      - Vampire → Sanguisuge Specialties
- **Soft Changes:** These will not require you to change anything. Any old content will be maintained in legacy code to maximize retro-compatibility.
  - Bugfixes
    - Fixed Vending Machine money cost issues
    - Fixed string mismatch causing CodPiece [sic] to spawn an error in shop inventories
- **Feature additions**
  - Behavior
    - Added Grab Alcohol & Grab Food
  - Added exceptions to Untrusting traits (Leave Weapons Behind, Offer Motivation, Pay Debt)
  - Faction Relationships
    - Added Firefighter Faction
#	CT!	General
##		T!	Automate trait name changes
This will need to run without a hitch.
##		T!	Vending machine DetermineMoneyCost

##		H	Explosion Trait Refactor
Move Explosion Type to its own trait
Explode on Death & Suicide bomber would then only need one trait each, or could be variegated in some other dimension (e.g., bomb timers)
##		C!	00 Names
Be absolutely sure where .WithName is assigning: any with a second argument for DisplayName(Type, string) might be disconnected if you use the wrong one. 
##		C	Add Cancellations
###			C	Verify DisplayName isn't breaking them
##		C	Dedicated section on Character Sheet
Should not be too hard, as the one method where it's filled out is pretty transparent
##		H	Config Files
###				Custom Flag list
Allow player to name booleans uniquely.
These should be grouped by Campaign, since that's the only place they're valid.
The config file should match the name of the campaign, if they allow the same characters.
###				Custom Level Tag List?
Not so sure about the utility of this. I don't think players should need more than 4 level tags.
- Whenever you have enough in the campaign to make it playable, test it in Player Edition and see if the experience is the same.
#	√H	Traits
##		√H	Interaction
###			H	Buy Slave
Pending actual assignment of owned slaves 
###			H	Cybernetic Surgery
Curated Trait-seller
###			H	Heal All
Like Doctor heal, but all in party with calculated price
###			H	Heal Other
Like Doctor heal, but activates reticle so you can select a party member or other.
###			H	Heal Partial
Like Doctor heal, but at a Blood Bag level.
####			C	Mouseover Price
Show price to heal targeted agent before selection
###			H	Quest Giver
New
###			H	Refill Guns
New
###			H	Repair Armor
New
###			H	Repair Weapons
New
###			H	Pay respects to Faction
Costs $1,000 to bump reputation up one level (Hostile → Annoyed etc)
"Improve Faction Relations"
Should only allow for 1 of these to simplify algorithm
But this means you'll need further faction traits
###			H	Sell Intel to Faction 
Reverse of buying into faction. Just a way to get cash in exchange for slightly reducing your relation. Friendly or better.
###			H	Start Election
New
###			H	Summon Professional
New
Pay a fee for him to teleport a Hacker, Thief, Doctor or Soldier to you. You still have to pay them to hire them.
###			H	This One's On Me
Buy a round for a patron and anyone with the same owner ID in the chunk.
No drink for that guy in the corner. Fuck that guy.
###			H	Train Attributes (Split to each)
New
###			H	Train Traits - Defense
New
Sell traits for double their Upgrade Machine cost
###			H	Train Traits - Guns
New
###			H	Train Traits - Melee
New
###			H	Train Traits - Movement
New
###			H	Train Traits - Social
New
###			H	Train Traits - Stealth
New
###			H	Train Traits - Trade
New
###			H	Visitor's Badge
Set Bribe options on separate traits
###			√	Administer Blood Bag
Complete
###			√	Borrow Money
Complete
###			√	Borrow Money (Moocher)
Complete
###			√	Bribe Cops
Complete
###			√	Bribe For Entry (Alcohol)
Complete
###			√	Buy Round
Complete
###			√	Give Blood
Complete
###			√	Heal (Player)
Complete
###			√	Identify
Complete
###			√	Influence Election
Complete
###			√	Leave Weapons Behind
Complete
###			√	Manage Chunk
####			√	Arena
Complete
####			√	Deportation Center
Complete
####			√	Hotel
Complete
###			√	Offer Motivation
Complete
###			√	Pay Debt
Complete
###			√	Pay Entry Fee
Complete
###			√	Play Bad Music
Complete
###			√	Use Blood Bag
Complete
##		T	Interaction Gate
###			T	00 Refactor
Changed how they gate interactions a bit, needs a test.
###			C	Insular
###			C	Insularer
###			C	Insularest
###			√	Untrusting
Complete
###			√	Untrustinger
Complete
###			√	Untrustingest
Complete
##		H	Hack
###			C	00 Interrupts
Works with Electronic, but hacking bar is interrupted
###			T	Go Haywire
Attempted
###			T	Tamper With Aim
Attempted
##		H	Appearance
###			C	Full-randomization bug
- Whole appearance is randomized when any appearance trait is added.
  - Should be a simple fix since it's doing less rather than more.
###			C	Facial Hair
####			C	Trait names changed
Changed trait names to not overlap with vanilla names. 
This will likely break the BEARD MACHINE, because it was based directly on the class names.
####			C	Vanilla facial hair no longer spawns
New
###			C	Hair Type
New
###			C	Skin Color
New
##		H	Bodyguarded
New
- There are a few other hits that came up in a string search (possibly "Musician"):
  - LoadLevel.SpawnStartingFollowers
  - ObjectMult.StartWithFollowersBodyguardA
    - Ignore this one, it's for the Player Bodyguard trait
- Check out ObjectMult.StartWithFollowers, there are something like 4 similarly named methods in there
###			C	Pilot Trait
No errors, but no effect.
###			C	Bodyguard Quantity Traits?
One / few / many, that's it
###			C	Bodyguarded - Cop
New
###			C	Bodyguarded - Blahd
New
###			C	Bodyguarded - Crepe
New
###			C	Bodyguarded - Goon
New
###			C	Bodyguarded - Gorilla
New
###			C	Bodyguarded - Mafia
New
###			C	Bodyguarded - Soldier
New
###			C	Bodyguarded - Supercop
New
###			C	Bodyguarded - Supergoon
New
##		H	Campaign Flags
Might need to limit this to a single flag, since having multiple true at the same time would complicate things
###			C	If Paid then Flag A/B/C/D True
New
###			C	If Killed then Flag A/B/C/D True
Etc.
##		H	Hire Duration
###			C	Fairweather Flunkie
Hiree will leave if they're damaged in combat
"I didn't sign up for this! You're nuts!"
###			C	Faithful Flunkie
Hiree will never "Not feel too good" and quit.
###			C	Homesickness Disabled
Automatic Homesickness Killer
###			C	Homesickness Mandatory
Overrides Homesickness Killer
###			C	Permanent Hire
New
~8x normal hire price
Does not affect damage threshold
###			C	Permanent Hire Only
As above, but removes the single-use hire option.
###			C	Timed Hire: 30s
Is this one even interesting?
##		H	Map Marker
Statuseffects.agent.SpawnNewMapMarker()
###			P	General Notes
- Check out:
GC
	.questMarkerList
PlayfieldObject
	.MinimapDisplay
x	.SpawnBigQuestMapMarker			Just goes into MinimapDisplay
x	.SpawnedBigQuetsMarkerRecently	Not relevant
	.SpawnNewMapMarker				This is where DrugDealer/Shopkeeper/etc. are detected
Quest
	.questMarkerPrefab
QuestMarker
	Entire class!
	.NetworkmarkerName = agentRealName		This looks like where marker type is determined

- Also, check out the GPYess trait and see where that comes up.
###			C	Pilot
No more errors, but no map marker. 
###			H	Bartender
Pending pilot
###			H	Drug Dealer
Pending pilot
###			H	Killer Robot
Pending pilot
###			H	Question Mark
Pending pilot
###			H	Shopkeeper
Pending pilot
###			H	Portrait
Pending customs
##		H	Merchant
###			H	Buyer
New
###			H	Buyer Only
New
###			H	Large Inventory
New
###			H	Refresh Inventory
New
###			H	Small Inventory
New
###			C	Vendor
####			C	Get full list of Vendor traits to test and complete category
##		H	Spawn
###			C	Enslaved
New
###			C	Hide In Object
Detect Bush/Manhole on same tile
###			C	Respawn
Never-ending waves of enemies
####			C	Respawn Delay
0, 3, 15, 30, 60, 120, 180 seconds
####			C	Respawn Quantity
1, 3, 10, Infinite
###			C	Roaming Gang
New
###			C	Slave Owner
NEw			
##		H	Utility
###			C	Hide Traits in Collapsed Groups
- While in Character Creator, hide traits in Collapsed Groups
  - Once all traits are in they're going to get hard to manage. 
###			C	Sort active Traits by Name
- ScrollingMenu.PushedButton @ 0006
  - Pretty much has exactly what you need.
- DW
###			C	Sort active Traits by Value
- ScrollingMenu.PushedButton @ 0006
  - Pretty much has exactly what you need.
##		H	Agent Group
###			C	Slum NPCs (Pilot)
New
###			H	Affect Campaign
Pending pilot
###			H	Affect Vanilla 
Pending pilot
###			C	Roamer Level Feature
New
##		H	Loadout
###			C	CC Items Inclusion
Any items added to the character in the CC will be included when spawned in a chunk. In vanilla, they are overridden by the chunk-defined inventory.
###			C	Item Groups
uwumacaronitime's idea: Item groups similar to NPC groups

I can see this going two ways: 
- As a trait for NPCs to generate with
- As a designated item in the chunk creator for use in NPC & Object inventories. 

I am leaning towards implementing both of these. But whichever is chosen, make it very clear to avoid confusion.

Vanilla list:
- Defense
- Drugs
- Food
- Guns
- GunAccessory
- Melee
- Movement
- NonViolent
- NonUsableTool
- NonStandardWeapons
- NonStandardWeapons2
- NotRealWeapons
- Passive
- Social
- Stealth
- Supplies
- Technology
- Trade
- Usable
- Weapons
- Weird
###			T	ChunkKey
- Attempted - InvDatabase.FillAgent()
###			T	ChunkMayorBadge
- Attempted - InvDatabase.FillAgent()
###			T	ChunkSafeCombo
- Attempted - InvDatabase.FillAgent()
###			C	Guns_Common
New
##		√H	Behavior
###			H	Clean Trash
New
###			H	Confiscate Contraband
Should also spawn Confiscation Center (See SORCE)
###			H	Deport Non-Citizens
Should also spawn Deportation Center (See SORCE)
###			H	Fight Fires
Agent.firefighter
Agent.fightsFires
###			H	Grab Everything
New
###			H	Grab Food
New
###			H	Guard Door
New
###			H	Hobo Beg (Custom)
Maybe just implement the whole Hey, You! overhaul here
###			H	Hog Turntables
New
Allow paid Musician behavior
###			H	Mutinous
Agent.mutinous
###			H	SecretHate
Agent.secretHate
Agent.choseSecretHate
I think this is Bounty behavior
###			H	Shakedown Player (Mobster)
New
Use this on leader w/ Wander Level
Use "Follow" behavior on agents placed behind them
No need for "Roaming Gang" Trait itself
###			H	Tattle (Upper Cruster)
New
###			√H	Accident-Prone
Works for: Crusher, Fire Spewer, Saw Blade
Doesn't work for: Slime, Floor Trigger, ??
###			√	Eat Corpse
Complete
###			√	Grab Drugs
Complete
###			√	Grab Money
Complete
###			√	Pickpocket
Complete
###			√	Seek & Destroy (Killer Robot)
Complete
##		√H	Combat
###			H	Lockdowner (R.I.P.)
Apparently Lockdown walls are broken in custom levels.
###			√	Coward
Complete
###			√	Fearless
Complete
##		√H	Cost Currency
###			C	00 Button ExtraCost Display
Bananas & alcohol are hardcoded
To display them correctly, prefix WorldSpaceGUI.ShowObjectButtons (interprets magic numbers)
###			H	Alcohol
A la Bouncer
###			H	Banana
Test
###			C	Blood
Blood Bags always an option
If Vampirism, allow drink
###			C	Flesh
Require Cannibalism? Maybe not
##		√	Cost Scale
###			√	Less
Complete
###			√	More
Complete
###			√	Zero
Complete
##		√H	Drug Warrior
###			C	Suicide Bomber
Initiate a 15s timer, then detonate a Huge explosion
##		√H	Explode On Death
###			C	00 Destroy body
For all non-normal explosion types
This will complete about half the holds in here.
###			H	00 Explodes when Arrested
Not too concerned, considering this is vanilla for Slaves.
###			H	Dizzy
Body remained
###			H	EMP
Works, but body remained
###			H	Firebomb
Didn't do anything
###			H	Noise Only
Doesn't work
Also not interesting
###			H	Oil
This doesn't exist but should follow water logic. Plus there are many other uses.
###			H	Ooze
Only did particle effect
###			H	Ridiculous
Only did particle effect, didn't end slow-mo, no kills.
This is the Bomb disaster one, so it will need special attention.
###			H	Slime
Only did particle effect
###			H	Stomp
Did particle effect
Pushed body away
Didn't stun anyone
###			H	Warp
Complete
###			H	Water
Body remained
###			√	Big
Complete
###			√	Normal
Complete
###			√	Huge
Complete
##		√H	Hire Type
###			C	00 Split off Hire Base Cost
Not referring to final cost, but the Hacker/Soldier cost tiers
###			H	Chloroform
New
###			H	Devour Corpse
New
###			H	Disarm Trap
New
###			H	Drink Blood
New
###			C	Hack
####			C	Cyber Nuke
Giving character Cyber Nuke allows Blow Up option, but it doesn't work
###			H	Handcuff
New
###			H	Mug
One-time use, mug target NPC
###			H	Pickpocket
New
###			H	Poison
New 
###			H	Safecrack
- Reticle does not activate 
	[Info   :  CCU_Core] DetermineButtons_Prefix: Hire
	[Info   :  CCU_Core] DetermineButtons_Prefix: Hire Order
	[Info   :  CCU_Core] PressedButton_Prefix: Method Call
	[Debug  :CCU_P_AgentInteractions]       buttonText: [CCU] Job - SafecrackSafe

Here's what comes up for Lockpick job:
	Agent
√		.GetCodeFromJob
√		.GetJobCode					Need to extend jobType enum
√		.ObjectAction
	AgentInteractions
√		.DetermineButtons
√		.LockpickDoor				
√		.PressedButton	
	GoalDoJob
√		.Activate					Check out 
√		.Terminate
	GoalDetails						E_GoalDetails
√		.LockpickDoorReal
	GoalLockpickDoor				GoalSafecrackSafe
√		.Activate
√		.Process
√		.Terminate
	GoalLockpickDoorReal			GoalSafecrackSafeReal
√		.Activate
√		.Process
√		.Terminate
	InvInterface
√		.ShowTarget					
√		.ShowTarget2
	ObjectMult
		.ObjectAction				Not sure yet - are these just logging messages, or are they important?
	PlayfieldObjectInteractions
√		.TargetObject

Objects to Analyze/track:
	Agent
		job
		jobCode
		target.targetType

In P_AgentInteractions.SafecrackSafe, I used JobType.GetSupplies as a placeholder until I figure out how to add to enums.
###			H	Set Explosive
On door or Safe: Plants door detonator
Elsewhere: Remote bomb
Gives you detonator when planted
###			H	Set Time Bomb
Based on and consumes Time Bombs in inventory. NPC starts with one.
###			H	Tamper
- Interface works but reticle is green for non-tamperable items.
  - Log message "Not implemented yet", fair enough
###			√	Bodyguard
Complete
###			√	Break In
Complete
###			√	Cause a Ruckus
Complete
##		√	Merchant Type
Complete
##		√H	Passive
###			C	Supernatural
Ghost-gibber vulnerable
###			C	Incorporeal
Ghost form
###			H	Vision Beams (Cop Bot)
DW
###			H	Invincible
New
###			H	Mute Dialogue
Cancels possible immersion-breaking dialogue tailored to vanilla NPCs
###			H	Reviveable (Infinite)
Instead of dying, agent will be Injured instead. Player can revive them or hire someone to do it unlimited times.
###			H	Reviveable (One)
Instead of dying, agent will be Injured instead. Player can revive them or hire someone to do it once.
###			H	Reviver
If hired and surviving, will revive the player once
###			H	Statue
Remove colors
Tint white
Make stationary, Invincible, non-reactive
###			H	Zombified
Agent.zombified
Agent.customZombified
###			√	Crusty
Complete
###			√	Extortable
Complete
###			C	Guilty
####			C	Cascade to Employees
SetRelationshipOriginal, under Drug Dealer
###			√	Innocent
Complete
###			√	Possessed
Complete
###			√	Status Effect Immune
Complete
###			√	Z-Infected
Complete
##		√H	Relationships - Faction
###			C	Faction Firefighter
###			C	Faction Cannibal
###			C	Faction Military
###			C	General concept
Friendly to faction doesn't align you. You do not inherit the faction's relationships.
Loyal causes you to inherit its relationships, but negative ones are moderated:
	Hostile → Annoyed
	Annoyed → Neutral
		The net effect of this one is that Loyalists are less likely to initate conflict on behalf of their faction.
Aligned means you fully inherit any faction-mandated relationships.
###			H	Config Files for unique player-defined factions
Generate traits based on these names
Allow multiple faction list files in a folder, to increase ease of compatibility.
###			H	00 Refactor
These should inherit from a shared class
public override char Faction => '1', etc.
public override string Relationship => VRelationship.Aligned, etc.
###			H	Faction 1 Annoyed
###			H	Faction 1 Friendly
###			H	Faction 1 Loyal
###			H	Faction 2 Annoyed
###			H	Faction 2 Friendly
###			H	Faction 2 Loyal
###			H	Faction 3 Annoyed
###			H	Faction 3 Friendly
###			H	Faction 3 Loyal
###			H	Faction 4 Annoyed
###			H	Faction 4 Friendly
###			H	Faction 4 Loyal
###			√	Faction 1 Aligned
###			√	Faction 1 Hostile
###			√	Faction 2 Aligned
###			√	Faction 2 Hostile
###			√	Faction 3 Aligned
###			√	Faction 3 Hostile
###			√	Faction 4 Aligned
###			√	Faction 4 Hostile
##		√H	Relationships - General
###			H	Class Unity
Like Class Solidarity, except Aligned
Note that Solidarity includes a No Infighting effect.
###			√	Hostile to Cannibal
Complete
###			√	Hostile to Soldier
Complete
###			√	Hostile to Vampire
Complete
###			√	Hostile to Werewolf
Complete
###			√	Relationless
Complete
##		√H	Relationships - Player
###			H	Player Secret Hate
###			√	Player Aligned
Complete
###			√	Player Annoyed
Complete
###			√	Player Friendly
Complete
###			√	Player Hostile
Complete
###			√	Player Loyal
Complete
###			√	Player Submissive 
Complete
##		√H	Trait Gates
###			H	Crust Enjoyer
If you have Upper Crusty, this character is Loyal
I think this is actually automatic with Enforcer
###			H	Gate Vendor
Won't sell unless you have appropriate trait
###			H	Gate Hire
Won't hire unless you have appropriate trait
###			√	Cool Cannibal
Complete
###			√	Bashable
Complete
###			√	Common Folk
Complete
###			√	Crushable
Complete
###			√	Cop	Access
Complete
###			√	Family Friend
Complete
###			√	Honorable Thief
Complete
###			√	Scumbag
Complete
###			√	Slayable
Complete
###			√	Specific Species
Complete
###			√	Suspicious Suspecter
Complete
#	H	Mutators
Focus on Traits for this version.
##		C	Requested features
- Random Disaster (Disasters Every Level not in editor)
- No Trait-Up
  - Allows designer control over character progression
##		C	00 Mutator List Population
###			C	Level Editor Mutator List
- I think this has since been addressed in Roguelibs, as of 3.5.0b
- Vanilla replacement worked, pending custom content
- [Error  : Unity Log] Coroutine 'SetScrollbarPlacement' couldn't be started!
  - This didn't affect anything but I'd like it resolved
###			C	00 Hide from Non-Editor access
- CreateMutatorListCampaign
###			√	General Mutator List
- Show up in LevelEditor UI, may be a manually constructed list
- LoadLevel.loadStuff2 @ 171
##		C	Branching
Basically allows options at Exit Elevator to choose the next level, and/or skipping levels on the level list

CampaignData
	levelNameList		List of strings
	levelList			List of LevelData
	mutatorList			List of strings
GC.sessionDataBig.curLevelEndless - 1
	Seems to be the main int counter of what level you're on

- LoadLevel
  - loadStuff2 @ 199
    - this.customLevel = this.customCampaign.levelList[n];

663:
	if (this.customCampaign.levelList[n].levelName == this.customCampaign.levelNameList[this.gc.sessionDataBig.curLevelEndless - 1])
		Transpile-replace the right side of this assignment to a custom method call that determines the new target level's int ID in the campaign level list

###			C	00 Usage Guide
This one will be complicated to explain, so it's best to go overboard on documentation and provide examples.
###			C	Label Alpha/Beta/Gamma/Delta
Labels a level
Mutually exclusive
###			C	Exit Alpha/Beta/Gamma/Delta per Flag A/B/C/D
If target boolean is true, exit to target level
###			C	Exit Alpha/Beta/Gamma/Delta
Destination at Elevator
###			C	Exit +1/2/3/4
Can have multiple, to allow Branching
Adds options at Elevator
###			C	Flag A/B/C/D false
For level access
###			C	Flag A/B/C/D true
For level access
###			C	Level Entry Requires A/B/C/D false
Gate level access
###			C	Level Entry Requires A/B/C/D true
Gate level access
###			C	Traits for Level Branching
##		T	Followers
###				00	Add conflicts
###			C	Homesickness Disabled
####			C	Set to Aligned
###			T	Homesickness Mandatory
Test
##		C	Gameplay
###			C	No Funny Business
For town levels. Ensures no one will be killed.
You will need to eliminate spontaneous hostiles for this to work, though.
##		C	Interface
In PlayerControl.Update there's a hidden keystroke for pressedInterfaceOff
###			C	No Minimap
##		C	Quests
###			C	Big Quest Exempt
Deactivate Big Quest for level, freeze mark counts
###			C	Big Quest Mandatory
Lose the game if you don't complete your Big Quest for this floor
Allows the creator to have greater control over the flow of the campaign
E.g., custom character with Doctor's big quest
###			C	Big Quest Stopping Point
Equivalent to Mayor's Village, where super special abilities activate if you completed the Big Quest
###			C	Major Contract
Main quest rewards are multiplied by 10
##		C	Utility
###			C	Sort active Mutators by Name
- ScrollingMenu.PushedButton @ 0006
  - Pretty much has exactly what you need.
#		H	Item Groups
Next release
#		H	Object Additions
##			C	Air Conditioner
Enable "Fumigating" w/ staff in gas masks as option
GasVent.fumigationSelected
##			C	Fire Hydrant
Ability to be already shooting water according to direction
##			C	Flaming Barrel
- Gibs (Black)
- Oil (Dark Green)
- Ooze (Yellow)
- Water (Blue)
##			C	Laser Emitter
###				C	Mode: Metal Detector
This would really only make sense with a Stop & Frisk mod.
##			C	Movie Screen
Allow Text like Sign
#		H	Player Utilities
This might need to be its own mod
##		H	Mouse3 Bind to command followers
- Target
  - Ground - All Stand Guard
  - Agent - All Attack
  - Self - All Follow
- Could also be an item or SA
##		H	Mutators to omit Vanilla content when custom is available
- If designer has added customs to be Roamers, or Hide in Bushes, etc., have some mutators to exclude Vanilla types from those spawning behaviors
##		H	Save Chunk Pack configuration between loads
- I.e., only deactivate chunk packs when the player says so!
  - This is useful but doesn't belong in CCU, it belongs in a QOL mod
##		H	Show Chunk info on Mouseover in Map mode
- When in gameplay map view, mouseover a chunk to see its name and author in the unused space in the margins.
  - Gives credit to author
  - Helps identify gamebreaking chunks, allowing you to not use the chunk pack or notify their author.
    - This is useful but doesn't belong in CCU, it belongs in a QOL mod
#		H	Demo Campaign - Shadowrun-but-not-really
Remember, simultaneous release would be a bad thing. Get it to a playable state so you can consistently test features with it, but don't need it finished yet.
##		C	Music Pack
###			C	Actually Making a Music Pack
How
###			C	Tracks
Cyberpunk / Darkwave?

		Track					Artist							Note
	Neo Tokyo				Perturbator					Super Tense
	Onna Musha				Vector Seven					Peaceful
	Black Bauhinia		Vector Seven					Dark
	Breach					Vector Seven					Not quite sure
	Arasaka Headquarters	Vector Seven					Sounds Stealthy
	Carnage				Vector Seven					Name fits but maybe not intense enough
	Acid Spit				Mega Drive						Fast
	Crimewave				Mega Drive						Dark, Urban
	Orbital Strike		Mega Drive						Robotty
	Drums A-Go-Go			Hollywood Persuaders			1960s campy heist, blues-box, but still a good energy
##		T	Player Character
"The Fixer," an old man who's not quite as tough as he used to be. But he has a lot of connections in the criminal world and knows how to put together a team. So this will direct the player to use a hiring-based playstyle.
Remove Low-Cost Jobs and add No In-Fighting
##		T	Home base (Perennial)
In between each mission, a slummy neighborhood that changes every time you see it
###			T	The Bar
Where the mercenaries hang out. All Talk interactions should tell you a bit about them.
Ensure that Buying a Round will affect all hires.
Feel free to use multiple copies of the same merc, since their appearances will vary! B)
Give them a "Permanent hire" trait
####			T	The Fence
He'll buy your shit from you. This should be a *rare* opportunity to sell stuff.
####			T	The Bartender
Maybe something clever to say. Maybe not.
####			T	The Mercs
- AHS Corp. Medbot
  - Pacifist, can be hired and sell healing
###			T	Hospital
- MedBots slowly replace the doctors
- Mission: Rescue the spare MedBot - adds them to your Vehicle
###			T	Fire Station
Privatized, and no one can afford them here
###			T	Hazardous Waste Handler's Union Local No. 606
One mutated guy who brags about his dad being a lifelong member
Rumors that they run a cannibal ring
###			T	Doc's
Skeezy place out back
###			T	Another Doc
Has to charge more because he doesn't work for criminals, and doesn't use robots
Sells his clinic to a medical corp, soon is replaced by MedBots
###			T	Coffin Hotel
Micro-apartments with an evil landlord
###			T	The "Vehicle"
Will start small, later evolve to carry an "away squad" that can serve as a mobile home base for healing, etc.
1 - Just a Van thing for a robbery
2 - Now has a Driver and 
##		T	The Projects
Reuse some Building Blocks chunks if you need
###			T	Exterior (possibly skip)
Slums city area
Try to do something cool that encourages player to use Factions to solve a problem
###			T	Ground Level
Gang has taken over the building and now runs a secure perimiter around the building
###			T	Basement
Destroy Squank production facilities and steal caches of it
##		T	Fud Factory
Semi-rural area, backwoods depressed town environs

###			T	Ground Floor
- Some redneck cabins out in the woods
  - Ensure there are a couple Barbecues here
  - They gripe about the factory poisoning the water and laying everyone off to employ robots
  - Sell Bombs & traps
  - Hire for cheap, all heavily armed
  - Red Hats & Camo 
- Disable Security Control Unit 
  - Destroy Alarm Buttons
    - If this actually works, that's an interesting strategy tension
- Shipping Dept.
  - Not a mission, but there's a lot of Fud in here if you can find a way to steal it. Heavily Secure.
- Overclocked Generators
  - There are four in a cross pattern, right at the edge of where a time bomb should be placed in the middle to hit all four. This isn't used yet, but later will be.
- Two routes to get into facility
  - Creek, there's a gap in the fence where it gets heavily wooded that opens on a less-secure area
  - Front entry, more security
- Goal: Freight Elevator
###			T	Production Basement level
- Agents:
  - Robot Workers
  - Humanish Technicians
  - Robot Managers
- Destroy 3 Power Boxes
- Cave tunnel to some weird kooky cave shit, entrance hidden by a shelf
###			T	Secret Sub-Basement Slave Quarters
- Facility breeds humans for slaughter because demand for Fud is so high
- Hellish Aesthetic
- Agents:
  - Fud Cattle Human
  - Fud Veal Flavor Baby Meat Baby
- Mission: Free Low-Fat Fud captives
  - Prison is a circular hallway with a conveyor belt pushing them in circles for exercise
- Mission: Kill Slavemasters & Rudy McFudy
  - Surrounded by Work Pits
###			T	Canning Plant
- Emerge on Freight Elevator with a bunch of freed slaves who now roam the level, getting picked off by the security team that arrives
  - Security team set to "Killer Robot" behavior? pwz
  - If they are some real killers, the challenge could be to just make it to the van with no hope of fighting them off
- Cramped Hive quarters for employees, virtually a prison
##		T	Underground
###			T	Subway to Cave Tunnel
###			T	Sewer City
Make sure some Ghostblasters are sold here
###			T	Catacombs
Necromancer Lair
##		T	Apollo Corp. Tower
###			T	Brief
Describe what quests represent beforehand in terms of plan
###			T	Ground Floor
- Initiate Facility Silent Lockdown (Press Button task)
- Destroy Offsite Security Team silent alarm conduit (Destroy Generator in base of a separate Radio Tower that you can see as you ascend or descend)
- Eliminate Onsite Security Team (Neutralize)
- Get to Employee Only Elevator
  - Interior route, through Atrium, security checkpoints etc. Brute force
  - Exterior route, stealth & distraction option
###			T	Dormitories
- Time to just fucking rob people 
- But you also need an Access code from the Chief Engineer, who you have to neutralize one way or another
- Show off affluent but empty lifestyle
- Get to Freight Elevator in Kitchen area to reach Infrastructure level
###			T	Infrastructure Sub-Basement
- Some underclass residential areas
  - The only place in Apollo where you see green skin
Mess with power to disable security safeguards of IT department
###			T	IT Department
- Living Quarters are utilitarian and futuristic, but coffin-like
- Lots of Gnomes & Dwarves
- Enable elevator access to Executive Suites
- Robots & Turrets & Shit
- This might be a good Killer Robot level, make it sort of mazelike
###			T	Executive Suites
- Almost all Human & Elf, zero Green up here
- It's a fuckin' PARTY up here. Execs and the hoi-polloi are living it up. There's still a lot of security but you should be able to play this as a social level.
- Wood floors w/ rugs, fireplaces, etc. whatever looks as fancy as possible
- Get Airship Key Fob for escape from the VP of Sales (Retrieve)
- Kill someone to settle a personal grudge? Maybe tie it into an earlier part of the plot (Eliminate)
Pool/Jacuzzi
Bar
Offices
###			T	Rooftop Escape
- Swat team is here. And they're blocking the escape, an airship you planned to hijack to get out of here. (Eliminate)
- Disable Swat team ship control override (Destroy Computer)
#		H	Demo Campaign - Wetwork
Progression & Patterns
  - Removal of base negative traits will affect certain early paths. 
    - Three as an example: Loud, Antisocial, Tech Illiterate
    - There should always be alternatives to killing non-targets, even if they're goons
    - Have up to 4 paths depending on which negatives the player has removed.
  - It should be clear to the player which enemies will go Annoyed versus Hostile.
  - Weapon gating
    - Very limited/controlled access to weapons
      - Mechanism: Heavily guarded weapon detector on subway platform entrances
    - Silenced pistol
    - Silenced revolver is a one-hit kill with Wetworker, eliminating guesswork
    - Sniping is only possible in the later levels, but it's fun as shit so it's worth the wait. 
      - Start it out loud, and give a silencer later.

Player Character
		Endurance	3
		Speed		3
		Ranged		3
		Melee		3
	Traits
		Tech Illiterate
		Loud
		Antisocial
		Pacifist
		Sniper - Limited to Pistol, call it a Rifle ban
		Wetworker (Doubletapper)
		Backstabber
		Sneaky Fingers - Tamper Kills!
	Special Ability
		Sticky Glove - You can control the circumstances where it's useful, at least
Items
	Generally Disposable - There should be a gate or two where the only way through is to ditch your weapons.
	Silenced Pistol - "Remember, always Double-Tap! A downed target is still a deadly one."
	Silenced Revolver - Allows Sniping, so it only comes into play halfway through the campaign.

Levels
First, get costs of Negative Trait removals to determine what will be doable at what point
1 - Ghetto
Do a string of burglaries while LOUD
Teach the player about the security camera sound trick
	Riskier with windows, since they're most likely to point AT a door and be next to a window
Rogue vision, night time. You don't know where chests will spawn.

"My contact called me and said his courier would be in a subway station bathroom."
Just a quick level with dialogue to 

|Level	|Area					|Notes	|
|:-----:|:----------------------|:------|
|	1	|Subway					|- Meet a contact, maybe see cool shit on the way. Free him from a "prison" like a subway bathroom.
#		Notes dump - Anything below here
Removed from Readme

##			Roamer Traits
- Add a trait to have that NPC show up in the list of available roaming NPCs in various districts.
  - E.g., make Junkie, add Roamer_Slums. Then make a level and in Features, his name will pop up. [Maybe possible through Trait OnAdded/OnRemoved behaviors]
  - Could also have this create a mutator with Custom Roamers, allowing designers to affect vanilla gameplay with new NPCs
  - LevelEditor.customCharacterList has all customs saved. Iterate through this and find appropriate traits, then add through RandomAgents.fillAgents
## CC Steam Workshop Upload

- Pop up Yes/No dialogue asking if user wants to do the following:
  - Automate screenshot of character portrait for thumbnail
  - Automate screenshot of character stat screen
  - Upload both screenshots into Character folder and name appropriately before upload
  - Output a Text file with all character content for upload as a description 
# Chunk Editor Utilities
To Sorquol
## Fields 
- Multiple In Chunk field for SpawnerAgent
## Item Groups *
- Know how you can select "Slum NPCs" as a type? Why can't we do that with items? 
## General *
- Spawn objects placed regardless of district (vanilla limits to district-appropriate objects)
- Red-tint any objects that might spawn inconsistently due to placement rules (e.g., Security Cams over gaps)