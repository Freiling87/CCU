using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU
{
	public class M_CCU : MutatorUnlock
	{
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
			$"{Core.CCUBlockTag} {DoSpaces(type.Namespace.Split('.')[2])} - {DoSpaces(custom) ?? DoSpaces(type.Name)}";

		public static string PlayerName(Type type) =>
			DoSpaces(type.Name);

		public string GetDesignerName => DesignerName(GetType());
		public string GetPlayerName => PlayerName(GetType());

		private static string DoSpaces(string input) =>
			input.Replace('_', ' ').Trim();
	}
}