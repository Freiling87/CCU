using BepInEx.Logging;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;

namespace CCU.Traits
{
    public static class TraitManager
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static void LogTraitList(Agent agent)
		{
			logger.LogDebug("TRAIT LIST: " + agent.agentRealName);

			foreach (Trait trait in agent.statusEffects.TraitList)
				logger.LogDebug("\t" + trait.traitName);
		}
		internal static List<Trait> OnlyNonhiddenTraits(List<Trait> traitList) =>
			traitList.Where(trait => trait.GetHook<T_CCU>() is null).ToList();
	}
}
