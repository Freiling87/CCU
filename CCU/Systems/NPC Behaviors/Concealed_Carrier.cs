using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections;
using UnityEngine;

namespace CCU.Traits.Behavior
{
	public class Concealed_Carrier : T_Behavior
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Concealed_Carrier>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This agent knows their rights, and declines your request for them to shut up about it. They'll hide their weapon when not in combat."),
					[LanguageCode.Spanish] = "Este NPC habla tranquilo pero trae fierro, Las armas que lleven no se prodran ver hasta que entren en combate.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Concealed_Carrier)),
					[LanguageCode.Spanish] = "Portador Oculto",
				})
				.WithUnlock(new TU_DesignerUnlock
				{
					Cancellations = { },
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}

		public bool BypassUnlockChecks => false;
		public override void SetupAgent(Agent agent) { }
	}
	[HarmonyPatch(typeof(GoalBattle))]
	class P_GoalBattle
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(nameof(GoalBattle.Terminate))]
		private static void ChooseWeaponAfterBattle(Agent ___agent)
		{
			___agent.agentInvDatabase.ChooseWeapon();
		}
	}

	[HarmonyPatch(typeof(GoalCombatEngage))]
	class P_GoalCombatEngage
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(nameof(GoalCombatEngage.Activate))]
		public static void Activate_Postfix(GoalCombatEngage __instance)
		{
			if (__instance.agent.HasTrait<Concealed_Carrier>() && __instance.agent.isPlayer == 0)
				__instance.agent.agentInvDatabase.ChooseWeapon();
		}

		[HarmonyPostfix, HarmonyPatch(nameof(GoalCombatEngage.Terminate))]
		public static void Terminate_Postfix(GoalCombatEngage __instance)
		{
			__instance.agent.agentInvDatabase.StartCoroutine(P_InvDatabase_OpenCarry.ConcealWeapon(__instance.agent.agentInvDatabase, true));
		}
	}

	[HarmonyPatch(typeof(InvDatabase))]
	class P_InvDatabase_OpenCarry
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(nameof(InvDatabase.ChooseWeapon), new[] { typeof(bool) })]
		public static bool ChooseWeapon_Prefix(InvDatabase __instance)
		{
			__instance.StartCoroutine(ConcealWeapon(__instance, true));
			return true;
		}
		public static IEnumerator ConcealWeapon(InvDatabase invDatabase, bool delay)
		{
			Agent agent = invDatabase.agent;

			if (agent.isPlayer == 0 
				&& !agent.inCombat
				// && agent.brain.Goals[0].goalCode != goalType.Flee // dw
				&& agent.HasTrait<Concealed_Carrier>())
					//|| (GC.challenges.Contains(nameof(No_Open_Carry))))) /*&& !agent.HasTrait<Outlaw>())))*/
			{
				float delayDuration = 0f;

				if (delay)
				{
					if (invDatabase.agent.HasTrait(VanillaTraits.NimbleFingers))
						delayDuration = 1.00f;
					else if (invDatabase.agent.HasTrait(VanillaTraits.PoorHandEyeCoordination))
						delayDuration = 3.00f;
					else
						delayDuration = 2.00f;
				}

				yield return new WaitForSeconds(delayDuration);
				invDatabase.EquipWeapon(invDatabase.fist);
			}
		}

		[HarmonyPrefix, HarmonyPatch(nameof(InvDatabase.EquipWeapon), new[] { typeof(InvItem), typeof(bool) })]
		public static bool EquipWeapon_Prefix(InvDatabase __instance, InvItem item, ref bool sfx)
		{
			if (__instance.agent.isPlayer == 0 && item.invItemName == VItemName.Fist)
				sfx = false;

			return true;
		}
	}

}