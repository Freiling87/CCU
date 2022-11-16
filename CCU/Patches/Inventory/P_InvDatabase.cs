using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Mutators.Laws;
using CCU.Patches.Agents;
using CCU.Systems.Containers;
using CCU.Systems.Investigateables;
using CCU.Traits.Behavior;
using CCU.Traits.Loadout;
using CCU.Traits.Loadout_Chunk_Items;
using CCU.Traits.Loadout_Money;
using CCU.Traits.Merchant_Stock;
using CCU.Traits.Merchant_Type;
using CCU.Traits.Passive;
using CCU.Traits.Player;
using CCU.Traits.Player.Ammo_Cap;
using CCU.Traits.Player.Armor_Durability;
using CCU.Traits.Player.Melee_Attack_Speed;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;
using UnityEngine;

namespace CCU.Patches.Inventory
{
    [HarmonyPatch(declaringType:typeof(InvDatabase))]
	public static class P_InvDatabase
	{
		// TODO: AddItemReal is private and used in AddRandItem as well as fillAgent

		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static FieldInfo nameProviderField = AccessTools.Field(typeof(RogueLibs), "NameProvider");
		public static CustomNameProvider nameProvider = (CustomNameProvider)nameProviderField.GetValue(null);

		//[HarmonyPrefix, HarmonyPatch(methodName: nameof(InvDatabase.AddItem), argumentTypes: new[] {typeof(string), typeof(int), typeof(List<string>), typeof(List<int>), typeof(List<int>), typeof(int), typeof(bool), typeof(bool), typeof(int), typeof(int), typeof(bool), typeof(string), typeof(bool), typeof(int), typeof(bool), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(bool)})]
		public static bool AddItem_Prefix(string itemName, string itemType, InvDatabase __instance)
		{
			logger.LogDebug("ItemName: " + itemName);
			logger.LogDebug("ItemType: " + itemType);

			return true;
		}

        [HarmonyPostfix, HarmonyPatch(methodName: nameof(InvDatabase.AddItem), argumentTypes: new[] { typeof(string), typeof(int), typeof(List<string>), typeof(List<int>), typeof(List<int>), typeof(int), typeof(bool), typeof(bool), typeof(int), typeof(int), typeof(bool), typeof(string), typeof(bool), typeof(int), typeof(bool), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(bool) })]
		public static void AddItem_Postfix(InvDatabase __instance, ref InvItem __result)
		{
			ModItemByTraits(__instance, ref __result);
		}

        [HarmonyPostfix, HarmonyPatch(methodName: nameof(InvDatabase.AddItemAtEmptySlot), argumentTypes: new[] { typeof(InvItem), typeof(bool), typeof(bool), typeof(int), typeof(int) })]		
		public static void AddItemAtEmptySlot_Postfix(InvDatabase __instance, ref InvItem item)
		{
			ModItemByTraits(__instance, ref item);
		}

		private static void ModItemByTraits(InvDatabase invDatabase, ref InvItem invItem)
		{
			if (!(invDatabase.agent is null))
            {
                if (invItem.itemType == "WeaponProjectile")
                {
                    T_AmmoCap.RecalculateMaxAmmo(invDatabase.agent, invItem, false);

                    if (invDatabase.agent.HasTrait<Pants_on_Autofire>())
                        invItem.rapidFire = true;
                }
				else if (invItem.itemType == "WeaponMelee")
                {
					if (invDatabase.agent.HasTrait<Melee_Maniac2>())
						invItem.rapidFire = true;
                }
            }
        }

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(InvDatabase.AddRandItem), argumentTypes: new[] { typeof(string) })]
		public static bool AddRandItem_Prefix(string itemNum, InvDatabase __instance, ref InvItem __result)
		{
			if (__instance.agent is null)
				return true;

			if (__instance.agent.GetTraits<T_MerchantType>().Any())
			{
				T_MerchantType trait = __instance.agent.GetTrait<T_MerchantType>();
				string rName = trait.DisplayName;

				if (__instance.CompareTag("SpecialInvDatabase") && !(rName is null))
				{
					string itemName;
					int num = 0;
					bool foundItem = false;

					do
					{
						try
						{
							itemName = __instance.rnd.RandomSelect(rName, "Items");
							itemName = __instance.SwapWeaponTypes(itemName);

							if (itemName != "")
								foundItem = true;
						}
						catch
						{
							itemName = "Empty";
						}

						foreach (InvItem invItem in __instance.InvItemList)
							if (invItem.invItemName == itemName && !invItem.canRepeatInShop)
								foundItem = false;

						if (itemName == "FreeItemVoucher")
							foundItem = false;

						num++;
					}
					while (!foundItem && num < 100);

					if (num == 100)
						itemName = "Empty";

					if (itemName != "Empty" && itemName != "")
					{
						MethodInfo addItemReal = AccessTools.DeclaredMethod(typeof(InvDatabase), "AddItemReal", new Type[1] { typeof(string) });
						__result = addItemReal.GetMethodWithoutOverrides<Func<string, InvItem>>(__instance).Invoke(itemName);

						return false;
					}

					return false;
				}
			}

			return true;
		}

        [HarmonyPrefix, HarmonyPatch(methodName: nameof(InvDatabase.ChooseWeapon), argumentTypes: new[] { typeof(bool) })]
		public static bool ChooseWeapon_Prefix(InvDatabase __instance)
        {
			Agent agent = __instance.agent;

			if (!agent.inCombat && !agent.GetOrAddHook<P_Agent_Hook>().weaponChosen &&
				(agent.HasTrait<Concealed_Carrier>() || 
				(GC.challenges.Contains(nameof(No_Open_Carry)) && !agent.HasTrait<Outlaw>())))
            {
				ConcealWeapon(__instance);
				agent.GetOrAddHook<P_Agent_Hook>().weaponChosen = true;
				return false;
            }

			return true;
        }
		public static async void ConcealWeapon(InvDatabase invDatabase)
		{
			if (invDatabase.agent.HasTrait(VanillaTraits.NimbleFingers))
				await Task.Delay(500);
			else if (invDatabase.agent.HasTrait(VanillaTraits.PoorHandEyeCoordination))
				await Task.Delay(2000);
			else
				await Task.Delay(1000);

			invDatabase.EquipWeapon(invDatabase.fist);
		}

		[HarmonyPrefix,HarmonyPatch(methodName: nameof(InvDatabase.DepleteArmor))]
		public static bool DepleteArmor_Modify(InvDatabase __instance, ref int amount)
        {
			float amt = amount;

			foreach (T_Myrmicosanostra trait in __instance.agent.GetTraits<T_Myrmicosanostra>())
            {
				logger.LogDebug(trait.TextName);
				amt *= trait.ArmorDurabilityChangeMultiplier;
			}

			amount = (int)Mathf.Max(1f, amt);
			return true;
        }

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(InvDatabase.FillAgent))]
		private static void FillAgent_Loadout(InvDatabase __instance)
		{
			LoadoutTools.SetupLoadout(__instance);
		}

        [HarmonyPostfix,HarmonyPatch(methodName: nameof(InvDatabase.FindMoneyAmt))]
		private static void FindMoneyAmount_Postfix(InvDatabase __instance, ref int __result)
        {
			if (!__instance.CompareTag("Agent"))
				return;

			Agent agent = __instance.agent;

			if (agent.HasTrait<Broke>())
            {
				__result = 0;
				return;
			}

			float amt = agent.HasTrait<Zillionaire>()
				? 1000f
				: __result;

			// Kept separate to allow multiplication
			if (agent.HasTrait<Rich>())
				amt *= 3f;
			if (agent.HasTrait<Wealthy>())
				amt *= 4f;
			if (agent.HasTrait<Poor>())
				amt /= 2f;

			__result = (int)amt;
		}

		[HarmonyPrefix, HarmonyPatch(methodName: "Awake")]
		public static bool Awake_Prefix(InvDatabase __instance)
		{
			string objectRealName = __instance.GetComponent<ObjectReal>()?.objectName ?? null;

			if (Containers.IsContainer(objectRealName))
				__instance.money = new InvItem();

			return true;
		}

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(InvDatabase.FillSpecialInv))]
		public static bool FillSpecialInv_Prefix(InvDatabase __instance)
        {
			Agent agent = __instance.agent;
			List<string> inventory = new List<string>();
			List<string> finalInventory = new List<string>();
            System.Random rnd = new System.Random();
			int attempts = 0;
			bool forceDuplicates = false;

			if (agent is null || agent.agentName != VanillaAgents.CustomCharacter || __instance.filledSpecialInv)
				return true;

			List<T_MerchantType> traits = agent.GetTraits<T_MerchantType>().ToList();

			foreach (T_MerchantType trait in traits)
				foreach (KeyValuePair<string, int> item in trait.MerchantInventory)
                {
					// Gives priority to Insider traits
					if (trait.MerchantInventory.Count == 1)
					{
						finalInventory.Add(trait.MerchantInventory[0].Key);
					}
					else
					{
						for (int i = 0; i < item.Value; i++) // Qty
							inventory.Add(__instance.SwapWeaponTypes(item.Key)); // Name
					}
				}

		redo: // Yeah redo this whole damn thing
			while (inventory.Any() && finalInventory.Count < 5 && attempts < 100)
			{
				attempts++;

				int bagPickedIndex = rnd.Next(0, Math.Max(0, inventory.Count - 1));
				string bagPickedItem = inventory[bagPickedIndex];

				if (forceDuplicates || !finalInventory.Contains(bagPickedItem) || agent.HasTrait<Clearancer>() )
                {
					finalInventory.Add(bagPickedItem);
					inventory.RemoveAt(bagPickedIndex);
					attempts = 0;
				}
            }

			if (inventory.Any() && finalInventory.Count < 5)
			{
				forceDuplicates = true;
				attempts = 0;
				goto redo;
			}

			foreach (string item in finalInventory) 
			{
				MethodInfo AddItemReal = AccessTools.DeclaredMethod(typeof(InvDatabase), "AddItemReal");
				InvItem invItem = null;

				try
				{
					string rListItem = __instance.rnd.RandomSelect(item, "Items");
					invItem = AddItemReal.GetMethodWithoutOverrides<Func<string, InvItem>>(__instance).Invoke(rListItem);

					if (agent.HasTrait<Clearancer>())
						invItem.canRepeatInShop = true;
				}
                catch
                {
					invItem = AddItemReal.GetMethodWithoutOverrides<Func<string, InvItem>>(__instance).Invoke(item);

					if (agent.HasTrait<Clearancer>())
						invItem.canRepeatInShop = true;
				}

				foreach (T_MerchantStock trait in agent.GetTraits<T_MerchantStock>())
					trait.OnAddItem(ref invItem);

				// This is apparently done automatically for Durability items
				if (T_MerchantStock.QuantityTypes.Contains(invItem.itemType))
					T_MerchantStock.ShopPrice(ref invItem);
			}

			__instance.filledSpecialInv = true;
			return false;
        }

		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(InvDatabase.FillAgent))]
		private static IEnumerable<CodeInstruction> FillAgent_LoadoutBadge(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo mayorBadgeMagicString = AccessTools.DeclaredMethod(typeof(P_InvDatabase), nameof(P_InvDatabase.MayorBadgeMagicString));
			FieldInfo wontFlee = AccessTools.DeclaredField(typeof(Agent), nameof(Agent.wontFlee));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Stfld, wontFlee),
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld),
				},
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Callvirt),
				},
				postfixInstructionSequence: new List<CodeInstruction>
                {
					new CodeInstruction(OpCodes.Ldstr, "Clerk"),
                },
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, mayorBadgeMagicString)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		private static string MayorBadgeMagicString(Agent agent) =>
			agent.HasTrait<Chunk_Mayor_Badge>()
				? "Clerk"
				: agent.name;

		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(InvDatabase.FillChest), argumentTypes: new[] { typeof(bool) })]
		private static IEnumerable<CodeInstruction> FillChest_FilterNotes_EVS(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo extraVarString = AccessTools.DeclaredField(typeof(PlayfieldObject), nameof(PlayfieldObject.extraVarString));
			MethodInfo magicVarString = AccessTools.DeclaredMethod(typeof(P_InvDatabase), nameof(P_InvDatabase.MagicVarString));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 5,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, extraVarString),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, magicVarString),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		public static string MagicVarString(string vanilla) =>
			Investigateables.IsInvestigationString(vanilla)
				? ""
				: vanilla;

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(InvDatabase.isEmpty))]
		public static bool IsEmpty_Replacement(InvDatabase __instance, ref bool __result)
        {
			for (int i = 0; i < __instance.InvItemList.Count; i++)
            {
				InvItem invItem = __instance.InvItemList[i];

				if (!GC.nameDB.GetName(invItem.invItemName, "Item").Contains("E_"))
				{
					__result = false;
					return false;
				}
			}

			__result = true;
			return false;
		}

		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(InvDatabase.TakeAll))]
		private static IEnumerable<CodeInstruction> TakeAll_ExcludeNotes(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo slots = AccessTools.DeclaredField(typeof(InvInterface), nameof(InvInterface.Slots));
			MethodInfo slotsFiltered = AccessTools.DeclaredMethod(typeof(Investigateables), nameof(Investigateables.FilteredSlots));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld), 
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Ldfld, slots),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Call, slotsFiltered),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}
}