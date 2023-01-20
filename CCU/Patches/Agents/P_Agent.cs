using BepInEx.Logging;
using BTHarmonyUtils.InstructionSearch;
using BTHarmonyUtils.MidFixPatch;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Challenges.Followers;
using CCU.Localization;
using CCU.Traits;
using CCU.Traits.App;
using CCU.Traits.Behavior;
using CCU.Traits.Gib_Type;
using CCU.Traits.Hire_Duration;
using CCU.Traits.Passive;
using CCU.Traits.Player.Language;
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
			if (__instance.HasTrait<Extortable>())
            {
				if (__instance.oma.shookDown || GC.loadLevel.LevelContainsMayor())
				{
					__result = false;
					return;
				}
				else
					__result = true;

				foreach (Agent agent in GC.agentList)
					if (agent.startingChunk == __instance.startingChunk && agent.ownerID == __instance.ownerID && agent.ownerID != 255 && agent.ownerID != 99 && __instance.ownerID != 0 && agent.oma.shookDown)
						__result = false;
			}
		}

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(Agent.CanUnderstandEachOther))]
		public static void CanUnderstandEachOther_Postfix(Agent __instance, Agent otherAgent, ref bool __result)
		{
			if (__result is false && 
				!__instance.statusEffects.hasStatusEffect(VStatusEffect.HearingBlocked) &&
                !otherAgent.statusEffects.hasStatusEffect(VStatusEffect.HearingBlocked) &&
				Language.HaveSharedLanguage(__instance, otherAgent))
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
			TraitManager.LogTraitList(__instance);

			logger.LogDebug("------- Inventory");
			foreach (InvItem ii in __instance.inventory.InvItemList)
				logger.LogDebug(ii.invItemName + "(" + ii.invItemCount + ")");

			if (!(__instance.specialInvDatabase is null))
            {
				logger.LogDebug("------- Special Inventory:");
				foreach (InvItem ii in __instance.specialInvDatabase.InvItemList)
					logger.LogDebug(ii.invItemName + "(" + ii.invItemCount + ")");
			}

			if (Core.debugMode && __instance.agentName == VanillaAgents.CustomCharacter)
				AppearanceTools.LogAppearance(__instance);
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

        [HarmonyPrefix, HarmonyPatch(methodName: nameof(Agent.Say), argumentTypes: new Type[] { typeof(string), typeof(bool) })]
		public static bool Say_Prefix(ref string myMessage)
        {
			if (myMessage == "E_CantHeal")
				myMessage = "Doctor_CantHeal";

			return true;
        }

        //[HarmonyPrefix, HarmonyPatch(methodName: nameof(Agent.SayDialogue), argumentTypes: new Type[] { typeof(bool), typeof(string), typeof(bool), typeof(NetworkInstanceId) })]
		public static bool SayDialogue_PrefixLogging(Agent __instance, string type)
        {
			logger.LogDebug("SayDialogue_Prefix");
			logger.LogDebug("AgentName: " + __instance.agentName);
			logger.LogDebug("Type: " + type);

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

        [HarmonyPrefix, HarmonyPatch(methodName: nameof(Agent.SetEmployer))]
		public static bool SetEmployer_Prefix(Agent __instance, ref Agent myEmployer)
		{
			if (__instance.GetOrAddHook<P_Agent_Hook>().HiredPermanently &&
				!(__instance.employer is null) && myEmployer is null)
            {
				myEmployer = __instance.employer;
				__instance.job = "Follow";
				__instance.jobCode = jobType.Follow;
				__instance.StartCoroutine(__instance.ChangeJobBig(""));
				__instance.oma.cantDoMoreTasks = false;
			}

			return true;
        }

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(Agent.SetFollowing))]
		public static bool SetFollowing_Prefix(Agent __instance, ref Agent myFollowing)
		{
			if (__instance.GetOrAddHook<P_Agent_Hook>().HiredPermanently &&
				!(__instance.following is null) && myFollowing is null)
            {
				myFollowing = __instance.employer;
				__instance.job = "Follow";
				__instance.jobCode = jobType.Follow;
				__instance.StartCoroutine(__instance.ChangeJobBig(""));
				__instance.oma.cantDoMoreTasks = false;
			}

			return true;
		}

		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(Agent.SetupAgentStats))]
		private static IEnumerable<CodeInstruction> SetupAgentStats_LegacyUpdater(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo traits = AccessTools.DeclaredField(typeof(SaveCharacterData), nameof(SaveCharacterData.traits));
			MethodInfo updateTraitList = AccessTools.DeclaredMethod(typeof(Legacy), nameof(Legacy.UpdateTraitList));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, traits),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, updateTraitList),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(Agent.SetupAgentStats), argumentTypes: new[] { typeof(string) })]
		public static void SetupAgentStats_Postfix(string transformationType, Agent __instance)
		{
			foreach (ISetupAgentStats trait in __instance.GetTraits<ISetupAgentStats>())
				trait.SetupAgentStats(__instance);

			if (!__instance.GetTraits<T_GibType>().Any())
				__instance.AddTrait<Meat_Chunks>();

			Language.AddLangsToVanillaAgents(__instance);

			// Negatives allow traits to take precedence over mutators.
			if ((GC.challenges.Contains(nameof(Homesickness_Disabled))  && !__instance.HasTrait<Homesickly>()) ||
				__instance.HasTrait<Homesickless>())
				__instance.canGoBetweenLevels = true;
			else if ((GC.challenges.Contains(nameof(Homesickness_Mandatory)) && !__instance.HasTrait<Homesickless>()) ||
				__instance.HasTrait<Homesickly>())
				__instance.canGoBetweenLevels = false;
		}

		[BTHarmonyMidFix(nameof(SetupAgentStats_InitCustomCharacter_Matcher))]
		[HarmonyPatch(methodName: nameof(Agent.SetupAgentStats), argumentTypes: new[] { typeof(string) })]
		private static void SetupAgentStats_InitCustomCharacter_MidFix(Agent __instance) {
			// melee- and gunSkill control:
			// - how frequently the agent performs actions,
			// - how likely he is to attack,
			// - how aggressively he performs in combat
			__instance.modGunSkill = Math.Min(2, __instance.customCharacterData.accuracy); // max supported gunSkill is 2
			__instance.modMeleeSkill = Math.Min(2, __instance.customCharacterData.strength); // max supported meleeSkill is 2
		}

		private static MidFixInstructionMatcher SetupAgentStats_InitCustomCharacter_Matcher() {
			FieldInfo field_customCharacterData = AccessTools.DeclaredField(typeof(Agent), nameof(Agent.customCharacterData));
			FieldInfo field_strength = AccessTools.DeclaredField(typeof(SaveCharacterData), nameof(SaveCharacterData.strength));

			return new MidFixInstructionMatcher(
					expectedMatches: 1,
					postfixInstructionSequence: new[] {
							InstructionMask.MatchOpCode(OpCodes.Ldarg_0),
							InstructionMask.MatchOpCode(OpCodes.Ldarg_0),
							InstructionMask.MatchInstruction(OpCodes.Ldfld, field_customCharacterData),
							InstructionMask.MatchInstruction(OpCodes.Ldfld, field_strength),
							InstructionMask.MatchOpCode(OpCodes.Call),
					}
			);
		}

		[HarmonyPrefix, HarmonyPatch(methodName: "Start")]
		public static bool Start_CreateHook(Agent __instance)
		{
			__instance.GetOrAddHook<P_Agent_Hook>().Reset();

			return true;
		}
	}

	public class P_Agent_Hook : HookBase<PlayfieldObject>
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		protected override void Initialize() 
		{
			//Core.LogMethodCall();
			GrabAppearance(); 
			appearanceRolled = false;
			SceneSetterFinished = false; // Avoids removal from series mid-traversal
		}

		public void GrabAppearance()
		{
			//Core.LogMethodCall();
			Agent agent = (Agent)Instance;
			//logger.LogDebug("Agent: " + agent.agentRealName);
			SaveCharacterData save = agent.customCharacterData;
			bodyColor = save.bodyColorName;
			bodyType = save.bodyType;
			eyesType = save.eyesType;
			skinColor = save.skinColorName;
		}

		public void Reset()
        {
			ClassifierScannedAgents.Clear();
        }

		public bool SceneSetterFinished;

		public bool WalkieTalkieUsed;
		public bool HiredPermanently;
		public int SuicideVestTimer;

		public bool appearanceRolled;
		public string bodyColor;
		public string bodyType;
		public string eyesType;
		public string skinColor;

		public List<string> ClassifierScannedAgents = new List<string> { };
	}
}
