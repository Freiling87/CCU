using BepInEx.Logging;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;

namespace CCU.Systems.Mutator_Configurator
{
	internal class DefineInteger : SubMenu
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		private static GameController GC => GameController.gameController;

		public override string Instructions =>
			"<b><color=yellow>" + MutatorConfigurator.CurrentHead.DisplayHeader + " " + DisplayHeader + "</color></b>" + 
			"\n\t" + ExtraInstructions;

		public int CurrentValue;
		public int DefaultValue;
		public int MaxValue;
		public int MinValue;

		public const string
			ButtonDivider = "------------------------",
			ClearSelections = "<color=red>Clear Selections</color>",
			ConfirmValue = "<color=lime>Confirm Value</color>",
			SetToDefault = "Default Value",
			SetToMax = "Max Value",
			SetToMin = "Min Value",

			z = "";

		public List<string> InterfaceEntries = new List<string>()
		{
			ButtonDivider,
			ConfirmValue,
			ButtonDivider,
			SetToMin,
			SetToDefault,
			SetToMax,
			ButtonDivider,
			ClearSelections,
		};

		public DefineInteger(string header, int defaultSelection, int minSelections, int maxSelections, int defaultValue, int minValue, int maxValue, string extraInstructions) : base(header, new List<string>(), true, new List<string>() { defaultSelection.ToString() }, minSelections, maxSelections, extraInstructions)
		{
			SelectionDefaults = new List<string>() { defaultValue.ToString() };
			DefaultValue = defaultValue;
			CurrentValue = defaultValue;
			MaxValue = maxValue;
			MinValue = minValue;

			List<string> entries = new List<string>() 
			{ 
				"-1", "+1"
			};

			if (MaxValue > 10)
			{
				entries.Insert(0, "-10");
				entries.Add("+10");
			}

			if (MaxValue > 100)
			{
				entries.Insert(0, "-100");
				entries.Add("+100");
			}

			entries.AddRange(InterfaceEntries);

			base.Entries = entries;
			Reset();
		}

		public override void OnButtonPressed(ButtonHelper buttonHelper)
		{
			//	Don't call Base Method! We don't want buttons sticking.

			string buttonText = buttonHelper.scrollingButtonType;

			if (int.TryParse(buttonText.Replace("+", ""), out int buttonValue))
			{
				if (CurrentValue + buttonValue <= MaxValue
					&& CurrentValue + buttonValue >= MinValue)
				{
					GC.audioHandler.Play(GC.playerAgent, VanillaAudio.ClickButton);
					CurrentValue += buttonValue;
				}
				else
					GC.audioHandler.Play(GC.playerAgent, VanillaAudio.Fail);
			}
			else
			{
				GC.audioHandler.Play(GC.playerAgent, VanillaAudio.ClickButton);

				switch (buttonText)
				{
					case ClearSelections:
						CurrentSelections.Clear();
						break;

					case ConfirmValue:
						if (ValidateCurrentValue(out string _)
							&& CurrentSelections.Count < MaximumSelections)
						{
							CurrentSelections.Add(CurrentValue.ToString());
							CurrentSelections = CurrentSelections.OrderBy(s => int.Parse(s)).ToList();
							CurrentValue = DefaultValue;
						}
						else
							GC.audioHandler.Play(GC.playerAgent, VanillaAudio.Fail);
						break;

					case SetToDefault:
						CurrentValue = DefaultValue;
						break;

					case SetToMax:
						CurrentValue = MaxValue;
						break;

					case SetToMin:
						CurrentValue = MinValue;
						break;
				}
			}

			MutatorConfigurator.UpdateInterface();
		}

		public override void Reset()
		{
			base.Reset();
			CurrentValue = int.Parse(SelectionDefaults[0]);
		}

		public override string ConfiguratorStatusTextCurrent()
		{
			ValidateCurrentValue(out string errorMessageCurrentValue);
			ValidateCurrentSelections(out string errorMessageSelections);

			string text = $"{errorMessageCurrentValue}"
				+ $"\n{errorMessageSelections}";

			return text;
		}

		public bool ValidateCurrentValue(out string errorMessage)
		{
			errorMessage = $"Current Value: <color=yellow>{CurrentValue}</color>\n";

			if (CurrentValue < MinValue)
				errorMessage += $"<color=red>Value must be at least {MinValue}.</color>";
			else if (CurrentValue > MaxValue)
				errorMessage += $"<color=red>Value must be at most {MaxValue}.</color>";
			else if (CurrentSelections.Contains(CurrentValue.ToString()))
				errorMessage += $"<color=red>Value is already added to Selections.</color>";
			else
				return true;

			return false;
		}

		public override bool ValidateCurrentSelections(out string errorMessageSelections)
		{
			errorMessageSelections = null;
			return ValidateCurrentValue(out string errorMessageValue) 
				&& base.ValidateCurrentSelections(out errorMessageSelections);
		}
	}
}