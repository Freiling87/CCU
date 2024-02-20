
##			Bug Archive
###			H	Concealed Carry Sitting
CL https://discord.com/channels/187414758536773632/1003391847902740561/1126306577008316508
	Concealed carrier still doesn't work with sitting NPCs until something briefly updates their behavior (like a noise making them stand up and look, then sit back down).
Attempted:
	AgentHitbox
		.UpdateAnim		Prefix
	GoalSleepReal
		.Activate		Prefix
		.Process		Prefix
	GoalSitReal
		.Activate		Prefix
###			H	Vanilla Appearance Randomization
The Character Select screen says you can press Ctrl to randomize appearance. This is broken, for obvious reasons. 
###		 H   Scene Setter Breaks when hostiles visible
https://discord.com/channels/187414758536773632/991046848536006678/1122856007706607656
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
###			H	Editor String Copying
This is a vanilla bug so I'm shelving for now.
I think it's JUST containers → Signs now
Select Vanilla or Modded Container, choose contained item. Then switch to Draw, and select Altar. Item text will be in EVS1. 
I think I found where this bug is from: 
	P_LevelEditor_ObjectButtonType
Located bug:
	LevelEditor.SetExtraVarString() last branch 