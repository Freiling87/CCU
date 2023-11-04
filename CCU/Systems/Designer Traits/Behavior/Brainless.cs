using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using System;

namespace CCU.Traits.Behavior
{
	public class Brainless : T_Behavior
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Brainless>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This agent won't react to anything."),
					[LanguageCode.Spanish] = "Este NPC no reacciona a nada, pero le gusta ver youtubers de drama de ves en cuando",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Brainless)),
					[LanguageCode.Spanish] = "Descerebrado",
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
		
		

		public override void SetupAgent(Agent agent) { }
	}

	[HarmonyPatch(typeof(Agent))]
	public class P_Agent_Brainless
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(nameof(Agent.SetBrainActive))]
		public static bool SetBrainActive_Prefix(Agent __instance, ref bool isActive)
		{
			if (__instance.HasTrait<Brainless>())
			{
				isActive = false;
				__instance.brain.active = false;
				__instance.interactable = false;
			}

			return true;
		}
	}

	[HarmonyPatch(typeof(BrainUpdate))]
	public static class P_BrainUpdate_MyUpdate
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(nameof(BrainUpdate.MyUpdate))]
		public static bool MyUpdate_Prefix(Agent ___agent)
		{
			if (___agent.HasTrait<Brainless>())
				return false;

			return true;
		}
	}

	[HarmonyPatch(typeof(InteractionHelper))]
	public static class P_InteractionHelper
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(nameof(InteractionHelper.CanInteractWithAgent))]
		public static bool CanInteractWithAgent_Prefix(Agent otherAgent, ref bool __result)
		{
			__result = !otherAgent.HasTrait<Brainless>();

			return __result;
		}
	}
}