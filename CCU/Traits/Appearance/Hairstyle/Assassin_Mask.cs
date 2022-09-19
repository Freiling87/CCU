﻿using RogueLibsCore;

namespace CCU.Traits.App_HS1
{
    public class Assassin_Mask : T_Hairstyle
	{
		public override string[] Rolls => new string[] { "AssassinMask" };

        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Assassin_Mask>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds this item to the appearance pool.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Assassin_Mask)),
				})
				.WithUnlock(new TraitUnlock_CCU
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