using RogueLibsCore;

namespace CCU.Traits.Rel_Faction
{
	public class Soldier_Hostile : T_Rel_Faction
	{
		public override int Faction => 0;
		public override Alignment FactionAlignment => Alignment.Hateful;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Soldier_Hostile>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent is Hostile to Soldiers and anyone with the Aligned to Soldier trait.",
					[LanguageCode.Spanish] = "Este personaje es hostil a los Soldados y todo aliado de los soldados.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Soldier_Hostile)),
					[LanguageCode.Spanish] = "Hostil a los Soldados",
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
			agent.agentName == VanillaAgents.Soldier ||
			agent.HasTrait<Soldier_Aligned>()
				? VRelationship.Hostile
				: null;
		
		
	}
}
