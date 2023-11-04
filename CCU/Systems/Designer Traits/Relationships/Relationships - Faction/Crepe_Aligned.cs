using RogueLibsCore;

namespace CCU.Traits.Rel_Faction
{
	public class Crepe_Aligned : T_Rel_Faction
	{
		public override int Faction => 0;
		public override Alignment FactionAlignment => Alignment.Aligned;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Crepe_Aligned>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent is Aligned to Crepes and anyone else with this trait. They are also a valid target for Crepe Crusher and the Crepe Super Special Ability.",
					[LanguageCode.Spanish] = "Este personaje es aliado de los Crepes y de todos que tengan este rasgo. Tambien son rivales de los que tengan Destructor de Crepes y son afectados por la Abilidad Super Especial del Gangster.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Crepe_Aligned)),
					[LanguageCode.Spanish] = "Aliado de los Crepes",
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
				? VRelationship.Aligned
				: null;
		
		
	}
}
