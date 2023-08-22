using BepInEx.Logging;
using CCU.Mutators;
using CCU.Systems.Mutator_Configurator;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CCU.Systems.Campaign_Branching
{
	internal class LevelGateMenuHead : M_MenuHead
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		private static GameController GC => GameController.gameController;

		public override bool ShowInLevelMutatorList => true;

		public override string DataHeader => StaticHeader.Replace(" ", "");
		public override string DisplayHeader => StaticHeader;
		private static readonly string StaticHeader = "Level Gate";
		public override Type OutputMutatorType => typeof(M_LevelGate);

		public override M_CCU CreateMutatorInstance(string displayName, bool unlockedFromStart)
		{
			M_LevelGate levelGate = new M_LevelGate(displayName, unlockedFromStart)
			{
				GateTypes = SubMenus[0].CurrentSelections,
				GateLabels = SubMenus[1].CurrentSelections,
				SwitchTypes = SubMenus[2].CurrentSelections,
				LogicGates = SubMenus[3].CurrentSelections,
			};

			return levelGate;
		}

		public LevelGateMenuHead(List<SubMenu> subMenus) : base($"{Core.CCUBlockTag} {StaticHeader} {MutatorConfigurator.ConfiguratorBlock}", true, subMenus) { }

		[RLSetup]
		private static void Start()
		{
			List<SubMenu> subMenus = new List<SubMenu>() { };

			subMenus.Add(new ChoiceList(
				header: "Type", 
				entries: new List<string> { "Entry", "Exit" }, alphabetizeEntries: true, 
				defaultSelections: new List<string>() { "Entry" }, minSelections: 1, maxSelections: 1, 
				extraInstructions: "Will this logic apply to entering or exiting this level?" 
			));

			subMenus.Add(new DefineInteger(
				header: "Label",
				defaultSelection: 0, minSelections: 1, maxSelections: 10,
				defaultValue: 0, minValue: 0, maxValue: 99,
				extraInstructions: "Labels are arbitrary, and only apply to Switches attached to the same label."
			));

			subMenus.Add(new ChoiceList(
				header: "Switch", 
				entries: new List<string>() { "Agent"/*, "Level", "Object"*/ }, alphabetizeEntries: true, 
				defaultSelections: new List<string>() { "Agent" }, minSelections: 1, maxSelections: 3, 
				extraInstructions: "Which switches are checked for this level gate?"
			));

			subMenus.Add(new ChoiceList(
				header: "Logic", 
				entries: BoolTools.logicGates.Select(g => g.Key).ToList(), alphabetizeEntries: true, 
				defaultSelections: new List<string>() { "AND" }, minSelections: 1, maxSelections: 1, 
				extraInstructions: "How should the gate evaluate the Switches attached to it, to determine its final value?"
			));

			RogueLibs.CreateCustomUnlock(new LevelGateMenuHead(subMenus))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Activates the " + StaticHeader + " Mutator Configurator.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(LevelGateMenuHead), StaticHeader + MutatorConfigurator.ConfiguratorBlock),
				});
		}
	}
}