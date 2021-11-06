using System;
using RogueLibsCore;
using BepInEx.Logging;
using HarmonyLib;
using CCU.Traits.Relationships;
using CCU.Traits.Combat;
using CCU.Traits;
using CCU.Traits.TraitGate;

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
            if (___agent.HasTrait<Combat_Coward>())
			{
                ___agent.mustFlee = true;
                ___agent.wontFlee = false;
            }
            else if (___agent.HasTrait<Combat_Fearless>())
			{
                ___agent.mustFlee = false;
                ___agent.wontFlee = true;
            }
                
		}

        [HarmonyPrefix, HarmonyPatch(methodName: nameof(Relationships.SetupRelationshipOriginal), argumentTypes: new [] { typeof(Agent) })]
        public static bool SetupRelationshipOriginal_Prefix(Agent otherAgent, Relationships __instance, Agent ___agent)
        {
            if (TraitManager.HasTraitFromList(___agent, TraitManager.RelationshipTraits))
			{
				#region Factions
				if ((___agent.HasTrait<Faction_1_Aligned>() && otherAgent.HasTrait<Faction_1_Aligned>()) ||
                    (___agent.HasTrait<Faction_2_Aligned>() && otherAgent.HasTrait<Faction_2_Aligned>()) ||
                    (___agent.HasTrait<Faction_3_Aligned>() && otherAgent.HasTrait<Faction_3_Aligned>()) ||
                    (___agent.HasTrait<Faction_4_Aligned>() && otherAgent.HasTrait<Faction_4_Aligned>()))
                {
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
                    __instance.SetRelInitial(otherAgent, "Hateful");
                    __instance.SetRelHate(otherAgent, 5);
                    otherAgent.relationships.SetRelInitial(___agent, "Hateful");
                    otherAgent.relationships.SetRelHate(___agent, 5);

                    return false;
                }
				#endregion
                if ((___agent.HasTrait<HostileToCannibals>() && otherAgent.agentName == VanillaAgents.Cannibal) ||
                    (___agent.HasTrait<HostileToSoldiers>() && otherAgent.agentName == VanillaAgents.Soldier) ||
                    (___agent.HasTrait<HostileToVampires>() && otherAgent.agentName == VanillaAgents.Vampire) ||
                    (___agent.HasTrait<HostileToWerewolves>() && otherAgent.agentName == VanillaAgents.Werewolf))
				{
                    __instance.SetRelInitial(otherAgent, "Hateful");
                    otherAgent.relationships.SetRelInitial(___agent, "Hateful");
                    __instance.SetRelHate(otherAgent, 5);
                    otherAgent.relationships.SetRelHate(___agent, 5);
                }
			}

			return true;
        }

        [HarmonyPostfix, HarmonyPatch(methodName: nameof(Relationships.SetupRelationshipOriginal), argumentTypes: new[] { typeof(Agent) })]
        public static void SetupRelationshipOriginal_Postfix(Agent otherAgent, Relationships __instance, Agent ___agent)
		{
            if (___agent.HasTrait<AnnoyedAtSuspicious>() && ___agent.ownerID != 0 && ___agent.startingChunkRealDescription != "DeportationCenter" && __instance.GetRel(otherAgent) == "Neutral" && otherAgent.statusEffects.hasTrait(VanillaTraits.Suspicious) && ___agent.ownerID > 0 && (!__instance.QuestInvolvement(___agent) || otherAgent.isPlayer == 0))
                __instance.SetStrikes(otherAgent, 2);

            if (___agent.HasTrait(VanillaTraits.FriendoftheCommonFolk) && otherAgent.HasTrait<TraitGate_CommonFolk>() && !otherAgent.guardSequence)
                otherAgent.relationships.SetRelInitial(___agent, "Loyal");

            if (___agent.HasTrait(VanillaTraits.FriendoftheFamily) && otherAgent.HasTrait<TraitGate_FamilyFriend>())
                otherAgent.relationships.SetRelInitial(___agent, "Aligned");

            if (otherAgent.HasTrait(VanillaTraits.CoolwithCannibals) && ___agent.HasTrait<TraitGate_CoolCannibal>())
            {
                __instance.SetRelHate(otherAgent, 0);
                __instance.SetRelInitial(otherAgent, "Neutral", true);
                otherAgent.relationships.SetRelHate(___agent, 0);
                otherAgent.relationships.SetRelInitial(___agent, "Neutral");
            }

            if (___agent.HasTrait(VanillaTraits.ScumbagSlaughterer) && otherAgent.HasTrait<TraitGate_Scumbag>())
			{
                otherAgent.relationships.GetRelationship(___agent).mechHate = true;
                otherAgent.oma.mustBeGuilty = true;
            }
        }
    }
}
