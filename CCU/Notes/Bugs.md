##			Bugs
Except crickets. Crickets are fine.
###			H	Broken Namespaces
Requested help from testers to verify if everything is loading properly
###			C	Language broke AGAIN
Can't speak to gorilla, have Speaks High Goryllian
###			C	Exempt Scene Setter NPCs from possession
Spawns shapeshifter on death
###			C	Grab Everything
Doesn't pick up safe combo
Do we want them to though?
Maybe a Grab Special would be appropriate
###			C	Shops are empty
Per CL, but this was with a test version.
###			C	Dual Alignment freeze
https://discord.com/channels/187414758536773632/1003391847902740561/1140063406997651469
BT's alignment methods should handle this... test it
###			C	Electronic blocks status effects
Giant robot! Giant robot! Giant robot!
###			C	Indestructible Corpse Explosions
GenEric: https://discord.com/channels/187414758536773632/991046848536006678/1138250512752459857
If a corpse is indestructible and has explode on death, they will explode repeatedly.
This is cancelled by Electronic.
###			C	Prison interrupts Dead SS
https://discord.com/channels/187414758536773632/991046848536006678/1137189897808117860
###			C	Undercant strings
Deleted WIPs, not much to save
###			T	Friend Phone Clone
Appearance patch is ready to go
###			C	Cyber Nuke by Proxy
This is POSSIBLY an old Roguelibs bug.
Hired Cyber Nuke doesn't show option.
"HacksBlowUpObjects"
First detect who the actual interactingAgent is when using a hacker hire
###			C	Editor String Copying
I think it's JUST containers → Signs now
Select Vanilla or Modded Container, choose contained item. Then switch to Draw, and select Altar. Item text will be in EVS1. 
I think I found where this bug is from: 
	P_LevelEditor_ObjectButtonType
Located bug:
	LevelEditor.SetExtraVarString() last branch
###			C	Z-Infected Appearance
https://discord.com/channels/187414758536773632/1003391847902740561/1127403137100161074
	Pretty weird one, if you mix randomized appearance with Z Infected then the resulting zombies will not just re-randomize but they'll also all become identical. Like if you randomize bodies and have a mix of hacker, clerk, bartender and goon then killing a crowd and raising them as zombies will make them all come back in clerk uniforms or whatever. It's not consistent so the thing they turn into is random each time (it's not just like it's reverting to the neutral pre-randomized appearance, alphabetical order or anything like that), but once the "zombie form" is randomized it seems like every member of the class will turn into that on death.
	And yes, I did somehow pivot from updating old NPCs to messing around with new zombie plague victim designs. This is why the Vendor Variety update and SoV Remaster are never getting released, it's physically impossible to spend more than an hour in the character editor with this mod without starting a completely new project.
###			C	Various Shop Inventory Reports
I think these have been addressed but verify:
https://discord.com/channels/187414758536773632/1003391847902740561/1126283884959629312
###			C	Deaf NPCs speak Polyglottal Gibberish
Good god
https://discord.com/channels/187414758536773632/991046848536006678/1127299576366383165
###			C	Random Teleport to Entry Elevator
Make a distance buffer from entry to minimise the cheesening
###			C	Gun Nut traits only apply on start??
I thought this was fixedddd
###			C	Concealed Carry Sitting
CL https://discord.com/channels/187414758536773632/1003391847902740561/1126306577008316508
	Concealed carrier still doesn't work with sitting NPCs until something briefly updates their behavior (like a noise making them stand up and look, then sit back down).
###			C	Vendor Cart Ransack not making noise
I mean Agent noise, not the sound effect
###			C	Language Teacher bugs
Maxior - https://discord.com/channels/187414758536773632/1003391847902740561/1125451549162881074
Language learning is broken. Asking an NPC to teach you will kick you out of the dialogue and when you talk with them again, learning the language(s) will be the only option. Every other option is gone, including shops and hire options.
###			C	Undercant Speakers lacking Gibberish Dialogue
For mismatched language
###			C	Upgrading traits doesnt' remove base trait
Melee Maniac + in upgrade machine, kept both traits
I guess do an OnAdded, frame-delayed, from T_CCU. This would be a good contribution to Roguelibs as well, if you can find out how.
###			C	Ammo Traits offered on same level
Ammo Auteur and Artiste
###			C	Gate Language for Home Base
Allow interact with all
Chaos at Home Base is through Werewolf interaction, for example
##			Bug Archive
###		 H   Scene Setter Breakage
https://discord.com/channels/187414758536773632/991046848536006678/1122856007706607656
Spawning with visible hostiles is confirmed to interrupt it inconsistently
Not able to replicate
###		 H   Merchant Stock Price issues
Two symptoms of the same issue. Abbysssal pointed out the solution, but this is not an important enough bug to delay release for.

- Gas Mask is consistently $30 too expensive by formula.
  - Abby found the issue:
		=== MoneyCostDurabilityAmmo_Logging: (GasMask)
		MoneyAmt:	  70
		MoneyAmtFloat: 53.33334
	This example for low durability, net price should have been 23
- Melee Weapons under 100 are underpriced.
	Item		Wrong	Right	Ratio1	Ratio2
	Knife		$5		$10		0.50	2.000
	BaseballBat $10		$17		0.58	1.700
	Crowbar		$8		$13		0.62	1.625
	Wrench		$8		$13		0.62	1.625
  - Abby found the issue:
		=== MoneyCostDurabilityAmmo_Logging: (Wrench)
		MoneyAmt:	  13
		MoneyAmtFloat: 8.58
####			C	Low-Qty Melee too Cheap
Melee weapons with low Durability are cheaper than they should be.
Below, Baseball bat at 66 Qty (200/3) is $10 but should be $17.


###		 H   Normal (EOD) in Shop Inventory
https://discord.com/channels/187414758536773632/1003391847902740561/1121962031235477524
No clue how to replicate this.
###		 H   Scene Setters Activate Chunk
Scene setters may cause events that the designer wished the player to witness to play out too early for them to see. Normally the trigger for activating the chunk is by player proximity.
Unfortunately, I think this fix will be a bit too complex for this project.
###		 H   Hidden Bomb Spawned in Vent off map
https://discord.com/channels/187414758536773632/187414758536773632/1093294336109727845
You might be able to replace the != Wastebasket string with a special validity detector.
So far I've been unable to replicate this bug. Took place on a standard Park map that did not lack other containers for the bomb.
###		 H   Equipment noise spam
Still occuring, even with vanillas.
For now, just disabling the noise if they switch to Fist.
###		 H   CCU Trait section in Load Character screen
Shows outdated traits when choosing
Don't really care yet.
###		 H   Clone changing appearance
Shelving this because I simply don't care enough to fix it yet. This is super-niche.
Buddy Cop Loneliness killer
The one that showed up on level 2 was identical to me
The one that came from level 1 rerolled appearance
###		 H   Unidentified Eyes Strings error message
Between Accessory & Body Type, current logs are uninterrupted
Shelved - no apparent effect for this error.
###		 H   Unidentified Loadout error message
Consistently, the Crepe Heavy has this error.
	[Info   : Unity Log] SETUPMORE4_7
	[Debug  :CCU_LoadoutTools] Custom Loadout: Custom(Crepe Heavy)
	[Error  : Unity Log] Couldn't do ChooseWeapon etc. for agent Custom (1132) (Agent)

This error is from LoadLevel.SetupMore.
But since there doesn't appear to be any actual repercussion on gameplay, I'm shelving it until it is an issue.

I want to say I later fixed this. Verify if the messages still come up.
###		 H   Jukebox Hacks
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
###		 H   $0 in container
https://discord.com/channels/187414758536773632/1003391847902740561/1007975536607383574
Maxior - Shelf w/ $0 as container, but not Trash Can
So far, unable to replicate
###		 √   Partisan Concealed Carry
Winnie - https://discord.com/channels/187414758536773632/646853913273696257/1117896939837587517
Issue was Loadout Money without a Loader trait.