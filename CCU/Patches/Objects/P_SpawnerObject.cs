using BepInEx.Logging;
using BunnyLibs;
using HarmonyLib;

namespace CCU.Patches.Objects
{
	[HarmonyPatch(typeof(SpawnerObject))]
	public static class P_SpawnerObject
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

	}
}
