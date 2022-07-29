using RogueLibsCore;
using System;

namespace CCU.Traits
{
    public abstract class T_CCU : CustomTrait
    {
        public static TraitBuilder PostProcess
        {
            // This is applied after each trait is created. I don't fully get how it works, but see where PP's value is assigned.
            set
            {
                value.Unlock.Unlock.cantLose = true;
                value.Unlock.Unlock.cantSwap = true;
                value.Unlock.Unlock.upgrade = null;
            }
        }

        // On its OWN commit, 
        //  Rename to Designer Name
        public static string DisplayName(Type type, string custom = null) =>
            "[CCU] " + 
            (type.Namespace).Split('.')[2].Replace('_', ' ') + 
            " - " +
            (custom ?? (type.Name).Replace('_', ' '));

        public static string PlayerName(Type type) =>
            (type.Name).Replace('_', ' ');

        //  ...and Rename to Documentation Name
        public static string ShortNameDocumentationOnly(Type type) =>
            (type.Namespace).Split('.')[2].Replace('_', ' ') + 
            " - " +
            (type.Name).Replace('_', ' ');


        public string TextName => DisplayName(GetType());
    }
}