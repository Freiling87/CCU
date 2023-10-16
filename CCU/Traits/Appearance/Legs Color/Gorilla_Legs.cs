﻿using RogueLibsCore;

namespace CCU.Traits.App_LC1
{
	public class Gorilla_Legs : T_LegsColor
	{
		public override string[] Rolls => new string[] { "GorillaSkin" };

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Gorilla_Legs>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds this item to the appearance pool. Gorilla is a color now.",
					[LanguageCode.Spanish] = "Agrega este color de piernas a los que el personaje puede usar. Olor no incluido.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Gorilla_Legs)),
					[LanguageCode.Spanish] = "Piernas de Gorila",
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
