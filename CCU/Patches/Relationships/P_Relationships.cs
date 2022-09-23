using BepInEx.Logging;
using CCU.Localization;
using CCU.Traits.Combat;
using CCU.Traits.Rel_Faction;
using CCU.Traits.Rel_Player;
using CCU.Traits.Trait_Gate;
using HarmonyLib;
using RogueLibsCore;
using System.Linq;
using static CCU.Traits.Rel_Faction.T_Rel_Faction;

namespace CCU.Patches.AgentRelationships
{
    [HarmonyPatch(declaringType: typeof(Relationships))]
    public static class P_Relationships
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

        [HarmonyPostfix, HarmonyPatch(methodName: nameof(Relationships.AssessFlee), argumentTypes: new[] { typeof(Agent), typeof(int), typeof(int), typeof(float), typeof(float), typeof(Relationship) })]
        public static void AssessFlee_Postfix(Agent otherAgent, int teamSize, int otherTeamSize, float dist, float relHate, Relationship rel, Agent ___agent, ref float __result)
		{
            if (___agent.HasTrait<Coward>())
			{
                ___agent.mustFlee = true;
                ___agent.wontFlee = false;
            }
            else if (___agent.HasTrait<Fearless>())
			{
                ___agent.mustFlee = false;
                ___agent.wontFlee = true;
            }
		}

        //  TODO: Refactor. Consider an Interface along the lines of ISetupAgentStats
        [HarmonyPostfix, HarmonyPatch(methodName: nameof(Relationships.SetupRelationshipOriginal), argumentTypes: new[] { typeof(Agent) })]
        public static void SetupRelationshipOriginal_Postfix(Agent otherAgent, Relationships __instance, Agent ___agent)
		{
            if (GC.levelType == "HomeBase")
                return;

            string relationship = __instance.GetRel(otherAgent);

            Core.LogMethodCall();
            logger.LogDebug("Relationship : " + relationship);

            //  Factions, Custom
            if (___agent.GetTraits<T_Rel_Faction>().Any(t => t.Faction != 0) && otherAgent.GetTraits<T_Rel_Faction>().Any(t => t.Faction != 0))
            {
                Alignment rel = AlignmentUtils.GetAverageAlignment(___agent, otherAgent);
                logger.Log("rel: " + rel);

                if (rel != Alignment.Neutral)
                    relationship = rel.ToString();
            }

            // Factions, Vanilla
            foreach (T_Rel_Faction trait in ___agent.GetTraits<T_Rel_Faction>().Where(t => t.Faction == 0))
                relationship = trait.GetRelationshipTo(otherAgent) ?? relationship;

            //  Player
            if (otherAgent.isPlayer > 0)
                foreach (T_Rel_Player trait in ___agent.GetTraits<T_Rel_Player>())
                    relationship = trait.Relationship ?? relationship;

            //  Trait Gates
            if (___agent.HasTrait<Scumbag>() && otherAgent.HasTrait(VanillaTraits.ScumbagSlaughterer))
                ___agent.relationships.GetRelationship(otherAgent).mechHate = true;

            if (___agent.HasTrait<Suspecter>() && ___agent.ownerID != 0 && ___agent.startingChunkRealDescription != "DeportationCenter" && __instance.GetRel(otherAgent) == VRelationship.Neutral && otherAgent.statusEffects.hasTrait(VanillaTraits.Suspicious) && ___agent.ownerID > 0 && (!__instance.QuestInvolvement(___agent) || otherAgent.isPlayer == 0))
                relationship = VRelationship.Annoyed;

            if (___agent.HasTrait<Cool_Cannibal>() && otherAgent.HasTrait(VanillaTraits.CoolwithCannibals))
                relationship = VRelationship.Neutral;

            if (___agent.HasTrait(VanillaTraits.FriendoftheCommonFolk) && otherAgent.HasTrait<Common_Folk>() && !otherAgent.guardSequence)
                relationship = VRelationship.Loyal;

            if (___agent.HasTrait<Family_Friend>() && (otherAgent.agentName == VanillaAgents.Mobster || otherAgent.HasTrait(VanillaTraits.FriendoftheFamily)))
                relationship = VRelationship.Aligned;

            if (!(relationship is null))
                SetRelationshipTo(___agent, otherAgent, relationship);
        }

        //  TODO: Refactor
        public static void SetRelationshipTo(Agent agent, Agent otherAgent, string relationship)
        {
            Relationships __instance = agent.relationships;

            switch (relationship)
            {
                case "":
                    break;
                case VRelationship.Aligned:
                    __instance.SetRelInitial(otherAgent, VRelationship.Aligned);
                    __instance.SetSecretHate(otherAgent, false);
                    otherAgent.relationships.SetRelInitial(agent, VRelationship.Aligned);
                    otherAgent.relationships.SetRelHate(agent, 0);
                    break;
                case VRelationship.Annoyed:
                    __instance.SetStrikes(otherAgent, 2);
                    break;
                case VRelationship.Friendly:
                    __instance.SetRel(otherAgent, VRelationship.Friendly);
                    __instance.SetSecretHate(otherAgent, false);
                    break;
                case VRelationship.Hostile:
                    __instance.SetRel(otherAgent, VRelationship.Hostile);
                    __instance.SetRelHate(otherAgent, 5);
                    otherAgent.relationships.GetRelationship(agent).mechHate = true;
                    otherAgent.relationships.SetRelInitial(agent, VRelationship.Hostile);
                    break;
                case VRelationship.Loyal:
                    __instance.SetRelInitial(otherAgent, VRelationship.Loyal);
                    __instance.SetSecretHate(otherAgent, false);
                    otherAgent.relationships.SetRelInitial(agent, VRelationship.Loyal);
                    otherAgent.relationships.SetRelHate(agent, 0);
                    break;
                case VRelationship.Neutral:
                    __instance.SetRelHate(otherAgent, 0);
                    __instance.SetRelInitial(otherAgent, VRelationship.Neutral);
                    __instance.SetSecretHate(otherAgent, false);
                    otherAgent.relationships.SetRelInitial(agent, VRelationship.Neutral);
                    otherAgent.relationships.SetRelHate(agent, 0);
                    break;
                case VRelationship.Submissive:
                    __instance.SetRel(otherAgent, VRelationship.Submissive);
                    __instance.SetSecretHate(otherAgent, false);
                    otherAgent.relationships.SetRel(agent, VRelationship.Neutral);
                    break;
            }
        }
    }
}