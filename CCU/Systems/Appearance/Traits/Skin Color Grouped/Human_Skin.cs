﻿using CCU.Traits.App_SC1;
using RogueLibsCore;

namespace CCU.Traits.App_SC2
{
	public class Human_Skin : T_SkinColor
	{
		// The double-presence of the WhiteSkin string is to mirror the vanilla code.
		// I'm not a huge fan of it, but complaints go to Matt, not to me.
		public override string[] Rolls =>
			new string[] { "BlackSkin", "GoldSkin", "LightBlackSkin", "MixedSkin", "PaleSkin", "PinkSkin", "WhiteSkin", "WhiteSkin" };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Human_Skin>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds multiple items to the appearance pool.",
					[LanguageCode.Spanish] = "Agrega varios colores de piel a los que el personaje puede usar.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Human_Skin)),
					[LanguageCode.Spanish] = "Piel Humana",
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
