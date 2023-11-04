using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using System;

namespace CCU.Traits.Passive
{
	public class Extortable : T_DesignerTrait
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Extortable>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character can be extorted for income, if the player has the Extortionist trait."),
					[LanguageCode.Spanish] = "Este NPC puede ser extorcionado si el jugador tiene el rasgo Extorsionador.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Extortable)),
					[LanguageCode.Spanish] = "Extorsionable",

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
		
		
	}

	[HarmonyPatch(typeof(Agent))]
	internal static class P_Agent_Extortable
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(nameof(Agent.CanShakeDown))]
		public static void CanShakeDown_Postfix(Agent __instance, ref bool __result)
		{
			if (__instance.HasTrait<Extortable>())
			{
				if (__instance.oma.shookDown || GC.loadLevel.LevelContainsMayor())
				{
					__result = false;
					return;
				}
				else
					__result = true;

				foreach (Agent agent in GC.agentList)
					if (agent.startingChunk == __instance.startingChunk && agent.ownerID == __instance.ownerID && agent.ownerID != 255 && agent.ownerID != 99 && __instance.ownerID != 0 && agent.oma.shookDown)
						__result = false;
			}
		}
	}
}