using RogueLibsCore;

namespace CCU.Traits.App_BT1
{
	public class Shapeshifter_Body : T_BodyType
	{
		public override string[] Rolls => new string[] { VanillaAgents.ShapeShifter };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Shapeshifter_Body>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds this item to the appearance pool.",
					[LanguageCode.Spanish] = "Agrega este cuerpo a los que el personaje puede usar.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Shapeshifter_Body)),
					[LanguageCode.Spanish] = "Cuerpo de Cambiaformas",
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
