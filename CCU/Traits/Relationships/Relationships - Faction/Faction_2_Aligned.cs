using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.Rel_Faction
{
	public class Faction_2_Aligned : T_Rel_Faction
	{
		public override int Faction => 2;
		public override Alignment FactionAlignment => Alignment.Aligned;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Faction_2_Aligned>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "This character is Aligned with all characters who share the trait.",
					[LanguageCode.Spanish] = "Este personaje es aliado de todos los que tengan este rasgo.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Faction_2_Aligned)),
					[LanguageCode.Spanish] = "Aliado de la Facción 2",

				})
				.WithUnlock(new TraitUnlock_CCU
				{
					Cancellations = { DesignerName(typeof(Faction_2_Hostile)) },
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}

		public override string GetRelationshipTo(Agent agent) =>
			agent.HasTrait<Faction_2_Aligned>()
				? VRelationship.Aligned
				: null;
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}
