namespace CCU.Mutators.Quest_Gen
{
	public class M_QuestGen : M_CCU
	{
		public override bool RollInDailyRun => true;
		public override bool ShowInCampaignMutatorList => true;
		public override bool ShowInHomeBaseMutatorList => true;
		public override bool ShowInLevelMutatorList => true;

		protected M_QuestGen(string name, bool unlockedFromStart) : base(name, unlockedFromStart) { }
	}
}