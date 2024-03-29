﻿using RogueLibsCore;

namespace CCU.Traits.Rel_Faction
{
	public class Common_Folk_Hostile : T_Rel_Faction
	{
		public override int Faction => 0;
		public override Alignment FactionAlignment => Alignment.Hateful;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Common_Folk_Hostile>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent is hostile to Slum Dwellers and anyone with the Friend of the Common Folk trait.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Common_Folk_Hostile)),

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
			agent.agentName == VanillaAgents.SlumDweller ||
			agent.HasTrait(VanillaTraits.FriendoftheCommonFolk)
				? VRelationship.Hostile
				: null;
		
		
	}
}
