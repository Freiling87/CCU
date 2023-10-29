using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Bartender_Fancy : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.Cocktail, 6),
			new KeyValuePair<string, int>( VItemName.Sugar, 3),
			new KeyValuePair<string, int>( VItemName.Whiskey, 6),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Bartender_Fancy>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells alcohol in an upscale establishment. Maybe a little Booger Sugar for the more discerning guests."),
					[LanguageCode.Spanish] = "Este NPC vende alcohol con la elegancia de francia, y drogas, para dar un poco de sabor a los inversores mas refinados.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Bartender_Fancy), "Bartender (Fancy)"),
					[LanguageCode.Spanish] = "Cantinero (De Clase)",

				})
				.WithUnlock(new TU_DesignerUnlock
				{
					Cancellations = { },
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
