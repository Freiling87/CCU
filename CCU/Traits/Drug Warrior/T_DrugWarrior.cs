namespace CCU.Traits.Drug_Warrior
{
    public abstract class T_DrugWarrior : T_CCU
    {
        public T_DrugWarrior() : base() { }

        public abstract string DrugEffect { get; }
    }
}