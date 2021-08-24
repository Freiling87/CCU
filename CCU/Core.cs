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
using System.Runtime.CompilerServices;

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

        public static GameController gc => GameController.gameController;
        public static readonly string loggerName = $"CCU_{MethodBase.GetCurrentMethod().DeclaringType?.Name}";
        public static new ManualLogSource Logger => _logger ?? (_logger = BepInEx.Logging.Logger.CreateLogSource(loggerName));
        public static ManualLogSource _logger;

        public void Awake()
        {
            LogMethodCall();

            new Harmony(pluginGUID).PatchAll();
            RogueLibs.LoadFromAssembly();
        }
        public static void LogCheckpoint(string note, [CallerMemberName] string callerName = "") =>
            _logger.LogInfo(callerName + ": " + note);
        public static void LogMethodCall([CallerMemberName] string callerName = "") =>
            _logger.LogInfo(callerName + ": Method Call");
    }

    public static class cTrait
	{
        public const string
            AI_Bartender_BuyRound = "AI: Bartender - Buy Round",
            AI_Bartender_Vendor = "AI: Bartender - Vendor",
            AI_Bouncer_AcceptBribe = "AI: Bouncer - Accept Bribe", // Requires Guard Door?
            AI_Bouncer_GuardDoor = "AI: Bouncer - Guard Door",
            AI_ButlerBot_Clean = "AI: Butler Bot - Clean",
            AI_Cannibal_Cannibalize = "AI: Cannibal - Cannibalize", // May require bite ability?
            AI_Cannibal_BushAmbush = "AI: Cannibal - Bush Ambush",
            AI_Cannibal_ManholeAmbush = "AI: Cannibal - Manhole Ambush",
            AI_Cannibal_HostileToSoldiers = "AI: Cannibal - Hostile to Soldiers", // Analogue in BM: Army of Negative One
            AI_Clerk_Bank = "AI: Clerk - Bank Teller",
            AI_Clerk_BloodBank = "AI: Clerk - Blood Bank",
            AI_Clerk_DeportationCenter = "AI: Clerk - Deportation Center",
            AI_Clerk_Hotel = "AI: Clerk - Hotel",
            AI_Clerk_MovieTheater = "AI: Clerk - Movie Theater",
            AI_Cop_AcceptBribe = "AI: Cop - Accept Bribe",
            AI_Cop_EnforceLaws = "AI: Cop - Enforce Laws",
            AI_Cop_Lockdown = "AI: Cop - Lockdown Clearance", // Might not be a thing - could just be The Law
            AI_CopBot_Enforcelaws = "AI: Cop Bot - Enforce Laws",
            AI_CopBot_VisionBeams = "AI: Cop Bot - Vision Beams",
            AI_Doctor_AdministerBloodBag = "AI: Doctor - Administer Blood Bag",
            AI_Doctor_Heal = "AI: Doctor - Heal",
            AI_Doctor_UseBloodBag = "AI: Doctor - Use Blood Bag",
            AI_DrugDealer_UseDrugs = "AI: Drug Dealer - Use Drugs",
            AI_DrugDealer_Vendor = "AI: Drug Dealer - Vendor",
            AI_Firefighter_FightFire = "AI: Firefighter - Fight Fires",
            AI_Gangbanger_SpawnRoamingGangs = "AI: Gangbanger - Spawn Roaming Gangs",
            AI_Gorilla_HireForBanana = "AI: Gorilla - Hire for Banana",
            AI_Gorilla_Vendor = "AI: Gorilla - Vendor",
            AI_Hacker_Hack = "AI: Hacker - Hack",
            AI_Jock_ArenaManager = "AI: Jock - Arena Manager",
            AI_KillerRobot_Chase = "AI: Killer Robot - Chase",
            AI_Mayor_Bodyguarded_SuperCop = "AI: Mayor - Bodyguarded by Supercops",
            AI_Mobster_InfluenceElection = "AI: Mobster - Influence Election",
            AI_Mobster_Shakedown = "AI: Mobster - Shakedown",
            AI_Musician_Bodyguarded_Goon = "AI: Musician - Bodyguarded by Goons",
            AI_Musician_Bodyguarded_Supergoon = "AI: Musician - Bodyguarded by Supergoons",
            AI_Musician_TurntablesGuard = "AI: Musician - Turntables Guard",
            AI_Musician_SongRequest = "AI: Musician - Song Request",
            AI_OfficeDrone_OfferMotivation = "AI: Office Drone - Offer Motivation",
            AI_Scientist_Identify = "AI: Scientist - Identify",
            AI_Shopkeeper_Vendor = "AI: Shopkeeper - Vendor",
            AI_Soldier_HostileToCannibals = "AI: Soldier - Hostile to Cannibals", // Analogue in BM: Cannibal Killer
            AI_Soldier_MallVendor = "AI: Soldier - Mall Vendor",
            AI_Slave_Enslaved = "AI: Slave - Enslaved",
            AI_Slavemaster_SellSlaves = "AI: Slavemaster - Sell Slaves",
            AI_Slavemaster_OwnSlaves = "AI: Slavemaster - Own Slaves",
            AI_SlumDweller_CashGrabber = "AI: Slum Dweller - Cash Grabber",
            AI_SlumDweller_CauseRuckus = "AI: Slum Dweller - Cause a Ruckus",
            AI_SlumDweller_CommonFolk = "AI: Slum Dweller - Common Folk",
            AI_Supercop_EnforceLaws = "AI: Supercop - Enforce Laws", // Might be identical to Cop
            AI_Thief_BreakIn = "AI: Thief - Break-In",
            AI_Thief_HonorAmongThieves = "AI: Thief - Honor Among Thieves",
            AI_Thief_ManholeAmbush = "AI: Thief - Manhole Ambush",
            AI_Thief_Pickpocket = "AI: Thief - Pickpocket", // Requires Sticky Gloves?
            AI_UpperCruster_Bodyguarded = "AI: Upper Cruster - Bodyguarded",
            AI_UpperCruster_OwnSlave = "AI: Upper Cruster - Own Slave",
            AI_UpperCruster_Tattletale = "AI: Upper Cruster - Tattle-tale",
            AI_Vampire_Bite = "AI: Vampire - Bite",
            AI_Vampire_HostileToWerewolves = "AI: Vampire - Hostile to Werewolves", // Analogue in BM: Werewolf Wrecker
            AI_Various_AnnoyedAtSuspicious = "AI: Various - Annoyed at Suspicious",
            AI_Various_Coward = "AI: Various - Coward",
            AI_Various_DrugGrabber = "AI: Various - Drug Grabber",
            AI_Various_Hireable = "AI: Various - Hireable",
            AI_Various_Extort = "AI: Various - Extortable",
            AI_Various_Guilty = "AI: Various - Guilty",
            AI_Various_Scumbag = "AI: Various - Scumbag",
            AI_Werewolf_HostileToVampires = "AI: Werewolf - Hostile to Vampires", // Analogue in BM: Vampire Vanquisher
            Appearance_FacialHair_Beard = "Appearance: Facial Hair - Beard",
            Appearance_FacialHair_Mustache = "Appearance: Facial Hair - Mustache",
            Appearance_FacialHair_MustacheCircus = "Appearance: Facial Hair - MustacheCircus",
            Appearance_FacialHair_MustacheRedneck = "Appearance: Facial Hair - MustacheRedneck",
            Appearance_FacialHair_None = "Appearance: Facial Hair - None",
            Appearance_HairColor_Brown = "Appearance: Hair Color - Brown",
            Appearance_HairColor_Black = "Appearance: Hair Color - Black",
            Appearance_HairColor_Blonde = "Appearance: Hair Color - Blonde",
            Appearance_HairColor_Blue = "Appearance: Hair Color - Blue",
            Appearance_HairColor_Green = "Appearance: Hair Color - Green",
            Appearance_HairColor_Grey = "Appearance: Hair Color - Grey",
            Appearance_HairColor_Orange = "Appearance: Hair Color - Orange",
            Appearance_HairColor_Pink = "Appearance: Hair Color - Pink",
            Appearance_HairColor_Purple = "Appearance: Hair Color - Purple",
            Appearance_HairColor_Red = "Appearance: Hair Color - Red",
            Appearance_Hairstyle_Afro = "Appearance: Hair Style - Afro",
            Appearance_Hairstyle_Bald = "Appearance: Hair Style - Bald",
            Appearance_Hairstyle_Balding = "Appearance: Hair Style - Balding", 
            Appearance_Hairstyle_BangsLong = "Appearance: Hair Style - BangsLong",
            Appearance_Hairstyle_BangsMedium = "Appearance: Hair Style - BangsMedium",
            Appearance_Hairstyle_Curtains = "Appearance: Hair Style - Curtains",
            Appearance_Hairstyle_Cutoff = "Appearance: Hair Style - Cutoff",
            Appearance_Hairstyle_FlatLong = "Appearance: Hair Style - FlatLong",
            Appearance_Hairstyle_Leia = "Appearance: Hair Style - Leia",
            Appearance_Hairstyle_HoboBeard = "Appearance: Hair Style - HoboBeard",
            Appearance_Hairstyle_MessyLong = "Appearance: Hair Style - MessyLong",
            Appearance_Hairstyle_Military = "Appearance: Hair Style - Military",
            Appearance_Hairstyle_Mohawk = "Appearance: Hair Style - Mohawk",
            Appearance_Hairstyle_Normal = "Appearance: Hair Style - Normal",
            Appearance_Hairstyle_NormalHigh = "Appearance: Hair Style - NormalHigh",
            Appearance_Hairstyle_Pompadour = "Appearance: Hair Style - Pompadour",
            Appearance_Hairstyle_Ponytail = "Appearance: Hair Style - Ponytail",
            Appearance_Hairstyle_PuffyLong = "Appearance: Hair Style - PuffyLong",
            Appearance_Hairstyle_PuffyShort = "Appearance: Hair Style - PuffyShort",
            Appearance_Hairstyle_Sidewinder = "Appearance: Hair Style - Sidewinder",
            Appearance_Hairstyle_Spiky = "Appearance: Hair Style - Spiky",
            Appearance_Hairstyle_SpikyShort = "Appearance: Hair Style - SpikyShort",
            Appearance_Hairstyle_Suave = "Appearance: Hair Style - Suave",
            Appearance_Hairstyle_Wave = "Appearance: Hair Style - Wave",
            Appearance_SkinColor_SuperPaleSkin = "Appearance: Skin Color - SuperPaleSkin",
            Appearance_SkinColor_PaleSkin = "Appearance: Skin Color - PaleSkin",
            Appearance_SkinColor_WhiteSkin = "Appearance: Skin Color - WhiteSkin",
            Appearance_SkinColor_PinkSkin = "Appearance: Skin Color - PinkSkin",
            Appearance_SkinColor_GoldSkin = "Appearance: Skin Color - GoldSkin",
            Appearance_SkinColor_MixedSkin = "Appearance: Skin Color - MixedSkin",
            Appearance_SkinColor_LightBlackSkin = "Appearance: Skin Color - LightBlackSkin",
            Appearance_SkinColor_BlackSkin = "Appearance: Skin Color - BlackSkin",
            Appearance_SkinColor_ZombieSkin1 = "Appearance: Skin Color - ZombieSkin1",
            Appearance_SkinColor_ZombieSkin2 = "Appearance: Skin Color - ZombieSkin2",
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
            Loadout_Shapeshifter = "Loadout: Shapeshifter",
            Loadout_Soldier = "Loadout: Soldier",
            Loadout_Thief = "Loadout: Thief",
            Loadout_Worker = "Loadout: Worker",
            MapMarker_Bartender = "Map Marker: Bartender",
            MapMarker_DrugDealer = "Map Marker: DrugDealer",
            MapMarker_Face = "Map Marker: Face",
            MapMarker_KillerRobot = "Map Marker: KillerRobot",
            MapMarker_QuestionMark = "Map Marker: QuestionMark",
            MapMarker_Shopkeeper = "Map Marker: Shopkeeper";
    }
}
