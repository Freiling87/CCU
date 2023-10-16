using BunnyLibs;
using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Slaves_Shop : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.Banana, 3),
			new KeyValuePair<string, int>( VItemName.HackingTool, 3),
			new KeyValuePair<string, int>( VItemName.Lockpick, 3),
			new KeyValuePair<string, int>( VItemName.SlaveHelmetRemover, 3),
			new KeyValuePair<string, int>( VItemName.Wrench, 3),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Slaves_Shop>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells gear for slaves.\n\nOh, you wanted the Slave Shop? Yeah, we don't get many customers."),
					[LanguageCode.Spanish] = "Este NPC vende items que pueden ayudar a esclavos.\n\nSi la verdad no tenemos muchos clientes.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Slaves_Shop), "Slaves' Shop"),
					[LanguageCode.Spanish] = "Tienda para Esclavos",

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
