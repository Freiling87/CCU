using BepInEx.Logging;
using BunnyLibs;
using HarmonyLib;

namespace CCU.Patches.Interface
{
	[HarmonyPatch(typeof(StatusEffectDisplay))]
	class P_StatusEffectDisplay
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		/// <summary>
		/// Filter CCU traits from lower-left display when possessing
		/// </summary>
		/// <param name="myStatusEffect"></param>
		/// <param name="myTrait"></param>
		/// <returns></returns>
		[HarmonyPrefix, HarmonyPatch(nameof(StatusEffectDisplay.AddDisplayPiece), argumentTypes: new[] { typeof(StatusEffect), typeof(Trait) })]
		public static bool AddDisplayPiece_Prefix(Trait myTrait)
		{
			return T_CCU.IsPlayerTrait(myTrait);
		}
	}
}