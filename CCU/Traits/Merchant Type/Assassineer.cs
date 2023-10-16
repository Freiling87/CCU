using BunnyLibs;
using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Assassineer : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.Blindenizer, 1),
			new KeyValuePair<string, int>( VItemName.CardboardBox, 1),
			new KeyValuePair<string, int>( VItemName.EarWarpWhistle, 1),
			new KeyValuePair<string, int>( VItemName.KillProfiter, 1),
			new KeyValuePair<string, int>( VItemName.Shuriken, 4),
			new KeyValuePair<string, int>( VItemName.Silencer, 2),
			new KeyValuePair<string, int>( VItemName.Sword, 3),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Assassineer>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells anything an assassin could need."),
					[LanguageCode.Spanish] = "Este NPC vende herramientas para matar de una manera mas sutil.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Assassineer)),
					[LanguageCode.Spanish] = "Asesinerista",

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
