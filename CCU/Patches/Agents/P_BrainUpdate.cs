using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using BunnyLibs;
using CCU.Traits.Behavior;
using CCU.Traits.Trait_Gate;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace CCU.Patches.Agents
{
	[HarmonyPatch(typeof(BrainUpdate))]
	public static class P_BrainUpdate_MyUpdate
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(typeof(BrainUpdate), nameof(BrainUpdate.MyUpdate))]
		private static IEnumerable<CodeInstruction> CallCustomLOSChecks(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo agent = AccessTools.DeclaredField(typeof(BrainUpdate), "agent");
			MethodInfo customLOSChecks = AccessTools.DeclaredMethod(typeof(P_BrainUpdate_MyUpdate), nameof(P_BrainUpdate_MyUpdate.CustomLOSChecks), new[] { typeof(Agent) });

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ble),
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld, agent),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, customLOSChecks),
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld, agent),
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Callvirt),
					new CodeInstruction(OpCodes.Ldstr, "Thief"),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		private static void CustomLOSChecks(Agent LOSagent)
		{
			if (LOSagent.agentName != VanillaAgents.CustomCharacter ||
				LOSagent.losCheckAtIntervalsTime < 7)
				return;

			List<string> pickupCategories = null;

			try
			{
				pickupCategories = LOSagent.GetTraits<T_Behavior>().Where(t => !(t.GrabItemCategories is null)).SelectMany(t => t.GrabItemCategories).ToList();
			}
			catch (Exception message)
			{
				logger.LogDebug("Error: " + message);
			}

			LOSagent.losCheckAtIntervalsTime = 0;

			//	Item Grabbing
			if (LOSagent.agentInvDatabase.hasEmptySlot() &&
				!LOSagent.hasEmployer && pickupCategories.Any())
			{
				List<Item> itemList = GC.itemList;

				for (int n = 0; n < itemList.Count; n++)
				{
					Item item = itemList[n];

					if (item.invItem.Categories.Intersect(pickupCategories).Any() &&
						(!item.objectSprite.dangerous || LOSagent.HasTrait<Accident_Prone>()) &&
						!item.fellInHole && item.curTileData.prison == LOSagent.curTileData.prison && !item.dontStealFromGround &&
						(LOSagent.curTileData.prison <= 0 || LOSagent.curTileData.chunkID == item.curTileData.chunkID) &&
						!GC.tileInfo.DifferentLockdownZones(LOSagent.curTileData, item.curTileData) && LOSagent.curPosX - 5f < item.curPosition.x && LOSagent.curPosX + 5f > item.curPosition.x && LOSagent.curPosY - 5f < item.curPosition.y && LOSagent.curPosY + 5f > item.curPosition.y && LOSagent.movement.HasLOSObjectNormal(item))
					{
						LOSagent.SetPreviousDefaultGoal(LOSagent.defaultGoal);
						LOSagent.SetDefaultGoal("GoGet");
						LOSagent.SetGoGettingTarget(item);
						LOSagent.stoleStuff = true;
						return; // Try a break here
					}
				}
			}

			//	LOS Actions
			if (LOSagent.specialAbility == VanillaAbilities.Cannibalize && LOSagent.HasTrait<Eat_Corpses>())
			{
				if (!LOSagent.hasEmployer || LOSagent.health <= 15f)
				{
					List<Agent> deadAgentList = GC.deadAgentList;

					for (int i = 0; i < deadAgentList.Count; i++)
					{
						Agent targetAgent = deadAgentList[i];

						try
						{
							if (targetAgent.dead && !targetAgent.resurrect && !targetAgent.ghost && !targetAgent.disappeared && !targetAgent.inhuman && !targetAgent.cantCannibalize && !targetAgent.hasGettingBitByAgent && LOSagent.prisoner == targetAgent.prisoner && LOSagent.slaveOwners.Count == 0 &&
								(LOSagent.prisoner <= 0 || LOSagent.curTileData.chunkID == targetAgent.curTileData.chunkID) &&
								(!(targetAgent.agentName == VanillaAgents.Cannibal) || !targetAgent.KnockedOut()) &&
								!targetAgent.arrested && !targetAgent.invisible && !GC.tileInfo.DifferentLockdownZones(LOSagent.curTileData, targetAgent.curTileData) && LOSagent.curPosX - 5f < targetAgent.curPosX && LOSagent.curPosX + 5f > targetAgent.curPosX && LOSagent.curPosY - 5f < targetAgent.curPosY && LOSagent.curPosY + 5f > targetAgent.curPosY && LOSagent.movement.HasLOSAgent(targetAgent) && targetAgent.fire == null)
							{
								LOSagent.SetPreviousDefaultGoal(LOSagent.defaultGoal);
								LOSagent.SetDefaultGoal("Cannibalize");
								LOSagent.SetCannibalizingTarget(targetAgent);
								LOSagent.losCheckAtIntervals = false;
								break;
							}
						}
						catch
						{
							Debug.LogError(string.Concat(new object[] { "Cannibalize Error: ", LOSagent, " - ", targetAgent }));
						}
					}
				}
			}
			if (LOSagent.specialAbility == VanillaAbilities.StickyGlove && LOSagent.HasTrait<Pick_Pockets>() && !LOSagent.brainUpdate.thiefNoSteal)
			{
				if (!LOSagent.hasEmployer)
				{
					List<Agent> lastSawAgentList2 = GC.lastSawAgentList;

					for (int i = 0; i < LOSagent.losCheckAtIntervalsList.Count; i++)
					{
						Agent targetAgent = LOSagent.losCheckAtIntervalsList[i];
						Relationship relationship = LOSagent.relationships.GetRelationship(targetAgent);

						bool honorFlag = LOSagent.HasTrait<Honorable_Thief>() &&
							(targetAgent.statusEffects.hasTrait(VanillaTraits.HonorAmongThieves) ||
							targetAgent.statusEffects.hasTrait("HonorAmongThieves2"));

						if (relationship.distance < 4f && !honorFlag && !targetAgent.mechEmpty && !targetAgent.objectAgent &&
							(relationship.relTypeCode == relStatus.Neutral || relationship.relTypeCode == relStatus.Annoyed) &&
							LOSagent.slaveOwners.Count == 0 && LOSagent.prisoner == targetAgent.prisoner && !targetAgent.invisible && !targetAgent.disappeared &&
							(LOSagent.prisoner <= 0 || LOSagent.curTileData.chunkID == targetAgent.curTileData.chunkID) &&
							!targetAgent.hasGettingArrestedByAgent && !LOSagent.hectoredAgents.Contains(targetAgent.agentID) && !GC.tileInfo.DifferentLockdownZones(LOSagent.curTileData, targetAgent.curTileData))
						{
							LOSagent.SetDefaultGoal("Steal");
							LOSagent.SetStealingFromAgent(targetAgent);
							LOSagent.hectoredAgents.Add(targetAgent.agentID);
							LOSagent.losCheckAtIntervals = false;
							LOSagent.noEnforcerAlert = true;
							LOSagent.oma.mustBeGuilty = true;
							break;
						}
					}
				}
			}
			if (LOSagent.specialAbility == VanillaAbilities.Bite && LOSagent.HasTrait<Suck_Blood>())
			{
				if (!LOSagent.hasEmployer || LOSagent.health <= 15f)
				{
					List<Agent> lastSawAgentList3 = GC.lastSawAgentList;

					for (int i = 0; i < LOSagent.losCheckAtIntervalsList.Count; i++)
					{
						Agent targetAgent = LOSagent.losCheckAtIntervalsList[i];
						Relationship relationship2 = LOSagent.relationships.GetRelationship(targetAgent);

						if (relationship2.distance < 5f && relationship2.relTypeCode != relStatus.Aligned && relationship2.relTypeCode != relStatus.Loyal && relationship2.relTypeCode != relStatus.Friendly && relationship2.relTypeCode != relStatus.Hostile && targetAgent.agentName != "Vampire" && !targetAgent.hasGettingBitByAgent && !targetAgent.mechEmpty && !targetAgent.mechFilled && !targetAgent.objectAgent && !targetAgent.dizzy && (targetAgent.localPlayer || targetAgent.isPlayer == 0) && LOSagent.prisoner == targetAgent.prisoner && !targetAgent.invisible && LOSagent.slaveOwners.Count == 0 && (LOSagent.prisoner <= 0 || LOSagent.curTileData.chunkID == targetAgent.curTileData.chunkID) && !targetAgent.hasGettingArrestedByAgent && !LOSagent.hectoredAgents.Contains(targetAgent.agentID) && !GC.tileInfo.DifferentLockdownZones(LOSagent.curTileData, targetAgent.curTileData) && !targetAgent.dead && !targetAgent.ghost && !targetAgent.hologram && !targetAgent.disappeared && !targetAgent.inhuman && !targetAgent.beast && !targetAgent.zombified)
						{
							LOSagent.SetPreviousDefaultGoal(LOSagent.defaultGoal);
							LOSagent.SetDefaultGoal("Bite");
							LOSagent.hectoredAgents.Add(targetAgent.agentID);
							LOSagent.SetBitingTarget(targetAgent);
							LOSagent.losCheckAtIntervals = false;
							LOSagent.noEnforcerAlert = true;
							LOSagent.oma.mustBeGuilty = true;
							LOSagent.oma.hasAttacked = true;
							break;
						}
					}
				}
			}
		}

		[HarmonyPrefix, HarmonyPatch(nameof(BrainUpdate.MyUpdate))]
		public static bool MyUpdate_Prefix(Agent ___agent)
		{
			if (___agent.HasTrait<Brainless>())
				return false;

			return true;
		}
	}
}