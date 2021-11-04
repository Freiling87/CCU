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

namespace CCU.Content
{
	public static class LevelEditorUtilities
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		// LevelEditor.inputFieldList.isFocused
		// InputField.ActivateInputField() & possibly DeadctivateInputField() at the end

		// levelEditor.floors2Button.transform.Find("ButtonEdges").GetComponent<Image>().color = Color.white;
		// White is for currently click-activated, but might use another color to show which is tab-active, pending player confirmation

		// __instance.inputFieldList

		// UnityEngine.UI.InputField.ActivateInputField()

		public static List<InputField> fieldsAgent;
		public static List<InputField> fieldsFloor;
		public static List<InputField> fieldsItem;
		public static List<InputField> fieldsLight;
		public static List<InputField> fieldsObject;
		public static List<InputField> fieldsPatrolPoint;
		public static List<InputField> fieldsWall = new List<InputField>()
		{
			
		};
		public static Dictionary<string, List<InputField>> fieldLists = new Dictionary<string, List<InputField>>()
		{
			// TODO: Double-check these strings
			{ LEInterfaces_Agents, fieldsAgent },
			{ LEInterfaces_Floors, fieldsFloor },
			{ LEInterfaces_Floors2, fieldsFloor },
			{ LEInterfaces_Floors3, fieldsFloor },
			{ LEInterfaces_Items, fieldsItem },
			{ LEInterfaces_Lights, fieldsLight },
			{ LEInterfaces_Objects, fieldsObject },
			{ LEInterfaces_PatrolPoints, fieldsPatrolPoint },
			{ LEInterfaces_Walls, fieldsWall },
		};
		public const string
			LEInterfaces_Agents = "Agents",
			LEInterfaces_Floors = "Floors",
			LEInterfaces_Floors2 = "Floors2",
			LEInterfaces_Floors3 = "Floors3",
			LEInterfaces_Items = "Items",
			LEInterfaces_Level = "Level",
			LEInterfaces_Lights = "Lights",
			LEInterfaces_Objects = "Objects",
			LEInterfaces_PatrolPoints = "PatrolPoints",
			LEInterfaces_Walls = "Walls"
			;

		public static InputField ActiveInputField(LevelEditor levelEditor)
		{
			foreach (InputField field in levelEditor.inputFieldList)
				if (field.isFocused)
					return field;

			return null;
		}
		public static InputField DirectionInputField(LevelEditor levelEditor)
		{
			string curInt = levelEditor.currentInterface;

			FieldInfo field =
				curInt == LEInterfaces_Floors ? AccessTools.Field(typeof(LevelEditor), "directionFloor") :
				curInt == LEInterfaces_Agents ? AccessTools.Field(typeof(LevelEditor), "directionAgent") :
				curInt == LEInterfaces_Objects ? AccessTools.Field(typeof(LevelEditor), "directionFloor") :
				null;

			try
			{
				return (InputField)field.GetValue(levelEditor);
			}
			catch
			{
				return null;
			}
		}
		public static void IncrementPatrolPointId(LevelEditor levelEditor, KeyCode input)
		{
			Core.LogMethodCall();
			logger.LogDebug("\tInput: " + input.ToString());

			FieldInfo inputField = AccessTools.Field(typeof(LevelEditor), "pointNumPatrolPoint");
			InputField inputFieldField = (InputField)inputField.GetValue(levelEditor);

			if (inputFieldField.text == "")
				inputFieldField.text = "1";
			else
			{
				int curVal = int.Parse(inputFieldField.text);
				inputFieldField.text =
					input == KeyCode.E ? (Math.Min(99, curVal + 1).ToString()) :
					input == KeyCode.Q ? (Math.Max(1, curVal - 1).ToString()) :
					"1";
			}

			logger.LogDebug("\tNew value: " + inputFieldField.text);

			levelEditor.SetPointNum();
		}
		public static void PressScrollingMenuButton(string buttonText)
		{
			foreach (ButtonHelper buttonHelper in GC.buttonHelpersList)
				if (buttonHelper.scrollingButtonType == buttonText)
				{
					buttonHelper.PressedScrollingMenuButton();
					break;
				}
		}
		public static void SetDirection(LevelEditor levelEditor, KeyCode input)
		{
			Core.LogMethodCall();

			InputField inputField = DirectionInputField(levelEditor);
			string curDir = inputField.text;

			string newDir =
				input == KeyCode.UpArrow	? "North" :
				input == KeyCode.DownArrow	? "South" :
				input == KeyCode.LeftArrow	? "West" :
				input == KeyCode.RightArrow ? "East" :
				input == KeyCode.E ? (
					curDir == "N" ? "East" :
					curDir == "E" ? "South" :
					curDir == "S" ? "West" :
					curDir == "W" ? "North" :
					"East") :
				input == KeyCode.Q ? (
					curDir == "N" ? "West" :
					curDir == "W" ? "South" :
					curDir == "S" ? "East" :
					curDir == "E" ? "North" :
					"West") :
				"Error: SOMETHING IS FUCKED UP AND I THINK IT'S YOU";

			if ((curDir == "N" && newDir == "North") ||
				(curDir == "S" && newDir == "South") ||
				(curDir == "E" && newDir == "East") ||
				(curDir == "W" && newDir == "West"))
				newDir = "";

			levelEditor.PressedLoadDirectionList();

			foreach (ButtonHelper buttonHelper in GC.buttonHelpersList)
				if (buttonHelper.scrollingButtonType == newDir)
				{
					buttonHelper.PressedScrollingMenuButton();
					break;
				}
		}
		public static void Tab(LevelEditor levelEditor, bool reverse)
		{
			Core.LogMethodCall();

			List<InputField> fieldList = fieldLists[levelEditor.currentLayer]; // Can't use yet: 1. Your own lists aren't filled out yet; 2. Not sure how to access the stuff that goes on those lists; 3. Need to test with vanilla stuff anyway.
			InputField oldFocus;

			try
			{
				oldFocus = ActiveInputField(levelEditor); // May cause NullRef
				logger.LogDebug("Active Field: " + oldFocus.name);

				if (!reverse)
					levelEditor.inputFieldList[levelEditor.inputFieldList.IndexOf(oldFocus) + 1].ActivateInputField();
				else
					levelEditor.inputFieldList[levelEditor.inputFieldList.IndexOf(oldFocus) - 1].ActivateInputField();
			}
			catch
			{
				levelEditor.inputFieldList[0].ActivateInputField();
			}

			logger.LogDebug("ActiveInputField: " + ActiveInputField(levelEditor));
		}
		public static void ToggleSelectAll(LevelEditor levelEditor, bool limitToLayer)
		{
			Core.LogMethodCall();

			string layer = levelEditor.currentLayer;
			List<LevelEditorTile> tilesThisLayer =
				layer == LEInterfaces_Walls ? levelEditor.wallTiles :
				layer == LEInterfaces_Floors ? levelEditor.floorTiles :
				layer == LEInterfaces_Floors2 ? levelEditor.floorTiles2 :
				layer == LEInterfaces_Floors3 ? levelEditor.floorTiles3 :
				layer == LEInterfaces_Objects ? levelEditor.objectTiles :
				layer == LEInterfaces_Items ? levelEditor.itemTiles :
				layer == LEInterfaces_Agents ? levelEditor.agentTiles :
				layer == LEInterfaces_Lights ? levelEditor.lightTiles :
				layer == LEInterfaces_PatrolPoints ? levelEditor.patrolPointTiles :
				layer == LEInterfaces_Level ? levelEditor.chunkTiles :
				null;

			logger.LogDebug("\tTile list count: " + tilesThisLayer.Count());
			logger.LogDebug("\tSelected count: " + levelEditor.selectedTiles.Count());
			foreach (LevelEditorTile tile in levelEditor.selectedTiles)
				logger.LogDebug("\t\tIndex " + levelEditor.selectedTiles.IndexOf(tile) + ": " + tile.tileName);

			int oldSelects = levelEditor.selectedTiles.Count();

			foreach (LevelEditorTile levelEditorTile in tilesThisLayer)
				if (!levelEditor.selectedTiles.Contains(levelEditorTile))
					levelEditor.SelectTile(levelEditorTile, false);

			// tilesThisLayer.Count() always seems to be 1024, so we can't use it to see if Select All is already active
			if (levelEditor.selectedTiles.Count() == oldSelects)
				levelEditor.ClearSelections(true);

			levelEditor.UpdateInterface(false);
		}
		public static void ZoomInFully(LevelEditor levelEditor) =>
			GC.cameraScript.zoomLevel = 1f;
		public static void ZoomOutFully(LevelEditor levelEditor)
		{
			if (levelEditor.levelMode)
				GC.cameraScript.zoomLevel = 0.1f;
			else if (!levelEditor.levelMode)
				GC.cameraScript.zoomLevel = 0.4f;
		}
	}
}
