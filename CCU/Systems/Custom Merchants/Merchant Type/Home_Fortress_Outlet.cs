using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Home_Fortress_Outlet : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.BananaPeel, 3),
			new KeyValuePair<string, int>( VItemName.Beartrap, 4),
			new KeyValuePair<string, int>( VItemName.CigaretteLighter, 2),
			new KeyValuePair<string, int>( VItemName.LandMine, 4),
			new KeyValuePair<string, int>( VItemName.OilContainer, 2),
			new KeyValuePair<string, int>( VItemName.ParalyzerTrap, 4),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Home_Fortress_Outlet>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells traps."),
					[LanguageCode.Spanish] = "Este NPC vende trampas.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Home_Fortress_Outlet)),
					[LanguageCode.Spanish] = "Contrapista",

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
