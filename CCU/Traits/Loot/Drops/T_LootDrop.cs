using System.Collections.Generic;

namespace CCU.Traits.Loot_Drops
{
    public abstract class T_LootDrop : T_CCU
    {
        public T_LootDrop() : base() { }

        public abstract bool IsUnspillable(InvItem invItem);

        public override void OnAdded() { }
        public override void OnRemoved() { }

        internal static List<string> SoftLockItems = new List<string>()
        {
            vItem.Key,
            vItem.KeyCard,
            vItem.SafeCombination,
            vItem.MayorBadge,
        };

    }
}
