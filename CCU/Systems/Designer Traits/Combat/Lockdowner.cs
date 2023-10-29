using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using System;

namespace CCU.Traits.Combat_
{
	public class Lockdowner : T_Combat
	{
		//[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Lockdowner>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character will activate a Lockdown when they enter combat. \n\n<color=red>Requires:</color> Lockdown Walls on level"),
					[LanguageCode.Spanish] = "Este NPC inicia un Bloqueo Policial al entrar en combate. \n\n<color=red>Require:</color> Paredes de bloqueo en el piso",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Lockdowner)),
					[LanguageCode.Spanish] = "Aislador",


				})
				.WithUnlock(new TraitUnlock_CCU
				{
					Cancellations = { },
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}

	[HarmonyPatch(typeof(GoalCombatEngage))]
	public static class P_GoalCombatEngage
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		/// <summary>
		/// Lockdowner (Shelved, possibly a vanilla bug with lockdown walls on custom levels)
		/// </summary>
		/// <param name="__instance"></param>
		//[HarmonyPostfix, HarmonyPatch(methodName:nameof(GoalCombatEngage.Process), new Type[0] { })]
		public static void Process_Postfix(GoalCombatEngage __instance)
		{
			if (__instance.gc.loadLevel.hasLockdownWalls &&
				__instance.agent.HasTrait<Lockdowner>() &&
				__instance.agent.curTileData.lockdownZone == __instance.battlingAgent.curTileData.lockdownZone && !__instance.agent.curTileData.lockdownWall && !__instance.agent.curTileData.dangerousToWalk &&
				(AlarmButton.lockdownTimer < 5f || !__instance.gc.lockdown) &&
				__instance.battlingAgent.isPlayer != 0)
			{
				__instance.agent.CauseLockdown();
			}
		}
	}
}