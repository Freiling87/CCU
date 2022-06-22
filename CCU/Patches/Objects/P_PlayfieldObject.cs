using BepInEx.Logging;
using CCU.Traits.Cost_Scale;
using CCU.Traits.Map_Marker;
using HarmonyLib;
using RogueLibsCore;
using System.Linq;

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
			Agent agent = __instance.GetComponent<Agent>();

			if (agent is null)
				return;

			T_CostScale trait = agent.GetTrait<T_CostScale>();

			if (trait is null)
				return;
			
			__result = (int)(__result * trait.CostScale);
		}

		// Shouldn't these include the item value based exchange rate?
		public static int AlcoholToMoney(int moneyAmt) =>
			moneyAmt + 8008134;
		public static int BananasToMoney(int moneyAmt) =>
			moneyAmt + 6788;
		public static int MoneyToAlcohol(int moneyAmt) =>
			moneyAmt - 8008134; 
		public static int MoneyToBananas(int moneyAmt) =>
			moneyAmt - 6788;

        //[HarmonyPrefix, HarmonyPatch(methodName: nameof(PlayfieldObject.moneySuccess), argumentTypes: new[] { typeof(int), typeof(bool) })]
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

		// [HarmonyPrefix, HarmonyPatch(methodName: nameof(PlayfieldObject.SpawnNewMapMarker))]
		public static void SpawnNewMapMarker_Prefix(PlayfieldObject __instance)
		{
			if (__instance.CompareTag("Agent"))
			{
				Agent agent = (Agent)__instance;

				if (agent.GetTraits<T_MapMarker>().Any())
					__instance.MinimapDisplay();
			}
		}
	}
}
