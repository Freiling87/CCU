using System.Collections.Generic;

namespace CCU.Systems.Campaign_Branching
{
	internal class If_Freed : T_AgentTrigger
	{
		internal override List<string> BigQuestPointTriggers => new List<string>() { };
		internal override List<string> SkillPointTriggers => new List<string>
		{
			"FreedSlave",
		};

		internal override bool GetTriggerValue() =>
			switchStatus;

		internal override void OnTriggeredBigQuestPoints()
		{
			throw new System.NotImplementedException();
		}

		internal override void OnTriggeredSkillPoints()
		{
			throw new System.NotImplementedException();
		}
	}
}
