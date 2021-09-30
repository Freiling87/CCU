using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using UnityEngine;
using RogueLibsCore;
using CCU.Traits.AI;
using CCU.Traits.AI.Behavior;
using CCU.Traits.AI.TraitTrigger;

namespace CCU.Patches
{
    [HarmonyPatch(declaringType: typeof(BrainUpdate))]
    public class P_BrainUpdate
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

        [HarmonyPrefix, HarmonyPatch(methodName: nameof(BrainUpdate.MyUpdate), argumentTypes: new Type[0] { })]
        public static bool MyUpdate_Prefix(BrainUpdate __instance, Agent ___agent, Brain ___brain)
        {
			/* Contains:
			 * - Pickpocket
			 */

			if (__instance.slowAIWait > 0 && !___agent.dead)
			{
				__instance.slowAIWait--;

				if (___brain.Goals.Count > 0)
					___brain.Goals[0].Process();
			}
			else
			{
				int num = 4;
			
				if (!___agent.agentActive)
					num = 10;
				
				if (___agent.onCamera || ___agent.needsCurAngle || (__instance.gc.multiplayerMode && ___brain.active))
					___agent.curAngle = ___agent.tr.eulerAngles.z;
				
				if (___brain.active)
					___agent.curRightAngle = __instance.tr.right;
				
				try
				{
					___agent.curPosition = ___agent.tr.position;
					___agent.curPosX = ___agent.curPosition.x;
					___agent.curPosY = ___agent.curPosition.y;
				}
				catch
				{
					Debug.Log("ERROR GETTING POSITION");
					___agent.curPosition = ___agent.transform.position;
					___agent.curPosX = ___agent.curPosition.x;
					___agent.curPosY = ___agent.curPosition.y;
				}
				
				if (__instance.gc.tileInfo.wallsDirtied || !___agent.previousActiveState || ___agent.haterated || ___agent.dead)
					___agent.notMovedSinceLastAIUpdate = false;
				else if (___agent.previousPosition == ___agent.curPosition)
				{
					if (___agent.previousAngle == ___agent.curAngle)
					{
						if (___agent.previousPreviousAngle == ___agent.curAngle)
							___agent.notMovedSinceLastAIUpdate = true;
						else
							___agent.notMovedSinceLastAIUpdate = false;
					}
					else
						___agent.notMovedSinceLastAIUpdate = false;
				}
				else
					___agent.notMovedSinceLastAIUpdate = false;
				
				if (__instance.gc.multiplayerMode && __instance.gc.serverPlayer)
				{
					bool flag = true;
					List<Agent> playerAgentList = __instance.gc.playerAgentList;

					if (playerAgentList.Count > 1)
					{
						for (int i = 0; i < playerAgentList.Count; i++)
						{
							Agent agent = playerAgentList[i];
					
							if (agent.agentID != __instance.gc.playerAgent.agentID && Vector2.Distance(agent.objectMultAgent.netAgent.curPosition, ___agent.curPosition) <= 16f)
							{
								flag = false;
								break;
							}
						}
					}

					if (flag)
					{
						float num2 = 1f;
					
						if (___agent.networkTransform.sendInterval != num2)
							___agent.networkTransform.sendInterval = num2;
					}
					else
					{
						float num3 = 0.125f;
					
						if (___agent.networkTransform.sendInterval != num3)
							___agent.networkTransform.sendInterval = num3;
					}
				}

				if (__instance.gc.multiplayerMode && ___agent != __instance.gc.playerAgent && __instance.gc.playerAgent != null && ___agent.networkTransform != null)
				{
					if (__instance.gc.serverPlayer && ___agent.isPlayer != 0 && !___agent.localPlayer)
						___agent.networkTransform.interpolateMovement = Mathf.Clamp(1f - (float)___agent.clientPing / 1000f * 2f, 0.2f, 1f);
					else if (!__instance.gc.serverPlayer)
						___agent.networkTransform.interpolateMovement = Mathf.Clamp(1f - (float)__instance.gc.playerAgent.clientPing / 1000f * 2f, 0.2f, 1f);
				}
				
				if (!___agent.overHole && !___agent.overWall && !___agent.overDanger)
				{
					___agent.mostRecentLandPos2 = ___agent.mostRecentLandPos;
					___agent.mostRecentLandPos = ___agent.curPosition;
				}
				
				if (!___agent.overHole && !___agent.overWall)
				{
					___agent.mostRecentLandPosLight2 = ___agent.mostRecentLandPosLight;
					___agent.mostRecentLandPosLight = ___agent.curPosition;
				}
				
				if (___agent.isPlayer > 0 && !___agent.outOfControl)
				{
					___agent.SetBrainActive(true);
				
					if (!___agent.curTileData.prisonOpened)
						___agent.prisoner = ___agent.curTileData.prison;
					else
						___agent.prisoner = 0;
				}
				else if (!__instance.gc.serverPlayer && !__instance.gc.clientControlling && !___agent.clientOutOfControl)
				{
					___agent.SetBrainActive(true);
				
					if (Vector2.Distance(__instance.gc.cameraScript.playerAgent.curPosition, ___agent.curPosition) > 16f)
					{
						if (!___agent.killerRobot && !___agent.outOfControl && !__instance.chunkStatic && __instance.collidersEnabled && ___agent.rb.velocity.magnitude <= 17f && !___agent.frozen)
							__instance.TurnOffCollision();
					}
					else if (!__instance.collidersEnabled)
						__instance.TurnOnCollision();
				}
				else if (___agent.objectAgent || ___agent.hologram || ___agent.mechEmpty)
					___agent.SetBrainActive(true);
				else
				{
					bool flag2 = false;

					if (__instance.gc.multiplayerMode)
					{
						flag2 = true;
						List<Agent> playerAgentList2 = __instance.gc.playerAgentList;
					
						for (int j = 0; j < playerAgentList2.Count; j++)
						{
							Agent agent2 = playerAgentList2[j];
						
							if (___agent.agentActive)
							{
								flag2 = false;
								break;
							}
							
							if (Vector2.Distance(___agent.curPosition, agent2.curPosition) <= 16f)
							{
								flag2 = false;
								break;
							}
							
							if (!__instance.gc.testMe && __instance.ContainsNearbyChunksAgents(agent2))
							{
								flag2 = false;
								break;
							}
						}

						if (!flag2)
						{
							bool flag3 = false;
						
							for (int k = 0; k < playerAgentList2.Count; k++)
							{
								Agent myPlayer = playerAgentList2[k];
							
								if (__instance.InFastAIBounds(myPlayer))
								{
									flag3 = true;
									break;
								}
							}

							if (!flag3)
								__instance.slowAIWait = num;
						}
					}
					else if (!__instance.gc.coopMode && !__instance.gc.fourPlayerMode)
					{
						if (___agent.agentActive)
							flag2 = false;
						else if (Vector2.Distance(___agent.curPosition, __instance.gc.playerAgent.curPosition) > 16f)
						{
							if (__instance.gc.testMe)
								flag2 = true;
							else if (!__instance.ContainsNearbyChunksAgents(__instance.gc.playerAgent))
								flag2 = true;
						}
					
						if (!flag2 && !__instance.InFastAIBounds(__instance.gc.playerAgent))
							__instance.slowAIWait = num;
					}
					else if (__instance.gc.coopMode && __instance.gc.loadComplete)
					{
						if (___agent.agentActive)
							flag2 = false;
						else
						{
							flag2 = true;
						
							if (flag2)
								flag2 = (Vector2.Distance(___agent.curPosition, __instance.gc.playerAgent.curPosition) > 16f && (__instance.gc.testMe || !__instance.ContainsNearbyChunksAgents(__instance.gc.playerAgent)));
						
							if (flag2)
								flag2 = (Vector2.Distance(___agent.curPosition, __instance.gc.playerAgent2.curPosition) > 16f && (__instance.gc.testMe || !__instance.ContainsNearbyChunksAgents(__instance.gc.playerAgent2)));
						}
					
						if (!flag2 && !__instance.InFastAIBounds(__instance.gc.playerAgent) && !__instance.InFastAIBounds(__instance.gc.playerAgent2))
							__instance.slowAIWait = num;
					}
					else if (__instance.gc.fourPlayerMode && __instance.gc.loadComplete)
					{
						if (___agent.agentActive)
							flag2 = false;
						else
						{
							flag2 = true;
						
							if (flag2)
								flag2 = (Vector2.Distance(___agent.curPosition, __instance.gc.playerAgent.curPosition) > 16f && (__instance.gc.testMe || !__instance.ContainsNearbyChunksAgents(__instance.gc.playerAgent)));
							
							if (flag2)
								flag2 = (Vector2.Distance(___agent.curPosition, __instance.gc.playerAgent2.curPosition) > 16f && (__instance.gc.testMe || !__instance.ContainsNearbyChunksAgents(__instance.gc.playerAgent2)));
						
							if (flag2)
								flag2 = (Vector2.Distance(___agent.curPosition, __instance.gc.playerAgent3.curPosition) > 16f && (__instance.gc.testMe || !__instance.ContainsNearbyChunksAgents(__instance.gc.playerAgent3)));
							
							if (flag2 && !__instance.gc.sessionDataBig.threePlayer)
								flag2 = (Vector2.Distance(___agent.curPosition, __instance.gc.playerAgent4.curPosition) > 16f && (__instance.gc.testMe || !__instance.ContainsNearbyChunksAgents(__instance.gc.playerAgent4)));
							
							if (!flag2 && !__instance.InFastAIBounds(__instance.gc.playerAgent) && !__instance.InFastAIBounds(__instance.gc.playerAgent2) && !__instance.InFastAIBounds(__instance.gc.playerAgent3) && !__instance.gc.sessionDataBig.threePlayer && !__instance.InFastAIBounds(__instance.gc.playerAgent4))
								__instance.slowAIWait = num;
						}
					}

					if (flag2 && ___agent.gang != 0 && __instance.gc.loadCompleteReally)
					{
						for (int l = 0; l < ___agent.gangMembers.Count; l++)
						{
							Agent agent3 = ___agent.gangMembers[l];
					
							if (agent3.agentID != ___agent.agentID && agent3.brain.active && agent3.gang == ___agent.gang)
							{
								flag2 = false;
								break;
							}
						}
					}

					if (flag2 && ___agent.defaultGoalCode == goalType.Investigate)
						flag2 = false;
					
					if (__instance.activeNextUpdate)
					{
						__instance.activeNextUpdate = false;
						flag2 = false;
					}
					
					if (__instance.gc.streamingWorld && __instance.gc.streamingWorldController.quickLoadChunks)
						flag2 = true;
					
					if (___agent.fakeNotActiveStreaming)
						flag2 = true;
					
					if (flag2 && !___agent.killerRobot && !___agent.outOfControl && !__instance.chunkStatic && ___agent.pathing == 0 && !___agent.mechEmpty)
					{
						___agent.SetBrainActive(false);
						__instance.slowAIWait = 0;
					
						if (__instance.gc.serverPlayer && __instance.collidersEnabled && ___agent.rb.velocity.magnitude <= 17f && !___agent.frozen)
							__instance.TurnOffCollision();
					}
					else
					{
						if (!__instance.collidersEnabled)
							__instance.TurnOnCollision();
						
						if (!___agent.dead && !___agent.frozen && !___agent.hasGettingBitByAgent && !___agent.chargingForward && !___agent.oma.hidden && !___agent.oma.mindControlled)
						{
							___agent.SetBrainActive(true);
							___agent.canWarn = null;
							___agent.canWarnBestDist = 100000f;
							___agent.curChunk = ___agent.curTileData.chunkID;
							___agent.curOwnerTile = ___agent.curTileData.owner;
						
							if (__instance.slowAIWait > 0 && !___agent.notMovedSinceLastAIUpdate)
								___agent.curTileData = __instance.gc.tileInfo.GetTileDataAndNearWater(___agent, ___agent.curPosition);
							
							if (___brain.Goals.Count == 0)
							{
								Goal item = new Goal();
								___brain.Goals.Add(item);
							}
							
							if (!__instance.gc.cinematic)
							{
								try
								{
									List<Agent> lastSawAgentList = __instance.gc.lastSawAgentList;

									for (int m = 0; m < lastSawAgentList.Count; m++)
									{
										Agent agent4 = lastSawAgentList[m];

										if (agent4.agentID != ___agent.agentID)
											___agent.relationships.LastSaw(agent4);
									}
								}
								catch { }
							}

							if (___agent.losCheckAtIntervals && ___agent.isPlayer == 0)
							{
								___agent.losCheckAtIntervalsTime++;

								// losCheckAtIntervals currently makes these traits incompatible with each other.
								// TODO: Refactor this so that GrabMoney & GrabDrugs can function alongside the SA types.

								if (___agent.losCheckAtIntervalsTime > 8)
								{
									if (___agent.agentName == "Hobo" || ___agent.HasTrait<Behavior_GrabMoney>()) // GrabMoney
									{
										___agent.losCheckAtIntervalsTime = 0;
								
										if (!___agent.hasEmployer)
										{
											List<Item> moneyList = __instance.gc.moneyList;

											for (int n = 0; n < moneyList.Count; n++)
											{
												Item item2 = moneyList[n];

												if (!item2.fellInHole && item2.curTileData.prison == ___agent.curTileData.prison && !item2.dontStealFromGround && 
													(___agent.curTileData.prison <= 0 || ___agent.curTileData.chunkID == item2.curTileData.chunkID) && 
													!__instance.gc.tileInfo.DifferentLockdownZones(___agent.curTileData, item2.curTileData) && ___agent.curPosX - 5f < item2.curPosition.x && ___agent.curPosX + 5f > item2.curPosition.x && ___agent.curPosY - 5f < item2.curPosition.y && ___agent.curPosY + 5f > item2.curPosition.y && ___agent.movement.HasLOSObjectNormal(item2))
												{
													___agent.SetPreviousDefaultGoal(___agent.defaultGoal);
													___agent.SetDefaultGoal("GoGet");
													___agent.SetGoGettingTarget(item2);
													___agent.stoleStuff = true;
													break;
												}
											}
										}
									}
									else if (___agent.HasTrait<Behavior_GrabDrugs>()) // Grab Drugs
									{
										___agent.losCheckAtIntervalsTime = 0;

										if (!___agent.hasEmployer)
										{
											List<Item> itemList = __instance.gc.itemList;

											for (int n = 0; n < itemList.Count; n++)
											{
												Item item2 = itemList[n];

												if (item2.invItem.Categories.Contains("Drugs") &&
													!item2.fellInHole && item2.curTileData.prison == ___agent.curTileData.prison && !item2.dontStealFromGround &&
													(___agent.curTileData.prison <= 0 || ___agent.curTileData.chunkID == item2.curTileData.chunkID) &&
													!__instance.gc.tileInfo.DifferentLockdownZones(___agent.curTileData, item2.curTileData) && ___agent.curPosX - 5f < item2.curPosition.x && ___agent.curPosX + 5f > item2.curPosition.x && ___agent.curPosY - 5f < item2.curPosition.y && ___agent.curPosY + 5f > item2.curPosition.y && ___agent.movement.HasLOSObjectNormal(item2))
												{
													___agent.SetPreviousDefaultGoal(___agent.defaultGoal);
													___agent.SetDefaultGoal("GoGet");
													___agent.SetGoGettingTarget(item2);
													___agent.stoleStuff = true;
													break;
												}
											}
										}
									}
									else if (___agent.agentName == "Cannibal" || ___agent.specialAbility == vSpecialAbility.Cannibalize) // Cannibalize
									{
										___agent.losCheckAtIntervalsTime = 0;

										if (!___agent.hasEmployer || ___agent.health <= 15f)
										{
											List<Agent> deadAgentList = __instance.gc.deadAgentList;

											for (int num4 = 0; num4 < deadAgentList.Count; num4++)
											{
												Agent target = deadAgentList[num4];

												try
												{
													if (target.dead && !target.resurrect && !target.ghost && !target.disappeared && !target.inhuman && !target.cantCannibalize && !target.hasGettingBitByAgent && ___agent.prisoner == target.prisoner && ___agent.slaveOwners.Count == 0 && 
														(___agent.prisoner <= 0 || ___agent.curTileData.chunkID == target.curTileData.chunkID) && 
														(!(target.agentName == "Cannibal") || !target.KnockedOut()) && 
														!target.arrested && !target.invisible && !__instance.gc.tileInfo.DifferentLockdownZones(___agent.curTileData, target.curTileData) && ___agent.curPosX - 5f < target.curPosX && ___agent.curPosX + 5f > target.curPosX && ___agent.curPosY - 5f < target.curPosY && ___agent.curPosY + 5f > target.curPosY && ___agent.movement.HasLOSAgent(target) && target.fire == null)
													{
														___agent.SetPreviousDefaultGoal(___agent.defaultGoal);
														___agent.SetDefaultGoal("Cannibalize");
														___agent.SetCannibalizingTarget(target);
														___agent.losCheckAtIntervals = false;
														break;
													}
												}
												catch
												{
													Debug.LogError(string.Concat(new object[] { "Cannibalize Error: ", ___agent, " - ", target }));
												}
											}
										}
									}
								}

								if (___agent.losCheckAtIntervalsTime > 9)
								{
									if ((___agent.agentName == "Thief" || ___agent.specialAbility == vSpecialAbility.StickyGlove) && !__instance.thiefNoSteal) // Pickpocket
									{
										logger.LogDebug("Pickpocket check Triggered");

										___agent.losCheckAtIntervalsTime = 0;
										if (!___agent.hasEmployer)
										{
											List<Agent> lastSawAgentList2 = __instance.gc.lastSawAgentList;
											for (int num5 = 0; num5 < ___agent.losCheckAtIntervalsList.Count; num5++)
											{
												Agent agent6 = ___agent.losCheckAtIntervalsList[num5];
												Relationship relationship = ___agent.relationships.GetRelationship(agent6);
												bool honorFlag = 
													((___agent.agentName == "Thief" || (___agent.specialAbility == vSpecialAbility.StickyGlove && ___agent.HasTrait<TraitTrigger_HonorableThief>())) && 
													(agent6.statusEffects.hasTrait("HonorAmongThieves") || agent6.statusEffects.hasTrait("HonorAmongThieves2")));

												logger.LogDebug("HonorFlag: " + honorFlag);

												if (relationship.distance < 4f && !honorFlag && !agent6.mechEmpty && !agent6.objectAgent && relationship.relTypeCode != relStatus.Aligned && relationship.relTypeCode != relStatus.Loyal && relationship.relTypeCode != relStatus.Friendly && relationship.relTypeCode != relStatus.Hostile && ___agent.slaveOwners.Count == 0 && ___agent.prisoner == agent6.prisoner && !agent6.invisible && !agent6.disappeared && (___agent.prisoner <= 0 || ___agent.curTileData.chunkID == agent6.curTileData.chunkID) && !agent6.hasGettingArrestedByAgent && !___agent.hectoredAgents.Contains(agent6.agentID) && !__instance.gc.tileInfo.DifferentLockdownZones(___agent.curTileData, agent6.curTileData))
												{
													___agent.SetDefaultGoal("Steal");
													___agent.SetStealingFromAgent(agent6);
													___agent.hectoredAgents.Add(agent6.agentID);
													___agent.losCheckAtIntervals = false;
													___agent.noEnforcerAlert = true;
													___agent.oma.mustBeGuilty = true;
													break;
												}
											}
										}
									}
									else if (___agent.agentName == "Vampire" || ___agent.specialAbility == vSpecialAbility.Bite) // Bite
									{
										___agent.losCheckAtIntervalsTime = 0;

										if (!___agent.hasEmployer || ___agent.health <= 15f)
										{
											List<Agent> lastSawAgentList3 = __instance.gc.lastSawAgentList;

											for (int num6 = 0; num6 < ___agent.losCheckAtIntervalsList.Count; num6++)
											{
												Agent agent7 = ___agent.losCheckAtIntervalsList[num6];
												Relationship relationship2 = ___agent.relationships.GetRelationship(agent7);

												if (relationship2.distance < 5f && relationship2.relTypeCode != relStatus.Aligned && relationship2.relTypeCode != relStatus.Loyal && relationship2.relTypeCode != relStatus.Friendly && relationship2.relTypeCode != relStatus.Hostile && agent7.agentName != "Vampire" && !agent7.hasGettingBitByAgent && !agent7.mechEmpty && !agent7.mechFilled && !agent7.objectAgent && !agent7.dizzy && (agent7.localPlayer || agent7.isPlayer == 0) && ___agent.prisoner == agent7.prisoner && !agent7.invisible && ___agent.slaveOwners.Count == 0 && (___agent.prisoner <= 0 || ___agent.curTileData.chunkID == agent7.curTileData.chunkID) && !agent7.hasGettingArrestedByAgent && !___agent.hectoredAgents.Contains(agent7.agentID) && !__instance.gc.tileInfo.DifferentLockdownZones(___agent.curTileData, agent7.curTileData) && !agent7.dead && !agent7.ghost && !agent7.hologram && !agent7.disappeared && !agent7.inhuman && !agent7.beast && !agent7.zombified)
												{
													___agent.SetPreviousDefaultGoal(___agent.defaultGoal);
													___agent.SetDefaultGoal("Bite");
													___agent.hectoredAgents.Add(agent7.agentID);
													___agent.SetBitingTarget(agent7);
													___agent.losCheckAtIntervals = false;
													___agent.noEnforcerAlert = true;
													___agent.oma.mustBeGuilty = true;
													___agent.oma.hasAttacked = true;
													break;
												}
											}
										}
									}

									___agent.losCheckAtIntervalsList.Clear();
								}
							}
							if (__instance.gc.loadComplete)
							{
								__instance.GoalArbitrate();
								___brain.Goals[0].Process();
							}
						}
						else if (___agent.oma.hidden)
						{
							___agent.SetBrainActive(true);

							if (___brain.Goals.Count == 0)
							{
								Goal item3 = new Goal();
								___brain.Goals.Add(item3);
							}

							try
							{
								List<Agent> lastSawAgentList4 = __instance.gc.lastSawAgentList;

								for (int num7 = 0; num7 < lastSawAgentList4.Count; num7++)
								{
									Agent agent8 = lastSawAgentList4[num7];

									if (agent8.agentID != ___agent.agentID)
										___agent.relationships.LastSaw(agent8);
								}
							}
							catch { }
						}
					}
				}

				if (!___agent.fakeNotActiveStreaming)
				{
					if (___brain.active && !__instance.gc.cinematic)
					{
						if (___agent.loud && !___agent.ghost && ___agent.rb.velocity.magnitude > 2f && __instance.gc.serverPlayer)
							__instance.gc.spawnerMain.SpawnNoise(___agent.curPosition, 0.5f, ___agent, "Normal", ___agent);
						
						if (___agent.isMayor)
						{
							List<Agent> playerAgentList3 = __instance.gc.playerAgentList;
						
							for (int num8 = 0; num8 < playerAgentList3.Count; num8++)
							{
								Agent agent9 = playerAgentList3[num8];
								agent9.distanceFromMayor = Vector2.Distance(agent9.curPosition, ___agent.curPosition);
								agent9.hasLosMayor = agent9.movement.HasLOSAgent360(___agent);
							}
						}

						if (__instance.gc.serverPlayer && ___agent.oma.hidden && ___agent.isPlayer == 0)
						{
							List<Agent> activeBrainAgentList = __instance.gc.activeBrainAgentList;
						
							for (int num9 = 0; num9 < activeBrainAgentList.Count; num9++)
							{
								Agent agent10 = activeBrainAgentList[num9];
							
								if (___agent.agentID != agent10.agentID && !agent10.dead && !agent10.ghost && !agent10.invisible && ___agent.prisoner == agent10.prisoner)
								{
									Relationship relationship3 = ___agent.relationships.GetRelationship(agent10);
								
									if (agent10.isPlayer != 0 && Vector2.Distance(agent10.curPosition, ___agent.curPosition) < 3f && 
										___agent.movement.HasLOSObject360(agent10) && 
											(agent10.bigQuest == "MechPilot" || ((!(___agent.agentName == "Thief") || !agent10.statusEffects.hasTrait("HonorAmongThieves")) && 
											(!(___agent.agentName == "Thief") || !agent10.statusEffects.hasTrait("HonorAmongThieves2")) && 
											(!(___agent.agentName == "Cannibal") || relationship3.relTypeCode == relStatus.Hostile) && 
											relationship3.relTypeCode != relStatus.Aligned && relationship3.relTypeCode != relStatus.Loyal && relationship3.relTypeCode != relStatus.Friendly)))
									{
										Manhole manhole = (Manhole)___agent.hiddenInObject;
										___agent.statusEffects.BecomeNotHidden();
									
										if (agent10.underBox)
											relationship3.sawUnderBox = true;
										
										if (___agent.agentName == "Thief") // AmbushManhole
										{
											___agent.SetDefaultGoal("Steal");
											___agent.SetStealingFromAgent(agent10);
										}
										else
											___agent.relationships.AddRelHate(agent10, 5);
										
										___agent.movement.RotateToObjectTr(agent10);
										
										if (manhole != null)
										{
											___agent.jumpDirection = agent10.tr.position - __instance.tr.position;
											___agent.jumpSpeed = 8f;
											___agent.pathing = 1;
											___agent.SetFinalDestPosition(agent10.curPosition);
											___agent.SetFinalDestObject(agent10);
											___agent.noEnforcerAlert = true;
											___agent.oma.mustBeGuilty = true;
											___agent.Jump();
											manhole.HoleAppear();
										}
									}
								}
							}
						}
					}

					if (___agent.oma.mindControlled && ___agent.mindControlAgent != null && ___agent.mindControlAgent.localPlayer && Vector2.Distance(___agent.tr.position, ___agent.mindControlAgent.tr.position) > 13.44f)
						___agent.relationships.StopMindControl();
					
					if (___agent.slaveOwners.Count > 0 && !___agent.teleporting && !___agent.justTeleportedLonger && !___agent.FellInHole() && !__instance.gc.cinematic)
					{
						float num10 = 10000f;
						List<Agent> slaveOwners = ___agent.slaveOwners;
					
						for (int num11 = 0; num11 < slaveOwners.Count; num11++)
						{
							Agent agent11 = slaveOwners[num11];
						
							if (___agent.tr != null && agent11.tr != null)
							{
								float num12 = Vector2.Distance(___agent.tr.position, agent11.tr.position);
							
								if (num12 < num10 && !agent11.teleporting && !agent11.justTeleportedLonger)
									num10 = num12;
							}
						}

						if (num10 > 17f && num10 != 10000f)
						{
							if (___agent.inventory.HasItem("SlaveHelmet"))
							{
								if (___agent.statusEffects.hasStatusEffect("Resurrection"))
								{
									for (int num13 = 0; num13 < slaveOwners.Count; num13++)
									{
										___agent.relationships.SetRel(slaveOwners[num13], "Hateful");
										___agent.relationships.SetRelHate(slaveOwners[num13], 5);
									}
									if (___agent.agentName == "Slave")
										___agent.SetDefaultGoal("WanderFar");
								}
						
								___agent.DestroySlaveHelmetRemote(___agent.inventory.FindItem("SlaveHelmet").tiedToItemCode);
								___agent.inventory.DestroyItem(___agent.inventory.FindItem("SlaveHelmet"));
							}

							if (slaveOwners.Count > 0)
							{
								___agent.lastHitByAgent = slaveOwners[0];
								___agent.justHitByAgent2 = slaveOwners[0];
								___agent.deathKiller = slaveOwners[0].agentName;
							}
							
							___agent.zombieWhenDead = false;
							
							if (__instance.gc.serverPlayer && ___agent.isPlayer != 0 && !___agent.localPlayer)
								___agent.objectMult.CallRpcChangeHealthFromServer(-200);
							else
								___agent.statusEffects.ChangeHealth(-200f);
							
							if (___agent.slaveOwners.Count > 0)
							{
								if (___agent.statusEffects.hasStatusEffect("Resurrection"))
								{
									___agent.relationships.SetRel(___agent.slaveOwners[0], "Hateful");
									___agent.relationships.SetRelHate(___agent.slaveOwners[0], 5);
									___agent.inventory.UnequipArmorHead();
								}
								else
								{
									Debug.Log(string.Concat(new object[] { "Slave Helmet Explosion 1: ", ___agent, " - ", ___agent.slaveOwners[0] }));

									if (___agent.slaveOwners[0].statusEffects.hasTrait("BiggerSlaveHelmetExplosions") || (___agent.slaveOwners[0].oma.superSpecialAbility && ___agent.slaveOwners[0].agentName == "Slavemaster"))
										__instance.gc.spawnerMain.SpawnExplosion(___agent.slaveOwners[0], ___agent.tr.position, "Big", true);
									else
										__instance.gc.spawnerMain.SpawnExplosion(___agent.slaveOwners[0], ___agent.tr.position, "Normal", true);
								}

								___agent.ClearSlaveOwners();
								__instance.gc.playerAgent.objectMult.ChangeSlaveOwners(___agent);
							}
						}
					}
				}
			}

			___agent.previousActiveState = ___agent.brain.active;
			___agent.previousPosition = ___agent.curPosition;
			___agent.previousPreviousAngle = ___agent.previousAngle;
			___agent.previousAngle = ___agent.curAngle;
			___agent.justGotUp = false;

			return false;	
		}
    }
}
