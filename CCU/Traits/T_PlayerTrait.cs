using RogueLibsCore;

namespace CCU.Traits
{
    public abstract class T_PlayerTrait : T_CCU
    {
        public T_PlayerTrait() : base() { }

        public static new TraitBuilder PostProcess
        {
            set
            {
                if (value.Unlock is TraitUnlock_CCU unlock)
                    unlock.PlayerTrait = true;

                // Examples from parent: 
                //value.Unlock.Unlock.cantLose = true;
                //value.Unlock.Unlock.cantSwap = true;
                //value.Unlock.Unlock.upgrade = null;
            }
        }
    }
}
