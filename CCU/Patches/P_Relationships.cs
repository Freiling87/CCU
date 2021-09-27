using System;
using RogueLibsCore;
using BepInEx.Logging;
using HarmonyLib;

namespace CCU.Patches
{
    [HarmonyPatch(declaringType: typeof(Relationships))]
    public static class P_Relationships
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

        [HarmonyPrefix, HarmonyPatch(methodName: nameof(Relationships.SetupRelationshipOriginal), argumentTypes: new Type[1] { typeof(Agent) })]
        public static bool SetupRelationshipOriginal_Prefix(Agent otherAgent, Relationships __instance, Agent ___agent)
        {
            Core.LogMethodCall();

            if ((___agent.HasTrait<Faction_1_Aligned>() && otherAgent.HasTrait<Faction_1_Aligned>()) ||
                (___agent.HasTrait<Faction_2_Aligned>() && otherAgent.HasTrait<Faction_2_Aligned>()) ||
                (___agent.HasTrait<Faction_3_Aligned>() && otherAgent.HasTrait<Faction_3_Aligned>()) ||
                (___agent.HasTrait<Faction_4_Aligned>() && otherAgent.HasTrait<Faction_4_Aligned>()))
            {
                Core.LogCheckpoint("A");

                __instance.SetRelInitial(otherAgent, "Aligned");
                __instance.SetRelHate(otherAgent, 0);
                otherAgent.relationships.SetRelInitial(___agent, "Aligned");
                otherAgent.relationships.SetRelHate(___agent, 0);

                return false;
            }

            if ((___agent.HasTrait<Faction_1_Hostile>() && otherAgent.HasTrait<Faction_1_Aligned>()) ||
                (___agent.HasTrait<Faction_1_Aligned>() && otherAgent.HasTrait<Faction_1_Hostile>()) ||
                (___agent.HasTrait<Faction_2_Hostile>() && otherAgent.HasTrait<Faction_2_Aligned>()) ||
                (___agent.HasTrait<Faction_2_Aligned>() && otherAgent.HasTrait<Faction_2_Hostile>()) ||
                (___agent.HasTrait<Faction_3_Hostile>() && otherAgent.HasTrait<Faction_3_Aligned>()) ||
                (___agent.HasTrait<Faction_3_Aligned>() && otherAgent.HasTrait<Faction_3_Hostile>()) ||
                (___agent.HasTrait<Faction_4_Hostile>() && otherAgent.HasTrait<Faction_4_Aligned>()) ||
                (___agent.HasTrait<Faction_4_Aligned>() && otherAgent.HasTrait<Faction_4_Hostile>()))
            {
                Core.LogCheckpoint("B");

                __instance.SetRelInitial(otherAgent, "Hateful");
                __instance.SetRelHate(otherAgent, 5);
                otherAgent.relationships.SetRelInitial(___agent, "Hateful");
                otherAgent.relationships.SetRelHate(___agent, 5);

                return false;
            }

            return true;
        }
    }
}
