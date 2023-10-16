﻿using BunnyLibs;
using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Firefighter_Five_and_Dime : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.Axe, 3),
			new KeyValuePair<string, int>( VItemName.Crowbar, 2),
			new KeyValuePair<string, int>( VItemName.FireExtinguisher, 3),
			new KeyValuePair<string, int>( VItemName.FireproofSuit, 3),
			new KeyValuePair<string, int>( VItemName.FirstAidKit, 3),
			new KeyValuePair<string, int>( VItemName.GasMask, 3),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Firefighter_Five_and_Dime>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells whatever a Firefighter could need... legally."),
					[LanguageCode.Spanish] = "Este NPC vende todo lo que un bombero necesita.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Firefighter_Five_and_Dime), "Firefighter Five & Dime"),
					[LanguageCode.Spanish] = "Bombereo",

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
