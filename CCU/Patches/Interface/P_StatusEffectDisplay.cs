using BepInEx.Logging;
using HarmonyLib;

namespace CCU.Patches.Interface
{
    [HarmonyPatch(declaringType: typeof(StatusEffectDisplay))]
    class P_StatusEffectDisplay
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

        /// <summary>
        /// Filter CCU traits from lower-left display when possessing
        /// </summary>
        /// <param name="myStatusEffect"></param>
        /// <param name="myTrait"></param>
        /// <returns></returns>
        [HarmonyPrefix, HarmonyPatch(methodName: nameof(StatusEffectDisplay.AddDisplayPiece), argumentTypes: new[] { typeof(StatusEffect), typeof(Trait) })]
        public static bool AddDisplayPiece_Prefix(StatusEffect myStatusEffect, Trait myTrait)
        {
            // First, let's identify the format of trait names so we're using the right versions.
            logger.LogDebug(myTrait.traitName);
            
            return true;
        }
    }
}