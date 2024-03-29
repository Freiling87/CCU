﻿using RogueLibsCore;

namespace CCU.Traits.Rel_Faction
{
	public class Gorilla_Hostile : T_Rel_Faction
	{
		public override int Faction => 0;
		public override Alignment FactionAlignment => Alignment.Hateful;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Gorilla_Hostile>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent is hostile to Gorillas and anyone with the Aligned to Gorilla trait.",
					[LanguageCode.Spanish] = "Este personaje es Hostil a los Gorilas y de todo quien sea Aliado de los Gorilas.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Gorilla_Hostile)),
					[LanguageCode.Spanish] = "Hostil a los Gorilas",

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
			agent.agentName == VanillaAgents.Gorilla ||
			agent.HasTrait<Gorilla_Aligned>()
				? VRelationship.Hostile
				: null;
		
		
	}
}
