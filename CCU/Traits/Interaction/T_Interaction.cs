using CCU.Localization;
using RogueLibsCore;

namespace CCU.Traits.Interaction
{
    public abstract class T_Interaction : CustomTrait
    {
        public T_Interaction() : base() { }

        public abstract string ButtonText { get; }
        public static string DisplayName<T>(string custom = null) =>
            "[CCU] " + (typeof(T).Namespace).Split('.')[2] + " - " +
            (custom ?? (typeof(T).Name).Replace('_', ' '));
    }
}