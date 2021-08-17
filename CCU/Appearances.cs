using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CCU
{
	//AgentHitBox.SetupFeatures line 1782
	// If Agent is Custom AND has appearance trait AND is not player, then roll new appearance

	public static class Appearance
	{
		#region lists
		private static List<string> FacialHairTraits = new List<string>()
		{
			cTrait.Appearance_FacialHair_Beard,
			cTrait.Appearance_FacialHair_Mustache,
			cTrait.Appearance_FacialHair_MustacheCircus,
			cTrait.Appearance_FacialHair_MustacheRedneck,
			cTrait.Appearance_FacialHair_None
		};
		private static List<string> HairColorTraits = new List<string>()
		{
			cTrait.Appearance_HairColor_Black,
			cTrait.Appearance_HairColor_Blonde,
			cTrait.Appearance_HairColor_Blue,
			cTrait.Appearance_HairColor_Brown,
			cTrait.Appearance_HairColor_Green,
			cTrait.Appearance_HairColor_Grey,
			cTrait.Appearance_HairColor_Orange,
			cTrait.Appearance_HairColor_Pink,
			cTrait.Appearance_HairColor_Purple,
			cTrait.Appearance_HairColor_Red
		};
		private static List<string> HairstyleTraits = new List<string>()
		{
			cTrait.Appearance_Hairstyle_Afro,
			cTrait.Appearance_Hairstyle_Bald,
			cTrait.Appearance_Hairstyle_Balding,
			cTrait.Appearance_Hairstyle_BangsLong,
			cTrait.Appearance_Hairstyle_BangsMedium,
			cTrait.Appearance_Hairstyle_Curtains,
			cTrait.Appearance_Hairstyle_Cutoff,
			cTrait.Appearance_Hairstyle_FlatLong,
			cTrait.Appearance_Hairstyle_HoboBeard,
			cTrait.Appearance_Hairstyle_Leia,
			cTrait.Appearance_Hairstyle_MessyLong,
			cTrait.Appearance_Hairstyle_Military,
			cTrait.Appearance_Hairstyle_Mohawk,
			cTrait.Appearance_Hairstyle_Normal,
			cTrait.Appearance_Hairstyle_NormalHigh,
			cTrait.Appearance_Hairstyle_Pompadour,
			cTrait.Appearance_Hairstyle_Ponytail,
			cTrait.Appearance_Hairstyle_PuffyLong,
			cTrait.Appearance_Hairstyle_PuffyShort,
			cTrait.Appearance_Hairstyle_Sidewinder,
			cTrait.Appearance_Hairstyle_Spiky,
			cTrait.Appearance_Hairstyle_SpikyShort,
			cTrait.Appearance_Hairstyle_Suave,
			cTrait.Appearance_Hairstyle_Wave
		};
		private static List<string> SkinColorTraits = new List<string>()
		{
			cTrait.Appearance_SkinColor_BlackSkin,
			cTrait.Appearance_SkinColor_GoldSkin,
			cTrait.Appearance_SkinColor_LightBlackSkin,
			cTrait.Appearance_SkinColor_MixedSkin,
			cTrait.Appearance_SkinColor_PaleSkin,
			cTrait.Appearance_SkinColor_PinkSkin,
			cTrait.Appearance_SkinColor_SuperPaleSkin,
			cTrait.Appearance_SkinColor_WhiteSkin,
			cTrait.Appearance_SkinColor_ZombieSkin1,
			cTrait.Appearance_SkinColor_ZombieSkin2
		};
		#endregion
		#region Rollers
		internal static void RollFacialHair(AgentHitbox agentHitBox, Agent agent)
		{
			List<string> pool = new List<string>();
			var random = new System.Random();

			foreach (string trait in FacialHairTraits)
				if (agent.statusEffects.hasTrait(trait))
					pool.Add(trait);

			if (pool.Count == 0)
				return;

			string selection = pool[random.Next(pool.Count)];
			agentHitBox.facialHairType = selection.Substring(selection.LastIndexOf("_"));
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
		internal static void RollHairColor(AgentHitbox agentHitBox, Agent agent)
		{
			List<string> pool = new List<string>();
			var random = new System.Random();

			foreach (string trait in FacialHairTraits)
				if (agent.statusEffects.hasTrait(trait))
					pool.Add(trait);

			if (pool.Count == 0)
				return;

			//agentHitBox.hairColor = pool[random.Next(pool.Count)];
		}
		internal static void RollHairstyle(AgentHitbox agentHitBox, Agent agent)
		{
			List<string> pool = new List<string>();
			var random = new System.Random();

			foreach (string trait in FacialHairTraits)
				if (agent.statusEffects.hasTrait(trait))
					pool.Add(trait);

			if (pool.Count == 0)
				return;

			agentHitBox.hairType = pool[random.Next(pool.Count)];
		}
		internal static void RollSkinColor(AgentHitbox agentHitBox, Agent agent)
		{
			List<string> pool = new List<string>();
			var random = new System.Random();

			foreach (string trait in FacialHairTraits)
				if (agent.statusEffects.hasTrait(trait))
					pool.Add(trait);

			if (pool.Count == 0)
				return;

			string selection = pool[random.Next(pool.Count)];
			agentHitBox.GetColorFromString(selection, "Skin");
			agentHitBox.skinColorName = selection;
		}
		#endregion
	}

	[HarmonyPatch(declaringType: typeof(AgentHitbox))]
    public static class AgentHitBox_Patches
    {
		public static GameController gc => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(AgentHitbox.SetupFeatures), argumentTypes: new Type[0] { })]
        private static void SetupFeatures_Prefix(AgentHitbox __instance)
        {
			Agent agent = __instance.agent;

			if (agent.agentName == "Custom")
			{
				if (gc.streamingWorld && agent.isPlayer == 0)
					UnityEngine.Random.InitState(agent.streamingChunkObjectID);

				__instance.MustRefresh();

				Appearance.RollFacialHair(__instance, agent);
				//Appearance.RollHairstyle(__instance, agent);
				//Appearance.RollSkinColor(__instance, agent);

				__instance.SetCantShowHairUnderHeadPiece();

				//Appearance.RollHairColor(__instance, agent);

				if (agent.isPlayer > 0 && !__instance.hasSetup && agent.localPlayer && !gc.fourPlayerMode)
				{
					gc.sessionDataBig.hairType[agent.isPlayer] = __instance.hairType;
					gc.sessionDataBig.hairColor[agent.isPlayer] = __instance.hairColorName;
					gc.sessionDataBig.facialHairColor[agent.isPlayer] = __instance.facialHairColorName;
					gc.sessionDataBig.facialHairType[agent.isPlayer] = __instance.facialHairType;
					gc.sessionDataBig.skinColor[agent.isPlayer] = __instance.skinColorName;
					gc.sessionDataBig.hairColor32[agent.isPlayer] = __instance.hairColor;
					gc.sessionDataBig.facialHairColor32[agent.isPlayer] = __instance.facialHairColor;
					gc.sessionDataBig.skinColor32[agent.isPlayer] = __instance.skinColor;
				}
				try
				{
					__instance.objectSprite.SetRenderer("Off");
				}
				catch { }

				__instance.SetUsesNewHead();
			}
		}
    }

	public class Appearance_FacialHair_Beard : CustomTrait
	{
		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<Appearance_FacialHair_Beard>()
				.WithDescription(new CustomNameInfo("When spawned as an NPC, this class will have a random appearance generated from all selected appearance traits."))
				.WithName(new CustomNameInfo("Appearance: Facial Hair - Beard"))
				.WithUnlock(new TraitUnlock
				{
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = true,
					UnlockCost = 0,
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
	public class Appearance_FacialHair_Mustache : CustomTrait
	{
		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<Appearance_FacialHair_Mustache>()
				.WithDescription(new CustomNameInfo("When spawned as an NPC, this class will have a random appearance generated from all selected appearance traits."))
				.WithName(new CustomNameInfo("Appearance: Facial Hair - Mustache"))
				.WithUnlock(new TraitUnlock
				{
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = true,
					UnlockCost = 0,
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
	public class Appearance_FacialHair_MustacheCircus : CustomTrait
	{
		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<Appearance_FacialHair_MustacheCircus>()
				.WithDescription(new CustomNameInfo("When spawned as an NPC, this class will have a random appearance generated from all selected appearance traits."))
				.WithName(new CustomNameInfo("Appearance: Facial Hair - Circus Mustache"))
				.WithUnlock(new TraitUnlock
				{
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = true,
					UnlockCost = 0,
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
	public class Appearance_FacialHair_MustacheRedneck : CustomTrait
	{
		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<Appearance_FacialHair_MustacheRedneck>()
				.WithDescription(new CustomNameInfo("When spawned as an NPC, this class will have a random appearance generated from all selected appearance traits."))
				.WithName(new CustomNameInfo("Appearance: Facial Hair - Redneck Mustache"))
				.WithUnlock(new TraitUnlock
				{
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = true,
					UnlockCost = 0,
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
	public class Appearance_FacialHair_None : CustomTrait
	{
		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<Appearance_FacialHair_None>()
				.WithDescription(new CustomNameInfo("When spawned as an NPC, this class will have a random appearance generated from all selected appearance traits."))
				.WithName(new CustomNameInfo("Appearance: Facial Hair - None"))
				.WithUnlock(new TraitUnlock
				{
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = true,
					UnlockCost = 0,
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}
