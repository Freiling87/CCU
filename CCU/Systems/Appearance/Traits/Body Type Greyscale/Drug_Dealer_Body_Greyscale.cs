using CCU.Traits.App_BT1;
using RogueLibsCore;

namespace CCU.Traits.App_BT2
{
	public class Drug_Dealer_Body_Greyscale : T_BodyType
	{
		public override string[] Rolls => new string[] { "G_" + VanillaAgents.DrugDealer };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Drug_Dealer_Body_Greyscale>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds this item to the appearance pool.",
					[LanguageCode.Spanish] = "Agrega este cuerpo a los que el personaje puede usar.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Drug_Dealer_Body_Greyscale)),
					[LanguageCode.Spanish] = "Cuerpo de Traficante de Drogas Gris",
				})
				.WithUnlock(new TU_DesignerUnlock
				{
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
