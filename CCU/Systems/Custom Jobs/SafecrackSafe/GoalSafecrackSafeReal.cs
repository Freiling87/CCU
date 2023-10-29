﻿using BepInEx.Logging;
using BunnyLibs;
using CCU.Extensions;
using UnityEngine;

namespace CCU.Content
{
	public class GoalSafecrackSafeReal : Goal
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public GoalSafecrackSafeReal()
		{
			goalName = CJob.SafecrackSafeReal;
		}

		public override void Activate()
		{
			base.Activate();
			agent.goalDetails.goalTimer = 0f;
			agent.goalDetails.goalTimer2 = 0f;
		}

		public override void Process()
		{
			base.Process();
			E_GoalDetails.SafecrackSafeReal(agent.goalDetails, agent);

			if (Vector2.Distance(agent.curPosition, agent.assignedPos) > 0.92f)
			{
				GC.audioHandler.StopOnClients(agent, "Operating");
				agent.isOperating = false;
				SetGoalState("Failed");

				return;
			}

			if (agent.goalDetails.goalTimer <= 0f)
				SetGoalState("Completed");
		}

		public override void Terminate()
		{
			base.Terminate();
			GC.audioHandler.StopOnClients(agent, "Operating");
			agent.isOperating = false;
		}
	}
}
