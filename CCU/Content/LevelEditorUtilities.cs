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
			{ "Agents", fieldsAgent },
			{ "Floors", fieldsFloor },
			{ "Floors2", fieldsFloor },
			{ "Floors3", fieldsFloor },
			{ "Items", fieldsItem },
			{ "Lights", fieldsLight },
			{ "Objects", fieldsObject },
			{ "PatrolPoints", fieldsPatrolPoint },
			{ "Walls" , fieldsWall },
		};

		public static InputField ActiveInputField(LevelEditor levelEditor)
		{
			foreach (InputField field in levelEditor.inputFieldList)
				if (field.isFocused)
					return field;

			return null;
		}
		public static InputField GetDirectionInputField(LevelEditor levelEditor)
		{
			string curInt = levelEditor.currentInterface;

			FieldInfo field =
				curInt == "Floors" ? AccessTools.Field(typeof(LevelEditor), "directionFloor") :
				curInt == "Agents" ? AccessTools.Field(typeof(LevelEditor), "directionAgent") :
				curInt == "Objects" ? AccessTools.Field(typeof(LevelEditor), "directionFloor") :
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
		public static void IncrementPatrolPoint(LevelEditor levelEditor, KeyCode input)
		{
			Core.LogMethodCall();
			logger.LogDebug("\tInput: " + input.ToString());

			FieldInfo inputField = AccessTools.Field(typeof(LevelEditor), "pointNumPatrolPoint");
			InputField pointNumPatrolPoint = (InputField)inputField.GetValue(levelEditor);

			if (pointNumPatrolPoint.text == "")
				pointNumPatrolPoint.text = "1";
			else
			{
				int curVal = int.Parse(pointNumPatrolPoint.text);
				pointNumPatrolPoint.text =
					input == KeyCode.E ? (Math.Max(99, curVal + 1).ToString()) :
					input == KeyCode.Q ? (Math.Min(1, curVal - 1).ToString()) :
					"1";
			}

			logger.LogDebug("\tNew value: " + pointNumPatrolPoint.text);

			levelEditor.SetPointNum();
		}
		public static void Rotate(LevelEditor levelEditor, KeyCode input)
		{
			Core.LogMethodCall();
			logger.LogDebug("\tinput: " + input.ToString());

			InputField inputField = GetDirectionInputField(levelEditor);
			string curDir = inputField.text;
			string newDir = "None";

			if (input == KeyCode.E)
				newDir =
					curDir == "N" ? "E" :
					curDir == "E" ? "S" :
					curDir == "S" ? "W" :
					curDir == "W" ? "N" :
					"E";
			else if (input == KeyCode.Q)
				newDir =
					curDir == "N" ? "W" :
					curDir == "W" ? "S" :
					curDir == "S" ? "E" :
					curDir == "E" ? "N" :
					"W";

			if (!(inputField is null))
				inputField.text = newDir;

			FieldInfo directionObject = AccessTools.Field(typeof(LevelEditor), "directionObject");
			directionObject.SetValue(levelEditor, newDir);

			foreach (LevelEditorTile levelEditorTile in levelEditor.selectedTiles)
				levelEditorTile.direction = newDir;
		}
		public static void SelectAllToggle(LevelEditor levelEditor)
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

			bool SelectingAll = false;

			foreach (LevelEditorTile levelEditorTile in list)
				if (!levelEditor.selectedTiles.Contains(levelEditorTile))
				{
					levelEditor.SelectTile(levelEditorTile, false);
					SelectingAll = true;
				}

			if (!SelectingAll)
				foreach (LevelEditorTile levelEditorTile in list)
				{
					levelEditor.DeselectTile(levelEditorTile, false);
				}
			// Instead of levelEditor.ClearSelections(false);

			levelEditor.UpdateInterface(false);
		}
		public static void SetOrientation(LevelEditor levelEditor, KeyCode input)
		{
			Core.LogMethodCall();
			logger.LogDebug("\tinput: " + input.ToString());

			InputField inputField = GetDirectionInputField(levelEditor);
			string curDir = inputField.text;
			string newDir =
				input == KeyCode.UpArrow	? "N" :
				input == KeyCode.DownArrow	? "S" :
				input == KeyCode.LeftArrow	? "W" :
				input == KeyCode.RightArrow ? "E" :
				"None"; // This line unreachable but prettier this way

			if (curDir == newDir)
				newDir = ""; // "" instead of "None" for levelEditorTile.direction

			logger.LogDebug("\tnewDir: " + newDir);

			if (!(inputField is null))
				inputField.text = newDir;

			FieldInfo directionObject = AccessTools.Field(typeof(LevelEditor), "directionObject");
			directionObject.SetValue(levelEditor, newDir);

			foreach (LevelEditorTile levelEditorTile in levelEditor.selectedTiles)
				levelEditorTile.direction = newDir;
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
	}
}
