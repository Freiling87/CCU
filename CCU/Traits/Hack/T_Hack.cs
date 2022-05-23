using CCU.Localization;
using RogueLibsCore;

namespace CCU.Traits.Hack
{
    public abstract class T_Hack : CustomTrait
    {
        public T_Hack() : base() { }

        public abstract string ButtonText { get; }
        public static string DisplayName<T>(string custom = null) =>
            "[CCU] " +
            LocalizationTools.UnderscoresToSpaces(typeof(T).Namespace) + " - " +
            custom != null
                ? custom
                : LocalizationTools.UnderscoresToSpaces(typeof(T).Name);
    }
}