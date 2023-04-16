using BepInEx.Logging;
using CCU.Hooks;
using HarmonyLib;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CCU.Traits
{
	// TODO: When you switch to C# 8.0, use default implementation of some methods to reduce repetition across children of this interface.
	public interface IModifyItems
	{
		List<string> EligibleItemTypes { get; }
		List<string> ExcludedItems { get; }
		bool IsEligible(Agent agent, InvItem invItem); // Make this private when you upgrade
		void OnDrop(Agent agent, InvItem invItem);
		void OnPickup(Agent agent, InvItem invItem);
	}

	[HarmonyPatch(declaringType: typeof(InvDatabase))]
	public static class P_InvDatabase
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		//[HarmonyPriority(Priority.First)]
		//[HarmonyPostfix, HarmonyPatch(methodName: nameof(InvDatabase.AddItem), argumentTypes: new[] { typeof(string), typeof(int), typeof(List<string>), typeof(List<int>), typeof(List<int>), typeof(int), typeof(bool), typeof(bool), typeof(int), typeof(int), typeof(bool), typeof(string), typeof(bool), typeof(int), typeof(bool), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(bool) })]
		//public static void AddItem_Postfix(InvDatabase __instance, ref InvItem __result)
		//{
		//	CCULogger.GetLogger().LogDebug("AddItem (A): " + (__instance.agent?.agentRealName ?? "(Null)") + ", " + __result.invItemName);
		//	ModifyItemHelper.SetupItem(__instance.agent, __result);
		//}
		//// → C

		[HarmonyPriority(Priority.First)]
		[HarmonyPostfix, HarmonyPatch(methodName: nameof(InvDatabase.AddItemAtEmptySlot), argumentTypes: new[] { typeof(InvItem), typeof(bool), typeof(bool), typeof(int), typeof(int) })]
		public static void AddItemAtEmptySlot_Postfix(InvDatabase __instance, ref InvItem item)
		{
			//logger.LogDebug("AddItemAtEmptySlot: " + item.invItemName);
			Agent agent = __instance.agent?? null;

			if (agent is null || agent.agentName != VanillaAgents.CustomCharacter ||
				item is null || item.invItemName is null || item.invItemName == "")
				return;

			foreach (IModifyItems trait in agent.GetTraits<IModifyItems>())
			{
				//logger.LogDebug("\ttrait:\t" + trait.ToString());
				trait.OnPickup(agent, item);
			}
		}
	}

	public static class ModifyItemHelper
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static void SetupInventory(Agent agent)
		{
			//logger.LogDebug("SetupInventory");

			if (agent is null || agent.agentName != VanillaAgents.CustomCharacter)
				return;

			foreach (IModifyItems trait in agent.GetTraits<IModifyItems>())
			{
				//logger.LogDebug("\ttrait:\t" + trait.ToString());
				foreach (InvItem invItem in agent.inventory.InvItemList)
				{
					//logger.LogDebug("\t\titem:\t" + invItem.invItemName);
					trait.OnPickup(agent, invItem);
					// if initial setup, Ammo Stock should raise ammo to max for NPCs
				}
			}
		}
	}
}