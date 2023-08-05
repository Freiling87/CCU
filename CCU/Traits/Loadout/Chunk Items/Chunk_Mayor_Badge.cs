using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Traits.Loadout;
using HarmonyLib;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Traits.Loadout_Chunk_Items
{
	public class Chunk_Mayor_Badge : T_Loadout
	{
        //[RLSetup]
        public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Chunk_Mayor_Badge>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "When placed in a chunk, this character will by default be the Badge Holder. If multiple characters have this trait, one will be chosen randomly. This will override default behaviors that assign keys to Clerks, etc.",
					
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Chunk_Mayor_Badge)),
					
				})
				.WithUnlock(new TraitUnlock_CCU
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

	[HarmonyPatch(typeof(InvDatabase))]
	internal class P_InvDatabase_MayorBadge
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		private static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(InvDatabase.FillAgent))]
		internal static IEnumerable<CodeInstruction> FillAgent_LoadoutBadge(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo mayorBadgeSoftcode = AccessTools.DeclaredMethod(typeof(P_InvDatabase_MayorBadge), nameof(P_InvDatabase_MayorBadge.MayorBadgeSoftcode));
			FieldInfo agent = AccessTools.DeclaredField(typeof(InvDatabase), nameof(InvDatabase.agent));
			FieldInfo wontFlee = AccessTools.DeclaredField(typeof(Agent), nameof(Agent.wontFlee));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Stfld, wontFlee),
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld, agent),
				},
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Callvirt),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, mayorBadgeSoftcode)
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldstr, VanillaAgents.Clerk),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		internal static string MayorBadgeSoftcode(Agent agent) =>
			agent.HasTrait<Chunk_Mayor_Badge>()
				? VanillaAgents.Clerk
				: agent.name;
	}
}