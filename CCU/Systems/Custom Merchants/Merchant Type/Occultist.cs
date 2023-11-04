using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Occultist : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.BloodBag, 4),
			new KeyValuePair<string, int>( VItemName.BooUrn, 1),
			new KeyValuePair<string, int>( VItemName.Cologne, 1),
			new KeyValuePair<string, int>( VItemName.CubeOfLampey, 1),
			new KeyValuePair<string, int>( VItemName.GhostGibber, 1),
			new KeyValuePair<string, int>( VItemName.Knife, 1),
			new KeyValuePair<string, int>( VItemName.Necronomicon, 1),
			new KeyValuePair<string, int>( VItemName.ResurrectionShampoo, 1),
			new KeyValuePair<string, int>( VItemName.Sword, 1),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Occultist>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells occult-related items."),
					[LanguageCode.Spanish] = "Este NPC vence items sobrenaturales.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Occultist)),
					[LanguageCode.Spanish] = "Ocultista",

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
