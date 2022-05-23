using System;
using RogueLibsCore;
using BepInEx.Logging;
using HarmonyLib;
using CCU.Traits.Relationships;
using CCU.Traits.Combat;
using CCU.Traits;
using CCU.Traits.TraitGate;
using CCU.Traits.Faction;

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
                if ((___agent.HasTrait<Hostile_To_Cannibal>() && (otherAgent.agentName == VanillaAgents.Cannibal || otherAgent.HasTrait<CoolCannibal>())) ||
                    (___agent.HasTrait<Hostile_To_Soldier>() && otherAgent.agentName == VanillaAgents.Soldier) ||
                    (___agent.HasTrait<Hostile_To_Vampire>() && otherAgent.agentName == VanillaAgents.Vampire) ||
                    (___agent.HasTrait<Hostile_To_Werewolf>() && otherAgent.agentName == VanillaAgents.Werewolf))
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
            string relationship = "";

            if (___agent.HasTrait<Suspicioner>() && ___agent.ownerID != 0 && ___agent.startingChunkRealDescription != "DeportationCenter" && __instance.GetRel(otherAgent) == "Neutral" && otherAgent.statusEffects.hasTrait(VanillaTraits.Suspicious) && ___agent.ownerID > 0 && (!__instance.QuestInvolvement(___agent) || otherAgent.isPlayer == 0))
                relationship = "Annoyed";

            if (___agent.HasTrait(VanillaTraits.FriendoftheCommonFolk) && otherAgent.HasTrait<CommonFolk>() && !otherAgent.guardSequence)
                relationship = "Loyal";

            if (___agent.HasTrait(VanillaTraits.FriendoftheFamily) && otherAgent.HasTrait<FamilyFriend>())
                relationship = "Aligned";

            if (otherAgent.HasTrait(VanillaTraits.CoolwithCannibals) && ___agent.HasTrait<CoolCannibal>())
                relationship = "Neutral";

            if (___agent.HasTrait(VanillaTraits.ScumbagSlaughterer) && otherAgent.HasTrait<Scumbag>())
			{
                relationship = "Hateful";
                otherAgent.oma.mustBeGuilty = true;
            }

            if (otherAgent.isPlayer != 0)
            {
                if (___agent.HasTrait<Player_Aligned>())
                    relationship = "Aligned";
                else if (___agent.HasTrait<Player_Annoyed>())
                    relationship = "Annoyed";
                else if (___agent.HasTrait<Player_Friendly>())
                    relationship = "Friendly";
                else if (___agent.HasTrait<Player_Hostile>())
                    relationship = "Hateful";
                else if (___agent.HasTrait<Player_Loyal>())
                    relationship = "Loyal";
                else if (___agent.HasTrait<Player_Neutral>())
                    relationship = "Neutral";
                else if (___agent.HasTrait<Player_Submissive>())
                    relationship = "Submissive";
            }

            switch (relationship)
            {
                case "":
                    break;
                case "Aligned":
                    otherAgent.relationships.SetRelInitial(___agent, "Aligned");
                    break;
                case "Annoyed":
                    __instance.SetStrikes(otherAgent, 2);
                    break;
                case "Hateful":
                    __instance.SetRel(otherAgent, "Hateful");
                    __instance.SetRelHate(otherAgent, 5);
                    otherAgent.relationships.GetRelationship(___agent).mechHate = true;
                    otherAgent.relationships.SetRelInitial(___agent, "Hateful");
                    break;
                case "Loyal":
                    otherAgent.relationships.SetRelInitial(___agent, "Loyal");
                    break;
                case "Neutral":
                    __instance.SetRelHate(otherAgent, 0);
                    __instance.SetRelInitial(otherAgent, "Neutral", true);
                    otherAgent.relationships.SetRelHate(___agent, 0);
                    otherAgent.relationships.SetRelInitial(___agent, "Neutral");
                    break;
                case "Submissive":
                    ___agent.relationships.SetRel(otherAgent, "Submissive");
                    ___agent.relationships.SetSecretHate(otherAgent, false);
                    otherAgent.relationships.SetRel(___agent, "Neutral");
                    break;
            }
        }
    }
}
