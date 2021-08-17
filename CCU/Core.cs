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
    [BepInPlugin(pluginGUID, pluginName, pluginVersion)]
    [BepInDependency(RogueLibs.GUID, RogueLibs.CompiledVersion)]
    public class Core : BaseUnityPlugin
    {
        public const string pluginGUID = "Freiling87.streetsofrogue.CCU";
        public const string pluginName = "Custom Content Utilities";
        public const string pluginVersion = "0.1.0";
        public const bool designerEdition = true;

        public static ManualLogSource MyLogger;

        public void Awake()
        {
            MyLogger = Logger;

            new Harmony(pluginGUID).PatchAll();
            RogueLibs.LoadFromAssembly();
        }
    }

    public static class cTrait
	{
        public const string
            AI_Bartender_BuyRound = "AI_Bartender_BuyRound",
            AI_Bartender_Vendor = "AI_Bartender_Vendor",
            AI_Bouncer_Bribeable = "AI_Bouncer_Bribeable",
            AI_Bouncer_GuardDoor = "AI_Bouncer_GuardDoor",
            AI_ButlerBot_Clean = "AI_ButlerBot_Clean",
            AI_Cannibal_Cannibalize = "AI_Cannibal_Cannibalize",
            AI_Cannibal_AmbushBush = "AI_Cannibal_AmbushBush",
            AI_Cannibal_AmbushManhole = "AI_Cannibal_AmbushManhole",
            AI_Cannibal_HostileToSoldiers = "AI_Cannibal_HostileToSoldiers", // Player-accessible
            AI_Clerk_Bank = "AI_Clerk_Bank",
            AI_Clerk_BloodBank = "AI_Clerk_BloodBank",
            AI_Clerk_DeportationCenter = "AI_Clerk_DeportationCenter",
            AI_Clerk_Hotel = "AI_Clerk_Hotel",
            AI_Clerk_MovieTheater = "AI_Clerk_MovieTheater",
            AI_Cop_AcceptBribe = "AI_Cop_AcceptBribe",
            AI_Cop_EnforceLaws = "AI_Cop_EnforceLaws",
            AI_Cop_Lockdown = "AI_Cop_Lockdown",
            AI_CopBot_Enforcelaws = "AI_CopBot_EnforceLaws",
            AI_CopBot_VisionBeams = "AI_CopBot_VisionBeams",
            AI_Doctor_AdministerBloodBag = "AI_Doctor_AdministerBloodBag",
            AI_Doctor_Heal = "AI_Doctor_Heal",
            AI_Doctor_UseBloodBag = "AI_Doctor_UseBloodBag",
            AI_DrugDealer_UseDrugs = "AI_DrugDealer_UseDrugs",
            AI_DrugDealer_Vendor = "AI_DrugDealer_Vendor",
            AI_Firefighter_FightFire = "AI_Firefighter_FightFire",
            AI_Gangbanger_SpawnRoamingGangs = "AI_Gangbanger_SpawnRoamingGangs",
            AI_Gorilla_Hire = "AI_Gorilla_Hire",
            AI_Gorilla_Vendor = "AI_Gorilla_Vendor",
            AI_Hacker_Hack = "AI_Hacker_Hack",
            AI_Jock_ArenaManager = "AI_Jock_ArenaManager",
            AI_KillerRobot_Chase = "AI_KillerRobot_Chase",
            AI_Mayor_Bodyguarded_SuperCop = "AI_Mayor_Bodyguarded_Supercop",
            AI_Mobster_InfluenceElection = "AI_Mobster_InfluenceElection",
            AI_Mobster_Shakedown = "AI_Mobster_Shakedown",
            AI_Musician_Bodyguarded_Goon = "AI_Various_Bodyguarded_Goon",
            AI_Musician_Bodyguarded_Supergoon = "AI_Various_Bodyguarded_Supergoon",
            AI_Musician_Turntables = "AI_Musician_Turntables",
            AI_OfficeDrone_OfferMotivation = "AI_OfficeDrone_OfferMotivation",
            AI_Scientist_Identify = "AI_Scientist_Identify",
            AI_Shapeshifter_Possess = "AI_Shapeshifter_Possess",
            AI_Shopkeeper_Vendor = "AI_Shopkeeper_Vendor",
            AI_Soldier_HostileToCannibals = "AI_Soldier_HostileToCannibals", // Player-accessible
            AI_Soldier_MallVendor = "AI_Soldier_MallVendor",
            AI_Slave_Enslaved = "AI_Slave_Enslaved",
            AI_Slavemaster_SellSlaves = "AI_Slavemaster_SellSlaves",
            AI_Slavemaster_OwnSlaves = "AI_Slavemaster_OwnSlaves",
            AI_SlumDweller_CauseRuckus = "AI_SlumDweller_CauseRuckus",
            AI_SlumDweller_CommonFolk = "AI_SlumDweller_CommonFolk",
            AI_SlumDweller_GrabMoney = "AI_SlumDweller_GrabMoney",
            AI_Supercop_EnforceLaws = "AI_Supercop_EnforceLaws",
            AI_Thief_AmbushManhole = "AI_Thief_AmbushManhole",
            AI_Thief_BreakIn = "AI_Thief_BreakIn",
            AI_Thief_HonorAmongThieves = "AI_Thief_HonorAmongThieves",
            AI_Thief_Pickpocket = "AI_Thief_Pickpocket",
            AI_UpperCruster_Bodyguarded = "AI_UpperCruster_Bodyguarded",
            AI_UpperCruster_OwnSlave = "AI_UpperCruster_OwnSlave",
            AI_UpperCruster_Tattle = "AI_UpperCruster_Tattle",
            AI_Vampire_Bite = "AI_Vampire_Bite",
            AI_Vampire_HostileToWerewolves = "AI_Vampire_HostileToWerewolves", // Player-accessible
            AI_Various_AnnoyedAtSuspicious = "AI_Various_AnnoyedAtSuspicious",
            AI_Various_Coward = "AI_Various_Coward",
            AI_Various_Hire = "AI_Various_Hire",
            AI_Various_Extort = "AI_Various_Extort",
            AI_Various_Guilty = "AI_Various_Guilty",
            AI_Various_Scumbag = "AI_Various_Scumbag",
            AI_Werewolf_HostileToVampires = "AI_Werewolf_HostileToVampires", // Player-accessible
            Appearance_HairColor_Normal = "Appearance_HairColor_Normal",
            Appearance_HairColor_NormalNoGrey = "Appearance_HairColor_NormalNoGrey",
            Appearance_HairColor_Wild = "Appearance_HairColor_Wild",
            Appearance_FacialHair = "Appearance_FacialHair",
            Appearance_Hair_Balding = "Appearance_Hair_Balding",
            Appearance_Hair_Bangs = "Appearance_Hair_Bangs",
            Appearance_Hair_CanHaveFacialHair = "Appearance_Hair_CanHaveFacialHair",
            Appearance_Hair_Female = "Appearance_Hair_Female",
            Appearance_Hair_Long = "Appearance_Hair_Long",
            Appearance_Hair_Male = "Appearance_Hair_Male",
            Appearance_Hair_Punk = "Appearance_Hair_Punk",
            Appearance_Hair_Short = "Appearance_Hair_Short",
            Appearance_Hair_ShortFemale = "Appearance_Hair_ShortFemale",
            Appearance_Hair_Stylish = "Appearance_Hair_Stylish",
            Appearance_Hair_NotHair = "Appearance_Hair_NotHair",
            Appearance_Skin_Any = "Appearance_Skin_Any",
            Appearance_Skin_Shapeshifter = "Appearance_Skin_Shapeshifter",
            Appearance_Skin_Vampire = "Appearance_Skin_Vampire",
            Appearance_Skin_Zombie = "Appearance_Skin_Zombie",
            Chunk_KeyHolder = "Key Holder",
            Chunk_SafeComboHolder = "Safe Combo Holder",
            Faction_1_Aligned = "Faction 1 Aligned",
            Faction_1_Hostile = "Faction 1 Hostile",
            Faction_2_Aligned = "Faction 2 Aligned",
            Faction_2_Hostile = "Faction 2 Hostile",
            Faction_3_Aligned = "Faction 3 Aligned",
            Faction_3_Hostile = "Faction 3 Hostile",
            Faction_4_Aligned = "Faction 4 Aligned",
            Faction_4_Hostile = "Faction 4 Hostile",
            Loadout_Shapeshifter = "Loadout_Shapeshifter",
            Loadout_Soldier = "Loadout_Soldier",
            Loadout_Thief = "Loadout_Thief",
            Loadout_Worker = "Loadout_Worker",
            MapMarker_Bartender = "MapMarker_Bartender",
            MapMarker_DrugDealer = "MapMarker_DrugDealer",
            MapMarker_Face = "MapMarker_Face",
            MapMarker_KillerRobot = "MapMarker_KillerRobot",
            MapMarker_QuestionMark = "MapMarker_QuestionMark",
            MapMarker_Shopkeeper = "MapMarker_Shopkeeper";
    }
}
