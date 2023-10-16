using BunnyLibs;
using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Cop_SWAT : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.BulletproofVest, 4),
			new KeyValuePair<string, int>( VItemName.DizzyGrenade, 4),
			new KeyValuePair<string, int>( VItemName.DoorDetonator, 4),
			new KeyValuePair<string, int>( VItemName.GasMask, 3),
			new KeyValuePair<string, int>( VItemName.WindowCutter, 3),
			new KeyValuePair<string, int>( VItemName.MachineGun, 4),
			new KeyValuePair<string, int>( VItemName.Shotgun, 4),
			new KeyValuePair<string, int>( VItemName.SkeletonKey, 1),
			new KeyValuePair<string, int>( VItemName.AccuracyMod, 2),
			new KeyValuePair<string, int>( VItemName.AmmoCapacityMod, 2),
			new KeyValuePair<string, int>( VItemName.Silencer, 2),
			new KeyValuePair<string, int>( VItemName.RateofFireMod, 2),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Cop_SWAT>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells gear for Doorkickers."),
					[LanguageCode.Spanish] = "Este NPC vende herramientas para rompepurtas, rompebunkers y rompebo-",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Cop_SWAT), "Cop (SWAT)"),
					[LanguageCode.Spanish] = "Poliswat",

				})
				.WithUnlock(new TraitUnlock_CCU
				{
					Cancellations = { },
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
