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
		#region AI
		#region Behavior
			AI_Behavior_CleanTrash = "AI: Behavior - Clean Trash",
			AI_Behavior_Coward = "AI: Behavior - Coward",
			AI_Behavior_GuardDoor = "AI: Behavior - Guard Door",
			AI_Behavior_Guilty = "AI: Behavior - Guilty",
			AI_Behavior_EnforceLaws = "AI: Behavior - Enforce Laws",
			AI_Behavior_EnforceLawsCopBot = "AI: Behavior - Enforce Laws Cop Bot",
			AI_Behavior_EnforceLawsSupercop = "AI: Behavior - Enforce Laws Supercop", // Might be identical to Cop
			AI_Behavior_FightFires = "AI: Behavior - Fight Fires",
			AI_Behavior_GrabDrugs = "AI: Behavior - Grab Drugs",
			AI_Behavior_GrabMoney = "AI: Behavior - Grab Money",
			AI_Behavior_HogTurntables = "AI: Behavior - Turntables Guard",
			AI_Behavior_Lockdown = "AI: Behavior - Lockdown Clearance", // Might not be a thing - could just be The Law
			AI_Behavior_Pacifist = "AI: Behavior - Pacifist",
			AI_Behavior_Scumbag = "AI: Behavior - Scumbag",
			AI_Behavior_SeekAndDestroy = "AI: Behavior - Seek & Destroy", // Killer Robot
			// AI_Behavior_Shakedown = "AI: Behavior - Shakedown", // Gate behind Mugger trait
			AI_Behavior_Tattletale = "AI: Behavior - Tattle-tale",
			AI_Behavior_UseDrugsInCombat = "AI: Behavior - Use Drugs", // Gate behind Drug-A-Lug?
			AI_Behavior_VisionBeams = "AI: Behavior - Vision Beams",
		#endregion
		#region Hire
			AI_Hire_Bodyguard = "AI: Hire - Bodyguard",
			AI_Hire_BreakIn = "AI: Hire - Break-in",
			AI_Hire_CauseRuckus = "AI: Hire - Slum Dweller",
			AI_Hire_CostBanana = "AI: Hire - Costs Banana",
			AI_Hire_CostLess = "AI: Hire - Costs Less",
			AI_Hire_CostMore = "AI: Hire - Costs More",
			AI_Hire_Hack = "AI: Hire - Hacker",
			AI_Hire_Safecrack = "AI: Hire - Safecrack", // Also need to extend vanilla thief behavior to this
			AI_Hire_Tamper = "AI: Hire - Tamper",
		#endregion
		#region Interaction 
			AI_Interaction_AcceptBribeCop = "AI: Interaction - Accept Bribe",
			AI_Interaction_AcceptBribeDoor = "AI: Interaction - Accept Bribe", // Requires Guard Door?
			AI_Interaction_AdministerBloodBag = "AI: Interaction - Administer Blood Bag",
			AI_Interaction_ArenaManager = "AI: Interaction - Arena Manager",
			AI_Interaction_BankTeller = "AI: Interaction - Bank Teller",
			AI_Interaction_BloodBank = "AI: Interaction - Blood Bank",
			AI_Interaction_BuyRound = "AI: Interaction - Buy Round",
			AI_Interaction_DeportationCenter = "AI: Interaction - Deportation Center",
			AI_Interaction_Extortable = "AI: Interaction - Extortable",
			AI_Interaction_Fence = "AI: Interaction - Fence",
			AI_Interaction_Heal = "AI: Interaction - Heal",
			AI_Interaction_Hotel = "AI: Interaction - Hotel",
			AI_Interaction_Identify = "AI: Interaction - Identify",
			AI_Interaction_InfluenceElection = "AI: Interaction - Influence Election",
			AI_Interaction_Moochable = "AI: Interaction - Moochable",
			AI_Interaction_OfferMotivation = "AI: Interaction - Offer Motivation",
			AI_Interaction_PlayBadSong = "AI: Interaction - Song Request",
			AI_Interaction_RefillGuns = "AI: Interaction - Refill Guns",
			AI_Interaction_RepairArmor = "AI: Interaction - Repair Armor",
			AI_Interaction_RepairMelee = "AI: Interaction - Repair Melee Weapons",
			AI_Interaction_SellSlaves = "AI: Interaction - Sell Slaves",
			AI_Interaction_UseBloodBag = "AI: Interaction - Use Blood Bag",
			AI_Interaction_VendorBuyer = "AI: Interaction - Vendor Buyer",
			AI_Interaction_VendorBuyerOnly = "AI: Interaction - Vendor Buyer Only", // Use with Vendor type to make them a buyer only
		#endregion
		#region Relationships
			AI_Relationships_Aggressive = "AI: Relationships - Aggressive", // Hostile to character except with Cool with Cannibals
			AI_Relationships_AnnoyedAtSuspicious = "AI: Relationships - Annoyed at Suspicious",
			AI_Relationships_Faction1Aligned = "AI: Relationships - Faction 1 Aligned",
			AI_Relationships_Faction1Hostile = "AI: Relationships - Faction 1 Hostile",
			AI_Relationships_Faction2Aligned = "AI: Relationships - Faction 2 Aligned",
			AI_Relationships_Faction2Hostile = "AI: Relationships - Faction 2 Hostile",
			AI_Relationships_Faction3Aligned = "AI: Relationships - Faction 3 Aligned",
			AI_Relationships_Faction3Hostile = "AI: Relationships - Faction 3 Hostile",
			AI_Relationships_Faction4Aligned = "AI: Relationships - Faction 4 Aligned",
			AI_Relationships_Faction4Hostile = "AI: Relationships - Faction 4 Hostile",
			AI_Relationships_HostileToCannibals = "AI: Relationships - Hostile to Cannibals", // Analogue in BM: Cannibal Killer
			AI_Relationships_HostileToSoldiers = "AI: Relationships - Hostile to Soldiers", // Analogue in BM: Army of Negative One
			AI_Relationships_HostileToVampires = "AI: Relationships - Hostile to Vampires", // Analogue in BM: Vampire Vanquisher
			AI_Relationships_HostileToWerewolves = "AI: Relationships - Hostile to Werewolves", // Analogue in BM: Werewolf Wrecker
		#endregion
		#region Spawn
			AI_Spawn_BodyguardedGoons = "AI: Spawn - Bodyguarded by Goons", // There is a "Bodyguard" trait in the source code apparently
			AI_Spawn_BodyguardedSupercops = "AI: Spawn - Bodyguarded by Supercops",
			AI_Spawn_BodyguardedSupergoons = "AI: Spawn - Bodyguarded by Supergoons",
			AI_Spawn_Enslaved = "AI: Spawn - Enslaved",
			AI_Spawn_HideInBush = "AI: Spawn - Hide In Bush",
			AI_Spawn_HideInManhole = "AI: Spawn - Hide In Manhole",
			AI_Spawn_RoamingGang = "AI: Spawn - Roaming Gangs",
			AI_Spawn_SlaveOwner = "AI: Spawn - Own Slaves",
		#endregion
		#region Trait Trigger
			AI_TraitTrigger_CommonFolk = "AI: Trait Trigger - Common Folk",
			AI_TraitTrigger_CoolCannibal = "AI: Trait Trigger - Cool Cannibal",
			AI_TraitTrigger_CopAccess = "AI: Trait Trigger - Cop Access",
			AI_TraitTrigger_HonorableThief = "AI: Trait Trigger - Honorable Thief",
		#endregion
		#region Vendor
			AI_Vendor_Armorer = "AI: Vendor - Armorer",
            AI_Vendor_Assassin = "AI: Vendor - Assassin",
            AI_Vendor_BananaBoutique = "AI: Vendor - Banana",
			AI_Vendor_BarbarianMerchant = "AI: Vendor - Barbarian",
            AI_Vendor_Bartender = "AI: Vendor - Bartender",
			AI_Vendor_BartenderDive = "AI: Vendor - Bartender (Dive)",
			AI_Vendor_BartenderFancy = "AI: Vendor - Bartender (Fancy)",
			AI_Vendor_Blacksmith = "AI: Vendor - Blacksmith",
			AI_Vendor_Anthropophagie = "AI: Vendor - Cannibal",
            AI_Vendor_ConsumerElectronics = "AI: Vendor - Consumer Electronics",
			AI_Vendor_ConvenienceStore = "AI: Vendor - Convenience Store",
            AI_Vendor_Contraband = "AI: Vendor - Cop Confiscated Goods",
			AI_Vendor_CopStandard = "AI: Vendor - Cop Patrolman's Equipment",
			AI_Vendor_CopSWAT = "AI: Vendor - Cop SWAT Equipment",
            AI_Vendor_DemolitionDepot = "AI: Vendor - Demolitionist", 
            AI_Vendor_DrugDealer = "AI: Vendor - Drug Dealer",
            AI_Vendor_FirefighterFiveAndDime = "AI: Vendor - Firefighter",
			AI_Vendor_FireSale = "AI: Vendor - Fire Sale",
            AI_Vendor_Gunsmith = "AI: Vendor - Gunsmith",
			AI_Vendor_HardwareStore = "AI: Vendor - Tool Shop",
			AI_Vendor_HighTech = "AI: Vendor - HighTech",
			AI_Vendor_HomeFortressOutlet = "AI: Vendor - Home Fortress Outlet",
            AI_Vendor_Hypnotist = "AI: Vendor - Hypnotist",
            AI_Vendor_JunkDealer = "AI: Vendor - Junk Dealer",
            AI_Vendor_McFuds = "AI: Vendor - McFud's",
            AI_Vendor_MedicalSupplier = "AI: Vendor - Medical Supplier",
            AI_Vendor_MiningGear = "AI: Vendor - Mining Gear",
			AI_Vendor_MonkeMart = "AI: Vendor - Monke Mart",
			AI_Vendor_MovieTheater = "AI: Vendor - Movie Theater",
            AI_Vendor_Occultist = "AI: Vendor - Occultist",
            AI_Vendor_OutdoorOutfitter = "AI: Vendor - Outdoor Outfitter",
			AI_Vendor_PacifistProvisioner = "AI: Vendor - Pacifist Provisioner",
            AI_Vendor_PawnShop = "AI: Vendor - Pawn Shop",
            AI_Vendor_PestControl = "AI: Vendor - Pest Control",
			AI_Vendor_Pharmacy = "AI: Vendor - Pharmacy",
			AI_Vendor_ResistanceCommissary = "AI: Vendor - Resistance Commissary",
            AI_Vendor_RiotInc = "AI: Vendor - Riot Depot",
            AI_Vendor_ResearchMaterials = "AI: Vendor - Scientist",
            AI_Vendor_Shopkeeper = "AI: Vendor - Shopkeeper",
            AI_Vendor_SlaveShop = "AI: Vendor - Slave Shop",
            AI_Vendor_Soldier = "AI: Vendor - Soldier",
            AI_Vendor_SportingGoods = "AI: Vendor - Sporting Goods",
            AI_Vendor_Teleportationist = "AI: Vendor - Teleportationist",
            AI_Vendor_Thief = "AI: Vendor - Thief",
			AI_Vendor_ThiefMaster = "AI: Vendor - Thief Master",
			AI_Vendor_ThrowceryStore = "AI: Vendor - Throwcery Store",
			AI_Vendor_ToyStore = "AI: Vendor - Toy Store",
            AI_Vendor_UpperCruster = "AI: Vendor - Upper Cruster",
            AI_Vendor_Vampire = "AI: Vendor - Vampire",
            AI_Vendor_Villain = "AI: Vendor - Villain",
		#endregion
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
		#region Loadout
			Loadout_Shapeshifter = "Loadout: Shapeshifter",
            Loadout_Soldier = "Loadout: Soldier",
            Loadout_Thief = "Loadout: Thief",
            Loadout_Worker = "Loadout: Worker",
		#endregion
		#region Misc
			Chunk_KeyHolder = "Key Holder",
			Chunk_QuestGiver = "Quest Giver",
			Chunk_SafeComboHolder = "Safe Combo Holder",
		#endregion
		#region Map Marker
			MapMarker_Bartender = "Map Marker: Bartender",
            MapMarker_DrugDealer = "Map Marker: DrugDealer",
            MapMarker_Face = "Map Marker: Face",
            MapMarker_KillerRobot = "Map Marker: KillerRobot",
            MapMarker_QuestionMark = "Map Marker: QuestionMark",
            MapMarker_Shopkeeper = "Map Marker: Shopkeeper";
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

	public static class vSpecialAbility // Vanilla Special Abilities
	{
		public const string
				Bite = "Bite",
				Camouflage = "Camouflage",
				Cannibalize = "Cannibalize",
				Chaaarge = "Chaaarge!",
				ChloroformHankie = "Chloroform Hankie",
				CryProfusely = "Cry Profusely",
				Enslave = "Enslave",
				Handcuffs = "Handcuffs",
				Joke = "Joke",
				Laptop = "Laptop",
				LaserGun = "Laser Gun",
				MechTransformation = "Mech Transformation",
				MindControl = "Mind Control",
				Possess = "Possess",
				PowerSap = "Power Sap",
				PrimalLunge = "Primal Lunge",
				SharpLunge = "Sharp Lunge",
				StickyGlove = "Sticky Glove",
				Stomp = "Stomp",
				Toss = "Toss",
				WaterCannon = "Water Cannon",
				WerewolfTransformation = "Werewolf Transformation",
				ZombieSpit = "Zombie Spit";
	}
}
