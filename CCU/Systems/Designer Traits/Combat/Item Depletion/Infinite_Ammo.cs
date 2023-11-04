using HarmonyLib;
using RogueLibsCore;
using System;

namespace CCU.Traits.Inventory
{
	public class Infinite_Ammo : T_DesignerTrait
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Infinite_Ammo>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Guess, smartypants."),
					[LanguageCode.Spanish] = "Adivina",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Infinite_Ammo)),
					[LanguageCode.Spanish] = "Municion Infinita",

				})
				.WithUnlock(new TU_DesignerUnlock
				{
					Cancellations = { },
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		
		
	}

	[HarmonyPatch(typeof(Gun))]
	public static class P_Gun
	{
		[HarmonyPrefix, HarmonyPatch(nameof(Gun.SubtractBullets))]
		public static bool SubtractBullets_Prefix(Gun __instance)
		{
			if (__instance.agent.HasTrait<Infinite_Ammo>())
				return false;

			return true;
		}
	}
}