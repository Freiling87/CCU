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
		public static readonly ManualLogSource logger = CCULogger.GetLogger();
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
		public static List<InputField> fieldsWall;
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
		public static void Tab(LevelEditor levelEditor, bool reverse)
		{
			List<InputField> fieldList = fieldLists[levelEditor.currentLayer];
			InputField oldFocus = ActiveInputField(levelEditor);

			if (oldFocus is null)
				fieldList[0].ActivateInputField();
			else
			{
				if (!reverse)
					fieldList[fieldList.IndexOf(oldFocus) + 1].ActivateInputField();
				else
					fieldList[fieldList.IndexOf(oldFocus) - 1].ActivateInputField();
			}
		}
	}
}
