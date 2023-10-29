namespace CCU.Traits.Interaction
{
	public interface IBranchInteractionMenu
	{
		InteractionState interactionState { get; }
		bool ButtonCanShow(Agent interactingAgent);
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