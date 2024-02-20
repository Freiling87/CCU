using RogueLibsCore;

namespace CCU.Interactions
{
	public interface IAgentInteraction
	{
		// TODO: How much of this should be parallel between PC/NPC interaction traits? That's why Panhandler might be informative to complete.

		string ButtonTextName { get; }
		string MoneyCostName { get; }
		bool NonMoneyCost { get; }
		bool RequireCommunication { get; }
		bool RequireTrust { get; }
		InteractionState? StateApplied { get; }
		InteractionState? StateRequired { get; }

		// TODO: Move default implementations here when you upgrade C#
		bool InteractionAllowed(Agent interactingAgent);
		void AddInteraction(SimpleInteractionProvider<Agent> h);
	}

	public enum InteractionState
	{
		Default,
		//
		LearnTraits_Defense,
		LearnTraits_Guns,
		LearnTraits_Melee,
		LearnTraits_Movement,
		LearnTraits_Language,
		LearnTraits_Social,
		LearnTraits_Stealth,
		LearnTraits_Trade,
	}
}