using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Traits.Cost;
using CCU.Traits.Hack;
using CCU.Traits.Hire_Type;
using CCU.Traits.Interaction;
using CCU.Traits.Merchant_Type;
using CCU.Traits.Passive;
using CCU.Traits.Trait_Gate;
using HarmonyLib;
using JetBrains.Annotations;
using RogueLibsCore;
using SORCE.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace CCU.Patches.Agents
{
	[HarmonyPatch(declaringType: typeof(AgentInteractions))]
	static class P_AgentInteractions_DetermineButtons
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, UsedImplicitly, HarmonyPatch(methodName: nameof(AgentInteractions.DetermineButtons), argumentTypes: new[] { typeof(Agent), typeof(Agent), typeof(List<string>), typeof(List<string>), typeof(List<int>) })]
        private static IEnumerable<CodeInstruction> DetermineButtons(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo ACAB = AccessTools.DeclaredMethod(typeof(P_AgentInteractions_DetermineButtons), nameof(AddCustomAgentButtons));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{ 
					new CodeInstruction(OpCodes.Ldstr, "AgentTalk"),
					new CodeInstruction(OpCodes.Callvirt),
					new CodeInstruction(OpCodes.Ldarg_1),
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Ldarg_2),
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Callvirt),
					new CodeInstruction(OpCodes.Br),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_1),				// agent
					new CodeInstruction(OpCodes.Call, ACAB)				// void
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
					//	string agentName = agent.agentName;
					//	uint num = < PrivateImplementationDetails >.ComputeStringHash(agentName);
					//	if (num <= 1634587502U)
					new CodeInstruction(OpCodes.Ldarg_1),
					new CodeInstruction(OpCodes.Callvirt),
					new CodeInstruction(OpCodes.Stloc_S, 15),
					new CodeInstruction(OpCodes.Ldloc_S, 15),
					new CodeInstruction(OpCodes.Brfalse),
					new CodeInstruction(OpCodes.Ldloc_S, 15),
					new CodeInstruction(OpCodes.Call),
					new CodeInstruction(OpCodes.Stloc_S, 16),
					new CodeInstruction(OpCodes.Ldloc_S, 16),
					new CodeInstruction(OpCodes.Ldc_I4)
					// Can't use the UInt here since it is automatically generated and may change between versions
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		public static void AddCustomAgentButtons(Agent agent)
        {
			logger.LogDebug("AddCustomAgentInteractionButtons");
			logger.LogDebug("=" + agent.agentName + "=");

			if (agent.agentName != "Custom")
				return;

			AgentInteractions agentInteractions = agent.agentInteractions;
			Agent interactingAgent = agent.interactingAgent;
			string relationship = agent.relationships.GetRel(interactingAgent);

			// Hire 
			if (agent.GetTraits<T_HireType>().Any())
			{
				if (agent.employer == null && agent.relationships.GetRelCode(interactingAgent) != relStatus.Annoyed)
				{
					if ((agent.HasTrait<Bashable>() && (interactingAgent.agentName == VanillaAgents.GangsterBlahd || (interactingAgent.agentName == "Gangbanger" && interactingAgent.oma.superSpecialAbility))) ||
						(agent.HasTrait<Crushable>() && (interactingAgent.agentName == VanillaAgents.GangsterCrepe || (interactingAgent.agentName == "Gangbanger" && interactingAgent.oma.superSpecialAbility))))
						agentInteractions.AddButton(VButtonText.JoinMe);
                    else
					{
						string hireButtonText =
							agent.GetTraits<T_HireType>().Where(t => t.HireButtonText == VButtonText.Hire_Muscle).Any()
							? VButtonText.Hire_Muscle
							: VButtonText.Hire_Expert;

						string cost =
							agent.GetTraits<T_HireType>().Where(t => t.HireButtonText == VButtonText.Hire_Muscle).Any()
							? VDetermineMoneyCost.Hire_Soldier
							: VDetermineMoneyCost.Hire_Hacker;

						agentInteractions.AddButton(hireButtonText, agent.determineMoneyCost((string)cost));

						if (interactingAgent.inventory.HasItem(vItem.HiringVoucher))
							agentInteractions.AddButton(hireButtonText, 6666);
					}
				}
				else if (!agent.oma.cantDoMoreTasks) // Ordering already-hired Agent
				{
					foreach (T_HireType hiredTrait in agent.GetTraits<T_HireType>().Where(t => t.HiredActionButtonText != null))
						agentInteractions.AddButton(hiredTrait.HiredActionButtonText);
				}
			}

			//// Hack
			//if (interactingAgent.interactionHelper.interactingFar)
			//	foreach (T_Hack hack in agent.GetTraits<T_Hack>())
			//		agentInteractions.AddButton(hack.ButtonText);

			// Interaction 
			foreach (T_Interaction interaction in agent.GetTraits<T_Interaction>())
			{
				// Simple Exceptions
				if ((interaction is Borrow_Money_Moocher && !interactingAgent.statusEffects.hasTrait(VanillaTraits.Moocher)) ||
					(interaction is Bribe_Cops && (interactingAgent.aboveTheLaw || !interactingAgent.statusEffects.hasTrait(VanillaTraits.CorruptionCosts))) ||
					((interaction is Bribe_for_Entry || interaction is Bribe_for_Entry_Alcohol) && (relationship == "Aligned" || relationship == "Loyal" || relationship == "Submissive" || interactingAgent.agentName == "Cop2")) ||
					(interaction is Influence_Election && agent.gc.sessionData.electionBribedMob[interactingAgent.isPlayer]) ||
					(interaction is Leave_Weapons_Behind && !interactingAgent.inventory.HasWeapons()) ||
					(interaction is Offer_Motivation && agent.oma.offeredOfficeDrone) ||
					(interaction is Pay_Debt && !interactingAgent.statusEffects.hasStatusEffect(VanillaEffects.InDebt1)
											&& !interactingAgent.statusEffects.hasStatusEffect(VanillaEffects.InDebt2)
											&& !interactingAgent.statusEffects.hasStatusEffect(VanillaEffects.InDebt3)) ||
					(interaction is UseBloodBag && !interactingAgent.inventory.HasItem(vItem.BloodBag)))
					continue;

				// Complex Exceptions
				if (interaction is Buy_Round)
				{
					List<Agent> patrons = new List<Agent>();
					for (int i = 0; i < agent.gc.agentList.Count; i++)
					{
						Agent possiblePatron = agent.gc.agentList[i];

						if (possiblePatron.startingChunk == agent.startingChunk && possiblePatron != agent && possiblePatron.prisoner == 0 && !possiblePatron.dead && !possiblePatron.zombified && !possiblePatron.oma.mindControlled && possiblePatron.gc.tileInfo.GetTileData(possiblePatron.tr.position).chunkID == possiblePatron.startingChunk)
						{
							if (relationship != "Hateful" && relationship != "Aligned" && relationship != "Loyal" && relationship != "Submissive" &&
								!possiblePatron.statusEffects.hasTrait("BloodRestoresHealth") && !possiblePatron.statusEffects.hasTrait("OilRestoresHealth"))
								patrons.Add(possiblePatron);
						}
					}

					if (!patrons.Any())
						continue;
				}
				else if (interaction is Buy_Slave)
				{
					bool hasSlaves = false;

					for (int i = 0; i < agent.gc.agentList.Count; i++)
					{
						Agent possibleSlave = agent.gc.agentList[i];

						if (possibleSlave.agentName == "Slave" && possibleSlave.slaveOwners.Contains(agent) && (possibleSlave.rescueForQuest != null || (!possibleSlave.gc.serverPlayer && possibleSlave.oma.rescuingForQuest)))
						{
							hasSlaves = true;
							break;
						}
					}

					if (!hasSlaves)
						continue;
				}
				else if (interaction is Play_Bad_Music)
				{
					bool turntables = false;
					foreach (ObjectReal objectReal in GC.objectRealList)
					{
						if (objectReal.objectName == vObject.Turntables && objectReal.startingChunk == agent.startingChunk && !objectReal.destroyed && objectReal.functional && Vector2.Distance(objectReal.tr.position, agent.tr.position) < 1.28f)
							turntables = true;
					}

					if (!turntables)
						continue;
				}

				agentInteractions.AddButton(interaction.ButtonText, agent.determineMoneyCost(interaction.ButtonText));
			}

			// Merchant 
			if (agent.GetTraits<T_MerchantType>().Any() && agent.hasSpecialInvDatabase)
			{
				Core.LogCheckpoint("Vendor");
				logger.LogDebug("\tCount: " + agent.specialInvDatabase.InvItemList.Count);

				bool cantBuy =
					(agent.HasTrait<Cool_Cannibal>() && !interactingAgent.statusEffects.hasTrait("CannibalsNeutral") && interactingAgent.agentName != VanillaAgents.Cannibal) ||
					(agent.HasTrait<Cop_Access>() && !interactingAgent.HasTrait("TheLaw") && interactingAgent.agentName != VanillaAgents.Cop && interactingAgent.agentName != VanillaAgents.CopBot && interactingAgent.agentName != VanillaAgents.SuperCop) ||
					(agent.HasTrait<Honorable_Thief>() && !interactingAgent.statusEffects.hasTrait("HonorAmongThieves") && interactingAgent.statusEffects.hasTrait("HonorAmongThieves2"));

				if (!cantBuy)
				{
					agentInteractions.AddButton("Buy");

					if (interactingAgent.inventory.HasItem("FreeItemVoucher"))
						agentInteractions.AddButton("UseVoucher");
				}
			}

			// Passive
			if (agent.HasTrait<Extortable>() && agent.CanShakeDown() && (interactingAgent.HasTrait("Shakedowner") || interactingAgent.HasTrait("Shakedowner")))
			{
				int threat = agent.relationships.FindThreat(interactingAgent, false);

				if ((agent.health <= agent.healthMax * 0.4f) ||
					(agent.relationships.GetRel(interactingAgent) == "Aligned") ||
					(agent.slaveOwners.Contains(interactingAgent)))
					threat = 100;

				agentInteractions.AddButton("Shakedown", " (" + threat + "%)");
			}
		}

		// Non-Patch
		public static void SafecrackSafe(Agent agent, Agent interactingAgent, PlayfieldObject mySafe)
		{
			if (GC.serverPlayer)
			{
				agent.job = CJob.SafecrackSafe;
				agent.jobCode = jobType.GetSupplies; // TODO
				agent.StartCoroutine(agent.ChangeJobBig(""));
				agent.assignedPos = mySafe.GetComponent<ObjectReal>().FindDoorObjectAgentPos();
				agent.assignedObject = mySafe.playfieldObjectReal;
				agent.assignedAgent = null;
				GC.audioHandler.Play(agent, "AgentOK");

				return;
			}

			interactingAgent.objectMult.CallCmdObjectActionExtraObjectID(agent.objectNetID, CJob.SafecrackSafe, mySafe.objectNetID);
		}

		// Non-Patch
		public static void TamperSomething(Agent agent, Agent interactingAgent, PlayfieldObject target)
		{
			if (GC.serverPlayer)
			{
				agent.job = CJob.SafecrackSafe;
				agent.jobCode = jobType.GetSupplies; // TODO
				agent.StartCoroutine(agent.ChangeJobBig(""));
				agent.assignedPos = target.GetComponent<ObjectReal>().FindDoorObjectAgentPos();
				agent.assignedObject = target.playfieldObjectReal;
				agent.assignedAgent = null;
				GC.audioHandler.Play(agent, "AgentOK");

				return;
			}

			interactingAgent.objectMult.CallCmdObjectActionExtraObjectID(agent.objectNetID, CJob.TamperSomething, target.objectNetID);
		}
	}
}
