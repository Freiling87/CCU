using BepInEx.Logging;
using CCU.Systems.Tools;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace CCU.Systems.Mutator_Configurator
{
	internal class ChoiceList : SubMenu
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		private static GameController GC => GameController.gameController;

		public override string Instructions =>
			"<b><color=yellow>" + MutatorConfigurator.CurrentHead.DisplayHeader + " " + DisplayHeader + "</color></b>\n" +
				(MaximumSelections == MinimumSelections
				? "Select " + MaximumSelections + " option(s)."
				: "Select " + MinimumSelections + "-" + MaximumSelections + " options.") +
			"\n\t" + ExtraInstructions;

		new static List<string> InterfaceEntries = new List<string>()
		{
			"ClearAll",
			"SelectAll",
			"SelectDefault",
		};

		public ChoiceList(string header, List<string> entries, bool alphabetizeEntries, List<string> defaultSelections, int minSelections, int maxSelections, string extraInstructions) : base(header, entries, alphabetizeEntries, defaultSelections, minSelections, maxSelections, extraInstructions) { }

		public new void OnButtonPressed(ButtonHelper buttonHelper)
		{
			logger.LogDebug($"=== OnButtonPressed: {buttonHelper.scrollingButtonType}");
			base.OnButtonPressed(buttonHelper);

			//	TODO: If Single Choice only, then deselect current selection.
		}
	}
}