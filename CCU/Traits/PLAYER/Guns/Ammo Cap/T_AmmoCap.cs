using BepInEx.Logging;
using RogueLibsCore;
using System.Collections.Generic;

namespace CCU.Traits.Player.Ammo
{
    public abstract class T_AmmoCap : T_PlayerTrait, IModifyItems
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();

		public T_AmmoCap() : base() { }

		public abstract float AmmoCapMultiplier { get; }

		public override void OnAdded() =>
			ModifyItemHelper.SetupInventory(Owner);
		public override void OnRemoved() =>
			ModifyItemHelper.SetupInventory(Owner);

		// IModifyItems
		public List<string> EligibleItemTypes => new List<string> { "WeaponProjectile" };
		public List<string> ExcludedItems => new List<string>() { };

		public bool IsEligible(Agent agent, InvItem invItem) =>
			EligibleItemTypes.Contains(invItem.itemType) &&
			!ExcludedItems.Contains(invItem.invItemName);

		public void OnDrop(Agent agent, InvItem invItem)
		{
			float ammoCap = invItem.initCount;

			if (invItem.contents.Contains(vItem.AmmoCapacityMod))
				ammoCap *= 1.4f;

			invItem.maxAmmo = (int)ammoCap;
		}

		public void OnPickup(Agent agent, InvItem invItem)
		{
			float ammoCap = invItem.initCount;

			if (invItem.contents.Contains(vItem.AmmoCapacityMod))
				ammoCap *= 1.4f;

			foreach (T_AmmoCap trait in agent.GetTraits<T_AmmoCap>())
				ammoCap *= trait.AmmoCapMultiplier;

			invItem.maxAmmo = (int)ammoCap;
		}
	}
}