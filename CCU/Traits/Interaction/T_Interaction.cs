using CCU.Traits.Cost;
using RogueLibsCore;

namespace CCU.Traits.Interaction
{
    public abstract class T_Interaction : CustomTrait
    {
        public T_Interaction() : base() { }

        public abstract string ButtonText { get; }
    }
}