using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Traits.Passive
{
	public class Z_Infectious : T_DesignerTrait
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Z_Infectious>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character is an asymptomatic carrier of the Z-Virus. They can infect other people with normal zombiism, but won't necessarily zombify on their own."),
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Z_Infectious), ("Z-Infectious")),
				})
				.WithUnlock(new TU_DesignerUnlock
				{
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}

		internal static bool IsZInfectious(Agent agent) =>
			agent.zombified || agent.HasTrait<Z_Infectious>();
	}

	[HarmonyPatch(typeof(MeleeHitbox))]
	internal static class P_MeleeHitbox_ZInfectious
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(MeleeHitbox.HitObject))]
		private static IEnumerable<CodeInstruction> SoftcodeZInfectiousStrike(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo zombified = AccessTools.DeclaredField(typeof(Agent), nameof(Agent.zombified));
			MethodInfo customMethod = AccessTools.DeclaredMethod(typeof(Z_Infectious), nameof(Z_Infectious.IsZInfectious));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 3,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction	(OpCodes.Ldfld, zombified),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{ 
					new CodeInstruction(OpCodes.Call, customMethod),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}
}