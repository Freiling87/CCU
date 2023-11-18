##			Feature Scope
###			C	Elevator Text
Pop up before and after entry/exit of level
Button 2
###			C	Improve Branching Interaction
If non-default branch:
- Clear all interactions before adding buttons
  - Because Learn Language menu included "Offer drink"
- Add "Back" button
  - Because Done just exits the interaction fully.
###			P	Pre-release checks
####			BunnyLibs
- Ensure that ALL your mods work with it before releasing any. 
- If library needs a re-release, check other mods for back-compatibility and re-release those if necessary.
####			Quality Review
Review all implemented features and fixes:
- Does this need to be gated for Home Base?
- Are both NPCs and PCs addressed?
- Have you confirmed that Hooks are used as minimally as possible?
####			Project modes
- Disable Developer mode
- Make Player & Designer editions
  - Maybe this can be automated in build event?
####			Designer Traits
Notes here
####			Player Traits
Upgrades: IsAvailable = false
	Keeps from showing in levelup
###			C	CCU Character Creation Page
Might not be impossible. See CharacterCreation.Awake.
Try COPYING something like TraitsMenu in CharacterCreation.Awake and see if you can repurpose it.
Just search Trait in that method, and do everything he does there.
###			T	Behavior - Vigilant
New
###			H	Collapsible Menus at last?
https://sugarbarrel.github.io/RogueLibs/docs/dev/unlocks/configuring-unlocks
See bottom examples, "Categories"
###			C	T_CCU bools
affectsPC
affectsNPC
###			C	Make Class Alignment Traits Player Traits
New
###			C	I think the PostProcess thing for traits is pointless
These can all be set with
Unlock = 
{ 
	cantLose. etc.
}
within the WithUnlock parameters
BUT when you do this you will need to go deep into those variables since there is no note about this thing's purpose.
###			T	Test out Chunk Key/ Combo
Their code was moved in my code but technically in the same place in the loadlevel sequence. Optimistic that it will work fine.
Check for:
	Duplicate key allocation
	No keys allocated
	Normal allocation taking place
###			C	New Scene Setters
Need tester feedback
Need a note saying classes don't cover all the bases here, some are just strings
###			C	Permanent Status Effects
I think this should be moved to ResistanceHR.
####			D	Player documentation

//##			Permanent Status Effect

|Trait												|CCPV	|Effect													|
|:--------------------------------------------------|------:|:------------------------------------------------------|
|Above the Laws										|32		|- Above the Law
|Bulletproofish										|8		|- Resist Bullets<br>- Bullet damage divided by 1.25
|Conductive											|10		|- Electro Touch
|Critter Hitter										|16		|- Always Crit
|Desecondive										|-32	|- Shrunken
|Dying												|-32	|- Poisoned
|Enfastened											|10		|- Fast
|Enstrongened										|16		|- Strength
|Even Shootier										|4		|- Accuracy<br>
|Gigantic											|100	|- Giant
|Invisibility Enjoyer								|100	|- Invisible
|Killer Throwerer									|32		|- Killer Thrower
|Lucky Duck											|7		|- Feelin' Lucky
|LyCANthrope										|32		|- Werewolf
|Ragestart											|-32	|- Enraged
|Regenerationist									|32		|- Regeneration
|Slothful											|-6		|- Slow
|Strong Immune System								|16		|- Stable System
|The Invincibility Gambit							|100	|- Invincible
|Thick Skin											|12		|- Resist Damage (Small)<br>- Damage divided by 1.25
|Thicker Skin										|24		|- Resist Damage (Medium)<br>- Damage divided by 1.5
|Thickest Skin										|36		|- Resist Damage (Large)<br>- Damage divided by 2
|Thickester Skin									|48		|- Numb to Pain<br>- Damage divided by 3
|Undying											|100	|- Resurrection
|Unlucky Duck										|-1		|- Feelin' Unlucky
####			C	General Issues
#####				C	Clone didn't keep SE
Elevator level transition
Clone Machine, Friend Phone and Loneliness Killer all lose Invisibility on moving to next level.
Trait list is fully intact
I think the trail is pointing to HireUnofficially, which spawns to party without party limit.
#####				C   Trait Upgrading
Test that this works properly
#####				C   Antidote
Left Gigantic intact at least, but verify there's not a limited list it can work on
#####				C   Toilet
Left Gigantic intact at least, but verify there's not a limited list it can work on
#####				C   Block UseItem Effects
Gigantizer if you're giant, e.g.
	This one in particular just wastes it. I'd like to prevent wasting the item.
#####				C   Opposite Effects
Shrinker if you're giant, e.g. Untested
####			C	Above the Laws
#####				C	Block Bribe Interaction
Might be automatic
####			C   Bulletproofish
Works I guess
#####				C	Block Equip Bulletproof Vest
Might be automatic
####			C   Conductive
New
####			C   Critter Hitter
Rename Literally Critler
####			C   Dying
Says Poisoned in SE, does that matter?
####			C   Enfastened
Works
####			C   Enstrongened
Works, but what's the cap?
####			C	Invisibility Enjoyer
E_ text in char sheet
####			C   Killer Throwerer
Works
####			C   Lucky Duck
Verify numerically
####			C   LyCANthrope
Doesn't transform
Just puts a number 2 above head
####			C   Popular
DW, you wanted to make a trait group anyway so just remove it
####			C   Ragestart
Might need to tell to attack neutrals, although this version is pretty interesting
####			C   Regenerationistest
E_RegenerateHealthFaster
Remove
####			C   Shrunk
Works
####			C   Slothful
Works, find caps
####			C   Stablemaster
Yeah I don't care to test this one
####			C   Invincibility Strategy
Sure yeah fine
####			C   Thick Skin
Test
####			C   Thicker Skin
Test
####			C   Thickest Skin
Seems to work
####			C   Unlucky Duck
Verify numerically
###			C	Check out PlayControl.Update
Has some debug tools that can be the basis for a bit of interesting content
###			C	OnAdded/OnRemoved Overrides
If you override these in an abstract class, you don't need to waste the space on the subclasses.