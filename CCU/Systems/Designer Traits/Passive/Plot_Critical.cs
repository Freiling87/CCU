using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using System;

namespace CCU.Traits.Passive
{
	public class Plot_Critical : T_CCU
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Plot_Critical>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("If this Agent is neutralized, the players lose."),

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Plot_Critical)),

				})
				.WithUnlock(new TU_DesignerUnlock
				{
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}

	[HarmonyPatch(typeof(StatusEffects))]
	internal static class P_StatusEffects_PlotCritical
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		private static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(typeof(StatusEffects), nameof(StatusEffects.SetupDeath), new[] { typeof(PlayfieldObject), typeof(bool), typeof(bool) })]
		private static bool ApplyOnDeathEffects(StatusEffects __instance)
		{
			if (__instance.agent.HasTrait<Plot_Critical>())
			{
				foreach (Agent playerAgent in GC.playerAgentList)
					playerAgent.StartCoroutine("SuicideWhenPossible");
			}

			return true;
		}
	}
}