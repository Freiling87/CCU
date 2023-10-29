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

namespace CCU.Traits.Combat_
{
	public class Backed_Up : T_Combat, ISetupAgentStats
	{
		//[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Backed_Up>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character has a Walkie Talkie, and will call for Police backup when they enter combat. They should probably eat more fiber."),
					[LanguageCode.Spanish] = "Este NPC tiene un Walkie Talkie que se activa al entrar en combate llamando a los polis cercanos. Como tipica chusma.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Backed_Up)),
					[LanguageCode.Spanish] = "Llama-refuerzos",
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					Cancellations = { },
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }

		public bool BypassUnlockChecks => false;
		public void SetupAgent(Agent agent)
		{
			agent.GetOrAddHook<H_AgentInteractions>().WalkieTalkieUsed = false;
		}
	}

	[HarmonyPatch(typeof(GoalBattle))]
	public static class P_GoalBattle
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(GoalBattle.Process))]
		private static IEnumerable<CodeInstruction> Process_StartCombatActions(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo agent = AccessTools.DeclaredField(typeof(Goal), nameof(GoalBattle.agent));
			FieldInfo canTakeDrugs = AccessTools.DeclaredField(typeof(Combat), nameof(Combat.canTakeDrugs));
			MethodInfo doCombatActions = AccessTools.DeclaredMethod(typeof(P_GoalBattle), nameof(StartCombatActions));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldc_I4_1),
					new CodeInstruction(OpCodes.Stloc_0),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld, agent),
					new CodeInstruction(OpCodes.Call, doCombatActions)
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Ldfld, canTakeDrugs),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		private static void StartCombatActions(Agent agent)
		{
			if (agent.HasTrait<Backed_Up>() && !agent.GetOrAddHook<H_AgentInteractions>().WalkieTalkieUsed)
			{
				agent.agentInteractions.UseWalkieTalkie(agent, agent.opponent); // Might be reversed, hard to tell
				agent.GetOrAddHook<H_AgentInteractions>().WalkieTalkieUsed = true;
			}
		}
	}
}