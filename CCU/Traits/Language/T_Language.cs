using RogueLibsCore;

namespace CCU.Traits.Language
{
    public abstract class T_Language : T_PlayerTrait
    {
        public T_Language() : base() { }

        public abstract string[] VanillaSpeakers { get; }
    }
    public class T_Language_Unlock : TraitUnlock
    {
    }
}