using CCU.Traits.Map_Marker;
using HarmonyLib;
using RogueLibsCore;
using System.Linq;

namespace CCU.Patches.Interface
{
    [HarmonyPatch(declaringType: typeof(QuestMarker))]
	class P_QuestMarker
	{
		[HarmonyPostfix, HarmonyPatch(methodName: "CheckIfSeen2")]
		public static void CheckifSeen2_Postfix(QuestMarker __instance)
		{
			if (__instance.myObject.CompareTag("Agent"))
			{
				Agent agent = __instance.tr.parent.GetComponent<Agent>();

				if (/*.nonQuestAgent && */ agent.GetTraits<T_MapMarker>().Any() && __instance.playerSeen)
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
}
