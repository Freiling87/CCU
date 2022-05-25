using BepInEx.Logging;
using CCU.Traits;
using CCU.Traits.Combat;
using CCU.Traits.Rel_Faction;
using CCU.Traits.Rel_General;
using CCU.Traits.Rel_Player;
using CCU.Traits.Trait_Gate;
using HarmonyLib;
using RogueLibsCore;
using System;

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

        [HarmonyPostfix, HarmonyPatch(methodName: nameof(Relationships.SetupRelationshipOriginal), argumentTypes: new[] { typeof(Agent) })]
        public static void SetupRelationshipOriginal_Postfix(Agent otherAgent, Relationships __instance, Agent ___agent)
		{
            string relationship = "";

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
                else if (___agent.HasTrait<Player_Secret_Hate>())
                    relationship = "SecretHate"; 
                else if (___agent.HasTrait<Player_Submissive>())
                    relationship = "Submissive";
            }

            if ((___agent.HasTrait<Hostile_To_Cannibal>() && (otherAgent.agentName == VanillaAgents.Cannibal || otherAgent.HasTrait<Cool_Cannibal>())) ||
                (___agent.HasTrait<Hostile_To_Soldier>() && otherAgent.agentName == VanillaAgents.Soldier) ||
                (___agent.HasTrait<Hostile_To_Vampire>() && otherAgent.agentName == VanillaAgents.Vampire) ||
                (___agent.HasTrait<Hostile_To_Werewolf>() && otherAgent.agentName == VanillaAgents.WerewolfTransformed) ||
                (___agent.HasTrait<Bashable>() && (otherAgent.agentName == VanillaAgents.GangsterCrepe || otherAgent.HasTrait(VanillaTraits.BlahdBasher))) ||
                (___agent.HasTrait<Crushable>() && (otherAgent.agentName == VanillaAgents.GangsterBlahd || otherAgent.HasTrait(VanillaTraits.CrepeCrusher))) ||
                (___agent.HasTrait<Slayable>() && (otherAgent.agentName == VanillaAgents.Gorilla || otherAgent.HasTrait("HatesScientist"))) ||
                (___agent.HasTrait<Scumbag>() && otherAgent.HasTrait(VanillaTraits.ScumbagSlaughterer)) ||
                (___agent.HasTrait<Specistist>() && (otherAgent.agentName == VanillaAgents.Scientist || otherAgent.HasTrait(VanillaTraits.Specist))) ||
                AgentsFactionHostile(___agent, otherAgent))
                relationship = "Hateful";

            if (___agent.HasTrait<Suspecter>() && ___agent.ownerID != 0 && ___agent.startingChunkRealDescription != "DeportationCenter" && __instance.GetRel(otherAgent) == "Neutral" && otherAgent.statusEffects.hasTrait(VanillaTraits.Suspicious) && ___agent.ownerID > 0 && (!__instance.QuestInvolvement(___agent) || otherAgent.isPlayer == 0))
                relationship = "Annoyed";

            if (otherAgent.HasTrait(VanillaTraits.CoolwithCannibals) && ___agent.HasTrait<Cool_Cannibal>())
                relationship = "Neutral";

            if (___agent.HasTrait(VanillaTraits.FriendoftheCommonFolk) && otherAgent.HasTrait<Common_Folk>() && !otherAgent.guardSequence)
                relationship = "Loyal";

            if ((___agent.HasTrait<Bashable>() && otherAgent.agentName == VanillaAgents.GangsterBlahd) ||
                (___agent.HasTrait<Crushable>() && otherAgent.agentName == VanillaAgents.GangsterCrepe) ||
                (___agent.HasTrait<Family_Friend>() && (otherAgent.agentName == VanillaAgents.Mobster || otherAgent.HasTrait(VanillaTraits.FriendoftheFamily))) ||
                AgentsFactionAligned(___agent, otherAgent))
                relationship = "Aligned";

            switch (relationship)
            {
                case "":
                    break;
                case "Aligned":
                    __instance.SetRelInitial(otherAgent, "Aligned");
                    __instance.SetSecretHate(otherAgent, false);
                    otherAgent.relationships.SetRelInitial(___agent, "Aligned");
                    break;
                case "Annoyed":
                    __instance.SetStrikes(otherAgent, 2);
                    break;
                case "Friendly":
                    __instance.SetRel(otherAgent, "Friendly");
                    __instance.SetSecretHate(otherAgent, false);
                    break;
                case "Hateful":
                    __instance.SetRel(otherAgent, "Hateful");
                    __instance.SetRelHate(otherAgent, 5);
                    otherAgent.relationships.GetRelationship(___agent).mechHate = true;
                    otherAgent.relationships.SetRelInitial(___agent, "Hateful");
                    break;
                case "Loyal":
                    __instance.SetRelInitial(otherAgent, "Loyal");
                    __instance.SetSecretHate(otherAgent, false);
                    otherAgent.relationships.SetRelInitial(___agent, "Loyal");
                    otherAgent.relationships.SetRelHate(___agent, 0);
                    break;
                case "Neutral":
                    __instance.SetRelHate(otherAgent, 0);
                    __instance.SetRelInitial(otherAgent, "Neutral");
                    __instance.SetSecretHate(otherAgent, false);
                    otherAgent.relationships.SetRelInitial(___agent, "Neutral");
                    otherAgent.relationships.SetRelHate(___agent, 0);
                    break;
                case "SecretHate":
                    otherAgent.relationships.SetSecretHate(___agent, true);
                    otherAgent.choseSecretHate = true;
                    otherAgent.hasSecretHate = true;
                    otherAgent.cantBeBountyHunter = false;
                    break;
                case "Submissive":
                    __instance.SetRel(otherAgent, "Submissive");
                    __instance.SetSecretHate(otherAgent, false);
                    otherAgent.relationships.SetRel(___agent, "Neutral");
                    break;
            }
        }

        private static bool AgentsFactionAligned(Agent agent, Agent otherAgent) =>
            (agent.HasTrait<Faction_1_Aligned>() && otherAgent.HasTrait<Faction_1_Aligned>()) ||
            (agent.HasTrait<Faction_2_Aligned>() && otherAgent.HasTrait<Faction_2_Aligned>()) ||
            (agent.HasTrait<Faction_3_Aligned>() && otherAgent.HasTrait<Faction_3_Aligned>()) ||
            (agent.HasTrait<Faction_4_Aligned>() && otherAgent.HasTrait<Faction_4_Aligned>());

        private static bool AgentsFactionHostile(Agent agent, Agent otherAgent) =>
            (agent.HasTrait<Faction_1_Hostile>() && otherAgent.HasTrait<Faction_1_Aligned>()) ||
            (agent.HasTrait<Faction_1_Aligned>() && otherAgent.HasTrait<Faction_1_Hostile>()) ||
            (agent.HasTrait<Faction_2_Hostile>() && otherAgent.HasTrait<Faction_2_Aligned>()) ||
            (agent.HasTrait<Faction_2_Aligned>() && otherAgent.HasTrait<Faction_2_Hostile>()) ||
            (agent.HasTrait<Faction_3_Hostile>() && otherAgent.HasTrait<Faction_3_Aligned>()) ||
            (agent.HasTrait<Faction_3_Aligned>() && otherAgent.HasTrait<Faction_3_Hostile>()) ||
            (agent.HasTrait<Faction_4_Hostile>() && otherAgent.HasTrait<Faction_4_Aligned>()) ||
            (agent.HasTrait<Faction_4_Aligned>() && otherAgent.HasTrait<Faction_4_Hostile>());
    }
}
