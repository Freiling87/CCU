﻿using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
	public class Stimpacker : T_DrugWarrior
	{
		public override string DrugEffect => VStatusEffect.RegenerateHealth;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Stimpacker>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character will start regenerating health upon entering combat."),
					[LanguageCode.Spanish] = "Este NPC lentamente regenera salud al entrar en combate.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Stimpacker)),
					[LanguageCode.Spanish] = "Sobremedicado",

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
