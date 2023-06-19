##			How to read this
If you wandered in here out of curiosity, this is my working notes file, and the place to find upcoming features. It's a markdown file, but best viewed in raw form with the Markdown Editor VS extension since a lot of its organization has to do with text characters' alignment.

Listed in order of Parent tier summary symbol priority:
	C, T = Code this, Test this
	H = Hold, usually pending resolution of a separate or grouped issue
	√ = Fully implemented feature or group of features
##			Before release
Notify Guoxin of finalized version so he can translate
##			v. 1.1.1 Changelog
- Compatibility
  - Verified compatibility with SOR v98 
- Bugfixes & Tweaks
  - Character Select no longer freezes when clicking an empty slot.
  - Vendor Cart "Operating" sound effect now plays for the correct duration.
  - Fixed Investigateable text showing hidden string ("investigateable-message:::").
  - Shops with Shoddy/Shiddy Goods no longer set Tasers to -1 quantity.
  - Ammo-related traits now give free ammo to NPCs with items added in the editor, rather than just items added through the Loadout system.
  - Indestructible now compatible with vanilla (Meat) Gib Type.
  - Permanent Hires no longer occupy a hire slot after death.
  - Tubes are now only valid containers for Hidden Bombs if they are within a Computer or Power Box's shutdown range.
  - Melee Maniac + now improves melee attack speed by 48.5% instead of 50%. This prevents the animation from clipping through targets.
- Feature additions
  - Items
    - Rubber Bullets Mod: Knocks out targets at 10% HP, kills at -10%. Pacifists can use Rubber Bullet guns. Not balanced!
  - Designer Traits
    - Appearance
      - Dynamic Player Appearance: Allows appearance variation when using the class as a player. Currently, appearance is only rerolled on starting a new run, but I plan to add a little more control over this in the future.
      - Fleshy Follicles: Matches hair color to skin color.
    - Behavior
      - Accident-Prone: Extended behavior to walk into Killer Plants & Laser Emitters
    - Combat
      - Toughness traits: Modify NPC willingness to fight tough odds.
      - Mag Dumper: Agent uses rapid fire for longer.
      - Melee skill traits: Modify frequency of attacks in combat.
      - Gun skill traits: Modify frequency of attacks in combat.
    - Passive
      - Crusty: Will now use Alarm Button when fleeing from combat.
      - Indomitable: Immune to mind control
    - Senses
      - Keen Ears traits: Various traits that determine how sensitive the NPC is to sound, and how they react to it.
  - Player Traits
    - Gun Nut: Agent automatically applies a mod to all eligible guns.
      - Accuracy Modder
      - Ammo Stocker
      - Rate of Fire Modder
      - Rubber Bulleteer
      - Silencerist
    - Knockback Peon: Reduces knockback, making followup attacks easier.
##			Bugs
Except crickets. Crickets are fine.
###				C	Syringe AV
IIRC someone said this stopped working, double-check it
###				C	Zero Money
	Maxior - The 0 money glitch is still here
	Money put in the custom containers like shelves will still only give 0 money
	Except garbage cans
###				C	Redo Pre-Build events 
Pull RL FROM plugins rather than copying TO it.
###				C	Language
Says "I can't speak English" but the other NPC's language is Goryllian (tried interact)
###				C	Random Teleport gives Yellow Name
New
###				C	ExtraVarString copying
It's back! Yay. It's back.
###				C	Random Teleport
	CL - Spawning idle
It's just the legacy entry I think. 
###				C	Ammo Cap
CL - Having a gun already when you pick one of the traits still doesn't update the max ammo
###				T	Fearless
https://discord.com/channels/187414758536773632/1003391847902740561/1087385752754716804
Added prefixes to AssessBattle & AssessFlee
Pending confirmation from Maxior
##			Bug Archive
###				H	Scene Setters Activate Chunk
Scene setters may cause events that the designer wished the player to witness to play out too early for them to see. Normally the trigger for activating the chunk is by player proximity.
Unfortunately, I think this fix will be a bit too complex for this project.
###				H	Hidden Bomb Spawned in Vent off map
https://discord.com/channels/187414758536773632/187414758536773632/1093294336109727845
You might be able to replace the != Wastebasket string with a special validity detector.
So far I've been unable to replicate this bug. Took place on a standard Park map that did not lack other containers for the bomb.
###				H	Equipment noise spam
Still occuring, even with vanillas.
For now, just disabling the noise if they switch to Fist.
###				H	Dismissed Perm Hire prevents Temp Hire
Can't temp hire after dismissing perm hire. 
This is MINOR, I don't care.
###				H	CCU Trait section in Load Character screen
Shows outdated traits when choosing
Don't really care yet.
###				H	Johnny Stabbs
Hold - Unable to replicate

Has Fac Blahd Aligned (replaced by Legacy)

On attempt Load from stored chars list, get error and no load:

	[Debug  :CCU_Legacy] Caught Legacy trait: Faction_Blahd_Aligned
	[Error  : Unity Log] NullReferenceException: Object reference not set to an instance of an object
	Stack trace:
	MenuGUI.TextChangedCharacterDescription () (at <3d326b8e9c744e5faf3a706691682c89>:0)
	UnityEngine.Events.InvokableCall.Invoke () (at <a5d0703505154901897ebf80e8784beb>:0)
	UnityEngine.Events.UnityEvent`1[T0].Invoke (T0 arg0) (at <a5d0703505154901897ebf80e8784beb>:0)
	UnityEngine.UI.InputField.SendOnValueChanged () (at <d5bb9c19c2a7429db6c6658c41074b11>:0)
	UnityEngine.UI.InputField.SetText (System.String value, System.Boolean sendCallback) (at <d5bb9c19c2a7429db6c6658c41074b11>:0)
	UnityEngine.UI.InputField.set_text (System.String value) (at <d5bb9c19c2a7429db6c6658c41074b11>:0)
	CharacterCreation.LoadCharacter2 (System.String characterName, System.Boolean secondTry, System.Boolean foundFile, System.Object mySaveObject) (at <3d326b8e9c744e5faf3a706691682c89>:0) 
	...
###				H	Clone changing appearance
Shelving this because I simply don't care enough to fix it yet. This is super-niche.
Buddy Cop Loneliness killer
The one that showed up on level 2 was identical to me
The one that came from level 1 rerolled appearance
###				H	Unidentified Eyes Strings error message
Between Accessory & Body Type, current logs are uninterrupted
Shelved - no apparent effect for this error.
###				H	Unidentified Loadout error message
Consistently, the Crepe Heavy has this error.
	[Info   : Unity Log] SETUPMORE4_7
	[Debug  :CCU_LoadoutTools] Custom Loadout: Custom(Crepe Heavy)
	[Error  : Unity Log] Couldn't do ChooseWeapon etc. for agent Custom (1132) (Agent)

This error is from LoadLevel.SetupMore.
But since there doesn't appear to be any actual repercussion on gameplay, I'm shelving it until it is an issue.
###				H	Upper Crusty Busybody 
Polices pickup and destruction of 0-Owner ID items and objects, and no others.
After reloading, was unable to replicate this.
###				H	Jukebox Hacks
Possibly RogueLibs, wait for confirmation.

Mambo:
	[Error  : Unity Log] NullReferenceException: Object reference not set to an instance of an object
	Stack trace:
	Turntables.PlayBadMusic () (at <7fd7dd1709b64c98aabccc051a37ae28>:0)
	RogueLibsCore.VanillaInteractions+<>c.<Patch_Turntables>b__68_2 (RogueLibsCore.InteractionModel`1[T] m) (at <ac95cf0d3a8543748b4a19536e8724e6>:0)
	RogueLibsCore.SimpleInteraction`1[T].OnPressed () (at <ac95cf0d3a8543748b4a19536e8724e6>:0)
	RogueLibsCore.InteractionModel.OnPressedButton2 (System.String buttonName) (at <ac95cf0d3a8543748b4a19536e8724e6>:0)
	RogueLibsCore.InteractionModel.OnPressedButton (System.String buttonName) (at <ac95cf0d3a8543748b4a19536e8724e6>:0)
	RogueLibsCore.RogueLibsPlugin.PressedButtonHook (PlayfieldObject __instance, System.String buttonText) (at <ac95cf0d3a8543748b4a19536e8724e6>:0)
	Turntables.PressedButton (System.String buttonText, System.Int32 buttonPrice) (at <7fd7dd1709b64c98aabccc051a37ae28>:0)
	ObjectReal.PressedButton (System.String buttonText) (at <7fd7dd1709b64c98aabccc051a37ae28>:0)
	WorldSpaceGUI.PressedButton (System.Int32 buttonNum) (at <7fd7dd1709b64c98aabccc051a37ae28>:0)
	ButtonHelper.PushButton () (at <7fd7dd1709b64c98aabccc051a37ae28>:0) 
	ButtonHelper.DoUpdate (System.Boolean onlySetImages) (at <7fd7dd1709b64c98aabccc051a37ae28>:0)
	ButtonHelper.Update () (at <7fd7dd1709b64c98aabccc051a37ae28>:0)
Bladder:  
	[Error  : Unity Log] NullReferenceException: Object reference not set to an instance of an object
	Stack trace:
	Turntables+<BadMusicPlayTime>d__31.MoveNext () (at <7fd7dd1709b64c98aabccc051a37ae28>:0)
	UnityEngine.SetupCoroutine.InvokeMoveNext (System.Collections.IEnumerator enumerator, System.IntPtr returnValueAddress) (at <a5d0703505154901897ebf80e8784beb>:0)
###				H	$0 in container
https://discord.com/channels/187414758536773632/1003391847902740561/1007975536607383574
Maxior - Shelf w/ $0 as container, but not Trash Can
So far, unable to replicate
###				√	NPC Free Ammo
Complete. Tested items added via Loadout as well as via editor.
###				√	Partisan Concealed Carry
Winnie - https://discord.com/channels/187414758536773632/646853913273696257/1117896939837587517
Issue was Loadout Money without a Loader trait.
###				√	Error on select empty slot
####				C	Issue
On selecting empty character slot:
	[Error  : Unity Log] NullReferenceException: Object reference not set to an instance of an object
	Stack trace:
	AgentHitbox.SetupBodyStrings () (at <3d326b8e9c744e5faf3a706691682c89>:0)
	CCU.Traits.App.AppearanceTools.RollEyeType (AgentHitbox agentHitbox) (at <6991a84d0f8f42f4bcade340f697d8d1>:0)
	CCU.Traits.App.AppearanceTools.SetupAppearance (AgentHitbox agentHitbox) (at <6991a84d0f8f42f4bcade340f697d8d1>:0)
	AgentHitbox.SetupFeatures () (at <3d326b8e9c744e5faf3a706691682c89>:0)
	CharacterCreation.ShowSlotAgent () (at <3d326b8e9c744e5faf3a706691682c89>:0)
	CharacterCreation.OpenCharacterCreation (System.Boolean resetting) (at <3d326b8e9c744e5faf3a706691682c89>:0)
	CharacterCreation.OpenCharacterCreation () (at <3d326b8e9c744e5faf3a706691682c89>:0)
	MainGUI.ShowCharacterCreation (PlayfieldObject otherObject, Agent myAgent, System.Int32 slotNumber) (at <3d326b8e9c744e5faf3a706691682c89>:0)
	InvSlot.ClickInvBox (UnityEngine.EventSystems.PointerEventData data, System.Boolean rightClicked) (at <3d326b8e9c744e5faf3a706691682c89>:0)
	InvSlot.OnPointerDown (UnityEngine.EventSystems.PointerEventData data) (at <3d326b8e9c744e5faf3a706691682c89>:0)
	UnityEngine.EventSystems.ExecuteEvents.Execute (UnityEngine.EventSystems.IPointerDownHandler handler, UnityEngine.EventSystems.BaseEventData eventData) (at <d5bb9c19c2a7429db6c6658c41074b11>:0)
	UnityEngine.EventSystems.ExecuteEvents.Execute[T] (UnityEngine.GameObject target, UnityEngine.EventSystems.BaseEventData eventData, UnityEngine.EventSystems.ExecuteEvents+EventFunction`1[T1] functor) (at <d5bb9c19c2a7429db6c6658c41074b11>:0)
	UnityEngine.EventSystems.ExecuteEvents:ExecuteHierarchy(GameObject, BaseEventData, EventFunction`1)
	Rewired.Integration.UnityUI.RewiredStandaloneInputModule:ProcessMousePress(MouseButtonEventData)
	Rewired.Integration.UnityUI.RewiredStandaloneInputModule:ProcessMouseEvent(Int32, Int32)
	Rewired.Integration.UnityUI.RewiredStandaloneInputModule:ProcessMouseEvents()
	Rewired.Integration.UnityUI.RewiredStandaloneInputModule:Process()
	UnityEngine.EventSystems.EventSystem:Update()
####				C	Fix
Was rolling appearance without checking for CustomCharacterData. Added early return.
##			Features
###			C	Vanillize Item Qty on load
Editor-added items give full ammo, should be semi-random and scaled to NPC level as Loadout items are
###			C	Hide Appearance Traits Button
Toggle trait visibility for:
- Trait Selection List (middle)
- Selected Trait List (right)
###			C	Profile scrollbar
To cancel illegibly tiny text on right panel
###			C	CharacterSelect.CharacterLoaded
This is a bool array that might be easier than checking for nulls on charsavedata. 
Slots 32-48 are custom characters, and it's true