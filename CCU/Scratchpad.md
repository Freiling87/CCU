# Test Notes / Bugfixing

# Initial Load

[Info   :  CCU_Core] Awake: Method Call
[Error  :RogueLibsCore] System.Reflection.TargetInvocationException: Exception has been thrown by the target of an invocation. ---> System.FormatException: Index (zero based) must be greater than or equal to zero and less than the size of the argument list.
  at System.Text.StringBuilder.AppendFormatHelper (System.IFormatProvider provider, System.String format, System.ParamsArray args) [0x000ff] in <44afb4564e9347cf99a1865351ea8f4a>:0
  at System.String.FormatHelper (System.IFormatProvider provider, System.String format, System.ParamsArray args) [0x00023] in <44afb4564e9347cf99a1865351ea8f4a>:0
  at System.String.Format (System.String format, System.Object arg0) [0x00008] in <44afb4564e9347cf99a1865351ea8f4a>:0
  at CCU.Traits.AI.Thief_Pickpocket.Setup () [0x00006] in <f615a5d8460c4c3cabe21c07fa476ebe>:0
  at (wrapper managed-to-native) System.Reflection.MonoMethod.InternalInvoke(System.Reflection.MonoMethod,object,object[],System.Exception&)
  at System.Reflection.MonoMethod.Invoke (System.Object obj, System.Reflection.BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00032] in <44afb4564e9347cf99a1865351ea8f4a>:0
   --- End of inner exception stack trace ---
  at System.Reflection.MonoMethod.Invoke (System.Object obj, System.Reflection.BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x0004b] in <44afb4564e9347cf99a1865351ea8f4a>:0
  at System.Reflection.MethodBase.Invoke (System.Object obj, System.Object[] parameters) [0x00000] in <44afb4564e9347cf99a1865351ea8f4a>:0
  at RogueLibsCore.RogueLibs.LoadFromAssembly () [0x000ef] in <83ac4c56ae974fd1ab7e1062800cad5c>:0


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
Works

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