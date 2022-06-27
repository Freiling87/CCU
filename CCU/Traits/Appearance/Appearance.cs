using BepInEx.Logging;
using CCU.Traits.Facial_Hair;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CCU.Patches.Appearance
{
	public static class Appearance
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static void RollFacialHair(AgentHitbox agentHitBox)
		{
			Core.LogMethodCall();

			var random = new Random();

			// TODO: This was changed to account for class refactor, but the code is untested.
			// The previous setup was to treat the trait names as the strings for facial hair types
			// Instead, you'll need to get override strings from T_FacialHair children.
			// Basically, don't expect anything below to work until you redo it.
			List<T_FacialHair> pool = agentHitBox.agent.GetTraits<T_FacialHair>().ToList();

			logger.LogDebug("\tpool: " + pool.Count);

			if (pool.Count == 0)
				return;

			CustomTrait selection = pool[random.Next(pool.Count)];
			string selectionName = selection.Trait.traitName; 
			agentHitBox.facialHairType = selectionName.Substring(selectionName.IndexOf(" - ") + 1); 
			agentHitBox.agent.oma.facialHairType = agentHitBox.agent.oma.convertFacialHairTypeToInt(agentHitBox.facialHairType);

			if (agentHitBox.facialHairType == "None" || agentHitBox.facialHairType == "" || agentHitBox.facialHairType == null)
			{
				agentHitBox.facialHair.gameObject.SetActive(false);
				agentHitBox.facialHairWB.gameObject.SetActive(false);
			}
			else
			{
				agentHitBox.facialHair.gameObject.SetActive(true);
				agentHitBox.facialHairWB.gameObject.SetActive(true);
			}
		}
		public static void RollHairColor(AgentHitbox agentHitBox)
		{
		}
		public static void RollHairstyle(AgentHitbox agentHitBox)
		{
		}
		public static void RollSkinColor(AgentHitbox agentHitBox)
		{
		}
	}
}
