using BepInEx.Logging;
using BTHarmonyUtils;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Localization;
using CCU.Traits.Behavior;
using HarmonyLib;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace CCU.Patches.P_Combat
{
	[HarmonyPatch(declaringType: typeof(Explosion))]
	public static class P_Explosion
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(Explosion.SetupExplosion))]
		private static IEnumerable<CodeInstruction> SetupExplosion_CustomExplosions(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo immediateHit = AccessTools.DeclaredField(typeof(Explosion), nameof(Explosion.immediateHit));
			MethodInfo RunCustomExplosion = AccessTools.DeclaredMethod(typeof(P_Explosion), nameof(P_Explosion.RunCustomExplosion));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld, immediateHit),
					new CodeInstruction(OpCodes.Brtrue_S),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),								//	this
					new CodeInstruction(OpCodes.Call, RunCustomExplosion),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		public static void RunCustomExplosion(Explosion explosion)
		{
			switch (explosion.explosionType)
			{
				case CExplosionType.OilSpill:
					explosion.StartCoroutine(explosion.SpawnNoiseLate());
					explosion.StartCoroutine(explosion.PlaySoundAfterTick());
					explosion.circleCollider2D.enabled = true;
					explosion.circleCollider2D.radius = 1.28f;
					explosion.gc.spawnerMain.SpawnParticleEffect("WaterExplosion", explosion.tr.position, 0f);
					explosion.StartCoroutine("SpillLiquidLate");

					break;
			}
		}

		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(Explosion.ExplosionHit), argumentTypes: new[] { typeof(GameObject), typeof(bool) })]
		private static IEnumerable<CodeInstruction> ExplosionHit_LimitEMPtoVanillaKillerRobot(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo killerRobot = AccessTools.DeclaredField(typeof(Agent), nameof(Agent.killerRobot));
			MethodInfo isVanillaKillerRobot = AccessTools.DeclaredMethod(typeof(Seek_and_Destroy), nameof(Seek_and_Destroy.IsVanillaKillerRobot));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 6,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, killerRobot),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, isVanillaKillerRobot)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}

	[HarmonyPatch(declaringType: typeof(Explosion))]
	static class P_Explosion_SpillLiquidLate
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTargetMethod, UsedImplicitly]
		private static MethodInfo Find_MoveNext_MethodInfo() =>
			PatcherUtils.FindIEnumeratorMoveNext(AccessTools.Method(typeof(Explosion), "SpillLiquidLate"));

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> SpillLiquidLate_HookSpecialExplosionTypes(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo explosionType = AccessTools.DeclaredField(typeof(Explosion), nameof(Explosion.explosionType));
			MethodInfo ExplosionTypeMagicStringMatcher = AccessTools.DeclaredMethod(typeof(P_Explosion_SpillLiquidLate), nameof(P_Explosion_SpillLiquidLate.ExplosionTypeMagicStringMatcher));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, explosionType),
					new CodeInstruction(OpCodes.Ldstr, "Water"),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, explosionType),
					new CodeInstruction(OpCodes.Ldstr, "Water"),
					new CodeInstruction(OpCodes.Call, ExplosionTypeMagicStringMatcher), 
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		// Magic string match
		public static string ExplosionTypeMagicStringMatcher(string vanilla) =>
			CExplosionType.Types.Contains(vanilla)
				? "Water"
				: vanilla;

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> SpillLiquidLate_SendExplosionTypes(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo explosionType = AccessTools.DeclaredField(typeof(Explosion), nameof(Explosion.explosionType));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldstr, "Water"),
					new CodeInstruction(OpCodes.Ldc_I4_1),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_1),
					new CodeInstruction(OpCodes.Ldfld, explosionType),
					new CodeInstruction(OpCodes.Ldc_I4_1),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}
}