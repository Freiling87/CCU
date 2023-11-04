using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Cop_Patrol : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.BulletproofVest, 2),
			new KeyValuePair<string, int>( VItemName.Pistol, 2),
			new KeyValuePair<string, int>( VItemName.PoliceBaton, 2),
			new KeyValuePair<string, int>( VItemName.Revolver, 2),
			new KeyValuePair<string, int>( VItemName.Shotgun, 2),
			new KeyValuePair<string, int>( VItemName.Taser, 1),
			new KeyValuePair<string, int>( VItemName.WalkieTalkie, 2),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Cop_Patrol>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells standard-issue patrolman's gear."),
					[LanguageCode.Spanish] = "Este NPC vende equipamiento principal de policia.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Cop_Patrol), "Cop (Patrol)"),
					[LanguageCode.Spanish] = "Poli Patrullero",

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
