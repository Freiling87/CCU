﻿namespace CCU.Traits.Interaction
{
    public abstract class T_Interaction : T_CCU
    {
        public T_Interaction() : base() { }

        public abstract bool AllowUntrusted { get; }
        public abstract string ButtonText { get; }
        public abstract bool ExtraTextCostOnly { get; }
        public abstract string DetermineMoneyCost { get; }
    }
}