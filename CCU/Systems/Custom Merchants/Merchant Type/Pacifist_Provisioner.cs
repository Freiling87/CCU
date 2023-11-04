using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Pacifist_Provisioner : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.BananaPeel, 2),
			new KeyValuePair<string, int>( VItemName.HologramBigfoot, 1),
			new KeyValuePair<string, int>( VItemName.Hypnotizer, 2),
			new KeyValuePair<string, int>( VItemName.HypnotizerII, 1),
			new KeyValuePair<string, int>( VItemName.FirstAidKit, 2),
			new KeyValuePair<string, int>( VItemName.ParalyzerTrap, 2),
			new KeyValuePair<string, int>( VItemName.QuickEscapeTeleporter, 2),
			new KeyValuePair<string, int>( VItemName.TranquilizerGun, 1),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Pacifist_Provisioner>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells tools to avoid hurting people."),
					[LanguageCode.Spanish] = "Este NPC sirve herramientas para pacifistas.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Pacifist_Provisioner)),
					[LanguageCode.Spanish] = "Provisiones Pacificas",

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
