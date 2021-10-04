using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using CCU.Extensions;

namespace CCU
{
	public class GoalSafecrackSafeReal : Goal
	{
		public GoalSafecrackSafeReal()
		{
			this.goalName = "SafecrackSafeReal";
		}

		public override void Activate()
		{
			base.Activate();
			this.agent.goalDetails.goalTimer = 0f;
			this.agent.goalDetails.goalTimer2 = 0f;
		}

		public override void Process()
		{
			base.Process();
			this.agent.goalDetails.LockpickDoorReal();
			GoalSafecrackSafeReal(agent.goalDetails, agent);
			if (Vector2.Distance(this.agent.curPosition, this.agent.assignedPos) > 0.92f)
			{
				this.gc.audioHandler.StopOnClients(this.agent, "Operating");
				this.agent.isOperating = false;
				base.SetGoalState("Failed");
				return;
			}
			if (this.agent.goalDetails.goalTimer <= 0f)
			{
				base.SetGoalState("Completed");
			}
		}

		public override void Terminate()
		{
			base.Terminate();
			this.gc.audioHandler.StopOnClients(this.agent, "Operating");
			this.agent.isOperating = false;
		}
	}
}
