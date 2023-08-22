using CCU.Mutators;

namespace CCU.Mutators.Laws
{
    internal abstract class M_Law : M_CCU
    {
        public M_Law() : base() { }

        public override bool ShowInCampaignMutatorList => true;
        public override bool ShowInHomeBaseMutatorList => true;
        public override bool ShowInLevelMutatorList => true;

        public abstract string LawText { get; } // Text for Sign at level entrance

        public abstract bool IsViolating(Agent agent);
        public abstract int Strikes { get; }
        public abstract string[] WarningMessage { get; }
    }
}
