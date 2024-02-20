using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Linq;

namespace CCU.Tools
{
	[HarmonyPatch(typeof(Agent))]
	public class Logging
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(nameof(Agent.Interact), new[] { typeof(Agent) })]
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
		public static string LogInventory(Agent agent)
		{
			string log = "======= Inventory ===============";

			foreach (InvItem ii in agent.inventory.InvItemList.Where(i => !(i.invItemName is null) && i.invItemName != ""))
			{
				log += $"\n\t- {ii.invItemName, 20} ({ii.invItemCount, 3}  / {ii.maxAmmo, 3})";

				foreach (string mod in ii.contents) // Includes special abilities like DR I guess
					log += $"\n\t\t- {mod}";
			}

			return log;
		}
		public static string LogShopInventory(Agent agent)
		{
			string log = "";

			if (agent.specialInvDatabase?.InvItemList.Any() ?? false)
			{
				log += "======= Shop Inventory ==========";

				// Name check prevents bug that breaks shops. Purchased items are not removed from the list but their name is nulled.
				foreach (InvItem ii in agent.specialInvDatabase.InvItemList.Where(i => !(i.invItemName is null)))
					log += $"\n\t- {ii.invItemName.PadRight(20)}\t*\t{ii.invItemCount}";
			}

			return log;
		}
		public static string LogTraitsDesigner(Agent agent)
		{
			string log = "======= Traits (Designer) =======";

			foreach (Trait trait in agent.statusEffects.TraitList.Where(t => TraitManager.IsDesignerTrait(t)))
				log += $"\n\t- {trait.traitName}";

			return log;
		}
		public static string LogTraitsPlayer(Agent agent)
		{
			string log = "======= Traits (Player) =========";

			foreach (Trait trait in agent.statusEffects.TraitList.Where(t => !TraitManager.IsDesignerTrait(t)))
				log += $"\n\t- {trait.traitName}";

			return log;
		}
		public static string LogStatusEffects(Agent agent)
		{
			string log = "======= Status  Effects =========";

			foreach (StatusEffect se in agent.statusEffects.StatusEffectList)
				log += $"\n\t- {se.statusEffectName}";

			return log;
		}
	}
}