<p align="center">

# Streets of Rogue Custom Content Utilities

|:exclamation: Asterisk indicates non-implemented features currently planned. :exclamation:|
|:-------------------------------------------------------------------------------:|
|All features are optional and modular.<br><br>Your content will still be playable without this mod - it just won't have the mod features. 

</p>

# Mod Core

## Designer Edition

The Designer Edition of the mod is virtually identical to the player edition, with the exception that your Character Creation menu will show all custom character traits.

## Player Edition

The **Player Edition** hides designer content to keep their experience as close to vanilla as possible:

- Hidden from Character Creation Menus
- Hidden from Character Sheet page *
- Hidden from HUD Trait list *

The traits will still be stored in the character, they just won't clutter up the player's side.

# Custom NPC Utilities

## Appearances *

There is a long list of traits for the following:
- Facial Hair
- Hair Color
- Hair Style
- Skin Color

You can add as many as you want to a custom character. When you do so:
- Player Characters will always have the vanilla appearance you define in the editor. 
- If you add any appearance traits, any NPCs generated from this character will have those aspects of their appearance randomized from the pool of traits you selected. This includes NPCs placed in custom Chunks, as well as those generated from Clone Machines or Loneliness Killer.

## Behaviors *

- Increase Trait slots by 1, since it will not affect player character *

Effects should be self-explanatory. Let me know if any are unclear.

|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|AI: Bartender BuyRound								|
|AI: Bartender Vendor
|AI: Bouncer Bribeable
|AI: Bouncer GuardDoor
|AI: ButlerBot Clean
|AI: Cannibal Cannibalize
|AI: Cannibal AmbushBush
|AI: Cannibal AmbushManhole
|AI: Cannibal HostileToSoldiers
|AI: Clerk Bank
|AI: Clerk BloodBank
|AI: Clerk DeportationCenter
|AI: Clerk Hotel
|AI: Clerk MovieTheater
|AI: Cop AcceptBribe
|AI: Cop EnforceLaws
|AI: Cop Lockdown
|AI: CopBot Enforcelaws
|AI: CopBot VisionBeams
|AI: Doctor AdministerBloodBag
|AI: Doctor Heal
|AI: Doctor UseBloodBag
|AI: DrugDealer UseDrugs
|AI: DrugDealer Vendor
|AI: Firefighter FightFire
|AI: Gangbanger SpawnRoamingGangs
|AI: Gorilla Hire
|AI: Gorilla Vendor
|AI: Hacker Hack
|AI: Jock ArenaManager
|AI: KillerRobot Chase
|AI: Mayor Bodyguarded SuperCop
|AI: Mobster InfluenceElection
|AI: Mobster Shakedown
|AI: Musician Bodyguarded Goo
|AI: Musician Bodyguarded Supergoo
|AI: Musician Turntables
|AI: OfficeDrone OfferMotivation
|AI: Scientist Identify
|AI: Shapeshifter Possess
|AI: Shopkeeper Vendor
|AI: Soldier HostileToCannibals
|AI: Soldier MallVendor
|AI: Slave Enslaved
|AI: Slavemaster SellSlaves
|AI: Slavemaster OwnSlaves
|AI: SlumDweller CauseRuckus
|AI: SlumDweller CommonFolk
|AI: SlumDweller GrabMoney
|AI: Supercop EnforceLaws
|AI: Thief AmbushManhole
|AI: Thief BreakIn
|AI: Thief HonorAmongThieves
|AI: Thief Pickpocket
|AI: UpperCruster Bodyguarded
|AI: UpperCruster OwnSlave
|AI: UpperCruster Tattle
|AI: Vampire Bite
|AI: Vampire HostileToWerewolves
|AI: Various AnnoyedAtSuspicious
|AI: Various Coward
|AI: Various Hire
|AI: Various Extort
|AI: Various Guilty
|AI: Various Scumbag

## Factions *

- Combine Owner IDs (hopefully possible) *

|Trait								|Effect													|
|:----------------------------------|:------------------------------------------------------|
|Faction 1 Aligned					|NPC aligned with all NPCs with same trait				|
|Faction 1 Hostile					|NPC hostile to all NPCs with Faction 1 Aligned			|
|Faction 2 Aligned					|NPC aligned with all NPCs with same trait				|
|Faction 2 Hostile					|NPC hostile to all NPCs with Faction 2 Aligned			|
|Faction 3 Aligned					|NPC aligned with all NPCs with same trait				|
|Faction 3 Hostile					|NPC hostile to all NPCs with Faction 3 Aligned			|
|Faction 4 Aligned					|NPC aligned with all NPCs with same trait				|
|Faction 4 Hostile					|NPC hostile to all NPCs with Faction 4 Aligned			|

## Steam Workshop Upload *

- Pop up Yes/No dialogue asking if user wants to do the following:
  - Automate screenshot of character portrait for thumbnail
  - Automate screenshot of character stat screen
  - Upload both screenshots into Character folder before upload
  - Output a Text file with all character content for upload as a description

# Chunk Editor Utilities

## Hotkeys

|Key 1				|Key 2				|Layer				|Function												|
|:------------------|:------------------|:------------------|:------------------------------------------------------|
|F5	*				|					|					|Quicksave current file (skip confirmation)
|F9	*				|					|					|Quickload current file (skip confirmation)
|[1 - 9]		    |					|					|Switch to Layer (Wall, Floor, Floor2, etc.)
|[1 - 9] *			|Ctrl				|					|Switch to Layer and open Type Selector
|Tab *				|					|					|Move through input fields				
|Tab *				|Shift				|					|Move through input fields, reversed	
|Shift *			|Ctrl				|Patrol				|Show only patrol points in the current Patrol ID
|A					|Ctrl				|					|Select all in Layer (Toggle)			
|O					|Ctrl				|					|Open file								
|Q, E *				|					|Object<br>NPC		|Rotate direction<br><br>Works in Draw or Select mode
|Q, E *				|					|Patrol				|Increase/Decrease Patrol Point ID<br><br>Works in Draw or Select mode
|S 					|Ctrl				|					|Save current file						
|Y *				|Ctrl				|					|Redo										
|Z *				|Ctrl				|					|Undo									
|Arrow Keys *		|					|Object<br>NPC		|Set direction<br><br>Press again to clear<br><br>Works in Draw or Select mode

## General *
- Spawn objects placed regardless of district (currently limited to district-appropriate objects)

# Chunk Pack Editor Utilities

# Level Editor Utilities *

## Hotkeys *

|Key 1				|Key 2				|Layer				|Function												|
|:------------------|:------------------|:------------------|:------------------------------------------------------|
|F5	*				|					|					|Quicksave current file (skip confirmation)
|F9	*				|					|					|Quickload current file (skip confirmation)
|Tab *				|					|					|Move through input fields				
|Tab *				|Shift				|					|Move through input fields, reversed	
|A *				|Ctrl				|					|Select all Chunks (Toggle)			
|O *				|Ctrl				|					|Open file								
|Q, E *				|					|Object<br>NPC		|Rotate Chunk<br><br>Works in Draw or Select mode
|S *				|Ctrl				|					|Save current file						
|Y *				|Ctrl				|					|Redo										
|Z *				|Ctrl				|					|Undo									
|Arrow Keys *		|					|Object<br>NPC		|Set Chunk direction<br><br>Press again to clear<br><br>Works in Draw or Select mode

# Campaign Editor Utilities *