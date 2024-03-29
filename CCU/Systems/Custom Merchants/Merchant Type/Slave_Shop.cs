﻿using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Slave_Shop : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.Axe, 2),
			new KeyValuePair<string, int>( VItemName.CodPiece, 4),
			new KeyValuePair<string, int>( VItemName.DizzyGrenade, 2),
			new KeyValuePair<string, int>( VItemName.FreezeRay, 1),
			new KeyValuePair<string, int>( VItemName.Sword, 2),
			new KeyValuePair<string, int>( VItemName.SlaveHelmetRemover, 2),
			new KeyValuePair<string, int>( VItemName.Taser, 1),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Slave_Shop>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells gear for slavemasters."),
					[LanguageCode.Spanish] = "Este NPC sirve herramientas para los amos de casa.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Slave_Shop)),
					[LanguageCode.Spanish] = "Tienda para Esclavitud",

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
