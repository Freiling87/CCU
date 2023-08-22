using BepInEx.Logging;
using CCU.Systems.Tools;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

namespace CCU.Systems.Mutator_Configurator
{
	public abstract class SubMenu
	{
		// WARNING: Virtuals don't work like overrides without casting, and manual casting is gonna suck ass.
		// See the most recent GPT answer for how to make this work.


		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		private static GameController GC => GameController.gameController;

		public virtual string DisplayHeader { get; set; } // E.g. "Gate Labels"
		public virtual string Instructions { get; }
		public virtual string ExtraInstructions { get; }

		public virtual List<string> Entries { get; set; }
		public virtual List<string> InterfaceEntries { get; set; }

		public virtual List<string> SelectionDefaults { get; set; }
		public virtual int MinimumSelections { get; set; }
		public virtual int MaximumSelections { get; set; }

		public virtual List<string> CurrentSelections { get; internal set; }

		public SubMenu(string header, List<string> entries, bool alphabetizeEntries, List<string> selectionDefaults, int minimumSelectionCountOrValue, int maximumSelectionCountOrValue, string extraInstructions)
		{
			CurrentSelections = new List<string>();
			DisplayHeader = header;
			Entries = entries;
			ExtraInstructions = extraInstructions;
			SelectionDefaults = selectionDefaults;
			MinimumSelections = minimumSelectionCountOrValue;
			MaximumSelections = maximumSelectionCountOrValue;
		}

		public virtual void OnButtonPressed(ButtonHelper buttonHelper)
		{
			string buttonText = buttonHelper.scrollingButtonType;
			GC.audioHandler.Play(GC.playerAgent, VanillaAudio.ClickButton);
			SpriteState spriteState = default;
			ButtonData buttonData = GC.levelEditor.buttonsDataLoad[buttonHelper.scrollingButtonNum];

			if (!buttonHelper.scrollingHighlighted)
			{
				CurrentSelections.Add(buttonText);
				spriteState.highlightedSprite = buttonHelper.solidObjectButtonSelected;
				buttonData.highlightedSprite = GC.mainGUI.scrollingMenuScript.solidObjectButtonSelected;
			}
			else
			{
				CurrentSelections.Remove(buttonText);
				spriteState.highlightedSprite = buttonHelper.solidObjectButton;
			}

			buttonHelper.button.spriteState = spriteState;
			buttonHelper.scrollingHighlighted = !buttonHelper.scrollingHighlighted;
		}
		public string FinalizedElementText()
		{
			string output = DisplayHeader + "=";

			foreach (string selection in CurrentSelections)
				output += selection + ",";

			output = output.Substring(0, output.Length - 1); // Remove last comma
			output += ";";
			return output;
		}
		public virtual bool ValidateCurrentSelections(out string errorMessage)
		{
			errorMessage = "";
			bool valid = true;

			if (CurrentSelections is null
				|| CurrentSelections.Count < MinimumSelections)
			{
				errorMessage += $"<color=red>Select at least {MinimumSelections} option(s)!</color>";
				valid = false;
			}
			else if (CurrentSelections.Count > MaximumSelections)
			{
				errorMessage += $"<color=red>Select at most {MaximumSelections} option(s)!</color>";
				valid = false;
			}
			else if (CurrentSelections.Count == MaximumSelections)
				errorMessage += $"\n<color=yellow>Maximum number of Selections added.</color>";

			return valid;
		}
		public virtual void Reset()
		{
			CurrentSelections.Clear();
		}
		public virtual string ConfiguratorStatusTextCompleted()
		{
			int desiredPixelWidth = 120;
			string outputBuilder = StringTools.PadHorizontal($"\t{DisplayHeader}:", desiredPixelWidth);

			if (CurrentSelections.Count > 0)
			{
				foreach (string elementValue in CurrentSelections)
					outputBuilder += $"<color=lime>{elementValue}</color>, ";

				outputBuilder = outputBuilder.Remove(outputBuilder.Length - 2, 2);
			}
			else
				outputBuilder += "<color=yellow>(None)</color>";

			outputBuilder += "\n";
			return outputBuilder;
		}
		public virtual string ConfiguratorStatusTextCurrent()
		{
			ValidateCurrentSelections(out string errorMessage); // Bool discarded here
			return errorMessage;
		}
	}
}