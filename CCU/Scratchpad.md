# Run error logs

Vendor traits are getting a nullref  but not sure how deep

# Notes / Bugfixing

## Chunk Editor Shortcuts

Ctrl + O load shows all menus but doesn't load anything
F9 load works but shows chunk selection menu

Ctrl + S works fine
F5 Save works but shows menus

Ctrl Numkeys

Q,E 
	Default to North as from-direction if None

Arrows 
	No effect

Ctrl A
	Select all works
	Toggle off doesn't

ADD:
	ALT trail for overhead menus
	Maybe [1][2],etc. indicators on menu buttons as hotkey hints

## Appearance Traits

Test transpiler method, simply replace if it doesn't work. Use transpilers later.

# Implementation

TODO: Move any behaviors linked to special abilities (Bite, Cannibalize, Sticky Glove) to link to the special ability

## Traits

- CCP limit adjustment for CCU Traits
	- CharacterCreation.traitCount in CreatePointTallyText();
	- Line 532, need to make a custom list of this.traitsChosen where they are not on CCU trait list

### Thief Hire

### Thief Honorable
Worked for pickpocket

### Thief Pickpocket
Stopped working when I switched it to StickyGlove check. 

### Thief Vendor
- Special Inv filling: 
  - InvDatabase.FillSpecialInv
  - InvDatabase.AddRandItem
    - @1246: base.CompareTag("SpecialInvDatabase")
      - I am hoping this is taken care of automatically. Testing will show.
  - Keep an eye out for agent name checks, one of these will need to be patched
  - InvDatabase.rnd.RandomSelect(rName, "Items")
- Agent Loadouts
  - InvDatabase.FillAgent

Not detecting trait: 
	[Info   : Unity Log] ADDRELHATE: Custom (1140) (Agent) - Playerr (Agent)
	[Info   :  CCU_Core] HasTraitFromList: Method Call
	[Info   :  CCU_Core] HasTraitFromList: Method Call
	[Info   :  CCU_Core] HasTraitFromList: Method Call