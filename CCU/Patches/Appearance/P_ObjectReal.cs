using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using RogueLibsCore;
using BepInEx.Logging;

namespace CCU.Patches.Appearance
{
	[HarmonyPatch(declaringType:typeof(ObjectReal))]
	public static class P_ObjectReal
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPatch(methodName: nameof(ObjectReal.noLight), MethodType.Getter)]
		public static bool noLight_Prefix(ref bool ___result)
		{
			if (GC.challenges.Contains(CMutators.NoObjectLights))
			{
				___result = true;
				return false;
			}

			return true;
		}
	}
}
