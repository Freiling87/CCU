namespace CCU.Traits.Relationships
{
    public abstract class T_Relationships : T_CCU
    {
        public T_Relationships() : base() { }

        public abstract string GetRelationshipTo(Agent agent);
    }
}