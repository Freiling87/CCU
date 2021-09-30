using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace CCU.Patches.Inventory
{
	[HarmonyPatch(declaringType: typeof(RandomItems))]
	class P_RandomItems
	{
		[HarmonyPostfix, HarmonyPatch(methodName: nameof(RandomItems.fillItems))]
		public static void fillItems_Postfix()
		{
			Core.LogMethodCall();

			RandomSelection sel = GameObject.Find("ScriptObject").GetComponent<RandomSelection>();

			RandomList rList = sel.CreateRandomList(CTrait.AI_Vendor_Armorer, "Items", "Item");
			sel.CreateRandomElement(rList, vItem.ArmorDurabilitySpray, 2);
			sel.CreateRandomElement(rList, vItem.BulletproofVest, 3);
			sel.CreateRandomElement(rList, vItem.Codpiece, 2);
			sel.CreateRandomElement(rList, vItem.FireproofSuit, 1);
			sel.CreateRandomElement(rList, vItem.GasMask, 1);
			sel.CreateRandomElement(rList, vItem.HardHat, 1);
			sel.CreateRandomElement(rList, vItem.SoldierHelmet, 3);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_Assassin, "Items", "Item");
			sel.CreateRandomElement(rList, vItem.Blindenizer, 1);
			sel.CreateRandomElement(rList, vItem.CardboardBox, 1);
			sel.CreateRandomElement(rList, vItem.EarWarpWhistle, 1);
			sel.CreateRandomElement(rList, vItem.KillProfiter, 1);
			sel.CreateRandomElement(rList, vItem.Shuriken, 4);
			sel.CreateRandomElement(rList, vItem.Silencer, 1);
			sel.CreateRandomElement(rList, vItem.Sword, 3);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_Banana, "Items", "Item");
			sel.CreateRandomElement(rList, vItem.Banana, 9);
			sel.CreateRandomElement(rList, vItem.BananaPeel, 1);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_Barbarian, "Items", "Item");
			sel.CreateRandomElement(rList, vItem.Axe, 3);
			sel.CreateRandomElement(rList, vItem.BaconCheeseburger, 3);
			sel.CreateRandomElement(rList, vItem.Beer, 6);
			sel.CreateRandomElement(rList, vItem.BraceletofStrength, 2);
			sel.CreateRandomElement(rList, vItem.Codpiece, 3);
			sel.CreateRandomElement(rList, vItem.HamSandwich, 3);
			sel.CreateRandomElement(rList, vItem.Sword, 3);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_Bartender, "Items", "Item"); // Vanilla
			sel.CreateRandomElement(rList, "Alcohol", 3); // TODO: Check this out and possibly use it
			sel.CreateRandomElement(rList, vItem.Cocktail, 3);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_BartenderDive, "Items", "Item"); 
			sel.CreateRandomElement(rList, vItem.Beer, 3);
			sel.CreateRandomElement(rList, vItem.HotFud, 3);
			sel.CreateRandomElement(rList, vItem.MolotovCocktail, 1);
			sel.CreateRandomElement(rList, vItem.Whiskey, 3);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_BartenderFancy, "Items", "Item"); 
			sel.CreateRandomElement(rList, vItem.Cocktail, 3);
			sel.CreateRandomElement(rList, vItem.Sugar, 3);
			sel.CreateRandomElement(rList, vItem.Whiskey, 3);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_Blacksmith, "Items", "Item");
			sel.CreateRandomElement(rList, vItem.Axe, 3);
			sel.CreateRandomElement(rList, vItem.BraceletofStrength, 1);
			sel.CreateRandomElement(rList, vItem.Knife, 2);
			sel.CreateRandomElement(rList, vItem.MeleeDurabilitySpray, 3);
			sel.CreateRandomElement(rList, vItem.Sledgehammer, 1);
			sel.CreateRandomElement(rList, vItem.Sword, 3);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_Cannibal, "Items", "Item");
			sel.CreateRandomElement(rList, vItem.Axe, 3);
			sel.CreateRandomElement(rList, vItem.Beartrap, 3);
			sel.CreateRandomElement(rList, vItem.Beer, 3);
			sel.CreateRandomElement(rList, vItem.Rock, 3);
			sel.CreateRandomElement(rList, vItem.Whiskey, 3);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_ConsumerElectronics, "Items", "Item");
			sel.CreateRandomElement(rList, vItem.BoomBox, 2);
			sel.CreateRandomElement(rList, vItem.FoodProcessor, 1);
			sel.CreateRandomElement(rList, vItem.FriendPhone, 2);
			sel.CreateRandomElement(rList, vItem.MiniFridge, 1);
			sel.CreateRandomElement(rList, vItem.Translator, 1);
			sel.CreateRandomElement(rList, vItem.WalkieTalkie, 1);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_Contraband, "Items", "Item");
			sel.CreateRandomElement(rList, "Drugs2", 9);
			sel.CreateRandomElement(rList, "Gun1", 3);
			sel.CreateRandomElement(rList, "Gun2", 3);
			sel.CreateRandomElement(rList, "Melee1", 3);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_ConvenienceStore, "Items", "Item");
			sel.CreateRandomElement(rList, vItem.Banana, 2);
			sel.CreateRandomElement(rList, vItem.Beer, 3);
			sel.CreateRandomElement(rList, vItem.CigaretteLighter, 2);
			sel.CreateRandomElement(rList, vItem.Cigarettes, 3);
			sel.CreateRandomElement(rList, vItem.Fireworks, 1);
			sel.CreateRandomElement(rList, vItem.FriendPhone, 2);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_CopStandard, "Items", "Item");
			sel.CreateRandomElement(rList, vItem.BulletproofVest, 1);
			sel.CreateRandomElement(rList, vItem.DizzyGrenade, 1);
			sel.CreateRandomElement(rList, vItem.Pistol, 2);
			sel.CreateRandomElement(rList, vItem.PoliceBaton, 2);
			sel.CreateRandomElement(rList, vItem.Revolver, 1);
			sel.CreateRandomElement(rList, vItem.Shotgun, 1);
			sel.CreateRandomElement(rList, vItem.Taser, 1);
			sel.CreateRandomElement(rList, vItem.WalkieTalkie, 2);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_CopSWAT, "Items", "Item");
			sel.CreateRandomElement(rList, vItem.BulletproofVest, 4);
			sel.CreateRandomElement(rList, vItem.DizzyGrenade, 4);
			sel.CreateRandomElement(rList, vItem.DoorDetonator, 2);
			sel.CreateRandomElement(rList, vItem.GasMask, 2);
			sel.CreateRandomElement(rList, vItem.MachineGun, 3);
			sel.CreateRandomElement(rList, vItem.Shotgun, 3);
			sel.CreateRandomElement(rList, vItem.SkeletonKey, 1);
			sel.CreateRandomElement(rList, "WeaponMod", 3);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_Demolitionist, "Items", "Item");
			sel.CreateRandomElement(rList, vItem.BombProcessor, 1);
			sel.CreateRandomElement(rList, vItem.DoorDetonator, 2);
			sel.CreateRandomElement(rList, vItem.Fireworks, 1);
			sel.CreateRandomElement(rList, vItem.Grenade, 3);
			sel.CreateRandomElement(rList, vItem.LandMine, 2);
			sel.CreateRandomElement(rList, vItem.RemoteBombTrigger, 1);
			sel.CreateRandomElement(rList, vItem.RocketLauncher, 1);
			sel.CreateRandomElement(rList, vItem.TimeBomb, 1);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_DrugDealer, "Items", "Item"); // Vanilla
			sel.CreateRandomElement(rList, "Drugs", 3);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_Firefighter, "Items", "Item");
			sel.CreateRandomElement(rList, vItem.Axe, 1);
			sel.CreateRandomElement(rList, vItem.Crowbar, 1);
			sel.CreateRandomElement(rList, vItem.FireExtinguisher, 2);
			sel.CreateRandomElement(rList, vItem.FireproofSuit, 2);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_Gunsmith, "Items", "Item");
			sel.CreateRandomElement(rList, vItem.AccuracyMod, 2);
			sel.CreateRandomElement(rList, vItem.AmmoCapacityMod, 2);
			sel.CreateRandomElement(rList, vItem.AmmoProcessor, 2);
			sel.CreateRandomElement(rList, vItem.KillAmmunizer, 1);
			sel.CreateRandomElement(rList, vItem.MachineGun, 2);
			sel.CreateRandomElement(rList, vItem.Pistol, 3);
			sel.CreateRandomElement(rList, vItem.RateofFireMod, 2);
			sel.CreateRandomElement(rList, vItem.Revolver, 3);
			sel.CreateRandomElement(rList, vItem.Shotgun, 3);
			sel.CreateRandomElement(rList, vItem.Silencer, 2);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_HardwareStore, "Items", "Item");
			sel.CreateRandomElement(rList, vItem.Axe, 2);
			sel.CreateRandomElement(rList, vItem.Crowbar, 2);
			sel.CreateRandomElement(rList, vItem.FireExtinguisher, 1);
			sel.CreateRandomElement(rList, vItem.GasMask, 1);
			sel.CreateRandomElement(rList, vItem.HardHat, 3);
			sel.CreateRandomElement(rList, vItem.Knife, 2);
			sel.CreateRandomElement(rList, vItem.Leafblower, 1);
			sel.CreateRandomElement(rList, vItem.MeleeDurabilitySpray, 1);
			sel.CreateRandomElement(rList, vItem.Sledgehammer, 1);
			sel.CreateRandomElement(rList, vItem.WindowCutter, 1);
			sel.CreateRandomElement(rList, vItem.Wrench, 2);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_HighTech, "Items", "Item");
			sel.CreateRandomElement(rList, vItem.Blindenizer, 2);
			sel.CreateRandomElement(rList, vItem.EMPGrenade, 4);
			sel.CreateRandomElement(rList, vItem.Explodevice, 1);
			sel.CreateRandomElement(rList, vItem.GhostGibber, 2);
			sel.CreateRandomElement(rList, vItem.HackingTool, 4);
			sel.CreateRandomElement(rList, vItem.IdentifyWand, 2);
			sel.CreateRandomElement(rList, vItem.PortableSellOMatic, 1);
			sel.CreateRandomElement(rList, vItem.MemoryMutilator, 2);
			sel.CreateRandomElement(rList, vItem.ResearchGun, 2);
			sel.CreateRandomElement(rList, vItem.SafeBuster, 2);
			sel.CreateRandomElement(rList, vItem.Translator, 3);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_Hypnotist, "Items", "Item");
			sel.CreateRandomElement(rList, vItem.Cologne, 1);
			sel.CreateRandomElement(rList, vItem.Haterator, 2);
			sel.CreateRandomElement(rList, vItem.Hypnotizer, 2);
			sel.CreateRandomElement(rList, vItem.HypnotizerII, 2);
			sel.CreateRandomElement(rList, vItem.MemoryMutilator, 2);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_JunkDealer, "Items", "Item");
			sel.CreateRandomElement(rList, vItem.BananaPeel, 8);
			sel.CreateRandomElement(rList, vItem.BaseballBat, 2);
			sel.CreateRandomElement(rList, vItem.CardboardBox, 2);
			sel.CreateRandomElement(rList, vItem.Codpiece, 2);
			sel.CreateRandomElement(rList, vItem.FourLeafClover, 1);
			sel.CreateRandomElement(rList, vItem.FreeItemVoucher, 1);
			sel.CreateRandomElement(rList, vItem.HiringVoucher, 1);
			sel.CreateRandomElement(rList, vItem.Rock, 8);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_McFuds, "Items", "Item");
			sel.CreateRandomElement(rList, vItem.Fud, 3);
			sel.CreateRandomElement(rList, vItem.HotFud, 3);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_MedicalSupplier, "Items", "Item");
			sel.CreateRandomElement(rList, vItem.Antidote, 2);
			sel.CreateRandomElement(rList, vItem.BloodBag, 3);
			sel.CreateRandomElement(rList, vItem.FirstAidKit, 2);
			sel.CreateRandomElement(rList, vItem.Knife, 2);
			sel.CreateRandomElement(rList, vItem.Syringe, 3);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_MiningGear, "Items", "Item");
			sel.CreateRandomElement(rList, vItem.BombProcessor, 1);
			sel.CreateRandomElement(rList, vItem.Fud, 3);
			sel.CreateRandomElement(rList, vItem.GasMask, 1);
			sel.CreateRandomElement(rList, vItem.HardHat, 4);
			sel.CreateRandomElement(rList, vItem.RemoteBombTrigger, 1);
			sel.CreateRandomElement(rList, vItem.Sledgehammer, 4);
			sel.CreateRandomElement(rList, vItem.TimeBomb, 1);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_MonkeMart, "Items", "Item");
			sel.CreateRandomElement(rList, vItem.Banana, 6);
			sel.CreateRandomElement(rList, vItem.Lockpick, 6);
			sel.CreateRandomElement(rList, vItem.MonkeyBarrel, 1);
			//sel.CreateRandomElement(rList, vItem., 2); // Hologram Bigfoot missing from list
			sel.CreateRandomElement(rList, vItem.Translator, 1);
			sel.CreateRandomElement(rList, vItem.Wrench, 2);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_MovieTheater, "Items", "Item");
			// TODO: Find this list

			rList = sel.CreateRandomList(CTrait.AI_Vendor_Occultist, "Items", "Item");
			sel.CreateRandomElement(rList, vItem.BloodBag, 3);
			sel.CreateRandomElement(rList, vItem.BooUrn, 1);
			sel.CreateRandomElement(rList, vItem.Cologne, 1);
			sel.CreateRandomElement(rList, vItem.CubeOfLampey, 1);
			sel.CreateRandomElement(rList, vItem.GhostGibber, 1);
			sel.CreateRandomElement(rList, vItem.Knife, 1);
			sel.CreateRandomElement(rList, vItem.Necronomicon, 1);
			sel.CreateRandomElement(rList, vItem.ResurrectionShampoo, 1);
			sel.CreateRandomElement(rList, vItem.Sword, 1);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_OutdoorOutfitter, "Items", "Item");
			sel.CreateRandomElement(rList, vItem.Axe, 1);
			sel.CreateRandomElement(rList, vItem.Beer, 1);
			sel.CreateRandomElement(rList, vItem.CigaretteLighter, 1);
			sel.CreateRandomElement(rList, vItem.Beartrap, 1);
			sel.CreateRandomElement(rList, vItem.Fireworks, 1);
			sel.CreateRandomElement(rList, vItem.OilContainer, 1);
			sel.CreateRandomElement(rList, vItem.ParalyzerTrap, 1);
			sel.CreateRandomElement(rList, vItem.Revolver, 1);
			sel.CreateRandomElement(rList, vItem.Whiskey, 1);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_PawnShop, "Items", "Item");
			sel.CreateRandomElement(rList, vItem.BaseballBat, 1);
			sel.CreateRandomElement(rList, vItem.BoomBox, 2);
			sel.CreateRandomElement(rList, vItem.Crowbar, 1);
			sel.CreateRandomElement(rList, vItem.FoodProcessor, 1);
			sel.CreateRandomElement(rList, vItem.FriendPhone, 1);
			sel.CreateRandomElement(rList, vItem.GasMask, 1);
			sel.CreateRandomElement(rList, vItem.HackingTool, 1);
			sel.CreateRandomElement(rList, vItem.HardHat, 1);
			sel.CreateRandomElement(rList, vItem.Knife, 1);
			sel.CreateRandomElement(rList, vItem.Leafblower, 2);
			sel.CreateRandomElement(rList, vItem.MiniFridge, 1);
			sel.CreateRandomElement(rList, vItem.Pistol, 2);
			sel.CreateRandomElement(rList, vItem.Revolver, 2);
			sel.CreateRandomElement(rList, vItem.Shotgun, 1);
			sel.CreateRandomElement(rList, vItem.Taser, 1);
			sel.CreateRandomElement(rList, vItem.WalkieTalkie, 1);
			sel.CreateRandomElement(rList, vItem.Wrench, 1);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_PestControl, "Items", "Item");
			sel.CreateRandomElement(rList, vItem.Antidote, 2);
			sel.CreateRandomElement(rList, vItem.Beartrap, 3);
			sel.CreateRandomElement(rList, vItem.CyanidePill, 1);
			sel.CreateRandomElement(rList, vItem.Flamethrower, 1);
			sel.CreateRandomElement(rList, vItem.GasMask, 3);
			sel.CreateRandomElement(rList, vItem.KillProfiter, 1);
			sel.CreateRandomElement(rList, vItem.TranquilizerGun, 2);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_Pharmacy, "Items", "Item");
			sel.CreateRandomElement(rList, vItem.Antidote, 3);
			sel.CreateRandomElement(rList, vItem.Cigarettes, 3);
			sel.CreateRandomElement(rList, vItem.FirstAidKit, 3);
			sel.CreateRandomElement(rList, vItem.MusclyPill, 3);
			sel.CreateRandomElement(rList, vItem.ResurrectionShampoo, 1);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_ResistanceCommissary, "Items", "Item");
			sel.CreateRandomElement(rList, vItem.FreeItemVoucher, 3);
			sel.CreateRandomElement(rList, vItem.HiringVoucher, 3);
			sel.CreateRandomElement(rList, vItem.QuickEscapeTeleporter, 2);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_RiotDepot, "Items", "Item");
			sel.CreateRandomElement(rList, vItem.BaseballBat, 3);
			sel.CreateRandomElement(rList, vItem.CigaretteLighter, 1);
			sel.CreateRandomElement(rList, vItem.Knife, 3);
			sel.CreateRandomElement(rList, vItem.MolotovCocktail, 4);
			sel.CreateRandomElement(rList, vItem.OilContainer, 2);
			sel.CreateRandomElement(rList, vItem.RagePoison, 1);
			sel.CreateRandomElement(rList, vItem.Rock, 4);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_Scientist, "Items", "Item");
			sel.CreateRandomElement(rList, vItem.FreezeRay, 1);
			sel.CreateRandomElement(rList, vItem.GhostGibber, 1);
			sel.CreateRandomElement(rList, vItem.IdentifyWand, 1);
			sel.CreateRandomElement(rList, vItem.ShrinkRay, 1);
			sel.CreateRandomElement(rList, vItem.Syringe, 3);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_Shopkeeper, "Items", "Item"); // Vanilla
			sel.CreateRandomElement(rList, "Food", 10);
			sel.CreateRandomElement(rList, "Medical2", 5);
			sel.CreateRandomElement(rList, "Gun1", 3);
			sel.CreateRandomElement(rList, "Gun2", 2);
			sel.CreateRandomElement(rList, "Melee1", 3);
			sel.CreateRandomElement(rList, "Melee2", 2);
			sel.CreateRandomElement(rList, "Armor1", 2);
			sel.CreateRandomElement(rList, "Armor2", 2);
			sel.CreateRandomElement(rList, "Armor3", 1);
			sel.CreateRandomElement(rList, "Everyday1", 2);
			sel.CreateRandomElement(rList, "Tech2", 1);
			sel.CreateRandomElement(rList, "Tool1", 1);
			sel.CreateRandomElement(rList, "Tool2", 1);
			sel.CreateRandomElement(rList, "Tool3", 1);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_SlaveShop, "Items", "Item");
			sel.CreateRandomElement(rList, vItem.Axe, 2);
			sel.CreateRandomElement(rList, vItem.Codpiece, 2);
			sel.CreateRandomElement(rList, vItem.DizzyGrenade, 2);
			sel.CreateRandomElement(rList, vItem.FreezeRay, 1);
			sel.CreateRandomElement(rList, vItem.Sword, 2);
			sel.CreateRandomElement(rList, vItem.SlaveHelmetRemover, 2);
			sel.CreateRandomElement(rList, vItem.Taser, 1);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_Soldier, "Items", "Item"); // Vanilla
			sel.CreateRandomElement(rList, "Gun2", 3);
			sel.CreateRandomElement(rList, "Gun3", 3);
			sel.CreateRandomElement(rList, "WeaponMod", 1);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_SportingGoods, "Items", "Item");
			sel.CreateRandomElement(rList, vItem.BaseballBat, 2);
			sel.CreateRandomElement(rList, vItem.Codpiece, 2);
			sel.CreateRandomElement(rList, vItem.FirstAidKit, 2);
			sel.CreateRandomElement(rList, vItem.MusclyPill, 2);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_Teleportationist, "Items", "Item");
			sel.CreateRandomElement(rList, vItem.AmmoStealer, 2);
			sel.CreateRandomElement(rList, vItem.BodySwapper, 2);
			sel.CreateRandomElement(rList, vItem.QuickEscapeTeleporter, 2);
			sel.CreateRandomElement(rList, vItem.WallBypasser, 2);
			sel.CreateRandomElement(rList, vItem.WarpGrenade, 2);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_Thief, "Items", "Item"); // Vanilla
			sel.CreateRandomElement(rList, "Crowbar", 3);
			sel.CreateRandomElement(rList, "Lockpick", 3);
			sel.CreateRandomElement(rList, "SafeBuster", 3);
			sel.CreateRandomElement(rList, "CardboardBox", 3);
			sel.CreateRandomElement(rList, "WallBypasser", 3);
			sel.CreateRandomElement(rList, "WindowCutter", 3);
			sel.CreateRandomElement(rList, "BodySwapper", 3);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_ToyStore, "Items", "Item");
			sel.CreateRandomElement(rList, vItem.BananaPeel, 1);
			sel.CreateRandomElement(rList, vItem.Blindenizer, 1);
			sel.CreateRandomElement(rList, vItem.EarWarpWhistle, 1);
			sel.CreateRandomElement(rList, vItem.Shuriken, 1);
			sel.CreateRandomElement(rList, vItem.WalkieTalkie, 1);
			sel.CreateRandomElement(rList, vItem.WaterPistol, 1);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_UpperCruster, "Items", "Item"); // Vanilla
			sel.CreateRandomElement(rList, "Cologne", 3);
			sel.CreateRandomElement(rList, "Cocktail", 3);
			sel.CreateRandomElement(rList, "ResurrectionShampoo", 1);
			sel.CreateRandomElement(rList, "BraceletStrength", 1);
			sel.CreateRandomElement(rList, "FriendPhone", 1);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_Vampire, "Items", "Item");
			sel.CreateRandomElement(rList, vItem.BloodBag, 6);
			sel.CreateRandomElement(rList, vItem.Cologne, 1);
			sel.CreateRandomElement(rList, vItem.CardboardBox, 1);
			sel.CreateRandomElement(rList, vItem.MemoryMutilator, 1);
			sel.CreateRandomElement(rList, vItem.Sword, 3);

			rList = sel.CreateRandomList(CTrait.AI_Vendor_Villain, "Items", "Item");
			sel.CreateRandomElement(rList, vItem.CyanidePill, 3);
			sel.CreateRandomElement(rList, vItem.Explodevice, 3);
			sel.CreateRandomElement(rList, vItem.Giantizer, 3);
			sel.CreateRandomElement(rList, vItem.Necronomicon, 3);
			sel.CreateRandomElement(rList, vItem.RagePoison, 3);
			sel.CreateRandomElement(rList, vItem.TimeBomb, 3);
		}
	}
}
