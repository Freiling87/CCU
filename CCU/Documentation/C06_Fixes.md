<p align="left">
<img src="../Images/CCU_160x160.png" alt="CCU Logo" align="left">
<img src="../Images/CCU_160x160.png" alt="Yeah there are two, so what" align="right">
</p>

<h1 align="center">
<br>
Gameplay Fixes
</h1>
<br><br>

Frequently-requested fixes to vanilla bugs, or tweaks that are obviously in favor of the spirit of the original game.

##		Combat Skills
*Many thanks to **BlazingTwist**, who submitted this fix!*

NPCs have hidden stats named `modGunSkill` and `modMeleeSkill`, which range from 0 to 2.

NPCs with higher combat skill:
* Are less likely to run when carrying the corresponding weapon type.
* Are more likely to attack with the corresponding weapon type.
* Spend less time idling between attacks.

Vanilla SoR leaves these values at 0 for all custom NPCs. CCU introduces a fix that scales the stat to 'Melee' or 'Firearms' attribute. 

Here's an explicit breakdown of how the attributes convert. It's identical for Melee.

|Firearms	|modGunSkill - Vanilla	|modGunSkill - CCU	|
|:---------:|:---------------------:|:-----------------:|
|1			|0						|0
|2			|0						|1
|3			|0						|2
|4			|0						|2