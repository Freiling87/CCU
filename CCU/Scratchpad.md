#	P	Header Symbol Notes
Listed in order of Parent tier summary symbol priority:
	P = Pinned to top (for notes)
	C, T = Code this, Test this
	N = Next release
	H = Hold, usually pending resolution of a separate or grouped issue
	√ = Fully implemented feature or group of features

#	P	Top-Priority Bugs
##		C	Uh AI broke
- Once I got to Industrial
  - Interactions proceed normally
  - Combat normal if provoked, but only within a certain distance, otherwise they stand or walk in place
  - Shopkeepers do not move, even if hostiled, but do go Submissive
  - Wander, Patrol, Knock
    - NPCs just stand in place
- The error persists even after leaving industrial, so something is actually breaking
##		C	Blood Junkie Fast Heal button wasn't using blood bags
- First time it occurred, need to log
  - Also can't manually drink themz
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
- I'm guessing this might be connected to Agent lights?

---

#	P	General Reminders & To-Dos

- I merged Passive and Interaction, but I'm thinking they need to be split back up. 
  - There was also a specific reason to make Extortable & Moochable into Passive instead of Interaction or Trait Trigger, but I can't remember it. Write it here if you do.
---


##	CT	All Interface
###		T	ScrollingButton
####		T	ScrollingButton Text Align Left
Added logging only
####		T	ScrollingButton Height
Attempted, font size only first
####		T	Keep ScrollingButton text size uniform rather than fit-to-width
Worked, but default size is way too big. You'd need to decrease font size as well.
Messed with fonts
###		CT	Visual Themes
####		C	Background Color
- UnityEngine.CanvasRenderer.SetColor
// LevelEditor.inputFieldList.isFocused
// InputField.ActivateInputField() & possibly DeadctivateInputField() at the end

// levelEditor.floors2Button.transform.Find("ButtonEdges").GetComponent<Image>().color = Color.white;
// White is for currently click-activated, but might use another color to show which is tab-active, pending player confirmation

// __instance.inputFieldList

// UnityEngine.UI.InputField.ActivateInputField()
####		C	Border Color
New
####		C	Hardcode replacement
Will need to find-replace any hardcoded colors in text, like "Required:" on traits, to fit primary, secondary, etc. colors per theme
####		T	Text Color
- Attempted
- See also:
  - UnityEngine.Canvas
    - I think this is the global UI aesthetic

#	C	Character Editor
##		C	Trait Hiding
###			C	Character Creator, Player Edition (CharacterCreation)
Possible future bug: If you create a character in DE, and edit/resave them in PE (hidden traits won't be visible), will it remove or keep their hidden traits?
Breaking mods up means the player edition should be 
###			√	Character Select (CharacterSelect)
Complete
###			√	Character Sheet (CharacterSheet)
Complete

#	C	Chunk Editor
##		C	Randomized Lights
New
##		N	Agent Goals
###			C	Arrested
New
###			C	Berserk
New
Just saves having to do Floor tricks
###			C	Commit Arson
New
In Vanilla
###			C	Die
New
###			C	Die + Burn
New
###			C	Escort
New
Follow Non-Owner without being hostile or warning
###			C	Explode
New
###			C	Follow
New
Follows the first Aligned Agent they see
Save that Agent ID and return them to them rather than following a new one
###			C	Injured
New
Interact to spend a First Aid Kit and revive them, getting an XP bonus and Friendly. Plus whatever that NPC can do.
Medical Professional: 50% chance to not spend Antidote
###			C	Knocked Out
New
###			C	Lookout
New
When alerted, will run towards nearest ally and alert them
###			C	Operate
New
Does "operating" animation like they're working on something
Halt if talked to
###			c	Panic
New
###			C	Protect (Lax)
New
Find nearest Important NPC, and stay near them. Allow interaction.
###			C	Protect (Strict)
New
Find nearest Important NPC, and stay near them. Prohibit interaction.
###			c	Punch
New
Just punch an object one square in front of you forever
Extremely limited, and noise will distract NPCs
###			C	Robot Clean
New
In vanilla
###			C	Robot Follow
New
Killer Robot behavior in vanilla
###			C	Sick
New
Interact to spend an Antidote and revive them, getting an XP bonus and Friendly. Plus whatever that NPC can do.
Medical Professional: 50% chance to not spend Antidote
###			C	Statue
New
Invincible, stationary, etc.
###			C	Wander behaviors (all) set to work within Prisons
New
###			C	Wander between Agents
New
###			C	Wander between Agents (Aligned)
New
###			C	Wander between Agents (Unaligned)
New
###			C	Wander between Objects & Operate
New 
See Operate
###			C	Yell
New
Will yell their Talk text? Unless you can get a second text box in there somewhere
##		N	District Object De-Limitation
###		C	BasicFloor
.Spawn (Fire Grate)
###		C	Computer	
.DetermineIfCanPoison
###		C	FlameGrate
.Start
###		C	Manhole
.Start
###		C	Pipe
.Start
###		C	SawBlade
.Start
###		T	SlimeBarrel
- New Attempt
###		C	SwitchFloor
Not sure about this one, may be too deeply hardcoded
###		C	Tube
.Start
###		C	WaterPump
.Start
##		N	Multiple In Chunk field for NPC Group selection
New

#	C	Documentation
##		C	Text files
###			C	Main ReadMe Page
####			C	Initial Pitch & addressing common concerns
####			C	Editor Section
####			C	Mutator Section
####			C	Traits Section
###			C	Chunk Editor Page
###			C	Level Editor Page
###			C	Promo Campaign Page
###			C	Mutator Page
####			C	Examples
###			C	Trait Page
####			C	Examples

#	C	Images
##		C	Font
Munro is the general interface font.
##		√	Logo 16x16
##		C	Logo 64_64
##		C	Steam Thumbnail caption

#	CT	Mutators
##		C	Level Editor Mutator List
- Vanilla replacement worked, pending custom content
- [Error  : Unity Log] Coroutine 'SetScrollbarPlacement' couldn't be started!
  - This didn't affect anything but I'd like it resolved
##		√	General Mutator List
- Show up in LevelEditor UI, may be a manually constructed list
- LoadLevel.loadStuff2 @ 171
##		C	Audio
###			M	Ambienter Ambience
New
Gate behind presence of objects/agents, e.g. disable Casino if all Slots are destroyed
##		C	Branching
Basically allows options at Exit Elevator to choose the next level, and/or skipping levels on the level list

CampaignData
	levelNameList		List of strings
	levelList			List of LevelData
	mutatorList			List of strings

- LoadLevel
  - loadStuff2 @ 199
    - this.customLevel = this.customCampaign.levelList[n];
###			C	00 Hide from Non-Editor access
- CreateMutatorListCampaign
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
###			C	Big Quest Stopping Point
- Cyan_Light:
  - The ability to specify a "big quest stopping point" in custom campaigns, essentially the vanilla mayor village effect of ending the quest and turning on super special abilities if you've completed it but you get to set which level this happens on in the campaign interface.
###			C	No Violence
New
For town levels
##		C	Interface
In PlayerControl.Update there's a hidden keystroke for pressedInterfaceOff
###			C	No Minimap
##		CT	Light Sources
- CameraScript.SetLighting
  - DW
- StatusEffects.WerewolfTransform
- StatusEffects.WerewolfTransformBack
- LoadLevel.SetNormalLighting 
- LoadLevel.SetRogueVisionLighting
###			C	Sepia
New
###			C	No Agent Lights
- The most recent attempt didn't make them move feet-first, but they still all have lights.
- Didn't work, and made the agent move feet-first
  - Same outcome for both locations of attempt
- Agent.hasLight
  - Postfix to false in
    - Agent.Awake
      - Attempted
        - DW
    - Agent.RecycleAwake
      - Attempted
        - DW
  - Note, there are a total of four attempts at this active so you'll need to pare down once you find a working one.
- Exclude Ghosts!
###			C	No Item/Wreckage Lights
- SpawnerMain.SetLighting2
  - Tried this another way
    - DW
###			T	No Object Glow
This is the yellow glow for when you have usable items with an object. As you collect more, eventually everything glows.
- gc.objectGlowDisabled
- gc.sessionDataBig.objectGlowDisabled
- Attempted, GC.Awake3 Prefix
###			C	No Object Lights
- Works!
- Need to exclude working machines with lights from this. Maybe jazz up their halos if possible.
- Fire sources are fine since the particle creates the light anyway.
###			C	New Moon
- Does not persist across saves
###			C	Player Agent Light Size
New
##		C	Quests
###			C	Big Quest Exempt
Deactivate Big Quest for level, freeze mark counts
##		C	Utility
###			C	Sort active Mutators by Name
- ScrollingMenu.PushedButton @ 0006
  - Pretty much has exactly what you need.
##		C	Wreckage
###			C	Bachelor-er Pads
Trash indoors
###			C	Dirtier Districts (Litter-ally the Worst)
New
Rename in code
###			C	Floral-er Flora
New
###			C	Shittier Toilets
New
###			C	Trashier Trashcans
New

#	C	Mod Split
##		C	Ambience Mutators
- Wreckage
- Lighting
- Ambient Audio
##		C	Civil Engineering
- Wall mods
- Floor mods
- City Size
##		C	CCU Mods

###			C	AI Traits

###			C	Campaign Mutators

###			C	Editor Hotkeys

#	C	Player Edition
- WHenever you have enough in the campaign to make it playable, test it in Player Edition and see if the experience is the same.

#	CT	Traits
##		C	Active
###			C	Clean Trash
New
###			C	Fight Fires
New
###			C	Grab Everything
New
###			C	Grab Food
New
###			C	Guard Door
New
###			C	Hobo Beg (Custom)
Maybe just implement the whole Hey, You! overhaul here
###			C	Hog Turntables
New
Allow paid Musician behavior
###			C	Seek & Destroy (Killer Robot)
New
###			C	Shakedown Player
New
Use this on leader w/ Wander Level
Use "Follow" behavior on agents placed behind them
No need for "Roaming Gang" Trait itself
###			C	Tattle (Upper Cruster)
New
###			√	Drink Blood
Complete
###			√	Eat Corpse
Complete
###			√	Grab Drugs
Complete
###			√	Grab Money
Complete
###			√	Pickpocket
Complete
##		C	Appearance				Group
###			C	Full-randomization bug
- Whole appearance is randomized when any appearance trait is added.
  - Should be a simple fix since it's doing less rather than more.
###			C	Factory Approach
- Since codewise these are entirely formulaic, it would be best to DRY and use Abbysssal's factory.
###			C	Facial Hair
- Main feature
  - Works
- Vanilla facial hair no longer seems to spawn
  - I think you just need to check for whether the agent is a player, and omit them from appearance traits.
###			C	Hair Type
Go ahead and try. Knowing the code they all work differently anyway :)
###			C	Skin Color
Go ahead and try. Knowing the code they all work differently anyway :)
###			√	Bugged Appearance
- Shows up with a little brown notch on the East side of the face regardless of orientation. It looks like the mustache but it's hard to tell.
  - Has not recurred.
##		C	Bodyguarded
- There are a few other hits that came up in a string search (possibly "Musician"):
  - LoadLevel.SpawnStartingFollowers
  - ObjectMult.StartWithFollowersBodyguardA
    - Ignore this one, it's for the Player Bodyguard trait
- Check out ObjectMult.StartWithFollowers, there are something like 4 similarly named methods in there
###			C	Pilot Trait
No errors, but no effect. Logging time
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
###		C	Branching
####		C	If Paid then A/B/C/D True
New
####		C	If Killed then A/B/C/D True
Etc.
##		C	Combat
###			C	Cause Lockdown
New
###			√	Coward
Complete
###			√	Fearless
Complete
###			√	Use Drugs in Combat
Complete
##		C	Factions
###			√	Aligned to Faction 1-4
Complete
###			C	Annoyed at Faction 1-4
###			C	Friendly to Faction 1-4
###			√	Hostile to Faction 1-4
Complete
##		CT	Hire
###			C	Cause a Ruckus
- Almost completely works, but character doesn't do animation
###			C	Chloroform
New
###			T	Cost - Banana
Test
###			T	Cost - Less
Test
###			T	Cost - More
Test
###			C	Devour Corpse
New
###			C	Disarm Trapdd
New
###			C	Drink Blood
New
###			C	Handcuff
New
###			C	Permanent Hire
New
~8x normal hire price
###			C	Permanent Hire Only
New
###			C	Pickpocket
New
###			C	Place Time Bomb
New
Based on and consumes Time Bombs in inventory. NPC starts with one.
###			C	Poison
New 
###			C	Safecrack
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
###			C	Set Explosive
On door or Safe: Plants door detonator
Elsewhere: Remote bomb
Gives you detonator when planted
###			C	Tamper
- Interface works but reticle is green for non-tamperable items.
  - Log message "Not implemented yet", fair enough
###			√	Bodyguard
Complete
###			√	Break In
Complete
###			√	Hack
Complete
##		C	Map Marker
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
##		C	Merchant (Buyer/Vendor)		Group
###			C	Buyer
Move VendorBuyer traits to this group
####			C	Buyer All
New
####			C	Buyer Only
For using VendorTypes to define Buyer Vendor Type, while blocking actual Vendor behavior
####			C	Buyer Vendor Type
New
####			C	Pay Banana
New
####			C	Pay More
New
####			C	Pay Less
New
###			C	Merchant
####			C	Buyer/Merchant/Vendor Trait Refactor
- Vendor & Buyer traits have a weird setup right now, with Buyer having a strange secondary relationship to Vendor that will lead to some arcane trait configurations.
    - Change Vendor Types to Merchant Types
    - Keep the actual passive behaviors as Buyer & Vendor.
      - Add Requirement: Merchant Type Trait
  - Will need Buyer Vendor to access the rLists/rNames you defined. There are two paths I see:
    - Re-access those and learn whatever strange setup they have
    - Or convert Merchant Type rName/rList rote code into an equally rote Dictionary<string, int> stored in TraitManager or elsewhere
      - Would imply developing a method that enters that into the rList/rName format, to replace the existing code with something that at least follows DRY principle. 
      - But if you have to write one for the Buyer traits anyway, the above will practically be written at the same time.
###			C	Vendor
####			√	General Notes
####			C	Cost Banana
New (erroneously marked as complete, must have meant hire)
####			C	Get full list of Vendor traits to test and complete category
##		C	Passive
###			C	Accept Bribe (Banana)
New
###			C	Accept Bribe (Beer/Whiskey)
New
###			C	Accept Bribe (Money)
New
###			C	Administer Blood Bag
New
###			C	Arena Manager
New
###			C	Bank Teller
New
###			C	Blood Bank Clerk
New
###			C	Buy Round
New
###			C	Cybernetic Surgery
Curated Trait-seller
###			C	Deportation Center Clerk
New
###			C	Explode On Death
Works but... exploded when arrested 
###			T	Extortable
Test
###			C	Fence
New
NOT a Vendor trait
###			C	Hackable - Tamper With Aim
New
###			C	Hackable - Go Haywire
New
###			C	Heal
New
###			C	Hire for Level
New
###			C	Hire Permanently
- Multiple skill useTHE 
- Stay until death
###			C	Hotel Clerk
New
###			C	Identify
New
###			C	Improve Relations w/ Faction 1-4
New
Costs $1000, improves your relations with that faction
###			C	Influence Election
New
###			C	Innocent
New
XP Penalty for neutralizing
###			C	Invincible
New
###			C	Mayor Clerk
New
###			T	Moochable
Test
###			C	Offer Motivation
New
###			C	Quest Giver
New
###			C	Refill Guns
New
###			C	Repair Armor
New
###			C	Repair Weapons
New
###			C	Reviveable (Infinite)
Instead of dying, agent will be Injured instead. Player can revive them or hire someone to do it unlimited times.
###			C	Reviveable (One)
Instead of dying, agent will be Injured instead. Player can revive them or hire someone to do it once.
###			C	Reviver
If hired and surviving, will revive the player once
###			C	Sell Faction Intel 1-4
New
Costs $1,000 to bump reputation up one level (Hostile → Annoyed etc)
"Improve Faction Relations"
Should only allow for 1 of these to simplify algorithm
But this means you'll need further faction traits
###			C	Sell Slaves
New
###			C	Statue
Remove colors
Tint white
Make stationary, Invincible, non-reactive
###			C	Summon Professional
New
Pay a fee for him to teleport a Hacker, Thief, Doctor or Soldier to you. You still have to pay them to hire them.
###			C	Train Attributes (Split to each)
New
###			C	Train Traits - Defense
New
Sell traits for double their Upgrade Machine cost
###			C	Train Traits - Guns
New
###			C	Train Traits - Melee
New
###			C	Train Traits - Movement
New
###			C	Train Traits - Social
New
###			C	Train Traits - Stealth
New
###			C	Train Traits - Trade
New
###			C	Use Bloodbag
New
###			C	Vision Beams (Cop Bot)
New
###			√	Guilty
Complete
##		CT	Relationships
###			C	Aggressive (Cannibal)
New
###			T	Annoyed At Suspicious
Attempted
###			C	Faction Traits
Expand to all relationship levels
###			C	Faction Trait Limitation to Same Content
Limit interaction to same campaign, and if not campaign then same chunk pack
###			T	Hostile to Cannibals
Attempted
###			T	Hostile to Soldiers
Attempted
###			T	Hostile to Vampires
Attempted
###			T	Hostile to Werewolves
Attempted
###			C	Musician Trait for Random Stans
New
###			C	Never Hostile
New
###			C	Secretly Hostile
A la Bounty disaster
###			C	Vanilla Faction Traits
For allying people and factions to Crepe/Blahd, etc.
##		C	Spawn
###			C	Enslaved
New
###			C	Hide In Bush
New
###			C	Hide In Manhole
New
###			C	Roaming Gang
New
###			C	Slave Owner
NEw			
##		T	Trait Gates
###			C	Customizers
- Lock Merchant behind Trait Gate
- Lock Hire behind Trait Gate
###			CT	Common Folk
- Dummy A
  - Not blocked by lacking trait
Hobos Aligned
Only SetRelationshipInitial - search for other occurrences of this trait in the code.
###			CT	Cool Cannibal
- Dummy D
  - Blocked
- Disable Cannibal Hostility
  - Pending that trait
- Also set gates to playing vanilla cannibal (lacks CWC trait)
###			C	Family Friend
- Aligned
  - Works
- Shop Gate
  - Not gated
- Also set gates to playing vanilla Mobster
w###			√	Honorable Thief
Complete
###			T	Scumbag
Hostile to Scumbag Slaughterer
Only SetRelationshipInitial - search for other occurrences of this trait in the code.
###			√	Cop	Access
Complete
##		C	Utility
###			C	Hide Traits in Collapsed Groups
- While in Character Creator, hide traits in Collapsed Groups
  - Once all traits are in they're going to get hard to manage. 
##		N	Agent Group
###			C	Slum NPCs (Pilot)
New
###			H	Affect Campaign
Pending pilot
###			H	Affect Vanilla 
Pending pilot
###			C	Roamer Level Feature
New
##		N	Loadout
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

#	H	Demo Campaign - Shadowrun-but-not-really
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
  - Might also want an "Infinite Continues" mutator for long-ass campaigns
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

#	N	Item Groups
Next release

#	N	Object Additions
##		C	Air Conditioner
Enable "Fumigating" w/ staff in gas masks as option
GasVent.fumigationSelected
##		C	Fire Hydrant
Ability to be already shooting water according to direction
##		C	Flaming Barrel
- Gibs (Black)
- Oil (Dark Green)
- Ooze (Yellow)
- Water (Blue)
##		C	Movie Screen
Allow Text like Sign

#	√	Campaign Editor
No features planned yet

#	√	Chunk Pack Editor
No features planned yet

#	√	Level Editor
None yet