using BepInEx.Logging;
using CCU.Mutators;
using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Systems.Mutator_Configurator
{
	public abstract class M_MenuHead : M_CCU
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		private static GameController GC => GameController.gameController;

		public abstract string DataHeader { get; }
		public abstract string DisplayHeader { get; }
		public string HeaderElement => Core.CCUBlockTag + DataHeader + "::";
		public List<SubMenu> SubMenus { get; set; }
		public abstract Type OutputMutatorType { get; } // TODO: Enforce C_OutputMutator force type

		public abstract M_CCU CreateMutatorInstance(string displayName, bool unlockedFromStart);

		public M_MenuHead(string name, bool unlockedFromStart, List<SubMenu> subMenus) : base(name, unlockedFromStart)
		{
			SubMenus = subMenus;
			MutatorConfigurator.MenuHeadSingletons.Add(this);
		}

		public bool IsInstance(string name)
		{
			logger.LogDebug("IsInstance: " + name);
			return
				name.StartsWith(DisplayHeader)
				&& name.Contains(MutatorConfigurator.ConfiguratorBlock);
		}
	}
}