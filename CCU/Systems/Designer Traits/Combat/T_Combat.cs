using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;

namespace CCU.Traits.Combat_
{
	public abstract class T_Combat : T_DesignerTrait
	{
		public T_Combat() : base() { }
	}


	[HarmonyPatch(typeof(Relationships))]
	public static class P_Relationships
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(nameof(Relationships.AssessBattle))]
		public static bool AssessBattle_CowardFearless(Agent ___agent, ref float __result)
		{
			if (___agent.HasTrait<Coward>())
			{
				__result = 0f;
				return false;
			}
			else if (___agent.HasTrait<Fearless>())
			{
				__result = 9999f;
				return false;
			}

			return true;
		}
		[HarmonyPrefix, HarmonyPatch(nameof(Relationships.AssessFlee))]
		public static bool AssessFlee_CowardFearless(Agent ___agent, ref float __result)
		{
			if (___agent.HasTrait<Coward>())
			{
				__result = 9999f;
				return false;
			}
			else if (___agent.HasTrait<Fearless>())
			{
				__result = 0f;
				return false;
			}

			return true;
		}
	}
}
