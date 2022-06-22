using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Traits.Behavior;
using CCU.Traits.Trait_Gate;
using HarmonyLib;
using JetBrains.Annotations;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace CCU.Patches.Agents
{
    [HarmonyPatch(declaringType: typeof(BrainUpdate))]
    public static class P_BrainUpdate
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, UsedImplicitly, HarmonyPatch(methodName: nameof(BrainUpdate.MyUpdate), argumentTypes: new Type[0] { })]
		private static IEnumerable<CodeInstruction> CallCustomLOSChecks(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo agent = AccessTools.DeclaredField(typeof(BrainUpdate), "agent");
			MethodInfo customLOSChecks = AccessTools.DeclaredMethod(typeof(P_BrainUpdate), nameof(P_BrainUpdate.CallCustomLOSChecks));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld, agent),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, customLOSChecks)
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld, agent),
					new CodeInstruction(OpCodes.Callvirt),
					new CodeInstruction(OpCodes.Ldstr, "Thief"),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		private static void CustomLOSChecks(Agent agent)
        {
			if (agent.agentName != VanillaAgents.CustomCharacter)
				return;

			List<string> pickupCategories = agent.GetTraits<T_Behavior>().Where(t => t.LosCheck).SelectMany(t => t.GrabItemCategories).ToList();

			if (!pickupCategories.Any())
				return;

			agent.losCheckAtIntervalsTime = 0;

			//	Item Grabbing
			if (!agent.hasEmployer)
			{
				List<Item> itemList = GC.itemList;

				for (int n = 0; n < itemList.Count; n++)
				{
					Item item = itemList[n];

					if (item.invItem.Categories.Intersect(pickupCategories).Any() &&
						!item.fellInHole && item.curTileData.prison == agent.curTileData.prison && !item.dontStealFromGround &&
						(agent.curTileData.prison <= 0 || agent.curTileData.chunkID == item.curTileData.chunkID) &&
						!GC.tileInfo.DifferentLockdownZones(agent.curTileData, item.curTileData) && agent.curPosX - 5f < item.curPosition.x && agent.curPosX + 5f > item.curPosition.x && agent.curPosY - 5f < item.curPosition.y && agent.curPosY + 5f > item.curPosition.y && agent.movement.HasLOSObjectNormal(item))
					{
						agent.SetPreviousDefaultGoal(agent.defaultGoal);
						agent.SetDefaultGoal("GoGet");
						agent.SetGoGettingTarget(item);
						agent.stoleStuff = true;
						return;
					}
				}
			}

			//	LOS Actions
			if (agent.specialAbility == vSpecialAbility.Cannibalize && agent.HasTrait<Eat_Corpses>())
			{
				agent.losCheckAtIntervalsTime = 0;

				if (!agent.hasEmployer || agent.health <= 15f)
				{
					List<Agent> deadAgentList = GC.deadAgentList;

					for (int i = 0; i < deadAgentList.Count; i++)
					{
						Agent targetAgent = deadAgentList[i];

						try
						{
							if (targetAgent.dead && !targetAgent.resurrect && !targetAgent.ghost && !targetAgent.disappeared && !targetAgent.inhuman && !targetAgent.cantCannibalize && !targetAgent.hasGettingBitByAgent && agent.prisoner == targetAgent.prisoner && agent.slaveOwners.Count == 0 &&
								(agent.prisoner <= 0 || agent.curTileData.chunkID == targetAgent.curTileData.chunkID) &&
								(!(targetAgent.agentName == VanillaAgents.Cannibal) || !targetAgent.KnockedOut()) &&
								!targetAgent.arrested && !targetAgent.invisible && !GC.tileInfo.DifferentLockdownZones(agent.curTileData, targetAgent.curTileData) && agent.curPosX - 5f < targetAgent.curPosX && agent.curPosX + 5f > targetAgent.curPosX && agent.curPosY - 5f < targetAgent.curPosY && agent.curPosY + 5f > targetAgent.curPosY && agent.movement.HasLOSAgent(targetAgent) && targetAgent.fire == null)
							{
								agent.SetPreviousDefaultGoal(agent.defaultGoal);
								agent.SetDefaultGoal("Cannibalize");
								agent.SetCannibalizingTarget(targetAgent);
								agent.losCheckAtIntervals = false;
								break;
							}
						}
						catch
						{
							Debug.LogError(string.Concat(new object[] { "Cannibalize Error: ", agent, " - ", targetAgent }));
						}
					}
				}
			}
			if (agent.specialAbility == vSpecialAbility.StickyGlove && agent.HasTrait<Pick_Pockets>() && !agent.brainUpdate.thiefNoSteal)
			{
				agent.losCheckAtIntervalsTime = 0;
				if (!agent.hasEmployer)
				{
					List<Agent> lastSawAgentList2 = GC.lastSawAgentList;
					for (int i = 0; i < agent.losCheckAtIntervalsList.Count; i++)
					{
						Agent targetAgent = agent.losCheckAtIntervalsList[i];
						Relationship relationship = agent.relationships.GetRelationship(targetAgent);

						bool honorFlag = agent.HasTrait<Honorable_Thief>() &&
							(targetAgent.statusEffects.hasTrait(VanillaTraits.HonorAmongThieves) || 
							targetAgent.statusEffects.hasTrait("HonorAmongThieves2"));

						if (relationship.distance < 4f && !honorFlag && !targetAgent.mechEmpty && !targetAgent.objectAgent && 
							(relationship.relTypeCode == relStatus.Neutral || relationship.relTypeCode == relStatus.Annoyed) &&
							agent.slaveOwners.Count == 0 && agent.prisoner == targetAgent.prisoner && !targetAgent.invisible && !targetAgent.disappeared && 
							(agent.prisoner <= 0 || agent.curTileData.chunkID == targetAgent.curTileData.chunkID) && 
							!targetAgent.hasGettingArrestedByAgent && !agent.hectoredAgents.Contains(targetAgent.agentID) && !GC.tileInfo.DifferentLockdownZones(agent.curTileData, targetAgent.curTileData))
						{
							agent.SetDefaultGoal("Steal");
							agent.SetStealingFromAgent(targetAgent);
							agent.hectoredAgents.Add(targetAgent.agentID);
							agent.losCheckAtIntervals = false;
							agent.noEnforcerAlert = true;
							agent.oma.mustBeGuilty = true;
							break;
						}
					}
				}
			}
			if (agent.specialAbility == vSpecialAbility.Bite && agent.HasTrait<Suck_Blood>())
			{
				agent.losCheckAtIntervalsTime = 0;

				if (!agent.hasEmployer || agent.health <= 15f)
				{
					List<Agent> lastSawAgentList3 = GC.lastSawAgentList;

					for (int i = 0; i < agent.losCheckAtIntervalsList.Count; i++)
					{
						Agent targetAgent = agent.losCheckAtIntervalsList[i];
						Relationship relationship2 = agent.relationships.GetRelationship(targetAgent);

						if (relationship2.distance < 5f && relationship2.relTypeCode != relStatus.Aligned && relationship2.relTypeCode != relStatus.Loyal && relationship2.relTypeCode != relStatus.Friendly && relationship2.relTypeCode != relStatus.Hostile && targetAgent.agentName != "Vampire" && !targetAgent.hasGettingBitByAgent && !targetAgent.mechEmpty && !targetAgent.mechFilled && !targetAgent.objectAgent && !targetAgent.dizzy && (targetAgent.localPlayer || targetAgent.isPlayer == 0) && agent.prisoner == targetAgent.prisoner && !targetAgent.invisible && agent.slaveOwners.Count == 0 && (agent.prisoner <= 0 || agent.curTileData.chunkID == targetAgent.curTileData.chunkID) && !targetAgent.hasGettingArrestedByAgent && !agent.hectoredAgents.Contains(targetAgent.agentID) && !GC.tileInfo.DifferentLockdownZones(agent.curTileData, targetAgent.curTileData) && !targetAgent.dead && !targetAgent.ghost && !targetAgent.hologram && !targetAgent.disappeared && !targetAgent.inhuman && !targetAgent.beast && !targetAgent.zombified)
						{
							agent.SetPreviousDefaultGoal(agent.defaultGoal);
							agent.SetDefaultGoal("Bite");
							agent.hectoredAgents.Add(targetAgent.agentID);
							agent.SetBitingTarget(targetAgent);
							agent.losCheckAtIntervals = false;
							agent.noEnforcerAlert = true;
							agent.oma.mustBeGuilty = true;
							agent.oma.hasAttacked = true;
							break;
						}
					}
				}
			}
		}
	}
}
