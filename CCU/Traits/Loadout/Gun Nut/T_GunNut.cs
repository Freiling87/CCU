using CCU.Traits.Loadout;
using RogueLibsCore;
using System.Collections.Generic;

namespace CCU.Traits.Loadout_Gun_Nut
{
    public abstract class T_GunNut : T_Loadout
    {
        public T_GunNut() : base() { }

        public override void OnAdded() { }
        public override void OnRemoved() { }

        public abstract string GunMod { get; }
        public abstract List<string> ExcludedItems { get; }

        public static void AddModsFromTraits(Agent agent, InvItem invItem)
        {
            foreach (T_GunNut trait in agent.GetTraits<T_GunNut>())
                if (invItem.itemType == "WeaponProjectile" && 
                        !invItem.contents.Contains(trait.GunMod) &&
                        !trait.ExcludedItems.Contains(invItem.invItemName))
                    invItem.contents.Add(trait.GunMod);
        }
    }
}
