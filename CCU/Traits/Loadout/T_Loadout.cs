namespace CCU.Traits.Loadout
{
    public abstract class T_Loadout : T_CCU
    {
        public T_Loadout() : base() { }

        public abstract string[] Rolls { get; }
    }
}