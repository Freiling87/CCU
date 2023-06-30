using CCU.Hooks;

namespace CCU.Traits.Interaction
{
	public interface IBranchInteractionMenu
	{
		InteractionState interactionState { get; }
	}

	public enum InteractionState
	{
		Default,
		//
		TeachTraits_Defense,
		TeachTraits_Guns,
		TeachTraits_Melee,
		TeachTraits_Movement,
		TeachTraits_Language,
		TeachTraits_Social,
		TeachTraits_Stealth,
		TeachTraits_Trade,
	}

}