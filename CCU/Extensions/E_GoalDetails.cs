using BepInEx.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CCU.Extensions
{
	// TODO: Learn how to do an actual class extension, lol

	public static class E_GoalDetails
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		// Based on Window.FinishWindowCutter
		public static void FinishSafecracker(Safe safe, Agent causerAgent)
		{
			if (GC.serverPlayer)
			{
				safe.lastHitByAgent = causerAgent;
				//safe.SpecialWindowDestroy(causerAgent);
				safe.UnlockSafe(); // TODO: Verify
				safe.gc.spawnerMain.SpawnNoise(safe.tr.position, 0.5f, null, null, causerAgent);
				safe.gc.OwnCheck(causerAgent, safe.go, "Normal", 0);
				causerAgent.skillPoints.AddPoints("UnlockSafePoints");

				return;
			}

			//safe.SpecialWindowDestroy(causerAgent);
			safe.UnlockSafe(); //
			causerAgent.objectMult.ObjectAction(safe.objectNetID, "FinishSafecrack");
		}

		// TODO: FinishTamperSomething
		// Will need a split where UnlockSafe is used based on Object type

		// Based on LockpickDoorReal
		public static void SafecrackSafeReal(GoalDetails goalDetails, Agent agent)
		{
			if (goalDetails.goalTimer2 <= 0f && Vector2.Distance(agent.curPosition, agent.assignedPos) <= 0.92f)
			{
				agent.gc.audioHandler.Play(agent, "Operating");
				agent.isOperating = true;
				goalDetails.goalTimer = 2f;
				goalDetails.goalTimer2 = 9999f;
			}

			if (agent.assignedObject != null)
			{
				GC.spawnerMain.SpawnNoise(agent.assignedObject.tr.position, 0.5f, null, null, agent);
				GC.OwnCheck(agent, agent.assignedObject.go, "Operating", 0);
			}
			
			if (goalDetails.goalTimer <= 0f)
			{
				if (Vector2.Distance(agent.curPosition, agent.assignedPos) <= 0.92f)
				{
					if (agent.assignedObject != null)
					{
						if (agent.assignedObject.objectName == "Safe" && ((Safe)agent.assignedObject).locked)
							FinishSafecracker((Safe)agent.assignedObject, agent);
						
						agent.isOperating = false;
						GC.audioHandler.StopOnClients(agent, "Operating");
					}

					bool flag = true;

					if (agent.employer != null && (agent.relationships.GetRel(agent.employer) == "Aligned" || agent.relationships.GetRel(agent.employer) == "Submissive"))
						flag = false;
					
					if (flag)
					{
						agent.SetEmployer(null);
						agent.SetTraversable("");
						agent.SetFollowing(null);
						agent.oma.cantDoMoreTasks = false;
					}
					else
					{
						agent.job = "Follow";
						agent.jobCode = jobType.Follow;
						agent.StartCoroutine(agent.ChangeJobBig(""));
						agent.oma.cantDoMoreTasks = true;
					}
					
					goalDetails.goalTimer = 9999f;
					goalDetails.goalTimer2 = 5f;
					return;
				}

				agent.job = "Follow";
				agent.jobCode = jobType.Follow;
				agent.StartCoroutine(agent.ChangeJobBig(""));
				goalDetails.goalTimer = 9999f;
				goalDetails.goalTimer2 = 5f;
				agent.SayDialogue("CantPath");
			}
		}

		// TODO: TamperSomethingReal
	}
}
