using BepInEx.Logging;
using HarmonyLib;
using System.Collections.Generic;

namespace CCU.Patches.Objects
{
	[HarmonyPatch(typeof(SlimeBarrel))]
	public static class CustomSlimeBarrel
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static bool DestroyMe_Prefix(PlayfieldObject damagerObject, SlimeBarrel __instance)
		{
			string extraVarString = __instance.extraVarString;

			if (extraVarString is null ||
				extraVarString == "" ||
				extraVarString == VExplosionType.Slime)
				return true;

			if (SlimeBarrelExplosionTypes.Contains(extraVarString))
				GC.spawnerMain.SpawnExplosion(damagerObject, __instance.tr.position, extraVarString, false, -1, false, __instance.FindMustSpawnExplosionOnClients(damagerObject));

			else if (extraVarString == "OilSpill")
			{
				// See if you can use Slime Explosion particle effect and tint it black?
			}

			return false;
		}

		private static readonly List<string> SlimeBarrelExplosionTypes = new List<string>()
		{
			VExplosionType.Big,
			VExplosionType.Dizzy,
			VExplosionType.EMP,
			VExplosionType.FireBomb,
			VExplosionType.Huge,
			//VExplosionType.MindControl,
			VExplosionType.NoiseOnly,
			VExplosionType.Normal,
			VExplosionType.Ooze,
			VExplosionType.Ridiculous,
			VExplosionType.Slime,
			VExplosionType.Stomp,
			VExplosionType.Warp,
			VExplosionType.Water,
		};
	}
}