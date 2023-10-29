using BepInEx.Logging;
using BunnyLibs;
using CCU.Systems.Custom_Jobs.SafecrackSafe;
using CCU.Systems.Custom_Jobs.TamperSomething;
using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;

namespace CCU.Systems.Custom_Jobs
{
	internal class CustomJobs { }

	[HarmonyPatch(typeof(Agent))]
	public class P_Agent
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(nameof(Agent.GetCodeFromJob))]
		public static bool GetCodeFromJob_Prefix(int jobInt, ref string __result)
		{
			switch (jobInt)
			{
				case 12:
					__result = CJob.SafecrackSafe;
					break;
				case 13:
					__result = CJob.TamperSomething;
					break;
				default:
					return true;
			}

			return false;
		}

		[HarmonyPrefix, HarmonyPatch(nameof(Agent.GetJobCode))]
		public static bool GetJobCode_Prefix(string jobString, ref jobType __result)
		{
			switch (jobString)
			{
				case CJob.SafecrackSafe:
					__result = jobType.GetSupplies; // TODO
					break;
				case CJob.TamperSomething:
					__result = jobType.GetSupplies; // TODO
					break;
				default:
					return true;
			}

			return false;
		}

		[HarmonyPrefix, HarmonyPatch(nameof(Agent.ObjectAction))]
		public static bool ObjectAction_Prefix(string myAction, string extraString, float extraFloat, Agent causerAgent, PlayfieldObject extraObject, Agent __instance, ref bool ___noMoreObjectActions)
		{
			Core.LogMethodCall();

			if (myAction == CJob.TamperSomething || myAction == CJob.SafecrackSafe)
			{
				// base.ObjectAction(myAction, extraString, extraFloat, causerAgent, extraObject);
				MethodInfo objectAction_base = AccessTools.DeclaredMethod(typeof(Agent).BaseType, "ObjectAction");
				objectAction_base.GetMethodWithoutOverrides<Action<string, string, float, Agent, PlayfieldObject>>(__instance).Invoke(myAction, extraString, extraFloat, causerAgent, extraObject);

				if (!___noMoreObjectActions)
				{
					if (myAction == CJob.SafecrackSafe)
						Safecrack.SafecrackSafe(__instance, causerAgent, extraObject);
					else if (myAction == CJob.TamperSomething)
						Tamper.TamperSomething(__instance, causerAgent, extraObject);

					___noMoreObjectActions = false;
				}

				return false;
			}

			return true;
		}

	}

	[HarmonyPatch(typeof(PlayfieldObjectInteractions))]
	public static class P_PlayfieldObjectInteractions
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(nameof(PlayfieldObjectInteractions.TargetObject))]
		public static bool TargetObject_Prefix(PlayfieldObject playfieldObject, PlayfieldObject otherObject, string combineType, ref bool __result)
		{
			if (!playfieldObject.CompareTag("Agent"))
				return true;

			Agent agent = (Agent)playfieldObject;

			if ((agent.commander.target.targetType == CJob.SafecrackSafe || agent.commander.target.targetType == CJob.TamperSomething) && otherObject != null)
			{
				if (agent.gc.splitScreen && Vector2.Distance(agent.commander.curPosition, otherObject.curPosition) > 15f)
					return false;

				if (otherObject.CompareTag("ObjectReal"))
				{
					ObjectReal objectReal = (ObjectReal)otherObject;

					if (agent.commander.target.targetType == CJob.SafecrackSafe)
					{
						bool isValidTarget = false;
						bool isWindow = false;

						if (objectReal.objectName == "Safe" && ((Safe)objectReal).locked)
							isValidTarget = true;

						if (!isValidTarget)
						{
							__result = false;
							return false;
						}

						if ((agent.ownerID != objectReal.owner || agent.startingChunk != objectReal.startingChunk || agent.ownerID == 0) &&
							(objectReal.prisonObject == 0 || objectReal.onPrisonEdge || isWindow) &&
							!objectReal.someoneInteracting)
						{
							if (combineType == "Combine")
							{
								HireSafecrack(agent, agent.commander, objectReal);
								agent.agentInteractions.interactor.mainGUI.invInterface.HideTarget();
							}

							__result = true;
							return false;
						}

						__result = false;
						return false;
					}
					else if (agent.commander.target.targetType == CJob.TamperSomething)
					{
						if ((agent.ownerID != objectReal.owner || agent.startingChunk != objectReal.startingChunk || agent.ownerID == 0) &&
							objectReal.functional && !objectReal.destroyed && !objectReal.destroying && objectReal.fire == null && !objectReal.someoneInteracting && !objectReal.startedFlashing)
						{
							if (combineType == "Combine")
							{
								HireTamper(agent, agent.commander, objectReal);
								agent.agentInteractions.interactor.mainGUI.invInterface.HideTarget();
							}

							__result = true;
							return false;
						}

						__result = false;
						return false;
					}
				}

				__result = false;
				return false;
			}

			return true;
		}
		private static void HireSafecrack(Agent agent, Agent interactingAgent, ObjectReal myObject)
		{
			if (interactingAgent.gc.serverPlayer)
			{
				agent.job = CJob.SafecrackSafe;
				agent.jobCode = jobType.Ruckus; // TODO
				agent.StartCoroutine(agent.ChangeJobBig(""));
				agent.assignedPos = myObject.GetComponent<ObjectReal>().FindDoorObjectAgentPos();
				agent.assignedObject = myObject.playfieldObjectReal;
				agent.assignedAgent = null;
				agent.gc.audioHandler.Play(agent, "AgentOK");

				return;
			}

			interactingAgent.objectMult.CallCmdObjectActionExtraObjectID(agent.objectNetID, CJob.SafecrackSafe, myObject.objectNetID);
		}
		private static void HireTamper(Agent agent, Agent interactingAgent, ObjectReal myObject)
		{
			if (interactingAgent.gc.serverPlayer)
			{
				agent.job = CJob.TamperSomething;
				agent.jobCode = jobType.Ruckus; // TODO
				agent.StartCoroutine(agent.ChangeJobBig(""));
				agent.assignedPos = myObject.GetComponent<ObjectReal>().FindDoorObjectAgentPos();
				agent.assignedObject = myObject.playfieldObjectReal;
				agent.assignedAgent = null;
				agent.gc.audioHandler.Play(agent, "AgentOK");

				return;
			}

			interactingAgent.objectMult.CallCmdObjectActionExtraObjectID(agent.objectNetID, CJob.TamperSomething, myObject.objectNetID);
		}
	}
}