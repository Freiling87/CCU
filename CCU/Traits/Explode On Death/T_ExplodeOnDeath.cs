namespace CCU.Traits.Explode_On_Death
{
    public abstract class T_ExplodeOnDeath : T_CCU
    {
        public T_ExplodeOnDeath() : base() { }

        public abstract string ExplosionType { get; }
    }
}