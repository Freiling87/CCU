<p align="left">
<img src="../Images/CCU_160x160.png" alt="CCU Logo" align="left">
<img src="../Images/CCU_160x160.png" alt="Yeah there are two, so what" align="right">
</p>

<h1 align="center">
<br>
Campaign Branching System
</h1>
<br><br>

This is mockup documentation in order to formulate my thoughts. This system is not created yet.

Look, this is gonna be relatively complex for SOR. Both my tools and yours are pretty limited, so expect a little bit of setup.

##		Terminology
  - ***Flag*** - A boolean (true/false) value tracked within the current level. The value is changed by meeting the conditions of various Switches.
  - ***Switch*** - An Agent Trait or Object Variable that can change the value of a Flag.
  - ***Tag*** - A label for a level that determines how it interacts with Flags and Switches.

##		Example
Here is an example of how to use this system. 

You're making a campaign. In this level, there are four witnesses in prison, and the Warden has slated them all for fast-track execution so they can't testify against him. 

The player's options are:
  - To free the witnesses by getting them to the Elevator.
  - To kill the warden and bribe his assistant. 

If the player succeeds, they get to exit to the Good Courtroom level, where the witnesses save the day. If they fail, they go to the Bad Courtroom where bad things happen. Now, how do we do that?

1. Give the Good Courtroom level the following mutators: 
  - Level Tag A: Simply labels the level for branching.
  - Level Conditions AND: Because the options are to free ALL the witnesses, or kill the warden AND bribe his assistant. Doing only part of either mission will not unlock this level.
2. Give the Bad Courtroom level the following mutators:
  - Level Tag A: Multiple levels can share the same tag.
  - Level Conditions NOT: Inverts how this level interacts with its Flag, making it mutually exclusive with the Good Courtroom level.


<<<< WARNING: This is incorrect. Since you have Level Conditions AND, there can't be two paths to the same level unless you somehow split the flags. >>>>

3. Assign traits:
	- Witness:
      - Flag Link A: Tells the system that this NPC will affect your access to any levels with Tag A.
      - Flag True on Rescued
    - Warden:
      - Flag True on Neutralized
    - Warden's Assistant:
      - Flag True on Paid


##		Level Tags
A Level Tag simply marks a level for interaction with other mechanics.

|Mutator											|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Tag A/B/C/D OR							            |- Unlocks this level when ANY of its Switches are flipped.
|Tag A/B/C/D AND						        	|- Unlocks this level when ALL of its Switches are flipped.
|Tag A/B/C/D NOT						        	|- Locks this level when any of its Switches are flipped. 

##		Elevator Variables

|Variable											|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Access Level A/B/C/D								|- Gives options to exit into the tagged level, if that level's Flag is TRUE.
|Access Level +1/+2/+3/+4							|- Gives options to skip ahead in level list by the given number, if that level's Flag is TRUE.<br>- Unflagged levels are freely accessible.
|Disable Vanilla									|- Disables vanilla "Exit Level" option

##		Switches
Switches are simply mechanisms to change the value of Flags.

###         Object Variables

###         Traits

|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Flag Conditions OR									|- Flag value is set when any condition for this agent is met.
|Flag Conditions AND								|- Flag value is only set when all conditions for this agent are met.
|Flag Link A/B/C/D									|- Attaches this Agent to the indicated Flag. Flips the Flag to TRUE if 
|Flag True On Neutralized							|- Sets Flag to TRUE if neutralized
|Flag True On Rescued								|- Sets Flag to TRUE if escapes via Elevator
|Flag True On Paid									|- Sets Flag to TRUE if paid $1000