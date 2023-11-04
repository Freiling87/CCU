using BepInEx.Logging;
using BunnyLibs;
using CCU.Traits.Loadout_Loader;
using CCU.Traits.Loadout_Money;
using CCU.Traits.Loadout_Pockets;
using CCU.Traits.Loadout_Slots;
using CCU.Traits.Player.Ammo;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CCU.Traits.Loadout
{
	public abstract class T_Loadout : T_DesignerTrait
	{
		public bool AlwaysApply => false;
		public T_Loadout() : base() { }
	}

	public static class LoadoutTools
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
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

		// TODO: You'll just end up rewriting the original trait logic here, unless you refactor the system.
		// This is an untested WIP, not scoped for 0.1.0
		public static string LoadoutTable(Agent agent)
		{
			string output = "Loadout Roll Chances: ";
			List<KeyValuePair<string, int>> table = new List<KeyValuePair<string, int>>();
			List<string> baseItemList = agent.customCharacterData.items;
			List<InvItem> baseInventory = new List<InvItem>();

			// Commented out due to incompleteness
			//foreach (string item in baseItemList)
			//{
			//	InvItem invItem = new InvItem();
			//	invItem.invItemName = item;
			//	invItem.SetupDetails(false);
			//}

			//foreach (Slots currentSlot in Enum.GetValues(typeof(Slots)))
			//{
			//	output += (" for Slot: " + currentSlot + Environment.NewLine);

			//	if (agent.HasTrait<Flat_Distribution>())
			//		output += ((int)(100f / (ItemsInSlot(baseInventory, currentSlot) + 1f)) + "% - None" + Environment.NewLine);

			//	foreach (InvItem invItem in baseInventory)
			//	{
			//		int chance = 0;

			//		if (agent.HasTrait<Flat_Distribution>())
			//			chance = (int)(100f / (ItemsInSlot(baseInventory, currentSlot) + 1f));
			//		else if (agent.HasTrait<Scaled_Distribution>())
			//		{
			//			a
			//		}
			//		else if (agent.HasTrait<Upscaled_Distribution>())
			//		{
			//			a
			//		}

			//		output += (chance + "% - " + invItem.invItemName + Environment.NewLine);
			//	}
			//}

			return output;
		}

		public static void SetupLoadout(InvDatabase invDatabase)
		{
			Agent agent = invDatabase.agent;

			if (agent.agentName != VanillaAgents.CustomCharacter ||
				!agent.GetTraits<T_LoadoutLoader>().Any() ||
				agent.isPlayer != 0)
				return;

			logger.LogDebug("SetupLoadout: " + agent.agentName + " (" + agent.agentRealName + ")");
			invDatabase.DontPlayPickupSounds(true);
			T_PocketMoney.AddMoney(agent);
			LoadCustomInventory(invDatabase);
			invDatabase.ChooseArmor();
			invDatabase.ChooseArmorHead();
			invDatabase.ChooseWeapon();
			invDatabase.DontPlayPickupSounds(false);
		}

		// TODO: set invDatabase to a static value, defined on entering this system.
		//	Then it can be called publicly without having to pass it around.
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
				int maximum = GetSlotMax(agent, currentInvSlot);
				bool pockets = currentInvSlot == Slots.Pockets;

				if ((pockets && (
						(agent.HasTrait<Have_Mostly>() && GC.percentChance(25)) ||
						(agent.HasTrait<Have_Not>() && GC.percentChance(50)))) ||
					(!pockets && (
						(agent.HasTrait<Equipment_Enjoyer>() && GC.percentChance(25)) ||
						(agent.HasTrait<Equipment_Virgin>() && GC.percentChance(50)))))
					continue;

				List<InvItem> itemBagForSlot =
					invItemsFromCC
						.Where(ii => GetSlotFromItem(ii) == currentInvSlot)
						.OrderBy(c => CoreTools.random.Next(0, 100))
						.ToList();

				while (ItemsInSlot(agent.agentInvDatabase.InvItemList, currentInvSlot) < maximum &&
					itemBagForSlot.Count() > 0)
				{
					InvItem pickedItem = null;
					bool addItem = false;

					#region Pick Item, roll Chance
					if (agent.HasTrait<Flat_Distribution>())
					{
						pickedItem = itemBagForSlot[CoreTools.random.Next(itemBagForSlot.Count())];

						// By default, a 1/(N+1)% chance to generate no item for the slot.
						int chance = (int)(100f / (itemBagForSlot.Count() + 1f));
						if (GC.percentChance(chance) &&
								((pockets && !agent.HasTrait<Have>()) ||
								(!pockets && !agent.HasTrait<Equipment_Chad>())))
							break;

						addItem = true;
					}
					else if (agent.HasTrait<Scaled_Distribution>() || agent.HasTrait<Upscaled_Distribution>())
					{
						pickedItem = itemBagForSlot[0];

						int chance =
							agent.HasTrait<Scaled_Distribution>()
								? Mathf.Clamp((int)((125f - pickedItem.itemValue) / 2.0f), 1, 100)
							: agent.HasTrait<Upscaled_Distribution>()
								? Mathf.Clamp((int)(pickedItem.itemValue / 3.0f), 1, 100)
									: 0;
						logger.LogDebug("Chance: " + chance + "%");

						if (GC.percentChance(chance) ||
							(pockets && agent.HasTrait<Have>() && itemBagForSlot.Count() is 1) ||
							(!pockets && agent.HasTrait<Equipment_Chad>() && itemBagForSlot.Count() is 1))
							addItem = true;
					}
					#endregion

					string invItemName = pickedItem.invItemName;
					itemBagForSlot.Remove(pickedItem);

					if (!addItem ||
						agent.inventory.InvItemList.Select(ii => ii.invItemName).Contains(invItemName))
						continue;

					#region Set Quantity
					switch (currentInvSlot)
					{
						case Slots.WeaponMelee:

							if (GC.challenges.Contains("InfiniteMeleeDurability"))
								pickedItem.invItemCount = 100;
							else
								pickedItem.invItemCount = int.Parse(invDatabase.rnd.RandomSelect("AgentMeleeDurability", "Others"));

							break;

						case Slots.WeaponRanged:

							if (GC.challenges.Contains("InfiniteAmmo") ||
								(GC.challenges.Contains("InfiniteAmmoNormalWeapons") && !pickedItem.Categories.Contains("NonStandardWeapons") && !pickedItem.Categories.Contains("NonStandardWeapons2")) ||
								agent.warZoneAgent)
								goto default;
							else
							{
								float roll = int.Parse(GC.rnd.RandomSelect("AgentProjectileBullets", "Others")) / 10f;
								pickedItem.invItemCount = (int)(pickedItem.initCountAI * roll);
							}

							break;

						default:
							pickedItem.invItemCount = pickedItem.initCount;
							break;
					}

					#endregion

					//pickedItem.dontAutomaticallySelect = false;
					// These might be an IModifyItems.NPCLoadoutSetup method
					InvItem final = invDatabase.AddItem(pickedItem);

					if (agent.isPlayer == 0) // Free starting ammo for NPCs
					{
						if (final.contents.Contains(VItemName.AmmoCapacityMod))
							final.invItemCount = (int)(final.invItemCount * 1.4f);

						foreach (T_AmmoCap trait in agent.GetTraits<T_AmmoCap>())
							final.invItemCount = (int)(final.invItemCount * trait.AmmoCapMultiplier);
					}
				}
			}
		}
		#region Slot Tools
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
			VItemName.ChloroformHankie,
			VItemName.Fist,
			VItemName.LaserGun,
			VItemName.Money,
			VItemName.ResearchGun,
			VItemName.StickyGlove,
			VItemName.WaterCannon,
		};
		private static int ItemsInSlot(List<InvItem> list, Slots slot) =>
			list.Where(ii => GetSlotFromItem(ii) == slot && ii.invItemName != "" && !(ii.invItemName is null) && !ExemptSlotItems.Contains(ii.invItemName)).Count();
		#endregion
	}

	[HarmonyPatch(typeof(InvDatabase))]
	public static class P_InvDatabase
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(nameof(InvDatabase.FillAgent))]
		private static void FillAgent_Loadout(InvDatabase __instance)
		{
			LoadoutTools.SetupLoadout(__instance);
		}
	}
}