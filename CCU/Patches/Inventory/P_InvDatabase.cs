using BepInEx.Logging;
using CCU.Traits.Loadout;
using CCU.Traits.Merchant_Type;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

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
					string text;
					int num = 0;
					bool foundItem = false;

					do
					{
						try
						{
							text = __instance.rnd.RandomSelect(rName, "Items");
							text = __instance.SwapWeaponTypes(text);

							if (text != "")
								foundItem = true;
						}
						catch
						{
							text = "Empty";
						}

						foreach (InvItem invItem in __instance.InvItemList)
							if (invItem.invItemName == text && !invItem.canRepeatInShop)
								foundItem = false;

						if (text == "FreeItemVoucher")
							foundItem = false;

						num++;
					}
					while (!foundItem && num < 100);

					if (num == 100)
						text = "Empty";

					if (text != "Empty" && text != "")
					{
						MethodInfo addItemReal = AccessTools.DeclaredMethod(typeof(InvDatabase), "AddItemReal", new Type[1] { typeof(string) });
						__result = addItemReal.GetMethodWithoutOverrides<Func<string, InvItem>>(__instance).Invoke(text);

						return false;
					}

					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Loadouts
		/// </summary>
		/// <param name="__instance"></param>
		/// <returns></returns>
		[HarmonyPrefix, HarmonyPatch(methodName: nameof(InvDatabase.FillAgent))] 
		public static bool FillAgent_Prefix(InvDatabase __instance)
		{
			Agent agent = __instance.agent;

			if (agent.agentName != VanillaAgents.CustomCharacter)
				return true;

			if (agent.HasTrait<Manager_Key>() || agent.HasTrait<Manager_Safe_Combo>())
			{
				for (int i = 0; i < GC.objectRealList.Count; i++)
				{
					ObjectReal objectReal = GC.objectRealList[i];

					if ((GC.levelShape == 0 && (objectReal.owner == agent.ownerID || agent.startingChunkRealDescription == VChunkType.Hotel || agent.ownerID == 99) && objectReal.startingChunk == agent.startingChunk) || (GC.levelShape == 2 && objectReal.startingSector == agent.startingSector))
					{
						if (objectReal.objectName == vObject.Door && objectReal.prisonObject == 0 && agent.HasTrait<Manager_Key>() && !agent.inventory.HasItem(vItem.Key))
						{
							Door door = (Door)objectReal;

							if (!(door.distributedKey is null) && door.locked)
							{
								Agent prevKeyHolder = door.distributedKey;

								if (!prevKeyHolder.HasTrait<Manager_Key>())
								{
									InvItem key = prevKeyHolder.inventory.FindItem(vItem.Key);
									prevKeyHolder.inventory.SubtractFromItemCount(key, 1);
									prevKeyHolder.oma.hasKey = false;
									door.distributedKey = null;
								}
							}

							if (door.distributedKey is null && door.locked)
							{
								InvItem invItem = __instance.AddItem(vItem.Key, 1);
								invItem.specificChunk = door.startingChunk;
								invItem.specificSector = door.startingSector;
								invItem.chunks.Add(door.startingChunk);
								invItem.sectors.Add(door.startingChunk);
								string doorDescription = door.startingChunkRealDescription;

								if (doorDescription == VChunkType.Generic)
									doorDescription = "GuardPost";

								invItem.contents.Add(doorDescription);
								door.distributedKey = agent;
								agent.oma.hasKey = true;
							}
						}
						else if (objectReal.objectName == vObject.Safe && agent.HasTrait<Manager_Safe_Combo>() && !agent.inventory.HasItem(vItem.SafeCombination))
						{
							Safe safe = (Safe)objectReal;

							if (!(safe.distributedKey is null))
							{
								Agent prevKeyHolder = safe.distributedKey;

								if (!prevKeyHolder.HasTrait<Manager_Safe_Combo>())
								{
									InvItem safeCombo = prevKeyHolder.inventory.FindItem(vItem.SafeCombination);
									prevKeyHolder.inventory.SubtractFromItemCount(safeCombo, 1);
									prevKeyHolder.oma.hasSafeCombination = false;
									safe.distributedKey = null;
								}
							}

							if (safe.distributedKey is null)
							{
								InvItem safeCombo = __instance.AddItem(vItem.SafeCombination, 1);
								safeCombo.specificChunk = safe.startingChunk;
								safeCombo.specificSector = safe.startingSector;
								safeCombo.chunks.Add(safe.startingChunk);
								safeCombo.sectors.Add(safe.startingChunk);
								safeCombo.contents.Add(safe.startingChunkRealDescription);
								safe.distributedKey = agent;
								agent.oma.hasSafeCombination = true;
							}
						}
					}
				}
			}

			if (__instance.agent.HasTrait<Manager_Mayor_Badge>() && __instance.agent.startingChunkRealDescription == VChunkType.MayorOffice)
			{
				__instance.AddItem(vItem.MayorsMansionGuestBadge, 1);
				__instance.agent.oma.hasMayorBadge = true;
			}

			return true;
		}
	}
}