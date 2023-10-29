using RogueLibsCore;

namespace CCU.Traits.Hire_Duration
{
	public class Permanent_Hire : T_HireDuration
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Permanent_Hire>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] =
						"Adds a 'Hire Permanently' interaction option at 8x the normal rate.\n\n" +
						"Permanent Hires can use their Expert ability unlimited times, and will ignore Homesickness.\n\n" +
						"<color=red>Requires:</color> Any Hire Type trait",
					[LanguageCode.Spanish] =
						"Agrega la opcion de 'Contratar Permanentemente' a un NPC al 8x del costo normal.\n\n" +
						"NPCs contratadors permanentemente pueden usar sus abilidades sin limites y siempre te seguiran.\n\n" +
						"<color=red>Require:</color> Cualquier Rasgo de Empleo",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Permanent_Hire)),
					[LanguageCode.Spanish] = "Contrato Permanente",
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
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}
