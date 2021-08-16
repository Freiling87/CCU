using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using BepInEx;
using UnityEngine;
using RogueLibsCore;
using BepInEx.Logging;
using HarmonyLib;

namespace CCU
{
    [HarmonyPatch(declaringType: typeof(Relationships))]
    public static class Relationships_Patches
	{
        [HarmonyPrefix, HarmonyPatch(methodName: nameof(Relationships.SetupRelationshipOriginal), argumentTypes: new Type[1] { typeof(Agent) })]
        public static bool SetupRelationshipOriginal_Prefix(Agent otherAgent, Relationships __instance, Agent ___agent)
        {
            if ((___agent.HasTrait(cTrait.Faction_1_Aligned) && otherAgent.HasTrait(cTrait.Faction_1_Aligned)) ||
                (___agent.HasTrait(cTrait.Faction_2_Aligned) && otherAgent.HasTrait(cTrait.Faction_2_Aligned)) ||
                (___agent.HasTrait(cTrait.Faction_3_Aligned) && otherAgent.HasTrait(cTrait.Faction_3_Aligned)) ||
                (___agent.HasTrait(cTrait.Faction_4_Aligned) && otherAgent.HasTrait(cTrait.Faction_4_Aligned)))
            {
                __instance.SetRelInitial(otherAgent, "Aligned");
                otherAgent.relationships.SetRelInitial(___agent, "Aligned");

                return false;
            }

            if ((___agent.HasTrait(cTrait.Faction_1_Hostile) && otherAgent.HasTrait(cTrait.Faction_1_Aligned)) ||
                (___agent.HasTrait(cTrait.Faction_1_Aligned) && otherAgent.HasTrait(cTrait.Faction_1_Hostile)) ||
                (___agent.HasTrait(cTrait.Faction_2_Hostile) && otherAgent.HasTrait(cTrait.Faction_2_Aligned)) ||
                (___agent.HasTrait(cTrait.Faction_2_Aligned) && otherAgent.HasTrait(cTrait.Faction_2_Hostile)) ||
                (___agent.HasTrait(cTrait.Faction_3_Hostile) && otherAgent.HasTrait(cTrait.Faction_3_Aligned)) ||
                (___agent.HasTrait(cTrait.Faction_3_Aligned) && otherAgent.HasTrait(cTrait.Faction_3_Hostile)) ||
                (___agent.HasTrait(cTrait.Faction_4_Hostile) && otherAgent.HasTrait(cTrait.Faction_4_Aligned)) ||
                (___agent.HasTrait(cTrait.Faction_4_Aligned) && otherAgent.HasTrait(cTrait.Faction_4_Hostile)))
			{
                __instance.SetRelInitial(otherAgent, "Hateful");
                otherAgent.relationships.SetRelInitial(___agent, "Hateful");

                return false;
            }

            return true;
        }
    }

    public class Faction_1_Aligned : CustomTrait
	{
        [RLSetup]
        public static void Setup()
		{
            RogueLibs.CreateCustomTrait<Faction_1_Aligned>()
                .WithDescription(new CustomNameInfo("This character is Aligned with all characters who share the trait."))
                .WithName(new CustomNameInfo("Faction 1 Aligned"))
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = { cTrait.Faction_1_Hostile },
                    CharacterCreationCost = 0,
                    IsAvailable = false,
                    IsAvailableInCC = true,
                    UnlockCost = 0,
                }) ;
		}
        public override void OnAdded() { }
        public override void OnRemoved() { }
	}
    public class Faction_1_Hostile : CustomTrait
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Faction_1_Hostile>()
                .WithDescription(new CustomNameInfo("This character is Hostile to all characters aligned with Faction 1."))
                .WithName(new CustomNameInfo("Faction 1 Hostile"))
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = { cTrait.Faction_1_Aligned },
                    CharacterCreationCost = 0,
                    IsAvailable = false,
                    IsAvailableInCC = true,
                    UnlockCost = 0,
                });
        }
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}
