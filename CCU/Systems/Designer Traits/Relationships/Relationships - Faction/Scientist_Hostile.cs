using RogueLibsCore;

namespace CCU.Traits.Rel_Faction
{
	public class Scientist_Hostile : T_Rel_Faction
	{
		public override int Faction => 0;
		public override Alignment FactionAlignment => Alignment.Hateful;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Scientist_Hostile>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent is Hostile to Scientists.",
					[LanguageCode.Spanish] = "Este personaje es Hostil a los Cientificos.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Scientist_Hostile)),
					[LanguageCode.Spanish] = "Hostil a los Cientificos",

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
			agent.agentName == VanillaAgents.Scientist
				? VRelationship.Hostile
				: null;
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}
