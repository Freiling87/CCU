﻿using RogueLibsCore;
using System;

namespace CCU.Traits.CombatGeneric
{
	public class Gun_Shy : T_GunSkill
	{
		public override int GunSkill => 0;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Gun_Shy>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Generally reluctant to use guns."),
					[LanguageCode.Spanish] = "Este NPC trata de no usar armas de fuego.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Gun_Shy)),
					[LanguageCode.Spanish] = "Inepto al Gatillo",

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
