using BepInEx.Logging;
using BTHarmonyUtils;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Traits.Explosion_Modifier;
using CCU.Traits.Gib_Type;
using CCU.Traits.Passive;
using HarmonyLib;
using JetBrains.Annotations;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Traits.Explode_On_Death
{
	public abstract class T_ExplodeOnDeath : T_DesignerTrait
	{
		public T_ExplodeOnDeath() : base() { }

		public abstract string ExplosionType { get; }

		public static string GetExplosionType(Agent agent) =>
			agent.GetTraits<T_ExplodeOnDeath>().Where(c => c.ExplosionType != null).FirstOrDefault()?.ExplosionType ??
			VExplosionType.Normal;
	}

	[HarmonyPatch(typeof(StatusEffects))]
	class P_StatusEffects
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(nameof(StatusEffects.ExplodeAfterDeathChecks),
			new Type[0] { })]
		public static void ExplodeAfterDeathChecks_Postfix(StatusEffects __instance)
		{
			if (__instance.agent.GetTraits<T_ExplodeOnDeath>().Any())
			{
				if (!__instance.agent.disappeared)
					__instance.agent.objectSprite.flashingRepeatedly = true;

				if (GC.serverPlayer)
					__instance.StartCoroutine("ExplodeBody");
			}
		}
	}

	[HarmonyPatch(typeof(StatusEffects))]
	static class P_StatusEffects_ExplodeBody
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTargetMethod, UsedImplicitly]
		private static MethodInfo Find_MoveNext_MethodInfo() =>
			PatcherUtils.FindIEnumeratorMoveNext(AccessTools.Method(typeof(StatusEffects), "ExplodeBody"));

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> CustomizeExplosion(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo ExplosionType = AccessTools.DeclaredMethod(typeof(T_ExplodeOnDeath), nameof(T_ExplodeOnDeath.GetExplosionType));
			FieldInfo agent = AccessTools.Field(typeof(StatusEffects), nameof(StatusEffects.agent));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldstr, "Normal")
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldc_I4_0),
					new CodeInstruction(OpCodes.Ldc_I4_M1)
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_1),
					new CodeInstruction(OpCodes.Ldfld, agent),
					new CodeInstruction(OpCodes.Call, ExplosionType)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> GibBody(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo agent = AccessTools.Field(typeof(StatusEffects), nameof(StatusEffects.agent));
			FieldInfo copBot = AccessTools.DeclaredField(typeof(Agent), nameof(Agent.copBot));
			MethodInfo explodeOnDeathCustomGibs = AccessTools.DeclaredMethod(typeof(P_StatusEffects_ExplodeBody), nameof(EOD_CustomGibs));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_1),
					new CodeInstruction(OpCodes.Call, explodeOnDeathCustomGibs),
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_1),
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Ldfld, copBot),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		private static void EOD_CustomGibs(StatusEffects __instance)
		{
			if (__instance.agent.GetTraits<T_ExplodeOnDeath>().Any())
				T_GibType.CustomGib(__instance);
		}
	}

	[HarmonyPatch(typeof(StatusEffects))]
	static class P_StatusEffects_ExplodeOnDeath_ExplodeBody
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTargetMethod, UsedImplicitly]
		private static MethodInfo Find_MoveNext_MethodInfo() =>
			PatcherUtils.FindIEnumeratorMoveNext(AccessTools.Method(typeof(StatusEffects), "ExplodeBody"));

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> SetExplosionTimer(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo agent = AccessTools.DeclaredField(typeof(StatusEffects), nameof(StatusEffects.agent));
			MethodInfo explosionTimerDuration = AccessTools.DeclaredMethod(typeof(T_ExplosionTimer), nameof(T_ExplosionTimer.ExplosionTimerDuration));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldc_R4, 1.5f)
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_1),
					new CodeInstruction(OpCodes.Ldfld, agent),
					new CodeInstruction(OpCodes.Call, explosionTimerDuration),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}

}