namespace CCU.Traits.Hire_Type
{
    public abstract class T_HireType : T_CCU
    {
        public T_HireType() : base() { }

        public abstract string HiredActionButtonText { get; }
        public abstract string ButtonText { get; }
        public abstract object HireCost { get; }
    }
}
