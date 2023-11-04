using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Sporting_Goods : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.BaseballBat, 3),
			new KeyValuePair<string, int>( VItemName.CodPiece, 2),
			new KeyValuePair<string, int>( VItemName.FirstAidKit, 2),
			new KeyValuePair<string, int>( VItemName.KillerThrower, 2),
			new KeyValuePair<string, int>( VItemName.MusclyPill, 2),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Sporting_Goods>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells sporting goods."),
					[LanguageCode.Spanish] = "Este NPC vende cosas que sirven para los deportes, sobretodo el deporte de romper cabezas.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Sporting_Goods)),
					[LanguageCode.Spanish] = "Tienda Deportiva",

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
