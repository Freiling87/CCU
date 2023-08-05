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

namespace CCU.Traits.Player.Status_Effect
{
	public abstract class T_PermanentStatusEffect_P : T_PlayerTrait, ISetupAgentStats, IRefreshAtLevelStart
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public T_PermanentStatusEffect_P() : base() { }

		public abstract string statusEffectName { get; }

		public void SetupAgentStats(Agent agent)
		{
			RefreshAtLevelStart(agent);
		}

		public void RefreshAtLevelStart(Agent agent)
		{
			agent.statusEffects.RemoveStatusEffect(statusEffectName);
			agent.statusEffects.AddStatusEffect(statusEffectName, 9999);

			StatusEffect statusEffectObject = agent.statusEffects.StatusEffectList.FirstOrDefault(se => se.statusEffectName == statusEffectName);

			if (statusEffectObject is null)
				return;

			statusEffectObject.keepBetweenLevels = true;
			statusEffectObject.removeOnKnockout = false;
			statusEffectObject.dontRemoveOnDeath = true;
			statusEffectObject.infiniteTime = true;
		}
	}

	[HarmonyPatch(typeof(StatusEffects))]
	public static class P_StatusEffects_PermanentSE
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(StatusEffects.RemoveAllStatusEffects), typeof(bool))]
		private static IEnumerable<CodeInstruction> BlockSERemoval_1(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo statusEffectList = AccessTools.DeclaredField(typeof(StatusEffects), nameof(StatusEffects.StatusEffectList));
			MethodInfo filteredStatusEffectList = AccessTools.DeclaredMethod(typeof(P_StatusEffects_PermanentSE), nameof(P_StatusEffects_PermanentSE.FilteredStatusEffectList));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 2,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, statusEffectList),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Call, filteredStatusEffectList),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(StatusEffects.RemoveAllStatusEffectsNotBetweenLevels))]
		private static IEnumerable<CodeInstruction> BlockSERemoval_2(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo statusEffectList = AccessTools.DeclaredField(typeof(StatusEffects), nameof(StatusEffects.StatusEffectList));
			MethodInfo filteredStatusEffectList = AccessTools.DeclaredMethod(typeof(P_StatusEffects_PermanentSE), nameof(P_StatusEffects_PermanentSE.FilteredStatusEffectList));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 2,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, statusEffectList),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Call, filteredStatusEffectList),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		internal static List<StatusEffect> FilteredStatusEffectList(List<StatusEffect> original, StatusEffects instance)
		{
			if (instance.agent is null)
				return original;

			List<string> permanentEffects = instance.agent.GetTraits<T_PermanentStatusEffect_P>().Select(t => t.statusEffectName).ToList();

			return original.Where(se => !permanentEffects.Contains(se.statusEffectName)).ToList();
		}
	}

	[HarmonyPatch(typeof(Toilet))]
	public static class P_Toilet_PermanentSE
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(Toilet.isStatusEffectPurgeable))]
		public static void FilterPermanentSE(Toilet __instance, string statusEffectName, ref bool __result)
		{
			if (__result)
				__result = !__instance.interactingAgent.GetTraits<T_PermanentStatusEffect_P>().Where(t => t.statusEffectName == statusEffectName).Any();
		}
	}
}