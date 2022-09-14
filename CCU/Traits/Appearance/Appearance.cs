using BepInEx.Logging;
using CCU.Traits.Facial_Hair;
using CCU.Traits.Hairstyle;
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
			if (agentHitbox.agent.agentName != VanillaAgents.CustomCharacter ||
				agentHitbox.agent.isPlayer != 0)
				return;

			RollSkinColor(agentHitbox);
			RollHairstyle(agentHitbox);
			RollFacialHair(agentHitbox);
			agentHitbox.SetCantShowHairUnderHeadPiece();
			RollHairColor(agentHitbox);
        }

		public static void RollFacialHair(AgentHitbox agentHitbox)
		{
			List<T_FacialHair> pool = agentHitbox.agent.GetTraits<T_FacialHair>().ToList();

			if (pool.Count == 0 || agentHitbox.agent.agentName != VanillaAgents.CustomCharacter)
				return;

			T_FacialHair selection = pool[CoreTools.random.Next(pool.Count)];
			string selectionName = selection.FacialHairType;
			agentHitbox.facialHairType = selectionName;
			agentHitbox.agent.oma.facialHairType = agentHitbox.agent.oma.convertFacialHairTypeToInt(agentHitbox.facialHairType);

			if (agentHitbox.facialHairType == "None" || agentHitbox.facialHairType == "" || agentHitbox.facialHairType == null)
			{
				agentHitbox.facialHair.gameObject.SetActive(false);
				agentHitbox.facialHairWB.gameObject.SetActive(false);
			}
			else
			{
				agentHitbox.facialHair.gameObject.SetActive(true);
				agentHitbox.facialHairWB.gameObject.SetActive(true);
			}
		}
		public static Color32 RollHairColor(AgentHitbox agentHitbox)
		{ 
			return new Color32();
		}
		public static void RollHairstyle(AgentHitbox agentHitbox)
		{
			try
			{
				List<string> pool = new List<string>();

				foreach (T_Hairstyle trait in agentHitbox.agent.GetTraits<T_Hairstyle>())
					pool.AddRange(trait.HairstyleType);

				if (pool.Count == 0 || agentHitbox.agent.agentName != VanillaAgents.CustomCharacter)
					return;

				string selectionName = pool[CoreTools.random.Next(pool.Count)];

				if (GC.serverPlayer || GC.clientControlling || agentHitbox.agent.localPlayer || !(agentHitbox.agent.name != "DummyAgent"))
				{
					if (agentHitbox.agent.agentName != "MechEmpty" && !agentHitbox.agent.isDummy &&
						((GC.levelFeeling == "PlanetOfApes" && agentHitbox.agent.isPlayer == 0) ||
						(GC.challenges.Contains(VanillaMutators.GorillaTown) && GC.levelType != "HomeBase" && !agentHitbox.agent.inhuman)))
						agentHitbox.hairType = "GorillaHead";
					else if (agentHitbox.agent.agentName != "Custom" || !agentHitbox.agent.localPlayer)
						agentHitbox.hairType = selectionName;

					agentHitbox.agent.oma.hairType = agentHitbox.agent.oma.convertHairTypeToInt(agentHitbox.hairType);
				}
				else
					agentHitbox.hairType = agentHitbox.agent.oma.convertIntToHairType(agentHitbox.agent.oma.hairType);
			}
			catch
			{
				agentHitbox.hairType = "NormalHigh";
			}
		}
		public static Color32 RollSkinColor(AgentHitbox agentHitbox)
		{
			return new Color32();
		}
	}
}
