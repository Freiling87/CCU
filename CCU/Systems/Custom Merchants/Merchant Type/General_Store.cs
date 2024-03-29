﻿using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class General_Store : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( "Food", 10),
			new KeyValuePair<string, int>( "Medical2", 5),
			new KeyValuePair<string, int>( "Gun1", 3),
			new KeyValuePair<string, int>( "Gun2", 2),
			new KeyValuePair<string, int>( "Melee1", 3),
			new KeyValuePair<string, int>( "Melee2", 2),
			new KeyValuePair<string, int>( "Armor1", 2),
			new KeyValuePair<string, int>( "Armor2", 2),
			new KeyValuePair<string, int>( "Armor3", 1),
			new KeyValuePair<string, int>( "Everyday1", 2),
			new KeyValuePair<string, int>( "Tech2", 1),
			new KeyValuePair<string, int>( "Tool1", 1),
			new KeyValuePair<string, int>( "Tool2", 1),
			new KeyValuePair<string, int>( "Tool3", 1),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<General_Store>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells miscellaneous stuff. A real retail slut. This is the vanilla Shopkeeper inventory."),
					[LanguageCode.Spanish] = "Este NPC maneja un almacen, que talves es una pantalla!... para ocultar otro almacen.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(General_Store)),
					[LanguageCode.Spanish] = "Almacen",

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
