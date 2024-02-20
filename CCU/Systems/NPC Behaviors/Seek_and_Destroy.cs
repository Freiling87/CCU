using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using HarmonyLib;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Traits.Behavior
{
	public class Seek_and_Destroy : T_Behavior
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Seek_and_Destroy>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = string.Format("This character will follow and attack the player like the Killer Robot."),
					[LanguageCode.Spanish] = "Este NPC buscara al jugador para atacarlo no importe donde este como si fuera el Robot Asesino.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Seek_and_Destroy), "Seek & Destroy"),
					[LanguageCode.Spanish] = "Buscar y Destruir",
				})
				.WithUnlock(new TU_DesignerUnlock
				{
					Cancellations = { },
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}

		public override void SetupAgent(Agent agent) { }

		internal static bool SeekAndDestroy(Agent agent) => 
			agent.killerRobot || agent.HasTrait<Seek_and_Destroy>();

		internal static FieldInfo killerRobot = AccessTools.DeclaredField(typeof(Agent), nameof(Agent.killerRobot));
		internal static MethodInfo seekAndDestroy = AccessTools.DeclaredMethod(typeof(Seek_and_Destroy), nameof(Seek_and_Destroy.SeekAndDestroy));
	}

	[HarmonyPatch(typeof(BrainUpdate))]
	internal static class P_BrainUpdate_SeekAndDestroy
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(BrainUpdate.GoalArbitrate))]
		private static IEnumerable<CodeInstruction> SetGoalWeights(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 3,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, Seek_and_Destroy.killerRobot),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, Seek_and_Destroy.seekAndDestroy)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		[HarmonyTranspiler, HarmonyPatch(nameof(BrainUpdate.MyUpdate))]
		private static IEnumerable<CodeInstruction> StickToWhatYoureGoodAtAndThatsSeekingAndOrDestroying(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 3,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, Seek_and_Destroy.killerRobot),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, Seek_and_Destroy.seekAndDestroy)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}

	[HarmonyPatch(typeof(Relationships))]
	internal static class P_Relationships_SeekAndDestroy
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(Relationships.SetupRelationshipOriginal))]
		private static IEnumerable<CodeInstruction> StickToWhatYoureGoodAtAndThatsSeekingAndOrDestroying(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 2,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, Seek_and_Destroy.killerRobot),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, Seek_and_Destroy.seekAndDestroy)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}
}