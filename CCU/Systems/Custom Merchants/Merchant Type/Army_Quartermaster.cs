using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Army_Quartermaster : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.MachineGun, 3),
			new KeyValuePair<string, int>( VItemName.Revolver, 3),
			new KeyValuePair<string, int>( VItemName.Shotgun, 3),
			new KeyValuePair<string, int>( VItemName.RocketLauncher, 3),
			new KeyValuePair<string, int>( VItemName.Flamethrower, 3),
			new KeyValuePair<string, int>( VItemName.Pistol, 3),
			new KeyValuePair<string, int>( VItemName.AccuracyMod, 3),
			new KeyValuePair<string, int>( VItemName.AmmoCapacityMod, 3),
			new KeyValuePair<string, int>( VItemName.RateofFireMod, 3),
			new KeyValuePair<string, int>( VItemName.Silencer, 3),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Army_Quartermaster>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells military hardware."),
					[LanguageCode.Spanish] = "Este NPC vende tecnología militar de alto rendimiento.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Army_Quartermaster)),
					[LanguageCode.Spanish] = "Oficial Intendente",

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
