using BepInEx.Logging;
using CCU.Challenges.Followers;
using CCU.Traits.Gib_Type;
using CCU.Traits.Hire_Duration;
using CCU.Traits.Player.Language;
using HarmonyLib;
using RogueLibsCore;
using System.Linq;

namespace CCU.Traits
{
	public interface ISetupAgentStats
    {
        void SetupAgentStats(Agent agent);
	}

	[HarmonyPatch(declaringType: typeof(Agent))]
	public class P_Agent_ISetupAgentStats
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(nameof(Agent.SetupAgentStats))]
		public static void ApplySetupAgentStats(string transformationType, Agent __instance)
		{
			foreach (ISetupAgentStats trait in __instance.GetTraits<ISetupAgentStats>())
				trait.SetupAgentStats(__instance);

			if (!__instance.GetTraits<T_GibType>().Any())
				__instance.AddTrait<Meat_Chunks>();

			T_Language.SetupAgent(__instance);

			// Negatives allow traits to take precedence over mutators.
			if ((GC.challenges.Contains(nameof(Homesickness_Disabled)) && !__instance.HasTrait<Homesickly>()) ||
				__instance.HasTrait<Homesickless>())
				__instance.canGoBetweenLevels = true;
			else if ((GC.challenges.Contains(nameof(Homesickness_Mandatory)) && !__instance.HasTrait<Homesickless>()) ||
				__instance.HasTrait<Homesickly>())
				__instance.canGoBetweenLevels = false;
		}
	}
}