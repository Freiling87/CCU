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
		};
		private static List<string> HairstyleTraits = new List<string>()
		{
		};
		private static List<string> SkinColorTraits = new List<string>()
		{
		};

		internal static bool HasFacialHairTrait(Agent agent)
		{
			foreach (string trait in FacialHairTraits)
				if (agent.statusEffects.hasTrait(trait))
					return true;

			return false;
		}
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

				if (Appearance.HasFacialHairTrait(agent))
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
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
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
					},
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
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
					},
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}
