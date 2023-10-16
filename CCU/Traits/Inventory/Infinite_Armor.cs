﻿using RogueLibsCore;
using System;

namespace CCU.Traits.Inventory
{
	public class Infinite_Armor : T_CCU
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Infinite_Armor>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Guess, smartypants."),
					[LanguageCode.Spanish] = "La verdad ni idea que esto hace, es muy dificil saberlo...",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Infinite_Armor)),
					[LanguageCode.Spanish] = "Durabilidad de Armadura Infinita",

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