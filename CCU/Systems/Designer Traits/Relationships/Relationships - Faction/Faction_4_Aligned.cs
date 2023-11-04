using RogueLibsCore;

namespace CCU.Traits.Rel_Faction
{
	public class Faction_4_Aligned : T_Rel_Faction
	{
		public override int Faction => 4;
		public override Alignment FactionAlignment => Alignment.Aligned;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Faction_4_Aligned>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "This character is Aligned with all characters who share the trait.",
					[LanguageCode.Spanish] = "Este personaje es aliado de todos los que tengan este rasgo.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Faction_4_Aligned)),
					[LanguageCode.Spanish] = "Aliado de la Facción 4",

				})
				.WithUnlock(new TU_DesignerUnlock
				{
					Cancellations = { DesignerName(typeof(Faction_4_Hostile)) },
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}

		public override string GetRelationshipTo(Agent agent) =>
			agent.HasTrait<Faction_4_Aligned>()
				? VRelationship.Aligned
				: null;
		
		
	}
}
