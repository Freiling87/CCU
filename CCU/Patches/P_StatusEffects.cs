using BepInEx.Logging;
using BTHarmonyUtils;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Localization;
using CCU.Traits.Behavior;
using CCU.Traits.Combat;
using CCU.Traits.Drug_Warrior;
using CCU.Traits.Explode_On_Death;
using CCU.Traits.Gib_Type;
using CCU.Traits.Passive;
using CCU.Traits.Rel_Faction;
using CCU.Traits.Trait_Gate;
using HarmonyLib;
using JetBrains.Annotations;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine.Networking;

namespace CCU.Patches
{
    [HarmonyPatch(declaringType: typeof(StatusEffects))]
	class P_StatusEffects
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

        /// <summary>
        /// SuicideBomb
        /// Currently Shelved
        /// </summary>
        /// <param name="statusEffectName"></param>
        /// <param name="showText"></param>
        /// <param name="causingAgent"></param>
        /// <param name="cameFromClient"></param>
        /// <param name="dontPrevent"></param>
        /// <param name="specificTime"></param>
        /// <param name="__instance"></param>
        /// <returns></returns>
        //[HarmonyPrefix, HarmonyPatch(methodName: nameof(StatusEffects.AddStatusEffect), argumentTypes: new[] { typeof(string), typeof(bool), typeof(Agent), typeof(NetworkInstanceId), typeof(bool), typeof(int), typeof(StatusEffects) })]
        [Obsolete]
        public static bool AddStatusEffect_Prefix(string statusEffectName, bool showText, Agent causingAgent, NetworkInstanceId cameFromClient, bool dontPrevent, int specificTime, StatusEffects __instance)
        {
			if (statusEffectName == CStatusEffect.SuicideBomb)
            {


				return false;
            }

			return true;
        }

		/// <summary>
		/// Legacy Trait Updater
		/// </summary>
		/// <param name="traitName"></param>
		/// <param name="__instance"></param>
		/// <returns></returns>
		[HarmonyPrefix, HarmonyPatch(methodName: nameof(StatusEffects.AddTrait), 
			argumentTypes: new[] { typeof(string), typeof(bool), typeof(bool) })]
		public static bool AddTrait_Prefix(ref string traitName, StatusEffects __instance)
        {
			if (Legacy.TraitConversions.ContainsKey(traitName))
				traitName = Legacy.TraitConversions[traitName].Name;

			return true;
        }

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(StatusEffects.AgentIsRival), 
			argumentTypes: new[] { typeof(Agent) })]
		public static bool AgentIsRival_Prefix(Agent myAgent, StatusEffects __instance, ref bool __result)
		{
			if (((__instance.agent.HasTrait<Faction_Blahd_Aligned>() || __instance.agent.HasTrait(VanillaTraits.CrepeCrusher)) && 
					(myAgent.HasTrait(VanillaTraits.BlahdBasher) || myAgent.agentName == VanillaAgents.GangsterCrepe || myAgent.HasTrait<Faction_Crepe_Aligned>())) ||
				((__instance.agent.HasTrait<Faction_Crepe_Aligned>() || __instance.agent.HasTrait(VanillaTraits.BlahdBasher)) && 
					(myAgent.HasTrait(VanillaTraits.CrepeCrusher) || myAgent.agentName == VanillaAgents.GangsterBlahd || myAgent.HasTrait<Faction_Blahd_Aligned>())) ||
				(__instance.agent.HasTrait<Cool_Cannibal>() && myAgent.agentName == VanillaAgents.Soldier) ||
				(__instance.agent.HasTrait<Slayable>() && myAgent.HasTrait("HatesScientist")) ||
				(__instance.agent.HasTrait<Faction_Gorilla_Aligned>() && myAgent.HasTrait(VanillaTraits.Specist)))
			{
				__result = true;
				return false;
			}

			return true;
		}

        [HarmonyPrefix, HarmonyPatch(methodName: nameof(StatusEffects.ChooseRandomDrugDealerStatusEffect), 
			argumentTypes: new Type[0] { })]
		public static bool ChooseRandomDrugDealerStatusEffect_Prefix(StatusEffects __instance, ref string __result)
        {
			T_DrugWarrior trait = __instance.agent.GetTrait<T_DrugWarrior>();
			
			if (trait is null || trait is Wildcard)
				return true;

			__result = trait.DrugEffect;
			return false;
        }

		/// <summary>
		/// Circumvent hardcoded explode on death behavior for agent.killerRobot
		/// </summary>
		/// <param name="__instance"></param>
		/// <returns></returns>
		[HarmonyPrefix, HarmonyPatch(methodName: nameof(StatusEffects.ExplodeAfterDeathChecks), 
			argumentTypes: new Type[0] { })]
		public static bool ExplodeAfterDeathChecks_Prefix(StatusEffects __instance)
        {
			if (__instance.agent.HasTrait<Seek_and_Destroy>() &&
				!__instance.agent.HasTrait<T_ExplodeOnDeath>())
				return false;

			return true;
        }

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(StatusEffects.ExplodeAfterDeathChecks), 
			argumentTypes: new Type[0] { })]
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

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(StatusEffects.IsInnocent), 
			argumentTypes: new[] { typeof(Agent) })]
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

        // This section slated for elimination once GibItAShot is working correctly.

        //      [HarmonyTranspiler, UsedImplicitly]
        //      private static IEnumerable<CodeInstruction> DisappearBody(IEnumerable<CodeInstruction> codeInstructions)
        //      {
        //          List<CodeInstruction> instructions = codeInstructions.ToList();
        //          FieldInfo copBot = AccessTools.DeclaredField(typeof(Agent), nameof(Agent.copBot));
        //          MethodInfo magicBool = AccessTools.DeclaredMethod(typeof(P_StatusEffects_ExplodeBody), nameof(MagicBool));

        //          CodeReplacementPatch patch = new CodeReplacementPatch(
        //              expectedMatches: 1,
        //              targetInstructionSequence: new List<CodeInstruction>
        //              {
        //                  new CodeInstruction(OpCodes.Ldfld, copBot)
        //              },
        //              insertInstructionSequence: new List<CodeInstruction>
        //              {
        //                  new CodeInstruction(OpCodes.Call, magicBool),
        //              });

        //          patch.ApplySafe(instructions, logger);
        //          return instructions;
        //      }

        //// Matt made me do it
        //      private static bool MagicBool(Agent agent) =>
        //          agent.copBot ||
        //          agent.GetTraits<T_ExplodeOnDeath>().Any();


        [HarmonyTranspiler, UsedImplicitly]
        private static IEnumerable<CodeInstruction> GibBody(IEnumerable<CodeInstruction> codeInstructions)
        {
            List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo agent = AccessTools.Field(typeof(StatusEffects), nameof(StatusEffects.agent));
			FieldInfo copBot = AccessTools.DeclaredField(typeof(Agent), nameof(Agent.copBot));
			MethodInfo gibItAShot = AccessTools.DeclaredMethod(typeof(P_StatusEffects_ExplodeBody), nameof(GibItAShot));

            CodeReplacementPatch patch = new CodeReplacementPatch(
                expectedMatches: 1,
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_1),								
					new CodeInstruction(OpCodes.Call, gibItAShot),						
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

		private static void GibItAShot(StatusEffects __instance)
		{
			if (__instance.agent.GetTraits<T_ExplodeOnDeath>().FirstOrDefault() is null)
				return;

			int gibType = T_GibType.GetGibType(__instance.agent);

			//	Networking version
			if (GC.multiplayerMode && !__instance.dontDoBloodExplosion)
				__instance.agent.objectMult.Gib(gibType); 

			//	Base version
			if (gibType == 0)
				__instance.NormalGib();
			else if (gibType == 1)
				__instance.IceGib();
			else if (gibType == 2)
				__instance.GhostGib();

			__instance.agent.objectMult.Gib(gibType);
		}
	}
}