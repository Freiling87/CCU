using BepInEx.Logging;

namespace CCU.Systems.Custom_Jobs.TamperSomething
{
	internal class Tamper
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

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