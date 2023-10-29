using RogueLibsCore;

namespace CCU.Traits.Rel_Faction
{
	public class Cannibal_Hostile : T_Rel_Faction
	{
		public override int Faction => 0;
		public override Alignment FactionAlignment => Alignment.Hateful;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Cannibal_Hostile>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent is hostile to Cannibals and anyone with the Aligned to Cannibal trait.",
					[LanguageCode.Spanish] = "Este personaje es Hostil a los Canibales y todo Aliado de los Canibales.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Cannibal_Hostile)),
					[LanguageCode.Spanish] = "Hostil a los Canibales",

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
			agent.agentName == VanillaAgents.Cannibal ||
			agent.HasTrait<Cannibal_Aligned>()
				? VRelationship.Hostile
				: null;
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}
