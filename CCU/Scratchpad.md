#	Test Notes / Bugfixing
##		00 Initial Load Errors

---
##		Chunk Editor Shortcuts

Ctrl + O load shows all menus but doesn't load anything
F9 load works but shows chunk selection menu

Ctrl + S works fine
F5 Save works but shows menus

Ctrl Numkeys

Q,E 
	Default to North as from-direction if None

Arrows 
	No effect

Ctrl A
	Select all works
	Toggle off doesn't

ADD:
	ALT trail for overhead menus
	Maybe [1][2],etc. indicators on menu buttons as hotkey hints

---
##		Hire AI Update Error
- Hired NPC. Once hired, they couldn't move and framerate skipped
  - The error message goes to A MoveNext that calls BrainUpdate.MyUpdate, so that's our main culprit
  - However, there's a possibility the real issue is in agent.pathfindingAI.UpdateTargetPosition(), and adding these missing declarations in PressedButton_Prefix will resolve a pathfinding issue that was causing the break
    - Nope, that wasn't it. 
  - Occurs for both Bodyguard & targeted skill jobs

[Info   :  CCU_Core] DetermineButtons_Prefix: Method Call
[Info   :  CCU_Core] HasTraitFromList: Method Call
[Info   :  CCU_Core] GetOnlyTraitFromList: Method Call
[Info   :  CCU_Core] HasTraitFromList: Method Call
[Info   :  CCU_Core] DetermineButtons_Prefix: Vendor
[Info   :  CCU_Core] HasTraitFromList: Method Call
[Info   :  CCU_Core] DetermineButtons_Prefix: Hire
[Info   :  CCU_Core] DetermineButtons_Prefix: Hire Initial
[Info   :  CCU_Core] PressedButton_Prefix: Method Call
[Error  : Unity Log] AI Update Error: Custom (1130) (Agent) ← Same error

---
##		Safecrack Job
Ready to test
##		Vendor Shops
Ready to test

---

#	Implementation
##		Appearance
RandomSkinHair
	fillSkinHair
##		Behavior
###			Eat Corpse
###			Grab Drugs
###			Grab Money
###			Pickpocket
###			Suck Blood
##		Hire
###			Bodyguard
###			Break In
###			Cause a Ruckus
###			Cost - Banana
###			Cost - Less
###			Cost - More
###			Hack
###			Safecrack

Here's what comes up for Lockpick job:
	Agent
√		.GetCodeFromJob
√		.GetJobCode					Need to extend jobType enum
√		.ObjectAction
	AgentInteractions
√		.DetermineButtons
√		.LockpickDoor				
√		.PressedButton				Passed name CauseRuckus to invInterface.ShowTarget, to avoid errors for now
	GoalDoJob
√		.Activate
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
√		.ShowTarget					Doesn't need patch, but uses GetName(myTargetType, "Interface"); so a Name will probably need to be made for our job types
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
####			Tamper
Pending results of Safecrack attempt
##		Interaction
###			Extortable
###			Fence
###			Moochable
###			Vendor Buyer
##		Item Groups
uwumacaronitime's idea: Item groups similar to NPC groups

I can see this going two ways: 
- As a trait for NPCs to generate with
- As a designated item in the chunk creator for use in NPC & Object inventories. 

I am leaning towards implementing both of these. But whichever is chosen, make it very clear to avoid confusion.
##		Loadout
  - InvDatabase.FillAgent
##		Relationships
###			Faction Traits
###			Vanilla Faction Traits
For allying people and factions to Crepe/Blahd, etc.
##		Trait Triggers
###			Honorable Thief
Worked for pickpocket
##		UI - Trait Hiding

###			Character Creation, Player Edition (CharacterCreation)

Possible future bug: If you create a character in DE, and edit/resave them in PE (hidden traits won't be visible), will it remove or keep their hidden traits?

###			Character Select (CharacterSelect)

Aspects to hide from CharSelect screen:

Behavior
	Not sure
Facial Hair
	Doesn't Work
Hire
	Works
Hire Specials (Not on list)
	Doesn't work
Trait Trigger
	Doesn't Work
Vendor
	Works

###			Character Sheet (CharacterSheet)

Behavior
	Works
Facial Hair
	Doesn't Work
Hire
	Works
Hire Specials (Not on list)
	Doesn't work
Relationships
	Doesn't work
Trait Trigger
	Doesn't Work
Vendor
##		Vendor
###			General
- Shop worked, but was empty

Agent
√	.CanShakeDown					For Extortable
InvDatabase
√	.AddRandItem					AccessTools returning void
√	.FillSpecialInv					Skip
PlayfieldObject
√	.determineMoneyCost
RandomAgentItems
√	.fillItems


Analyzing: agent.hasSpecialInvDatabase
Analyzing: agent.SpecialInvDatabase

Agent
√	.CanShakeDown
√	.RecycleStart2					Skip
√	.RevertAllVars					Skip
AgentInteractions
√	.DetermineButtons
ObjectMult
√	.InteractSuccess				Skip
PlayfieldObject
√	.SetupSpecialInvDatabase		Skip
PoolsScene
√	.ResetAgent						Skip
StatusEffects
√	.SetupDeath						Skip, Shopdrops will be automatic


- May need to put in behavior that Musician can visit Vendors and gifts a matching item type. 
- Traits that will need compatibility:
  - Shop Drops
  - That one discount one
###			Thief Vendor
- Special Inv filling: 
  - InvDatabase.FillSpecialInv
  - InvDatabase.AddRandItem
    - @1246: base.CompareTag("SpecialInvDatabase")
      - I am hoping this is taken care of automatically. Testing will show.
  - Keep an eye out for agent name checks, one of these will need to be patched
  - InvDatabase.rnd.RandomSelect(rName, "Items")