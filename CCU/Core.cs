using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using BTHarmonyUtils;
using UnityEngine;

namespace CCU
{
    [BepInPlugin(pluginGUID, pluginName, pluginVersion)]
	[BepInDependency(RogueLibs.GUID, RogueLibs.CompiledVersion)]
	public class Core : BaseUnityPlugin
	{
		public const string pluginGUID = "Freiling87.streetsofrogue.CCU";
		public const string pluginName = "CCU " + (designerEdition ? "[D]" : "[P]"); 
		public const string pluginVersion = "1.1.1";
		public const string subVersion = "b";
		public const bool designerEdition = true;
		public const bool debugMode = true;

		public static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public void Awake()
		{
			LogMethodCall();

			Harmony harmony = new Harmony(pluginGUID);
			harmony.PatchAll();
			PatcherUtils.PatchAll(harmony);
			RogueLibs.LoadFromAssembly();
			RogueLibs.CreateVersionText(pluginGUID, pluginName + " v" + pluginVersion + subVersion); 
		}
		public static void LogMethodCall([CallerMemberName] string callerName = "") =>
			logger.LogInfo(callerName + ": Method Call");
	}

	public static class CoreTools
	{
		public static readonly System.Random random = new System.Random();

		public static T GetMethodWithoutOverrides<T>(this MethodInfo method, object callFrom)
				where T : Delegate
		{
			IntPtr ptr = method.MethodHandle.GetFunctionPointer();
			return (T)Activator.CreateInstance(typeof(T), callFrom, ptr);
		}

		public static string GetRandomMember(List<string> list) =>
			list[random.Next(0, list.Count)];
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

	public static class CoroutineExecutor
	{
		private class ExecutorBehaviour : MonoBehaviour { }

		private static GameObject executorGO;
		private static ExecutorBehaviour executorBehaviour;

		private static void EnsureExists()
		{
			if (executorGO != null)
			{
				return;
			}
			executorGO = new GameObject("CoroutineExecutorObject");
			executorBehaviour = executorGO.AddComponent<ExecutorBehaviour>();
            UnityEngine.Object.DontDestroyOnLoad(executorGO);
		}

		public static Coroutine StartCoroutine(IEnumerator routine)
		{
			EnsureExists();
			return executorBehaviour.StartCoroutine(routine);
		}
	}

	public static class CJob // These names are extra but I want to ensure they're unique
	{
		public const string
			DisarmTrap = "[CCU] Job - DisarmTrap",
			Pickpocket = "[CCU] Job - Pickpocket",
			Poison = "[CCU] Job - Poison",
			SafecrackSafe = "[CCU] Job - SafecrackSafe",
			SafecrackSafeReal = "[CCU] Job - SafecrackSafeReal",
			TamperSomething = "[CCU] Job - TamperSomething",
			TamperSomethingReal = "[CCU] Job - TamperSomethingReal";
	}
	public static class CMutators
	{
		public const string
		#region Interface Themes
			//		BACKGROUND			TEXT COLOR			FONT
			InterfaceTheme_BloodMoon = "[CCU] Interface Theme - Blood Moon",                    //	Burgundy			Moon White			Gothic
			InterfaceTheme_Cyberpunk = "[CCU] Interface Theme - Cyberpunk",                 //	Black				Pink or Cyan		
			InterfaceTheme_Drone = "[CCU] Interface Theme - Drone",                     //	Post-It Yellow		Pencil Black		Boring
			InterfaceTheme_GovernmentIssue = "[CCU] Interface Theme - Government Issue",                //	OD Green			White				Stenciled
			InterfaceTheme_McFuds = "[CCU] Interface Theme - McFud's",                      //	Firetruck Red		Yellow				Something LOUD
			InterfaceTheme_Mojave = "[CCU] Interface Theme - Mojave",                       //	Orange-ish Black	Amber				Monospace
			InterfaceTheme_Parchment = "[CCU] Interface Theme - Parchment",                 //	Parchment			Charcoal Black		Something scripty
			InterfaceTheme_Radioactive = "[CCU] Interface Theme - Radioactive",                 //	Yello				Black				Stenciled
		#endregion
		#region Level Branching
			Level_Alpha = "[CCU] Level Alpha",
			Level_Beta = "[CCU] Level Beta",
			Level_Gamma = "[CCU] Level Gamma",
			Level_Delta = "[CCU] Level Delta",
			LevelExit_Alpha = "[CCU] Level Exit Alpha",
			LevelExit_Beta = "[CCU] Level Exit Beta",
			LevelExit_Gamma = "[CCU] Level Exit Gamma",
			LevelExit_Delta = "[CCU] Level Exit Delta",
			LevelExit_Plus1 = "[CCU] Level Exit +1",
			LevelExit_Plus2 = "[CCU] Level Exit +2",
			LevelExit_Plus3 = "[CCU] Level Exit +3",
			LevelExit_Plus4 = "[CCU] Level Exit +4",
			Require_False_A = "[CCU] Require False: A",
			Require_False_B = "[CCU] Require False: B",
			Require_False_C = "[CCU] Require False: C",
			Require_False_D = "[CCU] Require False: D",
			Require_True_A = "[CCU] Require True: A",
			Require_True_B = "[CCU] Require True: B",
			Require_True_C = "[CCU] Require True: C",
			Require_True_D = "[CCU] Require True: D",
			Set_False_A = "[CCU] Set False: A",
			Set_False_B = "[CCU] Set False: B",
			Set_False_C = "[CCU] Set False: C",
			Set_False_D = "[CCU] Set False: D",
			Set_True_A = "[CCU] Set True: A",
			Set_True_B = "[CCU] Set True: B",
			Set_True_C = "[CCU] Set True: C",
			Set_True_D = "[CCU] Set True: D";
		#endregion
		#region Utility
		//SortMutatorsByName = "! Sort Mutators by Name";
		#endregion
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
		#region Map Marker
			MapMarker_Bartender = "[CCU] Map Marker: Bartender",
			MapMarker_DrugDealer = "[CCU] Map Marker: DrugDealer",
			MapMarker_Face = "[CCU] Map Marker: Face",
			MapMarker_KillerRobot = "[CCU] Map Marker: KillerRobot",
			MapMarker_Pilot = "[CCU] Map Marker: Pilot",
			MapMarker_QuestionMark = "[CCU] Map Marker: QuestionMark",
			MapMarker_Shopkeeper = "[CCU] Map Marker: Shopkeeper",
        #endregion
		#region Spawn
			HideInObject = "[CCU] Spawn: Hide In Bush",
			Spawn_RoamingGang = "[CCU] Spawn: Roaming Gangs",
			Spawn_SlaveOwned = "[CCU] Spawn: Slave", // Should these be in Relationships instead?
			Spawn_SlaveOwner = "[CCU] Spawn: Slave Owner",
		#endregion
			NoMoreSemiColons = "lol";
	}
	public static class cItem
    {
		public const string
			StashHint = "StashHint",

			z = "z";
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
			CodPiece = "CodPiece",
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
			MayorBadge = "MayorBadge",
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

		public static List<string> RLists = new List<string>()
		{

		};
	}
	public static class vMutator // Vanilla Mutators
	{
		public const string
			AssassinsEveryLevel = "AssassinsEveryLevel",
			BigKnockbackForAll = "BigKnockbackForAll",
			CannibalsDontAttack = "CannibalsDontAttack",
			DoctorsMoreImportant = "DoctorsMoreImportant",
			EveryoneHatesYou = "EveryoneHatesYou",
			ExplodingBodies = "ExplodingBodies",
			FullHealth = "FullHealth",
			GorillaTown = "GorillaTown",
			HalfHealth = "HalfHealth",
			HighCost = "HighCost",
			InfiniteAmmo = "InfiniteAmmo",
			InfiniteAmmoNormalWeapons = "InfiniteAmmoNormalWeapons",
			InfiniteMeleeDurability = "InfiniteMeleeDurability",
			LowHealth = "LowHealth",
			ManyWerewolf = "ManyWerewolf",
			MixedUpLevels = "MixedUpLevels",
			MoneyRewards = "MoneyRewards",
			NoCops = "NoCops",
			NoCowards = "NoCowards",
			NoGuns = "NoGuns",
			NoLimits = "NoLimits",
			NoMelee = "NoMelee",
			RocketLaunchers = "RocketLaunchers",
			RogueVision = "RogueVision",
			SlowDown = "SlowDown",
			SpeedUp = "SpeedUp",
			SupercopsReplaceCops = "SupercopsReplaceCops",
			TimeLimit = "TimeLimit",
			TimeLimit2 = "TimeLimit2",
			TimeLimitQuestsGiveMoreTime = "TimeLimitQuestsGiveMoreTime",
			ZombieMutator = "ZombieMutator",
			ZombiesWelcome = "ZombiesWelcome";

		public static List<string> VanillaMutators = new List<string>()
		{
			AssassinsEveryLevel,
			BigKnockbackForAll,
			CannibalsDontAttack,
			DoctorsMoreImportant,
			EveryoneHatesYou,
			ExplodingBodies,
			FullHealth,
			GorillaTown,
			HalfHealth,
			HighCost,
			InfiniteAmmo,
			InfiniteAmmoNormalWeapons,
			InfiniteMeleeDurability,
			LowHealth,
			ManyWerewolf,
			MixedUpLevels,
			MoneyRewards,
			NoCops,
			NoCowards,
			NoGuns,
			NoLimits,
			NoMelee,
			RocketLaunchers,
			RogueVision,
			SlowDown,
			SpeedUp,
			SupercopsReplaceCops,
			TimeLimit,
			TimeLimit2,
			TimeLimitQuestsGiveMoreTime,
			ZombieMutator,
			ZombiesWelcome,
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
				SellOMatic = "PawnShopMachine",
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
