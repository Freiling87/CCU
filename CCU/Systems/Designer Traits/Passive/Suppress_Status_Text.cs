using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using System;
using UnityEngine.Networking;

namespace CCU.Traits.Passive
{
	public class Suppress_Status_Text : T_PlayerTrait
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_PlayerTrait = RogueLibs.CreateCustomTrait<Suppress_Status_Text>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Prevents Status Effect text popup when the agent receives a new status effect."),
					[LanguageCode.Spanish] = "El texto de efectos de este personaje se mantiene oculto.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Suppress_Status_Text)),
					[LanguageCode.Spanish] = "Suprimir Texto de Efecto",

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
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}

	[HarmonyPatch(typeof(SpawnerMain))]
	public static class P_SpawnerMain
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(nameof(SpawnerMain.SpawnStatusText), new[] { typeof(PlayfieldObject), typeof(string), typeof(string), typeof(string), typeof(NetworkInstanceId), typeof(string), typeof(string) })]
		public static bool SpawnStatusText_GateAV(PlayfieldObject myPlayfieldObject, string textType)
		{
			if (textType == "Buff"
				&& myPlayfieldObject is Agent agent
				&& agent.HasTrait<Suppress_Status_Text>())
				return false;

			return true;
		}
	}
}