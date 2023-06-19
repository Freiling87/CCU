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

        [HarmonyPrefix, HarmonyPatch(methodName: nameof(Relationships.AssessBattle))]
        public static bool AssessBattle_CowardFearless(Agent ___agent, ref float __result)
        {
            if (___agent.HasTrait<Coward>())
            {
                __result = 0f;
                return false;
            }
            else if (___agent.HasTrait<Fearless>())
            {
                __result = 9999f;
                return false;
            }

            return true;
        }
        [HarmonyPrefix, HarmonyPatch(methodName: nameof(Relationships.AssessFlee))]
        public static bool AssessFlee_CowardFearless(Agent ___agent, ref float __result)
		{
            if (___agent.HasTrait<Coward>())
			{
                __result = 9999f;
                return false;
            }
            else if (___agent.HasTrait<Fearless>())
			{
                __result = 0f;
                return false;
			}

            return true;
		}

        //  TODO: Refactor. Consider an Interface along the lines of ISetupAgentStats
        [HarmonyPostfix, HarmonyPatch(methodName: nameof(Relationships.SetupRelationshipOriginal), argumentTypes: new[] { typeof(Agent) })]
        public static void SetupRelationshipOriginal_Postfix(Agent otherAgent, Relationships __instance, Agent ___agent)
		{
            bool logging = ___agent.agentName == VanillaAgents.CustomCharacter 
                && false;

            if (logging)
                logger.LogDebug(string.Format("SetupRelationshipOriginal_Postfix: {0} / {1}", ___agent.agentRealName, otherAgent.agentRealName));

            if (GC.levelType == "HomeBase")
                return;

            string relationship = null;

            #region Factions
            //  Factions, Custom
            if (___agent.GetTraits<T_Rel_Faction>().Any(t => t.Faction != 0) && otherAgent.GetTraits<T_Rel_Faction>().Any(t => t.Faction != 0))
            {
                Alignment rel = AlignmentUtils.GetAverageAlignment(___agent, otherAgent);

                if (rel != Alignment.Neutral)
                    relationship = rel.ToString();
            }

            // Factions, Vanilla
            foreach (T_Rel_Faction trait in ___agent.GetTraits<T_Rel_Faction>().Where(t => t.Faction == 0))
                relationship = trait.GetRelationshipTo(otherAgent) ?? relationship;
            #endregion

            //  Player
            if (otherAgent.isPlayer > 0)
                foreach (T_Rel_Player trait in ___agent.GetTraits<T_Rel_Player>())
                    relationship = trait.Relationship ?? relationship;

            #region Trait Gates
            //  These are dual-direction because this overall method is run from agents in unpredictable order.

            if (___agent.HasTrait<Scumbag>() && otherAgent.HasTrait(VanillaTraits.ScumbagSlaughterer))
                ___agent.relationships.GetRelationship(otherAgent).mechHate = true;

            if (___agent.HasTrait<Suspecter>() && ___agent.ownerID != 0 && ___agent.startingChunkRealDescription != "DeportationCenter" && __instance.GetRel(otherAgent) == VRelationship.Neutral && otherAgent.statusEffects.hasTrait(VanillaTraits.Suspicious) && ___agent.ownerID > 0 && (!__instance.QuestInvolvement(___agent) || otherAgent.isPlayer == 0))
                relationship = VRelationship.Annoyed;

            if ((___agent.HasTrait<Cool_Cannibal>() && otherAgent.HasTrait(VanillaTraits.CoolwithCannibals)) ||
                (otherAgent.HasTrait<Cool_Cannibal>() && ___agent.HasTrait(VanillaTraits.CoolwithCannibals)))
                relationship = VRelationship.Neutral;

            if (((___agent.HasTrait<Common_Folk>() || ___agent.agentName == VanillaAgents.SlumDweller) && otherAgent.HasTrait(VanillaTraits.FriendoftheCommonFolk)) ||
                ((otherAgent.HasTrait<Common_Folk>() || otherAgent.agentName == VanillaAgents.SlumDweller) && ___agent.HasTrait(VanillaTraits.FriendoftheCommonFolk)))
                relationship = VRelationship.Loyal;

            if (___agent.HasTrait<Family_Friend>() && (otherAgent.agentName == VanillaAgents.Mobster || otherAgent.HasTrait(VanillaTraits.FriendoftheFamily)))
                relationship = VRelationship.Aligned;
            #endregion

            if (!(relationship is null))
                SetRelationshipTo(___agent, otherAgent, relationship, true);
        }

        //  TODO: Refactor
        public static void SetRelationshipTo(Agent agent, Agent otherAgent, string relationship, bool mutual)
        {
            Relationships __instance = agent.relationships;

            switch (relationship)
            {
                case "":
                    break;
                case VRelationship.Aligned:
                    __instance.SetRelInitial(otherAgent, VRelationship.Aligned);
                    __instance.SetSecretHate(otherAgent, false);
                    if (mutual)
                    {
                        otherAgent.relationships.SetRelInitial(agent, VRelationship.Aligned);
                        otherAgent.relationships.SetRelHate(agent, 0);
                    }
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
                    if (mutual)
                    {
                        otherAgent.relationships.GetRelationship(agent).mechHate = true;
                        otherAgent.relationships.SetRelInitial(agent, VRelationship.Hostile);
                    }
                    break;
                case VRelationship.Loyal:
                    __instance.SetRelInitial(otherAgent, VRelationship.Loyal);
                    __instance.SetSecretHate(otherAgent, false);
                    if (mutual)
                    {
                        otherAgent.relationships.SetRelInitial(agent, VRelationship.Loyal);
                        otherAgent.relationships.SetRelHate(agent, 0);
                    }
                    break;
                case VRelationship.Neutral:
                    __instance.SetRelHate(otherAgent, 0);
                    __instance.SetRelInitial(otherAgent, VRelationship.Neutral);
                    __instance.SetSecretHate(otherAgent, false);
                    if (mutual)
                    {
                        otherAgent.relationships.SetRelInitial(agent, VRelationship.Neutral);
                        otherAgent.relationships.SetRelHate(agent, 0);
                    }
                    break;
                case VRelationship.Submissive:
                    __instance.SetRel(otherAgent, VRelationship.Submissive);
                    __instance.SetSecretHate(otherAgent, false);
                    if (mutual)
                    {
                        otherAgent.relationships.SetRel(agent, VRelationship.Neutral);
                    }
                    break;
            }
        }
    }
}