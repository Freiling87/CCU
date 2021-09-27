# Streets of Rogue Custom Content Utilities

SOR gives you a few options for creating custom content, but it does have some limitations. Here are a few things that are currently impossible with the Vanilla version:
- Allying Custom NPCs in different chunks
- Varying Custom NPC appearances
- Giving Custom NPCs behaviors like pickpocketing, hiring, selling items, etc.
- Hotkeys in the Chunk editor - *really?!*
- Generate district-specific Objects like Flame Grates when placed, regardless of district

CCU aims to address these limitations, giving more power to custom content designers, while keeping the player's experience un-cluttered with designer tools.

|:exclamation: Asterisk indicates non-implemented features currently planned. :exclamation:|
|:-------------------------------------------------------------------------------:|

# The Mod Itself

> What if my players don't want the mod? Will this break my stuff?

Your content will still work just fine for vanilla players! They just won't get the *extra* features CCU adds.

CCU is packaged into two editions. One for you, one for your players. If you make content with CCU, just link them to the Player Edition and advise them to install it for the full experience.

Maybe even threaten them. "Or else" is a great phrase that is hard to use as evidence in court!

## Designer Edition

The Designer Edition of the mod is virtually identical to the player edition, with the exception that your Custom Character Creator's Trait Menu will show all of the new traits.

## Player Edition

The Player Edition allows players to play your content, while hiding it from view so it doesn't clutter the interface:
- Hides CCU traits from Character Creation Menus
- Hides CCU traits from Character Sheet page *
- Hides CCU traits from HUD Trait list *

# Custom NPC Utilities

All CCU traits will not count toward the Trait Limit, since they do not affect player characters. *

## Appearance Traits * 

There are traits for each variation within the following groups:
- Facial Hair
- Hair Color
- Hair Style
- Skin Color

You can add as many as you want to a custom character. When you do so:
- **Player Characters** will not be affected by appearance traits. 
- **NPCs** generated from this character will have those aspects of their appearance randomized from the pool of traits you selected. This includes NPCs placed in custom Chunks, as well as those generated from Clone Machines or Loneliness Killer.

## Behavior Traits *

|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|AI: Bartender BuyRound
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
|AI: Musician Bodyguarded Goon
|AI: Musician Bodyguarded Supergoon
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
|AI: Thief Vendor
|AI: UpperCruster Bodyguarded
|AI: UpperCruster OwnSlave
|AI: UpperCruster Tattle
|AI: Vampire Bite
|AI: Vampire HostileToWerewolves
|AI: Various AnnoyedAtSuspicious
|AI: Various Coward
|AI: Various Extortable
|AI: Various Hireable
|AI: Various Guilty
|AI: Various Scumbag

## Faction Traits *

|Trait								|Effect													|
|:----------------------------------|:------------------------------------------------------|
|Faction 1 Aligned					|Aligned with all Agents with same trait
|Faction 1 Hostile					|Hostile to all Agents with Faction 1 Aligned
|Faction 2 Aligned					|Aligned with all Agents with same trait
|Faction 2 Hostile					|Hostile to all Agents with Faction 2 Aligned
|Faction 3 Aligned					|Aligned with all Agents with same trait
|Faction 3 Hostile					|Hostile to all Agents with Faction 3 Aligned
|Faction 4 Aligned					|Aligned with all Agents with same trait
|Faction 4 Hostile					|Hostile to all Agents with Faction 4 Aligned

## Roamer Traits *
- Add a trait to have that NPC show up in the list of available roaming NPCs in various districts.
  - E.g., make Junkie, add Roamer_Slums. Then make a level and in Features, his name will pop up. [Maybe possible through Trait OnAdded/OnRemoved behaviors]
  - Could also have this create a mutator with Custom Roamers, allowing designers to affect vanilla gameplay with new NPCs

## Steam Workshop Upload *

- Pop up Yes/No dialogue asking if user wants to do the following:
  - Automate screenshot of character portrait for thumbnail
  - Automate screenshot of character stat screen
  - Upload both screenshots into Character folder and name appropriately before upload
  - Output a Text file with all character content for upload as a description

# Chunk Editor Utilities

## Fields 
- Multiple In Chunk field for SpawnerAgent

## Hotkeys

|Key 1				|Key 2				|Layer				|Function												|
|:------------------|:------------------|:------------------|:------------------------------------------------------|
|F5	*				|					|					|Quicksave current file (skip confirmation)
|F9	*				|					|					|Quickload current file (skip confirmation)
|F12 *				|					|					|Run chunk
|[1 - 9]		    |					|					|Switch to Layer (Wall, Floor, Floor2, etc.)
|[1 - 9]			|Ctrl				|					|Switch to Layer and open Type Selector
|Alt				|					|Object				|Security Cam: Highlight visible tiles<br>
|Tab *				|					|					|Move through input fields				
|Tab *				|Shift				|					|Move through input fields, reversed	
|Shift *			|Ctrl				|Any with Owner IDs	|View filter to layer<br><br>Show OwnerIDs on all content
|Shift *			|Ctrl				|Patrol				|Display Patrol ID on patrol points
|Shift *			|Alt				|Patrol				|Show only patrol points in the current Patrol ID
|A					|Ctrl				|					|Select all in Layer (Toggle)			
|O					|Ctrl				|					|Open file								
|Q, E *				|					|Object<br>NPC		|Rotate direction<br><br>Works in Draw or Select mode
|Q, E *				|					|Patrol				|Increase/Decrease Patrol Point ID<br><br>Works in Draw or Select mode
|S 					|Ctrl				|					|Save current file						
|Y *				|Ctrl				|					|Redo										
|Z *				|Ctrl				|					|Undo									
|Arrow Keys *		|					|Object<br>NPC		|Set direction<br><br>Press again to clear<br><br>Works in Draw or Select mode
|Middle Mouse *		|					|					|Drag Viewport

## General *
- Spawn objects placed regardless of district (vanilla limits to district-appropriate objects)
- Red-tint any objects that might spawn inconsistently due to placement rules (e.g., Security Cams over gaps)

## Warnings *
- Notify player when saving & edges are blocked

# Chunk Pack Editor Utilities *

No ideas for this one, yet. Taking requests.

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

No ideas for this one, yet. Taking requests.

# Player utilities *

- When in gameplay map view, mouseover a chunk to see its name and author in the unused space in the margins.
  - Gives credit to author
  - Helps identify gamebreaking chunks, allowing you to not use the chunk pack or notify their author.