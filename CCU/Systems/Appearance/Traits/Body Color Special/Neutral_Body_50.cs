﻿using CCU.Traits.App_BC1;
using RogueLibsCore;

namespace CCU.Traits.App_BC3
{
	public class Neutral_Body_50 : T_BodyColor
	{
		public override string[] Rolls => new string[] { };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Neutral_Body_50>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Rolls \"Neutral Body\" 50% of the time, regardless of the number of other items in the pool.",
					[LanguageCode.Spanish] = "Da una chance del 50% de tener \"Color Neutral\" No importe la cantidad de rasgos de aparencia.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Neutral_Body_50), "Neutral Body 50%"),
					[LanguageCode.Spanish] = "Cuerpo Neutral 50%",
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
