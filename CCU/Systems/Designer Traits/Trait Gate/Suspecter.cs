﻿using RogueLibsCore;

namespace CCU.Traits.Trait_Gate
{
	public class Suspecter : T_TraitGate
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Suspecter>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "This character is Annoyed at characters with the Suspicious trait.",
					[LanguageCode.Spanish] = "Este NPC se vuelve Irritado con personajes que tengan el rasgo Sospechoso",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Suspecter)),
					[LanguageCode.Spanish] = "Sospechante",

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
}
