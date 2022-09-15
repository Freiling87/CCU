﻿using RogueLibsCore;

namespace CCU.Traits.Body_Color
{
	public class Skintone_Darker : T_BodyColor
	{
		public override string BodyColor => "LightBlackSkin";

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Skintone_Darker>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds this item to the appearance pool.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Skintone_Darker)),
				})
				.WithUnlock(new TraitUnlock
				{
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}
