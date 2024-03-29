﻿using RogueLibsCore;

namespace CCU.Traits.Player.Ammo
{
	public class Ammo_Artiste : T_AmmoCap
	{
		public override float AmmoCapMultiplier => 1.8f;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_PlayerTrait = RogueLibs.CreateCustomTrait<Ammo_Artiste>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Ammo capacity increased by 80%.",
					[LanguageCode.Spanish] = "Aumenta la capacidad de munición por 80%.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Ammo_Artiste)),
					[LanguageCode.Spanish] = "Artiste de la Munición",
				})
				.WithUnlock(new TU_PlayerUnlock
				{
					Cancellations = { nameof(Ammo_Amateur), nameof(Ammo_Auteur) },
					CharacterCreationCost = 5,
					IsAvailable = false,
					IsAvailableInCC = false,
					
					UnlockCost = 7,
					Upgrade = nameof(Ammo_Auteur),
					Unlock =
					{
						categories = { VTraitCategory.Guns },
						isUpgrade = true,
					}
				});
		}
	}
}