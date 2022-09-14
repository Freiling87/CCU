using RogueLibsCore;

namespace CCU.Traits.Hairstyle
{
    public abstract class T_Hairstyle : T_CCU
    {
        public T_Hairstyle() : base() { }

        public abstract string[] HairstyleType { get; }
    }
}