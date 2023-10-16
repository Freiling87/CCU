using BunnyLibs;
using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Anthropophagie : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.Axe, 2),
			new KeyValuePair<string, int>( VItemName.Beartrap, 3),
			new KeyValuePair<string, int>( VItemName.Beer, 3),
			new KeyValuePair<string, int>( VItemName.Rock, 3),
			new KeyValuePair<string, int>( VItemName.Whiskey, 3),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Anthropophagie>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("A boutique for fine young cannibals."),
					[LanguageCode.Spanish] = "Este NPC vende items para ayudar a canibales en sus actividades dietarías.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Anthropophagie)),
					[LanguageCode.Spanish] = "Antropofagialogo",

				})
				.WithUnlock(new TraitUnlock_CCU
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
