using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using BTHarmonyUtils;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Traits.FacialHair;

namespace CCU.Patches.Appearance
{
	public static class Appearance
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		#region Trait Lists
		public static string indicator = " - "; // Indicates where string content in Trait name begins

		public static List<string> AppearanceTraits = new List<string>()
		{
			CTrait.Appearance_FacialHair_Beard,
			CTrait.Appearance_FacialHair_Mustache,
			CTrait.Appearance_FacialHair_MustacheCircus,
			CTrait.Appearance_FacialHair_MustacheRedneck,
			CTrait.Appearance_FacialHair_None,
			CTrait.Appearance_HairColor_Black,
			CTrait.Appearance_HairColor_Blonde,
			CTrait.Appearance_HairColor_Blue,
			CTrait.Appearance_HairColor_Brown,
			CTrait.Appearance_HairColor_Green,
			CTrait.Appearance_HairColor_Grey,
			CTrait.Appearance_HairColor_Orange,
			CTrait.Appearance_HairColor_Pink,
			CTrait.Appearance_HairColor_Purple,
			CTrait.Appearance_HairColor_Red,
			CTrait.Appearance_Hairstyle_Afro,
			CTrait.Appearance_Hairstyle_Bald,
			CTrait.Appearance_Hairstyle_Balding,
			CTrait.Appearance_Hairstyle_BangsLong,
			CTrait.Appearance_Hairstyle_BangsMedium,
			CTrait.Appearance_Hairstyle_Curtains,
			CTrait.Appearance_Hairstyle_Cutoff,
			CTrait.Appearance_Hairstyle_FlatLong,
			CTrait.Appearance_Hairstyle_HoboBeard,
			CTrait.Appearance_Hairstyle_Leia,
			CTrait.Appearance_Hairstyle_MessyLong,
			CTrait.Appearance_Hairstyle_Military,
			CTrait.Appearance_Hairstyle_Mohawk,
			CTrait.Appearance_Hairstyle_Normal,
			CTrait.Appearance_Hairstyle_NormalHigh,
			CTrait.Appearance_Hairstyle_Pompadour,
			CTrait.Appearance_Hairstyle_Ponytail,
			CTrait.Appearance_Hairstyle_PuffyLong,
			CTrait.Appearance_Hairstyle_PuffyShort,
			CTrait.Appearance_Hairstyle_Sidewinder,
			CTrait.Appearance_Hairstyle_Spiky,
			CTrait.Appearance_Hairstyle_SpikyShort,
			CTrait.Appearance_Hairstyle_Suave,
			CTrait.Appearance_Hairstyle_Wave,
			CTrait.Appearance_SkinColor_BlackSkin,
			CTrait.Appearance_SkinColor_GoldSkin,
			CTrait.Appearance_SkinColor_LightBlackSkin,
			CTrait.Appearance_SkinColor_MixedSkin,
			CTrait.Appearance_SkinColor_PaleSkin,
			CTrait.Appearance_SkinColor_PinkSkin,
			CTrait.Appearance_SkinColor_SuperPaleSkin,
			CTrait.Appearance_SkinColor_WhiteSkin,
			CTrait.Appearance_SkinColor_ZombieSkin1,
			CTrait.Appearance_SkinColor_ZombieSkin2
		};
		private static List<Type> FacialHairTraits = new List<Type>()
		{
			typeof(Beard),
			typeof(Mustache),
			typeof(MustcheCircus),
			typeof(MustacheRedneck),
			typeof(NoFacialHair)
		};
		private static List<string> HairColorTraits = new List<string>()
		{
			CTrait.Appearance_HairColor_Black,
			CTrait.Appearance_HairColor_Blonde,
			CTrait.Appearance_HairColor_Blue,
			CTrait.Appearance_HairColor_Brown,
			CTrait.Appearance_HairColor_Green,
			CTrait.Appearance_HairColor_Grey,
			CTrait.Appearance_HairColor_Orange,
			CTrait.Appearance_HairColor_Pink,
			CTrait.Appearance_HairColor_Purple,
			CTrait.Appearance_HairColor_Red
		};
		private static List<string> HairstyleTraits = new List<string>()
		{
			CTrait.Appearance_Hairstyle_Afro,
			CTrait.Appearance_Hairstyle_Bald,
			CTrait.Appearance_Hairstyle_Balding,
			CTrait.Appearance_Hairstyle_BangsLong,
			CTrait.Appearance_Hairstyle_BangsMedium,
			CTrait.Appearance_Hairstyle_Curtains,
			CTrait.Appearance_Hairstyle_Cutoff,
			CTrait.Appearance_Hairstyle_FlatLong,
			CTrait.Appearance_Hairstyle_HoboBeard,
			CTrait.Appearance_Hairstyle_Leia,
			CTrait.Appearance_Hairstyle_MessyLong,
			CTrait.Appearance_Hairstyle_Military,
			CTrait.Appearance_Hairstyle_Mohawk,
			CTrait.Appearance_Hairstyle_Normal,
			CTrait.Appearance_Hairstyle_NormalHigh,
			CTrait.Appearance_Hairstyle_Pompadour,
			CTrait.Appearance_Hairstyle_Ponytail,
			CTrait.Appearance_Hairstyle_PuffyLong,
			CTrait.Appearance_Hairstyle_PuffyShort,
			CTrait.Appearance_Hairstyle_Sidewinder,
			CTrait.Appearance_Hairstyle_Spiky,
			CTrait.Appearance_Hairstyle_SpikyShort,
			CTrait.Appearance_Hairstyle_Suave,
			CTrait.Appearance_Hairstyle_Wave
		};
		private static List<string> SkinColorTraits = new List<string>()
		{
			CTrait.Appearance_SkinColor_BlackSkin,
			CTrait.Appearance_SkinColor_GoldSkin,
			CTrait.Appearance_SkinColor_LightBlackSkin,
			CTrait.Appearance_SkinColor_MixedSkin,
			CTrait.Appearance_SkinColor_PaleSkin,
			CTrait.Appearance_SkinColor_PinkSkin,
			CTrait.Appearance_SkinColor_SuperPaleSkin,
			CTrait.Appearance_SkinColor_WhiteSkin,
			CTrait.Appearance_SkinColor_ZombieSkin1,
			CTrait.Appearance_SkinColor_ZombieSkin2
		};
		#endregion
		public static void RollFacialHair(AgentHitbox agentHitBox)
		{
			Core.LogMethodCall();

			var random = new System.Random();

			List<CustomTrait> pool = agentHitBox.agent.GetTraits<CustomTrait>()
				.Where(trait => FacialHairTraits.Contains(trait.GetType()))
				.ToList();

			if (pool.Count == 0)
				return;

			CustomTrait selection = pool[random.Next(pool.Count)];
			string selectionName = selection.Trait.traitName;
			agentHitBox.facialHairType = selectionName.Substring(selectionName.IndexOf(indicator) + indicator.Length);
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
