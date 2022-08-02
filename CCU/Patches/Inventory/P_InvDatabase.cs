using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Systems.Containers;
using CCU.Systems.Investigateables;
using CCU.Traits.Loadout;
using CCU.Traits.Merchant_Type;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections.Generic;
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
		private static IEnumerable<CodeInstruction> FillChest_FilterNotes_EVS3(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo extraVarString3 = AccessTools.DeclaredField(typeof(PlayfieldObject), nameof(PlayfieldObject.extraVarString3));
			MethodInfo magicVarString = AccessTools.DeclaredMethod(typeof(P_InvDatabase), nameof(P_InvDatabase.magicVarString));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 5,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, extraVarString3),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, magicVarString),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		// Returns a false negative if Note detected
		public static string magicVarString(string vanilla) =>
			Investigateables.IsInvestigationString(vanilla)
				? ""
				: vanilla;

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(InvDatabase.isEmpty))]
		public static bool IsEmpty_Replacement(InvDatabase __instance, ref bool __result)
        {
			__result = true;

			for (int i = 0; i < __instance.InvItemList.Count; i++)
            {
				string name = __instance.InvItemList[i].invItemName;

				if (name != null && name != "" && !Investigateables.IsInvestigationString(name))
					__result = false;
			}

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