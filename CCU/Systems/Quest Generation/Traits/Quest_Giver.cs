using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Mutators.Quest_Gen;
using HarmonyLib;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Traits.Special_Quests
{
	public class Quest_Giver : T_Quest
	{
		//[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Quest_Giver>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent is eligible to assign special quests.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Quest_Giver)),
				})
				.WithUnlock(new TU_DesignerUnlock
				{
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}

	[HarmonyPatch(typeof(Quests))]
	class P_Quests_QuestGiver
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(Quests.setupQuests))]
		private static IEnumerable<CodeInstruction> FilterLevelMutatorList(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo customMethod = AccessTools.DeclaredMethod(typeof(P_Quests_QuestGiver), nameof(P_Quests_QuestGiver.QuestGiverSoftcode));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_S, 34),
				},
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Callvirt),
					new CodeInstruction(OpCodes.Ldstr, VanillaAgents.Shopkeeper),
					new CodeInstruction(OpCodes.Call),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, customMethod),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		private static bool QuestGiverSoftcode(Agent agent) =>
			agent.isPlayer == 0 &&
				(agent.agentName == VanillaAgents.Shopkeeper
				|| agent.HasTrait<Quest_Giver>()
				|| (GC.challenges.Contains(nameof(Client_Network)) && ClientNetworkQuestGivers.Contains(agent.agentRealName)));

		private static readonly List<string> ClientNetworkQuestGivers = new List<string>()
		{
			VanillaAgents.Cop,
			VanillaAgents.GangsterBlahd,
			VanillaAgents.GangsterCrepe,
			VanillaAgents.InvestmentBanker,
			VanillaAgents.Mayor,
			VanillaAgents.Mobster,
			VanillaAgents.ResistanceLeader,
			VanillaAgents.Slavemaster,
			VanillaAgents.SuperCop,
			VanillaAgents.UpperCruster,
		};
	}
}