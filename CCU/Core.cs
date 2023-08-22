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
using System.Linq;

namespace CCU
{
    [BepInPlugin(pluginGUID, pluginName, pluginVersion)]
	[BepInDependency(RogueLibs.GUID, RogueLibs.CompiledVersion)]
	public class Core : BaseUnityPlugin
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		private static GameController GC => GameController.gameController;

		public const string pluginGUID = "Freiling87.streetsofrogue.CCU";
		public const string pluginName = "CCU " + (designerEdition ? "[D]" : "[P]"); 
		public const string pluginVersion = "1.3.0";
		public const string subVersion = "a";
		public static Sprite CCULogoSprite;

		public const bool designerEdition = true;
		public const bool developerEdition = true;
		public const bool debugMode = true;

		public void Awake()
		{
			Harmony harmony = new Harmony(pluginGUID);
			harmony.PatchAll();
			PatcherUtils.PatchAll(harmony);
			RogueLibs.LoadFromAssembly();
			RogueLibs.CreateVersionText(pluginGUID, pluginName + " v" + pluginVersion + subVersion);
			CCULogoSprite = RogueLibs.CreateCustomSprite(nameof(Properties.Resources.CCU_160x160), SpriteScope.Interface, Properties.Resources.CCU_160x160).Sprite;
		}
		public static void LogMethodCall([CallerMemberName] string callerName = "") =>
			logger.LogInfo(callerName + ": Method Call");

		public const string 
			CCUBlockTag = "[CCU]",

			z = "";
	}

	public static class CoreTools
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		private static GameController GC => GameController.gameController;

		public static readonly System.Random random = new System.Random();

		public static T GetMethodWithoutOverrides<T>(this MethodInfo method, object callFrom)
				where T : Delegate
		{
			IntPtr ptr = method.MethodHandle.GetFunctionPointer();
			return (T)Activator.CreateInstance(typeof(T), callFrom, ptr);
		}

		public static string GetRandomMember(List<string> list) =>
			list[random.Next(0, list.Count - 1)];

		public static List<TBase> AllClassesOfType<TBase>() where TBase : class
		{
			List<TBase> list = new List<TBase>();
			var derivedTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(TBase)));

			foreach (var type in derivedTypes)
			{
				var instance = Activator.CreateInstance(type) as TBase;
				list.Add(instance);
			}

			return list;
		}

		public static bool ContainsAll<T>(List<T> containingList, List<T> containedList) =>
			!containedList.Except(containingList).Any();

		// TODO: Works. This can be a lambda
		public static List<string> GetAllStringConstants(Type type)
		{
			var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
			var stringConstants = new List<string>();

			foreach (var field in fields)
			{
				if (field.IsLiteral && field.FieldType == typeof(string))
				{
					string value = field.GetValue(null) as string;
					stringConstants.Add(value);
				}
			}

			return stringConstants;
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
