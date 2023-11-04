using RogueLibsCore;

namespace CCU.Traits.App_BC1
{
	public class Werewolf_Colored_Body : T_BodyColor
	{
		public override string[] Rolls => new string[] { "WerewolfSkin" };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Werewolf_Colored_Body>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds this item to the appearance pool. Werewolf is a color now.",
					[LanguageCode.Spanish] = "Agrega este color de cuerpo a los que el personaje puede usar.. Los Lobos son mi segundo color favorito :D",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Werewolf_Colored_Body)),
					[LanguageCode.Spanish] = "Cuerpo Color Lobo ",
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
