using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Traits.Player.Movement
{
	public class Hydrosensitive : T_PlayerTrait
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_PlayerTrait = RogueLibs.CreateCustomTrait<Hydrosensitive>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Water hurts u :("),
					[LanguageCode.Spanish] = "Agua te duele :(",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Hydrosensitive)),
					[LanguageCode.Spanish] = "Hidrosensitivo",
				})
				.WithUnlock(new TU_PlayerUnlock
				{
					Cancellations = { },
					CharacterCreationCost = -3,
					IsAvailable = false,
					IsAvailableInCC = true,
					UnlockCost = 7,
					Unlock =
					{
						categories = { },
					}
				});
		}

		// This was using agent.electronic but I think by error. Try killerRobot first.
		internal static bool IsHydrosensitive(Agent agent) =>
			agent.killerRobot || agent.HasTrait<Hydrosensitive>();

		internal static FieldInfo hydrosensitiveVanilla = AccessTools.DeclaredField(typeof(Agent), nameof(Agent.killerRobot));
		internal static MethodInfo hydrosensitiveSoftcoded = AccessTools.DeclaredMethod(typeof(Hydrosensitive), nameof(Hydrosensitive.IsHydrosensitive));
	}

	[HarmonyPatch(typeof(Agent))]
	internal static class P_Agent
	{
		public static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(Agent.AgentLateUpdate))]
		private static IEnumerable<CodeInstruction> SoftcodeWaterDamage(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, Hydrosensitive.hydrosensitiveVanilla),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, Hydrosensitive.hydrosensitiveSoftcoded)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}

	[HarmonyPatch(typeof(BulletHitbox))]
	internal static class P_BulletHitbox
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(BulletHitbox.HitAftermath))]
		private static IEnumerable<CodeInstruction> SoftcodeWaterBulletDamage(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo copBot = AccessTools.DeclaredField(typeof(Agent), nameof(Agent.copBot));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 2,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, copBot),
					new CodeInstruction(OpCodes.Brtrue_S),
					new CodeInstruction(OpCodes.Ldarg_1),
				},
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, Hydrosensitive.hydrosensitiveVanilla),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, Hydrosensitive.hydrosensitiveSoftcoded)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}
}