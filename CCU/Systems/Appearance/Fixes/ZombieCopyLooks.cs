using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCU.Systems.Appearance.Fixes
{
	[HarmonyPatch(typeof(Relationships))]
	internal static class P_Relationships_Appearance
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		// TODO: Fix CopyLooks so it pulls from the rolled appearance

		//[HarmonyPostfix, HarmonyPatch(typeof(Relationships), nameof(Relationships.CopyLooks))]
		private static void CopyLooks(Relationships __instance, Agent otherAgent)
		{
			//	This is a postfix so that Vanilla first handles any that wouldn't apply below.

			//	New Zombie spawn, or original agent exiting Werewolf form or Mech
			Agent spawnedAgent = (Agent)AccessTools.DeclaredField(typeof(Relationships), "agent").GetValue(__instance);

			//	Copy of vanilla: Replace as you go
			//	// if this one is NOT covered in RollAppearance.
			spawnedAgent.agentHitboxScript.skinColorName = otherAgent.agentHitboxScript.skinColorName;
			spawnedAgent.agentHitboxScript.hairColorName = otherAgent.agentHitboxScript.hairColorName;
			spawnedAgent.agentHitboxScript.facialHairColorName = otherAgent.agentHitboxScript.facialHairColorName;
			spawnedAgent.agentHitboxScript.skinColor = otherAgent.agentHitboxScript.skinColor;
			spawnedAgent.agentHitboxScript.hairColor = otherAgent.agentHitboxScript.hairColor;
			spawnedAgent.agentHitboxScript.facialHairColor = otherAgent.agentHitboxScript.facialHairColor;
			spawnedAgent.agentHitboxScript.bodyColor = otherAgent.agentHitboxScript.bodyColor;
			spawnedAgent.agentHitboxScript.legsColor = otherAgent.agentHitboxScript.legsColor;
			spawnedAgent.agentHitboxScript.hairType = otherAgent.agentHitboxScript.hairType;
			spawnedAgent.agentHitboxScript.facialHairType = otherAgent.agentHitboxScript.facialHairType;
			spawnedAgent.agentHitboxScript.headPieceType = otherAgent.agentHitboxScript.headPieceType;
			spawnedAgent.agentHitboxScript.canHaveFacialHair = otherAgent.agentHitboxScript.canHaveFacialHair;
			spawnedAgent.agentHitboxScript.cantShowHairUnderHeadPiece = otherAgent.agentHitboxScript.cantShowHairUnderHeadPiece;
			spawnedAgent.inventory.defaultArmorHead = otherAgent.inventory.defaultArmorHead;
			spawnedAgent.agentHitboxScript.facialHair.gameObject.SetActive(otherAgent.agentHitboxScript.facialHair.gameObject.activeSelf);
			spawnedAgent.agentHitboxScript.facialHairWB.gameObject.SetActive(otherAgent.agentHitboxScript.facialHairWB.gameObject.activeSelf);
			spawnedAgent.agentOriginalName = otherAgent.agentOriginalName;
			spawnedAgent.agentHitboxScript.SetupBodyStrings();
			spawnedAgent.agentHitboxScript.MustRefresh();
			spawnedAgent.agentHitboxScript.UpdateAnim();
			spawnedAgent.melee.realArm2.enabled = true;
		}
	}
}
