using BepInEx.Logging;
using CCU.Hooks;
using CCU.Localization;
using CCU.Traits.App_AC1;
using CCU.Traits.App_AC3;
using CCU.Traits.App_BC1;
using CCU.Traits.App_BC3;
using CCU.Traits.App_BT1;
using CCU.Traits.App_EC1;
using CCU.Traits.App_EC3;
using CCU.Traits.App_ET1;
using CCU.Traits.App_ET3;
using CCU.Traits.App_FH1;
using CCU.Traits.App_FH3;
using CCU.Traits.App_HC1;
using CCU.Traits.App_HC3;
using CCU.Traits.App_HS1;
using CCU.Traits.App_HS2;
using CCU.Traits.App_HS3;
using CCU.Traits.App_LC1;
using CCU.Traits.App_LC3;
using CCU.Traits.App_SC1;
using HarmonyLib;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;

/// WARNING
/// Yes, this is awful code! I hate looking at it, I hate thinking about it.
/// I've attempted refactor about three times, wasting several hours before realizing it's not worth trying to get a perfect system here, because the base game's code is what it is and I'm forced to work with it. 
/// Any suggestions are appreciated, but it's gonna stay ugly for now.

namespace CCU.Traits.App
{
	public abstract class T_Appearance : T_CCU
	{
		public T_Appearance() : base() { }

		public abstract string[] Rolls { get; }
	}

	public static class AppearanceTools
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static void LogAppearance(Agent agent)
        {
			AgentHitbox agentHitbox = agent.tr.GetChild(0).transform.GetChild(0).GetComponent<AgentHitbox>();
			logger.LogDebug("======= APPEARANCE (" + agent.agentRealName + ")");
			logger.LogDebug("\tAccessory  :\t" + agent.inventory.startingHeadPiece);
			logger.LogDebug("\tBody Color :\t" + agentHitbox.bodyColor.ToString());
			logger.LogDebug("\tBody Type  :\t" + agent.oma.bodyType);  // dw
			logger.LogDebug("\tEye Color  :\t" + agentHitbox.eyesColor.ToString());
			logger.LogDebug("\tEye Type   :\t" + agentHitbox.eyesStrings[1]);
			logger.LogDebug("\tFacial Hair:\t" + agentHitbox.facialHairType);
			logger.LogDebug("\tFH Position:\t" + agentHitbox.facialHairPos);
			logger.LogDebug("\tHair Color :\t" + agentHitbox.hairColorName);
			logger.LogDebug("\tHair Style :\t" + agentHitbox.hairType);
			logger.LogDebug("\tLegs Color :\t" + agentHitbox.legsColor.ToString());
			logger.LogDebug("\tSkin Color :\t" + agentHitbox.skinColorName);
			
			//logger.LogDebug("\n--- EYE TYPE");
			//logger.LogDebug("Hook         :\t" + agent.GetOrAddHook<H_Agent>().eyesType); // Blank
			//logger.LogDebug("CustomCharSD :\t" + agent.customCharacterData.eyesType);
			//logger.LogDebug("OMA          :\t" + agent.oma.eyesType); // int
			//logger.LogDebug("EyesStrings  :");

			//foreach (string str in agentHitbox.eyesStrings)
			//	logger.LogDebug("\t" + str);
			//logger.LogDebug("EyesDeadStr :");
			//foreach (string str in agentHitbox.eyesDeadStrings)
			//	logger.LogDebug("\t" + str);
			//logger.LogDebug("EyesNarrowStr :");
			//foreach (string str in agentHitbox.eyesNarrowStrings)
			//	logger.LogDebug("\t" + str);
			//logger.LogDebug("EyesWideStrings :");
			//foreach (string str in agentHitbox.eyesWideStrings)
			//	logger.LogDebug("\t" + str);
		}
		public static void SetupAppearance(AgentHitbox agentHitbox)
		{
			Agent agent = agentHitbox.agent;

			if (agent.isPlayer != 0)
			{
				agent.GetOrAddHook<H_Agent>().GrabAppearance();
				RollEyeType(agentHitbox); // Does built-in setup that keeps from breaking
				return;
			}

			if (!agent.GetOrAddHook<H_Agent>().mustRollAppearance
				|| agent.customCharacterData is null)
				return;

			// Be very careful with reordering these - some of these are dependent on each other.
			RollSkinColor(agentHitbox);
			RollHairstyle(agentHitbox);
			RollFacialHair(agentHitbox);
			RollBodyColor(agentHitbox);
			RollHairColor(agentHitbox);
			RollAccessory(agentHitbox);
			RollBodyType(agentHitbox);
			RollEyeColor(agentHitbox);
			RollEyeType(agentHitbox);
			RollLegsColor(agentHitbox);
			agentHitbox.SetCantShowHairUnderHeadPiece();
			agent.GetOrAddHook<H_Agent>().mustRollAppearance = false;
		}
		private static string GetRoll<T>(AgentHitbox agentHitbox) where T : T_Appearance
		{
			Agent agent = agentHitbox.agent;
			List<string> pool = agent.GetTraits<T>().SelectMany(t => t.Rolls).ToList();

			var type = typeof(T);
            switch (true)
            {
				case var _ when type.IsAssignableFrom(typeof(T_Accessory)):
					if ((agent.HasTrait<No_Accessory_50>() && GC.percentChance(50)) ||
						(agent.HasTrait<No_Accessory_75>() && GC.percentChance(75)))
						return "";

					if (Not_Hairstyles.StaticList.Contains(agentHitbox.hairType) && !agent.HasTrait<Mask_Override>())
						pool.RemoveAll(i => Mask_Override.IncompatibleAccessories.Contains(i));

					if (pool.Count() is 0)
						return "";

					break;

				case var _ when type.IsAssignableFrom(typeof(T_BodyColor)):
					if (agent.HasTrait<Shirtless>())
						return agent.GetOrAddHook<H_Agent>().skinColor;

					if ((agent.HasTrait<Neutral_Body_50>() && GC.percentChance(50)) ||
						(agent.HasTrait<Neutral_Body_75>() && GC.percentChance(75)))
						return "White";

					if (pool.Count() is 0)
						return agent.customCharacterData.bodyColorName;

					if (agent.HasTrait<Shirtsome>())
					{
						string roll = agent.GetOrAddHook<H_Agent>().skinColor;

						while (roll == agent.GetOrAddHook<H_Agent>().skinColor)
							roll = pool[CoreTools.random.Next(pool.Count())];

						return roll;
					}
					 
					break;

				case var _ when type.IsAssignableFrom(typeof(T_BodyType)):

					break;

				case var _ when type.IsAssignableFrom(typeof(T_EyeColor)):

					if (agent.HasTrait<Beady_Eyed>())
						return agent.GetOrAddHook<H_Agent>().skinColor;

					if (pool.Count() is 0)
						return "White";

					break;

				case var _ when type.IsAssignableFrom(typeof(T_EyeType)):
					if (pool.Count() is 0)
						return agent.GetOrAddHook<H_Agent>().eyesType;

					if ((agent.HasTrait<Normal_Eyes_50>() && GC.percentChance(50)) ||
						(agent.HasTrait<Normal_Eyes_75>() && GC.percentChance(75)))
						return "Eyes";

					break;

				case var _ when type.IsAssignableFrom(typeof(T_FacialHair)):
					if ((agent.HasTrait<No_Facial_Hair_50>() && GC.percentChance(50)) ||
						(agent.HasTrait<No_Facial_Hair_75>() && GC.percentChance(75)))
						return "None";

					break;

				case var _ when type.IsAssignableFrom(typeof(T_HairColor)):
					if (Not_Hairstyles.StaticList.Contains(agentHitbox.hairType))
                    {
						if (agent.HasTrait<Matched_Masks>())
							return agent.GetOrAddHook<H_Agent>().bodyColor;
						else if (agent.HasTrait<Uncolored_Masks>())
							return "White";
					}
					else if (agent.HasTrait<Fleshy_Follicles>())
						return agent.GetOrAddHook<H_Agent>().skinColor;
					
					if (pool.Count() is 0)
						return agentHitbox.hairColorName;

					break;

				case var _ when type.IsAssignableFrom(typeof(T_LegsColor)):
					if (agent.HasTrait<Pantsless>())
						return agent.GetOrAddHook<H_Agent>().skinColor;
					 
					if (agent.HasTrait<Pantsuit>())
						return agent.GetOrAddHook<H_Agent>().bodyColor;

					if (pool.Count() is 0)
						return "Black";

					if (agent.HasTrait<Pantiful>())
					{
						string roll = agent.GetOrAddHook<H_Agent>().skinColor;

						while (roll == agent.GetOrAddHook<H_Agent>().skinColor)
							roll = pool[CoreTools.random.Next(pool.Count())];

						return roll;
					}


					break;

				case var _ when type.IsAssignableFrom(typeof(T_Hairstyle)):
					if (agent.HasTrait<Masks_50>() && pool.Any(i => Not_Hairstyles.StaticList.Contains(i)))
					{
						if (GC.percentChance(50))
                        {
							pool.RemoveAll(i => !Not_Hairstyles.StaticList.Contains(i));
							return pool[CoreTools.random.Next(pool.Count())];
						}
						else
							pool.RemoveAll(i => Not_Hairstyles.StaticList.Contains(i));
					}

					break;
			}

			//logger.LogDebug("Printing pool for (" + typeof(T) + ")");
			//foreach (string str in pool)
				//logger.LogDebug(pool.IndexOf(str) + ".\t" + str);

			if (pool.Count() is 0)
				return null;

			string result = pool[CoreTools.random.Next(pool.Count())]; // TODO: Shouldn't this throw an off by one error?
			//logger.LogDebug("RESULT: " + result);

			return result;
		}
		public static void RollAccessory(AgentHitbox agentHitbox)
		{
			Agent agent = agentHitbox.agent;

			if (!agent.GetTraits<T_Accessory>().Any())
				return;

			string roll = GetRoll<T_Accessory>(agentHitbox); 
			agent.inventory.AddStartingHeadPiece(roll, true);
			agent.inventory.startingHeadPiece = roll;
			agentHitbox.headPieceType = roll;
			agent.customCharacterData.startingHeadPiece = roll;
		}
		public static void RollBodyColor(AgentHitbox agentHitbox)
        {
			Agent agent = agentHitbox.agent;
			try { agent.GetOrAddHook<H_Agent>().bodyColor = agent.customCharacterData.bodyColorName; }
			catch { }

			if (!agent.GetTraits<T_BodyColor>().Any())
				return;

			string roll = GetRoll<T_BodyColor>(agentHitbox);
			agent.GetOrAddHook<H_Agent>().bodyColor = roll;
			agentHitbox.GetColorFromString(roll, "Body");
			agent.customCharacterData.bodyColorName = roll;
		}
		public static void RollBodyType(AgentHitbox agentHitbox)
		{
			Agent agent = agentHitbox.agent;
			try { agent.GetOrAddHook<H_Agent>().bodyType = agent.customCharacterData.bodyType; } 
			catch { }

			if (!agent.GetTraits<T_BodyType>().Any())
				return;

			string roll = GetRoll<T_BodyType>(agentHitbox);
			agent.GetOrAddHook<H_Agent>().bodyType = roll;
			agent.oma.bodyType = roll;
			agent.objectMult.bodyType = roll;
			agentHitbox.SetupBodyStrings();
			agentHitbox.agentBodyStrings.Clear();
			agentHitbox.agent.customCharacterData.bodyType = roll;

			foreach (string dir in VMapDirection.Directions)
				agentHitbox.agentBodyStrings.Add(roll + dir);
		}
		public static void RollEyeColor(AgentHitbox agentHitbox)
		{
			Agent agent = agentHitbox.agent;

			if (!agentHitbox.agent.GetTraits<T_EyeColor>().Any())
				return;

			string roll = GetRoll<T_EyeColor>(agentHitbox);
			agentHitbox.GetColorFromString(roll, "Eyes");
			agentHitbox.agent.customCharacterData.eyesColorName = roll;
		}
		public static void RollEyeType(AgentHitbox agentHitbox)
		{
			Agent agent = agentHitbox.agent;

			try
			{
				agent.GetOrAddHook<H_Agent>().eyesType = agent.customCharacterData.eyesType;
			}
			catch { }

			string roll = GetRoll<T_EyeType>(agentHitbox);
			agent.GetOrAddHook<H_Agent>().eyesType = roll;
			agent.oma.eyesType = agentHitbox.agent.oma.convertEyesTypeToInt(roll);
			agentHitbox.SetupBodyStrings();
			agent.customCharacterData.eyesType = roll;
		}
		public static void RollFacialHair(AgentHitbox agentHitbox)
		{
			Agent agent = agentHitbox.agent;

			// Default for no facial hair. Must be done for all agents.
			agentHitbox.facialHair.gameObject.SetActive(false);
			agentHitbox.facialHairWB.gameObject.SetActive(false);

			if (!agent.GetTraits<T_FacialHair>().Any())
				return;

			string roll = GetRoll<T_FacialHair>(agentHitbox);
			agentHitbox.facialHairType = roll;
			agent.oma.facialHairType = agentHitbox.agent.oma.convertFacialHairTypeToInt(agentHitbox.facialHairType);

			// If removed, causes Hair Cigarette bug
			if (agentHitbox.facialHairType != "None" && 
					agentHitbox.facialHairType != "" &&
					agentHitbox.facialHairType != null)
			{
				agentHitbox.facialHair.gameObject.SetActive(true);
				agentHitbox.facialHairWB.gameObject.SetActive(true);
			}

			agent.customCharacterData.facialHair = roll;
		}
		public static void RollHairColor(AgentHitbox agentHitbox)
		{
			Agent agent = agentHitbox.agent;

			if (!agent.GetTraits<T_HairColor>().Any()) 
				return;

			string roll = GetRoll<T_HairColor>(agentHitbox);
			string skinColorChoice = (string)AccessTools.DeclaredField(typeof(AgentHitbox), "skinColorChoice").GetValue(agentHitbox);

			if (!Not_Hairstyles.StaticList.Contains(roll) && !agent.HasTrait<Melanin_Mashup>() && !agent.HasTrait<Fleshy_Follicles>() &&
				(skinColorChoice == "BlackSkin" || skinColorChoice == "LightBlackSkin") &&  roll != "Grey" && roll != "White")
				roll = "Black";

			agent.oma.hairColor = agent.oma.convertColorToInt(roll);
			agentHitbox.GetColorFromString(roll, "Hair");
			agentHitbox.hairColorName = roll;
			agentHitbox.facialHairColor = agentHitbox.hairColor;
			agentHitbox.facialHairColorName = agentHitbox.hairColorName;

			agent.customCharacterData.hairColorName = roll;
		}
		public static void RollHairstyle(AgentHitbox agentHitbox)
		{
			Agent agent = agentHitbox.agent;

			if (!agent.GetTraits<T_Hairstyle>().Any())
				return;

			string roll = GetRoll<T_Hairstyle>(agentHitbox);

			if (agent.agentName != "MechEmpty" && !agent.isDummy &&
				((GC.levelFeeling == "PlanetOfApes" && agentHitbox.agent.isPlayer == 0) ||
				(GC.challenges.Contains(VanillaMutators.GorillaTown) && GC.levelType != "HomeBase" && !agent.inhuman)))
				roll = "GorillaHead";

			agentHitbox.hairType = roll;
			agent.oma.hairType = agent.oma.convertHairTypeToInt(agentHitbox.hairType);

			if (agentHitbox.hairType == "RobotPlayerHead" || agentHitbox.hairType == "CopBotHead" || agentHitbox.hairType == "ButlerBotHead")
				agentHitbox.headPiecePosY = 0.28f;
			else
				agentHitbox.headPiecePosY = 0.16f;

			agent.customCharacterData.hairType = roll;
		}
		public static void RollLegsColor(AgentHitbox agentHitbox)
		{
			Agent agent = agentHitbox.agent;

			if (!agent.HasTrait<T_LegsColor>())
				return;

			string roll = GetRoll<T_LegsColor>(agentHitbox);
			agentHitbox.GetColorFromString(roll, "Legs");

			agent.customCharacterData.legsColorName = roll;
		}
		public static void RollSkinColor(AgentHitbox agentHitbox)
		{
			Agent agent = agentHitbox.agent;

			if (!agent.HasTrait<T_SkinColor>())
				return;

			string roll = GetRoll<T_SkinColor>(agentHitbox);
			agent.GetOrAddHook<H_Agent>().skinColor = roll;
			AccessTools.DeclaredField(typeof(AgentHitbox), "skinColorChoice").SetValue(agentHitbox, roll);
			agentHitbox.GetColorFromString(roll, "Skin");
			agentHitbox.skinColorName = roll;

			agent.customCharacterData.skinColorName = roll;
		}
	}
}