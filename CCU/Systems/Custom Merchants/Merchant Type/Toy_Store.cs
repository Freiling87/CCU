using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Toy_Store : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.BananaPeel, 4),
			new KeyValuePair<string, int>( VItemName.Blindenizer, 1),
			new KeyValuePair<string, int>( VItemName.EarWarpWhistle, 1),
			new KeyValuePair<string, int>( VItemName.HologramBigfoot, 1),
			new KeyValuePair<string, int>( VItemName.Shuriken, 2),
			new KeyValuePair<string, int>( VItemName.WalkieTalkie, 2),
			new KeyValuePair<string, int>( VItemName.WaterPistol, 3),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Toy_Store>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells toys. Not adult toys."),
					[LanguageCode.Spanish] = "Este NPC vende juguetes. tecnicamente para adultos pero no digamos eso.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Toy_Store)),
					[LanguageCode.Spanish] = "Jugueteria",

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
