﻿using CCU.Traits.Passive;
using RogueLibsCore;

namespace CCU.Traits.Rel_General
{
	public class Hostile_to_Guilty : T_Rel_General
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Hostile_to_Guilty>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent is Aligned to Blahds and anyone else with this trait. They are also a valid target for Blahd Basher and the Blahd Super Special Ability.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Hostile_to_Guilty)),

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

		public override string GetRelationshipTo(Agent agent) =>
			agent.HasTrait<Guilty>()
			|| agent.oma.mustBeGuilty
				? VRelationship.Hostile
				: null;

		
		
	}
}
