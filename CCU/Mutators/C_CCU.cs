using RogueLibsCore;
using System;

namespace CCU.Challenges
{
    public abstract class C_CCU : MutatorUnlock
    {
        public C_CCU() : base() { }

        public static UnlockBuilder PostProcess
        {
            // Copied from Trait hierarchy.

            set
            {
                //value.Unlock.Unlock.cantLose = true;
                //value.Unlock.Unlock.cantSwap = true;
                //value.Unlock.Unlock.upgrade = null;
            }
        }

        public static string DesignerName(Type type, string custom = null) =>
            "[CCU] " +
            (type.Namespace).Split('.')[2].Replace('_', ' ') +
            " - " +
            (custom ?? (type.Name).Replace('_', ' '));

        public static string PlayerName(Type type) =>
            (type.Name).Replace('_', ' ');

        public static string LongishDocumentationName(Type type) =>
            (type.Namespace).Split('.')[2].Replace('_', ' ') +
            " - " +
            (type.Name).Replace('_', ' ');

        public string TextName => DesignerName(GetType());
    }
}