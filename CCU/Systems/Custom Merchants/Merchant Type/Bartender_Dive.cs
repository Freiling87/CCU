using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Bartender_Dive : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.Beer, 6),
			new KeyValuePair<string, int>( VItemName.HotFud, 2),
			new KeyValuePair<string, int>( VItemName.MolotovCocktail, 1),
			new KeyValuePair<string, int>( VItemName.Whiskey, 6),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Bartender_Dive>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells alcohol in a cheap, seedy place."),
					[LanguageCode.Spanish] = "Este NPC vende alcohol del barato y menos confiable.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Bartender_Dive), "Bartender (Dive)"),
					[LanguageCode.Spanish] = "Cantinero (Clandestino)",

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
