namespace CCU.Content
{
	public class GoalSafecrackSafe : Goal
	{
		public GoalSafecrackSafe()
		{
			goalName = CJob.SafecrackSafe;
		}

		public override void Activate()
		{
			base.Activate();
			GoalPathTo goalPathTo = new GoalPathTo();
			goalPathTo.pathToPosition = agent.assignedPos;
			brain.AddSubgoal(this, goalPathTo);
			GoalRotateToObject goalRotateToObject = new GoalRotateToObject();
			goalRotateToObject.playfieldObject = agent.assignedObject;
			brain.AddSubgoal(this, goalRotateToObject);
			GoalSafecrackSafeReal subGoal = new GoalSafecrackSafeReal();
			brain.AddSubgoal(this, subGoal);
		}

		public override void Process()
		{
			base.Process();
			brain.ProcessSubgoals(this);
			brain.ReactivateIfFailed(this);
		}

		public override void Terminate()
		{
			base.Terminate();
			brain.RemoveAllSubgoals(this);
		}

		public Agent workingForAgent;
	}
}
