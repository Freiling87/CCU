using BepInEx.Logging;
using CCU.Traits.Loadout;
using HarmonyLib;
using RogueLibsCore;
using System.Linq;

namespace CCU.Traits.Loadout_Money
{
	public abstract class T_PocketMoney : T_Loadout
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();

		public T_PocketMoney() : base() { }

		public abstract int MoneyAmount { get; }

		public override void OnAdded() { }
		public override void OnRemoved() { }

		public static void AddMoney(Agent agent)
		{
			if (!agent.GetTraits<T_PocketMoney>().Any() ||
				(agent.HasTrait<Bankrupt_25>() && GC.percentChance(25)) ||
				(agent.HasTrait<Bankrupt_50>() && GC.percentChance(50)) ||
				(agent.HasTrait<Bankrupt_75>() && GC.percentChance(75)))
				return;

			int moneyAmt = agent.GetTraits<T_PocketMoney>().Sum(t => t.MoneyAmount);

			if (!agent.inventory.HasItem(VItemName.Money))
			{
				InvItem money = new InvItem();
				money.invItemName = VItemName.Money;
				money.SetupDetails(false);
				agent.inventory.AddItem(money);
			}

			agent.inventory.money.invItemCount = moneyAmt;
			agent.mustSpillMoney = true;
		}
	}

	[HarmonyPatch(typeof(InvDatabase))]
	public static class P_InvDatabase
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(nameof(InvDatabase.FindMoneyAmt))]
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
	}
}