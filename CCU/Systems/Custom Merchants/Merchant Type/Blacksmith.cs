using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Blacksmith : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.Axe, 3),
			new KeyValuePair<string, int>( VItemName.BraceletofStrength, 1),
			new KeyValuePair<string, int>( VItemName.Knife, 3),
			new KeyValuePair<string, int>( VItemName.MeleeDurabilitySpray, 3),
			new KeyValuePair<string, int>( VItemName.Sledgehammer, 1),
			new KeyValuePair<string, int>( VItemName.Sword, 3),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Blacksmith>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells melee weapons and uh, related stuff."),
					[LanguageCode.Spanish] = "Este NPC vende armas de cuerpo a cuerpo y accesorios.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Blacksmith), "Blacksmith, Ye Olde"),
					[LanguageCode.Spanish] = "Eh Herrero",

				})
				.WithUnlock(new TU_DesignerUnlock
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
