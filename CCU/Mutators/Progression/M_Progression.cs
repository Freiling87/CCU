namespace CCU.Mutators.Progression
{
	internal abstract class M_Progression : M_CCU
    {
        public M_Progression() : base() { }

        public override bool ShowInCampaignMutatorList => true;
        public override bool ShowInHomeBaseMutatorList => true;
        public override bool ShowInLevelMutatorList => true;
    }
}
