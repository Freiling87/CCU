using BepInEx.Logging;
using CCU.Traits.Appearance.FacialHair;
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

		#region Trait Lists
		public static List<string> AppearanceTraits = new List<string>()
		{
			CTrait.FacialHair_Beard,
			CTrait.FacialHair_Mustache,
			CTrait.FacialHair_MustacheCircus,
			CTrait.FacialHair_MustacheRedneck,
			CTrait.FacialHair_None,
			CTrait.HairColor_Black,
			CTrait.HairColor_Blonde,
			CTrait.HairColor_Blue,
			CTrait.HairColor_Brown,
			CTrait.HairColor_Green,
			CTrait.HairColor_Grey,
			CTrait.HairColor_Orange,
			CTrait.HairColor_Pink,
			CTrait.HairColor_Purple,
			CTrait.HairColor_Red,
			CTrait.Hairstyle_Afro,
			CTrait.Hairstyle_Bald,
			CTrait.Hairstyle_Balding,
			CTrait.Hairstyle_BangsLong,
			CTrait.Hairstyle_BangsMedium,
			CTrait.Hairstyle_Curtains,
			CTrait.Hairstyle_Cutoff,
			CTrait.Hairstyle_FlatLong,
			CTrait.Hairstyle_HoboBeard,
			CTrait.Hairstyle_Leia,
			CTrait.Hairstyle_MessyLong,
			CTrait.Hairstyle_Military,
			CTrait.Hairstyle_Mohawk,
			CTrait.Hairstyle_Normal,
			CTrait.Hairstyle_NormalHigh,
			CTrait.Hairstyle_Pompadour,
			CTrait.Hairstyle_Ponytail,
			CTrait.Hairstyle_PuffyLong,
			CTrait.Hairstyle_PuffyShort,
			CTrait.Hairstyle_Sidewinder,
			CTrait.Hairstyle_Spiky,
			CTrait.Hairstyle_SpikyShort,
			CTrait.Hairstyle_Suave,
			CTrait.Hairstyle_Wave,
			CTrait.SkinColor_Black,
			CTrait.SkinColor_Gold,
			CTrait.SkinColor_LightBlack,
			CTrait.SkinColor_Mixed,
			CTrait.SkinColor_Pale,
			CTrait.SkinColor_Pink,
			CTrait.SkinColor_SuperPale,
			CTrait.SkinColor_White,
			CTrait.SkinColor_Zombie1,
			CTrait.SkinColor_Zombie2
		};
		private static List<Type> FacialHairTraits = new List<Type>()
		{
			typeof(Beard),
			typeof(Mustache),
			typeof(MustacheCircus),
			typeof(MustacheRedneck),
			typeof(NoFacialHair)
		};
		private static List<string> HairColorTraits = new List<string>()
		{
			CTrait.HairColor_Black,
			CTrait.HairColor_Blonde,
			CTrait.HairColor_Blue,
			CTrait.HairColor_Brown,
			CTrait.HairColor_Green,
			CTrait.HairColor_Grey,
			CTrait.HairColor_Orange,
			CTrait.HairColor_Pink,
			CTrait.HairColor_Purple,
			CTrait.HairColor_Red
		};
		private static List<string> HairstyleTraits = new List<string>()
		{
			CTrait.Hairstyle_Afro,
			CTrait.Hairstyle_Bald,
			CTrait.Hairstyle_Balding,
			CTrait.Hairstyle_BangsLong,
			CTrait.Hairstyle_BangsMedium,
			CTrait.Hairstyle_Curtains,
			CTrait.Hairstyle_Cutoff,
			CTrait.Hairstyle_FlatLong,
			CTrait.Hairstyle_HoboBeard,
			CTrait.Hairstyle_Leia,
			CTrait.Hairstyle_MessyLong,
			CTrait.Hairstyle_Military,
			CTrait.Hairstyle_Mohawk,
			CTrait.Hairstyle_Normal,
			CTrait.Hairstyle_NormalHigh,
			CTrait.Hairstyle_Pompadour,
			CTrait.Hairstyle_Ponytail,
			CTrait.Hairstyle_PuffyLong,
			CTrait.Hairstyle_PuffyShort,
			CTrait.Hairstyle_Sidewinder,
			CTrait.Hairstyle_Spiky,
			CTrait.Hairstyle_SpikyShort,
			CTrait.Hairstyle_Suave,
			CTrait.Hairstyle_Wave
		};
		private static List<string> SkinColorTraits = new List<string>()
		{
			CTrait.SkinColor_Black,
			CTrait.SkinColor_Gold,
			CTrait.SkinColor_LightBlack,
			CTrait.SkinColor_Mixed,
			CTrait.SkinColor_Pale,
			CTrait.SkinColor_Pink,
			CTrait.SkinColor_SuperPale,
			CTrait.SkinColor_White,
			CTrait.SkinColor_Zombie1,
			CTrait.SkinColor_Zombie2
		};
		#endregion
		public static void RollFacialHair(AgentHitbox agentHitBox)
		{
			Core.LogMethodCall();

			var random = new Random();

			List<CustomTrait> pool = agentHitBox.agent.GetTraits<CustomTrait>()
				.Where(trait => FacialHairTraits.Contains(trait.GetType()))
				.ToList();

			logger.LogDebug("\tpool: " + pool.Count);

			if (pool.Count == 0)
				return;

			CustomTrait selection = pool[random.Next(pool.Count)];
			string selectionName = selection.Trait.traitName;
			logger.LogDebug("\tselectionName: " + selectionName);
			agentHitBox.facialHairType = selectionName.Substring(selectionName.IndexOf(" - ") + 1);
			logger.LogDebug("\tfacialHairType: " + agentHitBox.facialHairType);
			agentHitBox.agent.oma.facialHairType = agentHitBox.agent.oma.convertFacialHairTypeToInt(agentHitBox.facialHairType);
			logger.LogDebug("\tfacialHairType = " + agentHitBox.agent.oma.facialHairType);

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
