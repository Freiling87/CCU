using BepInEx.Logging;
using BunnyLibs;
using CCU.Traits.Loadout;
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
}