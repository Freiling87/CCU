﻿using CCU.Traits.App_HC1;
using RogueLibsCore;

namespace CCU.Traits.App_HC2
{
	public class Normal_No_Grey : T_HairColor
	{
		public override string[] Rolls => StaticList;
		public static string[] StaticList => new string[] { "Brown", "Black", "Blonde", "Orange" };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Normal_No_Grey>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds multiple hair colors to the appearance pool.",
					[LanguageCode.Spanish] = "Agrega varios colores comunes de pelo a los que el personaje puede usar, exepto gris.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Normal_No_Grey), "Normal (No Grey)"),
					[LanguageCode.Spanish] = "Colores Normales (Sin Gris)",
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