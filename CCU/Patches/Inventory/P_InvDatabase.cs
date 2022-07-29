using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Systems.Containers;
using CCU.Traits.Loadout;
using CCU.Traits.Merchant_Type;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

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

        [HarmonyPrefix, HarmonyPatch(methodName: "Awake")]
		public static bool Awake_Prefix(InvDatabase __instance)
        {
			string objectRealName = __instance.GetComponent<ObjectReal>()?.objectName ?? null;

			if (objectRealName != null && Containers.ContainerObjects.Contains(objectRealName))
				__instance.money = new InvItem();

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

			if (agent.HasTrait<Chunk_Key>() || agent.HasTrait<Chunk_Safe_Combo>())
			{
				for (int i = 0; i < GC.objectRealList.Count; i++)
				{
					ObjectReal objectReal = GC.objectRealList[i];

					if ((GC.levelShape == 0 && (objectReal.owner == agent.ownerID || agent.startingChunkRealDescription == VChunkType.Hotel || agent.ownerID == 99) && objectReal.startingChunk == agent.startingChunk) || (GC.levelShape == 2 && objectReal.startingSector == agent.startingSector))
					{
						if (objectReal.objectName == vObject.Door && objectReal.prisonObject == 0 && agent.HasTrait<Chunk_Key>() && !agent.inventory.HasItem(vItem.Key))
						{
							Door door = (Door)objectReal;

							if (!(door.distributedKey is null) && door.locked)
							{
								Agent prevKeyHolder = door.distributedKey;

								if (!prevKeyHolder.HasTrait<Chunk_Key>())
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
						else if (objectReal.objectName == vObject.Safe && agent.HasTrait<Chunk_Safe_Combo>() && !agent.inventory.HasItem(vItem.SafeCombination))
						{
							Safe safe = (Safe)objectReal;

							if (!(safe.distributedKey is null))
							{
								Agent prevKeyHolder = safe.distributedKey;

								if (!prevKeyHolder.HasTrait<Chunk_Safe_Combo>())
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

			if (__instance.agent.HasTrait<Chunk_Mayor_Badge>() && __instance.agent.startingChunkRealDescription == VChunkType.MayorOffice)
			{
				__instance.AddItem(vItem.MayorsMansionGuestBadge, 1);
				__instance.agent.oma.hasMayorBadge = true;
			}

			return true;
		}

        [HarmonyPrefix, HarmonyPatch(methodName: nameof(InvDatabase.isEmpty))]
		public static bool IsEmpty_Replacement(InvDatabase __instance, ref bool __result)
        {
			for (int i = 0; i < __instance.InvItemList.Count; i++)
            {
				string name = __instance.InvItemList[i].invItemName;

				if (name != null && name != "")
				{
					try
					{
						GC.nameDB.GetName(name, "Item");
						__result = false;
						return false;
					}
					catch { }
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
			MethodInfo slotsFiltered = AccessTools.DeclaredMethod(typeof(P_InvDatabase), nameof(P_InvDatabase.FilteredSlots));

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

		private static List<InvSlot> FilteredSlots(InvDatabase invDatabase) =>
			invDatabase.agent.mainGUI.invInterface.Slots.Where(islot => !GC.nameDB.GetName(islot.item.invItemName, "Item").Contains("E_")).ToList();

	}
}