using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Gun_Dealer_Heavy : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.Flamethrower, 3),
			new KeyValuePair<string, int>( VItemName.Grenade, 4),
			new KeyValuePair<string, int>( VItemName.KillAmmunizer, 1),
			new KeyValuePair<string, int>( VItemName.MachineGun, 4),
			new KeyValuePair<string, int>( VItemName.RocketLauncher, 3),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Gun_Dealer_Heavy>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells heavy guns."),
					[LanguageCode.Spanish] = "Este NPC vende las armas pesadas.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Gun_Dealer_Heavy), "Gun Dealer (Heavy)"),
					[LanguageCode.Spanish] = "Armamentos (Pesados)",

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
