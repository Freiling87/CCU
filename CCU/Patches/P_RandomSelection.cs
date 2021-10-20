using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;

namespace CCU.Patches
{
	[HarmonyPatch(declaringType: typeof(RandomSelection))]
	class P_RandomSelection
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(RandomSelection.CreateRandomList), argumentTypes: new[] { typeof(string), typeof(string), typeof(string) })]
		public static void CreateRandomList_Prefix(string rName, string rCategory, string rObjectType)
		{
			Core.LogMethodCall();
			logger.LogDebug("\trName: " + rName);
			logger.LogDebug("\trCategory: " + rCategory);
			logger.LogDebug("\trObjectType: " + rObjectType);
		}

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(RandomSelection.CreateRandomElement), argumentTypes: new[] { typeof(RandomList), typeof(string), typeof(int) })]
		public static void CreateRandomElement_Prefix(RandomList rList, string rName, int rChance)
		{
			Core.LogMethodCall();
			logger.LogDebug("\trList: " + rList.rName);
			logger.LogDebug("\trName: " + rName);
			logger.LogDebug("\trChance: " + rChance);
		}
	}
}
