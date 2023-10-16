﻿using RogueLibsCore;

namespace CCU.Traits.App_AC1
{
	public class No_Accessory : T_Accessory
	{
		public override string[] Rolls => new string[] { "" };

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<No_Accessory>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds this item to the appearance pool.",
					[LanguageCode.Spanish] = "Agrega una chance que el personaje generado no lleve accesorio",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(No_Accessory)),
					[LanguageCode.Spanish] = "Sin Accesorio",
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
