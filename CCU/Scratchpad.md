#		Header Symbol Notes

Listed in order of Parent tier summary symbol priority

C, T = Code this, Test this
H = Hold, usually pending resolution of a separate or grouped issue
√ = Fully implemented feature or group of features

#	C	00 Initial Load Error Logs

##		C	Crash on hire
- Probably just need to write the Interaction chain

---

##	C	00 Top-Priority Bugs

###		T	Apply Logging level fix
Config file, add Debug to log levels

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
##		T	00 Error Logs Unidentified
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
###			H	Alt + Security Cam - Highlight Visible Tiles
Pending anyone indicating they actually could use this feature
###			H	Alt + NumKeys, NumPad - Menu Trails
ALT trail for overhead menus
This one is likely beyond my ability right now since we'd need to underline text in menus or make popup shortcut letter boxes. 
###			C	Arrow Keys - Orient (Draw)
- Not reflecting in Direction field
  - Pending logging config fix
###			√	Arrow Keys - Orient (Select)
Complete
###			√	Arrow Keys - Match current direction to set to None
Complete
###			C	Ctrl + A - Deselect All
- Try logging the count of selected objects, then go from there
  - Pending logging config fix
###			√	Ctrl + A - Select All
Complete
###			H	Ctrl + Alt - Show Spawn Chances
- Pending Pilot NumberBox display
- Filter to layer too?
###			H	Ctrl + C - Copy, All Layers
Hold
###			H	Ctrl + Shift + C - Copy, One Layer
Hold
###			√	Ctrl + E, Q - Increment Patrol Point (Draw)
Complete
###			√	Ctrl + E, Q - Increment Patrol Point (Select)
Complete
###			C	Ctrl + E, Q - Rotate Object (Draw)
- Not reflecting in Direction field
  - Pending logging config fix
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
###			H	Ctrl + V - Paste All Layers
- Pending completion of Copy
###			H	Ctrl + Shift + V - Paste One Layer
Hold
###			H	Ctrl + Y - Redo
New
###			H	Ctrl + Z - Undo
New
###			√	E, Q - Zoom In/Out
Complete
###			√	F5 - Quicksave
Complete
###			T	F9 - Quickload
  - Pending logging config fix
- Works perfectly for a while, but then... it doesn't. Not sure what changes. But I have noticed that loading a chunk through normal means re-sets it, so I think that pathway must be filling out the field that's getting nulled here. It's possible this is a garbage collection thing, too, in the way that I have no idea how that concept works so I couldn't say.
- Attempting pulling name from __instance.chunkNameField (___chunkNameField).text rather than __instance.chunkName
- This is still occurring after unpredictable intervals:
	[Error  : Unity Log] NullReferenceException: Object reference not set to an instance of an object
	Stack trace:
	LevelEditor.LoadChunkFromFile (System.String chunkName, ButtonHelper myButtonHelper) (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	CCU.Patches.Interface.P_LevelEditor.FixedUpdate_Prefix (LevelEditor __instance, UnityEngine.GameObject ___helpScreen, UnityEngine.GameObject ___initialSelection, UnityEngine.GameObject ___workshopSubmission, UnityEngine.GameObject ___longDescription, ButtonHelper ___yesNoButtonHelper, UnityEngine.UI.InputField ___chunkNameField) (at <2cf93e496da547c5942062d518cecca4>:0)
	LevelEditor.FixedUpdate () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)

###			C	F9 - Abort function if no matching filename to field
New
###			√	F12 - Play Chunk
Complete
###			H	F12 - Exit Playing Chunk
On ice, pending user request
###			H	Letter Keys - skip to letter on scrolling menu
On ice, pending collaboration with someone who uderstands UI methods well
###			H	Mouse3 - Drag Viewport
New, and hell no
###			√	NumKeys - Select Layer
Complete
###			√	Shift + E, Q - Max Zoom In/Out
Complete
###			H	Shift + Ctrl - Filter + Display Owner IDs
New
###			H	Shift + Ctrl - Filter + Display Patrol IDs (group, not sequence) on all Points
New
###			H	Shift + Alt - Filter + Display Patrol Sequence IDs on all Points in field Patrol ID 
New
###			√	Shift + Tab - Reverse-Tab through fields
Complete
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
##		C	Item Groups
For placement in containers/inventories
##		C	Multiple In Chunk field for NPC Group selection
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
##		C	Agent Group
###		H	Affect Campaign
Pending pilot
###		H	Affect Vanilla 
Pending pilot
###		C	Slum NPCs (Pilot)
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
##		C	Behavior Active
###			C	Clean Trash
New
###			√	Drink Blood
Complete
###			√	Eat Corpse
Complete
###			C	Enforce Laws
New
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
###			C	Vision Beams (Cop Bot)
New
##		CT	Behavior Passive
###			T	Explode On Death
Attempted
###			√	Guilty
Complete
###			C	Hackable - Tamper With Aim
New
###			C	Hackable - Go Haywire
New
##		T	Bodyguarded
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
##		CT	Trait Triggers
###			C	Common Folk
New
###			C	Cool Cannibal
New
###			H	Cop	Access
Pending Vendor issues resolution
###			H	Honorable Thief
Pending Vendor issues resolution
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