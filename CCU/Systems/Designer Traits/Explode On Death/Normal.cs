﻿using RogueLibsCore;
using System;

namespace CCU.Traits.Explode_On_Death
{
	public class Normal : T_ExplodeOnDeath
	{
		public override string ExplosionType => VExplosionType.Normal;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Normal>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("On death, this character explodes. About 1 Slave's worth."),
					[LanguageCode.Spanish] = "Al morir, este NPC explota como un esclavo.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Normal)),
					[LanguageCode.Spanish] = "Normal",

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