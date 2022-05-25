namespace CCU.Traits.Bodyguarded
{
    public abstract class T_Bodyguarded : T_CCU
    {
        public T_Bodyguarded() : base() { }

        public abstract string BodyguardType { get; }
    }
}