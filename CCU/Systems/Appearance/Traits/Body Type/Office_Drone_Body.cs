using RogueLibsCore;

namespace CCU.Traits.App_BT1
{
	public class Office_Drone_Body : T_BodyType
	{
		public override string[] Rolls => new string[] { VanillaAgents.OfficeDrone };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Office_Drone_Body>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds this item to the appearance pool.",
					[LanguageCode.Spanish] = "Agrega este cuerpo a los que el personaje puede usar.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Office_Drone_Body)),
					[LanguageCode.Spanish] = "Cuerpo de Zángano de Oficina",
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
