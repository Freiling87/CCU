using BepInEx.Logging;
using BTHarmonyUtils;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Localization;
using CCU.Traits.Behavior;
using CCU.Traits.Drug_Warrior;
using CCU.Traits.Explode_On_Death;
using CCU.Traits.Explosion_Modifier;
using CCU.Traits.Gib_Type;
using CCU.Traits.Loot_Drops;
using CCU.Traits.Passive;
using CCU.Traits.Player;
using CCU.Traits.Player.Ammo;
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
using UnityEngine;
using UnityEngine.Networking;
using static CCU.Traits.Gib_Type.T_GibType;

namespace CCU.Patches
{
    [HarmonyPatch(declaringType: typeof(StatusEffects))]
	class P_StatusEffects
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

        [HarmonyTranspiler, HarmonyPatch(methodName: nameof(StatusEffects.AddStatusEffect), 
			argumentTypes: new[] { typeof(string), typeof(bool), typeof(Agent), typeof(NetworkInstanceId), typeof(bool), typeof(int) })]
		private static IEnumerable<CodeInstruction> AddStatusEffect_ExtendedRelease(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo extendedReleaseCheck = AccessTools.DeclaredMethod(typeof(P_StatusEffects), nameof(P_StatusEffects.ExtendedReleaseCheck));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
                {
					new CodeInstruction(OpCodes.Ldloc_0)
                },
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, extendedReleaseCheck),
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldc_I4, 9999)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		private static int ExtendedReleaseCheck(int vanilla) =>
			vanilla == 69420
				? 9999
				: vanilla;

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

        [HarmonyPrefix, HarmonyPatch(methodName: nameof(StatusEffects.ChangeHealth), 
			argumentTypes: new[] { typeof(float), typeof(PlayfieldObject), typeof(NetworkInstanceId), typeof(float), typeof(string), typeof(byte) })]
		public static bool ChangeHealth_Prefix(StatusEffects __instance, ref float healthNum)
        {
			if (__instance.agent.HasTrait<Not_Vincible>() && healthNum < 0f)
				healthNum = 0f;

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
			// Explode on Death is hardcoded into the agent.killerRobot variable. This is to filter that for custom agents.
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

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(StatusEffects.NormalGib))]
		public static bool NormalGib_Redirect(StatusEffects __instance)
		{
			if (__instance.agent.HasTrait<Meat_Chunks>() || !__instance.agent.GetTraits<T_GibType>().Any())
				return true;

			if (!__instance.agent.HasTrait<Indestructible>())
				P_StatusEffects_ExplodeBody.CustomGib(__instance);

			return false;
		}

        [HarmonyPrefix, HarmonyPatch(methodName: nameof(StatusEffects.SetupDeath), argumentTypes: new[] { typeof(PlayfieldObject), typeof(bool), typeof(bool) })]
		public static bool SetupDeath_FilterSpillables(StatusEffects __instance)
		{
			Agent agent = __instance.agent;

			foreach (InvItem invItem in agent.inventory.InvItemList)
			{
				foreach (T_LootDrop trait in agent.GetTraits<T_LootDrop>())
					if (trait.ProtectedItem(invItem))
					{
						invItem.doSpill = false;
						invItem.cantDropNPC = true;
					}

				if (agent.GetTraits<T_AmmoCap>().Any())
					T_AmmoCap.ResetMaxAmmoOnSpill(agent, invItem);
			}

			return true;
        }

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(StatusEffects.UseQuickEscapeTeleporter))]
		public static bool UseQuickEscapeTeleporter_Blinker(bool isEndOfFrame, StatusEffects __instance)
        {
			// Thought this was broken, but it was QET + No Teleports

			try
			{
				if (__instance.agent.HasTrait<Blinker>())
				{
					Agent agent = __instance.agent;
					Vector3 targetLoc = Vector3.zero;
					int attempts = 0;

					do
					{
						targetLoc = GC.tileInfo.FindRandLocation(agent, true, true);
						attempts++;
					}
					while (Vector2.Distance(targetLoc, agent.tr.position) > 5f);

					if (targetLoc == Vector3.zero)
						targetLoc = agent.tr.position;

					agent.Teleport(targetLoc, false, true);
					agent.agentCamera.fastLerpTime = 1f;
					GC.audioHandler.Play(agent, VanillaAudio.Spawn);

					return false;
				}
			}
			catch { }

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
			// Checks for EOD to detect custom characters.
			// Cannot check for GibType here since vanilla means normal Gibs.
			if (__instance.agent.GetTraits<T_ExplodeOnDeath>().Any() &&
				!__instance.agent.HasTrait<Indestructible>())
				CustomGib(__instance); 
		}

		// Largely a modified copy of vanilla
		public static void CustomGib(StatusEffects statusEffects)
		{
			Agent agent = statusEffects.agent;
			T_GibType gibTrait = agent.GetTraits<T_GibType>().FirstOrDefault();

            if (statusEffects.slaveHelmetGonnaBlow)
			{
				MethodInfo slaveHelmetBlow = AccessTools.DeclaredMethod(typeof(StatusEffects), "SlaveHelmetBlow");
				slaveHelmetBlow.GetMethodWithoutOverrides<Action>(statusEffects).Invoke();
				return;
			}

			if ((GC.serverPlayer && !statusEffects.agent.disappeared) || (!GC.serverPlayer && !statusEffects.agent.gibbed && !statusEffects.agent.fellInHole))
			{
				statusEffects.agent.gibbed = true;
				statusEffects.Disappear();

				if (agent.HasTrait<Gibless>())
					return;

				if (GC.bloodEnabled)
				{
					if ((!statusEffects.agent.overHole || statusEffects.agent.underWater) && (!statusEffects.agent.warZoneAgent || !statusEffects.agent.underWater))
					{ 
						InvItem invItem = new InvItem();
						invItem.invItemName = "Giblet";
						invItem.SetupDetails(false);
						string gibSpriteName = GetGibType(statusEffects.agent).ToString();

						for (int i = 1; i <= gibTrait.gibQuantity; i++)
						{
							int gibSpriteIterator = Math.Max(1, i % (gibTrait.gibSpriteIteratorLimit + 1));
							invItem.LoadItemSprite(gibSpriteName + gibSpriteIterator);
							GC.spawnerMain.SpawnWreckage(statusEffects.agent.tr.position, invItem, statusEffects.agent, null, false);
						}

						if (!statusEffects.agent.underWater && gibTrait.gibDecal != DecalSpriteName.None)
							GC.spawnerMain.SpawnFloorDecal(statusEffects.agent.tr.position, gibTrait.gibDecal.ToString());
					}
					if (!statusEffects.dontDoBloodExplosion && !(gibTrait.particleEffect is null)) // dontdobloodexplosion is strictly tied to cannibalism
					{
						GC.spawnerMain.SpawnParticleEffect(gibTrait.particleEffect, statusEffects.agent.tr.position, 0f);
						GC.audioHandler.Play(statusEffects.agent, gibTrait.audioClipName);
					}
					else
						GC.audioHandler.Play(statusEffects.agent, VanillaAudio.CannibalFinish);// This is specific to cannibalize; not sure if that will ever lead here but just in case.
				}
				else // I doubt many players play with gore disabled, but who knows
				{
					GC.spawnerMain.SpawnParticleEffect("WallDestroyed", statusEffects.agent.tr.position, 0f);

					if (statusEffects.agent.agentName == VanillaAgents.Robot)
						GC.audioHandler.Play(statusEffects.agent, VanillaAudio.RobotDeath);
					else
						GC.audioHandler.Play(statusEffects.agent, VanillaAudio.AgentDie);
				}

				if (statusEffects.agent.isPlayer != 0 && !statusEffects.playedPlayerDeath)
					GC.audioHandler.Play(statusEffects.agent, VanillaAudio.PlayerDeath);
			}
		}

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

    [HarmonyPatch(declaringType: typeof(StatusEffects))]
	static class P_StatusEffects_UpdateStatusEffect
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTargetMethod, UsedImplicitly]
		private static MethodInfo Find_MoveNext_MethodInfo() =>
			PatcherUtils.FindIEnumeratorMoveNext(AccessTools.Method(typeof(StatusEffects), "UpdateStatusEffect"));
	}
}