using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using BunnyLibs;
using CCU.Traits.Behavior;
using CCU.Traits.Player.Ranged_Combat;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Patches.P_Combat
{
	[HarmonyPatch(typeof(Combat))]
	public static class P_Combat
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(Combat.CombatCheck), argumentTypes: new Type[0] { })]
		private static IEnumerable<CodeInstruction> CombatCheck_LimitFearlessToVanillaKillerRobot(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo killerRobot = AccessTools.DeclaredField(typeof(Agent), nameof(Agent.killerRobot));
			MethodInfo isVanillaKillerRobot = AccessTools.DeclaredMethod(typeof(Seek_and_Destroy), nameof(Seek_and_Destroy.IsVanillaKillerRobot));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, killerRobot),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, isVanillaKillerRobot)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		[HarmonyPostfix, HarmonyPatch(nameof(Combat.DoRapidFire))]
		private static void DoRapidFire_TriggerHappy(Combat __instance, ref Agent ___agent)
		{
			foreach (T_RateOfFire trait in ___agent.GetTraits<T_RateOfFire>())
				__instance.rapidFireTime /= trait.CooldownMultiplier;
		}

		// I believe this is only called for Rapid Fire ranged attacks
		[HarmonyPostfix, HarmonyPatch(nameof(Combat.SetPersonalCooldown))]
		private static void SetPersonalCooldown_TriggerHappy(Combat __instance, ref Agent ___agent)
		{
			foreach (T_RateOfFire trait in ___agent.GetTraits<T_RateOfFire>())
				__instance.personalCooldown *= trait.CooldownMultiplier;
		}
	}
}