﻿using RogueLibsCore;

namespace CCU.Traits.Eye_Type
{
	public class Narrow_Eyes : T_EyeType
	{
		public override string EyeType => "NarrowEyes";

        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Narrow_Eyes>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds this item to the appearance pool.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Narrow_Eyes)),
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
