using CCU.Localization;
using RogueLibsCore;

namespace CCU.Traits.Hack
{
    public abstract class T_Hack : CustomTrait
    {
        public T_Hack() : base() { }

        public abstract string ButtonText { get; }
        public static string DisplayName<T>(string custom = null) =>
            "[CCU] " + (typeof(T).Namespace).Split('.')[2] + " - " +
            (custom ?? (typeof(T).Name).Replace('_', ' '));
    }
}  