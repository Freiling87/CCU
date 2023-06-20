using BepInEx.Logging;
using BTHarmonyUtils;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Hooks;
using CCU.Localization;
using CCU.Traits.Behavior;
using CCU.Traits.Cost_Scale;
using CCU.Traits.Hire_Duration;
using CCU.Traits.Hire_Type;
using CCU.Traits.Interaction;
using CCU.Traits.Interaction_Gate;
using CCU.Traits.Merchant_Type;
using CCU.Traits.Player.Language;
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

		// TODO: Refactor
        [RLSetup]
		private static void Setup()
        {
			RogueInteractions.CreateProvider<Agent>(h =>
			{
				Agent agent = h.Object;
				bool log = false;

				if (agent.agentName != VanillaAgents.CustomCharacter)
					return;

				if (log) logger.LogDebug("         INTERACTION DATA: " + agent.agentRealName);
				if (log) logger.LogDebug("OwnerID: " + agent.ownerID);
				if (log) logger.LogDebug("======== AGENTINTERACTIONS");
				AgentInteractions agentInteractions = agent.agentInteractions;
				Agent interactingAgent = h.Agent;
				string relationship = agent.relationships.GetRel(interactingAgent);
				int relationshipLevel = VRelationship.GetRelationshipLevel(relationship);
				if (log) logger.LogDebug(string.Format("Relationship: {0} ({1})", agent.relationships.GetRel(interactingAgent), relationshipLevel));

				T_InteractionGate trustTrait = agent.GetTraits<T_InteractionGate>().Where(t => t.MinimumRelationship > 0).FirstOrDefault(); // Should only ever be one
				bool untrusted = relationship == VRelationship.Annoyed ||
					(!(trustTrait is null) && relationshipLevel < trustTrait.MinimumRelationship);

				//// Hack
				//if (interactingAgent.interactionHelper.interactingFar)
				//	foreach (T_Hack hack in agent.GetTraits<T_Hack>())
				//		agentInteractions.AddButton(hack.ButtonText);

				if (log) logger.LogDebug("=== LANGUAGE");
				if (!Language.HaveSharedLanguage(agent, interactingAgent))
                {
					h.AddImplicitButton("None", m =>
					{
						Language.SayGibberish(agent);
						return;
					});
				}

				if (log) logger.LogDebug("=== HIRE");
				if (!untrusted && agent.GetTraits<T_HireType>().Any())
				{
					if (agent.employer == null && agent.relationships.GetRelCode(interactingAgent) != relStatus.Annoyed)
					{
						if ((agent.HasTrait<Blahd_Aligned>() &&
								interactingAgent.agentName == VanillaAgents.GangsterBlahd && interactingAgent.oma.superSpecialAbility) ||
							(agent.HasTrait<Crepe_Aligned>() &&
								interactingAgent.agentName == VanillaAgents.GangsterCrepe && interactingAgent.oma.superSpecialAbility))
							h.AddButton(VButtonText.JoinMe, m =>
							{
								m.Object.agentInteractions.QualifyHireAsProtection(m.Object, interactingAgent, 0);
							});
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

							int normalHireCost = agent.determineMoneyCost(costString);
							int permanentHireCost = agent.determineMoneyCost(costString + "_Permanent");

							if (!agent.HasTrait<Permanent_Hire_Only>()) // Normal Hire
							{
								if (interactingAgent.inventory.HasItem(vItem.HiringVoucher))
									h.AddButton(hireButtonText + "_Voucher", 6666, m =>
									{
										m.Agent.agentInteractions.QualifyHireAsProtection(agent, interactingAgent, 6666);
									});

								h.AddButton(hireButtonText, normalHireCost, m =>
                                {
									m.Object.agentInteractions.PressedButton(m.Object, interactingAgent, hireButtonText, normalHireCost);
                                });
							}

							if (agent.HasTrait<Permanent_Hire_Only>() || agent.HasTrait<Permanent_Hire>())
							{
								//if (interactingAgent.inventory.HasItem(vItem.HiringVoucher /*Add Gold Version*/))
								//	h.AddButton(hireButtonText + "_Permanent_Voucher", 6667, m =>
								//	{
								//		//m.Object.agentInteractions.PressedButton(m.Object, interactingAgent, hireButtonText, 6667);
								//		m.Agent.agentInteractions.QualifyHireAsProtection(agent, interactingAgent, 6667);
								//	});

								h.AddButton(hireButtonText + "_Permanent", permanentHireCost, m =>
								{
									agent.agentInteractions.QualifyHireAsProtection(agent, interactingAgent, permanentHireCost);

									if (agent.followingNum == interactingAgent.agentID)
									{
										logger.LogDebug("Successfully detected permanent hire and notated");
										agent.GetOrAddHook<H_Agent>().HiredPermanently = true;
										agent.canGoBetweenLevels = true;
									}
								});
							}
						}
					}
					else if (!agent.oma.cantDoMoreTasks) // Ordering already-hired Agent
						foreach (T_HireType hiredTrait in agent.GetTraits<T_HireType>().Where(t => t.HiredActionButtonText != null))
							h.AddButton(hiredTrait.HiredActionButtonText, m =>
                            {
								m.Object.agentInteractions.PressedButton(m.Object, interactingAgent, hiredTrait.HiredActionButtonText, 0);
							});
				}

				if (log) logger.LogDebug("=== INTERACTION");
				foreach (T_Interaction trait in agent.GetTraits<T_Interaction>())
				{
					// Simple Exceptions
					if ((!(trait is Offer_Motivation || trait is Leave_Weapons_Behind || trait is Pay_Debt || trait is Pay_Entrance_Fee) && untrusted) || // Untrusted Exceptions
						(trait is Borrow_Money_Moocher && !interactingAgent.statusEffects.hasTrait(VanillaTraits.Moocher)) ||
						(trait is Influence_Election && GC.sessionData.electionBribedMob[interactingAgent.isPlayer]) ||
						(trait is Leave_Weapons_Behind && !interactingAgent.inventory.HasWeapons()) ||
						(trait is Offer_Motivation && agent.oma.offeredOfficeDrone) ||
						(trait is Pay_Debt && !interactingAgent.statusEffects.hasStatusEffect(VanillaEffects.InDebt1)
												&& !interactingAgent.statusEffects.hasStatusEffect(VanillaEffects.InDebt2)
												&& !interactingAgent.statusEffects.hasStatusEffect(VanillaEffects.InDebt3)) ||
						(trait is Use_Blood_Bag && !interactingAgent.inventory.HasItem(vItem.BloodBag)))
						continue;

					// Complex Exceptions
					if (trait is Buy_Round)
					{
						List<Agent> patrons = new List<Agent>();

						for (int i = 0; i < GC.agentList.Count; i++)
						{
							Agent possiblePatron = GC.agentList[i];

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
							h.AddButton(trait.ButtonText, agent.determineMoneyCost(patrons.Count, VDetermineMoneyCost.BuyRound), m =>
                            {
								if (m.Object.moneySuccess(m.Object.determineMoneyCost(patrons.Count, VDetermineMoneyCost.BuyRound)))
									m.Object.agentInteractions.BuyRound(m.Object, interactingAgent);

								m.Object.StopInteraction();
							});
					}
					else if (trait is Bribe_Cops)
					{
						if (!interactingAgent.aboveTheLaw && !interactingAgent.HasTrait(VanillaTraits.AbovetheLaw) && !agent.statusEffects.hasStatusEffect(VStatusEffect.AbovetheLaw) && !interactingAgent.enforcer && !interactingAgent.upperCrusty)
						{
							if (interactingAgent.statusEffects.hasStatusEffect(VStatusEffect.CopDebt1))
								h.AddButton(VButtonText.PayCops, agent.determineMoneyCost(VDetermineMoneyCost.PayCops1), m =>
                                {
									if (m.Object.moneySuccess(m.Object.determineMoneyCost(VDetermineMoneyCost.PayCops1)))
										m.Object.agentInteractions.PayCops(m.Object, interactingAgent);

									m.Object.StopInteraction();
								});
							else if (interactingAgent.statusEffects.hasStatusEffect(VStatusEffect.CopDebt2))
								h.AddButton(VButtonText.PayCops, agent.determineMoneyCost(VDetermineMoneyCost.PayCops2), m =>
								{
									if (m.Object.moneySuccess(m.Object.determineMoneyCost(VDetermineMoneyCost.PayCops2)))
										m.Object.agentInteractions.PayCops(m.Object, interactingAgent);

									m.Object.StopInteraction();
								});
							else if (!interactingAgent.statusEffects.hasTrait(VanillaTraits.CorruptionCosts))
								h.AddButton(VButtonText.BribeCops, agent.determineMoneyCost(VDetermineMoneyCost.BribeCops), m =>
                                {
									if (interactingAgent.aboveTheLaw || interactingAgent.upperCrusty)
									{
										m.Object.SayDialogue("DontNeedMoney");
										m.Object.StopInteraction();
										return;
									}

									if (m.Object.moneySuccess(m.Object.determineMoneyCost(VDetermineMoneyCost.BribeCops)))
										m.Object.agentInteractions.BribeCops(m.Object, interactingAgent);

									m.Object.StopInteraction();
								});
						}
					}
					else if (trait is Bribe_for_Entry_Alcohol || trait is Pay_Entrance_Fee) // Bouncer traits
					{
						bool hasOtherBouncer = false;

						for (int i = 0; i < GC.agentList.Count; i++)
						{
							Agent iAgent = GC.agentList[i];

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

						if (agentInteractions.HasMetalDetector(agent, interactingAgent) && 
							(relationshipLevel > 2 || relationship == VRelationship.Submissive || interactingAgent.agentName == VanillaAgents.SuperCop))
						{
							agent.SayDialogue("DontNeedMoney");
							agentInteractions.BouncerTurnOffLaserEmitter(agent, interactingAgent, false);
						}
						else
						{
							if ((relationshipLevel > 2 || relationship == VRelationship.Submissive) && 
								(!hasOtherBouncer || relationship == VRelationship.Submissive))
								agent.SayDialogue("DontNeedMoney");
							else if (interactingAgent.statusEffects.hasTrait("Unlikeable") || 
									interactingAgent.statusEffects.hasTrait("Naked") || 
									interactingAgent.statusEffects.hasTrait("Suspicious"))
								agent.SayDialogue("WontJoinA");
							else if (trait is Bribe_for_Entry_Alcohol)
							{
								if (interactingAgent.inventory.HasItem(vItem.Beer))
									h.AddButton(VButtonText.BribeForEntryBeer, m =>
                                    {
										agentInteractions.Bribe(m.Object, interactingAgent, vItem.Beer, 0);
									});
								else if (interactingAgent.inventory.HasItem(vItem.Whiskey))
									h.AddButton(VButtonText.BribeForEntryWhiskey, m =>
                                    {
										agentInteractions.Bribe(m.Object, interactingAgent, vItem.Whiskey, 0);
									});
							}
							else if (trait is Pay_Entrance_Fee)
							{
								string buttonText = 
									(agent.startingChunkRealDescription == VChunkType.Bar ||
									agent.startingChunkRealDescription == VChunkType.DanceClub ||
									agent.startingChunkRealDescription == VChunkType.Arena ||
									agent.startingChunkRealDescription == VChunkType.MusicHall)
										? VButtonText.PayEntranceFee
										: VButtonText.Bribe;

									h.AddButton(buttonText, agent.determineMoneyCost(VDetermineMoneyCost.Bribe), m =>
                                    {
										agentInteractions.Bribe(m.Object, interactingAgent, vItem.Money, m.Object.determineMoneyCost(VDetermineMoneyCost.Bribe));
									});
							}
						}
					}
					else if (trait is Buy_Slave)
					{
						bool hasSlaves = false;
						bool questSlave = false;

						for (int i = 0; i < GC.agentList.Count; i++)
						{
							Agent possibleSlave = GC.agentList[i];

							if (possibleSlave.agentName == VanillaAgents.Slave && possibleSlave.slaveOwners.Contains(agent))
							{
								hasSlaves = true;

								if (possibleSlave.rescueForQuest != null || (!possibleSlave.gc.serverPlayer && possibleSlave.oma.rescuingForQuest))
									questSlave = true;
							}
						}

						if (questSlave)
                        {
							int price = agent.determineMoneyCost(VDetermineMoneyCost.QuestSlavePurchase);

							h.AddButton(trait.ButtonText, price, m =>
							{
								agentInteractions.GiveSlave(m.Object, interactingAgent, true, 0, price, false, false);
							});
						}
						else if (hasSlaves)
                        {
							int price = agent.determineMoneyCost(VDetermineMoneyCost.SlavePurchase);

							h.AddButton(trait.ButtonText, price, m =>
							{
								agentInteractions.GiveSlave(m.Object, interactingAgent, true, 0, price, false, false);
							});
						}
					}
					else if (trait is Leave_Weapons_Behind)
					{
						h.AddButton(trait.ButtonText, m =>
                        {
							interactingAgent.inventory.DropWeapons();
							m.Object.RefreshButtons();
						});

						for (int i = 0; i < agent.gc.agentList.Count; i++)
						{
							Agent followerCandidate = agent.gc.agentList[i];

							if (followerCandidate.employer == interactingAgent && followerCandidate.inventory.HasWeapons() && !followerCandidate.inCombat && followerCandidate.jobCode != jobType.GoHere && Vector2.Distance(agent.tr.position, followerCandidate.tr.position) < 13.44f)
							{
								h.AddButton(VButtonText.FollowersLeaveWeaponsBehind, m =>
                                {
									for (int l = 0; l < m.Object.gc.agentList.Count; l++)
									{
										Agent agent4 = m.Object.gc.agentList[l];
										if (agent4.employer == interactingAgent && !agent4.inCombat && agent4.jobCode != jobType.GoHere && Vector2.Distance(m.Object.tr.position, agent4.tr.position) < 13.44f)
										{
											agent4.inventory.DropWeapons();
										}
									}

									m.Object.RefreshButtons();
								});

								break;
							}
						}
					}
					else if (trait is Manage_Chunk)
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

										if (!eventTriggerFloor.functional && 
											eventTriggerFloor.triggerState != "NeedToPayOut" && 
											eventTriggerFloor.triggerState != "NoPayout" && 
											eventTriggerFloor.triggerState != "Cheated")
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

											h.AddButton(VButtonText.Arena_SignUpToFight, m =>
                                            {
												agentInteractions.SignUpToFight(m.Object, interactingAgent);
												m.Object.StopInteraction();
											});
										}
										else if (eventTriggerFloor.triggerState == "FightSignedUp")
											agent.SayDialogue("SignedUpToFight");
										else if (eventTriggerFloor.triggerState == "FightStarted")
											agent.SayDialogue("FightCheer");
										else if (eventTriggerFloor.triggerState == "NeedToPayOut")
										{
											agent.SayDialogue("NoMoreFights");
											h.AddButton(VButtonText.PayOutFight, m =>
                                            {
												agentInteractions.PayOutFight(m.Object, interactingAgent);
												m.Object.StopInteraction();
											});
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
									h.AddButton(VButtonText.BribeDeportation, agent.determineMoneyCost(VDetermineMoneyCost.BribeDeportation), m =>
                                    {
										agentInteractions.Bribe(m.Object, interactingAgent, vItem.Money, agent.determineMoneyCost(VDetermineMoneyCost.BribeDeportation));
									});
									h.AddButton(VButtonText.BribeDeportationItem, m =>
                                    {
										m.Object.ShowUseOn("BribeDeportationItem");
									});
								}
								break;
							case VChunkType.Hotel:
								if (agent.inventory.HasItem(vItem.Key))
									h.AddButton(VButtonText.BuyKeyHotel, agent.determineMoneyCost(VDetermineMoneyCost.BuyKey), m =>
                                    {
										if (!interactingAgent.inventory.hasEmptySlot())
										{
											interactingAgent.inventory.PlayerFullResponse(interactingAgent);
											m.Object.StopInteraction();
											return;
										}

										if (!m.Object.moneySuccess(agent.determineMoneyCost(VDetermineMoneyCost.BuyKey)))
										{
											m.Object.StopInteraction();
											return;
										}
										
										agentInteractions.BuyKey(m.Object, interactingAgent);
										m.Object.SayDialogue("BoughtHotelKey");
										m.Object.SetChangeElectionPoints(interactingAgent);
										m.Object.StopInteraction();
									});
								break;
						}
					}
					else if (trait is Pay_Debt)
					{
						// Cost scaling is done slightly different for this interaction, since it's not subject to level scaling.
						float costScale = agent.GetTrait<T_CostScale>()?.CostScale ?? 1f;
						int totalCost = (int)(interactingAgent.CalculateDebt() * costScale);

						h.AddButton(trait.ButtonText, totalCost, m =>
                        {
							m.Object.agentInteractions.PressedButton(m.Object, interactingAgent, trait.ButtonText, totalCost);
                        });
					}
					else if (trait is Play_Bad_Music)
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
										h.AddButton(VButtonText.PlayMayorEvidence, agent.determineMoneyCost(VDetermineMoneyCost.PlayMayorEvidence), m =>
                                        {
											if (!m.Object.moneySuccess(m.Object.determineMoneyCost(VDetermineMoneyCost.PlayMayorEvidence)))
											{
												m.Object.StopInteraction();
												return;
											}
											agentInteractions.PlayMayorEvidence(m.Object, interactingAgent);
											m.Object.StopInteraction();
										});

									h.AddButton(VButtonText.PlayBadMusic, agent.determineMoneyCost(VDetermineMoneyCost.PlayBadMusic), m =>
                                    {
										if (!m.Object.moneySuccess(m.Object.determineMoneyCost(VDetermineMoneyCost.PlayBadMusic)))
										{
											m.Object.StopInteraction();
											return;
										}
										agentInteractions.PlayBadMusic(m.Object, interactingAgent);
										m.Object.SetChangeElectionPoints(interactingAgent);
										m.Object.StopInteraction();
									});
								}
							}
						}
					}
					else // Non-Exceptions
					{
						if (trait.DetermineMoneyCost is null)
							h.AddButton(trait.ButtonText, m =>
                            {
								m.Object.agentInteractions.PressedButton(m.Object, interactingAgent, trait.ButtonText, 0);
                            });
						else if (trait.ExtraTextCostOnly)
							h.AddButton(trait.ButtonText, trait.DetermineMoneyCost, m =>
                            {
								m.Object.agentInteractions.PressedButton(m.Object, interactingAgent, trait.ButtonText, 0);
							});
						else
							h.AddButton(trait.ButtonText, agent.determineMoneyCost(trait.DetermineMoneyCost), m =>
                            {
								m.Object.agentInteractions.PressedButton(m.Object, interactingAgent, trait.ButtonText, m.Object.determineMoneyCost(trait.DetermineMoneyCost));
							});
					}
				}

				if (log) logger.LogDebug("=== MERCHANT");
				if (!untrusted && agent.GetTraits<T_MerchantType>().Any() && agent.hasSpecialInvDatabase)
				{
					bool cantBuy =
						(agent.HasTrait<Cop_Access>() && 
							!interactingAgent.HasTrait(VanillaTraits.TheLaw) && interactingAgent.agentName != VanillaAgents.Cop && interactingAgent.agentName != VanillaAgents.CopBot && interactingAgent.agentName != VanillaAgents.SuperCop) ||
						(agent.HasTrait<Honorable_Thief>() && 
							!interactingAgent.statusEffects.hasTrait(VanillaTraits.HonorAmongThieves) && !interactingAgent.statusEffects.hasTrait("HonorAmongThieves2"));

					if (!cantBuy)
					{
						h.AddButton(VButtonText.Buy, m =>
                        {
							agent.ShowNPCChest();
						});

						if (interactingAgent.inventory.HasItem(vItem.FreeItemVoucher))
							h.AddButton(VButtonText.UseVoucher, m =>
                            {
								m.Object.ShowNPCChest();
								interactingAgent.usingVoucher = true;
							});
					}
				}
			});
        }

        [HarmonyTranspiler, HarmonyPatch(methodName: nameof(AgentInteractions.DetermineButtons), argumentTypes: new[] { typeof(Agent), typeof(Agent), typeof(List<string>), typeof(List<string>), typeof(List<int>) })]
		private static IEnumerable<CodeInstruction> DetermineButtons_BypassLanguageHardcode(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo canUnderstandEachOther = AccessTools.DeclaredMethod(typeof(Language), nameof(Language.HaveSharedLanguage));

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
					new CodeInstruction(OpCodes.Call, canUnderstandEachOther),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

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
		public static bool LetGo_Prefix(Agent agent)
        {
			agent.GetOrAddHook<H_Agent>().HiredPermanently = false;

			return true;
        }

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(AgentInteractions.PressedButton), 
			argumentTypes: new[] { typeof(Agent), typeof(Agent), typeof(string), typeof(int) })]
		public static bool PressedButton_Prefix(Agent agent, Agent interactingAgent, string buttonText, int buttonPrice)
        {
			logger.LogDebug("======= PressedButton_Prefix");
			logger.LogDebug("ButtonText: " + buttonText); 
			logger.LogDebug("ButtonPrice: " + buttonPrice);

			return true;
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

		// Based on AgentInteractions.QualifyHireAsProtection
		public static void HirePermanently(Agent agent, Agent interactingAgent, int buttonPrice)
		{
			int teamSize = agent.FindNumFollowing(interactingAgent);
			bool canHire = false;

			if ((interactingAgent.statusEffects.hasTrait(VanillaTraits.TeamBuildingExpert) && teamSize < 3) ||
				(interactingAgent.statusEffects.hasTrait(VanillaTraits.ArmyofFive) && teamSize < 5))
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
			else if (teamSize < 1)
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

			//agent.agentInteractions.AssistMe(agent, interactingAgent); // Replaced:
			agent.SayDialogue("Joined");
			agent.gc.audioHandler.Play(agent, "AgentJoin");
			agent.relationships.StartCoroutine(agent.relationships.joinPartyDelay(interactingAgent, "HireAsProtection"));
			//
			agent.SetChangeElectionPoints(interactingAgent);
			agent.StopInteraction();
			agent.GetOrAddHook<H_Agent>().HiredPermanently = true;
			agent.canGoBetweenLevels = true;

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