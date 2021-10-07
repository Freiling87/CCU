#	Test Notes / Bugfixing
##		00 Initial Load Errors

##		Chunk Editor
- Tried to place Safe, then this: 
	[Error  : Unity Log] IndexOutOfRangeException: Index was outside the bounds of the array.
	Stack trace:
	tk2dRuntime.TileMap.RenderMeshBuilder.BuildForChunk (tk2dTileMap tileMap, tk2dRuntime.TileMap.SpriteChunk chunk, tk2dRuntime.TileMap.ColorChunk colorChunk, System.Boolean useColor, System.Boolean skipPrefabs, System.Int32 baseX, System.Int32 baseY) (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	tk2dRuntime.TileMap.RenderMeshBuilder.Build (tk2dTileMap tileMap, System.Boolean editMode, System.Boolean forceBuild) (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	tk2dTileMap.Build (tk2dTileMap+BuildFlags buildFlags) (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	tk2dTileMap.Build () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	LevelEditor.SetTileName () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	LevelEditor.LeftClickAction (LevelEditorTile myTile) (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	LevelEditor.PressedMouseButton (System.Int32 buttonNum) (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	LevelEditor.Update () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
  - Remove SpritepackLoader and see what happens

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
- Don't test this with Safecrack yet - more vanilla ones will help identify issue

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

- Check out brain.AddSubgoal, as jobs are passed to it3
- After killing a bugged Agent:
	CCU.Patches.Behaviors.P_GoalDoJob.Terminate_Prefix (GoalDoJob __instance) (at <2052eae91fad498b965def95486033b6>:0)
	GoalDoJob.Terminate () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	System.DefaultBinder.BindToMethod (System.Reflection.BindingFlags bindingAttr, System.Reflection.MethodBase[] match, System.Object[]& args, System.Reflection.ParameterModifier[] modifiers, System.Globalization.CultureInfo cultureInfo, System.String[] names, System.Object& state) (at <44afb4564e9347cf99a1865351ea8f4a>:0)
	System.RuntimeType.CreateInstanceImpl (System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, System.Object[] args, System.Globalization.CultureInfo culture, System.Object[] activationAttributes, System.Threading.StackCrawlMark& stackMark) (at <44afb4564e9347cf99a1865351ea8f4a>:0)
	System.Activator.CreateInstance (System.Type type, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, System.Object[] args, System.Globalization.CultureInfo culture, System.Object[] activationAttributes) (at <44afb4564e9347cf99a1865351ea8f4a>:0)
	System.Activator.CreateInstance (System.Type type, System.Object[] args) (at <44afb4564e9347cf99a1865351ea8f4a>:0)
	CCU.CoreTools.GetMethodWithoutOverrides[T] (System.Reflection.MethodInfo method, System.Object callFrom) (at <2052eae91fad498b965def95486033b6>:0)
---
##		Safecrack Job
Ready to test
##		Vendor Shops
- Still empty, need more logging. After PressedButton("Buy")
  - Attempted
- I think they do have a SpecialInvDatabase, but the lists aren't working. I think it's pulling names via agentname instead of your intended way.
  - "ShopkeeperSpecialInv" used in RandomItems.fillItems
  - Attempted
- After putting in logging messages:
	[Info   :  CCU_Core] DetermineButtons_Prefix: Method Call
	[Info   :  CCU_Core] HasTraitFromList: Method Call
	[Info   :  CCU_Core] HasTraitFromList: Method Call
	[Info   :  CCU_Core] GetOnlyTraitFromList: Method Call
	[Info   :  CCU_Core] DetermineButtons_Prefix: Vendor
	[Info   :  CCU_Core] HasTraitFromList: Method Call
	[Info   :  CCU_Core] DetermineButtons_Prefix: Hire
	[Info   :  CCU_Core] DetermineButtons_Prefix: Hire Initial
	[Info   :  CCU_Core] PressedButton_Prefix: Method Call
  - So none of them are firing. 

---

#	Implementation
##		Appearance

Search for "Custom" (Agent name
AgentHitbox
	.chooseFacialHairType
CharacterSelect
	.ChangeHairColor
RandomSkinHair
√	.fillSkinHair

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

####			Tamper
Pending results of Safecrack attempt
### Tamper
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