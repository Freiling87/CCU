using RogueLibsCore;

namespace CCU.Traits.App_BC1
{
	// Named awkwardly to avoid overlap with same-name body type
	public class Gorilla_Colored_Body : T_BodyColor
	{
		public override string[] Rolls => new string[] { "GorillaSkin" };

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Gorilla_Colored_Body>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds this item to the appearance pool. Gorilla is a color now.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Gorilla_Colored_Body)),
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
