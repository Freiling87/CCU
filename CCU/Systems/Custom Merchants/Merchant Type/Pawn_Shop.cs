using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Pawn_Shop : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.BaseballBat, 1),
			new KeyValuePair<string, int>( VItemName.BoomBox, 2),
			new KeyValuePair<string, int>( VItemName.Crowbar, 1),
			new KeyValuePair<string, int>( VItemName.FoodProcessor, 1),
			new KeyValuePair<string, int>( VItemName.FriendPhone, 1),
			new KeyValuePair<string, int>( VItemName.GasMask, 1),
			new KeyValuePair<string, int>( VItemName.HackingTool, 1),
			new KeyValuePair<string, int>( VItemName.HardHat, 1),
			new KeyValuePair<string, int>( VItemName.Knife, 1),
			new KeyValuePair<string, int>( VItemName.Leafblower, 2),
			new KeyValuePair<string, int>( VItemName.MiniFridge, 1),
			new KeyValuePair<string, int>( VItemName.Pistol, 2),
			new KeyValuePair<string, int>( VItemName.Revolver, 2),
			new KeyValuePair<string, int>( VItemName.Shotgun, 1),
			new KeyValuePair<string, int>( VItemName.Taser, 1),
			new KeyValuePair<string, int>( VItemName.WalkieTalkie, 1),
			new KeyValuePair<string, int>( VItemName.Wrench, 2),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Pawn_Shop>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells various goods of low value."),
					[LanguageCode.Spanish] = "Este NPC vende varios bienes baratos.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Pawn_Shop)),
					[LanguageCode.Spanish] = "Casa de Empeño",

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
