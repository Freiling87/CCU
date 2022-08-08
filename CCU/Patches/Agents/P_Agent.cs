using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Challenges.Followers;
using CCU.Localization;
using CCU.Traits;
using CCU.Traits.Behavior;
using CCU.Traits.Combat;
using CCU.Traits.Drug_Warrior;
using CCU.Traits.Gib_Type;
using CCU.Traits.Hack;
using CCU.Traits.Language;
using CCU.Traits.Merchant_Type;
using CCU.Traits.Passive;
using CCU.Traits.Rel_General;
using CCU.Traits.Trait_Gate;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Patches.Agents
{
    [HarmonyPatch(declaringType: typeof(Agent))]
	public class P_Agent
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

        [HarmonyTranspiler, HarmonyPatch(methodName: nameof(Agent.AgentLateUpdate), argumentTypes: new Type[0] { })]
		private static IEnumerable<CodeInstruction> AgentLateUpdate_LimitWaterDamageToVanillaKillerRobot(IEnumerable<CodeInstruction> codeInstructions)
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

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(Agent.CanShakeDown))]
		public static void CanShakeDown_Postfix(Agent __instance, ref bool __result)
		{
			if (!__instance.oma.shookDown && !GC.loadLevel.LevelContainsMayor() && __instance.HasTrait<Extortable>())
				__result = true;
		}

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(Agent.CanUnderstandEachOther))]
		public static void CanUnderstandEachOther_Postfix(Agent __instance, Agent otherAgent, ref bool __result)
		{
			if (__result is true || 
				__instance.statusEffects.hasStatusEffect(VStatusEffect.HearingBlocked) ||
				otherAgent.statusEffects.hasStatusEffect(VStatusEffect.HearingBlocked))
				return;

			List<T_Language> myLanguages = __instance.GetTraits<T_Language>().ToList();
			List<T_Language> yourLanguages = otherAgent.GetTraits<T_Language>().ToList();

			if (myLanguages.Select(myLang => myLang.TextName).Intersect(
					yourLanguages.Select(yourLang => yourLang.TextName)).Any())
				__result = true;

			return;
		}

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(Agent.FindSpeed))]
		public static bool FindSpeed_Prefix(Agent __instance, ref int __result)
        {
			if (__instance.HasTrait<Immobile>())
            {
				__result = 0;
				return false;
            }

			return true;
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

        [HarmonyPostfix, HarmonyPatch(methodName: nameof(Agent.Interact), argumentTypes: new[] { typeof(Agent) })]
		public static void Interact_Prefix(Agent otherAgent, Agent __instance)
        {
			TraitManager.LogTraitList(__instance); // Leave it, you'll need it

			logger.LogDebug("Inventory: ");
			foreach (InvItem ii in __instance.inventory.InvItemList)
				logger.LogDebug(ii.invItemName);

			// InteractingAgent is not set yet, so you'll have to do that ad hoc here if you want IA's trait list
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
						P_AgentInteractions.SafecrackSafe(__instance, causerAgent, extraObject);
					else if (myAction == CJob.TamperSomething)
						P_AgentInteractions.TamperSomething(__instance, causerAgent, extraObject);

					___noMoreObjectActions = false;
				}

				return false;
			}

			return true;
		}

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(Agent.SetBrainActive))]
		public static bool SetBrainActive_Prefix(Agent __instance, ref bool isActive)
        {
			if (__instance.HasTrait<Brainless>())
            {
				isActive = false;
				__instance.brain.active = false;
				__instance.interactable = false;
			}

			return true;
        }

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(Agent.SetupAgentStats), argumentTypes: new[] { typeof(string) })]
		public static void SetupAgentStats_Postfix(string transformationType, Agent __instance)
		{
			foreach (ISetupAgentStats trait in __instance.GetTraits<ISetupAgentStats>())
				trait.SetupAgentStats(__instance);

			if (!__instance.GetTraits<T_GibType>().Any())
				__instance.AddTrait<Meat_Chunks>();

			Language.AddLangsToVanillaAgents(__instance);

			if (GC.challenges.Contains(nameof(Homesickness_Disabled)))
				__instance.canGoBetweenLevels = true;
			else if (GC.challenges.Contains(nameof(Homesickness_Mandatory)))
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
		public bool SceneSetterFinished = false; // Avoids removal from series mid-traversal

		public bool WalkieTalkieUsed;
		public bool PermanentHire;
		public int SuicideVestTimer;

		protected override void Initialize() { }
	}
}
