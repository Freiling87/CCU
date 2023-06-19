

//	[HarmonyPrefix, HarmonyPatch(methodName: nameof(ExitPoint.TryToExit))]
//	public static bool GateExit(ExitPoint __instance)
//	{
//		if (GC.challenges.Contains(nameof(Big_Quest_Mandatory)))
//		{
//			if (GC.playerAgentList.Any(a => a.)
//			{

//				return false;
//			}
//		}


//		return true;
//	}










using System;
using System.Collections.Generic;
using UnityEngine;

public class GoalNoiseReact : Goal
{
	// Token: 0x06000B0F RID: 2831 RVA: 0x000DAA40 File Offset: 0x000D8C40
	public override void Activate()
	{
		base.Activate();
		bool isNoiseJoke2 = false;
		bool isDoorNoise = false;
		bool isWindowNoise = false;
		bool blockedByPrisonOrLockdown = false;
		bool radiationBlasts = false;
		bool hearerIsVictim = false;
		bool isNoiseJoke = false;

		if (this.noise.type == "Joke")
		{
			isNoiseJoke2 = true;
			isNoiseJoke = true;
			this.noise.playfieldObject.GetComponent<Agent>().statusEffects.jokeAgents.Add(this.agent);
			this.agent.listeningToAgentsJoke = this.noise.playfieldObject.GetComponent<Agent>();
			this.agent.Say("");
		}

		this.agent.SetTraversable("IgnoreFireSpewer");
		this.agent.goBackToPosition = true;

		if (this.agent.job == "")
		{
			if (this.noise.noiseCrimes.Count > 0)
			{
				using (List<NoiseCrime>.Enumerator enumerator = this.noise.noiseCrimes.GetEnumerator())
					while (enumerator.MoveNext())
						if (enumerator.Current.victim == this.agent && this.agent.prisoner == 0)
							hearerIsVictim = true;
			}

			if (this.noise.type == "DoorHelper" && this.noise.doorHelperAgent == this.agent)
			{
				DoorHelper doorHelper = (DoorHelper)this.noise.playfieldObject;
				GoalPathTo goalPathTo = new GoalPathTo();

				try
				{
					goalPathTo.pathToPosition = doorHelper.door.tr.position;
				}
				catch
				{
					goalPathTo.pathToPosition = this.noise.tr.position;
				}

				this.brain.AddSubgoal(this, goalPathTo);
				GoalPause goalPause = new GoalPause();
				if (isNoiseJoke2)
				{
					goalPause.pauseTime = 3f;
				}
				else
				{
					goalPause.pauseTime = 1f;
				}
				this.brain.AddSubgoal(this, goalPause);
				isDoorNoise = true;
				this.agent.answeringDoor = true;
			}
			else if (this.noise.type == "WindowHelper" && this.noise.windowHelperAgent == this.agent)
			{
				GoalPathTo goalPathTo2 = new GoalPathTo();
				Window window = ((WindowHelper)this.noise.playfieldObject).window;
				try
				{
					goalPathTo2.pathToPosition = window.windowHelper.tr.position;
				}
				catch
				{
					goalPathTo2.pathToPosition = this.noise.tr.position;
				}
				this.brain.AddSubgoal(this, goalPathTo2);
				isWindowNoise = true;
			}
			else if (this.noise.type == "Attract")
			{
				TileData tileData = this.gc.tileInfo.GetTileData(this.agent.tr.position);
				TileData tileData2 = this.gc.tileInfo.GetTileData(this.noise.tr.position);
				if ((tileData.chunkID == tileData2.chunkID || tileData2.owner == 0) && tileData.owner == tileData2.owner && Vector2.Distance(this.agent.tr.position, this.noise.playfieldObject.tr.position) > 0.96f)
				{
					GoalPathTo goalPathTo3 = new GoalPathTo();
					goalPathTo3.pathToPosition = this.noise.tr.position;
					this.brain.AddSubgoal(this, goalPathTo3);
				}
			}
		}

		if (!this.agent.movement.HasLOSPosition360(this.noisePosition))
		{
			TileData noiseSourceTile = this.gc.tileInfo.GetTileData(this.noisePosition);
			bool notMyProperty = false;

			if (noiseSourceTile.owner > 0)
			{
				if (noiseSourceTile.owner != this.agent.ownerID)
					notMyProperty = true;

				if (noiseSourceTile.owner == this.agent.ownerID && noiseSourceTile.chunkID != this.agent.startingChunk)
					notMyProperty = true;
			}

			if (noiseSourceTile.prison > 0)
			{
				if (noiseSourceTile.prison != this.agent.prisoner && !noiseSourceTile.prisonOpened)
					blockedByPrisonOrLockdown = true;

				if (noiseSourceTile.prison == this.agent.prisoner && noiseSourceTile.chunkID != this.agent.startingChunk && !noiseSourceTile.prisonOpened)
					blockedByPrisonOrLockdown = true;
			}

			if (this.gc.lockdown && this.gc.tileInfo.DifferentLockdownZones(noiseSourceTile, this.agent.curTileData))
				blockedByPrisonOrLockdown = true;

			bool isMyProperty = false;
			
			if (noiseSourceTile.owner > 0 && noiseSourceTile.owner == this.agent.ownerID && noiseSourceTile.chunkID == this.agent.startingChunk && this.noise.type != "WindowHelper" && !this.agent.haunted)
				isMyProperty = true;
			
			if (this.gc.levelFeeling == "HarmAtIntervals" && !this.agent.ghost && !this.gc.tileInfo.IsIndoors(this.noise.tr.position))
				radiationBlasts = true;
			
			bool soundHeard = false;

			////////////////////////
			#region Vigilance Checks

			// 1: Soldier, Slavemaster, Shopkeeper; 3: Supercop
			if (!hearerIsVictim 
				&& (this.agent.modVigilant == 1 || this.agent.modVigilant == 3)  
				&& this.agent.job == "" 
				&& this.agent.prisoner == 0 
				&& !this.agent.arenaBattler 
				&& (!notMyProperty || this.agent.modVigilant == 3)  // Supercop duty
				&& !blockedByPrisonOrLockdown 
				&& !isDoorNoise 
				&& !isWindowNoise 
				&& this.noise.volume != 0f 
				&& !radiationBlasts 
				&& !noiseSourceTile.arenaFloor 
				&& !this.agent.haunted)
			{
				soundHeard = true;
				GoalCurious goalCurious = new GoalCurious();
				goalCurious.curiousPosition = this.noisePosition;
				this.brain.AddSubgoal(this, goalCurious);
				this.agent.SetRunBackToPosition(true);

				if (this.agent.isPlayer == 0)
					this.agent.SayDialogue("NoiseReact");
				
				if (this.agent.oma.modProtectsProperty > 0 && isMyProperty)
					this.gc.spawnerMain.SpawnStateIndicator(this.agent, "Search");
			}

			// 2: Bartender, Bouncer, Cannibal, Goon
			if (!hearerIsVictim 
				&& this.agent.modVigilant == 2  
				&& noiseSourceTile.owner == this.agent.ownerID
				&& this.agent.job == "" && this.agent.prisoner == 0 
				&& !this.agent.arenaBattler 
				&& !notMyProperty // No supercop save
				&& !blockedByPrisonOrLockdown 
				&& !isDoorNoise 
				&& !isWindowNoise 
				&& this.noise.volume != 0f 
				&& !radiationBlasts 
				&& !noiseSourceTile.arenaFloor 
				&& !this.agent.haunted)
			{
				soundHeard = true;
				GoalCurious goalCurious = new GoalCurious();
				goalCurious.curiousPosition = this.noisePosition;
				this.brain.AddSubgoal(this, goalCurious);
				this.agent.SetRunBackToPosition(true);

				if (this.agent.isPlayer == 0)
					this.agent.SayDialogue("NoiseReact");
				
				if (this.agent.oma.modProtectsProperty > 0 && isMyProperty)
					this.gc.spawnerMain.SpawnStateIndicator(this.agent, "Search");
			}

			if (!hearerIsVictim 
				// 3f noise volume: STRICTLY distraction items and hacks
				&& ((this.noise.volume == 3f && !isDoorNoise && !notMyProperty && this.gc.levelFeeling != "Lockdown") || isNoiseJoke) 
				&& this.agent.modVigilant != 1 // 0, 2, 3: Anyone BUT Shopkeeper, Slavemaster & Soldier
				&& this.agent.job == "" 
				&& this.agent.prisoner == 0 
				&& !this.agent.arenaBattler 
				&& (!this.agent.dontLeavePost /* Only Bouncer & Res Leader have true*/ || isMyProperty) 
				&& !blockedByPrisonOrLockdown 
				&& !isWindowNoise 
				&& !radiationBlasts 
				&& !noiseSourceTile.arenaFloor)
				// Skips check for Haunted
			{
				soundHeard = true;
				GoalCurious goalCurious = new GoalCurious();
				goalCurious.curiousPosition = this.noisePosition;
				this.brain.AddSubgoal(this, goalCurious);
				this.agent.SetRunBackToPosition(true);

				if (this.agent.isPlayer == 0)
					this.agent.SayDialogue("NoiseReact");
				
				if (this.agent.oma.modProtectsProperty > 0 && isMyProperty)
					this.gc.spawnerMain.SpawnStateIndicator(this.agent, "Search");
			}

			// All agents
			if (!soundHeard 
				&& !hearerIsVictim 
				&& this.noise.volume == 4f // Alarms, explosions, etc. Loudest value.
				&& this.agent.job == "" 
				&& this.agent.prisoner == 0 
				&& !this.agent.arenaBattler 
				&& (!this.agent.dontLeavePost || isMyProperty) 
				&& !radiationBlasts 
				&& !noiseSourceTile.arenaFloor)
				// Skips check for Haunted
			{
				GoalCurious goalCurious = new GoalCurious();
				goalCurious.curiousPosition = this.noisePosition;
				this.brain.AddSubgoal(this, goalCurious);
				this.agent.SetRunBackToPosition(true);

				if (this.agent.isPlayer == 0)
					this.agent.SayDialogue("NoiseReact");
			
				if (this.agent.oma.modProtectsProperty > 0 && isMyProperty)
					this.gc.spawnerMain.SpawnStateIndicator(this.agent, "Search");
			}

			// Aligned combat-noise search response
			if (!soundHeard 
				&& this.noise.searchAgentSource != null 
				&& this.agent.modToughness > 0 //
				&& this.agent.modVigilant > 0  // 
				&& this.agent.job == "" 
				&& this.agent.prisoner == 0 
				&& !blockedByPrisonOrLockdown 
				&& !radiationBlasts 
				&& !noiseSourceTile.arenaFloor)
			{
				bool searchForAlignedCombat = false;

				for (int i = 0; i < this.gc.agentList.Count; i++)
				{
					Agent agent = this.gc.agentList[i];

					if ((agent.justHitByAgent == this.noise.searchAgentSource || agent.opponent == this.noise.searchAgentSource) 
						&& this.agent.relationships.GetRel(agent) == "Aligned")
					{
						searchForAlignedCombat = true;
						break;
					}
				}

				if (searchForAlignedCombat)
				{
					GoalCurious goalCurious = new GoalCurious();
					goalCurious.curiousPosition = this.noisePosition;
					this.brain.AddSubgoal(this, goalCurious);
					this.agent.SetRunBackToPosition(true);

					if (this.agent.isPlayer == 0)
						this.agent.SayDialogue("NoiseReact");

					this.gc.spawnerMain.SpawnStateIndicator(this.agent, "Search");
				}
			}
#endregion

			int count = this.noise.noiseCrimes.Count;
			if (this.agent.job != "Follow" && !isDoorNoise)
			{
				if (isWindowNoise)
				{
					Window window2 = ((WindowHelper)this.noise.playfieldObject).window;
					GoalRotateToPosition goalRotateToPosition = new GoalRotateToPosition();
					try
					{
						goalRotateToPosition.pos = window2.tr.position;
					}
					catch
					{
						goalRotateToPosition.pos = this.noise.tr.position;
					}
					this.brain.AddSubgoal(this, goalRotateToPosition);
				}
				else
				{
					GoalRotateToPosition goalRotateToPosition2 = new GoalRotateToPosition();
					if (this.noise.type == "WindowHelper" && this.noise.outsideWindowNoisePos != Vector3.zero)
					{
						goalRotateToPosition2.pos = this.noise.outsideWindowNoisePos;
					}
					else
					{
						goalRotateToPosition2.pos = this.noisePosition;
					}
					this.brain.AddSubgoal(this, goalRotateToPosition2);
				}
				if (!isNoiseJoke2)
				{
					GoalPause goalPause2 = new GoalPause();
					goalPause2.pauseTime = UnityEngine.Random.Range(0.5f, 1.5f);
					this.brain.AddSubgoal(this, goalPause2);
				}
				else
				{
					GoalPause goalPause3 = new GoalPause();
					goalPause3.pauseTime = 3f;
					this.brain.AddSubgoal(this, goalPause3);
				}
			}
			if (this.agent.job == "Follow" && this.agent.pathing == 0 && !isDoorNoise)
			{
				GoalRotateToPosition goalRotateToPosition3 = new GoalRotateToPosition();
				goalRotateToPosition3.pos = this.noisePosition;
				this.brain.AddSubgoal(this, goalRotateToPosition3);
			}
		}
		else
		{
			int count2 = this.noise.noiseCrimes.Count;
			if (this.agent.job != "Follow" && !isDoorNoise)
			{
				if (isWindowNoise)
				{
					Window window3 = ((WindowHelper)this.noise.playfieldObject).window;
					GoalRotateToPosition goalRotateToPosition4 = new GoalRotateToPosition();
					try
					{
						goalRotateToPosition4.pos = window3.tr.position;
					}
					catch
					{
						goalRotateToPosition4.pos = this.noise.tr.position;
					}
					this.brain.AddSubgoal(this, goalRotateToPosition4);
				}
				else
				{
					GoalRotateToPosition goalRotateToPosition5 = new GoalRotateToPosition();
					if (this.noise.type == "WindowHelper" && this.noise.outsideWindowNoisePos != Vector3.zero)
					{
						goalRotateToPosition5.pos = this.noise.outsideWindowNoisePos;
					}
					else
					{
						goalRotateToPosition5.pos = this.noisePosition;
					}
					this.brain.AddSubgoal(this, goalRotateToPosition5);
				}
				if (!isNoiseJoke2)
				{
					GoalPause goalPause4 = new GoalPause();
					goalPause4.pauseTime = UnityEngine.Random.Range(0.5f, 1.5f);
					this.brain.AddSubgoal(this, goalPause4);
				}
				else
				{
					GoalPause goalPause5 = new GoalPause();
					goalPause5.pauseTime = 3f;
					this.brain.AddSubgoal(this, goalPause5);
				}
			}
			if (this.agent.job == "Follow" && this.agent.pathing == 0 && !isDoorNoise)
			{
				GoalRotateToPosition goalRotateToPosition6 = new GoalRotateToPosition();
				goalRotateToPosition6.pos = this.noisePosition;
				this.brain.AddSubgoal(this, goalRotateToPosition6);
			}
		}

		if (hearerIsVictim && (!this.agent.dontLeavePost || this.noise.volume == 3f || this.noise.volume == 4f) && !radiationBlasts && this.noise.searchAgentSource != null && !this.agent.haunted)
		{
			this.brain.TerminateSubgoalType(this, "Pause");
			this.brain.RemoveSubgoalType(this, "Pause");
			this.agent.SetRunBackToPosition(true);
			this.gc.spawnerMain.SpawnStateIndicator(this.agent, "Search");
			this.crimeInvestigation = 1;
			this.agent.investigatingCrime = true;
			this.agent.crimeInvestigationTime = 0f;
			if (this.agent.isPlayer == 0)
			{
				this.agent.SayDialogue("NoiseReact");
			}
		}
	}

	// Token: 0x04000FB4 RID: 4020
	public Noise noise;

	// Token: 0x04000FB5 RID: 4021
	public Vector3 noisePosition;

	// Token: 0x04000FB6 RID: 4022
	public int crimeInvestigation;

	// Token: 0x04000FB7 RID: 4023
	public bool onSearch;

	// Token: 0x04000FB8 RID: 4024
	public int numSearches = 3;

	// Token: 0x04000FB9 RID: 4025
	public int waitCyclesThenStop;
}


// Quests
// Token: 0x0600337A RID: 13178 RVA: 0x002D0378 File Offset: 0x002CE578
public void setupQuests()
{
	this.settingUpQuests = true;
	this.questItemsGiven.Remove("Money");
	this.questItemsGiven.Remove("TwitchMystery");
	this.questItemsGiven.Remove("Nugget");

	if (this.gc.loadLevel.LevelContainsMayor())
	{
		this.oneQuest = "MayorQuest";
	}
	if (this.oneQuest != "" || this.gc.levelType == "Tutorial" || this.gc.streamingWorld)
	{
		this.totalQuests = 1;
	}
	this.gc.challenges.Contains("Sandbox");

	while (this.questTriesMain < this.questTriesTotal && this.settingUpQuests)
	{
		int num3 = 0;
		bool flag2 = false;

		while (num3 < 100 && !flag2 && this.settingUpQuests)
		{
			Agent agent9 = null;
			Agent agent2 = null;
			ObjectReal objectReal = null;
			ObjectReal objectReal2 = null;
			ObjectReal objectReal3 = null;
			string text2 = "";
			string text3 = "";
			bool flag3 = false;
			int num4 = 0;
			bool flag4 = false;

			if (this.numQuests == this.totalQuests - 1 && this.oneQuest == "" && this.gc.levelType != "Tutorial")
			{
				flag4 = true;
				while (!flag3)
				{
					if (num4 >= 100)
					{
						break;
					}
					agent9 = this.gc.agentList[UnityEngine.Random.Range(0, this.gc.agentList.Count)];
					try
					{
						if (!this.CheckIfQuested(agent9, null, "QuestGiver") && (agent9.agentName == "Shopkeeper" || agent9.agentName == "DrugDealer" || (agent9.agentName == "Clerk" && agent9.startingChunkRealDescription != "DeportationCenter") || agent9.agentName == "Bartender"))
						{
							flag3 = true;
						}
						if (agent9.startingChunkReal.giveQuest > 0)
						{
							flag3 = false;
						}
					}
					catch
					{
					}
					num4++;
				}
			}
			else
			{
				flag3 = true;
			}

			if (flag3)
			{
				if (this.gc.streamingWorld)
				{
					UnityEngine.Random.InitState(this.gc.loadLevel.randomSeedNum + num3);
				}
				bool flag5 = true;
				this.chosenChunk = null;
				if (this.gc.levelType == "Tutorial")
				{
					text2 = "Destroy";
					for (int num5 = 0; num5 < this.gc.objectRealList.Count; num5++)
					{
						ObjectReal objectReal6 = this.gc.objectRealList[num5];
						if (objectReal6.objectName == "Generator")
						{
							objectReal = objectReal6;
							break;
						}
					}
				}
				else if (flag5 && this.oneQuest == "")
				{
					if (agent9 != null)
					{
						agent9.startingChunkReal.quested = true;
					}
					string text4 = "";
					bool flag6 = true;
					if (this.gc.customLevel && num3 < 10 && !this.gc.loadLevel.customLevel.randomizeQuests)
					{
						foreach (Chunk chunk in this.gc.loadLevel.levelChunks)
						{
							if (chunk.specificQuest != "" && chunk.specificQuest != null && chunk.specificQuest != "None" && !chunk.quested && chunk.importantObjects.Count != 0)
							{
								this.chosenChunk = chunk;
								if (chunk.specificQuest != "Random")
								{
									text4 = chunk.specificQuest;
									break;
								}
								break;
							}
						}
					}
					if (this.gc.customLevel && this.chosenChunk == null && !this.gc.loadLevel.customLevel.randomizeQuests)
					{
						flag6 = false;
					}
					if (this.chosenChunk == null && flag6)
					{
						foreach (Chunk chunk2 in this.gc.loadLevel.levelChunks)
						{
							if (chunk2.giveQuest > 0 && !chunk2.quested && chunk2.importantObjects.Count != 0 && this.SpecialCasesCheck(chunk2))
							{
								this.chosenChunk = chunk2;
								break;
							}
						}
					}
					if (this.chosenChunk == null && flag6)
					{
						int num6 = 0;
						foreach (Chunk chunk3 in this.gc.loadLevel.levelChunks)
						{
							if (!chunk3.quested && chunk3.importantObjects.Count != 0 && this.SpecialCasesCheck(chunk3) && chunk3.difficultyLevel > num6)
							{
								num6 = chunk3.difficultyLevel;
								this.chosenChunk = chunk3;
							}
						}
					}
					if (this.numQuests == 0)
					{
						bool flag7 = this.gc.percentChance(30);
						bool flag8 = this.gc.percentChance(10);
						for (int num7 = 0; num7 < this.gc.agentList.Count; num7++)
						{
							Agent agent10 = this.gc.agentList[num7];
							if (agent10.gang != 0 && !agent10.isBigQuestObject && ((agent10.agentName == "Musician" && flag7) || (agent10.agentName == "Mafia" && flag8) || (agent10.agentName == "Gangbanger" && flag8) || (agent10.agentName == "GangbangerB" && flag8)))
							{
								bool flag9 = true;
								for (int num8 = 0; num8 < this.gc.playerAgentList.Count; num8++)
								{
									Agent agent11 = this.gc.playerAgentList[num8];
									if ((agent10.agentName == "Mafia" || agent10.agentName == "Gangbanger" || agent10.agentName == "GangbangerB") && agent11.agentName == agent10.agentName)
									{
										flag9 = false;
									}
								}
								for (int num9 = 0; num9 < this.gc.agentList.Count; num9++)
								{
									Agent agent12 = this.gc.agentList[num9];
									if (agent12.gang == agent10.gang && agent12.isBigQuestObject)
									{
										flag9 = false;
									}
								}
								if (flag9)
								{
									agent2 = agent10;
									this.chosenChunk = null;
									text2 = "Kill";
									break;
								}
							}
						}
					}
					if (this.chosenChunk != null)
					{
						this.chosenChunk.quested = true;
						List<string> list = new List<string>();
						foreach (PlayfieldObject playfieldObject in this.chosenChunk.importantObjects)
						{
							if (playfieldObject.playfieldObjectType == "Agent")
							{
								agent2 = (Agent)playfieldObject;
								if (!this.isAlignedInChunk(agent2))
								{
									if (agent2.prisoner > 0 && agent2.ownerID == 0)
									{
										bool flag10 = false;
										for (int num10 = 0; num10 < this.gc.agentList.Count; num10++)
										{
											if (this.gc.agentList[num10].startingChunk == agent2.startingChunk && this.gc.agentList[num10].prisoner > 0 && this.gc.agentList[num10].prisoner != agent2.prisoner)
											{
												flag10 = true;
												break;
											}
										}
										if (flag10)
										{
											if (this.gc.levelFeeling != "Ooze")
											{
												text2 = this.gc.Choose<string>("Rescue", "Rescue", new string[]
												{
													"Rescue"
												});
											}
											agent2.potentialQuestTypes.Add("PrisonBreak");
											if (!list.Contains("PrisonBreak"))
											{
												list.Add("PrisonBreak");
												list.Add("PrisonBreak");
												list.Add("PrisonBreak");
											}
											bool flag11 = true;
											for (int num11 = 0; num11 < this.gc.playerAgentList.Count; num11++)
											{
												if (agent2.relationships.GetRel(this.gc.playerAgentList[num11]) == "Hateful")
												{
													flag11 = false;
												}
												if (!agent2.relationships.RelForRescueQuestOkay(this.gc.playerAgentList[num11]))
												{
													flag11 = false;
												}
												if (agent2.agentName == "Gorilla" && this.gc.playerAgentList[num11].bigQuest == "Gorilla" && flag4)
												{
													flag11 = false;
												}
											}
											if (this.gc.multiplayerMode && this.gc.serverPlayer)
											{
												for (int num12 = 0; num12 < this.gc.networkManagerB.nextCharacter.Count; num12++)
												{
													if (!agent2.relationships.RelForRescueQuestOkayMult(this.gc.networkManagerB.nextCharacter[num12]))
													{
														flag11 = false;
													}
													if (agent2.agentName == "Gorilla" && this.gc.networkManagerB.nextCharacter[num12] == "Gorilla" && flag4)
													{
														flag11 = false;
													}
												}
											}
											if (agent2.zombified)
											{
												flag11 = false;
											}
											if (agent9 != null)
											{
												try
												{
													if (agent9.relationships.GetRel(agent2) == "Hateful" || agent9.relationships.GetRel(agent2) == "Annoyed")
													{
														flag11 = false;
													}
												}
												catch
												{
												}
												for (int num13 = 0; num13 < this.gc.agentList.Count; num13++)
												{
													Agent agent13 = this.gc.agentList[num13];
													if (agent13.startingChunk == agent9.startingChunk && agent9.startingChunk != 0 && (agent13.relationships.GetRel(agent2) == "Hateful" || agent13.relationships.GetRel(agent2) == "Annoyed"))
													{
														flag11 = false;
													}
												}
											}
											if (flag11 && this.gc.levelFeeling != "Ooze")
											{
												agent2.potentialQuestTypes.Add("Rescue");
												if (!list.Contains("Rescue"))
												{
													list.Add("Rescue");
												}
											}
											bool flag12 = true;
											for (int num14 = 0; num14 < this.gc.playerAgentList.Count; num14++)
											{
												if (agent2.relationships.GetRel(this.gc.playerAgentList[num14]) == "Aligned" || ((this.gc.playerAgentList[num14].statusEffects.hasStatusEffect("DontHitOwnKind") || agent2.statusEffects.hasStatusEffect("DontHitOwnKind")) && this.gc.playerAgentList[num14].agentName == agent2.agentName))
												{
													flag12 = false;
												}
											}
											if (agent2.zombified)
											{
												flag12 = false;
											}
											if (flag12)
											{
												agent2.potentialQuestTypes.Add("Kill");
												if (!list.Contains("Kill"))
												{
													list.Add("Kill");
												}
											}
										}
										else
										{
											if (this.gc.levelFeeling != "Ooze")
											{
												text2 = this.gc.Choose<string>("Rescue", "Rescue", new string[]
												{
													"Rescue"
												});
											}
											bool flag13 = true;
											for (int num15 = 0; num15 < this.gc.playerAgentList.Count; num15++)
											{
												if (agent2.relationships.GetRel(this.gc.playerAgentList[num15]) == "Hateful")
												{
													flag13 = false;
												}
												if (!agent2.relationships.RelForRescueQuestOkay(this.gc.playerAgentList[num15]))
												{
													flag13 = false;
												}
												if (agent2.agentName == "Gorilla" && this.gc.playerAgentList[num15].bigQuest == "Gorilla" && flag4)
												{
													flag13 = false;
												}
											}
											if (this.gc.multiplayerMode && this.gc.serverPlayer)
											{
												for (int num16 = 0; num16 < this.gc.networkManagerB.nextCharacter.Count; num16++)
												{
													if (!agent2.relationships.RelForRescueQuestOkayMult(this.gc.networkManagerB.nextCharacter[num16]))
													{
														flag13 = false;
													}
													if (agent2.agentName == "Gorilla" && this.gc.networkManagerB.nextCharacter[num16] == "Gorilla" && flag4)
													{
														flag13 = false;
													}
												}
											}
											if (agent2.zombified)
											{
												flag13 = false;
											}
											if (agent9 != null)
											{
												try
												{
													if (agent9.relationships.GetRel(agent2) == "Hateful" || agent9.relationships.GetRel(agent2) == "Annoyed")
													{
														flag13 = false;
													}
												}
												catch
												{
												}
											}
											if (flag13 && this.gc.levelFeeling != "Ooze")
											{
												agent2.potentialQuestTypes.Add("Rescue");
												if (!list.Contains("Rescue"))
												{
													list.Add("Rescue");
													list.Add("Rescue");
												}
											}
											bool flag14 = true;
											for (int num17 = 0; num17 < this.gc.playerAgentList.Count; num17++)
											{
												if (agent2.relationships.GetRel(this.gc.playerAgentList[num17]) == "Aligned" || ((this.gc.playerAgentList[num17].statusEffects.hasStatusEffect("DontHitOwnKind") || agent2.statusEffects.hasStatusEffect("DontHitOwnKind")) && this.gc.playerAgentList[num17].agentName == agent2.agentName))
												{
													flag14 = false;
												}
											}
											if (agent2.zombified)
											{
												flag14 = false;
											}
											if (flag14)
											{
												agent2.potentialQuestTypes.Add("Kill");
												if (!list.Contains("Kill"))
												{
													list.Add("Kill");
												}
											}
										}
									}
									else if (agent2.agentName == "Slave" && agent2.slaveOwners.Count != 0)
									{
										text2 = "FreeSlave";
										agent2.potentialQuestTypes.Add("FreeSlave");
										if (!list.Contains("FreeSlave"))
										{
											list.Add("FreeSlave");
											list.Add("FreeSlave");
										}
									}
									else
									{
										bool flag15 = false;
										int num18 = 0;
										do
										{
											text2 = this.gc.Choose<string>("Kill", "KillAll", new string[]
											{
												"KillAll",
												"KillAndRetrieve"
											});
											bool flag16 = true;
											for (int num19 = 0; num19 < this.gc.playerAgentList.Count; num19++)
											{
												if (agent2.relationships.GetRel(this.gc.playerAgentList[num19]) == "Aligned" || ((this.gc.playerAgentList[num19].statusEffects.hasStatusEffect("DontHitOwnKind") || agent2.statusEffects.hasStatusEffect("DontHitOwnKind")) && this.gc.playerAgentList[num19].agentName == agent2.agentName))
												{
													flag16 = false;
												}
											}
											if (agent2.zombified)
											{
												flag16 = false;
											}
											if (flag16)
											{
												agent2.potentialQuestTypes.Add("Kill");
												if (!list.Contains("Kill"))
												{
													list.Add("Kill");
												}
												agent2.potentialQuestTypes.Add("KillAndRetrieve");
												if (!list.Contains("KillAndRetrieve"))
												{
													list.Add("KillAndRetrieve");
												}
												agent9 != null;
												flag15 = true;
											}
											if (agent2.ownerID != 0)
											{
												for (int num20 = 0; num20 < this.gc.agentList.Count; num20++)
												{
													Agent agent14 = this.gc.agentList[num20];
													if (agent2 != agent14 && agent2.startingChunk == agent14.startingChunk && agent2.ownerID == agent14.ownerID && agent2.agentName != "Bouncer" && agent2.agentName != "Guard" && agent2.agentName != "Guard2" && !agent2.ghost && agent14.agentName != "Bouncer" && agent14.agentName != "Guard" && agent14.agentName != "Guard2" && !agent14.ghost)
													{
														bool flag17 = true;
														for (int num21 = 0; num21 < this.gc.playerAgentList.Count; num21++)
														{
															if (agent2.relationships.GetRel(this.gc.playerAgentList[num21]) == "Aligned" || ((this.gc.playerAgentList[num21].statusEffects.hasStatusEffect("DontHitOwnKind") || agent2.statusEffects.hasStatusEffect("DontHitOwnKind")) && this.gc.playerAgentList[num21].agentName == agent2.agentName))
															{
																flag17 = false;
															}
														}
														if (agent2.zombified)
														{
															flag17 = false;
														}
														if (flag17)
														{
															agent2.potentialQuestTypes.Add("KillAll");
															if (!list.Contains("KillAll"))
															{
																list.Add("KillAll");
																if ((!this.gc.challenges.Contains("QuickGame") && this.gc.sessionDataBig.curLevelEndless > 9) || (this.gc.challenges.Contains("QuickGame") && this.gc.sessionDataBig.curLevelEndless > 6))
																{
																	list.Add("KillAll");
																}
																if ((!this.gc.challenges.Contains("QuickGame") && this.gc.sessionDataBig.curLevelEndless > 12) || (this.gc.challenges.Contains("QuickGame") && this.gc.sessionDataBig.curLevelEndless > 8))
																{
																	list.Add("KillAll");
																}
															}
															flag15 = true;
														}
													}
												}
											}
											num18++;
											if (flag15)
											{
												break;
											}
										}
										while (num18 < 100);
									}
								}
							}
							else if (playfieldObject.playfieldObjectType == "ObjectReal")
							{
								objectReal = (ObjectReal)playfieldObject;
								if (!this.isAlignedInChunk(objectReal))
								{
									if (objectReal.GetComponent<InvDatabase>() != null)
									{
										if (!objectReal.plantable)
										{
											if (objectReal.destroyableForQuest)
											{
												text2 = "DestroyAndRetrieve";
												objectReal.potentialQuestTypes.Add("DestroyAndRetrieve");
												if (!list.Contains("DestroyAndRetrieve"))
												{
													list.Add("DestroyAndRetrieve");
												}
											}
											else if (objectReal.chestReal && objectReal.canSpill)
											{
												text2 = "Retrieve";
												objectReal.potentialQuestTypes.Add("Retrieve");
												if (!list.Contains("Retrieve"))
												{
													list.Add("Retrieve");
												}
											}
										}
									}
									else if (objectReal.usable)
									{
										for (int num22 = 0; num22 < this.gc.objectRealList.Count; num22++)
										{
											ObjectReal objectReal7 = this.gc.objectRealList[num22];
											if (objectReal != objectReal7 && objectReal.startingChunk == objectReal7.startingChunk && objectReal.objectName == objectReal7.objectName)
											{
												text2 = "UseAll";
												objectReal.potentialQuestTypes.Add("UseAll");
												if (!list.Contains("UseAll"))
												{
													list.Add("UseAll");
													list.Add("UseAll");
												}
											}
										}
									}
									else if (!objectReal.OnFloor && (!objectReal.bulletsCanPass || !objectReal.meleeCanPass) && !objectReal.cantBeDestroyed && objectReal.damageAccumulates)
									{
										bool flag18 = true;
										text2 = "Destroy";
										for (int num23 = 0; num23 < this.gc.playerAgentList.Count; num23++)
										{
											if ((objectReal.objectName == "Generator" || objectReal.objectName == "Generator2") && this.gc.playerAgentList[num23].bigQuest == "Soldier" && flag4)
											{
												flag18 = false;
											}
											if (objectReal.objectName == "PowerBox" && this.gc.playerAgentList[num23].bigQuest == "RobotPlayer" && flag4)
											{
												flag18 = false;
											}
										}
										if (this.gc.multiplayerMode && this.gc.serverPlayer)
										{
											for (int num24 = 0; num24 < this.gc.networkManagerB.nextCharacter.Count; num24++)
											{
												if ((objectReal.objectName == "Generator" || objectReal.objectName == "Generator2") && this.gc.networkManagerB.nextCharacter[num24] == "Soldier" && flag4)
												{
													flag18 = false;
												}
												if (objectReal.objectName == "PowerBox" && this.gc.networkManagerB.nextCharacter[num24] == "RobotPlayer" && flag4)
												{
													flag18 = false;
												}
											}
										}
										if (flag18)
										{
											objectReal.potentialQuestTypes.Add("Destroy");
											if (!list.Contains("Destroy"))
											{
												list.Add("Destroy");
											}
											for (int num25 = 0; num25 < this.gc.objectRealList.Count; num25++)
											{
												ObjectReal objectReal8 = this.gc.objectRealList[num25];
												if (objectReal != objectReal8 && objectReal.startingChunk == objectReal8.startingChunk && objectReal.objectName == objectReal8.objectName)
												{
													text2 = "DestroyAll";
													objectReal.potentialQuestTypes.Add("DestroyAll");
													if (!list.Contains("DestroyAll"))
													{
														list.Add("DestroyAll");
														list.Add("DestroyAll");
													}
												}
											}
										}
									}
								}
							}
						}
						agent2 = null;
						objectReal = null;
						int num26 = 0;
						if (list.Count > 0)
						{
							bool flag19 = true;
							if (text4 != "" && list.Contains(text4))
							{
								flag19 = false;
								text2 = text4;
							}
							if (flag19)
							{
								do
								{
									text2 = list[UnityEngine.Random.Range(0, list.Count)];
									num26++;
								}
								while (num26 < 30 && this.levelQuestTypes.Contains(text2));
							}
							this.levelQuestTypes.Add(text2);
							using (List<PlayfieldObject>.Enumerator enumerator2 = this.chosenChunk.importantObjects.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									PlayfieldObject playfieldObject2 = enumerator2.Current;
									if (playfieldObject2.potentialQuestTypes.Contains(text2))
									{
										if (playfieldObject2.playfieldObjectType == "Agent")
										{
											agent2 = (Agent)playfieldObject2;
											break;
										}
										if (playfieldObject2.playfieldObjectType == "ObjectReal")
										{
											objectReal = (ObjectReal)playfieldObject2;
											break;
										}
									}
								}
								goto IL_1E99;
							}
						}
						string str2 = "Couldn't find potential quest within chunk: ";
						Chunk chunk4 = this.chosenChunk;
						Debug.Log(str2 + ((chunk4 != null) ? chunk4.ToString() : null));
					}
				}
			IL_1E99:
				if (this.oneQuest != "")
				{
					if (this.oneQuest == "MayorQuest")
					{
						text2 = "MayorQuest";
						for (int num27 = 0; num27 < this.gc.agentList.Count; num27++)
						{
							if (this.gc.agentList[num27].agentName == "Mayor")
							{
								agent2 = this.gc.agentList[num27];
								this.chosenChunk = agent2.startingChunkReal;
								break;
							}
						}
					}
					else if (this.oneQuest == "FindBombs")
					{
						text2 = "FindBombs";
						int num28 = 0;

						foreach (Chunk chunk5 in this.gc.loadLevel.levelChunks)
						{
							if (chunk5.giveQuest > 0 && !chunk5.quested)
							{
								foreach (KeyValuePair<int, ObjectReal> keyValuePair in this.gc.chestDic)
								{
									ObjectReal value = keyValuePair.Value;
									if (value.startingChunkReal == chunk5 && value.canSpill && this.gc.tileInfo.IsIndoors(value.tr.position) && value.objectName != "WasteBasket")
									{
										if (num28 == 0)
										{
											objectReal = value;
										}
										else if (num28 == 1)
										{
											objectReal2 = value;
										}
										else if (num28 == 2)
										{
											objectReal3 = value;
										}
										num28++;
										chunk5.quested = true;
										break;
									}
								}
								if (num28 == 3)
								{
									break;
								}
							}
						}
						int num29 = 0;
						while (num28 < 3 && num29 < 5)
						{
							foreach (Chunk chunk6 in this.gc.loadLevel.levelChunks)
							{
								if (!chunk6.quested)
								{
									foreach (KeyValuePair<int, ObjectReal> keyValuePair2 in this.gc.chestDic)
									{
										ObjectReal value2 = keyValuePair2.Value;
										if (value2.startingChunkReal == chunk6 && value2.canSpill && this.gc.tileInfo.IsIndoors(value2.tr.position))
										{
											if (num28 == 0)
											{
												objectReal = value2;
											}
											else if (num28 == 1)
											{
												objectReal2 = value2;
											}
											else if (num28 == 2)
											{
												objectReal3 = value2;
											}
											num28++;
											chunk6.quested = true;
											break;
										}
									}
									if (num28 == 3)
									{
										break;
									}
								}
							}
							num29++;
						}
						num29 = 0;
						while (num28 < 3 && num29 < 5)
						{
							foreach (KeyValuePair<int, ObjectReal> keyValuePair3 in this.gc.chestDic)
							{
								ObjectReal value3 = keyValuePair3.Value;
								if (value3.canSpill && value3 != objectReal && value3 != objectReal2 && value3 != objectReal3)
								{
									if (num28 == 0)
									{
										objectReal = value3;
									}
									else if (num28 == 1)
									{
										objectReal2 = value3;
									}
									else if (num28 == 2)
									{
										objectReal3 = value3;
									}
									num28++;
									break;
								}
							}
							if (num28 == 3)
							{
								break;
							}
							num29++;
						}
					}
				}
				if (text2 == "")
				{
					text2 = "Kill";
				}
				if (agent9 != null)
				{
					text3 = this.setQuestInfo(agent9.agentName, text2);
				}
				if (this.gc.streamingWorld)
				{
					text2 = "Kill";
				}
				if (text2 != null)
				{
					uint num30 = < PrivateImplementationDetails >.ComputeStringHash(text2);
					if (num30 <= 1230354862U)
					{
						if (num30 <= 740927535U)
						{
							if (num30 <= 321077238U)
							{
								if (num30 != 24605961U)
								{
									if (num30 == 321077238U)
									{
										if (text2 == "DestroyAndRetrieve")
										{
											if (objectReal == null)
											{
												objectReal = this.FindObjectType(text3, agent9, "Object");
											}
											if (objectReal != null)
											{
												this.SetupQuestType(text2, agent9, objectReal, null, null, flag4);
												this.numQuests++;
												flag2 = true;
											}
										}
									}
								}
								else if (text2 == "PlantItem")
								{
									if (objectReal == null)
									{
										objectReal = this.FindObjectType(text3, agent9, "Object");
									}
									if (objectReal != null)
									{
										this.SetupQuestType(text2, agent9, objectReal, null, null, flag4);
										this.numQuests++;
										flag2 = true;
									}
								}
							}
							else if (num30 != 637477804U)
							{
								if (num30 != 737074361U)
								{
									if (num30 == 740927535U)
									{
										if (text2 == "UseAll")
										{
											if (objectReal == null)
											{
												objectReal = this.FindObjectType(text3, agent9, "Object");
											}
											if (objectReal != null)
											{
												this.SetupQuestType(text2, agent9, objectReal, null, null, flag4);
												this.numQuests++;
												flag2 = true;
											}
										}
									}
								}
								else if (text2 == "Kill")
								{
									if (agent2 == null)
									{
										agent2 = this.FindAgentType(text3, agent9, "Enemy");
									}
									if (agent2 != null)
									{
										this.SetupQuestType(text2, agent9, agent2, null, null, flag4);
										this.numQuests++;
										flag2 = true;
									}
								}
							}
							else if (text2 == "Rescue")
							{
								if (agent2 == null)
								{
									agent2 = this.FindAgentType(text3, agent9, "Prisoner");
								}
								if (agent2 != null)
								{
									this.SetupQuestType(text2, agent9, agent2, null, null, flag4);
									this.numQuests++;
									flag2 = true;
								}
							}
						}
						else if (num30 <= 778721476U)
						{
							if (num30 != 748805737U)
							{
								if (num30 == 778721476U)
								{
									if (text2 == "DestroyAll")
									{
										if (objectReal == null)
										{
											objectReal = this.FindObjectType(text3, agent9, "Object");
										}
										if (objectReal != null)
										{
											this.SetupQuestType(text2, agent9, objectReal, null, null, flag4);
											this.numQuests++;
											flag2 = true;
										}
									}
								}
							}
							else if (text2 == "Retrieve")
							{
								if (objectReal == null)
								{
									objectReal = this.FindRetrieveLocation(null, agent9, "Object");
								}
								if (objectReal != null)
								{
									this.SetupQuestType(text2, agent9, objectReal, null, null, flag4);
									this.numQuests++;
									flag2 = true;
								}
							}
						}
						else if (num30 != 915147270U)
						{
							if (num30 != 1172247267U)
							{
								if (num30 == 1230354862U)
								{
									if (text2 == "Deliver")
									{
										if (agent2 == null)
										{
											agent2 = this.FindAgentType(text3, agent9, "QuestEnder");
										}
										if (agent2 != null)
										{
											this.SetupQuestType(text2, agent9, agent2, null, null, flag4);
											this.numQuests++;
											flag2 = true;
										}
									}
								}
							}
							else if (text2 == "RoughUp")
							{
								if (agent2 == null)
								{
									agent2 = this.FindAgentType(text3, agent9, "Enemy");
								}
								if (agent2 != null)
								{
									this.SetupQuestType(text2, agent9, agent2, null, null, flag4);
									this.numQuests++;
									flag2 = true;
								}
							}
						}
						else if (text2 == "RetrieveDontKill")
						{
							if (agent2 == null)
							{
								agent2 = this.FindAgentType(text3, agent9, "Enemy");
							}
							if (agent2 != null)
							{
								objectReal = this.FindRetrieveDontKillLocation(null, agent9, "Object", agent2);
								if (objectReal != null)
								{
									this.SetupQuestType(text2, agent9, objectReal, agent2, null, flag4);
									this.numQuests++;
									flag2 = true;
								}
							}
						}
					}
					else if (num30 <= 2624426747U)
					{
						if (num30 <= 1593263083U)
						{
							if (num30 != 1299621093U)
							{
								if (num30 == 1593263083U)
								{
									if (text2 == "MayorQuest")
									{
										if (agent2 != null)
										{
											this.SetupQuestType(text2, agent9, agent2, null, null, flag4);
											this.numQuests++;
											flag2 = true;
										}
									}
								}
							}
							else if (text2 == "Destroy")
							{
								if (objectReal == null)
								{
									objectReal = this.FindObjectType(text3, agent9, "Object");
								}
								if (objectReal != null)
								{
									this.SetupQuestType(text2, agent9, objectReal, null, null, flag4);
									this.numQuests++;
									flag2 = true;
								}
							}
						}
						else if (num30 != 1758578471U)
						{
							if (num30 != 1968442976U)
							{
								if (num30 == 2624426747U)
								{
									if (text2 == "GiveStatusEffect")
									{
										if (agent2 == null)
										{
											agent2 = this.FindAgentType(text3, agent9, "Enemy");
										}
										if (agent2 != null)
										{
											this.SetupQuestType(text2, agent9, agent2, null, null, flag4);
											this.numQuests++;
											flag2 = true;
										}
									}
								}
							}
							else if (text2 == "KillAll")
							{
								if (agent2 == null)
								{
									agent2 = this.FindAgentType(text3, agent9, "Enemy");
								}
								if (agent2 != null)
								{
									this.SetupQuestType(text2, agent9, agent2, null, null, flag4);
									this.numQuests++;
									flag2 = true;
								}
							}
						}
						else if (text2 == "FindBombs")
						{
							if (objectReal == null)
							{
								objectReal = this.FindObjectType(text3, agent9, "Object");
							}
							if (objectReal != null)
							{
								this.SetupQuestType(text2, agent9, objectReal, objectReal2, objectReal3, flag4);
								this.numQuests++;
								flag2 = true;
							}
						}
					}
					else if (num30 <= 3558474965U)
					{
						if (num30 != 3145842578U)
						{
							if (num30 == 3558474965U)
							{
								if (text2 == "Escort")
								{
									if (agent2 == null)
									{
										agent2 = this.FindAgentType("Shopkeeper", agent9, text3);
									}
									if (agent2 != null)
									{
										this.SetupQuestType(text2, agent9, agent2, null, null, flag4);
										this.numQuests++;
										flag2 = true;
									}
								}
							}
						}
						else if (text2 == "FreeSlave")
						{
							if (agent2 == null)
							{
								agent2 = this.FindAgentType(text3, agent9, "Slave");
							}
							if (agent2 != null)
							{
								this.SetupQuestType(text2, agent9, agent2, null, null, flag4);
								this.numQuests++;
								flag2 = true;
							}
						}
					}
					else if (num30 != 3858897898U)
					{
						if (num30 != 3859108611U)
						{
							if (num30 == 3907636557U)
							{
								if (text2 == "PrisonBreak")
								{
									if (agent2 == null)
									{
										agent2 = this.FindAgentType(text3, agent9, "Prisoner");
									}
									if (agent2 != null)
									{
										this.SetupQuestType(text2, agent9, agent2, null, null, flag4);
										this.numQuests++;
										flag2 = true;
									}
								}
							}
						}
						else if (text2 == "UseObject")
						{
							if (objectReal == null)
							{
								objectReal = this.FindObjectType(text3, agent9, "Object");
							}
							if (objectReal != null)
							{
								this.SetupQuestType(text2, agent9, objectReal, null, null, flag4);
								this.numQuests++;
								flag2 = true;
							}
						}
					}
					else if (text2 == "KillAndRetrieve")
					{
						if (agent2 == null)
						{
							agent2 = this.FindAgentType(text3, agent9, "Enemy");
						}
						if (agent2 != null)
						{
							this.SetupQuestType(text2, agent9, agent2, null, null, flag4);
							this.numQuests++;
							flag2 = true;
						}
					}
				}
				num3++;
			}
			else
			{
				Debug.Log("Couldn't find quest agent");
				this.settingUpQuests = false;
			}
		}
		this.questTriesMain++;
		if (this.numQuests >= this.totalQuests)
		{
			this.settingUpQuests = false;
			Debug.Log("Set up all quests");
		}
	}

	if (this.questTriesMain == this.questTriesTotal && this.settingUpQuests)
	{
		this.settingUpQuests = false;
		Debug.Log("Couldn't set up all quests");
	}
}
