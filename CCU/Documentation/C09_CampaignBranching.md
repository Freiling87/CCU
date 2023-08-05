<p align="left">
<img src="../Images/CCU_160x160.png" alt="CCU Logo" align="left">
<img src="../Images/CCU_160x160.png" alt="Yeah there are two, so what" align="right">
</p>

<h1 align="center">
<br>
Campaign Branching System
</h1>
<br><br>

THIS IS A DRAFT. THIS SYSTEM IS NOT IMPLEMENTED YET.

##		Terminology
  - ***Flag*** - A boolean (true/false) value tracked within the current level. The value is changed by meeting the conditions of various Switches.
  - ***Switch*** - An Agent Trait or Object Variable that can change the value of a Flag.
  - ***Gate*** - A label for a level that determines how it interacts with Flags and Switches.

##		Example
Here is an example of how to use this system. 

You're making a campaign. In this level, there is a Witness in prison who wants to testify against a mob boss. The mob boss has bribed the prison's Warden to fast-track the Witness for execution so he can't testify against him. The player's options are:
  - To free the witnesses by getting them to the Elevator.
  - To kill the Warden and bribe his Assistant to keep his mouth shut about it. 

If the player succeeds, they get to exit to the Good Courtroom level, where the witnesses save the day. If they fail, they go to the Bad Courtroom where bad things happen. Now, how do we do that?

1. Give the Good Courtroom level the following mutators: 

2. Give the Bad Courtroom level the following mutators:

3. Assign traits:
	- Witness
	- Warden
	- Warden's Assistant

4. Configure the Elevator's exits:

##		Level Mutators

###			Exit Gates

|Mutator											|Notes													|
|:--------------------------------------------------|:------------------------------------------------------|
|Exit [A/B/C/D]: [AND/NOT/OR/XOR]					|- Level unlocked when its Switch conditions are met. If no Switches are present on the preceding level, it is unlocked by default.<br>To be clear, there are effectively sixteen mutators to manage here.

##		Elevator Variables
[These will have to be multi-option Scrolling Button Lists that allow adding a whole list of variables to ExtraVarString]

|Variable											|Notes													|
|:--------------------------------------------------|:------------------------------------------------------|
|Exit [A/B/C/D]										|- Gives option to exit into the tagged level, if that level's Gate conditions are met.
|Exit [-4 to 4]										|- Gives option to skip back/on itself/ahead in level list by the given number. Levels accessed this way are not subject to Gate/Switch logic.
|Exit Progression                                   |- When using "Exit Level" vanilla interaction, index of the target level increases every time this level is entered. If this is level 1, elevator will exit to level 2 the first time, 3 the second, etc. This is one way to create a Hub level.
|Disable Vanilla									|- Disables vanilla "Exit Level" option

##		Switches
Switches are simply mechanisms to change the value of Flags. They can take multiple forms.

###         Object Variables

|Variable                               |Notes						|
|:--------------------------------------|:--------------------------|
|Link [Gate A/B/C/D]                    |- Links this object's Switch to Gate A/B/C/D
|Switch [AND/OR/NOT/XOR]				|
|If [Switch Trigger] then [True/False]	|

####			Object Switch Triggers

|Trigger											|Notes													|
|:--------------------------------------------------|:------------------------------------------------------|
|Deactivated
|Destroyed
|Hacked
|Powered
|Tampered

###         Traits

|Trait												|Notes													|
|:--------------------------------------------------|:------------------------------------------------------|
|Link [Gate A/B/C/D]						        |- Attaches this agent's Switch to Gate A/B/C/D 
|Switch [AND/NOT/OR/XOR]							|
|If [Switch Trigger] then [True/False]				|- See Trigger table below

####			Agent Switch Triggers

|Trigger											|Notes													|
|:--------------------------------------------------|:------------------------------------------------------|
|Freed												|- Free from Prison
|Gibbed												|-
|Hired												|-
|Hired Permanently									|-
|Holed												|- Gibbing doesn't count
|Killed												|-
|[Relationship]										|- To any player
|Neutralized										|- Any method 
|Exit Level											|- 
|Paid												|- Paid $1000