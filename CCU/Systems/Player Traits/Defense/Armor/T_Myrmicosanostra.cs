using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using UnityEngine;

namespace CCU.Traits.Player.Armor
{
	public abstract class T_Myrmicosanostra : T_PlayerTrait
	{
		public T_Myrmicosanostra() : base() { }

		public override void OnAdded() { }
		public override void OnRemoved() { }

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

			foreach (T_Myrmicosanostra trait in __instance.agent.GetTraits<T_Myrmicosanostra>())
				amt *= trait.ArmorDurabilityChangeMultiplier;

			amount = (int)Mathf.Max(1f, amt);
			return true;
		}
	}
}
