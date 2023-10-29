using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Hardware_Store : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.Axe, 2),
			new KeyValuePair<string, int>( VItemName.Crowbar, 3),
			new KeyValuePair<string, int>( VItemName.FireExtinguisher, 1),
			new KeyValuePair<string, int>( VItemName.GasMask, 2),
			new KeyValuePair<string, int>( VItemName.HardHat, 4),
			new KeyValuePair<string, int>( VItemName.Knife, 2),
			new KeyValuePair<string, int>( VItemName.Leafblower, 2),
			new KeyValuePair<string, int>( VItemName.MeleeDurabilitySpray, 3),
			new KeyValuePair<string, int>( VItemName.Sledgehammer, 2),
			new KeyValuePair<string, int>( VItemName.WindowCutter, 1),
			new KeyValuePair<string, int>( VItemName.Wrench, 3),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Hardware_Store>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells tools."),
					[LanguageCode.Spanish] = "Este NPC vende herramientas, solo herramientas nada muy tematico.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Hardware_Store)),
					[LanguageCode.Spanish] = "Ferreteria",

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
