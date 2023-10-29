using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU
{
	public abstract class T_CCU : CustomTrait
	{
		public static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		//	IRefreshPerLevel
		public bool AlwaysApply => false;

		public static TraitBuilder PostProcess_DesignerTrait
		{
			set
			{
				value.Unlock.Unlock.cantLose = true;
				value.Unlock.Unlock.cantSwap = true;
				value.Unlock.Unlock.upgrade = null;
			}
		}

		public static List<Trait> DesignerTraitList(List<Trait> original) =>
			original.Where(t => IsDesignerTrait(t)).ToList();
		public static List<Unlock> DesignerUnlockList(List<Unlock> original) =>
			original.Where(u => IsDesignerUnlock(u)).ToList();

		public static List<Trait> PlayerTraitList(List<Trait> original) =>
			original.Where(t => IsPlayerTrait(t)).ToList();
		public static List<Unlock> PlayerUnlockList(List<Unlock> original) =>
			original.Where(u => IsPlayerUnlock(u)).ToList();

		public static List<Unlock> SortUnlocksByValue(List<Unlock> original) =>
			original.OrderBy(u => u.cost3).ToList();
		public static List<Unlock> SortUnlocksByName(List<Unlock> original) =>
			original.OrderBy(u => GC.nameDB.GetName(u.unlockName, "StatusEffect")).ToList();

		public static List<Unlock> VanillaTraitList(List<Unlock> original) =>
			original.Where(u => !(u.GetHook() is TU_DesignerUnlock)).ToList();

		public static bool IsDesignerTrait(Trait trait) =>
			!(trait?.GetHook<T_CCU>() is null) &&
			trait?.GetHook<T_PlayerTrait>() is null;
		public static bool IsDesignerUnlock(Unlock unlock) =>
		   unlock.GetHook() is TU_DesignerUnlock traitUnlock_CCU &&
		   !traitUnlock_CCU.IsPlayerTrait;

		public static bool IsPlayerTrait(Trait trait) =>
			trait?.GetHook<T_CCU>() is null ||
			!(trait?.GetHook<T_PlayerTrait>() is null);
		public static bool IsPlayerUnlock(Unlock unlock) =>
			!(unlock.GetHook() is TU_DesignerUnlock) ||
			(unlock.GetHook() is TU_DesignerUnlock traitUnlock_CCU && traitUnlock_CCU.IsPlayerTrait);

		public static string DesignerName(Type type, string custom = null) =>
			$"{Core.CCUBlockTag} {Prettify(GroupFromNamespace(type))} - {custom ?? Prettify(type.Name)}";
			
		public static string LongishDocumentationName(Type type) =>
			$"{Prettify(GroupFromNamespace(type))} - {Prettify(type.Name)}";

		public static string PlayerName(Type type) =>
			Prettify(type.Name);

		// TODO: Eliminate this. Make an abstract group name string field to be filled out per group.
		private static string GroupFromNamespace(Type type) =>
			type.Namespace.Split('.')[2];

		private static string Prettify(string original) =>
			original
				.Replace('_', ' ')
				.Replace("2", "+")
				.Trim();

		// This makes override in child classes optional.
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}

	[HarmonyPatch(typeof(CharacterCreation))]
	public static class P_CharacterCreation
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		// Filters trait list on character sheet to Player Traits
		// This method transpiles before Sorquol.P_CharacterCreation.CreatePointTallyText_AutoSortTraitsChosen. Changes here must be reflected in its criteria.
		[HarmonyTranspiler, HarmonyPatch(nameof(CharacterCreation.CreatePointTallyText))]
		private static IEnumerable<CodeInstruction> ExemptDesignerTraitsFromCCPV(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo traitsChosen = AccessTools.DeclaredField(typeof(CharacterCreation), nameof(CharacterCreation.traitsChosen));
			MethodInfo playerUnlockList = AccessTools.DeclaredMethod(typeof(T_CCU), nameof(T_CCU.PlayerUnlockList), new[] { typeof(List<Unlock>) });

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 2,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld, traitsChosen)
				},
				targetInstructionSequence: new List<CodeInstruction>
				{
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, playerUnlockList),
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call),
					new CodeInstruction(OpCodes.Stloc_S, 12),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		/// <summary>
		/// Custom CCU Trait list section on character sheet
		/// TODO: Add a "Free Traits" section
		/// </summary>
		/// <param name="codeInstructions"></param>
		/// <returns></returns>
		[HarmonyTranspiler, HarmonyPatch(nameof(CharacterCreation.CreatePointTallyText))]
		private static IEnumerable<CodeInstruction> DesignerTraitSection(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo itemsChosen = AccessTools.DeclaredField(typeof(CharacterCreation), nameof(CharacterCreation.itemsChosen));
			MethodInfo printTraitList = AccessTools.DeclaredMethod(typeof(P_CharacterCreation), nameof(P_CharacterCreation.PrintTraitList));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
				},
				targetInstructionSequence: new List<CodeInstruction>
				{

				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, printTraitList),
					new CodeInstruction(OpCodes.Ldarg_0), // Replace original
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, itemsChosen),
					new CodeInstruction(OpCodes.Callvirt),
					new CodeInstruction(OpCodes.Ldc_I4_0),
					new CodeInstruction(OpCodes.Ble_S),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		public static void PrintTraitList(CharacterCreation CC)
		{
			if (Core.designerEdition &&
				T_CCU.DesignerUnlockList(CC.traitsChosen).Any())
			{
				CC.pointTallyText.text += "\n<color=yellow>- CCU TRAITS -</color>\n";
				foreach (Unlock unlock in T_CCU.SortUnlocksByName(T_CCU.DesignerUnlockList(CC.traitsChosen).Where(u => !T_CCU.IsPlayerUnlock(u)).ToList()))
				{
					string traitName = GC.nameDB.GetName(unlock.unlockName, "StatusEffect") + "\n";
					traitName = traitName.Replace("[CCU] ", "");
					CC.pointTallyText.text += traitName;
				}
			}
		}
	}

	[HarmonyPatch(typeof(CharacterSelect))]
	public static class P_CharacterSelect
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(CharacterSelect.SetupSlotAgent))]
		private static IEnumerable<CodeInstruction> SetupSlotAgent_HideCCUTraits(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo dummyAgent = AccessTools.DeclaredField(typeof(CharacterSelect), nameof(CharacterSelect.dummyAgent));
			FieldInfo statusEffects = AccessTools.DeclaredField(typeof(Agent), nameof(Agent.statusEffects));
			FieldInfo traitList = AccessTools.DeclaredField(typeof(StatusEffects), nameof(StatusEffects.TraitList));
			MethodInfo displayableTraits = AccessTools.DeclaredMethod(typeof(T_CCU), nameof(T_CCU.PlayerTraitList));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld, dummyAgent),
					new CodeInstruction(OpCodes.Ldfld, statusEffects),
					new CodeInstruction(OpCodes.Ldfld, traitList),
				},
				targetInstructionSequence: new List<CodeInstruction>
				{
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, displayableTraits),
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Callvirt),
					new CodeInstruction(OpCodes.Stloc_S, 41),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}

	[HarmonyPatch(typeof(CharacterSheet))]
	public static class P_CharacterSheet
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(CharacterSheet.UpdateStats))]
		private static IEnumerable<CodeInstruction> UpdateStats_HideCCUTraits(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo traitList = AccessTools.DeclaredField(typeof(StatusEffects), nameof(StatusEffects.TraitList));
			MethodInfo displayableTraits = AccessTools.DeclaredMethod(typeof(T_CCU), nameof(T_CCU.PlayerTraitList));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 2,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldstr, " - </color>\n"),
					new CodeInstruction(OpCodes.Call),
					new CodeInstruction(OpCodes.Callvirt),
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Ldfld, traitList),
				},
				targetInstructionSequence: new List<CodeInstruction>
				{
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, displayableTraits),
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}

	[HarmonyPatch(typeof(StatusEffectDisplay))]
	class P_StatusEffectDisplay
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		/// <summary>
		/// Filter CCU traits from lower-left display when possessing
		/// </summary>
		/// <param name="myStatusEffect"></param>
		/// <param name="myTrait"></param>
		/// <returns></returns>
		[HarmonyPrefix, HarmonyPatch(nameof(StatusEffectDisplay.AddDisplayPiece), new[] { typeof(StatusEffect), typeof(Trait) })]
		public static bool AddDisplayPiece_Prefix(Trait myTrait)
		{
			return T_CCU.IsPlayerTrait(myTrait);
		}
	}
}