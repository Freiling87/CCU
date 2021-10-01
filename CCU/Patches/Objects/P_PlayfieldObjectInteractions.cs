using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using UnityEngine;
using RogueLibsCore;
using CCU.Traits.AI;
using Random = UnityEngine.Random;
using System.Reflection;
using CCU.Traits;
using CCU.Traits.AI.Hire;
using CCU.Traits.AI.Vendor;
using CCU.Traits.AI.TraitTrigger;
using Google2u;

namespace CCU.Patches.Objects
{
	[HarmonyPatch(declaringType:typeof(PlayfieldObjectInteractions))]
	public static class P_PlayfieldObjectInteractions
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(methodName:nameof(PlayfieldObjectInteractions.TargetObject), argumentTypes: new[] { typeof(PlayfieldObject), typeof(Agent), typeof(PlayfieldObject), typeof(string) })]
		public static bool TargetObject_Prefix(PlayfieldObject playfieldObject, Agent interactingAgent, PlayfieldObject otherObject, string combineType, PlayfieldObjectInteractions __instance, ref bool __result)
		{
			// TODO: GoalDoJob.Terminate()

			Agent agent = (Agent)playfieldObject;

			if ((agent.commander.target.targetType == "HireSafecrackTarget" ||
				agent.commander.target.targetType == "HireTamperTarget") &&
				otherObject != null)
			{
				if (agent.gc.splitScreen && Vector2.Distance(agent.commander.curPosition, otherObject.curPosition) > 15f)
					return false;

				if (otherObject.CompareTag("ObjectReal"))
				{
					ObjectReal objectReal = (ObjectReal)otherObject;

					if (agent.commander.target.targetType == "HireSafecrackTarget")
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
					else if (agent.commander.target.targetType == "HireTamperTarget")
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

		private static void HireTamper(Agent agent, Agent interactingAgent, ObjectReal myObject)
		{
			if (interactingAgent.gc.serverPlayer)
			{
				agent.job = "HireTamperTarget";
				agent.jobCode = jobType.LockpickDoor; //
				agent.StartCoroutine(agent.ChangeJobBig(""));
				agent.assignedPos = myObject.GetComponent<ObjectReal>().FindDoorObjectAgentPos();
				agent.assignedObject = myObject.playfieldObjectReal;
				agent.assignedAgent = null;
				agent.gc.audioHandler.Play(agent, "AgentOK");

				return;
			}

			interactingAgent.objectMult.CallCmdObjectActionExtraObjectID(agent.objectNetID, "HireTamperTarget", myObject.objectNetID);
		}

		private static void HireSafecrack(Agent agent, Agent interactingAgent, ObjectReal myObject)
		{
			if (interactingAgent.gc.serverPlayer)
			{
				agent.job = "HireSafecrackTarget";
				agent.jobCode = jobType.LockpickDoor; //
				agent.StartCoroutine(agent.ChangeJobBig(""));
				agent.assignedPos = myObject.GetComponent<ObjectReal>().FindDoorObjectAgentPos();
				agent.assignedObject = myObject.playfieldObjectReal;
				agent.assignedAgent = null;
				agent.gc.audioHandler.Play(agent, "AgentOK");

				return;
			}

			interactingAgent.objectMult.CallCmdObjectActionExtraObjectID(agent.objectNetID, "HireSafecrackTarget", myObject.objectNetID);
		}
	}
}
