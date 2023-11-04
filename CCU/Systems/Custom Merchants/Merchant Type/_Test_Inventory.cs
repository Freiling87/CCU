using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class _Test_Inventory : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.WaterPistol, 12),
		};

		[RLSetup]
		public static void Setup()
		{
			if (Core.developerEdition)
				PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<_Test_Inventory>()
					.WithDescription(new CustomNameInfo
					{
						[LanguageCode.English] = String.Format("Developer test inventory"),
						[LanguageCode.Spanish] = "Te la rasca",

					})
					.WithName(new CustomNameInfo
					{
						[LanguageCode.English] = DesignerName(typeof(_Test_Inventory)),
						[LanguageCode.Spanish] = "Pica pica",
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
