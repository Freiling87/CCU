using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueLibsCore;
using HarmonyLib;
using System.Reflection;
using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using System.Reflection.Emit;
using CCU.Traits;
using Random = UnityEngine.Random;
using CCU.Patches.Appearance;

namespace CCU.Patches.Appearance
{
	[HarmonyPatch(declaringType: typeof(AgentHitbox))]
	public static class P_AgentHitbox
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		//[HarmonyPrefix, HarmonyPatch(methodName: nameof(AgentHitbox.chooseFacialHairType), argumentTypes: new[] { typeof(string) })]
		public static bool ChooseFacialHairType_Prefix(string agentName, AgentHitbox __instance, ref string __result)
		{
			Agent agent = __instance.agent;

			if (TraitManager.HasTraitFromList(agent, TraitManager.FacialHairTraits))
			{
				Appearance.RollFacialHair(__instance);
			}
			
			if (__instance.canHaveFacialHair)
			{
				if (__instance.hasSetup)
					goto IL_28B;
			
				if (agent.agentName == "Werewolf")
					__instance.facialHairType = "Beard";
					goto IL_28B;

#pragma warning disable CS0162 // Unreachable code detected
                try
				{
					if ((GC.serverPlayer || GC.clientControlling || agent.localPlayer || !(agent.name != "DummyAgent")) && 
						agent.agentName != "Custom")
					{
						__instance.facialHairType = GC.rnd.RandomSelect("FacialHairStyles", "HairTypes");
						agent.oma.facialHairType = agent.oma.convertFacialHairTypeToInt(__instance.facialHairType);
					}
					else
						__instance.facialHairType = agent.oma.convertIntToFacialHairType(agent.oma.facialHairType);
					
					goto IL_28B;
				}
				catch
				{
					__instance.facialHair.gameObject.SetActive(false);
					__instance.facialHairWB.gameObject.SetActive(false);

					goto IL_28B;
				}
#pragma warning restore CS0162 // Unreachable code detected
            }

			__instance.facialHair.gameObject.SetActive(false);
			__instance.facialHairWB.gameObject.SetActive(false);
			__instance.facialHairType = "None";

		IL_28B:
			if (__instance.facialHairType == "None" || __instance.facialHairType == "" || __instance.facialHairType == null)
			{
				__instance.facialHair.gameObject.SetActive(false);
				__instance.facialHairWB.gameObject.SetActive(false);
			}
			else
			{
				__instance.facialHair.gameObject.SetActive(true);
				__instance.facialHairWB.gameObject.SetActive(true);
			}

			__result = __instance.facialHairType;
			return true;
		}
	}
}