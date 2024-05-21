using BepInEx;
using BepInEx.Logging;
using BTHarmonyUtils;
using HarmonyLib;
using RogueLibsCore;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace CCU
{
	[BepInPlugin(pluginGUID, pluginName, pluginVersion)]
	[BepInDependency(RogueLibs.GUID, RogueLibs.CompiledVersion)]
    [BepInDependency(BTHarmonyUtilsPlugin.pluginGuid, BTHarmonyUtilsPlugin.minCompatibleVersion)]
	public class Core : BaseUnityPlugin
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		private static GameController GC => GameController.gameController;

		public const string pluginGUID = "Freiling87.streetsofrogue.CCU";
		public const string pluginName = "CCU " + (designerEdition ? "[D]" : "[P]");
		public const string pluginVersion = "2.0.0";
		public const string subVersion = "a";
		public static Sprite CCULogoSprite;

		public const bool designerEdition = true;
		public const bool developerEdition = true;
		public const bool debugMode = true;

		public void Awake()
		{
			Harmony harmony = new Harmony(pluginGUID);
			harmony.PatchAll();
			PatcherUtils.PatchAll(harmony);
			RogueLibs.LoadFromAssembly();
			RogueLibs.CreateVersionText(pluginGUID, pluginName + " v" + pluginVersion + subVersion);
			CCULogoSprite = RogueLibs.CreateCustomSprite(nameof(Properties.Resources.CCU_160x160), SpriteScope.Interface, Properties.Resources.CCU_160x160).Sprite;
		}

		public static void LogMethodCall([CallerMemberName] string callerName = "") =>
			logger.LogDebug(callerName + ": Method Call");

		public const string
			CCUBlockTag = "[CCU]";
	}
}
