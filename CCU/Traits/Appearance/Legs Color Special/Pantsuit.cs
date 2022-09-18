using CCU.Traits.App_LC1;
using RogueLibsCore;

namespace CCU.Traits.App_LC3
{
	public class Pantsuit : T_LegsColor
	{
		public override string[] Rolls => new string[] { };

        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Pantsuit>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Causes legs to match body color.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Pantsuit)),
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
