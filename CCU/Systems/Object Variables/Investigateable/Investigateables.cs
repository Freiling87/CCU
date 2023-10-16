using BepInEx.Logging;
using BTHarmonyUtils;
using BTHarmonyUtils.TranspilerUtils;
using BunnyLibs;
using HarmonyLib;
using JetBrains.Annotations;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UI;

namespace CCU.Systems.Investigateables
{
	public static class Investigateables
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static string InvestigateableStringPrefix = "investigateable-message:::";

		// There is currently no overlap between Investigateables and Containers. If you change that, do it carefully.
		public static List<string> InvestigateableObjects_Slot1 = new List<string>()
		{
			VanillaObjects.Altar,
			VanillaObjects.ArcadeGame,
			VanillaObjects.Boulder,
			VanillaObjects.Computer,
			// VanillaObjects.Counter,		// Really needs a sprite change
			VanillaObjects.Gravestone,
			VanillaObjects.Jukebox,
			// VanillaObjects.MovieScreen, // Didn't work yet, see notes
			VanillaObjects.Podium,
			VanillaObjects.Speaker,
			VanillaObjects.Television,
			VanillaObjects.Window,
		};
		public static List<string> InvestigateableObjects_Slot2 = new List<string>()
		{
			//VanillaObjects.Door,
			//VanillaObjects.Shelf,
		};

		public static string MagicObjectName(string originalName) =>
			IsInvestigateable(originalName)
				? VanillaObjects.Sign
				: originalName;

		public static bool IsInvestigateable(PlayfieldObject playfieldObject) =>
			IsInvestigateable(playfieldObject.objectName);
		public static bool IsInvestigateable(string name) =>
			InvestigateableObjects_Slot1.Contains(name) ||
			InvestigateableObjects_Slot2.Contains(name);

		public static bool IsInvestigationString(string name) =>
			name?.Contains(InvestigateableStringPrefix) ?? false;

		public static string PlayerDisplayInvestigationText(string vanilla) =>
			vanilla?.Replace(InvestigateableStringPrefix, "") ?? "";

		public static List<InvSlot> FilteredSlots(InvDatabase invDatabase) =>
			invDatabase.agent.mainGUI.invInterface.Slots
				.Where(slot => !IsInvestigationString(slot.itemNameText.text)).ToList();

		public static InvDatabase InvDatabaseWithoutInvText(InvDatabase invDatabase)
		{
			invDatabase.InvItemList = FilteredInvItemList(invDatabase.InvItemList);
			return invDatabase;
		}

		public static List<InvItem> FilteredInvItemList(List<InvItem> invItemList) =>
			invItemList.Where(invItem =>
				!IsInvestigationString(invItem.invItemName) &&
				IsActualItem(invItem)
			).ToList();

		public static bool IsActualItem(InvItem invItem) =>
			!invItem.invItemName?.Contains("E_") ?? false;

		[RLSetup]
		public static void Setup()
		{
			string t = NameTypes.Interface;
			RogueLibs.CreateCustomName(CButtonText.Investigate, t, new CustomNameInfo(CButtonText.Investigate));

			RogueInteractions.CreateProvider(h =>
			{
				if (!h.Agent.interactionHelper.interactingFar &&
					IsInvestigateable(h.Object) &&
					IsInvestigationString(h.Object.extraVarString))
				{
					string text = PlayerDisplayInvestigationText(h.Object.extraVarString);

					if (text.Replace(Environment.NewLine, "").Replace(" ", "").Length < 2)
						return;

					if (h.Object is Computer computer)
						h.RemoveButton(VButtonText.ReadEmail);

					h.AddImplicitButton(CButtonText.Investigate, m =>
					{
						m.Object.ShowBigImage(text, "", null);
					});
				}
			});
		}
	}

	[HarmonyPatch(typeof(BasicObject))]
	public static class P_BasicObject
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(BasicObject.Spawn), new[] { typeof(SpawnerBasic), typeof(string), typeof(Vector2), typeof(Vector2), typeof(Chunk) })]
		public static IEnumerable<CodeInstruction> Spawn_SetupReadables(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo magicObjectName = AccessTools.DeclaredMethod(typeof(Investigateables), nameof(Investigateables.MagicObjectName));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_2),
					new CodeInstruction(OpCodes.Ldstr, "Sign"),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_2),
					new CodeInstruction(OpCodes.Call, magicObjectName),
					new CodeInstruction(OpCodes.Ldstr, "Sign"),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}

	[HarmonyPatch(typeof(InvDatabase))]
	public static class P_InvDatabase
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(InvDatabase.FillChest), argumentTypes: new[] { typeof(bool) })]
		private static IEnumerable<CodeInstruction> FillChest_FilterNotes_EVS(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo extraVarString = AccessTools.DeclaredField(typeof(PlayfieldObject), nameof(PlayfieldObject.extraVarString));
			MethodInfo magicVarString = AccessTools.DeclaredMethod(typeof(P_InvDatabase), nameof(P_InvDatabase.MagicVarString));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 5,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, extraVarString),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, magicVarString),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		public static string MagicVarString(string vanilla) =>
			Investigateables.IsInvestigationString(vanilla)
				? ""
				: vanilla;

		[HarmonyTranspiler, HarmonyPatch(nameof(InvDatabase.TakeAll))]
		private static IEnumerable<CodeInstruction> TakeAll_ExcludeNotes(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo slots = AccessTools.DeclaredField(typeof(InvInterface), nameof(InvInterface.Slots));
			MethodInfo slotsFiltered = AccessTools.DeclaredMethod(typeof(Investigateables), nameof(Investigateables.FilteredSlots));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Ldfld, slots),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Call, slotsFiltered),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}

	[HarmonyPatch(typeof(LevelEditor))]
	public static class P_LevelEditor
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(LevelEditor.OpenLongDescription))]
		public static IEnumerable<CodeInstruction> OpenLongDescription_RemoveSpecialStrings(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo extraVarStringObject = AccessTools.DeclaredField(typeof(LevelEditor), "extraVarStringObject");
			MethodInfo getText = AccessTools.DeclaredMethod(typeof(InputField), "get_text");
			MethodInfo LongDescriptionWithoutSpecialStrings = AccessTools.DeclaredMethod(typeof(P_LevelEditor), nameof(P_LevelEditor.LongDescriptionWithoutSpecialStrings));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, extraVarStringObject),
					new CodeInstruction(OpCodes.Callvirt, getText),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, LongDescriptionWithoutSpecialStrings),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		public static string LongDescriptionWithoutSpecialStrings(string vanilla)
		{
			if (vanilla is null || vanilla == "")
				return vanilla;

			if (Investigateables.IsInvestigationString(vanilla))
				vanilla = Investigateables.PlayerDisplayInvestigationText(vanilla);

			return vanilla;
		}

		[HarmonyTranspiler, HarmonyPatch(nameof(LevelEditor.PressedLoadExtraVarStringList), new Type[0] { })]
		public static IEnumerable<CodeInstruction> PressedLoadExtraVarStringList_EditTextBox(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo magicObjectName = AccessTools.DeclaredMethod(typeof(Investigateables), nameof(Investigateables.MagicObjectName));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_1),
					new CodeInstruction(OpCodes.Ldstr, VanillaObjects.Sign),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_1),					//	Object real name
					new CodeInstruction(OpCodes.Call, magicObjectName),		//	"Sign" if investigateable, or real name if not
					new CodeInstruction(OpCodes.Ldstr, VanillaObjects.Sign),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		[HarmonyTranspiler, HarmonyPatch(nameof(LevelEditor.PressedScrollingMenuButton), new[] { typeof(ButtonHelper) })]
		public static IEnumerable<CodeInstruction> PressedScrollingMenuButton_TextBoxValid(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo scrollingButtonType = AccessTools.DeclaredField(typeof(ButtonHelper), nameof(ButtonHelper.scrollingButtonType));
			MethodInfo magicObjectName = AccessTools.DeclaredMethod(typeof(Investigateables), nameof(Investigateables.MagicObjectName));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 2,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_1),
					new CodeInstruction(OpCodes.Ldfld, scrollingButtonType),
					new CodeInstruction(OpCodes.Ldstr, VanillaObjects.Sign),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_1),
					new CodeInstruction(OpCodes.Ldfld, scrollingButtonType),
					new CodeInstruction(OpCodes.Call, magicObjectName),
					new CodeInstruction(OpCodes.Ldstr, VanillaObjects.Sign)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		[HarmonyPostfix, HarmonyPatch(nameof(LevelEditor.SetExtraVarString))]
		public static void SetExtraVarString_FormatInvestigateableText(LevelEditor __instance, InputField ___tileNameObject, InputField ___extraVarStringObject)
		{
			if (__instance.currentInterface == "Objects")
				if (Investigateables.IsInvestigateable(___tileNameObject.text) &&
					___extraVarStringObject.text.Length > 0 &&
					!Investigateables.IsInvestigationString(___extraVarStringObject.text))
					___extraVarStringObject.text = Investigateables.InvestigateableStringPrefix + ___extraVarStringObject.text;
		}

		[HarmonyTranspiler, HarmonyPatch(nameof(LevelEditor.UpdateInterface), new[] { typeof(bool) })]
		private static IEnumerable<CodeInstruction> UpdateInterface_ShowTextBoxForInvestigateables(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo magicObjectName = AccessTools.DeclaredMethod(typeof(Investigateables), nameof(Investigateables.MagicObjectName));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_S, 34),
					new CodeInstruction(OpCodes.Ldstr, VanillaObjects.Sign),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_S, 34),				//	Object real name
					new CodeInstruction(OpCodes.Call, magicObjectName),		//	"Sign" if investigateable, or real name if not
					new CodeInstruction(OpCodes.Ldstr, VanillaObjects.Sign),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}

	[HarmonyPatch(typeof(ObjectReal))]
	static class P_ObjectReal_DestroyMe2
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTargetMethod, UsedImplicitly]
		private static MethodInfo Find_MoveNext_MethodInfo() =>
			PatcherUtils.FindIEnumeratorMoveNext(AccessTools.Method(typeof(ObjectReal), "DestroyMe2"));

		// Excludes E_ items and investigateable strings.
		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> DestroyMe2_DontSpillNote(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo invItemList = AccessTools.DeclaredField(typeof(InvDatabase), nameof(InvDatabase.InvItemList));
			MethodInfo filteredList = AccessTools.DeclaredMethod(typeof(Investigateables), nameof(Investigateables.FilteredInvItemList));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, invItemList),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, filteredList)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}

	[HarmonyPatch(typeof(WorldSpaceGUI))]
	public static class P_WorldSpaceGUI
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		// I think this was for when I had some overlap between Investigateables and Containers. I culled those lists of any intersects to avoid special issues, so I'm not 100% if this patch even works if reactivated.
		//[HarmonyTranspiler, HarmonyPatch(nameof(WorldSpaceGUI.ShowChest), new[] { typeof(GameObject), typeof(InvDatabase), typeof(Agent) })]
		private static IEnumerable<CodeInstruction> ShowChest_FilterNotes(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo chestDatabase = AccessTools.DeclaredField(typeof(InvInterface), nameof(InvInterface.chestDatabase));
			MethodInfo filteredInvDatabase = AccessTools.DeclaredMethod(typeof(Investigateables), nameof(Investigateables.InvDatabaseWithoutInvText));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Stfld, chestDatabase),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, filteredInvDatabase)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}
}