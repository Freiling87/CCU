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

namespace CCU.Traits.Passive
{
	internal class Explodeviceable : T_DesignerTrait
	{
		//	Just need one more patch: Add conditions to the empty branch in ItemFunctions.UseItem, "for (int l = 0; l < this.gc.agentList.Count; l++)"

		//[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Explodeviceable>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Explosive Stimulator works on this character."),
					[LanguageCode.Spanish] = "La estimuladora explosiva funciona en este personaje..",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Explodeviceable)),
					[LanguageCode.Spanish] = "Estimulador Explosivo Vulnerable",
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

		internal static bool IsVulnerable(Agent agent) =>
			agent.killerRobot || agent.HasTrait<Explodeviceable>();


		internal static FieldInfo vulnerableVanilla = AccessTools.DeclaredField(typeof(Agent), nameof(Agent.killerRobot));
		internal static MethodInfo vulnerable = AccessTools.DeclaredMethod(typeof(Explodeviceable), nameof(Explodeviceable.IsVulnerable));

	}

	[HarmonyPatch(typeof(AgentInteractions))]
	internal static class P_AgentInteractions_UseExplosiveStimulator
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTargetMethod, UsedImplicitly]
		private static MethodInfo Find_MoveNext_MethodInfo() =>
			PatcherUtils.FindIEnumeratorMoveNext(AccessTools.Method(typeof(AgentInteractions), nameof(AgentInteractions.UseExplosiveStimulator)));

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> SoftcodeExplosiveStimulator(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, Explodeviceable.vulnerableVanilla),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, Explodeviceable.vulnerable)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}
}