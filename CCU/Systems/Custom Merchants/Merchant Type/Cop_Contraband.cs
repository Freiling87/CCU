using CCU.Traits.Trait_Gate;
using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Cop_Contraband : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.Syringe, 6),
			new KeyValuePair<string, int>( VItemName.MusclyPill, 6),
			new KeyValuePair<string, int>( VItemName.Sugar, 6),
			new KeyValuePair<string, int>( VItemName.Pistol, 3),
			new KeyValuePair<string, int>( VItemName.MachineGun, 3),
			new KeyValuePair<string, int>( VItemName.Revolver, 3),
			new KeyValuePair<string, int>( VItemName.Shotgun, 3),
			new KeyValuePair<string, int>( VItemName.Taser, 3),
			new KeyValuePair<string, int>( VItemName.Knife, 3),
			new KeyValuePair<string, int>( VItemName.BaseballBat, 3),
			new KeyValuePair<string, int>( VItemName.Crowbar, 3),
			new KeyValuePair<string, int>( VItemName.BaseballBat, 3),
			new KeyValuePair<string, int>( VItemName.Sledgehammer, 1),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Cop_Contraband>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Stuff confiscated from the City's Ne'er- and/or Rarely-Do-Wells.\n\n" +
						"<color=green>{0}</color> = Player needs The Law to access shop", LongishDocumentationName(typeof(Cop_Access))),
					[LanguageCode.Spanish] = String.Format("Este NPC vende cosas confiscadas de personas malas, que posiblemente eran mejor que tu.\n\n" +
						"<color=green>{0}</color> = Jugador necesita el rasgo La Ley para acceder a la tienda", LongishDocumentationName(typeof(Cop_Access))),
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Cop_Contraband), "Cop (Contraband)"),
					[LanguageCode.Spanish] = "Contrabando",

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
