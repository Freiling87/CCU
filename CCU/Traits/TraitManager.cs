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

		public static readonly List<KeyValuePair<string, string>> Groups = new List<KeyValuePair<string, string>>()
		{
			new KeyValuePair<string , string> ("Appearance", "App" ),
			new KeyValuePair<string , string> ("Behavior", "Behavior" ),

		};

		//[RLSetup]
		public static void Setup()
		{
			logger.LogDebug("Trait count: " + RogueFramework.Unlocks.OfType<TraitUnlock_CCU>().Count());

			foreach (KeyValuePair<string, string> group in Groups)
            {
				TraitUnlock_CCU unlock = new TraitUnlock_CCU(group.Key, group.Value);
				RogueLibs.CreateCustomUnlock(unlock)
					.WithDescription(new CustomNameInfo
					{
						[LanguageCode.English] = "Hides/Shows items in this group in the trait list.",
					})
					.WithName(new CustomNameInfo
					{
						[LanguageCode.English] = "[CCU] " + group.Key + " (+)",
					});
			}

		}

		public static void LogTraitList(Agent agent)
		{
			logger.LogDebug("TRAIT LIST: " + agent.agentRealName);

			foreach (Trait trait in agent.statusEffects.TraitList)
			{
				logger.LogDebug("\t" + trait.GetType().Namespace + ":\t" + trait.traitName);
			}
		}
	}

    public class TraitGroup : T_CCU
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public TraitGroup() : base() { }


		public override void OnAdded()
		{
		}
		public override void OnRemoved() { }
    }
}