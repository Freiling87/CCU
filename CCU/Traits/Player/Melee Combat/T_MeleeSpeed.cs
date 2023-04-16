namespace CCU.Traits.Player.Melee_Combat
{
	public abstract class T_MeleeSpeed : T_PlayerTrait
    {
        public T_MeleeSpeed() : base() { }

		public abstract float SpeedMultiplier { get; }
	}
}