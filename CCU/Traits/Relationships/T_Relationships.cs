namespace CCU.Traits
{
	public abstract class T_Relationships : T_CCU
	{
		public T_Relationships() : base() { }

		public abstract string GetRelationshipTo(Agent agent);
	}
}