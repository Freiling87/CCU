using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using BunnyLibs;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UI;

namespace CCU.Systems.Containers
{
	class Containers
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static List<string> ContainerObjects_Slot1 = new List<string>()
		{
			// NOTE: Before adding any, ensure that you've accounted for Hidden Bombs, since they'll all become eligible.
			VObjectReal.Barbecue,
			VObjectReal.Bathtub,
			VObjectReal.Bed,
			// VObjectReal.Crate,	Likely has special rules that will need attention
			VObjectReal.Desk,
			VObjectReal.Fireplace,
			VObjectReal.FlamingBarrel,
			VObjectReal.GasVent,	// Require screwdriver
			//VObjectReal.Manhole,	// Need SORCE's code here
			VObjectReal.Plant,
			// VObjectReal.Podium,	Investigateable
			VObjectReal.PoolTable,
			VObjectReal.Refrigerator,
			VObjectReal.Shelf,
			//VObjectReal.SlimeBarrel,	Poison looter
			VObjectReal.Stove,
			VObjectReal.Toilet,
			VObjectReal.TrashCan,
			VObjectReal.Tube,
			VObjectReal.VendorCart,
			VObjectReal.WaterPump,
			VObjectReal.Well,
		};
		public static List<string> FireParticleEffectObjects = new List<string>()
		{
			VObjectReal.Barbecue,
			VObjectReal.Fireplace,
			VObjectReal.FlamingBarrel,
		};

		public enum ContainerValues
		{
			Hidden,
			// Desk is only lockable of the above. These are mostly pointless.
			//Hidden_Locked,
			//Hidden_Locked_Keycoded,
			//Hidden_Keycoded,
			//Locked,
			//Locked_Keycoded,
			//Keycoded,
			None
		}

		public static string MagicObjectName(string originalName) =>
			IsContainer(originalName)
				? VObjectReal.ChestBasic
				: originalName;

		[RLSetup]
		public static void Setup()
		{
			SetupText();

			RogueInteractions.CreateProvider(h =>
			{
				if (IsContainer(h.Object.objectName) && !h.Helper.interactingFar && !h.Object.objectInvDatabase.isEmpty())
				{
					if (h.HasButton(VButtonText.Open))
						h.RemoveButton(VButtonText.Open);

					if (h.Object is VendorCart)
						h.AddButton(CButtonText.Ransack, m =>
						{
							if (!m.Agent.statusEffects.hasTrait(VanillaTraits.SneakyFingers))
							{
								GC.audioHandler.Play(m.Agent, VanillaAudio.Operating);
								GC.spawnerMain.SpawnNoise(m.Object.tr.position, 0.4f, m.Agent, "Normal", m.Agent);
								GC.OwnCheck(m.Agent, m.Object.go, "Normal", 2);
							}

							m.StartOperating(2f, true, COperatingBarText.Ransacking);
						});
					else
						h.AddImplicitButton(CButtonText.Container_Open, m =>
						{
							TryOpenChest(m.Object, m.Agent);
						});
				}
			});
		}

		public const string
			CantAccessContainer_TooHot = "CantAccessContainer_TooHot",
			CantAccessContainer_ToiletShidded = "CantAccessContainer_ToiletShidded",
			CantAccessContainer_TubeFunctional = "CantAccessContainer_TubeFunctional",

			NoMoreSemicolon = "";

		public static void SetupText()
		{
			string t = NameTypes.Interface;
			RogueLibs.CreateCustomName(CButtonText.Container_Open, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Search",
			});
			RogueLibs.CreateCustomName(CButtonText.Ransack, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Ransack",
			});
			RogueLibs.CreateCustomName(COperatingBarText.Ransacking, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Ransacking",
			});

			t = NameTypes.Dialogue;
			// Stash hint mockups. These three are not implemented yet, so no need to translate if you want to skip them.
			// Dialogue is spoken on item pickup, shown in an Investigation window if item activated.
			//RogueLibs.CreateCustomName("StashHint_Barbecue_01", t, new CustomNameInfo
			//{
			//	[LanguageCode.English] = "I won't make it for Christmas this year, sorry. You won't end up with a lump of coal, at least. But you'll probably have to dig through some to find your gift. Hope you were good this year!",
			//});
			//RogueLibs.CreateCustomName("StashHint_Barbecue_02", t, new CustomNameInfo
			//{
			//	[LanguageCode.English] = "The cops ransacked the place again this week, and they looked hungry for evidence. Well, unless they're hungry for some pork ribs they're not gonna find what they're looking for. HINT HINT.",
			//});
			//RogueLibs.CreateCustomName("StashHint_Bathtub_01", t, new CustomNameInfo
			//{
			//	[LanguageCode.English] = "That thing we stole... it's too hot to sell yet, even to a fence. Let's stay out of prison for now. I hid it somewhere where it's safe to drop the soap.",
			//});

			// Inaccessible Containers
			RogueLibs.CreateCustomName(CantAccessContainer_ToiletShidded, t, new CustomNameInfo
			{
				[LanguageCode.English] = "It's too shiddy!",
			});
			RogueLibs.CreateCustomName(CantAccessContainer_TooHot, t, new CustomNameInfo
			{
				[LanguageCode.English] = "It's too hot to touch!",
			});
			RogueLibs.CreateCustomName(CantAccessContainer_TubeFunctional, t, new CustomNameInfo
			{
				[LanguageCode.English] = "It's still running, and I want to keep all my limbs.",
			});
		}

		public static void TryOpenChest(PlayfieldObject playfieldObject, Agent agent)
		{
			bool isHot =
				(FireParticleEffectObjects.Contains(playfieldObject.objectName) && playfieldObject.ora.hasParticleEffect) ||
				(playfieldObject is FlameGrate flameGrate && !(flameGrate.myFire is null));
			bool grabHotStuff =
				agent.HasTrait(VanillaTraits.FireproofSkin) ||
				agent.HasTrait(VanillaTraits.FireproofSkin2) ||
				agent.statusEffects.hasStatusEffect(VStatusEffect.ResistFire);

			if (isHot && !grabHotStuff)
			{
				agent.SayDialogue(CantAccessContainer_TooHot);
				return;
			}
			else if (playfieldObject is Tube tube && tube.functional)
			{
				agent.SayDialogue(CantAccessContainer_TubeFunctional);
				return;
			}

			playfieldObject.ShowChest();
		}

		public static bool IsContainer(string objectName) =>
			!(objectName is null) &&
			ContainerObjects_Slot1.Contains(objectName);

		public static bool IsContainer(ObjectReal objectReal) =>
			IsContainer(objectReal.objectName);
	}

	[HarmonyPatch(typeof(Computer))]
	public static class P_ComputerShutdown
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPriority(1000)]
		[HarmonyTranspiler, HarmonyPatch(nameof(Computer.MakeNonFunctional))]
		private static IEnumerable<CodeInstruction> ShutdownTube(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo interactingAgent = AccessTools.DeclaredField(typeof(PlayfieldObject), nameof(PlayfieldObject.interactingAgent));
			MethodInfo switchLinkOperate = AccessTools.DeclaredMethod(typeof(ObjectReal), nameof(ObjectReal.SwitchLinkOperate));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldstr, "FlameGrates"),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldstr, "Tubes"),
					new CodeInstruction(OpCodes.Ldstr, "Off"),
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld, interactingAgent),
					new CodeInstruction(OpCodes.Call, switchLinkOperate),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}

	[HarmonyPatch(typeof(InvDatabase))]
	public static class P_InvDatabase
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch("Awake")]
		public static bool Awake_Prefix(InvDatabase __instance)
		{
			string objectName = __instance.GetComponent<ObjectReal>()?.objectName ?? null;

			if (Containers.IsContainer(objectName))
				__instance.money = new InvItem();

			return true;
		}

		[HarmonyPostfix, HarmonyPatch(nameof(InvDatabase.FillChest), new[] { typeof(bool) })]
		private static void FillChest_Money(InvDatabase __instance)
		{
			if (!(__instance.objectReal is null)
				&& Containers.IsContainer(__instance.objectReal))
			{
				InvItem money = __instance.FindItem(VItemName.Money);

				if (!(money is null) && money.invItemCount == 0)
				{
					__instance.objectReal.chestMoneyTier = 2;
					money.invItemCount = __instance.FindMoneyAmt(true);
				}
			}
		}

		[HarmonyPrefix, HarmonyPatch(nameof(InvDatabase.isEmpty))]
		public static bool IsEmpty_Replacement(InvDatabase __instance, ref bool __result)
		{
			for (int i = 0; i < __instance.InvItemList.Count; i++)
			{
				InvItem invItem = __instance.InvItemList[i];

				if (!GC.nameDB.GetName(invItem.invItemName, "Item").Contains("E_"))
				{
					__result = false;
					return false;
				}
			}

			__result = true;
			return false;
		}
	}

	[HarmonyPatch(typeof(InvSlot))]
	public static class P_InvSlot
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(nameof(InvSlot.MoveFromChestToInventory))]
		public static void MoveFromChestToInventory_Postfix(InvSlot __instance)
		{
			// TODO: What's this about? Shouldn't they already be setup? Not even sure which system this is supposed to tie into.
			__instance.item.SetupDetails(true);
		}
	}

	[HarmonyPatch(typeof(LevelEditor))]
	public static class P_LevelEditor
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		// Pulls up ScrollingList when you click EVS field.
		[HarmonyTranspiler, HarmonyPatch(nameof(LevelEditor.PressedLoadExtraVarStringList), new Type[0] { })]
		public static IEnumerable<CodeInstruction> PressedLoadExtraVarStringList_EnableContainerControls(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo magicObjectName = AccessTools.DeclaredMethod(typeof(Containers), nameof(Containers.MagicObjectName));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_1),
					new CodeInstruction(OpCodes.Ldstr, "ChestBasic"),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_1),					//	Object real name
					new CodeInstruction(OpCodes.Call, magicObjectName),		//	"ChestBasic" if readable, or real name if not
					new CodeInstruction(OpCodes.Ldstr, "ChestBasic"),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		[HarmonyTranspiler, HarmonyPatch(nameof(LevelEditor.PressedScrollingMenuButton), new[] { typeof(ButtonHelper) })]
		public static IEnumerable<CodeInstruction> PressedScrollingMenuButton_OnChoice_ShowCustomInterface(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo deactivateLoadMenu = AccessTools.DeclaredMethod(typeof(LevelEditor), nameof(LevelEditor.DeactivateLoadMenu));
			MethodInfo showCustomInterface = AccessTools.DeclaredMethod(typeof(P_LevelEditor), nameof(P_LevelEditor.ShowCustomInterface));
			FieldInfo scrollingButtonType = AccessTools.DeclaredField(typeof(ButtonHelper), nameof(ButtonHelper.scrollingButtonType));
			MethodInfo setActive = AccessTools.DeclaredMethod(typeof(GameObject), nameof(GameObject.SetActive));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					// End of branch starting: else if (text == "LoadObject")
					new CodeInstruction(OpCodes.Ldc_I4_0),
					new CodeInstruction(OpCodes.Callvirt, setActive),
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Call, deactivateLoadMenu),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldarg_1),
					new CodeInstruction(OpCodes.Ldfld, scrollingButtonType),
					// This is putting "Shelf" as contents of Shelf
					new CodeInstruction(OpCodes.Call, showCustomInterface),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		[HarmonyTranspiler, HarmonyPatch(nameof(LevelEditor.UpdateInterface), new[] { typeof(bool) })]
		private static IEnumerable<CodeInstruction> UpdateInterface_OnSelect_ShowCustomInterface(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo oneOfEachSceneObject = AccessTools.DeclaredField(typeof(LevelEditor), "oneOfEachSceneObject");
			FieldInfo scrollingButtonType = AccessTools.DeclaredField(typeof(ButtonHelper), nameof(ButtonHelper.scrollingButtonType));
			MethodInfo showCustomInterface = AccessTools.DeclaredMethod(typeof(P_LevelEditor), nameof(P_LevelEditor.ShowCustomInterface));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld, oneOfEachSceneObject),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldloc_S, 9),
					new CodeInstruction(OpCodes.Call, showCustomInterface),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		private static void ShowCustomInterface(LevelEditor levelEditor, string tileNameText)
		{
			InputField tileNameObject = (InputField)AccessTools.Field(typeof(LevelEditor), "tileNameObject").GetValue(levelEditor);
			string objectName = tileNameObject.text;

			if (Containers.IsContainer(objectName))
			{
				InputField extraVarObject = (InputField)AccessTools.Field(typeof(LevelEditor), "extraVarObject").GetValue(levelEditor);
				extraVarObject.gameObject.SetActive(false);

				InputField extraVarStringObject = (InputField)AccessTools.Field(typeof(LevelEditor), "extraVarStringObject").GetValue(levelEditor); // √
				levelEditor.SetNameText(extraVarObject, extraVarObject.text, "Interface");
				extraVarStringObject.gameObject.SetActive(true);

				InputField extraVarString2Object = (InputField)AccessTools.Field(typeof(LevelEditor), "extraVarString2Object").GetValue(levelEditor);
				extraVarString2Object.gameObject.SetActive(false);

				InputField extraVarString3Object = (InputField)AccessTools.Field(typeof(LevelEditor), "extraVarString3Object").GetValue(levelEditor);
				extraVarString3Object.gameObject.SetActive(false);
			}
		}

		[HarmonyTranspiler, HarmonyPatch(nameof(LevelEditor.UpdateInterface), new[] { typeof(bool) })]
		private static IEnumerable<CodeInstruction> ShowExtraVarStringsForContainers(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo magicObjectName = AccessTools.DeclaredMethod(typeof(Containers), nameof(Containers.MagicObjectName));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_S, 34),
					new CodeInstruction(OpCodes.Ldstr, "ChestBasic"),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_S, 34),				//	Object real name
					new CodeInstruction(OpCodes.Call, magicObjectName),		//	"ChestBasic" if Container, or real name if not
					new CodeInstruction(OpCodes.Ldstr, "ChestBasic"),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}

	[HarmonyPatch(typeof(ObjectReal))]
	public static class P_ObjectReal_Containers
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(nameof(ObjectReal.FinishedOperating))]
		public static void FinishedOperating_Postfix(ObjectReal __instance)
		{
			if (!__instance.interactingAgent.interactionHelper.interactingFar)
			{
				if (__instance is VendorCart && __instance.operatingBarType == COperatingBarText.Ransacking)
					Containers.TryOpenChest(__instance, __instance.interactingAgent);
			}
		}

		[HarmonyPrefix, HarmonyPatch("Start", new Type[0] { })]
		public static bool Start_SetupInvDatabasesForContainers(ObjectReal __instance)
		{
			if (Containers.IsContainer(__instance.objectName))
			{
				if (__instance.GetComponent<InvDatabase>() is null)
					__instance.objectInvDatabase = __instance.go.AddComponent<InvDatabase>();

				__instance.chestReal = true;
			}

			return true;
		}

		[HarmonyTranspiler, HarmonyPatch(nameof(ObjectReal.SwitchLinkOperate))]
		private static IEnumerable<CodeInstruction> ShutdownObjects(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo switchOffObject = AccessTools.DeclaredMethod(typeof(P_ObjectReal_Containers), nameof(P_ObjectReal_Containers.SwitchOffObject));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Stloc_1),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldarg_1),
					new CodeInstruction(OpCodes.Ldarg_2),
					new CodeInstruction(OpCodes.Ldarg_3),
					new CodeInstruction(OpCodes.Ldloc_1),
					new CodeInstruction(OpCodes.Call, switchOffObject),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		private static void SwitchOffObject(ObjectReal instance, string type, string state, Agent myAgent, ObjectReal otherObject)
		{
			if (type == "Tubes"
				&& otherObject is Tube tube
				&& (tube.startingChunk == instance.startingChunk
					|| (tube.startingSector == instance.startingSector && instance.startingSector != 0)))
			{
				if (state == null)
					tube.ToggleFunctional();
				else if (state == "On")
					tube.MakeFunctional();
				else if (state == "Off")
					tube.MakeNonFunctional(null);
			}
		}
	}

	[HarmonyPatch(typeof(ObjectMultObject))]
	public static class P_ObjectMultObject
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(nameof(ObjectMultObject.OnDeserialize))]
		public static void OnDeserialize_Postfix(ObjectMultObject __instance)
		{
			MethodInfo getName = AccessTools.DeclaredMethod(typeof(ObjectMultObject), "GetName");

			try { getName.GetMethodWithoutOverrides<Action<string, string>>(__instance).Invoke(__instance.chestItem1, "Item"); }
			catch { __instance.chestItem1 = ""; }

			try { getName.GetMethodWithoutOverrides<Action<string, string>>(__instance).Invoke(__instance.chestItem2, "Item"); }
			catch { __instance.chestItem2 = ""; }

			try { getName.GetMethodWithoutOverrides<Action<string, string>>(__instance).Invoke(__instance.chestItem3, "Item"); }
			catch { __instance.chestItem3 = ""; }
		}
	}

	[HarmonyPatch(typeof(Quests))]
	public static class P_Quests
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		//	TODO: Sure this works? I don't remember it working
		[HarmonyTranspiler, HarmonyPatch(nameof(Quests.setupQuests))]
		private static IEnumerable<CodeInstruction> ScreenContainersForBombDisaster(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo chestDic = AccessTools.DeclaredField(typeof(GameController), nameof(GameController.chestDic));
			MethodInfo filteredChestDic = AccessTools.DeclaredMethod(typeof(P_Quests), nameof(P_Quests.FilteredChestDic));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 3,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, chestDic),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, filteredChestDic),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		private static Dictionary<int, ObjectReal> FilteredChestDic(Dictionary<int, ObjectReal> original)
		{
			Dictionary<int, ObjectReal> final =
				original
				.Where(pair => IsValidBombContainer(pair.Value))
				.ToDictionary(pair => pair.Key, pair => pair.Value);

			return final;
		}
		private static bool IsValidBombContainer(ObjectReal bombContainer)
		{
			if (bombContainer is Tube tube)
			{
				foreach (ObjectReal powerObject in GC.objectRealList)
					if ((powerObject is Computer computer
							&& (computer.startingChunk == tube.startingChunk
								|| (computer.startingSector == tube.startingSector && computer.startingSector != 0)))
						|| (powerObject is PowerBox powerBox
							&& AffectedChunks(powerBox).Contains(tube.startingChunk)))
						return true;

				return false;
			}

			return true;
		}
		// Adapted from PowerBox.ShutDown & PowerBox.AddAffectedChunk
		private static List<int> AffectedChunks(PowerBox powerBox)
		{
			float num = 10.24f;
			List<int> chunks = new List<int>();
			List<Vector2> ChunkNeighbors = new List<Vector2>()
			{
				new Vector2(powerBox.tr.position.x, powerBox.tr.position.y),
				new Vector2(powerBox.tr.position.x + num, powerBox.tr.position.y + num),
				new Vector2(powerBox.tr.position.x + num, powerBox.tr.position.y - num),
				new Vector2(powerBox.tr.position.x + num, powerBox.tr.position.y),
				new Vector2(powerBox.tr.position.x - num, powerBox.tr.position.y + num),
				new Vector2(powerBox.tr.position.x - num, powerBox.tr.position.y - num),
				new Vector2(powerBox.tr.position.x - num, powerBox.tr.position.y),
				new Vector2(powerBox.tr.position.x, powerBox.tr.position.y + num),
				new Vector2(powerBox.tr.position.x, powerBox.tr.position.y - num)
			};

			foreach (Vector2 vector2 in ChunkNeighbors)
			{
				int chunkID = powerBox.gc.tileInfo.GetTileData(new Vector2(vector2.x, vector2.y)).chunkID;

				if (!chunks.Contains(chunkID) && chunkID != 0)
					chunks.Add(chunkID);
			}

			return chunks;
		}
	}
}