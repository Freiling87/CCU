namespace CCU.Traits.Drug_Warrior
{
    public abstract class T_DrugWarrior : T_CCU, ISetupAgentStats
    {
        public T_DrugWarrior() : base() { }

        public abstract string DrugEffect { get; }

        public void SetupAgentStats(Agent agent)
        {
            agent.combat.canTakeDrugs = true;
        }
    }
}