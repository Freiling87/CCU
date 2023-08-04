﻿using RogueLibsCore;

namespace CCU.Traits.App_HC1
{
	public class Red_Hair : T_HairColor
	{
		public override string[] Rolls => new string[] { "Red" };

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Red_Hair>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds this item to the appearance pool.",
                    [LanguageCode.Spanish] = "Agrega este color de pelo a los que el personaje puede usar.",
                })
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Red_Hair)),
                    [LanguageCode.Spanish] = "Pelo Rojo",
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