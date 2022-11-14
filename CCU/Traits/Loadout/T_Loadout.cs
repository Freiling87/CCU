using BepInEx.Logging;
using CCU.Traits.Loadout_Loader;
using CCU.Traits.Loadout_Money;
using CCU.Traits.Loadout_Pockets;
using CCU.Traits.Loadout_Slots;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CCU.Traits.Loadout
{
    public abstract class T_Loadout : T_CCU
	{
		public T_Loadout() : base() { }
	}

	public static class LoadoutTools
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public enum Slots
		{
			Pockets,
			Headgear,
			Body,
			WeaponMelee,
			WeaponThrown,
			WeaponRanged, // This order to ensure precedence of equipping
		}

		public static void SetupLoadout(InvDatabase invDatabase)
		{
			Agent agent = invDatabase.agent;
			logger.LogDebug("SetupLoadout: " + agent.agentName + "(" + agent.agentRealName + ")");

			if (agent.agentName != VanillaAgents.CustomCharacter ||
				!agent.GetTraits<T_Loadout>().Any() ||
				agent.isPlayer != 0)
				return;

			invDatabase.DontPlayPickupSounds(true);
			T_PocketMoney.AddMoney(agent);
			LoadCustomInventory(invDatabase);
			invDatabase.ChooseArmor();
			invDatabase.ChooseArmorHead();
			invDatabase.ChooseWeapon();
			invDatabase.DontPlayPickupSounds(false);
		}

		// TODO: set invDatabase to a static value, defined on entering this system.
		//	Then it can be called internally without having to pass it around.
		private static void LoadCustomInventory(InvDatabase invDatabase)
        {
			Agent agent = invDatabase.agent;
			List<InvItem> invItemsFromCC = new List<InvItem>();

			foreach (string str in agent.customCharacterData.items)
            {
				InvItem invItem = new InvItem();
				invItem.invItemName = invDatabase.SwapWeaponTypes(str); // Applies No Guns, Rocket Chaos, etc.
				invItem.SetupDetails(false); // TODO: Verify that Syringe and cocktail generate correctly
				invItemsFromCC.Add(invItem);
            }

			foreach (Slots currentInvSlot in Enum.GetValues(typeof(Slots)))
			{
				if (agent.HasTrait<Flat_Distribution>() &&
					(currentInvSlot == Slots.Pockets && (
						(agent.HasTrait<Have_Mostly>() && GC.percentChance(25)) ||
						(agent.HasTrait<Have_Not>() && GC.percentChance(50)))
					) ||
					(currentInvSlot != Slots.Pockets && (
						(agent.HasTrait<Equipment_Enjoyer>() && GC.percentChance(25)) ||
						(agent.HasTrait<Equipment_Virgin>() && GC.percentChance(50))
					)))
					continue;
				
				int maximum = GetSlotMax(agent, currentInvSlot);
				List<InvItem> itemBagForSlot = 
					invItemsFromCC
						.Where(ii => GetSlotFromItem(ii) == currentInvSlot)
						.OrderByDescending(ii => ii.itemValue)
						.ToList();

				while (ItemsInSlot(invDatabase.InvItemList, currentInvSlot) < maximum && 
					itemBagForSlot.Count() > 0)
				{
					InvItem pickedItem = null;
					bool addItem = false;

                    #region Pick Item, roll Chance
                    if (agent.HasTrait<Flat_Distribution>())
                    {
						addItem = true;
						pickedItem = itemBagForSlot[CoreTools.random.Next(itemBagForSlot.Count())];
					}
					else if (agent.HasTrait<Scaled_Distribution>() || agent.HasTrait<Upscaled_Distribution>())
					{
						pickedItem = itemBagForSlot[0];

						int chance = 
							agent.HasTrait<Scaled_Distribution>()
								? Mathf.Clamp((int)((200f - pickedItem.itemValue) / 3.4f), 1, 100)
								: agent.HasTrait<Upscaled_Distribution>()
									? Mathf.Clamp((int)(pickedItem.itemValue / 3.4f), 1, 100) 
									: 0;

						if (currentInvSlot is Slots.Pockets)
                        {
							if (agent.HasTrait<Have_Mostly>())
								chance *= 2;

							if (agent.HasTrait<Have_Not>())
								chance /= 2;

							if ((agent.HasTrait<Have>() && itemBagForSlot.Count() is 1) ||
								GC.percentChance(chance))
								addItem = true;
						}
                        else
						{
							if (agent.HasTrait<Equipment_Enjoyer>())
								chance *= 2;

							if (agent.HasTrait<Equipment_Virgin>())
								chance /= 2;

							if ((agent.HasTrait<Equipment_Chad>() && itemBagForSlot.Count() is 1) ||
								GC.percentChance(chance))
								addItem = true;
						}
					}
                    #endregion

                    if (pickedItem is null)
						continue;

					string invItemName = pickedItem.invItemName;

					if (agent.inventory.InvItemList.Select(ii => ii.invItemName).Contains(invItemName))
						continue;

                    #region Set Count
                    switch (currentInvSlot)
                    {
						case Slots.WeaponMelee:
							if (GC.challenges.Contains("InfiniteMeleeDurability"))
								pickedItem.invItemCount = 100;
							else
								pickedItem.invItemCount = int.Parse(invDatabase.rnd.RandomSelect("AgentMeleeDurability", "Others"));

							break;

						case Slots.WeaponRanged:
							if (!GC.challenges.Contains("InfiniteAmmo") &&
							!(GC.challenges.Contains("InfiniteAmmoNormalWeapons") &&
								!pickedItem.Categories.Contains("NonStandardWeapons") ||
								!pickedItem.Categories.Contains("NonStandardWeapons2")) &&
							!invDatabase.agent.warZoneAgent)
								pickedItem.invItemCount = pickedItem.initCount;
							else
								pickedItem.invItemCount = (int)(pickedItem.initCountAI *
									(int.Parse(invDatabase.rnd.RandomSelect("AgentProjectileBullets", "Others")) / 10f));

							break;

						default:
							pickedItem.invItemCount = pickedItem.initCount;

							break;
					}
                    #endregion

                    pickedItem.dontAutomaticallySelect = false;
					itemBagForSlot.Remove(pickedItem);

					if (addItem)
						invDatabase.AddItem(pickedItem);
				}
			}
		}
		public static Slots GetSlotFromItem(string invItemName)
		{
			InvItem invItem = new InvItem();
			invItem.invItemName = invItemName;
			invItem.SetupDetails(false);
			return GetSlotFromItem(invItem);
		}
		public static Slots GetSlotFromItem(InvItem invItem)
        {
			if (invItem.isArmorHead)
				return Slots.Headgear;
			else if (invItem.isArmor)
				return Slots.Body;
			else if (// Yes, "Weapons" is added to a couple of non-weapon groups in the base code.
				invItem.itemType != "Wearable" &&
				invItem.itemType != "GunAccessory" &&
				invItem.Categories.Contains("Weapons"))
			{
				if (invItem.itemType is "WeaponMelee" || invItem.weaponCode is weaponType.WeaponMelee)
					return Slots.WeaponMelee;
				else if (invItem.itemType is "WeaponProjectile" || invItem.weaponCode is weaponType.WeaponProjectile)
					return Slots.WeaponRanged;
				else if (invItem.itemType is "WeaponThrown" || invItem.weaponCode is weaponType.WeaponThrown)
					return Slots.WeaponThrown;
			}
			
			return Slots.Pockets;
		}
		private static int GetSlotMax(Agent agent, Slots slot)
		{
			int max = 1;

			if (slot is Slots.Pockets)
            {

				if (agent.HasTrait<FunnyPack>())
					max += 1;

				if (agent.HasTrait<FunnyPack_Extreme>())
					max += 2;

				if (agent.HasTrait<FunnyPack_Pro>())
					max = 99;
			}
			else
            {
				if (agent.HasTrait<Sidearmed>())
					max += 1;

				if (agent.HasTrait<Sidearmed_but_on_Both_Sides>())
					max += 2;

				if (agent.HasTrait<Sidearmed_to_the_Teeth>())
					max = 99;
			}

			return max;
		}
		private static List<string> ExemptSlotItems = new List<string>()
		{
			vItem.ChloroformHankie,
			vItem.Fist,
			vItem.LaserGun,
			vItem.ResearchGun,
			vItem.StickyGlove,
			vItem.WaterCannon,
		};
        private static int ItemsInSlot(List<InvItem> list, Slots slot) =>
            list.Where(ii => GetSlotFromItem(ii) == slot && ii.invItemName != "" && !(ii.invItemName is null) && !ExemptSlotItems.Contains(ii.invItemName)).Count();
	}
}