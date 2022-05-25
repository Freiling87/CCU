using BepInEx.Logging;
using CCU.Mutators;
using CCU.Traits.Bodyguarded;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CCU.Patches.Spawn
{
    [HarmonyPatch(declaringType: typeof(LoadLevel))]
    public static class P_LoadLevel
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(LoadLevel.loadStuff2))]
		public static bool LoadStuff2_Prefix(LoadLevel __instance)
		{
			if (GC.customCampaign && MutatorManager.IsMutatorFromListActive(MutatorManager.CampaignMutators))
			{
				foreach (LevelData levelData in __instance.customCampaign.levelList)
				{
					foreach (string mutator in levelData.levelMutators)
					{
						foreach (Type ccuMutator in MutatorManager.CampaignMutators)
						{
							string ccuMutatorName = MutatorManager.MutatorName(ccuMutator);

							if (mutator == ccuMutatorName)
							{
								logger.LogDebug("There is definitely a better way to do this! :)");
							}
						}
					}
				}

			}

			return true;
		}

        //[HarmonyPostfix, HarmonyPatch(methodName: "SetupMore3_3")]
        public static void SetupMore3_3_Postfix(LoadLevel __instance)
		{
			Core.LogMethodCall();
			List<Agent> vips = new List<Agent>();

			foreach (Agent agent in GC.agentList)
				if (agent.GetTraits<T_Bodyguarded>().Any() && agent.gang == 0)
					vips.Add(agent);

			logger.LogDebug("\tVIP count: " + vips.Count());

			if (vips.Count() > 0)
				foreach(Agent vip in vips)
				{
					logger.LogDebug("Processing Bodyguarded Agent :" + vip.agentName);

					T_Bodyguarded bodyguardTrait = vip.GetTrait<T_Bodyguarded>();
					Vector2 spawnLoc;
					int attempts = 0;

					do
						spawnLoc = GC.tileInfo.FindLocationNearLocation(vip.tr.position, null, 0.32f, 1.28f, true, true);
					while (spawnLoc == Vector2.zero && attempts++ < 300);

					logger.LogDebug("\tValid SpawnLoc: " + (spawnLoc != Vector2.zero));

					if (spawnLoc != Vector2.zero)
					{
						Agent.gangCount++;
						vip.gang = Agent.gangCount;
						vip.modLeashes = 0;
						//Random.InitState(__instance.randomSeedNum);
						int numGuards = Random.Range(2, 4);

						for (int i = 0; i < numGuards; i++)
						{
							logger.LogDebug("\tBodyguard Trait: " + bodyguardTrait.TextName);
							logger.LogDebug("\tBodyguard Type: " + bodyguardTrait.BodyguardType);

							Agent bodyguard = GC.spawnerMain.SpawnAgent(spawnLoc, null, bodyguardTrait.BodyguardType);
							bodyguard.movement.RotateToAngleTransform(Random.Range(0f, 360f));
							bodyguard.SetDefaultGoal("WanderFar");
							bodyguard.gang = vip.gang;
							bodyguard.modLeashes = 0;
							bodyguard.modVigilant = 0;
							bodyguard.relationships.SetRelInitial(vip, "Aligned");
							vip.relationships.SetRelInitial(bodyguard, "Aligned");

							foreach (Agent gangmember in vip.gangMembers)
							{
								bodyguard.relationships.SetRelInitial(gangmember, "Aligned");
								gangmember.relationships.SetRelInitial(bodyguard, "Aligned");
							}

							vip.gangMembers.Add(bodyguard);
							bodyguard.gangMembers.Add(vip);
						}
					}
				}
		}
    }
}
