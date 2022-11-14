using CCU.Challenges;

namespace CCU.Mutators.Laws
{
    internal abstract class C_Law : C_CCU
    {
        public C_Law() : base() { }

        public abstract string LawText { get; } // Text for Sign at level entrance

        public abstract bool IsViolating(Agent agent);
        public abstract int Strikes { get; }
        public abstract string[] WarningMessage { get; }
    }
}
