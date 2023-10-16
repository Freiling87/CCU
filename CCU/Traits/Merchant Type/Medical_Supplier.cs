﻿using BunnyLibs;
using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Medical_Supplier : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.Antidote, 3),
			new KeyValuePair<string, int>( VItemName.BloodBag, 3),
			new KeyValuePair<string, int>( VItemName.FirstAidKit, 3),
			new KeyValuePair<string, int>( VItemName.Knife, 3),
			new KeyValuePair<string, int>( VItemName.Syringe, 6),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Medical_Supplier>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells medicine and medical supplies."),
					[LanguageCode.Spanish] = "Este NPC vende medicina.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Medical_Supplier)),
					[LanguageCode.Spanish] = "Suplementos Medicos",

				})
				.WithUnlock(new TraitUnlock_CCU
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
