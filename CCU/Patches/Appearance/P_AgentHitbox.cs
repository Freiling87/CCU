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

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(AgentHitbox.chooseFacialHairType), argumentTypes: new[] { typeof(string) })]
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

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(AgentHitbox.SetupFeatures))]
		public static bool SetupFeatures_Prefix(AgentHitbox __instance)
		{
			Agent agent = __instance.agent;

			if (GC.streamingWorld && agent.isPlayer == 0)
				Random.InitState(agent.streamingChunkObjectID);
			
			__instance.MustRefresh();
			
			if (GC.challenges.Contains("NewCharacterEveryLevel") && agent.isPlayer != 0 && agent.localPlayer)
				__instance.hasSetup = false;
			
			if (agent.zombified)
			{
				if (agent.wasPossessed)
					__instance.hasSetup = false;
			
				if (agent.agentOriginalName == "" || agent.agentOriginalName == null || agent.agentOriginalName.Contains("Playerr"))
					agent.agentOriginalName = "Zombie";
				else if (GC.challenges.Contains("NewCharacterEveryLevel") && agent.isPlayer != 0 && !GC.loadComplete)
					agent.agentOriginalName = "Zombie";
			}

			bool flag = false;
			
			if (agent.agentName == "Custom" && agent.customCharacterData != null)
			{
				using (List<RandomElement>.Enumerator enumerator = GC.rnd.randomListTableStatic["NotHairStyles"].elementList.GetEnumerator())
					while (enumerator.MoveNext())
						if (enumerator.Current.rName == agent.customCharacterData.hairType)
							flag = true;
			}
			
			if ((agent.agentName == "Custom" && !TraitManager.HasTraitFromList(agent, TraitManager.AppearanceTraitsGroup)) ||
				agent.isPlayer != 0)
				__instance.hasSetup = true; // Bypasses vanilla appearance randomization
			
			if (__instance.hasSetup == false) 
			{
				__instance.canHaveFacialHair = false;
				__instance.cantShowHairUnderHeadPiece = false;
			
				__instance.chooseSkinColor(agent.agentName);
				
				if (!agent.zombified || agent.agentOriginalName == "Zombie")
					__instance.chooseHairType(agent.agentName);
				else if (agent.zombified && GC.challenges.Contains("NewCharacterEveryLevel") && agent.isPlayer != 0 && !GC.loadComplete)
					__instance.chooseHairType(agent.agentName);
			}

			if ((agent.agentName == "Custom") && agent.isPlayer != 0)
				__instance.chooseFacialHairType(agent.agentName);
			else if (!agent.zombified || agent.agentOriginalName == "Zombie")
				__instance.chooseFacialHairType(agent.agentName);
			else if (agent.zombified && GC.challenges.Contains("NewCharacterEveryLevel") && agent.isPlayer != 0 && !GC.loadComplete)
				__instance.chooseFacialHairType(agent.agentName);
			
			__instance.SetCantShowHairUnderHeadPiece();
			
			if (!__instance.hasSetup && (agent.agentName != "Custom" || (!flag && GC.levelType != "HomeBase")))
			{
				if (!agent.zombified || !(agent.agentOriginalName != "Zombie"))
					__instance.chooseHairColor(agent.agentName, false);
				else if (agent.zombified && GC.challenges.Contains("NewCharacterEveryLevel") && agent.isPlayer != 0 && !GC.loadComplete)
					__instance.chooseHairColor(agent.agentName, false);
			}
			
			if (agent.isPlayer > 0 && !__instance.hasSetup && agent.localPlayer && !GC.fourPlayerMode)
			{
				GC.sessionDataBig.hairType[agent.isPlayer] = __instance.hairType;
				GC.sessionDataBig.hairColor[agent.isPlayer] = __instance.hairColorName;
				GC.sessionDataBig.facialHairColor[agent.isPlayer] = __instance.facialHairColorName;
				GC.sessionDataBig.facialHairType[agent.isPlayer] = __instance.facialHairType;
				GC.sessionDataBig.skinColor[agent.isPlayer] = __instance.skinColorName;
				GC.sessionDataBig.hairColor32[agent.isPlayer] = __instance.hairColor;
				GC.sessionDataBig.facialHairColor32[agent.isPlayer] = __instance.facialHairColor;
				GC.sessionDataBig.skinColor32[agent.isPlayer] = __instance.skinColor;
			}

			try
			{
				__instance.objectSprite.SetRenderer("Off");
			}
			catch { }
			
			__instance.SetUsesNewHead();

			return false;
		}
	}
}
