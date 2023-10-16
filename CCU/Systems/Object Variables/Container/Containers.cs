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
			VanillaObjects.Barbecue,
			VanillaObjects.Bathtub,
			VanillaObjects.Bed,
			// VanillaObjects.Crate,	Likely has special rules that will need attention
			VanillaObjects.Desk,
			VanillaObjects.Fireplace,
			VanillaObjects.FlamingBarrel,
			VanillaObjects.GasVent,	// Require screwdriver
			//VanillaObjects.Manhole,	// Need SORCE's code here
			VanillaObjects.Plant,
			// VanillaObjects.Podium,	Investigateable
			VanillaObjects.PoolTable,
			VanillaObjects.Refrigerator,
			VanillaObjects.Shelf,
			//VanillaObjects.SlimeBarrel,	Poison looter
			VanillaObjects.Stove,
			VanillaObjects.Toilet,
			VanillaObjects.TrashCan,
			VanillaObjects.Tube,
			VanillaObjects.VendorCart,
			VanillaObjects.WaterPump,
			VanillaObjects.Well,
		};
		public static List<string> FireParticleEffectObjects = new List<string>()
		{
			VanillaObjects.Barbecue,
			VanillaObjects.Fireplace,
			VanillaObjects.FlamingBarrel,
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
				? VanillaObjects.Chest
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

		[HarmonyPostfix, HarmonyPatch(nameof(InvDatabase.FillChest), argumentTypes: new[] { typeof(bool) })]
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
	public static class P_ObjectReal
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

		[HarmonyPrefix, HarmonyPatch("Start", argumentTypes: new Type[0] { })]
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
	}
}