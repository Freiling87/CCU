using RogueLibsCore;

namespace CCU.Traits.App_BT1
{
	public class Resistance_Leader_Body : T_BodyType
	{
		public override string[] Rolls => new string[] { VanillaAgents.ResistanceLeader };

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Resistance_Leader_Body>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds this item to the appearance pool.",
					[LanguageCode.Spanish] = "Agrega este cuerpo a los que el personaje puede usar.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Resistance_Leader_Body)),
					[LanguageCode.Spanish] = "Cuerpo de Lider de la Resistencia",
				})
				.WithUnlock(new TraitUnlock_CCU
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
