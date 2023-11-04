using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Junk_Dealer : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.BananaPeel, 8),
			new KeyValuePair<string, int>( VItemName.BaseballBat, 3),
			new KeyValuePair<string, int>( VItemName.CardboardBox, 3),
			new KeyValuePair<string, int>( VItemName.CodPiece, 3),
			new KeyValuePair<string, int>( VItemName.FourLeafClover, 1),
			new KeyValuePair<string, int>( VItemName.FreeItemVoucher, 1),
			new KeyValuePair<string, int>( VItemName.HiringVoucher, 1),
			new KeyValuePair<string, int>( VItemName.Rock, 8),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Junk_Dealer>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells junk."),
					[LanguageCode.Spanish] = "Este NPC vende basura.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Junk_Dealer)),
					[LanguageCode.Spanish] = "Cartonista",

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
