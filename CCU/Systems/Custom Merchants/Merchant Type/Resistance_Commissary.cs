﻿using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Resistance_Commissary : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.FreeItemVoucher, 6),
			new KeyValuePair<string, int>( VItemName.HiringVoucher, 6),
			new KeyValuePair<string, int>( VItemName.QuickEscapeTeleporter, 2),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Resistance_Commissary>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character distributes resources for the Resistance."),
					[LanguageCode.Spanish] = "Este NPC vende recursos por parte de la resistencia.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Resistance_Commissary)),
					[LanguageCode.Spanish] = "Comisaria de la Resistencia",

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
