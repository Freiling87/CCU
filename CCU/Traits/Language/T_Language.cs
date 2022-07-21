namespace CCU.Traits.Language
{
    public abstract class T_Language : T_CCU
    {
        public T_Language() : base() { }

        public abstract string[] VanillaSpeakers { get; }
    }
}