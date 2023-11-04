using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Barbarian_Merchant : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.Axe, 3),
			new KeyValuePair<string, int>( VItemName.BaconCheeseburger, 2),
			new KeyValuePair<string, int>( VItemName.Beer, 2),
			new KeyValuePair<string, int>( VItemName.BraceletofStrength, 1),
			new KeyValuePair<string, int>( VItemName.CodPiece, 3),
			new KeyValuePair<string, int>( VItemName.RagePoison, 1),
			new KeyValuePair<string, int>( VItemName.Sword, 3),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Barbarian_Merchant>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Ale, meat & a sharp blade. All that is best in life!"),
					[LanguageCode.Spanish] = "Ale, carne y cuchilla, Este NPC vende todas las herramientas de un bravo barbaro, barba y musculatura no incluida.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Barbarian_Merchant)),
					[LanguageCode.Spanish] = "Bar-Barbaro",

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
