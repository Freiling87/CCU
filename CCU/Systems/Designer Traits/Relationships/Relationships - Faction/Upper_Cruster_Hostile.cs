using CCU.Traits.Passive;
using RogueLibsCore;

namespace CCU.Traits.Rel_Faction
{
	public class Upper_Cruster_Hostile : T_Rel_Faction
	{
		public override int Faction => 0;
		public override Alignment FactionAlignment => Alignment.Hateful;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Upper_Cruster_Hostile>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "This character is Hostile to transformed Werewolves. If they have Werewolf A-Were-Ness, they are hostile to the non-transformed as well.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Upper_Cruster_Hostile)),
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
			agent.agentName == VanillaAgents.UpperCruster
			|| agent.HasTrait(VanillaTraits.UpperCrusty)
			|| agent.HasTrait<Crusty>()
				? VRelationship.Hostile
				: null;
		
		
	}
}
