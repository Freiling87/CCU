using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Bloodsuckers_Bazaar : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.BloodBag, 6),
			new KeyValuePair<string, int>( VItemName.Cologne, 1),
			new KeyValuePair<string, int>( VItemName.Hypnotizer, 2),
			new KeyValuePair<string, int>( VItemName.QuickEscapeTeleporter, 1),
			new KeyValuePair<string, int>( VItemName.MemoryMutilator, 1),
			new KeyValuePair<string, int>( VItemName.Sword, 3),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Bloodsuckers_Bazaar>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells anything a Vampire could need."),
					[LanguageCode.Spanish] = "Este NPC vende herramientas para chupasangres con patas.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Bloodsuckers_Bazaar), "Bloodsuckers' Bazaar"),
					[LanguageCode.Spanish] = "Bazar de Sanguijuelas",

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
