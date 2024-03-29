﻿using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
	public class Immortalish : T_DrugWarrior
	{
		public override string DrugEffect => VStatusEffect.Resurrection;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Immortalish>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character will gain a Resurrection status upon entering combat."),
					[LanguageCode.Spanish] = "Este NPC resucitara al entrar en combate.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Immortalish)),
					[LanguageCode.Spanish] = "Semi-Mortal",

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
