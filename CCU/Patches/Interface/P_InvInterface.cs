using BepInEx.Logging;
using CCU.Traits;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace CCU.Patches.Interface
{
	[HarmonyPatch(declaringType: typeof(InvInterface))]
	public static class P_InvInterface
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(InvInterface.ShowTarget2))]
		public static bool ShowTarget2_Prefix(InvInterface __instance)
		{
			if (__instance.mainGUI.targetItem != null)
			{
				for (int i = 0; i < GC.objectRealList.Count; i++)
				{
					ObjectReal objectReal = GC.objectRealList[i];

					if (objectReal.objectSprite != null)
					{
						objectReal.objectSprite.useWithItem2[__instance.mainGUI.agent.isPlayer - 1] = false;
					
						if (__instance.mainGUI.targetItem.TargetObject(objectReal, ""))
							objectReal.objectSprite.SetHighlight("Target", __instance.mainGUI.agent);
					}
				}

				for (int j = 0; j < GC.agentList.Count; j++)
				{
					Agent agent = GC.agentList[j];
				
					if (agent.objectSprite != null)
					{
						agent.objectSprite.useWithItem2[__instance.mainGUI.agent.isPlayer - 1] = false;
					
						if (__instance.mainGUI.targetItem.TargetObject(agent, ""))
							agent.objectSprite.SetHighlight("Target", __instance.mainGUI.agent);
					}
				}

				if (__instance.mainGUI.agent.hasRogueVision)
				{
					__instance.mainGUI.agent.brainUpdate.RogueVisionChecks();
				
					return false;
				}
			}
			else if (__instance.mainGUI.targetObject != null)
			{
				for (int k = 0; k < GC.itemList.Count; k++)
				{
					Item item = GC.itemList[k];

					if (__instance.mainGUI.targetObject.TargetObject(item, ""))
					{
						try
						{
							item.objectSprite.SetHighlight("Target", __instance.mainGUI.agent);
						}
						catch { }
					}
				}

				string targetType = __instance.mainGUI.agent.target.targetType;

				if (targetType == "LockpickDoor")
				{
					for (int l = 0; l < GC.objectRealList.Count; l++)
					{
						ObjectReal objectReal2 = GC.objectRealList[l];

						if (objectReal2.objectName == "Door" && ((Door)objectReal2).locked)
						{
							objectReal2.objectSprite.useWithItem2[__instance.mainGUI.agent.isPlayer - 1] = false;
						
							if (__instance.mainGUI.targetObject.TargetObject(objectReal2, ""))
								objectReal2.objectSprite.SetHighlight("Target", __instance.mainGUI.agent);
						}

						if (objectReal2.objectName == "Window")
						{
							objectReal2.objectSprite.useWithItem2[__instance.mainGUI.agent.isPlayer - 1] = false;
						
							if (__instance.mainGUI.targetObject.TargetObject(objectReal2, ""))
								objectReal2.objectSprite.SetHighlight("Target", __instance.mainGUI.agent);
						}
					}

					return false;
				}
				else if (targetType == "HackSomething")
				{
					for (int m = 0; m < GC.objectRealList.Count; m++)
					{
						ObjectReal objectReal3 = GC.objectRealList[m];

						if (objectReal3.hackable && objectReal3.functional)
						{
							objectReal3.objectSprite.useWithItem2[__instance.mainGUI.agent.isPlayer - 1] = false;

							if (__instance.mainGUI.targetObject.TargetObject(objectReal3, ""))
								objectReal3.objectSprite.SetHighlight("Target", __instance.mainGUI.agent);
						}
					}

					for (int n = 0; n < GC.agentList.Count; n++)
					{
						Agent agent2 = GC.agentList[n];
					
						if (((agent2.hackable && agent2.isPlayer == 0) || agent2.slaveOwners.Count > 0) && !agent2.dead)
						{
							agent2.objectSprite.useWithItem2[__instance.mainGUI.agent.isPlayer - 1] = false;
						
							if (__instance.mainGUI.targetObject.TargetObject(agent2, ""))
								agent2.objectSprite.SetHighlight("Target", __instance.mainGUI.agent);
						}
					}
				}
				else if (targetType == CJob.SafecrackSafe)
				{
					for (int l = 0; l < GC.objectRealList.Count; l++)
					{
						ObjectReal objectReal2 = GC.objectRealList[l];

						if (objectReal2.objectName == "Safe" && ((Safe)objectReal2).locked)
						{
							objectReal2.objectSprite.useWithItem2[__instance.mainGUI.agent.isPlayer - 1] = false;

							if (__instance.mainGUI.targetObject.TargetObject(objectReal2, ""))
								objectReal2.objectSprite.SetHighlight("Target", __instance.mainGUI.agent);
						}
					}

					return false;
				}
			}

			return false;
		}
	}
}