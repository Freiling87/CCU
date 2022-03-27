using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using BepInEx.Logging;
using System.Reflection;
using CCU.Content;

namespace CCU.Patches.Interface
{
	[HarmonyPatch(declaringType: typeof(LevelEditor))]
	public static class P_LevelEditor
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(LevelEditor.CreateGoalList))]
		public static bool CreateGoalList_Prefix(LevelEditor __instance, ref float ___numButtonsLoad)
		{
			// CURRENTLY VANILLA

			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			list.Add("None");
			//list2.Add("Arrested");
			//list2.Add("CommitArson");
			//list2.Add("Dead");
			//list2.Add("DeadBurned");
			//list2.Add("Explode");
			//list2.Add("KnockedOut");
			//list2.Add("Panic"); 
			//list2.Add("RobotClean"); // In Vanilla
			//list2.Add("WanderAgents");
			//list2.Add("WanderAgentsAligned");
			//list2.Add("WanderAgentsUnaligned");
			list2.Add("Idle");
			list2.Add("Guard");
			list2.Add("Patrol");
			list2.Add("Dance");
			list2.Add("IceSkate");
			list2.Add("Swim");
			list2.Add("ListenToJokeNPC");
			list2.Add("Joke");
			list2.Add("Sit");
			list2.Add("Sleep");
			list2.Add("CuriousObject");
			list2.Add("Wander");
			list2.Add("WanderInOwnedProperty");
			list2.Add("WanderFar");
			__instance.ActivateLoadMenu();
			___numButtonsLoad = (float)(list.Count + list2.Count);
			__instance.OpenObjectLoad(list, list2);
			__instance.StartCoroutine("SetScrollBarPlacement");
			return false;
		}

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(LevelEditor.CreateMutatorListLevel))]
		public static bool CreateMutatorList_Level(LevelEditor __instance, ref float ___numButtonsLoad)
		{
			List<string> list = new List<string>();

			list.AddRange(vMutator.VanillaMutators); // This list is copied from this method so it shouldn't break anything
			list.AddRange(CMutators.LevelOnlyMutators);

			__instance.ActivateLoadMenu(); 
			___numButtonsLoad = (float)list.Count;
			__instance.OpenObjectLoad(list);
			__instance.StartCoroutine("SetScrollbarPlacement");

			return false;
		}
	}
}