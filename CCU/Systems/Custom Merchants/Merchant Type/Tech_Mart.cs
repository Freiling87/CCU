using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Tech_Mart : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.Blindenizer, 2),
			new KeyValuePair<string, int>( VItemName.EMPGrenade, 4),
			new KeyValuePair<string, int>( VItemName.Explodevice, 1),
			new KeyValuePair<string, int>( VItemName.GhostGibber, 2),
			new KeyValuePair<string, int>( VItemName.HackingTool, 4),
			new KeyValuePair<string, int>( VItemName.IdentifyWand, 2),
			new KeyValuePair<string, int>( VItemName.PortableSellOMatic, 1),
			new KeyValuePair<string, int>( VItemName.MemoryMutilator, 2),
			new KeyValuePair<string, int>( VItemName.SafeBuster, 2),
			new KeyValuePair<string, int>( VItemName.Translator, 3),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Tech_Mart>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells high-tech gear."),
					[LanguageCode.Spanish] = "Este NPC vende items de alta tecnologia.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Tech_Mart)),
					[LanguageCode.Spanish] = "Imart",

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
