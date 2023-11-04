using RogueLibsCore;

namespace CCU.Traits.Rel_Faction
{
	public class Firefighter_Aligned : T_Rel_Faction
	{
		public override int Faction => 0;
		public override Alignment FactionAlignment => Alignment.Aligned;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Firefighter_Aligned>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent is Aligned to Firefighters and anyone else with this trait. They are hostile to Arsonists.",
					[LanguageCode.Spanish] = "Este personaje es aliado de los Bomberos y de todos que tengan este rasgo. Tambien son hostiles a los Incendiarios.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Firefighter_Aligned)),
					[LanguageCode.Spanish] = "Aliado de los Bomberos",
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
			agent.agentName == VanillaAgents.Firefighter ||
			agent.HasTrait<Firefighter_Aligned>()
				? VRelationship.Aligned
				: agent.arsonist || agent.activeArsonist
					? VRelationship.Hostile
					: null;
		
		
	}
}
