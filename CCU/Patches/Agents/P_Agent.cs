using BepInEx.Logging;
using BTHarmonyUtils.InstructionSearch;
using BTHarmonyUtils.MidFixPatch;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Hooks;
using CCU.Localization;
using CCU.Traits;
using CCU.Traits.Behavior;
using CCU.Traits.Passive;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Patches.Agents
{
	// TODO: Move to BunnyLib
	[HarmonyPatch(declaringType: typeof(Agent))]
	public class P_Agent
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		#region Agent Info Logging
		[HarmonyPostfix, HarmonyPatch(methodName: nameof(Agent.Interact), argumentTypes: new[] { typeof(Agent) })]
		public static void Interact_Prefix(Agent __instance)
		{
			bool logAgent = true;
			bool logInteractingAgent = true;

			if (logAgent)
				DoAllLogging(__instance);

			if (logInteractingAgent)
			{
				Agent interactingAgent = __instance.interactingAgent;
				if (interactingAgent is null)
					return;

				DoAllLogging(interactingAgent);
			}
		}
		public static void DoAllLogging(Agent agent)
		{
			logger.LogDebug(Environment.NewLine
					+ "||||||||||| AGENT INFO DUMP: " + agent.agentRealName + " " + " (" + agent.agentID + ") ".PadRight(24, '|') + Environment.NewLine
					+ LogAppearance(agent) + Environment.NewLine
					+ LogInventory(agent) + Environment.NewLine
					+ LogShopInventory(agent) + Environment.NewLine
					+ LogTraitsDesigner(agent) + Environment.NewLine
					+ LogTraitsPlayer(agent) + Environment.NewLine
					+ LogStatusEffects(agent) + Environment.NewLine
					);
		}
		public static string LogAppearance(Agent agent)
		{
			AgentHitbox agentHitbox = agent.tr.GetChild(0).transform.GetChild(0).GetComponent<AgentHitbox>();

			string log = "======= Appearance ==============="
				+ "\n\t- Accessory  :\t" + agent.inventory.startingHeadPiece
				+ "\n\t- Body Color :\t" + agentHitbox.bodyColor.ToString()
				+ "\n\t- Body Type  :\t" + agent.objectMult.bodyType  // dw
				+ "\n\t- Eye Color  :\t" + agentHitbox.eyesColor.ToString()
				+ "\n\t- Eye Type   :\t" + agentHitbox.eyesStrings[1]
				+ "\n\t- Facial Hair:\t" + agentHitbox.facialHairType
				+ "\n\t- FH Position:\t" + agentHitbox.facialHairPos
				+ "\n\t- Hair Color :\t" + agentHitbox.hairColorName
				+ "\n\t- Hair Style :\t" + agentHitbox.hairType
				+ "\n\t- Legs Color :\t" + agentHitbox.legsColor.ToString()
				+ "\n\t- Skin Color :\t" + agentHitbox.skinColorName;

			return log;
		}
		private static string LogInventory(Agent agent)
		{
			string log = "======= Inventory ===============";

			foreach (InvItem ii in agent.inventory.InvItemList.Where(i => !(i.invItemName is null) && i.invItemName != ""))
			{
				log += "\n\t- " + ii.invItemName.PadRight(20) + "(" + ii.invItemCount.ToString().PadLeft(3) + " / " + ii.maxAmmo.ToString().PadLeft(3) + ")";
				foreach (string mod in ii.contents) // Includes special abilities like DR I guess
					log += "\n\t\t- " + mod;
			}

			return log;
		}
		private static string LogShopInventory(Agent agent)
		{
			string log = "";

			if (agent.specialInvDatabase?.InvItemList.Any() ?? false)
			{
				log += "======= Shop Inventory ==========";

				// Name check prevents bug that breaks shops. Purchased items are not removed from the list but their name is nulled.
				foreach (InvItem ii in agent.specialInvDatabase.InvItemList.Where(i => !(i.invItemName is null))) 
					log += "\n\t- " + ii.invItemName.PadRight(20) + "* " + ii.invItemCount;
			}

			return log;
		}
		public static string LogTraitsDesigner(Agent agent)
		{
			string log = "======= Traits (Designer) =======";

			foreach (Trait trait in T_CCU.DesignerTraitList(agent.statusEffects.TraitList))
				log += "\n\t- " + trait.traitName;

			return log;
		}
		public static string LogTraitsPlayer(Agent agent)
		{
			string log = "======= Traits (Player) =========";

			foreach (Trait trait in T_CCU.PlayerTraitList(agent.statusEffects.TraitList))
				log += "\n\t- " + trait.traitName;

			return log;
		}
		public static string LogStatusEffects(Agent agent)
		{
			string log = "======= Status  Effects =========";

			foreach (StatusEffect se in agent.statusEffects.StatusEffectList)
				log += "\n\t- " + se.statusEffectName;

			return log;
		}
		#endregion

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
			if (__instance.GetOrAddHook<H_Agent>().HiredPermanently &&
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
			if (__instance.GetOrAddHook<H_Agent>().HiredPermanently &&
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

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(Agent.SetTraversable))]
		public static bool SetTraversable_AccidentProne(Agent __instance, ref string type)
		{
			if (__instance.HasTrait<Accident_Prone>())
				type = "TraverseAll";

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
			__instance.GetOrAddHook<H_Agent>().Reset();

			return true;
		}
	}
}
