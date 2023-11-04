using RogueLibsCore;

namespace CCU.Traits.App_BC1
{
	public class Yellow_Body : T_BodyColor
	{
		public override string[] Rolls => new string[] { "Blonde" };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Yellow_Body>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds this item to the appearance pool.",
					[LanguageCode.Spanish] = "Agrega este color de cuerpo a los que el personaje puede usar... Ni los simpsons tenian tanta enfermedad.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Yellow_Body)),
					[LanguageCode.Spanish] = "Cuerpo Amarillo ",
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
