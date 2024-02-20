using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using System;
using UnityEngine;

namespace CCU.Traits.Player.Movement
{
	public class Blinker : T_PlayerTrait
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_PlayerTrait = RogueLibs.CreateCustomTrait<Blinker>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("When damaged, instantly teleports to a random nearby spot."),
					[LanguageCode.Spanish] = "Al sufrir daño, este NPC se teletransporta instantaneamente a un lugar cercano.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Blinker)),
					[LanguageCode.Spanish] = "Teleportivo",
				})
				.WithUnlock(new TU_PlayerUnlock
				{
					Cancellations = { },
					CharacterCreationCost = 5,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					
					UnlockCost = 7,
					Unlock =
					{
						categories = { VTraitCategory.Movement },
					}
				});
		}
		
		
	}

	[HarmonyPatch(typeof(StatusEffects))]
	class P_StatusEffects
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(nameof(StatusEffects.UseQuickEscapeTeleporter))]
		public static bool UseQuickEscapeTeleporter_Blinker(StatusEffects __instance)
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
}