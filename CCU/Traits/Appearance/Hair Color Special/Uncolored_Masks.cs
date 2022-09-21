using CCU.Traits.App_HC1;
using RogueLibsCore;

namespace CCU.Traits.App_HC3
{
	public class Uncolored_Masks : T_HairColor
	{
		public override string[] Rolls => new string[] { };

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Uncolored_Masks>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "If a Mask is rolled, matches its color to Body Color.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Uncolored_Masks)),
				})
				.WithUnlock(new TraitUnlock
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
