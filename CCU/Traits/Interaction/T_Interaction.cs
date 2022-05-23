using CCU.Localization;
using RogueLibsCore;

namespace CCU.Traits.Interaction
{
    public abstract class T_Interaction : CustomTrait
    {
        public T_Interaction() : base() { }

        public abstract string ButtonText { get; }
        public static string DisplayName<T>(string custom = null) =>
            "[CCU] " +
            LocalizationTools.UnderscoresToSpaces(typeof(T).Namespace) + " - " +
            custom != null
                ? custom
                : LocalizationTools.UnderscoresToSpaces(typeof(T).Name);
    }
}