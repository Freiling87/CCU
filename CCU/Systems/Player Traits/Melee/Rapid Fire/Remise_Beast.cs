using BunnyLibs;

using RogueLibsCore;
using System.Collections.Generic;

namespace CCU.Traits.Player.Melee_Combat
{
	public class Remise_Beast : T_PlayerTrait, IModifyItems
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_PlayerTrait = RogueLibs.CreateCustomTrait<Remise_Beast>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "All melee weapons have rapid fire.",
					[LanguageCode.Spanish] = "Todo ataque Cuerpo a Cuerpo es ahora automatico!",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Remise_Beast)),
					[LanguageCode.Spanish] = "Bestia Agitada",
				})
				.WithUnlock(new TU_PlayerUnlock
				{
					CharacterCreationCost = 5,
					IsAvailable = true,
					IsAvailableInCC = true,
					
					UnlockCost = 15,
					Unlock =
					{
						categories = { VTraitCategory.Melee },
					}
				});
		}

		public override void OnAdded() =>
			ModifyItemHelper.SetupInventory(Owner);
		public override void OnRemoved() =>
			ModifyItemHelper.SetupInventory(Owner);

		// IModifyItems
		public List<string> EligibleItemTypes => new List<string> { "WeaponMelee" };
		public List<string> ExcludedItems => new List<string>() { VItemName.Fist };

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