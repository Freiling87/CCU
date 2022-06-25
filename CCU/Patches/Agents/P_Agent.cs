using BepInEx.Logging;
using HarmonyLib;
using System;
using RogueLibsCore;
using System.Reflection;
using CCU.Traits;
using CCU.Traits.Combat;
using CCU.Traits.Passive;
using CCU.Traits.Behavior;
using CCU.Traits.Interaction;
using CCU.Traits.Hack;
using CCU.Traits.Rel_Player;
using CCU.Traits.Rel_General;
using CCU.Traits.Trait_Gate;
using System.Linq;
using CCU.Traits.Merchant_Type;
using CCU.Traits.Drug_Warrior;
using System.Collections.Generic;
using BTHarmonyUtils.TranspilerUtils;
using System.Reflection.Emit;

namespace CCU.Patches.Agents
{
	[HarmonyPatch(declaringType: typeof(Agent))]
	public class P_Agent
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

        [HarmonyTranspiler, HarmonyPatch(methodName: nameof(Agent.AgentLateUpdate), argumentTypes: new Type[0] { })]
		private static IEnumerable<CodeInstruction> KillerRobotWaterDamage(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo killerRobot = AccessTools.DeclaredField(typeof(Agent), nameof(Agent.killerRobot));
			MethodInfo isKRorSNS = AccessTools.DeclaredMethod(typeof(P_Agent), nameof(IsKillerRobotOrSNS));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, killerRobot),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, isKRorSNS)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		private static bool IsKillerRobotOrSNS(Agent agent) =>
			agent.killerRobot &&
			//	Agent.KillerRobot has water damage hardcoded. Exclude it if they don't add electronic.
			!(agent.HasTrait<Seek_and_Destroy>() && !agent.HasTrait(VanillaTraits.Electronic));


		[HarmonyPostfix, HarmonyPatch(methodName: nameof(Agent.CanShakeDown))]
		public static void CanShakeDown_Postfix(Agent __instance, ref bool __result)
		{
			if (!__instance.oma.shookDown && !GC.loadLevel.LevelContainsMayor() && __instance.HasTrait<Extortable>())
				__result = true;
		}

		/// <summary>
		/// Extend Job Type Pseudo-enum 
		/// Code contributions from uwumacaronitime
		/// </summary>
		/// <param name="jobInt"></param>
		/// <param name="__result"></param>
		/// <returns></returns>
		[HarmonyPrefix, HarmonyPatch(methodName: nameof(Agent.GetCodeFromJob), argumentTypes: new[] { typeof(int) })]
		public static bool GetCodeFromJob_Prefix(int jobInt, ref string __result)
		{
			switch (jobInt)
			{
				case 12:
					__result = CJob.SafecrackSafe;
					break;
				case 13:
					__result = CJob.TamperSomething;
					break;
				default:
					return true;
			}

			return false;
		}

		/// <summary>
		/// Code contributions from uwumacaronitime
		/// </summary>
		/// <param name="jobString"></param>
		/// <param name="__result"></param>
		/// <returns></returns>
		[HarmonyPrefix, HarmonyPatch(methodName: nameof(Agent.GetJobCode), argumentTypes: new[] { typeof(string) })]
		public static bool GetJobCode_Prefix(string jobString, ref jobType __result)
		{
			// No idea how to extend an actual enum, and the advice I've gotten has been worrying.

			switch (jobString)
			{
				case CJob.SafecrackSafe:
					__result = jobType.GetSupplies; // TODO
					break;
				case CJob.TamperSomething:
					__result = jobType.GetSupplies; // TODO
					break;
				default:
					return true;
			}

			return false;
		}

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(Agent.ObjectAction), argumentTypes: new[] { typeof(string), typeof(string), typeof(float), typeof(Agent), typeof(PlayfieldObject) })]
		public static bool ObjectAction_Prefix(string myAction, string extraString, float extraFloat, Agent causerAgent, PlayfieldObject extraObject, Agent __instance, ref bool ___noMoreObjectActions)
		{
			Core.LogMethodCall();

			if (myAction == CJob.TamperSomething || myAction == CJob.SafecrackSafe)
			{
				// base.ObjectAction(myAction, extraString, extraFloat, causerAgent, extraObject);
				MethodInfo objectAction_base = AccessTools.DeclaredMethod(typeof(Agent).BaseType, "ObjectAction");
				objectAction_base.GetMethodWithoutOverrides<Action<string, string, float, Agent, PlayfieldObject>>(__instance).Invoke(myAction, extraString, extraFloat, causerAgent, extraObject);

				if (!___noMoreObjectActions)
				{
					if (myAction == CJob.SafecrackSafe)
						P_AgentInteractions_DetermineButtons.SafecrackSafe(__instance, causerAgent, extraObject);
					else if (myAction == CJob.TamperSomething)
						P_AgentInteractions_DetermineButtons.TamperSomething(__instance, causerAgent, extraObject);

					___noMoreObjectActions = false;
				}

				return false;
			}

			return true;
		}

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(Agent.SetupAgentStats), argumentTypes: new[] { typeof(string) })]
		public static void SetupAgentStats_Postfix(string transformationType, Agent __instance)
		{
			#region Active
			if (__instance.GetTraits<T_Behavior>().Where(c => c.LosCheck).Any())
			{
				// Thieves have their LOScheck set to 50% in vanilla
				if (__instance.HasTrait<Pick_Pockets>() && GC.percentChance(50))
					return;

				// All others excluded
				__instance.losCheckAtIntervals = true;
			}

			if (__instance.HasTrait<Seek_and_Destroy>())
				__instance.killerRobot = true;
			#endregion
			#region Interaction
			if (__instance.HasTrait<T_Hack>())
				__instance.hackable = true;
			#endregion
			#region Combat
			if (__instance.HasTrait<T_DrugWarrior>())
				__instance.combat.canTakeDrugs = true;
			#endregion
			#region Merchant
			if (__instance.GetTraits<T_MerchantType>().Any())
				__instance.SetupSpecialInvDatabase();
			#endregion
			#region Passive
			if (__instance.HasTrait<AccidentProne>())
				__instance.dontStopForDanger = true;

			if (__instance.HasTrait<Crusty>())
				__instance.upperCrusty = true;

			if (__instance.HasTrait<Guilty>())
				__instance.oma.mustBeGuilty = true;

			if (__instance.HasTrait<Possessed>())
			{
				__instance.secretShapeShifter = true;
				__instance.oma.secretShapeShifter = true;
				__instance.oma.mustBeGuilty = true;
				__instance.agentHitboxScript.GetColorFromString("Red", "Eyes");
			}

			if (__instance.HasTrait<Status_Effect_Immune>())
				__instance.preventStatusEffects = true;

			if (__instance.HasTrait<Vision_Beams>())
				__instance.agentSecurityBeams.enabled = true;

			if (__instance.HasTrait<Z_Infected>())
				__instance.zombieWhenDead = true;
			#endregion
			#region Relationships
			if (__instance.HasTrait<Relationless>())
				__instance.dontChangeRelationships = true;
			#endregion
			#region Trait Gates
			if (__instance.HasTrait<Scumbag>())
				__instance.oma.mustBeGuilty = true;
			#endregion

			if (GC.challenges.Contains(CMutators.HomesicknessDisabled))
				__instance.canGoBetweenLevels = true;
			else if (GC.challenges.Contains(CMutators.HomesicknessMandatory))
				__instance.canGoBetweenLevels = false;
		}

		[HarmonyPostfix, HarmonyPatch(methodName: "Start", argumentTypes: new Type[0])]
		public static void Start_Postfix(Agent __instance)
		{
			__instance.AddHook<P_Agent_Hook>();
		}
	}

	public class P_Agent_Hook : HookBase<PlayfieldObject>
	{
		public int SuicideVestTimer;

		protected override void Initialize() { }
	}
}
