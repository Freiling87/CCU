using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Research_Materials : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.FreezeRay, 1),
			new KeyValuePair<string, int>( VItemName.GhostGibber, 1),
			new KeyValuePair<string, int>( VItemName.IdentifyWand, 2),
			new KeyValuePair<string, int>( VItemName.ShrinkRay, 1),
			new KeyValuePair<string, int>( VItemName.Syringe, 4),
			new KeyValuePair<string, int>( VItemName.TranquilizerGun, 1),
			new KeyValuePair<string, int>( VItemName.WaterPistol, 1),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Research_Materials>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells chemicals and experimental tools."),
					[LanguageCode.Spanish] = "Este NPC vende quimicos y armas experimentales.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Research_Materials)),
					[LanguageCode.Spanish] = "Recursos de Estudio",

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
