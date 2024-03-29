﻿using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
	public class Harshmellow : T_DrugWarrior
	{
		public override string DrugEffect => VStatusEffect.Withdrawal;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Harshmellow>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character gains Withdrawal when they enter combat. Bummer."),
					[LanguageCode.Spanish] = "A este NPC le quitas a vibra a golpes, como cualquier hippie se lo merece no te preoqupes.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Harshmellow)),
					[LanguageCode.Spanish] = "Quemado",

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
