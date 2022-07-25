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

namespace CCU.Patches.Interface
{
	[HarmonyPatch(declaringType: typeof(LevelEditor))]
	public static class P_LevelEditor
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(LevelEditor.CreateGoalList))]
		public static bool CreateGoalList_Prefix(LevelEditor __instance, ref float ___numButtonsLoad)
		{
			// CURRENTLY VANILLA

			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			list.Add("None");
			//list2.Add("Arrested");
			//list2.Add("CommitArson");
			//list2.Add("Dead");
			//list2.Add("DeadBurned");
			//list2.Add("Explode");
			//list2.Add("KnockedOut");
			//list2.Add("Panic"); 
			//list2.Add("RobotClean"); // In Vanilla
			//list2.Add("WanderAgents");
			//list2.Add("WanderAgentsAligned");
			//list2.Add("WanderAgentsUnaligned");
			list2.Add("Idle");
			list2.Add("Guard");
			list2.Add("Patrol");
			list2.Add("Dance");
			list2.Add("IceSkate");
			list2.Add("Swim");
			list2.Add("ListenToJokeNPC");
			list2.Add("Joke");
			list2.Add("Sit");
			list2.Add("Sleep");
			list2.Add("CuriousObject");
			list2.Add("Wander");
			list2.Add("WanderInOwnedProperty");
			list2.Add("WanderFar");
			__instance.ActivateLoadMenu();
			___numButtonsLoad = (float)(list.Count + list2.Count);
			__instance.OpenObjectLoad(list, list2);
			__instance.StartCoroutine("SetScrollBarPlacement");
			return false;
		}

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(LevelEditor.CreateMutatorListLevel))]
		public static bool CreateMutatorList_Level(LevelEditor __instance, ref float ___numButtonsLoad)
		{
			List<string> list = new List<string>();

			list.AddRange(vMutator.VanillaMutators); // This list is copied from this method so it shouldn't break anything

			// TODO: RETEST
			foreach (C_CCU challenge in RogueFramework.Unlocks.OfType<C_CCU>())
				list.Add(nameof(challenge));

			__instance.ActivateLoadMenu(); 
			___numButtonsLoad = (float)list.Count;
			__instance.OpenObjectLoad(list);
			__instance.StartCoroutine("SetScrollbarPlacement");

			return false;
		}

		//[HarmonyPrefix, HarmonyPatch(methodName: nameof(LevelEditor.PressedLoadExtraVarStringList))]
		public static bool PressedLoadExtraVarStringList_Prefix(LevelEditor __instance, InputField ___tileNameObject, ref float ___numButtonsLoad)
        {
			string text = ___tileNameObject.text;

			if (text == vObject.Window)
			{
				__instance.scrollingMenuType = "LoadExtraVarsWindow";
				P_Window.CreateExtraVarsWindowList_1(__instance, ref ___numButtonsLoad);
				return false;
			}

			return true;
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