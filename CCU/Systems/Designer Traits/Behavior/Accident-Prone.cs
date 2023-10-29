using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using System;

namespace CCU.Traits.Behavior
{
	public class Accident_Prone : T_Behavior
	{
		public bool BypassUnlockChecks => false;
		public override void SetupAgent(Agent agent)
		{
			agent.dontStopForDanger = true;
		}

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Accident_Prone>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format($"This character will not path around Crushers, Fire Spewers, Killer Plants, Laser Emitters & Sawblades.\n" +
						"<color=green>{0}</color>: Will try to pick up armed traps.", LongishDocumentationName(typeof(LOS_Behavior.Grab_Everything))),
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Accident_Prone), "Accident-Prone"),
					[LanguageCode.Spanish] = "Accidentado-Compulsivo",
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
	public class P_Agent_AccidentProne
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(nameof(Agent.SetTraversable))]
		public static bool SetTraversable_AccidentProne(Agent __instance, ref string type)
		{
			if (__instance.HasTrait<Accident_Prone>())
				type = "TraverseAll";

			return true;
		}
	}
}