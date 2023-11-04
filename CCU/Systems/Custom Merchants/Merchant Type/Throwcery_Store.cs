using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Throwcery_Store : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.BananaPeel, 4),
			new KeyValuePair<string, int>( VItemName.KillerThrower, 2),
			new KeyValuePair<string, int>( VItemName.Rock, 4),
			new KeyValuePair<string, int>( VItemName.Shuriken, 6),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Throwcery_Store>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells throwing weapons & Killer Thrower."),
					[LanguageCode.Spanish] = "Este NPC vende cosas que puedes tirar, y cosas para mejorar tu abilidad en tirar.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Throwcery_Store)),
					[LanguageCode.Spanish] = "Tiradera",

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
		
		
	}
}
