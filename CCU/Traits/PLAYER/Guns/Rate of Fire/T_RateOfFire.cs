namespace CCU.Traits.Player.Ranged_Combat
{
	public abstract class T_RateOfFire : T_PlayerTrait
	{
		public T_RateOfFire() : base() { }

		public abstract float CooldownMultiplier { get; }
	}
}