using BunnyLibs;
using HarmonyLib;
using RogueLibsCore;
using System;

namespace CCU.Traits.Passive
{
	public class Immobile : T_CCU, IModMovement
	{
		public float Acceleration => 0f;
		public float MoveSpeedMax => 0f;
		public float MoveVolume => 1.00f;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Immobile>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This agent can't move. They can still be knocked back, so add Immovable if you don't want them to."),
					[LanguageCode.Spanish] = "Este personaje no puede moverse, aun asi puedes empujarlo.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Immobile)),
					[LanguageCode.Spanish] = "Inmobil",
				})
				.WithUnlock(new TU_DesignerUnlock
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

	[HarmonyPatch(typeof(Agent))]
	public class P_Agent
	{
		// TODO: IModMovement
		[HarmonyPrefix, HarmonyPatch(nameof(Agent.FindSpeed))]
		public static bool FindSpeed_Prefix(Agent __instance, ref int __result)
		{
			if (__instance.HasTrait<Immobile>())
			{
				__result = 0;
				return false;
			}

			return true;
		}
	}
}