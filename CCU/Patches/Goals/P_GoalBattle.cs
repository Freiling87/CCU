using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Patches.Agents;
using CCU.Traits.Combat;
using CCU.Traits.Drug_Warrior_Modifier;
using CCU.Traits.Passive;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CCU.Patches.Goals
{
	[HarmonyPatch(declaringType: typeof(GoalBattle))]
	public static class P_GoalBattle
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(GoalBattle.Process))]
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
			if (agent.HasTrait<Backed_Up>() && !agent.GetOrAddHook<P_Agent_Hook>().WalkieTalkieUsed)
            {
				agent.agentInteractions.UseWalkieTalkie(agent, agent.opponent); // Might be reversed, hard to tell
				agent.GetOrAddHook<P_Agent_Hook>().WalkieTalkieUsed = true;
            }
        }

		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(GoalBattle.Process))]
		private static IEnumerable<CodeInstruction> Process_GateDrugWarriorAV(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo agent = AccessTools.DeclaredField(typeof(Goal), nameof(GoalBattle.agent));
			MethodInfo gateDrugWarriorAV = AccessTools.DeclaredMethod(typeof(P_GoalBattle), nameof(P_GoalBattle.GateDrugWarriorAV));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					//	this.gc.spawnerMain.SpawnStatusText(this.agent, "UseItem", "Syringe", "Item");
					//	this.gc.audioHandler.Play(this.agent, "UseSyringe");

					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Ldstr, "UseItem"),
					new CodeInstruction(OpCodes.Ldstr, "Syringe"),
					new CodeInstruction(OpCodes.Ldstr, "Item"),
					new CodeInstruction(OpCodes.Callvirt),
					new CodeInstruction(OpCodes.Pop),
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Ldstr, "UseSyringe"),
					new CodeInstruction(OpCodes.Callvirt),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld, agent),
					new CodeInstruction(OpCodes.Call, gateDrugWarriorAV),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		private static void GateDrugWarriorAV(Agent agent)
        {
			if (agent.HasTrait<Suppress_Syringe_AV>())
				return;
		
			GC.spawnerMain.SpawnStatusText(agent, "UseItem", vItem.Syringe, "Item");
			GC.audioHandler.Play(agent, VanillaAudio.UseSyringe);
        }

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(GoalBattle.Terminate))]
		private static void Terminate_Postfix(Agent ___agent)
        {
			___agent.inventory.ChooseWeapon();
        }
	}
}