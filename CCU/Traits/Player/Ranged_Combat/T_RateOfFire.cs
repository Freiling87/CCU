using BepInEx.Logging;
using RogueLibsCore;

namespace CCU.Traits.Player.Ranged_Combat
{
    public abstract class T_RateOfFire : T_PlayerTrait
    {
        public T_RateOfFire() : base() { }

        public abstract float CooldownMultiplier { get; }

        public static float WeaponCooldown(Agent agent, float vanilla)
        {
            foreach (T_RateOfFire trait in agent.GetTraits<T_RateOfFire>())
                vanilla *= trait.CooldownMultiplier;

            return vanilla;
        }
    }
}