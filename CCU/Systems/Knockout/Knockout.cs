using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Items;
//using CCU.Systems.Knockout.Traits;
using HarmonyLib;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine.Networking;

namespace CCU.Knockout
{
	[HarmonyPatch(typeof(InvDatabase))]
	public static class P_InvDatabase_Knockout
	{
		[HarmonyPrefix, HarmonyPatch(nameof(InvDatabase.DetermineIfCanUseWeapon))]
		public static bool PacifistFlexibility(InvDatabase __instance, InvItem item, ref bool __result)
		{
			if (__instance.agent.HasTrait(VanillaTraits.Pacifist)
				&& (item.contents.Contains(VItemName.RubberBulletsMod)))
			//|| (item.invItemName == VanillaItems.PoliceBaton && (__instance.agent.HasTrait<Always_Baton_Red>() || __instance.agent.HasTrait<Always_Baton_Red_2>()))))
			{
				__result = true;
				return false;
			}

			return true;
		}
	}

	[HarmonyPatch(typeof(StatusEffects))]
	static class P_StatusEffects_Knockout
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		/// <summary>
		/// Applies the HP threshold to damage.
		/// </summary>
		/// <param name="__instance"></param>
		/// <param name="damagerObject"></param>
		/// <param name="healthNum"></param>
		/// <returns></returns>
		[HarmonyPrefix, HarmonyPatch(nameof(StatusEffects.ChangeHealth), new[] { typeof(float), typeof(PlayfieldObject), typeof(NetworkInstanceId), typeof(float), typeof(string), typeof(byte) })]
		private static bool LessLethalKnockout(StatusEffects __instance, ref PlayfieldObject damagerObject, ref float healthNum, ref byte extraVar)
		{
			Agent agent = __instance.agent ?? null;
			Agent hitterAgent = agent?.lastHitByAgent ?? null;
			bool isRubberBullet = !(damagerObject is null) && damagerObject.isBullet && damagerObject.playfieldObjectBullet.rubber;
			bool isKnockoutPunch = false // Shelved
				&& !(hitterAgent is null) && hitterAgent.agentInvDatabase.equippedWeapon.invItemName == VanillaItems.Fist; //&& hitterAgent.HasTrait<Knocker_Out>();
			bool isBaton = false && // Shelved
				!(hitterAgent is null) && hitterAgent.agentInvDatabase.equippedWeapon.invItemName == VanillaItems.PoliceBaton; //&& hitterAgent.HasTrait<Always_Baton_Red>();

			if (agent is null || damagerObject is null ||
				(!isRubberBullet && !isKnockoutPunch && !isBaton) ||
				// Vanilla exceptions below
				(agent.teleporting && !agent.skillPoints.justGainedLevel) ||
				(agent.ghost && agent.skillPoints.justGainedLevel) ||
				(agent.dead && agent.skillPoints.justGainedLevel && !agent.teleporting && !agent.suicided && !agent.finishedLevel && !agent.finishedLevelImmediate && !agent.finishedLevelImmediateMult && !agent.finishedLevelRealMult && !agent.oma.finishedLevel) ||
				((agent.finishedLevel || agent.finishedLevelImmediate || agent.finishedLevelImmediateMult || agent.finishedLevelRealMult || agent.oma.finishedLevel) && !agent.suicided && healthNum < 0f) ||
				(agent.butlerBot || agent.hologram || (agent.mechEmpty && healthNum < 0f)) ||
				(GC.cinematic && GC.loadLevel.LevelContainsMayor()))
				return true;

			float knockoutThreshold = agent.healthMax / 10;
			float deathThreshold = -knockoutThreshold; // 10%
			float netHealth = agent.health + healthNum; // Damage is negative

			if (netHealth <= deathThreshold) // Accidental kill()
			{
				if (isRubberBullet)
					damagerObject.playfieldObjectBullet.rubber = false;

				if (!agent.knockedOut && !agent.knockedOutLocal
						&& (hitterAgent.HasTrait(VanillaTraits.TheLaw) || agent.enforcer)
						&& GC.percentChance(10))
					RubberBulletsMod.SayDialogue(hitterAgent);
			}
			else if (netHealth <= knockoutThreshold) // Knockout()
			{
				agent.healthBeforeKnockout = agent.health + healthNum;
				agent.health += healthNum; // Bypassed for rubber knockout in vanilla code
				agent.knockedOut = true;
				agent.knockedOutLocal = true;
				extraVar = 2;
			}

			return true;
		}

		[HarmonyTranspiler, HarmonyPatch(nameof(StatusEffects.ChangeHealth), new[] { typeof(float), typeof(PlayfieldObject), typeof(NetworkInstanceId), typeof(float), typeof(string), typeof(byte) })]
		private static IEnumerable<CodeInstruction> RubberBullets_ScaleHealth(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo agent = AccessTools.DeclaredField(typeof(StatusEffects), nameof(StatusEffects.agent));
			MethodInfo scaledDeathThreshold = AccessTools.DeclaredMethod(typeof(P_StatusEffects_Knockout), nameof(P_StatusEffects_Knockout.ScaleDeathThreshold));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 2,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldc_R4, -10),// May need float arg
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld, agent),
					new CodeInstruction(OpCodes.Call, scaledDeathThreshold),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		private static float ScaleDeathThreshold(Agent agent)
		{
			logger.LogDebug("ScaledDeathThreshold: " + agent.agentRealName + ", at " + agent.healthMax / -10f + "/" + agent.healthMax);
			return agent.healthMax / -10f;
		}

		// TEST: Should be obsolete due to patch above
		//[HarmonyTranspiler, HarmonyPatch(nameof(StatusEffects.ChangeHealth), new[] { typeof(float), typeof(PlayfieldObject), typeof(NetworkInstanceId), typeof(float), typeof(string), typeof(byte) })]
		private static IEnumerable<CodeInstruction> RubberBullets_AccidentalKillThreshold(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo rubberBulletPlayerBypass = AccessTools.DeclaredMethod(typeof(P_StatusEffects_Knockout), nameof(P_StatusEffects_Knockout.BypassPlayerGateForDeathThreshold));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_S, 5),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldarg_2), // damagerObject
					new CodeInstruction(OpCodes.Call, rubberBulletPlayerBypass),
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.And),
					new CodeInstruction(OpCodes.Brfalse),
					new CodeInstruction(OpCodes.Ldstr, "Death Type 2: "),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		private static bool BypassPlayerGateForDeathThreshold(bool vanillaValue, StatusEffects statusEffects, PlayfieldObject damagerObject)
		{
			if (damagerObject.isBullet && damagerObject.playfieldObjectBullet.rubber)
				return true;

			return vanillaValue;
		}
	}
}
