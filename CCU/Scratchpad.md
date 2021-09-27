# Test Notes / Bugfixing

# Initial Load

[Info   :   BepInEx] Loading [Custom Content Utilities 0.1.0]
[Error  : Unity Log] NullReferenceException: Object reference not set to an instance of an object
Stack trace:
CCU.Core.LogMethodCall (System.String callerName) (at <3aad7d0a933f486b8d7bc36adea33e63>:0)
CCU.Core.Awake () (at <3aad7d0a933f486b8d7bc36adea33e63>:0)
UnityEngine.GameObject:AddComponent(Type)
BepInEx.Bootstrap.Chainloader:Start()
UnityEngine.Application:.cctor()
Rewired.InputManager_Base:Awake()

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

## Faction Traits

Test

## Appearance Traits

Test transpiler method, simply replace if it doesn't work. Use transpilers later.

# Implementation

## Traits

- CCP limit adjustment for CCU Traits
	- CharacterCreation.traitCount in CreatePointTallyText();
	- Line 532, need to make a custom list of this.traitsChosen where they are not on CCU trait list