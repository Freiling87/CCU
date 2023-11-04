namespace CCU.Traits.Interaction_Gate
{
	public abstract class T_InteractionGate : T_DesignerTrait
	{
		public T_InteractionGate() : base() { }

		public abstract int MinimumRelationship { get; }
	}
}