namespace CCU.Traits.Interaction
{
    public abstract class T_Interaction : T_CCU
    {
        public T_Interaction() : base() { }

        public abstract bool AllowUntrusted { get; }
        public abstract string ButtonID { get; }
        public abstract bool HideCostInButton { get; }
        public abstract string DetermineMoneyCostID { get; }
    }
}