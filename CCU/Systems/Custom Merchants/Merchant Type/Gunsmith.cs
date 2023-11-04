using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Gunsmith : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.AmmoProcessor, 2),
			new KeyValuePair<string, int>( VItemName.AmmoStealer, 1),
			new KeyValuePair<string, int>( VItemName.KillAmmunizer, 1),
			new KeyValuePair<string, int>( VItemName.MachineGun, 2),
			new KeyValuePair<string, int>( VItemName.Pistol, 3),
			new KeyValuePair<string, int>( VItemName.Revolver, 3),
			new KeyValuePair<string, int>( VItemName.Shotgun, 3),
			new KeyValuePair<string, int>( VItemName.AccuracyMod, 1),
			new KeyValuePair<string, int>( VItemName.AmmoCapacityMod, 1),
			new KeyValuePair<string, int>( VItemName.Silencer, 1),
			new KeyValuePair<string, int>( VItemName.RateofFireMod, 1),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Gunsmith>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells guns and gun accessories."),
					[LanguageCode.Spanish] = "Este NPC vende armas y modificadores.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Gunsmith)),
					[LanguageCode.Spanish] = "Armamentos Avanzados",

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
