﻿using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Banana_Boutique : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.Banana, 11),
			new KeyValuePair<string, int>( VItemName.BananaPeel, 1),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Banana_Boutique>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Yes, that is a banana in their pocket. But yes, they're still happy to see you."),
					[LanguageCode.Spanish] = "Este NPC vende bananas, eso es todo.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Banana_Boutique)),
					[LanguageCode.Spanish] = "Banadero",

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
