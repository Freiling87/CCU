#		Header Symbol Notes

Listed in order of Parent tier summary symbol priority

C, T = Code this, Test this
H = Hold, usually pending resolution of a separate or grouped issue
√ = Fully implemented feature or group of features

#	C	00 Initial Load Error Logs

##		C	On Quickstart, before CharSelect:

###				1
	[Info   :  CCU_Core] AddRandItem_Prefix: Method Call
	[Info   :  CCU_Core] GetOnlyTraitFromList: Method Call
	[Error  : Unity Log] NullReferenceException: Object reference not set to an instance of an object
	Stack trace:
	CCU.Patches.Inventory.P_InvDatabase.AddRandItem_Prefix (System.String itemNum, InvDatabase __instance, InvItem& __result) (at <ad877a3c182446eabfa8d33336aac4bc>:0)
	InvDatabase.AddRandItem (System.String itemNum) (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	InvDatabase.FillSpecialInv () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	InvDatabase.Start () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)

---

#	√	Campaign Editor
No features planned yet

#	 T	Character Editor
##		T	UI - Trait Hiding
###			T	Character Creation, Player Edition (CharacterCreation)
Possible future bug: If you create a character in DE, and edit/resave them in PE (hidden traits won't be visible), will it remove or keep their hidden traits?
###			√	Character Select (CharacterSelect)
Complete
###			√	Character Sheet (CharacterSheet)
Complete

#	CT	Chunk Editor
##		C	00 Error Logs Unidentified
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
##		C	District Object De-Limitation
E.g. Slime Barrels, Fire Grates
##		C	Edges Blocked Warning on Save
If it's not already a thing
##		CT	Hotkeys
###			C	Alt + Security Cam - Highlight Visible Tiles
New
###			C	Alt + NumKeys, NumPad - Menu Trails
ALT trail for overhead menus
This one is likely beyond my ability right now since we'd need to underline text in menus or make popup shortcut letter boxes. 
###			C	Arrow Keys - Rotate
No effect
###			C	Arrow Keys - Match current direction to set Orientation to None
No effect
###			T	Ctrl + A - Deselect All
Doesn't work
###			√	Ctrl + A - Select All
Works
###			C	Ctrl + Alt - Show Spawn Chances
Filter to layer too?
###			C	Ctrl + NumKeys, NumPad - Select Layer & Open Draw Type Selector
Works, but needs to switch to Draw mode as well
###			C	Ctrl + O - Open
Ctrl + O load shows all menus but doesn't load anything.
F9 successfully loads, though. Not sure why.
###			√	Ctrl + S - Save
Works
###			C	Ctrl + Y - Redo
New
###			C	Ctrl + Z - Undo
New
###			C	F5 - Quicksave
Save works but shows popup yes/no
###			C	F9 - Quickload
Shows chunk selection menu
###			C	F12 - Play Chunk
New
###			√	NumKeys - Select Layer
Works
###			C	Q, E - Rotate Draw Object
Default to North as from-direction if None
No effect
###			C	Q, E - Rotate Select Object
New
###			C	Shift + Ctrl - Filter View to Layer, and display Owner ID on all tiles
New
###			C	Shift + Ctrl - Display Patrol IDs (group, not sequence) on all Points
New
###			C	Shift + Alt - Display Patrol Sequence IDs on all Points in field Patrol ID 
New
###			T	Shift + Tab - Reverse-Tab through fields
No attempts
###			T	Tab - Tab through fields
[Error  : Unity Log] NullReferenceException: Object reference not set to an instance of an object
Stack trace:
CCU.LevelEditorUtilities.Tab (LevelEditor levelEditor, System.Boolean reverse) (at <ae32907916534692aa75127ab0d31add>:0)
CCU.Patches.Interface.P_LevelEditor.FixedUpdate_Prefix (LevelEditor __instance, UnityEngine.GameObject ___helpScreen, UnityEngine.GameObject ___initialSelection, UnityEngine.GameObject ___workshopSubmission, UnityEngine.GameObject ___longDescription, UnityEngine.UI.InputField ___directionObject, UnityEngine.UI.InputField ___pointNumPatrolPoint) (at <ae32907916534692aa75127ab0d31add>:0)
LevelEditor.FixedUpdate () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
##		C	Item Groups
For placement in containers/inventories
##		C	Multiple In Chunk field for NPC Group selection
New
##		C	Red-Tint Out-Of-District Objects
I.e., Show stuff that won't show up
##		H	Edit Mode Object Orientation
I.e., show rotated sprite for any objects
##		H	Play Mode Chunk Rotation
This sounds hard

#	√	Chunk Pack Editor √
No features planned yet

#	C	Level Editor
##		C	Hotkeys
###			C	Arrow Keys - Set Chunk Direction, Draw or Select Mode
New
###			C	Arrow Keys - Clear Chunk Direction
New
###			C	Ctrl + A - De-select All Chunks if All Selected
New
###			C	Ctrl + A - Select All Chunks
New
###			C	Ctrl + H - Flip Chunk Horizontally
New
###			C	Ctrl + S - Save
New
###			C	Ctrl + O - Open
New
###			C	Ctrl + V - Flip Chunk Vertically
New
###			C	Ctrl + Y - Redo
New
###			C	Ctrl + Z - Redo
New
###			C	F5 - Quicksave
New
###			C	F9 - Quickload
New
###			C	Q, E - Rotate Chunk, Draw Mode
New
###			C	Q, E - Rotate Chunk, Select Mode
New
###			C	Tab - Tab through fields
New
###			C	Shift + Tab - Reverse-Tab through fields
New

#	C	Player Utilities
##		C	Mutators to omit Vanilla content when custom is available
- If designer has added customs to be Roamers, or Hide in Bushes, etc., have some mutators to exclude Vanilla types from those spawning behaviors
##		C	Save Chunk Pack configuration between loads
- I.e., only deactivate chunk packs when the player says so!
##		C	Show Chunk info on Mouseover in Map mode
- When in gameplay map view, mouseover a chunk to see its name and author in the unused space in the margins.
  - Gives credit to author
  - Helps identify gamebreaking chunks, allowing you to not use the chunk pack or notify their author.

#	CT	Traits
###		C	Agent Group
####		H	Affect Campaign
Pending pilot
####		H	Affect Vanilla 
Pending pilot
####		C	Slum NPCs (Pilot)
New
##		C	Appearance
This actually sorta worked, sorta. 
When run in the chunk editor, an Appearance-Traited character did have a randomized appearance. But all features were randomized and none were limited to the traits selected.
###			C	Facial Hair
Search for "Custom" (Agent name)

AgentHitbox
	.chooseFacialHairType
CharacterSelect
	.ChangeHairColor
RandomSkinHair
√	.fillSkinHair
####			C	Bugged Appearance
Shows up with a little brown notch on the East side of the face regardless of orientation. It looks like the mustache but it's hard to tell.
###			C	Hair Color
Go ahead and try. Knowing the code they all work differently anyway :)
###			C	Hairstyle
Go ahead and try. Knowing the code they all work differently anyway :)
###			C	Skin Color
Go ahead and try. Knowing the code they all work differently anyway :)
##		C	Behavior
###			√	Guilty
Complete
###			C	There are more here, just not added yet
##		√	Behavior LOS
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
##		C	Combat
###			√	Coward
Complete
###			√	Fearless
Complete
###			C	Pacifist
New
###			√	Use Drugs in Combat
Complete
##		C	Generation
###			C	Bodyguarded - Cops
New
###			C	Bodyguarded - Goons
New
###			C	Bodyguarded - Supercops
New
###			C	Bodyguarded - Supergoons
New
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
##		CT	Hire
###			C	00 General AI Update error
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
###			H	Bodyguard
Pending General AI Update Error resolution
###			H	Break In
Pending General AI Update Error resolution
###			H	Cause a Ruckus
Pending General AI Update Error resolution
###			T	Cost - Banana
Test
###			T	Cost - Less
Test
###			T	Cost - More
Test
###			H	Hack
Pending General AI Update Error resolution
###			H	Safecrack

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

####				Tamper
Pending results of Safecrack attempt
### Tamper
##		C	Interaction
###			C	Extortable
No effect
###			C	Fence
New
###			C	Moochable
No effect
###			C	Vendor Buyer
New
##		CT	Loadout
###			C	Item Groups
uwumacaronitime's idea: Item groups similar to NPC groups

I can see this going two ways: 
- As a trait for NPCs to generate with
- As a designated item in the chunk creator for use in NPC & Object inventories. 

I am leaning towards implementing both of these. But whichever is chosen, make it very clear to avoid confusion.
###			T	ChunkKey
- Attempted - InvDatabase.FillAgent()
###			T	ChunkMayorBadge
- Attempted - InvDatabase.FillAgent()
###			T	ChunkSafeCombo
- Attempted - InvDatabase.FillAgent()
###			C	Guns_Common
##		T	Map Marker
###			√	General Notes
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
###			T	Pilot
- Prefixed PlayfieldObject.SpawnNewMapMarker
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
##		C	NPC Groups
###			C	Roamer LevelFeature
New
###			C	Slum NPCs
New
##		C	Relationships
###			√	Faction Traits
Complete
###			C	Vanilla Faction Traits
For allying people and factions to Crepe/Blahd, etc.
##		CT	Trait Triggers
###			C	Common Folk
New
###			C	Cool Cannibal
New
###			H	Cop	Access
Pending Vendor issues resolution
###			T	Honorable Thief
Worked for pickpocket
##		CT	Vendor
###			CT	00 No Button
- "Buy" button no longer showing up
	[Info   :  CCU_Core] DetermineButtons_Prefix: Method Call
	[Info   :  CCU_Core] HasTraitFromList: Method Call
	[Info   :  CCU_Core] HasTraitFromList: Method Call
	[Info   :  CCU_Core] GetOnlyTraitFromList: Method Call
	[Info   :  CCU_Core] HasTraitFromList: Method Call
	[Info   :  CCU_Core] DetermineButtons_Prefix: Hire
	[Info   :  CCU_Core] DetermineButtons_Prefix: Hire Initial
###			√	General Notes
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
###			T	00 Empty Inventory
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
###			√	Cost Banana
Complete
###			H	Thief Vendor
- Special Inv filling: 
  - InvDatabase.FillSpecialInv
  - InvDatabase.AddRandItem
    - @1246: base.CompareTag("SpecialInvDatabase")
      - I am hoping this is taken care of automatically. Testing will show.
  - Keep an eye out for agent name checks, one of these will need to be patched
  - InvDatabase.rnd.RandomSelect(rName, "Items")