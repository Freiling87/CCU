using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Convenience_Store : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.Banana, 1),
			new KeyValuePair<string, int>( VItemName.Beer, 5),
			new KeyValuePair<string, int>( VItemName.CigaretteLighter, 3),
			new KeyValuePair<string, int>( VItemName.Cigarettes, 5),
			new KeyValuePair<string, int>( VItemName.Fireworks, 1),
			new KeyValuePair<string, int>( VItemName.FriendPhone, 3),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Convenience_Store>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells stuff you might find in a corner store or bodega."),
					[LanguageCode.Spanish] = "Este NPC vende items simples, lo que encontras en una bodega o una esquina.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Convenience_Store)),
					[LanguageCode.Spanish] = "Dispensa",

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
