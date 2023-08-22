using CCU.Traits;
using System.Collections.Generic;

namespace CCU.Systems.Campaign_Branching
{
	internal abstract class T_Trigger : T_CCU
	{
		internal T_Trigger() : base() { }

		internal bool status;
		internal bool frozen; // For quickly bypassing complicated logic.
		internal abstract List<string> BigQuestPointTriggers { get; }
		internal abstract List<string> SkillPointTriggers { get; }

		internal abstract bool GetTriggerValue();
		internal abstract void OnTriggeredBigQuestPoints();
		internal abstract void OnTriggeredSkillPoints();

		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}