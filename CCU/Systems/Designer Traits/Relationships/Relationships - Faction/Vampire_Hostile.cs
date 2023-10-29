using CCU.Traits.LOS_Behavior;
using RogueLibsCore;

namespace CCU.Traits.Rel_Faction
{
	public class Vampire_Hostile : T_Rel_Faction
	{
		public override int Faction => 0;
		public override Alignment FactionAlignment => Alignment.Hateful;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Vampire_Hostile>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "This character is Hostile to Vampires and anyone with the Suck Blood trait.",
					[LanguageCode.Spanish] = "Este personaje es Hostil a los Vampiros y todo quien chupe Sangre.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Vampire_Hostile)),
					[LanguageCode.Spanish] = "Hostil a los Vampiros",

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
			agent.agentName == VanillaAgents.Vampire ||
			agent.HasTrait<Suck_Blood>()
				? VRelationship.Hostile
				: null;
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}
