using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CCU
{
	public class GoalSafecrackSafe : Goal
	{
		public GoalSafecrackSafe()
		{
			this.goalName = "SafecrackSafe";
		}

		public override void Activate()
		{
			base.Activate();
			GoalPathTo goalPathTo = new GoalPathTo();
			goalPathTo.pathToPosition = this.agent.assignedPos;
			this.brain.AddSubgoal(this, goalPathTo);
			GoalRotateToObject goalRotateToObject = new GoalRotateToObject();
			goalRotateToObject.playfieldObject = this.agent.assignedObject;
			this.brain.AddSubgoal(this, goalRotateToObject);
			GoalSafecrackSafeReal subGoal = new GoalSafecrackSafeReal();
			this.brain.AddSubgoal(this, subGoal);
		}

		public override void Process()
		{
			base.Process();
			this.brain.ProcessSubgoals(this);
			this.brain.ReactivateIfFailed(this);
		}

		public override void Terminate()
		{
			base.Terminate();
			this.brain.RemoveAllSubgoals(this);
		}

		public Agent workingForAgent;
	}
}
