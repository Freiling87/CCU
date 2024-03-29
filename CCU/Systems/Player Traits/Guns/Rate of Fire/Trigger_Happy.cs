﻿using RogueLibsCore;

namespace CCU.Traits.Player.Ranged_Combat
{
	public class Trigger_Happy : T_RateOfFire
	{
		public override float CooldownMultiplier => 0.8f;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_PlayerTrait = RogueLibs.CreateCustomTrait<Trigger_Happy>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] =
					Core.designerEdition
						? "Fire rate cooldown decreased by 20%."
						: "Fire rate cooldown decreased by 20 %.\n\n" +
							"<color=yellow>NPCs:</color> Firing interval decreased by 20%",
					[LanguageCode.Spanish] =
					Core.designerEdition
						? "Velocidad de fuego aumentada por 20%."
						: "Velocidad de fuego aumentada por 20 %.\n\n" +
							"<color=yellow>NPCs:</color> Intervalo entre disparos reducido por 20%",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Trigger_Happy)),
					[LanguageCode.Spanish] = "Gatillo Facil",

				})
				.WithUnlock(new TU_PlayerUnlock
				{
					CharacterCreationCost = 5,
					IsAvailable = true,
					IsAvailableInCC = Core.designerEdition,
					
					UnlockCost = 7,
					Upgrade = nameof(Trigger_Junkie),
					Unlock =
					{
						categories = { VTraitCategory.Guns },
					}
				}); ;
		}
		
		
	}
}