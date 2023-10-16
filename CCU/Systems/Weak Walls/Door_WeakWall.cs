using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace CCU.Weak_Walls
{
	internal enum DoorExtraVars
	{
		// Maybe this order, not sure.
		//list.Add("Normal");
		//list.Add("LockedMaterialRandom");
		//list.Add("LockedNormal");
		//list.Add("LockedSteel");
		//list.Add("DoorNoEntry");
		//list.Add("LockDetonatorNoRandom");
		//list.Add("DetonatorRandom");
		//list.Add("LockDetonator");
		//list.Add("Arena");
		//list.Add("PanicRoom");
	}

	public class WeakWalls
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		internal static GameController GC => GameController.gameController;

		[RLSetup]
		private static void Setup()
		{
			SetupNames();
		}
		private static void SetupNames()
		{
			RogueLibs.CreateCustomName(WeakWallDoorVar, NameTypes.Interface, new CustomNameInfo
			{
				[LanguageCode.English] = "Weak Wall",
			});
		}

		public const string
			WeakWallDoorVar = "WeakWallDoorVar",

			z = "";
	}

	[HarmonyPatch(typeof(Door))]
	static class P_Door_WeakWalls
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		internal static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(nameof(Door.SpawnActions))]
		private static bool CheckSpawnActions(Door __instance, int myExtraVar)
		{
			logger.LogDebug("CheckSpawnActions: " + __instance.name + " / " + myExtraVar);
			return true;
		}
	}

	[HarmonyPatch(typeof(LevelEditor))]
	static class P_LevelEditor_WeakWall
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		internal static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(nameof(LevelEditor.FillTile))]
		private static void LogTile(LevelEditorTile myTile)
		{
			logger.LogDebug("LogTile: " + myTile.tileName + " / " + myTile.extraVar);
		}

		[HarmonyPrefix, HarmonyPatch(nameof(LevelEditor.PressedScrollingMenuButton))]
		private static bool InterceptExtraVar(LevelEditor __instance, ButtonHelper myButtonHelper)
		{
			if (__instance.scrollingMenuType != "LoadExtraVarsDoor" || myButtonHelper.scrollingButtonType != WeakWalls.WeakWallDoorVar)
				return true;

			logger.LogDebug("Caught ExtraVar Press: ");

			InputField extraVarObject = (InputField)AccessTools.DeclaredField(typeof(LevelEditor), "extraVarObject").GetValue(__instance);
			extraVarObject.SetTextWithoutNotify("11");
			//extraVarObject.text == "11"; // Inaccessible
			__instance.SetNameText(extraVarObject, WeakWalls.WeakWallDoorVar, "Interface");
			__instance.DeactivateLoadMenu();
			__instance.scrollerControllerLoad.myScroller.RefreshActiveCellViews();
			return false;
		}

		[HarmonyTranspiler, HarmonyPatch(nameof(LevelEditor.CreateExtraVarsDoorList))]
		private static IEnumerable<CodeInstruction> AddToExtraVarsList(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo customMethod = AccessTools.DeclaredMethod(typeof(P_LevelEditor_WeakWall), nameof(P_LevelEditor_WeakWall.AdjustExtraVarList));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				pullNextLabelUp: false,
				expectedMatches: 1,
				prefixInstructions: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldstr, "PanicRoom"),
					new CodeInstruction(OpCodes.Callvirt),
				},
				insertInstructions: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_0),
					new CodeInstruction(OpCodes.Call, customMethod),
					new CodeInstruction(OpCodes.Stloc_0),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		private static List<string> AdjustExtraVarList(List<string> vanilla) =>
			vanilla.AddItem<string>(WeakWalls.WeakWallDoorVar).ToList();
	}
}
