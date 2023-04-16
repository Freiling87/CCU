using CCU.Hooks;
using RogueLibsCore;
using System.Collections.Generic;

namespace CCU.Traits.Player.Ranged_Combat
{
	public class Pants_on_Autofire : T_PlayerTrait, IModifyItems
    {
		[RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Pants_on_Autofire>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "You totally don't have to shit, you *swear*, but you're really in a hurry so you need to shoot all these guys real quick. All your weapons have autofire. Good luck in there.",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = PlayerName(typeof(Pants_on_Autofire)),
                })
                .WithUnlock(new TraitUnlock_CCU
                {
                    Cancellations = { },
                    CharacterCreationCost = 5,
                    IsAvailable = true,
                    IsAvailableInCC = true,
                    IsPlayerTrait = true,
                    UnlockCost = 15,
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
			if (IsEligible(agent, invItem))
			    invItem.rapidFire = true;
		}
    }
}