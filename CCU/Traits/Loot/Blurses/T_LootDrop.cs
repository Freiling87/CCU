using BunnyLibs;
using System.Collections.Generic;

namespace CCU.Traits.Loot_Drops
{
	public abstract class T_LootDrop : T_CCU
	{
		public T_LootDrop() : base() { }

		public abstract bool IsUnspillable(InvItem invItem);

		public override void OnAdded() { }
		public override void OnRemoved() { }

		public static List<string> SoftLockItems = new List<string>()
		{
			VItemName.Key,
			VItemName.KeyCard,
			VItemName.SafeCombination,
			VItemName.MayorBadge,
		};

	}
}
