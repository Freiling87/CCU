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

		// TODO: Untested after refactor
		public static void RollFacialHair(AgentHitbox agentHitBox)
		{
			List<T_FacialHair> pool = agentHitBox.agent.GetTraits<T_FacialHair>().ToList();

			if (pool.Count == 0 || agentHitBox.agent.agentName != VanillaAgents.CustomCharacter)
				return;

			var random = new Random();
			T_FacialHair selection = pool[random.Next(pool.Count)];
			string selectionName = selection.FacialHairType;
			agentHitBox.facialHairType = selectionName;
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
