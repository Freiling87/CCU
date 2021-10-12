using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using BepInEx.Logging;
using System.Reflection;
using CCU.Content;

namespace CCU.Patches.Interface
{
	[HarmonyPatch(declaringType: typeof(LevelEditor))]
	public static class P_LevelEditor
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(methodName: "FixedUpdate", argumentTypes: new Type[0] { })]
		public static bool FixedUpdate_Prefix(LevelEditor __instance, GameObject ___helpScreen, GameObject ___initialSelection, GameObject ___workshopSubmission, GameObject ___longDescription, InputField ___directionObject, InputField ___pointNumPatrolPoint)
		{
			if (!GC.loadCompleteReally || GC.loadLevel.restartingGame)
				return false;

			if (__instance.loadMenu.activeSelf || ___helpScreen.activeSelf || ___initialSelection.activeSelf || ___workshopSubmission.activeSelf || GC.menuGUI.onMenu || ___longDescription.activeSelf)
				return false;

			if (!(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKey(KeyCode.A) && !__instance.InputFieldFocused())
				__instance.ScrollW();
			if (!(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKey(KeyCode.D) && !__instance.InputFieldFocused())
				__instance.ScrollE();
			if (!(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKey(KeyCode.W) && !__instance.InputFieldFocused())
				__instance.ScrollN();
			if (!(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKey(KeyCode.S) && !__instance.InputFieldFocused())
				__instance.ScrollS();

			#region Arrow Keys - Object Orientation Toggle
			if (Input.GetKey(KeyCode.UpArrow))
			{
				if (___directionObject.text == "N")
					___directionObject.text = "None";
				else
					___directionObject.text = "N";
			}
			if (Input.GetKey(KeyCode.DownArrow))
			{
				if (___directionObject.text == "S")
					___directionObject.text = "None";
				else
					___directionObject.text = "S";
			}
			if (Input.GetKey(KeyCode.RightArrow))
			{
				if (___directionObject.text == "E")
					___directionObject.text = "None";
				else
					___directionObject.text = "E";
			}
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				if (___directionObject.text == "W")
					___directionObject.text = "None";
				else
					___directionObject.text = "W";
			}
			#endregion
			#region Q & E - Object Orientation Rotation / Patrol Point increment
			if (Input.GetKey(KeyCode.Q))
			{
				if (__instance.currentLayer == "Agents" || __instance.currentLayer == "Objects")
				{
					if (___directionObject.text == "N")
						___directionObject.text = "W";
					else if (___directionObject.text == "W")
						___directionObject.text = "S";
					else if (___directionObject.text == "S")
						___directionObject.text = "E";
					else if (___directionObject.text == "E")
						___directionObject.text = "N";
				}
				else if (__instance.currentLayer == "PatrolPoints")
					___pointNumPatrolPoint.text = (int.Parse(___pointNumPatrolPoint.text) - 1).ToString();
			}
			if (Input.GetKey(KeyCode.E))
			{
				if (__instance.currentLayer == "Agents" || __instance.currentLayer == "Objects")
				{
					if (___directionObject.text == "N")
						___directionObject.text = "E";
					else if (___directionObject.text == "E")
						___directionObject.text = "S";
					else if (___directionObject.text == "S")
						___directionObject.text = "W";
					else if (___directionObject.text == "W")
						___directionObject.text = "N";
				}
				else if (__instance.currentLayer == "PatrolPoints")
					___pointNumPatrolPoint.text = (int.Parse(___pointNumPatrolPoint.text) + 1).ToString();
			}
			#endregion
			#region Number keys - Layer / Ctrl + Number keys - Layer + Selector menu
			if (Input.GetKey(KeyCode.Alpha1))
				__instance.PressedWallsButton();
			if (Input.GetKey(KeyCode.Alpha1) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
			{
				__instance.PressedWallsButton();
				__instance.PressedLoadWalls();
			}

			if (Input.GetKey(KeyCode.Alpha2))
				__instance.PressedFloorsButton();
			if (Input.GetKey(KeyCode.Alpha2) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
			{
				__instance.PressedFloorsButton();
				__instance.PressedLoadFloors();
			}

			if (Input.GetKey(KeyCode.Alpha3))
				__instance.PressedFloors2Button();
			if (Input.GetKey(KeyCode.Alpha3) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
			{
				__instance.PressedFloors2Button();
				__instance.PressedLoadFloors();
			}

			if (Input.GetKey(KeyCode.Alpha4))
				__instance.PressedFloors3Button();
			if (Input.GetKey(KeyCode.Alpha4) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
			{
				__instance.PressedFloors3Button();
				__instance.PressedLoadFloors();
			}

			if (Input.GetKey(KeyCode.Alpha5))
				__instance.PressedObjectsButton();
			if (Input.GetKey(KeyCode.Alpha5) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
			{
				__instance.PressedObjectsButton();
				__instance.PressedLoadObjects();
			}

			if (Input.GetKey(KeyCode.Alpha6))
				__instance.PressedAgentsButton();
			if (Input.GetKey(KeyCode.Alpha6) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
			{
				__instance.PressedAgentsButton();
				__instance.PressedLoadAgents();
			}

			if (Input.GetKey(KeyCode.Alpha7))
				__instance.PressedItemsButton();
			if (Input.GetKey(KeyCode.Alpha7) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
			{
				__instance.PressedItemsButton();
				__instance.PressedLoadItems();
			}

			if (Input.GetKey(KeyCode.Alpha8))
				__instance.PressedLightsButton();
			if (Input.GetKey(KeyCode.Alpha8) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
			{
				__instance.PressedLightsButton();
				__instance.PressedLoadLights();
			}

			if (Input.GetKey(KeyCode.Alpha9))
				__instance.PressedPatrolPointsButton();
			#endregion
			#region Saving & Loading
			if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKey(KeyCode.O))
				__instance.PressedLoadChunksFile();
			if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKey(KeyCode.S))
				__instance.PressedSave();
			if (Input.GetKey(KeyCode.F5))
			{
				if (__instance.ChunkNameUsed(__instance.chunkName))
					__instance.SaveChunkData(true, false);
				else
					__instance.PressedSave();
			}
			if (Input.GetKey(KeyCode.F9))
			{
				//if (__instance.ChunkNameUsed(__instance.chunkName))
				//	//Quickload here
				// There is a long line of myButtonHelper passed through to the load function, I am not sure how to do it directly.
				//else
				__instance.PressedLoad();
			}
			#endregion
			#region Misc.
			//if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKey(KeyCode.Y))
			//	Redo();
			if (Input.GetKey(KeyCode.A) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
				LevelEditorUtilities.SelectAllToggle(__instance);
			if (Input.GetKey(KeyCode.Tab))
				LevelEditorUtilities.Tab(__instance, false);
			if (Input.GetKey(KeyCode.Tab) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
				LevelEditorUtilities.Tab(__instance, true);
			//if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKey(KeyCode.Z))
			//	Undo();
			#endregion

			Vector3 vector = GC.cameraScript.actualCamera.ScreenCamera.ScreenToWorldPoint(Input.mousePosition);
			int num;
			int num2;

			if (__instance.currentLayer == "Walls" || __instance.currentLayer == "Floors" || __instance.currentLayer == "Floors2" || __instance.currentLayer == "Floors3")
			{
				num = (int)((vector.x + 0.32f) / 0.64f);
				num2 = (int)((vector.y + 0.32f) / 0.64f);
			}
			else
			{
				num = (int)((vector.x + 0.16f) / 0.32f);
				num2 = (int)((vector.y + 0.16f) / 0.32f);
			}

			if (num != __instance.previousMouseTileX || num2 != __instance.previousMouseTileY || (vector.x >= -0.64f && __instance.previousMousePosX < -0.64f) || (vector.y >= -0.64f && __instance.previousMousePosY < -0.64f))
			{
				__instance.previousMouseTileX = num;
				__instance.previousMouseTileY = num2;
				__instance.previousMousePosX = vector.x;
				__instance.previousMousePosY = vector.y;
				__instance.EnteredTile();
			}

			return false;
		}

		[HarmonyPostfix, HarmonyPatch(methodName: "Start", argumentTypes: new Type[0] { })]
		public static void Start_Postfix(LevelEditor __instance, GameObject ___wallInterface, GameObject ___floorInterface, GameObject ___itemInterface, GameObject ___objectInterface, GameObject ___agentInterface, GameObject ___lightInterface, GameObject ___patrolPointInterface)
		{
			logger.Log(LogLevel.Info, "Loading Input fields...");

			InputField[] inputFields_Wall = ___wallInterface.transform.GetComponentsInChildren<InputField>(true);

			foreach (InputField field in inputFields_Wall)
				logger.Log(LogLevel.Info, field.name);

			LevelEditorUtilities.fieldsItem = inputFields_Wall.OrderBy(i => i.transform.position.x).OrderBy(i => i.transform.position.y).ToList();

			// Need a couple more filters here, see comments from BT 
		}

	}
}
