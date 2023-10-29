using BunnyLibs;

using RogueLibsCore;
using System.Collections.Generic;

namespace CCU.Traits.Player.Ranged_Combat
{
	public class Pants_on_Autofire : T_PlayerTrait, IModifyItems
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_PlayerTrait = RogueLibs.CreateCustomTrait<Pants_on_Autofire>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "You totally don't have to shit, you *swear*, but you're really in a hurry so you need to shoot all these guys real quick. All your weapons have autofire. Good luck in there.",
					[LanguageCode.Spanish] = "No hay tiempo que perder, *SI HAY* balas que perder, todas tus armas son automaticas!",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Pants_on_Autofire)),
					[LanguageCode.Spanish] = "Fuego Automatizado",
				})
				.WithUnlock(new TU_PlayerUnlock
				{
					Cancellations = { },
					CharacterCreationCost = 5,
					IsAvailable = true,
					IsAvailableInCC = true,
					
					UnlockCost = 15,
					Unlock =
					{
						categories = { VTraitCategory.Guns },
					}
				});
		}

		public override void OnAdded() =>
			ModifyItemHelper.SetupInventory(Owner);
		public override void OnRemoved() =>
			ModifyItemHelper.SetupInventory(Owner);

		// IModifyItems
		public List<string> EligibleItemTypes => new List<string>() { "WeaponProjectile" };
		public List<string> ExcludedItems => new List<string>() { };

		public bool IsEligible(Agent agent, InvItem invItem) =>
			EligibleItemTypes.Contains(invItem.itemType) &&
			!ExcludedItems.Contains(invItem.invItemName);

		public void OnDrop(Agent agent, InvItem invItem)
		{
			invItem.rapidFire = invItem.GetOrAddHook<H_InvItem>().vanillaRapidFire;
		}

		public void OnPickup(Agent agent, InvItem invItem)
		{
			invItem.rapidFire = true;
		}
	}
}