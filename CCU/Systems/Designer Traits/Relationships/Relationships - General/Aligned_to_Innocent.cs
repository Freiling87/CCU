using CCU.Traits.Passive;
using RogueLibsCore;

namespace CCU.Traits.Rel_General
{
	public class Aligned_to_Innocent : T_Rel_General
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Aligned_to_Innocent>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = $"Agent is Aligned to any agents with the {nameof(Innocent)} trait.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Aligned_to_Innocent)),

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
			agent.HasTrait<Innocent>() || agent.HasTrait<Aligned_to_Innocent>()
				? VRelationship.Aligned
				: null;

		
		
	}
}
