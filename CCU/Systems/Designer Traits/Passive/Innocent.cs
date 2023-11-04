using BepInEx.Logging;
using BunnyLibs;
using HarmonyLib;
using RogueLibsCore;
using System;

namespace CCU.Traits.Passive
{
	public class Innocent : T_DesignerTrait, ISetupAgentStats
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Innocent>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character will never be designated Guilty."),
					[LanguageCode.Spanish] = "Este NPC no puede volverse nunca culpable.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Innocent)),
					[LanguageCode.Spanish] = "Inocente",

				})
				.WithUnlock(new TU_DesignerUnlock
				{
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		
		

		public bool BypassUnlockChecks => false;
		public void SetupAgent(Agent agent)
		{
			agent.oma.mustBeGuilty = false;
			agent.oma.mustBeInnocent = true;
			agent.oma._mustBeInnocent = true;
		}
	}

	[HarmonyPatch(typeof(StatusEffects))]
	class P_StatusEffects_GuiltyInnocent
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(nameof(StatusEffects.IsInnocent))]
		public static bool IsInnocent_Prefix(StatusEffects __instance, ref bool __result)
		{
			if (__instance.agent.HasTrait<Innocent>())
			{
				__result = true;
				return false;
			}
			else if (__instance.agent.HasTrait<Guilty>())
			{
				__result = false;
				return false;
			}

			return true;
		}
	}
}