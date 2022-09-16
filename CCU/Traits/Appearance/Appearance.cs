using BepInEx.Logging;
using CCU.Traits.Facial_Hair;
using CCU.Traits.Hair_Color;
using CCU.Traits.Hairstyle;
using CCU.Traits.Hairstyle_Grouped;
using HarmonyLib;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

		// TODO: Combine the redundant (randomization) portions of the methods below into one.

		public static void RollFacialHair(AgentHitbox agentHitbox)
		{
			List<T_FacialHair> pool = agentHitbox.agent.GetTraits<T_FacialHair>().ToList();

			if (pool.Count == 0)
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
		public static void RollHairColor(AgentHitbox agentHitbox)
		{
			Core.LogMethodCall();
			string text;

			if (GC.serverPlayer || GC.clientControlling || agentHitbox.agent.localPlayer || !(agentHitbox.agent.name != "DummyAgent"))
			{
				try
				{
					List<string> pool = new List<string>();

					foreach (T_HairColor trait in agentHitbox.agent.GetTraits<T_HairColor>())
					{

						pool.AddRange(trait.HairColors);
					}

					logger.LogDebug("Pool: " + pool.Count);

					if (pool.Count == 0)
						return;

					text = pool[CoreTools.random.Next(pool.Count)];
				}
				catch
				{
					text = "Brown";
				}

				string skinColorChoice = (string)AccessTools.DeclaredField(typeof(AgentHitbox), "skinColorChoice").GetValue(agentHitbox);

				logger.LogDebug("SkinColor: " + skinColorChoice);

				if ((skinColorChoice == "BlackSkin" || skinColorChoice == "LightBlackSkin") && text != "Grey" && text != "White") // Added white ✊👴
					text = "Black";
				
				agentHitbox.agent.oma.hairColor = agentHitbox.agent.oma.convertColorToInt(text);
			}
			else
				text = agentHitbox.agent.oma.convertIntToColor(agentHitbox.agent.oma.hairColor);

			logger.LogDebug("Hair color chosen: " + text);
			agentHitbox.GetColorFromString(text, "Hair");
			agentHitbox.hairColorName = text;
			agentHitbox.facialHairColor = agentHitbox.hairColor;
			agentHitbox.facialHairColorName = agentHitbox.hairColorName;

			if (Not_Hairstyles.StaticList.Contains(agentHitbox.hairType))
			{
				agentHitbox.hairColor = AgentHitbox.white;
				agentHitbox.hairColorName = "White";

				if (GC.serverPlayer)
					agentHitbox.agent.oma.hairColor = agentHitbox.agent.oma.convertColorToInt("White");
			}

			return;
		}
		public static void RollHairstyle(AgentHitbox agentHitbox)
		{
			try
			{
				List<string> pool = new List<string>();

				foreach (T_Hairstyle trait in agentHitbox.agent.GetTraits<T_Hairstyle>())
					pool.AddRange(trait.HairstyleTypes);

				if (pool.Count == 0)
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
