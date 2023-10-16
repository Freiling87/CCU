using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using BunnyLibs;
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
					[LanguageCode.Spanish] = "Este NPC se le asignara la Placa de Visitante del Alcalde, En caso de multiples NPCs con este rasgo, se eligira uno aleatoriamente. Normalmete la placa se asigna a el Empleado Central.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Chunk_Mayor_Badge)),
					[LanguageCode.Spanish] = "Placa de Visitante",

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

	// TODO: IModInventory
	[HarmonyPatch(typeof(InvDatabase))]
	public class P_InvDatabase_MayorBadge
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		private static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(InvDatabase.FillAgent))]
		public static IEnumerable<CodeInstruction> FillAgent_LoadoutBadge(IEnumerable<CodeInstruction> codeInstructions)
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

		public static string MayorBadgeSoftcode(Agent agent) =>
			agent.HasTrait<Chunk_Mayor_Badge>()
				? VanillaAgents.Clerk
				: agent.name;
	}
}