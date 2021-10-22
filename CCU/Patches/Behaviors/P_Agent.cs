using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using UnityEngine;
using RogueLibsCore;
using CCU.Traits.AI;
using Random = UnityEngine.Random;
using System.Reflection;
using CCU.Traits;
using CCU.Traits.AI.Active;
using CCU.Traits.AI.Interaction;
using CCU.Traits.AI.Combat;
using CCU.Traits.AI.Behavior;

namespace CCU.Patches.Behaviors
{
	[HarmonyPatch(declaringType: typeof(Agent))]
	public class P_Agent
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(Agent.CanShakeDown))]
		public static void CanShakeDown_Postfix(Agent __instance, ref bool __result)
		{
			if (!__instance.oma.shookDown && !GC.loadLevel.LevelContainsMayor() &&  __instance.HasTrait<Interaction_Extortable>())
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
			if (TraitManager.HasTraitFromList(__instance, TraitManager.ActiveTraits))
			{
				if (__instance.HasTrait<Behavior_Pickpocket>() && GC.percentChance(33))
					return;

				__instance.losCheckAtIntervals = true;
			}

			if (TraitManager.HasTraitFromList(__instance, TraitManager.VendorTypeTraits))
			{
				logger.LogDebug("Vendor found: " + TraitManager.GetOnlyTraitFromList(__instance, TraitManager.VendorTypeTraits));

				__instance.SetupSpecialInvDatabase();
			}

			if (__instance.HasTrait<Combat_UseDrugs>())
				__instance.combat.canTakeDrugs = true;

			if (__instance.HasTrait<Behavior_Guilty>())
				__instance.oma.mustBeGuilty = true;
		}
	}
}
