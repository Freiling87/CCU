﻿using BepInEx.Logging;
using CCU.Traits;
using HarmonyLib;
using RogueLibsCore;

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
        public static bool AddDisplayPiece_Prefix(Trait myTrait)
        {
            return T_CCU.IsPlayerTrait(myTrait);
        }
    }
}