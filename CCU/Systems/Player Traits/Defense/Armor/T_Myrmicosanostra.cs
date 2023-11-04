using BepInEx.Logging;
using CCU.Traits.Inventory;
using HarmonyLib;
using RogueLibsCore;
using UnityEngine;

namespace CCU.Traits.Player.Armor
{
	public abstract class T_Myrmicosanostra : T_PlayerTrait // TODO: IModArmorDepletion, BunnyLibs
	{
		public T_Myrmicosanostra() : base() { }

		
		

		public abstract float ArmorDurabilityChangeMultiplier { get; }
	}

	[HarmonyPatch(typeof(InvDatabase))]
	public static class P_InvDatabase
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(nameof(InvDatabase.DepleteArmor))]
		public static bool DepleteArmor_Modify(InvDatabase __instance, ref int amount)
		{
			float amt = amount;

			if (__instance.agent.HasTrait<Infinite_Armor>())
				amount = 0;
			else
			{
				foreach (T_Myrmicosanostra trait in __instance.agent.GetTraits<T_Myrmicosanostra>())
					amt *= trait.ArmorDurabilityChangeMultiplier;

				amount = (int)Mathf.Max(1f, amt);
			}

			return true;
		}
	}
}