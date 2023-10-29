using RogueLibsCore;

namespace CCU.Traits.App_BT1
{
	public class Werewolf_Body : T_BodyType
	{
		public override string[] Rolls => new string[] { VanillaAgents.WerewolfTransformed };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Werewolf_Body>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds this item to the appearance pool.",
					[LanguageCode.Spanish] = "Agrega este cuerpo a los que el personaje puede usar.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Werewolf_Body)),
					[LanguageCode.Spanish] = "Cuerpo de Hombre Lobo",
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
