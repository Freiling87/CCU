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
		public static bool FixedUpdate_Prefix(LevelEditor __instance, GameObject ___helpScreen, GameObject ___initialSelection, GameObject ___workshopSubmission, GameObject ___longDescription)
		{
			if (!GC.loadCompleteReally || GC.loadLevel.restartingGame)
				return false;

			if (__instance.loadMenu.activeSelf || ___helpScreen.activeSelf || ___initialSelection.activeSelf || ___workshopSubmission.activeSelf || GC.menuGUI.onMenu || ___longDescription.activeSelf)
				return false;

			string currentInterface = __instance.currentInterface;
			bool ctrl = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
			bool shift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
			bool fieldFocused = __instance.InputFieldFocused();

			if (ctrl)
			{
				if (Input.GetKeyDown(KeyCode.A))
					LevelEditorUtilities.SelectAllToggle(__instance);
				if (Input.GetKeyDown(KeyCode.E))
				{
					if (currentInterface == "Objects" || currentInterface == "Agents" || currentInterface == "Floors")
						LevelEditorUtilities.Rotate(__instance, KeyCode.E);
					else if (currentInterface == "PatrolPoints")
						LevelEditorUtilities.IncrementPatrolPoint(__instance, KeyCode.E);
				}
				if (Input.GetKeyDown(KeyCode.O))
					__instance.PressedLoadChunksFile();
				if (Input.GetKeyDown(KeyCode.Q))
				{
					if (currentInterface == "Objects" || currentInterface == "Agents" || currentInterface == "Floors")
						LevelEditorUtilities.Rotate(__instance, KeyCode.Q);
					else if (currentInterface == "PatrolPoints")
						LevelEditorUtilities.IncrementPatrolPoint(__instance, KeyCode.Q);
				}
				if (Input.GetKeyDown(KeyCode.S))
					__instance.PressedSave();
			}
			else if (!fieldFocused)
			{
				if (Input.GetKey(KeyCode.UpArrow))
					LevelEditorUtilities.SetOrientation(__instance, KeyCode.UpArrow);
				if (Input.GetKey(KeyCode.DownArrow))
					LevelEditorUtilities.SetOrientation(__instance, KeyCode.DownArrow);
				if (Input.GetKey(KeyCode.LeftArrow))
					LevelEditorUtilities.SetOrientation(__instance, KeyCode.LeftArrow);
				if (Input.GetKey(KeyCode.RightArrow))
					LevelEditorUtilities.SetOrientation(__instance, KeyCode.RightArrow);
				if (Input.GetKey(KeyCode.A))
					__instance.ScrollW();
				if (Input.GetKey(KeyCode.D))
					__instance.ScrollE();
				if (Input.GetKey(KeyCode.E))
					__instance.ZoomIn();
				if (Input.GetKey(KeyCode.Q))
					__instance.ZoomOut();
				if (Input.GetKey(KeyCode.S))
					__instance.ScrollS();
				if (Input.GetKey(KeyCode.W))
					__instance.ScrollN();
			}

			#region (Ctrl + ) Number keys - Set Layer (& Open Selector)
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				__instance.PressedWallsButton();

				if (ctrl)
				{
					__instance.PressedLoadWalls();
					__instance.EnterDrawMode();
				}
			}
			if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				__instance.PressedFloorsButton();

				if (ctrl)
				{
					__instance.PressedLoadFloors();
					__instance.EnterDrawMode();
				}
			}
			if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				__instance.PressedFloors2Button();

				if (ctrl)
				{
					__instance.PressedLoadFloors();
					__instance.EnterDrawMode();
				}
			}
			if (Input.GetKeyDown(KeyCode.Alpha4))
			{
				__instance.PressedFloors3Button();

				if (ctrl)
				{
					__instance.PressedLoadFloors();
					__instance.EnterDrawMode();
				}
			}
			if (Input.GetKeyDown(KeyCode.Alpha5))
			{
				__instance.PressedObjectsButton();

				if (ctrl)
				{
					__instance.PressedLoadObjects();
					__instance.EnterDrawMode();
				}
			}
			if (Input.GetKeyDown(KeyCode.Alpha6))
			{
				__instance.PressedAgentsButton();

				if (ctrl)
				{
					__instance.PressedLoadAgents();
					__instance.EnterDrawMode();
				}
			}
			if (Input.GetKeyDown(KeyCode.Alpha7))
			{
				__instance.PressedItemsButton();

				if (ctrl)
				{
					__instance.PressedLoadItems();
					__instance.EnterDrawMode();
				}
			}
			if (Input.GetKeyDown(KeyCode.Alpha8))
			{ 
				__instance.PressedLightsButton();

				if (ctrl)
				{
					__instance.PressedLoadLights();
					__instance.EnterDrawMode();
				}
			}
			if (Input.GetKeyDown(KeyCode.Alpha9))
				__instance.PressedPatrolPointsButton();
			#endregion
			#region F5, F9, F12
			if (Input.GetKeyDown(KeyCode.F5))
			{
				if (__instance.ChunkNameUsed(__instance.chunkName))
					__instance.SaveChunkData(true, false);
				else
					__instance.PressedSave();
			}
			if (Input.GetKeyDown(KeyCode.F9))
			{
				//if (__instance.ChunkNameUsed(__instance.chunkName))
				//	//Quickload here
				// There is a long line of myButtonHelper passed through to the load function, I am not sure how to do it directly.
				//else
				__instance.PressedLoad();
			}
			if (Input.GetKeyDown(KeyCode.F12))
				__instance.PressedPlayButton();
			#endregion
			if (Input.GetKeyDown(KeyCode.Tab))
				LevelEditorUtilities.Tab(__instance, shift);
			#region Mouse tracking
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
			#endregion
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
