using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Teleportationist : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.AmmoStealer, 3),
			new KeyValuePair<string, int>( VItemName.BodySwapper, 3),
			new KeyValuePair<string, int>( VItemName.QuickEscapeTeleporter, 3),
			new KeyValuePair<string, int>( VItemName.WallBypasser, 3),
			new KeyValuePair<string, int>( VItemName.WarpGrenade, 3),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Teleportationist>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells teleportation tools. Get them before they're gone!"),
					[LanguageCode.Spanish] = "Este NPC vende tecnologia de teletransporte, Compra antes que se desaparesca!",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Teleportationist)),
					[LanguageCode.Spanish] = "Teleportacionista",

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
