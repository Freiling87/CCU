using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using BunnyLibs;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Traits.Passive
{
	public class Crusty : T_DesignerTrait, ISetupAgentStats
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Crusty>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character has the privileges of the upper class."),
					[LanguageCode.Spanish] = "Este personaje tiene todos los beneficios de la clase alta.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Crusty)),
					[LanguageCode.Spanish] = "Rico",
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

		public bool BypassUnlockChecks => false;
		public void SetupAgent(Agent agent)
		{
			agent.upperCrusty = true;
		}
	}

	[HarmonyPatch(typeof(GoalFlee))]
	public static class P_GoalFlee
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(GoalFlee.Activate))]
		private static IEnumerable<CodeInstruction> GoalFlee_CrustyTattle(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo agent = AccessTools.DeclaredField(typeof(Goal), nameof(Goal.agent));
			MethodInfo upperCrusterSoftcode = AccessTools.DeclaredMethod(typeof(P_GoalFlee), nameof(P_GoalFlee.UpperCrusterSoftcode));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld, agent),
					new CodeInstruction(OpCodes.Callvirt),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld, agent),
					new CodeInstruction(OpCodes.Call, upperCrusterSoftcode),
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldstr, VanillaAgents.UpperCruster)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		private static string UpperCrusterSoftcode(Agent agent) =>
			agent.HasTrait<Crusty>()
				? VanillaAgents.UpperCruster
				: agent.agentName;
	}
}