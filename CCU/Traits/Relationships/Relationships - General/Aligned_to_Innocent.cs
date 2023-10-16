using BunnyLibs;
using CCU.Traits.Passive;
using RogueLibsCore;

namespace CCU.Traits.Rel_General
{
	public class Aligned_to_Innocent : T_Rel_General
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Aligned_to_Innocent>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent is Aligned to Blahds and anyone else with this trait. They are also a valid target for Blahd Basher and the Blahd Super Special Ability.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Aligned_to_Innocent)),

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
			agent.HasTrait<Innocent>()
			|| agent.HasTrait<Aligned_to_Innocent>()
				? VRelationship.Aligned
				: null;

		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}
