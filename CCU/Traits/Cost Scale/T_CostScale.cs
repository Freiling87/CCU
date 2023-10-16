namespace CCU.Traits.Cost_Scale
{
	public abstract class T_CostScale : T_CCU
	{
		public T_CostScale() : base() { }

		public abstract float CostScale { get; }
	}
}