<p align="left">
<img src="../Images/CCU_160x160.png" alt="CCU Logo" align="left">
<img src="../Images/CCU_160x160.png" alt="Yeah there are two, so what" align="right">
</p>

<h1 align="center">
<br>
Campaign Branching System
</h1>
<br><br>

Mockup documentation to formulate thoughts for clarity.

##		Terminology
  - ***Flag*** - A boolean (true/false) value tracked within the current level. The value is changed by meeting the conditions of various Switches.
  - ***Switch*** - An Agent Trait or Object Variable that can change the value of a Flag.
  - ***Gate*** - A label for a level that determines how it interacts with Flags and Switches.

##		Example
Here is an example of how to use this system. 

You're making a campaign. In this level, there are four witnesses in prison, and the Warden has slated them all for fast-track execution so they can't testify against him. 

The player's options are:
  - To free the witnesses by getting them to the Elevator.
  - To kill the warden and bribe his assistant. 

If the player succeeds, they get to exit to the Good Courtroom level, where the witnesses save the day. If they fail, they go to the Bad Courtroom where bad things happen. Now, how do we do that?

1. Give the Good Courtroom level the following mutators: 
  - Gate A: Simply labels the level for branching.
  - Gate B: You CAN use multiple tags on a level. We have two tags here to denote the multiple paths a player may take to access it.
  - Switch AND: Requires ALL linked Switches to be true to unlock a Gate. Because the options are to free ALL the witnesses, or kill the warden AND bribe his assistant. Doing only part of either mission will not unlock this level.
2. Give the Bad Courtroom level the following mutators:
  - Gate A: Multiple levels can share the same tag, as well. 
  - Gate B
  - Switch NOT: Unlocks this level if any Switches are false. So this level will be accessible by default, and its access will be revoked if either Gate A or B is unlocked.

3. Assign traits:
	- Witness
	  - Switch A: Links this character with Gate A.
	  - True on Rescued
	- Warden
	  - Switch B
	  - True on Neutralized
	- Warden's Assistant
	  - Switch B
	  - True on Paid

4. Configure the Elevator's exits:
   - Exit A
   - Exit B

##		Level Mutators

|Mutator											|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Gate A/B/C/D   						            |- Level unlocked when its Switch conditions are met. If no Switches are present on the preceding level, it is unlocked by default.
|Gate Level											|- Requires player level 3, 5, 10, 15, 20 to access
|Switch AND                                         |- Requires ALL Switches to be true to gain access.
|Switch NOT                                         |- Revokes access if any Switches are true.
|Switch OR                                          |- Requires a minimum of one Switch for access.
|Switch XOR                                         |- Requires a minimum and maximum of one Switch for access.

##		Elevator Variables
[These will have to be multi-option Scrolling Button Lists that allow adding a whole list of variables to ExtraVarString]

|Variable											|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Exit A/B/C/D								        |- Gives option to exit into the tagged level, if that level's Gate conditions are met.
|Exit +1-4 / 0 / -1-4							    |- Gives option to skip back/on itself/ahead in level list by the given number. Levels accessed this way are not subject to Gate/Switch logic.
|Exit Progression                                   |- When using "Exit Level" vanilla interaction, index of the target level increases every time this level is entered. If this is level 1, elevator will exit to level 2 the first time, 3 the second, etc. This is one way to create a Hub level.
|Disable Vanilla									|- Disables vanilla "Exit Level" option

##		Switches
Switches are simply mechanisms to change the value of Flags. They can take multiple forms.

###         Object Variables

|Variable                               |Objects                                |Effect                     |
|:--------------------------------------|:--------------------------------------|:--------------------------|
|Switch A/B/C/D                         |                                       |- Links this object's Switch to Gate A/B/C/D
|On Destroyed                           |Computer, Generator, Power Box         |
|On Hacked                              |Computer, Elevator                     |
|On Tampered                            |Computer, Elevator, Generator, Power Box|

###         Traits

|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Switch NOT                                         |- Switch true by default, goes false if any conditions for this agent are met.
|Switch OR									        |- Sets Switch to true when any condition for this agent is met.
|Switch AND								            |- Sets Switch to true when all conditions for this agent are met.
|Switch A/B/C/D								        |- Attaches this agent's Switch to Gate A/B/C/D 
|False on Hostile                                   |- Hostile to any player
|False on Neutralize                                |- 
|True On Neutralized							    |- 
|True On Rescued								    |- Escape via Elevator
|True On Paid									    |- Paid $1000