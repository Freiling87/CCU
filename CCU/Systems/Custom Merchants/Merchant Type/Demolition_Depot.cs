using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Demolition_Depot : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.BombProcessor, 1),
			new KeyValuePair<string, int>( VItemName.DoorDetonator, 3),
			new KeyValuePair<string, int>( VItemName.Fireworks, 1),
			new KeyValuePair<string, int>( VItemName.Grenade, 3),
			new KeyValuePair<string, int>( VItemName.LandMine, 3),
			new KeyValuePair<string, int>( VItemName.RemoteBomb, 3),
			// new KeyValuePair<string, int>( VItemName.RemoteBombTrigger, 1), // Can't sell
			new KeyValuePair<string, int>( VItemName.RocketLauncher, 1),
			new KeyValuePair<string, int>( VItemName.TimeBomb, 3),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Demolition_Depot>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells explosives."),
					[LanguageCode.Spanish] = "Este NPC vende explosivos.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Demolition_Depot)),
					[LanguageCode.Spanish] = "Depot de Demolicion",

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
