using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Challenges;
using CCU.Systems.Containers;
using CCU.Systems.CustomGoals;
using CCU.Systems.Investigateables;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UI;

namespace CCU.Patches.Interface
{
    [HarmonyPatch(declaringType: typeof(LevelEditor))]
	public static class P_LevelEditor
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(LevelEditor.CreateGoalList))]
		private static IEnumerable<CodeInstruction> CreateGoalList_ShowExtendedOptions(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo updateList = AccessTools.DeclaredMethod(typeof(CustomGoals), nameof(CustomGoals.CustomGoalList));
			MethodInfo activateLoadMenu = AccessTools.DeclaredMethod(typeof(LevelEditor), nameof(LevelEditor.ActivateLoadMenu));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_1),
					new CodeInstruction(OpCodes.Ldstr, "WanderFar"),
					new CodeInstruction(OpCodes.Callvirt),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_1),
					new CodeInstruction(OpCodes.Call, updateList),
					new CodeInstruction(OpCodes.Stloc_1),
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{ 
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Call, activateLoadMenu),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		 
		[HarmonyPrefix, HarmonyPatch(methodName: nameof(LevelEditor.CreateMutatorListLevel))]
		public static bool CreateMutatorList_Level(LevelEditor __instance, ref float ___numButtonsLoad)
		{
			List<string> list = new List<string>();
			list.AddRange(vMutator.VanillaMutators);

			// Likely to be added to RogueLibs as a feature, so this might be redundant at some point.
			foreach (C_CCU challenge in RogueFramework.Unlocks.OfType<C_CCU>())
				list.Add(nameof(challenge));

			__instance.ActivateLoadMenu(); 
			___numButtonsLoad = (float)list.Count;
			__instance.OpenObjectLoad(list);
			__instance.StartCoroutine("SetScrollbarPlacement");

			return false;
		}

        [HarmonyPostfix, HarmonyPatch(methodName: nameof(LevelEditor.SetExtraVarString))]
		public static void SetExtraVarString_Postfix(LevelEditor __instance, InputField ___tileNameObject, InputField ___extraVarStringObject)
        {
			if (__instance.currentInterface == "Objects" &&
					Investigateables.InvestigateableObjects_Slot1.Contains(___tileNameObject.text) &&
					!Investigateables.IsInvestigationString(___extraVarStringObject.text))
				___extraVarStringObject.text = Investigateables.ExtraVarStringPrefix + Environment.NewLine + ___extraVarStringObject.text;
        }

		#region Containers
		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(LevelEditor.PressedLoadExtraVarStringList), new Type[0] { })]
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

		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(LevelEditor.PressedScrollingMenuButton), new[] { typeof(ButtonHelper) })]
		public static IEnumerable<CodeInstruction> PressedScrollingMenuButton_OnChoice_ShowCustomInterface(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo deactivateLoadMenu = AccessTools.DeclaredMethod(typeof(LevelEditor), nameof(LevelEditor.DeactivateLoadMenu));
			MethodInfo loadSpecialInterfaces = AccessTools.DeclaredMethod(typeof(P_LevelEditor), nameof(P_LevelEditor.ShowCustomInterface));
			FieldInfo scrollingButtonType = AccessTools.DeclaredField(typeof(ButtonHelper), nameof(ButtonHelper.scrollingButtonType));
			MethodInfo setActive = AccessTools.DeclaredMethod(typeof(GameObject), nameof(GameObject.SetActive));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
                {
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
					new CodeInstruction(OpCodes.Ldstr, ""),
					new CodeInstruction(OpCodes.Call, loadSpecialInterfaces),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(LevelEditor.UpdateInterface), new[] { typeof(bool) })]
		private static IEnumerable<CodeInstruction> UpdateInterface_OnSelect_ShowCustomInterface(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo oneOfEachSceneObject = AccessTools.DeclaredField(typeof(LevelEditor), "oneOfEachSceneObject");
			MethodInfo loadSpecialInterface = AccessTools.DeclaredMethod(typeof(P_LevelEditor), nameof(P_LevelEditor.ShowCustomInterface));

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
					new CodeInstruction(OpCodes.Ldloc_S, 34),					//	text10 (item name)
					new CodeInstruction(OpCodes.Ldloc_S, 43),					//	tileNameText2
					new CodeInstruction(OpCodes.Call, loadSpecialInterface),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		private static void ShowCustomInterface(LevelEditor levelEditor, string objectName, string itemName = "")
        {
			if (Containers.ContainerObjects.Contains(objectName))
            {
				InputField extraVarObject = (InputField)AccessTools.Field(typeof(LevelEditor), "extraVarObject").GetValue(levelEditor);
				InputField extraVarStringObject = (InputField)AccessTools.Field(typeof(LevelEditor), "extraVarStringObject").GetValue(levelEditor);

				extraVarObject.gameObject.SetActive(false);
				extraVarStringObject.gameObject.SetActive(true);
				levelEditor.SetNameText(extraVarStringObject, itemName, "Item");
			}
		}
        #endregion
        #region Investigateables
        [HarmonyTranspiler, HarmonyPatch(methodName: nameof(LevelEditor.OpenLongDescription))]
		public static IEnumerable<CodeInstruction> OpenLongDescription_RemoveSpecialStrings(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo extraVarStringObject = AccessTools.DeclaredField(typeof(LevelEditor), "extraVarStringObject");
			MethodInfo getText = AccessTools.DeclaredMethod(typeof(InputField), "get_text");
			MethodInfo LongDescriptionWithoutSpecialStrings = AccessTools.DeclaredMethod(typeof(P_LevelEditor), nameof(P_LevelEditor.LongDescriptionWithoutSpecialStrings));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, extraVarStringObject),
					new CodeInstruction(OpCodes.Callvirt, getText),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, LongDescriptionWithoutSpecialStrings),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		//TODO: Move this to a general PlayfieldObject Extension System class
		public static string LongDescriptionWithoutSpecialStrings(string vanilla)
        {
			Core.LogMethodCall();
			logger.LogDebug("vanilla: " + vanilla);

			if (vanilla is null || vanilla == "")
				return vanilla;

			if (vanilla.Contains(Investigateables.ExtraVarStringPrefix))
				vanilla = vanilla.Replace(Investigateables.ExtraVarStringPrefix + Environment.NewLine, "");

			return vanilla;
        }

		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(LevelEditor.PressedLoadExtraVarStringList), new Type[0] { })]
		public static IEnumerable<CodeInstruction> PressedLoadExtraVarStringList_EditTextBox(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo magicObjectName = AccessTools.DeclaredMethod(typeof(Investigateables), nameof(Investigateables.MagicObjectName));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_1),
					new CodeInstruction(OpCodes.Ldstr, "Sign"),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_1),					//	Object real name
					new CodeInstruction(OpCodes.Call, magicObjectName),		//	"Sign" if investigateable, or real name if not
					new CodeInstruction(OpCodes.Ldstr, "Sign"),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(LevelEditor.PressedScrollingMenuButton), new [] { typeof(ButtonHelper) } )]
		public static IEnumerable<CodeInstruction> PressedScrollingMenuButton_TextBoxValid(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo scrollingButtonType = AccessTools.DeclaredField(typeof(ButtonHelper), nameof(ButtonHelper.scrollingButtonType));
			MethodInfo magicObjectName = AccessTools.DeclaredMethod(typeof(Investigateables), nameof(Investigateables.MagicObjectName));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 2,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_1),
					new CodeInstruction(OpCodes.Ldfld, scrollingButtonType),
					new CodeInstruction(OpCodes.Ldstr, "Sign"),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_1),
					new CodeInstruction(OpCodes.Ldfld, scrollingButtonType),
					new CodeInstruction(OpCodes.Call, magicObjectName),
					new CodeInstruction(OpCodes.Ldstr, "Sign")
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(LevelEditor.UpdateInterface), new[] { typeof(bool) })]
		private static IEnumerable<CodeInstruction> UpdateInterface_ShowTextBoxForInvestigateables(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo magicObjectName = AccessTools.DeclaredMethod(typeof(Investigateables), nameof(Investigateables.MagicObjectName));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_S, 34),
					new CodeInstruction(OpCodes.Ldstr, "Sign"),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_S, 34),				//	Object real name
					new CodeInstruction(OpCodes.Call, magicObjectName),		//	"Sign" if investigateable, or real name if not
					new CodeInstruction(OpCodes.Ldstr, "Sign"),				
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		#endregion
	}
}