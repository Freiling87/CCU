#	P	Header Symbol Notes
Listed in order of Parent tier summary symbol priority:
	P = Pinned to top (for notes)
	C, T = Code this, Test this
	N = Next release
	H = Hold, usually pending resolution of a separate or grouped issue
	√ = Fully implemented feature or group of features

#	P	Top-Priority Bugs

---

#	P	General Reminders & To-Dos

- I merged Passive and Interaction, but I'm thinking they need to be split back up. 
  - There was also a specific reason to make Extortable & Moochable into Passive instead of Interaction or Trait Trigger, but I can't remember it. Write it here if you do.
---

#	C	All Editors
##		CT	Hotkeys
###			C	F11 - Return to Editor
- Tried adding a check for GC.loadCompleteReally
  - DW
- LoadLevel.NextLevel() might be a good shortcut
  - Patch this into PlayerControl.Update
  - Issue with this one is that it will be limited to chunk-testing rather than campaign testing
- levelEditor.ReturnToLevelEditor();
  - Patched this into PlayerControl.Update
   	- In Editor: Reloads editor
    - In Testing: Works
    - Called by NextLevel, so it's probably better to do the latter since it will handle weird cases
- See also MenuGUI Class in case above doesn't work
###			C	Enter or Space - Yes on YesNo menu
- Changed to GetKeyDown to rate-limit
Need to test this with *all* menus, because you're not guaranteed that they're all made the same 
- Load Chunk
  - Esc works, not Enter
###			C	Escape - No on YesNo menu
- Changed to GetKeyDown to rate-limit
- Works, but seems to either trigger per-frame, or go directly to the meta-menu when you press escape from the editor. Find a way to halt the input once it's done it once.
- Need to test this with *all* menus, because you're not guaranteed that they're all made the same 
- See also levelEditor.CloseNotification(), CloseHelpScreen()...
- On Load Chunk, it actually closed the Chunk name selector window before the yesno window. How do I direct it?
###			C	Letters - Scroll Menu to section starting with letter
- ScrollingMenu.OpenScrollingMenu: 
  - __instance.scrollBarDetails.value = 1f;
    - Possibly where initial scroll location is set?
- Detect whether ScrollingMenu is active/open
  - ButtonHelper.scrollingMenu != null
  - Detect input
    - Get all ScrollingButtons from ScrollingMenu, count to letter
      - GC.buttonHelpersList
      - GC.menuButtonHelpersList
      - ScrollingMenu.numButtonsOnScreen
      - Set ScrollBar to that y-axis
        - ScrollBar.Set
###			√	Alt + 1/2/3/4 - Switch to Editor
Complete
###			√	Alt + Ctrl + 1/2/3/4 - Quickswitch to Editor
Complete

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
###		N	Access from Chunk/Campaign Editor selector dropdown
Next release
##		C	Trait Hiding
###			C	Character Creator, Player Edition (CharacterCreation)
Possible future bug: If you create a character in DE, and edit/resave them in PE (hidden traits won't be visible), will it remove or keep their hidden traits?
Breaking mods up means the player edition should be 
###			√	Character Select (CharacterSelect)
Complete
###			√	Character Sheet (CharacterSheet)
Complete
##		C	Trait Alphabetization
- Attempted
  - DW

#	C	Chunk Editor
##		C	Hotkeys
###			C	00 SetOrientation/SetDirection not updating in fields for Draw Mode only
- Trying calling SetDirection() after these both
  - Remove this now that other works
- Attempting test on LEFT ARROW ONLY, without ability to toggle off yet.
  - This worked. Distribute the changes.
###			C	F9 - Quickload
- Maybe try this through calling menus and sending a ButtonHelper as Pressed.
  - Proposed:
	// Get current file name
	// Open SCrollingmenu
	// TRY send pressedbutton
	// If no match, some kind of UI feedback to indicate it
	// Maybe those popup dialogues are easier than you think?
    - Attempted
      - Omitting PressedYesNoButton() after PressedScrollingMenuButton(), because I am not sure yet if it even asks for confirmation, or if so, if it always asks or only does so when there are no changes to the current chunk's saved version.
      - Logging messages showed, but no effect whatsoever:
			[Debug  :CCU_P_LevelEditor]     Attempting Quickload:
			Chunk Name: 00TestChunk
		
// MenuGUI.OpenYesNoScreen, the non-yesno equivalent
- Probably also need a general static method you can call to send a button you determine by name, since you'll likely need to reuse it somewhere.
- Confirmed ButtonHelper is not a Singleton class, however LevelEditor declares yesNoButtonHelper
    - ButtonHelper.PressedScrollingMenuButton
- This is still occurring after unpredictable intervals:
	[Debug  :CCU_P_LevelEditor]     Attempting Quickload:
        Chunk Name: 00TestChunk
	[Error  : Unity Log] NullReferenceException: Object reference not set to an instance of an object
	Stack trace:
	LevelEditor.LoadChunkFromFile (System.String chunkName, ButtonHelper myButtonHelper) (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	CCU.Patches.Interface.P_LevelEditor.FixedUpdate_Prefix (LevelEditor __instance, UnityEngine.GameObject ___helpScreen, UnityEngine.GameObject ___initialSelection, UnityEngine.GameObject ___workshopSubmission, UnityEngine.GameObject ___longDescription, ButtonHelper ___yesNoButtonHelper, UnityEngine.UI.InputField ___chunkNameField) (at <ca47c8100fc149198a1a46d28d85f694>:0)
	LevelEditor.FixedUpdate () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
- Log it.
###			H	A-Z - skip to letter on scrolling menu
On ice, pending collaboration with someone who uderstands UI methods well
###			H	A + Ctrl + Shift - Select all in Layer only
What I mean here is that the normal Ctrl + A should select in all layers. Is that possible?
###			H	C + Ctrl - Copy, All Layers
Hold
###			H	C + Ctrl + Shift - Copy, One Layer
Hold
###			H	V + Ctrl - Paste All Layers
- Pending completion of Copy
###			H	V + Ctrl + Shift - Paste One Layer
Hold
###			H	Y + Ctrl - Redo
New
###			H	Z + Ctrl - Undo
New
###			H	Alt - Security Cam: Highlight Visible Tiles
Pending anyone indicating they actually could use this feature
###			H	Alt + Ctrl - Show Spawn Chances
- Pending Pilot NumberBox display
- Filter to layer too?
###			H	Alt + Shift - Filter + Display Patrol Sequence IDs on all Points in field Patrol ID 
New
###			H	Ctrl + Shift - Filter & Display Owner IDs
New
###			H	Ctrl + Shift - Filter & Display Patrol IDs (group, not sequence) on all Points
New
###			H	F12 - Exit Playing Chunk
On ice, pending user request
###			H	Mouse3 - Drag Viewport
New, and hell no
###			H	NumKeys, NumPad + Alt - Menu Trails
ALT trail for overhead menus
This one is likely beyond my ability right now since we'd need to underline text in menus or make popup shortcut letter boxes. 
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
###			√	A + Ctrl - De/Select All
Complete
###			√	E, Q - Zoom In/Out
Complete
###			√	E, Q + Ctrl - Increment Patrol Point
Complete
###			√	E, Q + Ctrl - Rotate Direction Field Value
Complete
###			√	E, Q + Shift - Max Zoom In/Out
Complete
###			√	N + Ctrl - New
Complete
###			√	O + Ctrl - Open
Complete
###			√	S + Ctrl - Save
Complete
###			√	Arrow Keys - Set Direction Field Value, or Toggle if matching
Complete
###			√	F2 - QuickNew
Complete
###			√	F5 - Quicksave
Complete
###			√	F9 - Abort function if no matching filename to field
This is pretty much automatic already, since it will just fail. But reactivate if you ever want to put up a warning message or some kind of UI indicator.
###			√	F11 - Play Chunk
Complete
###			√	NumKeys - Select Layer
Complete
###			√	NumKeys + Ctrl - Select Layer & Open Draw Type Selector
Complete
###			√	Tab + Shift- Reverse-Tab through fields
Complete
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
##		H	Red-Tint Out-Of-District Objects
I.e., Show stuff that won't show up, unless you can disable that disabling behavior
##		H	Orient Object Sprites in Edit Mode
I.e., show rotated sprite for any objects
##		H	Rotate Chunks in Play Mode
This sounds hard

#	C	Documentation
##		C	Keyboard Layout Diagrams 
###			√	Chunk Editor
http://www.keyboard-layout-editor.com/#/gists/2f3df5c48d93b5cbb24ebddca302ffd6
###			C	Level Editor
New
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

#	C	Level Editor
##		C	Hotkeys
###			C	Arrow Keys - Set Chunk Direction, Draw or Select Mode
Need separate version
###			H	Arrow Keys - Clear Chunk Direction
Pending resolution of Original
###			H	Ctrl + A - De-select All Chunks if All Selected
Pending resolution of Select All, but seems to work here
###			C	Ctrl + A - Select All Chunks
- Only selects one:
  - Select All:
		[Info   :  CCU_Core] ToggleSelectAll: Method Call
		[Debug  :CCU_LevelEditorUtilities]      Tile list count: 100
		[Debug  :CCU_LevelEditorUtilities]      Selected count: 0
  - Deselect all (actually worked):
		[Info   :  CCU_Core] ToggleSelectAll: Method Call
		[Debug  :CCU_LevelEditorUtilities]      Tile list count: 100
		[Debug  :CCU_LevelEditorUtilities]      Selected count: 1
		[Debug  :CCU_LevelEditorUtilities]              Index 0: TestChunkExit
		[Error  : Unity Log] ArgumentOutOfRangeException: Index was out of range. Must be non-negative and less than the size of the collection.
		Parameter name: index
		Stack trace:
		System.ThrowHelper.ThrowArgumentOutOfRangeException (System.ExceptionArgument argument, System.ExceptionResource resource) (at <44afb4564e9347cf99a1865351ea8f4a>:0)
		System.ThrowHelper.ThrowArgumentOutOfRangeException () (at <44afb4564e9347cf99a1865351ea8f4a>:0)
		LevelEditor.UpdateInterface (System.Boolean setDefaults) (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
		LevelEditor.ClearSelections (System.Boolean setDefaults) (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
		CCU.Content.LevelEditorUtilities.ToggleSelectAll (LevelEditor levelEditor, System.Boolean limitToLayer) (at <60860e74db334df19fb9b913ab023427>:0)
		CCU.Patches.Interface.P_LevelEditor.FixedUpdate_Prefix (LevelEditor __instance, UnityEngine.GameObject ___helpScreen, UnityEngine.GameObject ___initialSelection, UnityEngine.GameObject ___workshopSubmission, UnityEngine.GameObject ___longDescription, ButtonHelper ___yesNoButtonHelper, UnityEngine.UI.InputField ___chunkNameField) (at <60860e74db334df19fb9b913ab023427>:0)
		LevelEditor.FixedUpdate () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
###			C	Up/Down - Flip Chunk over X axis
New
###			C	Ctrl + E, Q - Toggle Rotation
New
###			C	Left/Right - Flip over Y axis
New
###			H	Ctrl + Y - Redo
Pending the skills to do this
###			H	Ctrl + Z - Redo
Pending the skills to do this
###			C	F9 - Quickload
Opens Load selector menu
###			√	Ctrl + S - Save
Complete
###			√	Ctrl + O - Open
Complete
###			√	F5 - Quicksave
Complete

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
##		H	Followers
Pending resolution of hire AI Update error
###			T	Homesickness Disabled
- First attempt
Disable hire dismissal for this level
ExitPoint.EmployeesExit prefix
###			T	Homesickness Mandatory
- First attempt
Always dismiss hires at end of level, even if Homesickness Killer is active
ExitPoint.EmployeesExit prefix
##		C	Gameplay
###			C	No Violence
New
For town levels
##		C	Interface
In PlayerControl.Update there's a hidden keystroke for pressedInterfaceOff
##		C	Ambient Light
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
###			C	No Object Lights
- Works!
- Need to exclude working machines with lights from this. Maybe jazz up their halos if possible.
- Fire sources are fine since the particle creates the light anyway.
###			√	New Moon
Complete
Confirmed Werewolf returns it to how you want it.
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

#	C	Player Edition
- WHenever you have enough in the campaign to make it playable, test it in Player Edition and see if the experience is the same.

#	CT	Traits
##		PC	Blank Traits Menu
No ScrollingButtons appear on Trait Choice menu
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
This actually sorta worked, sorta. 
When run in the chunk editor, an Appearance-Traited character did have a randomized appearance. But all features were randomized and none were limited to the traits selected.
- Search for "Custom" (Agent name)
	AgentHitbox
		.chooseFacialHairType
	CharacterSelect
		.ChangeHairColor
	RandomSkinHair
	√	.fillSkinHair
###			C	Facial Hair
- Just saw this:
	[Info   :  CCU_Core] RollFacialHair: Method Call
	[Error  : Unity Log] KeyNotFoundException: The given key was not present in the dictionary.
	Stack trace:
	System.Collections.Generic.Dictionary`2[TKey,TValue].get_Item (TKey key) (at <44afb4564e9347cf99a1865351ea8f4a>:0)
	HealthBar.SetupFace () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	HealthBar+<SetupFaceAfterTick>d__44.MoveNext () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	UnityEngine.SetupCoroutine.InvokeMoveNext (System.Collections.IEnumerator enumerator, System.IntPtr returnValueAddress) (at <451019b49f1347529b43a32c5de769af>:0)
###			C	Hair Color
Go ahead and try. Knowing the code they all work differently anyway :)
###			C	Hairstyle
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
##		T	Hire

####			√	Try Disabling Completely
- Disabled BrainUpdate.MyUpdate patch method. This holds the Active LOS behaviors (which work fine), but I don't think I tested Hiring before it was added.
  - Error still occurred, so I think the silver lining is that I can leave those intact unless the attempt below directs us directly to it.
####			C	Make Vanilla Replacment + Logging patch of BrainUpdate.MyUpdate
New
###			H	Bodyguard
Pending General AI Update Error resolution
###			H	Break In
Pending General AI Update Error resolution
###			H	Cause a Ruckus
Pending General AI Update Error resolution
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
###			C	Disarm Trap
New
###			C	Drink Blood
New
###			H	Hack
Pending General AI Update Error resolution
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
##		N	Agent Group
###			C	Slum NPCs (Pilot)
New
###			H	Affect Campaign
Pending pilot
###			H	Affect Vanilla 
Pending pilot
###			C	Roamer Level Feature
New##		C	Passive
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
##		T	Trait Triggers
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
##		C	Utility
###			C	Sort active Traits by Name
- ScrollingMenu.PushedButton @ 0006
  - Pretty much has exactly what you need.
- DW
###			C	Sort active Traits by Value
- ScrollingMenu.PushedButton @ 0006
  - Pretty much has exactly what you need.
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

#	N	Player Utilities
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

#	√	Campaign Editor
No features planned yet

#	√	Chunk Pack Editor
No features planned yet