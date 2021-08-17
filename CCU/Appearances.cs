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
		private static List<string> HairColorTraits = new List<string>()
		{
			cTrait.Appearance_HairColor_Normal,
			cTrait.Appearance_HairColor_NormalNoGrey,
			cTrait.Appearance_HairColor_Wild
		};
		private static List<string> HairstyleTraits = new List<string>()
		{
			cTrait.Appearance_Hair_Balding,
			cTrait.Appearance_Hair_Bangs,
			cTrait.Appearance_Hair_CanHaveFacialHair,
			cTrait.Appearance_Hair_Female,
			cTrait.Appearance_Hair_Long,
			cTrait.Appearance_Hair_Male,
			cTrait.Appearance_Hair_NotHair,
			cTrait.Appearance_Hair_Punk,
			cTrait.Appearance_Hair_Short,
			cTrait.Appearance_Hair_ShortFemale,
			cTrait.Appearance_Hair_Stylish,
		};
		private static List<string> SkinColorTraits = new List<string>()
		{
			cTrait.Appearance_Skin_Any,
			cTrait.Appearance_Skin_Shapeshifter,
			cTrait.Appearance_Skin_Vampire,
			cTrait.Appearance_Skin_Zombie
		};
		public static bool HasHairColorTrait(Agent agent)
		{
			foreach (string trait in HairColorTraits)
				if (agent.statusEffects.hasTrait(trait))
					return true;

			return false;
		}
		public static bool HasHairstyleTrait(Agent agent)
		{
			foreach (string trait in HairstyleTraits)
				if (agent.statusEffects.hasTrait(trait))
					return true;

			return false;
		}
		public static bool HasSkinTrait(Agent agent)
		{
			foreach (string trait in SkinColorTraits)
				if (agent.statusEffects.hasTrait(trait))
					return true;

			return false;
		}
	}

    [HarmonyPatch(declaringType: typeof(AgentHitbox))]
    public static class AgentHitBox_Patches
    {
		public static GameController gc => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(AgentHitbox.SetupFeatures), argumentTypes: new Type[0] { })]
        private static void SetupFeatures_Prefix(AgentHitbox __instance)
        {
			Agent agent = __instance.agent;

			if (agent.agentName == "Custom")
			{
				if (gc.streamingWorld && agent.isPlayer == 0)
					UnityEngine.Random.InitState(agent.streamingChunkObjectID);

				__instance.MustRefresh();

				if (Appearance.HasSkinTrait(agent))
					__instance.chooseSkinColor(agent.agentName);

				if (Appearance.HasHairstyleTrait(agent))
					__instance.chooseHairType(agent.agentName);

				if (agent.statusEffects.hasTrait(cTrait.Appearance_FacialHair))
					__instance.chooseFacialHairType(agent.agentName);

				__instance.SetCantShowHairUnderHeadPiece();

				if (Appearance.HasHairColorTrait(agent))
					__instance.chooseHairColor(agent.agentName, false);

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

	public class Appearance_HairColor_Normal : CustomTrait
	{
		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<Appearance_HairColor_Normal>()
				.WithDescription(new CustomNameInfo("This NPC type will have varied hair colors when spawned as an NPC."))
				.WithName(new CustomNameInfo("Appearance: Hair Color - Normal"))
				.WithUnlock(new TraitUnlock
				{
					Cancellations = 
					{
						cTrait.Appearance_HairColor_NormalNoGrey,
						cTrait.Appearance_HairColor_Wild 
					},
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = true,
					UnlockCost = 0,
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
	public class Appearance_HairColor_NormalNoGrey : CustomTrait
	{
		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<Appearance_HairColor_NormalNoGrey>()
				.WithDescription(new CustomNameInfo("This NPC type will have varied hair colors when spawned as an NPC."))
				.WithName(new CustomNameInfo("Appearance: Hair Color - Normal, No Grey"))
				.WithUnlock(new TraitUnlock
				{
					Cancellations =
					{
						cTrait.Appearance_HairColor_Normal,
						cTrait.Appearance_HairColor_Wild
					},
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = true,
					UnlockCost = 0,
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
	public class Appearance_HairColor_Wild : CustomTrait
	{
		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<Appearance_HairColor_Wild>()
				.WithDescription(new CustomNameInfo("This NPC type will have varied hair colors when spawned as an NPC."))
				.WithName(new CustomNameInfo("Appearance: Hair Color - Wild"))
				.WithUnlock(new TraitUnlock
				{
					Cancellations =
					{
						cTrait.Appearance_HairColor_Normal,
						cTrait.Appearance_HairColor_NormalNoGrey
					},
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
