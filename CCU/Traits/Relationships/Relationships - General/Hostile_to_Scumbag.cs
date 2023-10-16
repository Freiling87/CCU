using BunnyLibs;
using CCU.Traits.Trait_Gate;
using RogueLibsCore;

namespace CCU.Traits.Rel_General
{
	public class Hostile_to_Scumbag : T_Rel_General
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Hostile_to_Scumbag>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent is Aligned to Blahds and anyone else with this trait. They are also a valid target for Blahd Basher and the Blahd Super Special Ability.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Hostile_to_Scumbag)),

				})
				.WithUnlock(new TraitUnlock_CCU
				{
					Cancellations = { },
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}

		public override string GetRelationshipTo(Agent agent) =>
			agent.HasTrait<Scumbag>()
			|| agent.oma.mustBeGuilty
				? VRelationship.Hostile
				: null;

		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}
