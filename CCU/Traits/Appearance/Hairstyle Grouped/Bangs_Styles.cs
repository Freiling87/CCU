﻿using CCU.Traits.Hairstyle;
using RogueLibsCore;

namespace CCU.Traits.Hairstyle_Grouped
{
    public class Bangs_Styles : T_Hairstyle
	{
		public override string[] HairstyleType => new string[] { "BangsMedium", "BangsLong", "Sidewinder" };

        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Bangs_Styles>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds multiple hairstyles to the appearance pool.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Bangs_Styles)),
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