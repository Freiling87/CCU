using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace CCU.Patches.Objects
{
	[HarmonyPatch(typeof(PlayfieldObject))]
	class P_PlayfieldObject_CustomCosts
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(PlayfieldObject.determineMoneyCost), new[] { typeof(int), typeof(string) })]
		private static IEnumerable<CodeInstruction> DetermineMoneyCost_DetectCustomValues(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo customPriceByName = AccessTools.DeclaredMethod(typeof(P_PlayfieldObject_CustomCosts), nameof(CustomPriceByName));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_S, 4),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_0),					//	float num
					new CodeInstruction(OpCodes.Ldarg_2),					//	string transactionType
					new CodeInstruction(OpCodes.Call, customPriceByName),	//	float
					new CodeInstruction(OpCodes.Stloc_0),					//	num
					new CodeInstruction(OpCodes.Ldloc_S, 4),				//	
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		private static float CustomPriceByName(float originalPrice, string transactionType)
		{
			int levelModifier = Mathf.Clamp(GC.sessionDataBig.curLevelEndless, 1, 15) - 1;
			int levelScaleRate = 16;
			int levelScaler = levelModifier * levelScaleRate;

			switch (transactionType)
			{
				case CDetermineMoneyCost.HirePermanentExpert:
					return 240f + levelScaler;
				case CDetermineMoneyCost.HirePermanentMuscle:
					return 160f + levelScaler;
				case CDetermineMoneyCost.LearnLanguageEnglish:
					return 600; // Removal cost for Vocally Challenged
				case CDetermineMoneyCost.LearnLanguageOther: // TODO: Scale cost to Trait value, since Goryllian is a little more valuable.
					return 200;

				default:
					return originalPrice;
			}
		}
	}
}