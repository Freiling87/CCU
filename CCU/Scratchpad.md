# Run error logs

- Hired NPC. Once hired, they couldn't move and framerate skipped

[Info   :  CCU_Core] DetermineButtons_Prefix: Method Call
[Info   :  CCU_Core] HasTraitFromList: Method Call
[Info   :  CCU_Core] GetOnlyTraitFromList: Method Call
[Info   :  CCU_Core] HasTraitFromList: Method Call
[Info   :  CCU_Core] DetermineButtons_Prefix: Vendor
[Info   :  CCU_Core] HasTraitFromList: Method Call
[Info   :  CCU_Core] DetermineButtons_Prefix: Hire
[Info   :  CCU_Core] DetermineButtons_Prefix: Hire Initial
[Info   :  CCU_Core] DetermineButtons_Prefix: 0
[Info   :  CCU_Core] DetermineButtons_Prefix: 1
[Info   :  CCU_Core] DetermineButtons_Prefix: 2
[Info   :  CCU_Core] DetermineButtons_Prefix: 3
[Info   :  CCU_Core] DetermineButtons_Prefix: 4
[Info   :  CCU_Core] DetermineButtons_Prefix: 5
[Info   :  CCU_Core] DetermineButtons_Prefix: 6
[Info   :  CCU_Core] DetermineButtons_Prefix: 7
[Info   :  CCU_Core] DetermineButtons_Prefix: 8
[Error  : Unity Log] AI Update Error: Custom (1126) (Agent)
[Info   :  CCU_Core] DetermineButtons_Prefix: Method Call
[Info   :  CCU_Core] HasTraitFromList: Method Call
[Info   :  CCU_Core] GetOnlyTraitFromList: Method Call
[Info   :  CCU_Core] HasTraitFromList: Method Call
[Info   :  CCU_Core] DetermineButtons_Prefix: Vendor
[Info   :  CCU_Core] HasTraitFromList: Method Call
[Info   :  CCU_Core] DetermineButtons_Prefix: Hire
[Info   :  CCU_Core] DetermineButtons_Prefix: Hire Order
[Error  : Unity Log] AI Update Error: Custom (1126) (Agent)
[Error  : Unity Log] AI Update Error: Custom (1126) (Agent)
[Error  : Unity Log] AI Update Error: Custom (1126) (Agent)
[Error  : Unity Log] AI Update Error: Custom (1126) (Agent)
[Error  : Unity Log] AI Update Error: Custom (1126) (Agent)
[Error  : Unity Log] AI Update Error: Custom (1126) (Agent)
[Error  : Unity Log] AI Update Error: Custom (1126) (Agent)
[Error  : Unity Log] AI Update Error: Custom (1126) (Agent)
[Error  : Unity Log] AI Update Error: Custom (1126) (Agent)
[Error  : Unity Log] AI Update Error: Custom (1126) (Agent) ...

- Shop worked, but was empty

- LOS abilities all work 👍

---
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


---
# Implementation

## Traits

### Trait hiding

#### Character Creation, Player Edition (CharacterCreation)

Possible future bug: If you create a character in DE, and edit/resave them in PE (hidden traits won't be visible), will it remove or keep their hidden traits?

#### Character Select (CharacterSelect)

Facial Hair
	Doesn't Work
Hire
	Works
Hire Specials (Not on list)
	Doesn't work
Trait Trigger
	Doesn't Work
Vendor
	Works

#### Character Sheet (CharacterSheet)

Attempted

### Thief Hire

### Thief Honorable
Worked for pickpocket

### Thief Vendor
- Special Inv filling: 
  - InvDatabase.FillSpecialInv
  - InvDatabase.AddRandItem
    - @1246: base.CompareTag("SpecialInvDatabase")
      - I am hoping this is taken care of automatically. Testing will show.
  - Keep an eye out for agent name checks, one of these will need to be patched
  - InvDatabase.rnd.RandomSelect(rName, "Items")

## Agent Loadouts
  - InvDatabase.FillAgent