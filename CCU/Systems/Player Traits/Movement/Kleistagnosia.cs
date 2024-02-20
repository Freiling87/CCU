using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Traits.Behavior;
using HarmonyLib;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Systems.Player_Traits.Movement
{
	internal class Kleistagnosia : T_PlayerTrait
	{
		// + version for Steel Doors?

		//[RLSetup]
		public static void Setup()
		{
			PostProcess_PlayerTrait = RogueLibs.CreateCustomTrait<Kleistagnosia>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = string.Format("You're missing the part of your brain that can tell whether doors are closed or not. You just walk right through. Maybe that's how you hurt your brain in the first place."),
					[LanguageCode.Spanish] = "Te falta la parte de tu cerebro que puede decir si las puertas están cerradas o no. Simplemente pasas. Tal vez así es como te lastimaste el cerebro al primer.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Kleistagnosia)),
					[LanguageCode.Spanish] = "Kleistagnosia",
				})
				.WithUnlock(new TU_PlayerUnlock
				{
					Cancellations = { },
					CharacterCreationCost = 5,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,

					UnlockCost = 10,
					Unlock =
					{
						categories = { VTraitCategory.Movement },
					}
				});
		}

		internal static bool CanSmashDoors(Agent agent) =>
			agent.killerRobot || agent.HasTrait<Kleistagnosia>();
	}

	//[HarmonyPatch(typeof(Door))]
	internal static class P_Door
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(Door.ObjectCollide))]
		private static IEnumerable<CodeInstruction> SoftcodeWalkThroughDoors(IEnumerable<CodeInstruction> codeInstructions)
		{
			//	BUG:	NPCs stop short of interaction range when answering knocked doors
			List<CodeInstruction> instructions = codeInstructions.ToList();

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, AccessTools.DeclaredField(typeof(Agent), nameof(Agent.killerRobot))),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, AccessTools.DeclaredMethod(typeof(Kleistagnosia), nameof(Kleistagnosia.CanSmashDoors)))
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}
}