﻿using CCU.Traits.Hair_Color;
using CCU.Traits.Hairstyle;
using RogueLibsCore;

namespace CCU.Traits.Hair_Color_Grouped
{
    public class Normal_No_Grey : T_HairColor
	{
		public override string[] HairColors => new string[] { "Brown", "Black", "Blonde", "Orange" };

        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Normal_No_Grey>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds multiple hairstyles to the appearance pool.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Normal_No_Grey), "Normal (No Grey)"),
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