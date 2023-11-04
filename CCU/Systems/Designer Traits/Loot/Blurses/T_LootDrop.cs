using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using System.Collections.Generic;

namespace CCU.Traits.Loot_Drops
{
	public abstract class T_LootDrop : T_DesignerTrait
	{
		public T_LootDrop() : base() { }

		public abstract bool IsUnspillable(InvItem invItem);

		
		

		public static List<string> SoftLockItems = new List<string>()
		{
			VItemName.Key,
			VItemName.KeyCard,
			VItemName.SafeCombination,
			VItemName.MayorBadge,
		};
	}

	[HarmonyPatch(typeof(StatusEffects))]
	class P_StatusEffects
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(nameof(StatusEffects.SetupDeath), new[] { typeof(PlayfieldObject), typeof(bool), typeof(bool) })]
		public static bool SetupDeath_FilterSpillables(StatusEffects __instance)
		{
			Agent agent = __instance.agent;

			foreach (InvItem invItem in agent.inventory.InvItemList)
			{
				foreach (T_LootDrop trait in agent.GetTraits<T_LootDrop>())
					if (trait.IsUnspillable(invItem))
					{
						invItem.doSpill = false;
						invItem.cantDropNPC = true;
					}
			}

			return true;
		}
	}
}