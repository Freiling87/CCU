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
            return BepInEx.Logging.Logger.CreateLogSource(GetLoggerName(containingClass));
        }
    }

    public static class CTrait
	{
        public const string
            AI_Bartender_BuyRound = "AI: Bartender - Buy Round",
            AI_Bouncer_AcceptBribe = "AI: Bouncer - Accept Bribe", // Requires Guard Door?
            AI_Bouncer_GuardDoor = "AI: Bouncer - Guard Door",
            AI_Clerk_Bank = "AI: Clerk - Bank Teller",
            AI_Clerk_BloodBank = "AI: Clerk - Blood Bank",
            AI_Clerk_DeportationCenter = "AI: Clerk - Deportation Center",
            AI_Clerk_Hotel = "AI: Clerk - Hotel",
            AI_Cop_AcceptBribe = "AI: Cop - Accept Bribe",
            AI_Cop_Lockdown = "AI: Cop - Lockdown Clearance", // Might not be a thing - could just be The Law
            AI_CopBot_Enforcelaws = "AI: Cop Bot - Enforce Laws",
            AI_CopBot_VisionBeams = "AI: Cop Bot - Vision Beams",
            AI_Doctor_AdministerBloodBag = "AI: Doctor - Administer Blood Bag",
            AI_Doctor_Heal = "AI: Doctor - Heal",
            AI_Doctor_UseBloodBag = "AI: Doctor - Use Blood Bag",
            AI_DrugDealer_UseDrugs = "AI: Drug Dealer - Use Drugs",
            AI_Firefighter_FightFire = "AI: Firefighter - Fight Fires",
            AI_Gangbanger_SpawnRoamingGangs = "AI: Gangbanger - Spawn Roaming Gangs",
            AI_Jock_ArenaManager = "AI: Jock - Arena Manager",
            AI_KillerRobot_Chase = "AI: Killer Robot - Chase",
            AI_Mayor_Bodyguarded_SuperCop = "AI: Mayor - Bodyguarded by Supercops",
            AI_Mobster_InfluenceElection = "AI: Mobster - Influence Election",
            AI_Musician_Bodyguarded_Goon = "AI: Musician - Bodyguarded by Goons",
            AI_Musician_Bodyguarded_Supergoon = "AI: Musician - Bodyguarded by Supergoons",
            AI_Musician_TurntablesGuard = "AI: Musician - Turntables Guard",
            AI_Musician_SongRequest = "AI: Musician - Song Request",
            AI_OfficeDrone_OfferMotivation = "AI: Office Drone - Offer Motivation",
            AI_Scientist_Identify = "AI: Scientist - Identify",
            AI_Slave_Enslaved = "AI: Slave - Enslaved",
            AI_Slavemaster_SellSlaves = "AI: Slavemaster - Sell Slaves",
            AI_Slavemaster_OwnSlaves = "AI: Slavemaster - Own Slaves",
            AI_Supercop_EnforceLaws = "AI: Supercop - Enforce Laws", // Might be identical to Cop
            AI_UpperCruster_Bodyguarded = "AI: Upper Cruster - Bodyguarded",
            AI_UpperCruster_OwnSlave = "AI: Upper Cruster - Own Slave",
            AI_UpperCruster_Tattletale = "AI: Upper Cruster - Tattle-tale",
            AI_Various_AnnoyedAtSuspicious = "AI: Various - Annoyed at Suspicious",
            AI_Various_Coward = "AI: Various - Coward",
            AI_Various_Extort = "AI: Various - Extortable",
            AI_Various_Guilty = "AI: Various - Guilty",
            AI_Various_Scumbag = "AI: Various - Scumbag",

			// Hire 
			AI_Hire_Generic = "AI: Hire - Generic", // Verify if Soldier/Gangbanger/etc. are indeed identical
			AI_Hire_Gorilla = "AI: Hire - Gorilla",
			AI_Hire_Hacker = "AI: Hire - Hacker",
			AI_Hire_SlumDweller = "AI: Hire - Slum Dweller",
			AI_Hire_Thief = "AI: Hire - Thief",

			// Relationships
			AI_Relationships_Aggressive = "AI: Relationships - Aggressive", // Hostile to character except with Cool with Cannibals
			AI_Relationships_HostileToCannibals = "AI: Relationships - Hostile to Cannibals", // Analogue in BM: Cannibal Killer
			AI_Relationships_HostileToSoldiers = "AI: Relationships - Hostile to Soldiers", // Analogue in BM: Army of Negative One
			AI_Relationships_HostileToVampires = "AI: Relationships - Hostile to Vampires", // Analogue in BM: Vampire Vanquisher
			AI_Relationships_HostileToWerewolves = "AI: Relationships - Hostile to Werewolves", // Analogue in BM: Werewolf Wrecker

			// Roam Behavior
			AI_RoamBehavior_HideInBush = "AI: Roam Behavior - Hide In Bush",
			AI_RoamBehavior_HideInManhole = "AI: Roam Behavior - Hide In Manhole",
			AI_RoamBehavior_Clean = "AI: Roam Behavior - Clean",
			AI_RoamBehavior_Cannibalize = "AI: Roam Behavior - Cannibalize", // May require bite ability?
			AI_RoamBehavior_DrinkBlood = "AI: Roam Behavior - Drink Blood",
			AI_RoamBehavior_EnforceLaws = "AI: Roam Behavior - Enforce Laws",
			AI_RoamBehavior_GrabDrugs = "AI: Roam Behavior - Grab Drugs",
			AI_RoamBehavior_GrabMoney = "AI: Roam Behavior - Grab Money",
			AI_RoamBehavior_Pickpocket = "AI: Roam Behavior - Pickpocket", // Requires Sticky Gloves?
			AI_RoamBehavior_Shakedown = "AI: Roam Behavior - Shakedown",

			// Trait Trigger
			AI_TraitTrigger_CommonFolk = "AI: Trait Trigger - Common Folk",
			AI_TraitTrigger_CoolCannibal = "AI: Trait Trigger - Cool Cannibal",
			AI_TraitTrigger_CopAccess = "AI: Trait Trigger - Cop Access",
			AI_TraitTrigger_HonorableThief = "AI: Trait Trigger - Honorable Thief",

			// Vendor
			AI_Vendor_Armorer = "AI: Vendor - Armorer",
            AI_Vendor_Assassin = "AI: Vendor - Assassin",
            AI_Vendor_Banana = "AI: Vendor - Banana",
			AI_Vendor_Barbarian = "AI: Vendor - Barbarian",
            AI_Vendor_Bartender = "AI: Vendor - Bartender",
            AI_Vendor_Blacksmith = "AI: Vendor - Blacksmith",
            AI_Vendor_Buyer = "AI: Vendor - Buyer",
			AI_Vendor_Cannibal = "AI: Vendor - Cannibal",
            AI_Vendor_ConsumerElectronics = "AI: Vendor - Consumer Electronics",
			AI_Vendor_ConvenienceStore = "AI: Vendor - Convenience Store",
            AI_Vendor_Contraband = "AI: Vendor - Cop Confiscated Goods",
			AI_Vendor_CopStandard = "AI: Vendor - Cop Patrolman's Equipment",
			AI_Vendor_CopSWAT = "AI: Vendor - Cop SWAT Equipment",
            AI_Vendor_Demolitionist = "AI: Vendor - Demolitionist", 
            AI_Vendor_DrugDealer = "AI: Vendor - Drug Dealer",
            AI_Vendor_Firefighter = "AI: Vendor - Firefighter",
            AI_Vendor_Gunsmith = "AI: Vendor - Gunsmith",
            AI_Vendor_HighTech = "AI: Vendor - HighTech",
            AI_Vendor_Hypnotist = "AI: Vendor - Hypnotist",
            AI_Vendor_JunkDealer = "AI: Vendor - Junk Dealer",
            AI_Vendor_McFuds = "AI: Vendor - McFud's",
            AI_Vendor_MedicalSupplier = "AI: Vendor - Medical Supplier",
            AI_Vendor_MiningGear = "AI: Vendor - Mining Gear",
            AI_Vendor_MovieTheater = "AI: Vendor - Movie Theater",
            AI_Vendor_Occultist = "AI: Vendor - Occultist",
            AI_Vendor_OutdoorOutfitter = "AI: Vendor - Trapper Supply",
            AI_Vendor_PawnShop = "AI: Vendor - Pawn Shop",
            AI_Vendor_PestControl = "AI: Vendor - Pest Control",
			AI_Vendor_ResistanceCommissary = "AI: Vendor - Resistance Commissary",
            AI_Vendor_RiotDepot = "AI: Vendor - Riot Depot",
            AI_Vendor_Scientist = "AI: Vendor - Scientist",
            AI_Vendor_Shopkeeper = "AI: Vendor - Shopkeeper",
            AI_Vendor_SlaveShop = "AI: Vendor - Slave Shop",
            AI_Vendor_Soldier = "AI: Vendor - Soldier",
            AI_Vendor_SportingGoods = "AI: Vendor - Sporting Goods",
            AI_Vendor_Teleportationist = "AI: Vendor - Teleportationist",
            AI_Vendor_Thief = "AI: Vendor - Thief",
            AI_Vendor_HardwareStore = "AI: Vendor - Tool Shop",
			AI_Vendor_ToyStore = "AI: Vendor - Toy Store",
            AI_Vendor_UpperCruster = "AI: Vendor - Upper Cruster",
            AI_Vendor_Vampire = "AI: Vendor - Vampire",
            AI_Vendor_Villain = "AI: Vendor - Villain",

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
}
