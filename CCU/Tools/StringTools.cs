using BepInEx.Logging;
using BunnyLibs;
using System;
using UnityEngine;

namespace CCU.Systems.Tools
{
	public static class StringTools
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		private static GameController GC => GameController.gameController;

		private static readonly TextGenerator TextGenerator = new TextGenerator();
		private static readonly TextGenerationSettings MunroSettings = new TextGenerationSettings() { font = GC.munroFont };

		public static int MunroTextWidth(string text) =>
			(int)TextGenerator.GetPreferredWidth(text, MunroSettings);

		public static string PadHorizontal(string text, int desiredPixelWidth)
		{
			// Tab = 20 px wide
			int textWidth = MunroTextWidth(text);
			int tabs = (desiredPixelWidth - textWidth) / 20;
			string tabsString = new string('\t', tabs);
			text += tabsString;
			return text;
		}

		public static string PadVertical(string text, int desiredHeight)
		{
			int lineCount = text.Split(new[] { '\n' }, StringSplitOptions.None).Length - 1;
			int breaks = desiredHeight - lineCount;
			string breaksString = new string('\n', breaks); // NRE
			text += breaksString;
			return text;
		}
	}
}