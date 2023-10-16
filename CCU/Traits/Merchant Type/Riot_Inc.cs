using BunnyLibs;
using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Riot_Inc : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.BaseballBat, 3),
			new KeyValuePair<string, int>( VItemName.CigaretteLighter, 1),
			new KeyValuePair<string, int>( VItemName.CubeOfLampey, 1),
			new KeyValuePair<string, int>( VItemName.Knife, 3),
			new KeyValuePair<string, int>( VItemName.MolotovCocktail, 4),
			new KeyValuePair<string, int>( VItemName.OilContainer, 2),
			new KeyValuePair<string, int>( VItemName.RagePoison, 1),
			new KeyValuePair<string, int>( VItemName.Rock, 4),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Riot_Inc>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells anything a rioter could want."),
					[LanguageCode.Spanish] = "Este NPC vende herramientas de destrucion livianas perfectas para la revolucion de tu ideologia completamente apta!",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Riot_Inc), "Riot, Inc."),
					[LanguageCode.Spanish] = "Alboroto, Disturbios y Asociados",

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
