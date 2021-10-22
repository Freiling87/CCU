#		Header Symbol Notes

Listed in order of Parent tier summary symbol priority

C, T = Code this, Test this
H = Hold, usually pending resolution of a separate or grouped issue
√ = Fully implemented feature or group of features

#	C	00 Initial Load Error Logs

---

#	C	00 Top-Priority Bugs
###		T	Vendor error
- Here:
	[Info   :  CCU_Core] GetOnlyTraitFromList: Method Call
	[Debug  :CCU_TraitManager]      null return
	[Debug  :CCU_P_Agent] Vendor found:
	[Info   : Unity Log] RANDOM NUMBER 7: 824
	[Info   : Unity Log] 50% - RANDOM NUMBER 8: 285
	[Info   : Unity Log] Hang Test 1 - True
	[Info   :  CCU_Core] AddRandItem_Prefix: Method Call
	[Info   :  CCU_Core] GetOnlyTraitFromList: Method Call
	[Debug  :CCU_TraitManager]      null return
	[Error  : Unity Log] NullReferenceException: Object reference not set to an instance of an object
	Stack trace:
	CCU.Patches.Inventory.P_InvDatabase.AddRandItem_Prefix (System.String itemNum, InvDatabase __instance, InvItem& __result) (at <927250a200ec4d9f8efc8135c58ffa18>:0)
	InvDatabase.AddRandItem (System.String itemNum) (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	InvDatabase.FillSpecialInv () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	InvDatabase.Start () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
- I think this is coming from one of two places:
  - within AddRandItem
    - Attempted
  - Before AddRandItem, not properly checking for a null 
###		T	HasTraitFromLlist
- This method was modified, see if the bug is resolved
	[Info   :  CCU_Core] HasTraitFromList: Method Call
	[Error  : Unity Log] InvalidOperationException: Collection was modified; enumeration operation may not execute.
	Stack trace:
	System.ThrowHelper.ThrowInvalidOperationException (System.ExceptionResource resource) (at <44afb4564e9347cf99a1865351ea8f4a>:0)
	System.Collections.Generic.List`1+Enumerator[T].MoveNextRare () (at <44afb4564e9347cf99a1865351ea8f4a>:0)
	System.Collections.Generic.List`1+Enumerator[T].MoveNext () (at <44afb4564e9347cf99a1865351ea8f4a>:0)
	CCU.Patches.Spawn.P_LoadLevel.SetupMore3_3_Postfix (LoadLevel __instance) (at <2d5650d29b9141339f263611b9ba9c1e>:0)
	LoadLevel.SetupMore3_3 () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	LoadLevel.SetupMore3_2 () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	LoadLevel+<SetupMore3Wait>d__146.MoveNext () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	UnityEngine.SetupCoroutine.InvokeMoveNext (System.Collections.IEnumerator enumerator, System.IntPtr returnValueAddress) (at <451019b49f1347529b43a32c5de769af>:0)
###		T	idk
- This method was modified, see if the bug is resolved
	[Info   : Unity Log] 62% - SETUPMORE2
	[Info   : Unity Log] Set BigQuest: Hobo
	[Info   : Unity Log] Player Info: Playerr (Agent) - Hobo - 0 - True -  - 1150
	[Info   : Unity Log] 75% - SETUPMORE3
	[Info   : Unity Log] 1 False - True - False
	[Info   : Unity Log] SETUPMORE3 2 False - True - False
	[Info   : Unity Log] 87% - SETUPMORE3_2
	[Info   :  CCU_Core] GetOnlyTraitFromList: Method Call
	[Debug  :CCU_TraitManager]      null return
	[Error  : Unity Log] InvalidOperationException: Collection was modified; enumeration operation may not execute.
	Stack trace:
	System.ThrowHelper.ThrowInvalidOperationException (System.ExceptionResource resource) (at <44afb4564e9347cf99a1865351ea8f4a>:0)
	System.Collections.Generic.List`1+Enumerator[T].MoveNextRare () (at <44afb4564e9347cf99a1865351ea8f4a>:0)
	System.Collections.Generic.List`1+Enumerator[T].MoveNext () (at <44afb4564e9347cf99a1865351ea8f4a>:0)
	CCU.Patches.Spawn.P_LoadLevel.SetupMore3_3_Postfix (LoadLevel __instance) (at <927250a200ec4d9f8efc8135c58ffa18>:0)
	LoadLevel.SetupMore3_3 () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	LoadLevel.SetupMore3_2 () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	LoadLevel+<SetupMore3Wait>d__146.MoveNext () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	UnityEngine.SetupCoroutine.InvokeMoveNext (System.Collections.IEnumerator enumerator, System.IntPtr returnValueAddress) (at <451019b49f1347529b43a32c5de769af>:0)

---

#	√	Campaign Editor
No features planned yet

#	T	Character Editor
##		T	UI - Trait Hiding
###			T	Character Creation, Player Edition (CharacterCreation)
Possible future bug: If you create a character in DE, and edit/resave them in PE (hidden traits won't be visible), will it remove or keep their hidden traits?
###			√	Character Select (CharacterSelect)
Complete
###			√	Character Sheet (CharacterSheet)
Complete
###		C	Access from Chunk/Campaign Editor selector dropdown

#	CT	Chunk Editor
##		C	Additional Agent Behaviors
###			C	Die
New
###			C	Die + Burn
New
###			C	Explode
New
###			C	Sleep
New
Already exists?
###			C	Wander between Agents in Chunk
New
##		CT	District Object De-Limitation
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
Attempted
###		C	SwitchFloor
Not sure about this one, may be too deeply hardcoded
###		C	Tube
.Start
###		C	WaterPump
.Start
##		C	Edges Blocked Warning on Save
If it's not already a thing
##		CT	Hotkeys
###			C	00 Use Traverse to access subfields of private
Might not be needed, if current attempt is working fine
###			C	Arrow Keys - Orient (Draw)
- Draw mode ends up with blank for NewDir
	[Info   :  CCU_Core] OrientObject: Method Call
	[Debug  :CCU_LevelEditorUtilities]      input: UpArrow
	[Debug  :CCU_LevelEditorUtilities]      inputField: DirectionFloor
	[Debug  :CCU_LevelEditorUtilities]      its value:
	[Debug  :CCU_LevelEditorUtilities]      curDir:
	[Debug  :CCU_LevelEditorUtilities]      newDir: N
	[Debug  :CCU_LevelEditorUtilities]      directionObjectField: DirectionObject
	[Debug  :CCU_LevelEditorUtilities]      its value: N
	[Info   :  CCU_Core] OrientObject: Method Call
	[Debug  :CCU_LevelEditorUtilities]      input: UpArrow
	[Debug  :CCU_LevelEditorUtilities]      inputField: DirectionFloor
	[Debug  :CCU_LevelEditorUtilities]      its value: N
	[Debug  :CCU_LevelEditorUtilities]      curDir: N
	[Debug  :CCU_LevelEditorUtilities]      newDir: N
	[Debug  :CCU_LevelEditorUtilities]      directionObjectField: DirectionObject
	[Debug  :CCU_LevelEditorUtilities]      its value:
###			H	Ctrl + E, Q - Rotate Object (Draw)
- Same issue as Arrow Keys, resolve as one
###			C	Ctrl + A - Deselect All
- Identical logs, confirm that it's the backend not working, not an interface update issue
	[Info   :  CCU_Core] ToggleSelectAll: Method Call
	[Debug  :CCU_LevelEditorUtilities]      Tile list count: 1024
	[Debug  :CCU_LevelEditorUtilities]      Selected count: 4
	[Debug  :CCU_LevelEditorUtilities]              Index 0: Test Dummy B
	[Debug  :CCU_LevelEditorUtilities]              Index 1: Test Dummy C
	[Debug  :CCU_LevelEditorUtilities]              Index 2: Test Dummy D
	[Debug  :CCU_LevelEditorUtilities]              Index 3: Test Dummy A
###			C	F9 - Quickload
- Works perfectly for a while, but then... it doesn't. 
- Attempting pulling name from __instance.chunkNameField (___chunkNameField).text rather than __instance.chunkName
- This is still occurring after unpredictable intervals:
	[Debug  :CCU_P_LevelEditor]     Attempting Quickload:
        Chunk Name: 00TestChunk
	[Error  : Unity Log] NullReferenceException: Object reference not set to an instance of an object
	Stack trace:
	LevelEditor.LoadChunkFromFile (System.String chunkName, ButtonHelper myButtonHelper) (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	CCU.Patches.Interface.P_LevelEditor.FixedUpdate_Prefix (LevelEditor __instance, UnityEngine.GameObject ___helpScreen, UnityEngine.GameObject ___initialSelection, UnityEngine.GameObject ___workshopSubmission, UnityEngine.GameObject ___longDescription, ButtonHelper ___yesNoButtonHelper, UnityEngine.UI.InputField ___chunkNameField) (at <ca47c8100fc149198a1a46d28d85f694>:0)
	LevelEditor.FixedUpdate () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
###			H	Alt + Security Cam - Highlight Visible Tiles
Pending anyone indicating they actually could use this feature
###			H	Alt + NumKeys, NumPad - Menu Trails
ALT trail for overhead menus
This one is likely beyond my ability right now since we'd need to underline text in menus or make popup shortcut letter boxes. 
###			H	Ctrl + Alt - Show Spawn Chances
- Pending Pilot NumberBox display
- Filter to layer too?
###			H	Ctrl + C - Copy, All Layers
Hold
###			H	Ctrl + Shift + C - Copy, One Layer
Hold
###			H	Ctrl + V - Paste All Layers
- Pending completion of Copy
###			H	Ctrl + Shift + V - Paste One Layer
Hold
###			H	Ctrl + Y - Redo
New
###			H	Ctrl + Z - Undo
New
###			H	F12 - Exit Playing Chunk
On ice, pending user request
###			H	Letter Keys - skip to letter on scrolling menu
On ice, pending collaboration with someone who uderstands UI methods well
###			H	Mouse3 - Drag Viewport
New, and hell no
###			H	Shift + Ctrl - Filter + Display Owner IDs
New
###			H	Shift + Ctrl - Filter + Display Patrol IDs (group, not sequence) on all Points
New
###			H	Shift + Alt - Filter + Display Patrol Sequence IDs on all Points in field Patrol ID 
New
###			H	Tab - Tab through fields
- Putting this on ice - not sure how useful of a feature it is yet
- Pending Input Rate Limit
- Technically works. Only moved between the three Spawn% fields in the horizontal group
	[Info   :  CCU_Core] Tab: Method Call
	[Debug  :CCU_LevelEditorUtilities] Active Field: SpawnChance3Agent
	[Debug  :CCU_LevelEditorUtilities] ActiveInputField: SpawnChance3Agent (UnityEngine.UI.InputField)
	[Info   :  CCU_Core] Tab: Method Call
	[Debug  :CCU_LevelEditorUtilities] Active Field: SpawnChance2Agent
	[Debug  :CCU_LevelEditorUtilities] ActiveInputField: SpawnChance2Agent (UnityEngine.UI.InputField)
	[Info   :  CCU_Core] Tab: Method Call
	[Debug  :CCU_LevelEditorUtilities] Active Field: SpawnChanceAgent
	[Debug  :CCU_LevelEditorUtilities] ActiveInputField: SpawnChanceAgent (UnityEngine.UI.InputField)
	[Info   :  CCU_Core] Tab: Method Call
	[Debug  :CCU_LevelEditorUtilities] Active Field: SpawnChance2Agent
	[Debug  :CCU_LevelEditorUtilities] ActiveInputField: SpawnChance2Agent (UnityEngine.UI.InputField)
	[Info   :  CCU_Core] Tab: Method Call
	[Debug  :CCU_LevelEditorUtilities] Active Field: SpawnChance3Agent
	[Debug  :CCU_LevelEditorUtilities] ActiveInputField: SpawnChance3Agent (UnityEngine.UI.InputField)
- Review the output from LevelEditor.Start_Postfix, as it traverses a field list by name.
###			√	Arrow Keys - Orient (Select)
Complete
###			√	Arrow Keys - Match current direction to set to None
Complete
###			√	Ctrl + A - Select All
Complete
###			√	Ctrl + E, Q - Increment Patrol Point (Draw)
Complete
###			√	Ctrl + E, Q - Increment Patrol Point (Select)
Complete
###			√	Ctrl + E, Q - Rotate Object (Select)
Complete
###			√	Ctrl + NumKeys - Select Layer & Open Draw Type Selector
Complete
###			√	Ctrl + O - Open
Ctrl + O load shows all menus but doesn't load anything.
F9 successfully loads, though. Not sure why.
- Attempted, copied what F9 does here
###			√	Ctrl + S - Save
Complete
###			√	E, Q - Zoom In/Out
Complete
###			√	F5 - Quicksave
Complete
###			√	F9 - Abort function if no matching filename to field
This is pretty much automatic already, since it will just fail. But reactivate if you ever want to put up a warning message or some kind of UI indicator.
###			√	F12 - Play Chunk
Complete
###			√	NumKeys - Select Layer
Complete
###			√	Shift + E, Q - Max Zoom In/Out
Complete
###			√	Shift + Tab - Reverse-Tab through fields
Complete
##		C	Item Groups
For placement in containers/inventories
##		C	Multiple In Chunk field for NPC Group selection
New
##		C	Randomized Lights
New
##		C	Red-Tint Out-Of-District Objects
I.e., Show stuff that won't show up
##		H	Orient Object Sprites in Edit Mode
I.e., show rotated sprite for any objects
##		H	Rotate Chunks in Play Mode
This sounds hard

#	√	Chunk Pack Editor √
No features planned yet

#	C	General
##		C	Color the colons
In Trait descriptions, colons are colored

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

#	T	Promo Campaign

##		T	Theme
- Donny & Smamuel sitting next to each other in front of a glass wall, commenting on what's going on beyond it (in the screen)

##		T	Level 1 - Home Base
- Two dudes on computers, talking about their SOR campaigns
  - DUMBASS DONNY plays without mods, we'll see his campaign first, also he is dumb
  - SMART SMAMUEL plays with mods, and is cool and good and handsome
- Smamuel: Hey so a bunch of your shit doesn't work man, you need mods. I put notes in your chunks if you wanna check it out.
- Donny: Duhhhh, I'm dumb. What's a mod?
##		T	Level 2 - Dumbass Donny's level
- Chunk 1
  - A custom Vampire not sucking blood
  - A custom Cannibal not eating a corpse
  - A custom pickpocket not picking pockets
  - A custom Firefighter not fighting fires
##		T	Level 3 - Smart Smamuel's level
- 

#	CT	Traits
##		H	Agent Group
###		H	Slum NPCs (Pilot)
New
###		H	Affect Campaign
Pending pilot
###		H	Affect Vanilla 
Pending pilot
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
##		C	Behavior Active
###			C	Clean Trash
New
###			√	Drink Blood
Complete
###			√	Eat Corpse
Complete
###			C	Fight Fires
New
###			√	Grab Drugs
Complete
###			C	Grab Everything
New
###			C	Grab Food
New
###			√	Grab Money
Complete
###			C	Guard Door
New
###			C	Hobo Beg (Custom)
Maybe just implement the whole Hey, You! overhaul here
###			C	Hog Turntables
New
Allow paid Musician behavior
###			√	Pickpocket
Complete
###			C	Seek & Destroy (Killer Robot)
New
###			C	Shakedown Player
New
###			C	Tattle (Upper Cruster)
New
##		C	Bodyguarded
- LoadLevel.SetupMore3_3 where "Musician"
  - Attempted here
  - But there are a few other hits in searching this string in the code:
    - LoadLevel.SpawnStartingFollowers
    - ObjectMult.StartWithFollowersBodyguardA
      - Ignore this one, it's for the Player Bodyguard trait
###			C	Bodyguarded - Cop
New
###			C	Bodyguarded - Blahd
New
###			C	Bodyguarded - Crepe
New
###			C	Bodyguarded - Goon
New
###			C	Bodyguarded - Mafia
New
###			C	Bodyguarded - Soldier
New
###			C	Bodyguarded - Supercop
New
###			C	Bodyguarded - Supergoon
New
##		C	Combat
###			C	Cause Lockdown
New
###			√	Coward
Complete
###			√	Fearless
Complete
###			√	Use Drugs in Combat
Complete
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
###			C	Disarm Trap
New
###			H	Hack
Pending General AI Update Error resolution
###			C	Pickpocket
New
###			C	Poison
New
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
###			C	Set Explosive
On door or Safe: Plants door detonator
Elsewhere: Remote bomb
Gives you detonator when planted
###			C	Tamper
New 
##		C	Interaction
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
###			C	Deportation Center Clerk
New
###			C	Extortable
No effect
###			C	Fence
New
NOT a Vendor trait
###			C	Heal
New
###			C	Hotel Clerk
New
###			C	Identify
New
###			C	Influence Election
New
###			C	Mayor Clerk
New
###			C	Moochable
No effect
###			C	Offer Motivation
New
###			C	Permanent Hire
- Multiple skill use
- Stay until death
###			C	Quest Giver
New
###			C	Refill Guns
New
###			C	Repair Armor
New
###			C	Repair Weapons
New
###			C	Sell Slaves
New
###			C	Summon Professional
New
Pay a fee for him to teleport a Hacker, Thief, Doctor or Soldier to you. You still have to pay them to hire them.
###			C	Use Bloodbag
New
###			C	Vendor Buyer
New
###			C	Vendor Buyer Only
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
##		C	Map Marker
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
###			C	Pilot
- Prefixed PlayfieldObject.SpawnNewMapMarker
  - Didn't work
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
##		C	Passive
###			C	Explode On Death
- requires Coroutine from private IEnumerator ExplodeBody(). Commented out relevant section.
###			√	Guilty
Complete
###			C	Hackable - Tamper With Aim
New
###			C	Hackable - Go Haywire
New
###			C	Vision Beams (Cop Bot)
New
##		C	Relationships
###			C	Aggressive (Cannibal)
New
###			T	Annoyed At Suspicious
Attempted
###			√	Faction Traits
Complete
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
###			C	Secretly Hostile
A la Bounty disaster
###			C	Vanilla Faction Traits
For allying people and factions to Crepe/Blahd, etc.
##		C	Trait Triggers
###			T	Common Folk
Attempted
Only SetRelationshipInitial - search for other occurrences of this trait in the code.
###			T	Cool Cannibal
Attempted
Only SetRelationshipInitial - search for other occurrences of this trait in the code.
###			H	Cop	Access
Pending Vendor issues resolution
###			T	Family Friend
Attempted
Only SetRelationshipInitial - search for other occurrences of this trait in the code.
###			H	Honorable Thief
Pending Vendor issues resolution
###			T	Scumbag
Attempted
Only SetRelationshipInitial - search for other occurrences of this trait in the code.
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
###			T	00 FillItem bug
- Added logging to RandomSelection methods to identify issue
###			T	00 Empty Inventory
- Still empty, need more logging. After PressedButton("Buy")
- I think they do have a SpecialInvDatabase, but the lists aren't working. I think it's pulling names via agentname instead of your intended way.
  - "ShopkeeperSpecialInv" used in RandomItems.fillItems
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
- 
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