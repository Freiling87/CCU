using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using BepInEx.Logging;
using System.Reflection;
using CCU.Content;
using CCU.Challenges;
using BTHarmonyUtils.TranspilerUtils;
using System.Reflection.Emit;
using CCU.Systems.Investigateables;
using CCU.Patches.Objects;
using CCU.Systems.CustomGoals;

namespace CCU.Patches.Interface
{
	[HarmonyPatch(declaringType: typeof(LevelEditor))]
	public static class P_LevelEditor
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(LevelEditor.CreateGoalList))]
		private static IEnumerable<CodeInstruction> CreateGoalList_Extend(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo updateList = AccessTools.DeclaredMethod(typeof(CustomGoals), nameof(CustomGoals.CustomGoalList));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldstr, "WanderFar"),
					new CodeInstruction(OpCodes.Callvirt),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_1),	//	list2
					new CodeInstruction(OpCodes.Call, updateList),
					new CodeInstruction(OpCodes.Stloc_1),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(LevelEditor.CreateMutatorListLevel))]
		public static bool CreateMutatorList_Level(LevelEditor __instance, ref float ___numButtonsLoad)
		{
			List<string> list = new List<string>();

			list.AddRange(vMutator.VanillaMutators); // This list is copied from this method so it shouldn't break anything

			// TODO: RETEST 20220725
			foreach (C_CCU challenge in RogueFramework.Unlocks.OfType<C_CCU>())
				list.Add(nameof(challenge));

			__instance.ActivateLoadMenu(); 
			___numButtonsLoad = (float)list.Count;
			__instance.OpenObjectLoad(list);
			__instance.StartCoroutine("SetScrollbarPlacement");

			return false;
		}

        #region Investigateables
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