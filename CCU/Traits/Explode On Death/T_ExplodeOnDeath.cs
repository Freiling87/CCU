using CCU.Localization;
using RogueLibsCore;
using System.Linq;

namespace CCU.Traits.Explode_On_Death
{
    public abstract class T_ExplodeOnDeath : T_CCU
    {
        public T_ExplodeOnDeath() : base() { }

        public abstract string ExplosionType { get; }

        public static string GetExplosionType(Agent agent) =>
            agent.GetTraits<T_ExplodeOnDeath>().Where(c => c.ExplosionType != null).FirstOrDefault()?.ExplosionType ??
            VExplosionType.Normal;
    }
}