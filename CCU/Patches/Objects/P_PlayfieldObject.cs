using BepInEx.Logging;
using CCU.Traits;
using CCU.Traits.Cost;
using HarmonyLib;
using RogueLibsCore;

namespace CCU.Patches.Objects
{
    [HarmonyPatch(declaringType: typeof(PlayfieldObject))]
	class P_PlayfieldObject
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(PlayfieldObject.determineMoneyCost), argumentTypes: new[] { typeof(int), typeof(string) })]
		public static void DetermineMoneyCost_Postfix(int moneyAmt, string transactionType, PlayfieldObject __instance, ref int __result)
		{
			// Need initial Cost → Item conversion

			Agent agent = __instance.GetComponent<Agent>();
			bool bananas = moneyAmt >= 6789 && moneyAmt < 6825;
			bool alcohol = moneyAmt >= 8008135 && moneyAmt < 8008171;

			if (alcohol)
				moneyAmt = MoneyToAlcohol(moneyAmt);
			else if (bananas)
				moneyAmt = MoneyToBananas(moneyAmt);

			if (agent.HasTrait<CostLess>())
				__result = (int)((float)__result * 0.5f);
			else if (agent.HasTrait<CostMore>())
				__result = (int)((float)__result * 1.5f);

			if (alcohol)
				moneyAmt = AlcoholToMoney(moneyAmt);
			else if (bananas)
				moneyAmt = BananasToMoney(moneyAmt);

			__result = moneyAmt;
		}

		public static int AlcoholToMoney(int moneyAmt) =>
			moneyAmt + 8008134;
		public static int BananasToMoney(int moneyAmt) =>
			moneyAmt + 6788;
		public static int MoneyToAlcohol(int moneyAmt) =>
			moneyAmt - 8008134;
		public static int MoneyToBananas(int moneyAmt) =>
			moneyAmt - 6788;

        [HarmonyPrefix, HarmonyPatch(methodName: nameof(PlayfieldObject.moneySuccess), argumentTypes: new[] { typeof(int), typeof(bool) })]
        public static bool MoneySuccess_Prefix(int moneyAmt, bool countTowardStats, PlayfieldObject __instance, ref bool __result)
        {
			if (moneyAmt >= 6789 && moneyAmt < 6825) // 1-36 banaan
			{
				int bananaCost = moneyAmt - 6788;
				InvItem banana = null;

				foreach (InvItem invItem in __instance.interactingAgent.inventory.InvItemList)
					if (invItem.invItemName == vItem.Banana)
					{
						banana = invItem;
						break;
					}
				
				if (banana.invItemCount < bananaCost)
				{
					__instance.interactingAgent.SayDialogue("NeedBananas");
					__result = false;
					return false;
				}
				
				__instance.interactingAgent.inventory.SubtractFromItemCount(banana, bananaCost);
				__result = true;
				return false;
			}
			else if (moneyAmt >= 8008135 && moneyAmt < 8008171) // 1-36 drinkies
            {
				int drinkCost = moneyAmt - 6788;
				InvItem beer = null;
				InvItem whiskey = null;

				foreach (InvItem invItem in __instance.interactingAgent.inventory.InvItemList)
				{
					if (invItem.invItemName == vItem.Beer)
						beer = invItem;
					else if (invItem.invItemName == vItem.Whiskey)
						whiskey = invItem;

					if (beer != null && whiskey != null)
						break;
				}

				if (beer.invItemCount >= drinkCost)
				{
					__instance.interactingAgent.inventory.SubtractFromItemCount(beer, drinkCost);
					goto Success;
				}
				else if (beer.invItemCount + whiskey.invItemCount >= drinkCost)
                {
					drinkCost -= beer.invItemCount;
					__instance.interactingAgent.inventory.SubtractFromItemCount(beer, beer.invItemCount);
					__instance.interactingAgent.inventory.SubtractFromItemCount(whiskey, drinkCost);
					goto Success;
				}
				else if (whiskey.invItemCount >= drinkCost)
				{
					__instance.interactingAgent.inventory.SubtractFromItemCount(whiskey, drinkCost);
					goto Success;
				}

				__instance.interactingAgent.SayDialogue("NeedCash");
				__result = false;
				return false;

			Success:
				__result = true;
				return false;
			}

			return true;
        }

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(PlayfieldObject.SpawnNewMapMarker))]
		public static void SpawnNewMapMarker_Prefix(PlayfieldObject __instance)
		{
			if (__instance.CompareTag("Agent"))
				if (TraitManager.HasTraitFromList((Agent)__instance, TraitManager.MapMarkerTraits))
					__instance.MinimapDisplay();
		}
	}
}
