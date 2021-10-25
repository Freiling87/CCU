Behaviors
	Active
	Combat
	Passive
Interactions
	Hire
	Vendor
Relationships
	Faction

#		Header Symbol Notes

Listed in order of Parent tier summary symbol priority

C, T = Code this, Test this
H = Hold, usually pending resolution of a separate or grouped issue
√ = Fully implemented feature or group of features

#	C	00 Initial Load Error Logs

---

#	C	00 Top-Priority Bugs
###		C	Vendor
- Goblin Pyro:
	[Info   :  CCU_Core] DetermineButtons_Prefix: Method Call
	[Debug  :CCU_TraitManager] TRAIT LIST: Custom
	[Debug  :CCU_TraitManager]      Combat_UseDrugs
	[Debug  :CCU_TraitManager]      Behavior_GrabDrugs
	[Debug  :CCU_TraitManager]      Hire_CauseRuckus
	[Debug  :CCU_TraitManager]      Hire_DisarmTrap
	[Debug  :CCU_TraitManager]      Behavior_ExplodeOnDeath
	[Debug  :CCU_TraitManager]      Vendor_Demolitionist
	[Debug  :CCU_TraitManager]      Addict
	[Debug  :CCU_TraitManager]      Beard
	[Debug  :CCU_TraitManager]      Mustache
	[Debug  :CCU_TraitManager]      MustcheCircus
	[Debug  :CCU_TraitManager]      MustacheRedneck
	[Debug  :CCU_TraitManager]      NoFacialHair
	[Debug  :CCU_TraitManager]      ExplosionsDontDamageCauser
	[Debug  :CCU_TraitManager]      ExplosionsBreakEverything
	[Debug  :CCU_TraitManager]      HardToSeeFromDistance
	[Debug  :CCU_TraitManager]      FireproofSkin
	[Debug  :CCU_TraitManager]      FastWhenHealthLow
	[Debug  :CCU_TraitManager]      DestructionXP
	[Debug  :CCU_TraitManager]      RegenerateHealthWhenLow
	[Debug  :CCU_TraitManager]      HardToShoot
	[Debug  :CCU_TraitManager]      TechExpert
	[Debug  :CCU_TraitManager]      Diminutive
	[Debug  :CCU_P_AgentInteractions] =Custom=
	[Debug  :CCU_P_AgentInteractions] hasSpecialInvDatabase: True
	[Info   :  CCU_Core] DetermineButtons_Prefix: Vendor
*	[Debug  :CCU_P_AgentInteractions]       Count: 5
	[Info   :  CCU_Core] DetermineButtons_Prefix: Hire
	[Info   :  CCU_Core] DetermineButtons_Prefix: Hire Initial
    - The Starred list is the number of items in their specialinvdatabase. So it is loading correctly, it's just not put in the shop correctly.
###		T	New error
On Interact with Orc Street Samurai:
	[Info   :  CCU_Core] DetermineButtons_Prefix: Method Call
	[Debug  :CCU_TraitManager] TRAIT LIST: Custom
	[Debug  :CCU_TraitManager]      CantUseGuns
	[Debug  :CCU_TraitManager]      Loud
	[Debug  :CCU_TraitManager]      NoTechSkill
	[Debug  :CCU_TraitManager]      IncreasedCritChance
	[Debug  :CCU_TraitManager]      Combat_Fearless
	[Debug  :CCU_TraitManager]      Combat_UseDrugs
	[Debug  :CCU_TraitManager]      Hire_Bodyguard
	[Debug  :CCU_TraitManager]      Behavior_Guilty
	[Debug  :CCU_P_AgentInteractions] =Custom=
	[Error  : Unity Log] InvalidDataException: Agent Custom (1132) was expected to have one trait from list, but has none.
	Stack trace:
	CCU.Traits.TraitManager.GetOnlyTraitFromList (Agent agent, System.Collections.Generic.List`1[T] traitList) (at <564910051a164833af95d20b2481ae64>:0)
	CCU.Patches.Behaviors.P_AgentInteractions.DetermineButtons_Prefix (Agent agent, Agent interactingAgent, System.Collections.Generic.List`1[T] buttons1, System.Collections.Generic.List`1[T] buttonsExtra1, System.Collections.Generic.List`1[T] buttonPrices1, AgentInteractions __instance, System.Collections.Generic.List`1[System.String]& ___buttons, System.Collections.Generic.List`1[System.String]& ___buttonsExtra, System.Collections.Generic.List`1[System.Int32]& ___buttonPrices, Agent& ___mostRecentAgent, Agent& ___mostRecentInteractingAgent) (at <564910051a164833af95d20b2481ae64>:0)
	AgentInteractions.DetermineButtons (Agent agent, Agent interactingAgent, System.Collections.Generic.List`1[T] buttons1, System.Collections.Generic.List`1[T] buttonsExtra1, System.Collections.Generic.List`1[T] buttonPrices1) (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	Agent.DetermineButtons () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	PlayfieldObject.Interact (Agent agent) (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	Agent.Interact (Agent otherAgent) (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	InteractionHelper.UpdateInteractionHelper () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	Updater.UpdateInterface () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	Updater.Update () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
###		T	Vendor error
- Here:
	[Info   :  CCU_Core] AddRandItem_Prefix: Method Call
	[Debug  :CCU_P_InvDatabase]     rName: Vendor_JunkDealer
	[Debug  :CCU_P_InvDatabase]     Catch
	[Debug  :CCU_P_InvDatabase]     Text: Empty
	[Debug  :CCU_P_InvDatabase]     Catch
	[Debug  :CCU_P_InvDatabase]     Text: Empty
	[Debug  :CCU_P_InvDatabase]     Catch
	[Debug  :CCU_P_InvDatabase]     Text: Empty
	- It looks like that's not the right rName. When I logged existing rNames they were the const string values prefixed by [CCU]. 
      - string rName = TraitManager.GetOnlyTraitFromList(__instance.agent, TraitManager.VendorTypeTraits).Name;
        - is accessing the wrong type of Name.

---

#	CT	Character Editor
###		C	Access from Chunk/Campaign Editor selector dropdown
New
##		T	CCU Trait Hiding
###			T	Character Creation, Player Edition (CharacterCreation)
Possible future bug: If you create a character in DE, and edit/resave them in PE (hidden traits won't be visible), will it remove or keep their hidden traits?
###			√	Character Select (CharacterSelect)
Complete
###			√	Character Sheet (CharacterSheet)
Complete
##		C	Trait Alphabetization
Sort them right after the save command is issued rather than after load, so preview works correctly.
OR, have a "trait" button that automatically un-selects itself when clicked but sorts trait list.

#	CT	Chunk Editor
##		C	Agent Goals
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
###			C	Knocked Out
New
###			C	Lookout
New
When alerted, will run towards nearest ally and alert them
###			c	Panic
New
###			C	Robot Clean
New
In vanilla
###			C	Robot Follow
New
Killer Robot behavior in vanilla
###			C	Wander between Agents
New
###			C	Wander between Agents (Aligned)
New
###			C	Wander between Agents (Unaligned)
New
###			C	Yell
New
Will yell their Talk text? Unless you can get a second text box in there somewhere
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
- Haven't really tested yet but: 
	System.DefaultBinder.BindToMethod (System.Reflection.BindingFlags bindingAttr, System.Reflection.MethodBase[] match, System.Object[]& args, System.Reflection.ParameterModifier[] modifiers, System.Globalization.CultureInfo cultureInfo, System.String[] names, System.Object& state) (at <44afb4564e9347cf99a1865351ea8f4a>:0)
	System.RuntimeType.CreateInstanceImpl (System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, System.Object[] args, System.Globalization.CultureInfo culture, System.Object[] activationAttributes, System.Threading.StackCrawlMark& stackMark) (at <44afb4564e9347cf99a1865351ea8f4a>:0)
	System.Activator.CreateInstance (System.Type type, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, System.Object[] args, System.Globalization.CultureInfo culture, System.Object[] activationAttributes) (at <44afb4564e9347cf99a1865351ea8f4a>:0)
	System.Activator.CreateInstance (System.Type type, System.Object[] args) (at <44afb4564e9347cf99a1865351ea8f4a>:0)
	CCU.CoreTools.GetMethodWithoutOverrides[T] (System.Reflection.MethodInfo method, System.Object callFrom) (at <f8fe071de5a046f18a2599c39a4d8e95>:0)
	CCU.Patches.Objects.P_SlimeBarrel.Start (SlimeBarrel __instance) (at <f8fe071de5a046f18a2599c39a4d8e95>:0)
	SlimeBarrel.Start () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	CCU.Patches.Objects.P_SlimeBarrel.Start (SlimeBarrel __instance) (at <f8fe071de5a046f18a2599c39a4d8e95>:0)
	SlimeBarrel.Start () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	CCU.Patches.Objects.P_SlimeBarrel.Start (SlimeBarrel __instance) (at <f8fe071de5a046f18a2599c39a4d8e95>:0)
	SlimeBarrel.Start () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	CCU.Patches.Objects.P_SlimeBarrel.Start (SlimeBarrel __instance) (at <f8fe071de5a046f18a2599c39a4d8e95>:0)
	SlimeBarrel.Start () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	- Attempted calling private a different way
###		C	SwitchFloor
Not sure about this one, may be too deeply hardcoded
###		C	Tube
.Start
###		C	WaterPump
.Start
##		CT	Hotkeys
###			T	00 SetOrientation/SetDirection not updating in fields
- Trying calling SetDirection() after these both
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
###			T	Deactivate hotkeys if typing into inputfield
Attempted
###			T	Ctrl + A - Deselect All
- Identical logs, confirm that it's the backend not working, not an interface update issue
	[Info   :  CCU_Core] ToggleSelectAll: Method Call
	[Debug  :CCU_LevelEditorUtilities]      Tile list count: 1024
	[Debug  :CCU_LevelEditorUtilities]      Selected count: 4
	[Debug  :CCU_LevelEditorUtilities]              Index 0: Test Dummy B
	[Debug  :CCU_LevelEditorUtilities]              Index 1: Test Dummy C
	[Debug  :CCU_LevelEditorUtilities]              Index 2: Test Dummy D
	[Debug  :CCU_LevelEditorUtilities]              Index 3: Test Dummy A
 	- Attempted redesigning this 
###			T	F2 - QuickNew
Attempted
###			T	F9 - Quickload
- Attempt: Calling ClickedLoadChunkButton or whatever first
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
###			√	Arrow Keys - Orient (Draw)
Complete
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
###			√	Ctrl + E, Q - Rotate Object (Draw)
Complete
###			√	Ctrl + E, Q - Rotate Object (Select)
Complete
###			√	Ctrl + NumKeys - Select Layer & Open Draw Type Selector
Complete
###			√	Ctrl + N - New
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
I.e., Show stuff that won't show up, unless you can disable that disabling behavior
##		H	Orient Object Sprites in Edit Mode
I.e., show rotated sprite for any objects
##		H	Rotate Chunks in Play Mode
This sounds hard

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

#	T	Mutators
##		√	General
- Show up in LevelEditor UI, may be a manually constructed list
##		T	Homesickness Disabled
- First attempt
Disable hire dismissal for this level
ExitPoint.EmployeesExit prefix
##		T	Homesickness Mandatory
- First attempt
Always dismiss hires at end of level, even if Homesickness Killer is active
ExitPoint.EmployeesExit prefix

#	C	Player Utilities
##		C	Mutators to omit Vanilla content when custom is available
- If designer has added customs to be Roamers, or Hide in Bushes, etc., have some mutators to exclude Vanilla types from those spawning behaviors
##		C	Save Chunk Pack configuration between loads
- I.e., only deactivate chunk packs when the player says so!
##		C	Show Chunk info on Mouseover in Map mode
- When in gameplay map view, mouseover a chunk to see its name and author in the unused space in the margins.
  - Gives credit to author
  - Helps identify gamebreaking chunks, allowing you to not use the chunk pack or notify their author.

#	T	Promo Campaign - Shadowrun-but-not-really
##		T	Player Character
"The Fixer," an old man who's not quite as tough as he used to be. But he has a lot of connections in the criminal world and knows how to put together a team. So this will direct the player to use a hiring-based playstyle.
##		T	Home base 
In between each mission, a small hive city neighborhood with all the things you would need.
###			T	The Bar
Where the mercenaries hang out. All Talk interactions should tell you a bit about them.
Ensure that Buying a Round will affect all hires.
Feel free to use multiple copies of the same merc, since their appearances will vary! B)
Give them a "Permanent hire" trait
####			T	The Fence
He'll buy your shit from you. This should be a *rare* opportunity to sell stuff.
####			T	The Bartender
Maybe something clever to say. Maybe not.
####			√	The Mercs
###			T	Doc's
##		T	Mission Format
##		T	Apollo Tower

###			T	Infrastructure Sub-Basement
###			T	Executive Suites
###			T	Rooftop Escape

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
Use this on leader w/ Wander Level
Use "Follow" behavior on agents placed behind them
No need for "Roaming Gang" Trait itself
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
###			C	Permanent Hire
New
~8x normal hire price
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
##		C	Interaction
###			C	Rename
Interaction is a group of traits, encompassing:
- Hire
- Vendor
- These
  - Currently under the Interaction group. Call them Requests or something?
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
###			C	Hire for Level
New
###			C	Hire Permanently
- Multiple skill useTHE 
- Stay until death
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
###			C	Train Traits - Defense
New
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
- There's an Exploding Bodies mutator, doofus! Copy that.
###			√	Guilty
Complete
###			C	Hackable - Tamper With Aim
New
###			C	Hackable - Go Haywire
New
###			C	Invincible
News
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
###			C	Never Hostile
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

#	√	Campaign Editor
No features planned yet

#	√	Chunk Pack Editor
No features planned yet