﻿using RogueLibsCore;

namespace CCU.Traits.Appearance.FacialHair
{
    public class MustacheRedneck : CustomTrait
	{
		//[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<MustacheRedneck>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds this trait to the appearance pool. If this character is generated as an NPC, their appearance will be randomized between all appearance traits in the pool.",
					[LanguageCode.Russian] = "",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = CTrait.FacialHair_MustacheRedneck,
					[LanguageCode.Russian] = "",
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
