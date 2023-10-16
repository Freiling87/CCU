using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.Rel_Faction
{
	public class Mafia_Hostile : T_Rel_Faction
	{
		public override int Faction => 0;
		public override Alignment FactionAlignment => throw new System.NotImplementedException();

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Mafia_Hostile>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent is hostile to Mobsters and anyone with the Friend of the Family trait.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Mafia_Hostile)),

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
			agent.agentName == VanillaAgents.Mobster ||
			agent.HasTrait(VanillaTraits.FriendoftheFamily)
				? VRelationship.Hostile
				: null;
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}