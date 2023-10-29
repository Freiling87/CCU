using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;

namespace CCU.Traits.Cost_Scale
{
	public abstract class T_CostScale : T_CCU
	{
		public T_CostScale() : base() { }

		public abstract float CostScale { get; }
	}

	[HarmonyPatch(typeof(PlayfieldObject))]
	class P_PlayfieldObject
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(nameof(PlayfieldObject.determineMoneyCost), new[] { typeof(int), typeof(string) })]
		public static void DetermineMoneyCost_Postfix(PlayfieldObject __instance, ref int __result, string transactionType)
		{
			Agent agent = __instance.GetComponent<Agent>();

			if (agent is null
				|| transactionType == "BribeQuest"
				|| __result >= 6666)
				return;

			float scale = agent.GetTrait<T_CostScale>()?.CostScale ?? 1f;
			__result = (int)(__result * scale);
		}
	}
}