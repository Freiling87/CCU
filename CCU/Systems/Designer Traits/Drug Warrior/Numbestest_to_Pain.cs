﻿using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
	public class Numbestest_to_Pain : T_DrugWarrior
	{
		public override string DrugEffect => VStatusEffect.NumbToPain;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Numbestest_to_Pain>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character gains a 66% damage resistance upon entering combat."),
					[LanguageCode.Spanish] = "Este NPC tiene una resistencia de 66% contra todo daño al entrar en combate.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Numbestest_to_Pain)),
					[LanguageCode.Spanish] = "Entumecidicicicicimo al Dolor",

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
