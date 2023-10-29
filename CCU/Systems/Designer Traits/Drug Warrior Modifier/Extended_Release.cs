using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine.Networking;


namespace CCU.Traits.Drug_Warrior_Modifier
{
	public class Extended_Release : T_DrugWarriorModifier
	{
		//[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Extended_Release>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Drug warrior status effect lasts until end of combat."),
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Extended_Release)),
				})
				.WithUnlock(new TU_DesignerUnlock
				{
					Cancellations = { },
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}

	[HarmonyPatch(typeof(StatusEffects))]
	class P_StatusEffects
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(StatusEffects.AddStatusEffect),
			new[] { typeof(string), typeof(bool), typeof(Agent), typeof(NetworkInstanceId), typeof(bool), typeof(int) })]
		private static IEnumerable<CodeInstruction> AddStatusEffect_ExtendedRelease(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo extendedReleaseCheck = AccessTools.DeclaredMethod(typeof(P_StatusEffects), nameof(P_StatusEffects.ExtendedReleaseCheck));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_0)
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, extendedReleaseCheck),
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldc_I4, 9999)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		private static int ExtendedReleaseCheck(int vanilla) =>
			vanilla == 69420
				? 9999
				: vanilla;
	}
}
