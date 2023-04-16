using RogueLibsCore;

namespace CCU.Traits
{
    public class TraitUnlock_CCU : TraitUnlock
    {
        public TraitUnlock_CCU() { }

        // This is here because of the parallel systems between traits and unlocks.
        public bool IsPlayerTrait = false;
    }
}