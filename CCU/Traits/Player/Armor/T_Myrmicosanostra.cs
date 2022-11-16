namespace CCU.Traits.Player.Armor
{
    public abstract class T_Myrmicosanostra : T_PlayerTrait
    {
        public T_Myrmicosanostra() : base() { }

        public override void OnAdded() { }
        public override void OnRemoved() { }

        public abstract float ArmorDurabilityChangeMultiplier { get; }
    }
}
