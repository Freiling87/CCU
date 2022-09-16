using RogueLibsCore;

namespace CCU.Traits.Hair_Color
{
    public abstract class T_HairColor : T_CCU
    {
        public T_HairColor() : base() { }

        public abstract string[] HairColors { get; }
    }
}