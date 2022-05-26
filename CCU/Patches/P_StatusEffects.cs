using BepInEx.Logging;
using BTHarmonyUtils;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Traits.Drug_Warrior;
using CCU.Traits.Explode_On_Death;
using CCU.Traits.Passive;
using CCU.Traits.Trait_Gate;
using HarmonyLib;
using JetBrains.Annotations;
using RogueLibsCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace CCU.Patches
{
	[HarmonyPatch(declaringType: typeof(StatusEffects))]
	class P_StatusEffects
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(StatusEffects.AgentIsRival), argumentTypes: new[] { typeof(Agent) })]
		public static bool AgentIsRival_Prefix(Agent myAgent, StatusEffects __instance, ref bool __result)
		{
			if ((__instance.agent.HasTrait<Bashable>() && (myAgent.HasTrait(VanillaTraits.BlahdBasher) || myAgent.agentName == VanillaAgents.GangsterCrepe)) ||
				(__instance.agent.HasTrait<Cool_Cannibal>() && (myAgent.agentName == VanillaAgents.Soldier)) ||
				(__instance.agent.HasTrait<Crushable>() && (myAgent.HasTrait(VanillaTraits.CrepeCrusher) || myAgent.agentName == VanillaAgents.GangsterBlahd)) ||
				(__instance.agent.HasTrait<Slayable>() && myAgent.HasTrait("HatesScientist")) ||
				(__instance.agent.HasTrait<Specistist>() && myAgent.HasTrait(VanillaTraits.Specist)))
			{
				__result = true;
				return false;
			}

			return true;
		}

        [HarmonyPrefix, HarmonyPatch(methodName: nameof(StatusEffects.ChooseRandomDrugDealerStatusEffect), argumentTypes: new Type[0] { })]
		public static bool ChooseRandomDrugDealerStatusEffect_Prefix(StatusEffects __instance, ref string __result)
        {
			T_DrugWarrior trait = __instance.agent.GetTrait<T_DrugWarrior>();
			
			if (trait != null)
            {
				__result =
                    trait is Traits.Drug_Warrior.Wildcard
                    ? __instance.ChooseRandomDrugDealerStatusEffect()
					: trait.DrugEffect;
				
				return false;
            }

			return true;
        }

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(StatusEffects.ExplodeAfterDeathChecks))]
		public static void ExplodeAfterDeathChecks_Postfix(StatusEffects __instance)
		{
			if (__instance.agent.GetTraits<T_ExplodeOnDeath>().Any())
			{
				if (!__instance.agent.disappeared)
					__instance.agent.objectSprite.flashingRepeatedly = true;

				// TODO: See note 2205240553
				if (GC.serverPlayer)
					__instance.StartCoroutine("ExplodeBody");
			}
		}

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(StatusEffects.IsInnocent), argumentTypes: new[] { typeof(Agent) })]
		public static bool IsInnocent_Prefix(Agent playerGettingPoints, StatusEffects __instance, ref bool __result)
		{
			if (__instance.agent.HasTrait<Innocent>())
			{
				__result = true;
				return false;
			}

			return true;
        }
	}

    [HarmonyPatch(declaringType: typeof(StatusEffects))]
	static class P_StatusEffects_ExplodeBody
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTargetMethod, UsedImplicitly]
		private static MethodInfo Find_MoveNext_MethodInfo() =>
			PatcherUtils.FindIEnumeratorMoveNext(AccessTools.Method(typeof(StatusEffects), "ExplodeBody", new Type[] { }));

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> CustomizeExplosion(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo ExplosionType = AccessTools.DeclaredMethod(typeof(P_StatusEffects_ExplodeBody), nameof(ExplosionType));
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

		public static string ExplosionType(Agent agent)
        {
			string result = agent.GetTraits<T_ExplodeOnDeath>().Where(c => c.ExplosionType != null).FirstOrDefault().ExplosionType;

			logger.LogDebug("result: " + result);

			return result;
        }
	}
}
