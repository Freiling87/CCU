using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Localization;
using CCU.Mutators.Laws;
using CCU.Traits;
using CCU.Traits.Behavior;
using CCU.Traits.Inventory;
using CCU.Traits.Loadout;
using CCU.Traits.Loadout_Chunk_Items;
using CCU.Traits.Loadout_Money;
using CCU.Traits.Merchant_Stock;
using CCU.Traits.Merchant_Type;
using CCU.Traits.Passive;
using CCU.Traits.Player.Armor;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
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

		/// <summary>
		/// This SHOULD cover all non-Loadout item additions, like the 3 item slots in the editor.
		/// </summary>
		/// <param name="__instance"></param>
		/// <param name="__result"></param>
		[HarmonyPostfix, HarmonyPatch(methodName: "AddItemReal")]
		public static void AddItemReal_InitSetup(InvDatabase __instance, ref InvItem __result)
		{
			if (__instance.agent is null)
				return;
			
			ModifyItemHelper.SetupItem(__instance.agent, __result);

			// Free NPC ammo
			if (__instance.agent.isPlayer == 0)
			{
				float ratio = (float)__result.maxAmmo / (float)__result.initCount;
				__result.invItemCount = (int)(__result.invItemCount * ratio);
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
			__instance.StartCoroutine(ConcealWeapon(__instance));
			return true;
        }
		public static IEnumerator ConcealWeapon(InvDatabase invDatabase)
		{
			Agent agent = invDatabase.agent;

			if (agent.isPlayer == 0 &&
				!agent.inCombat &&
					(agent.HasTrait<Concealed_Carrier>() ||
					(GC.challenges.Contains(nameof(No_Open_Carry)) && !agent.HasTrait<Outlaw>())))
			{
				if (invDatabase.agent.HasTrait(VanillaTraits.NimbleFingers))
					yield return new WaitForSeconds(1.00f);
				else if (invDatabase.agent.HasTrait(VanillaTraits.PoorHandEyeCoordination))
					yield return new WaitForSeconds(3.00f);
				else
					yield return new WaitForSeconds(2.00f);

				invDatabase.EquipWeapon(invDatabase.fist);
			}
		}

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(InvDatabase.DepleteArmor))]
		public static bool DepleteArmor_Modify(InvDatabase __instance, ref int amount)
        {
			if (__instance.agent.HasTrait<Infinite_Armor>())
				return false;

			float amt = amount;

			foreach (T_Myrmicosanostra trait in __instance.agent.GetTraits<T_Myrmicosanostra>())
				amt *= trait.ArmorDurabilityChangeMultiplier;

			amount = (int)Mathf.Max(1f, amt);
			return true;
        }

        [HarmonyPrefix, HarmonyPatch(methodName: nameof(InvDatabase.DepleteMelee), argumentTypes: new[] { typeof(int), typeof(InvItem) })]
		public static bool DepleteMelee_Prefix(InvDatabase __instance, int amount)
        {
			if (__instance.agent.HasTrait<Infinite_Melee>())
				return false;

			return true;
        }

        [HarmonyPrefix, HarmonyPatch(methodName: nameof(InvDatabase.EquipWeapon), argumentTypes: new[] { typeof(InvItem), typeof(bool) })]
		public static bool EquipWeapon_Prefix(InvDatabase __instance, InvItem item, ref bool sfx)
        {
			if (__instance.agent.isPlayer == 0 && item.invItemName == vItem.Fist)
				sfx = false;

			return true;
        }

		// TODO: Move to T_Loadout
		[HarmonyPostfix, HarmonyPatch(methodName: nameof(InvDatabase.FillAgent))]
		private static void FillAgent_Loadout(InvDatabase __instance)
		{
			LoadoutTools.SetupLoadout(__instance);
		}

        [HarmonyPrefix, HarmonyPatch(methodName: nameof(InvDatabase.FindMoneyAmt))]
		private static bool FindMoneyAmount_Prefix(InvDatabase __instance, ref int __result)
        {
			if (__instance.CompareTag("Agent") && (
				__instance.agent.GetTraits<T_PocketMoney>().Any() || 
				(__instance.agent.HasTrait<Bankrupt_25>() && GC.percentChance(25)) ||
				(__instance.agent.HasTrait<Bankrupt_50>() && GC.percentChance(50)) ||
				(__instance.agent.HasTrait<Bankrupt_75>() && GC.percentChance(75))))
			{
				__result = 0;
				return false;
            }

			return true;
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
	}
}