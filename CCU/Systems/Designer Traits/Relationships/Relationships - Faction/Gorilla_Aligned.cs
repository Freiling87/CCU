using RogueLibsCore;

namespace CCU.Traits.Rel_Faction
{
	public class Gorilla_Aligned : T_Rel_Faction
	{
		public override int Faction => 0;
		public override Alignment FactionAlignment => Alignment.Aligned;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Gorilla_Aligned>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent is Aligned to Gorillas and anyone else with this trait.",
					[LanguageCode.Spanish] = "Este personaje es aliado de los soldados y de todos que tengan este rasgo.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Gorilla_Aligned)),
					[LanguageCode.Spanish] = "Aliado de los Gorilas",
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
				? VRelationship.Aligned
				: null;
		
		
	}
}
