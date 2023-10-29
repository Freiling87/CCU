using BepInEx.Logging;
using BunnyLibs;
using HarmonyLib;
using RogueLibsCore;

namespace CCU.Traits.Drug_Warrior
{
	public abstract class T_DrugWarrior : T_CCU, ISetupAgentStats
	{
		public T_DrugWarrior() : base() { }

		public abstract string DrugEffect { get; }

		public bool BypassUnlockChecks => false;
		public void SetupAgent(Agent agent)
		{
			agent.combat.canTakeDrugs = true;
		}
	}

	[HarmonyPatch(typeof(StatusEffects))]
	class P_StatusEffects
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;


		[HarmonyPrefix, HarmonyPatch(nameof(StatusEffects.ChooseRandomDrugDealerStatusEffect))]
		public static bool ChooseRandomDrugDealerStatusEffect_Prefix(StatusEffects __instance, ref string __result)
		{
			T_DrugWarrior trait = __instance.agent.GetTrait<T_DrugWarrior>();

			if (trait is null || trait is Wildcard)
				return true;

			__result = trait.DrugEffect;
			return false;
		}
	}
}