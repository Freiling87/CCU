using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Gun_Dealer : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.AmmoProcessor, 2),
			new KeyValuePair<string, int>( VItemName.KillAmmunizer, 1),
			new KeyValuePair<string, int>( VItemName.MachineGun, 3),
			new KeyValuePair<string, int>( VItemName.Pistol, 3),
			new KeyValuePair<string, int>( VItemName.Revolver, 3),
			new KeyValuePair<string, int>( VItemName.Shotgun, 3),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Gun_Dealer>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells guns."),
					[LanguageCode.Spanish] = "Este NPC vende armas comunes.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Gun_Dealer)),
					[LanguageCode.Spanish] = "Armamentos",

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
