namespace CCU.Traits.Behavior
{
    public abstract class T_Behavior : T_CCU
    {
        public T_Behavior() : base() { }

        public abstract bool LosCheck { get; }
    }
}