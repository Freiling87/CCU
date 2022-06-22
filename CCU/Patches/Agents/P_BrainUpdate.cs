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
						Agent target = deadAgentList[i];

						try
						{
							if (target.dead && !target.resurrect && !target.ghost && !target.disappeared && !target.inhuman && !target.cantCannibalize && !target.hasGettingBitByAgent && agent.prisoner == target.prisoner && agent.slaveOwners.Count == 0 &&
								(agent.prisoner <= 0 || agent.curTileData.chunkID == target.curTileData.chunkID) &&
								(!(target.agentName == VanillaAgents.Cannibal) || !target.KnockedOut()) &&
								!target.arrested && !target.invisible && !GC.tileInfo.DifferentLockdownZones(agent.curTileData, target.curTileData) && agent.curPosX - 5f < target.curPosX && agent.curPosX + 5f > target.curPosX && agent.curPosY - 5f < target.curPosY && agent.curPosY + 5f > target.curPosY && agent.movement.HasLOSAgent(target) && target.fire == null)
							{
								agent.SetPreviousDefaultGoal(agent.defaultGoal);
								agent.SetDefaultGoal("Cannibalize");
								agent.SetCannibalizingTarget(target);
								agent.losCheckAtIntervals = false;
								break;
							}
						}
						catch
						{
							Debug.LogError(string.Concat(new object[] { "Cannibalize Error: ", agent, " - ", target }));
						}
					}
				}
			}
			if (agent.specialAbility == vSpecialAbility.StickyGlove && agent.HasTrait<Pick_Pockets>() &&
				!agent.brainUpdate.thiefNoSteal)
			{
				agent.losCheckAtIntervalsTime = 0;
				if (!agent.hasEmployer)
				{
					List<Agent> lastSawAgentList2 = GC.lastSawAgentList;
					for (int num5 = 0; num5 < agent.losCheckAtIntervalsList.Count; num5++)
					{
						Agent agent6 = agent.losCheckAtIntervalsList[num5];
						Relationship relationship = agent.relationships.GetRelationship(agent6);
						bool honorFlag =
							(agent6.statusEffects.hasTrait("HonorAmongThieves") || agent6.statusEffects.hasTrait("HonorAmongThieves2")) &&
							(agent.agentName == "Thief" ||
								(agent.specialAbility == vSpecialAbility.StickyGlove && agent.HasTrait<Pick_Pockets>() && agent.HasTrait<Honorable_Thief>()));

						if (relationship.distance < 4f && !honorFlag && !agent6.mechEmpty && !agent6.objectAgent && relationship.relTypeCode != relStatus.Aligned && relationship.relTypeCode != relStatus.Loyal && relationship.relTypeCode != relStatus.Friendly && relationship.relTypeCode != relStatus.Hostile && agent.slaveOwners.Count == 0 && agent.prisoner == agent6.prisoner && !agent6.invisible && !agent6.disappeared && (agent.prisoner <= 0 || agent.curTileData.chunkID == agent6.curTileData.chunkID) && !agent6.hasGettingArrestedByAgent && !agent.hectoredAgents.Contains(agent6.agentID) && !GC.tileInfo.DifferentLockdownZones(agent.curTileData, agent6.curTileData))
						{
							agent.SetDefaultGoal("Steal");
							agent.SetStealingFromAgent(agent6);
							agent.hectoredAgents.Add(agent6.agentID);
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
						Agent agent7 = agent.losCheckAtIntervalsList[i];
						Relationship relationship2 = agent.relationships.GetRelationship(agent7);

						if (relationship2.distance < 5f && relationship2.relTypeCode != relStatus.Aligned && relationship2.relTypeCode != relStatus.Loyal && relationship2.relTypeCode != relStatus.Friendly && relationship2.relTypeCode != relStatus.Hostile && agent7.agentName != "Vampire" && !agent7.hasGettingBitByAgent && !agent7.mechEmpty && !agent7.mechFilled && !agent7.objectAgent && !agent7.dizzy && (agent7.localPlayer || agent7.isPlayer == 0) && agent.prisoner == agent7.prisoner && !agent7.invisible && agent.slaveOwners.Count == 0 && (agent.prisoner <= 0 || agent.curTileData.chunkID == agent7.curTileData.chunkID) && !agent7.hasGettingArrestedByAgent && !agent.hectoredAgents.Contains(agent7.agentID) && !GC.tileInfo.DifferentLockdownZones(agent.curTileData, agent7.curTileData) && !agent7.dead && !agent7.ghost && !agent7.hologram && !agent7.disappeared && !agent7.inhuman && !agent7.beast && !agent7.zombified)
						{
							agent.SetPreviousDefaultGoal(agent.defaultGoal);
							agent.SetDefaultGoal("Bite");
							agent.hectoredAgents.Add(agent7.agentID);
							agent.SetBitingTarget(agent7);
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
