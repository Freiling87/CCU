using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Villains_Vault : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.CyanidePill, 2),
			new KeyValuePair<string, int>( VItemName.Explodevice, 2),
			new KeyValuePair<string, int>( VItemName.Giantizer, 2),
			new KeyValuePair<string, int>( VItemName.MonkeyBarrel, 2),
			new KeyValuePair<string, int>( VItemName.Necronomicon, 2),
			new KeyValuePair<string, int>( VItemName.RagePoison, 2),
			new KeyValuePair<string, int>( VItemName.TimeBomb, 2),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Villains_Vault>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells things that are dastardly to use. If they have a mustache, they will twirl it deviously."),
					[LanguageCode.Spanish] = "Este NPC vende items altamente destructivos, perfectos para un maligno pierre nodoyuna.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Villains_Vault), "Villains' Vault"),
					[LanguageCode.Spanish] = "La Vobeda Villanesca",

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
