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
	public class Ghostbustable : T_DesignerTrait
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Ghostbustable>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Ghost gibber hurts u :("),
					[LanguageCode.Spanish] = "El Ghost Gibber te duele :(",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Ghostbustable)),
					[LanguageCode.Spanish] = "Ghostbustable",
				})
				.WithUnlock(new TU_DesignerUnlock());
		}

		internal static bool IsGhostbustable(Agent agent) =>
			agent.ghost || agent.HasTrait<Ghostbustable>();

		internal static FieldInfo GhostbustableVanilla = AccessTools.DeclaredField(typeof(Agent), nameof(Agent.ghost));
		internal static MethodInfo GhostbustableSoftcoded = AccessTools.DeclaredMethod(typeof(Ghostbustable), nameof(Ghostbustable.IsGhostbustable));
	}

	[HarmonyPatch(typeof(Bullet))]
	internal static class P_Bullet_Ghostbustable
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(Bullet.RayHit))]
		private static IEnumerable<CodeInstruction> SoftcodeGhostBlasterDamage(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo ghost = AccessTools.DeclaredField(typeof(Agent), nameof(Agent.ghost));
			MethodInfo customMethod = AccessTools.DeclaredMethod(typeof(Ghostbustable), nameof(Ghostbustable.IsGhostbustable));
			
			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 2,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, ghost),
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