<p align="left">
<img src="../Resources/Image/CCU/CCU_160x160.png" alt="CCU Logo" align="left">
<img src="../Resources/Image/CCU/CCU_160x160.png" alt="Yeah there are two, so what" align="right">
</p>

<h1 align="center">
<br>
Campaign Branching System
</h1>
<br><br>

The Campaign Branching system uses <b>[Custom Mutators](/CCU/Documentation/S02_Configurator.md)</b>. ~~Crossed out~~ entries indicate planned content.

##		Important Terms
- Label - An arbitrary integer value that labels mutators, traits, etc. Only content that shares a Label will interact.
- Switch - A True/False value based on certain conditions. 
  - E.g., an Agent may have a Switch that flips to True if they are rescued.

###			Level Gate
Level Gates prevent entry to a level unless their conditions are met.

|Element				|Values						|Notes													|
|:----------------------|:--------------------------|:------------------------------------------------------|
|Type					|Entry, ~~Exit~~			|- Determines whether this mutator gates entry to the level, or exiting from it. Logic is applied at Exit Elevators, for now.
|Label					|1 or more Integers			|- Gate logic applies only to Switches with selected labels.
|Switch					|Agent, ~~Level, Object~~	|- Gate logic applies only to Switches of the selected types.
|Logic					|AND, NAND, NOR, OR, XNOR, XOR|- Applies boolean logic to all Switches with the same label. 

<!--
WIP NOTES

###			Level Gate Switch Triggers

|Mutator											|Notes													|
|:--------------------------------------------------|:------------------------------------------------------|
|Alien Summoned
|All [class] neutralized
|Computer Hacked
|Happy Waves

###			Tracked Variables
Track countable events throughout the campaign for freer branching.

##         Object Variables

###			Object Switches

|Variable											|Notes						|
|:--------------------------------------------------|:--------------------------|
|Switch [A/B/C/D]									|
|Switch Modifiers [OR/NOT/XOR]						|- Default behavior: OR

####			Object Switch Triggers

|Trigger											|Notes													|
|:--------------------------------------------------|:------------------------------------------------------|
|Deactivated
|Destroyed
|Hacked
|Powered
|Tampered

###			Elevator Variables
[These will have to be multi-option Scrolling Button Lists that allow adding a whole list of variables to ExtraVarString]

|Variable											|Notes													|
|:--------------------------------------------------|:------------------------------------------------------|
|Exit [A/B/C/D]										|- Gives option to exit into the tagged level, if that level's Gate conditions are met.
|Exit [-4 to 4]										|- Gives option to skip back/on itself/ahead in level list by the given number. Levels accessed this way are not subject to Gate/Switch logic.
|Exit Progression                                   |- When using "Exit Level" vanilla interaction, index of the target level increases every time this level is entered. If this is level 1, elevator will exit to level 2 the first time, 3 the second, etc. This is one way to create a Hub level.
|Disable Vanilla									|- Disables vanilla "Exit Level" option

##		Traits

|Trait												|Notes													|
|:--------------------------------------------------|:------------------------------------------------------|
|Switch [A/B/C/D]									|- If Switch Triggers are true, flips this Switch to true, contributing to access to Gate.
|Switch Modifiers [AND/OR/XOR]						|- Default behavior: OR

###			Agent Switch Triggers

|Trigger											|Notes													|
|:--------------------------------------------------|:------------------------------------------------------|
|Dismissed											|
|Freed												|
|Gibbed												| 
|Hired												|
|Hired Permanently									|
|Holed												|
|Killed												|
|[Relationship]	to Player							|
|Neutralized										|
|Exit Level											| 
|Paid $1000											|

--!>