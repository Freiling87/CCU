using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Upper_Cruster : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.Cologne, 4),
			new KeyValuePair<string, int>( VItemName.Cocktail, 4),
			new KeyValuePair<string, int>( VItemName.ResurrectionShampoo, 1),
			new KeyValuePair<string, int>( VItemName.BraceletofStrength, 1),
			new KeyValuePair<string, int>( VItemName.FriendPhone, 1),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Upper_Cruster>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells fancy shit by top brands made by slaves. This is the vanilla Upper Cruster's mall inventory."),
					[LanguageCode.Spanish] = "Este NPC vende basura cara echa por esclavos de fabricas extranjeras. en resumen vende lo que los ricos del shopping venden.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Upper_Cruster)),
					[LanguageCode.Spanish] = "Rico",

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
