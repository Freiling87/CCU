Relationships
	Faction

#	P	Header Symbol Notes
Listed in order of Parent tier summary symbol priority:
	P = Pinned to top (for notes)
	C, T = Code this, Test this
	N = Next release
	H = Hold, usually pending resolution of a separate or grouped issue
	√ = Fully implemented feature or group of features

#	P	00 Top-Priority Bugs

---

#	C	All Editors
General utilities that work across editors
##		C	Hotkeys
###			C	Return to Editor
When you die testing, you get an option "Q - Editor" 
###			C	Alt + 1/2/3/4 - Switch to Editor
New
###			C	Alt + Ctrl + 1/2/3/4 - Quickswitch to Editor
New
###			C	Enter or Space - Yes on YesNo menu
New
###			C	Escape - No on YesNo menu
New

#	T	Character Editor
###		N	Access from Chunk/Campaign Editor selector dropdown
Next release
##		T	Trait Hiding
###			T	Character Creator, Player Edition (CharacterCreation)
Possible future bug: If you create a character in DE, and edit/resave them in PE (hidden traits won't be visible), will it remove or keep their hidden traits?
###			√	Character Select (CharacterSelect)
Complete
###			√	Character Sheet (CharacterSheet)
Complete
##		N	Trait Alphabetization
Sort them right after the save command is issued rather than after load, so preview works correctly.
OR, have a "trait" button that automatically un-selects itself when clicked but sorts trait list.
- SCrollingMenu.PushedButton Postfix 

#	CT	Chunk Editor
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
##		CT	Hotkeys
###			C	00 SetOrientation/SetDirection not updating in fields for Draw Mode only
- Trying calling SetDirection() after these both
  - DW
###			C	F9 - Quickload
- This is still occurring after unpredictable intervals:
	[Debug  :CCU_P_LevelEditor]     Attempting Quickload:
        Chunk Name: 00TestChunk
	[Error  : Unity Log] NullReferenceException: Object reference not set to an instance of an object
	Stack trace:
	LevelEditor.LoadChunkFromFile (System.String chunkName, ButtonHelper myButtonHelper) (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	CCU.Patches.Interface.P_LevelEditor.FixedUpdate_Prefix (LevelEditor __instance, UnityEngine.GameObject ___helpScreen, UnityEngine.GameObject ___initialSelection, UnityEngine.GameObject ___workshopSubmission, UnityEngine.GameObject ___longDescription, ButtonHelper ___yesNoButtonHelper, UnityEngine.UI.InputField ___chunkNameField) (at <ca47c8100fc149198a1a46d28d85f694>:0)
	LevelEditor.FixedUpdate () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
- Log it.
###			H	Ctrl + Shift + A - Select all in Layer only
What I mean here is that the normal Ctrl + A should select in all layers. Is that possible?
###			√	Ctrl + A - Deselect All
Complete
###			√	F2 - QuickNew
Complete
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
##		N	Multiple In Chunk field for NPC Group selection
New
##		C	Randomized Lights
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
##		√	Logo 16x16
##		C	Logo 64_64
##		C	Steam Thumbnail caption

#	N	Item Groups
Next release

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

#	C	Mutators
!!! All of these are on hold pending adding to mutator list for level editor
##		√	General Mutator List
- Show up in LevelEditor UI, may be a manually constructed list
- LoadLevel.loadStuff2 @ 171
##		C	Audio
###			C	Ambienter Ambience
New
Gate behind presence of objects/agents, e.g. disable Casino if all Slots are destroyed
##		H	Branching
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
##		T	Lighting
###			T	Darker Darkness
- Doesn't show on trait list, looks like RL3 doesn't do it for the editor?
  - Report that to abbysssal
- Attempted
- Next to try:
  - SetNewLightingAmbient from WerewolfTransform
- Alternative: 
  - "Filter" mutators, allowing designer to choose color filters. Want orange for your fallout mod?
###			T	No Agent Lights
Test
###			T	No Item/Wreckage Lights
Test
###			T	No Object Lights
Test
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

#	C	Player Edition
- WHenever you have enough in the campaign to make it playable, test it in Player Edition and see if the experience is the same.

#	H	Player Utilities
##		H	Mouse3 Bind to command followers
- Target
  - Ground - All Stand Guard
  - Agent - All Attack
  - Self - All Follow
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

#	T	Promo Campaign - Shadowrun-but-not-really
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

#	T	Traits
PressedButton is now Vanilla to start from scratch, just FYI
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
Didn't work

AgentHitbox
	.chooseFacialHairType
CharacterSelect
	.ChangeHairColor
RandomSkinHair
√	.fillSkinHair
###			√	Bugged Appearance
- Shows up with a little brown notch on the East side of the face regardless of orientation. It looks like the mustache but it's hard to tell.
  - Has not recurred.
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
##		C	Behavior Passive
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
###			C	Improve Relations w/ Faction 1-4
New
Costs $1000, improves your relations with that faction
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
###			C	Vendor Buyer
New
###			C	Vendor Buyer Only
New
##		C	Bodyguarded
  - But there are a few other hits that came up in a string search (possibly "Musician"):
    - LoadLevel.SpawnStartingFollowers
    - ObjectMult.StartWithFollowersBodyguardA
      - Ignore this one, it's for the Player Bodyguard trait
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
##		C	Combat
###			C	Cause Lockdown
New
###			√	Coward
Complete
###			√	Fearless
Complete
###			√	Use Drugs in Combat
Complete
##		CT	Hire
###			T	00 General AI Update error
- Trying:
  - Disabled BrainUpdate.MyUpdate patch method. This holds the Active LOS behaviors (which work fine), but I don't think I tested Hiring before it was added.
- Hired NPC. Once hired, they couldn't move and framerate skipped.
- Also occurs with Vanilla hires.
	[Debug  :CCU_P_AgentInteractions]       buttonText: AssistMe
	[Info   :  CCU_Core] MyUpdate_Prefix: Method Call
	[Info   :  CCU_Core] CombatCheck_Prefix: Method Call
	[Info   :  CCU_Core] UpdateTargetPosition_Prefix: Method Call
	[Info   :  CCU_Core] MyUpdate_Prefix: Method Call
	[Info   :  CCU_Core] CombatCheck_Prefix: Method Call
	[Info   :  CCU_Core] UpdateTargetPosition_Prefix: Method Call
	[Info   :  CCU_Core] MyUpdate_Prefix: Method Call
	[Info   :  CCU_Core] CombatCheck_Prefix: Method Call
	[Info   :  CCU_Core] UpdateTargetPosition_Prefix: Method Call
	[Info   :  CCU_Core] MyUpdate_Prefix: Method Call
	[Info   :  CCU_Core] CombatCheck_Prefix: Method Call
	[Info   :  CCU_Core] UpdateTargetPosition_Prefix: Method Call
	[Info   :  CCU_Core] MyUpdate_Prefix: Method Call
	[Info   :  CCU_Core] CombatCheck_Prefix: Method Call
	[Info   :  CCU_Core] UpdateTargetPosition_Prefix: Method Call
	[Info   :  CCU_Core] MyUpdate_Prefix: Method Call
	[Info   :  CCU_Core] CombatCheck_Prefix: Method Call
	[Info   :  CCU_Core] UpdateTargetPosition_Prefix: Method Call
	[Info   :  CCU_Core] MyUpdate_Prefix: Method Call
	[Info   :  CCU_Core] CombatCheck_Prefix: Method Call
	[Info   :  CCU_Core] UpdateTargetPosition_Prefix: Method Call
	[Info   :  CCU_Core] MyUpdate_Prefix: Method Call
	[Info   :  CCU_Core] CombatCheck_Prefix: Method Call
	[Info   :  CCU_Core] UpdateTargetPosition_Prefix: Method Call
	[Info   :  CCU_Core] MyUpdate_Prefix: Method Call
	[Info   :  CCU_Core] CombatCheck_Prefix: Method Call
	[Info   :  CCU_Core] UpdateTargetPosition_Prefix: Method Call
	[Info   :  CCU_Core] MyUpdate_Prefix: Method Call
	[Info   :  CCU_Core] CombatCheck_Prefix: Method Call
	[Info   :  CCU_Core] UpdateTargetPosition_Prefix: Method Call
	[Info   :  CCU_Core] MyUpdate_Prefix: Method Call
	[Info   :  CCU_Core] CombatCheck_Prefix: Method Call
	[Info   :  CCU_Core] UpdateTargetPosition_Prefix: Method Call
	[Info   :  CCU_Core] MyUpdate_Prefix: Method Call
	[Info   :  CCU_Core] CombatCheck_Prefix: Method Call
	[Info   :  CCU_Core] UpdateTargetPosition_Prefix: Method Call
	[Info   :  CCU_Core] MyUpdate_Prefix: Method Call
	[Info   :  CCU_Core] CombatCheck_Prefix: Method Call
	[Info   :  CCU_Core] UpdateTargetPosition_Prefix: Method Call
	[Info   :  CCU_Core] MyUpdate_Prefix: Method Call
	[Info   :  CCU_Core] CombatCheck_Prefix: Method Call
	[Info   :  CCU_Core] UpdateTargetPosition_Prefix: Method Call
	[Info   :  CCU_Core] MyUpdate_Prefix: Method Call
	[Info   :  CCU_Core] CombatCheck_Prefix: Method Call
	[Info   :  CCU_Core] UpdateTargetPosition_Prefix: Method Call
	[Info   :  CCU_Core] MyUpdate_Prefix: Method Call
	[Error  : Unity Log] AI Update Error: Thief (1124) (Agent)
- In the failed Try{} block:
  - agent = this.AIOffsetGroups[curGroup][j];
  - agent.brainUpdate.MyUpdate();
    - Added logging
  - agent.combat.CombatCheck();
    - Added logging
  - agent.pathfindingAI.UpdateTargetPosition();
    - Added logging
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
##		C	Interaction
###			C	Administer Bloodbag
New
###			C	Arena Manager
New
###			C	Accept Bribe Cop
New
###			C	Accept Bribe Cop
New
###			C	Accept Bribe Cop
New
###			C	Buyer All
New
###			C	Buyer Vendor
New
###			C	Extortable
I think this may be in two categories
###			C	Moochable
I think this may be in two categories
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
##		CT	Map Marker
###			T	Agent Assumption Bug
- Error:
	[Info   : Unity Log] 62% - SETUPMORE2
	[Info   : Unity Log] Set BigQuest: Hobo
	[Info   : Unity Log] Player Info: Playerr (Agent) - Hobo - 0 - True -  - 1044
	[Error  : Unity Log] ArgumentNullException: Value cannot be null.
	Parameter name: agent
	Stack trace:
	RogueLibsCore.RogueExtensions.HasTrait (Agent agent, System.Type traitType) (at <d35155fde6a3417a9000d4114e51e814>:0)
	CCU.Traits.TraitManager+<>c__DisplayClass26_0.<HasTraitFromList>b__0 (System.Type p) (at <31b69145882c4eb4ae22fe099cb9c0dd>:0)
	System.Linq.Enumerable.Any[TSource] (System.Collections.Generic.IEnumerable`1[T] source, System.Func`2[T,TResult] predicate) (at <55b3683038794c198a24e8a1362bfc61>:0)
	CCU.Traits.TraitManager.HasTraitFromList (Agent agent, System.Collections.Generic.List`1[T] traitList) (at <31b69145882c4eb4ae22fe099cb9c0dd>:0)
	CCU.Patches.Interface.P_QuestMarker.CheckifSeen2_Postfix (QuestMarker __instance) (at <31b69145882c4eb4ae22fe099cb9c0dd>:0)
	QuestMarker.CheckIfSeen2 () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	QuestMarker.CheckIfSeen () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	QuestMarker+<CheckIfMapFilled>d__74.MoveNext () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	UnityEngine.SetupCoroutine.InvokeMoveNext (System.Collections.IEnumerator enumerator, System.IntPtr returnValueAddress) (at <451019b49f1347529b43a32c5de769af>:0)

	[Error  : Unity Log] ArgumentNullException: Value cannot be null.
	Parameter name: agent
	Stack trace:
	RogueLibsCore.RogueExtensions.HasTrait (Agent agent, System.Type traitType) (at <d35155fde6a3417a9000d4114e51e814>:0)
	CCU.Traits.TraitManager+<>c__DisplayClass26_0.<HasTraitFromList>b__0 (System.Type p) (at <31b69145882c4eb4ae22fe099cb9c0dd>:0)
	System.Linq.Enumerable.Any[TSource] (System.Collections.Generic.IEnumerable`1[T] source, System.Func`2[T,TResult] predicate) (at <55b3683038794c198a24e8a1362bfc61>:0)
	CCU.Traits.TraitManager.HasTraitFromList (Agent agent, System.Collections.Generic.List`1[T] traitList) (at <31b69145882c4eb4ae22fe099cb9c0dd>:0)
	CCU.Patches.Interface.P_QuestMarker.CheckifSeen2_Postfix (QuestMarker __instance) (at <31b69145882c4eb4ae22fe099cb9c0dd>:0)
	QuestMarker.CheckIfSeen2 () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	QuestMarker.CheckIfSeen () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	QuestMarker+<CheckIfMapFilled>d__74.MoveNext () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	UnityEngine.SetupCoroutine.InvokeMoveNext (System.Collections.IEnumerator enumerator, System.IntPtr returnValueAddress) (at <451019b49f1347529b43a32c5de769af>:0)
	UnityEngine.MonoBehaviour:StartCoroutine(IEnumerator)
	QuestMarker:StartReal()
	<WaitToStart>d__70:MoveNext()
	UnityEngine.MonoBehaviour:StartCoroutine(IEnumerator)
	QuestMarker:Start()
- Fix:
  - Gate this part behind checking if it's an agent
    - Didn't work, still occurs
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
    - There's a note up in general that should be here
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
##		N	NPC Groups
###			C	Roamer LevelFeature
New
###			C	Slum NPCs
New
##		CT	Passive
###			C	Explode On Death
Works but... exploded when arrested 
###			√	Guilty
Complete
###			C	Hackable - Tamper With Aim
New
###			C	Hackable - Go Haywire
New
###			C	Innocent
New
XP Penalty for neutralizing
###			C	Invincible
New
###			C	Reviveable (Infinite)
Instead of dying, agent will be Injured instead. Player can revive them or hire someone to do it unlimited times.
###			C	Reviveable (Standard)
Instead of dying, agent will be Injured instead. Player can revive them or hire someone to do it once.
###			C	Vision Beams (Cop Bot)
New
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
New
###			C	Sort active Traits by Value
New
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