namespace CCU.Traits.Rel_Player
{
	public abstract class T_Rel_Player : T_Relationships
	{
		public T_Rel_Player() : base() { }

		public abstract string Relationship { get; }
		public override string GetRelationshipTo(Agent agent) =>
			agent.isPlayer > 0
			? Relationship
			: null;
	}
}