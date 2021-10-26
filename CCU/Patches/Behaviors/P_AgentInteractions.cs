using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using UnityEngine;
using RogueLibsCore;
using CCU.Traits;
using CCU.Traits.Hire;
using CCU.Traits.TraitTrigger;
using CCU.Traits.Interaction;

namespace CCU.Patches.Behaviors
{
	[HarmonyPatch(declaringType: typeof(AgentInteractions))]
	public class P_AgentInteractions
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(AgentInteractions.DetermineButtons), argumentTypes: new[] { typeof(Agent), typeof(Agent), typeof(List<string>), typeof(List<string>), typeof(List<int>) })]
		private static bool DetermineButtons_Prefix(Agent agent, Agent interactingAgent, List<string> buttons1, List<string> buttonsExtra1, List<int> buttonPrices1, AgentInteractions __instance, ref List<string> ___buttons, ref List<string> ___buttonsExtra, ref List<int> ___buttonPrices, ref Agent ___mostRecentAgent, ref Agent ___mostRecentInteractingAgent)
		{
			Core.LogMethodCall();
			TraitManager.LogTraitList(agent);

			___buttons = buttons1;
			___buttonsExtra = buttonsExtra1;
			___buttonPrices = buttonPrices1;
			___mostRecentAgent = agent;
			___mostRecentInteractingAgent = interactingAgent;

			bool flag = false;
			
			if (__instance.secondSelect)
				__instance.secondSelect = false;
			
			if (agent == GC.playerAgent)
			{
				if (agent.personalObjectButtonsType == "Kick" || agent.personalObjectButtonsType == "Ban")
				{
					for (int i = 0; i < GC.playerAgentList.Count; i++)
					{
						Agent agent2 = GC.playerAgentList[i];

						if (agent2.agentRealName != "E_" && agent2.agentRealName != "" && agent2.agentRealName != null && agent2 != GC.playerAgent)
							__instance.AddButton(agent2.playerUniqueID);
					}
				}

				if (agent.personalObjectButtonsType == "Emote")
				{
					__instance.AddButton("PlayerList");
					__instance.AddButton("HelpEmote");
					__instance.AddButton("FollowMeEmote");
					__instance.AddButton("WaitHereEmote");
					__instance.AddButton("TakeItEmote");
					__instance.AddButton("YesEmote");
					__instance.AddButton("NoEmote");
					__instance.AddButton("ThankYouEmote");
				
					if (GC.serverPlayer && GC.playerAgentList.Count > 1 && GC.sessionDataBig.multGameType == "Internet" && !GC.consoleVersion)
					{
						__instance.AddButton("KickEmote");
						__instance.AddButton("BanEmote");
					}
				}
			}

			if (GC.levelType == "HomeBase" && agent != interactingAgent)
			{
				string agentName = agent.agentName;

				if (agentName == "Doctor")
				{
					if (GC.justFinishedTutorial && GC.tutorialSeeDoctor && !GC.cinematics.didDoctorAfterTutorial)
					{
						GC.cinematics.DoctorAfterTutorial(agent);
						return false;
					}

					agent.SayDialogue("InteractHB");
					GC.audioHandler.Play(agent, "AgentTalk");
					__instance.AddButton("UnlockTraits");
					__instance.AddButton("TraitConfigs");
					return false;
				}
				else if (agentName == "ResistanceLeader")
				{
					if (agent.extraVar == 30)
						agent.Say(GC.nameDB.GetName("Tut_BathroomLine_2", "Dialogue"));
					else if (agent.extraVar == 3 && !GC.multiplayerMode)
					{
						agent.Say(GC.nameDB.GetName("ResistanceLeaderGiveTutorialHB", "Dialogue"));
						__instance.AddButton("StartTutorial");
					}
					else
						agent.SayDialogue("InteractHB");

					GC.audioHandler.Play(agent, "AgentTalk");
					return false;
				}

				// This is a highly redundant block that allowed to remove several categories below.
				agent.SayDialogue("InteractHB");
				GC.audioHandler.Play(agent, "AgentTalk");

				if (agentName == "Gangbanger")
				{
					if (GC.multiplayerMode)
						return false;

					if (!GC.fourPlayerMode && !GC.coopMode)
					{
						__instance.AddButton("StartCoop");
						__instance.AddButton("Start3Player");
						__instance.AddButton("Start4Player");

						if (!GC.consoleVersion)
							__instance.AddButton("StartOnline");
						
						return false;
					}
					else if (GC.coopMode)
					{
						__instance.AddButton("EndCoop");
						__instance.AddButton("Start3Player");
						__instance.AddButton("Start4Player");

						if (!GC.consoleVersion)
							__instance.AddButton("StartOnline");
						
						return false;
					}
					else if (GC.fourPlayerMode && GC.sessionDataBig.threePlayer)
					{
						__instance.AddButton("EndCoop");
						__instance.AddButton("StartCoop");
						__instance.AddButton("Start4Player");

						if (!GC.consoleVersion)
							__instance.AddButton("StartOnline");
						
						return false;
					}
					else
					{
						if (!GC.fourPlayerMode || GC.sessionDataBig.threePlayer)
							return false;
						
						__instance.AddButton("EndCoop");
						__instance.AddButton("StartCoop");
						__instance.AddButton("Start3Player");
						
						if (!GC.consoleVersion)
							__instance.AddButton("StartOnline");
						
						return false;
					}
				}
				else if (agentName == "Hacker")
				{
					__instance.AddButton("Challenges");
					__instance.AddButton("MutatorConfigs");
					return false;
				}
				else if (agentName == "Thief")
				{
					__instance.AddButton("UnlockItems");
					__instance.AddButton("RewardConfigs");
					return false;
				}
				else if (agentName == "Scientist")
				{
					if (!GC.serverPlayer || interactingAgent.isPlayer != 1)
					{
						agent.SayDialogue("CantSetSeed");
						return false;
					}

					__instance.AddButton("SetSeed");
					
					if (GC.sessionDataBig.userSetSeed != "" && GC.sessionDataBig.userSetSeed != null)
						__instance.AddButton("ClearSeed");
					
					return false;
				}
				else if (agentName == "Soldier")
				{
					__instance.AddButton("Loadouts"); //
					return false;
				}

				return false;
			}

			if (GC.levelType == "Tutorial")
			{
				string agentName = agent.agentName;

				if (agentName == "Soldier")
				{
					agent.SayDialogue("Interact");
					GC.audioHandler.Play(agent, "AgentTalk");
					__instance.AddButton("EndTutorial");
					return false;
				}
			}

			if (agent.mechEmpty)
			{
				if (interactingAgent.statusEffects.hasSpecialAbility("MechTransform"))
				{
					if (!interactingAgent.transforming)
					{
						if (agent.health <= 1f)
						{
							interactingAgent.SayDialogue("MechNeedsOil");
							InvItem invItem = interactingAgent.inventory.FindItem("OilContainer");

							if (invItem == null)
							{
								GC.audioHandler.Play(interactingAgent, "CantDo");
								return false;
							}
							
							if (invItem.invItemCount > 0)
							{
								__instance.AddButton("GiveMechOil");
								return false;
							}
							
							GC.audioHandler.Play(interactingAgent, "CantDo");
							return false;
						}
						else
						{
							if (agent.health < agent.healthMax)
							{
								__instance.AddButton("EnterMech");
								__instance.AddButton("GiveMechOil");
								return false;
							}

							interactingAgent.statusEffects.PressedSpecialAbility();
							return false;
						}
					}
				}
				else
				{
					if (interactingAgent.mechFilled)
					{
						interactingAgent.SayDialogue("CantMechUseAugmentationBooth");
						return false;
					}

					interactingAgent.SayDialogue("CantTransformMech");
					return false;
				}
			}

			if (agent.oma.bodyGuarded && interactingAgent.statusEffects.hasTrait("Bodyguard") && agent.dead)
			{
				if (interactingAgent.statusEffects.hasTrait("MusicianTakesLessHealth") || (interactingAgent.oma.superSpecialAbility && interactingAgent.agentName == "Bouncer"))
				{
					if (GC.challenges.Contains("LowHealth"))
					{
						__instance.AddButton("ReviveBodyguard", " - 7HP");
						return false;
					}
					__instance.AddButton("ReviveBodyguard", " - 15HP");
					return false;
				}
				else
				{
					if (GC.challenges.Contains("LowHealth"))
					{
						__instance.AddButton("ReviveBodyguard", " - 15HP");
						return false;
					}
					__instance.AddButton("ReviveBodyguard", " - 30HP");
					return false;
				}
			}
			else
			{
				if (!agent.oma.mindControlled)
				{
					#region Agent-Hacking
					// It was just too damn ugly to refactor.
					if (interactingAgent.interactionHelper.interactingFar && !interactingAgent.interactionHelper.interactingCounter)
					{
						bool flag2 = true;

						if ((interactingAgent.agentName == "Hacker" && interactingAgent.oma.superSpecialAbility) || interactingAgent.statusEffects.hasTrait("HackImmediate"))
							flag2 = false;
						
						string agentName = agent.agentName;
						
						if (!(agentName == "Robot"))
						{
							if (!(agentName == "CopBot"))
							{
								if (!(agentName == "RobotPlayer"))
								{
									if (!(agentName == "Slave"))
									{
										if (agent.slaveOwners.Count > 0)
										{
											if (!agent.justFinishedOperating && flag2)
												agent.StartCoroutine(agent.Operating(interactingAgent, interactingAgent.interactingUsingItem, 2f, false, "Hacking"));
											else
											{
												__instance.AddButton("DeactivateHelmet");
												__instance.AddButton("BlowUpHelmet");
											}
										}
									}
									else if (!agent.justFinishedOperating && flag2)
										agent.StartCoroutine(agent.Operating(interactingAgent, interactingAgent.interactingUsingItem, 2f, false, "Hacking"));
									else
									{
										__instance.AddButton("DeactivateHelmet");
										__instance.AddButton("BlowUpHelmet");
									}
								}
								else if (!agent.justFinishedOperating && flag2)
									agent.StartCoroutine(agent.Operating(interactingAgent, interactingAgent.interactingUsingItem, 2f, false, "Hacking"));
								else
								{
									GC.unlocks.DoUnlock("RobotPlayer", "Agent");
									__instance.AddButton("RobotEnrage");
								}
							}
							else if (!agent.justFinishedOperating && flag2)
								agent.StartCoroutine(agent.Operating(interactingAgent, interactingAgent.interactingUsingItem, 2f, false, "Hacking"));
							else
							{
								GC.unlocks.DoUnlock("RobotPlayer", "Agent");
								__instance.AddButton("RobotEnrage");
							}
						}
						else if (!agent.justFinishedOperating && flag2)
							agent.StartCoroutine(agent.Operating(interactingAgent, interactingAgent.interactingUsingItem, 2f, false, "Hacking"));
						else
						{
							GC.unlocks.DoUnlock("RobotPlayer", "Agent");
							__instance.AddButton("TamperRobotAim");
						}
						if (!flag2)
							agent.ShowObjectButtons();

						return false;
					}
					#endregion

					if (!GC.serverPlayer && agent.relationships.GetRelCode(interactingAgent) == relStatus.Annoyed && 
						(agent.gang != interactingAgent.gangMugging || agent.gang == 0) && 
						agent.doingMugging != interactingAgent.agentID && !interactingAgent.statusEffects.hasTrait("Mugger") && !interactingAgent.statusEffects.hasTrait("Mugger2"))
						return false;

					if (agent.oma.modProtectsProperty > 0)
					{
						string rel = agent.relationships.GetRel(interactingAgent);

						if (rel == "Hateful" || rel == "Annoyed" || rel == "Neutral")
						{
							if (agent.ModProtectsProperty2Check(interactingAgent) && (!(rel == "Annoyed") || (!interactingAgent.statusEffects.hasTrait("Mugger") && !interactingAgent.statusEffects.hasTrait("Mugger2"))))
							{
								agent.relationships.ProtectOwnedLight(interactingAgent, agent.relationships.GetRelationship(interactingAgent));
								agent.movement.RotateToObject(interactingAgent.go);

								if (agent.relationships.GetRel(interactingAgent) == "Annoyed" || agent.relationships.GetRel(interactingAgent) == "Hateful")
									return false;
							}
							else if (agent.oma.modProtectsProperty == 1 && (!interactingAgent.ownersNotHostile || (!interactingAgent.statusEffects.hasTrait("Mugger") && !interactingAgent.statusEffects.hasTrait("Mugger2"))))
							{
								agent.relationships.ProtectOwned(interactingAgent, agent.relationships.GetRelationship(interactingAgent));
								agent.movement.RotateToObject(interactingAgent.go);
							
								if (agent.relationships.GetRel(interactingAgent) == "Hateful")
									return false;
							}
						}
					}

					if (!agent.CanUnderstandEachOther(interactingAgent, false, true))
					{
						if (agent.statusEffects.hasStatusEffect("HearingBlocked"))
							agent.SayDialogue("CantHear");
						else
							agent.SayDialogue("Interact");
						
						GC.audioHandler.Play(agent, "AgentTalk");
						agent.movement.RotateToObject(interactingAgent.go);
						
						return false;
					}

					if (interactingAgent.statusEffects.hasTrait("EveryoneHatesZombie") && agent.relationships.GetRel(interactingAgent) != "Aligned" && agent.relationships.GetRel(interactingAgent) != "Loyal" && agent.relationships.GetRel(interactingAgent) != "Friendly" && agent.relationships.GetRel(interactingAgent) != "Submissive" && !agent.zombified && agent.questGiverQuest == null && agent.questEnderQuest == null && !agent.oma.bodyGuarded)
					{
						if (agent.ModProtectsProperty2Check(interactingAgent))
							agent.relationships.ProtectOwnedLight(interactingAgent, agent.relationships.GetRelationship(interactingAgent));
						else if (agent.oma.modProtectsProperty == 1)
							agent.relationships.ProtectOwned(interactingAgent, agent.relationships.GetRelationship(interactingAgent));
						
						agent.movement.RotateToObject(interactingAgent.go);
						return false;
					}

					if (agent.relationships.GetRelCode(interactingAgent) == relStatus.Annoyed && agent.relationships.GetStrikes(interactingAgent) > 1f && 
						(agent.gang != interactingAgent.gangMugging || agent.gang == 0) && 
						agent.doingMugging != interactingAgent.agentID && !interactingAgent.statusEffects.hasTrait("Mugger") && !interactingAgent.statusEffects.hasTrait("Mugger2") && !agent.oma.bodyGuarded)
						return false;
					
					if (agent.extraVarString4 != "" && agent.extraVarString4 != null)
						__instance.AddButton("TalkAgent");
					
					if (agent.questGiverQuest != null && agent.questGiverQuest.questStatus != "Failed" && agent.questGiverQuest.questStatus != "Done")
					{
						__instance.AddButton("Quest");
						flag = true;
					}
					
					if (agent.questEnderQuest != null && !flag && agent.questEnderQuest.questStatus == "Completed")
						__instance.AddButton("Quest");
					
					if (agent.oma.guardTarget && interactingAgent.bigQuest == "Guard")
					{
						bool flag3 = true;

						for (int j = 0; j < GC.agentList.Count; j++)
							if (GC.agentList[j].guardTargetKill && GC.agentList[j] != agent)
							{
								flag3 = false;
								break;
							}
						
						if (flag3)
							__instance.AddButton("StartGuardSequence");
					}
				
					if (agent.oma.hasCourierPackage && interactingAgent.bigQuest == "Courier" && (interactingAgent.inventory.HasItem("CourierPackage") || interactingAgent.inventory.HasItem("CourierPackageBroken")))
						__instance.AddButton("GetCourierPackage");
				}

				bool hasKey = agent.inventory.HasItem("Key");
				bool hasSafeCombo = agent.inventory.HasItem("SafeCombination");

				if (GC.multiplayerMode && !GC.serverPlayer)
				{
					if (agent.oma.hasKey)
						hasKey = true;
				
					if (agent.oma.hasSafeCombination)
						hasSafeCombo = true;
				}

				if ((agent.relationships.GetRel(interactingAgent) == "Aligned" || agent.relationships.GetRel(interactingAgent) == "Loyal" || agent.relationships.GetRel(interactingAgent) == "Submissive") && 
					agent.isPlayer == 0 && agent.startingChunkRealDescription != "Generic")
				{
					if (hasKey && hasSafeCombo)
						__instance.AddButton("GiveMeKeyAndSafeCombination");
					else if (hasKey)
						__instance.AddButton("GiveMeKey");
					else if (hasSafeCombo)
						__instance.AddButton("GiveMeSafeCombination");
				}
				else if (interactingAgent.statusEffects.hasTrait("ArtOfTheDeal") && agent.isPlayer == 0 && agent.startingChunkRealDescription != "Generic")
				{
					if (hasKey && agent.startingChunkRealDescription != "Hotel" && hasSafeCombo)
						__instance.AddButton("BuyKeyAndSafeCombination", agent.determineMoneyCost("BuyKeyAndSafeCombination"));
					else if (hasKey && agent.startingChunkRealDescription != "Hotel")
						__instance.AddButton("BuyKey", agent.determineMoneyCost("BuyKey"));
					else if (hasSafeCombo)
						__instance.AddButton("BuySafeCombination", agent.determineMoneyCost("BuySafeCombination"));
				}

				bool isAliveNotInteractWFormerOwner = false;
				bool canCommandStandGuard = false;
				bool CanCommandAttack = false;
				bool flag9 = false;
				bool flag10 = false;
				bool canBeHealed = false;
				bool flag12 = false;

				if (agent.employer == interactingAgent)
				{
					if (!agent.dead)
						isAliveNotInteractWFormerOwner = true;
					
					if (agent.CanCommandStandGuard(agent, interactingAgent) && !agent.oma.bodyGuarded)
						canCommandStandGuard = true;
					
					if (agent.CanCommandAttack(agent, interactingAgent) && !agent.oma.bodyGuarded)
						CanCommandAttack = true;
					
					if (agent.slaveOwners.Contains(interactingAgent) && !agent.oma.bodyGuarded)
						flag9 = true;
					
					flag10 = true;
					
					if (agent.oma.bodyGuarded ||
						(agent.rescueForQuest != null && agent.rescueForQuest.questStatus != "Done") ||
						(!GC.serverPlayer && agent.oma.rescuingForQuest) ||
						agent.slaveOwners.Contains(interactingAgent))
						flag10 = false;
				
					if (interactingAgent.statusEffects.hasTrait("MedicalProfessional"))
						canBeHealed = true;
				}

				if (agent.formerEmployer == interactingAgent)
				{
					if (!agent.dead && !agent.formerSlaveOwners.Contains(interactingAgent))
						isAliveNotInteractWFormerOwner = true;
				
					if (interactingAgent.statusEffects.hasTrait("MedicalProfessional"))
						canBeHealed = true;
				}

				bool flag13 = false;
				Quest quest = null;
				Quest quest2 = null;

				if (!GC.serverPlayer)
				{
					if (agent.oma.hasQuestItem && agent.oma.questNum != -1)
					{
						quest2 = GC.mainGUI.questSheetScript.questSlot[agent.oma.questNum].myQuest;

						if (quest2.questType == "KillAndRetrieve" && quest2.questStatus != "Failed" && quest2.questStatus != "NotAccepted")
							flag13 = true;
					}
					else if (agent.killForQuest != null && (agent.killForQuest.questType == "Kill" || agent.killForQuest.questType == "KillAll"))
						quest = agent.killForQuest;
				}
				else if (agent.killForQuest != null)
				{
					if (agent.killForQuest.questType == "Kill" || agent.killForQuest.questType == "KillAll")
						quest = agent.killForQuest;
					else
						quest2 = agent.killForQuest;
				}

				if (quest2 != null && quest2.questType == "KillAndRetrieve" && quest2.questStatus != "Failed" && quest2.questStatus != "NotAccepted")
				{
					using (List<InvItem>.Enumerator enumerator = agent.inventory.InvItemList.GetEnumerator())
						while (enumerator.MoveNext())
							if (enumerator.Current.questItem)
								flag13 = true;
				}

				if (flag13)
				{
					if (agent.relationships.GetRel(interactingAgent) == "Aligned" || agent.relationships.GetRel(interactingAgent) == "Loyal" || agent.relationships.GetRel(interactingAgent) == "Submissive")
						__instance.AddButton("GiveMeQuestItem");
					else
					{
						__instance.AddButton("BribeQuestItem");
						__instance.AddButton("BribeQuestMoney", agent.determineMoneyCost("BribeQuest"));
						__instance.AddButton("Threaten", " (" + agent.relationships.FindThreat(interactingAgent, false) + "%)");
					}
				}

				if (agent.oma.mindControlled)
				{
					agent.SayDialogue("MindControlInteract");
					bool flag14 = false;

					if (agent.inventory.HasItem("MayorBadge"))
						flag14 = true;
					
					if (GC.multiplayerMode && !GC.serverPlayer && agent.oma.hasMayorBadge)
						flag14 = true;
					
					if (!interactingAgent.inventory.HasItem("MayorBadge") && flag14)
						__instance.AddButton("GiveMeMayorBadge");
					
					if (agent.inventory.equippedArmorHead != null && agent.inventory.equippedArmorHead.invItemName == "MayorHat")
						__instance.AddButton("MayorGiveHat");
					
					__instance.AddButton("GiveItem");
					return false;
				}

				if (agent.sleeping)
					agent.sleeping = false;
				
				if ((agent.isPlayer > 0 || agent.playerColor != 0) && agent.ghost && !interactingAgent.ghost)
				{
					__instance.AddButton("ReviveTakeHealth");
					__instance.AddButton("Revive", agent.determineMoneyCost("Revive"));
				}
				else if ((agent.isPlayer <= 0 || !agent.KnockedOut() || interactingAgent.ghost) && agent.isPlayer <= 0)
				{
					if (agent.oma.rescuingForQuest && agent.agentName != "Slave")
					{
						if (agent.agentName == "Gorilla")
							agent.SayDialogue("InteractB");
						else
							agent.SayDialogue("Interact");
						
						GC.audioHandler.Play(agent, "AgentTalk");
						agent.movement.RotateToObject(interactingAgent.go);
					}
					else
					{
						string agentName = agent.agentName;

						if (agentName == "Alien")
						{
							if (interactingAgent.agentName == "Alien")
								agent.SayDialogue("InteractB");
							else
								agent.SayDialogue("Interact");
							
							GC.audioHandler.Play(agent, "AgentTalk");
							goto IL_48BF;
						}
						else if (agentName == "Athlete")
						{
							bool flag15 = false;
							GC.audioHandler.Play(agent, "AgentTalk");

							if (agent.hasSpecialInvDatabase)
							{
								__instance.AddButton("Buy");
							
								if (interactingAgent.inventory.HasItem("FreeItemVoucher"))
									__instance.AddButton("UseVoucher");
							}

							if (interactingAgent.statusEffects.hasTrait("CanBorrowMoney"))
								__instance.AddButton("BorrowMoney");
							
							if (agent.startingChunkRealDescription == "Arena")
							{
								int l = 0;

								while (l < GC.objectRealList.Count)
								{
									ObjectReal objectReal = GC.objectRealList[l];

									if (objectReal.startingChunk == agent.startingChunk && objectReal.objectName == "EventTriggerFloor")
									{
										EventTriggerFloor eventTriggerFloor = (EventTriggerFloor)objectReal;

										if (!eventTriggerFloor.functional && eventTriggerFloor.triggerState != "NeedToPayOut" && eventTriggerFloor.triggerState != "NoPayout" && eventTriggerFloor.triggerState != "Cheated")
										{
											agent.SayDialogue("NoMoreFights");
											flag15 = true;
											break;
										}

										if (eventTriggerFloor.triggerState == "Initial")
										{
											bool flag16 = false;

											for (int m = 0; m < GC.agentList.Count; m++)
											{
												Agent agent3 = GC.agentList[m];

												if ((agent3.isPlayer > 0 || agent3.employer != null) && agent3 != interactingAgent)
													flag16 = true;
											}

											if (flag16)
												agent.SayDialogue("AskToStartFightMultiple");
											else
												agent.SayDialogue("AskToStartFight");
											
											flag15 = true;
											__instance.AddButton("SignUpToFight");
											break;
										}

										if (eventTriggerFloor.triggerState == "FightSignedUp")
										{
											agent.SayDialogue("SignedUpToFight");
											flag15 = true;
											break;
										}
										
										if (eventTriggerFloor.triggerState == "FightStarted")
										{
											agent.SayDialogue("FightCheer");
											flag15 = true;
											break;
										}
										
										if (eventTriggerFloor.triggerState == "NeedToPayOut")
										{
											agent.SayDialogue("NoMoreFights");
											flag15 = true;
											__instance.AddButton("PayOutFight");
											break;
										}
										
										if (eventTriggerFloor.triggerState == "NoPayout")
										{
											agent.SayDialogue("LostFightNoPayout");
											flag15 = true;
											break;
										}
										
										if (eventTriggerFloor.triggerState == "Cheated")
										{
											agent.SayDialogue("CheatedNoPayout");
											flag15 = true;
											break;
										}
										
										if (eventTriggerFloor.triggerState == "FightsOver")
										{
											agent.SayDialogue("NoMoreFights");
											flag15 = true;
											break;
										}
										
										break;
									}
									else
										l++;
								}
							}

							if (!flag15)
							{
								agent.SayDialogue("Interact");
								goto IL_48BF;
							}
							
							goto IL_48BF;
						}
						else if (agentName == "Bartender")
						{
							agent.SayDialogue("Interact");
							GC.audioHandler.Play(agent, "AgentTalk");

							if (agent.hasSpecialInvDatabase)
							{
								__instance.AddButton("Buy");
							
								if (interactingAgent.inventory.HasItem("FreeItemVoucher"))
									__instance.AddButton("UseVoucher");
								
								if (agent.startingChunk != 0 && GC.tileInfo.IsIndoors(agent.tr.position) && GC.tileInfo.GetTileData(agent.tr.position).chunkID == agent.startingChunk)
								{
									List<Agent> list = new List<Agent>();
								
									for (int num8 = 0; num8 < GC.agentList.Count; num8++)
									{
										Agent agent10 = GC.agentList[num8];
									
										if (agent10.startingChunk == agent.startingChunk && agent10 != agent && agent10.prisoner == 0 && !agent10.dead && !agent10.zombified && !agent10.oma.mindControlled && agent10.gc.tileInfo.GetTileData(agent10.tr.position).chunkID == agent10.startingChunk)
										{
											string rel3 = agent10.relationships.GetRel(interactingAgent);
										
											if (rel3 != "Hateful" && rel3 != "Aligned" && rel3 != "Loyal" && rel3 != "Submissive" && !agent10.statusEffects.hasTrait("BloodRestoresHealth") && !agent10.statusEffects.hasTrait("OilRestoresHealth"))
												list.Add(agent10);
										}
									}

									if (list.Count > 0)
										__instance.AddButton("BuyRound", agent.determineMoneyCost(list.Count, "BuyRound"));
								}
							}

							if (interactingAgent.statusEffects.hasTrait("CanBorrowMoney"))
							{
								__instance.AddButton("BorrowMoney");
								goto IL_48BF;
							}

							goto IL_48BF;
						}
						else if (agentName == "Bouncer")
						{
							GC.audioHandler.Play(agent, "AgentTalk");
							string rel2 = agent.relationships.GetRel(interactingAgent);
							bool flag19 = false;

							for (int num2 = 0; num2 < GC.agentList.Count; num2++)
							{
								Agent agent5 = GC.agentList[num2];

								if (agent5.startingChunk == agent.startingChunk && agent5.ownerID == agent.ownerID && agent5 != agent && (agent5.oma.modProtectsProperty != 0 || agent5.agentName == "Bouncer"))
								{
									relStatus relCode = agent5.relationships.GetRelCode(interactingAgent);

									if (relCode != relStatus.Friendly && relCode != relStatus.Loyal && relCode != relStatus.Aligned && relCode != relStatus.Submissive)
										flag19 = true;
								}
							}

							if (__instance.HasMetalDetector(agent, interactingAgent))
							{
								if (agent.startingChunkRealDescription == "Arena")
								{
									for (int num3 = 0; num3 < GC.objectRealList.Count; num3++)
									{
										ObjectReal objectReal2 = GC.objectRealList[num3];
							
										if (objectReal2.startingChunk == agent.startingChunk && objectReal2.objectName == "EventTriggerFloor")
										{
											EventTriggerFloor eventTriggerFloor2 = (EventTriggerFloor)objectReal2;
										
											if (eventTriggerFloor2.triggerState == "Initial")
												agent.SayDialogue("TalkToAthlete");
											else if (eventTriggerFloor2.triggerState == "FightSignedUp")
											{
												agent.SayDialogue("CanKeepWeaponsSafe");
											
												if (interactingAgent.inventory.HasWeapons())
													__instance.AddButton("LeaveWeaponsBehind");
												for (int num4 = 0; num4 < GC.agentList.Count; num4++)
												{
													Agent agent6 = GC.agentList[num4];
												
													if (agent6.employer == interactingAgent && agent6.inventory.HasWeapons() && !agent6.inCombat && agent6.jobCode != jobType.GoHere && Vector2.Distance(agent.tr.position, agent6.tr.position) < 13.44f)
													{
														__instance.AddButton("FollowersLeaveWeaponsBehind");
														break;
													}
												}
											}
											else
												agent.SayDialogue("Interact");
										}
									}

									goto IL_48BF;
								}

								if (agent.startingChunkRealDescription == "Generic")
								{
									if (rel2 == "Aligned" || rel2 == "Loyal" || rel2 == "Submissive" || interactingAgent.agentName == "Cop2")
									{
										agent.SayDialogue("DontNeedMoney");
										__instance.BouncerTurnOffLaserEmitter(agent, interactingAgent, true);
										goto IL_48BF;
									}
								
									agent.SayDialogue("CanKeepWeaponsSafe");
									
									if (interactingAgent.inventory.HasWeapons())
										__instance.AddButton("LeaveWeaponsBehind");
									for (int num5 = 0; num5 < GC.agentList.Count; num5++)
									{
										Agent agent7 = GC.agentList[num5];
									
										if (agent7.employer == interactingAgent && agent7.inventory.HasWeapons() && !agent7.inCombat && agent7.jobCode != jobType.GoHere && Vector2.Distance(agent.tr.position, agent7.tr.position) < 13.44f)
										{
											__instance.AddButton("FollowersLeaveWeaponsBehind");
											break;
										}
									}

									__instance.AddButton("BribeMayorElevator", agent.determineMoneyCost("BribeMayorElevator"));
									goto IL_48BF;
								}
								else
								{
									bool flag20 = false;

									for (int num6 = 0; num6 < GC.agentList.Count; num6++)
									{
										Agent agent8 = GC.agentList[num6];
									
										if (agent8.oma.guardTarget && agent8.ownerID == agent.ownerID && agent8.startingChunk == agent.startingChunk)
										{
											flag20 = true;
											break;
										}
									}

									if ((rel2 == "Friendly" || rel2 == "Aligned" || rel2 == "Loyal" || rel2 == "Submissive" || interactingAgent.agentName == "Cop2") && flag20)
									{
										agent.SayDialogue("DontNeedMoney");
										__instance.BouncerTurnOffLaserEmitter(agent, interactingAgent, false);
										goto IL_48BF;
									}
									
									agent.SayDialogue("CanKeepWeaponsSafe");
									
									if (interactingAgent.inventory.HasWeapons())
										__instance.AddButton("LeaveWeaponsBehind");
									
									for (int num7 = 0; num7 < GC.agentList.Count; num7++)
									{
										Agent agent9 = GC.agentList[num7];
									
										if (agent9.employer == interactingAgent && agent9.inventory.HasWeapons() && !agent9.inCombat && agent9.jobCode != jobType.GoHere && Vector2.Distance(agent.tr.position, agent9.tr.position) < 13.44f)
										{
											__instance.AddButton("FollowersLeaveWeaponsBehind");
											break;
										}
									}

									goto IL_48BF;
								}
							}
							else
							{
								if (agent.startingChunkRealDescription == "DeportationCenter")
								{
									agent.SayDialogue("InteractB");
									goto IL_48BF;
								}

								if (agent.startingChunk == 0)
								{
									agent.SayDialogue("Interact");
									goto IL_48BF;
								}
								
								if ((rel2 == "Friendly" || rel2 == "Aligned" || rel2 == "Loyal" || rel2 == "Submissive") && (!flag19 || rel2 == "Submissive"))
								{
									agent.SayDialogue("DontNeedMoney");
									goto IL_48BF;
								}
								
								if (interactingAgent.statusEffects.hasTrait("Unlikeable") || interactingAgent.statusEffects.hasTrait("Naked") || interactingAgent.statusEffects.hasTrait("Suspicious"))
								{
									agent.SayDialogue("WontJoinA");
									goto IL_48BF;
								}
								
								agent.SayDialogue("Interact");
								
								if (interactingAgent.inventory.HasItem("Beer"))
									__instance.AddButton("BribeBeer");
								else if (interactingAgent.inventory.HasItem("Whiskey"))
									__instance.AddButton("BribeWhiskey");
								
								if (agent.startingChunkRealDescription == "Bar" || agent.startingChunkRealDescription == "DanceClub" || agent.startingChunkRealDescription == "Arena" || agent.startingChunkRealDescription == "MusicHall")
								{
									__instance.AddButton("PayEntranceFee", agent.determineMoneyCost("Bribe"));
									goto IL_48BF;
								}
								
								__instance.AddButton("Bribe", agent.determineMoneyCost("Bribe"));
								goto IL_48BF;
							}
						}
						else if (agentName == "ButlerBot")
						{
							agent.SayDialogue("Interact");
							GC.audioHandler.Play(agent, "AgentTalk");
							goto IL_48BF;
						}
						else if (agentName == "Cop")
						{
							GC.audioHandler.Play(agent, "AgentTalk");

							if (interactingAgent.enforcer)
							{
								agent.SayDialogue("InteractB");
								goto IL_48BF;
							}
							
							if (interactingAgent.aboveTheLaw || interactingAgent.upperCrusty)
							{
								agent.SayDialogue("DontNeedMoney");
								goto IL_48BF;
							}
							
							if (interactingAgent.statusEffects.hasStatusEffect("OweCops1") || interactingAgent.statusEffects.hasStatusEffect("OweCops2"))
							{
								agent.SayDialogue("Interact");
							
								if (interactingAgent.statusEffects.hasStatusEffect("OweCops1"))
								{
									__instance.AddButton("PayCops", agent.determineMoneyCost("PayCops1"));
									goto IL_48BF;
								}
								
								if (interactingAgent.statusEffects.hasStatusEffect("OweCops2"))
								{
									__instance.AddButton("PayCops", agent.determineMoneyCost("PayCops2"));
									goto IL_48BF;
								}
								
								goto IL_48BF;
							}
							else
							{
								if (!interactingAgent.statusEffects.hasTrait("MustPayCops"))
								{
									agent.SayDialogue("Interact");
									__instance.AddButton("BribeCops", agent.determineMoneyCost("BribeCops"));
									goto IL_48BF;
								}
								
								agent.SayDialogue("Interact");
								goto IL_48BF;
							}
						}
						else if (agentName == "Cop2")
						{
							GC.audioHandler.Play(agent, "AgentTalk");
						
							if (interactingAgent.enforcer)
							{
								agent.SayDialogue("InteractB");
								goto IL_48BF;
							}
							
							if (!interactingAgent.statusEffects.hasStatusEffect("OweCops1") && !interactingAgent.statusEffects.hasStatusEffect("OweCops2"))
							{
								agent.SayDialogue("Interact");
								goto IL_48BF;
							}
							
							agent.SayDialogue("Interact");
							
							if (interactingAgent.statusEffects.hasStatusEffect("OweCops1"))
								__instance.AddButton("PayCops", agent.determineMoneyCost("PayCops1"));
							else if (interactingAgent.statusEffects.hasStatusEffect("OweCops2"))
								__instance.AddButton("PayCops", agent.determineMoneyCost("PayCops2"));
							
							goto IL_48BF;
						}
						else if (agentName == "CopBot")
						{
							GC.audioHandler.Play(agent, "AgentTalk");

							if (interactingAgent.copBotAccosting == agent.agentID)
							{
								string a = agent.oma.convertIntToSecurityType(agent.oma.securityType);

								if (a == "Normal")
									__instance.AddButton("MugDrugsAlcohol");
								else if (a == "Weapons")
									__instance.AddButton("MugWeapons");
								else if (a == "ID")
									__instance.AddButton("MugID");
								
								goto IL_48BF;
							}
							else
							{
								if (agent.inhuman)
								{
									agent.SayDialogue(GC.Choose<string>("Interact_3", "Interact_4", "Interact_5" ));
									goto IL_48BF;
								}

								agent.SayDialogue("Interact");
								goto IL_48BF;
							}
						}
						else if (agentName == "Clerk")
						{
							GC.audioHandler.Play(agent, "AgentTalk");
							string rel5 = agent.relationships.GetRel(interactingAgent);

							if (agent.startingChunkRealDescription == "DeportationCenter")
							{
								if (interactingAgent.upperCrusty)
									agent.SayDialogue("NoDeportationUpperCrusty");
								else if (rel5 == "Friendly" || rel5 == "Aligned" || rel5 == "Loyal" || rel5 == "Submissive")
									agent.SayDialogue("DontNeedMoney");
								else
								{
									agent.SayDialogue("Interact");
									__instance.AddButton("BribeDeportation", agent.determineMoneyCost("BribeDeportation"));
									__instance.AddButton("BribeDeportationItem");
								}
								
								if (interactingAgent.statusEffects.hasTrait("CanBorrowMoney"))
									__instance.AddButton("BorrowMoney");
								
								goto IL_48BF;
							}
							else if (agent.startingChunkRealDescription == "Bank")
							{
								agent.SayDialogue("GenericTalk");
								__instance.AddButton("BorrowMoney");

								if (interactingAgent.statusEffects.hasStatusEffect("InDebt") || interactingAgent.statusEffects.hasStatusEffect("InDebt2") || interactingAgent.statusEffects.hasStatusEffect("InDebt3"))
									__instance.AddButton("PayBackDebt", interactingAgent.CalculateDebt());
								
								if (interactingAgent.bigQuest == "Hobo" && !GC.loadLevel.LevelContainsMayor())
									__instance.AddButton("PutMoneyTowardHome", agent.determineMoneyCost("PutMoneyTowardHome"));
								
								goto IL_48BF;
							}
							else if (agent.startingChunkRealDescription == "Hospital")
							{
								agent.SayDialogue("HospitalTalk");

								if (GC.challenges.Contains("LowHealth"))
									__instance.AddButton("GiveBlood", " - 10HP/$20");
								else
									__instance.AddButton("GiveBlood", " - 20HP/$20");
								
								if (interactingAgent.statusEffects.hasTrait("CanBorrowMoney"))
									__instance.AddButton("BorrowMoney");
								
								goto IL_48BF;
							}
							else if (agent.startingChunkRealDescription == "MovieTheater")
							{
								agent.SayDialogue("MovieTheaterTalk");

								if (agent.hasSpecialInvDatabase)
								{
									__instance.AddButton("Buy");
								
									if (interactingAgent.inventory.HasItem("FreeItemVoucher"))
										__instance.AddButton("UseVoucher");
								}

								if (interactingAgent.statusEffects.hasTrait("CanBorrowMoney"))
									__instance.AddButton("BorrowMoney");
								
								goto IL_48BF;
							}
							else if (agent.startingChunkRealDescription == "Hotel")
							{
								agent.SayDialogue("GenericTalk");

								if (hasKey)
									__instance.AddButton("BuyKeyHotel", agent.determineMoneyCost("BuyKey"));
								
								if (interactingAgent.statusEffects.hasTrait("CanBorrowMoney"))
									__instance.AddButton("BorrowMoney");
								
								goto IL_48BF;
							}
							else if (agent.startingChunkRealDescription == "OfficeBuilding" || agent.startingChunkRealDescription == "FireStation")
							{
								agent.SayDialogue("OfficeBuildingTalk");

								if (interactingAgent.statusEffects.hasTrait("CanBorrowMoney"))
									__instance.AddButton("BorrowMoney");
								
								goto IL_48BF;
							}
							else
							{
								if (!(agent.startingChunkRealDescription == "MayorOffice"))
								{
									agent.SayDialogue("Interact");
									goto IL_48BF;
								}

								EventTriggerFloor eventTrigger2 = __instance.GetEventTrigger("MayorElectionResultsPosition");
								bool flag23 = false;
								
								if (eventTrigger2 != null)
								{
									if (eventTrigger2.triggerState == "RunningElection" || eventTrigger2.triggerState == "ReadyToAnnounceElectionWinner" || eventTrigger2.triggerState == "AnnouncingElectionWinner" || eventTrigger2.triggerState == "ElectionWinnerAnnouncedPlayer" || eventTrigger2.triggerState == "ElectionWinnerAnnouncedMayor")
									{
										flag23 = true;

										if (agent.assignedEventTrigger2 == null)
										{
											agent.assignedEventTrigger = __instance.GetEventTrigger("ClerkElectionResultsPosition");
											agent.assignedEventTrigger2 = __instance.GetEventTrigger("MayorElectionResultsPosition");
										}
										
										if (agent.assignedEventTrigger2 != null)
										{
											if (agent.assignedEventTrigger2.triggerState == "ReadyToAnnounceElectionWinner")
											{
												agent.SayDialogue("ReadyForElectionWinner");
												__instance.AddButton("GetElectionResults");
											}
											else if (agent.assignedEventTrigger2.triggerState == "RunningElection")
											{
												if (agent.assignedEventTrigger.triggerCount == 1)
													agent.SayDialogue("MayorNotReadyForResults");
												else
													agent.SayDialogue("GetResultsAtPark");
											}
										}
									}
								}
								else
									Debug.LogError("Election Trigger Not Found");
								
								if (flag23)
									goto IL_48BF;
								
								agent.SayDialogue("GenericTalk");
								EventTriggerFloor eventTrigger3 = __instance.GetEventTrigger("MayorElectionResultsPosition");
								bool flag24 = false;
								
								if (agent.inventory.HasItem("MayorBadge"))
									flag24 = true;
								
								if (GC.multiplayerMode && !GC.serverPlayer && agent.oma.hasMayorBadge)
									flag24 = true;
								
								if (!interactingAgent.inventory.HasItem("MayorBadge") && flag24)
								{
									if (agent.relationships.GetRel(interactingAgent) == "Aligned" || agent.relationships.GetRel(interactingAgent) == "Loyal" || agent.relationships.GetRel(interactingAgent) == "Submissive")
										__instance.AddButton("GiveMeMayorBadge");
									else
									{
										__instance.AddButton("BribeMayorBadge");
										__instance.AddButton("BribeMayorBadgeMoney", agent.determineMoneyCost("BribeMayorBadge"));
										__instance.AddButton("ThreatenMayorBadge", " (" + agent.relationships.FindThreat(interactingAgent, false) + "%)");
									}
								}
								
								if (!(eventTrigger3 != null))
								{
									Debug.LogError("Election Trigger Not Found 2");
									goto IL_48BF;
								}
								
								if (eventTrigger3.triggerState != "ElectionDonePlayer" && eventTrigger3.triggerState != "ElectionDoneMayor")
								{
									__instance.AddButton("RunForOffice");
									goto IL_48BF;
								}
								
								goto IL_48BF;
							}
						}
						else if (agentName == "Courier")
						{
							GC.audioHandler.Play(agent, "AgentTalk");
							agent.SayDialogue("Interact");
							goto IL_48BF;
						}
						else if (agentName == "Doctor")
						{
							agent.SayDialogue("Interact");
							GC.audioHandler.Play(agent, "AgentTalk");
							__instance.AddButton("Heal", agent.determineMoneyCost("Heal"));
							__instance.AddButton("AdministerBloodBag", " - 20HP");

							if (interactingAgent.inventory.HasItem("BloodBag"))
								__instance.AddButton("UseBloodBag");
							
							goto IL_48BF;
						}
						else if (agentName == "DrugDealer")
						{
							agent.SayDialogue("Interact");
							GC.audioHandler.Play(agent, "AgentTalk");

							if (agent.hasSpecialInvDatabase)
							{
								__instance.AddButton("Buy");
							
								if (interactingAgent.inventory.HasItem("FreeItemVoucher"))
									__instance.AddButton("UseVoucher");
							}

							if (interactingAgent.statusEffects.hasTrait("CanBorrowMoney"))
								__instance.AddButton("BorrowMoney");
							
							goto IL_48BF;
						}
						else if (agentName == "Firefighter")
						{
							agent.SayDialogue("Interact");
							GC.audioHandler.Play(agent, "AgentTalk");
							goto IL_48BF;
						}
						else if (agentName == "Gangbanger")
						{
							agent.SayDialogue("Interact");
							GC.audioHandler.Play(agent, "AgentTalk");

							if (!(agent.employer == null) || agent.relationships.GetRelCode(interactingAgent) == relStatus.Annoyed)
								goto IL_48BF;
							
							if (interactingAgent.agentName == "Gangbanger" || (interactingAgent.agentName == "GangbangerB" && interactingAgent.oma.superSpecialAbility))
							{
								__instance.AddButton("JoinMe");
								goto IL_48BF;
							}
							
							if (interactingAgent.inventory.HasItem("HiringVoucher"))
								__instance.AddButton("HireAsProtection", 6666);
							
							__instance.AddButton("HireAsProtection", agent.determineMoneyCost("GangbangerHire"));
							goto IL_48BF;
						}
						else if (agentName == "GangbangerB")
						{
							agent.SayDialogue("Interact");
							GC.audioHandler.Play(agent, "AgentTalk");

							if (!(agent.employer == null) || agent.relationships.GetRelCode(interactingAgent) == relStatus.Annoyed)
								goto IL_48BF;
							
							if (interactingAgent.agentName == "GangbangerB" || (interactingAgent.agentName == "Gangbanger" && interactingAgent.oma.superSpecialAbility))
							{
								__instance.AddButton("JoinMe");
								goto IL_48BF;
							}
							
							if (interactingAgent.inventory.HasItem("HiringVoucher"))
								__instance.AddButton("HireAsProtection", 6666);
							
							__instance.AddButton("HireAsProtection", agent.determineMoneyCost("GangbangerHire"));
							goto IL_48BF;
						}
						else if (agentName == "Ghost")
						{
							agent.SayDialogue("Interact");
							GC.audioHandler.Play(agent, "AgentTalk");
							goto IL_48BF;
						}
						else if (agentName == "Gorilla")
						{
							if (!(interactingAgent.agentName == "Gorilla") && !interactingAgent.inventory.HasItem("Translator") && !agent.inventory.HasItem("Translator"))
							{
								agent.SayDialogue("Interact");
								GC.audioHandler.Play(agent, "AgentTalk");
								agent.movement.RotateToObject(interactingAgent.go);
								goto IL_48BF;
							}

							agent.SayDialogue("InteractB");
							GC.audioHandler.Play(agent, "AgentTalk");

							if (agent.startingChunkRealDescription == "Mall" && agent.ownerID != 0)
							{
								if (agent.hasSpecialInvDatabase)
								{
									__instance.AddButton("Buy");

									if (interactingAgent.inventory.HasItem("FreeItemVoucher"))
										__instance.AddButton("UseVoucher");
								}

								if (interactingAgent.statusEffects.hasTrait("CanBorrowMoney"))
									__instance.AddButton("BorrowMoney");
								
								goto IL_48BF;
							}
							else
							{
								if (!(agent.employer == null) || agent.relationships.GetRelCode(interactingAgent) == relStatus.Annoyed)
									goto IL_48BF;
								
								if (interactingAgent.agentName != "Gorilla")
								{
									if (interactingAgent.inventory.HasItem("HiringVoucher"))
										__instance.AddButton("HireAsProtection", 6666);
								
									__instance.AddButton("HireAsProtection", 6789);
									goto IL_48BF;
								}

								__instance.AddButton("JoinMe");
								goto IL_48BF;
							}
						}
						else if (agentName == "Guard")
						{
							agent.SayDialogue("Interact");
							GC.audioHandler.Play(agent, "AgentTalk");
							goto IL_48BF;
						}
						else if (agentName == "Guard2")
						{
							agent.SayDialogue("Interact");
							GC.audioHandler.Play(agent, "AgentTalk");
							goto IL_48BF;
						}
						else if (agentName == "Hacker")
						{
							agent.SayDialogue("Interact");
							GC.audioHandler.Play(agent, "AgentTalk");
							agent.movement.RotateToObject(interactingAgent.go);

							if (agent.employer == null)
							{
								if (agent.relationships.GetRelCode(interactingAgent) != relStatus.Annoyed)
								{
									if (interactingAgent.inventory.HasItem("HiringVoucher"))
										__instance.AddButton("AssistMe", 6666);
									
									__instance.AddButton("AssistMe", agent.determineMoneyCost("HackerAssist"));
								}

								goto IL_48BF;
							}
							else
							{
								if (!agent.oma.cantDoMoreTasks)
									__instance.AddButton("HackSomething");
								
								goto IL_48BF;
							}
						}
						else if (agentName == "Hobo")
						{
							agent.SayDialogue("Interact");
							GC.audioHandler.Play(agent, "AgentTalk");

							if (!(agent.employer == null))
								__instance.AddButton("CauseRuckus");
							else if (agent.relationships.GetRelCode(interactingAgent) != relStatus.Annoyed)
							{
								if (interactingAgent.inventory.HasItem("HiringVoucher"))
									__instance.AddButton("AssistMe", 6666);
							
								__instance.AddButton("AssistMe", agent.determineMoneyCost("HoboAssist"));
							}

							goto IL_48BF;
						}
						else if (agentName == "Mafia")
						{
							GC.audioHandler.Play(agent, "AgentTalk");

							if (agent.gang == interactingAgent.gangMugging && agent.gang != 0)
							{
								__instance.AddButton("MugMoney", agent.determineMoneyCost("Mug"));
								__instance.AddButton("MugItem");
							
								for (int k = 0; k < GC.agentList.Count; k++)
									if (GC.agentList[k].slaveOwners.Contains(interactingAgent))
									{
										__instance.AddButton("MugSlave");
										break;
									}
							}
							else
								agent.SayDialogue("Interact");
							
							if (!GC.sessionData.electionBribedMob[interactingAgent.isPlayer])
								__instance.AddButton("ElectionBribe", agent.determineMoneyCost("ElectionBribe"));
							
							goto IL_48BF;
						}
						else if (agentName == "RobotPlayer")
						{
							GC.audioHandler.Play(agent, "AgentTalk");
							agent.SayDialogue("Interact");
							goto IL_48BF;
						}
						else if (agentName == "Mayor")
						{
							GC.audioHandler.Play(agent, "AgentTalk");

							if (agent.inventory.equippedArmorHead == null)
							{
								agent.SayDialogue("InteractC");
								goto IL_48BF;
							}
							
							if (!(agent.inventory.equippedArmorHead.invItemName == "MayorHat"))
							{
								agent.SayDialogue("InteractC");
								goto IL_48BF;
							}
							
							EventTriggerFloor eventTrigger = __instance.GetEventTrigger("MayorElectionResultsPosition");
							bool flag21 = false;
							
							if (eventTrigger != null)
							{
								if (eventTrigger.triggerState == "ElectionDonePlayer" && (eventTrigger.triggerAgent == interactingAgent || (GC.multiplayerMode && eventTrigger.triggerInt == interactingAgent.playerColor)))
								{
									flag21 = true;
									agent.SayDialogue("InteractB");
									__instance.AddButton("MayorGiveHat");
								}
							}
							else
								Debug.LogError("Election Trigger Not Found 3");
							
							if (flag21)
								goto IL_48BF;
							
							agent.SayDialogue("Interact");
							bool flag22 = false;
							
							if (interactingAgent.inventory.equippedArmor != null && interactingAgent.inventory.equippedArmor.invItemName == "MayorBadge")
								flag22 = true;
							
							string rel4 = agent.relationships.GetRel(interactingAgent);
							
							if (!flag22 && rel4 != "Friendly" && rel4 != "Loyal" && rel4 != "Aligned" && rel4 != "Submissive")
							{
								agent.SayDialogue("NeedBadge");
								goto IL_48BF;
							}
							
							if (interactingAgent.bigQuest == "Wrestler" && GC.quests.CheckIfBigQuestCompleteRun(interactingAgent, true))
								__instance.AddButton("ChallengeToFight");
							
							if ((rel4 == "Friendly" || rel4 == "Aligned" || rel4 == "Loyal" || rel4 == "Submissive") && !agent.oma.didAsk)
							{
								__instance.AddButton("AskMayorHat", " (" + agent.relationships.FindAskMayorHatPercentage(interactingAgent, false) + "%)");
								goto IL_48BF;
							}
							
							__instance.AddButton("ThreatenMayor", " (" + agent.relationships.FindThreat(interactingAgent, false) + "%)");
							goto IL_48BF;
						}
						else if (agentName == "Musician")
						{
							if (!agent.oma.bodyGuarded || !interactingAgent.statusEffects.hasTrait("Bodyguard"))
							{
								agent.SayDialogue("Interact");
								GC.audioHandler.Play(agent, "AgentTalk");

								for (int num9 = 0; num9 < GC.objectRealList.Count; num9++)
								{
									ObjectReal objectReal3 = GC.objectRealList[num9];

									if (objectReal3.objectName == "Turntables" && objectReal3.startingChunk == agent.startingChunk && !objectReal3.destroyed && objectReal3.functional && Vector2.Distance(objectReal3.tr.position, agent.tr.position) < 1.28f)
									{
										Turntables turntables = (Turntables)objectReal3;

										if (turntables.SpeakersFunctional() && !turntables.badMusicPlaying)
										{
											if (interactingAgent.inventory.HasItem("MayorEvidence"))
												__instance.AddButton("PlayMayorEvidence", agent.determineMoneyCost("PlayMayorEvidence"));
											
											__instance.AddButton("PlayBadMusic", agent.determineMoneyCost("PlayBadMusic"));
										}
									}
								}

								goto IL_48BF;
							}

							if (!agent.dead)
								flag12 = true;
							
							goto IL_48BF;
						}
						else if (agentName == "OfficeDrone")
						{
							GC.audioHandler.Play(agent, "AgentTalk");
							agent.SayDialogue("Interact");

							if (!agent.oma.offeredOfficeDrone && agent.slaveOwners.Count == 0)
							{
								agent.SayDialogue("Interact");
								__instance.AddButton("OfferOfficeDrone");
							}
							else 
								agent.SayDialogue("InteractB");
							
							goto IL_48BF;
						}
						else if (agentName == "ResistanceLeader")
						{
							agent.SayDialogue("Interact");
							GC.audioHandler.Play(agent, "AgentTalk");
							goto IL_48BF;
						}
						else if (agentName == "Shopkeeper")
						{
							agent.SayDialogue("Interact");
							GC.audioHandler.Play(agent, "AgentTalk");

							if (agent.hasSpecialInvDatabase)
							{
								__instance.AddButton("Buy");
							
								if (interactingAgent.inventory.HasItem("FreeItemVoucher"))
									__instance.AddButton("UseVoucher");
							}

							if (interactingAgent.statusEffects.hasTrait("CanBorrowMoney"))
								__instance.AddButton("BorrowMoney");
							
							goto IL_48BF;
						}
						else if (agentName == "Slave")
						{
							if (!agent.slaveOwners.Contains(interactingAgent))
							{
								if (agent.slaveOwners.Count > 0)
								{
									agent.SayDialogue("Interact");
									GC.audioHandler.Play(agent, "AgentTalk");

									if (agent.slaveOwners.Contains(interactingAgent))
										goto IL_48BF;

									bool flag18 = false;
									
									using (List<InvItem>.Enumerator enumerator = interactingAgent.inventory.InvItemList.GetEnumerator())
									{
										while (enumerator.MoveNext())
											if (enumerator.Current.invItemName == "SlaveHelmetRemover")
											{
												flag18 = true;
												__instance.AddButton("RemoveHelmetSlaveHelmetRemover");
												break;
											}
									}

									if (flag18)
										goto IL_48BF;
									
									using (List<InvItem>.Enumerator enumerator = interactingAgent.inventory.InvItemList.GetEnumerator())
									{
										while (enumerator.MoveNext())
											if (enumerator.Current.invItemName == "Wrench")
											{
												__instance.AddButton("RemoveHelmetWrench", " (" + interactingAgent.inventory.FindItem("Wrench").invItemCount + ") -30");
												flag18 = true;
												break;
											}
									}
									
									using (List<InvItem>.Enumerator enumerator = interactingAgent.inventory.InvItemList.GetEnumerator())
									{
										while (enumerator.MoveNext())
											if (enumerator.Current.invItemName == "Saw")
											{
												__instance.AddButton("RemoveHelmetSaw", " (" + interactingAgent.inventory.FindItem("Saw").invItemCount + ") -30");
												flag18 = true;
												break;
											}

										goto IL_48BF;
									}
								}

								agent.SayDialogue("InteractB");
								GC.audioHandler.Play(agent, "AgentTalk");
								goto IL_48BF;
							}

							agent.SayDialogue("Interact");
							GC.audioHandler.Play(agent, "AgentTalk");

							if (!___buttons.Contains("YoureFree"))
								__instance.AddButton("YoureFree");
							
							goto IL_48BF;
						}
						else if (agentName == "Slavemaster")
						{
							if (interactingAgent.slaveOwners.Contains(agent))
							{
								agent.SayDialogue("InteractB");
								GC.audioHandler.Play(agent, "AgentTalk");
								__instance.AddButton("SlaveBuyFreedom", agent.determineMoneyCost("SlavePurchase"));
								goto IL_48BF;
							}

							agent.SayDialogue("Interact");
							GC.audioHandler.Play(agent, "AgentTalk");
							
							if (agent.relationships.GetRel(interactingAgent) == "Aligned" || agent.relationships.GetRel(interactingAgent) == "Submissive")
							{
								__instance.AddButton("GiveMeSlave");
								goto IL_48BF;
							}
							
							bool questSlavePurchase = false;
							
							for (int n = 0; n < GC.agentList.Count; n++)
							{
								Agent agent4 = GC.agentList[n];
							
								if (agent4.agentName == "Slave" && agent4.slaveOwners.Contains(agent) && (agent4.rescueForQuest != null || (!agent4.gc.serverPlayer && agent4.oma.rescuingForQuest)))
								{
									questSlavePurchase = true;
									break;
								}
							}

							if (questSlavePurchase)
								__instance.AddButton("PurchaseSlave", agent.determineMoneyCost("QuestSlavePurchase"));
							else 
								__instance.AddButton("PurchaseSlave", agent.determineMoneyCost("SlavePurchase"));

							goto IL_48BF;
						}
						else if (agentName == "Soldier")
						{
							agent.SayDialogue("Interact");
							GC.audioHandler.Play(agent, "AgentTalk");

							if (agent.startingChunkRealDescription == "Mall" && agent.ownerID != 0)
							{
								if (agent.hasSpecialInvDatabase)
								{
									__instance.AddButton("Buy");
							
									if (interactingAgent.inventory.HasItem("FreeItemVoucher"))
										__instance.AddButton("UseVoucher");
								}

								if (interactingAgent.statusEffects.hasTrait("CanBorrowMoney"))
									__instance.AddButton("BorrowMoney");
							}
							else
							{
								if (agent.employer == null && !agent.warZoneAgent && agent.relationships.GetRelCode(interactingAgent) != relStatus.Annoyed)
								{
									if (interactingAgent.inventory.HasItem("HiringVoucher"))
										__instance.AddButton("HireAsProtection", 6666);
									
									__instance.AddButton("HireAsProtection", agent.determineMoneyCost("SoldierHire"));
								}
							}

							goto IL_48BF;
						}
						else if (agentName == "Scientist")
						{
							agent.SayDialogue("Interact");
							GC.audioHandler.Play(agent, "AgentTalk");
							__instance.AddButton("Identify", agent.determineMoneyCost("IdentifySyringe"));
							goto IL_48BF;
						}
						else if (agentName == "Thief")
						{
							if (interactingAgent.statusEffects.hasTrait("HonorAmongThieves") || interactingAgent.statusEffects.hasTrait("HonorAmongThieves2"))
							{
								agent.SayDialogue("InteractB");
								GC.audioHandler.Play(agent, "AgentTalk");

								if (agent.hasSpecialInvDatabase)
								{
									__instance.AddButton("Buy");
								
									if (interactingAgent.inventory.HasItem("FreeItemVoucher"))
										__instance.AddButton("UseVoucher");
								}
							}
							else
							{
								agent.SayDialogue("Interact");
								GC.audioHandler.Play(agent, "AgentTalk");
								agent.movement.RotateToObject(interactingAgent.go);
								// agent.employer == null; [sic]
							}

							if (agent.employer == null)
							{
								if (agent.relationships.GetRelCode(interactingAgent) != relStatus.Annoyed)
								{
									if (interactingAgent.inventory.HasItem("HiringVoucher"))
										__instance.AddButton("AssistMe", 6666);
							
									__instance.AddButton("AssistMe", agent.determineMoneyCost("ThiefAssist"));
								}
							}
							else if (!agent.oma.cantDoMoreTasks)
								__instance.AddButton("LockpickDoor");

							goto IL_48BF;
						}
						else if (agentName == "UpperCruster")
						{
							GC.audioHandler.Play(agent, "AgentTalk");

							if (!(agent.startingChunkRealDescription == "Mall") || agent.ownerID == 0)
							{
								agent.SayDialogue("Interact");
								goto IL_48BF;
							}

							if (agent.hasSpecialInvDatabase)
							{
								__instance.AddButton("Buy");

								if (interactingAgent.inventory.HasItem("FreeItemVoucher"))
									__instance.AddButton("UseVoucher");
							}

							if (interactingAgent.statusEffects.hasTrait("CanBorrowMoney"))
								__instance.AddButton("BorrowMoney");
							
							goto IL_48BF;
						}
						else if (agentName == "Worker")
						{
							agent.SayDialogue("Interact");
							agent.gc.audioHandler.Play(agent, "AgentTalk");
							goto IL_48BF;
						}
						else if (agentName == "Zombie")
						{
							if (!(interactingAgent.agentName == "Zombie") && !interactingAgent.inventory.HasItem("Translator") && !agent.inventory.HasItem("Translator"))
							{
								agent.SayDialogue("Interact");
								GC.audioHandler.Play(agent, "AgentTalk");
								agent.movement.RotateToObject(interactingAgent.go);
								goto IL_48BF;
							}

							agent.SayDialogue("InteractB");
							GC.audioHandler.Play(agent, "AgentTalk");
							
							if (!agent.hasSpecialInvDatabase)
								goto IL_48BF;
							
							__instance.AddButton("Buy");
							
							if (interactingAgent.inventory.HasItem("FreeItemVoucher"))
								__instance.AddButton("UseVoucher");
							
							goto IL_48BF;
						}
						else // put agentName == "Custom" or whatever in here when you know what it is
						{
							logger.LogDebug("=" + agentName + "=");

							if (TraitManager.HasTraitFromList(agent, TraitManager.InteractionTraitsGroup))
							{
								// Interaction 
								if (TraitManager.HasTraitFromList(agent, TraitManager.InteractionTraits))
								{
									Core.LogCheckpoint("Interaction");

									if (agent.HasTrait<Interaction_BuyerAll>())
										__instance.AddButton("SellAny"); // TODO

									if (agent.HasTrait<Interaction_Extortable>() && agent.CanShakeDown())
									{
										int threat = agent.relationships.FindThreat(interactingAgent, false);

										if ((agent.health <= agent.healthMax * 0.4f) ||
											(agent.relationships.GetRel(interactingAgent) == "Aligned") ||
											(agent.slaveOwners.Contains(interactingAgent)))
											threat = 100;
										
										__instance.AddButton("Shakedown", " (" + threat + "%)");
									}

									if (agent.HasTrait<Interaction_Moochable>() && interactingAgent.statusEffects.hasTrait(VanillaTraits.Moocher))
										__instance.AddButton("BorrowMoney");
								}

								// Vendor 

								logger.LogDebug("hasSpecialInvDatabase: " + agent.hasSpecialInvDatabase);

								if (TraitManager.HasTraitFromList(agent, TraitManager.VendorTypeTraits) && agent.hasSpecialInvDatabase)
								{
									Core.LogCheckpoint("Vendor");

									Type vendorTrait = TraitManager.GetOnlyTraitFromList(agent, TraitManager.VendorTypeTraits);
									logger.LogDebug("\tCount: " + agent.specialInvDatabase.InvItemList.Count);

									bool canBuy =
										!(agent.HasTrait<TraitTrigger_CoolCannibal>() && !interactingAgent.statusEffects.hasTrait("CannibalsNeutral")) &&
										!(agent.HasTrait<TraitTrigger_CopAccess>() && !interactingAgent.HasTrait("TheLaw")) &&
										!(agent.HasTrait<TraitTrigger_HonorableThief>() && !(interactingAgent.statusEffects.hasTrait("HonorAmongThieves") || interactingAgent.statusEffects.hasTrait("HonorAmongThieves2")));

									if (canBuy)
									{
										__instance.AddButton("Buy");

										if (interactingAgent.inventory.HasItem("FreeItemVoucher"))
											__instance.AddButton("UseVoucher");
									}
								}

								// Hire 
								if (TraitManager.HasTraitFromList(agent, TraitManager.HireTypeTraits))
								{
									Core.LogCheckpoint("Hire");

									if (agent.employer == null && agent.relationships.GetRelCode(interactingAgent) != relStatus.Annoyed)
									{
										Core.LogCheckpoint("Hire Initial");

										bool bananaCost = agent.HasTrait<Hire_CostBanana>();

										if (agent.HasTrait<Hire_Bodyguard>())
										{
											if (interactingAgent.inventory.HasItem("HiringVoucher"))
												__instance.AddButton("HireAsProtection", 6666);
											else
												__instance.AddButton("HireAsProtection", bananaCost ? 6789 : (int)(agent.determineMoneyCost("SoldierHire")));
										}
										else if (
											agent.HasTrait<Hire_BreakIn>() ||
											agent.HasTrait<Hire_CauseRuckus>() ||
											agent.HasTrait<Hire_Hack>() ||
											agent.HasTrait<Hire_Safecrack>() ||
											agent.HasTrait<Hire_Tamper>())
										{
											if (interactingAgent.inventory.HasItem("HiringVoucher"))
												__instance.AddButton("AssistMe", 6666);
											else
												__instance.AddButton("AssistMe", bananaCost ? 6789 : (int)(agent.determineMoneyCost("ThiefAssist")));
										}
									}
									else if (!agent.oma.cantDoMoreTasks)
									{
										Core.LogCheckpoint("Hire Order");

										if (agent.HasTrait<Hire_BreakIn>())
											__instance.AddButton("LockpickDoor");
										if (agent.HasTrait<Hire_CauseRuckus>())
											__instance.AddButton("CauseRuckus");
										if (agent.HasTrait<Hire_Hack>())
											__instance.AddButton("HackSomething");
										if (agent.HasTrait<Hire_Safecrack>())
											__instance.AddButton(CJob.SafecrackSafe);
										if (agent.HasTrait<Hire_Tamper>())
											__instance.AddButton(CJob.TamperSomething);
									}
								}
							}
						}

						agent.SayDialogue("Interact");
						GC.audioHandler.Play(agent, "AgentTalk");
						agent.movement.RotateToObject(interactingAgent.go);
					}
				}

			IL_48BF:
				if (quest != null && quest.questStatus != "Failed" && quest.questStatus != "NotAccepted" && quest.questStatus != "Done" && !agent.zombified)
				{
					if ((agent.relationships.GetRel(interactingAgent) == "Friendly" || 
						agent.relationships.GetRel(interactingAgent) == "Aligned" || 
						agent.relationships.GetRel(interactingAgent) == "Loyal" || 
						agent.relationships.GetRel(interactingAgent) == "Submissive") && !agent.oma.didAsk)
						__instance.AddButton("AskLeaveTown", " (" + agent.relationships.FindAskPercentage(interactingAgent, false) + "%)");
					else
						__instance.AddButton("ThreatenLeaveTown", " (" + agent.relationships.FindThreat(interactingAgent, false) + "%)");
				}

				if (CanCommandAttack && !___buttons.Contains("Attack"))
				{
					if (agent.FindNumFollowingCanAttack(interactingAgent) > 1)
						__instance.AddButton("AllAttack");
					
					__instance.AddButton("Attack");
				}

				if (canCommandStandGuard)
				{
					if (agent.FindNumFollowingCanFollow(interactingAgent) > 1 && !___buttons.Contains("AllFollow"))
						__instance.AddButton("AllFollow");
				
					if (agent.job == "GoHere" && !___buttons.Contains("FollowMe"))
						__instance.AddButton("FollowMe");
					
					if (agent.FindNumFollowingCanStandGuard(interactingAgent) > 1 && !___buttons.Contains("AllStandGuard"))
						__instance.AddButton("AllStandGuard");
					
					if (!___buttons.Contains("StandGuard"))
						__instance.AddButton("StandGuard");
				}

				if (agent.isPlayer == 0 && !agent.butlerBot)
				{
					if (interactingAgent.statusEffects.hasTrait("ArtOfTheDeal"))
					{
						// WTAF, Matt
						//for (int i = 0; i < agent.inventory.InvItemList.Count; i++)
						//{
						//	InvItem invItem2 = agent.inventory.InvItemList[i];

						//	if (!invItem2.questItem && invItem2.invItemName != "Money")
						//		int itemValue = invItem2.itemValue;
						//}
					
						if (!interactingAgent.ghost)
							__instance.AddButton("BuyItem");
					}

					if (interactingAgent.statusEffects.hasTrait("ServeDrinks") && !agent.copBot && !agent.electronic && !agent.ghost && agent.agentName != "Bartender" && !agent.oma.offeredDrink && !agent.statusEffects.hasTrait("BloodRestoresHealth") && !agent.statusEffects.hasTrait("OilRestoresHealth") && 
						(interactingAgent.inventory.HasItem("Cocktail") || interactingAgent.inventory.HasItem("Beer") || interactingAgent.inventory.HasItem("Whiskey")))
						__instance.AddButton("OfferDrink");
				}

				if (canBeHealed && !___buttons.Contains("AdministerTreatment"))
					__instance.AddButton("AdministerTreatment");
				
				if (isAliveNotInteractWFormerOwner && !___buttons.Contains("GiveItem"))
					__instance.AddButton("GiveItem");
				
				if (flag10 && !___buttons.Contains("YouCanGo"))
					__instance.AddButton("YouCanGo");
				
				if (flag9 && !___buttons.Contains("YoureFree"))
					__instance.AddButton("YoureFree");
				
				if (flag12 && !___buttons.Contains("TransfuseBlood"))
				{
					interactingAgent.objectMult.BodyguardedDialogue(agent);
					GC.audioHandler.Play(agent, "AgentTalk");

					if (interactingAgent.statusEffects.hasTrait("MusicianTakesLessHealth") || (interactingAgent.oma.superSpecialAbility && interactingAgent.agentName == "Bouncer"))
						__instance.AddButton("TransfuseBlood", " - 5HP");
					else
						__instance.AddButton("TransfuseBlood", " - 10HP");
				}

				if (interactingAgent.bigQuest == "Wrestler" && !___buttons.Contains("ChallengeToFight") && agent.isPlayer == 0 && ((agent.agentName == interactingAgent.oma.bigQuestTarget1 && interactingAgent.oma.bigQuestObjectCount == 0) || interactingAgent.oma.superSpecialAbility || interactingAgent.statusEffects.hasTrait("ChallengeAnyoneToFight")))
					__instance.AddButton("ChallengeToFight");
				
				if ((interactingAgent.statusEffects.hasTrait("Shakedowner") || interactingAgent.statusEffects.hasTrait("Shakedowner2")) && agent.CanShakeDown())
				{
					int num11 = agent.relationships.FindThreat(interactingAgent, false);
				
					if (agent.health <= agent.healthMax * 0.4f)
						num11 = 100;
					else if (agent.relationships.GetRel(interactingAgent) == "Aligned")
						num11 = 100;
					else if (agent.slaveOwners.Contains(interactingAgent))
						num11 = 100;
					
					__instance.AddButton("Shakedown", " (" + num11 + "%)");
				}

				if (agent.relationships.GetRel(interactingAgent) != "Aligned" && agent.relationships.GetRel(interactingAgent) != "Loyal" && agent.relationships.GetRel(interactingAgent) != "Submissive" && agent.isPlayer == 0 && agent.startingChunkRealDescription != "Generic" && (interactingAgent.statusEffects.hasTrait("Mugger") || interactingAgent.statusEffects.hasTrait("Mugger2")) && agent.isPlayer == 0)
				{
					if (hasKey && agent.startingChunkRealDescription != "Hotel" && hasSafeCombo)
						__instance.AddButton("ThreatenKeyAndSafeCombination", " (" + agent.relationships.FindThreat(interactingAgent, false) + "%)");
					else if (hasKey && agent.startingChunkRealDescription != "Hotel")
						__instance.AddButton("ThreatenKey", " (" + agent.relationships.FindThreat(interactingAgent, false) + "%)");
					else if (hasSafeCombo)
						__instance.AddButton("ThreatenSafeCombination", " (" + agent.relationships.FindThreat(interactingAgent, false) + "%)");
				}
				
				if ((interactingAgent.statusEffects.hasTrait("Mugger") || interactingAgent.statusEffects.hasTrait("Mugger2")) && !agent.enforcer && agent.isPlayer == 0 && !agent.inhuman && !agent.ghost && agent.agentName != "Mafia")
				{
					bool flag25 = false;
					int num12 = 0;
				
					for (int num13 = 0; num13 < agent.inventory.InvItemList.Count; num13++)
					{
						InvItem invItem3 = agent.inventory.InvItemList[num13];
					
						if (invItem3.invItemName == "Money")
						{
							flag25 = true;
							num12 = invItem3.invItemCount;
						}
					}

					if (flag25)
					{
						int num14 = agent.relationships.FindThreat(interactingAgent, false);
						
						if (agent.relationships.GetRel(interactingAgent) == "Aligned" || agent.relationships.GetRel(interactingAgent) == "Submissive")
							num14 = 100;

						__instance.AddButton("ThreatenMoney", string.Concat(new object[] { " $", num12, " (", num14, "%)" }));
					}
				}

				if (GC.streamingWorld)
					__instance.AddButton("CreateQuestStreaming");
			}

			return false;
		}

		[HarmonyPrefix, HarmonyPatch(methodName:nameof(AgentInteractions.PressedButton), argumentTypes:new[] { typeof(Agent), typeof(Agent), typeof(string), typeof(int) })]
		public static bool PressedButton_Prefix(Agent agent, Agent interactingAgent, string buttonText, int buttonPrice, AgentInteractions __instance)
		{
			Core.LogMethodCall();
			logger.LogDebug("\tbuttonText: " + buttonText);

			__instance.interactor = null;
			__instance.allAttack = false;
			__instance.allGoHere = false;

			if (buttonText == CJob.SafecrackSafe)
			{
				__instance.interactor = interactingAgent;
				agent.commander = interactingAgent;
				interactingAgent.mainGUI.invInterface.ShowTarget(agent, CJob.SafecrackSafe);

				return false;
			}
			else if (buttonText == CJob.TamperSomething)
			{
				__instance.interactor = interactingAgent;
				agent.commander = interactingAgent;
				interactingAgent.mainGUI.invInterface.ShowTarget(agent, CJob.TamperSomething);

				return false;
			}

			return true;
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
