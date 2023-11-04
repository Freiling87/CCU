using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Pest_Control : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.Antidote, 3),
			new KeyValuePair<string, int>( VItemName.Beartrap, 3),
			new KeyValuePair<string, int>( VItemName.CyanidePill, 3),
			new KeyValuePair<string, int>( VItemName.Flamethrower, 1),
			new KeyValuePair<string, int>( VItemName.GasMask, 3),
			new KeyValuePair<string, int>( VItemName.KillProfiter, 1),
			new KeyValuePair<string, int>( VItemName.Taser, 1),
			new KeyValuePair<string, int>( VItemName.TranquilizerGun, 1),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Pest_Control>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells chemicals and tools for exterminating or subduing pests."),
					[LanguageCode.Spanish] = "Este NPC vende herramientas para exterminar todo tipo de pestes, incluyendo la chusma.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Pest_Control)),
					[LanguageCode.Spanish] = "Controls de Peste",

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
