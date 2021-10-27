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
			// Where is GangCount incremented? Might need to ++ it here
			Core.LogMethodCall();
			logger.LogDebug("TOTAL AGENT COUNT: " + GC.agentList.Count());

			foreach (Agent vipAgent in GC.agentList)
			{
				logger.LogDebug("Index: " + GC.agentList.IndexOf(vipAgent));
				if (TraitManager.HasTraitFromList(vipAgent, TraitManager.BodyguardedTraits))
				{
					Core.LogCheckpoint("A");

					Type bodyguardTrait = TraitManager.GetOnlyTraitFromList(vipAgent, TraitManager.BodyguardedTraits);
					Vector2 spawnLoc;
					int num81 = 0;

					Core.LogCheckpoint("B");

					do
					{
						spawnLoc = GC.tileInfo.FindLocationNearLocation(vipAgent.tr.position, null, 0.32f, 1.28f, true, true);
						num81++;
					}
					while (spawnLoc == Vector2.zero && num81 < 300);

					Core.LogCheckpoint("C");

					if (spawnLoc != Vector2.zero)
					{
						Agent.gangCount++;
						vipAgent.gang = Agent.gangCount;
						vipAgent.modLeashes = 0;
						Random.InitState(__instance.randomSeedNum);
						int numGuards = Random.Range(2, 4);

						Core.LogCheckpoint("D");

						for (int j = 0; j < numGuards; j++)
						{
							string bodyguardType;

							bodyguardType =
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
								Core.LogCheckpoint("E");

								if (gangmember.gang == vipAgent.gang)
								{
									bodyguard1.relationships.SetRelInitial(gangmember, "Aligned");
									gangmember.relationships.SetRelInitial(bodyguard1, "Aligned");

									if (!gangmember.gangMembers.Contains(bodyguard1))
										gangmember.gangMembers.Add(bodyguard1);

									if (!bodyguard1.gangMembers.Contains(gangmember))
										bodyguard1.gangMembers.Add(gangmember);
								}
							}
							Core.LogCheckpoint("F");
						}
						Core.LogCheckpoint("G");
					}
					Core.LogCheckpoint("H");
				}
				Core.LogCheckpoint("I");
			}
		}
    }
}
