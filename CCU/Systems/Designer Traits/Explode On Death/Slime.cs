﻿using RogueLibsCore;
using System;

namespace CCU.Traits.Explode_On_Death
{
	public class Slime : T_ExplodeOnDeath
	{
		public override string ExplosionType => VExplosionType.Slime;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Slime>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character dies doing what they loved: Secreting toxic slime.\n\nR.I.P."),
					[LanguageCode.Spanish] = "Al morir, este NPC demuestra su verdadera identidad, un asqueroso oso baboso.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Slime)),
					[LanguageCode.Spanish] = "Baba",

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