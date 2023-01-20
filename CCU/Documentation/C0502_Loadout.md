<p align="left">
<img src="../Images/CCU_160x160.png" alt="CCU Logo" align="left">
<img src="../Images/CCU_160x160.png" alt="Yeah there are two, so what" align="right">
</p>

<h1 align="center">
<br>
Loadout System
</h1>
<br><br>

This system makes use of the items you add in the character editor. You control the generation of the inventory through a series of traits.

#		Terms Used
* **<u>Pockets</u>** - All inventory not put into an equipment Slot. 
* **<u>Pool</u>** - All items added to a character for a particular Slot.
* **<u>Slot</u>** - Equipment slot. Slots: Headgear, Body armor, Ranged Weapon, Melee Weapon, Thrown Weapon, Pocket.

#		How to use the system
First, pick your items. Add them through the Character Creator as you normally would for a player character.

Then, select a Loader trait. Without one, items will not be generated on your character.

#	Traits 

##		Chunk Items

|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Chunk Key											|- Starts with the chunk's Key
|Chunk Safe Combo									|- Starts with the chunk's Safe Combo

##		Gun Nut

|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Accuracy Mod										|- Applies mod to all applicable weapons in inventory
|Ammo Stock											|"
|Rate of Fire Mod									|"
|Silencer											|"

##		Loader

|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Flat Distribution									|- Pulls a random item for each Slot from its Pool. All items have an equal chance. "No item" is given an equal chance to all items in the Pool.
|Scaled Distribution								|- Each item in a Slot's Pool is assigned a chance to generate, inverse to its cost - this means cheaper items are more likely to generate. Rolls an attempt to generate each item. If all chance rolls fail, no item is generated for that Slot.
|Upscaled Distribution								|Identical to the Scaled Distribution Loader, except the chance for an item to generate is proportional to its cost. This causes the most expensive items selected to be more likely.

##		Money

|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Bankrupt 25%										|- 25% chance for agent to not spawn with Money
|Bankrupt 50%										|- 50% chance for agent to not spawn with Money
|Bankrupt 75%										|- 75% chance for agent to not spawn with Money
|Broke												|- Agent spawns with $1 to $6
|Desperate											|- Agent spawns with $6 to $11
|Poor												|- Agent spawns with $11 to $26
|Prosperous											|- Agent spawns with $26 to $41
|Rich												|- Agent spawns with $41 to $61
|Wealthy											|- Agent spawns with $81 to $100
|Zillionaire										|- Agent spawns with $1000

##		Pockets
Default Pocket item max is 1.

|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|FunnyPack											|- Pocket Item generation limit raised by 1
|FunnyPack Extreme									|- Pocket Item generation limit raised by 2
|FunnyPack Pro										|- Pocket Item generation limit raised by 99
|Have												|- Agent will never fail to draw a Pocket item, if there are any in the Pool.
|Have Mostly										|- 25% chance to not generate a pocket item.
|Have Not											|- 50% chance to not generate a pocket item.

##		Slots
Slot item limit default is 1 item.

|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Equipment Chad										|- Agent will never fail to draw an item, if there are any in that slot's Pool.
|Equipment Enjoyer									|- 25% chance per Slot to not generate an item.
|Equipment Virgin									|- 50% chance per Slot to not generate a pocket item.
|Sidearmed											|- Item generation limit raised by 1 per Slot
|Sidearmed but on Both Sides						|- Item generation limit raised by 2 per Slot
|Sidearmed to the Teeth								|- Item generation limit raised to 99 per Slot