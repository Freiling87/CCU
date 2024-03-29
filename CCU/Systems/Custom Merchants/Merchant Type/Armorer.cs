﻿using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Armorer : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.ArmorDurabilitySpray, 2),
			new KeyValuePair<string, int>( VItemName.BulletproofVest, 3),
			new KeyValuePair<string, int>( VItemName.CodPiece, 2),
			new KeyValuePair<string, int>( VItemName.FireproofSuit, 1),
			new KeyValuePair<string, int>( VItemName.GasMask, 1),
			new KeyValuePair<string, int>( VItemName.HardHat, 1),
			new KeyValuePair<string, int>( VItemName.SoldierHelmet, 3),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Armorer>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells Armor & Armor accessories."),
					[LanguageCode.Spanish] = "Este NPC vende prendas perfectas para la calle, kevlar y nike.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Armorer)),
					[LanguageCode.Spanish] = "Armadura",

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
