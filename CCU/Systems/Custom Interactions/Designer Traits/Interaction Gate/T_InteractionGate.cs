namespace CCU.Traits.Interaction_Gate
{
	public abstract class T_InteractionGate : T_CCU
	{
		public T_InteractionGate() : base() { }

		public abstract int MinimumRelationship { get; }
	}
}