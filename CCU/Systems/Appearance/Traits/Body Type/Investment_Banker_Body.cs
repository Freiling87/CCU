using RogueLibsCore;

namespace CCU.Traits.App_BT1
{
	public class Investment_Banker_Body : T_BodyType
	{
		public override string[] Rolls => new string[] { VanillaAgents.InvestmentBanker };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Investment_Banker_Body>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds this item to the appearance pool.",
					[LanguageCode.Spanish] = "Agrega este cuerpo a los que el personaje puede usar.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Investment_Banker_Body)),
					[LanguageCode.Spanish] = "Cuerpo de Banquero",
				})
				.WithUnlock(new TU_DesignerUnlock
				{
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		
		
	}
}
