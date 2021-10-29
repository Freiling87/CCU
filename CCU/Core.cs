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

        public static readonly ManualLogSource logger = CCULogger.GetLogger();
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

	public static class CJob // These names are extra but I want to ensure they're unique
	{
		public const string
			DisarmTrap =							"[CCU] Job - DisarmTrap",
			Pickpocket =							"[CCU] Job - Pickpocket",
			Poison =								"[CCU] Job - Poison",
			SafecrackSafe =							"[CCU] Job - SafecrackSafe",
			SafecrackSafeReal =						"[CCU] Job - SafecrackSafeReal",
			TamperSomething =						"[CCU] Job - TamperSomething",
			TamperSomethingReal =					"[CCU] Job - TamperSomethingReal";
	}

	public static class CMutators
	{
		public const string
		#region Followers
			HomesicknessDisabled =					"[CCU] Homesickness Disabled",
			HomesicknessMandatory =					"[CCU] Homesickness Mandatory",
		#endregion
		#region Interface
			ScrollingButtonHeight50 =				"[CCU] Interface - Scrolling Button Height 50%",
			ScrollingButtonHeight75 =				"[CCU] Interface - Scrolling Button Height 75%",
			ScrollingButtonTextAlignLeft =			"[CCU] Interface - Scrolling Button Text Align Left",
			ScrollingButtonTextSizeStatic =			"[CCU] Interface - Scrolling Button Text Size Static",
		#endregion
		#region Interface Themes
																											//		BACKGROUND			TEXT COLOR		FONT
			InterfaceTheme_BloodMoon =				"[CCU] Interface Theme - Blood Moon",					//	Burgundy			Moon White			Gothic
			InterfaceTheme_Cyberpunk =				"[CCU] Interface Theme - Cyberpunk",					//	Black				Pink or Cyan		
			InterfaceTheme_Drone =					"[CCU] Interface Theme - Drone",						//	Post-It Yellow		Pencil Black		Boring
			InterfaceTheme_GovernmentIssue =		"[CCU] Interface Theme - Government Issue",				//	OD Green			White				Stenciled
			InterfaceTheme_McFuds =					"[CCU] Interface Theme - McFud's",						//	Firetruck Red		Yellow				Something LOUD
			InterfaceTheme_Mojave =					"[CCU] Interface Theme - Mojave",						//	Orange-ish Black	Amber				Monospace
			InterfaceTheme_Parchment =				"[CCU] Interface Theme - Parchment",					//	Parchment			Charcoal Black		Something scripty
			InterfaceTheme_Radioactive =			"[CCU] Interface Theme - Radioactive",					//	Yello				Black				Stenciled
		#endregion
		#region Level Branching
			Level_Alpha =							"[CCU] Level Alpha",
			Level_Beta =							"[CCU] Level Beta",
			Level_Gamma =							"[CCU] Level Gamma",
			Level_Delta =							"[CCU] Level Delta",
			LevelExit_Alpha =						"[CCU] Level Exit Alpha",
			LevelExit_Beta =						"[CCU] Level Exit Beta",
			LevelExit_Gamma =						"[CCU] Level Exit Gamma",
			LevelExit_Delta =						"[CCU] Level Exit Delta",
			LevelExit_Plus1 =						"[CCU] Level Exit +1",
			LevelExit_Plus2 =						"[CCU] Level Exit +2",
			LevelExit_Plus3 =						"[CCU] Level Exit +3",
			LevelExit_Plus4 =						"[CCU] Level Exit +4",
			Require_False_A =						"[CCU] Require False: A",
			Require_False_B =						"[CCU] Require False: B",
			Require_False_C =						"[CCU] Require False: C",
			Require_False_D =						"[CCU] Require False: D",
			Require_True_A =						"[CCU] Require True: A",
			Require_True_B =						"[CCU] Require True: B",
			Require_True_C =						"[CCU] Require True: C",
			Require_True_D =						"[CCU] Require True: D",
			Set_False_A =							"[CCU] Set False: A",
			Set_False_B =							"[CCU] Set False: B",
			Set_False_C =							"[CCU] Set False: C",
			Set_False_D =							"[CCU] Set False: D",
			Set_True_A =							"[CCU] Set True: A",
			Set_True_B =							"[CCU] Set True: B",
			Set_True_C =							"[CCU] Set True: C",
			Set_True_D =							"[CCU] Set True: D",
		#endregion
		#region Lighting
			DarkerDarkness =						"[CCU] Darker Darkness",
			NoAgentLights =							"[CCU] No Agent Lights",
			NoItemLights =							"[CCU] No Item Lights",
			NoObjectLights =						"[CCU] No Object Lights",
		#endregion
		#region Wreckage
			AmbienterAmbience =						"[CCU] Ambient-er Ambience",
			FloralerFlora =							"[CCU] Floral-er Flora",
			LitterallyTheWorst =					"[CCU] Litter-ally the Worst",
			ShittierToilets =						"[CCU] Shittier Toilets",
			TrashierTrashcans =						"[CCU] Trashier Trashcans",
		#endregion
		#region Utility
			SortMutatorsByName =					"! Sort Mutators by Name";
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
		#region Appearance
		#region Accessory
			Accessory_CopHat =					"[CCU] Accessory - Cop Hat",
			Accessory_DoctorHeadlamp =			"[CCU] Accessory - Doctor Headlamp",
			Accessory_Fedora =					"[CCU] Accessory - Fedora",
			Accessory_FireHelmet =				"[CCU] Accessory - Fire Helmet",
			Accessory_HackerGlasses =			"[CCU] Accessory - Hacker Glasses",
			Accessory_HatBlue =					"[CCU] Accessory - Hat (Blue)",
			Accessory_HatRed =					"[CCU] Accessory - Hat (Red)",
			Accessory_Headphones =				"[CCU] Accessory - Headphones",
			Accessory_None =					"[CCU] Accessory - None",
			Accessory_Sunglasses =				"[CCU] Accessory - Sunglasses",
			Accessory_SupercopHat =				"[CCU] Accessory - Supercop Hat",
			Accessory_ThiefHat =				"[CCU] Accessory - Thief Hat",
		#endregion
		#region Eyes

		#endregion
		#region Facial Hair
			FacialHair_Beard =					"[CCU] Facial Hair - Beard",
			FacialHair_Mustache =				"[CCU] Facial Hair - Mustache",
			FacialHair_MustacheCircus =			"[CCU] Facial Hair - MustacheCircus",
			FacialHair_MustacheRedneck =		"[CCU] Facial Hair - MustacheRedneck",
			FacialHair_None =					"[CCU] Facial Hair - None",
		#endregion
		#region Hair Color
			HairColor_Brown =					"[CCU] Hair Color - Brown",
			HairColor_Black =					"[CCU] Hair Color - Black",
			HairColor_Blonde =					"[CCU] Hair Color - Blonde",
			HairColor_Blue =					"[CCU] Hair Color - Blue",
			HairColor_Green =					"[CCU] Hair Color - Green",
			HairColor_Grey =					"[CCU] Hair Color - Grey",
			HairColor_Orange =					"[CCU] Hair Color - Orange",
			HairColor_Pink =					"[CCU] Hair Color - Pink",
			HairColor_Purple =					"[CCU] Hair Color - Purple",
			HairColor_Red =						"[CCU] Hair Color - Red",
		#endregion
		#region Hairstyle
			Hairstyle_Afro =					"[CCU] Hairstyle - Afro",
			Hairstyle_AlienHead =				"[CCU] Hairstyle - Alien Head",
			Hairstyle_AssassinMask =			"[CCU] Hairstyle - Assassin Mask",
			Hairstyle_Bald =					"[CCU] Hairstyle - Bald",
			Hairstyle_Balding =					"[CCU] Hairstyle - Balding",
			Hairstyle_BangsLong =				"[CCU] Hairstyle - BangsLong",
			Hairstyle_BangsMedium =				"[CCU] Hairstyle - BangsMedium",
			Hairstyle_ButlerBot =				"[CCU] Hairstyle - Butler Bot Head",
			Hairstyle_Curtains =				"[CCU] Hairstyle - Curtains",
			Hairstyle_Cutoff =					"[CCU] Hairstyle - Cutoff",
			Hairstyle_FlatLong =				"[CCU] Hairstyle - FlatLong",
			Hairstyle_GorillaHead =				"[CCU] Hairstyle - Gorilla Head",
			Hairstyle_HoboBeard =				"[CCU] Hairstyle - HoboBeard",
			Hairstyle_Hoodie =					"[CCU] Hairstyle - Hoodie",
			Hairstyle_Leia =					"[CCU] Hairstyle - Leia",
			Hairstyle_MessyLong =				"[CCU] Hairstyle - MessyLong",
			Hairstyle_Military =				"[CCU] Hairstyle - Military",
			Hairstyle_Mohawk =					"[CCU] Hairstyle - Mohawk",
			Hairstyle_Normal =					"[CCU] Hairstyle - Normal",
			Hairstyle_NormalHigh =				"[CCU] Hairstyle - NormalHigh",
			Hairstyle_Pompadour =				"[CCU] Hairstyle - Pompadour",
			Hairstyle_Ponytail =				"[CCU] Hairstyle - Ponytail",
			Hairstyle_PuffyLong =				"[CCU] Hairstyle - PuffyLong",
			Hairstyle_PuffyShort =				"[CCU] Hairstyle - PuffyShort",
			Hairstyle_RobotHead =				"[CCU] Hairstyle - Robot Head",
			Hairstyle_Sidewinder =				"[CCU] Hairstyle - Sidewinder",
			Hairstyle_SlaverMask =				"[CCU] Hairstyle - Slavemaster Mask",
			Hairstyle_Spiky =					"[CCU] Hairstyle - Spiky",
			Hairstyle_SpikyShort =				"[CCU] Hairstyle - SpikyShort",
			Hairstyle_Suave =					"[CCU] Hairstyle - Suave",
			Hairstyle_Wave =					"[CCU] Hairstyle - Wave",
			Hairstyle_WerewolfHead =			"[CCU] Hairstyle - Werewolf Head",
		#endregion
		#region Skin Color	
			SkinColor_SuperPale =				"[CCU] Skin Color - SuperPaleSkin",
			SkinColor_Pale =					"[CCU] Skin Color - PaleSkin",
			SkinColor_White =					"[CCU] Skin Color - WhiteSkin",
			SkinColor_Pink =					"[CCU] Skin Color - PinkSkin",
			SkinColor_Gold =					"[CCU] Skin Color - GoldSkin",
			SkinColor_Mixed =					"[CCU] Skin Color - MixedSkin",
			SkinColor_LightBlack =				"[CCU] Skin Color - LightBlackSkin",
			SkinColor_Black =					"[CCU] Skin Color - BlackSkin",
			SkinColor_Zombie1 =					"[CCU] Skin Color - ZombieSkin1",
			SkinColor_Zombie2 =					"[CCU] Skin Color - ZombieSkin2",
		#endregion
		#endregion
		#region Active
			Active_CleanTrash =					"[CCU] Active - Clean Trash",
			Active_EatCorpse =					"[CCU] Active - Eat Corpse",
			Active_EnforceLaws =				"[CCU] Active - Enforce Laws (Cop)",
			Active_EnforceLawsCopBot =			"[CCU] Active - Enforce Laws (Cop Bot)",
			Active_FightFires =					"[CCU] Active - Fight Fires",
			Active_GrabDrugs =					"[CCU] Active - Grab Drugs",
			Active_GrabMoney =					"[CCU] Active - Grab Money",
			Active_GuardDoor =					"[CCU] Active - Guard Door",
			Active_HogTurntables =				"[CCU] Active - Turntables Guard",
			Active_Lockdown =					"[CCU] Active - Lockdown Clearance", // Might not be a thing - could just be The Law
			Active_Pickpocket =					"[CCU] Active - Pickpocket",
			Active_SeekAndDestroy =				"[CCU] Active - Seek & Destroy",
			Active_SuckBlood =					"[CCU] Active - Suck Blood",
			Active_Shakedown =					"[CCU] Active - Shakedown",
			Active_Tattletale =					"[CCU] Active - Tattletale",
		#endregion
		#region Bodyguarded
			Bodyguarded_Pilot =					"[CCU] Bodyguarded: Pilot",
			Bodyguarded_Cops =					"[CCU] Bodyguarded: Cops",
			Bodyguarded_Goons =					"[CCU] Bodyguarded: Goons",
			Bodyguarded_Supercops =				"[CCU] Bodyguarded: Supercops",
			Bodyguarded_Supergoons =			"[CCU] Bodyguarded: Supergoons",
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
			Hire_Permanent =					"[CCU] Hire - Permanent",
			Hire_PermanentOnly =				"[CCU] Hire - Permanent Only",
			Hire_Pickpocket =					"[CCU] Hire - Pickpocket",
			Hire_Poison	=						"[CCU] Hire - Poison",
			Hire_Safecrack =					"[CCU] Hire - Safecrack", // Also need to extend vanilla thief behavior to this
			Hire_Tamper =						"[CCU] Hire - Tamper",
		#endregion
		#region Interaction
			Interaction_AdministerBloodBag =	"[CCU] Interaction - Administer Blood Bag",
			Interaction_ArenaManager =			"[CCU] Interaction - Arena Manager",
			Interaction_BankTeller =			"[CCU] Interaction - Bank Teller",
			Interaction_BloodBank =				"[CCU] Interaction - Blood Bank", 
			Interaction_BribeCops =				"[CCU] Interaction - Bribe Cops",
			Intearction_BribeEntryBeer =		"[CCU] Interaction - Bribe for Entry (Beer)",// Requires Passive Guard Door?
			Interaction_BribeEntryMoney =		"[CCU] Interaction - Bribe for Entry (Money)", 
			Interaction_BribeEntryWhiskey =		"[CCU] Interaction - Bribe for Entry (Whiskey)",
			Interaction_BuyerAll =				"[CCU] Interaction - Buyer All", 
			Interaction_BuyerOnly =				"[CCU] Interaction - Buyer Only",
			Interaction_BuyerVendor =			"[CCU] Interaction - Buyer Vendor",
			Interaction_BuyRound =				"[CCU] Interaction - Buy Round",
			Interaction_DeportationCenter =		"[CCU] Interaction - Deportation Center",
			Interaction_Extortable =			"[CCU] Interaction - Extortable",
			Interaction_Heal =					"[CCU] Interaction - Heal",
			Interaction_HealAll =				"[CCU] Interaction - Heal All",
			Interaction_HealOther =				"[CCU] Interaction - Heal Other",
			Interaction_HotelManager =			"[CCU] Interaction - Hotel Manager",
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
			Passive_ExplodeOnDeathLarge =		"[CCU] Passive - Explode On Death (Large)",
			Passive_ExplodeOnDeathMolotov =		"[CCU] Passive - Explode On Death (Molotov)",
			Passive_ExplodeOnDeathSlime =		"[CCU] Passive - Explode On Death (Slime)",
			Passive_ExplodeOnDeathWater =		"[CCU] Passive - Explode On Death (Water)",
			Passive_Guilty =					"[CCU] Passive - Guilty",
			Passive_Hackable_Haywire =			"[CCU] Passive - Hackable (Haywire)",
			Passive_Hackable_TamperWAim =		"[CCU] Passive - Hackable (Tamper w/ Aim)",
			Passive_VisionBeams =				"[CCU] Passive - Vision Beams",
		#endregion
		#region Relationships
			Relationships_AggressiveCannibal =	"[CCU] Relationships - Aggressive (Cannibal)",   // Hostile to character except with Cool with Cannibals
			Relationships_AnnoyedAtSuspicious = "[CCU] Relationships - Annoyed at Suspicious",
			Relationships_Faction1Aligned =		"[CCU] Relationships - Faction 1 Aligned",
			Relationships_Faction1Hostile =		"[CCU] Relationships - Faction 1 Hostile",
			Relationships_Faction2Aligned =		"[CCU] Relationships - Faction 2 Aligned",
			Relationships_Faction2Hostile =		"[CCU] Relationships - Faction 2 Hostile",
			Relationships_Faction3Aligned =		"[CCU] Relationships - Faction 3 Aligned",
			Relationships_Faction3Hostile =		"[CCU] Relationships - Faction 3 Hostile",
			Relationships_Faction4Aligned =		"[CCU] Relationships - Faction 4 Aligned",
			Relationships_Faction4Hostile =		"[CCU] Relationships - Faction 4 Hostile",
			Relationships_HostileToCannibals =	"[CCU] Relationships - Hostile to Cannibals",    // Analogue in BM: Cannibal Killer
			Relationships_HostileToSoldiers =	"[CCU] Relationships - Hostile to Soldiers",  // Analogue in BM: Army of Negative One
			Relationships_HostileToVampires =	"[CCU] Relationships - Hostile to Vampires",  // Analogue in BM: Vampire Vanquisher
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
		#region Utility
			Utility_SortTraitsByName =				"[CCU] ! Sort by Name",
			Utility_SortTraitsByValue =				"[CCU] ! Sort by Value",
		#endregion
		#region Vendor
			Vendor_Anthropophagie =				"[CCU] Vendor - Anthropophagie",
			Vendor_Armorer =					"[CCU] Vendor - Armorer",
			Vendor_Assassin =					"[CCU] Vendor - Assassin",
			Vendor_BananaBoutique =				"[CCU] Vendor - Banana",
			Vendor_BarbarianMerchant =			"[CCU] Vendor - Barbarian",
			Vendor_Bartender =					"[CCU] Vendor - Bartender",
			Vendor_BartenderDive =				"[CCU] Vendor - Bartender (Dive)",
			Vendor_BartenderFancy =				"[CCU] Vendor - Bartender (Fancy)",
			Vendor_Blacksmith =					"[CCU] Vendor - Blacksmith",
			Vendor_ConsumerElectronics =		"[CCU] Vendor - Consumer Electronics",
			Vendor_ConvenienceStore =			"[CCU] Vendor - Convenience Store",
			Vendor_Contraband =					"[CCU] Vendor - Cop Confiscated Goods",
			Vendor_CopStandard =				"[CCU] Vendor - Cop Patrolman's Equipment",
			Vendor_CopSWAT =					"[CCU] Vendor - Cop SWAT Equipment",
			Vendor_DemolitionDepot =			"[CCU] Vendor - Demolitionist",
			Vendor_DrugDealer =					"[CCU] Vendor - Drug Dealer",
			Vendor_FirefighterFiveAndDime =		"[CCU] Vendor - Firefighter",
			Vendor_FireSale =					"[CCU] Vendor - Fire Sale",
			Vendor_GunDealer =					"[CCU] Vendor - Gun Dealer",
			Vendor_GunDealerHeavy =				"[CCU] Vendor - Gun Dealer (Heavy)",
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
