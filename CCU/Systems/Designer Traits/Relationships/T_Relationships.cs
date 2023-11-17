using BepInEx.Logging;
using CCU.Traits.Rel_Faction;
using CCU.Traits.Rel_Player;
using CCU.Traits.Trait_Gate;
using HarmonyLib;
using RogueLibsCore;
using System.Linq;
using static CCU.Traits.Rel_Faction.T_Rel_Faction;

namespace CCU.Traits
{
	// TODO: BunnyLibs.ISetupRelationshipOriginal
	public abstract class T_Relationships : T_DesignerTrait
	{
		public T_Relationships() : base() { }

		public abstract string GetRelationshipTo(Agent agent);
	}

	[HarmonyPatch(typeof(Relationships))]
	public static class P_Relationships
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(nameof(Relationships.SetupRelationshipOriginal))]
		public static void SetupRelationshipOriginal_Postfix(Agent otherAgent, Relationships __instance, Agent ___agent)
		{
			if (GC.levelType == "HomeBase")
				return;

			string relationship = null;

			#region Factions
			//  Factions 1-4
			if (___agent.GetTraits<T_Rel_Faction>().Any(t => t.Faction != 0) && otherAgent.GetTraits<T_Rel_Faction>().Any(t => t.Faction != 0))
			{
				Alignment rel = AlignmentUtils.GetAverageAlignment(___agent, otherAgent);

				if (rel != Alignment.Neutral)
					relationship = rel.ToString();
			}

			// Non-numbered factions
			// For why Basher/Crusher don't make mutually hostile: https://discord.com/channels/187414758536773632/991046848536006678/1063173054257168424
			foreach (T_Rel_Faction trait in ___agent.GetTraits<T_Rel_Faction>().Where(t => t.Faction == 0))
			{
				relationship = trait.GetRelationshipTo(otherAgent) ?? relationship;
			}
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
					__instance.SetRelHate(otherAgent, 0);
					__instance.SetSecretHate(otherAgent, false);

					if (mutual)
					{
						otherAgent.relationships.SetRelInitial(agent, VRelationship.Aligned);
						otherAgent.relationships.SetRelHate(agent, 0);
						otherAgent.relationships.SetSecretHate(agent, false);
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

	[HarmonyPatch(typeof(StatusEffects))]
	class P_StatusEffects
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(nameof(StatusEffects.AgentIsRival))]
		public static bool AgentIsRival_Prefix(Agent myAgent, StatusEffects __instance, ref bool __result)
		{
			Agent agent = __instance.agent;

			if
			(
				(agent.HasTrait(VanillaTraits.BlahdBasher) && AlignmentUtils.CountsAsBlahd(myAgent)) ||
				(agent.HasTrait(VanillaTraits.CrepeCrusher) && AlignmentUtils.CountsAsCrepe(myAgent)) ||
				(agent.HasTrait("HatesScientist") && myAgent.HasTrait<Slayable>()) ||
				(agent.HasTrait(VanillaTraits.Specist) && myAgent.HasTrait<Specistist>()))
			{
				__result = true;
				return false;
			}

			return true;
		}
	}
}