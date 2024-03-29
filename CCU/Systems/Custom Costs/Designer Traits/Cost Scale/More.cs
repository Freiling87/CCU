﻿using RogueLibsCore;

namespace CCU.Traits.Cost_Scale
{
	public class More : T_CostScale
	{
		public override float CostScale => 1.50f;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<More>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "This character's costs are increased by 50%.",
					[LanguageCode.Spanish] = "Los Precios de este NPC son Aumentados por 50%.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(More)),
					[LanguageCode.Spanish] = "Caro",

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
