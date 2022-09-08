using BepInEx.Logging;
using CCU.Traits.Facial_Hair;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CCU.Patches.Appearance
{
    public static class Appearance
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static void SetupAppearance(AgentHitbox agentHitbox)
        {
			if (agentHitbox.agent.agentName != VanillaAgents.CustomCharacter)
				return;

			//RollFacialHair(agentHitbox); // Might need special treatment, verify
			RollHairColor(agentHitbox);
			RollHairstyle(agentHitbox);
			RollSkinColor(agentHitbox);
        }

		// TODO: Untested after refactor
		public static string RollFacialHair(AgentHitbox agentHitBox)
		{
			List<T_FacialHair> pool = agentHitBox.agent.GetTraits<T_FacialHair>().ToList();

			if (pool.Count == 0 || agentHitBox.agent.agentName != VanillaAgents.CustomCharacter)
				return null; // Change if used as other than void

			var random = new System.Random();
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

			return selectionName;
		}
		public static Color32 RollHairColor(AgentHitbox agentHitbox)
		{
			return new Color32();
		}
		public static string RollHairstyle(AgentHitbox agentHitBox)
		{
			return "";
		}
		public static Color32 RollSkinColor(AgentHitbox agentHitBox)
		{
			return new Color32();
		}
	}
}
