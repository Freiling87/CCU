﻿using RogueLibsCore;
using System;

namespace CCU.Traits.Interaction_Gate
{
	public class Insularer : T_InteractionGate
	{
		public override int MinimumRelationship => 4;

		//[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Insularer>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character will not interact with anyone who has a relationship with their faction at Loyal or worse."),
					[LanguageCode.Spanish] = "Este NPC no interactuara con quien sea leal o peor.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Insularer)),
					[LanguageCode.Spanish] = "Aun Mas Insular",

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
