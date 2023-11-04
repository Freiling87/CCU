using RogueLibsCore;

namespace CCU.Traits.Hire_Duration
{
	public class Permanent_Hire_Only : T_HireDuration
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Permanent_Hire_Only>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Offers Permanent Hire, AND removes the regular hire option.\n\n" +
						"Permanent Hires can use their Expert ability unlimited times, and will ignore Homesickness.\n\n" +
						"<color=red>Requires:</color> Any Hire Type trait",
					[LanguageCode.English] = "Remplaza la opcion de asistencia con la de contratar permanentemente.\n\n" +
						"NPCs contratadors permanentemente pueden usar sus abilidades sin limites y siempre te seguiran.\n\n" +
						"<color=red>Require:</color> Cualquier Rasgo de Empleo",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Permanent_Hire_Only)),
					[LanguageCode.Spanish] = "Solo Contrato Permanente",
				})
				.WithUnlock(new TU_DesignerUnlock
				{
					Cancellations = { },
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		
		
	}
}
