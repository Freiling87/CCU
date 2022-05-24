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
using CCU.Traits.Relationships;

namespace CCU.Patches.Agents
{
	[HarmonyPatch(declaringType: typeof(Agent))]
	public class P_Agent
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(Agent.CanShakeDown))]
		public static void CanShakeDown_Postfix(Agent __instance, ref bool __result)
		{
			if (!__instance.oma.shookDown && !GC.loadLevel.LevelContainsMayor() &&  __instance.HasTrait<Extortable>())
				__result = true;
		}

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(Agent.GetCodeFromJob), argumentTypes: new[] { typeof(int) })]
		public static bool GetCodeFromJob_Prefix(int jobInt, ref string __result)
		{
			string result = "";

			switch (jobInt)
			{
				case 0:
					result = "None";
					break;
				case 1:
					result = "Follow";
					break;
				case 2:
					result = "GoHere";
					break;
				case 3:
					result = "Ruckus";
					break;
				case 4:
					result = "Attack";
					break;
				case 5:
					result = "LockpickDoor";
					break;
				case 6:
					result = "HackSomething";
					break;
				case 7:
					result = "UseToilet";
					break;
				case 8:
					result = "GetSupplies";
					break;
				case 9:
					result = "GetDrugs";
					break;
				case 10:
					result = "GetDrink";
					break;
				case 11:
					result = "UseATM";
					break;
				case 12:
					result = CJob.SafecrackSafe;
					break;
				case 13:
					result = CJob.TamperSomething;
					break;
			}

			__result = result;
			return false;
		}

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(Agent.GetJobCode), argumentTypes: new[] { typeof(string) })]
		public static bool GetJobCode_Prefix(string jobString, ref jobType __result)
		{
			jobType result = jobType.None;

			if (jobString == "Attack")
				result = jobType.Attack;
			else if (jobString == "Follow")
				result = jobType.Follow;
			else if (jobString == "GetDrink")
				result = jobType.GetDrink;
			else if (jobString == "GetDrugs")
				result = jobType.GetDrugs;
			else if (jobString == "GetSupplies")
				result = jobType.GetSupplies;
			else if (jobString == "GoHere")
				result = jobType.GoHere;
			else if (jobString == "HackSomething")
				result = jobType.HackSomething;
			else if (jobString == "LockpickDoor")
				result = jobType.LockpickDoor;
			else if (jobString == "Ruckus")
				result = jobType.Ruckus;
			else if (jobString == "UseATM")
				result = jobType.UseATM;
			else if (jobString == "UseToilet")
				result = jobType.UseToilet;
			else if (jobString == CJob.SafecrackSafe)
				result = jobType.GetSupplies; // TODO
			else if (jobString == CJob.TamperSomething)
				result = jobType.GetSupplies; // TODO
			else if (jobString != null && jobString.Length == 0)
				result = jobType.None;

			__result = result;
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

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(Agent.SetupAgentStats), argumentTypes: new[] { typeof(string) })]
		public static void SetupAgentStats_Postfix(string transformationType, Agent __instance)
		{
			# region Active
			// TODO: Change this to a Linq OfType expression
			// RogueFramework.Unlocks.OfType<ACtiveTrait> etc.
            if (TraitManager.HasTraitFromList(__instance, TraitManager.BehaviorActiveTraits))
			{
				// Thieves have their LOScheck set to 50% in vanilla
				if (__instance.HasTrait<Pickpocket>() && GC.percentChance(50))
					return;

				// All others excluded
				__instance.losCheckAtIntervals = true;
			}

			if (__instance.HasTrait<SeekAndDestroy>())
				__instance.killerRobot = true;
			#endregion
			#region Interaction
			if (__instance.HasTrait<Go_Haywire>() ||
				__instance.HasTrait<Tamper_with_Aim>())
				__instance.hackable = true;
            #endregion
            #region Combat
            if (__instance.HasTrait<DrugWarrior>())
				__instance.combat.canTakeDrugs = true;
			#endregion
			#region Merchant
			if (TraitManager.HasTraitFromList(__instance, TraitManager.MerchantTypeTraits))
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

			if (__instance.HasTrait<Zombie_Infected>())
				__instance.zombieWhenDead = true;
			#endregion
			#region Relationships
			if (__instance.HasTrait<Relationless>())
				__instance.dontChangeRelationships = true;
            #endregion

            if (GC.challenges.Contains(CMutators.HomesicknessDisabled))
				__instance.canGoBetweenLevels = true;
			else if (GC.challenges.Contains(CMutators.HomesicknessMandatory))
				__instance.canGoBetweenLevels = false;
		}
	}
}
