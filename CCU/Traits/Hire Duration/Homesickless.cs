using RogueLibsCore;

namespace CCU.Traits.Hire_Duration
{
	public class Homesickless : T_HireDuration
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Homesickless>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent will always follow the player between levels.",
					[LanguageCode.Spanish] = "Este NPC te seguira hasta que mueras, que el muera o que simplemente te odie o le digas que se .",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Homesickless)),
					[LanguageCode.Spanish] = "Pegajoso Social",

				})
				.WithUnlock(new TraitUnlock_CCU
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
