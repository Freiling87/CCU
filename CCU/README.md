# Streets of Rogue Custom Content Utilities

SOR gives you a few options for creating custom content, but it does have some limitations. Here are a few things that are currently impossible with the Vanilla version:
- Allying Custom NPCs in different chunks
- Varying Custom NPC appearances
- Giving Custom NPCs behaviors like pickpocketing, hiring, selling items, etc.
- Hotkeys in the Chunk editor - *really?!*
- Generate district-specific Objects like Flame Grates when placed, regardless of district

CCU aims to address these limitations, giving more power to custom content designers, while keeping the player's experience un-cluttered with designer tools.

|:exclamation: Asterisk indicates non-implemented features currently planned. :exclamation:|
|:----------------------------------------------------------------------------------------:|

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

You can add as many as you want to a custom character. When you do so, NPCs generated from this character will have those aspects of their appearance randomized from the pool of traits you selected. This includes NPCs placed in custom Chunks, as well as those generated from Clone Machines or Loneliness Killer.

## Behavior Traits

|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Drink Blood										|- Enables blood drinking behavior<br>- Requires: Bite
|Eat Corpse											|- Enables corpse eating behavior<br>- Requires: Cannibalize
|Grab Drugs											|- If NPC sees Drugs, they'll try to grab them
|Grab Money											|- If NPC sees Money, they'll try to grab it
|Pickpocket											|- Enables pickpocketing behavior<br>- Requires: Sticky Glove

## Hire Traits 

|Trait												|Effect	|
|:--------------------------------------------------|:------|
|Bodyguard											|- Hire as protection
|Break In											|- Break into target Door or Window
|Cause Ruckus										|- Cause a Ruckus
|Cost Banana										|- Any Hire trait costs a Banana instead of Money
|Cost Less											|- Hire cost decreased by 50%
|Cost More											|- Hire cost increased by 50%
|Hack												|- Hack target Object or Agent
|Safecrack											|- Break into target Safe
|Tamper												|- Tamper with target Object

## Interaction Traits

|Trait								|Effect	|
|:----------------------------------|:------|
|Fence								|- Agent will buy any items for a bad price
|Moochable							|- Agent can be targeted with Moocher
|Vendor Buyer						|- If Agent has a Vendor trait, they will also buy items from that item pool

## Relationship Traits

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
  - LevelEditor.customCharacterList has all customs saved. Iterate through this and find appropriate traits, then add through RandomAgents.fillAgents

## Trait Trigger Traits

|Trait Trigger Trait				|Effect	|
|:----------------------------------|:------|
|Common Folk						|- NPC will be Loyal to those with Friend of the Common Folk
|Cool Cannibal						|- If NPC has Aggressive trait, will no longer be Hostile if you have Cool With Cannibals
|Cop Access							|- If NPC has any Vendor trait, will not sell to the player unless they have The Law
|Honorable Thief					|- If NPC has Sticky Glove, will not Pickpocket the player if they have Honor Among Thieves<br>- If NPC has any Vendor trait, will not sell to the player unless they have Honor Among Thieves

## Vendor Traits *

|Vendor Trait						|Inventory	|
|:----------------------------------|:----------|
|Anthropophagie						|Alcohol, Axes & Bear Traps
|Armorer							|Armor, Durability Spray
|Assassin							|Weaponry & Stealth tools
|Banana Boutique					|Bananas & Peels
|Barbarian Merchant					|Beer, Meat & Tools for Conquest
|Bartender							|Vanilla Bartender shop inventory
|Bartender (Dive)					|Inventory for seedier joints
|Bartender (Fancy)					|Inventory for upscale establishments
|Blacksmith							|Melee Weapons & Durability Spray
|Consumer Electronics				|Appliances & Electronics for the modern idiot
|Contraband							|Stuff confiscated from the City's Ne'er-do-wells<br>If Vendor has Cop Access trait, player will need The Law to buy from this Vendor
|Convenience Store					|Beer, Smokes, and the last place your dad said he'd be before he left
|Cop (Standard)						|Standard Patrolman's gear<br>If Vendor has Cop Access trait, player will need The Law to buy from this Vendor
|Cop (SWAT)							|Doorkicker's gear<br>If Vendor has Cop Access trait, player will need The Law to buy from this Vendor
|Demolition Depot					|Explosives
|Drug Dealer						|Vanilla Drug Dealer shop inventory
|Firefighter Five-and-Dime			|Firefighting & EMT equipment
|Fire Sale							|Arsonist's tools
|Gunsmith							|Guns, Mods & Ammo
|Hardware Store						|Tools & safety equipment
|High Tech							|High tech shit
|Home Fortress Outlet				|Traps
|Hypnotist							|Make people like you with this shit!
|Junk Dealer						|Trash, used toilet paper (Resistance Vouchers), and occasionally a gem
|McFud's							|Home of the Slopper!
|Medical Supplier					|Medicine, scalpels, syringes
|Mining Gear						|Safety Equipment, tools & explosives
|Monke Mart							|Stuff for monke, gorgia, you name it
|Movie Theater						|Vanilla Movie Theater inventory
|Occultist							|Supernatural, ritualistic & spooky shit
|Outdoor Outfitter					|Outdoor & Survival goods
|Pacifist Provisioner				|Tools to avoid unnecessary casualties
|Pawn Shop							|A mixed bag of stuff you'd find in a Pawn Shop
|Pest Control						|Tools to help get rid of pesky vermin
|Pharmacy							|Mostly medicine
|Research Materials					|Weird guns & scientist stuff
|Resistance Commissary				|Escape teleporter & Vouchers
|Riot, Inc.							|All the ingredients in the Anarchist's Cookbook
|Shopkeeper							|Vanilla Shopkeeper shop inventory
|Slave Shop							|Tools for acquiring human merchandise
|Soldier							|Vanilla Soldier shop inventory
|Sporting Goods						|Just Goods, no Bads
|Teleportationist					|If it teleports, we sell-aports! (We're working on the slogan)
|Thief								|Vanilla Thief shop inventory
|Thief Master						|Advanced intrusion tools
|Throwcery Store					|Thrown items & killer thrower
|Toy Store							|Harmless stuff that you probably wouldn't have any use for
|Upper Cruster						|Vanilla Upper Cruster store inventory
|Vampire							|Bloodsucker's supply
|Villain							|For when you're not concerned about collateral damage

## CC Steam Workshop Upload *

- Pop up Yes/No dialogue asking if user wants to do the following:
  - Automate screenshot of character portrait for thumbnail
  - Automate screenshot of character stat screen
  - Upload both screenshots into Character folder and name appropriately before upload
  - Output a Text file with all character content for upload as a description

# Chunk Editor Utilities

## Fields 
- Multiple In Chunk field for SpawnerAgent

## Hotkeys

|Key 1				|Key 2				|Layer				|Mode (Draw/Select)	|Function												|
|:------------------|:------------------|:------------------|:-----------------:|:------------------------------------------------------|
|Arrow Keys			|					|Object<br>NPC		|D / S				|Set direction<br><br>Press again to clear
|F5	*				|					|					|D / S				|Quicksave current file (skip confirmation)
|F9	*				|					|					|D / S				|Quickload current file (skip confirmation)
|F12				|					|					|D / S				|Run chunk
|[1 - 9]		    |					|					|D / S				|Switch to Layer (Wall, Floor, Floor2, etc.)
|[1 - 9]			|Ctrl				|					|D					|Switch to Layer and open Type Selector
|Alt *				|					|Object				|S					|Security Cam: Highlight visible tiles
|Tab *				|					|					|D / S				|Move through input fields & buttons
|Tab *				|Shift				|					|D / S				|Move through input fields & buttons, reversed	
|Shift *			|Ctrl				|Any with Owner IDs	|D / S				|View filter to layer<br><br>Show OwnerIDs on all content
|Shift *			|Ctrl				|Patrol				|D / S				|Display Patrol ID on patrol points
|Shift *			|Alt				|Patrol				|D / S				|Show only patrol points in the current Patrol ID
|A					|Ctrl				|Any				|S					|Select all in Layer<br><br>Press again to Deselect All
|C *				|Ctrl				|Any				|S					|Copy all layers
|C *				|Ctrl + Shift		|Any				|S					|Copy only current layer
|E, Q				|					|Any				|D / S				|Zoom In/Out
|E, Q				|Ctrl				|Object<br>NPC		|D / S				|Rotate direction
|E, Q				|					|Patrol				|D / S				|Increase/Decrease Patrol Point ID
|O					|Ctrl				|					|D / S				|Open file
|S 					|Ctrl				|					|D / S				|Save current file						
|V *				|Ctrl				|Any				|D / S				|Paste all layers on clipboard
|V *				|Ctrl + Shift		|Any				|D / S				|Past only current layer on clipboard
|Y *				|Ctrl				|					|D / S				|Redo										
|Z *				|Ctrl				|					|D / S				|Undo									
|Middle Mouse *		|					|					|D / S				|Drag Viewport
|Letter Keys		|					|Any with Type		|D / S				|When Type Selector is open, scroll to alphabetical entry starting in key pressed.<br>Press again to cycle through entries under that letter.

## Item Groups *
- Know how you can select "Slum NPCs" as a type? Why can't we do that with items? Game already has most of the rLists made anyway 

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
