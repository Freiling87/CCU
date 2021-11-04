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
using CCU.Mutators;

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
				if (TraitManager.HasTraitFromList(agent, TraitManager.BodyguardedTraits) && agent.gang == 0)
					vips.Add(agent);

			logger.LogDebug("\tVIP count: " + vips.Count());

			if (vips.Count() > 0)
				foreach(Agent vip in vips)
				{
					logger.LogDebug("Processing Bodyguarded Agent :" + vip.agentName);

					Type bodyguardTrait = TraitManager.GetOnlyTraitFromList(vip, TraitManager.BodyguardedTraits);
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
							string bodyguardType =
								bodyguardTrait == typeof(Bodyguarded_Pilot) ? "Guard" :
								"Assassin";

							logger.LogDebug("\tBodyguard Trait: " + bodyguardTrait.Name);
							logger.LogDebug("\tBodyguard Type: " + bodyguardType);

							Agent bodyguard = GC.spawnerMain.SpawnAgent(spawnLoc, null, bodyguardType);
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
