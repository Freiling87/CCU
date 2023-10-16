using BunnyLibs;

using RogueLibsCore;
using System.Collections.Generic;

namespace CCU.Traits.Player.Melee_Combat
{
	public class Speedbagger : T_PlayerTrait, IModifyItems
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Speedbagger>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Your fists have Rapid Fire.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Speedbagger)),
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					CharacterCreationCost = 5,
					IsAvailable = true,
					IsAvailableInCC = true,
					IsPlayerTrait = true,
					UnlockCost = 15,
					Unlock =
					{
						categories = { CTraitCategory.Unarmed },
					}
				});
		}

		public override void OnAdded() =>
			ModifyItemHelper.SetupInventory(Owner);
		public override void OnRemoved() =>
			ModifyItemHelper.SetupInventory(Owner);

		// IModifyItems
		public List<string> EligibleItemTypes => new List<string> { VItemType.WeaponMelee };
		public List<string> ExcludedItems => new List<string>() { };

		public bool IsEligible(Agent agent, InvItem invItem) =>
			invItem.invItemName == VItemName.Fist;

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