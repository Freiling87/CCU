using System;
using RogueLibsCore;
using BepInEx.Logging;
using HarmonyLib;
using CCU.Traits.Relationships;
using CCU.Traits.Combat;
using UnityEngine;
using Random = UnityEngine.Random;
using CCU.Traits;
using CCU.Traits.Spawn;
using System.Linq;
using System.Collections.Generic;

namespace CCU.Patches.Spawn
{
    [HarmonyPatch(declaringType: typeof(LoadLevel))]
    public static class P_LoadLevel
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

        //[HarmonyPostfix, HarmonyPatch(methodName: "SetupMore3_3")]
        public static void SetupMore3_3_Postfix(LoadLevel __instance)
		{
			Core.LogMethodCall();
			List<Agent> vips = new List<Agent>();

			foreach (Agent agent in GC.agentList)
				if (TraitManager.HasTraitFromList(agent, TraitManager.BodyguardedTraits) && agent.gang == 0)
					vips.Add(agent);

			if (vips.Count() > 0)
				foreach(Agent vip in vips)
				{
					Type bodyguardTrait = TraitManager.GetOnlyTraitFromList(vip, TraitManager.BodyguardedTraits);
					Vector2 spawnLoc;
					int attempts = 0;

					do
						spawnLoc = GC.tileInfo.FindLocationNearLocation(vip.tr.position, null, 0.32f, 1.28f, true, true);
					while (spawnLoc == Vector2.zero && attempts++ < 300);

					if (spawnLoc != Vector2.zero)
					{
						Agent.gangCount++;
						vip.gang = Agent.gangCount;
						vip.modLeashes = 0;
						Random.InitState(__instance.randomSeedNum);
						int numGuards = Random.Range(2, 4);

						for (int i = 0; i < numGuards; i++)
						{
							string bodyguardType =
								bodyguardTrait == typeof(Bodyguarded_Pilot) ? "Guard" :
								"Assassin";

							Agent bodyguard1 = GC.spawnerMain.SpawnAgent(spawnLoc, null, bodyguardType);
							bodyguard1.movement.RotateToAngleTransform((float)Random.Range(0, 360));
							bodyguard1.SetDefaultGoal("WanderFar");
							bodyguard1.gang = Agent.gangCount;
							bodyguard1.modLeashes = 0;
							bodyguard1.modVigilant = 0;

							foreach (Agent gangmember in GC.agentList)
							{
								if (gangmember.gang == vip.gang)
								{
									bodyguard1.relationships.SetRelInitial(gangmember, "Aligned");
									gangmember.relationships.SetRelInitial(bodyguard1, "Aligned");

									if (!gangmember.gangMembers.Contains(bodyguard1))
										gangmember.gangMembers.Add(bodyguard1);

									if (!bodyguard1.gangMembers.Contains(gangmember))
										bodyguard1.gangMembers.Add(gangmember);
								}
							}
						}
					}
				}
		}
    }
}
