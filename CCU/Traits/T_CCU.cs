using RogueLibsCore;
using System;

namespace CCU.Traits
{
    public abstract class T_CCU : CustomTrait
    {
        public static string DisplayName(Type type, string custom = null) =>
            "[CCU] " + 
            (type.Namespace).Split('.')[2].Replace('_', ' ') + 
            " - " +
            (custom ?? (type.Name).Replace('_', ' '));

        public static string ShortNameDocumentationOnly(Type type) =>
            (type.Namespace).Split('.')[2].Replace('_', ' ') + 
            " - " +
            (type.Name).Replace('_', ' ');

        public string TextName => DisplayName(GetType());

        
    }
}