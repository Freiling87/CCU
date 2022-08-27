using BepInEx.Logging;
using BTHarmonyUtils;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Localization;
using CCU.Traits.Behavior;
using CCU.Traits.Cost_Scale;
using CCU.Traits.Hire_Duration;
using CCU.Traits.Hire_Type;
using CCU.Traits.Interaction;
using CCU.Traits.Interaction_Gate;
using CCU.Traits.Language;
using CCU.Traits.Merchant_Type;
using CCU.Traits.Passive;
using CCU.Traits.Rel_Faction;
using CCU.Traits.Trait_Gate;
using HarmonyLib;
using JetBrains.Annotations;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace CCU.Patches.Agents
{
    [HarmonyPatch(declaringType: typeof(AgentInteractions))]
	static class P_AgentInteractions
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		// TODO: Replace with Roguelibs' Custom Interactions system
		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(AgentInteractions.DetermineButtons), 
			argumentTypes: new[] { typeof(Agent), typeof(Agent), typeof(List<string>), typeof(List<string>), typeof(List<int>) })]
        private static IEnumerable<CodeInstruction> DetermineButtons_AddCustomAgentButtons(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo addCustomAgentButtons = AccessTools.DeclaredMethod(typeof(P_AgentInteractions), nameof(AddCustomAgentButtons));

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
					new CodeInstruction(OpCodes.Call, addCustomAgentButtons)				// void
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
			AgentInteractions agentInteractions = agent.agentInteractions;
			Agent interactingAgent = agent.interactingAgent;
			string relationship = agent.relationships.GetRel(interactingAgent);
			int relationshipLevel = VRelationship.GetRelationshipLevel(relationship);

			if (agent.agentName != VanillaAgents.CustomCharacter)
				return;

			T_InteractionGate trustTrait = agent.GetTraits<T_InteractionGate>().Where(t => t.MinimumRelationship > 0).FirstOrDefault(); // Should only ever be one

			bool untrusted = trustTrait is null
				? false
				: relationshipLevel < trustTrait.MinimumRelationship;

			//// Hack
			//if (interactingAgent.interactionHelper.interactingFar)
			//	foreach (T_Hack hack in agent.GetTraits<T_Hack>())
			//		agentInteractions.AddButton(hack.ButtonText);

			// Hire 
			if (!untrusted && agent.GetTraits<T_HireType>().Any())
			{
				if (agent.employer == null && agent.relationships.GetRelCode(interactingAgent) != relStatus.Annoyed)
				{
					if ((agent.HasTrait<Faction_Blahd_Aligned>() &&
						(interactingAgent.agentName == VanillaAgents.GangsterBlahd ||
							(interactingAgent.agentName == VanillaAgents.GangsterBlahd && interactingAgent.oma.superSpecialAbility))) ||
						(agent.HasTrait<Faction_Crepe_Aligned>() &&
						(interactingAgent.agentName == VanillaAgents.GangsterCrepe ||
							(interactingAgent.agentName == VanillaAgents.GangsterCrepe && interactingAgent.oma.superSpecialAbility))))
						agentInteractions.AddButton(VButtonText.JoinMe);
					else
					{
						string hireButtonText =
							agent.GetTraits<T_HireType>().Where(t => t.ButtonText == VButtonText.Hire_Muscle).Any()
								? VButtonText.Hire_Muscle
								: VButtonText.Hire_Expert;

						string costString =
							agent.GetTraits<T_HireType>().Where(t => t.ButtonText == VButtonText.Hire_Muscle).Any()
								? VDetermineMoneyCost.Hire_Soldier 
								: VDetermineMoneyCost.Hire_Hacker;

						if (agent.HasTrait<Permanent_Hire_Only>())
							agentInteractions.AddButton(hireButtonText + "_Permanent", agent.determineMoneyCost(costString + "_Permanent"));
						else if (agent.HasTrait<Permanent_Hire>())
                        {
							agentInteractions.AddButton(hireButtonText, agent.determineMoneyCost(costString));
							agentInteractions.AddButton(hireButtonText + "_Permanent", agent.determineMoneyCost(costString + "_Permanent"));

							if (interactingAgent.inventory.HasItem(vItem.HiringVoucher))
								agentInteractions.AddButton(hireButtonText, 6666);
						}
						else // Vanilla Hire
                        {
							agentInteractions.AddButton(hireButtonText, agent.determineMoneyCost(costString));
							
							if (interactingAgent.inventory.HasItem(vItem.HiringVoucher))
								agentInteractions.AddButton(hireButtonText, 6666);
						}
					}
				}
				else if (!agent.oma.cantDoMoreTasks) // Ordering already-hired Agent
				{
					foreach (T_HireType hiredTrait in agent.GetTraits<T_HireType>().Where(t => t.HiredActionButtonText != null))
						agentInteractions.AddButton(hiredTrait.HiredActionButtonText);
				}
			}

			// Interaction 
			foreach (T_Interaction interaction in agent.GetTraits<T_Interaction>())
			{
				// Simple Exceptions
				if ((!(interaction is Offer_Motivation || interaction is Leave_Weapons_Behind || interaction is Pay_Debt || interaction is Pay_Entrance_Fee) && untrusted) ||
					(interaction is Borrow_Money_Moocher && !interactingAgent.statusEffects.hasTrait(VanillaTraits.Moocher)) ||
					(interaction is Influence_Election && agent.gc.sessionData.electionBribedMob[interactingAgent.isPlayer]) ||
					(interaction is Leave_Weapons_Behind && !interactingAgent.inventory.HasWeapons()) ||
					(interaction is Offer_Motivation && agent.oma.offeredOfficeDrone) ||
					(interaction is Pay_Debt && !interactingAgent.statusEffects.hasStatusEffect(VanillaEffects.InDebt1)
											&& !interactingAgent.statusEffects.hasStatusEffect(VanillaEffects.InDebt2)
											&& !interactingAgent.statusEffects.hasStatusEffect(VanillaEffects.InDebt3)) ||
					(interaction is Use_Blood_Bag && !interactingAgent.inventory.HasItem(vItem.BloodBag)))
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
							if (relationship != VRelationship.Hostile && relationship != VRelationship.Aligned && relationship != VRelationship.Loyal && relationship != VRelationship.Submissive &&
								!possiblePatron.statusEffects.hasTrait("BloodRestoresHealth") && !possiblePatron.statusEffects.hasTrait("OilRestoresHealth"))
								patrons.Add(possiblePatron);
						}
					}

					if (!patrons.Any())
						continue;
					else
						agentInteractions.AddButton(interaction.ButtonText, agent.determineMoneyCost(patrons.Count, VDetermineMoneyCost.BuyRound));
				}
				else if (interaction is Bribe_Cops)
				{
					if (!interactingAgent.aboveTheLaw && !interactingAgent.HasTrait(VanillaTraits.AbovetheLaw) && !agent.statusEffects.hasStatusEffect(VStatusEffect.AbovetheLaw) && !interactingAgent.enforcer && !interactingAgent.upperCrusty)
					{
						if (interactingAgent.statusEffects.hasStatusEffect(VStatusEffect.CopDebt1))
							agentInteractions.AddButton(VButtonText.PayCops, agent.determineMoneyCost(VDetermineMoneyCost.PayCops1));
						else if (interactingAgent.statusEffects.hasStatusEffect(VStatusEffect.CopDebt2))
							agentInteractions.AddButton(VButtonText.PayCops, agent.determineMoneyCost(VDetermineMoneyCost.PayCops2));
						else if (!interactingAgent.statusEffects.hasTrait(VanillaTraits.CorruptionCosts))
							agentInteractions.AddButton(VButtonText.BribeCops, agent.determineMoneyCost(VDetermineMoneyCost.BribeCops));
					}
				}
				else if (interaction is Bribe_for_Entry_Alcohol || interaction is Pay_Entrance_Fee) // Bouncer traits
				{
					bool hasOtherBouncer = false;

					for (int i = 0; i < agent.gc.agentList.Count; i++)
					{
						Agent iAgent = agent.gc.agentList[i];

						if (iAgent.startingChunk == agent.startingChunk && iAgent.ownerID == agent.ownerID && iAgent != agent && (iAgent.oma.modProtectsProperty != 0 || iAgent.agentName == VanillaAgents.Bouncer))
						{
							relStatus relCode = iAgent.relationships.GetRelCode(interactingAgent);

							if (relationshipLevel < 3 && relCode != relStatus.Submissive)
							{
								hasOtherBouncer = true;
								break;
							}
						}
					}

					if (agentInteractions.HasMetalDetector(agent, interactingAgent) && (relationshipLevel > 2 || relationship == VRelationship.Submissive || interactingAgent.agentName == VanillaAgents.SuperCop))
					{
						agent.SayDialogue("DontNeedMoney");
						agentInteractions.BouncerTurnOffLaserEmitter(agent, interactingAgent, false);
					}
					else
					{
						if ((relationshipLevel > 2 || relationship == VRelationship.Submissive) && (!hasOtherBouncer || relationship == VRelationship.Submissive))
							agent.SayDialogue("DontNeedMoney");

						else if (interactingAgent.statusEffects.hasTrait("Unlikeable") || interactingAgent.statusEffects.hasTrait("Naked") || interactingAgent.statusEffects.hasTrait("Suspicious"))
							agent.SayDialogue("WontJoinA");
						else if (interaction is Bribe_for_Entry_Alcohol)
						{
							if (interactingAgent.inventory.HasItem(vItem.Beer))
								agentInteractions.AddButton(VButtonText.BribeForEntryBeer);
							else if (interactingAgent.inventory.HasItem(vItem.Whiskey))
								agentInteractions.AddButton(VButtonText.BribeForEntryWhiskey);
						}
						else if (interaction is Pay_Entrance_Fee)
						{
							if (agent.startingChunkRealDescription == VChunkType.Bar ||
								agent.startingChunkRealDescription == VChunkType.DanceClub ||
								agent.startingChunkRealDescription == VChunkType.Arena ||
								agent.startingChunkRealDescription == VChunkType.MusicHall)
								agentInteractions.AddButton(VButtonText.PayEntranceFee, agent.determineMoneyCost(VDetermineMoneyCost.Bribe));
							else
								agentInteractions.AddButton(VButtonText.Bribe, agent.determineMoneyCost(VDetermineMoneyCost.Bribe));
						}
					}
				}
				else if (interaction is Buy_Slave)
				{
					bool hasSlaves = false;
					bool questSlave = false;

					for (int i = 0; i < agent.gc.agentList.Count; i++)
					{
						Agent possibleSlave = agent.gc.agentList[i];

						if (possibleSlave.agentName == VanillaAgents.Slave && possibleSlave.slaveOwners.Contains(agent))
						{
							hasSlaves = true;

							if (possibleSlave.rescueForQuest != null || (!possibleSlave.gc.serverPlayer && possibleSlave.oma.rescuingForQuest))
								questSlave = true;
						}
					}

					if (questSlave)
						agentInteractions.AddButton(interaction.ButtonText, agent.determineMoneyCost(VDetermineMoneyCost.QuestSlavePurchase));
					else if (hasSlaves)
						agentInteractions.AddButton(interaction.ButtonText, agent.determineMoneyCost(VDetermineMoneyCost.SlavePurchase));
				}
				else if (interaction is Leave_Weapons_Behind)
				{
					agentInteractions.AddButton(interaction.ButtonText);

					for (int i = 0; i < agent.gc.agentList.Count; i++)
					{
						Agent followerCandidate = agent.gc.agentList[i];

						if (followerCandidate.employer == interactingAgent && followerCandidate.inventory.HasWeapons() && !followerCandidate.inCombat && followerCandidate.jobCode != jobType.GoHere && Vector2.Distance(agent.tr.position, followerCandidate.tr.position) < 13.44f)
						{
							agentInteractions.AddButton(VButtonText.FollowersLeaveWeaponsBehind);
							break;
						}
					}
				}
				else if (interaction is Manage_Chunk)
				{
					switch (agent.startingChunkRealDescription)
					{
						case VChunkType.Arena:
							for (int i = 0; i < agent.gc.objectRealList.Count; i++)
							{
								ObjectReal objectReal = agent.gc.objectRealList[i];

								if (objectReal.startingChunk == agent.startingChunk && objectReal.objectName == vObject.EventTriggerFloor)
								{
									EventTriggerFloor eventTriggerFloor = (EventTriggerFloor)objectReal;

									if (!eventTriggerFloor.functional && eventTriggerFloor.triggerState != "NeedToPayOut" && eventTriggerFloor.triggerState != "NoPayout" && eventTriggerFloor.triggerState != "Cheated")
										agent.SayDialogue("NoMoreFights");
									else if (eventTriggerFloor.triggerState == "Initial")
									{
										bool groupFight = false;

										for (int m = 0; m < agent.gc.agentList.Count; m++)
										{
											Agent agent3 = agent.gc.agentList[m];

											if ((agent3.isPlayer > 0 || agent3.employer != null) && agent3 != interactingAgent)
												groupFight = true;
										}

										if (groupFight)
											agent.SayDialogue("AskToStartFightMultiple");
										else
											agent.SayDialogue("AskToStartFight");

										agentInteractions.AddButton(VButtonText.Arena_SignUpToFight);
									}
									else if (eventTriggerFloor.triggerState == "FightSignedUp")
										agent.SayDialogue("SignedUpToFight");
									else if (eventTriggerFloor.triggerState == "FightStarted")
										agent.SayDialogue("FightCheer");
									else if (eventTriggerFloor.triggerState == "NeedToPayOut")
									{
										agent.SayDialogue("NoMoreFights");
										agentInteractions.AddButton("PayOutFight");
									}
									else if (eventTriggerFloor.triggerState == "NoPayout")
										agent.SayDialogue("LostFightNoPayout");
									else if (eventTriggerFloor.triggerState == "Cheated")
										agent.SayDialogue("CheatedNoPayout");
									else if (eventTriggerFloor.triggerState == "FightsOver")
										agent.SayDialogue("NoMoreFights");
								}
							}
							break;
						case VChunkType.DeportationCenter:
							if (interactingAgent.upperCrusty)
								agent.SayDialogue("NoDeportationUpperCrusty");
							else if (relationshipLevel > 2 || relationship == VRelationship.Submissive)
								agent.SayDialogue("DontNeedMoney");
							else
							{
								agentInteractions.AddButton(VButtonText.BribeDeportation, agent.determineMoneyCost(VDetermineMoneyCost.BribeDeportation));
								agentInteractions.AddButton(VButtonText.BribeDeportationItem);
							}
							break;
						case VChunkType.Hotel:
							if (agent.inventory.HasItem(vItem.Key))
								agentInteractions.AddButton(VButtonText.BuyKeyHotel, agent.determineMoneyCost(VDetermineMoneyCost.BuyKey));
							break;
					}
				}
				else if (interaction is Pay_Debt)
				{
					// Cost scaling is done slightly different for this interaction, since it's not subject to level scaling.
					float costScale = agent.GetTrait<T_CostScale>()?.CostScale ?? 1f;

					agentInteractions.AddButton(interaction.ButtonText, (int)(interactingAgent.CalculateDebt() * costScale));
				}
				else if (interaction is Play_Bad_Music)
				{
					for (int i = 0; i < agent.gc.objectRealList.Count; i++)
					{
						ObjectReal objectReal = agent.gc.objectRealList[i];

						if (objectReal.objectName == vObject.Turntables && objectReal.startingChunk == agent.startingChunk && !objectReal.destroyed && objectReal.functional && Vector2.Distance(objectReal.tr.position, agent.tr.position) < 1.28f)
						{
							Turntables turntables = (Turntables)objectReal;

							if (turntables.SpeakersFunctional() && !turntables.badMusicPlaying)
							{
								if (interactingAgent.inventory.HasItem(vItem.RecordofEvidence))
									agentInteractions.AddButton(VButtonText.PlayMayorEvidence, agent.determineMoneyCost(VDetermineMoneyCost.PlayMayorEvidence));

								agentInteractions.AddButton(VButtonText.PlayBadMusic, agent.determineMoneyCost(VDetermineMoneyCost.PlayBadMusic));
							}
						}
					}
				}
				else // Non-Exceptions
				{
					if (interaction.DetermineMoneyCost is null)
						agentInteractions.AddButton(interaction.ButtonText);
					else if (interaction.ExtraTextCostOnly)
						agentInteractions.AddButton(interaction.ButtonText, interaction.DetermineMoneyCost);
					else
						agentInteractions.AddButton(interaction.ButtonText, agent.determineMoneyCost(interaction.DetermineMoneyCost));
				}
			}

			// Merchant 
			if (!untrusted && agent.GetTraits<T_MerchantType>().Any() && agent.hasSpecialInvDatabase)
			{
				bool cantBuy =
					(agent.HasTrait<Cool_Cannibal>() && !interactingAgent.statusEffects.hasTrait("CannibalsNeutral") && interactingAgent.agentName != VanillaAgents.Cannibal) ||
					(agent.HasTrait<Cop_Access>() && !interactingAgent.HasTrait("TheLaw") && interactingAgent.agentName != VanillaAgents.Cop && interactingAgent.agentName != VanillaAgents.CopBot && interactingAgent.agentName != VanillaAgents.SuperCop) ||
					(agent.HasTrait<Honorable_Thief>() && !interactingAgent.statusEffects.hasTrait("HonorAmongThieves") && !interactingAgent.statusEffects.hasTrait("HonorAmongThieves2"));

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

        [HarmonyTranspiler, HarmonyPatch(methodName: nameof(AgentInteractions.DetermineButtons), argumentTypes: new[] { typeof(Agent), typeof(Agent), typeof(List<string>), typeof(List<string>), typeof(List<int>) })]
		private static IEnumerable<CodeInstruction> DetermineButtons_BypassLanguageHardcode(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo cantUnderstandEachOther = AccessTools.DeclaredMethod(typeof(P_AgentInteractions), nameof(P_AgentInteractions.CanUnderstandEachOther));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 2,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_2),
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Ldstr, "Translator"),
					new CodeInstruction(OpCodes.Callvirt),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_1),
					new CodeInstruction(OpCodes.Ldarg_2),
					new CodeInstruction(OpCodes.Call, cantUnderstandEachOther),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		private static bool CanUnderstandEachOther(Agent agent, Agent otherAgent) =>
			otherAgent.inventory.HasItem(vItem.Translator) ||
			agent.CanUnderstandEachOther(otherAgent, true, true);

		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(AgentInteractions.FinishedOperating), 
			argumentTypes: new[] { typeof(Agent) })]
		private static IEnumerable<CodeInstruction> FinishedOperating_LimitHackToVanillaKillerRobot(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo killerRobot = AccessTools.DeclaredField(typeof(Agent), nameof(Agent.killerRobot));
			MethodInfo isVanillaKillerRobot = AccessTools.DeclaredMethod(typeof(Seek_and_Destroy), nameof(Seek_and_Destroy.IsVanillaKillerRobot));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, killerRobot),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, isVanillaKillerRobot)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(AgentInteractions.LetGo), 
			argumentTypes: new[] { typeof(Agent), typeof(Agent) })]
		public static bool LetGo_Prefix(Agent agent, Agent otherAgent)
        {
			if (agent.GetHook<P_Agent_Hook>().PermanentHire)
				return false;

			return true;
        }

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(AgentInteractions.PressedButton), 
			argumentTypes: new[] { typeof(Agent), typeof(Agent), typeof(string), typeof(int) })]
		public static bool PressedButton_Prefix(Agent agent, Agent interactingAgent, string buttonText, int buttonPrice)
        {
			switch (buttonText)
            {
				case CButtonText.HirePermanentExpert:
					HirePermanently(agent, interactingAgent, buttonPrice);
					return false;
				case CButtonText.HirePermanentMuscle:
					HirePermanently(agent, interactingAgent, buttonPrice);
					return false;
				default:
					return true;
            }
        }

		[HarmonyPrefix, HarmonyPatch (methodName: nameof(AgentInteractions.UseItemOnObject), 
			argumentTypes: new[] { typeof(Agent), typeof(Agent), typeof(InvItem), typeof(int), typeof(string), typeof(string) })]
		public static bool UseItemOnObject_Prefix(Agent agent, Agent interactingAgent, InvItem item, string combineType, string useOnType, ref bool __result)
        {
			if (useOnType == VButtonText.Identify && agent.agentName == VanillaAgents.CustomCharacter)
			{
				if (item.invItemName == vItem.Syringe)
				{
					if (combineType == "Combine")
					{
						if (!GC.syringesIdentified.Contains(item.contents[0]) && !interactingAgent.statusEffects.hasTrait(VanillaTraits.Drugalug))
						{
							if (agent.moneySuccess(agent.determineMoneyCost(VDetermineMoneyCost.IdentifySyringe)))
							{
								agent.SayDialogue("Bought2");
								interactingAgent.statusEffects.IdentifySyringe(item.contents[0]);
							}
						}
						else
						{
							interactingAgent.SayDialogue("AlreadyIdentified");
							GC.audioHandler.Play(interactingAgent, "CantDo");
						}
					}

					__result = true;
					return false;
				}

				__result = false;
				return false;
			}

			return true;
		}

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
		public static void HirePermanently(Agent agent, Agent interactingAgent, int buttonPrice)
		{
			int num = agent.FindNumFollowing(interactingAgent);
			bool canHire = false;

			if ((interactingAgent.statusEffects.hasTrait(VanillaTraits.TeamBuildingExpert) && num < 3) ||
				(interactingAgent.statusEffects.hasTrait(VanillaTraits.ArmyofFive) && num < 5))
				canHire = true;
			else if (interactingAgent.statusEffects.hasTrait(VanillaTraits.Malodorous) &&
				!interactingAgent.statusEffects.hasTrait(VanillaTraits.Charismatic) &&
				!interactingAgent.statusEffects.hasTrait("Likeable2") &&
				!interactingAgent.statusEffects.hasTrait("NiceSmelling"))
			{
				agent.SayDialogue("WontJoinA");
				agent.StopInteraction();
			}
			else if (interactingAgent.statusEffects.hasTrait(VanillaTraits.Antisocial))
			{
				agent.SayDialogue("WontJoinA");
				agent.StopInteraction();
			}
			else if (num < 1)
				canHire = true;
			else
			{
				agent.SayDialogue("WontJoinB");
				agent.StopInteraction();
			}

			if (!canHire)
				return;

			if (!agent.moneySuccess(buttonPrice))
			{
				agent.StopInteraction();
				return;
			}

			agent.agentInteractions.AssistMe(agent, interactingAgent);
			agent.SetChangeElectionPoints(interactingAgent);
			agent.StopInteraction();
			agent.GetHook<P_Agent_Hook>().PermanentHire = true;

			return;
		}
	}

    [HarmonyPatch(declaringType: typeof(AgentInteractions))]
	static class P_AgentInteractions_UseExplosiveStimulator
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTargetMethod, UsedImplicitly]
		private static MethodInfo Find_MoveNext_MethodInfo() =>
			PatcherUtils.FindIEnumeratorMoveNext(AccessTools.Method(typeof(AgentInteractions), nameof(AgentInteractions.UseExplosiveStimulator), 
				new[] { typeof(Agent), typeof(List<PlayfieldObject>) }));

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> LimitToVanillaKillerRobot(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo killerRobot = AccessTools.DeclaredField(typeof(Agent), nameof(Agent.killerRobot));
			MethodInfo isVanillaKillerRobot = AccessTools.DeclaredMethod(typeof(Seek_and_Destroy), nameof(Seek_and_Destroy.IsVanillaKillerRobot));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, killerRobot),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, isVanillaKillerRobot)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}
}