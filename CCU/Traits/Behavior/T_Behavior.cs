namespace CCU.Traits.Behavior
{
	public abstract class T_Behavior : T_CCU
	{
		public T_Behavior() : base() { }

		// Move to a LOSCheckTrait interface
		public abstract bool LosCheck { get; }
		public abstract string[] GrabItemCategories { get; }
	}
}