﻿using CCU.Traits.App_HS1;
using RogueLibsCore;

namespace CCU.Traits.App_HS3
{
	public class Masks_50 : T_Hairstyle
	{
		public override string[] Rolls => new string[] { };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Masks_50>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "If any Masks (Hoodie, Slavemaster mask, etc.) are in the hairstyle pool, they have a 50% chance to be selected.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Masks_50), "Masks 50%"),
					[LanguageCode.Spanish] = "Máscaras 50%",
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