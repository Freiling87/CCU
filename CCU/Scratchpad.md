# Header Symbol Notes

Listed in order of Parent tier summary symbol priority

C, T = Code this, Test this
H = Hold, usually pending resolution of a separate or grouped issue
√ = Fully implemented feature or group of features

#	00 Initial Load Error Logs

---
#	Campaign Editor √
Nothing yet

#	Character Editor
##	C	UI - Trait Hiding
###		T	Character Creation, Player Edition (CharacterCreation)
Possible future bug: If you create a character in DE, and edit/resave them in PE (hidden traits won't be visible), will it remove or keep their hidden traits?
###		T	Character Select (CharacterSelect)
Aspects to hide from CharSelect screen:
####		T	Behavior
Attempted
####		T	Facial Hair
Attempted
####		T	Hire
Attempted
####		T	Hire Specials
Attempted
####		T	Trait Triggers
Attempted
####		T	Vendor
Attempted
###		T	Character Sheet (CharacterSheet)
####		T	Behavior
Attempted
####		T	Facial Hair
Attempted
####		T	Hire
Attempted
####		T	Hire Specials
Attempted
####		T	Trait Triggers
Attempted
####		T	Vendor
Attempted

#	Chunk Editor
##	C	00 Error Logs Unidentified
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
##	C	District Object De-Limitation
E.g. Slime Barrels, Fire Grates
##	C	Edges Blocked Warning on Save
If it's not already a thing
##	CT	Hotkeys
###		C	Alt + Security Cam - Highlight Visible Tiles
New
###		√	Alt + NumKeys, NumPad - Menu Trails
ALT trail for overhead menus
This one is likely beyond my ability right now since we'd need to underline text in menus or make popup shortcut letter boxes. 
###		T	Arrows - Not sure
No effect
###		T	Ctrl + A - Deselect All
Doesn't work
###		√	Ctrl + A - Select All
Works
###		T	Ctrl + NumKeys, NumPad - Select Layer & Open Draw Type Selector
Not sure rn
###		C	Ctrl + O - Open
Ctrl + O load shows all menus but doesn't load anything
###		√	Ctrl + S - Save
Works
###		C	Ctrl + Y - Redo
New
###		C	Ctrol + Z - Undo
New
###		C	F5 - Quicksave
Save works but shows menus
###		C	F9 - Quickload
load works but shows chunk selection menu
###		C	F12 - Play Chunk
New
###		√	NumKeys - Select Layer
Works
###		C	Q, E - Rotate Draw Object
Default to North as from-direction if None
Not sure if any attempts
###		C	Q, E - Match current direction to set to None
New
###		C	Shift + Ctrl - Filter View to Layer, and display Owner ID on all tiles
New
###		C	Shift + Ctrl - Display Patrol IDs (group, not sequence) on all Points
New
###		C	Shift + Alt - Display Patrol Sequence IDs on all Points in field Patrol ID 
New
###		T	Shift + Tab - Reverse-Tab through fields
Not sure
###		T	Tab - Tab through fields
Not sure
##	C	Item Groups
For placement in containers/inventories
##	C	Multiple In Chunk field for NPC Group selection
New
##	C	Red-Tint Out-Of-District Objects
I.e., Show stuff that won't show up

#	Chunk Pack Editor √
Nothing yet

#	Level Editor
##	C	Hotkeys
Put the specifics in ### here

#	Player Utilities
##	C	Show Chunk info on Mouseover in Map mode
- When in gameplay map view, mouseover a chunk to see its name and author in the unused space in the margins.
  - Gives credit to author
  - Helps identify gamebreaking chunks, allowing you to not use the chunk pack or notify their author.

#	Traits
##	C	Appearance
###		C	Facial Hair
Search for "Custom" (Agent name
AgentHitbox
	.chooseFacialHairType
CharacterSelect
	.ChangeHairColor
RandomSkinHair
√	.fillSkinHair
###		C	Hair Color
Go ahead and try. Knowing the code they all work differently anyway :)
###		C	Hairstyle
Go ahead and try. Knowing the code they all work differently anyway :)
###		C	Skin Color
Go ahead and try. Knowing the code they all work differently anyway :)
##	T	Behavior
###		T	Drink Blood
###		T	Eat Corpse
###		T	Grab Drugs
###		T	Grab Money
###		T	Pickpocket
##	CT	Hire
###		C	00 General AI Update error
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
- Check out brain.AddSubgoal, as jobs are passed to it
- After killing a bugged Agent:
	CCU.Patches.Behaviors.P_GoalDoJob.Terminate_Prefix (GoalDoJob __instance) (at <2052eae91fad498b965def95486033b6>:0)
	GoalDoJob.Terminate () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	System.DefaultBinder.BindToMethod (System.Reflection.BindingFlags bindingAttr, System.Reflection.MethodBase[] match, System.Object[]& args, System.Reflection.ParameterModifier[] modifiers, System.Globalization.CultureInfo cultureInfo, System.String[] names, System.Object& state) (at <44afb4564e9347cf99a1865351ea8f4a>:0)
	System.RuntimeType.CreateInstanceImpl (System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, System.Object[] args, System.Globalization.CultureInfo culture, System.Object[] activationAttributes, System.Threading.StackCrawlMark& stackMark) (at <44afb4564e9347cf99a1865351ea8f4a>:0)
	System.Activator.CreateInstance (System.Type type, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, System.Object[] args, System.Globalization.CultureInfo culture, System.Object[] activationAttributes) (at <44afb4564e9347cf99a1865351ea8f4a>:0)
	System.Activator.CreateInstance (System.Type type, System.Object[] args) (at <44afb4564e9347cf99a1865351ea8f4a>:0)
	CCU.CoreTools.GetMethodWithoutOverrides[T] (System.Reflection.MethodInfo method, System.Object callFrom) (at <2052eae91fad498b965def95486033b6>:0)
###		H	Bodyguard
Pending General AI Update Error resolution
###		H	Break In
Pending General AI Update Error resolution
###		H	Cause a Ruckus
Pending General AI Update Error resolution
###		T	Cost - Banana
Test
###		T	Cost - Less
Test
###		T	Cost - More
Test
###		H	Hack
Pending General AI Update Error resolution
###		H	Safecrack

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
##	C	Interaction
###		C	Extortable
New
###		C	Fence
New
###		C	Moochable
New
###		C	Vendor Buyer
New
##	CT	Loadout
###		C	Item Groups
uwumacaronitime's idea: Item groups similar to NPC groups

I can see this going two ways: 
- As a trait for NPCs to generate with
- As a designated item in the chunk creator for use in NPC & Object inventories. 

I am leaning towards implementing both of these. But whichever is chosen, make it very clear to avoid confusion.
###		T	ChunkKey
- Attempted - InvDatabase.FillAgent()
###		T	ChunkMayorBadge
- Attempted - InvDatabase.FillAgent()
###		T	ChunkSafeCombo
- Attempted - InvDatabase.FillAgent()
###		C	Guns_Common
##	C	NPC Groups
###		C	Roamer LevelFeature
###		C	Slum NPCs
##	C	Relationships
###		√	Faction Traits
Complete
###		C	Vanilla Faction Traits
For allying people and factions to Crepe/Blahd, etc.
##	CT	Trait Triggers
###		C	Common Folk
New
###		C	Cool Cannibal
New
###		H	Cop	Access
Pending Vendor issues resolution
###		T	Honorable Thief
Worked for pickpocket
##	CT	Vendor
###		C	00 No Button
- "Buy" button no longer showing up
	[Info   :  CCU_Core] DetermineButtons_Prefix: Method Call
	[Info   :  CCU_Core] HasTraitFromList: Method Call
	[Info   :  CCU_Core] HasTraitFromList: Method Call
	[Info   :  CCU_Core] GetOnlyTraitFromList: Method Call
	[Info   :  CCU_Core] HasTraitFromList: Method Call
	[Info   :  CCU_Core] DetermineButtons_Prefix: Hire
	[Info   :  CCU_Core] DetermineButtons_Prefix: Hire Initial
###		√	General Notes
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
###		T	00 Empty Inventory
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

- May need to put in behavior that Musician can visit Vendors and gifts a matching item type. 
- Traits that will need compatibility:
  - Shop Drops
  - That one discount one
###		H	Thief Vendor
- Special Inv filling: 
  - InvDatabase.FillSpecialInv
  - InvDatabase.AddRandItem
    - @1246: base.CompareTag("SpecialInvDatabase")
      - I am hoping this is taken care of automatically. Testing will show.
  - Keep an eye out for agent name checks, one of these will need to be patched
  - InvDatabase.rnd.RandomSelect(rName, "Items")