using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU
{
	public class M_CCU : MutatorUnlock
	{
		//private static readonly ManualLogSource logger = BLLogger.GetLogger();
		//private static GameController GC => GameController.gameController;

		public M_CCU() : base() { }
		public M_CCU(string name, bool unlockedFromStart) : base(name, unlockedFromStart)
		{
			IsAvailableInDailyRun = RollInDailyRun;
		}

		public virtual bool RollInDailyRun => false;
		public virtual bool ShowInCampaignMutatorList => false;
		public virtual bool ShowInHomeBaseMutatorList => false;
		public virtual bool ShowInLevelMutatorList => false;

		public virtual List<string> TraitCancellations => new List<string>(); // TODO: IMPLEMENT THIS

		public static string DesignerName(Type type, string custom = null) =>
			"[CCU] " +
				type.Namespace.Split('.')[2].Replace('_', ' ') +
				" - " +
				(custom.Replace('_', ' ')
					?? type.Name.Replace('_', ' '));
		public static string LongishDocumentationName(Type type) =>
			type.Namespace.Split('.')[2].Replace('_', ' ') +
			" - " +
			type.Name.Replace('_', ' ');
		public static string PlayerName(Type type) =>
			type.Name.Replace('_', ' ');
		public string TextName => DesignerName(GetType());

		//  OnPushedButton is NOT implemented for the level editor
	}
}