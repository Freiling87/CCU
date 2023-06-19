using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Hooks;
using CCU.Items;
using CCU.Localization;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.Networking;

namespace CCU.Traits.Loadout_Gun_Nut
{
	internal class Rubber_Bulleteer : T_GunNut
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();

		public override string GunMod => vItem.RubberBulletsMod;
		public override List<string> ExcludedItems => new List<string>()
		{
			vItem.FireExtinguisher,
			vItem.Flamethrower,
			vItem.FreezeRay,
			vItem.GhostGibber,
			vItem.Leafblower,
			vItem.OilContainer,
			vItem.ResearchGun,
			vItem.RocketLauncher,
			vItem.ShrinkRay,
			vItem.Taser,
			vItem.TranquilizerGun,
			vItem.WaterPistol,
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Rubber_Bulleteer>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent applies a Rubber Bullets Mod to all traditional firearms in inventory. If rubber bullet damage reduces an NPC's health to 10% or lower, they are knocked out. If they go below -10%, they are killed... but less lethally! Rubber Bullet guns are usable by Pacifists."
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Rubber_Bulleteer)),
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					CharacterCreationCost = 10,
					IsAvailable = true,
					IsAvailableInCC = true,
					IsPlayerTrait = true,
					UnlockCost = 16,
				});
			RogueLibs.CreateCustomName("CantUseWeapons", NameTypes.Description, new CustomNameInfo
			{
				English = "Can't use weapons, except for some thrown items and Rubber Bullet-modded guns.",
			});	
		}
	}

	[HarmonyPatch(declaringType: typeof(StatusEffects))]
	static class P_StatusEffects_RubberBulleteer
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		/// <summary>
		/// Applies the HP threshold to damage.
		/// </summary>
		/// <param name="__instance"></param>
		/// <param name="damagerObject"></param>
		/// <param name="healthNum"></param>
		/// <returns></returns>
		[HarmonyPrefix, HarmonyPatch(methodName: nameof(StatusEffects.ChangeHealth), argumentTypes: new Type[] { typeof(float), typeof(PlayfieldObject), typeof(NetworkInstanceId), typeof(float), typeof(string), typeof(byte) })]
		private static bool RubberBulletsKnockout(StatusEffects __instance, ref PlayfieldObject damagerObject, ref float healthNum, ref byte extraVar)
		{
			Agent agent = __instance.agent ?? null;

			if (agent is null || damagerObject is null ||
				!(damagerObject.isBullet && damagerObject.playfieldObjectBullet.rubber) ||
					// Copy of Vanilla exceptions follow
				(agent.teleporting && !agent.skillPoints.justGainedLevel) ||
				(agent.ghost && agent.skillPoints.justGainedLevel) ||
				(agent.dead && agent.skillPoints.justGainedLevel && !agent.teleporting && !agent.suicided && !agent.finishedLevel && !agent.finishedLevelImmediate && !agent.finishedLevelImmediateMult && !agent.finishedLevelRealMult && !agent.oma.finishedLevel) ||
				((agent.finishedLevel || agent.finishedLevelImmediate || agent.finishedLevelImmediateMult || agent.finishedLevelRealMult || agent.oma.finishedLevel) && !agent.suicided && healthNum < 0f) ||
				(agent.butlerBot || agent.hologram || (agent.mechEmpty && healthNum < 0f)) ||
				(GC.cinematic && GC.loadLevel.LevelContainsMayor()))
				return true;

			float knockoutThreshold = agent.healthMax / 10; //  10%
			float deathThreshold = -knockoutThreshold; // 10%
			float netHealth = agent.health + healthNum ; // Damage is negative

			logger.LogDebug("RubberBulletsDamage: " + agent.agentRealName + " (" + agent.health + "/" + agent.healthMax + ") taking " + healthNum + " damage");
			logger.LogDebug("NetHealth <= Knockout?:\t" + netHealth + " <= " + knockoutThreshold + " ? " + (netHealth <= knockoutThreshold));
			logger.LogDebug("NetHealth <= Death?   :\t" + netHealth + " <= " + deathThreshold + " ? " + (netHealth <= deathThreshold));

			if (netHealth <= deathThreshold)
			{
				logger.LogDebug("Rubber Bullet death threshold passed");
				damagerObject.playfieldObjectBullet.rubber = false;
				
				if (GC.percentChance(33))
					RubberBulletsMod.ByTheBook(damagerObject.playfieldObjectBullet.agent);
			}
			else if (netHealth <= knockoutThreshold)
			{
				logger.LogDebug("Rubber Bullet knockout threshold passed");
				agent.healthBeforeKnockout = agent.health + healthNum;
				agent.health += healthNum; // Bypassed for rubber knockout in vanilla code
				agent.knockedOut = true;
				agent.knockedOutLocal = true;
				extraVar = 2;
			}

			return true;
		}
		
		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(StatusEffects.ChangeHealth), argumentTypes: new Type[] { typeof(float), typeof(PlayfieldObject), typeof(NetworkInstanceId), typeof(float), typeof(string), typeof(byte) })]
		private static IEnumerable<CodeInstruction> RubberBullets_ScaleHealth(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo agent = AccessTools.DeclaredField(typeof(StatusEffects), nameof(StatusEffects.agent));
			MethodInfo scaledDeathThreshold = AccessTools.DeclaredMethod(typeof(P_StatusEffects_RubberBulleteer), nameof(P_StatusEffects_RubberBulleteer.ScaleDeathThreshold));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 2,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldc_R4, -10),
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

		//[HarmonyTranspiler, HarmonyPatch(methodName: nameof(StatusEffects.ChangeHealth), argumentTypes: new Type[] { typeof(float), typeof(PlayfieldObject), typeof(NetworkInstanceId), typeof(float), typeof(string), typeof(byte) })]
		private static IEnumerable<CodeInstruction> RubberBullets_AccidentalKillThreshold(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo rubberBulletPlayerBypass = AccessTools.DeclaredMethod(typeof(P_StatusEffects_RubberBulleteer), nameof(P_StatusEffects_RubberBulleteer.BypassPlayerGateForDeathThreshold));

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