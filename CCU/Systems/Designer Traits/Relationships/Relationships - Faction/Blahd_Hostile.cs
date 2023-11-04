using RogueLibsCore;

namespace CCU.Traits.Rel_Faction
{
	public class Blahd_Hostile : T_Rel_Faction
	{
		public override int Faction => 0;
		public override Alignment FactionAlignment => Alignment.Hateful;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Blahd_Hostile>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent is Hostile to Blahds and anyone with the Aligned to Blahd trait.",
					[LanguageCode.Spanish] = "Este Personaje es Hostil a los Blahds y todo quien sea Aliado de los Blahd.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Blahd_Hostile)),
					[LanguageCode.Spanish] = "Hostil a los Blahd",
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
			AlignmentUtils.CountsAsBlahd(agent)
			|| agent.HasTrait(VanillaTraits.CrepeCrusher)
				? VRelationship.Hostile
				: null;
		
		
	}
}
