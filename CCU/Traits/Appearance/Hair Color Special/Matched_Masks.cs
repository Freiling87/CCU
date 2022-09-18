using CCU.Traits.App_HC1;
using RogueLibsCore;

namespace CCU.Traits.App_HC3
{
	public class Matched_Masks : T_HairColor
	{
		public override string[] Rolls => new string[] { };

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Matched_Masks>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "If a non-hair hairstyle (Hoodie, Gorilla, etc.) is rolled, matches its color to Body Color.\n\n" + 
					"<color=red>Note:</color> Requires at least one Hair Color trait, or else hair will be randomized among body colors.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Matched_Masks)),
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
