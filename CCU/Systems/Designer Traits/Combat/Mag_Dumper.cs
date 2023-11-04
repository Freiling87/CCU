using BunnyLibs;

using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Combat_
{
	public class Mag_Dumper : T_Combat, IModifyItems
	{
		public List<string> EligibleItemTypes => new List<string>() { "WeaponProjectile" };
		public List<string> ExcludedItems => new List<string>() { };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Mag_Dumper>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("When this character uses rapid fire, they hold the trigger down for much longer. What a mook."),
					[LanguageCode.Spanish] = "Este NPC usa balas de mas al usar armas automaticas. Como todo un rambo o pelotu-.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Mag_Dumper)),
					[LanguageCode.Spanish] = "Descarga-Balas",

				})
				.WithUnlock(new TU_DesignerUnlock
				{
					Cancellations = { },
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}

		public bool IsEligible(Agent agent, InvItem invItem) =>
			EligibleItemTypes.Contains(invItem.itemType) &&
			!ExcludedItems.Contains(invItem.invItemName);

		
		

		public void OnDrop(Agent agent, InvItem invItem)
		{
			invItem.longerRapidFire = invItem.GetOrAddHook<H_InvItem>().vanillaLongerRapidFire;
		}

		public void OnPickup(Agent agent, InvItem invItem)
		{
			invItem.longerRapidFire = true;
		}
	}
}
