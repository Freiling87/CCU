using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Fire_Sale : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.CigaretteLighter, 2),
			new KeyValuePair<string, int>( VItemName.Flamethrower, 2),
			new KeyValuePair<string, int>( VItemName.FireproofSuit, 2),
			new KeyValuePair<string, int>( VItemName.MolotovCocktail, 4),
			new KeyValuePair<string, int>( VItemName.OilContainer, 2),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Fire_Sale>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Sale! Big sale! Everything must go (up in flames)!"),
					[LanguageCode.Spanish] = "Este NPC vende cosas CALIENTES.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Fire_Sale)),
					[LanguageCode.Spanish] = "Venta Hot Sale",

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
		
		
	}
}
