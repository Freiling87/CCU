using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.Rel_Faction
{
	public class Blahd_Aligned : T_Rel_Faction
	{
		public override int Faction => 0;
		public override Alignment FactionAlignment => throw new System.NotImplementedException();

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Blahd_Aligned>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent is Aligned to Blahds and anyone else with this trait. They are also a valid target for Blahd Basher and the Blahd Super Special Ability.",
					[LanguageCode.Spanish] = "Este personaje es aliado de los Blahds y de todos que tengan este rasgo. Tambien son rivales de los que tengan Demolidor de Blahds y son afectados por la Abilidad Super Especial del Gangster.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Blahd_Aligned)),
					[LanguageCode.Spanish] = "Aliado de los Crepes",

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
			AlignmentUtils.CountsAsBlahd(agent)
				? VRelationship.Aligned
				: null;

		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}
