using RogueLibsCore;

namespace CCU.Traits.Rel_Faction
{
	public class Crepe_Hostile : T_Rel_Faction
	{
		public override int Faction => 0;
		public override Alignment FactionAlignment => Alignment.Hateful;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Crepe_Hostile>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent is Hostile to Crepes and anyone with the Aligned to Crepe trait.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Crepe_Hostile)),
					[LanguageCode.Spanish] = "Hostil a los Crepes",

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
			AlignmentUtils.CountsAsCrepe(agent)
			|| agent.HasTrait(VanillaTraits.BlahdBasher)
				? VRelationship.Hostile
				: null;
		
		
	}
}
