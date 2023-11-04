using BepInEx.Logging;
using BunnyLibs;
using CCU.Systems.Language;
using CCU.Traits.Cost_Scale;
using CCU.Traits.Hire_Duration;
using CCU.Traits.Hire_Type;
using CCU.Traits.Interaction_Gate;
using CCU.Traits.Merchant_Type;
using CCU.Traits.Trait_Gate;
using HarmonyLib;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static CCU.Traits.Rel_Faction.T_Rel_Faction;

namespace CCU.Traits.Interaction
{
	public abstract class T_Interaction : T_DesignerTrait
	{
		public T_Interaction() : base() { }

		public abstract bool AllowUntrusted { get; }
		public abstract string ButtonID { get; }
		public abstract bool HideCostInButton { get; }
		public abstract string DetermineMoneyCostID { get; }
	}

	[HarmonyPatch(typeof(AgentInteractions))]
	static class P_AgentInteractions_VanillaInteractions
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		// TODO: Refactor
		[RLSetup]
		private static void Setup()
		{
			RogueInteractions.CreateProvider<Agent>(h =>
			{
				Agent agent = h.Object;

				if (agent.agentName != VanillaAgents.CustomCharacter)
					return;

				AgentInteractions agentInteractions = agent.agentInteractions;
				H_AgentInteractions agentInteractionsHook = agent.GetOrAddHook<H_AgentInteractions>();
				Agent interactingAgent = h.Agent;
				string relationship = agent.relationships.GetRel(interactingAgent);
				int relationshipLevel = VRelationship.GetRelationshipLevel(relationship);

				if (agentInteractionsHook.interactionState is InteractionState.Default)
				{
					// Language check
					if (!agent.CanUnderstandEachOther(interactingAgent, true, true))
					{
						h.AddImplicitButton("Interact", m =>
						{
							LanguageSystem.SayGibberish(agent);
						});

						return;
					}

					T_InteractionGate untrustingTrait = agent.GetTraits<T_InteractionGate>().Where(t => t.MinimumRelationship > 0).FirstOrDefault(); // Should only ever be one
					bool untrusted =
						relationship == VRelationship.Annoyed ||
						(!(untrustingTrait is null) && relationshipLevel < untrustingTrait.MinimumRelationship);

					// Hire
					if (!untrusted && agent.GetTraits<T_HireType>().Any())
					{
						if (agent.employer == null && agent.relationships.GetRelCode(interactingAgent) != relStatus.Annoyed)
						{
							if ((AlignmentUtils.CountsAsBlahd(agent) &&
									interactingAgent.agentName == VanillaAgents.GangsterBlahd && interactingAgent.oma.superSpecialAbility) ||
								(AlignmentUtils.CountsAsCrepe(agent) &&
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
									if (interactingAgent.inventory.HasItem(VItemName.HiringVoucher))
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
									//if (interactingAgent.inventory.HasItem(VItemName.HiringVoucher /*Add Gold Version*/))
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
											agentInteractionsHook.HiredPermanently = true;
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

					foreach (T_Interaction trait in agent.GetTraits<T_Interaction>())
					{
						#region Interaction Gates
						if ((!trait.AllowUntrusted && untrusted) ||
							(trait is Borrow_Money_Moocher && !interactingAgent.statusEffects.hasTrait(VanillaTraits.Moocher)) ||
							(trait is Influence_Election && GC.sessionData.electionBribedMob[interactingAgent.isPlayer]) ||
							(trait is Leave_Weapons_Behind && !interactingAgent.inventory.HasWeapons()) ||
							(trait is Offer_Motivation && agent.oma.offeredOfficeDrone) ||
							(trait is Pay_Debt && !interactingAgent.statusEffects.hasStatusEffect(VanillaEffects.InDebt1)
													&& !interactingAgent.statusEffects.hasStatusEffect(VanillaEffects.InDebt2)
													&& !interactingAgent.statusEffects.hasStatusEffect(VanillaEffects.InDebt3)) ||
							(trait is Use_Blood_Bag && !interactingAgent.inventory.HasItem(VItemName.BloodBag)))
							continue;
						#endregion
						#region Vanilla ad hoc exceptions
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
								h.AddButton(trait.ButtonID, agent.determineMoneyCost(patrons.Count, VDetermineMoneyCost.BuyRound), m =>
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
									if (interactingAgent.inventory.HasItem(VItemName.Beer))
										h.AddButton(VButtonText.BribeForEntryBeer, m =>
										{
											agentInteractions.Bribe(m.Object, interactingAgent, VItemName.Beer, 0);
										});
									else if (interactingAgent.inventory.HasItem(VItemName.Whiskey))
										h.AddButton(VButtonText.BribeForEntryWhiskey, m =>
										{
											agentInteractions.Bribe(m.Object, interactingAgent, VItemName.Whiskey, 0);
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
										agentInteractions.Bribe(m.Object, interactingAgent, VItemName.Money, m.Object.determineMoneyCost(VDetermineMoneyCost.Bribe));
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

								h.AddButton(trait.ButtonID, price, m =>
								{
									agentInteractions.GiveSlave(m.Object, interactingAgent, true, 0, price, false, false);
								});
							}
							else if (hasSlaves)
							{
								int price = agent.determineMoneyCost(VDetermineMoneyCost.SlavePurchase);

								h.AddButton(trait.ButtonID, price, m =>
								{
									agentInteractions.GiveSlave(m.Object, interactingAgent, true, 0, price, false, false);
								});
							}
						}
						else if (trait is Leave_Weapons_Behind)
						{
							h.AddButton(trait.ButtonID, m =>
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

										if (objectReal.startingChunk == agent.startingChunk && objectReal.objectName == VObjectReal.EventTriggerFloor)
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
											agentInteractions.Bribe(m.Object, interactingAgent, VItemName.Money, agent.determineMoneyCost(VDetermineMoneyCost.BribeDeportation));
										});
										h.AddButton(VButtonText.BribeDeportationItem, m =>
										{
											m.Object.ShowUseOn("BribeDeportationItem");
										});
									}
									break;
								case VChunkType.Hotel:
									if (agent.inventory.HasItem(VItemName.Key))
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

							h.AddButton(trait.ButtonID, totalCost, m =>
							{
								m.Object.agentInteractions.PressedButton(m.Object, interactingAgent, trait.ButtonID, totalCost);
							});
						}
						else if (trait is Play_Bad_Music)
						{
							for (int i = 0; i < agent.gc.objectRealList.Count; i++)
							{
								ObjectReal objectReal = agent.gc.objectRealList[i];

								if (objectReal.objectName == "Turntables"/*VObjectReal.Turntables*/ && objectReal.startingChunk == agent.startingChunk && !objectReal.destroyed && objectReal.functional && Vector2.Distance(objectReal.tr.position, agent.tr.position) < 1.28f)
								{
									Turntables turntables = (Turntables)objectReal;

									if (turntables.SpeakersFunctional() && !turntables.badMusicPlaying)
									{
										if (interactingAgent.inventory.HasItem(VItemName.RecordofEvidence))
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
						#endregion
						else
						{
							if (trait is IBranchInteractionMenu branchTrait) // Assuming no cost to talk
							{
								if (branchTrait.ButtonCanShow(agent))
									h.AddButton(trait.ButtonID, m =>
									{
										agentInteractionsHook.interactionState = branchTrait.interactionState;
									});
							}
							else if (trait.DetermineMoneyCostID is null)
								h.AddButton(trait.ButtonID, m =>
								{
									m.Object.agentInteractions.PressedButton(m.Object, interactingAgent, trait.ButtonID, 0);
								});
							else if (trait.HideCostInButton)
								h.AddButton(trait.ButtonID, trait.DetermineMoneyCostID, m =>
								{
									m.Object.agentInteractions.PressedButton(m.Object, interactingAgent, trait.ButtonID, 0);
								});
							else
								h.AddButton(trait.ButtonID, agent.determineMoneyCost(trait.DetermineMoneyCostID), m =>
								{
									m.Object.agentInteractions.PressedButton(m.Object, interactingAgent, trait.ButtonID, m.Object.determineMoneyCost(trait.DetermineMoneyCostID));
								});
						}
					}

					// Shops
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

							if (interactingAgent.inventory.HasItem(VItemName.FreeItemVoucher))
								h.AddButton(VButtonText.UseVoucher, m =>
								{
									m.Object.ShowNPCChest();
									interactingAgent.usingVoucher = true;
								});
						}
					}
				}
				else if (agentInteractionsHook.interactionState is InteractionState.LearnTraits_Language)
				{
					List<string> teacherLanguages = LanguageSystem.KnownLanguagesWithoutTranslator(agent, false);
					List<string> studentLanguages = LanguageSystem.KnownLanguagesWithoutTranslator(interactingAgent, false);

					foreach (string language in teacherLanguages.Where(l => !studentLanguages.Contains(l)))
					{
						string determineMoneyCostText = language is LanguageSystem.English
							? CDetermineMoneyCost.LearnLanguageEnglish
							: CDetermineMoneyCost.LearnLanguageOther;
						int cost = agent.determineMoneyCost(determineMoneyCostText);

						h.AddButton("Learn_" + language, cost, m =>
						{
							if (agent.moneySuccess(cost))
							{
								if (language == LanguageSystem.English)
								{
									interactingAgent.statusEffects.RemoveTrait(VanillaTraits.VocallyChallenged);
								}
								else
								{
									List<T_Language> allLangTraits = CoreTools.AllClassesOfType<T_Language>();

									T_Language trait = CoreTools.AllClassesOfType<T_Language>()
										.Where(t =>
											!(t is Polyglot)
											&& t.LanguageNames.Contains(language))
										.FirstOrDefault();

									interactingAgent.AddTrait(trait.GetType());
								}
							}
						});
					}

					agentInteractionsHook.interactionState = InteractionState.Default;
				}
			});
		}
	}
}