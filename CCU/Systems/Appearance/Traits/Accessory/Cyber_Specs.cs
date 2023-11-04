﻿using RogueLibsCore;

namespace CCU.Traits.App_AC1
{
	public class Cyber_Specs : T_Accessory
	{
		public override string[] Rolls => new string[] { "HackerGlasses" };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Cyber_Specs>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds this item to the appearance pool.",
					[LanguageCode.Spanish] = "Agrega este accesorio a los que el personaje puede usar.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Cyber_Specs)),
					[LanguageCode.Spanish] = "Cyber-Gafas",
				})
				.WithUnlock(new TU_DesignerUnlock
				{
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		
		
	}
}
