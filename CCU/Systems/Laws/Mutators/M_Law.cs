namespace CCU.Mutators.Laws
{
	public abstract class M_Law : M_CCU
	{
		protected M_Law(string name, bool unlockedFromStart) : base(name, unlockedFromStart) { }

		public override bool RollInDailyRun => true;
		public override bool ShowInCampaignMutatorList => true;
		public override bool ShowInHomeBaseMutatorList => true;
		public override bool ShowInLevelMutatorList => true;

		public abstract string LawText { get; } // Text for Sign at level entrance

		public abstract bool IsViolating(Agent agent);
		public abstract int Strikes { get; }
		public abstract string[] WarningMessage { get; }
	}
}
