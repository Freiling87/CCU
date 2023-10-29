using BunnyLibs;
using RogueLibsCore;
using System.Collections.Generic;

namespace CCU.Traits.Loadout_Gun_Nut
{
	public abstract class T_GunNut : T_PlayerTrait, IModifyItems, IRefreshAtEndOfLevelStart
	{
		public T_GunNut() : base() { }

		public abstract string[] AddedItemCategories { get; }
		public abstract string GunMod { get; }

		public override void OnAdded() =>
			ModifyItemHelper.SetupInventory(Owner);
		public override void OnRemoved() =>
			ModifyItemHelper.SetupInventory(Owner);

		// IModifyItems
		public abstract List<string> ExcludedItems { get; }
		public List<string> EligibleItemTypes => new List<string>() { "WeaponProjectile" };

		public bool IsEligible(Agent agent, InvItem invItem) =>
			EligibleItemTypes.Contains(invItem.itemType) &&
			!ExcludedItems.Contains(invItem.invItemName) &&
			!invItem.contents.Contains(GunMod);
		public void OnDrop(Agent agent, InvItem invItem)
		{
			// Leave the mods on. Gives a reason to actually use the trait.
		}
		public void OnPickup(Agent agent, InvItem invItem)
		{
			invItem.GetOrAddHook<H_InvItem>().TryModWeapon(GunMod);
		}

		public bool BypassUnlockChecks => false;
		public void Refresh() { }
		public void Refresh(Agent agent)
		{
			ModifyItemHelper.SetupInventory(agent);
		}
		public bool RunThisLevel() => true;
	}
}