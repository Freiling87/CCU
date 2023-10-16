using RogueLibsCore;

namespace CCU.Traits
{
	public class TraitUnlock_CCU : TraitUnlock
	{
		public TraitUnlock_CCU() { }

		// This is what determines 


		// This is here because of the parallel systems between traits and unlocks.
		//  Coming back to this, I still don't see the point. The type is not used anywhere. Was this just a way to make Designer traits the default?
		public bool IsPlayerTrait = false;
	}
}