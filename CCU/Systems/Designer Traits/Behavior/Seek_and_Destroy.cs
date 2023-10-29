using BepInEx.Logging;
using BTHarmonyUtils;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Traits.Explode_On_Death;
using HarmonyLib;
using JetBrains.Annotations;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Traits.Behavior
{
	//	TODO:
	//		Eliminate all the patches here.
	//	Patch:
	//		BrainUpdate (various)
	//		Relationships.SetupRelationshipOriginal
	//	That's it! The shortcut was a longcut.

	public class Seek_and_Destroy : T_Behavior
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Seek_and_Destroy>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = string.Format("This character will follow and attack the player like the Killer Robot."),
					[LanguageCode.Spanish] = "Este NPC buscara al jugador para atacarlo no importe donde este como si fuera el Robot Asesino.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Seek_and_Destroy), "Seek & Destroy"),
					[LanguageCode.Spanish] = "Buscar y Destruir",
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

		public override void SetupAgent(Agent agent)
		{
			agent.killerRobot = true;
		}

		public static bool IsVanillaKillerRobot(Agent agent) =>
			agent.killerRobot &&
			agent.agentName == VanillaAgents.KillerRobot;

		internal static FieldInfo killerRobot = AccessTools.DeclaredField(typeof(Agent), nameof(Agent.killerRobot));
		internal static MethodInfo isVanillaKillerRobot = AccessTools.DeclaredMethod(typeof(Seek_and_Destroy), nameof(Seek_and_Destroy.IsVanillaKillerRobot));
	}

	[HarmonyPatch(typeof(Agent))]
	internal static class P_Agent
	{
		public static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(Agent.AgentLateUpdate))]
		private static IEnumerable<CodeInstruction> SoftcodeWaterDamage(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, Seek_and_Destroy.killerRobot),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, Seek_and_Destroy.isVanillaKillerRobot)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}

	[HarmonyPatch(typeof(AgentInteractions))]
	internal static class P_AgentInteractions
	{
		public static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(AgentInteractions.FinishedOperating))]
		private static IEnumerable<CodeInstruction> SoftcodeHack(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, Seek_and_Destroy.killerRobot),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, Seek_and_Destroy.isVanillaKillerRobot)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
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
					new CodeInstruction(OpCodes.Ldfld, Seek_and_Destroy.killerRobot),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, Seek_and_Destroy.isVanillaKillerRobot)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}

	[HarmonyPatch(typeof(BulletHitbox))]
	internal static class P_BulletHitbox
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(BulletHitbox.HitAftermath))]
		private static IEnumerable<CodeInstruction> SoftcodeWaterBulletDamage(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo copBot = AccessTools.DeclaredField(typeof(Agent), nameof(Agent.copBot));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 2,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, copBot),
					new CodeInstruction(OpCodes.Brtrue_S),
					new CodeInstruction(OpCodes.Ldarg_1),
				},
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, Seek_and_Destroy.killerRobot),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, Seek_and_Destroy.isVanillaKillerRobot)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}

	[HarmonyPatch(typeof(Combat))]
	internal static class P_Combat
	{
		public static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(Combat.CombatCheck))]
		private static IEnumerable<CodeInstruction> SoftcodeFearlessness(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, Seek_and_Destroy.killerRobot),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, Seek_and_Destroy.isVanillaKillerRobot)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}

	[HarmonyPatch(typeof(Door))]
	internal static class P_Door
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(Door.ObjectCollide))]
		private static IEnumerable<CodeInstruction> SoftcodeWalkThroughDoors(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, Seek_and_Destroy.killerRobot),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, Seek_and_Destroy.isVanillaKillerRobot)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}

	[HarmonyPatch(typeof(Explosion))]
	internal static class P_Explosion
	{
		public static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(Explosion.ExplosionHit))]
		private static IEnumerable<CodeInstruction> SoftcodeEMPVulnerability(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 6,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, Seek_and_Destroy.killerRobot),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, Seek_and_Destroy.isVanillaKillerRobot)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}

	[HarmonyPatch(typeof(ItemFunctions))]
	internal static class P_ItemFunctions
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(ItemFunctions.TargetObject))]
		private static IEnumerable<CodeInstruction> SoftcodeHack(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 3,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld,Seek_and_Destroy.killerRobot),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, Seek_and_Destroy.isVanillaKillerRobot)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}

	[HarmonyPatch(typeof(Melee))]
	internal static class P_Melee
	{
		public static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(Melee.Attack), new[] { typeof(bool) })]
		private static IEnumerable<CodeInstruction> SoftcodeKnockbackBonus(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, Seek_and_Destroy.killerRobot),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, Seek_and_Destroy.isVanillaKillerRobot)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}

	[HarmonyPatch(typeof(ObjectMult))]
	internal static class P_ObjectMult
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(ObjectMult.UpdateObjectMult))]
		private static IEnumerable<CodeInstruction> SoftcodeHealthChange(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, Seek_and_Destroy.killerRobot),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, Seek_and_Destroy.isVanillaKillerRobot)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}

	[HarmonyPatch(typeof(StatusEffects))]
	internal static class P_StatusEffects
	{
		public static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(nameof(StatusEffects.ExplodeAfterDeathChecks))]
		public static bool SoftcodeExplodeOnDeath(StatusEffects __instance)
		{
			// TODO: eliminate ext ref
			if (__instance.agent.HasTrait<Seek_and_Destroy>() && !__instance.agent.HasTrait<T_ExplodeOnDeath>())
				return false;

			return true;
		}
	}
}