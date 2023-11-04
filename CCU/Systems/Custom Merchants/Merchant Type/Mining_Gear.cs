using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Mining_Gear : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.BombProcessor, 1),
			new KeyValuePair<string, int>( VItemName.Fud, 3),
			new KeyValuePair<string, int>( VItemName.GasMask, 1),
			new KeyValuePair<string, int>( VItemName.HardHat, 4),
			new KeyValuePair<string, int>( VItemName.RemoteBombTrigger, 1),
			new KeyValuePair<string, int>( VItemName.Sledgehammer, 4),
			new KeyValuePair<string, int>( VItemName.TimeBomb, 1),
			new KeyValuePair<string, int>( VItemName.WallBypasser, 2),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Mining_Gear>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells mining gear. Now accepting payments not in company scrip!"),
					[LanguageCode.Spanish] = "Este NPC vende herramientas para mineros.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Mining_Gear)),
					[LanguageCode.Spanish] = "Minero",

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
