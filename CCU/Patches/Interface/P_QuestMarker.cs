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
using CCU.Traits.AI.Hire;
using CCU.Traits.AI.Vendor;
using CCU.Traits.AI.TraitTrigger;
using Google2u;
using CCU.Traits.AI.TraitTrigger;

namespace CCU.Patches.Interface
{
	[HarmonyPatch(declaringType: typeof(QuestMarker))]
	class P_QuestMarker
	{
		[HarmonyPostfix, HarmonyPatch(methodName: "CheckIfSeen2")]
		public static void CheckifSeen2_Postfix(QuestMarker __instance)
		{
			Agent agent = __instance.tr.parent.GetComponent<Agent>();

			if (TraitManager.HasTraitFromList(agent, TraitManager.MapMarkerTraits) && __instance.playerSeen)
			{
				__instance.nonQuestAgentSprite = __instance.gr.minimapImages[14];
				__instance.smallImage2.sprite = __instance.nonQuestAgentSprite;
				__instance.smallImage2.color = __instance.vis;
				__instance.colorVis = true;
				__instance.colorInvis = false;
				__instance.questMarkerSmall2.myText.text = agent.agentRealName;
			}
		}
	}
}
