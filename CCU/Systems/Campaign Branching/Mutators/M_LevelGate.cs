using CCU.Mutators;
using System.Collections.Generic;

namespace CCU.Systems.Campaign_Branching
{
	public class M_LevelGate : M_CCU
	{
		public override bool RollInDailyRun => false;
		public override bool ShowInCampaignMutatorList => false;
		public override bool ShowInHomeBaseMutatorList => false;
		public override bool ShowInLevelMutatorList => true;

		public List<string> GateTypes { get; set; }
		public List<string> GateLabels { get; set; }
		public List<string> SwitchTypes { get; set; }
		public List<string> LogicGates { get; set; }

		public M_LevelGate(string displayName, bool unlockedFromStart) : base(displayName, unlockedFromStart) { } 
	}
}