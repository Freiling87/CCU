using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Drug_Dealer : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.Syringe, 6),
			new KeyValuePair<string, int>( VItemName.Cigarettes, 3),
			new KeyValuePair<string, int>( VItemName.MusclyPill, 3),
			new KeyValuePair<string, int>( VItemName.ElectroPill, 3),
			new KeyValuePair<string, int>( VItemName.CyanidePill, 3),
			new KeyValuePair<string, int>( VItemName.CritterUpper, 3),
			new KeyValuePair<string, int>( VItemName.Antidote, 3),
			new KeyValuePair<string, int>( VItemName.Giantizer, 3),
			new KeyValuePair<string, int>( VItemName.Shrinker, 3),
			new KeyValuePair<string, int>( VItemName.RagePoison, 3),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Drug_Dealer>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells drugs."),
					[LanguageCode.Spanish] = "Este NPC trafica drogas.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Drug_Dealer)),
					[LanguageCode.Spanish] = "Traficante de Drogas",

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
