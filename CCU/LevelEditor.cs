using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace CCU
{
	[HarmonyPatch(declaringType: typeof(LevelEditor))]
	public static class LevelEditor_Patches
	{
		public static GameController gc => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(methodName:"LevelEditor.FixedUpdate", argumentTypes: new Type[] { })]
        public static bool FixedUpdate_Prefix(LevelEditor __instance, GameObject ___helpScreen, GameObject ___initialSelection, GameObject ___workshopSubmission, GameObject ___longDescription)
        {
			if (!gc.loadCompleteReally || gc.loadLevel.restartingGame)
				return false;
			
			if (__instance.loadMenu.activeSelf || ___helpScreen.activeSelf || ___initialSelection.activeSelf || ___workshopSubmission.activeSelf || gc.menuGUI.onMenu || ___longDescription.activeSelf)
				return false;
			
			if (Input.GetKey(KeyCode.A) && !__instance.InputFieldFocused())
				__instance.ScrollW();
			
			if (Input.GetKey(KeyCode.D) && !__instance.InputFieldFocused())
				__instance.ScrollE();
			
			if (Input.GetKey(KeyCode.W) && !__instance.InputFieldFocused())
				__instance.ScrollN();
			
			if (Input.GetKey(KeyCode.S) && !__instance.InputFieldFocused())
				__instance.ScrollS();

			// Additions

			if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKey(KeyCode.Alpha1))
				__instance.PressedWallsButton();

			if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKey(KeyCode.Alpha2))
				__instance.PressedFloorsButton();
			
			if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKey(KeyCode.Alpha3))
				__instance.PressedFloors2Button();

			if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKey(KeyCode.Alpha4))
				__instance.PressedFloors3Button();

			if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKey(KeyCode.Alpha5))
				__instance.PressedObjectsButton();

			if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKey(KeyCode.Alpha6))
				__instance.PressedAgentsButton();

			if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKey(KeyCode.Alpha7))
				__instance.PressedItemsButton();

			if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKey(KeyCode.Alpha8))
				__instance.PressedLightsButton();

			if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKey(KeyCode.Alpha9))
				__instance.PressedPatrolPointsButton();

			if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKey(KeyCode.O))
				__instance.PressedLoadChunksFile();

			if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKey(KeyCode.S))
				__instance.PressedSave();

			if (Input.GetKey(KeyCode.F9))
			{
				ButtonHelper buttonHelper = new ButtonHelper();
				buttonHelper.chunkData.shape = __instance.chunkShape;

				__instance.LoadChunkFromFile(__instance.chunkName, buttonHelper);
			}

			if (Input.GetKey(KeyCode.F5))
				__instance.PressedSave2();

			//if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKey(KeyCode.Y))
			//	Redo();

			if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKey(KeyCode.A))
				SelectAll(__instance);

			if (Input.GetKey(KeyCode.Tab))
				Tab();

			//if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKey(KeyCode.Z))
			//	Undo();

			// End Additions

			Vector3 vector = gc.cameraScript.actualCamera.ScreenCamera.ScreenToWorldPoint(Input.mousePosition);
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
		private static void SelectAll(LevelEditor levelEditor)
		{
			List<LevelEditorTile> list = null;
			string layer = levelEditor.currentLayer;
			
			if (layer == "Walls")
				list = levelEditor.wallTiles;
			else if (layer == "Floors")
				list = levelEditor.floorTiles;
			else if (layer == "Floors2")
				list = levelEditor.floorTiles2;
			else if (layer == "Floors3")
				list = levelEditor.floorTiles3;
			else if (layer == "Objects")
				list = levelEditor.objectTiles;
			else if (layer == "Items")
				list = levelEditor.itemTiles;
			else if (layer == "Agents")
				list = levelEditor.agentTiles;
			else if (layer == "Lights")
				list = levelEditor.lightTiles;
			else if (layer == "PatrolPoints")
				list = levelEditor.patrolPointTiles;
			else if (layer == "Level")
				list = levelEditor.chunkTiles;

			foreach (LevelEditorTile levelEditorTile in list)
			{
				if (!levelEditor.selectedTiles.Contains(levelEditorTile) && !levelEditor.deselecting)
					levelEditor.SelectTile(levelEditorTile, false);
				else if (levelEditor.deselecting)
					levelEditor.DeselectTile(levelEditorTile, false);
			}

			levelEditor.UpdateInterface(false);
		}
		private static void Tab()
		{
			// LevelEditor.inputFieldList.isFocused
		}
    }
}
