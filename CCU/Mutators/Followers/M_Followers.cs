using RogueLibsCore;

namespace CCU.Mutators.Followers
{
    internal abstract class M_Followers : M_CCU
    {
        public M_Followers(string v1, bool v2) : base(v1, v2) { }

        public override bool RollInDailyRun => true;
        public override bool ShowInCampaignMutatorList => true;
        public override bool ShowInHomeBaseMutatorList => true;
        public override bool ShowInLevelMutatorList => true;
	}
}