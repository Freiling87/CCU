# Run error logs

## Implementing Shops
- Shop worked, but was empty

Agent
	.CanShakeDown
InvDatabase
√	.AddRandItem					AccessTools returning void
	.FillAgent
	.FillSpecialInv
PlayfieldObject
	.determineMoneyCost
	.determineMoneyCostSelling
RandomAgentItems
√	.fillItems


Analyzing: agent.hasSpecialInvDatabase

Agent
	.CanShakeDown
	.RecycleStart2
	.RevertAllVars
AgentInteractions
	.DetermineButtons
ObjectMult
	.InteractSuccess
PlayfieldObject
	.SetupSpecialInvDatabase
PoolsScene
	.ResetAgent
StatusEffects
	.SetupDeath


Analyzing: agent.SpecialInvDatabase


## Hire AI Update Error
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
# Notes / Bugfixing

## Chunk Editor Shortcuts

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

## Appearance Traits

RandomSkinHair
	fillSkinHair

## Vendors
Check out "VendorsDropShopItem" (Shop Drops), it looks like agent.specialInvDatabase is the right way to go

- May need to put in behavior that Musician can visit Vendors and gifts a matching item type. 
- Traits that will need compatibility:
  - Shop Drops
  - That one discount one

---
# Implementation

## Safecrack job

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

## Trait hiding

### Character Creation, Player Edition (CharacterCreation)

Possible future bug: If you create a character in DE, and edit/resave them in PE (hidden traits won't be visible), will it remove or keep their hidden traits?

### Character Select (CharacterSelect)

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

### Character Sheet (CharacterSheet)

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

## Thief Hire

## Thief Honorable
Worked for pickpocket

## Thief Vendor
- Special Inv filling: 
  - InvDatabase.FillSpecialInv
  - InvDatabase.AddRandItem
    - @1246: base.CompareTag("SpecialInvDatabase")
      - I am hoping this is taken care of automatically. Testing will show.
  - Keep an eye out for agent name checks, one of these will need to be patched
  - InvDatabase.rnd.RandomSelect(rName, "Items")

## Agent Loadouts
  - InvDatabase.FillAgent