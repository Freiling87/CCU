using RogueLibsCore;

namespace CCU.Traits.Rel_Faction
{
	public class Faction_1_Hostile : T_Rel_Faction
	{
		public override int Faction => 1;
		public override Alignment FactionAlignment => Alignment.Hateful;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Faction_1_Hostile>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "This character is Hostile to all characters aligned with Faction 1.",
					[LanguageCode.Spanish] = "Este personaje es hostil a todo quien sea Aliado de La Facción 1.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Faction_1_Hostile)),
					[LanguageCode.Spanish] = "Hostil a la Facción 2",

				})
				.WithUnlock(new TU_DesignerUnlock
				{
					Cancellations = { DesignerName(typeof(Faction_1_Aligned)) },
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}

		public override string GetRelationshipTo(Agent agent) =>
			agent.HasTrait<Faction_1_Aligned>()
				? VRelationship.Hostile
				: null;
		
		
	}
}
