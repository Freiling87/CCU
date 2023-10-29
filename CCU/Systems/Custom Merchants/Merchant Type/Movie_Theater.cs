﻿using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Movie_Theater : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.Banana, 3),
			new KeyValuePair<string, int>( VItemName.BaconCheeseburger, 6),
			new KeyValuePair<string, int>( VItemName.HamSandwich, 3),
			new KeyValuePair<string, int>( VItemName.Fud, 3),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Movie_Theater>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells obnoxiously overpriced food. \"Just doing my job,\" isn't an excuse!"),
					[LanguageCode.Spanish] = "Este NPC vende comida muy pero muy cara,",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Movie_Theater)),
					[LanguageCode.Spanish] = "Teatro de Peliculas",

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
