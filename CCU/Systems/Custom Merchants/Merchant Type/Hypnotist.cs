using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Hypnotist : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.Cologne, 2),
			new KeyValuePair<string, int>( VItemName.Haterator, 3),
			new KeyValuePair<string, int>( VItemName.Hypnotizer, 6),
			new KeyValuePair<string, int>( VItemName.HypnotizerII, 3),
			new KeyValuePair<string, int>( VItemName.MemoryMutilator, 3),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Hypnotist>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells hypnotist's tools."),
					[LanguageCode.Spanish] = "Este NPC vende herramientas de hynotismo.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Hypnotist)),
					[LanguageCode.Spanish] = "Hypnotista",

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
