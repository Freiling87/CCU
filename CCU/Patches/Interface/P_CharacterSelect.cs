using BepInEx.Logging;
using CCU.Traits;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace CCU.Patches.Interface
{
	[HarmonyPatch(declaringType: typeof(CharacterSelect))]
	public static class P_CharacterSelect
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(CharacterSelect.SetupSlotAgent), argumentTypes: new Type[3] { typeof(int), typeof(string), typeof(Agent) })]
		public static bool SetupSlotAgent_Prefix(int n, string mySlotAgentType, Agent curPlayer, CharacterSelect __instance, bool ___removingCustomCharacter)
		{
			if (GC.sessionDataBig.CShairType[0, 0] != null && GC.sessionDataBig.CShairType[0, 0] != "" && !__instance.setSlotAgent[n] && n != 48 && !__instance.resettingSlotAgent)
			{
				__instance.setSlotAgent[n] = true;
				__instance.dummyAgent.agentHitboxScript.canHaveFacialHair = false;
				__instance.dummyAgent.inventory.DestroyAllItems();
				__instance.dummyAgent.inventory.startingHeadPiece = "";
				__instance.dummyAgent.inventory.equippedSpecialAbility = null;
				__instance.dummyAgent.inventory.defaultArmorHead = null;
				__instance.dummyAgent.inventory.equippedArmor = null;
				__instance.dummyAgent.inventory.equippedArmorHead = null;
				__instance.dummyAgent.customCharacterData = null;
				__instance.dummyAgent.hasSpecialAbilityArm2 = false;
				__instance.dummyAgent.zombified = false;
				__instance.dummyAgent.statusEffects.TraitList.Clear();

				if (n >= 32 && n < 48)
				{
					__instance.dummyAgent.agentName = "Custom";
					__instance.dummyAgent.bigQuest = "";
					__instance.dummyAgent.customCharacterData = GC.sessionDataBig.customCharacterSlotsDetails[n - 32];
					__instance.dummyAgent.agentHitboxScript.GetColorFromString(__instance.dummyAgent.customCharacterData.bodyColorName, "Body");
					__instance.dummyAgent.agentHitboxScript.GetColorFromString(__instance.dummyAgent.customCharacterData.hairColorName, "Hair");
					__instance.dummyAgent.agentHitboxScript.GetColorFromString(__instance.dummyAgent.customCharacterData.skinColorName, "Skin");
					__instance.dummyAgent.agentHitboxScript.GetColorFromString(__instance.dummyAgent.customCharacterData.legsColorName, "Legs");
					__instance.dummyAgent.agentHitboxScript.GetColorFromString(__instance.dummyAgent.customCharacterData.eyesColorName, "Eyes");
					__instance.dummyAgent.agentHitboxScript.hairType = __instance.dummyAgent.customCharacterData.hairType;
					__instance.dummyAgent.agentHitboxScript.facialHairType = __instance.dummyAgent.customCharacterData.facialHair;

					if (__instance.dummyAgent.customCharacterData.startingHeadPiece != "" && __instance.dummyAgent.customCharacterData.startingHeadPiece != null)
						__instance.dummyAgent.agentInvDatabase.AddStartingHeadPiece(__instance.dummyAgent.customCharacterData.startingHeadPiece);
				}
				else
					__instance.dummyAgent.agentName = mySlotAgentType;

				__instance.dummyAgent.SetupAgentStats("");
				__instance.slotAgent[n].gameObject.SetActive(true);

				if (mySlotAgentType == "Cop2")
					mySlotAgentType = "Cop";
				if (mySlotAgentType == "Cop" && GC.challenges.Contains("SuperSpecialCharacters") && GC.unlocks.isBigQuestCompleted(mySlotAgentType))
					mySlotAgentType = "Cop2";
				if (mySlotAgentType == "Guard2")
					mySlotAgentType = "Guard";
				if (mySlotAgentType == "Guard" && GC.challenges.Contains("SuperSpecialCharacters") && GC.unlocks.isBigQuestCompleted(mySlotAgentType))
					mySlotAgentType = "Guard2";
				if (mySlotAgentType == "UpperCruster")
					mySlotAgentType = "Hobo";
				if (mySlotAgentType == "Hobo" && GC.challenges.Contains("SuperSpecialCharacters") && GC.unlocks.isBigQuestCompleted(mySlotAgentType))
					mySlotAgentType = "UpperCruster";

				__instance.slotAgent[n].transform.parent.GetComponent<InvSlot>().characterSelectType = mySlotAgentType;

				if (__instance.dummyAgent.agentName == "Custom")
				{
					Vector2 sizeDelta;

					if (__instance.dummyAgent.customCharacterData.bodyType.Contains("G_"))
					{
						__instance.slotAgent[n].transform.Find("Body").GetComponent<Image>().sprite = __instance.gr.bodyGDic[__instance.dummyAgent.customCharacterData.bodyType + "S"];
						sizeDelta = new Vector2(__instance.gr.bodyGDic[__instance.dummyAgent.customCharacterData.bodyType + "S"].bounds.size.x * 100f, __instance.gr.bodyGDic[__instance.dummyAgent.customCharacterData.bodyType + "S"].bounds.size.y * 100f);
					}
					else
					{
						__instance.slotAgent[n].transform.Find("Body").GetComponent<Image>().sprite = __instance.gr.bodyDic[__instance.dummyAgent.customCharacterData.bodyType + "S"];
						sizeDelta = new Vector2(__instance.gr.bodyDic[__instance.dummyAgent.customCharacterData.bodyType + "S"].bounds.size.x * 100f, __instance.gr.bodyDic[__instance.dummyAgent.customCharacterData.bodyType + "S"].bounds.size.y * 100f);
					}

					__instance.slotAgent[n].transform.transform.Find("Body").GetComponent<RectTransform>().sizeDelta = sizeDelta;
				}
				else
				{
					__instance.slotAgent[n].transform.Find("Body").GetComponent<Image>().sprite = __instance.gr.bodyDic[__instance.dummyAgent.agentName + "S"];
					Vector2 sizeDelta = new Vector2(__instance.gr.bodyDic[__instance.dummyAgent.agentName + "S"].bounds.size.x * 100f, __instance.gr.bodyDic[__instance.dummyAgent.agentName + "S"].bounds.size.y * 100f);
					__instance.slotAgent[n].transform.transform.Find("Body").GetComponent<RectTransform>().sizeDelta = sizeDelta;
				}

				if (__instance.dummyAgent.agentName == "Custom")
					__instance.slotAgent[n].transform.Find("Body").GetComponent<Image>().color = __instance.dummyAgent.agentHitboxScript.bodyColor;
				else if (mySlotAgentType == "Wrestler" || mySlotAgentType == "ShapeShifter")
					__instance.slotAgent[n].transform.Find("Body").GetComponent<Image>().color = __instance.skinColor32[curPlayer.isPlayer - 1, n];
				else
					__instance.slotAgent[n].transform.Find("Body").GetComponent<Image>().color = Color.white;

				if (__instance.dummyAgent.agentName == "Custom")
					__instance.slotAgent[n].transform.Find("Head").transform.Find("Head").GetComponent<Image>().color = __instance.dummyAgent.agentHitboxScript.skinColor;
				else
					__instance.slotAgent[n].transform.Find("Head").transform.Find("Head").GetComponent<Image>().color = __instance.skinColor32[curPlayer.isPlayer - 1, n];

				string text = "";

				if (__instance.dummyAgent.agentName == "Custom")
					text = __instance.dummyAgent.agentHitboxScript.hairType;
				else
					text = __instance.hairType[curPlayer.isPlayer - 1, n];

				if (__instance.dummyAgent.agentName == "Slavemaster")
					__instance.slotAgent[n].transform.Find("Head").transform.Find("Hair").GetComponent<Image>().sprite = __instance.gr.hairDic["SlavemasterMaskSpecialS"];
				else
					__instance.slotAgent[n].transform.Find("Head").transform.Find("Hair").GetComponent<Image>().sprite = __instance.gr.hairDic[text + "S"];

				bool flag = false;
				bool flag2 = false;

				foreach (RandomElement randomElement in GC.rnd.randomListTableStatic["NotHairStyles"].elementList)
					if (randomElement.rName == __instance.hairType[curPlayer.isPlayer - 1, n])
					{
						__instance.slotAgent[n].transform.Find("Head").transform.Find("Hair").GetComponent<Image>().color = AgentHitbox.white;

						if (__instance.dummyAgent.agentName != "Custom")
							flag = true;

						if (randomElement.rName != "Hoodie" && randomElement.rName != "AssassinMask" && randomElement.rName != "SlavemasterMask")
						{
							flag2 = true;
							break;
						}

						break;
					}

				if (!flag)
				{
					if (__instance.dummyAgent.agentName == "Custom")
						__instance.slotAgent[n].transform.Find("Head").transform.Find("Hair").GetComponent<Image>().color = __instance.dummyAgent.agentHitboxScript.hairColor;
					else
						__instance.slotAgent[n].transform.Find("Head").transform.Find("Hair").GetComponent<Image>().color = __instance.hairColor32[curPlayer.isPlayer - 1, n];
				}

				if (flag2)
				{
					__instance.slotAgent[n].transform.Find("Head").transform.Find("Head").gameObject.SetActive(false);
					__instance.slotAgent[n].transform.Find("Head").transform.Find("Eyes").gameObject.SetActive(false);
				}
				else
				{
					__instance.slotAgent[n].transform.Find("Head").transform.Find("Head").gameObject.SetActive(true);
					__instance.slotAgent[n].transform.Find("Head").transform.Find("Eyes").gameObject.SetActive(true);
				}

				Vector2 sizeDelta2 = new Vector2(__instance.gr.hairDic[text + "S"].bounds.size.x * 100f, __instance.gr.hairDic[text + "S"].bounds.size.y * 100f);
				__instance.slotAgent[n].transform.Find("Head").transform.Find("Hair").GetComponent<RectTransform>().sizeDelta = sizeDelta2;

				if (__instance.dummyAgent.agentName == "Custom")
				{
					if (__instance.dummyAgent.agentHitboxScript.facialHairType == "None")
						__instance.slotAgent[n].transform.Find("Head").transform.Find("FacialHair").gameObject.SetActive(false);
					else
						__instance.slotAgent[n].transform.Find("Head").transform.Find("FacialHair").gameObject.SetActive(true);
				}
				else if (__instance.facialHairType[curPlayer.isPlayer - 1, n] == "None")
					__instance.slotAgent[n].transform.Find("Head").transform.Find("FacialHair").gameObject.SetActive(false);
				else
					__instance.slotAgent[n].transform.Find("Head").transform.Find("FacialHair").gameObject.SetActive(true);

				if (__instance.dummyAgent.agentName == "Custom")
				{
					__instance.slotAgent[n].transform.Find("Head").transform.Find("FacialHair").GetComponent<Image>().sprite = __instance.gr.facialHairDic[__instance.dummyAgent.agentHitboxScript.facialHairType + "S"];
					__instance.slotAgent[n].transform.Find("Head").transform.Find("FacialHair").GetComponent<Image>().color = __instance.dummyAgent.agentHitboxScript.hairColor;
				}
				else
				{
					__instance.slotAgent[n].transform.Find("Head").transform.Find("FacialHair").GetComponent<Image>().sprite = __instance.gr.facialHairDic[__instance.facialHairType[curPlayer.isPlayer - 1, n] + "S"];
					__instance.slotAgent[n].transform.Find("Head").transform.Find("FacialHair").GetComponent<Image>().color = __instance.facialHairColor32[curPlayer.isPlayer - 1, n];
				}

				if (__instance.dummyAgent.agentName == "Custom")
				{
					__instance.slotAgent[n].transform.Find("Arms").transform.Find("Arm1").GetComponent<Image>().color = __instance.dummyAgent.agentHitboxScript.skinColor;

					if (__instance.dummyAgent.hasSpecialAbilityArm2)
					{
						string invItemName = __instance.dummyAgent.inventory.equippedSpecialAbility.invItemName;
						__instance.slotAgent[n].transform.Find("Arms").transform.Find("Arm2").GetComponent<Image>().color = Color.white;

						if (invItemName == "WaterCannon" || invItemName == "LaserGun")
							__instance.slotAgent[n].transform.Find("Arms").transform.Find("Arm2").GetComponent<RectTransform>().sizeDelta = new Vector2(64f, 64f);
						else
							__instance.slotAgent[n].transform.Find("Arms").transform.Find("Arm2").GetComponent<RectTransform>().sizeDelta = new Vector2(16f, 16f);

						__instance.slotAgent[n].transform.Find("Arms").transform.Find("Arm2").GetComponent<Image>().sprite = __instance.gr.itemDic[invItemName + "Melee"];
					}
					else
					{
						__instance.slotAgent[n].transform.Find("Arms").transform.Find("Arm2").GetComponent<Image>().color = __instance.dummyAgent.agentHitboxScript.skinColor;
						__instance.slotAgent[n].transform.Find("Arms").transform.Find("Arm2").GetComponent<RectTransform>().sizeDelta = new Vector2(16f, 16f);
						__instance.slotAgent[n].transform.Find("Arms").transform.Find("Arm2").GetComponent<Image>().sprite = __instance.gr.itemDic["Arm"];
					}
				}
				else
				{
					__instance.slotAgent[n].transform.Find("Arms").transform.Find("Arm1").GetComponent<Image>().color = __instance.skinColor32[curPlayer.isPlayer - 1, n];

					if (__instance.dummyAgent.hasSpecialAbilityArm2)
					{
						string invItemName2 = __instance.dummyAgent.inventory.equippedSpecialAbility.invItemName;
						__instance.slotAgent[n].transform.Find("Arms").transform.Find("Arm2").GetComponent<Image>().color = Color.white;

						if (invItemName2 == "WaterCannon" || invItemName2 == "LaserGun")
							__instance.slotAgent[n].transform.Find("Arms").transform.Find("Arm2").GetComponent<RectTransform>().sizeDelta = new Vector2(64f, 64f);
						else
							__instance.slotAgent[n].transform.Find("Arms").transform.Find("Arm2").GetComponent<RectTransform>().sizeDelta = new Vector2(16f, 16f);

						__instance.slotAgent[n].transform.Find("Arms").transform.Find("Arm2").GetComponent<Image>().sprite = __instance.gr.itemDic[invItemName2 + "Melee"];
					}
					else
					{
						__instance.slotAgent[n].transform.Find("Arms").transform.Find("Arm2").GetComponent<Image>().color = __instance.skinColor32[curPlayer.isPlayer - 1, n];
						__instance.slotAgent[n].transform.Find("Arms").transform.Find("Arm2").GetComponent<RectTransform>().sizeDelta = new Vector2(16f, 16f);
						__instance.slotAgent[n].transform.Find("Arms").transform.Find("Arm2").GetComponent<Image>().sprite = __instance.gr.itemDic["Arm"];
					}
				}

				if (mySlotAgentType == "ShapeShifter")
				{
					__instance.slotAgent[n].transform.Find("Legs").transform.Find("Leg1").GetComponent<Image>().color = __instance.skinColor32[curPlayer.isPlayer - 1, n];
					__instance.slotAgent[n].transform.Find("Legs").transform.Find("Leg2").GetComponent<Image>().color = __instance.skinColor32[curPlayer.isPlayer - 1, n];
				}
				else
				{
					__instance.slotAgent[n].transform.Find("Legs").transform.Find("Leg1").GetComponent<Image>().color = __instance.dummyAgent.agentHitboxScript.legsColor;
					__instance.slotAgent[n].transform.Find("Legs").transform.Find("Leg2").GetComponent<Image>().color = __instance.dummyAgent.agentHitboxScript.legsColor;
				}

				if (__instance.dummyAgent.statusEffects.hasTrait("RollerSkates"))
				{
					__instance.slotAgent[n].transform.Find("Legs").transform.Find("Leg1").transform.Find("Footwear1").gameObject.SetActive(true);
					__instance.slotAgent[n].transform.Find("Legs").transform.Find("Leg2").transform.Find("Footwear2").gameObject.SetActive(true);
					__instance.slotAgent[n].transform.Find("Legs").transform.Find("Leg1").transform.Find("Footwear1").GetComponent<Image>().color = __instance.dummyAgent.agentHitboxScript.footwearColor;
					__instance.slotAgent[n].transform.Find("Legs").transform.Find("Leg2").transform.Find("Footwear2").GetComponent<Image>().color = __instance.dummyAgent.agentHitboxScript.footwearColor;
				}
				else
				{
					__instance.slotAgent[n].transform.Find("Legs").transform.Find("Leg1").transform.Find("Footwear1").gameObject.SetActive(false);
					__instance.slotAgent[n].transform.Find("Legs").transform.Find("Leg2").transform.Find("Footwear2").gameObject.SetActive(false);
				}

				if (mySlotAgentType == "ShapeShifter")
					__instance.slotAgent[n].transform.Find("Head").transform.Find("Eyes").GetComponent<Image>().color = Color.red;
				else if (mySlotAgentType == "Zombie")
					__instance.slotAgent[n].transform.Find("Head").transform.Find("Eyes").GetComponent<Image>().color = Color.yellow;
				else
					__instance.slotAgent[n].transform.Find("Head").transform.Find("Eyes").GetComponent<Image>().color = __instance.dummyAgent.agentHitboxScript.eyesColor;

				string text2 = "Eyes";

				if (__instance.dummyAgent.agentName == "Custom")
				{
					text2 = __instance.dummyAgent.customCharacterData.eyesType;

					if (text2 == "" || text2 == null)
						text2 = "Eyes";
				}
				else if (__instance.dummyAgent.agentName == "Cannibal")
					text2 = "EyesCannibal";
				else if (__instance.dummyAgent.agentName == "Zombie")
					text2 = "EyesZombie";

				if (text2 == "EyesCannibal")
				{
					__instance.slotAgent[n].transform.Find("Head").transform.Find("Head").GetComponent<Image>().sprite = __instance.gr.headDic["HeadCannibal"];
					__instance.slotAgent[n].transform.Find("Head").transform.Find("Eyes").GetComponent<Image>().sprite = __instance.gr.eyesDic["EyesCannibalS"];
				}
				else if (text2 == "EyesZombie")
				{
					__instance.slotAgent[n].transform.Find("Head").transform.Find("Head").GetComponent<Image>().sprite = __instance.gr.headDic["Head"];
					__instance.slotAgent[n].transform.Find("Head").transform.Find("Eyes").GetComponent<Image>().sprite = __instance.gr.eyesDic["EyesZombieS"];
				}
				else
				{
					__instance.slotAgent[n].transform.Find("Head").transform.Find("Head").GetComponent<Image>().sprite = __instance.gr.headDic["Head"];
					__instance.slotAgent[n].transform.Find("Head").transform.Find("Eyes").GetComponent<Image>().sprite = __instance.gr.eyesDic[text2 + "S"];
				}

				bool flag3 = false;

				using (List<RandomElement>.Enumerator enumerator = __instance.dummyAgent.agentHitboxScript.agent.gc.scriptObject.GetComponent<RandomSelection>().randomListTableStatic["CantShowUnderHeadPiece"].elementList.GetEnumerator())
				{
					while (enumerator.MoveNext())
						if (enumerator.Current.rName == text)
						{
							flag3 = true;
							break;
						}
				}

				__instance.slotAgent[n].transform.Find("Head").transform.Find("HeadPiece").gameObject.SetActive(false);

				if (__instance.dummyAgent.inventory.equippedArmorHead == null && __instance.dummyAgent.inventory.defaultArmorHead != null)
				{
					__instance.slotAgent[n].transform.Find("Head").transform.Find("HeadPiece").gameObject.SetActive(true);
					__instance.slotAgent[n].transform.Find("Head").transform.Find("HeadPiece").GetComponent<Image>().sprite = __instance.gr.headPiecesDic[__instance.dummyAgent.inventory.defaultArmorHead.invItemName + "S"];
				}

				if (__instance.armorBehindHair[n] && !flag2)
				{
					__instance.slotAgent[n].transform.Find("Head").transform.Find("Head").transform.SetParent(__instance.slotAgent[n].transform.Find("Body").transform);
					__instance.slotAgent[n].transform.Find("Body").transform.Find("Head").transform.SetParent(__instance.slotAgent[n].transform.Find("Head").transform);
					__instance.slotAgent[n].transform.Find("Head").transform.Find("Eyes").transform.SetParent(__instance.slotAgent[n].transform.Find("Body").transform);
					__instance.slotAgent[n].transform.Find("Body").transform.Find("Eyes").transform.SetParent(__instance.slotAgent[n].transform.Find("Head").transform);
					__instance.slotAgent[n].transform.Find("Head").transform.Find("HeadPiece").transform.SetParent(__instance.slotAgent[n].transform.Find("Body").transform);
					__instance.slotAgent[n].transform.Find("Body").transform.Find("HeadPiece").transform.SetParent(__instance.slotAgent[n].transform.Find("Head").transform);
					__instance.slotAgent[n].transform.Find("Head").transform.Find("Hair").transform.SetParent(__instance.slotAgent[n].transform.Find("Body").transform);
					__instance.slotAgent[n].transform.Find("Body").transform.Find("Hair").transform.SetParent(__instance.slotAgent[n].transform.Find("Head").transform);
					__instance.slotAgent[n].transform.Find("Head").transform.Find("FacialHair").transform.SetParent(__instance.slotAgent[n].transform.Find("Body").transform);
					__instance.slotAgent[n].transform.Find("Body").transform.Find("FacialHair").transform.SetParent(__instance.slotAgent[n].transform.Find("Head").transform);
				}
				else
				{
					__instance.slotAgent[n].transform.Find("Head").transform.Find("Head").transform.SetParent(__instance.slotAgent[n].transform.Find("Body").transform);
					__instance.slotAgent[n].transform.Find("Body").transform.Find("Head").transform.SetParent(__instance.slotAgent[n].transform.Find("Head").transform);
					__instance.slotAgent[n].transform.Find("Head").transform.Find("Eyes").transform.SetParent(__instance.slotAgent[n].transform.Find("Body").transform);
					__instance.slotAgent[n].transform.Find("Body").transform.Find("Eyes").transform.SetParent(__instance.slotAgent[n].transform.Find("Head").transform);
					__instance.slotAgent[n].transform.Find("Head").transform.Find("Hair").transform.SetParent(__instance.slotAgent[n].transform.Find("Body").transform);
					__instance.slotAgent[n].transform.Find("Body").transform.Find("Hair").transform.SetParent(__instance.slotAgent[n].transform.Find("Head").transform);
					__instance.slotAgent[n].transform.Find("Head").transform.Find("FacialHair").transform.SetParent(__instance.slotAgent[n].transform.Find("Body").transform);
					__instance.slotAgent[n].transform.Find("Body").transform.Find("FacialHair").transform.SetParent(__instance.slotAgent[n].transform.Find("Head").transform);
					__instance.slotAgent[n].transform.Find("Head").transform.Find("HeadPiece").transform.SetParent(__instance.slotAgent[n].transform.Find("Body").transform);
					__instance.slotAgent[n].transform.Find("Body").transform.Find("HeadPiece").transform.SetParent(__instance.slotAgent[n].transform.Find("Head").transform);
				}

				if (__instance.dummyAgent.inventory.defaultArmorHead != null)
				{
					if (__instance.dummyAgent.inventory.defaultArmorHead.cantShowHairAtAll)
					{
						__instance.slotAgent[n].transform.Find("Head").transform.Find("Hair").GetComponent<Image>().sprite = __instance.gr.gui[0];
						return false;
					}

					if (flag3 && __instance.dummyAgent.inventory.defaultArmorHead.cantShowHair)
					{
						__instance.slotAgent[n].transform.Find("Head").transform.Find("Hair").GetComponent<Image>().sprite = __instance.gr.hairDic[text + "S_U"];
						return false;
					}
				}
			}
			else
			{
				if ((!__instance.setSlotAgent[n] || (n >= 32 && n < 48) || __instance.resettingSlotAgent) && n != 48)
				{
					__instance.setSlotAgent[n] = true;
					__instance.dummyAgent.agentHitboxScript.canHaveFacialHair = false;
					__instance.dummyAgent.inventory.DestroyAllItems();
					__instance.dummyAgent.inventory.startingHeadPiece = "";
					__instance.dummyAgent.inventory.equippedSpecialAbility = null;
					__instance.dummyAgent.inventory.defaultArmorHead = null;
					__instance.dummyAgent.inventory.equippedArmor = null;
					__instance.dummyAgent.inventory.equippedArmorHead = null;
					__instance.dummyAgent.customCharacterData = null;
					__instance.dummyAgent.hasSpecialAbilityArm2 = false;
					__instance.dummyAgent.zombified = false;
					__instance.dummyAgent.statusEffects.TraitList.Clear();

					if (n >= 32 && n < 48)
					{
						__instance.dummyAgent.agentName = "Custom";
						__instance.dummyAgent.bigQuest = "";
						__instance.dummyAgent.customCharacterData = GC.sessionDataBig.customCharacterSlotsDetails[n - 32];
						__instance.dummyAgent.agentHitboxScript.GetColorFromString(__instance.dummyAgent.customCharacterData.bodyColorName, "Body");
						__instance.dummyAgent.agentHitboxScript.GetColorFromString(__instance.dummyAgent.customCharacterData.hairColorName, "Hair");
						__instance.dummyAgent.agentHitboxScript.GetColorFromString(__instance.dummyAgent.customCharacterData.skinColorName, "Skin");
						__instance.dummyAgent.agentHitboxScript.GetColorFromString(__instance.dummyAgent.customCharacterData.legsColorName, "Legs");
						__instance.dummyAgent.agentHitboxScript.GetColorFromString(__instance.dummyAgent.customCharacterData.eyesColorName, "Eyes");
						__instance.dummyAgent.agentHitboxScript.hairType = __instance.dummyAgent.customCharacterData.hairType;
						__instance.dummyAgent.agentHitboxScript.facialHairType = __instance.dummyAgent.customCharacterData.facialHair;

						if (__instance.dummyAgent.customCharacterData.startingHeadPiece != "" && __instance.dummyAgent.customCharacterData.startingHeadPiece != null)
							__instance.dummyAgent.agentInvDatabase.AddStartingHeadPiece(__instance.dummyAgent.customCharacterData.startingHeadPiece);
					}
					else
						__instance.dummyAgent.agentName = mySlotAgentType;

					__instance.dummyAgent.SetupAgentStats("");
					__instance.slotAgent[n].gameObject.SetActive(true);

					if (mySlotAgentType == "Cop2")
						mySlotAgentType = "Cop";
					if (mySlotAgentType == "Cop" && GC.challenges.Contains("SuperSpecialCharacters") && GC.unlocks.isBigQuestCompleted(mySlotAgentType))
						mySlotAgentType = "Cop2";
					if (mySlotAgentType == "Guard2")
						mySlotAgentType = "Guard";
					if (mySlotAgentType == "Guard" && GC.challenges.Contains("SuperSpecialCharacters") && GC.unlocks.isBigQuestCompleted(mySlotAgentType))
						mySlotAgentType = "Guard2";
					if (mySlotAgentType == "UpperCruster")
						mySlotAgentType = "Hobo";
					if (mySlotAgentType == "Hobo" && GC.challenges.Contains("SuperSpecialCharacters") && GC.unlocks.isBigQuestCompleted(mySlotAgentType))
						mySlotAgentType = "UpperCruster";

					__instance.slotAgent[n].transform.parent.GetComponent<InvSlot>().characterSelectType = mySlotAgentType;

					if (__instance.dummyAgent.agentName == "Custom")
					{
						Vector2 sizeDelta;

						if (__instance.dummyAgent.customCharacterData.bodyType.Contains("G_"))
						{
							__instance.slotAgent[n].transform.Find("Body").GetComponent<Image>().sprite = __instance.gr.bodyGDic[__instance.dummyAgent.customCharacterData.bodyType + "S"];
							sizeDelta = new Vector2(__instance.gr.bodyGDic[__instance.dummyAgent.customCharacterData.bodyType + "S"].bounds.size.x * 100f, __instance.gr.bodyGDic[__instance.dummyAgent.customCharacterData.bodyType + "S"].bounds.size.y * 100f);
						}
						else
						{
							__instance.slotAgent[n].transform.Find("Body").GetComponent<Image>().sprite = __instance.gr.bodyDic[__instance.dummyAgent.customCharacterData.bodyType + "S"];
							sizeDelta = new Vector2(__instance.gr.bodyDic[__instance.dummyAgent.customCharacterData.bodyType + "S"].bounds.size.x * 100f, __instance.gr.bodyDic[__instance.dummyAgent.customCharacterData.bodyType + "S"].bounds.size.y * 100f);
						}

						__instance.slotAgent[n].transform.transform.Find("Body").GetComponent<RectTransform>().sizeDelta = sizeDelta;
					}
					else
					{
						__instance.slotAgent[n].transform.Find("Body").GetComponent<Image>().sprite = __instance.gr.bodyDic[__instance.dummyAgent.agentName + "S"];
						Vector2 sizeDelta = new Vector2(__instance.gr.bodyDic[__instance.dummyAgent.agentName + "S"].bounds.size.x * 100f, __instance.gr.bodyDic[__instance.dummyAgent.agentName + "S"].bounds.size.y * 100f);
						__instance.slotAgent[n].transform.transform.Find("Body").GetComponent<RectTransform>().sizeDelta = sizeDelta;
					}

					Color32 c = Color.white;

					if (__instance.dummyAgent.agentName != "Custom")
					{
						__instance.dummyAgent.agentHitboxScript.chooseSkinColor(__instance.dummyAgent.agentName);
					}

					c = __instance.dummyAgent.agentHitboxScript.skinColor;

					if (__instance.dummyAgent.agentName == "Custom")
						__instance.slotAgent[n].transform.Find("Body").GetComponent<Image>().color = __instance.dummyAgent.agentHitboxScript.bodyColor;
					else if (mySlotAgentType == "Wrestler" || mySlotAgentType == "ShapeShifter")
						__instance.slotAgent[n].transform.Find("Body").GetComponent<Image>().color = c;
					else
						__instance.slotAgent[n].transform.Find("Body").GetComponent<Image>().color = Color.white;

					__instance.slotAgent[n].transform.Find("Head").transform.Find("Head").GetComponent<Image>().color = c;
					string text3 = "";

					if (__instance.dummyAgent.agentName == "Custom")
						text3 = __instance.dummyAgent.agentHitboxScript.hairType;
					else
						text3 = __instance.dummyAgent.agentHitboxScript.chooseHairType(__instance.dummyAgent.agentName);

					bool flag4 = false;
					bool flag5 = false;
					Color32 color = Color.white;

					if (__instance.dummyAgent.agentName == "Custom")
						color = __instance.dummyAgent.agentHitboxScript.hairColor;
					else
						color = __instance.dummyAgent.agentHitboxScript.chooseHairColor(__instance.dummyAgent.agentName, true);

					Color32 color2 = color;

					if (__instance.dummyAgent.agentName == "Custom")
						color2 = __instance.dummyAgent.agentHitboxScript.hairColor;

					foreach (RandomElement randomElement2 in GC.rnd.randomListTableStatic["NotHairStyles"].elementList)
					{
						if (randomElement2.rName == text3)
						{
							color = AgentHitbox.white;

							if (__instance.dummyAgent.agentName != "Custom")
								flag4 = true;

							if (randomElement2.rName != "Hoodie" && randomElement2.rName != "AssassinMask" && randomElement2.rName != "SlavemasterMask")
							{
								flag5 = true;
								break;
							}

							break;
						}
					}

					if (!flag4)
						color = color2;

					if (flag5)
					{
						__instance.slotAgent[n].transform.Find("Head").transform.Find("Head").gameObject.SetActive(false);
						__instance.slotAgent[n].transform.Find("Head").transform.Find("Eyes").gameObject.SetActive(false);
					}
					else
					{
						__instance.slotAgent[n].transform.Find("Head").transform.Find("Head").gameObject.SetActive(true);
						__instance.slotAgent[n].transform.Find("Head").transform.Find("Eyes").gameObject.SetActive(true);
					}

					if (__instance.dummyAgent.agentName == "Slavemaster")
						__instance.slotAgent[n].transform.Find("Head").transform.Find("Hair").GetComponent<Image>().sprite = __instance.gr.hairDic["SlavemasterMaskSpecialS"];
					else
						__instance.slotAgent[n].transform.Find("Head").transform.Find("Hair").GetComponent<Image>().sprite = __instance.gr.hairDic[text3 + "S"];

					__instance.slotAgent[n].transform.Find("Head").transform.Find("Hair").GetComponent<Image>().color = color;
					Vector2 sizeDelta2 = new Vector2(__instance.gr.hairDic[text3 + "S"].bounds.size.x * 100f, __instance.gr.hairDic[text3 + "S"].bounds.size.y * 100f);
					__instance.slotAgent[n].transform.Find("Head").transform.Find("Hair").GetComponent<RectTransform>().sizeDelta = sizeDelta2;
					string text4;

					if (__instance.dummyAgent.agentName == "Custom")
						text4 = __instance.dummyAgent.agentHitboxScript.facialHairType;
					else
						text4 = __instance.dummyAgent.agentHitboxScript.chooseFacialHairType(__instance.dummyAgent.agentName);

					if (text4 == "" || text4 == "None")
						__instance.slotAgent[n].transform.Find("Head").transform.Find("FacialHair").gameObject.SetActive(false);
					else
					{
						__instance.slotAgent[n].transform.Find("Head").transform.Find("FacialHair").gameObject.SetActive(true);
						__instance.slotAgent[n].transform.Find("Head").transform.Find("FacialHair").GetComponent<Image>().sprite = __instance.gr.facialHairDic[text4 + "S"];
					}

					__instance.slotAgent[n].transform.Find("Head").transform.Find("FacialHair").GetComponent<Image>().color = __instance.dummyAgent.agentHitboxScript.hairColor;
					__instance.slotAgent[n].transform.Find("Arms").transform.Find("Arm1").GetComponent<Image>().color = c;

					if (__instance.dummyAgent.hasSpecialAbilityArm2)
					{
						string invItemName3 = __instance.dummyAgent.inventory.equippedSpecialAbility.invItemName;
						__instance.slotAgent[n].transform.Find("Arms").transform.Find("Arm2").GetComponent<Image>().color = Color.white;

						if (invItemName3 == "WaterCannon" || invItemName3 == "LaserGun")
							__instance.slotAgent[n].transform.Find("Arms").transform.Find("Arm2").GetComponent<RectTransform>().sizeDelta = new Vector2(64f, 64f);
						else
							__instance.slotAgent[n].transform.Find("Arms").transform.Find("Arm2").GetComponent<RectTransform>().sizeDelta = new Vector2(16f, 16f);

						__instance.slotAgent[n].transform.Find("Arms").transform.Find("Arm2").GetComponent<Image>().sprite = __instance.gr.itemDic[invItemName3 + "Melee"];
					}
					else
					{
						__instance.slotAgent[n].transform.Find("Arms").transform.Find("Arm2").GetComponent<Image>().color = c;
						__instance.slotAgent[n].transform.Find("Arms").transform.Find("Arm2").GetComponent<RectTransform>().sizeDelta = new Vector2(16f, 16f);
						__instance.slotAgent[n].transform.Find("Arms").transform.Find("Arm2").GetComponent<Image>().sprite = __instance.gr.itemDic["Arm"];
					}

					if (mySlotAgentType == "ShapeShifter")
					{
						__instance.slotAgent[n].transform.Find("Legs").transform.Find("Leg1").GetComponent<Image>().color = c;
						__instance.slotAgent[n].transform.Find("Legs").transform.Find("Leg2").GetComponent<Image>().color = c;
					}
					else
					{
						__instance.slotAgent[n].transform.Find("Legs").transform.Find("Leg1").GetComponent<Image>().color = __instance.dummyAgent.agentHitboxScript.legsColor;
						__instance.slotAgent[n].transform.Find("Legs").transform.Find("Leg2").GetComponent<Image>().color = __instance.dummyAgent.agentHitboxScript.legsColor;
					}

					if (__instance.dummyAgent.statusEffects.hasTrait("RollerSkates"))
					{
						__instance.slotAgent[n].transform.Find("Legs").transform.Find("Leg1").transform.Find("Footwear1").gameObject.SetActive(true);
						__instance.slotAgent[n].transform.Find("Legs").transform.Find("Leg2").transform.Find("Footwear2").gameObject.SetActive(true);
						__instance.slotAgent[n].transform.Find("Legs").transform.Find("Leg1").transform.Find("Footwear1").GetComponent<Image>().color = __instance.dummyAgent.agentHitboxScript.footwearColor;
						__instance.slotAgent[n].transform.Find("Legs").transform.Find("Leg2").transform.Find("Footwear2").GetComponent<Image>().color = __instance.dummyAgent.agentHitboxScript.footwearColor;
					}
					else
					{
						__instance.slotAgent[n].transform.Find("Legs").transform.Find("Leg1").transform.Find("Footwear1").gameObject.SetActive(false);
						__instance.slotAgent[n].transform.Find("Legs").transform.Find("Leg2").transform.Find("Footwear2").gameObject.SetActive(false);
					}

					if (mySlotAgentType == "ShapeShifter")
						__instance.slotAgent[n].transform.Find("Head").transform.Find("Eyes").GetComponent<Image>().color = Color.red;
					else if (mySlotAgentType == "Zombie")
						__instance.slotAgent[n].transform.Find("Head").transform.Find("Eyes").GetComponent<Image>().color = Color.yellow;
					else
						__instance.slotAgent[n].transform.Find("Head").transform.Find("Eyes").GetComponent<Image>().color = __instance.dummyAgent.agentHitboxScript.eyesColor;

					string text5 = "Eyes";

					if (__instance.dummyAgent.agentName == "Custom")
					{
						text5 = __instance.dummyAgent.customCharacterData.eyesType;

						if (text5 == "" || text5 == null)
							text5 = "Eyes";
					}
					else if (__instance.dummyAgent.agentName == "Cannibal")
						text5 = "EyesCannibal";
					else if (__instance.dummyAgent.agentName == "Zombie")
						text5 = "EyesZombie";

					if (text5 == "EyesCannibal")
					{
						__instance.slotAgent[n].transform.Find("Head").transform.Find("Head").GetComponent<Image>().sprite = __instance.gr.headDic["HeadCannibal"];
						__instance.slotAgent[n].transform.Find("Head").transform.Find("Eyes").GetComponent<Image>().sprite = __instance.gr.eyesDic["EyesCannibalS"];
					}
					else if (text5 == "EyesZombie")
					{
						__instance.slotAgent[n].transform.Find("Head").transform.Find("Head").GetComponent<Image>().sprite = __instance.gr.headDic["Head"];
						__instance.slotAgent[n].transform.Find("Head").transform.Find("Eyes").GetComponent<Image>().sprite = __instance.gr.eyesDic["EyesZombieS"];
					}
					else
					{
						__instance.slotAgent[n].transform.Find("Head").transform.Find("Head").GetComponent<Image>().sprite = __instance.gr.headDic["Head"];
						__instance.slotAgent[n].transform.Find("Head").transform.Find("Eyes").GetComponent<Image>().sprite = __instance.gr.eyesDic[text5 + "S"];
					}

					bool flag3 = false;

					using (List<RandomElement>.Enumerator enumerator = __instance.dummyAgent.agentHitboxScript.agent.gc.scriptObject.GetComponent<RandomSelection>().randomListTableStatic["CantShowUnderHeadPiece"].elementList.GetEnumerator())
					{
						while (enumerator.MoveNext())
							if (enumerator.Current.rName == __instance.dummyAgent.agentHitboxScript.hairType)
							{
								flag3 = true;
								break;
							}
					}

					__instance.slotAgent[n].transform.Find("Head").transform.Find("Hair").gameObject.SetActive(true);
					__instance.slotAgent[n].transform.Find("Head").transform.Find("HeadPiece").gameObject.SetActive(false);

					if (__instance.dummyAgent.inventory.equippedArmorHead == null && __instance.dummyAgent.inventory.defaultArmorHead != null)
					{
						__instance.slotAgent[n].transform.Find("Head").transform.Find("HeadPiece").gameObject.SetActive(true);
						__instance.slotAgent[n].transform.Find("Head").transform.Find("HeadPiece").GetComponent<Image>().sprite = __instance.gr.headPiecesDic[__instance.dummyAgent.inventory.defaultArmorHead.invItemName + "S"];

						if (__instance.dummyAgent.inventory.defaultArmorHead.behindHair && !flag5)
						{
							__instance.slotAgent[n].transform.Find("Head").transform.Find("Head").transform.SetParent(__instance.slotAgent[n].transform.Find("Body").transform);
							__instance.slotAgent[n].transform.Find("Body").transform.Find("Head").transform.SetParent(__instance.slotAgent[n].transform.Find("Head").transform);
							__instance.slotAgent[n].transform.Find("Head").transform.Find("Eyes").transform.SetParent(__instance.slotAgent[n].transform.Find("Body").transform);
							__instance.slotAgent[n].transform.Find("Body").transform.Find("Eyes").transform.SetParent(__instance.slotAgent[n].transform.Find("Head").transform);
							__instance.slotAgent[n].transform.Find("Head").transform.Find("HeadPiece").transform.SetParent(__instance.slotAgent[n].transform.Find("Body").transform);
							__instance.slotAgent[n].transform.Find("Body").transform.Find("HeadPiece").transform.SetParent(__instance.slotAgent[n].transform.Find("Head").transform);
							__instance.slotAgent[n].transform.Find("Head").transform.Find("Hair").transform.SetParent(__instance.slotAgent[n].transform.Find("Body").transform);
							__instance.slotAgent[n].transform.Find("Body").transform.Find("Hair").transform.SetParent(__instance.slotAgent[n].transform.Find("Head").transform);
							__instance.slotAgent[n].transform.Find("Head").transform.Find("FacialHair").transform.SetParent(__instance.slotAgent[n].transform.Find("Body").transform);
							__instance.slotAgent[n].transform.Find("Body").transform.Find("FacialHair").transform.SetParent(__instance.slotAgent[n].transform.Find("Head").transform);
							__instance.armorBehindHair[n] = true;
						}
						else
						{
							__instance.slotAgent[n].transform.Find("Head").transform.Find("Head").transform.SetParent(__instance.slotAgent[n].transform.Find("Body").transform);
							__instance.slotAgent[n].transform.Find("Body").transform.Find("Head").transform.SetParent(__instance.slotAgent[n].transform.Find("Head").transform);
							__instance.slotAgent[n].transform.Find("Head").transform.Find("Eyes").transform.SetParent(__instance.slotAgent[n].transform.Find("Body").transform);
							__instance.slotAgent[n].transform.Find("Body").transform.Find("Eyes").transform.SetParent(__instance.slotAgent[n].transform.Find("Head").transform);
							__instance.slotAgent[n].transform.Find("Head").transform.Find("Hair").transform.SetParent(__instance.slotAgent[n].transform.Find("Body").transform);
							__instance.slotAgent[n].transform.Find("Body").transform.Find("Hair").transform.SetParent(__instance.slotAgent[n].transform.Find("Head").transform);
							__instance.slotAgent[n].transform.Find("Head").transform.Find("FacialHair").transform.SetParent(__instance.slotAgent[n].transform.Find("Body").transform);
							__instance.slotAgent[n].transform.Find("Body").transform.Find("FacialHair").transform.SetParent(__instance.slotAgent[n].transform.Find("Head").transform);
							__instance.slotAgent[n].transform.Find("Head").transform.Find("HeadPiece").transform.SetParent(__instance.slotAgent[n].transform.Find("Body").transform);
							__instance.slotAgent[n].transform.Find("Body").transform.Find("HeadPiece").transform.SetParent(__instance.slotAgent[n].transform.Find("Head").transform);
						}

						if (__instance.dummyAgent.inventory.defaultArmorHead.cantShowHairAtAll)
							__instance.slotAgent[n].transform.Find("Head").transform.Find("Hair").GetComponent<Image>().sprite = __instance.gr.gui[0];
						else if (flag3 && __instance.dummyAgent.inventory.defaultArmorHead.cantShowHair)
							__instance.slotAgent[n].transform.Find("Head").transform.Find("Hair").GetComponent<Image>().sprite = __instance.gr.hairDic[text3 + "S_U"];

						__instance.cantShowHair[n] = true;
					}

					__instance.hairType[0, n] = __instance.dummyAgent.agentHitboxScript.hairType;
					__instance.facialHairType[0, n] = __instance.dummyAgent.agentHitboxScript.facialHairType;
					__instance.skinColor[0, n] = __instance.dummyAgent.agentHitboxScript.skinColorName;
					__instance.hairColor[0, n] = __instance.dummyAgent.agentHitboxScript.hairColorName;
					__instance.facialHairColor[0, n] = __instance.dummyAgent.agentHitboxScript.facialHairColorName;
					__instance.skinColor32[0, n] = __instance.dummyAgent.agentHitboxScript.skinColor;
					__instance.hairColor32[0, n] = color;
					__instance.facialHairColor32[0, n] = color2;
					__instance.dummyAgent.agentHitboxScript.hasSetup = false;
					__instance.dummyAgent.agentHitboxScript.SetupFeatures();

					if (!flag4)
					{
						if (__instance.dummyAgent.agentName == "Custom")
							color = __instance.dummyAgent.agentHitboxScript.hairColor;
						else
							color = __instance.dummyAgent.agentHitboxScript.chooseHairColor(__instance.dummyAgent.agentName, true);

						color2 = color;
					}

					if (__instance.dummyAgent.agentName == "Custom")
						color = __instance.dummyAgent.agentHitboxScript.hairColor;
					else
						__instance.dummyAgent.agentHitboxScript.chooseHairColor(__instance.dummyAgent.agentName, false);

					__instance.hairType[1, n] = __instance.dummyAgent.agentHitboxScript.hairType;
					__instance.facialHairType[1, n] = __instance.dummyAgent.agentHitboxScript.facialHairType;
					__instance.skinColor[1, n] = __instance.dummyAgent.agentHitboxScript.skinColorName;
					__instance.hairColor[1, n] = __instance.dummyAgent.agentHitboxScript.hairColorName;
					__instance.facialHairColor[1, n] = __instance.dummyAgent.agentHitboxScript.facialHairColorName;
					__instance.skinColor32[1, n] = __instance.dummyAgent.agentHitboxScript.skinColor;
					__instance.hairColor32[1, n] = color;
					__instance.facialHairColor32[1, n] = color2;
					__instance.dummyAgent.agentHitboxScript.hasSetup = false;
					__instance.dummyAgent.agentHitboxScript.SetupFeatures();

					if (!flag4)
					{
						if (__instance.dummyAgent.agentName == "Custom")
							color = __instance.dummyAgent.agentHitboxScript.hairColor;
						else
							color = __instance.dummyAgent.agentHitboxScript.chooseHairColor(__instance.dummyAgent.agentName, true);

						color2 = color;
					}

					__instance.hairType[2, n] = __instance.dummyAgent.agentHitboxScript.hairType;
					__instance.facialHairType[2, n] = __instance.dummyAgent.agentHitboxScript.facialHairType;
					__instance.skinColor[2, n] = __instance.dummyAgent.agentHitboxScript.skinColorName;
					__instance.hairColor[2, n] = __instance.dummyAgent.agentHitboxScript.hairColorName;
					__instance.facialHairColor[2, n] = __instance.dummyAgent.agentHitboxScript.facialHairColorName;
					__instance.skinColor32[2, n] = __instance.dummyAgent.agentHitboxScript.skinColor;
					__instance.hairColor32[2, n] = color;
					__instance.facialHairColor32[2, n] = color2;
					__instance.dummyAgent.agentHitboxScript.hasSetup = false;
					__instance.dummyAgent.agentHitboxScript.SetupFeatures();

					if (!flag4)
					{
						if (__instance.dummyAgent.agentName == "Custom")
							color = __instance.dummyAgent.agentHitboxScript.hairColor;
						else
							color = __instance.dummyAgent.agentHitboxScript.chooseHairColor(__instance.dummyAgent.agentName, true);

						color2 = color;
					}

					__instance.hairType[3, n] = __instance.dummyAgent.agentHitboxScript.hairType;
					__instance.facialHairType[3, n] = __instance.dummyAgent.agentHitboxScript.facialHairType;
					__instance.skinColor[3, n] = __instance.dummyAgent.agentHitboxScript.skinColorName;
					__instance.hairColor[3, n] = __instance.dummyAgent.agentHitboxScript.hairColorName;
					__instance.facialHairColor[3, n] = __instance.dummyAgent.agentHitboxScript.facialHairColorName;
					__instance.skinColor32[3, n] = __instance.dummyAgent.agentHitboxScript.skinColor;
					__instance.hairColor32[3, n] = color;
					__instance.facialHairColor32[3, n] = color2;

					return false;
				}

				if (n == 48 && mySlotAgentType != "")
				{
					int num = 0;
					GameObject gameObject = null;
					GameObject gameObject2 = __instance.slotAgent[n + (curPlayer.isPlayer - 1)];
					__instance.dummyAgent.agentHitboxScript.canHaveFacialHair = false;
					__instance.dummyAgent.inventory.DestroyAllItems();
					__instance.dummyAgent.inventory.startingHeadPiece = "";
					__instance.dummyAgent.inventory.equippedSpecialAbility = null;
					__instance.dummyAgent.inventory.defaultArmorHead = null;
					__instance.dummyAgent.inventory.equippedArmor = null;
					__instance.dummyAgent.inventory.equippedArmorHead = null;
					__instance.dummyAgent.customCharacterData = null;
					__instance.dummyAgent.hasSpecialAbilityArm2 = false;
					__instance.dummyAgent.zombified = false;
					__instance.dummyAgent.statusEffects.TraitList.Clear();
					bool flag6 = false;
					int num2 = 0;

					for (int i = 32; i < 48; i++)
					{
						if (__instance.slotAgent[i].transform.parent.GetComponent<InvSlot>().characterSelectType == mySlotAgentType)
						{
							__instance.dummyAgent.agentName = "Custom";
							__instance.dummyAgent.bigQuest = "";
							__instance.dummyAgent.customCharacterData = GC.sessionDataBig.customCharacterSlotsDetails[i - 32];
							__instance.dummyAgent.agentHitboxScript.GetColorFromString(__instance.dummyAgent.customCharacterData.bodyColorName, "Body");
							__instance.dummyAgent.agentHitboxScript.GetColorFromString(__instance.dummyAgent.customCharacterData.hairColorName, "Hair");
							__instance.dummyAgent.agentHitboxScript.GetColorFromString(__instance.dummyAgent.customCharacterData.skinColorName, "Skin");
							__instance.dummyAgent.agentHitboxScript.GetColorFromString(__instance.dummyAgent.customCharacterData.legsColorName, "Legs");
							__instance.dummyAgent.agentHitboxScript.GetColorFromString(__instance.dummyAgent.customCharacterData.eyesColorName, "Eyes");
							__instance.dummyAgent.agentHitboxScript.hairType = __instance.dummyAgent.customCharacterData.hairType;
							__instance.dummyAgent.agentHitboxScript.facialHairType = __instance.dummyAgent.customCharacterData.facialHair;

							if (__instance.dummyAgent.customCharacterData.startingHeadPiece != "" && __instance.dummyAgent.customCharacterData.startingHeadPiece != null)
								__instance.dummyAgent.agentInvDatabase.AddStartingHeadPiece(__instance.dummyAgent.customCharacterData.startingHeadPiece);

							flag6 = true;
							gameObject = __instance.slotAgent[i];
							num2 = i;
						}
					}

					if (!flag6)
						__instance.dummyAgent.agentName = mySlotAgentType;

					__instance.dummyAgent.SetupAgentStats("");
					gameObject2.transform.Find("Body").gameObject.SetActive(true);
					gameObject2.transform.Find("Head").transform.Find("Head").gameObject.SetActive(true);
					gameObject2.transform.Find("Head").transform.Find("Hair").gameObject.SetActive(true);
					gameObject2.transform.Find("Head").transform.Find("FacialHair").gameObject.SetActive(true);
					gameObject2.transform.Find("Head").transform.Find("Eyes").gameObject.SetActive(true);
					gameObject2.transform.Find("Head").transform.Find("HeadPiece").gameObject.SetActive(true);
					gameObject2.transform.Find("Arms").transform.Find("Arm1").gameObject.SetActive(true);
					gameObject2.transform.Find("Arms").transform.Find("Arm2").gameObject.SetActive(true);
					gameObject2.transform.Find("Legs").transform.Find("Leg1").gameObject.SetActive(true);
					gameObject2.transform.Find("Legs").transform.Find("Leg2").gameObject.SetActive(true);
					__instance.curSelected[curPlayer.isPlayer - 1] = mySlotAgentType;

					if (mySlotAgentType == "Cop2" && GC.challenges.Contains("SuperSpecialCharacters") && GC.unlocks.isBigQuestCompleted("Cop") && !__instance.slotAgentTypes.Contains("Cop2"))
						mySlotAgentType = "Cop";
					if (mySlotAgentType == "Guard2" && GC.challenges.Contains("SuperSpecialCharacters") && GC.unlocks.isBigQuestCompleted("Guard") && !__instance.slotAgentTypes.Contains("Guard2"))
						mySlotAgentType = "Guard";
					if (mySlotAgentType == "UpperCruster" && GC.challenges.Contains("SuperSpecialCharacters") && GC.unlocks.isBigQuestCompleted("Hobo") && !__instance.slotAgentTypes.Contains("UpperCruster"))
						mySlotAgentType = "Hobo";

					if (__instance.dummyAgent.agentName != "Custom")
					{
						using (List<string>.Enumerator enumerator2 = __instance.slotAgentTypes.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								if (enumerator2.Current == mySlotAgentType)
								{
									gameObject = __instance.slotAgent[num];
									__instance.curSelectedNum[curPlayer.isPlayer - 1] = num;
									num2 = num;

									break;
								}

								num++;
							}
						}
					}

					if (mySlotAgentType == "Cop2")
						mySlotAgentType = "Cop";
					if (mySlotAgentType == "Cop" && GC.challenges.Contains("SuperSpecialCharacters") && GC.unlocks.isBigQuestCompleted(mySlotAgentType))
						mySlotAgentType = "Cop2";
					if (mySlotAgentType == "Guard2")
						mySlotAgentType = "Guard";
					if (mySlotAgentType == "Guard" && GC.challenges.Contains("SuperSpecialCharacters") && GC.unlocks.isBigQuestCompleted(mySlotAgentType))
						mySlotAgentType = "Guard2";
					if (mySlotAgentType == "UpperCruster")
						mySlotAgentType = "Hobo";
					if (mySlotAgentType == "Hobo" && GC.challenges.Contains("SuperSpecialCharacters") && GC.unlocks.isBigQuestCompleted(mySlotAgentType))
						mySlotAgentType = "UpperCruster";
					if (__instance.dummyAgent.agentName == "Custom")
						__instance.MakeSelectablesInteractable(curPlayer.isPlayer - 1, false);
					else
						__instance.MakeSelectablesInteractable(curPlayer.isPlayer - 1, true);

					gameObject.gameObject.SetActive(true);
					gameObject.transform.parent.GetComponent<InvSlot>().characterSelectType = mySlotAgentType;
					gameObject2.transform.Find("Body").GetComponent<Image>().sprite = gameObject.transform.Find("Body").GetComponent<Image>().sprite;

					if (__instance.dummyAgent.agentName == "Custom")
					{
						Vector2 sizeDelta;

						if (__instance.dummyAgent.customCharacterData.bodyType.Contains("G_"))
							sizeDelta = new Vector2(__instance.gr.bodyGDic[__instance.dummyAgent.customCharacterData.bodyType + "S"].bounds.size.x * 100f, __instance.gr.bodyGDic[__instance.dummyAgent.customCharacterData.bodyType + "S"].bounds.size.y * 100f);
						else
							sizeDelta = new Vector2(__instance.gr.bodyDic[__instance.dummyAgent.customCharacterData.bodyType + "S"].bounds.size.x * 100f, __instance.gr.bodyDic[__instance.dummyAgent.customCharacterData.bodyType + "S"].bounds.size.y * 100f);

						gameObject2.transform.transform.Find("Body").GetComponent<RectTransform>().sizeDelta = sizeDelta;
					}
					else
					{
						Vector2 sizeDelta = new Vector2(__instance.gr.bodyDic[__instance.dummyAgent.agentName + "S"].bounds.size.x * 100f, __instance.gr.bodyDic[__instance.dummyAgent.agentName + "S"].bounds.size.y * 100f);
						gameObject2.transform.transform.Find("Body").GetComponent<RectTransform>().sizeDelta = sizeDelta;
					}

					if (__instance.dummyAgent.agentName == "Custom")
						gameObject2.transform.Find("Body").GetComponent<Image>().color = __instance.dummyAgent.agentHitboxScript.bodyColor;
					else if (mySlotAgentType == "Wrestler" || mySlotAgentType == "ShapeShifter")
						gameObject2.transform.Find("Body").GetComponent<Image>().color = __instance.skinColor32[curPlayer.isPlayer - 1, num];
					else
						gameObject2.transform.Find("Body").GetComponent<Image>().color = Color.white;

					if (__instance.dummyAgent.agentName == "Custom")
						gameObject2.transform.Find("Head").transform.Find("Head").GetComponent<Image>().color = __instance.dummyAgent.agentHitboxScript.skinColor;
					else
						gameObject2.transform.Find("Head").transform.Find("Head").GetComponent<Image>().color = __instance.skinColor32[curPlayer.isPlayer - 1, num];

					if (__instance.dummyAgent.agentName == "Custom")
						gameObject2.transform.Find("Head").transform.Find("Hair").GetComponent<Image>().sprite = __instance.gr.hairDic[__instance.dummyAgent.agentHitboxScript.hairType + "S"];
					else if (__instance.dummyAgent.agentName == "Slavemaster")
						gameObject2.transform.Find("Head").transform.Find("Hair").GetComponent<Image>().sprite = __instance.gr.hairDic["SlavemasterMaskSpecialS"];
					else
						gameObject2.transform.Find("Head").transform.Find("Hair").GetComponent<Image>().sprite = __instance.gr.hairDic[__instance.hairType[curPlayer.isPlayer - 1, num] + "S"];

					string text6 = "";

					if (__instance.dummyAgent.agentName == "Custom")
						text6 = __instance.dummyAgent.agentHitboxScript.hairType;
					else
						text6 = __instance.hairType[curPlayer.isPlayer - 1, num];

					bool flag7 = false;
					bool flag8 = false;

					foreach (RandomElement randomElement3 in GC.rnd.randomListTableStatic["NotHairStyles"].elementList)
					{
						if (randomElement3.rName == text6)
						{
							gameObject2.transform.Find("Head").transform.Find("Hair").GetComponent<Image>().color = AgentHitbox.white;

							if (__instance.dummyAgent.agentName != "Custom")
								flag7 = true;
							if (randomElement3.rName != "Hoodie" && randomElement3.rName != "AssassinMask" && randomElement3.rName != "SlavemasterMask")
							{
								flag8 = true;

								break;
							}

							break;
						}
					}

					if (!flag7)
					{
						if (__instance.dummyAgent.agentName == "Custom")
							gameObject2.transform.Find("Head").transform.Find("Hair").GetComponent<Image>().color = __instance.dummyAgent.agentHitboxScript.hairColor;
						else
							gameObject2.transform.Find("Head").transform.Find("Hair").GetComponent<Image>().color = __instance.hairColor32[curPlayer.isPlayer - 1, num];
					}

					if (flag8)
					{
						gameObject2.transform.Find("Head").transform.Find("Head").gameObject.SetActive(false);
						gameObject2.transform.Find("Head").transform.Find("Eyes").gameObject.SetActive(false);
					}
					else
					{
						gameObject2.transform.Find("Head").transform.Find("Head").gameObject.SetActive(true);
						gameObject2.transform.Find("Head").transform.Find("Eyes").gameObject.SetActive(true);
					}

					Vector2 sizeDelta2 = new Vector2(__instance.gr.hairDic[text6 + "S"].bounds.size.x * 100f, __instance.gr.hairDic[text6 + "S"].bounds.size.y * 100f);
					gameObject2.transform.Find("Head").transform.Find("Hair").GetComponent<RectTransform>().sizeDelta = sizeDelta2;

					if (__instance.dummyAgent.agentName == "Custom")
					{
						if (__instance.dummyAgent.agentHitboxScript.facialHairType == "None")
							gameObject2.transform.Find("Head").transform.Find("FacialHair").gameObject.SetActive(false);
						else
							gameObject2.transform.Find("Head").transform.Find("FacialHair").gameObject.SetActive(true);
					}
					else if (__instance.facialHairType[curPlayer.isPlayer - 1, num] == "None")
						gameObject2.transform.Find("Head").transform.Find("FacialHair").gameObject.SetActive(false);
					else
						gameObject2.transform.Find("Head").transform.Find("FacialHair").gameObject.SetActive(true);

					if (__instance.dummyAgent.agentName == "Custom")
						gameObject2.transform.Find("Head").transform.Find("FacialHair").GetComponent<Image>().sprite = __instance.gr.facialHairDic[__instance.dummyAgent.agentHitboxScript.facialHairType + "S"];
					else
						gameObject2.transform.Find("Head").transform.Find("FacialHair").GetComponent<Image>().sprite = __instance.gr.facialHairDic[__instance.facialHairType[curPlayer.isPlayer - 1, num] + "S"];

					if (__instance.dummyAgent.agentName == "Custom")
						gameObject2.transform.Find("Head").transform.Find("FacialHair").GetComponent<Image>().color = __instance.dummyAgent.agentHitboxScript.hairColor;
					else
						gameObject2.transform.Find("Head").transform.Find("FacialHair").GetComponent<Image>().color = __instance.facialHairColor32[curPlayer.isPlayer - 1, num];

					if (__instance.dummyAgent.agentName == "Custom")
					{
						gameObject2.transform.Find("Arms").transform.Find("Arm1").GetComponent<Image>().color = __instance.dummyAgent.agentHitboxScript.skinColor;

						if (__instance.dummyAgent.hasSpecialAbilityArm2)
						{
							string invItemName4 = __instance.dummyAgent.inventory.equippedSpecialAbility.invItemName;
							gameObject2.transform.Find("Arms").transform.Find("Arm2").GetComponent<Image>().color = Color.white;

							if (invItemName4 == "WaterCannon" || invItemName4 == "LaserGun")
								gameObject2.transform.Find("Arms").transform.Find("Arm2").GetComponent<RectTransform>().sizeDelta = new Vector2(64f, 64f);
							else
								gameObject2.transform.Find("Arms").transform.Find("Arm2").GetComponent<RectTransform>().sizeDelta = new Vector2(16f, 16f);

							gameObject2.transform.Find("Arms").transform.Find("Arm2").GetComponent<Image>().sprite = __instance.gr.itemDic[invItemName4 + "Melee"];
						}
						else
						{
							gameObject2.transform.Find("Arms").transform.Find("Arm2").GetComponent<Image>().color = __instance.dummyAgent.agentHitboxScript.skinColor;
							gameObject2.transform.Find("Arms").transform.Find("Arm2").GetComponent<RectTransform>().sizeDelta = new Vector2(16f, 16f);
							gameObject2.transform.Find("Arms").transform.Find("Arm2").GetComponent<Image>().sprite = __instance.gr.itemDic["Arm"];
						}
					}
					else
					{
						gameObject2.transform.Find("Arms").transform.Find("Arm1").GetComponent<Image>().color = __instance.skinColor32[curPlayer.isPlayer - 1, num];

						if (__instance.dummyAgent.hasSpecialAbilityArm2)
						{
							string invItemName5 = __instance.dummyAgent.inventory.equippedSpecialAbility.invItemName;
							gameObject2.transform.Find("Arms").transform.Find("Arm2").GetComponent<Image>().color = Color.white;

							if (invItemName5 == "WaterCannon" || invItemName5 == "LaserGun")
								gameObject2.transform.Find("Arms").transform.Find("Arm2").GetComponent<RectTransform>().sizeDelta = new Vector2(64f, 64f);
							else
								gameObject2.transform.Find("Arms").transform.Find("Arm2").GetComponent<RectTransform>().sizeDelta = new Vector2(16f, 16f);

							gameObject2.transform.Find("Arms").transform.Find("Arm2").GetComponent<Image>().sprite = __instance.gr.itemDic[invItemName5 + "Melee"];
						}
						else
						{
							gameObject2.transform.Find("Arms").transform.Find("Arm2").GetComponent<Image>().color = __instance.skinColor32[curPlayer.isPlayer - 1, num];
							gameObject2.transform.Find("Arms").transform.Find("Arm2").GetComponent<RectTransform>().sizeDelta = new Vector2(16f, 16f);
							gameObject2.transform.Find("Arms").transform.Find("Arm2").GetComponent<Image>().sprite = __instance.gr.itemDic["Arm"];
						}
					}

					if (mySlotAgentType == "ShapeShifter")
					{
						gameObject2.transform.Find("Legs").transform.Find("Leg1").GetComponent<Image>().color = __instance.skinColor32[curPlayer.isPlayer - 1, num];
						gameObject2.transform.Find("Legs").transform.Find("Leg2").GetComponent<Image>().color = __instance.skinColor32[curPlayer.isPlayer - 1, num];
					}
					else
					{
						gameObject2.transform.Find("Legs").transform.Find("Leg1").GetComponent<Image>().color = gameObject.transform.Find("Legs").transform.Find("Leg1").GetComponent<Image>().color;
						gameObject2.transform.Find("Legs").transform.Find("Leg2").GetComponent<Image>().color = gameObject.transform.Find("Legs").transform.Find("Leg2").GetComponent<Image>().color;
					}

					if (__instance.dummyAgent.statusEffects.hasTrait("RollerSkates"))
					{
						gameObject2.transform.Find("Legs").transform.Find("Leg1").transform.Find("Footwear1").gameObject.SetActive(true);
						gameObject2.transform.Find("Legs").transform.Find("Leg2").transform.Find("Footwear2").gameObject.SetActive(true);
						gameObject2.transform.Find("Legs").transform.Find("Leg1").transform.Find("Footwear1").GetComponent<Image>().color = __instance.dummyAgent.agentHitboxScript.footwearColor;
						gameObject2.transform.Find("Legs").transform.Find("Leg2").transform.Find("Footwear2").GetComponent<Image>().color = __instance.dummyAgent.agentHitboxScript.footwearColor;
					}
					else
					{
						gameObject2.transform.Find("Legs").transform.Find("Leg1").transform.Find("Footwear1").gameObject.SetActive(false);
						gameObject2.transform.Find("Legs").transform.Find("Leg2").transform.Find("Footwear2").gameObject.SetActive(false);
					}

					if (mySlotAgentType == "ShapeShifter")
						gameObject2.transform.Find("Head").transform.Find("Eyes").GetComponent<Image>().color = Color.red;
					else if (mySlotAgentType == "Zombie")
						gameObject2.transform.Find("Head").transform.Find("Eyes").GetComponent<Image>().color = Color.yellow;
					else
						gameObject2.transform.Find("Head").transform.Find("Eyes").GetComponent<Image>().color = __instance.dummyAgent.agentHitboxScript.eyesColor;

					string text7 = "Eyes";

					if (__instance.dummyAgent.agentName == "Custom")
					{
						text7 = __instance.dummyAgent.customCharacterData.eyesType;

						if (text7 == "" || text7 == null)
							text7 = "Eyes";
					}
					else if (__instance.dummyAgent.agentName == "Cannibal")
						text7 = "EyesCannibal";
					else if (__instance.dummyAgent.agentName == "Zombie")
						text7 = "EyesZombie";

					if (text7 == "EyesCannibal")
					{
						gameObject2.transform.Find("Head").transform.Find("Head").GetComponent<Image>().sprite = __instance.gr.headDic["HeadCannibal"];
						gameObject2.transform.Find("Head").transform.Find("Eyes").GetComponent<Image>().sprite = __instance.gr.eyesDic["EyesCannibalS"];
					}
					else if (text7 == "EyesZombie")
					{
						gameObject2.transform.Find("Head").transform.Find("Head").GetComponent<Image>().sprite = __instance.gr.headDic["Head"];
						gameObject2.transform.Find("Head").transform.Find("Eyes").GetComponent<Image>().sprite = __instance.gr.eyesDic["EyesZombieS"];
					}
					else
					{
						gameObject2.transform.Find("Head").transform.Find("Head").GetComponent<Image>().sprite = __instance.gr.headDic["Head"];
						gameObject2.transform.Find("Head").transform.Find("Eyes").GetComponent<Image>().sprite = __instance.gr.eyesDic[text7 + "S"];
					}

					gameObject2.transform.Find("Head").transform.Find("HeadPiece").gameObject.SetActive(gameObject.transform.Find("Head").transform.Find("HeadPiece").gameObject.activeSelf);
					gameObject2.transform.Find("Head").transform.Find("HeadPiece").gameObject.SetActive(gameObject.transform.Find("Head").transform.Find("HeadPiece").gameObject.activeSelf);
					gameObject2.transform.Find("Head").transform.Find("HeadPiece").GetComponent<Image>().sprite = gameObject.transform.Find("Head").transform.Find("HeadPiece").GetComponent<Image>().sprite;
					bool flag3 = false;

					using (List<RandomElement>.Enumerator enumerator = __instance.dummyAgent.agentHitboxScript.agent.gc.scriptObject.GetComponent<RandomSelection>().randomListTableStatic["CantShowUnderHeadPiece"].elementList.GetEnumerator())
					{
						while (enumerator.MoveNext())
							if (enumerator.Current.rName == text6)
							{
								flag3 = true;
								break;
							}
					}

					if (__instance.armorBehindHair[num2] && !flag8)
					{
						gameObject2.transform.Find("Head").transform.Find("Head").transform.SetParent(gameObject2.transform.Find("Body").transform);
						gameObject2.transform.Find("Body").transform.Find("Head").transform.SetParent(gameObject2.transform.Find("Head").transform);
						gameObject2.transform.Find("Head").transform.Find("Eyes").transform.SetParent(gameObject2.transform.Find("Body").transform);
						gameObject2.transform.Find("Body").transform.Find("Eyes").transform.SetParent(gameObject2.transform.Find("Head").transform);
						gameObject2.transform.Find("Head").transform.Find("HeadPiece").transform.SetParent(gameObject2.transform.Find("Body").transform);
						gameObject2.transform.Find("Body").transform.Find("HeadPiece").transform.SetParent(gameObject2.transform.Find("Head").transform);
						gameObject2.transform.Find("Head").transform.Find("Hair").transform.SetParent(gameObject2.transform.Find("Body").transform);
						gameObject2.transform.Find("Body").transform.Find("Hair").transform.SetParent(gameObject2.transform.Find("Head").transform);
						gameObject2.transform.Find("Head").transform.Find("FacialHair").transform.SetParent(gameObject2.transform.Find("Body").transform);
						gameObject2.transform.Find("Body").transform.Find("FacialHair").transform.SetParent(gameObject2.transform.Find("Head").transform);
					}
					else
					{
						gameObject2.transform.Find("Head").transform.Find("Head").transform.SetParent(gameObject2.transform.Find("Body").transform);
						gameObject2.transform.Find("Body").transform.Find("Head").transform.SetParent(gameObject2.transform.Find("Head").transform);
						gameObject2.transform.Find("Head").transform.Find("Eyes").transform.SetParent(gameObject2.transform.Find("Body").transform);
						gameObject2.transform.Find("Body").transform.Find("Eyes").transform.SetParent(gameObject2.transform.Find("Head").transform);
						gameObject2.transform.Find("Head").transform.Find("Hair").transform.SetParent(gameObject2.transform.Find("Body").transform);
						gameObject2.transform.Find("Body").transform.Find("Hair").transform.SetParent(gameObject2.transform.Find("Head").transform);
						gameObject2.transform.Find("Head").transform.Find("FacialHair").transform.SetParent(gameObject2.transform.Find("Body").transform);
						gameObject2.transform.Find("Body").transform.Find("FacialHair").transform.SetParent(gameObject2.transform.Find("Head").transform);
						gameObject2.transform.Find("Head").transform.Find("HeadPiece").transform.SetParent(gameObject2.transform.Find("Body").transform);
						gameObject2.transform.Find("Body").transform.Find("HeadPiece").transform.SetParent(gameObject2.transform.Find("Head").transform);
					}

					if (__instance.dummyAgent.inventory.defaultArmorHead != null)
					{
						if (__instance.dummyAgent.inventory.defaultArmorHead.cantShowHairAtAll)
							__instance.slotAgent[n].transform.Find("Head").transform.Find("Hair").GetComponent<Image>().sprite = __instance.gr.gui[0];
						else if (flag3 && __instance.dummyAgent.inventory.defaultArmorHead.cantShowHair)
							gameObject2.transform.Find("Head").transform.Find("Hair").GetComponent<Image>().sprite = __instance.gr.hairDic[text6 + "S_U"];
					}

					__instance.dummyAgent.agentHitboxScript.canHaveFacialHair = false;
					__instance.dummyAgent.inventory.DestroyAllItems();
					__instance.dummyAgent.inventory.startingHeadPiece = "";
					__instance.dummyAgent.inventory.equippedSpecialAbility = null;
					__instance.dummyAgent.inventory.defaultArmorHead = null;
					__instance.dummyAgent.statusEffects.TraitList.Clear();
					__instance.dummyAgent.SetupAgentStats("");
					__instance.characterSelectStatsText[curPlayer.isPlayer - 1].text = "";

					if (GC.multiplayerMode && !GC.sessionDataBig.passwordProtection && !GC.debugMode && __instance.agent.controllerType == "Gamepad" && __instance.agent.mainGUI.curSelected != null && __instance.agent.mainGUI.curSelected.GetComponent<InvSlot>() != null)
					{
						int slotNumber = __instance.agent.mainGUI.curSelected.GetComponent<InvSlot>().slotNumber;

						if (slotNumber >= 32 && GC.multiplayerMode && !GC.sessionDataBig.passwordProtection && GC.sessionDataBig.customCharacterSlotsDetails[slotNumber - 32] != null && GC.sessionDataBig.customCharacterSlotsDetails[slotNumber - 32].exceededPoints)
							__instance.characterSelectStatsText[curPlayer.isPlayer - 1].text = "<color=orange>" + GC.nameDB.GetName("OnlyPasswordProtected", "Interface") + "</color>\n\n";
					}

					if (__instance.dummyAgent.customCharacterData != null && __instance.dummyAgent.customCharacterData.exceededPoints)
					{
						Text text8 = __instance.characterSelectStatsText[curPlayer.isPlayer - 1];
						text8.text = text8.text + "<color=red>" + GC.nameDB.GetName("ExceededPoints", "Interface") + "</color>\n\n";
					}

					__instance.bigSlotText[curPlayer.isPlayer - 1].text = __instance.dummyAgent.agentRealName;

					if (GC.fourPlayerMode)
					{
						string text9 = __instance.dummyAgent.agentName;

						if (text9 == "UpperCruster")
							text9 = "Hobo";
						else if (text9 == "Cop2")
							text9 = "Cop";
						else if (text9 == "Guard2")
							text9 = "Guard";

						if (GC.unlocks.isBigQuestCompleted(text9))
						{
							Text text10 = __instance.characterSelectStatsText[curPlayer.isPlayer - 1];
							text10.text = text10.text + "<color=lime>" + GC.nameDB.GetName("BigQuestCompleted", "Interface") + "</color>\n\n";
						}
					}

					Text text11 = __instance.characterSelectStatsText[curPlayer.isPlayer - 1];
					text11.text = text11.text + "<color=yellow>- " + GC.nameDB.GetName("Stats", "Interface") + " - </color>\n";
					Text text12 = __instance.characterSelectStatsText[curPlayer.isPlayer - 1];
					text12.text = string.Concat(new object[]
					{
						text12.text,
						GC.nameDB.GetName("Endurance", "Interface"),
						": ",
						__instance.dummyAgent.enduranceStatMod + 1,
						"/4\n"
					});

					if (__instance.dummyAgent.statusEffects.hasTrait("Rechargeable"))
					{
						text12 = __instance.characterSelectStatsText[curPlayer.isPlayer - 1];
						text12.text = string.Concat(new string[]
						{
							text12.text,
							GC.nameDB.GetName("Speed", "Interface"),
							": ",
							GC.nameDB.GetName("Rechargeable", "StatusEffect"),
							"\n"
						});
					}
					else if (__instance.dummyAgent.statusEffects.hasTrait("RollerSkates"))
					{
						text12 = __instance.characterSelectStatsText[curPlayer.isPlayer - 1];
						text12.text = string.Concat(new string[]
						{
							text12.text,
							GC.nameDB.GetName("Speed", "Interface"),
							": ",
							GC.nameDB.GetName("RollerSkates", "StatusEffect"),
							"\n"
						});
					}
					else
					{
						text12 = __instance.characterSelectStatsText[curPlayer.isPlayer - 1];
						text12.text = string.Concat(new object[]
						{
							text12.text,
							GC.nameDB.GetName("Speed", "Interface"),
							": ",
							__instance.dummyAgent.speedStatMod + 1,
							"/4\n"
						});
					}
					if (__instance.dummyAgent.statusEffects.hasTrait("Rechargeable"))
					{
						text12 = __instance.characterSelectStatsText[curPlayer.isPlayer - 1];
						text12.text = string.Concat(new string[]
						{
							text12.text,
							GC.nameDB.GetName("Strength", "Interface"),
							": ",
							GC.nameDB.GetName("Rechargeable", "StatusEffect"),
							"\n"
						});
					}
					else if (__instance.dummyAgent.statusEffects.hasTrait("CantAttack"))
					{
						text12 = __instance.characterSelectStatsText[curPlayer.isPlayer - 1];
						text12.text = string.Concat(new string[]
						{
							text12.text,
							GC.nameDB.GetName("Strength", "Interface"),
							": ",
							GC.nameDB.GetName("CantAttack", "StatusEffect"),
							"\n"
						});
					}
					else if (__instance.dummyAgent.statusEffects.hasTrait("AttacksOneDamage"))
					{
						text12 = __instance.characterSelectStatsText[curPlayer.isPlayer - 1];
						text12.text = string.Concat(new string[]
						{
							text12.text,
							GC.nameDB.GetName("Strength", "Interface"),
							": ",
							GC.nameDB.GetName("AttacksOneDamage", "StatusEffect"),
							"\n"
						});
					}
					else
					{
						text12 = __instance.characterSelectStatsText[curPlayer.isPlayer - 1];
						text12.text = string.Concat(new object[]
						{
							text12.text,
							GC.nameDB.GetName("Strength", "Interface"),
							": ",
							__instance.dummyAgent.strengthStatMod + 1,
							"/4\n"
						});
					}
					if (__instance.dummyAgent.statusEffects.hasTrait("Rechargeable"))
					{
						text12 = __instance.characterSelectStatsText[curPlayer.isPlayer - 1];
						text12.text = string.Concat(new string[]
						{
							text12.text,
							GC.nameDB.GetName("Accuracy", "Interface"),
							": ",
							GC.nameDB.GetName("Rechargeable", "StatusEffect"),
							"\n"
						});
					}
					else if (__instance.dummyAgent.statusEffects.hasTrait("CantAttack"))
					{
						text12 = __instance.characterSelectStatsText[curPlayer.isPlayer - 1];
						text12.text = string.Concat(new string[]
						{
							text12.text,
							GC.nameDB.GetName("Accuracy", "Interface"),
							": ",
							GC.nameDB.GetName("CantAttack", "StatusEffect"),
							"\n"
						});
					}
					else if (__instance.dummyAgent.statusEffects.hasTrait("AttacksOneDamage"))
					{
						text12 = __instance.characterSelectStatsText[curPlayer.isPlayer - 1];
						text12.text = string.Concat(new string[]
						{
							text12.text,
							GC.nameDB.GetName("Accuracy", "Interface"),
							": ",
							GC.nameDB.GetName("AttacksOneDamage", "StatusEffect"),
							"\n"
						});
					}
					else
					{
						text12 = __instance.characterSelectStatsText[curPlayer.isPlayer - 1];
						text12.text = string.Concat(new object[]
						{
							text12.text,
							GC.nameDB.GetName("Accuracy", "Interface"),
							": ",
							__instance.dummyAgent.accuracyStatMod + 1,
							"/4\n"
						});
					}

					if (__instance.dummyAgent.inventory.equippedSpecialAbility != null)
					{
						Text text13 = __instance.characterSelectStatsText[curPlayer.isPlayer - 1];
						text13.text = text13.text + "\n<color=yellow>- " + GC.nameDB.GetName("SpecialAbility", "Interface") + " - </color>\n";
						text12 = __instance.characterSelectStatsText[curPlayer.isPlayer - 1];
						text12.text = string.Concat(new string[]
						{
							text12.text,
							"<color=cyan>",
							GC.nameDB.GetName(__instance.dummyAgent.inventory.equippedSpecialAbility.invItemName, "Item"),
							"</color>\n<color=white>",
							GC.nameDB.GetName(__instance.dummyAgent.inventory.equippedSpecialAbility.invItemName, "Description"),
							"</color>\n"
						});
					}

					if (__instance.dummyAgent.statusEffects.TraitList.Count > 0)
					{
						Text text14 = __instance.characterSelectStatsText[curPlayer.isPlayer - 1];
						text14.text = text14.text + "\n<color=yellow>- " + GC.nameDB.GetName("Traits", "Interface") + " - </color>\n";

						foreach (Trait trait in __instance.dummyAgent.statusEffects.TraitList)
						{
							if (TraitManager.AllCCUTraitNamesGroup.Contains(trait.traitName)) // Omit hidden traits
								continue;

							text12 = __instance.characterSelectStatsText[curPlayer.isPlayer - 1];

							text12.text = string.Concat(new string[]
							{
								text12.text,
								"<color=cyan>",
								GC.nameDB.GetName(trait.traitName, "StatusEffect"),
								"</color>\n<color=white>",
								GC.nameDB.GetName(trait.traitName, "Description"),
								"</color>\n"
							});
						}
					}

					bool flag9 = false;

					foreach (InvItem invItem in __instance.dummyAgent.inventory.InvItemList)
						if (invItem.invItemName != null && invItem.invItemName != "")
							flag9 = true;

					if (flag9)
					{
						Text text15 = __instance.characterSelectStatsText[curPlayer.isPlayer - 1];
						text15.text = text15.text + "\n<color=yellow>- " + GC.nameDB.GetName("StartingItems", "Interface") + " - </color>\n";

						foreach (InvItem invItem2 in __instance.dummyAgent.inventory.InvItemList)
						{
							if (invItem2.invItemName != null && invItem2.invItemName != "")
							{
								Text text16 = __instance.characterSelectStatsText[curPlayer.isPlayer - 1];
								text16.text += invItem2.invItemRealName;

								if (invItem2.invItemCount > 1 && invItem2.itemType != "WeaponProjectile" && invItem2.itemType != "WeaponMelee" && !invItem2.isArmor && !invItem2.isArmorHead)
								{
									text12 = __instance.characterSelectStatsText[curPlayer.isPlayer - 1];
									text12.text = string.Concat(new object[]
									{
										text12.text,
										" (",
										invItem2.invItemCount,
										")\n"
									});
								}
								else
								{
									Text text17 = __instance.characterSelectStatsText[curPlayer.isPlayer - 1];
									text17.text += "\n";
								}
							}
						}
					}

					if (__instance.dummyAgent.agentName == "Custom")
						__instance.characterSelectDescriptionText[curPlayer.isPlayer - 1].text = __instance.dummyAgent.customCharacterData.characterDescription;
					else
						__instance.characterSelectDescriptionText[curPlayer.isPlayer - 1].text = GC.nameDB.GetName(__instance.dummyAgent.agentName, "Description") + "\n";

					__instance.characterSelectQuestBigText[curPlayer.isPlayer - 1].text = GC.nameDB.GetName(__instance.dummyAgent.agentName + "_BQ", "Unlock") + "\n" + GC.nameDB.GetName("D_" + __instance.dummyAgent.agentName + "_BQ", "Unlock");

					if ((GC.challenges.Contains("SuperSpecialCharacters") && __instance.dummyAgent.oma.superSpecialAbility) || __instance.dummyAgent.agentName == "Cop2" || __instance.dummyAgent.agentName == "Guard2" || __instance.dummyAgent.agentName == "UpperCruster")
					{
						__instance.characterSelectQuestBigText[curPlayer.isPlayer - 1].text = "<color=magenta>" + GC.nameDB.GetName("SuperSpecialAbility", "Interface") + "</color>";
						string text18 = __instance.dummyAgent.agentName;

						if (text18 == "Cop2")
							text18 = "Cop";
						else if (text18 == "Guard2")
							text18 = "Guard";
						else if (text18 == "UpperCruster")
							text18 = "Hobo";

						Text text19 = __instance.characterSelectQuestBigText[curPlayer.isPlayer - 1];
						text19.text = text19.text + "\n" + GC.nameDB.GetName("BQC_" + text18, "Unlock");
					}
					else
					{
						string text20 = __instance.dummyAgent.agentName;
						bool flag10 = GC.unlocks.IsUnlocked(text20, "Agent");
						__instance.characterSelectQuestBigText[curPlayer.isPlayer - 1].text = "<color=yellow>- " + GC.nameDB.GetName("BigQuest", "Interface") + " -</color>";

						if (text20 == "Cop2")
							text20 = "Cop";
						else if (text20 == "Guard2")
							text20 = "Guard";
						else if (text20 == "UpperCruster")
							text20 = "Hobo";

						string text21 = text20;

						if (text20 == "Custom")
						{
							text21 = __instance.dummyAgent.bigQuest;
							flag10 = true;
						}

						string name = GC.nameDB.GetName(text21 + "_BQ", "Unlock");

						if (GC.freeVersion)
						{
							Text text22 = __instance.characterSelectQuestBigText[curPlayer.isPlayer - 1];
							text22.text = text22.text + "\n(" + GC.nameDB.GetName("SteamOnlyShort", "Interface") + ")";
						}
						else if (name.Contains("E_"))
						{
							if (text20 != "Custom")
							{
								Text text23 = __instance.characterSelectQuestBigText[curPlayer.isPlayer - 1];
								text23.text = text23.text + "\n(" + GC.nameDB.GetName("ComingSoon", "Interface") + ")";
							}
							else
							{
								__instance.characterSelectQuestBigText[curPlayer.isPlayer - 1].text = "";
							}
						}
						else if (!flag10)
							__instance.characterSelectQuestBigText[curPlayer.isPlayer - 1].text = "";
						else
						{
							Text text24 = __instance.characterSelectQuestBigText[curPlayer.isPlayer - 1];
							text24.text = text24.text + "\n<color=cyan>" + name + "</color>";

							if (GC.unlocks.isBigQuestCompleted(text21))
							{
								Text text25 = __instance.characterSelectQuestBigText[curPlayer.isPlayer - 1];
								text25.text = text25.text + "\n<color=lime>" + GC.nameDB.GetName("QuestCompleted", "Interface") + "</color>";
							}

							Text text26 = __instance.characterSelectQuestBigText[curPlayer.isPlayer - 1];
							text26.text = text26.text + "\n" + GC.nameDB.GetName("D_" + text21 + "_BQ", "Unlock");
						}
					}

					if (GC.freeVersion && !__instance.unlockedInFreeVersion.Contains(__instance.dummyAgent.agentName))
						__instance.characterSelectStatsText[curPlayer.isPlayer - 1].text = "<color=yellow>- " + GC.nameDB.GetName("SteamOnlyShort", "Interface") + " - </color>\n";
					else if (!GC.freeVersion && !GC.unlocks.IsUnlocked(__instance.dummyAgent.agentName, "Agent") && __instance.dummyAgent.agentName != "Custom")
					{
						__instance.characterSelectStatsText[curPlayer.isPlayer - 1].text = "<color=yellow>- " + GC.nameDB.GetName("Prerequisites", "Unlock") + " - </color>\n" + GC.unlocks.GetSpecialUnlockInfo(__instance.dummyAgent.agentName, GC.unlocks.GetUnlock(__instance.dummyAgent.agentName, "Agent"));
					}

					__instance.selectedInvSlot[curPlayer.isPlayer - 1] = gameObject.transform.parent.GetComponent<InvSlot>();

					if (curPlayer.controllerType == "Keyboard" || curPlayer.controllerType == "")
						curPlayer.mainGUI.invInterface.selectionBoxMainTr.position = __instance.selectedInvSlot[curPlayer.isPlayer - 1].tr.position;

					if (__instance.dummyAgent.agentName == "Custom")
					{
						__instance.curSelectedNum[curPlayer.isPlayer - 1] = __instance.selectedInvSlot[curPlayer.isPlayer - 1].slotNumber;

						return false;
					}
				}
				else if (n == 48 && mySlotAgentType == "" && (curPlayer.controllerType != "Keyboard" || ___removingCustomCharacter))
				{
					GameObject gameObject3 = __instance.slotAgent[n + (curPlayer.isPlayer - 1)];
					gameObject3.transform.Find("Body").gameObject.SetActive(false);
					gameObject3.transform.Find("Head").transform.Find("Head").gameObject.SetActive(false);
					gameObject3.transform.Find("Head").transform.Find("Hair").gameObject.SetActive(false);
					gameObject3.transform.Find("Head").transform.Find("FacialHair").gameObject.SetActive(false);
					gameObject3.transform.Find("Head").transform.Find("Eyes").gameObject.SetActive(false);
					gameObject3.transform.Find("Head").transform.Find("HeadPiece").gameObject.SetActive(false);
					gameObject3.transform.Find("Arms").transform.Find("Arm1").gameObject.SetActive(false);
					gameObject3.transform.Find("Arms").transform.Find("Arm2").gameObject.SetActive(false);
					gameObject3.transform.Find("Legs").transform.Find("Leg1").gameObject.SetActive(false);
					gameObject3.transform.Find("Legs").transform.Find("Leg2").gameObject.SetActive(false);
					__instance.bigSlotText[curPlayer.isPlayer - 1].text = "";

					if (__instance.agent.mainGUI.curSelected.GetComponent<InvSlot>().slotNumber >= 32)
						__instance.characterSelectStatsText[curPlayer.isPlayer - 1].text = GC.nameDB.GetName("CreateMe", "Interface");
					else
						__instance.characterSelectStatsText[curPlayer.isPlayer - 1].text = "";

					__instance.characterSelectDescriptionText[curPlayer.isPlayer - 1].text = "";

					if (GC.multiplayerMode && !GC.sessionDataBig.passwordProtection && !GC.debugMode && __instance.agent.controllerType == "Gamepad" && __instance.agent.mainGUI.curSelected != null && __instance.agent.mainGUI.curSelected.GetComponent<InvSlot>() != null)
					{
						int slotNumber2 = __instance.agent.mainGUI.curSelected.GetComponent<InvSlot>().slotNumber;

						if (slotNumber2 >= 32 && GC.multiplayerMode && !GC.sessionDataBig.passwordProtection && GC.sessionDataBig.customCharacterSlotsDetails[slotNumber2 - 32] != null && GC.sessionDataBig.customCharacterSlotsDetails[slotNumber2 - 32].exceededPoints)
							__instance.characterSelectStatsText[curPlayer.isPlayer - 1].text = "<color=orange>" + GC.nameDB.GetName("OnlyPasswordProtected", "Interface") + "</color>";
					}

					if (GC.freeVersion && __instance.agent.controllerType == "Gamepad" && __instance.agent.mainGUI.curSelected != null && __instance.agent.mainGUI.curSelected.GetComponent<InvSlot>() != null && __instance.agent.mainGUI.curSelected.GetComponent<InvSlot>().slotNumber >= 32)
					{
						__instance.characterSelectStatsText[curPlayer.isPlayer - 1].text = string.Concat(new string[]
						{
							"<color=orange>",
							GC.nameDB.GetName("CharacterCreation", "Interface"),
							"\n",
							GC.nameDB.GetName("SteamOnly", "Interface"),
							"</color>"
						});
					}

					__instance.characterSelectQuestBigText[curPlayer.isPlayer - 1].text = "";
					__instance.selectedInvSlot[curPlayer.isPlayer - 1] = null;
					__instance.MakeSelectablesInteractable(curPlayer.isPlayer - 1, false);
				}
			}

			return false;
		}
	}
}
