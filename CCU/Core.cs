using System;
using System.Reflection;
using BepInEx;
using RogueLibsCore;
using BepInEx.Logging;
using HarmonyLib;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Collections.Generic;

namespace CCU
{
    [BepInPlugin(pluginGUID, pluginName, pluginVersion)]
    [BepInProcess("StreetsOfRogue.exe")]
    [BepInDependency(RogueLibs.GUID, RogueLibs.CompiledVersion)]
    public class Core : BaseUnityPlugin
    {
        public const string pluginGUID = "Freiling87.streetsofrogue.CCU";
        public const string pluginName = "Custom Content Utilities";
        public const string pluginVersion = "0.1.0";
        public const bool designerEdition = true;

        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

        public void Awake()
        {
            LogMethodCall();

            new Harmony(pluginGUID).PatchAll();
            RogueLibs.LoadFromAssembly();
        }
        public static void LogCheckpoint(string note, [CallerMemberName] string callerName = "") =>
            logger.LogInfo(callerName + ": " + note);
        public static void LogMethodCall([CallerMemberName] string callerName = "") =>
            logger.LogInfo(callerName + ": Method Call");
    }

    public static class CoreTools
    {
        public static T GetMethodWithoutOverrides<T>(this MethodInfo method, object callFrom)
                where T : Delegate
        {
            IntPtr ptr = method.MethodHandle.GetFunctionPointer();
            return (T)Activator.CreateInstance(typeof(T), callFrom, ptr);
        }
    }

    public static class CCULogger
	{
        private static string GetLoggerName(Type containingClass)
        {
            return $"CCU_{containingClass.Name}";
        }

        public static ManualLogSource GetLogger()
        {
            Type containingClass = new StackFrame(1, false).GetMethod().ReflectedType;
            return Logger.CreateLogSource(GetLoggerName(containingClass));
        }
    }

	public static class CJob
	{
		public const string
			TamperSomething = "TamperSomething",
			TamperSomethingReal = "TamperSomethingReal",
			SafecrackSafe = "SafecrackSafe",
			SafecrackSafeReal = "SafecrackSafeReal";
	}

    public static class CTrait 
	{
		public const string
		#region Agent Group
			AgentGroup_AffectVanilla = "[CCU] Agent Group - Affect Vanilla", // Toggles use in campaign or vanilla gameplay 
			AgentGroup_AffectCampaign = "[CCU] Agent Group - Affect Campaign", // Will need an "Agent Lists Extended" mutator that can be added to vanilla or campaign to prevent affecting non-modded campaigns

			AgentGroup_AllTypes = "[CCU] Agent Group - All Types",
			AgentGroup_AllTypesNoFactions = "[CCU] Agent Group - All Types (No Factions)",
			AgentGroup_ApeTown = "[CCU] Agent Group - Ape Town",
			AgentGroup_ArenaBattler = "[CCU] Agent Group - Arena Battler",
			AgentGroup_Blahd = "[CCU] Agent Group - Blahd",
			AgentGroup_BlueCollars = "[CCU] Agent Group - Blue-Collars",
			AgentGroup_BusinessOwners = "[CCU] Agent Group - Business Owners",
			AgentGroup_Cannibal = "[CCU] Agent Group - Cannibal",
			AgentGroup_CaveAgent = "[CCU] Agent Group - Cave",
			AgentGroup_Creatures = "[CCU] Agent Group - Creatures",
			AgentGroup_Crepe = "[CCU] Agent Group - Crepe",
			AgentGroup_DowntownAgent = "[CCU] Agent Group - Downtown",
			AgentGroup_Entertainers = "[CCU] Agent Group - Entertainers",
			AgentGroup_Fighters = "[CCU] Agent Group - Fighters",
			AgentGroup_GangbangerType = "[CCU] Agent Group - Gangbangers",
			AgentGroup_GuardType = "[CCU] Agent Group - Guards",
			AgentGroup_HideoutAgent = "[CCU] Agent Group - Hideout",
			AgentGroup_HideoutAgentIndustrial = "[CCU] Agent Group - Hideout (Industrial)",
			AgentGroup_Hooligans = "[CCU] Agent Group - Hooligans",
			AgentGroup_HooligansNoGangbangers = "[CCU] Agent Group - Hooligans (No Gangbangers)",
			AgentGroup_Kidnapped = "[CCU] Agent Group - Kidnapped",
			AgentGroup_LawWorkers = "[CCU] Agent Group - Law Enforcers",
			AgentGroup_Magicians = "[CCU] Agent Group - Magicians",
			AgentGroup_ParkAgent = "[CCU] Agent Group - Park",
			AgentGroup_ParkHomeAgent = "[CCU] Agent Group - Park (Home)",
			AgentGroup_Poverty = "[CCU] Agent Group - Poverty",
			AgentGroup_Resistance = "[CCU] Agent Group - Resistance",
			AgentGroup_Sewers = "[CCU] Agent Group - Sewers",
			AgentGroup_SlumsAgent = "[CCU] Agent Group - Slums",
			AgentGroup_UndeadFeral = "[CCU] Agent Group - Undead (Feral)",
			AgentGroup_UndeadSentient = "[CCU] Agent Group - Undead (Sentient)",
			AgentGroup_Underdark = "[CCU] Agent Group - Underdark",
			AgentGroup_Unique = "[CCU] Agent Group - Unique",
			AgentGroup_UptownAgent = "[CCU] Agent Group - Uptown",
			AgentGroup_UptownHomeAgent = "[CCU] Agent Group - Uptown (Home)",
			AgentGroup_UptownNoGangs = "[CCU] Agent Group - Uptown (No Gangs)",
			AgentGroup_Vampire = "[CCU] Agent Group - Vampire",
			AgentGroup_WhiteCollars = "[CCU] Agent Group - White-Collars",
		#endregion
		#region Appearance
		#region Accessory
			Appearance_Accessory_CopHat = "Appearance: Accessory - Cop Hat",
			Appearance_Accessory_DoctorHeadlamp = "Appearance: Accessory - Doctor Headlamp",
			Appearance_Accessory_Fedora = "Appearance: Accessory - Fedora",
			Appearance_Accessory_FireHelmet = "Appearance: Accessory - Fire Helmet",
			Appearance_Accessory_HackerGlasses = "Appearance: Accessory - Hacker Glasses",
			Appearance_Accessory_HatBlue = "Appearance: Accessory - Hat (Blue)",
			Appearance_Accessory_HatRed = "Appearance: Accessory - Hat (Red)",
			Appearance_Accessory_Headphones = "Appearance: Accessory - Headphones",
			Appearance_Accessory_None = "Appearance: Accessory - None",
			Appearance_Accessory_Sunglasses = "Appearance: Accessory - Sunglasses",
			Appearance_Accessory_SupercopHat = "Appearance: Accessory - Supercop Hat",
			Appearance_Accessory_ThiefHat = "Appearance: Accessory - Thief Hat",
		#endregion
		#region Eyes

		#endregion
		#region Facial Hair
			Appearance_FacialHair_Beard = "Appearance: Facial Hair - Beard",
			Appearance_FacialHair_Mustache = "Appearance: Facial Hair - Mustache",
			Appearance_FacialHair_MustacheCircus = "Appearance: Facial Hair - MustacheCircus",
			Appearance_FacialHair_MustacheRedneck = "Appearance: Facial Hair - MustacheRedneck",
			Appearance_FacialHair_None = "Appearance: Facial Hair - None",
		#endregion
		#region Hair Color
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
		#endregion
		#region Hairstyle
			Appearance_Hairstyle_Afro = "Appearance: Hairstyle - Afro",
			Appearance_Hairstyle_AlienHead = "Appearance: Hairstyle - Alien Head",
			Appearance_Hairstyle_AssassinMask = "Appearance: Hairstyle - Assassin Mask",
			Appearance_Hairstyle_Bald = "Appearance: Hairstyle - Bald",
			Appearance_Hairstyle_Balding = "Appearance: Hairstyle - Balding",
			Appearance_Hairstyle_BangsLong = "Appearance: Hairstyle - BangsLong",
			Appearance_Hairstyle_BangsMedium = "Appearance: Hairstyle - BangsMedium",
			Appearance_Hairstyle_ButlerBotHead = "Appearance: Hairstyle - Butler Bot Head",
			Appearance_Hairstyle_Curtains = "Appearance: Hairstyle - Curtains",
			Appearance_Hairstyle_Cutoff = "Appearance: Hairstyle - Cutoff",
			Appearance_Hairstyle_FlatLong = "Appearance: Hairstyle - FlatLong",
			Appearance_Hairstyle_GorillaHead = "Appearance: Hairstyle - Gorilla Head",
			Appearance_Hairstyle_HoboBeard = "Appearance: Hairstyle - HoboBeard",
			Appearance_Hairstyle_Hoodie = "Appearance: Hairstyle - Hoodie",
			Appearance_Hairstyle_Leia = "Appearance: Hairstyle - Leia",
			Appearance_Hairstyle_MessyLong = "Appearance: Hairstyle - MessyLong",
			Appearance_Hairstyle_Military = "Appearance: Hairstyle - Military",
			Appearance_Hairstyle_Mohawk = "Appearance: Hairstyle - Mohawk",
			Appearance_Hairstyle_Normal = "Appearance: Hairstyle - Normal",
			Appearance_Hairstyle_NormalHigh = "Appearance: Hairstyle - NormalHigh",
			Appearance_Hairstyle_Pompadour = "Appearance: Hairstyle - Pompadour",
			Appearance_Hairstyle_Ponytail = "Appearance: Hairstyle - Ponytail",
			Appearance_Hairstyle_PuffyLong = "Appearance: Hairstyle - PuffyLong",
			Appearance_Hairstyle_PuffyShort = "Appearance: Hairstyle - PuffyShort",
			Appearance_Hairstyle_RobotHead = "Appearance: Hairstyle - Robot Head",
			Appearance_Hairstyle_Sidewinder = "Appearance: Hairstyle - Sidewinder",
			Appearance_Hairstyle_SlavemasterMask = "Appearance: Hairstyle - Slavemaster Mask",
			Appearance_Hairstyle_Spiky = "Appearance: Hairstyle - Spiky",
			Appearance_Hairstyle_SpikyShort = "Appearance: Hairstyle - SpikyShort",
			Appearance_Hairstyle_Suave = "Appearance: Hairstyle - Suave",
			Appearance_Hairstyle_Wave = "Appearance: Hairstyle - Wave",
			Appearance_Hairstyle_WerewolfHead = "Appearance: Hairstyle - Werewolf Head",
		#endregion
		#region Skin Color
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
		#endregion
		#endregion
		#region Behavior
			Behavior_CleanTrash =				"[CCU] Behavior - Clean Trash",
			Behavior_EatCorpse =				"[CCU] Behavior - Eat Corpse",
			Behavior_EnforceLaws =				"[CCU] Behavior - Enforce Laws (Cop)",
			Behavior_EnforceLawsCopBot =		"[CCU] Behavior - Enforce Laws (Cop Bot)",
			Behavior_FightFires =				"[CCU] Behavior - Fight Fires",
			Behavior_GrabDrugs =				"[CCU] Behavior - Grab Drugs",
			Behavior_GrabMoney =				"[CCU] Behavior - Grab Money",
			Behavior_GuardDoor =				"[CCU] Behavior - Guard Door",
			Behavior_HogTurntables =			"[CCU] Behavior - Turntables Guard",
			Behavior_Lockdown =					"[CCU] Behavior - Lockdown Clearance", // Might not be a thing - could just be The Law
			Behavior_Pickpocket =				"[CCU] Behavior - Pickpocket",
			Behavior_SeekAndDestroy =			"[CCU] Behavior - Seek & Destroy",
			Behavior_SuckBlood =				"[CCU] Behavior - Suck Blood",
			Behavior_Shakedown =				"[CCU] Behavior - Shakedown",
			Behavior_Tattletale =				"[CCU] Behavior - Tattletale",
		#endregion
		#region Bodyguarded
			Bodyguarded_Pilot = "[CCU] Bodyguarded: Pilot",
			Bodyguarded_Cops = "[CCU] Bodyguarded: Cops",
			Bodyguarded_Goons = "[CCU] Bodyguarded: Goons",
			Bodyguarded_Supercops = "[CCU] Bodyguarded: Supercops",
			Bodyguarded_Supergoons = "[CCU] Bodyguarded: Supergoons",
		#endregion
		#region Buyer
			Interaction_BuyerAll =				"[CCU] Interaction - Buyer All", // Nonspecific
			Interaction_BuyerOnly =				"[CCU] Interaction - Buyer Only",
			Interaction_BuyerVendor =			"[CCU] Interaction - Buyer Vendor", // Buy only items sold
		#endregion
		#region Combat
			Combat_CauseLockdown =				"[CCU] Combat - Cause Lockdown",
			Combat_Coward =						"[CCU] Combat - Coward",
			Combat_Fearless =					"[CCU] Combat - Fearless",
			Combat_UseDrugs =					"[CCU] Combat - Use Drugs", // Gate behind Drug-A-Lug?
		#endregion
		#region Hire
			Hire_Bodyguard =					"[CCU] Hire - Bodyguard",
			Hire_BreakIn =						"[CCU] Hire - Break In",
			Hire_CauseRuckus =					"[CCU] Hire - Cause a Ruckus",
			Hire_CostBanana =					"[CCU] Hire - Costs Banana",
			Hire_CostLess =						"[CCU] Hire - Costs Less",
			Hire_CostMore =						"[CCU] Hire - Costs More",
			Hire_DisarmTrap =					"[CCU] Hire - Disarm Trap",
			Hire_Hack =							"[CCU] Hire - Hack",
			Hire_Pickpocket =					"[CCU] Hire - Pickpocket",
			Hire_Poison	=						"[CCU] Hire - Poison",
			Hire_Safecrack =					"[CCU] Hire - Safecrack", // Also need to extend vanilla thief behavior to this
			Hire_Tamper =						"[CCU] Hire - Tamper",
		#endregion
		#region Interaction 
			Interaction_AcceptBribeCop =		"[CCU] Interaction - Accept Bribe",
			Interaction_AcceptBribeDoor =		"[CCU] Interaction - Accept Bribe", // Requires Guard Door?
			Interaction_AdministerBloodBag =	"[CCU] Interaction - Administer Blood Bag",
			Interaction_ArenaManager =			"[CCU] Interaction - Arena Manager",
			Interaction_BankTeller =			"[CCU] Interaction - Bank Teller",
			Interaction_BloodBank =				"[CCU] Interaction - Blood Bank",
			Interaction_BuyRound =				"[CCU] Interaction - Buy Round",
			Interaction_DeportationCenter =		"[CCU] Interaction - Deportation Center",
			Interaction_Extortable =			"[CCU] Interaction - Extortable",
			Interaction_Heal =					"[CCU] Interaction - Heal",
			Interaction_Hotel =					"[CCU] Interaction - Hotel",
			Interaction_Identify =				"[CCU] Interaction - Identify",
			Interaction_InfluenceElection =		"[CCU] Interaction - Influence Election",
			Interaction_MayorClerk =			"[CCU] Interaction - Mayor Clerk",
			Interaction_Moochable =				"[CCU] Interaction - Moochable",
			Interaction_OfferMotivation =		"[CCU] Interaction - Offer Motivation",
			Interaction_PlayBadSong =			"[CCU] Interaction - Song Request",
			Interaction_QuestGiver =			"[CCU] Interaction - Quest Giver",
			Interaction_RefillGuns =			"[CCU] Interaction - Refill Guns",
			Interaction_RepairArmor =			"[CCU] Interaction - Repair Armor",
			Interaction_RepairMelee =			"[CCU] Interaction - Repair Melee Weapons",
			Interaction_SellSlaves =			"[CCU] Interaction - Sell Slaves",
			Interaction_UseBloodBag =			"[CCU] Interaction - Use Blood Bag",
		#endregion
		#region Loadout
			Loadout_ChunkKey =					"[CCU] Loadout: Chunk Key Holder",
			Loadout_ChunkMayorBadge =			"[CCU] Loadout: Chunk Mayor Badge",
			Loadout_ChunkSafeCombo =			"[CCU] Loadout: Chunk Safe Combo Holder",
			Loadout_Guns_Common =				"[CCU] Loadout: Guns (Common)",
			Loadout_Guns_Heavy =				"[CCU] Loadout: Guns (Heavy)",
			Loadout_Guns_Rare =					"[CCU] Loadout: Guns (Rare)",
			Loadout_Guns_Weird =				"[CCU] Loadout: Guns (Weird)",
		#endregion
		#region Map Marker
			MapMarker_Bartender =				"[CCU] Map Marker: Bartender",
			MapMarker_DrugDealer =				"[CCU] Map Marker: DrugDealer",
			MapMarker_Face =					"[CCU] Map Marker: Face",
			MapMarker_KillerRobot =				"[CCU] Map Marker: KillerRobot",
			MapMarker_Pilot =					"[CCU] Map Marker: Pilot",
			MapMarker_QuestionMark =			"[CCU] Map Marker: QuestionMark",
			MapMarker_Shopkeeper =				"[CCU] Map Marker: Shopkeeper",
		#endregion
		#region Passive
			Passive_ExplodeOnDeath =			"[CCU] Passive - Explode On Death",
			Passive_Guilty =					"[CCU] Passive - Guilty",
			Passive_Hackable_Haywire =			"[CCU] Passive - Hackable (Haywire)",
			Passive_Hackable_TamperWAim =		"[CCU] Passive - Hackable (Tamper w/ Aim)",
			Passive_VisionBeams =				"[CCU] Passive - Vision Beams",
		#endregion
		#region Relationships
			Relationships_AggressiveCannibal = "[CCU] Relationships - Aggressive (Cannibal)",   // Hostile to character except with Cool with Cannibals
			Relationships_AnnoyedAtSuspicious = "[CCU] Relationships - Annoyed at Suspicious",
			Relationships_Faction1Aligned = "[CCU] Relationships - Faction 1 Aligned",
			Relationships_Faction1Hostile = "[CCU] Relationships - Faction 1 Hostile",
			Relationships_Faction2Aligned = "[CCU] Relationships - Faction 2 Aligned",
			Relationships_Faction2Hostile = "[CCU] Relationships - Faction 2 Hostile",
			Relationships_Faction3Aligned = "[CCU] Relationships - Faction 3 Aligned",
			Relationships_Faction3Hostile = "[CCU] Relationships - Faction 3 Hostile",
			Relationships_Faction4Aligned = "[CCU] Relationships - Faction 4 Aligned",
			Relationships_Faction4Hostile = "[CCU] Relationships - Faction 4 Hostile",
			Relationships_HostileToCannibals = "[CCU] Relationships - Hostile to Cannibals",    // Analogue in BM: Cannibal Killer
			Relationships_HostileToSoldiers = "[CCU] Relationships - Hostile to Soldiers",  // Analogue in BM: Army of Negative One
			Relationships_HostileToVampires = "[CCU] Relationships - Hostile to Vampires",  // Analogue in BM: Vampire Vanquisher
			Relationships_HostileToWerewolves = "[CCU] Relationships - Hostile to Werewolves",  // Analogue in BM: Werewolf Wrecker
		#endregion
		#region Spawn
			Spawn_HideInBush =					"[CCU] Spawn: Hide In Bush",
			Spawn_HideInManhole =				"[CCU] Spawn: Hide In Manhole",
			Spawn_RoamingGang =					"[CCU] Spawn: Roaming Gangs",
			Spawn_SlaveOwned =					"[CCU] Spawn: Slave",
			Spawn_SlaveOwner =					"[CCU] Spawn: Slave Owner",
		#endregion
		#region Trait Trigger
			TraitTrigger_CommonFolk =			"[CCU] Trait Trigger - Common Folk",
			TraitTrigger_CoolCannibal =			"[CCU] Trait Trigger - Cool Cannibal",
			TraitTrigger_CopAccess =			"[CCU] Trait Trigger - Cop Access",
			TraitTrigger_FamilyFriend =			"[CCU] Trait Trigger - Family Friend",
			TraitTrigger_HonorableThief =		"[CCU] Trait Trigger - Honorable Thief",
			TraitTrigger_Scumbag =				"[CCU] Trait Trigger - Scumbag",
		#endregion
		#region Vendor
		Vendor_Armorer =					"[CCU] Vendor - Armorer",
			Vendor_Assassin =					"[CCU] Vendor - Assassin",
			Vendor_BananaBoutique =				"[CCU] Vendor - Banana",
			Vendor_BarbarianMerchant =			"[CCU] Vendor - Barbarian",
			Vendor_Bartender =					"[CCU] Vendor - Bartender",
			Vendor_BartenderDive =				"[CCU] Vendor - Bartender (Dive)",
			Vendor_BartenderFancy =				"[CCU] Vendor - Bartender (Fancy)",
			Vendor_Blacksmith =					"[CCU] Vendor - Blacksmith",
			Vendor_Anthropophagie =				"[CCU] Vendor - Cannibal",
			Vendor_ConsumerElectronics =		"[CCU] Vendor - Consumer Electronics",
			Vendor_ConvenienceStore =			"[CCU] Vendor - Convenience Store",
			Vendor_Contraband =					"[CCU] Vendor - Cop Confiscated Goods",
			Vendor_CopStandard =				"[CCU] Vendor - Cop Patrolman's Equipment",
			Vendor_CopSWAT =					"[CCU] Vendor - Cop SWAT Equipment",
			Vendor_DemolitionDepot =			"[CCU] Vendor - Demolitionist",
			Vendor_DrugDealer =					"[CCU] Vendor - Drug Dealer",
			Vendor_FirefighterFiveAndDime =		"[CCU] Vendor - Firefighter",
			Vendor_FireSale =					"[CCU] Vendor - Fire Sale",
			Vendor_Gunsmith =					"[CCU] Vendor - Gunsmith",
			Vendor_HardwareStore =				"[CCU] Vendor - Tool Shop",
			Vendor_HighTech =					"[CCU] Vendor - HighTech",
			Vendor_HomeFortressOutlet =			"[CCU] Vendor - Home Fortress Outlet",
			Vendor_Hypnotist =					"[CCU] Vendor - Hypnotist",
			Vendor_JunkDealer =					"[CCU] Vendor - Junk Dealer",
			Vendor_McFuds =						"[CCU] Vendor - McFud's",
			Vendor_MedicalSupplier =			"[CCU] Vendor - Medical Supplier",
			Vendor_MiningGear =					"[CCU] Vendor - Mining Gear",
			Vendor_MonkeMart =					"[CCU] Vendor - Monke Mart",
			Vendor_MovieTheater =				"[CCU] Vendor - Movie Theater",
			Vendor_Occultist =					"[CCU] Vendor - Occultist",
			Vendor_OutdoorOutfitter =			"[CCU] Vendor - Outdoor Outfitter",
			Vendor_PacifistProvisioner =		"[CCU] Vendor - Pacifist Provisioner",
			Vendor_PawnShop =					"[CCU] Vendor - Pawn Shop",
			Vendor_PestControl =				"[CCU] Vendor - Pest Control",
			Vendor_Pharmacy =					"[CCU] Vendor - Pharmacy",
			Vendor_ResistanceCommissary =		"[CCU] Vendor - Resistance Commissary",
			Vendor_RiotInc =					"[CCU] Vendor - Riot Depot",
			Vendor_ResearchMaterials =			"[CCU] Vendor - Scientist",
			Vendor_Shopkeeper =					"[CCU] Vendor - Shopkeeper",
			Vendor_SlaveShop =					"[CCU] Vendor - Slave Shop",
			Vendor_Soldier =					"[CCU] Vendor - Soldier",
			Vendor_SportingGoods =				"[CCU] Vendor - Sporting Goods",
			Vendor_Teleportationist =			"[CCU] Vendor - Teleportationist",
			Vendor_Thief =						"[CCU] Vendor - Thief",
			Vendor_ThiefMaster =				"[CCU] Vendor - Thief Master",
			Vendor_ThrowceryStore =				"[CCU] Vendor - Throwcery Store",
			Vendor_ToyStore =					"[CCU] Vendor - Toy Store",
			Vendor_UpperCruster =				"[CCU] Vendor - Upper Cruster",
			Vendor_Vampire =					"[CCU] Vendor - Vampire",
			Vendor_Villain =					"[CCU] Vendor - Villain";
		#endregion
	}
	public static class vItem // Vanilla Items
	{
		public const string
				AccuracyMod = "AccuracyMod",
				AmmoCapacityMod = "AmmoCapacityMod",
				AmmoProcessor = "AmmoProcessor",
				AmmoStealer = "AmmoStealer",
				Antidote = "Antidote",
				ArmorDurabilitySpray = "ArmorDurabilityDoubler",
				Axe = "Axe",
				BaconCheeseburger = "BaconCheeseburger",
				BalletShoes = "BalletShoes",
				Banana = "Banana",
				BananaPeel = "BananaPeel",
				BaseballBat = "BaseballBat",
				Beartrap = "BearTrap",
				BeartrapfromPark = "BearTrapPark",
				Beer = "Beer",
				BFG = "BFG",
				BigBomb = "BigBomb",
				Blindenizer = "Blindenizer",
				BloodBag = "BloodBag",
				Blowtorch = "Blowtorch",
				Blueprints = "Blueprints",
				BodySwapper = "BodySwapper",
				BodyVanisher = "BodyVanisher",
				BombProcessor = "BombMaker",
				BoomBox = "Boombox",
				BooUrn = "BooUrn",
				BraceletofStrength = "BraceletStrength",
				Briefcase = "Briefcase",
				BrokenCourierPackage = "CourierPackageBroken",
				BulletproofVest = "BulletproofVest",
				CardboardBox = "CardboardBox",
				Chainsaw = "Chainsaw",
				ChloroformHankie = "ChloroformHankie",
				CigaretteLighter = "CigaretteLighter",
				Cigarettes = "Cigarettes",
				CircuitBoard = "CircuitBoard",
				Cocktail = "Cocktail",
				Codpiece = "Codpiece",
				Cologne = "Cologne",
				CourierPackage = "CourierPackage",
				CritterUpper = "CritterUpper",
				Crowbar = "Crowbar",
				CubeOfLampey = "CubeOfLampey",
				CyanidePill = "CyanidePill",
				DeliveryApp = "DeliveryApp",
				DizzyGrenade = "GrenadeDizzy",
				DoorDetonator = "DoorDetonator",
				DrinkMixer = "DrinkMixer",
				EarWarpWhistle = "HearingBlocker",
				ElectroPill = "ElectroPill",
				ElectroTetherVest = "BodyguardTether",
				EMPGrenade = "GrenadeEMP",
				Evidence = "Evidence",
				Explodevice = "ExplosiveStimulator",
				FireExtinguisher = "FireExtinguisher",
				FireproofSuit = "FireproofSuit",
				Fireworks = "Fireworks",
				FirstAidKit = "FirstAidKit",
				Fist = "Fist",
				FiveLeafClover = "FiveLeafClover",
				Flamethrower = "Flamethrower",
				FlamingSword = "FlamingSword",
				Flask = "Flask",
				FoodProcessor = "FoodProcessor",
				Forcefield = "ForceField",
				FourLeafClover = "FourLeafClover",
				FreeItemVoucher = "FreeItemVoucher",
				FreezeRay = "FreezeRay",
				FriendPhone = "FriendPhone",
				Fud = "Fud",
				GasMask = "GasMask",
				GhostGibber = "GhostBlaster",
				Giantizer = "Giantizer",
				GrapplingHook = "GrapplingHook",
				Grenade = "Grenade",
				GuidedMissileLauncher = "GuidedMissileLauncher",
				HackingTool = "HackingTool",
				HamSandwich = "HamSandwich",
				HardDrive = "HardDrive",
				HardHat = "HardHat",
				Haterator = "Haterator",
				HiringVoucher = "HiringVoucher",
				HologramBigfoot = "HologramItem",
				HotFud = "HotFud",
				Hypnotizer = "Hypnotizer",
				HypnotizerII = "Hypnotizer2",
				IdentifyWand = "IdentifyWand",
				IncriminatingPhoto = "IncriminatingPhoto",
				ItemTeleporter = "ItemTeleporter",
				Jackhammer = "Jackhammer",
				Key = "Key",
				KeyCard = "KeyCard",
				KillAmmunizer = "KillProfiterAmmo",
				KillerThrower = "KillerThrower",
				KillHealthenizer = "KillProfiterHealth",
				KillProfiter = "KillProfiter",
				Knife = "Knife",
				KnockerGrenade = "GrenadeKnocker",
				KnockerMelee = "KnockerMelee",
				LandMine = "LandMine",
				Laptop = "Laptop",
				LaserBlazer = "LaserBlazer",
				LaserGun = "LaserGun",
				Leafblower = "LeafBlower",
				Lockpick = "Lockpick",
				MacGuffinMuffin = "MacguffinMuffin",
				MachineGun = "MachineGun",
				MagicLamp = "MagicLamp",
				MayorHat = "MayorHat",
				MayorsMansionGuestBadge = "MayorBadge",
				MechKey = "MechTransformItem",
				MeleeDurabilitySpray = "MeleeDurabilityDoubler",
				MemoryMutilator = "MemoryEraser",
				MindReaderDevice = "MindReaderDevice",
				MiniFridge = "MiniFridge",
				MolotovCocktail = "MolotovCocktail",
				Money = "Money",
				MonkeyBarrel = "MonkeyBarrel",
				MoodRing = "MoodRing",
				MusclyPill = "Steroids",
				Necronomicon = "Necronomicon",
				OilContainer = "OilContainer",
				ParalyzerTrap = "ParalyzerTrap",
				Pistol = "Pistol",
				PlasmaSword = "PlasmaSword",
				PoliceBaton = "PoliceBaton",
				PortableSellOMatic = "PortableSellOMatic",
				PossessionStone = "Depossessor",
				PowerDrill = "PowerDrill",
				PropertyDeed = "PropertyDeed",
				QuickEscapeTeleporter = "QuickEscapeTeleporter",
				RagePoison = "RagePoison",
				RateofFireMod = "RateOfFireMod",
				RecordofEvidence = "MayorEvidence",
				RemoteBomb = "RemoteBomb",
				RemoteBombTrigger = "BombTrigger",
				ResearchGun = "ResearchGun",
				ResurrectionShampoo = "ResurrectionShampoo",
				Revolver = "Revolver",
				Rock = "Rock",
				RocketLauncher = "RocketLauncher",
				RubberBulletsMod = "RubberBulletsMod",
				SafeBuster = "SafeBuster",
				SafeCombination = "SafeCombination",
				SafeCrackingTool = "SafeCrackingTool",
				Shotgun = "Shotgun",
				Shovel = "Shovel",
				Shrinker = "Shrinker",
				ShrinkRay = "ShrinkRay",
				Shuriken = "Shuriken",
				SignedBaseball = "SignedBaseball",
				Silencer = "Silencer",
				SixLeafClover = "SixLeafClover",
				SkeletonKey = "SkeletonKey",
				SlaveHelmet = "SlaveHelmet",
				SlaveHelmetRemote = "SlaveHelmetRemote",
				SlaveHelmetRemover = "SlaveHelmetRemover",
				Sledgehammer = "Sledgehammer",
				SniperRifle = "SniperRifle",
				SoldierHelmet = "SoldierHelmet",
				StickyGlove = "StealingGlove",
				StickyMine = "StickyMine",
				Sugar = "Cocaine",
				Sword = "Sword",
				Syringe = "Syringe",
				Taser = "Taser",
				Teleporter = "Teleporter",
				TimeBomb = "TimeBomb",
				Tooth = "Tooth",
				TranquilizerGun = "TranquilizerGun",
				Translator = "Translator",
				TripMine = "TripMine",
				VoodooDoll = "VoodooDoll",
				WalkieTalkie = "WalkieTalkie",
				WallBypasser = "WallBypasser",
				WarpGrenade = "GrenadeWarp",
				WarpZoner = "WarpZoner",
				WaterCannon = "WaterCannon",
				WaterPistol = "WaterPistol",
				Whiskey = "Whiskey",
				Will = "Will",
				WindowCutter = "WindowCutter",
				Wrench = "Wrench";

		public static List<string> alcohol = new List<string>()
		{
				Beer,
				Cocktail,
				Whiskey
		};

		public static List<string> drugs = new List<string>()
		{
				Antidote,
				Cigarettes,
				Sugar,
				CritterUpper,
				CyanidePill,
				ElectroPill,
				Giantizer,
				KillerThrower,
				RagePoison,
				Shrinker,
				MusclyPill,
				Syringe
		};

		public static List<string> nonVegetarian = new List<string>()
		{
				BaconCheeseburger,
				HamSandwich
		};

		public static List<string> vegetarian = new List<string>()
		{
				Beer,
				Banana,
				Cocktail,
				Fud,
				HotFud,
				Whiskey
		};

		public static List<string> blunt = new List<string>()
		{ };

		public static List<string> explosive = new List<string>()
		{ };

		public static List<string> heavy = new List<string>()
		{
				Axe,
				BaseballBat,
				Beartrap,
				BulletproofVest,
				Crowbar,
				FireExtinguisher,
				FireproofSuit,
				Flamethrower,
				GhostGibber,
				LandMine,
				MachineGun,
				Revolver,
				RocketLauncher,
				Shotgun,
				Sledgehammer,
				Wrench
		};

		public static List<string> loud = new List<string>()
		{
				BoomBox,
				DizzyGrenade,
				DoorDetonator,
				EMPGrenade,
				Explodevice,
				FireExtinguisher,
				Fireworks,
				GhostGibber,
				Grenade,
				EarWarpWhistle,
				Leafblower,
				LandMine,
				MachineGun,
				MolotovCocktail,
				Pistol,
				RemoteBomb,
				Revolver,
				RocketLauncher,
				Shotgun,
				TimeBomb,
				WarpGrenade
		};

		public static List<string> piercing = new List<string>()
		{
				Axe,
				Beartrap,
				Grenade,
				Knife,
				LandMine,
				MachineGun,
				Pistol,
				Revolver,
				RocketLauncher,
				Shotgun,
				Shuriken,
				Sword
		};

		public static List<string> tools = new List<string>()
		{
				Crowbar,
				Wrench,
		};
	}
	public static class vObject // Vanilla Objects
	{
		public const string
				AirConditioner = "AirConditioner",
				AlarmButton = "AlarmButton",
				Altar = "Altar",
				AmmoDispenser = "AmmoDispenser",
				ArcadeGame = "ArcadeGame",
				ATMMachine = "ATMMachine",
				AugmentationBooth = "AugmentationBooth",
				Barbecue = "Barbecue",
				BarStool = "BarStool",
				Bathtub = "Bathtub",
				Bed = "Bed",
				Boulder = "Boulder",
				BoulderSmall = "BoulderSmall",
				Bush = "Bush",
				CapsuleMachine = "CapsuleMachine",
				Chair = "Chair",
				Chair2 = "Chair2",
				ChestBasic = "ChestBasic",
				CloneMachine = "CloneMachine",
				Computer = "Computer",
				Counter = "Counter",
				Crate = "Crate",
				Desk = "Desk",
				Door = "Door",
				Elevator = "Elevator",
				EventTriggerFloor = "EventTriggerFloor",
				ExplodingBarrel = "ExplodingBarrel",
				FireHydrant = "FireHydrant",
				Fireplace = "Fireplace",
				FireSpewer = "FireSpewer",
				FlameGrate = "FlameGrate",
				FlamingBarrel = "FlamingBarrel",
				GasVent = "GasVent",
				Generator = "Generator",
				Generator2 = "Generator2",
				Gravestone = "Gravestone",
				Jukebox = "Jukebox",
				KillerPlant = "KillerPlant",
				Lamp = "Lamp",
				LaserEmitter = "LaserEmitter",
				LoadoutMachine = "LoadoutMachine",
				Manhole = "Manhole",
				Mine = "Mine",
				MovieScreen = "MovieScreen",
				PawnShopMachine = "PawnShopMachine",
				Plant = "Plant",
				Podium = "Podium",
				PoliceBox = "PoliceBox",
				PoolTable = "PoolTable",
				PowerBox = "PowerBox",
				Refrigerator = "Refrigerator",
				Safe = "Safe",
				SatelliteDish = "SatelliteDish",
				SecurityCam = "SecurityCam",
				Shelf = "Shelf",
				Sign = "Sign",
				SlimeBarrel = "SlimeBarrel",
				SlimePuddle = "SlimePuddle",
				SlotMachine = "SlotMachine",
				Speaker = "Speaker",
				Stove = "Stove",
				SwitchBasic = "SwitchBasic",
				SwitchFloor = "SwitchFloor",
				Table = "Table",
				TableBig = "TableBig",
				Television = "Television",
				Toilet = "Toilet",
				TrashCan = "TrashCan",
				Tree = "Tree",
				Tube = "Tube",
				Turntables = "Turntables",
				Turret = "Turret",
				VendorCart = "VendorCart",
				WaterPump = "WaterPump",
				Well = "Well",
				Window = "Window";

		public static List<string> TamperableObjects = new List<string>() // TODO: Add compatibility with BunnyMod tampering
		{
			vObject.AlarmButton,
			vObject.Crate,
			vObject.Door,
			vObject.Generator,
			vObject.Generator2,
			vObject.LaserEmitter,
			vObject.PoliceBox,
			vObject.PowerBox,
			vObject.SatelliteDish,
			vObject.SecurityCam,
		};
	}
	public static class vSpecialAbility // Vanilla Special Abilities
	{
		public const string
				Bite = "Bite",
				Camouflage = "Camouflage",
				Cannibalize = "Cannibalize",
				Chaaarge = "Charge",
				ChloroformHankie = "ChloroformHankie",
				CryProfusely = "TutorialAbility",
				Enslave = "Enslave",
				Handcuffs = "Handcuffs",
				Joke = "Joke",
				Laptop = "Laptop",
				LaserGun = "LaserGun",
				MechTransformation = "MechTransform",
				MindControl = "MindControl",
				Possess = "Possess",
				PowerSap = "PowerSap",
				PrimalLunge = "Lunge",
				SharpLunge = "WerewolfLunge",
				StickyGlove = "StealingGlove",
				Stomp = "Stomp",
				Toss = "Toss",
				WaterCannon = "WaterCannon",
				WerewolfTransformation = "WerewolfTransform",
				ZombieSpit = "ZombieSpit";
	}
}
