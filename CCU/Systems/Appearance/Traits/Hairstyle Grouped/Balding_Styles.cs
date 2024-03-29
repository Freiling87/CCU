﻿using CCU.Traits.App_HS1;
using RogueLibsCore;

namespace CCU.Traits.App_HS2
{
	public class Balding_Styles : T_Hairstyle
	{
		public override string[] Rolls => new string[] { "Bald", "Balding", "Sidewinder" };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Balding_Styles>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds multiple hairstyles to the appearance pool.",
					[LanguageCode.Spanish] = "Agrega varios peinado a los que el personaje puede usar.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Balding_Styles)),
					[LanguageCode.Spanish] = "Peinados con Calvicie",
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