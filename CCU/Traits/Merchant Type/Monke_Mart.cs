using BunnyLibs;
using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Monke_Mart : T_MerchantType
	{
		public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
		{
			new KeyValuePair<string, int>( VItemName.Banana, 12),
			new KeyValuePair<string, int>( VItemName.Lockpick, 3),
			new KeyValuePair<string, int>( VItemName.MonkeyBarrel, 3),
			new KeyValuePair<string, int>( VItemName.HologramBigfoot, 3),
			new KeyValuePair<string, int>( VItemName.Translator, 3),
			new KeyValuePair<string, int>( VItemName.Wrench, 3),
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Monke_Mart>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character sells stuff for monke, gorgia, you name it.\n\nThanks for coming, please return (to monke)!"),
					[LanguageCode.Spanish] = "Este NPC vende items relacionados a los gorilas, monos, el parque? no se lo que sea.\n\nDales gracias tenes television a color y la pildora anticonceptiva."

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Monke_Mart)),
					[LanguageCode.Spanish] = "Mercado de Monos",

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
