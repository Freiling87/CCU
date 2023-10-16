using BunnyLibs;

using RogueLibsCore;
using System.Collections.Generic;

namespace CCU.Traits.Loadout_Gun_Nut
{
	public abstract class T_GunNut : T_PlayerTrait, IModifyItems
	{
		public T_GunNut() : base() { }

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
			// Cyan: I don't think it would be overpowered, gun mods are great but they're not game-breakingly great. If you hand a crepe a silenced rapid firing shotgun they're still going to find a way to burn themselves to death on the way through the hideout you're raiding, might even lose your shotgun down the conveyer hole.
			//H_InvItem hook = invItem.GetOrAddHook<H_InvItem>();

			//if (hook.freeWeaponMods.Contains(GunMod))
			//	hook.RemoveWeaponMod(GunMod);
		}

		public void OnPickup(Agent agent, InvItem invItem)
		{
			invItem.GetOrAddHook<H_InvItem>().AddWeaponMod(GunMod);
		}
	}
}
