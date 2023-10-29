using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Traits.Passive;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Traits.Drug_Warrior_Modifier
{
	public class Suppress_Syringe_AV : T_DrugWarriorModifier
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Suppress_Syringe_AV>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Prevents Syringe text and sound when this agent uses a drug. N.b.: This only addresses the \"-Syringe\" text. To suppress status effect text, use {0}.", DesignerName(typeof(Suppress_Status_Text))),
					[LanguageCode.Spanish] = "Oculta el texto cuando un NPCs usa una droga, solo funciona con el texto al usarse, el texto informativo sobre los NPCs solo se puede quitar con (Suprimir Texto de Status)",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Suppress_Syringe_AV)),
					[LanguageCode.Spanish] = "Suprimir Aviso de Efecto",

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
	}

	[HarmonyPatch(typeof(GoalBattle))]
	public static class P_GoalBattle_SuppressSyringeAV
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(GoalBattle.Process))]
		private static IEnumerable<CodeInstruction> HijackSyringeAV(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo agent = AccessTools.DeclaredField(typeof(Goal), nameof(GoalBattle.agent));
			MethodInfo gateDrugWarriorAV = AccessTools.DeclaredMethod(typeof(P_GoalBattle_SuppressSyringeAV), nameof(P_GoalBattle_SuppressSyringeAV.DoSyringeAV));

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
		private static void DoSyringeAV(Agent agent)
		{
			if (agent.HasTrait<Suppress_Syringe_AV>())
				return;

			GC.spawnerMain.SpawnStatusText(agent, "UseItem", VItemName.Syringe, "Item");
			GC.audioHandler.Play(agent, VanillaAudio.UseSyringe);
		}
	}
}