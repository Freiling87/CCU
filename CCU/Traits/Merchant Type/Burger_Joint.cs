using BunnyLibs;
using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Burger_Joint : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.BaconCheeseburger, 8),
			new KeyValuePair<string, int>( VItemName.Beer, 5),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Burger_Joint>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Burgers & beer, get 'em here!"),
					[LanguageCode.Spanish] = "'burgesas y cervesa, este NPC vende los sueños de todo buen americano.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Burger_Joint)),
					[LanguageCode.Spanish] = "Hamburgeseria",
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					Cancellations = { },
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					Recommendations = new List<string> { "Clearancer" },
					UnlockCost = 0,
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}