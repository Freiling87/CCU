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

namespace CCU.Traits.Trait_Gate
{
	public class Scumbag : T_TraitGate, ISetupAgentStats
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Scumbag>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This Agent is a valid target for Scumbag Slaughterer, and will be hostile to them."),
					[LanguageCode.Spanish] = "Este personaje es afectado por Aplastascoria, por lo que sera hostil y tendra mejores armas al tener la Gran Misión del Mecarobot.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Scumbag)),
					[LanguageCode.Spanish] = "Escoria",
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
		public override void OnAdded() { }
		public override void OnRemoved() { }

		public bool BypassUnlockChecks => false;
		public void SetupAgent(Agent agent)
		{
			//agent.oma.mustBeGuilty = true;
			agent.oma._mustBeGuilty = true;
			//agent.isBigQuestObject = true;
			//agent.bigQuestType = "MechPilot";
			//agent.showBigQuestMarker = true;
			//agent.bigQuestMarkerAlwaysSeen = true;
			//agent.bigQuestMarkerMustBeDiscovered = true;
			//agent.SpawnNewMapMarker();
		}
	}

	[HarmonyPatch(typeof(Quests))]
	public static class P_Quests
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(Quests.CheckIfBigQuestObject))]
		private static IEnumerable<CodeInstruction> MakeScumbag(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo scumbagSoftcode = AccessTools.DeclaredMethod(typeof(P_Quests), nameof(P_Quests.ScumbagSoftcode));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_1),
				},
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Callvirt),
					new CodeInstruction(OpCodes.Ldstr, VanillaAgents.GangsterCrepe),
					new CodeInstruction(OpCodes.Call),
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Brtrue),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, scumbagSoftcode)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		private static bool ScumbagSoftcode(PlayfieldObject playfieldObject) =>
			playfieldObject.CompareTag("Agent")
				? ((Agent)playfieldObject).HasTrait<Scumbag>() || playfieldObject.objectName == VanillaAgents.GangsterCrepe
				: false;

	}
}
