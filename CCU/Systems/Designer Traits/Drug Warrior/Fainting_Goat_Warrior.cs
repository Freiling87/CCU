﻿using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
	public class Fainting_Goat_Warrior : T_DrugWarrior
	{
		public override string DrugEffect => VStatusEffect.Tranquilized;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Fainting_Goat_Warrior>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character will become Tranquilized upon entering combat. Shoulda done cardio."),
					[LanguageCode.Spanish] = "Este NPC se tranquiliza al entrar en combate. a mimir.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Fainting_Goat_Warrior)),
					[LanguageCode.Spanish] = "Bestia de la Siesta",

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
