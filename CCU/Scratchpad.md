#		Header Symbol Notes
Listed in order of Parent tier summary symbol priority:
	C, T = Code this, Test this
	H = Hold, usually pending resolution of a separate or grouped issue
	√ = Fully implemented feature or group of features
#		Top-Priority Bugs
##		C	Blood Junkie Fast Heal button wasn't using blood bags
- First time it occurred, need to log
  - Also can't manually drink them
##		C	Infinite use hire?
- Slum Dweller had offered Ruckus twice, not sure if that's ever possible in vanilla
##		C	Mech Broken
- When re-entering mech, it works fine but the sprite doesn't move:
	[Info   : Unity Log] HideInventory Sound Debug: False - False - False - False - False - False
	[Error  : Unity Log] ArgumentNullException: Value cannot be null.
	Parameter name: key
	Stack trace:
	System.Collections.Generic.Dictionary`2[TKey,TValue].FindEntry (TKey key) (at <44afb4564e9347cf99a1865351ea8f4a>:0)
	System.Collections.Generic.Dictionary`2[TKey,TValue].TryGetValue (TKey key, TValue& value) (at <44afb4564e9347cf99a1865351ea8f4a>:0)
	RogueLibsCore.CustomItemFactory.TryCreate (InvItem instance, RogueLibsCore.IHook`1[InvItem]& hook) (at <d35155fde6a3417a9000d4114e51e814>:0)
	RogueLibsCore.RogueLibsPlugin.InvItem_SetupDetails (InvItem __instance) (at <d35155fde6a3417a9000d4114e51e814>:0)
	InvItem.SetupDetails (System.Boolean notNew) (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	Agent.SetupAgentStats (System.String transformationType) (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	SpawnerMain.TransformAgent (Agent myAgent, System.String agentType, System.String transformationType) (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	StatusEffects.MechTransform (Agent chosenMech) (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	StatusEffects.MechTransformStart (Agent chosenMech) (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	StatusEffects.PressedMechTransform () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	StatusEffects.PressedSpecialAbility () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	CCU.Patches.Agents.P_AgentInteractions.DetermineButtons_Prefix (Agent agent, Agent interactingAgent, System.Collections.Generic.List`1[T] buttons1, System.Collections.Generic.List`1[T] buttonsExtra1, System.Collections.Generic.List`1[T] buttonPrices1, AgentInteractions __instance, System.Collections.Generic.List`1[System.String]& ___buttons, System.Collections.Generic.List`1[System.String]& ___buttonsExtra, System.Collections.Generic.List`1[System.Int32]& ___buttonPrices, Agent& ___mostRecentAgent, Agent& ___mostRecentInteractingAgent) (at <ff2bba522a4541fd913dcede4cf1f47e>:0)
	AgentInteractions.DetermineButtons (Agent agent, Agent interactingAgent, System.Collections.Generic.List`1[T] buttons1, System.Collections.Generic.List`1[T] buttonsExtra1, System.Collections.Generic.List`1[T] buttonPrices1) (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	Agent.DetermineButtons () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	PlayfieldObject.Interact (Agent agent) (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	Agent.Interact (Agent otherAgent) (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	InteractionHelper.UpdateInteractionHelper () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	Updater.UpdateInterface () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	Updater.Update () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)

---
#		General Reminders & To-Dos
##			C	Trait Class Refactor
###				C	CCU Trait
####			C	List<string> CCU_Categories
To avoid having to maintain string lists, use Linq expressions instead from RogueFramework.Unlocks
####				C	CCU_DisplayName()
  - name = GenerateCCUName(trait) →
    - [CCU] + Last namespace + Nameof(trait)
###				C	Interaction Trait
Subclass of CCU trait
string: ButtonText
This will handle most cases
##		H	Config Files
###				Custom Flag list
Allow player to name booleans uniquely.
These should be grouped by Campaign, since that's the only place they're valid.
The config file should match the name of the campaign, if they allow the same characters.
###				Custom Level Tag List?
Not so sure about the utility of this. I don't think players should need more than 4 level tags.
##	C	Player Edition
- Whenever you have enough in the campaign to make it playable, test it in Player Edition and see if the experience is the same.
#	CT	Traits
##		T	Hack
###			T	Go Haywire
Attempted
###			T	Tamper With Aim
Attempted
##		H	Interaction
###			C	Shelf?
This is going to be pretty elaborate, unfortunately. Might want to shelf anything that's really complex. 
Plus, it sounds like Agent Intearctions might be next in line for RogueLibs.
SORRY, category!
###			C	00 Cost Display
Bananas & alcohol are hardcoded
To display them correctly, prefix WorldSpaceGUI.ShowObjectButtons (interprets magic numbers)
You might be able to simply remove 
###			C	Blood Bag - Administer
Might need extratext to work
Setting to Hospital chunk didn't work
###			C	Blood Bag - Donate
Might need extratext to work
###			C	Blood Bag - Use
New
###			C	Borrow Money
Moocher one worked, but not this one
Setting to Bank chunk didn't work
###			C	Bribe Cops
Check gates
###			C	Bribe For Entry
Check gates
###			C	Bribe For Entry (Alcohol)
Check gates
###			C	Buy Round
####			C	Add Cost
Didn't show up with vanilla text
###			H	Buy Slave
Pending actual assignment of owned slaves
###			C	Heal (Player)
Not showing up after cost text added
###			C	Identify
Interface works, but unidentified items are not enabled to click
###			C	Manage Chunk
New
###			C	Pay Debt
Technically works but didn't show/charge a cost
###			C	Play Bad Music
New
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
###			H	Improve Relations w/ Faction 1-4
New
Costs $1000, improves your relations with that faction
###			H	Quest Giver
New
###			H	Refill Guns
New
###			H	Repair Armor
New
###			H	Repair Weapons
New
###			H	Sell Faction Intel 1-4
Costs $1,000 to bump reputation up one level (Hostile → Annoyed etc)
"Improve Faction Relations"
Should only allow for 1 of these to simplify algorithm
But this means you'll need further faction traits
###			H	Start Election
New
###			H	Summon Professional
New
Pay a fee for him to teleport a Hacker, Thief, Doctor or Soldier to you. You still have to pay them to hire them.
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
###			H	Untrusting
This character must be Friendly or better to unlock Interaction options
###			H	Visitor's Badge
Set Bribe options on separate traits
###			√	Borrow Money (Moocher)
Complete
###			√	Influence Election
Complete
###			√	Leave Weapons Behind
Complete
###			√	Offer Motivation
Complete
##		T	Passive
###			T	Crusty
Agent.upperCrusty
###			T	Innocent
####			C	XP Penalty for neutralizing innocent
###			T	Possessed
Attempted
###			T	Status Effect Immune
Attempted
###			T	Vision Beams (Cop Bot)
Attempted
###			T	Z-Infected
Agent.zombieWhenDead
###			H	Invincible
New
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
###			√	Explode On Death
####			H	Explodes when Arrested
Not too concerned, considering this is vanilla for Slaves.
###			√	Extortable
Complete
###			√	Guilty
Complete
##		CT	Relationships
###			C	Hostile to Werewolf
Hostile from start, but this isn't how it works Vanilla. I think they need to see you transform.
Check out agent.originalWerewolf & secretWerewolf
###			T	Player Aligned
###			T	Player Annoyed
###			T	Player Friendly
###			T	Player Hostile
####			C	Cool Cannibal Interaction
###			T	Player Loyal
###			T	Player Submissive
###			T	Relationless
###			C	Class Unity
Like Class Solidarity, except Aligned
Note that Solidarity includes a No Infighting effect.
###			H	Secretly Hostile
A la Bounty disaster
Agent.bountyHunter
Agent.secretHate
Agent.choseSecretHate
###			√	Hostile to Cannibal
Complete
###			√	Hostile to Soldier
Complete
###			√	Hostile to Vampire
Complete
##		CT	Trait Gates
###			T	Cool Cannibal
- Dummy D
- Now this can be tested with Player Hostile
###			T	Bashable
####			T	Alignment
####			T	Hostility
####			T	XP Bonus
###			T	Crushable
####			T	Alignment
####			T	Hostility
####			T	XP Bonus
###			T	Slayable
####			T	Hostility
####			T	XP Bonus
###			T	Specific Species
####			T	Hostility
####			T	XP Bonus
###			H	Crust Enjoyer
If you have Upper Crusty, this character is Loyal
I think this is actually automatic with Enforcer
###			H	Gate Vendor
Won't sell unless you have appropriate trait
###			H	Gate Hire
Won't hire unless you have appropriate trait
###			√	Common Folk
Complete
###			√	Cop	Access
Complete
###			√	Family Friend
Complete
###			√	Honorable Thief
Complete
###			√	Scumbag
Complete
###			√	Suspicious Suspecter
Complete
##		H	Appearance
###			C	Full-randomization bug
- Whole appearance is randomized when any appearance trait is added.
  - Should be a simple fix since it's doing less rather than more.
###			C	Facial Hair
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
##		H	Booleans
Might need to limit this to a single flag, since having multiple true at the same time would complicate things
###			C	If Paid then Flag A/B/C/D True
New
###			C	If Killed then Flag A/B/C/D True
Etc.
##		H	Cost
These should apply to Hiring as well as Vendor traits
###			C	Cost - Alcohol
A la Bouncer
###			T	Cost - Banana
Test
###			C	Cost - Free
Free followers, aligned suppliers, etc.
###			T	Cost - Less
Test
###			T	Cost - More
Test
##		H	Hire Duration
###			C	Fairweather
Hiree will leave if they're damaged in combat
###			C	Homesickness Mandatory
Overrides Homesickness Killer
###			C	Homesickness Disabled
###			C	Permanent
New
~8x normal hire price
###			C	Permanent Only
As above, but removes the single-use hire option.
##		H	Map Marker
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
###			√H	Drug Warrior
####			C	Subtypes
Uses only a specific drug on entering combat:
						Always Crit
	Angel Duster		Invincible
						Invisible
	Stimpacker			Regenerate Health
	Cokehead			Speed
	Roid Rager			Strength
###			H	Lockdowner (R.I.P.)
Apparently Lockdown walls are broken in custom levels.
###			√	Coward
Complete
###			√	Fearless
Complete
##		√H	Faction
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
##		√H	Hire
###			H	Chloroform
New
###			H	Devour Corpse
New
###			H	Disarm Trap
New
###			H	Drink Blood
New
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
###			√H	Cause a Ruckus
####			C	Doesn't say Dialogue
###			√	Bodyguard
Complete
###			√	Break In
Complete
###			√	Hack
Complete
#		H	Mutators
Focus on Traits for this version.
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

- LoadLevel
  - loadStuff2 @ 199
    - this.customLevel = this.customCampaign.levelList[n];
###			C	00 Usage Guide
This one will be complicated to explain, so it's best to go overboard on documentation and provide examples.
###			C	Exit Alpha/Beta/Gamma/Delta
Destination at Elevator
###			C	Exit +1/2/3/4
Can have multiple, to allow Branching
Adds options at Elevator			
###			C	Level Alpha/Beta/Gamma/Delta
Designates a level as a place to return to
###			C	Set A/B/C/D false
For level access
###			C	Set A/B/C/D true
For level access
###			C	Require A/B/C/D false
Gate level access
###			C	Require A/B/C/D true
Gate level access
###			C	Traits for Level Branching
##		T	Followers
###			T	Homesickness Disabled
- Attempting with setting CanGobetweenLevels after SetupAgentStats, but I think you'll need it in multiple places.
- This is acting unexpectedly, but next time you test be clear on the rules.
- NPCs who WILL follow:
  - Freed Prisoners
  - Spawned Clones
  - Friend Phone
- I'm beginning to think maybe I just misunderstood something when I first saw this bug. Try it again and see what happens.
- Now, the *failure* to skip homesickness with the mutator surprised me
###			T	Homesickness Mandatory
- First attempt
Always dismiss hires at end of level, even if Homesickness Killer is active
ExitPoint.EmployeesExit prefix
##		C	Gameplay
###			C	No Funny Business
New
For town levels. Ensures no one will be killed.
You will need to eliminate spontaneous hostiles for this to work, though.
###
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