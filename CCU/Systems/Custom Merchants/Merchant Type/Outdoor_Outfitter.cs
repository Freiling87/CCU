using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Outdoor_Outfitter : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.Axe, 1),
			new KeyValuePair<string, int>( VItemName.Beer, 1),
			new KeyValuePair<string, int>( VItemName.CigaretteLighter, 1),
			new KeyValuePair<string, int>( VItemName.Beartrap, 2),
			new KeyValuePair<string, int>( VItemName.Fireworks, 1),
			new KeyValuePair<string, int>( VItemName.FirstAidKit, 2),
			new KeyValuePair<string, int>( VItemName.OilContainer, 1),
			new KeyValuePair<string, int>( VItemName.ParalyzerTrap, 1),
			new KeyValuePair<string, int>( VItemName.Revolver, 2),
			new KeyValuePair<string, int>( VItemName.Shotgun, 1),
			new KeyValuePair<string, int>( VItemName.Whiskey, 1),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Outdoor_Outfitter>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells traps & wilderness gear. And fireworks, because yee-haw!"),
					[LanguageCode.Spanish] = "Este NPC vende herramientas para sobrevivir el exterior, y fuegos artificiales, viva el patriotismo y muerte a los bosques!",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Outdoor_Outfitter)),
					[LanguageCode.Spanish] = "Sobrevivencia al Externo",

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
