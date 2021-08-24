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
        public static GameController gc => GameController.gameController;
        public static readonly string loggerName = $"CCU_{MethodBase.GetCurrentMethod().DeclaringType?.Name}";
        public static ManualLogSource Logger => _logger ?? (_logger = BepInEx.Logging.Logger.CreateLogSource(loggerName));
        public static ManualLogSource _logger;

        [HarmonyPrefix, HarmonyPatch(methodName: nameof(Relationships.SetupRelationshipOriginal), argumentTypes: new Type[1] { typeof(Agent) })]
        public static bool SetupRelationshipOriginal_Prefix(Agent otherAgent, Relationships __instance, Agent ___agent)
        {
            Core.LogMethodCall();

            if ((___agent.HasTrait<Faction_1_Aligned>() && otherAgent.HasTrait<Faction_1_Aligned>()) ||
                (___agent.HasTrait<Faction_2_Aligned>() && otherAgent.HasTrait<Faction_2_Aligned>()) ||
                (___agent.HasTrait<Faction_3_Aligned>() && otherAgent.HasTrait<Faction_3_Aligned>()) ||
                (___agent.HasTrait<Faction_4_Aligned>() && otherAgent.HasTrait<Faction_4_Aligned>()))
            {
                __instance.SetRelInitial(otherAgent, "Aligned");
                otherAgent.relationships.SetRelInitial(___agent, "Aligned");

                Core.LogCheckpoint("A");

                return false;
            }

            if ((___agent.HasTrait<Faction_1_Hostile>() && otherAgent.HasTrait<Faction_1_Aligned>()) ||
                (___agent.HasTrait<Faction_1_Aligned>() && otherAgent.HasTrait<Faction_1_Hostile>()) ||
                (___agent.HasTrait<Faction_2_Hostile>() && otherAgent.HasTrait<Faction_2_Aligned>()) ||
                (___agent.HasTrait<Faction_2_Aligned>() && otherAgent.HasTrait<Faction_2_Hostile>()) ||
                (___agent.HasTrait<Faction_3_Hostile>() && otherAgent.HasTrait<Faction_3_Aligned>()) ||
                (___agent.HasTrait<Faction_3_Aligned>() && otherAgent.HasTrait<Faction_3_Hostile>()) ||
                (___agent.HasTrait<Faction_4_Hostile>() && otherAgent.HasTrait<Faction_4_Aligned>()) ||
                (___agent.HasTrait<Faction_4_Aligned>() && otherAgent.HasTrait<Faction_4_Hostile>()))
			{
                __instance.SetRelInitial(otherAgent, "Hateful");
                otherAgent.relationships.SetRelInitial(___agent, "Hateful");

                Core.LogCheckpoint("B");

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
                .WithDescription(new CustomNameInfo("This character is Aligned with all characters who share the trait.\n\nWarning: This is for use by content creators only. Use by players, unless instructed by campaign author, may cause unintended consequences."))
                .WithName(new CustomNameInfo(cTrait.Faction_1_Aligned))
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = { cTrait.Faction_1_Hostile },
                    CharacterCreationCost = 0,
                    IsAvailable = false,
                    IsAvailableInCC = Core.designerEdition,
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
                .WithDescription(new CustomNameInfo("This character is Hostile to all characters aligned with Faction 1.\n\nWarning: This is for use by content creators only. Use by players, unless instructed by campaign author, may cause unintended consequences."))
                .WithName(new CustomNameInfo(cTrait.Faction_1_Hostile))
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = { cTrait.Faction_1_Aligned },
                    CharacterCreationCost = 0,
                    IsAvailable = false,
                    IsAvailableInCC = Core.designerEdition,
                    UnlockCost = 0,
                });
        }
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
    public class Faction_2_Aligned : CustomTrait
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Faction_2_Aligned>()
                .WithDescription(new CustomNameInfo("This character is Aligned with all characters who share the trait.\n\nWarning: This is for use by content creators only. Use by players, unless instructed by campaign author, may cause unintended consequences."))
                .WithName(new CustomNameInfo(cTrait.Faction_2_Aligned))
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = { cTrait.Faction_2_Hostile },
                    CharacterCreationCost = 0,
                    IsAvailable = false,
                    IsAvailableInCC = Core.designerEdition,
                    UnlockCost = 0,
                });
        }
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
    public class Faction_2_Hostile : CustomTrait
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Faction_2_Hostile>()
                .WithDescription(new CustomNameInfo("This character is Hostile to all characters aligned with Faction 2.\n\nWarning: This is for use by content creators only. Use by players, unless instructed by campaign author, may cause unintended consequences."))
                .WithName(new CustomNameInfo(cTrait.Faction_2_Hostile))
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = { cTrait.Faction_2_Aligned },
                    CharacterCreationCost = 0,
                    IsAvailable = false,
                    IsAvailableInCC = Core.designerEdition,
                    UnlockCost = 0,
                });
        }
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
    public class Faction_3_Aligned : CustomTrait
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Faction_3_Aligned>()
                .WithDescription(new CustomNameInfo("This character is Aligned with all characters who share the trait.\n\nWarning: This is for use by content creators only. Use by players, unless instructed by campaign author, may cause unintended consequences."))
                .WithName(new CustomNameInfo(cTrait.Faction_3_Aligned))
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = { cTrait.Faction_3_Hostile },
                    CharacterCreationCost = 0,
                    IsAvailable = false,
                    IsAvailableInCC = Core.designerEdition,
                    UnlockCost = 0,
                });
        }
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
    public class Faction_3_Hostile : CustomTrait
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Faction_3_Hostile>()
                .WithDescription(new CustomNameInfo("This character is Hostile to all characters aligned with Faction 3.\n\nWarning: This is for use by content creators only. Use by players, unless instructed by campaign author, may cause unintended consequences."))
                .WithName(new CustomNameInfo(cTrait.Faction_3_Hostile))
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = { cTrait.Faction_3_Aligned },
                    CharacterCreationCost = 0,
                    IsAvailable = false,
                    IsAvailableInCC = Core.designerEdition,
                    UnlockCost = 0,
                });
        }
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
    public class Faction_4_Aligned : CustomTrait
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Faction_4_Aligned>()
                .WithDescription(new CustomNameInfo("This character is Aligned with all characters who share the trait.\n\nWarning: This is for use by content creators only. Use by players, unless instructed by campaign author, may cause unintended consequences."))
                .WithName(new CustomNameInfo(cTrait.Faction_4_Aligned))
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = { cTrait.Faction_4_Hostile },
                    CharacterCreationCost = 0,
                    IsAvailable = false,
                    IsAvailableInCC = Core.designerEdition,
                    UnlockCost = 0,
                });
        }
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
    public class Faction_4_Hostile : CustomTrait
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Faction_4_Hostile>()
                .WithDescription(new CustomNameInfo("This character is Hostile to all characters aligned with Faction 4.\n\nWarning: This is for use by content creators only. Use by players, unless instructed by campaign author, may cause unintended consequences."))
                .WithName(new CustomNameInfo(cTrait.Faction_4_Hostile))
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = { cTrait.Faction_4_Aligned },
                    CharacterCreationCost = 0,
                    IsAvailable = false,
                    IsAvailableInCC = Core.designerEdition,
                    UnlockCost = 0,
                });
        }
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}
