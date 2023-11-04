using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Consumer_Electronics : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.BoomBox, 3),
			new KeyValuePair<string, int>( VItemName.FoodProcessor, 2),
			new KeyValuePair<string, int>( VItemName.FriendPhone, 3),
			new KeyValuePair<string, int>( VItemName.MiniFridge, 3),
			new KeyValuePair<string, int>( VItemName.Translator, 2),
			new KeyValuePair<string, int>( VItemName.WalkieTalkie, 2),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Consumer_Electronics>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells consumer electronics and appliances."),
					[LanguageCode.Spanish] = "Este NPC vende electronicos para el consumidor.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Consumer_Electronics)),
					[LanguageCode.Spanish] = "Electronicos de Consumo",

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
