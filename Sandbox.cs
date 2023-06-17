

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
			bool notMyJob = false;

			if (noiseSourceTile.owner > 0)
			{
				if (noiseSourceTile.owner != this.agent.ownerID)
					notMyJob = true;

				if (noiseSourceTile.owner == this.agent.ownerID && noiseSourceTile.chunkID != this.agent.startingChunk)
					notMyJob = true;
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
			
			bool flag10 = false;

			////////////////////////
			#region Vigilance Checks

			// Owners & Cops
			if (!hearerIsVictim 
				&& (this.agent.modVigilant == 1 || this.agent.modVigilant == 3)  //
				&& this.agent.job == "" 
				&& this.agent.prisoner == 0 
				&& !this.agent.arenaBattler 
				&& (!notMyJob || this.agent.modVigilant == 3)  // Supercop laziness override
				&& !blockedByPrisonOrLockdown 
				&& !isDoorNoise 
				&& !isWindowNoise 
				&& this.noise.volume != 0f 
				&& !radiationBlasts 
				&& !noiseSourceTile.arenaFloor 
				&& !this.agent.haunted)
			{
				flag10 = true;
				GoalCurious goalCurious = new GoalCurious();
				goalCurious.curiousPosition = this.noisePosition;
				this.brain.AddSubgoal(this, goalCurious);
				this.agent.SetRunBackToPosition(true);

				if (this.agent.isPlayer == 0)
					this.agent.SayDialogue("NoiseReact");
				
				if (this.agent.oma.modProtectsProperty > 0 && isMyProperty)
					this.gc.spawnerMain.SpawnStateIndicator(this.agent, "Search");
			}

			// Owned tiles only
			if (!hearerIsVictim 
				&& this.agent.modVigilant == 2  // Bartender, Bouncer, Cannibal, Goon
				&& noiseSourceTile.owner == this.agent.ownerID
				&& this.agent.job == "" && this.agent.prisoner == 0 
				&& !this.agent.arenaBattler 
				&& !notMyJob 
				&& !blockedByPrisonOrLockdown 
				&& !isDoorNoise 
				&& !isWindowNoise 
				&& this.noise.volume != 0f 
				&& !radiationBlasts 
				&& !noiseSourceTile.arenaFloor 
				&& !this.agent.haunted)
			{
				flag10 = true;
				GoalCurious goalCurious = new GoalCurious();
				goalCurious.curiousPosition = this.noisePosition;
				this.brain.AddSubgoal(this, goalCurious);
				this.agent.SetRunBackToPosition(true);

				if (this.agent.isPlayer == 0)
					this.agent.SayDialogue("NoiseReact");
				
				if (this.agent.oma.modProtectsProperty > 0 && isMyProperty)
					this.gc.spawnerMain.SpawnStateIndicator(this.agent, "Search");
			}

			// Ignores Haunted
			if (!hearerIsVictim 
				&& ((this.noise.volume == 3f && !isDoorNoise && !notMyJob && this.gc.levelFeeling != "Lockdown") || isNoiseJoke) 
				&& this.agent.modVigilant != 1  // Shopkeeper, Slavemaster & Soldier
				&& this.agent.job == "" 
				&& this.agent.prisoner == 0 
				&& !this.agent.arenaBattler 
				&& (!this.agent.dontLeavePost || isMyProperty) 
				&& !blockedByPrisonOrLockdown 
				&& !isWindowNoise 
				&& !radiationBlasts 
				&& !noiseSourceTile.arenaFloor)
			{
				flag10 = true;
				GoalCurious goalCurious = new GoalCurious();
				goalCurious.curiousPosition = this.noisePosition;
				this.brain.AddSubgoal(this, goalCurious);
				this.agent.SetRunBackToPosition(true);

				if (this.agent.isPlayer == 0)
					this.agent.SayDialogue("NoiseReact");
				
				if (this.agent.oma.modProtectsProperty > 0 && isMyProperty)
					this.gc.spawnerMain.SpawnStateIndicator(this.agent, "Search");
			}

			if (!flag10 
				&& !hearerIsVictim 
				&& this.noise.volume == 4f 
				&& this.agent.job == "" 
				&& this.agent.prisoner == 0 
				&& !this.agent.arenaBattler 
				&& (!this.agent.dontLeavePost || isMyProperty) 
				&& !radiationBlasts 
				&& !noiseSourceTile.arenaFloor)
			{
				GoalCurious goalCurious4 = new GoalCurious();
				goalCurious4.curiousPosition = this.noisePosition;
				this.brain.AddSubgoal(this, goalCurious4);
				this.agent.SetRunBackToPosition(true);

				if (this.agent.isPlayer == 0)
					this.agent.SayDialogue("NoiseReact");
			
				if (this.agent.oma.modProtectsProperty > 0 && isMyProperty)
					this.gc.spawnerMain.SpawnStateIndicator(this.agent, "Search");
			}

			if (!flag10 
				&& this.noise.searchAgentSource != null 
				&& this.agent.modToughness > 0 
				&& this.agent.modVigilant > 0  //
				&& this.agent.job == "" 
				&& this.agent.prisoner == 0 
				&& !blockedByPrisonOrLockdown 
				&& !radiationBlasts 
				&& !noiseSourceTile.arenaFloor)
			{
				bool flag11 = false;

				for (int i = 0; i < this.gc.agentList.Count; i++)
				{
					Agent agent = this.gc.agentList[i];

					if ((agent.justHitByAgent == this.noise.searchAgentSource || agent.opponent == this.noise.searchAgentSource) && this.agent.relationships.GetRel(agent) == "Aligned")
					{
						flag11 = true;
						break;
					}
				}

				if (flag11)
				{
					GoalCurious goalCurious5 = new GoalCurious();
					goalCurious5.curiousPosition = this.noisePosition;
					this.brain.AddSubgoal(this, goalCurious5);
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
