using BepInEx.Logging;
using CCU.Traits.Combat;
using CCU.Traits.Passive;
using HarmonyLib;
using RogueLibsCore;
using System;

namespace CCU.Patches.Goals
{
    [HarmonyPatch(declaringType: typeof(GoalCombatEngage))]
    public static class P_GoalCombatEngage
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

        [HarmonyPostfix, HarmonyPatch(methodName: nameof(GoalCombatEngage.Activate))]
        private static void Activate_Postfix(Agent ___agent)
        {
            if (___agent.HasTrait<Concealed_Carrier>())
            {
                InvItem equippedWeapon = ___agent.inventory.equippedWeapon;

                if (!(equippedWeapon is null))
                    ___agent.gun.ShowGun(equippedWeapon);
            }
        }

        /// <summary>
        /// Lockdowner (Shelved, possibly a vanilla bug with lockdown walls on custom levels)
        /// </summary>
        /// <param name="__instance"></param>
        //[HarmonyPostfix, HarmonyPatch(methodName:nameof(GoalCombatEngage.Process), argumentTypes: new Type[0] { })]
        public static void Process_Postfix(GoalCombatEngage __instance)
        {
            if (__instance.gc.loadLevel.hasLockdownWalls && 
                __instance.agent.HasTrait<Lockdowner>() && 
                __instance.agent.curTileData.lockdownZone == __instance.battlingAgent.curTileData.lockdownZone && !__instance.agent.curTileData.lockdownWall && !__instance.agent.curTileData.dangerousToWalk && 
                (AlarmButton.lockdownTimer < 5f || !__instance.gc.lockdown) && 
                __instance.battlingAgent.isPlayer != 0)
            {
                __instance.agent.CauseLockdown();
            }
        }
    }
}
